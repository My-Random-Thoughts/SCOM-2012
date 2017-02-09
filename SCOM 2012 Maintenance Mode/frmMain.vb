Imports System.IO
Imports System.Text
Imports Microsoft.EnterpriseManagement
Imports Microsoft.EnterpriseManagement.Common
Imports Microsoft.EnterpriseManagement.Administration
Imports Microsoft.EnterpriseManagement.Configuration
Imports Microsoft.EnterpriseManagement.Monitoring
Imports System.Text.RegularExpressions
Imports System.Diagnostics
Imports System.Threading

Public Class frmMain
	Dim cPages As New Collection		' Collection of tab pages for showing and hiding
	Dim cServer As New Collection		' Collection of servers as retrieved from SCOM
	Public sTaskSaved As String = vbNullString

	' Load the SCOM DLLs
	Private Delegate Sub DLLUpdateStatus(bDone As Boolean)
	Private Delegate Sub DLLLoadDone(bDone As Boolean)

	' These numbers match the img16 icon numbers
	Private Enum MonitoringReturnStatus
		Success = 2
		Warning = 3
		Failed = 4
		None = 5
	End Enum

	' Structure to hold the currently loaded control settings
	Private Structure pSettings
		Shared sS_Server As String
		Shared sS_Window As String
		Shared sS_Comment As String
		Shared sS_AutoStart As Boolean
		Shared sS_CategoryC As Boolean
		Shared sS_CategoryI As Integer
		Shared sS_Disallowed As String
	End Structure

	' Structure to hold the defaults for the control settings
	Private Structure pDefaultSettings
		Shared sS_Server As String = "Enter Management Servers FQDN Here"
		Shared sS_Window As String = "60"
		Shared sS_Comment As String = "No Comment Given"
		Shared sS_CategoryC As Boolean = True
		Shared sS_CategoryI As Integer = 3
		Shared sS_Disallowed As String = vbNullString
	End Structure

	' Format: "Time Label", "Minutes", ...
	Public sPresetWindow() As String = ({"1 Hour", "60", "2 Hours", "120", "3 Hours", "180", "-", "-", "1 Day", "1440", "2 Days", "2880", "-", "-", "1 Week", "10080", "2 Weeks", "20160"})
	Private bCanAutoStart As Boolean = True
	Public sSettingsPath As String = Application.StartupPath & "\Settings.cfg"
	Private sLogFile As String = Application.StartupPath & "\AuditLogs\Audit Log (" & Date.Now.ToString("yyyy.MM.dd - HH.mm") & "  -  " & Environment.UserName & ").txt"
	Private lvwR As ListViewColumnSorter
	Private lvwS As ListViewColumnSorter
	Private swStopwatch As New Stopwatch

	' Custom Menu Items
	Private Const WM_SYSCOMMAND As Integer = &H112
	Private Const MF_BYPOSITION As Integer = &H400
	Private Const MF_SEPARATOR As Integer = &H800
	Private Const mnuCustom_About As Integer = 1000
	Private Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As IntPtr, ByVal bRevert As Long) As IntPtr
	Private Declare Function InsertMenu Lib "user32" Alias "InsertMenuA" (ByVal hMenu As IntPtr, ByVal nPosition As Integer, ByVal wFlags As Integer, ByVal wIDNewItem As Integer, ByVal lpNewItem As String) As Boolean

	' INI file handling
	Public Declare Auto Function GetPrivateProfileString Lib "kernel32" (ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
	Public Declare Auto Function WritePrivateProfileString Lib "kernel32" (ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

	' Start up Code
	Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		lvwR = New ListViewColumnSorter()
		lvwS = New ListViewColumnSorter()

		dtpEntry_Window.MinDate = Now.AddMinutes(10)
		pSettings.sS_AutoStart = False

		' Set version and add custom menu item
		Me.Icon = My.Resources.MainIcon
		Me.Text = "SCOM 2012 Maintenance Mode - v" & Application.ProductVersion
		Dim hSysMenu As IntPtr = GetSystemMenu(Me.Handle, CLng(False))
		InsertMenu(hSysMenu, 10, MF_BYPOSITION Or MF_SEPARATOR, 0, String.Empty)
		InsertMenu(hSysMenu, 11, MF_BYPOSITION, mnuCustom_About, "About...")

		' Set default control visuals
		lblMaint_WhatIfMode.Visible = False
		lblMaint_WhatIfMode.Font = New Font(lblMaint_WhatIfMode.Font.Name, lblMaint_WhatIfMode.Font.Size, FontStyle.Bold)
		picHelp.Image = My.Resources._13___Help.ToBitmap
		picSearch_Loading.Visible = False
		picSearch_Loading.Image = My.Resources._16___Loading

		txtEntry_Window.MaxLength = 5
		txtEntry_Comment.MaxLength = 255
		txtAdmin_DefaultsWindow.MaxLength = 5
		txtAdmin_DefaultsComment.MaxLength = 255
		lblSearch_ServerCount.Visible = False

		' Build SCOM Category dropdown control
		With cmoAdmin_Category
			.DropDownStyle = ComboBoxStyle.DropDownList
		End With
		With cmoEntry_Category
			.DropDownStyle = ComboBoxStyle.DropDownList
		End With
		chkAdmin_Category.Checked = True
		chkEntry_Category.Checked = True
		Call chkAdmin_Category_CheckedChanged(Nothing, Nothing)
		Call chkEntry_Category_CheckedChanged(Nothing, Nothing)

		' Build image list control
		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Connecting", My.Resources._16___Connecting)					' 0
				.Add("_16___Scan___Scanning", My.Resources._16___Scan___Scanning)		' 1
				.Add("_16___Scan___Pass", My.Resources._16___Scan___Pass)				' 2
				.Add("_16___Scan___Warning", My.Resources._16___Scan___Warning)			' 3
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)			' 4
				.Add("_16___Scan___Blank", My.Resources._16___Scan___Blank)				' 5
				.Add("_16___Scan___Maintenance", My.Resources._16___Scan___Maintenance)	' 6
				.Add("_16___Scan___NothingToDo", My.Resources._16___Scan___NothingToDo)	' 7

				.Add("E", My.Resources.E)	' Error				\
				.Add("W", My.Resources.W)	' Warning			 | These are all
				.Add("O", My.Resources.O)	' All OK			 > 12x12 icons in
				.Add("M", My.Resources.M)	' Maintenance		 | a 16x16 frame
				.Add("N", My.Resources.N)	' Not Monitored		/
			End With
		End With
		Application.DoEvents()

		' Build maintenance results list view control
		With lstMaint_Results
			.Clear()
			With .Columns
				.Add("name", "", lstMaint_Results.Width - 64 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("a", "", 32)
				.Add("b", "", 32)
				.Add("HIDDEN", "HIDDEN", 0)
			End With
			.FullRowSelect = True
			.GridLines = False
			.HeaderStyle = ColumnHeaderStyle.None
			.ListViewItemSorter = lvwR
			.MultiSelect = False
			.ShowGroups = True
			.SmallImageList = img16
			.View = View.Details
		End With
		lvwR.SortColumn = lstMaint_Results.Columns("name").Index
		lvwR.Order = SortOrder.Ascending

		With picMaint_COM
			.Image = My.Resources._16___COM.ToBitmap
			.Left = lstMaint_Results.Left + lstMaint_Results.Columns("name").Width + CInt(lstMaint_Results.Columns("a").Width / 2) - 8
		End With
		With picMaint_CLU
			.Image = My.Resources._16___CLU.ToBitmap
			.Left = picMaint_COM.Left + lstMaint_Results.Columns("a").Width
		End With

		Dim tt As New ToolTip
		tt.SetToolTip(picMaint_COM, "Computer Object Result")
		tt.SetToolTip(picMaint_CLU, "Cluster Resource Result")

		' Build search results list view control
		With lstSearch_Search
			.Clear()
			With .Columns
				.Add("name", "", lstSearch_Search.Width - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("HIDDEN", "HIDDEN", 0)
			End With
			.CheckBoxes = True
			.FullRowSelect = True
			.GridLines = False
			.HeaderStyle = ColumnHeaderStyle.None
			.ListViewItemSorter = lvwS
			.MultiSelect = False
			.ShowGroups = True
			.SmallImageList = img16
			.View = View.Details
		End With
		lvwS.SortColumn = lstSearch_Search.Columns("name").Index
		lvwS.Order = SortOrder.Ascending

		' Add all tab pages to a collection
		cPages.Add(tPages.TabPages("tpL"), "tpL")	' List
		cPages.Add(tPages.TabPages("tpO"), "tpO")	' Options
		cPages.Add(tPages.TabPages("tpR"), "tpR")	' Results
		cPages.Add(tPages.TabPages("tpS"), "tpS")	' Search
		cPages.Add(tPages.TabPages("tpA"), "tpA")	' Admin

		' Select the first tab
		tPages.SelectedTab = tPages.TabPages("tpL")

		' Remove tab pages not required yet
		tPages.Controls.Remove(tPages.TabPages("tpO"))
		tPages.Controls.Remove(tPages.TabPages("tpR"))
		tPages.Controls.Remove(tPages.TabPages("tpS"))
		tPages.Controls.Remove(tPages.TabPages("tpA"))

		' Check for "Microsoft.Win32.TaskScheduler.dll"
		chkEntry_Schedule.Enabled = My.Computer.FileSystem.FileExists(Application.StartupPath & "\Microsoft.Win32.TaskScheduler.dll")
		lblEntry_Schedule.Enabled = chkEntry_Schedule.Enabled

		' Load SCOM DLLs in the background
		picLoadingDLLs.Image = My.Resources._16___Loading
		lblLoadingDLLs.Font = New Font(lblLoadingDLLs.Font.Name, lblLoadingDLLs.Font.Size, FontStyle.Bold)
		Dim t As Thread
		t = New Thread(AddressOf DLLLoadStart)
		t.Start()

		' Show me and try to load the options
		Me.Visible = True
		Call loadOptions()
		Call txtEntry_Window_TextChanged(sender, e)
		Application.DoEvents()

		' Are we accepting command line options.?
		Dim sCommand As ReadOnlyCollection(Of String) = My.Application.CommandLineArgs
		If (sCommand.Count > 0) Then
			For Each sCmd As String In sCommand
				If sCmd.ToLower = "-admin" Then
					tPages.TabPages.Add(CType(cPages("tpA"), TabPage))
					tPages.SelectedTab = tPages.TabPages("tpA")
					txtAdmin_ManagementServer.Text = pSettings.sS_Server
					txtAdmin_DefaultsComment.Text = pSettings.sS_Comment
					txtAdmin_DefaultsWindow.Text = pSettings.sS_Window
					Call controlChange(False)
				Else
					If (Strings.Right(sCmd, 5).ToLower.Contains(".txt") = True) Then
						Call dragDropFile(sCmd)
					End If
				End If
			Next
		End If

		Call txtEntry_ServerList_TextChanged(Nothing, Nothing)
		Me.Cursor = Cursors.Default
	End Sub

	' #############################################################################################

#Region "THREADING: Loading SCOM DLL Files In Background"
	Private Class DLLLoad
		Public bUpdateStatus As DLLUpdateStatus
		Public bDLLDone As DLLLoadDone
		Public Sub LoadDLL(ByVal callback As Object)
			Try
				Dim mg As ManagementGroup = Nothing
				Dim bNull As Boolean = mg.IsConnected
			Catch
				Call bUpdateStatus(False)
			End Try
			Call bUpdateStatus(True)
		End Sub
	End Class
	Private Sub DLLLoadStart()
		Dim DLLScan As New DLLLoad
		DLLScan.bUpdateStatus = AddressOf DLLDone
		ThreadPool.QueueUserWorkItem(AddressOf DLLScan.LoadDLL)
	End Sub
	Private Sub DLLDone(bDone As Boolean)
		If lblLoadingDLLs.InvokeRequired Then
			Try
				lblLoadingDLLs.Invoke(New DLLUpdateStatus(AddressOf DLLResult), New Object() {bDone})
			Catch
			End Try
		Else
			DLLResult(bDone)
		End If
	End Sub
	Private Sub DLLResult(bDone As Boolean)
		panLoadingDLLs.Visible = Not bDone
	End Sub
#End Region

	' #############################################################################################

#Region "TAB: (tpL) Server List Entry"
	' Accepts drag drop requests from various controls
	Private Sub App_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop, txtEntry_ServerList.DragDrop, tpL.DragDrop
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			bCanAutoStart = False
			Dim droppedFiles() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
			For Each sFile As String In droppedFiles
				Call dragDropFile(sFile)
			Next
		End If
	End Sub

	' Changes mouse pointer to show drag drop request status
	Private Sub App_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter, txtEntry_ServerList.DragEnter, tpL.DragEnter
		If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
			e.Effect = DragDropEffects.Copy
		Else
			e.Effect = DragDropEffects.None
		End If
	End Sub

	''' <summary>
	''' Reads in a text file and processes each line
	''' </summary>
	''' <param name="sFileName">Full path the drag drop file</param>
	''' <remarks>Can be called more than once, each line passed to 'addServerEntry' for processing</remarks> 
	Private Sub dragDropFile(ByVal sFileName As String)
		Dim bError As Boolean = True
		Try
			Me.Cursor = Cursors.WaitCursor
			Using sr As StreamReader = New StreamReader(sFileName.Replace(Chr(34), ""))
				Do
					Call addServerEntry(sr.ReadLine())
				Loop Until sr.EndOfStream
			End Using

		Catch ex As Exception
			MessageBox.Show(Me, "The dropped file could not be opened." & vbCrLf & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

		Finally
			Me.Cursor = Cursors.Default
		End Try

		Call txtEntry_ServerList_TextChanged(Nothing, Nothing)
		Call txtEntry_ServerList_LostFocus(Nothing, Nothing)
		If ((bCanAutoStart = True) AndAlso (pSettings.sS_AutoStart = True)) Then
			Call cmdEntry_Start_Click(Me, New System.EventArgs)
		End If
	End Sub

	' Allows importing of a text file for processing
	Private Sub cmdEntry_Import_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Import.Click
		With dlgOpenFile
			.CheckFileExists = True
			.CheckPathExists = True
			.FileName = vbNullString
			.Filter = "Text Files (*.txt)|*.txt|All Files|*.*"
			.InitialDirectory = Application.StartupPath
			.SupportMultiDottedExtensions = True
			.Title = "Select a plain text file to import..."
		End With

		If (dlgOpenFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
			Try
				Me.Cursor = Cursors.WaitCursor
				Using sr As StreamReader = New StreamReader(dlgOpenFile.FileName)
					Do
						addServerEntry(sr.ReadLine())
					Loop Until sr.EndOfStream
				End Using

				Call txtEntry_ServerList_TextChanged(Nothing, Nothing)
				Call txtEntry_ServerList_LostFocus(Nothing, Nothing)
				MessageBox.Show(Me, "File imported successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)

			Catch ex As Exception
				MessageBox.Show(Me, "The file could not be imported.  The error is:" & vbCrLf & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)

			Finally
				Me.Cursor = Cursors.Default
			End Try

			Call txtEntry_ServerList_TextChanged(Nothing, Nothing)
			Call txtEntry_ServerList_LostFocus(Nothing, Nothing)
		End If
	End Sub

	' Allow exporting of currently entered server list with optional configuration settings
	Public Sub cmdEntry_Export_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Export.Click
		Dim sFilename As String
		Dim dResult As DialogResult
		Dim bTask As Boolean = False
		If ((sender Is Nothing) AndAlso (e Is Nothing)) Then bTask = True

		If (bTask = True) Then
			sFilename = Application.StartupPath & "\" & Guid.NewGuid.ToString.Substring(0, 8) & ".txt"
			dResult = Windows.Forms.DialogResult.Yes

		Else
			With dlgSaveFile
				.CheckPathExists = True
				.FileName = "ServerExportList.txt"
				.Filter = "Text Files (*.txt)|*.txt|All Files|*.*"
				.InitialDirectory = Application.StartupPath
				.SupportMultiDottedExtensions = True
				.Title = "Create a plain text file to export..."
			End With

			If (dlgSaveFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
				sFilename = dlgSaveFile.FileName
				dResult = MessageBox.Show(Me, "Do you also want to save the configuration settings with this export.?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			Else
				Return
			End If
		End If

		Try
			Me.Cursor = Cursors.WaitCursor
			Using sw As StreamWriter = New StreamWriter(sFilename)
				' Save settings...
				If (dResult = Windows.Forms.DialogResult.Yes) Then
					If (radEntry_Window1.Checked = True) Then
						sw.WriteLine("#window    = " & txtEntry_Window.Text)
					Else
						sw.WriteLine("#window    = " & dtpEntry_Window.Value.ToString("dd MMMM yyyy   HH:mm"))
					End If
					sw.WriteLine("#category  = " & chkEntry_Category.Checked.ToString & "|" & cmoEntry_Category.SelectedIndex.ToString)
					sw.WriteLine("#comment   = " & txtEntry_Comment.Text)
					sw.WriteLine("#whatif    = " & chkEntry_WhatIf.Checked.ToString)
					If (bTask = True) Then
						sw.WriteLine("#auto-start")
					Else
						sw.WriteLine()
						sw.WriteLine("; Uncomment the following line to have this server list autostart")
						sw.WriteLine(";#auto-start")
					End If
					sw.WriteLine()
				End If

				' Save server list...
				For Each sServer As String In txtEntry_ServerList.Lines
					sw.WriteLine(sServer.ToLower)
				Next

				sw.Flush()
			End Using
			If (bTask = False) Then MessageBox.Show(Me, "Selected Server List Exported Successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
			sTaskSaved = sFilename

		Catch ex As Exception
			MessageBox.Show(Me, "An error occurred:" & vbCrLf & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			sTaskSaved = vbNullString

		Finally
			Me.Cursor = Cursors.Default
		End Try
	End Sub

	' Changes to search tab and performs search or displays previous results
	Private Sub cmdEntry_SearchSCOM_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_SearchSCOM.Click
		Call controlChange(False)
		Call controlChangeSearch(False)

		lstSearch_Search.Items.Clear()
		lstSearch_Search.Groups.Clear()

		tPages.TabPages.Add(CType(cPages("tpS"), TabPage))
		tPages.SelectedTab = tPages.TabPages("tpS")
		Me.CancelButton = cmdSearch_Cancel

		Application.DoEvents()
		If (cServer.Count > 0) Then
			Call txtSearch_Search_TextChanged(sender, e)
			Call controlChangeSearch(True)
		Else

			Me.Cursor = Cursors.WaitCursor
			panLoadingDLLs.Visible = False
			lstSearch_Search.CheckBoxes = False
			lstSearch_Search.Groups.Clear()
			lstSearch_Search.Items.Clear()
			lstSearch_Search.Items.Add("N", lblLoadingDLLs.Text & "...", "_16___Connecting")
			lstSearch_Search.Refresh()

			Call cmdSearch_UpdateSCOMSearch_Click(Nothing, Nothing)
		End If
	End Sub

	' Performs a SELECT-ALL on the server entry list when CTRL+A is pressed
	Private Sub txtEntry_ServerList_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtEntry_ServerList.KeyUp
		If (e.KeyCode = Keys.A) And (e.Modifiers = Keys.Control) Then txtEntry_ServerList.SelectAll()
	End Sub

	' Sorts server list when leaving control
	Private Sub txtEntry_ServerList_LostFocus(sender As Object, e As System.EventArgs) Handles txtEntry_ServerList.LostFocus
		If (txtEntry_ServerList.TextLength = 0) Then Exit Sub

		' Remove "Disallowed" servers first...
		If (pSettings.sS_Disallowed <> vbNullString) Then
			For Each sDis As String In pSettings.sS_Disallowed.Split(","c)
				txtEntry_ServerList.Text = txtEntry_ServerList.Text.ToLower.Replace(sDis.ToLower.Trim, vbNullString)
			Next
		End If

		' Clean up and sort...
		Dim iItems(txtEntry_ServerList.Lines.Count - 1) As String
		For i As Integer = 0 To txtEntry_ServerList.Lines.Count - 1
			iItems(i) = txtEntry_ServerList.Lines(i)
		Next
		Array.Sort(iItems)
		Dim dItems As IEnumerable(Of String) = iItems.Distinct()
		txtEntry_ServerList.Lines = dItems.Where(Function(nn) nn.Trim.Length > 5).ToArray
	End Sub

	' Enables or disabled various controls depending on text input
	Private Sub txtEntry_ServerList_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEntry_ServerList.TextChanged
		Dim bEnabled As Boolean = False
		If ((txtEntry_ServerList.Text <> vbNullString) AndAlso (txtEntry_ServerList.Text.Length > 5)) Then bEnabled = True
		cmdEntry_Export.Enabled = bEnabled
		cmdEntry_Start.Enabled = bEnabled
		cmdEntry_Stop.Enabled = bEnabled
		cmdEntry_Next.Enabled = bEnabled
		cmdEntry_Verify.Enabled = bEnabled
	End Sub

	' Validates the time window text entry range
	Private Sub txtEntry_Window_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEntry_Window.TextChanged
		If (txtEntry_Window.Text.Length > 0) Then
			If (CLng(txtEntry_Window.Text) > 86400) Then txtEntry_Window.Text = "86400" ' Max of 2 months
			If (CLng(txtEntry_Window.Text) > 9) Then dtpEntry_Window.Value = Now.AddMinutes(CInt(txtEntry_Window.Text))
		End If
	End Sub

	' Changes tab page from tpL to tpO
	Private Sub cmdEntry_Next_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Next.Click
		tPages.TabPages.Add(CType(cPages("tpO"), TabPage))
		tPages.Controls.Remove(tPages.TabPages("tpL"))
		tPages.SelectedTab = tPages.TabPages("tpO")
	End Sub

	' Check manually typed server names
	Private Sub cmdEntry_Verify_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Verify.Click
		Call controlChange(False)
		Dim vServer As New Collection
		If (cServer.Count = 0) Then
			' Nothing in cache, let's fill it...
			Me.Cursor = Cursors.WaitCursor
			Dim lView As ListView = New ListView
			Dim mg As ManagementGroup = connectToManagementServer(lView)
			If (mg Is Nothing) Then
				lView.Dispose()
				Call controlChange(True)
				Return
			End If

			Dim comClass As ManagementPackClassCriteria = New ManagementPackClassCriteria("Name='System.Computer'")	' <-- was "Microsoft.Windows.Computer"
			Dim comClasses As IList(Of ManagementPackClass) = mg.EntityTypes.GetClasses(comClass)
			Dim comMO As List(Of MonitoringObject) = New List(Of MonitoringObject)
			For Each monClass As ManagementPackClass In comClasses
				Dim reader As IObjectReader(Of MonitoringObject)
				reader = mg.EntityObjects.GetObjectReader(Of MonitoringObject)(monClass, ObjectQueryOptions.Default)
				comMO.AddRange(reader)
			Next

			Dim lItem As ListViewItem
			For Each sServer As MonitoringObject In comMO
				' Check if server is part of the "Disallowed" list...
				Dim bDisallowed As Boolean = False
				If (pSettings.sS_Disallowed <> vbNullString) Then
					If (InStr(pSettings.sS_Disallowed.ToLower, sServer.Name.ToLower.Trim, CompareMethod.Text) > 0) Then bDisallowed = True
				End If

				If (bDisallowed = False) Then
					lItem = New ListViewItem(sServer.Name.ToLower.Trim)
					lItem.Name = sServer.Id.ToString.ToLower
					lItem.SubItems.Add(Mid(sServer.Name, InStr(sServer.Name, ".") + 1).ToLower)	' HIDDEN (Domain Name)
					If (lItem.SubItems(1).Text.ToLower = sServer.Name.ToLower) Then lItem.SubItems(1).Text = " No Domain"
					vServer.Add(lItem, sServer.Id.ToString)
				End If
			Next
		Else
			vServer = cServer
		End If

		' Check cache...
		Dim iFailedCount As Integer = 0
		Dim cItems As IEnumerable(Of ListViewItem) = vServer.Cast(Of ListViewItem)()
		Dim sReplacementText As String = txtEntry_ServerList.Text
		For Each sTypedServer As String In txtEntry_ServerList.Lines
			If (sTypedServer.Length > 5) Then
				Dim vItems As IEnumerable(Of ListViewItem) = Nothing
				Dim sTS As String = sTypedServer.ToLower
				vItems = cItems.Where(Function(S) S.Text.ToLower.Contains(sTS))
				If (vItems.Count > 0) Then
					sReplacementText = sReplacementText.Replace(sTypedServer, vItems(0).Text.ToLower)
				Else
					sReplacementText = sReplacementText.Replace(sTypedServer, "x-" & sTS.ToLower)
					iFailedCount = iFailedCount + 1
				End If
			End If
		Next

		vServer = Nothing
		txtEntry_ServerList.Text = sReplacementText
		Call controlChange(True)
		Call txtEntry_ServerList_LostFocus(sender, e)
		Me.Cursor = Cursors.Default

		If (iFailedCount > 0) Then
			MessageBox.Show(Me, iFailedCount.ToString & " server(s) failed to verify correctly", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End If
	End Sub
#End Region

	' #############################################################################################

#Region "TAB: (tpO) Server List Options"
	' Enables or disables controls as required
	Private Sub radEntry_Window1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radEntry_Window1.CheckedChanged
		If (radEntry_Window1.Checked = True) Then
			dtpEntry_Window.Enabled = False
			txtEntry_Window.Enabled = True
			txtEntry_Window.Focus()
		Else
			txtEntry_Window.Enabled = False
			dtpEntry_Window.Enabled = True
			dtpEntry_Window.Focus()
		End If
	End Sub

	' Loads and displays the preset timings list
	Private Sub cmdEntry_WindowPresets_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_WindowPresets.Click
		Me.Cursor = Cursors.WaitCursor
		Dim sPresets() As String = Split(readSetting("Presets", "Settings", "FAIL|"), "|")
		If (sPresets(0) = "FAIL") Then
			sPresets = sPresetWindow
			WritePrivateProfileString("Settings", "Presets", Join(sPresetWindow, "|"), sSettingsPath)
		End If

		Dim iCnt As Integer = 0
		Dim mItem As ToolStripMenuItem
		With mnuRightClick.Items
			.Clear()
			Do
				If sPresets(iCnt) <> "-" Then
					mItem = New ToolStripMenuItem(sPresets(iCnt), Nothing, AddressOf mnuPresetsWindow)
					mItem.Tag = sPresets(iCnt + 1)
					.Add(mItem)
				Else
					.Add("-")
				End If
				iCnt = iCnt + 2
			Loop Until (iCnt >= sPresets.Count)
		End With
		Me.Cursor = Cursors.Default
		mnuRightClick.Show(cmdEntry_WindowPresets, CInt(cmdEntry_WindowPresets.Width / 4), CInt(cmdEntry_WindowPresets.Height / 2))
	End Sub

	' Enters a selected preset value in the time window field
	Private Sub mnuPresetsWindow(sender As System.Object, e As System.EventArgs)
		Dim mItem As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
		txtEntry_Window.Text = mItem.Tag.ToString
		mItem.Dispose()
		txtEntry_Window.Refresh()
	End Sub

	' Changes tab page from tpO to tpL
	Private Sub cmdEntry_Back_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Back.Click
		tPages.TabPages.Add(CType(cPages("tpL"), TabPage))
		tPages.Controls.Remove(tPages.TabPages("tpO"))
		tPages.SelectedTab = tPages.TabPages("tpL")
	End Sub

	' Calls fillMaintenanceModeReason with specified values
	Private Sub chkEntry_Category_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEntry_Category.CheckedChanged
		Call fillMaintenanceModeReason(chkEntry_Category, cmoEntry_Category)
	End Sub

	' Sets the pSettings values for the selected category options
	Private Sub cmoEntry_Category_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoEntry_Category.SelectedIndexChanged
		pSettings.sS_CategoryC = chkEntry_Category.Checked
		pSettings.sS_CategoryI = cmoEntry_Category.SelectedIndex
	End Sub

	' Checks the server list settings values
	Private Function checkInputValues() As Boolean
		If (txtEntry_Comment.TextLength = 0) Then txtEntry_Comment.Text = "No Comment Given"

		If (radEntry_Window1.Checked = True) Then
			If ((txtEntry_Window.TextLength = 0) OrElse (CInt(txtEntry_Window.Text) < 10)) Then
				txtEntry_Window.Text = "10"
				Dim dResult As DialogResult = MessageBox.Show(Me, "Minimum maintenance window time is 10 minutes." & vbCrLf & "The previously entered value has been changed.", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
				If dResult = Windows.Forms.DialogResult.Cancel Then Return False
			End If
		Else
			If (dtpEntry_Window.Value < Now.AddMinutes(10)) Then
				dtpEntry_Window.Value = Now.AddMinutes(10)
				Dim dResult As DialogResult = MessageBox.Show(Me, "Minimum maintenance window time is 10 minutes." & vbCrLf & "The previously entered value has been changed.", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
				If dResult = Windows.Forms.DialogResult.Cancel Then Return False
			End If
		End If
		Return True
	End Function

	' Sets button text and options when changed 
	Private Sub chkEntry_Schedule_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEntry_Schedule.CheckedChanged
		Select Case chkEntry_Schedule.Checked
			Case True
				If (chkEntry_Category.Checked = False) Then
					Dim dResult As DialogResult
					dResult = MessageBox.Show("This will also change the category to 'Planned'." & vbCrLf & "Are you sure you want to do this.?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					If dResult = Windows.Forms.DialogResult.No Then
						chkEntry_Schedule.Checked = False
						Return
					End If
				End If

				chkEntry_Category.Checked = True
				chkEntry_Category.Enabled = False
				cmdEntry_Start.Text = "Schedule" & vbCrLf & "Start / Update"
				cmdEntry_Stop.Text = "Schedule" & vbCrLf & "Stop"
				cmdEntry_Stop.Enabled = False

			Case False
				chkEntry_Category.Enabled = True
				cmdEntry_Start.Text = "Start / Update"
				cmdEntry_Stop.Text = "Stop"
				cmdEntry_Stop.Enabled = True
		End Select
	End Sub
#End Region

	' #############################################################################################

#Region "TAB: (tpR) Maintenance Results"
	' Start button
	Private Sub cmdEntry_Start_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Start.Click
		If (checkInputValues() = False) Then Exit Sub

		' Check if running for a schedule task
		If (chkEntry_Schedule.Checked = True) Then
			sTaskSaved = CStr(False)
			frmTask.ShowDialog(Me)
			If (CBool(sTaskSaved) = True) Then Call cmdEntry_Back_Click(Nothing, Nothing)
			Return
		End If

		lblMaint_WhatIfMode.Visible = CBool(chkEntry_WhatIf.Checked)
		Me.Cursor = Cursors.WaitCursor

		tPages.TabPages.Add(CType(cPages("tpR"), TabPage))
		tPages.Controls.Remove(tPages.TabPages("tpO"))
		tPages.SelectedTab = tPages.TabPages("tpR")
		picHelp.Focus()

		Call controlChange(False)
		Me.CancelButton = cmdMaint_Done
		lstMaint_Results.Enabled = False

		Me.Refresh()
		Application.DoEvents()

		panLoadingDLLs.Visible = False
		lstMaint_Results.Groups.Clear()
		lstMaint_Results.Items.Clear()
		lstMaint_Results.Items.Add("N", lblLoadingDLLs.Text & "...", "_16___Connecting")
		lstMaint_Results.Refresh()

		Call doScan(CBool(IIf(sender IsNot Nothing, True, False)))
	End Sub

	' As above
	Private Sub cmdEntry_Stop_Click(sender As System.Object, e As System.EventArgs) Handles cmdEntry_Stop.Click
		Call cmdEntry_Start_Click(Nothing, Nothing)
	End Sub

	' Does the main bulk of the work
	Private Sub doScan(ByVal bStart_MM As Boolean)

		Dim mg As ManagementGroup = connectToManagementServer(lstMaint_Results)
		If (mg Is Nothing) Then
			cmdEntry_SearchSCOM.Visible = True
			cmdMaint_Done.Enabled = True
			cmdMaint_ViewLog.Enabled = True
			Me.Cursor = Cursors.Default

			If (pSettings.sS_AutoStart = True) Then
				Call cmdMaint_Done_Click(Nothing, Nothing)
				Me.Close()
			Else
				Return
			End If
		End If
		Me.Update()
		Application.DoEvents()

		Call aLog(">  Settings:")
		Call aLog("      Action:   " & IIf(bStart_MM = True, "START/UPDATE", "STOP").ToString & " Maintenance Mode")
		If (bStart_MM = True) Then
			Call aLog("      Window:   " & dtpEntry_Window.Value)
			Call aLog("      Category: " & cmoEntry_Category.Text)
			Call aLog("      Comment:  " & txtEntry_Comment.Text)
		End If
		Call aLog("      WhatIf:   " & chkEntry_WhatIf.Checked.ToString)

		lstMaint_Results.Items.Clear()
		lstMaint_Results.Items.Add("N", "Getting server information...", "_16___Scan___Scanning")
		lstMaint_Results.Refresh()
		
		Call aLog("   Getting server list")
		Dim comCriteria As ManagementPackClassCriteria = New ManagementPackClassCriteria("Name='System.Computer'") ' <-- was "Microsoft.Windows.Computer"
		Dim cluCriteria As ManagementPackClassCriteria = New ManagementPackClassCriteria("Name='Microsoft.Windows.Cluster'")
		Dim comClass As IList(Of ManagementPackClass) = mg.EntityTypes.GetClasses(comCriteria)
		Dim cluClass As IList(Of ManagementPackClass) = mg.EntityTypes.GetClasses(cluCriteria)

		Dim comMO As List(Of MonitoringObject) = New List(Of MonitoringObject)
		For Each monClass As ManagementPackClass In comClass
			Dim reader As IObjectReader(Of MonitoringObject)
			reader = mg.EntityObjects.GetObjectReader(Of MonitoringObject)(monClass, ObjectQueryOptions.Default)
			comMO.AddRange(reader)
		Next

		Dim cluMO As List(Of MonitoringObject) = New List(Of MonitoringObject)
		For Each monClass As ManagementPackClass In cluClass
			Dim reader As IObjectReader(Of MonitoringObject)
			reader = mg.EntityObjects.GetObjectReader(Of MonitoringObject)(monClass, ObjectQueryOptions.Default)
			cluMO.AddRange(reader)
		Next

		Call buildResultsTable()
		lstMaint_Results.Enabled = True

		Call aLog("")
		Call aLog("   Starting server enumeration...")
		Me.Update()
		Application.DoEvents()

		Dim bIsCluster As Boolean = False

		lstMaint_Results.EndUpdate()	' <-- Just in case
		For Each gGroup As ListViewGroup In lstMaint_Results.Groups
			For Each lItem As ListViewItem In gGroup.Items
				bIsCluster = False
				lItem.Selected = True
				Call ensureVisible(lstMaint_Results, lItem)
				Call aLog(">     Server: " & lItem.Text)
				lItem.SubItems(1).Text = "ICON:1"
				lstMaint_Results.Refresh()
				Application.DoEvents()

				Dim tmpItem As String = lItem.Text.ToLower

				If (lItem.Text.StartsWith("!x-") = False) Then
					Try
						Dim moItem As MonitoringObject = comMO.Single(Function(N) N.Name.ToLower = tmpItem)
						Dim clItems As List(Of MonitoringObject) = Nothing

						' Check for cluster resources
						Dim resMO As IList(Of MonitoringObject) = New List(Of MonitoringObject)
						resMO = moItem.ManagementGroup.EntityObjects.GetRelatedObjects(Of MonitoringObject)(moItem.Id, TraversalDepth.Recursive, ObjectQueryOptions.Default)
						Dim sClusterName As String = vbNullString
						If (resMO.Count > 0) Then
							For Each mo As MonitoringObject In resMO
								If (mo.Name IsNot Nothing) AndAlso _
								  ((mo.Name.Contains("Default Group")) Or _
								   (mo.Name.Contains("Cluster Group"))) Then sClusterName = mo.Name.Split("."c)(0)
								If ((mo.DisplayName IsNot Nothing) AndAlso (mo.DisplayName = "Cluster Service")) Then bIsCluster = True
							Next
						End If

						If (bIsCluster = True) Then clItems = cluMO.FindAll(Function(C) C.Name.ToLower = sClusterName.ToLower)

						' #############################################################################

						Call aLog("         Starting operation...")
						Application.DoEvents()
						Dim sMode As String = vbNullString
						If (bStart_MM = True) Then
							'START or UPDATE MAINTENANCE MODE...
							If (moItem.InMaintenanceMode = True) Then sMode = "update" Else sMode = "start"
						Else
							'STOP MAINTENANCE MODE (but only if actually in maintenance mode)...
							If (moItem.InMaintenanceMode = True) Then sMode = "stop" Else sMode = vbNullString
						End If

						' #############################################################################

						' Perform action...
						If (sMode <> vbNullString) Then
							lItem.SubItems(1).Text = "ICON:" & CInt(setMonitoring(moItem, sMode, "Computer"))
							If (bIsCluster = True) Then
								Dim iClusterResult As Integer = 0
								For Each mo As MonitoringObject In clItems
									Dim tmpResult As Integer = setMonitoring(mo, sMode, "Cluster Resource - " & mo.Name)
									If (tmpResult > iClusterResult) Then iClusterResult = tmpResult
								Next
								lItem.SubItems(2).Text = "ICON:" & iClusterResult
							End If
							Call aLog("      Done")
						Else
							lItem.SubItems(1).Text = "ICON:7"	' Nothing To Do
							lItem.SubItems(2).Text = "ICON:5"
							Call aLog("            Not in maintenance mode - nothing to do")
							Call aLog("      Done")
						End If

						'Reset(Stuff)
						resMO = Nothing
						moItem = Nothing
						clItems = Nothing

					Catch ex As Exception
						lItem.SubItems(1).Text = "ICON:4"	' Failed
						lItem.SubItems(2).Text = "ICON:5"
						Dim errMessage As String = vbNullString
						Select Case ex.Message
							Case "Sequence contains no matching element"
								errMessage = "Server not Found"
							Case "WIBBLE"
								errMessage = "WIBBLE"
							Case Else
								errMessage = ex.Message
						End Select
						Call aLog("***      ERROR During Maintenance Configuration" & vbCrLf & "***      " & errMessage)
					End Try

					Call aLog(" ")
					Application.DoEvents()
				Else

					lItem.SubItems(1).Text = "ICON:3"	' Warning
					lItem.SubItems(2).Text = "ICON:5"
					Call aLog("      Server not found, skipping")
				End If
			Next
		Next

		'Reset More Stuff
		comMO = Nothing
		cluMO = Nothing
		comCriteria = Nothing
		cluCriteria = Nothing
		comClass = Nothing
		cluClass = Nothing
		mg.Dispose()

		Me.Update()
		Application.DoEvents()

		Call aLog("Complete")
		Call controlChange(True)
		Me.Cursor = Cursors.Default

		If (pSettings.sS_AutoStart = True) Then
			Call cmdMaint_Done_Click(Nothing, Nothing)
			Me.Close()
		Else
			Dim sResult As String = "Maintenance mode configuration complete" & vbCrLf & "Check the audit log file for any issues"
			MessageBox.Show(Me, sResult, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub

	''' <summary>
	''' General function to perform the required maintenance operation
	''' </summary>
	''' <param name="mo">ReadOnly Collection of MonitoringObject</param>
	''' <param name="start_update_stop">String of required operation</param>
	''' <param name="sOpTarget">Text of operation target</param>
	''' <returns>MonitoringReturnStatus value, for use in image icon status</returns>
	''' <remarks> </remarks>
	Private Function setMonitoring(ByVal mo As MonitoringObject, ByRef start_update_stop As String, ByRef sOpTarget As String) As MonitoringReturnStatus
		Dim sOperation As String = vbNullString
		Select Case start_update_stop.ToLower
			Case "start" : sOperation = "Starting"
			Case "update" : sOperation = "Changing"
			Case "stop" : sOperation = "Stopping"
		End Select
		Call aLog("            " & sOperation & " maintenance mode for : " & sOpTarget)

		If (mo IsNot Nothing) Then
			Try
				If (chkEntry_WhatIf.Checked = True) Then
					Call aLog("            * WhatIf.? Enabled - Not making any changes")
				Else
					Dim iMMR As MaintenanceModeReason = CType(getMaintenanceModeReason(pSettings.sS_CategoryC, pSettings.sS_CategoryI), MaintenanceModeReason)
					Select Case start_update_stop.ToLower
						Case "start" : mo.ScheduleMaintenanceMode(Now.ToUniversalTime, dtpEntry_Window.Value.ToUniversalTime, iMMR, txtEntry_Comment.Text, Common.TraversalDepth.Recursive)
						Case "update" : mo.UpdateMaintenanceMode(dtpEntry_Window.Value.ToUniversalTime, iMMR, txtEntry_Comment.Text, Common.TraversalDepth.Recursive)
						Case "stop" : mo.StopMaintenanceMode(Now.ToUniversalTime, Common.TraversalDepth.Recursive)
					End Select
				End If
				Return MonitoringReturnStatus.Success

			Catch ex As Exception
				Call aLog("*     ERROR During Set Maintenance" & vbCrLf & "*     " & ex.Message)
				Return MonitoringReturnStatus.Failed
			End Try
		Else
			Call aLog("         * Monitoring item does not exist")
			Return MonitoringReturnStatus.Warning
		End If
	End Function

	' Builds the server list table for the results to be shown
	Private Sub buildResultsTable()
		Dim lItem As ListViewItem
		Dim cServers As New List(Of String)

		' Remove "Disallowed" servers first...
		If (pSettings.sS_Disallowed <> vbNullString) Then
			For Each sDis As String In pSettings.sS_Disallowed.Split(","c)
				txtEntry_ServerList.Text = txtEntry_ServerList.Text.ToLower.Replace(sDis.ToLower.Trim, vbNullString)
			Next
		End If

		For Each sServer As String In txtEntry_ServerList.Lines
			cServers.Add(sServer)
		Next
		cServers.Sort()
		Dim dServers As IEnumerable(Of String) = cServers.Distinct()

		lstMaint_Results.Items.Clear()
		lstMaint_Results.Groups.Clear()
		For Each sServer As String In dServers
			If (sServer.Length > 1) Then
				lItem = New ListViewItem(sServer.ToLower.Trim)
				lItem.ImageIndex = -1
				lItem.SubItems.Add(vbNullString)									' Computer Status
				lItem.SubItems.Add(vbNullString)									' Cluster Status
				lItem.SubItems.Add(Mid(sServer, InStr(sServer, ".") + 1).ToLower)	' HIDDEN (Domain Name)
				If (lItem.SubItems(3).Text.ToLower = sServer.ToLower) Then lItem.SubItems(3).Text = " No Domain"
				lstMaint_Results.Items.Add(lItem)
			End If
		Next

		Call doGroupListView(lstMaint_Results, "HIDDEN", False, False)
		lstMaint_Results.EndUpdate()
		lstMaint_Results.Refresh()
		Me.Update()
	End Sub

	' Removes results tab and dumps user back to server entry tab
	Private Sub cmdMaint_Done_Click(sender As System.Object, e As System.EventArgs) Handles cmdMaint_Done.Click
		Call cmdEntry_Back_Click(Nothing, Nothing)
		tPages.Controls.Remove(tPages.TabPages("tpR"))
		lstMaint_Results.Items.Clear()
		Call controlChange(True)
		Me.Update()
	End Sub

	' Refuse list access when "Done" button is disabled
	Private Sub lstMaint_Results_GotFocus(sender As Object, e As System.EventArgs) Handles lstMaint_Results.GotFocus
		If (cmdMaint_Done.Enabled = False) Then picHelp.Focus()
	End Sub

	' View the latest log file for this user
	Private Sub cmdMaint_ViewLog_Click(sender As System.Object, e As System.EventArgs) Handles cmdMaint_ViewLog.Click
		Shell("notepad.exe " & sLogFile, AppWinStyle.NormalFocus, False)
	End Sub

#End Region

	' #############################################################################################

#Region "TAB: (tpS) Search SCOM"
	' Performs search of SCOM and displays the results, sorted and grouped
	Private Sub cmdSearch_UpdateSCOMSearch_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch_UpdateSCOMSearch.Click
		cServer.Clear()
		Call controlChangeSearch(False)
		lstSearch_Search.CheckBoxes = False
		lstSearch_Search.ShowGroups = False
		lblSearch_ServerCount.Visible = False
		tPages.TabPages("tpS").Refresh()

		Me.Refresh()

		Dim mg As ManagementGroup = connectToManagementServer(lstSearch_Search)
		Me.Cursor = Cursors.Default
		If (mg Is Nothing) Then
			cmdSearch_Cancel.Enabled = True
			Return
		End If

		Me.Cursor = Cursors.WaitCursor
		Application.DoEvents()
		lstSearch_Search.Items("N").Text = "Getting list of servers... (this could take a while)"
		lstSearch_Search.Items("N").ImageKey = "_16___Scan___Scanning"
		Application.DoEvents()

		Call aLog("   Creating classes")
		Dim comClass As ManagementPackClassCriteria = New ManagementPackClassCriteria("Name='System.Computer'")	' <-- was "Microsoft.Windows.Computer"
		Dim comClasses As IList(Of ManagementPackClass) = mg.EntityTypes.GetClasses(comClass)
		Call aLog("   Creating monitoring object criteria")
		Dim comMO As List(Of MonitoringObject) = New List(Of MonitoringObject)
		For Each monClass As ManagementPackClass In comClasses
			Dim reader As IObjectReader(Of MonitoringObject)
			reader = mg.EntityObjects.GetObjectReader(Of MonitoringObject)(monClass, ObjectQueryOptions.Default)
			comMO.AddRange(reader)
		Next

		Dim lItem As ListViewItem
		Application.DoEvents()
		Call aLog("   Found " & comMO.Count & " servers")

		If (comMO.Count > 0) Then
			lstSearch_Search.Items("N").Text = "Adding servers to list..."
			Call aLog("   Adding servers to list...")

			For Each sServer As MonitoringObject In comMO
				If (sServer.Name.Length > 1) Then

					' Check if server is part of the "Disallowed" list...
					Dim bDisallowed As Boolean = False
					If (pSettings.sS_Disallowed <> vbNullString) Then
						If (InStr(pSettings.sS_Disallowed.ToLower, sServer.Name.ToLower.Trim, CompareMethod.Text) > 0) Then bDisallowed = True
					End If

					If (bDisallowed = False) Then
						lItem = New ListViewItem(sServer.Name.ToLower.Trim)
						lItem.Name = sServer.Id.ToString.ToLower

						Select Case sServer.HealthState
							Case HealthState.Error : lItem.ImageKey = "E"
							Case HealthState.Warning : lItem.ImageKey = "W"
							Case HealthState.Success : lItem.ImageKey = "O"
							Case HealthState.Uninitialized : lItem.ImageKey = "N"
							Case Else : lItem.ImageKey = "_16___Scan___Blank"
						End Select

						If (sServer.InMaintenanceMode = True) Then lItem.ImageKey = "M"
						lItem.SubItems.Add(Mid(sServer.Name, InStr(sServer.Name, ".") + 1).ToLower)	' HIDDEN (Domain Name)
						If (lItem.SubItems(1).Text.ToLower = sServer.Name.ToLower) Then lItem.SubItems(1).Text = " No Domain"
						cServer.Add(lItem, sServer.Id.ToString)
					End If
				End If
			Next
			Call aLog("Search Complete" & vbCrLf)
			Application.DoEvents()
		End If

		lstSearch_Search.CheckBoxes = True
		lblSearch_ServerCount.Text = comMO.Count & " servers found"
		comMO = Nothing
		comClass = Nothing
		comClasses = Nothing
		mg = Nothing

		txtSearch_Search.Text = vbNullString
		Call txtSearch_Search_TextChanged(Nothing, Nothing)
		lblSearch_ServerCount.Visible = True
		Me.Cursor = Cursors.Default
		Call controlChangeSearch(True)
		txtSearch_Search.Focus()
	End Sub

	' Searches SCOM server list using text input
	Private Sub txtSearch_Search_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch_Search.TextChanged
		' Delay to allow typing to finish...
		If ((sender IsNot Nothing) AndAlso (e IsNot Nothing)) Then
			swStopwatch.Reset() : swStopwatch.Start()
			Do
				picSearch_Loading.Visible = True
				Application.DoEvents()
			Loop Until swStopwatch.ElapsedMilliseconds >= 500
			swStopwatch.Stop()
		End If

		lstSearch_Search.BeginUpdate()
		lstSearch_Search.Items.Clear()
		Dim cItems As IEnumerable(Of ListViewItem)
		Dim sSearchQuery() As String = {}
		If (txtSearch_Search.TextLength > 1) Then sSearchQuery = txtSearch_Search.Text.Split(" "c)

		' Search the list for any servers containing the text entered...
		cItems = cServer.Cast(Of ListViewItem)()
		If (sSearchQuery.Count > 0) Then
			For Each sQuery As String In sSearchQuery
				Dim Q As String = sQuery.ToLower

				Select Case True
					Case Q.Equals("!c")
						cItems = cItems.Where(Function(s) s.Checked = True)
					Case Q.StartsWith("!")
						If (Q.Length > 1) Then cItems = cItems.Where(Function(s) s.ImageKey.ToLower = Q.Substring(1, 1))
					Case Else
						cItems = cItems.Where(Function(s) s.Text.ToLower.Contains(Q))
				End Select
			Next
		End If

		If (cItems.Count = 0) Then
			lstSearch_Search.ShowGroups = False
			lstSearch_Search.CheckBoxes = False
			lstSearch_Search.Items.Add("N", "No servers match your search query.", 4)
		Else
			lstSearch_Search.CheckBoxes = True
			lstSearch_Search.Items.AddRange(cItems.ToArray)
			Call doGroupListView(lstSearch_Search, "HIDDEN", False, False)
		End If

		lstSearch_Search.EndUpdate()

		If (txtSearch_Search.TextLength > 0) Then
			picSearch_Loading.Image = GrayScaleImage(My.Resources._16___Scan___Failed.ToBitmap, True)
		Else
			picSearch_Loading.Visible = False
		End If

		Application.DoEvents()
	End Sub

	' Sets cServer list checked item entry.  Used for searching and checking several times before adding
	Private Sub lstSearch_Search_ItemChecked(sender As Object, e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstSearch_Search.ItemChecked
		If (lstSearch_Search.CheckBoxes = False) Then Return
		If (lstSearch_Search.Items(0).Name = "N") Then Return
		If (cServer.Count = 0) Then Return

		Dim iCnt As Integer = 0
		For Each lItem As ListViewItem In cServer
			If (lItem.Equals(e.Item)) Then lItem.Checked = e.Item.Checked
		Next
	End Sub

	' Adds the checked list of servers to the server entry field
	Private Sub cmdSearch_Add_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch_Add.Click
		Me.Cursor = Cursors.WaitCursor
		Dim cItems As IEnumerable(Of ListViewItem) = cServer.Cast(Of ListViewItem)().Where(Function(nn) nn.Checked = True)
		For Each lItem As ListViewItem In cItems
			If (lItem.Checked = True) Then
				Call addServerEntry(lItem.Text)
			End If
		Next

		For Each litem As ListViewItem In cServer
			litem.Checked = False
		Next
		Me.Cursor = Cursors.Default
		Call cmdSearch_Cancel_Click(sender, e)
	End Sub

	' Changed tab page from tp3 to tp1
	Private Sub cmdSearch_Cancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch_Cancel.Click
		tPages.SelectedTab = tPages.TabPages("tpL")
		tPages.Controls.Remove(tPages.TabPages("tpS"))
		Call controlChange(True)
		Call txtEntry_ServerList_TextChanged(Nothing, Nothing)
		Call txtEntry_ServerList_LostFocus(Nothing, Nothing)
		Me.Cursor = Cursors.Default
	End Sub

	' Select All/None/Invert key check
	Private Sub lstSearch_Search_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lstSearch_Search.KeyUp
		Dim oSender As New ToolStripMenuItem
		Select Case True
			Case (e.KeyCode = Keys.A) And (e.Modifiers = Keys.Control) : oSender.Text = "Select All"
			Case (e.KeyCode = Keys.N) And (e.Modifiers = Keys.Control) : oSender.Text = "Select None"
			Case (e.KeyCode = Keys.I) And (e.Modifiers = Keys.Control) : oSender.Text = "Invert Selection"
		End Select
		Call mnuSelectItems(oSender, e)
	End Sub

	' Shows popup menu for select all, select none, invert selection
	Private Sub lstSearch_Search_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstSearch_Search.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Return
		If (lstSearch_Search.Items.Count = 0) Then Return
		If (lstSearch_Search.Items(0).Name = "N") Then Return

		With mnuRightClick.Items
			.Clear()
			.Add("Select All", My.Resources._16___Select_All.ToBitmap, AddressOf mnuSelectItems)
			.Add("Select None", My.Resources._16___Select_None.ToBitmap, AddressOf mnuSelectItems)
			.Add("Invert Selection", My.Resources._16___Selection_Invert.ToBitmap, AddressOf mnuSelectItems)

			If (lstSearch_Search.SelectedItems.Count = 1) Then
				.Add("-")
				.Add("Show Maintenance History", My.Resources._16___Help___Open.ToBitmap, AddressOf mnuMaintHistory)
			End If
		End With
		mnuRightClick.Show(Windows.Forms.Cursor.Position)
	End Sub

	' Handles menu selection from above
	Private Sub mnuSelectItems(sender As System.Object, e As System.EventArgs) ' Handles Nothing
		Dim mItem As ToolStripMenuItem = TryCast(sender, ToolStripMenuItem)
		For Each lItem As ListViewItem In lstSearch_Search.Items
			Select Case mItem.Text
				Case "Select All" : lItem.Checked = True
				Case "Select None" : lItem.Checked = False
				Case "Invert Selection" : lItem.Checked = Not lItem.Checked
			End Select
		Next
		mItem.Dispose()
	End Sub

	Private Sub mnuMaintHistory(sender As System.Object, e As System.EventArgs)
		frmHistory.sServerName = lstSearch_Search.SelectedItems(0).Text
		frmHistory.ShowDialog(Me)
		Me.Cursor = Cursors.Default
	End Sub

	' Quick search help information
	Private Sub lnkSearch_QuickHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSearch_QuickHelp.LinkClicked
		Dim sMsg As String = vbNullString
		sMsg = sMsg & "SEARCH QUICK HELP GUIDE" & vbCrLf
		sMsg = sMsg & "The search field is not case-sensitive" & vbCrLf
		sMsg = sMsg & "" & vbCrLf
		sMsg = sMsg & "Filter server list by adding text:" & vbCrLf
		sMsg = sMsg & "   LOC DOM 1" & vbCrLf
		sMsg = sMsg & "" & vbCrLf
		sMsg = sMsg & "Search on server status by using:" & vbCrLf
		sMsg = sMsg & "   !e" & vbTab & "Error" & vbCrLf
		sMsg = sMsg & "   !w" & vbTab & "Warning" & vbCrLf
		sMsg = sMsg & "   !o" & vbTab & "OK" & vbCrLf
		sMsg = sMsg & "   !m" & vbTab & "Maintenance" & vbCrLf
		sMsg = sMsg & "   !n" & vbTab & "Not monitored" & vbCrLf
		sMsg = sMsg & "   !c" & vbTab & "Currently Selected (Checked)" & vbCrLf
		sMsg = sMsg & "" & vbCrLf
		sMsg = sMsg & "" & vbCrLf
		sMsg = sMsg & "EXAMPLE:" & vbCrLf
		sMsg = sMsg & "   Search for all servers with 'LOC' and 'DOM' in" & vbCrLf
		sMsg = sMsg & "   the name, and are currently in an error state:" & vbCrLf
		sMsg = sMsg & "      LOC DOM !E" & vbCrLf
		MessageBox.Show(Me, sMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	' Clears search text
	Private Sub picSearch_Loading_Click(sender As System.Object, e As System.EventArgs) Handles picSearch_Loading.Click
		txtSearch_Search.Text = vbNullString
		picSearch_Loading.Visible = False
	End Sub
#End Region

	' #############################################################################################

#Region "TAB: (tpA) Admin Options"
	' Tests connection to the entered management server
	Private Sub cmdTestConnection_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdmin_TestConnection.Click
		If (txtAdmin_ManagementServer.Text = pDefaultSettings.sS_Server) Then
			MessageBox.Show(Me, "Enter Management Server FQDN", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Return
		End If

		pSettings.sS_Server = txtAdmin_ManagementServer.Text
		picAdmin_TestResult.Image = My.Resources._16___Scan___Blank.ToBitmap

		Dim mg As ManagementGroup = connectToManagementServer(lstSearch_Search)
		If (mg IsNot Nothing) Then
			Dim comClass As ManagementPackClassCriteria = New ManagementPackClassCriteria("Name='System.Computer'")	' <-- was "Microsoft.Windows.Computer"
			Dim comClasses As IList(Of ManagementPackClass) = mg.EntityTypes.GetClasses(comClass)
			Dim comMO As List(Of MonitoringObject) = New List(Of MonitoringObject)
			For Each monClass As ManagementPackClass In comClasses
				Dim reader As IObjectReader(Of MonitoringObject)
				reader = mg.EntityObjects.GetObjectReader(Of MonitoringObject)(monClass, ObjectQueryOptions.Default)
				comMO.AddRange(reader)
			Next

			picAdmin_TestResult.Image = My.Resources._16___Scan___Pass.ToBitmap
			Me.Cursor = Cursors.Default
			MessageBox.Show(Me, "Connection to management server successful," & vbCrLf & _
			  "Found " & comMO.Count & " monitored computers.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
			comMO = Nothing
			comClasses = Nothing
			comClass = Nothing
		Else
			Me.Cursor = Cursors.Default
			picAdmin_TestResult.Image = My.Resources._16___Scan___Failed.ToBitmap
		End If
	End Sub

	' Resets the management text result image
	Private Sub txtManagementServer_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtAdmin_ManagementServer.TextChanged
		picAdmin_TestResult.Image = My.Resources._16___Scan___Blank.ToBitmap
	End Sub

	' Saves the currently entered admin options
	Private Sub cmdAdmin_Save_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdmin_Save.Click
		If (txtAdmin_ManagementServer.Text = pDefaultSettings.sS_Server) Then Exit Sub

		Call WritePrivateProfileString("Settings", "Server", txtAdmin_ManagementServer.Text, sSettingsPath)
		Call WritePrivateProfileString("Settings", "Window", txtAdmin_DefaultsWindow.Text, sSettingsPath)
		Call WritePrivateProfileString("Settings", "Planned", chkAdmin_Category.Checked.ToString, sSettingsPath)
		Call WritePrivateProfileString("Settings", "Category", cmoAdmin_Category.SelectedIndex.ToString, sSettingsPath)
		Call WritePrivateProfileString("Settings", "Disallowed", txtAdmin_DefaultsDisallowed.Text, sSettingsPath)

		Dim sComment As String = txtAdmin_DefaultsComment.Text.Replace(vbCrLf, "\n")
		Call WritePrivateProfileString("Settings", "Comment", sComment, sSettingsPath)
		pSettings.sS_Server = txtAdmin_ManagementServer.Text

		MessageBox.Show(Me, "Settings saved", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
		tPages.SelectedTab = tPages.TabPages("tpL")
		tPages.Controls.Remove(tPages.TabPages("tpA"))
		Call controlChange(True)
		Call loadOptions()
	End Sub

	' Checks and sets the max value of the time window field
	Private Sub txtAdmin_DefaultsWindow_TextChanged(sender As Object, e As System.EventArgs) Handles txtAdmin_DefaultsWindow.TextChanged
		If (CInt(txtAdmin_DefaultsWindow.Text) > 86400) Then txtAdmin_DefaultsWindow.Text = "86400" ' Max of 2 months
	End Sub

	' Tries to load the admin options, if that fails will use the defaults (set at the top)
	Private Sub loadOptions()
		If (System.IO.File.Exists(sSettingsPath) = False) Then
			MessageBox.Show(Me, "Configuration file not found, please enter the required values", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

			tPages.TabPages.Add(CType(cPages("tpA"), TabPage))
			tPages.SelectedTab = tPages.TabPages("tpA")
			Call controlChange(False)
			Call cmdAdmin_Reset_Click(Nothing, Nothing)
			txtAdmin_ManagementServer.Focus()
		Else

			pSettings.sS_Server = readSetting("Server", "Settings", pDefaultSettings.sS_Server)
			pSettings.sS_Window = readSetting("Window", "Settings", "60")
			pSettings.sS_Comment = readSetting("Comment", "Settings", "No Comment")
			pSettings.sS_CategoryC = CBool(readSetting("Planned", "Settings", "TRUE"))
			pSettings.sS_CategoryI = CInt(readSetting("Category", "Settings", "3"))
			If IsNumeric(pSettings.sS_Window) = False Then pSettings.sS_Window = "60"
			pSettings.sS_Disallowed = readSetting("Disallowed", "Settings", vbNullString)

			If (pSettings.sS_Server = pDefaultSettings.sS_Server) Then
				MessageBox.Show(Me, "Configuration file is damaged, please enter the required values", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				tPages.TabPages.Add(CType(cPages("tpA"), TabPage))
				tPages.SelectedTab = tPages.TabPages("tpA")
				Call controlChange(False)
			Else

				lblManagementServer.Text = "Management Server: " & pSettings.sS_Server
				txtEntry_Window.Text = pSettings.sS_Window
				txtEntry_Comment.Text = pSettings.sS_Comment.Replace("\n", vbCrLf)
				chkEntry_Category.Checked = pSettings.sS_CategoryC
				cmoEntry_Category.SelectedIndex = pSettings.sS_CategoryI
				Call controlChange(True)
			End If
		End If
	End Sub

	''' <summary>
	''' General function to read in the settings from the configuration file
	''' </summary>
	''' <param name="sSettingName">Key Value pair key name</param>
	''' <param name="sSection">INI Section title</param>
	''' <param name="sDefault">Default value to be returned</param>
	''' <returns>Either the configuration file value specified or the default value given</returns>
	''' <remarks></remarks>
	Public Function readSetting(ByVal sSettingName As String, ByVal sSection As String, ByVal sDefault As String) As String
		Dim sString As New StringBuilder(500)
		Dim sReturn As String
		Try
			Call GetPrivateProfileString(sSection, sSettingName, vbNullString, sString, sString.Capacity, sSettingsPath)
			sReturn = sString.ToString.Trim
			If (sReturn.Length = 0) Then Return sDefault Else Return sReturn
		Catch ex As Exception
			Return sDefault
		End Try
	End Function

	' Resets the admin options to the default settings (set at the top)
	Private Sub cmdAdmin_Reset_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdmin_Reset.Click
		txtAdmin_ManagementServer.Text = pDefaultSettings.sS_Server
		txtAdmin_DefaultsComment.Text = pDefaultSettings.sS_Comment
		txtAdmin_DefaultsWindow.Text = pDefaultSettings.sS_Window
		chkAdmin_Category.Checked = pDefaultSettings.sS_CategoryC
		cmoAdmin_Category.SelectedIndex = pDefaultSettings.sS_CategoryI
		txtAdmin_DefaultsDisallowed.Text = vbNullString
	End Sub

	' Show the time presets form
	Private Sub cmdAdmin_ConfigPresets_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdmin_ConfigPresets_Window.Click
		frmPresets.ShowDialog(Me)
	End Sub

	' Calls fillMaintenanceModeReason with specified values
	Private Sub chkAdmin_Category_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAdmin_Category.CheckedChanged
		Call fillMaintenanceModeReason(chkAdmin_Category, cmoAdmin_Category)
	End Sub
#End Region

	' #############################################################################################

#Region "Common / General Code"
	' General code to enable/disable various controls as needed
	Private Sub controlChange(ByVal bEnabled As Boolean)
		cmdMaint_Done.Enabled = bEnabled
		cmdMaint_ViewLog.Enabled = bEnabled

		cmdEntry_Import.Enabled = bEnabled
		cmdEntry_Export.Enabled = bEnabled
		cmdEntry_Next.Enabled = bEnabled
		cmdEntry_Verify.Enabled = bEnabled
		txtEntry_ServerList.Enabled = bEnabled
		cmdEntry_SearchSCOM.Enabled = bEnabled

		txtEntry_Window.Enabled = bEnabled
		txtEntry_Comment.Enabled = bEnabled
		chkEntry_WhatIf.Enabled = bEnabled
		radEntry_Window1.Enabled = bEnabled
		radEntry_Window2.Enabled = bEnabled
		cmdEntry_WindowPresets.Enabled = bEnabled
		cmdEntry_Start.Enabled = bEnabled
		cmdEntry_Stop.Enabled = bEnabled

		lblMinutesFromNow.Enabled = bEnabled

		Application.DoEvents()
		If (tPages.SelectedTab.Equals(tPages.TabPages("tpL")) = True) Then Me.CancelButton = Nothing
	End Sub

	' General code to enable/disable various controls as needed - more specific than above
	Private Sub controlChangeSearch(ByVal bEnabled As Boolean)
		txtSearch_Search.Enabled = bEnabled
		lstSearch_Search.Enabled = bEnabled
		lstSearch_Search.CheckBoxes = bEnabled
		lnkSearch_QuickHelp.Enabled = bEnabled
		cmdSearch_Add.Enabled = bEnabled
		cmdSearch_Cancel.Enabled = bEnabled
		cmdSearch_UpdateSCOMSearch.Enabled = bEnabled
		Application.DoEvents()
	End Sub

	' Very basic validation of the input value server name
	Public Function validateServerNameInput(ByVal sInput As String) As String
		If (sInput Is Nothing) Then Return Nothing
		If (sInput.StartsWith(";")) Then Return Nothing

		sInput = sInput.Trim
		If (sInput = vbNullString) Then Return Nothing
		If (InStr(sInput, " ", CompareMethod.Text) > 0) Then Return Nothing
		If (InStr(txtEntry_ServerList.Text, sInput & vbCrLf, CompareMethod.Text) > 0) Then Return Nothing
		Return sInput
	End Function

	' Writes and entry to the audit log file
	Private Sub aLog(ByVal sAuditText As String)
		Dim dFolder As String = System.IO.Path.GetDirectoryName(sLogFile)
		If (Dir(dFolder, FileAttribute.Directory) = vbNullString) Then System.IO.Directory.CreateDirectory(dFolder)
		Using sw As StreamWriter = New StreamWriter(sLogFile, True)
			sw.WriteLine(sAuditText)
			sw.Flush()
		End Using
	End Sub

	' Adds a server entry to the server list field.  Also checks for control codes.  Comes from input/drag drop
	Private Sub addServerEntry(ByVal sServerName As String)
		If (sServerName.StartsWith("#")) Then
			Dim sName As String = sServerName
			Dim sValue As String = vbNullString
			If (sServerName.Contains(":") = True) Then sValue = sServerName.Split(":"c)(1).Trim
			If (sServerName.Contains("=") = True) Then sValue = sServerName.Split("="c)(1).Trim

			If (sName.ToLower = "#auto-start") Then pSettings.sS_AutoStart = True
			If (sValue <> vbNullString) Then
				Select Case True
					Case sName.ToLower.StartsWith("#window")
						If IsNumeric(sValue) Then
							If ((CLng(sValue) < 10) Or (CLng(sValue) > 86400)) Then sValue = pDefaultSettings.sS_Window
							radEntry_Window1.Checked = True
							txtEntry_Window.Text = sValue

						ElseIf IsDate(sValue) Then
							radEntry_Window2.Checked = True
							dtpEntry_Window.Value = CDate(sValue)
						End If
					Case sName.ToLower.StartsWith("#category")
						If (InStr(sValue, "|", CompareMethod.Text) > 0) Then
							chkEntry_Category.Checked = CBool(sValue.Split("|"c)(0))
							Call chkEntry_Category_CheckedChanged(Nothing, Nothing)
							cmoEntry_Category.SelectedIndex = CInt(sValue.Split("|"c)(1))
						End If

					Case sName.ToLower.StartsWith("#comment")
						txtEntry_Comment.Text = sValue

					Case sName.ToLower.StartsWith("#whatif")
						If ((sValue.ToLower = "true") Or (sValue.ToLower = "false")) Then chkEntry_WhatIf.Checked = CBool(sValue)
				End Select
			End If
		Else
			sServerName = validateServerNameInput(sServerName)
			If sServerName IsNot Nothing Then
				If (InStr(txtEntry_ServerList.Text.ToLower, sServerName.ToLower, CompareMethod.Text) = 0) Then
					txtEntry_ServerList.SelectionStart = txtEntry_ServerList.TextLength
					txtEntry_ServerList.AppendText(vbCrLf)
					txtEntry_ServerList.AppendText(sServerName.ToLower & vbCrLf)
				End If
			End If
		End If
	End Sub

	' Allows only numbers to be used in the time window field
	Private Sub txtNumbers_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdmin_DefaultsWindow.KeyPress
		If ((Char.IsDigit(e.KeyChar) = False) AndAlso (Char.IsControl(e.KeyChar) = False)) Then e.Handled = True
	End Sub

	' Try's to connect to the management server and returns a connection handle (if available)
	Public Function connectToManagementServer(ByVal lView As ListView) As ManagementGroup
		Application.DoEvents()
		Call aLog(vbCrLf & "####################################################################################################")
		Call aLog(Space(82) & Date.Now.ToString("yyyy/MM/dd - HH:mm"))
		Call aLog("Connecting to management server...")
		lView.Groups.Clear()
		lView.Items.Clear()
		lView.Items.Add("N", "Connecting to management server...", "_16___Connecting")
		lView.Refresh()
		Me.Cursor = Cursors.WaitCursor
		Application.DoEvents()

		Dim mg As ManagementGroup = Nothing
		Try
			mg = ManagementGroup.Connect(pSettings.sS_Server)
			Application.DoEvents()
			If (mg.IsConnected = False) Then Throw New System.Exception("Connection to management server failed")
			Call aLog("Success")
			'Me.Cursor = Cursors.Default  <<--- Don't do this here.!
			Return mg

		Catch ex As Exception
			Call aLog("Failed" & vbCrLf & "*  " & ex.Message & vbCrLf & vbCrLf)
			lView.Items("N").Text = "Connection to management server failed"

			If (pSettings.sS_AutoStart = False) Then
				MessageBox.Show(Me, "Failed to connect to the management server" & vbCrLf & _
						"The following error was returned:" & vbCrLf & vbCrLf & _
						ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
			End If

			Me.Cursor = Cursors.Default
			Application.DoEvents()
			Return Nothing
		End Try
	End Function

	' Handler for "About..." menu item on the system menu
	Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
		MyBase.WndProc(m)
		If (m.Msg = WM_SYSCOMMAND) Then
			Select Case m.WParam.ToInt32
				Case mnuCustom_About
					Dim sMsg As String = Application.ProductName & vbCrLf & "v" & Application.ProductVersion & vbCrLf & vbCrLf
					sMsg = sMsg & "Copyright (C) Mike Blackett" & vbCrLf & "support@myrandomthoughts.co.uk" & vbCrLf & vbCrLf
					sMsg = sMsg & "Any comments, issues or bugs, please let me know."
					MessageBox.Show(Me, sMsg, " About...", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End Select
		End If
	End Sub

	' Loads the help form and specifies a specific page to load
	Private Sub Help_Click(sender As System.Object, e As System.EventArgs) Handles picHelp.Click, lnkHelp.LinkClicked
		Select Case tPages.SelectedTab.Name
			Case "tpL" : frmHelp.sSelectPageByID = "help0100"		' Server List
			Case "tpO" : frmHelp.sSelectPageByID = "help0500"		' Server List Options
			Case "tpR" : frmHelp.sSelectPageByID = "help0200"		' Maintenance Results
			Case "tpS" : frmHelp.sSelectPageByID = "help0300"		' Search SCOM
			Case "tpA" : frmHelp.sSelectPageByID = "help0400"	' Admin Options
			Case Else : frmHelp.sSelectPageByID = "help0000"		' Introduction
		End Select

		frmHelp.ShowDialog(Me)
	End Sub

	' Fills the specified combo box box with the required category reason list
	Private Sub fillMaintenanceModeReason(ByVal chkBox As CheckBox, ByVal cmoList As ComboBox)
		' List taken from http://msdn.microsoft.com/en-us/library/hh327658.aspx
		' Use (combolist INDEX x2, and if unchecked, + 1)
		' * Special case options

		Dim iIndex As Integer = cmoList.SelectedIndex
		If (chkBox.Checked = True) Then
			With cmoList
				With .Items
					.Clear()
					.Add("Other  (planned)")								' 00	0
					.Add("Hardware Maintenance  (planned)")					' 02	1
					.Add("Hardware Installation  (planned)")				' 04	2
					.Add("Operating System Reconfiguration  (planned)")		' 06	3
					.Add("Application Maintenance  (planned)")				' 08	4
					.Add("Application Installation")						' 10	5

					.Add("Security Issue")									' 13*	6
				End With
			End With
		Else
			With cmoList
				With .Items
					.Clear()
					.Add("Other  (unplanned)")								' 01	0
					.Add("Hardware Maintenance  (unplanned)")				' 03	1
					.Add("Hardware Installation  (unplanned)")				' 05	2
					.Add("Operating System Reconfiguration  (unplanned)")	' 07	3
					.Add("Application Maintenance  (unplanned)")			' 09	4
					.Add("Application Unresponsive")						' 11	5

					.Add("Application Unstable")							' 12*	6
					.Add("Loss of network connectivity")					' 14*	7
				End With
			End With
		End If
		If (iIndex = 7) Then iIndex = 0
		cmoList.SelectedIndex = iIndex
	End Sub

	''' <summary>
	''' Works out the required MaintenanceModeReason code for the selected options
	''' </summary>
	''' <param name="chkChecked">Planned checkbox value</param>
	''' <param name="cmoIndex">Combo box control to use</param>
	''' <returns>Integer value between 0 and 14</returns>
	''' <remarks>See <seealso cref="fillMaintenanceModeReason">fillMaintenanceModeReason</seealso> for the full list</remarks>
	Public Function getMaintenanceModeReason(ByVal chkChecked As Boolean, ByVal cmoIndex As Integer) As Integer
		Dim iIndex As Integer
		iIndex = (cmoIndex * 2)
		If (iIndex = 15) Then iIndex = 14
		If (cmoIndex = 6) Then
			If (chkChecked = True) Then iIndex = iIndex + 1
		Else
			If (chkChecked = False) Then iIndex = iIndex + 1
		End If
		Return iIndex
	End Function
#End Region
End Class

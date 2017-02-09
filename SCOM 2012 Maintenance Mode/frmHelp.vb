Option Explicit On

Imports System.IO

Public Class frmHelp
	Public sSelectPageByID As String
	Private bExpandAllTobics As Boolean = True

	Private Sub frmHelp_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Help___Closed", My.Resources._16___Help___Closed)
				.Add("_16___Help___Open", My.Resources._16___Help___Open)
			End With
		End With

		cmdCancel.SendToBack()		' We don't see this, only use it for the ESC key to cancel

		With tvwHelp
			.ImageList = img16
			.ItemHeight = 20
			.Nodes.Clear()
			.ShowRootLines = True
			.ShowPlusMinus = True
			.ShowLines = False
		End With

		With SplitContainer1
			.SplitterDistance = 250
			.FixedPanel = FixedPanel.Panel1
		End With

		With webHelpText
			.IsWebBrowserContextMenuEnabled = False
			.ScriptErrorsSuppressed = True
			.WebBrowserShortcutsEnabled = False
			'.AllowWebBrowserDrop = False
		End With

		Call createHELP()
		lblStatusLabel.Text = vbNullString

		Me.CenterToScreen()
		Me.Visible = True
		Application.DoEvents()
		Me.Cursor = Cursors.WaitCursor

		' Load the selected page, if specified...
		If (sSelectPageByID IsNot Nothing) Then
			Dim sFind() As TreeNode = tvwHelp.Nodes.Find(sSelectPageByID, True)
			If ((sFind IsNot Nothing) AndAlso (sFind.Count > 0)) Then
				tvwHelp.SelectedNode = sFind(0)
				tvwHelp.SelectedNode.EnsureVisible()
				tvwHelp.SelectedNode.Expand()
			Else
				tvwHelp.Nodes("help0000").Expand()
			End If
		Else
			tvwHelp.Nodes("help0000").Expand()
		End If
	End Sub

	Private Sub createHELP()
		'        Topic Title (indents only for readability)         Page        Parent
		'       ----------------------------------------------------------------------------
		addHelp("Introduction                                   ", "help0000", "")

		addHelp("Server Entry List                              ", "help0100", "")
		addHelp("    Import                                     ", "help0101", "help0100")
		addHelp("        Control Codes                          ", "help0151", "help0101")
		addHelp("    Export                                     ", "help0102", "help0100")
		addHelp("    Search SCOM                                ", "help0103", "help0100")
		addHelp("    Manual Entry                               ", "help0104", "help0100")

		addHelp("Server List Options                            ", "help0500", "")
		addHelp("    Categories                                 ", "help0501", "help0500")
		addHelp("    Scheduled Tasks                            ", "help0502", "help0500")
		addHelp("        Known Error Messages                   ", "help0503", "help0500")

		addHelp("Maintenance Results                            ", "help0200", "")
		addHelp("    Column Headers                             ", "help0201", "help0200")

		addHelp("Search SCOM                                    ", "help0300", "")
		addHelp("    Search Text                                ", "help0301", "help0300")
		addHelp("    Update List                                ", "help0303", "help0300")

		addHelp("Admin Options                                  ", "help0400", "")
		addHelp("    SCOM Server                                ", "help0401", "help0400")
		addHelp("    Application Defaults                       ", "help0402", "help0400")
	End Sub

	' #############################################################################################

	Public Sub addHelp(ByVal sFolderName As String, ByVal sFolderID As String, ByVal sParentFolderID As String)
		Dim tvNode As New TreeNode
		Dim tvParent As New TreeNode
		With tvNode
			.Text = Trim(sFolderName)
			.Text = .Text.Replace(vbTab, vbNullString)
			.ImageKey = "_16___Help___Closed"
			.SelectedImageKey = "_16___Help___Open"
			.Name = Trim(sFolderID)
			.Tag = Trim(sFolderID)
		End With

		If (Trim(sParentFolderID) = vbNullString) Then
			tvwHelp.Nodes.Add(tvNode)
		Else
			tvParent = tvwHelp.Nodes.Find(Trim(sParentFolderID), True)(0)
			tvParent.Nodes.Add(tvNode)
			If bExpandAllTobics = True Then tvParent.ExpandAll()
		End If
		Application.DoEvents()
		tvNode = Nothing
		tvParent = Nothing
	End Sub

	Private Sub tvwHelp_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwHelp.AfterSelect
		Dim tvItem As TreeNode = tvwHelp.SelectedNode
		If (tvItem Is Nothing) Then Exit Sub
		If (tvItem.Tag.ToString = vbNullString) Then
			webHelpText.DocumentText = vbNullString
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		Dim sSource As String
		Try
			Dim sBreadcrumb As String = tvItem.FullPath.Replace(tvwHelp.PathSeparator, "&nbsp; &gt; &nbsp;")
			lblStatusLabel.Text = tvItem.FullPath.Replace(tvwHelp.PathSeparator, "  >  ")

			sSource = My.Resources.ResourceManager.GetObject(tvItem.Tag.ToString).ToString
			sSource = sSource.Replace("<!-- STYLESHEET -->", "<style>" & vbCrLf & My.Resources.stylesheet & vbCrLf & "</style>")
			sSource = sSource.Replace("<!-- BREADCRUMB -->", sBreadcrumb)
		Catch ex As Exception
			webHelpText.DocumentText = "Missing Text (" & tvItem.Tag.ToString & ")"
			Me.Cursor = Cursors.Default
			Exit Sub
		End Try

		' Search for an insert images from the resources...
		Dim iPosS As Integer = sSource.IndexOf("data:image/png;base64,")
		If (iPosS > 0) Then
			Do While iPosS > 0
				Dim iPosE As Integer = sSource.IndexOf(Chr(34), iPosS)
				Dim sImage As String = sSource.Substring(iPosS + 22, iPosE - (iPosS + 22))
				Dim iImage As Image = Nothing
				Dim base64 As String = vbNullString

				Dim tmpObj As Object = My.Resources.ResourceManager.GetObject(sImage)
				If (tmpObj IsNot Nothing) Then
					Select Case tmpObj.GetType.ToString
						Case "System.Drawing.Icon"
							iImage = CType(My.Resources.ResourceManager.GetObject(sImage), Icon).ToBitmap
							base64 = ImageToBase64String(iImage, Imaging.ImageFormat.Png)

						Case "System.Drawing.Bitmap"
							iImage = CType(My.Resources.ResourceManager.GetObject(sImage), Bitmap)
							base64 = ImageToBase64String(iImage, Imaging.ImageFormat.Jpeg)

						Case Else
							iImage = Nothing
							Me.Cursor = Cursors.Default
							MessageBox.Show(Me, "frmHelp (tvwHelp_AfterSelect): " & vbCrLf & tmpObj.GetType.ToString, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Select
				End If

				If (iImage Is Nothing) Then base64 = Chr(34) & " alt=" & Chr(34) & "Image '" & sImage & "' Missing"
				sSource = sSource.Substring(0, iPosS + 22) & base64 & sSource.Substring(iPosE)
				iPosS = sSource.IndexOf("data:image/png;base64,", iPosE + 1)
			Loop
		End If

		webHelpText.DocumentText = sSource
		Me.Cursor = Cursors.Default
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Close()
	End Sub

	Private Function ImageToBase64String(ByVal image As Image, ByVal imageFormat As Imaging.ImageFormat) As String
		Using memStream As New MemoryStream
			image.Save(memStream, imageFormat)
			Dim result As String = Convert.ToBase64String(memStream.ToArray())
			Return result
		End Using
	End Function

End Class
Public Class frmPresets

	Private bCancelEdit As Boolean
	Private lvwC As ListViewColumnSorter
	Dim lLVItem As ListViewItem
	Dim lSubItem As ListViewItem.ListViewSubItem
	Private Const sSep As String = "--- Seperator ----------"

	Private Sub frmPresets_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Add", My.Resources._16___Add)
				.Add("_16___Remove", My.Resources._16___Remove)
				.Add("_16___Arrow_Up", My.Resources._16___Arrow_Up)
				.Add("_16___Arrow_Dn", My.Resources._16___Arrow_Dn)
			End With
		End With

		cmdAdd.ImageList = img16
		cmdDel.ImageList = img16
		cmdMoveUp.ImageList = img16
		cmdMoveDn.ImageList = img16
		cmdAdd.ImageKey = "_16___Add"
		cmdDel.ImageKey = "_16___Remove"
		cmdMoveUp.ImageKey = "_16___Arrow_Up"
		cmdMoveDn.ImageKey = "_16___Arrow_Dn"

		With lstPresets
			.Clear()
			.View = View.Details
			With .Columns
				.Add("lbl", "Label", lstPresets.Width - 75 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("min", "Minutes", 75, HorizontalAlignment.Right, -1)
			End With
			.FullRowSelect = True
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			.LabelEdit = True
		End With

		txtSubItemEditor.Visible = False
		txtSubItemEditor.TextAlign = HorizontalAlignment.Right

		Dim sPresets() As String = Split(frmMain.readSetting("Presets", "Settings", "FAIL|"), "|")
		If (sPresets(0) <> "FAIL") Then Call displaySettings(sPresets) Else Call cmdReset_Click(Nothing, Nothing)
	End Sub
	Private Sub cmdReset_Click(sender As System.Object, e As System.EventArgs) Handles cmdReset.Click
		Call displaySettings(frmMain.sPresetWindow)
	End Sub

	Private Sub displaySettings(ByVal sSettings() As String)
		Dim iCnt As Integer = 0
		With lstPresets.Items
			.Clear()
			Do
				If (sSettings(iCnt) <> "-") Then
					Call addItem(sSettings(iCnt), sSettings(iCnt + 1), False)
				Else
					Call addItem(sSep, "-----", False)
				End If
				iCnt = iCnt + 2
			Loop Until (iCnt >= sSettings.Count)
		End With
	End Sub

	Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
		Call addItem("", "60", True)
	End Sub

	Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
		If (lstPresets.SelectedItems.Count = 0) Then Exit Sub
		Dim sResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to delete the selected item.?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If sResult = Windows.Forms.DialogResult.No Then
			lstPresets.Focus()
			Exit Sub
		End If
		For Each lItem As ListViewItem In lstPresets.SelectedItems
			lItem.Remove()
		Next
		lstPresets.Focus()
	End Sub

	Private Sub addItem(ByVal sItemText As String, ByVal sSubItemText As String, ByVal bEditItem As Boolean)
		If ((sItemText <> vbNullString) AndAlso (sSubItemText <> vbNullString)) Then
			If (lstPresets.Items.Find(sItemText, True).Count > 0) Then Exit Sub
		End If

		Dim lItem As New ListViewItem(sItemText)
		lItem.UseItemStyleForSubItems = False
		lItem.ImageKey = "_16___Connection"
		lItem.Name = Guid.NewGuid.ToString.Substring(0, 8)
		lItem.SubItems.Add(sSubItemText)
		lstPresets.Items.Add(lItem)
		If (bEditItem = True) Then lItem.BeginEdit()
	End Sub

	Private Sub lstPresets_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles lstPresets.AfterLabelEdit
		If (e.Label = Nothing) Then e.CancelEdit = True
		If (e.Label = String.Empty) Then e.CancelEdit = True
		If ((e.CancelEdit = True) And (lstPresets.Items(e.Item).Text = vbNullString)) Then lstPresets.Items(e.Item).Remove()
		If e.CancelEdit = True Then Exit Sub

		e.CancelEdit = True

		If (e.Label = "-") Then
			With lstPresets.Items(e.Item)
				.Text = sSep
				.SubItems(1).Text = "-----"
				.ForeColor = SystemColors.ControlDark
			End With
		Else
			lstPresets.Items(e.Item).Text = e.Label
			Dim r As Rectangle = lstPresets.Items(e.Item).SubItems(1).Bounds
			Dim m As New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, r.X + 2, r.Y + 2, 0)
			Call lstPresets_MouseDoubleClick(sender, m)
			m = Nothing
			r = Nothing
		End If
	End Sub

	Private Sub lstPresets_BeforeLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles lstPresets.BeforeLabelEdit
		If (lstPresets.Items(e.Item).Text = sSep) Then e.CancelEdit = True
	End Sub

	Private Sub lstPresets_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstPresets.MouseDoubleClick
		lLVItem = lstPresets.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		If (lLVItem.Text = sSep) Then Exit Sub

		lSubItem = lLVItem.GetSubItemAt(e.X, e.Y)

		If (lLVItem.SubItems.IndexOf(lSubItem) = 0) Then
			lLVItem.BeginEdit()
			Exit Sub
		End If

		Select Case lLVItem.SubItems.IndexOf(lSubItem)
			Case 1
				Dim lLeft = lSubItem.Bounds.Left + 2
				Dim lWidth As Integer = lSubItem.Bounds.Width
				With txtSubItemEditor
					.SetBounds(lLeft + lstPresets.Left, lSubItem.Bounds.Top + lstPresets.Top, lWidth, lSubItem.Bounds.Height)
					.Text = lSubItem.Text
					.Show()
					.Focus()
				End With
		End Select
	End Sub

	Private Sub txtSubItemEditor_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubItemEditor.KeyPress
		Select Case e.KeyChar
			Case ChrW(Keys.Return)
				bCancelEdit = False
				e.Handled = True
				txtSubItemEditor.Hide()

			Case ChrW(Keys.Escape)
				bCancelEdit = True
				e.Handled = True
				txtSubItemEditor.Hide()

				'Case CChar(":")
				'	txtSubItemEditor.AppendText(":")
				'	e.Handled = True
		End Select

		If ((Char.IsDigit(e.KeyChar) = False) AndAlso (Char.IsControl(e.KeyChar) = False)) Then e.Handled = True
	End Sub

	Private Sub txtSubItemEditor_LostFocus(sender As Object, e As System.EventArgs) Handles txtSubItemEditor.LostFocus
		txtSubItemEditor.Hide()
		If (bCancelEdit = False) Then
			Call validateMinutes(txtSubItemEditor.Text)
			lSubItem.Text = txtSubItemEditor.Text
		Else
			bCancelEdit = False
		End If
		lstPresets.Focus()
	End Sub

	Private Sub validateMinutes(ByVal sInput As String)
		Dim bFailed As Boolean = False
		If (sInput Is Nothing) Then bFailed = True
		If (sInput = vbNullString) Then bFailed = True

		'If (sInput.Contains(":")) Then
		'	Dim sD() As String = sInput.Split(":"c)
		'	If (CLng(sD(0)) > 23) Then sD(0) = "23"
		'	If (CLng(sD(1)) > 59) Then sD(1) = "59"
		'	If ((CLng(sD(0)) = 0) AndAlso (CLng(sD(1)) = 0)) Then sD = {"00", "01"}
		'	txtSubItemEditor.Text = CDate(Join(sD, ":")).ToShortTimeString
		'Else
		If (IsNumeric(sInput) = True) Then
			If (CInt(sInput) < 10) Then txtSubItemEditor.Text = "10"
			If (CInt(sInput) > 86400) Then txtSubItemEditor.Text = "86400"
		Else
			bFailed = True
		End If
		'End If
	End Sub

	Private Sub cmdMoveUp_Click(sender As System.Object, e As System.EventArgs) Handles cmdMoveUp.Click
		If (lstPresets.SelectedItems.Count = 0) Then
			lstPresets.Focus()
			Exit Sub
		End If
		If (lstPresets.SelectedItems(0).Index = 0) Then
			lstPresets.Focus()
			Exit Sub
		End If

		For Each lItem As ListViewItem In lstPresets.SelectedItems
			Dim iIndex As Integer = lItem.Index
			Dim lTmp As ListViewItem = lItem
			lstPresets.Items.RemoveAt(iIndex)
			lstPresets.Items.Insert(iIndex - 1, lTmp)
			lstPresets.Focus()
		Next
	End Sub

	Private Sub cmdMoveDn_Click(sender As System.Object, e As System.EventArgs) Handles cmdMoveDn.Click
		If (lstPresets.SelectedItems.Count = 0) Then
			lstPresets.Focus()
			Exit Sub
		End If
		If (lstPresets.SelectedItems(lstPresets.SelectedItems.Count - 1).Index = lstPresets.Items.Count - 1) Then
			lstPresets.Focus()
			Exit Sub
		End If

		For Each lItem As ListViewItem In New ReverseIterator(lstPresets.SelectedItems)
			Dim iIndex As Integer = lItem.Index
			Dim lTmp As ListViewItem = lItem
			lstPresets.Items.RemoveAt(iIndex)
			lstPresets.Items.Insert(iIndex + 1, lTmp)
			lstPresets.Focus()
		Next
	End Sub

	Private Sub cmdTest_Click(sender As System.Object, e As System.EventArgs) Handles cmdTest.Click
		With mnuRight.Items
			.Clear()
			For Each lItem As ListViewItem In lstPresets.Items
				If (lItem.Text <> sSep) Then .Add(lItem.Text) Else .Add("-")
			Next
		End With
		mnuRight.Show(cmdTest, CInt(cmdTest.Width / 4), CInt(cmdTest.Height / 2))
	End Sub

	Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
		Dim sSettings As String = vbNullString
		For Each lItem As ListViewItem In lstPresets.Items
			sSettings = sSettings & lItem.Text & "|" & lItem.SubItems(1).Text & "|"
		Next

		sSettings = Replace(sSettings, sSep, "-")			' Replace '--- Seperator ---...' String
		sSettings = Replace(sSettings, "-----", "-")		' Replace '-----' String
		sSettings = sSettings.Remove(sSettings.Length - 1)	' Remove trailing pipe charactor

		Call frmMain.WritePrivateProfileString("Settings", "Presets", sSettings, frmMain.sSettingsPath)
		MessageBox.Show(Me, "Preset settings saved successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Me.Close()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Dim dResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to quit this editor.?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If dResult = Windows.Forms.DialogResult.Yes Then Me.Close()
	End Sub
End Class

' Taken From: http://www.devx.com/vb2themax/Tip/18796
Class ReverseIterator
	Implements IEnumerable
	Dim items As New ArrayList()
	Sub New(ByVal collection As IEnumerable)
		Dim o As Object
		For Each o In collection
			items.Insert(0, o)
		Next
	End Sub
	Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
		Return items.GetEnumerator()
	End Function
End Class
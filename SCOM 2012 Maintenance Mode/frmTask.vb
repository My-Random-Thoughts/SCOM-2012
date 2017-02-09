Imports Microsoft.Win32.TaskScheduler		' EXTERNAL DLL
Imports System.IO

Public Class frmTask
	' Startup Code
	Private Sub frmTask_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		picHelp.Image = My.Resources._13___Help.ToBitmap

		txtName.Text = "SCOM Maintenance Mode"
		txtDesc.Text = "Maintenance mode task for the following servers:" & vbCrLf
		txtDesc.AppendText(frmMain.txtEntry_ServerList.Text.Replace(vbCrLf, ", "))

		txtUsername.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower
		txtPassword.Text = vbNullString
		dtDateTime.Value = Now.AddHours(1)
		dtDateTime.MinDate = Now.AddMinutes(10)

		cmdSave.Enabled = False
	End Sub

	' Close form and cancel task creation
	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		frmMain.sTaskSaved = CStr(False)
		Close()
	End Sub

	' Show help form
	Private Sub Help_Click(sender As System.Object, e As System.EventArgs) Handles picHelp.Click, lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0105"
		frmHelp.ShowDialog(Me)
	End Sub

	' Creates task from details given
	Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
		If (frmMain.sTaskSaved = CStr(False)) Then Call frmMain.cmdEntry_Export_Click(Nothing, Nothing)
		If (My.Computer.FileSystem.FileExists(frmMain.sTaskSaved) = False) Then Return

		Try
			Me.Cursor = Cursors.WaitCursor
			Using ts As New TaskService
				' Add Task
				Dim td As TaskDefinition = ts.NewTask
				td.RegistrationInfo.Description = txtDesc.Text
				td.RegistrationInfo.Author = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower

				' Add Trigger
				Dim tt As Trigger = New TimeTrigger(dtDateTime.Value)
				td.Triggers.Add(tt)

				' Add Executable
				Dim ea As ExecAction = New ExecAction()
				ea.Path = Chr(34) & Application.ExecutablePath & Chr(34)
				ea.Arguments = Chr(34) & frmMain.sTaskSaved & Chr(34)
				td.Actions.Add(ea)

				' Register Task (with unique name)
				Dim sUniqueName As String = txtName.Text & " (" & My.Computer.FileSystem.GetName(frmMain.sTaskSaved)
				sUniqueName = sUniqueName.Substring(0, sUniqueName.Length - 4) & ")"
				ts.RootFolder.RegisterTaskDefinition(sUniqueName, td, TaskCreation.CreateOrUpdate, txtUsername.Text, txtPassword.Text, TaskLogonType.Password)

				tt.Dispose()
				td.Dispose()
			End Using
			Me.Cursor = Cursors.Default
			MessageBox.Show("A scheduled task as been created successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
			frmMain.sTaskSaved = CStr(True)
			Close()

		Catch ex As Exception
			Dim sMsg As String = vbNullString
			Me.Cursor = Cursors.Default
			sMsg = "Scheduled task creation failed." & vbCrLf

			Select Case True
				Case ex.Message.Contains("A specified logon session does not exist. It may already have been terminated")
					sMsg = sMsg & "This is a known error when a specific GPO is configured" & vbCrLf & vbCrLf & ex.Message

				Case Else
					sMsg = sMsg & "The following message was returned..." & vbCrLf & vbCrLf & ex.Message
			End Select

			sMsg = sMsg & vbCrLf & vbCrLf & "See help for more information."
			MessageBox.Show(sMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	' Form validation checks
	Private Sub inputCheck_Leave(sender As Object, e As System.EventArgs) Handles txtName.Leave, txtDesc.Leave, txtUsername.Leave, txtPassword.Leave, txtPassword.TextChanged
		Dim bReturn As Boolean = True
		If (txtName.Text = vbNullString) Then bReturn = False
		If (txtUsername.Text = vbNullString) Then bReturn = False
		If (txtPassword.Text = vbNullString) Then bReturn = False
		cmdSave.Enabled = bReturn
	End Sub
End Class
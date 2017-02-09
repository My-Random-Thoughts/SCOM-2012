Imports Microsoft.EnterpriseManagement.Configuration
Imports Microsoft.EnterpriseManagement.Monitoring
Imports Microsoft.EnterpriseManagement.Common

Public Class frmHistory
	Public sServerName As String

	Private Sub frmHistory_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

		Me.Width = 550
		Me.Height = 250
		Me.CenterToParent()

		With lstHistory
			.Clear()
			.View = View.Details
			.FullRowSelect = True
			.MultiSelect = False
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			With .Columns
				.Add("A", "Start")
				.Add("B", "Stop")
				.Add("C", "Duration")
				.Add("D", "User")
				.Add("E", "Reason")
				.Add("F", "Description")
			End With
		End With

		lblStatus.Text = vbNullString
		For Each c As ColumnHeader In lstHistory.Columns
			c.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
		Next
		lstHistory.Columns("A").Width = lstHistory.Columns("A").Width + (lstHistory.Columns("F").Width - 75)
		lstHistory.Columns("F").Width = 75

		Me.Visible = True
		Application.DoEvents()
		Call getMaintenanceHistory()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Me.Cursor = Cursors.Default
		Close()
	End Sub

	Private Sub getMaintenanceHistory()
		Dim mg As ManagementGroup = frmMain.connectToManagementServer(lstHistory)
		Me.Cursor = Cursors.Default
		If (mg Is Nothing) Then
			lstHistory.Items("N").Text = "Error Connecting To Management Server"
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		Dim comClass As ManagementPackClassCriteria = New ManagementPackClassCriteria("Name='Microsoft.Windows.Computer'")
		Dim comClasses As IList(Of ManagementPackClass) = mg.EntityTypes.GetClasses(comClass)
		Dim comMO As List(Of MonitoringObject) = New List(Of MonitoringObject)
		For Each monClass As ManagementPackClass In comClasses
			Dim reader As IObjectReader(Of MonitoringObject)
			reader = mg.EntityObjects.GetObjectReader(Of MonitoringObject)(monClass, ObjectQueryOptions.Default)
			comMO.AddRange(reader)
		Next

		For Each sServer As MonitoringObject In comMO
			If (sServer.Name.ToLower.Trim = sServerName.ToLower.Trim) Then
				Dim gmwh As IList(Of MaintenanceWindow) = Nothing
				Try
					gmwh = sServer.GetMaintenanceWindowHistory()

				Catch ex As Exception
					Me.Cursor = Cursors.Default
					lstHistory.Items("N").Text = "Error Getting History Details"
					Exit Sub
				End Try

				If ((gmwh Is Nothing) OrElse (gmwh.Count = 0)) Then
					Me.Cursor = Cursors.Default
					lstHistory.Items("N").Text = "No History For This Server"
					Exit Sub
				End If

				Try
					lstHistory.Items.Clear()
					lblStatus.Text = gmwh.Count.ToString & " Entries"
					For Each rocMW As MaintenanceWindow In gmwh
						Dim lItem As New ListViewItem
						lItem.Text = rocMW.StartTime.ToShortDateString & " " & rocMW.StartTime.ToShortTimeString
						lItem.Name = rocMW.MonitoringObjectId.ToString
						lItem.SubItems.Add(rocMW.ScheduledEndTime.ToShortDateString & " " & rocMW.ScheduledEndTime.ToShortTimeString)

						Dim iSpan As TimeSpan = TimeSpan.FromMinutes(DateDiff(DateInterval.Minute, rocMW.StartTime, rocMW.ScheduledEndTime))
						lItem.SubItems.Add("~" & iSpan.Hours.ToString.PadLeft(2, "0"c) & "h " & iSpan.Minutes.ToString.PadLeft(2, "0"c) & "m")

						lItem.SubItems.Add(rocMW.User)
						lItem.SubItems.Add(rocMW.Reason.ToString)
						lItem.SubItems.Add(rocMW.Comments)
						lstHistory.Items.Add(lItem)
					Next

					For Each c As ColumnHeader In lstHistory.Columns
						c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
					Next
					Me.Cursor = Cursors.Default

				Catch ex As Exception
					Me.Cursor = Cursors.Default
					MsgBox("Error displaying some history entries" & vbCrLf & ex.Message)
				End Try

				Exit For
			End If
		Next
		If ((lstHistory.Items.ContainsKey("N") = True) AndAlso (lstHistory.Items("N").Text = "Connecting to management server...")) Then
			lstHistory.Items("N").Text = "Error Getting Server List"
		End If

		comMO = Nothing
		comClass = Nothing
		comClasses = Nothing
		mg = Nothing
		Me.Cursor = Cursors.Default
	End Sub
End Class
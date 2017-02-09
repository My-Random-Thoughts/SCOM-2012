<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTask
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.txtName = New System.Windows.Forms.TextBox()
		Me.txtUsername = New System.Windows.Forms.TextBox()
		Me.txtPassword = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.txtDesc = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.dtDateTime = New System.Windows.Forms.DateTimePicker()
		Me.cmdSave = New System.Windows.Forms.Button()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.picHelp = New System.Windows.Forms.PictureBox()
		Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
		CType(Me.picHelp, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 15)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(65, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Task Name:"
		'
		'txtName
		'
		Me.txtName.Location = New System.Drawing.Point(100, 12)
		Me.txtName.Name = "txtName"
		Me.txtName.Size = New System.Drawing.Size(307, 20)
		Me.txtName.TabIndex = 0
		'
		'txtUsername
		'
		Me.txtUsername.Location = New System.Drawing.Point(100, 151)
		Me.txtUsername.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.txtUsername.Name = "txtUsername"
		Me.txtUsername.Size = New System.Drawing.Size(307, 20)
		Me.txtUsername.TabIndex = 3
		'
		'txtPassword
		'
		Me.txtPassword.Location = New System.Drawing.Point(100, 177)
		Me.txtPassword.Margin = New System.Windows.Forms.Padding(3, 3, 3, 12)
		Me.txtPassword.Name = "txtPassword"
		Me.txtPassword.Size = New System.Drawing.Size(200, 20)
		Me.txtPassword.TabIndex = 4
		Me.txtPassword.UseSystemPasswordChar = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 154)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3, 12, 3, 0)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(67, 13)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "RunAs User:"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 180)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(56, 13)
		Me.Label3.TabIndex = 5
		Me.Label3.Text = "Password:"
		'
		'txtDesc
		'
		Me.txtDesc.Location = New System.Drawing.Point(100, 38)
		Me.txtDesc.Margin = New System.Windows.Forms.Padding(3, 3, 3, 12)
		Me.txtDesc.Multiline = True
		Me.txtDesc.Name = "txtDesc"
		Me.txtDesc.Size = New System.Drawing.Size(307, 45)
		Me.txtDesc.TabIndex = 1
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 41)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(63, 13)
		Me.Label5.TabIndex = 8
		Me.Label5.Text = "Description:"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(12, 113)
		Me.Label6.Margin = New System.Windows.Forms.Padding(3, 30, 3, 0)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(58, 13)
		Me.Label6.TabIndex = 9
		Me.Label6.Text = "Start Time:"
		'
		'dtDateTime
		'
		Me.dtDateTime.CustomFormat = " dd MMMM yyyy          HH:mm"
		Me.dtDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.dtDateTime.Location = New System.Drawing.Point(100, 107)
		Me.dtDateTime.Margin = New System.Windows.Forms.Padding(3, 12, 3, 12)
		Me.dtDateTime.Name = "dtDateTime"
		Me.dtDateTime.Size = New System.Drawing.Size(200, 20)
		Me.dtDateTime.TabIndex = 2
		'
		'cmdSave
		'
		Me.cmdSave.Location = New System.Drawing.Point(282, 235)
		Me.cmdSave.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(125, 25)
		Me.cmdSave.TabIndex = 5
		Me.cmdSave.Text = "Create"
		Me.cmdSave.UseVisualStyleBackColor = True
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(192, 235)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 12, 12, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 6
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'lnkHelp
		'
		Me.lnkHelp.AutoSize = True
		Me.lnkHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
		Me.lnkHelp.Location = New System.Drawing.Point(25, 247)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0, 5, 3, 0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(110, 13)
		Me.lnkHelp.TabIndex = 16
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Scheduled Task Help"
		'
		'picHelp
		'
		Me.picHelp.Location = New System.Drawing.Point(12, 247)
		Me.picHelp.Name = "picHelp"
		Me.picHelp.Size = New System.Drawing.Size(13, 13)
		Me.picHelp.TabIndex = 15
		Me.picHelp.TabStop = False
		'
		'dlgOpenFile
		'
		Me.dlgOpenFile.FileName = "OpenFileDialog1"
		'
		'frmTask
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(419, 272)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picHelp)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdSave)
		Me.Controls.Add(Me.dtDateTime)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.txtDesc)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.txtPassword)
		Me.Controls.Add(Me.txtUsername)
		Me.Controls.Add(Me.txtName)
		Me.Controls.Add(Me.Label1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmTask"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Scheduled Task Creator"
		CType(Me.picHelp, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents txtName As System.Windows.Forms.TextBox
	Friend WithEvents txtUsername As System.Windows.Forms.TextBox
	Friend WithEvents txtPassword As System.Windows.Forms.TextBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents txtDesc As System.Windows.Forms.TextBox
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents dtDateTime As System.Windows.Forms.DateTimePicker
	Friend WithEvents cmdSave As System.Windows.Forms.Button
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picHelp As System.Windows.Forms.PictureBox
	Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
End Class

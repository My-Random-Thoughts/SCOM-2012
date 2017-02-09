<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPresets
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
		Me.components = New System.ComponentModel.Container()
		Me.lstPresets = New System.Windows.Forms.ListView()
		Me.cmdReset = New System.Windows.Forms.Button()
		Me.cmdSave = New System.Windows.Forms.Button()
		Me.txtSubItemEditor = New System.Windows.Forms.TextBox()
		Me.cmdDel = New System.Windows.Forms.Button()
		Me.cmdAdd = New System.Windows.Forms.Button()
		Me.cmdMoveUp = New System.Windows.Forms.Button()
		Me.cmdMoveDn = New System.Windows.Forms.Button()
		Me.img16 = New System.Windows.Forms.ImageList(Me.components)
		Me.Label1 = New System.Windows.Forms.Label()
		Me.cmdTest = New System.Windows.Forms.Button()
		Me.mnuRight = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'lstPresets
		'
		Me.lstPresets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstPresets.Location = New System.Drawing.Point(12, 43)
		Me.lstPresets.Name = "lstPresets"
		Me.lstPresets.Size = New System.Drawing.Size(395, 281)
		Me.lstPresets.TabIndex = 4
		Me.lstPresets.UseCompatibleStateImageBehavior = False
		'
		'cmdReset
		'
		Me.cmdReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdReset.Location = New System.Drawing.Point(12, 339)
		Me.cmdReset.Name = "cmdReset"
		Me.cmdReset.Size = New System.Drawing.Size(75, 25)
		Me.cmdReset.TabIndex = 5
		Me.cmdReset.Text = "Reset"
		Me.cmdReset.UseVisualStyleBackColor = True
		'
		'cmdSave
		'
		Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdSave.Location = New System.Drawing.Point(332, 339)
		Me.cmdSave.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(75, 25)
		Me.cmdSave.TabIndex = 8
		Me.cmdSave.Text = "Save"
		Me.cmdSave.UseVisualStyleBackColor = True
		'
		'txtSubItemEditor
		'
		Me.txtSubItemEditor.Location = New System.Drawing.Point(183, 342)
		Me.txtSubItemEditor.Name = "txtSubItemEditor"
		Me.txtSubItemEditor.Size = New System.Drawing.Size(20, 20)
		Me.txtSubItemEditor.TabIndex = 85
		Me.txtSubItemEditor.TabStop = False
		'
		'cmdDel
		'
		Me.cmdDel.Location = New System.Drawing.Point(43, 12)
		Me.cmdDel.Name = "cmdDel"
		Me.cmdDel.Size = New System.Drawing.Size(25, 25)
		Me.cmdDel.TabIndex = 1
		Me.cmdDel.UseVisualStyleBackColor = True
		'
		'cmdAdd
		'
		Me.cmdAdd.Location = New System.Drawing.Point(12, 12)
		Me.cmdAdd.Name = "cmdAdd"
		Me.cmdAdd.Size = New System.Drawing.Size(25, 25)
		Me.cmdAdd.TabIndex = 0
		Me.cmdAdd.UseVisualStyleBackColor = True
		'
		'cmdMoveUp
		'
		Me.cmdMoveUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdMoveUp.Location = New System.Drawing.Point(351, 12)
		Me.cmdMoveUp.Name = "cmdMoveUp"
		Me.cmdMoveUp.Size = New System.Drawing.Size(25, 25)
		Me.cmdMoveUp.TabIndex = 2
		Me.cmdMoveUp.UseVisualStyleBackColor = True
		'
		'cmdMoveDn
		'
		Me.cmdMoveDn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdMoveDn.Location = New System.Drawing.Point(382, 12)
		Me.cmdMoveDn.Name = "cmdMoveDn"
		Me.cmdMoveDn.Size = New System.Drawing.Size(25, 25)
		Me.cmdMoveDn.TabIndex = 3
		Me.cmdMoveDn.UseVisualStyleBackColor = True
		'
		'img16
		'
		Me.img16.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
		Me.img16.ImageSize = New System.Drawing.Size(16, 16)
		Me.img16.TransparentColor = System.Drawing.Color.Transparent
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(74, 18)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(264, 13)
		Me.Label1.TabIndex = 90
		Me.Label1.Text = "To add a menu separator, enter a dash ' - ' as the label"
		'
		'cmdTest
		'
		Me.cmdTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdTest.Location = New System.Drawing.Point(102, 339)
		Me.cmdTest.Margin = New System.Windows.Forms.Padding(12, 3, 3, 3)
		Me.cmdTest.Name = "cmdTest"
		Me.cmdTest.Size = New System.Drawing.Size(75, 25)
		Me.cmdTest.TabIndex = 6
		Me.cmdTest.Text = "Test"
		Me.cmdTest.UseVisualStyleBackColor = True
		'
		'mnuRight
		'
		Me.mnuRight.Name = "mnuRight"
		Me.mnuRight.Size = New System.Drawing.Size(61, 4)
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.Location = New System.Drawing.Point(242, 339)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 7
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'frmPresets
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(419, 376)
		Me.ControlBox = False
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdTest)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmdMoveUp)
		Me.Controls.Add(Me.cmdMoveDn)
		Me.Controls.Add(Me.cmdDel)
		Me.Controls.Add(Me.cmdAdd)
		Me.Controls.Add(Me.txtSubItemEditor)
		Me.Controls.Add(Me.cmdReset)
		Me.Controls.Add(Me.cmdSave)
		Me.Controls.Add(Me.lstPresets)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmPresets"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Time Window Presets"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lstPresets As System.Windows.Forms.ListView
	Friend WithEvents cmdReset As System.Windows.Forms.Button
	Friend WithEvents cmdSave As System.Windows.Forms.Button
	Friend WithEvents txtSubItemEditor As System.Windows.Forms.TextBox
	Friend WithEvents cmdDel As System.Windows.Forms.Button
	Friend WithEvents cmdAdd As System.Windows.Forms.Button
	Friend WithEvents cmdMoveUp As System.Windows.Forms.Button
	Friend WithEvents cmdMoveDn As System.Windows.Forms.Button
	Friend WithEvents img16 As System.Windows.Forms.ImageList
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents cmdTest As System.Windows.Forms.Button
	Friend WithEvents mnuRight As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
End Class

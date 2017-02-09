<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
		Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
		Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog()
		Me.tpR = New System.Windows.Forms.TabPage()
		Me.picMaint_COM = New System.Windows.Forms.PictureBox()
		Me.picMaint_CLU = New System.Windows.Forms.PictureBox()
		Me.cmdMaint_ViewLog = New System.Windows.Forms.Button()
		Me.lblMaint_WhatIfMode = New System.Windows.Forms.Label()
		Me.cmdMaint_Done = New System.Windows.Forms.Button()
		Me.lstMaint_Results = New SCOM2012MaintenanceMode.ctrlListView_SubIcons()
		Me.tpL = New System.Windows.Forms.TabPage()
		Me.cmdEntry_Verify = New System.Windows.Forms.Button()
		Me.cmdEntry_Next = New System.Windows.Forms.Button()
		Me.lblFQDNNote1 = New System.Windows.Forms.Label()
		Me.cmdEntry_Export = New System.Windows.Forms.Button()
		Me.cmdEntry_SearchSCOM = New System.Windows.Forms.Button()
		Me.txtEntry_ServerList = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.cmdEntry_Import = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.tPages = New System.Windows.Forms.TabControl()
		Me.tpO = New System.Windows.Forms.TabPage()
		Me.lblEntry_Schedule = New System.Windows.Forms.Label()
		Me.chkEntry_Schedule = New System.Windows.Forms.CheckBox()
		Me.chkEntry_Category = New System.Windows.Forms.CheckBox()
		Me.Label16 = New System.Windows.Forms.Label()
		Me.cmoEntry_Category = New System.Windows.Forms.ComboBox()
		Me.radEntry_Window2 = New System.Windows.Forms.RadioButton()
		Me.radEntry_Window1 = New System.Windows.Forms.RadioButton()
		Me.cmdEntry_Back = New System.Windows.Forms.Button()
		Me.dtpEntry_Window = New System.Windows.Forms.DateTimePicker()
		Me.cmdEntry_WindowPresets = New System.Windows.Forms.Button()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.chkEntry_WhatIf = New System.Windows.Forms.CheckBox()
		Me.lblSetMaintenanceMode = New System.Windows.Forms.Label()
		Me.cmdEntry_Stop = New System.Windows.Forms.Button()
		Me.cmdEntry_Start = New System.Windows.Forms.Button()
		Me.txtEntry_Window = New System.Windows.Forms.TextBox()
		Me.txtEntry_Comment = New System.Windows.Forms.TextBox()
		Me.lblMinutesFromNow = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.tpS = New System.Windows.Forms.TabPage()
		Me.lblSearch_ServerCount = New System.Windows.Forms.Label()
		Me.Label14 = New System.Windows.Forms.Label()
		Me.lnkSearch_QuickHelp = New System.Windows.Forms.LinkLabel()
		Me.picSearch_Loading = New System.Windows.Forms.PictureBox()
		Me.lstSearch_Search = New System.Windows.Forms.ListView()
		Me.cmdSearch_UpdateSCOMSearch = New System.Windows.Forms.Button()
		Me.cmdSearch_Cancel = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.cmdSearch_Add = New System.Windows.Forms.Button()
		Me.txtSearch_Search = New System.Windows.Forms.TextBox()
		Me.tpA = New System.Windows.Forms.TabPage()
		Me.txtAdmin_DefaultsDisallowed = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.chkAdmin_Category = New System.Windows.Forms.CheckBox()
		Me.Label17 = New System.Windows.Forms.Label()
		Me.cmoAdmin_Category = New System.Windows.Forms.ComboBox()
		Me.cmdAdmin_ConfigPresets_Window = New System.Windows.Forms.Button()
		Me.cmdAdmin_Reset = New System.Windows.Forms.Button()
		Me.picAdmin_TestResult = New System.Windows.Forms.PictureBox()
		Me.cmdAdmin_TestConnection = New System.Windows.Forms.Button()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.txtAdmin_DefaultsComment = New System.Windows.Forms.TextBox()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.txtAdmin_DefaultsWindow = New System.Windows.Forms.TextBox()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.lblFQDN = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.txtAdmin_ManagementServer = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.cmdAdmin_Save = New System.Windows.Forms.Button()
		Me.lblManagementServer = New System.Windows.Forms.Label()
		Me.picHelp = New System.Windows.Forms.PictureBox()
		Me.img16 = New System.Windows.Forms.ImageList(Me.components)
		Me.mnuRightClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.panLoadingDLLs = New System.Windows.Forms.Panel()
		Me.lblLoadingDLLs = New System.Windows.Forms.Label()
		Me.picLoadingDLLs = New System.Windows.Forms.PictureBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.tpR.SuspendLayout()
		CType(Me.picMaint_COM, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picMaint_CLU, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpL.SuspendLayout()
		Me.tPages.SuspendLayout()
		Me.tpO.SuspendLayout()
		Me.tpS.SuspendLayout()
		CType(Me.picSearch_Loading, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tpA.SuspendLayout()
		CType(Me.picAdmin_TestResult, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picHelp, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.panLoadingDLLs.SuspendLayout()
		CType(Me.picLoadingDLLs, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'dlgOpenFile
		'
		Me.dlgOpenFile.FileName = "OpenFileDialog1"
		'
		'tpR
		'
		Me.tpR.BackColor = System.Drawing.SystemColors.Control
		Me.tpR.Controls.Add(Me.picMaint_COM)
		Me.tpR.Controls.Add(Me.picMaint_CLU)
		Me.tpR.Controls.Add(Me.cmdMaint_ViewLog)
		Me.tpR.Controls.Add(Me.lblMaint_WhatIfMode)
		Me.tpR.Controls.Add(Me.cmdMaint_Done)
		Me.tpR.Controls.Add(Me.lstMaint_Results)
		Me.tpR.Location = New System.Drawing.Point(4, 28)
		Me.tpR.Margin = New System.Windows.Forms.Padding(0)
		Me.tpR.Name = "tpR"
		Me.tpR.Padding = New System.Windows.Forms.Padding(9)
		Me.tpR.Size = New System.Drawing.Size(443, 359)
		Me.tpR.TabIndex = 1
		Me.tpR.Text = "Maintenance Results"
		'
		'picMaint_COM
		'
		Me.picMaint_COM.Location = New System.Drawing.Point(377, 12)
		Me.picMaint_COM.Name = "picMaint_COM"
		Me.picMaint_COM.Size = New System.Drawing.Size(16, 16)
		Me.picMaint_COM.TabIndex = 9
		Me.picMaint_COM.TabStop = False
		'
		'picMaint_CLU
		'
		Me.picMaint_CLU.Location = New System.Drawing.Point(399, 12)
		Me.picMaint_CLU.Name = "picMaint_CLU"
		Me.picMaint_CLU.Size = New System.Drawing.Size(16, 16)
		Me.picMaint_CLU.TabIndex = 8
		Me.picMaint_CLU.TabStop = False
		'
		'cmdMaint_ViewLog
		'
		Me.cmdMaint_ViewLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdMaint_ViewLog.Location = New System.Drawing.Point(12, 322)
		Me.cmdMaint_ViewLog.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdMaint_ViewLog.Name = "cmdMaint_ViewLog"
		Me.cmdMaint_ViewLog.Size = New System.Drawing.Size(125, 25)
		Me.cmdMaint_ViewLog.TabIndex = 7
		Me.cmdMaint_ViewLog.Text = "View Log File"
		Me.cmdMaint_ViewLog.UseVisualStyleBackColor = True
		'
		'lblMaint_WhatIfMode
		'
		Me.lblMaint_WhatIfMode.AutoSize = True
		Me.lblMaint_WhatIfMode.Location = New System.Drawing.Point(12, 15)
		Me.lblMaint_WhatIfMode.Name = "lblMaint_WhatIfMode"
		Me.lblMaint_WhatIfMode.Size = New System.Drawing.Size(85, 13)
		Me.lblMaint_WhatIfMode.TabIndex = 5
		Me.lblMaint_WhatIfMode.Text = "'WhatIf' Enabled"
		'
		'cmdMaint_Done
		'
		Me.cmdMaint_Done.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdMaint_Done.Location = New System.Drawing.Point(356, 322)
		Me.cmdMaint_Done.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdMaint_Done.Name = "cmdMaint_Done"
		Me.cmdMaint_Done.Size = New System.Drawing.Size(75, 25)
		Me.cmdMaint_Done.TabIndex = 1
		Me.cmdMaint_Done.Text = "Done"
		Me.cmdMaint_Done.UseVisualStyleBackColor = True
		'
		'lstMaint_Results
		'
		Me.lstMaint_Results.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstMaint_Results.Location = New System.Drawing.Point(12, 34)
		Me.lstMaint_Results.Name = "lstMaint_Results"
		Me.lstMaint_Results.OwnerDraw = True
		Me.lstMaint_Results.Size = New System.Drawing.Size(419, 273)
		Me.lstMaint_Results.TabIndex = 0
		Me.lstMaint_Results.UseCompatibleStateImageBehavior = False
		'
		'tpL
		'
		Me.tpL.AllowDrop = True
		Me.tpL.BackColor = System.Drawing.SystemColors.Control
		Me.tpL.Controls.Add(Me.cmdEntry_Verify)
		Me.tpL.Controls.Add(Me.cmdEntry_Next)
		Me.tpL.Controls.Add(Me.lblFQDNNote1)
		Me.tpL.Controls.Add(Me.cmdEntry_Export)
		Me.tpL.Controls.Add(Me.cmdEntry_SearchSCOM)
		Me.tpL.Controls.Add(Me.txtEntry_ServerList)
		Me.tpL.Controls.Add(Me.Label2)
		Me.tpL.Controls.Add(Me.cmdEntry_Import)
		Me.tpL.Controls.Add(Me.Label1)
		Me.tpL.Location = New System.Drawing.Point(4, 28)
		Me.tpL.Margin = New System.Windows.Forms.Padding(0)
		Me.tpL.Name = "tpL"
		Me.tpL.Padding = New System.Windows.Forms.Padding(9)
		Me.tpL.Size = New System.Drawing.Size(443, 359)
		Me.tpL.TabIndex = 0
		Me.tpL.Text = "Server List"
		'
		'cmdEntry_Verify
		'
		Me.cmdEntry_Verify.Location = New System.Drawing.Point(306, 322)
		Me.cmdEntry_Verify.Name = "cmdEntry_Verify"
		Me.cmdEntry_Verify.Size = New System.Drawing.Size(125, 25)
		Me.cmdEntry_Verify.TabIndex = 89
		Me.cmdEntry_Verify.Text = "Verify Server List"
		Me.cmdEntry_Verify.UseVisualStyleBackColor = True
		'
		'cmdEntry_Next
		'
		Me.cmdEntry_Next.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdEntry_Next.Location = New System.Drawing.Point(100, 322)
		Me.cmdEntry_Next.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdEntry_Next.Name = "cmdEntry_Next"
		Me.cmdEntry_Next.Size = New System.Drawing.Size(75, 25)
		Me.cmdEntry_Next.TabIndex = 1
		Me.cmdEntry_Next.Text = "Next  >"
		Me.cmdEntry_Next.UseVisualStyleBackColor = True
		'
		'lblFQDNNote1
		'
		Me.lblFQDNNote1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblFQDNNote1.Enabled = False
		Me.lblFQDNNote1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblFQDNNote1.Location = New System.Drawing.Point(12, 71)
		Me.lblFQDNNote1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 1)
		Me.lblFQDNNote1.Name = "lblFQDNNote1"
		Me.lblFQDNNote1.Size = New System.Drawing.Size(82, 236)
		Me.lblFQDNNote1.TabIndex = 88
		Me.lblFQDNNote1.Text = "In most cases the FQDN will be required" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If in doubt, try the search function" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
		  " "
		Me.lblFQDNNote1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		'
		'cmdEntry_Export
		'
		Me.cmdEntry_Export.Location = New System.Drawing.Point(190, 12)
		Me.cmdEntry_Export.Name = "cmdEntry_Export"
		Me.cmdEntry_Export.Size = New System.Drawing.Size(75, 25)
		Me.cmdEntry_Export.TabIndex = 3
		Me.cmdEntry_Export.TabStop = False
		Me.cmdEntry_Export.Text = "Export"
		Me.cmdEntry_Export.UseVisualStyleBackColor = True
		'
		'cmdEntry_SearchSCOM
		'
		Me.cmdEntry_SearchSCOM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdEntry_SearchSCOM.Location = New System.Drawing.Point(306, 12)
		Me.cmdEntry_SearchSCOM.Name = "cmdEntry_SearchSCOM"
		Me.cmdEntry_SearchSCOM.Size = New System.Drawing.Size(125, 25)
		Me.cmdEntry_SearchSCOM.TabIndex = 4
		Me.cmdEntry_SearchSCOM.Text = "Search SCOM"
		Me.cmdEntry_SearchSCOM.UseVisualStyleBackColor = True
		'
		'txtEntry_ServerList
		'
		Me.txtEntry_ServerList.AllowDrop = True
		Me.txtEntry_ServerList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtEntry_ServerList.Location = New System.Drawing.Point(100, 49)
		Me.txtEntry_ServerList.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.txtEntry_ServerList.Multiline = True
		Me.txtEntry_ServerList.Name = "txtEntry_ServerList"
		Me.txtEntry_ServerList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtEntry_ServerList.Size = New System.Drawing.Size(331, 258)
		Me.txtEntry_ServerList.TabIndex = 0
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 52)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(60, 13)
		Me.Label2.TabIndex = 66
		Me.Label2.Text = "Server List:"
		'
		'cmdEntry_Import
		'
		Me.cmdEntry_Import.Location = New System.Drawing.Point(100, 12)
		Me.cmdEntry_Import.Margin = New System.Windows.Forms.Padding(3, 3, 12, 3)
		Me.cmdEntry_Import.Name = "cmdEntry_Import"
		Me.cmdEntry_Import.Size = New System.Drawing.Size(75, 25)
		Me.cmdEntry_Import.TabIndex = 2
		Me.cmdEntry_Import.Text = "Import"
		Me.cmdEntry_Import.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 18)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(75, 13)
		Me.Label1.TabIndex = 63
		Me.Label1.Text = "List Functions:"
		'
		'tPages
		'
		Me.tPages.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tPages.Controls.Add(Me.tpL)
		Me.tPages.Controls.Add(Me.tpO)
		Me.tPages.Controls.Add(Me.tpR)
		Me.tPages.Controls.Add(Me.tpS)
		Me.tPages.Controls.Add(Me.tpA)
		Me.tPages.Location = New System.Drawing.Point(9, 9)
		Me.tPages.Margin = New System.Windows.Forms.Padding(0)
		Me.tPages.Name = "tPages"
		Me.tPages.Padding = New System.Drawing.Point(12, 6)
		Me.tPages.SelectedIndex = 0
		Me.tPages.Size = New System.Drawing.Size(451, 391)
		Me.tPages.TabIndex = 0
		Me.tPages.TabStop = False
		'
		'tpO
		'
		Me.tpO.Controls.Add(Me.lblEntry_Schedule)
		Me.tpO.Controls.Add(Me.chkEntry_Schedule)
		Me.tpO.Controls.Add(Me.chkEntry_Category)
		Me.tpO.Controls.Add(Me.Label16)
		Me.tpO.Controls.Add(Me.cmoEntry_Category)
		Me.tpO.Controls.Add(Me.radEntry_Window2)
		Me.tpO.Controls.Add(Me.radEntry_Window1)
		Me.tpO.Controls.Add(Me.cmdEntry_Back)
		Me.tpO.Controls.Add(Me.dtpEntry_Window)
		Me.tpO.Controls.Add(Me.cmdEntry_WindowPresets)
		Me.tpO.Controls.Add(Me.Label9)
		Me.tpO.Controls.Add(Me.chkEntry_WhatIf)
		Me.tpO.Controls.Add(Me.lblSetMaintenanceMode)
		Me.tpO.Controls.Add(Me.cmdEntry_Stop)
		Me.tpO.Controls.Add(Me.cmdEntry_Start)
		Me.tpO.Controls.Add(Me.txtEntry_Window)
		Me.tpO.Controls.Add(Me.txtEntry_Comment)
		Me.tpO.Controls.Add(Me.lblMinutesFromNow)
		Me.tpO.Controls.Add(Me.Label5)
		Me.tpO.Location = New System.Drawing.Point(4, 28)
		Me.tpO.Name = "tpO"
		Me.tpO.Padding = New System.Windows.Forms.Padding(9)
		Me.tpO.Size = New System.Drawing.Size(443, 359)
		Me.tpO.TabIndex = 5
		Me.tpO.Text = "Server List Options"
		'
		'lblEntry_Schedule
		'
		Me.lblEntry_Schedule.AutoSize = True
		Me.lblEntry_Schedule.Location = New System.Drawing.Point(12, 215)
		Me.lblEntry_Schedule.Name = "lblEntry_Schedule"
		Me.lblEntry_Schedule.Size = New System.Drawing.Size(82, 13)
		Me.lblEntry_Schedule.TabIndex = 131
		Me.lblEntry_Schedule.Text = "Schedule Task:"
		'
		'chkEntry_Schedule
		'
		Me.chkEntry_Schedule.AutoSize = True
		Me.chkEntry_Schedule.Location = New System.Drawing.Point(100, 214)
		Me.chkEntry_Schedule.Margin = New System.Windows.Forms.Padding(3, 6, 0, 3)
		Me.chkEntry_Schedule.Name = "chkEntry_Schedule"
		Me.chkEntry_Schedule.Size = New System.Drawing.Size(197, 17)
		Me.chkEntry_Schedule.TabIndex = 130
		Me.chkEntry_Schedule.Text = "Schedule a future maintenance task"
		Me.chkEntry_Schedule.UseVisualStyleBackColor = True
		'
		'chkEntry_Category
		'
		Me.chkEntry_Category.AutoSize = True
		Me.chkEntry_Category.Location = New System.Drawing.Point(100, 91)
		Me.chkEntry_Category.Name = "chkEntry_Category"
		Me.chkEntry_Category.Size = New System.Drawing.Size(65, 17)
		Me.chkEntry_Category.TabIndex = 5
		Me.chkEntry_Category.Text = "Planned"
		Me.chkEntry_Category.UseVisualStyleBackColor = True
		'
		'Label16
		'
		Me.Label16.AutoSize = True
		Me.Label16.Location = New System.Drawing.Point(13, 92)
		Me.Label16.Name = "Label16"
		Me.Label16.Size = New System.Drawing.Size(52, 13)
		Me.Label16.TabIndex = 129
		Me.Label16.Text = "Category:"
		'
		'cmoEntry_Category
		'
		Me.cmoEntry_Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoEntry_Category.FormattingEnabled = True
		Me.cmoEntry_Category.Location = New System.Drawing.Point(171, 89)
		Me.cmoEntry_Category.Margin = New System.Windows.Forms.Padding(3, 12, 3, 6)
		Me.cmoEntry_Category.Name = "cmoEntry_Category"
		Me.cmoEntry_Category.Size = New System.Drawing.Size(260, 21)
		Me.cmoEntry_Category.TabIndex = 6
		'
		'radEntry_Window2
		'
		Me.radEntry_Window2.AutoSize = True
		Me.radEntry_Window2.Location = New System.Drawing.Point(100, 49)
		Me.radEntry_Window2.Margin = New System.Windows.Forms.Padding(0)
		Me.radEntry_Window2.Name = "radEntry_Window2"
		Me.radEntry_Window2.Size = New System.Drawing.Size(14, 13)
		Me.radEntry_Window2.TabIndex = 3
		Me.radEntry_Window2.UseVisualStyleBackColor = True
		'
		'radEntry_Window1
		'
		Me.radEntry_Window1.AutoSize = True
		Me.radEntry_Window1.Checked = True
		Me.radEntry_Window1.Location = New System.Drawing.Point(100, 24)
		Me.radEntry_Window1.Margin = New System.Windows.Forms.Padding(0)
		Me.radEntry_Window1.Name = "radEntry_Window1"
		Me.radEntry_Window1.Size = New System.Drawing.Size(14, 13)
		Me.radEntry_Window1.TabIndex = 0
		Me.radEntry_Window1.TabStop = True
		Me.radEntry_Window1.UseVisualStyleBackColor = True
		'
		'cmdEntry_Back
		'
		Me.cmdEntry_Back.Location = New System.Drawing.Point(12, 322)
		Me.cmdEntry_Back.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdEntry_Back.Name = "cmdEntry_Back"
		Me.cmdEntry_Back.Size = New System.Drawing.Size(75, 25)
		Me.cmdEntry_Back.TabIndex = 12
		Me.cmdEntry_Back.Text = "<  Back"
		Me.cmdEntry_Back.UseVisualStyleBackColor = True
		'
		'dtpEntry_Window
		'
		Me.dtpEntry_Window.CustomFormat = " dd MMMM yyyy          HH:mm"
		Me.dtpEntry_Window.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.dtpEntry_Window.Location = New System.Drawing.Point(117, 45)
		Me.dtpEntry_Window.Margin = New System.Windows.Forms.Padding(3, 2, 3, 12)
		Me.dtpEntry_Window.Name = "dtpEntry_Window"
		Me.dtpEntry_Window.Size = New System.Drawing.Size(200, 20)
		Me.dtpEntry_Window.TabIndex = 4
		'
		'cmdEntry_WindowPresets
		'
		Me.cmdEntry_WindowPresets.Location = New System.Drawing.Point(356, 18)
		Me.cmdEntry_WindowPresets.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdEntry_WindowPresets.Name = "cmdEntry_WindowPresets"
		Me.cmdEntry_WindowPresets.Size = New System.Drawing.Size(75, 25)
		Me.cmdEntry_WindowPresets.TabIndex = 2
		Me.cmdEntry_WindowPresets.Text = "Presets..."
		Me.cmdEntry_WindowPresets.UseVisualStyleBackColor = True
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Location = New System.Drawing.Point(12, 122)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(54, 13)
		Me.Label9.TabIndex = 126
		Me.Label9.Text = "Comment:"
		'
		'chkEntry_WhatIf
		'
		Me.chkEntry_WhatIf.AutoSize = True
		Me.chkEntry_WhatIf.Location = New System.Drawing.Point(100, 188)
		Me.chkEntry_WhatIf.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.chkEntry_WhatIf.Name = "chkEntry_WhatIf"
		Me.chkEntry_WhatIf.Size = New System.Drawing.Size(143, 17)
		Me.chkEntry_WhatIf.TabIndex = 9
		Me.chkEntry_WhatIf.Text = "Perform audit check only"
		Me.chkEntry_WhatIf.UseVisualStyleBackColor = False
		'
		'lblSetMaintenanceMode
		'
		Me.lblSetMaintenanceMode.AutoSize = True
		Me.lblSetMaintenanceMode.Location = New System.Drawing.Point(12, 189)
		Me.lblSetMaintenanceMode.Name = "lblSetMaintenanceMode"
		Me.lblSetMaintenanceMode.Size = New System.Drawing.Size(51, 13)
		Me.lblSetMaintenanceMode.TabIndex = 125
		Me.lblSetMaintenanceMode.Text = "WhatIf.?:"
		'
		'cmdEntry_Stop
		'
		Me.cmdEntry_Stop.Location = New System.Drawing.Point(306, 302)
		Me.cmdEntry_Stop.Name = "cmdEntry_Stop"
		Me.cmdEntry_Stop.Size = New System.Drawing.Size(125, 45)
		Me.cmdEntry_Stop.TabIndex = 11
		Me.cmdEntry_Stop.Text = "Stop"
		Me.cmdEntry_Stop.UseVisualStyleBackColor = True
		'
		'cmdEntry_Start
		'
		Me.cmdEntry_Start.Location = New System.Drawing.Point(166, 302)
		Me.cmdEntry_Start.Margin = New System.Windows.Forms.Padding(3, 3, 12, 3)
		Me.cmdEntry_Start.Name = "cmdEntry_Start"
		Me.cmdEntry_Start.Size = New System.Drawing.Size(125, 45)
		Me.cmdEntry_Start.TabIndex = 10
		Me.cmdEntry_Start.Text = "Start / Update"
		Me.cmdEntry_Start.UseVisualStyleBackColor = True
		'
		'txtEntry_Window
		'
		Me.txtEntry_Window.Location = New System.Drawing.Point(117, 21)
		Me.txtEntry_Window.Margin = New System.Windows.Forms.Padding(3, 6, 3, 2)
		Me.txtEntry_Window.Name = "txtEntry_Window"
		Me.txtEntry_Window.Size = New System.Drawing.Size(58, 20)
		Me.txtEntry_Window.TabIndex = 1
		Me.txtEntry_Window.Text = "60"
		Me.txtEntry_Window.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'txtEntry_Comment
		'
		Me.txtEntry_Comment.Location = New System.Drawing.Point(100, 119)
		Me.txtEntry_Comment.Margin = New System.Windows.Forms.Padding(3, 3, 3, 12)
		Me.txtEntry_Comment.Multiline = True
		Me.txtEntry_Comment.Name = "txtEntry_Comment"
		Me.txtEntry_Comment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtEntry_Comment.Size = New System.Drawing.Size(331, 45)
		Me.txtEntry_Comment.TabIndex = 7
		Me.txtEntry_Comment.Text = "No comment given"
		'
		'lblMinutesFromNow
		'
		Me.lblMinutesFromNow.AutoSize = True
		Me.lblMinutesFromNow.Location = New System.Drawing.Point(178, 24)
		Me.lblMinutesFromNow.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
		Me.lblMinutesFromNow.Name = "lblMinutesFromNow"
		Me.lblMinutesFromNow.Size = New System.Drawing.Size(43, 13)
		Me.lblMinutesFromNow.TabIndex = 124
		Me.lblMinutesFromNow.Text = "minutes"
		'
		'Label5
		'
		Me.Label5.Location = New System.Drawing.Point(12, 21)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(79, 44)
		Me.Label5.TabIndex = 122
		Me.Label5.Text = "Duration or Specific Time:"
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'tpS
		'
		Me.tpS.Controls.Add(Me.lblSearch_ServerCount)
		Me.tpS.Controls.Add(Me.Label14)
		Me.tpS.Controls.Add(Me.lnkSearch_QuickHelp)
		Me.tpS.Controls.Add(Me.picSearch_Loading)
		Me.tpS.Controls.Add(Me.lstSearch_Search)
		Me.tpS.Controls.Add(Me.cmdSearch_UpdateSCOMSearch)
		Me.tpS.Controls.Add(Me.cmdSearch_Cancel)
		Me.tpS.Controls.Add(Me.Label3)
		Me.tpS.Controls.Add(Me.cmdSearch_Add)
		Me.tpS.Controls.Add(Me.txtSearch_Search)
		Me.tpS.Location = New System.Drawing.Point(4, 28)
		Me.tpS.Margin = New System.Windows.Forms.Padding(0)
		Me.tpS.Name = "tpS"
		Me.tpS.Padding = New System.Windows.Forms.Padding(9)
		Me.tpS.Size = New System.Drawing.Size(443, 359)
		Me.tpS.TabIndex = 2
		Me.tpS.Text = "Search SCOM"
		'
		'lblSearch_ServerCount
		'
		Me.lblSearch_ServerCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblSearch_ServerCount.Location = New System.Drawing.Point(143, 322)
		Me.lblSearch_ServerCount.Name = "lblSearch_ServerCount"
		Me.lblSearch_ServerCount.Size = New System.Drawing.Size(117, 25)
		Me.lblSearch_ServerCount.TabIndex = 68
		Me.lblSearch_ServerCount.Text = "0 servers found"
		Me.lblSearch_ServerCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Label14
		'
		Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label14.AutoSize = True
		Me.Label14.Enabled = False
		Me.Label14.Location = New System.Drawing.Point(189, 35)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(242, 13)
		Me.Label14.TabIndex = 67
		Me.Label14.Text = "To show all servers in maintenance mode, use: !m"
		'
		'lnkSearch_QuickHelp
		'
		Me.lnkSearch_QuickHelp.AutoSize = True
		Me.lnkSearch_QuickHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
		Me.lnkSearch_QuickHelp.Location = New System.Drawing.Point(97, 35)
		Me.lnkSearch_QuickHelp.Name = "lnkSearch_QuickHelp"
		Me.lnkSearch_QuickHelp.Padding = New System.Windows.Forms.Padding(1, 0, 0, 0)
		Me.lnkSearch_QuickHelp.Size = New System.Drawing.Size(67, 13)
		Me.lnkSearch_QuickHelp.TabIndex = 66
		Me.lnkSearch_QuickHelp.TabStop = True
		Me.lnkSearch_QuickHelp.Text = "Search Help"
		'
		'picSearch_Loading
		'
		Me.picSearch_Loading.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSearch_Loading.BackColor = System.Drawing.SystemColors.Window
		Me.picSearch_Loading.Location = New System.Drawing.Point(413, 14)
		Me.picSearch_Loading.Name = "picSearch_Loading"
		Me.picSearch_Loading.Size = New System.Drawing.Size(16, 16)
		Me.picSearch_Loading.TabIndex = 65
		Me.picSearch_Loading.TabStop = False
		'
		'lstSearch_Search
		'
		Me.lstSearch_Search.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstSearch_Search.Location = New System.Drawing.Point(12, 51)
		Me.lstSearch_Search.Name = "lstSearch_Search"
		Me.lstSearch_Search.Size = New System.Drawing.Size(419, 256)
		Me.lstSearch_Search.TabIndex = 2
		Me.lstSearch_Search.UseCompatibleStateImageBehavior = False
		'
		'cmdSearch_UpdateSCOMSearch
		'
		Me.cmdSearch_UpdateSCOMSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdSearch_UpdateSCOMSearch.Location = New System.Drawing.Point(12, 322)
		Me.cmdSearch_UpdateSCOMSearch.Name = "cmdSearch_UpdateSCOMSearch"
		Me.cmdSearch_UpdateSCOMSearch.Size = New System.Drawing.Size(125, 25)
		Me.cmdSearch_UpdateSCOMSearch.TabIndex = 5
		Me.cmdSearch_UpdateSCOMSearch.Text = "Update List"
		Me.cmdSearch_UpdateSCOMSearch.UseVisualStyleBackColor = True
		'
		'cmdSearch_Cancel
		'
		Me.cmdSearch_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdSearch_Cancel.Location = New System.Drawing.Point(266, 322)
		Me.cmdSearch_Cancel.Margin = New System.Windows.Forms.Padding(3, 3, 12, 3)
		Me.cmdSearch_Cancel.Name = "cmdSearch_Cancel"
		Me.cmdSearch_Cancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdSearch_Cancel.TabIndex = 4
		Me.cmdSearch_Cancel.Text = "Cancel"
		Me.cmdSearch_Cancel.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 15)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(63, 13)
		Me.Label3.TabIndex = 64
		Me.Label3.Text = "Search List:"
		'
		'cmdSearch_Add
		'
		Me.cmdSearch_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdSearch_Add.Location = New System.Drawing.Point(356, 322)
		Me.cmdSearch_Add.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdSearch_Add.Name = "cmdSearch_Add"
		Me.cmdSearch_Add.Size = New System.Drawing.Size(75, 25)
		Me.cmdSearch_Add.TabIndex = 3
		Me.cmdSearch_Add.Text = "Add"
		Me.cmdSearch_Add.UseVisualStyleBackColor = True
		'
		'txtSearch_Search
		'
		Me.txtSearch_Search.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtSearch_Search.Location = New System.Drawing.Point(100, 12)
		Me.txtSearch_Search.Name = "txtSearch_Search"
		Me.txtSearch_Search.Size = New System.Drawing.Size(331, 20)
		Me.txtSearch_Search.TabIndex = 0
		Me.txtSearch_Search.Tag = ""
		'
		'tpA
		'
		Me.tpA.Controls.Add(Me.Label7)
		Me.tpA.Controls.Add(Me.txtAdmin_DefaultsDisallowed)
		Me.tpA.Controls.Add(Me.Label6)
		Me.tpA.Controls.Add(Me.chkAdmin_Category)
		Me.tpA.Controls.Add(Me.Label17)
		Me.tpA.Controls.Add(Me.cmoAdmin_Category)
		Me.tpA.Controls.Add(Me.cmdAdmin_ConfigPresets_Window)
		Me.tpA.Controls.Add(Me.cmdAdmin_Reset)
		Me.tpA.Controls.Add(Me.picAdmin_TestResult)
		Me.tpA.Controls.Add(Me.cmdAdmin_TestConnection)
		Me.tpA.Controls.Add(Me.Label13)
		Me.tpA.Controls.Add(Me.Label12)
		Me.tpA.Controls.Add(Me.txtAdmin_DefaultsComment)
		Me.tpA.Controls.Add(Me.Label11)
		Me.tpA.Controls.Add(Me.txtAdmin_DefaultsWindow)
		Me.tpA.Controls.Add(Me.Label10)
		Me.tpA.Controls.Add(Me.lblFQDN)
		Me.tpA.Controls.Add(Me.Label8)
		Me.tpA.Controls.Add(Me.txtAdmin_ManagementServer)
		Me.tpA.Controls.Add(Me.Label4)
		Me.tpA.Controls.Add(Me.cmdAdmin_Save)
		Me.tpA.Location = New System.Drawing.Point(4, 28)
		Me.tpA.Margin = New System.Windows.Forms.Padding(0)
		Me.tpA.Name = "tpA"
		Me.tpA.Padding = New System.Windows.Forms.Padding(9)
		Me.tpA.Size = New System.Drawing.Size(443, 359)
		Me.tpA.TabIndex = 4
		Me.tpA.Text = "Admin Options"
		'
		'txtAdmin_DefaultsDisallowed
		'
		Me.txtAdmin_DefaultsDisallowed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtAdmin_DefaultsDisallowed.Location = New System.Drawing.Point(100, 268)
		Me.txtAdmin_DefaultsDisallowed.Margin = New System.Windows.Forms.Padding(3, 9, 3, 1)
		Me.txtAdmin_DefaultsDisallowed.Name = "txtAdmin_DefaultsDisallowed"
		Me.txtAdmin_DefaultsDisallowed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtAdmin_DefaultsDisallowed.Size = New System.Drawing.Size(331, 20)
		Me.txtAdmin_DefaultsDisallowed.TabIndex = 133
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(24, 271)
		Me.Label6.Margin = New System.Windows.Forms.Padding(9, 0, 3, 16)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(61, 13)
		Me.Label6.TabIndex = 134
		Me.Label6.Text = "Disallowed:"
		'
		'chkAdmin_Category
		'
		Me.chkAdmin_Category.AutoSize = True
		Me.chkAdmin_Category.Location = New System.Drawing.Point(100, 183)
		Me.chkAdmin_Category.Name = "chkAdmin_Category"
		Me.chkAdmin_Category.Size = New System.Drawing.Size(65, 17)
		Me.chkAdmin_Category.TabIndex = 4
		Me.chkAdmin_Category.Text = "Planned"
		Me.chkAdmin_Category.UseVisualStyleBackColor = True
		'
		'Label17
		'
		Me.Label17.AutoSize = True
		Me.Label17.Location = New System.Drawing.Point(24, 184)
		Me.Label17.Name = "Label17"
		Me.Label17.Size = New System.Drawing.Size(52, 13)
		Me.Label17.TabIndex = 132
		Me.Label17.Text = "Category:"
		'
		'cmoAdmin_Category
		'
		Me.cmoAdmin_Category.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoAdmin_Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoAdmin_Category.FormattingEnabled = True
		Me.cmoAdmin_Category.Location = New System.Drawing.Point(171, 181)
		Me.cmoAdmin_Category.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.cmoAdmin_Category.Name = "cmoAdmin_Category"
		Me.cmoAdmin_Category.Size = New System.Drawing.Size(260, 21)
		Me.cmoAdmin_Category.TabIndex = 5
		'
		'cmdAdmin_ConfigPresets_Window
		'
		Me.cmdAdmin_ConfigPresets_Window.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdAdmin_ConfigPresets_Window.Location = New System.Drawing.Point(306, 147)
		Me.cmdAdmin_ConfigPresets_Window.Margin = New System.Windows.Forms.Padding(3, 36, 3, 3)
		Me.cmdAdmin_ConfigPresets_Window.Name = "cmdAdmin_ConfigPresets_Window"
		Me.cmdAdmin_ConfigPresets_Window.Size = New System.Drawing.Size(125, 25)
		Me.cmdAdmin_ConfigPresets_Window.TabIndex = 9
		Me.cmdAdmin_ConfigPresets_Window.Text = "Time Presets..."
		Me.cmdAdmin_ConfigPresets_Window.UseVisualStyleBackColor = True
		'
		'cmdAdmin_Reset
		'
		Me.cmdAdmin_Reset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdAdmin_Reset.Location = New System.Drawing.Point(12, 322)
		Me.cmdAdmin_Reset.Name = "cmdAdmin_Reset"
		Me.cmdAdmin_Reset.Size = New System.Drawing.Size(75, 25)
		Me.cmdAdmin_Reset.TabIndex = 82
		Me.cmdAdmin_Reset.Text = "Reset All"
		Me.cmdAdmin_Reset.UseVisualStyleBackColor = True
		'
		'picAdmin_TestResult
		'
		Me.picAdmin_TestResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picAdmin_TestResult.Location = New System.Drawing.Point(284, 87)
		Me.picAdmin_TestResult.Name = "picAdmin_TestResult"
		Me.picAdmin_TestResult.Size = New System.Drawing.Size(16, 16)
		Me.picAdmin_TestResult.TabIndex = 79
		Me.picAdmin_TestResult.TabStop = False
		'
		'cmdAdmin_TestConnection
		'
		Me.cmdAdmin_TestConnection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdAdmin_TestConnection.Location = New System.Drawing.Point(306, 83)
		Me.cmdAdmin_TestConnection.Name = "cmdAdmin_TestConnection"
		Me.cmdAdmin_TestConnection.Size = New System.Drawing.Size(125, 25)
		Me.cmdAdmin_TestConnection.TabIndex = 1
		Me.cmdAdmin_TestConnection.Text = "Test Connection"
		Me.cmdAdmin_TestConnection.UseVisualStyleBackColor = True
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Location = New System.Drawing.Point(12, 125)
		Me.Label13.Margin = New System.Windows.Forms.Padding(3, 30, 3, 3)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(101, 13)
		Me.Label13.TabIndex = 74
		Me.Label13.Text = "Application Defaults"
		'
		'Label12
		'
		Me.Label12.AutoSize = True
		Me.Label12.Location = New System.Drawing.Point(156, 153)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(43, 13)
		Me.Label12.TabIndex = 3
		Me.Label12.Text = "minutes"
		'
		'txtAdmin_DefaultsComment
		'
		Me.txtAdmin_DefaultsComment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtAdmin_DefaultsComment.Location = New System.Drawing.Point(100, 211)
		Me.txtAdmin_DefaultsComment.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.txtAdmin_DefaultsComment.Multiline = True
		Me.txtAdmin_DefaultsComment.Name = "txtAdmin_DefaultsComment"
		Me.txtAdmin_DefaultsComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtAdmin_DefaultsComment.Size = New System.Drawing.Size(331, 45)
		Me.txtAdmin_DefaultsComment.TabIndex = 6
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Location = New System.Drawing.Point(24, 214)
		Me.Label11.Margin = New System.Windows.Forms.Padding(9, 0, 3, 16)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(54, 13)
		Me.Label11.TabIndex = 7
		Me.Label11.Text = "Comment:"
		'
		'txtAdmin_DefaultsWindow
		'
		Me.txtAdmin_DefaultsWindow.Location = New System.Drawing.Point(100, 150)
		Me.txtAdmin_DefaultsWindow.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.txtAdmin_DefaultsWindow.Name = "txtAdmin_DefaultsWindow"
		Me.txtAdmin_DefaultsWindow.Size = New System.Drawing.Size(50, 20)
		Me.txtAdmin_DefaultsWindow.TabIndex = 2
		Me.txtAdmin_DefaultsWindow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(24, 153)
		Me.Label10.Margin = New System.Windows.Forms.Padding(3, 12, 3, 16)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(50, 13)
		Me.Label10.TabIndex = 5
		Me.Label10.Text = "Duration:"
		'
		'lblFQDN
		'
		Me.lblFQDN.AutoSize = True
		Me.lblFQDN.Enabled = False
		Me.lblFQDN.Location = New System.Drawing.Point(97, 64)
		Me.lblFQDN.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lblFQDN.Name = "lblFQDN"
		Me.lblFQDN.Size = New System.Drawing.Size(253, 13)
		Me.lblFQDN.TabIndex = 4
		Me.lblFQDN.Text = "This should be the FQDN of the management server"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(12, 12)
		Me.Label8.Margin = New System.Windows.Forms.Padding(3)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(300, 13)
		Me.Label8.TabIndex = 3
		Me.Label8.Text = "These options configure the default settings for the application"
		'
		'txtAdmin_ManagementServer
		'
		Me.txtAdmin_ManagementServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtAdmin_ManagementServer.Location = New System.Drawing.Point(100, 43)
		Me.txtAdmin_ManagementServer.Margin = New System.Windows.Forms.Padding(3, 12, 3, 1)
		Me.txtAdmin_ManagementServer.Name = "txtAdmin_ManagementServer"
		Me.txtAdmin_ManagementServer.Size = New System.Drawing.Size(331, 20)
		Me.txtAdmin_ManagementServer.TabIndex = 0
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 46)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(75, 13)
		Me.Label4.TabIndex = 1
		Me.Label4.Text = "SCOM Server:"
		'
		'cmdAdmin_Save
		'
		Me.cmdAdmin_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdAdmin_Save.Location = New System.Drawing.Point(356, 322)
		Me.cmdAdmin_Save.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdAdmin_Save.Name = "cmdAdmin_Save"
		Me.cmdAdmin_Save.Size = New System.Drawing.Size(75, 25)
		Me.cmdAdmin_Save.TabIndex = 8
		Me.cmdAdmin_Save.Text = "Save"
		Me.cmdAdmin_Save.UseVisualStyleBackColor = True
		'
		'lblManagementServer
		'
		Me.lblManagementServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblManagementServer.Enabled = False
		Me.lblManagementServer.Location = New System.Drawing.Point(60, 404)
		Me.lblManagementServer.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.lblManagementServer.Name = "lblManagementServer"
		Me.lblManagementServer.Size = New System.Drawing.Size(400, 13)
		Me.lblManagementServer.TabIndex = 1
		Me.lblManagementServer.Text = "Management Server: Unknown"
		Me.lblManagementServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'picHelp
		'
		Me.picHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.picHelp.Location = New System.Drawing.Point(12, 404)
		Me.picHelp.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
		Me.picHelp.Name = "picHelp"
		Me.picHelp.Size = New System.Drawing.Size(13, 13)
		Me.picHelp.TabIndex = 3
		Me.picHelp.TabStop = False
		'
		'img16
		'
		Me.img16.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
		Me.img16.ImageSize = New System.Drawing.Size(16, 16)
		Me.img16.TransparentColor = System.Drawing.Color.Transparent
		'
		'mnuRightClick
		'
		Me.mnuRightClick.Name = "mnuRightClick"
		Me.mnuRightClick.Size = New System.Drawing.Size(61, 4)
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.AutoSize = True
		Me.lnkHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
		Me.lnkHelp.Location = New System.Drawing.Point(25, 404)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0, 5, 3, 0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(29, 13)
		Me.lnkHelp.TabIndex = 6
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		'
		'panLoadingDLLs
		'
		Me.panLoadingDLLs.Controls.Add(Me.lblLoadingDLLs)
		Me.panLoadingDLLs.Controls.Add(Me.picLoadingDLLs)
		Me.panLoadingDLLs.Location = New System.Drawing.Point(203, 9)
		Me.panLoadingDLLs.Name = "panLoadingDLLs"
		Me.panLoadingDLLs.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.panLoadingDLLs.Size = New System.Drawing.Size(254, 22)
		Me.panLoadingDLLs.TabIndex = 7
		'
		'lblLoadingDLLs
		'
		Me.lblLoadingDLLs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblLoadingDLLs.Location = New System.Drawing.Point(12, 4)
		Me.lblLoadingDLLs.Margin = New System.Windows.Forms.Padding(0)
		Me.lblLoadingDLLs.Name = "lblLoadingDLLs"
		Me.lblLoadingDLLs.Size = New System.Drawing.Size(222, 15)
		Me.lblLoadingDLLs.TabIndex = 91
		Me.lblLoadingDLLs.Text = "Loading SCOM System Files"
		Me.lblLoadingDLLs.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picLoadingDLLs
		'
		Me.picLoadingDLLs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picLoadingDLLs.Location = New System.Drawing.Point(234, 3)
		Me.picLoadingDLLs.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picLoadingDLLs.Name = "picLoadingDLLs"
		Me.picLoadingDLLs.Size = New System.Drawing.Size(16, 16)
		Me.picLoadingDLLs.TabIndex = 92
		Me.picLoadingDLLs.TabStop = False
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Enabled = False
		Me.Label7.Location = New System.Drawing.Point(97, 289)
		Me.Label7.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(319, 13)
		Me.Label7.TabIndex = 135
		Me.Label7.Text = "List of comma-seperated servers not allowed to enter maintenance"
		'
		'frmMain
		'
		Me.AllowDrop = True
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(469, 422)
		Me.Controls.Add(Me.panLoadingDLLs)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picHelp)
		Me.Controls.Add(Me.lblManagementServer)
		Me.Controls.Add(Me.tPages)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.Name = "frmMain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "SCOM 2012 Maintenance Mode"
		Me.tpR.ResumeLayout(False)
		Me.tpR.PerformLayout()
		CType(Me.picMaint_COM, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picMaint_CLU, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpL.ResumeLayout(False)
		Me.tpL.PerformLayout()
		Me.tPages.ResumeLayout(False)
		Me.tpO.ResumeLayout(False)
		Me.tpO.PerformLayout()
		Me.tpS.ResumeLayout(False)
		Me.tpS.PerformLayout()
		CType(Me.picSearch_Loading, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tpA.ResumeLayout(False)
		Me.tpA.PerformLayout()
		CType(Me.picAdmin_TestResult, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picHelp, System.ComponentModel.ISupportInitialize).EndInit()
		Me.panLoadingDLLs.ResumeLayout(False)
		CType(Me.picLoadingDLLs, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
	Friend WithEvents dlgSaveFile As System.Windows.Forms.SaveFileDialog
	Friend WithEvents tpR As System.Windows.Forms.TabPage
	Friend WithEvents cmdMaint_Done As System.Windows.Forms.Button
	Friend WithEvents tpL As System.Windows.Forms.TabPage
	Friend WithEvents cmdEntry_Export As System.Windows.Forms.Button
	Friend WithEvents cmdEntry_SearchSCOM As System.Windows.Forms.Button
	Friend WithEvents txtEntry_ServerList As System.Windows.Forms.TextBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents cmdEntry_Import As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents tPages As System.Windows.Forms.TabControl
	Friend WithEvents tpS As System.Windows.Forms.TabPage
	Friend WithEvents cmdSearch_Cancel As System.Windows.Forms.Button
	Friend WithEvents cmdSearch_Add As System.Windows.Forms.Button
	Friend WithEvents txtSearch_Search As System.Windows.Forms.TextBox
	Friend WithEvents lblManagementServer As System.Windows.Forms.Label
	Friend WithEvents tpA As System.Windows.Forms.TabPage
	Friend WithEvents Label13 As System.Windows.Forms.Label
	Friend WithEvents Label12 As System.Windows.Forms.Label
	Friend WithEvents txtAdmin_DefaultsComment As System.Windows.Forms.TextBox
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents txtAdmin_DefaultsWindow As System.Windows.Forms.TextBox
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents lblFQDN As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents txtAdmin_ManagementServer As System.Windows.Forms.TextBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents cmdAdmin_Save As System.Windows.Forms.Button
	Friend WithEvents cmdSearch_UpdateSCOMSearch As System.Windows.Forms.Button
	Friend WithEvents picHelp As System.Windows.Forms.PictureBox
	Friend WithEvents img16 As System.Windows.Forms.ImageList
	Friend WithEvents cmdAdmin_TestConnection As System.Windows.Forms.Button
	Friend WithEvents picAdmin_TestResult As System.Windows.Forms.PictureBox
	Friend WithEvents lstSearch_Search As System.Windows.Forms.ListView
	Friend WithEvents lblMaint_WhatIfMode As System.Windows.Forms.Label
	Friend WithEvents mnuRightClick As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents lblFQDNNote1 As System.Windows.Forms.Label
	Friend WithEvents lstMaint_Results As SCOM2012MaintenanceMode.ctrlListView_SubIcons
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdAdmin_Reset As System.Windows.Forms.Button
	Friend WithEvents cmdAdmin_ConfigPresets_Window As System.Windows.Forms.Button
	Friend WithEvents picSearch_Loading As System.Windows.Forms.PictureBox
	Friend WithEvents lnkSearch_QuickHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents cmdEntry_Next As System.Windows.Forms.Button
	Friend WithEvents tpO As System.Windows.Forms.TabPage
	Friend WithEvents cmdEntry_Back As System.Windows.Forms.Button
	Friend WithEvents dtpEntry_Window As System.Windows.Forms.DateTimePicker
	Friend WithEvents cmdEntry_WindowPresets As System.Windows.Forms.Button
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents chkEntry_WhatIf As System.Windows.Forms.CheckBox
	Friend WithEvents lblSetMaintenanceMode As System.Windows.Forms.Label
	Friend WithEvents cmdEntry_Stop As System.Windows.Forms.Button
	Friend WithEvents cmdEntry_Start As System.Windows.Forms.Button
	Friend WithEvents txtEntry_Window As System.Windows.Forms.TextBox
	Friend WithEvents txtEntry_Comment As System.Windows.Forms.TextBox
	Friend WithEvents lblMinutesFromNow As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label14 As System.Windows.Forms.Label
	Friend WithEvents radEntry_Window2 As System.Windows.Forms.RadioButton
	Friend WithEvents radEntry_Window1 As System.Windows.Forms.RadioButton
	Friend WithEvents lblSearch_ServerCount As System.Windows.Forms.Label
	Friend WithEvents Label16 As System.Windows.Forms.Label
	Friend WithEvents cmoEntry_Category As System.Windows.Forms.ComboBox
	Friend WithEvents chkEntry_Category As System.Windows.Forms.CheckBox
	Friend WithEvents chkAdmin_Category As System.Windows.Forms.CheckBox
	Friend WithEvents Label17 As System.Windows.Forms.Label
	Friend WithEvents cmoAdmin_Category As System.Windows.Forms.ComboBox
	Friend WithEvents lblEntry_Schedule As System.Windows.Forms.Label
	Friend WithEvents chkEntry_Schedule As System.Windows.Forms.CheckBox
	Friend WithEvents cmdMaint_ViewLog As System.Windows.Forms.Button
	Friend WithEvents panLoadingDLLs As System.Windows.Forms.Panel
	Friend WithEvents lblLoadingDLLs As System.Windows.Forms.Label
	Friend WithEvents picLoadingDLLs As System.Windows.Forms.PictureBox
	Friend WithEvents picMaint_COM As System.Windows.Forms.PictureBox
	Friend WithEvents picMaint_CLU As System.Windows.Forms.PictureBox
	Friend WithEvents cmdEntry_Verify As System.Windows.Forms.Button
	Friend WithEvents txtAdmin_DefaultsDisallowed As System.Windows.Forms.TextBox
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label

End Class

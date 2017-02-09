Option Explicit On

Public Class ctrlListView_SubIcons
	Inherits System.Windows.Forms.ListView

	Private Const LVM_FIRST As Integer = 4096
	Private Const LVM_GETEXTENDEDLISTVIEWSTYLE As Integer = (LVM_FIRST + 54)
	Private Const LVS_EX_GRIDLINES As Integer = 1
	Private Const LVS_EX_SUBITEMIMAGES As Integer = 2
	Private Const LVS_EX_CHECKBOXES As Integer = 4
	Private Const LVS_EX_TRACKSELECT As Integer = 8
	Private Const LVS_EX_FULLROWSELECT As Integer = 32	' applies to report mode only
	Private Const WM_NOTIFY As Integer = 78

	' Change the style to accept images on subitems.
	Public Sub New()
		Me.OwnerDraw = True
		Me.DoubleBuffered = True
		AddHandler Me.HandleCreated, AddressOf ctrlListView_SubIcons_HandleCreated
		AddHandler Me.DrawSubItem, AddressOf ctrlListView_SubIcons_DrawSubItem
		AddHandler Me.DrawColumnHeader, AddressOf ctrlListView_SubIcons_DrawColumnHeader
	End Sub

	Private Sub ctrlListView_SubIcons_HandleCreated(ByVal sender As Object, ByVal e As EventArgs)
		' Change the style of listview to accept image on subitems
		Dim m As System.Windows.Forms.Message = New Message
		m.HWnd = Me.Handle
		m.Msg = LVM_GETEXTENDEDLISTVIEWSTYLE
		m.LParam = New IntPtr(LVS_EX_SUBITEMIMAGES Or LVS_EX_FULLROWSELECT Or LVS_EX_TRACKSELECT Or LVS_EX_CHECKBOXES Or LVS_EX_GRIDLINES)
		m.WParam = IntPtr.Zero
		Me.WndProc(m)
	End Sub

	' Handle DrawSubItem event
	Private Sub ctrlListView_SubIcons_DrawSubItem(ByVal sender As Object, ByVal e As DrawListViewSubItemEventArgs)
		Dim r As Rectangle
		Dim bFirstColumn As Boolean
		Dim iImg As String = e.Item.ImageKey
		Dim rItemBounds As Rectangle = New Rectangle(e.Item.Bounds.X, e.Item.Bounds.Y, e.Item.Bounds.Width - 1, e.Item.Bounds.Height - 1)
		Dim cControl As ctrlListView_SubIcons = CType(sender, ctrlListView_SubIcons)
		If (e.SubItem.Bounds.X = e.Item.Bounds.X) Then bFirstColumn = True

		Dim cImageCtrl As ImageList = cControl.SmallImageList
		If ((e.SubItem.Text.StartsWith("ICON:") = True) Or (e.SubItem.Text.StartsWith("BOTH:") = True)) Then
			If (e.Bounds.Width = 16) Then
				e.DrawDefault = False
				e.Graphics.DrawRectangle(SystemPens.Window, e.SubItem.Bounds)
				Exit Sub
			End If
			iImg = cImageCtrl.Images.Keys(CInt(e.SubItem.Text.Substring(5, 1)))
		End If

		'Dim wImg As Integer = Me.SmallImageList.ImageSize.Width
		'Dim hImg As Integer = Me.SmallImageList.ImageSize.Height
		Dim wImg As Integer = 16
		Dim hImg As Integer = 16
		Dim xPos As Integer = (e.SubItem.Bounds.X + CInt(e.SubItem.Bounds.Width / 2) - CInt(wImg / 2))		' Middle Of "e.SubItem.Bounds.Width"
		Dim yPos As Integer = (e.SubItem.Bounds.Y + CInt(e.SubItem.Bounds.Height / 2) - CInt(hImg / 2))		' Middle of "e.SubItem.Bounds.Height"

		Dim sbTextColour As SolidBrush = New SolidBrush(e.SubItem.ForeColor)
		Dim fFont As New Font(cControl.Font, FontStyle.Regular)
		Dim fPos As Integer = CInt((e.SubItem.Bounds.Height - fFont.Height) / 2)

		If (e.SubItem.Text.Length < 5) Then
			e.DrawDefault = True
			e.DrawBackground()
			Exit Sub
		End If

		Select Case e.SubItem.Text.Substring(0, 5).ToUpper
			Case "ICON:"
				e.DrawDefault = False
				r = New Rectangle(xPos, yPos, wImg, hImg)
				e.Graphics.DrawIcon(CType(My.Resources.ResourceManager.GetObject(iImg), Icon), r)

			Case "BOTH:"
				e.DrawDefault = False
				r = New Rectangle(e.SubItem.Bounds.X + 2, yPos, wImg, hImg)
				e.Graphics.DrawIcon(CType(My.Resources.ResourceManager.GetObject(iImg), Icon), r)
				r = New Rectangle(e.SubItem.Bounds.X + 2 + wImg, e.SubItem.Bounds.Y + fPos, e.SubItem.Bounds.Width - wImg - 2, e.SubItem.Bounds.Height - fPos)
				e.Graphics.DrawString(e.SubItem.Text.Substring(7), fFont, sbTextColour, r)
				'                                              ^- BOTH:x:text
			Case Else
				e.DrawDefault = True
		End Select
	End Sub

	' Handle DrawColumnHeader event
	Private Sub ctrlListView_SubIcons_DrawColumnHeader(ByVal sender As Object, ByVal e As DrawListViewColumnHeaderEventArgs)
		e.DrawDefault = True
		e.DrawBackground()
		e.DrawText()
	End Sub
End Class

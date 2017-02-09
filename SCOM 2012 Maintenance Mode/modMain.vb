Imports System.Drawing.Imaging

Module modMain
	Public Sub doGroupListView(ByVal lvw As ListView, ByVal sColumn As String, ByVal UseTag As Boolean, ByVal bShowCounts As Boolean)
		If (lvw Is Nothing) Then Exit Sub
		If (lvw.Items.Count = 0) Then Exit Sub

		Dim sBlank As String = "(Not Specified)"
		Dim iCol As Integer = lvw.Columns.IndexOfKey(sColumn)
		Dim gGroups As New List(Of String)

		For Each lItem As ListViewItem In lvw.Items
			If (lItem.SubItems(iCol).Text = vbNullString) Then
				gGroups.Add(sBlank)
			Else
				If (UseTag = True) Then
					If ((lItem.SubItems(iCol).Text.StartsWith("ICON:")) Or (lItem.SubItems(iCol).Text.StartsWith("BOTH:"))) Then
						gGroups.Add(lItem.SubItems(iCol).Text.Substring(5) & "|" & lItem.SubItems(iCol).Tag.ToString)
					Else
						gGroups.Add(lItem.SubItems(iCol).Tag.ToString & "|" & lItem.SubItems(iCol).Text)
					End If
				Else
					gGroups.Add(lItem.SubItems(iCol).Text)
				End If
			End If
		Next

		Dim nGroup As ListViewGroup
		Dim dGroups As IEnumerable(Of String) = gGroups.Distinct
		gGroups = Nothing
		lvw.Groups.Clear()
		lvw.ShowGroups = True

		For Each gItem As String In dGroups
			If (UseTag = True) Then
				nGroup = New ListViewGroup(Split(gItem, "|")(0), Split(gItem, "|")(1))
			Else
				nGroup = New ListViewGroup(gItem, gItem)
			End If
			nGroup.HeaderAlignment = HorizontalAlignment.Left
			lvw.Groups.Add(nGroup)
			nGroup = Nothing
		Next
		dGroups = Nothing

		For Each lItem As ListViewItem In lvw.Items
			If (lItem.SubItems(iCol).Text = vbNullString) Then
				lItem.Group = lvw.Groups(sBlank)
			Else
				If (UseTag = True) Then
					If ((lItem.SubItems(iCol).Text.StartsWith("ICON:")) Or (lItem.SubItems(iCol).Text.StartsWith("BOTH:"))) Then
						lItem.Group = lvw.Groups(lItem.SubItems(iCol).Text.Substring(5))
					Else
						lItem.Group = lvw.Groups(lItem.SubItems(iCol).Tag.ToString)
					End If
				Else
					lItem.Group = lvw.Groups(lItem.SubItems(iCol).Text)
				End If
			End If
		Next

		CType(lvw, ListView_GroupSorter).SortGroups(True, UseTag)
	End Sub

	Public Sub ensureVisible(ByVal listviewControl As ListView, ByVal lItem As ListViewItem)
		With listviewControl
			If ((lItem.Index + 3) < .Items.Count) Then
				.Items(lItem.Index + 3).EnsureVisible()
			Else
				.Items(.Items.Count - 1).EnsureVisible()
			End If
		End With
	End Sub

	Public Function GrayScaleImage(ByVal SourceImage As Image, ByVal bWithTransparency As Boolean) As Image
		If (SourceImage Is Nothing) Then Return Nothing
		Try
			Dim newImage As Image = New Bitmap(SourceImage.Width, SourceImage.Height)
			Using gHandle As Graphics = Graphics.FromImage(newImage)
				Dim cMatrix As ColorMatrix = New ColorMatrix
				Dim iAttributes As ImageAttributes = New ImageAttributes

				cMatrix.Matrix00 = 0.0F		' \
				cMatrix.Matrix10 = 1.0F		'  | These make the
				cMatrix.Matrix11 = 1.0F		'  | image appear to
				cMatrix.Matrix12 = 1.0F		'  | be grey-scale
				cMatrix.Matrix22 = 0.0F		' /
				If (bWithTransparency = True) Then cMatrix.Matrix33 = 0.5F

				iAttributes.SetColorMatrix(cMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
				gHandle.DrawImage(SourceImage, New Rectangle(0, 0, SourceImage.Width, SourceImage.Height), 0, 0, newImage.Width, newImage.Height, GraphicsUnit.Pixel, iAttributes)
			End Using
			Return newImage
		Catch ex As Exception
			Return Nothing
		End Try
	End Function
End Module


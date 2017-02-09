Option Explicit On

Public Class ListViewColumnSorter
	Implements System.Collections.IComparer
	Private ColumnToSort As Integer
	Private OrderOfSort As SortOrder
	Private ObjectCompare As CaseInsensitiveComparer
	'
	' Taken From http://support.microsoft.com/kb/319399
	'
	Public Sub New()
		ColumnToSort = 0
		OrderOfSort = SortOrder.Ascending
		ObjectCompare = New CaseInsensitiveComparer()
	End Sub

	Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
		If ((x Is Nothing) Or (y Is Nothing)) Then Return 0
		Dim compareResult As Integer
		Dim lvX As ListViewItem = CType(x, ListViewItem)
		Dim lvY As ListViewItem = CType(y, ListViewItem)

		Try
			compareResult = ObjectCompare.Compare(lvX.Text, lvY.Text)
			If (OrderOfSort = SortOrder.Ascending) Then
				Return compareResult
			ElseIf (OrderOfSort = SortOrder.Descending) Then
				Return (-compareResult)
			Else
				Return 0
			End If
		Catch ex As Exception
			Return 0
		End Try
	End Function
	Public Property SortColumn() As Integer
		Set(ByVal Value As Integer)
			ColumnToSort = Value
		End Set
		Get
			Return ColumnToSort
		End Get
	End Property

	Public Property Order() As SortOrder
		Set(ByVal Value As SortOrder)
			OrderOfSort = Value
		End Set
		Get
			Return OrderOfSort
		End Get
	End Property
End Class

' #################################################################################################

Public Class ListView_GroupSorter
	Friend _listview As ListView
	Public Shared Widening Operator CType(listview As ListView) As ListView_GroupSorter
		Return New ListView_GroupSorter(listview)
	End Operator
	Friend Sub New(listview As ListView)
		_listview = listview
	End Sub
	Public Sub SortGroups(ascending As Boolean, tagSort As Boolean)
		_listview.BeginUpdate()
		Dim lvgs As New List(Of ListViewGroup)()
		For Each lvg As ListViewGroup In _listview.Groups
			lvgs.Add(lvg)
		Next
		_listview.Groups.Clear()
		lvgs.Sort(New ListView_GroupHeaderSorter(ascending, tagSort))
		_listview.Groups.AddRange(lvgs.ToArray())
		_listview.EndUpdate()
	End Sub
End Class

Public Class ListView_GroupHeaderSorter
	Implements IComparer(Of ListViewGroup)
	Private _ascending As Boolean = True
	Private _tagSort As Boolean = False
	Public Sub New(ascending As Boolean, SortByTag As Boolean)
		_ascending = ascending
		_tagSort = SortByTag
	End Sub
	Public Function Compare(x As Windows.Forms.ListViewGroup, y As Windows.Forms.ListViewGroup) As Integer Implements Collections.Generic.IComparer(Of System.Windows.Forms.ListViewGroup).Compare
		If (_ascending) Then
			If _tagSort = True Then
				Return String.Compare(DirectCast(x, ListViewGroup).Name, DirectCast(y, ListViewGroup).Name)
			Else
				Return String.Compare(DirectCast(x, ListViewGroup).Header, DirectCast(y, ListViewGroup).Header)
			End If
		Else
			Return String.Compare(DirectCast(y, ListViewGroup).Header, DirectCast(x, ListViewGroup).Header)
		End If
	End Function
End Class

Imports System.Windows.Forms

Public Class ContactBIZ
#Region "Variables"
    Private contactDAL As ContactDAL
    Private club As Club
#End Region
    Public Sub GetContacts(ByVal dtgridContacts As DataGridView, ByVal isSelectAll As Boolean, ByVal contactCollection As String, ByVal clubname As String, ByVal category As String)
        Try
            contactDAL = New ContactDAL()
            Dim dtresult As DataSet
            dtresult = contactDAL.GetContacts(isSelectAll, ContactCollections(contactCollection), clubname, category)
            If dtresult.Tables.Count > 0 Then
                dtgridContacts.DataSource = dtresult.Tables(0)
                If dtgridContacts.ColumnCount > 0 Then
                    dtgridContacts.Columns(0).ReadOnly = False
                    dtgridContacts.Columns(1).ReadOnly = True
                    dtgridContacts.Columns(2).ReadOnly = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetContactCategory(ByVal barangayName As String) As DataSet
        Try
            contactDAL = New ContactDAL()
            Dim dtresult As DataSet
            dtresult = contactDAL.GetContactCategory(barangayName)
            Return dtresult
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetClubList() As String
        Try
            club = New Club()
            Dim dtresult As String = club.GetClub()

            'dtresult = contactDAL.GetClubList()
            Return dtresult
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function ContactCollections(ByVal smscolstr As String) As DataTable
        Try
            Dim SMSNumber As DataTable = New DataTable()

            ' Split string based on spaces
            Dim numbers As String() = smscolstr.Split(New Char() {";"c})
            SMSNumber.Columns.Add("SMSNumber")

            ' Use For Each loop over words and display them
            Dim number As String
            For Each number In numbers
                Dim SMSRow As DataRow
                SMSRow = SMSNumber.NewRow

                SMSRow.Item(0) = number

                SMSNumber.Rows.Add(SMSRow)
            Next

            Return SMSNumber
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

Public Class ContactDAL
    Private DbConn As New DatabaseConnection
    Public Function GetContacts(ByVal isSelectAll As Boolean, ByVal contactCollection As DataTable, ByVal clubname As String, ByVal category As String) As DataSet
        Dim contacts As DataSet
        Dim da As New SqlClient.SqlDataAdapter
        contacts = New DataSet()
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("SMSRegisteredNumberSelect")
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@IsSelectAll", isSelectAll)
                .sqlcomm.Parameters.AddWithValue("@Category", category)
                .sqlcomm.Parameters.AddWithValue("@SelectedItems", contactCollection)
                .sqlcomm.Parameters.AddWithValue("@ClubName", clubname)
                da.SelectCommand = .sqlcomm
                da.Fill(contacts)
                .sqlconn.Close()
            End With
            Return contacts
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetContactCategory(ByVal barangayName As String) As DataSet
        Dim contacts As DataSet
        Dim da As New SqlClient.SqlDataAdapter
        contacts = New DataSet()
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("GetContactCategory")
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@BarangayName", barangayName)
                da.SelectCommand = .sqlcomm
                da.Fill(contacts)
                .sqlconn.Close()
            End With
            Return contacts
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetClubList() As DataSet
        Dim contacts As DataSet
        Dim da As New SqlClient.SqlDataAdapter
        contacts = New DataSet()
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("ClubSelectAll")
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()

                da.SelectCommand = .sqlcomm
                da.Fill(contacts)
                .sqlconn.Close()
            End With
            Return contacts
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

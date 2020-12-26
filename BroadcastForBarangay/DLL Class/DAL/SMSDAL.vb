Public Class SMSDAL
    Private DbConn As New DatabaseConnection
    Private ActivationCodeValue As String

    Public Property ActivationCode As String
        Get
            Return ActivationCodeValue
        End Get
        Set(ByVal value As String)
            ActivationCodeValue = value.Trim
        End Set
    End Property
    Public Function InboxSave(ByVal SMSID As String, ByVal SMSContent As String, ByVal Sender As String, ByVal SMSDate As String, ByVal SMSTime As String, ByVal ModemIDValue As String) As String
        Dim message As String
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("InboxSave")
        message = ""
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@SMSID", SMSID)
                .sqlcomm.Parameters.AddWithValue("@SMSContent", SMSContent)
                .sqlcomm.Parameters.AddWithValue("@Sender", Sender)
                .sqlcomm.Parameters.AddWithValue("@SMSDate", SMSDate)
                .sqlcomm.Parameters.AddWithValue("@SMSTime", SMSTime)
                .sqlcomm.Parameters.AddWithValue("@ActivationCode", ActivationCode)
                .sqlcomm.Parameters.AddWithValue("@ModemID", ModemIDValue)
                message = .sqlcomm.ExecuteScalar()
                .sqlconn.Close()
            End With
            Return message
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SendMessage(ByVal Message As String, ByVal Sender As String) As Boolean
        'Dim message As String
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("SendMessage")
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.CommandTimeout = 0
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@Message", Message)
                .sqlcomm.Parameters.AddWithValue("@SMSMobileNumber", Sender)
                Message = .sqlcomm.ExecuteScalar()
                .sqlconn.Close()
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function OutBoxSave(ByVal SMSContent As String, ByVal Recipient As String, ByVal Status As String, ByVal Remarks As String, ByVal InboxID As String, ByVal InboxType As String) As Boolean
        Dim message As Boolean
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("OutboxSave")
        message = False
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@SMSContent", SMSContent)
                .sqlcomm.Parameters.AddWithValue("@Recipient", Recipient)
                .sqlcomm.Parameters.AddWithValue("@Status", Status)
                .sqlcomm.Parameters.AddWithValue("@StatusRemarks", Remarks)
                .sqlcomm.Parameters.AddWithValue("@InboxID", InboxID)
                .sqlcomm.Parameters.AddWithValue("@InboxType", InboxType)
                .sqlcomm.ExecuteNonQuery()
                .sqlconn.Close()
                message = True
            End With
            Return message
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRecordForReply(ByVal activationcode As String, ByVal inboxID As String) As DataTable
        Dim dtResult As New DataTable
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("GetRecordForReply")
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@activationcode", activationcode)
                .sqlcomm.Parameters.AddWithValue("@inboxID", inboxID)

                Dim dr As SqlClient.SqlDataReader
                dr = .sqlcomm.ExecuteReader()
                dtResult.Load(dr)
                .sqlconn.Close()
            End With
            Return dtResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

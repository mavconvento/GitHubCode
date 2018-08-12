
Public Class VerificationCodeDAL
    Private DbConn As New DatabaseConnection

    Public Function GenerateVerificationCode(ByVal portName As String) As String
        Dim code As String
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("GenerateVerificationCode")
        code = ""
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@PortName", portName)
                code = .sqlcomm.ExecuteScalar()
                .sqlconn.Close()
            End With
            Return code
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function VerifyVerificationCode(ByVal code As String) As String
        Dim portName As String
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("ActivateVerificationCode")
        portName = ""
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.Parameters.AddWithValue("@verificationcode", code)
                portName = .sqlcomm.ExecuteScalar()
                .sqlconn.Close()
            End With
            Return portName
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

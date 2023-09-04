
Imports System.IO

Public Class SMSDAL
    Implements IDisposable

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
                .sqlcomm.CommandTimeout = 0
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
    Public Function InboxSaveToLocal(ByVal SMSID As String, ByVal SMSContent As String, ByVal Sender As String, ByVal SMSDate As String, ByVal SMSTime As String, ByVal ModemIDValue As String) As String
        Dim sysdir As String = ""
        sysdir = My.Application.Info.DirectoryPath
        Dim strFile As String = sysdir + "\Integrate\SMSStorage\InboxLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
        Dim sw As StreamWriter
        Try
            If (Not File.Exists(strFile)) Then
                sw = File.CreateText(strFile)
                sw.WriteLine(String.Concat(SMSID, "|", SMSContent, "|", Sender, "|", SMSDate, "|", SMSTime, "|", ModemIDValue))
            Else
                sw = File.AppendText(strFile)
                sw.WriteLine(String.Concat(SMSID, "|", SMSContent, "|", Sender, "|", SMSDate, "|", SMSTime, "|", ModemIDValue))
            End If
            sw.Close()
            Return Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function OutBoxSave(ByVal SMSContent As String, ByVal Recipient As String, ByVal Status As String, ByVal Remarks As String, ByVal InboxID As String, ByVal InboxType As String) As Boolean
        Dim message As Boolean
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("OutboxSave", "web")
        message = False
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.CommandTimeout = 0
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
                .sqlcomm.CommandTimeout = 0
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

    Public Function GetModemIDList(ByVal modemType As String) As DataTable
        Dim dtResult As New DataTable
        If DbConn.sqlconn.State = ConnectionState.Open Then DbConn.sqlconn.Close()
        DbConn.DatabaseConnection("GetModemIDList", "local")
        Try
            With DbConn
                .sqlconn.Open()
                .sqlcomm.Parameters.Clear()
                .sqlcomm.CommandTimeout = 0
                .sqlcomm.Parameters.AddWithValue("@ModemType", modemType)

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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                DbConn.sqlconn.Dispose()
                DbConn.sqlcomm.Dispose()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


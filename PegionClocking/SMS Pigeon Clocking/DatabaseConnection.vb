Option Explicit On
Option Strict On
Imports System.IO
Public Class DatabaseConnection
    Public sqlconn As New SqlClient.SqlConnection
    Public sqlcomm As New SqlClient.SqlCommand
    Private servername As String
    Private databasename As String
    Private username As String
    Private password As String

    Private Sub ReadConnecntionStringFile(Optional ByVal connType As String = "local")
        Dim sysdir As String = ""
        sysdir = My.Application.Info.DirectoryPath


        Dim connectionstring As String = ""

        If connType = "local" Then
            connectionstring = sysdir & "\ConnectionString.inf"
        ElseIf connType = "web" Then
            connectionstring = sysdir & "\ConnectionString_web.inf"
        End If

        If File.Exists(connectionstring) Then
            Using tr As TextReader = New StreamReader(connectionstring)
                servername = tr.ReadLine
                databasename = tr.ReadLine
                username = tr.ReadLine
                password = tr.ReadLine
            End Using
        End If
    End Sub

    Public Sub DatabaseConnection(ByVal procname As String, Optional ByVal connType As String = "local")
        Me.ReadConnecntionStringFile(connType)
        sqlconn.ConnectionString = "Address=" + servername _
                                    + ";database=" + databasename _
                                    + ";user id=" + username _
                                    + ";pwd=" + password
        Try
            sqlcomm.Connection = sqlconn
            sqlcomm.CommandType = CommandType.StoredProcedure
            sqlcomm.CommandText = procname
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
End Class

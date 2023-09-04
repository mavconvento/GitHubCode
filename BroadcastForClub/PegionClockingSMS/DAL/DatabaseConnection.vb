Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Public Class DatabaseConnection
    Public sqlconn As New SqlClient.SqlConnection
    Public sqlcomm As New SqlClient.SqlCommand
    Private servername As String
    Private databasename As String
    Private username As String
    Private password As String

    Private Sub ReadConnecntionStringFile()
        Dim sysdir As String = ""
        sysdir = My.Application.Info.DirectoryPath

        Dim connectionstring As String = sysdir & "\ConnectionString.inf"

        servername = "198.38.94.72"
        username = "sa"
        password = "06242009"

        If File.Exists(connectionstring) Then
            Using tr As TextReader = New StreamReader(connectionstring)
                databasename = "pigeonclocking_" + tr.ReadLine
            End Using
        End If
    End Sub



    Public Sub DatabaseConnection(ByVal procname As String)
        Me.ReadConnecntionStringFile()
        sqlconn.ConnectionString = "Address=" + servername _
                                    + ";database=" + databasename _
                                    + ";user id=" + username _
                                    + ";pwd=" + password
        Try
            sqlcomm.Connection = sqlconn
            sqlcomm.CommandType = CommandType.StoredProcedure
            sqlcomm.CommandText = procname
        Catch ex As Exception
            MessageBox.Show("Connection to the Server Failed" & Chr(13) & "Please call your System Developer for assistant", _
                            "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
End Class

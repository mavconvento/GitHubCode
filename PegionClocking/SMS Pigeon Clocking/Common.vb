Option Explicit On
Option Strict On
Imports System.IO
Public Class Common
    Public Function ReadConnecntionStringTypeFile() As String
        Dim sysdir As String = ""
        sysdir = My.Application.Info.DirectoryPath

        Dim connectionstring As String = ""
        Dim connType As String = ""
        connectionstring = sysdir & "\ConnectionStringType.inf"

        If File.Exists(connectionstring) Then
            Using tr As TextReader = New StreamReader(connectionstring)
                connType = tr.ReadLine
            End Using
        End If

        Return connType
    End Function
End Class

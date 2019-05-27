Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Public Class Club
    Public Function GetClub() As String
        Dim club As String = ""
        Dim sysdir As String = My.Application.Info.DirectoryPath

        Dim connectionstring As String = sysdir & "\Club.inf"

        If File.Exists(connectionstring) Then
            Using tr As TextReader = New StreamReader(connectionstring)
                club = tr.ReadLine
            End Using
        End If
        Return club
    End Function

    Public Function GetModem() As String
        Dim club As String = ""
        Dim sysdir As String = My.Application.Info.DirectoryPath

        Dim connectionstring As String = sysdir & "\Modem.inf"

        If File.Exists(connectionstring) Then
            Using tr As TextReader = New StreamReader(connectionstring)
                club = tr.ReadLine
            End Using
        End If
        Return club
    End Function
End Class

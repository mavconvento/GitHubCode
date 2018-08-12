Imports System
Imports System.Threading
Imports System.ComponentModel
Imports System.IO.Ports
Imports SerialFix = SerialPortTester.SerialPortFixer

Public Class SMSComponents
    Private Const delayValue As Decimal = 1.0
    Public WithEvents SMSPort As SerialPort
    Public Event Sending(ByVal Done As Boolean)
    Public Event DataReceived(ByVal Message As String)
    Private SMSThread As Thread
    Private ReadThread As Thread
    Private _Wait As Boolean = False
    Shared _Continue As Boolean = False
    Shared _ContSMS As Boolean = False
    Shared _ReadPort As Boolean = False
    Private portNoValue As String = ""
    Private modemTypeValue As String = ""
    Private memoryTypeValue As String = ""
    Private messageTypeValue As String = ""
    Private baudRateTypeValue As String = ""
    Private modemIDValue As String = ""
    Private deleteDelayValue As Integer = 0

    Public Property BaudRate As String
        Get
            Return baudRateTypeValue.Trim
        End Get
        Set(ByVal value As String)
            baudRateTypeValue = value.Trim
        End Set
    End Property
    Public Property MessageType As String
        Get
            Return messageTypeValue.Trim
        End Get
        Set(ByVal value As String)
            messageTypeValue = value.Trim
        End Set
    End Property

    Public Property PortNo As String
        Get
            Return portNoValue.Trim
        End Get
        Set(ByVal value As String)
            portNoValue = value.Trim
        End Set
    End Property
    Public Property ModemType As String
        Get
            Return modemTypeValue.Trim
        End Get
        Set(ByVal value As String)
            modemTypeValue = value.Trim
        End Set
    End Property
    Public Property MemoryType As String
        Get
            Return memoryTypeValue.Trim
        End Get
        Set(ByVal value As String)
            memoryTypeValue = value.Trim
        End Set
    End Property
    Public Property DeleteDelay As Integer
        Get
            Return deleteDelayValue
        End Get
        Set(ByVal value As Integer)
            deleteDelayValue = value
        End Set
    End Property
    Public Sub CommPortSetup()
        'initialize all values
        SMSPort = New SerialPort
        With SMSPort
            .PortName = PortNo
            .BaudRate = BaudRate
            .Parity = Parity.None
            .DataBits = 8
            .StopBits = StopBits.One
            .Handshake = Handshake.RequestToSend
            .DtrEnable = True
            .RtsEnable = True
            .NewLine = vbCrLf
        End With
    End Sub
    Public Function SendSMS(ByVal isDeleted As Boolean, ByVal inboxID As Int64, ByVal mobileNumber As String, ByVal smsContent As String) As Boolean
        Try
            Dim isSMSSend As Boolean = False
            Open()
            If SMSPort.IsOpen = True Then
                SMSPort.WriteLine("AT")
                DELAY(delayValue)
                SMSPort.WriteLine("AT+CMGF=1" & vbCrLf)

                'If isDeleted Then
                '    DELAY(delayValue)
                '    SMSPort.WriteLine("AT+CMGD=" & inboxID & "" & vbCrLf)
                'End If

                If (Len(mobileNumber) <= 13) Then
                    If ModemType = "Smart" Then
                        SMSPort.WriteLine("AT+CSCA=""+639180000371""" & vbCrLf) 'for smart
                    Else
                        SMSPort.WriteLine("AT+CSCA=""+639170000130""" & vbCrLf) 'for globe
                    End If

                    DELAY(delayValue + deleteDelayValue)
                    SMSPort.WriteLine("AT+CMGS=""" & mobileNumber & """" & vbCrLf)
                    DELAY(delayValue + deleteDelayValue)
                    SMSPort.WriteLine(smsContent & vbCrLf & Chr(26)) 'SMS sending
                    isSMSSend = True
                End If
            End If
            Close()
            Return isSMSSend
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub InitialModem()
        Try
            Dim message As String
            message = ""
            Open()
            If SMSPort.IsOpen = True Then
                SMSPort.WriteLine("AT")
                DELAY(delayValue)
                If (MemoryType = "ME") Then SMSPort.WriteLine("AT+CPMS=""" + MemoryType + """,""" + MemoryType + """,""" + MemoryType + """" & vbCrLf) 'set memory use is gsm at+cpms? for me storage 
                If (MemoryType = "SM") Then SMSPort.WriteLine("AT+CPMS=""" + MemoryType + """,""" + MemoryType + """" & vbCrLf) 'set memory use is gsm at+cpms? for sm storage 
                DELAY(delayValue)
                SMSPort.WriteLine("AT&W" & vbCrLf)
                DELAY(delayValue)
                SMSPort.WriteTimeout = 30
                'DELAY(10)
            End If
            Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ReadSMS() As String
        Try
            Dim messageRead As String
            Dim messageUnRead As String
            Dim message As String = ""
            messageRead = ""
            messageUnRead = ""

            Open()
            If SMSPort.IsOpen = True Then
                SMSPort.WriteLine("AT")
                DELAY(delayValue)
                SMSPort.WriteLine("AT+CMGF=1" & vbCrLf) 'set command message format to text mode(1)
                DELAY(delayValue)
                SMSPort.DiscardInBuffer()
                DELAY(delayValue)
                If MessageType = "REC READ" Or MessageType = "ALL" Then
                    SMSPort.WriteLine("AT+CMGL=""REC READ""" & vbCrLf)
                    DELAY(delayValue + 6)
                    message = SMSPort.ReadExisting
                    DELAY(delayValue)
                End If
                If MessageType = "REC UNREAD" Or MessageType = "ALL" Then
                    If Not message.Contains("+CMGL:") Then
                        SMSPort.DiscardInBuffer()
                        DELAY(delayValue)
                        SMSPort.WriteLine("AT+CMGL=""REC UNREAD""" & vbCrLf)
                        DELAY(delayValue + 6)
                        message = SMSPort.ReadExisting
                        DELAY(delayValue)
                    End If
                End If
                SMSPort.WriteTimeout = 30
                'DELAY(4)
            End If
            Close()

            'message = messageRead + messageUnRead
            Return message
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DeleteReadSMS(ByVal inboxID As Integer) As String
        Try
            Dim message As String
            message = ""
            Open()
            If SMSPort.IsOpen = True Then
                SMSPort.WriteLine("AT")
                DELAY(delayValue + deleteDelayValue)
                'SMSPort.WriteLine("AT+CMGF=1" & vbCrLf) 'set command message format to text mode(1)
                'DELAY(delayValue)
                SMSPort.WriteLine("AT+CMGD=" & inboxID & "" & vbCrLf)
                DELAY(delayValue)
                SMSPort.WriteTimeout = 30
            End If
            Close()
            Return message
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Open()
        Try
            Dim status As Boolean = False
            Dim serialPortFix As New SerialFix

            CommPortSetup()
            serialPortFix.Execute(PortNo)
            If Not (SMSPort.IsOpen = True) Then
                SMSPort.Open()
                status = True
            End If
            Return status
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub Close()
        If SMSPort.IsOpen = True Then
            SMSPort.DtrEnable = False
            SMSPort.Close()
        End If
    End Sub
    Public Function CommPortValidation() As Boolean
        Dim IsPassed As Boolean
        Dim message As String
        Try
            IsPassed = False
            message = ""
            Open()
            If SMSPort.IsOpen = True Then
                SMSPort.WriteLine("AT")
                DELAY(delayValue)
                message = SMSPort.ReadExisting
                SMSPort.WriteTimeout = 30
                If message.Contains("OK") Then
                    IsPassed = True
                End If
            End If
            Close()
        Catch ex As Exception
            Throw ex
        Finally
            IsPassed = False
        End Try
        Return IsPassed
    End Function
    Public Function ReadString(SMS As SMSComponents) As String
        Dim a As String
        'Open()
        a = SMS.SMSPort.ReadExisting()
        'Close()
        Return a
    End Function

    Private Sub DELAY(ByVal delayValue As Int64)
        Thread.Sleep(delayValue * 100)
    End Sub
End Class

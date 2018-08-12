Imports System.Data
Imports System.IO.Ports
Module Module1
    Private ModemID As String
    Private PortName As String
    Private BaudRate As String
    Private MemoryType As String
    Private MessageType As String
    Private NetworkType As String
    Private ProcessType As String
    Private InboxID As String
    Private IsReply As Boolean
    Private DelDelay As Integer
    Private SMSDal As New SMSDAL
    Private smsmod As SMSModule.SMSComponents
    Private MyThreadReadSMS As Threading.Thread
    Private SMS As SMSBIZ

    Sub Main()
        Start()
        Console.ReadLine()
    End Sub

    Private Sub Start()
        Dim IsError As String
        Try
            NetworkType = GetNetworkType()
            DelDelay = DeleteDelay()
            ModemID = GetModemID()
            PortName = GetPortNumber()
            BaudRate = "115200"
            MemoryType = "SM"
            MessageType = "ALL"
            InboxID = "Inbox 1"
            IsError = "0"
start:
            Dim IsSet As String = "0"
            'IsError = "-1"
            Console.Clear()
            Console.WriteLine("NetworkType : " + NetworkType)
            Console.WriteLine(" MemoryType : " + MemoryType)
            Console.WriteLine("ProcessType : " + ProcessType)
            Console.WriteLine("   BaudRate : " + BaudRate)
            Console.WriteLine("MessageType : " + MessageType)
            Console.WriteLine("   PortName : " + PortName)
            Console.WriteLine("    ModemID : " + ModemID)
            Console.WriteLine("---------------------------")
            If (IsError = 0) Then
                Console.WriteLine("Press 1 to set SMS Clocking")
                IsSet = Console.ReadLine()
            ElseIf (IsError = -1) Then
                IsSet = 1
            End If

            If IsSet = "1" Then
                InitializeModem()
                StartSMS(IsError)
                IsError = "0"
                IsSet = "0"
            Else
                GoTo start
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine("---------------------------")
            Console.WriteLine("Initializing Modem Due To Error")
            System.Threading.Thread.Sleep(9000)
            IsError = "-1"
            GoTo start
        End Try
    End Sub

    Private Sub InitializeModem()
        Try
            smsmod = New SMSModule.SMSComponents
            smsmod.ModemType = NetworkType
            smsmod.BaudRate = BaudRate
            smsmod.MemoryType = MemoryType
            smsmod.PortNo = PortName
            smsmod.DeleteDelay = DelDelay
            smsmod.InitialModem()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub StartSMS(ByVal IsFromError As String)
        Try
            Dim index As Integer
            Dim isTest As String
            Dim MobileNumber As String = ""

            If IsFromError = "-1" Then
                GoTo STARTNOW
            Else
                If IsReply Then
                    Console.Clear()
                    Console.WriteLine("Press 1 to Test Sender")
                    Console.WriteLine("Press any key to start reply")
                    isTest = Console.ReadLine()

                    If isTest = "1" Then
                        Console.Clear()
                        Console.WriteLine("Please Enter Mobile Number")
                        MobileNumber = Console.ReadLine()
                    End If
                    If MobileNumber <> "" Then TestSender(MobileNumber)
                    Console.Clear()
                    Console.WriteLine("PRESS 1 TO CONTINUE....")
                    isTest = Console.ReadLine()
                    If isTest = 1 Then
                        GoTo STARTNOW
                    Else
                        GoTo EXITNOW
                    End If
                End If
            End If

STARTNOW:
            Console.Clear()
            Console.WriteLine("Processing Inbox.....")
            Console.WriteLine("ModemID :" + ModemID)
            While index = 0
                System.Threading.Thread.Sleep(500)
                SMS = New SMSBIZ()
                SMS.PortName = PortName
                SMS.ModemType = NetworkType
                SMS.IsSendReply = IsReply
                SMS.ActivationCode = ModemID
                SMS.MessageType = MessageType
                SMS.MemoryType = MemoryType
                SMS.BaudRate = BaudRate
                SMS.ModemID = ModemID
                SMS.DeleteDelay = DelDelay
                SMS.InboxType = InboxID
                If (IsReply) Then
                    SMS.GetRecordForReply()
                Else
                    SMS.ReadSMS()
                End If
            End While
EXITNOW:
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TestSender(ByVal mobileno As String)
        Try
            Dim status As Boolean
            SMS = New SMSBIZ()
            smsmod = New SMSModule.SMSComponents

            SMS.PortName = PortName
            SMS.ModemType = ModemID
            SMS.MemoryType = MemoryType
            SMS.MessageType = MessageType
            SMS.BaudRate = BaudRate
            mobileNo = mobileNo

            If mobileNo <> "" Then
                If SMS.SendSMS(mobileNo, "Test successful", False, -1, "Broadcast") Then
                    status = True
                Else
                    status = False
                End If
            End If

            If (status) Then
                Console.WriteLine("Message Sent")
            Else
                Console.WriteLine("Message Sending Failed")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetNetworkType() As String
        Dim networkTypeValue As String = ""
        Console.Clear()
repeat:
        Console.WriteLine("Network Type")
        Console.WriteLine("1. Smart")
        Console.WriteLine("2. Globe")

        Console.WriteLine("Select Network Type")
        networkTypeValue = Console.ReadLine()

        If networkTypeValue = 1 Then
            networkTypeValue = "Smart"
        ElseIf networkTypeValue = 2 Then
            networkTypeValue = "Globe"
        Else
            Console.Clear()
            Console.WriteLine("Invalid ModemType")
            GoTo repeat
        End If

        Return networkTypeValue
    End Function

    Private Function GetModemID() As String
        Try
            Console.Clear()
prints:
            Dim modem As String = ""
            Dim modemType As String = ""
            Console.WriteLine("Modem Type")
            Console.WriteLine("1. Receiver")
            Console.WriteLine("2. Sender")
            Console.WriteLine("Select ModemType")
            modemType = Console.ReadLine

            If modemType = 1 Then
                modemType = "Receiver"
                IsReply = False
            ElseIf modemType = 2 Then
                modemType = "Sender"
                IsReply = True
            Else
                Console.Clear()
                Console.WriteLine("Invalid Modem Type")
                GoTo prints
            End If
            ProcessType = modemType
            modem = PrintModemID(modemType)
            Return modem
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function PrintModemID(ByVal modemType As String)
        Dim dt As DataTable
        Dim modemID As String = ""
        Dim selectedModemID As String = ""
        dt = ModemList(modemType)
        Console.Clear()
repeat:
        Dim counter As Integer = 1
        For Each item As DataRow In dt.Rows
            Console.WriteLine(counter.ToString() + ":" + item("ModemID"))
            counter += 1
        Next

        Console.WriteLine("ProcessType:" + ProcessType)
        Console.WriteLine("Select ModemID:")
        selectedModemID = Console.ReadLine()
        modemID = selectedModemID
        counter = 1

        For Each item As DataRow In dt.Rows
            If selectedModemID = counter.ToString() Then
                modemID = item("ModemID")
                Exit For
            End If
            counter += 1
        Next

        If selectedModemID = modemID Then
            Console.Clear()
            Console.WriteLine("Invalid ModemID")
            GoTo repeat
        End If
        Return modemID

    End Function
    Private Function DeleteDelay() As Integer
        Dim delay As Integer = 0
        Console.Clear()
        Console.WriteLine("Delete Delay:")
        delay = Console.ReadLine()
        Return delay
    End Function
    Private Function ModemList(ByVal modemType As String) As DataTable
        Dim dtModemList As DataTable

        dtModemList = SMSDal.GetModemIDList(modemType)
        Return dtModemList
    End Function

    Private Function GetPortNumber() As String
        Dim portNumber As String = ""
        portNumber = PrintPortName()
        Return portNumber
    End Function

    Private Function PrintPortName() As String
        Dim portName As String = ""
        Dim selectedPortName As String = ""
        Dim portList As DataTable

repeat:
        Dim counter As Integer = 1
        portList = GetPort()
        Console.WriteLine("ProcessType:" + ProcessType)
        Console.WriteLine("ModemID:" + ModemID)
        Console.WriteLine("Select PortName")
        selectedPortName = Console.ReadLine()
        portName = selectedPortName

        For Each item As DataRow In portList.Rows
            If selectedPortName = counter.ToString() Then
                portName = item("PortName")
                Exit For
            End If
            counter += 1
        Next

        If selectedPortName = portName Then
            Console.Clear()
            Console.WriteLine("Invalid PortName")
            GoTo repeat
        End If
        Return portName
    End Function

    Private Function GetPort()
        Dim counter As Integer = 1
        Dim dt As DataTable = New DataTable
        Dim dr As DataRow
        Dim dc As DataColumn

        dc = New DataColumn
        dc.DataType = System.Type.GetType("System.String")
        dc.ColumnName = "PortName"
        dt.Columns.Add(dc)
        Console.Clear()
        For Each portName As String In SerialPort.GetPortNames()
            If portName <> "COM1" Then
                dr = dt.NewRow()
                dr("PortName") = portName
                dt.Rows.Add(dr)
                Console.WriteLine(counter.ToString + ":" + portName)
                counter += 1
            End If
        Next
        Return dt
    End Function

End Module

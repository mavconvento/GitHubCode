Public Class SMSBIZ
#Region "Variables"
    Private SMSDal As New SMSDAL
    Private SMSMod As SMSModule.SMSComponents
    Private isSendReplyValue As Boolean = True
    Private portNameValue As String = ""
    Private modemTypeValue As String = ""
    Private activationCodeValue As String = ""
    Private memoryTypeValue As String = ""
    Private messageTypeValue As String = ""
    Private baudRateTypeValue As String = ""
    Private modemIDvalue As String = ""
    Private inboxTypevalue As String = ""
    Private deleteDelayValue As Integer = 0
#End Region

#Region "Properties"
    Public Property DeleteDelay As Integer
        Get
            Return deleteDelayValue
        End Get
        Set(ByVal value As Integer)
            deleteDelayValue = value
        End Set
    End Property
    Public Property BaudRate As String
        Get
            Return baudRateTypeValue.Trim
        End Get
        Set(ByVal value As String)
            baudRateTypeValue = value.Trim
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
    Public Property MessageType As String
        Get
            Return messageTypeValue.Trim
        End Get
        Set(ByVal value As String)
            messageTypeValue = value.Trim
        End Set
    End Property
    Public Property PortName As String
        Get
            Return portNameValue.Trim
        End Get
        Set(ByVal value As String)
            portNameValue = value.Trim
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
    Public Property ActivationCode As String
        Get
            Return activationCodeValue.Trim
        End Get
        Set(ByVal value As String)
            activationCodeValue = value.Trim
        End Set
    End Property
    Public Property IsSendReply As Boolean
        Get
            Return isSendReplyValue
        End Get
        Set(ByVal value As Boolean)
            isSendReplyValue = value
        End Set
    End Property
    Public Property ModemID As String
        Get
            Return modemIDvalue.Trim
        End Get
        Set(ByVal value As String)
            modemIDvalue = value.Trim
        End Set
    End Property
    Public Property InboxType As String
        Get
            Return inboxTypevalue.Trim
        End Get
        Set(ByVal value As String)
            inboxTypevalue = value.Trim
        End Set
    End Property
#End Region

    Public Sub ReadSMS()
        Try
            Dim message As String = ""
            SMSMod = New SMSModule.SMSComponents()
            SMSMod.PortNo = PortName
            SMSMod.MessageType = MessageType
            SMSMod.MemoryType = MemoryType
            SMSMod.BaudRate = BaudRate
            DecodeMessage(SMSMod.ReadSMS())
            'Threading.Thread.Sleep(2000)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function SendSMS(ByVal mobilenumber As String, ByVal message As String, ByVal IsDeleted As Boolean, ByVal InboxID As String, ByVal InboxType As String) As Boolean
        Try
            Dim SendingStatus As Boolean = False
            Dim status As String = ""
            Dim Remark As String = ""
            SMSMod = New SMSModule.SMSComponents()
            SMSMod.PortNo = PortName
            SMSMod.ModemType = ModemType
            SMSMod.MessageType = MessageType
            SMSMod.MemoryType = MemoryType
            SMSMod.BaudRate = BaudRate
            SMSDal = New SMSDAL()

            If (SMSMod.SendSMS(IsDeleted, InboxID, mobilenumber, message)) Then
                status = "Success"
            Else
                status = "Failed"
            End If
            If InboxID > 0 Then
                Remark = "AutoReply"
            Else
                Remark = "Manual"
            End If
            If status = "Success" Then SMSDal.OutBoxSave(message, mobilenumber, status, Remark, InboxID, InboxType)
            Return True
        Catch ex As Exception
            SMSDal.OutBoxSave(message, mobilenumber, "Error", ex.Message, "0", InboxType)
            Throw ex
        End Try
    End Function

    Private Sub DecodeMessage(ByVal SMSMessage As String)
        Dim ValCollection As Array
        Dim value As String = ""
        Dim findStr As String = "CMGL:"
        Dim strIndex As Int64 = 0
        Dim counter As Int64 = 0
        Try
            If SMSMessage.Contains(findStr) Then
                strIndex = SMSMessage.IndexOf("+" + findStr, 1)
                If strIndex <> -1 Then
                    SMSMessage = SMSMessage.Substring(strIndex - 1)
                    While SMSMessage <> ""
                        value = ""
                        If SMSMessage.IndexOf(findStr, 1) > 0 Then
                            SMSMessage = SMSMessage.Substring(SMSMessage.IndexOf(findStr, 1), SMSMessage.Length() - SMSMessage.IndexOf(findStr, 1)).Trim
                            If SMSMessage.IndexOf(findStr, 1) > 0 Then
                                value = SMSMessage.Substring(6, SMSMessage.IndexOf(findStr, 1) - 7).Trim
                            Else
                                value = SMSMessage.Substring(6, SMSMessage.Length - 6).Trim
                                SMSMessage = ""
                                If counter > 15 Then value = ""
                            End If
                            If value <> "" Then
                                ValCollection = value.Split(vbCrLf)
                                If ValCollection.Length >= 2 Then
                                    ParseMessage(ValCollection(0).ToString().Trim + " " + ValCollection(1).ToString().Trim)
                                    counter += 1
                                End If

                            End If
                        Else
                            SMSMessage = ""
                        End If
                        'Threading.Thread.Sleep(2500)
                    End While
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ParseMessage(ByVal message As String)
        Dim array As Array
        Dim ID As String = ""
        Dim Sender As String = ""
        Dim MessageType As String = ""
        Dim DateReceived As String = ""
        Dim TimeReceived As String = ""
        Dim Content As String = ""
        Dim KeyWord As String = ""
        Dim IsReply As Boolean = False
        Dim ReplyMessge As String = ""

        Try
            SMSDal = New SMSDAL()
            message = message.Replace("""", "").Replace(",,", ",")
            array = message.Split(",")

            ID = array.GetValue(0)
            Sender = array.GetValue(2)
            MessageType = array.GetValue(1)
            DateReceived = array.GetValue(3)

            Dim spitTimeContent As Array

            spitTimeContent = array.GetValue(4).ToString.Split(" ")
            TimeReceived = spitTimeContent(0).ToString()
            Content = array.GetValue(4).ToString.Substring(Len(TimeReceived) + 1)

            SMSDal.ActivationCode = ActivationCode

            SMSDal.InboxSave(ID, Content, Sender, DateReceived, TimeReceived, ModemID)
            SMSMod.PortNo = PortName
            SMSMod.DeleteDelay = DeleteDelay
            SMSMod.DeleteReadSMS(CType(ID, Integer)) 'delete sms in storage

            Console.WriteLine("-----------------------------------------")
            Console.WriteLine("          ID:" + ID.ToString())
            Console.WriteLine("     Content:" + Content.ToString())
            Console.WriteLine("      Sender:" + Sender.ToString())
            Console.WriteLine("DateReceived:" + DateReceived.ToString())
            Console.WriteLine("TimeReceived:" + TimeReceived.ToString())
            Console.WriteLine("     ModemID:" + ModemID.ToString())
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetRecordForReply()
        Try
            Dim dt As New DataTable
            dt = SMSDal.GetRecordForReply(ActivationCode, InboxType)
            'Console.Clear()
            For Each dr As DataRow In dt.Rows
                Console.WriteLine("--------------------------------------------------------")
                SendSMS(dr("Sender"), dr("ReplyMessage"), False, dr("InboxID"), InboxType)
                Console.WriteLine("   Sender:" + dr("Sender"))
                Console.WriteLine("  Keyword:" + dr("Keyword"))
                Console.WriteLine("InboxType:" + InboxType)
                Console.WriteLine("  InboxID:" + dr("InboxID").ToString())
                Console.WriteLine("  ModemID:" + ModemID.ToString())
                Threading.Thread.Sleep(3500)
            Next
            Threading.Thread.Sleep(5000)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class


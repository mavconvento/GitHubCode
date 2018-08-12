Imports System
Imports System.Threading
Imports System.ComponentModel
Imports System.IO.Ports

Public Class Main
#Region "Variables"
    Private VerificationCode As VerificationCodeBIZ
    Private smsmod As SMSModule.SMSComponents
    Private SMS As SMSBIZ
    Public WithEvents SMSPort As SerialPort


    'Property Value
    Private portNoValue As String = ""
    Private modemTypeValue As String = ""
    Private memoryTypeValue As String = ""
    Private messageTypeValue As String = ""
    Private baudRateTypeValue As String = ""
    Private contactListValue As DataTable
    Private contactCollectionValue As String = ""
    Private isReplyValue As Boolean = False
    Private isStopvalue As Boolean = False
    Private activationCodeValue As String = ""
    Private modemIDvalue As String = ""
#End Region

#Region "Properties"

    Public Property PortNo As String
        Get
            Return portNoValue
        End Get
        Set(ByVal value As String)
            portNoValue = value
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
    Public Property ModemType As String
        Get
            Return modemTypeValue.Trim
        End Get
        Set(ByVal value As String)
            modemTypeValue = value.Trim
        End Set
    End Property
    Public Property ContactList As DataTable
        Get
            Return contactListValue
        End Get
        Set(ByVal value As DataTable)
            contactListValue = value
        End Set
    End Property
    Public Property IsReply As Boolean
        Get
            Return isReplyValue
        End Get
        Set(ByVal value As Boolean)
            isReplyValue = value
        End Set
    End Property
    Public Property ContactCollection As String
        Get
            Return contactCollectionValue.Trim
        End Get
        Set(ByVal value As String)
            contactCollectionValue = value.Trim
        End Set
    End Property
    Public Property IsStop As Boolean
        Get
            Return isStopvalue
        End Get
        Set(ByVal value As Boolean)
            isStopvalue = value
        End Set
    End Property
    Public Property ActivationCode As String
        Get
            Return activationCodeValue
        End Get
        Set(ByVal value As String)
            activationCodeValue = value
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
#End Region

#Region "Events"
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetPort()
        GetComboBoxItem()
        GetDefaultValue()
        ReadOnlyControl(False)
    End Sub
    Private Sub btnTestConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestConnection.Click
        SendVerificationCode()
    End Sub
    Private Sub btnActivation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActivation.Click
        ActivateVerificationCode()
    End Sub
    Private Sub btnDeactivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeactivate.Click
        ReadOnlyControl(False)
    End Sub
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Try
            If (Me.cmbInbox.Text <> "" And rbReply.Checked) Or (GetModemID() <> "" And rbReceive.Checked) Then
                Dim autoreply As New FrmAutoReply
                Me.Hide()
                btnStart.Enabled = False
                autoreply.PortName = GetPortName()
                autoreply.ModemType = GetModemType()
                autoreply.MemoryType = MemoryType
                autoreply.MessageType = MessageType
                autoreply.BaudRate = BaudRate
                autoreply.IsReply = GetIsreply()
                autoreply.ModemID = GetModemID()
                autoreply.ActivationCode = ActivationCode
                autoreply.InboxID = Me.cmbInbox.Text
                autoreply.ShowDialog()
                btnStart.Enabled = True
                Me.Visible = True
            Else
                MessageBox.Show("Please check your ModemID and Inbox ID, Invalid value", "Error")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        'MyThreadReadSMS.Abort()
        'isStopvalue = True
        'btnStart.Enabled = True
        'btnStop.Enabled = False
        'GroupBox2.Enabled = True
        'GroupBox4.Enabled = True
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContacts.Click
        ViewContacts()
    End Sub
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.txtMobileNumber.Text = ""
    End Sub
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        btnSend.Enabled = False
        BroadCast()
        btnSend.Enabled = True
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        StartWithoutActivation()
    End Sub
    Private Sub dynamicLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles dynamicLinkLabel.LinkClicked
        dynamicLinkLabel.LinkVisited = True
        System.Diagnostics.Process.Start("http://www.pigeonclocking.somee.com")
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.txtActivationCode.Text = GetVerificationCode()
        'MessageBox.Show(GetVerificationCode, "Verification Code")
    End Sub
#End Region

#Region "Background Worker"
    Private Sub bgwAutoSend_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwAutosend.DoWork
        Try
            Dim IsStop As Boolean = False
            While Not IsStop
                If bgwAutosend.CancellationPending Then
                    e.Cancel = True
                    IsStop = True
                End If
                bgwAutosend.ReportProgress(1)
                System.Threading.Thread.Sleep(5000)
            End While
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub bgwAutoSend_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwAutosend.ProgressChanged
        Try
            SMS = New SMSBIZ()
            GroupBox2.Enabled = False
            SMS.PortName = GetPortName()
            SMS.IsSendReply = GetIsreply()
            SMS.ReadSMS()
            GroupBox2.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub bgwAutoSend_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwAutosend.RunWorkerCompleted
        btnStart.Enabled = True
        btnStop.Enabled = False
    End Sub
#End Region

#Region "Private Methods"
    Private Sub KillProcess()
        Dim proc = Process.GetProcessesByName("PostResult")
        For i As Integer = 0 To proc.Count - 1
            proc(i).Kill()
        Next i
    End Sub
    Private Sub GetDefaultValue()
        Me.cmbBaudRate.Text = "115200"
        Me.cmbMemory.Text = "GSM Memory"
        Me.cmbMessageType.Text = "ALL"
    End Sub
    Private Sub GetComboBoxItem()
        Me.cmbMemory.Items.Add("GSM Memory")
        Me.cmbMemory.Items.Add("SIM Memory")

        Me.cmbMessageType.Items.Add("REC UNREAD")
        Me.cmbMessageType.Items.Add("REC READ")
        Me.cmbMessageType.Items.Add("ALL")

        Me.cmbBaudRate.Items.Add("2400")
        Me.cmbBaudRate.Items.Add("4800")
        Me.cmbBaudRate.Items.Add("9600")
        Me.cmbBaudRate.Items.Add("19200")
        Me.cmbBaudRate.Items.Add("38400")
        Me.cmbBaudRate.Items.Add("57600")
        Me.cmbBaudRate.Items.Add("115200")

        Me.cmbInbox.Items.Add("Inbox 1")
        Me.cmbInbox.Items.Add("Inbox 2")

        Me.cmbModemID.Items.Add("--Receiver Globe--")
        Me.cmbModemID.Items.Add("Receiver 1 (Globe)")
        Me.cmbModemID.Items.Add("Receiver 2 (Globe)")
        Me.cmbModemID.Items.Add("Receiver 3 (Globe)")
        Me.cmbModemID.Items.Add("Receiver 4 (Globe)")
        Me.cmbModemID.Items.Add("Receiver 5 (Globe)")
        Me.cmbModemID.Items.Add(Chr(13))

        Me.cmbModemID.Items.Add("--Receiver Smart--")
        Me.cmbModemID.Items.Add("Receiver 1 (Smart)")
        Me.cmbModemID.Items.Add("Receiver 2 (Smart)")
        Me.cmbModemID.Items.Add("Receiver 3 (Smart)")
        Me.cmbModemID.Items.Add("Receiver 4 (Smart)")
        Me.cmbModemID.Items.Add("Receiver 5 (Smart)")
        Me.cmbModemID.Items.Add(Chr(13))

        Me.cmbModemID.Items.Add("--Sender Globe--")
        Me.cmbModemID.Items.Add("Sender 1 (Globe)")
        Me.cmbModemID.Items.Add("Sender 2 (Globe)")
        Me.cmbModemID.Items.Add("Sender 3 (Globe)")
        Me.cmbModemID.Items.Add("Sender 4 (Globe)")
        Me.cmbModemID.Items.Add("Sender 5 (Globe)")
        Me.cmbModemID.Items.Add("Sender 6 (Globe)")
        Me.cmbModemID.Items.Add("Sender 7 (Globe)")
        Me.cmbModemID.Items.Add("Sender 8 (Globe)")
        Me.cmbModemID.Items.Add("Sender 9 (Globe)")
        Me.cmbModemID.Items.Add("Sender 10 (Globe)")
        Me.cmbModemID.Items.Add("Sender 11 (Globe)")
        Me.cmbModemID.Items.Add("Sender 12 (Globe)")
        Me.cmbModemID.Items.Add("Sender 13 (Globe)")
        Me.cmbModemID.Items.Add("Sender 14 (Globe)")
        Me.cmbModemID.Items.Add("Sender 15 (Globe)")
        Me.cmbModemID.Items.Add(Chr(13))

        Me.cmbModemID.Items.Add("--Sender Smart--")
        Me.cmbModemID.Items.Add("Sender 1 (Smart)")
        Me.cmbModemID.Items.Add("Sender 2 (Smart)")
        Me.cmbModemID.Items.Add("Sender 3 (Smart)")
        Me.cmbModemID.Items.Add("Sender 4 (Smart)")
        Me.cmbModemID.Items.Add("Sender 5 (Smart)")
        Me.cmbModemID.Items.Add("Sender 6 (Smart)")
        Me.cmbModemID.Items.Add("Sender 7 (Smart)")
        Me.cmbModemID.Items.Add("Sender 8 (Smart)")
        Me.cmbModemID.Items.Add("Sender 9 (Smart)")
        Me.cmbModemID.Items.Add("Sender 10 (Smart)")
        Me.cmbModemID.Items.Add("Sender 11 (Smart)")
        Me.cmbModemID.Items.Add("Sender 12 (Smart)")
        Me.cmbModemID.Items.Add("Sender 13 (Smart)")
        Me.cmbModemID.Items.Add("Sender 14 (Smart)")
        Me.cmbModemID.Items.Add("Sender 15 (Smart)")

    End Sub
    Private Sub InitializeModem()
        Try
            smsmod = New SMSModule.SMSComponents
            'SMS.PortName = GetPortName()
            'SMS.ModemType = GetModemType()
            smsmod.ModemType = ModemType
            smsmod.BaudRate = BaudRate
            smsmod.MemoryType = MemoryType
            smsmod.PortNo = GetPortName()
            smsmod.InitialModem()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
    Private Sub BroadCast()
        Try
            Dim mobileNo As String
            Dim mobileNoCollection As Array
            Dim status As Boolean
            Dim counter As Int64 = 0
            SMS = New SMSBIZ()
            smsmod = New SMSModule.SMSComponents

            SMS.PortName = GetPortName()
            SMS.ModemType = GetModemType()
            SMS.MemoryType = MemoryType
            SMS.MessageType = MessageType
            SMS.BaudRate = BaudRate
            mobileNo = txtMobileNumber.Text

            If mobileNo <> "" Then
                mobileNoCollection = mobileNo.Split(";")
                Me.ProgressBar1.Maximum = mobileNoCollection.Length
                For Each item As String In mobileNoCollection
                    If item <> "" Then
                        If SMS.SendSMS(item, txtMessage.Text, False, -1, "Broadcast") Then
                            status = True
                        Else
                            status = False
                        End If
                    End If
                    counter += 1
                    Me.ProgressBar1.Value = counter
                    Thread.Sleep(3500)
                Next
                If (status) Then
                    MessageBox.Show("Message Sent", "Sending Message")
                    Me.txtMessage.Text = ""
                    Me.txtMobileNumber.Text = ""
                    Me.ProgressBar1.Value = 0
                Else
                    MessageBox.Show("Message Sending Failed", "Sending Message")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
    Private Function GetPortName() As String
        If PortNo = "" Then PortNo = cmbPort.Text
        Return PortNo
    End Function
    Private Function GetModemType() As String
        If rdGlobe.Checked Then
            ModemType = "Globe"
        Else
            ModemType = "Smart"
        End If
        Return ModemType
    End Function
    Private Function GetModemID() As String
        ModemID = Me.cmbModemID.Text
        Return ModemID
    End Function
    Private Function GetIsreply() As String
        If rbReply.Checked Then
            IsReply = True
        Else
            IsReply = False
        End If
        Return IsReply
    End Function
    Private Sub GetPort()
        For Each portName As String In SerialPort.GetPortNames()
            If portName <> "COM1" Then cmbPort.Items.Add(portName)
        Next
    End Sub
    Private Sub ActivateVerificationCode()
        VerificationCode = New VerificationCodeBIZ()
        VerificationCode.ActivateVerificationCode()
        If VerificationCode.PortName <> "" Then
            cmbPort.Text = VerificationCode.PortName
            ActivationCode = VerificationCode.ActivationCode()
            ReadOnlyControl(True)
        End If
    End Sub
    Private Sub StartWithoutActivation()
        ReadOnlyControl(True)
        ActivationCode = "NOT SET"
    End Sub
    Private Sub ReadOnlyControl(ByVal value As Boolean)
        Me.GroupBox1.Enabled = value
        Me.GroupBox2.Enabled = value
        Me.GroupBox3.Enabled = Not value
    End Sub
    Private Function GetVerificationCode() As String
        VerificationCode = New VerificationCodeBIZ()
        VerificationCode.ModemType = ModemType
        VerificationCode.PortName = cmbPort.Text
        Return VerificationCode.GenerateVerificationCode()
    End Function
    Private Sub SendVerificationCode()
        VerificationCode = New VerificationCodeBIZ()
        VerificationCode.ModemType = ModemType
        VerificationCode.PortName = cmbPort.Text
        VerificationCode.SendGenerationCode()
    End Sub
    Private Sub ViewContacts()
        Try
            Dim contacts As New Contacts
            contacts.ContactCollection = Me.txtMobileNumber.Text
            Me.txtMobileNumber.Text = ""
            contacts.ShowDialog()
            ContactCollection = ""
            ContactList = contacts.DataGridView1.DataSource
            If (ContactList.Rows.Count > 0) Then
                For Each row As DataRow In ContactList.Rows
                    If row(0).Equals(True) Then
                        If ContactCollection = "" Then
                            ContactCollection = row.Item(2).ToString()
                        Else
                            ContactCollection = ContactCollection + ";" + row.Item(2).ToString()
                        End If
                    End If
                Next
                If ContactCollection <> "" Then
                    txtMobileNumber.Text = ContactCollection
                    Me.lblreceivercount.Text = ContactList.Rows.Count.ToString()
                Else
                    Me.lblreceivercount.Text = "0"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
#End Region

#Region "MyThread"

#End Region

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetModem.Click
        If Me.cmdSetModem.Text = "SET MODEM" Then
            Dim errorMessage As String = ""
            If cmbBaudRate.Text = "" Then
                errorMessage = "Please select baudrate"
            ElseIf cmbMemory.Text = "" Then
                errorMessage = "Please select memory storage"
            ElseIf cmbMessageType.Text = "" Then
                errorMessage = "Please select message type"
            ElseIf cmbPort.Text = "" Then
                errorMessage = "Please select port number"
            End If

            If errorMessage = "" Then
                PortNo = cmbPort.Text
                BaudRate = cmbBaudRate.Text
                MessageType = cmbMessageType.Text
                If cmbMemory.Text = "GSM Memory" Then
                    MemoryType = "ME"
                Else
                    MemoryType = "SM"
                End If

                Me.cmdSetModem.Text = "UNSET MODEM"
                Me.cmbMemory.Enabled = False
                Me.cmbBaudRate.Enabled = False
                Me.cmbPort.Enabled = False
                Me.cmbMessageType.Enabled = False
                Me.GroupBox4.Enabled = True
                InitializeModem()
            Else
                MessageBox.Show(errorMessage, "Error")
            End If
        Else
            Me.cmdSetModem.Text = "SET MODEM"
            Me.cmbMemory.Enabled = True
            Me.cmbBaudRate.Enabled = True
            Me.cmbPort.Enabled = True
            Me.cmbMessageType.Enabled = True
            Me.GroupBox4.Enabled = False
        End If
    End Sub

    Private Sub txtMessage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMessage.KeyDown

    End Sub

    Private Sub txtMessage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMessage.KeyPress
        Validate()
    End Sub

    Private Sub txtMessage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMessage.TextChanged
        Me.Label8.Text = "Text Message : " & Len(Me.txtMessage.Text)
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        KillProcess()
    End Sub

    Private Sub rbReply_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbReply.CheckedChanged
        If Me.rbReply.Checked Then
            Me.cmbInbox.Enabled = True
        End If
    End Sub

    Private Sub rbReceive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbReceive.CheckedChanged
        If Me.rbReceive.Checked Then
            Me.cmbInbox.Enabled = False
        End If
    End Sub
End Class
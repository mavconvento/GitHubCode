<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.rbReceive = New System.Windows.Forms.RadioButton()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.rbReceiveReply = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.cmbMessageType = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmdSetModem = New System.Windows.Forms.Button()
        Me.cmbBaudRate = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbMemory = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPort = New System.Windows.Forms.ComboBox()
        Me.rbSmart = New System.Windows.Forms.RadioButton()
        Me.rdGlobe = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblreceivercount = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMobileNumber = New System.Windows.Forms.TextBox()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.btnContacts = New System.Windows.Forms.Button()
        Me.bgwAutosend = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbInbox = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.rbReply = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnDeactivate = New System.Windows.Forms.Button()
        Me.btnActivation = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtActivationCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbModemID = New System.Windows.Forms.ComboBox()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'rbReceive
        '
        Me.rbReceive.AutoSize = True
        Me.rbReceive.Checked = True
        Me.rbReceive.Location = New System.Drawing.Point(6, 35)
        Me.rbReceive.Name = "rbReceive"
        Me.rbReceive.Size = New System.Drawing.Size(65, 17)
        Me.rbReceive.TabIndex = 49
        Me.rbReceive.TabStop = True
        Me.rbReceive.Text = "Receive"
        Me.rbReceive.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(763, 285)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(128, 25)
        Me.btnStop.TabIndex = 34
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'rbReceiveReply
        '
        Me.rbReceiveReply.AutoSize = True
        Me.rbReceiveReply.Location = New System.Drawing.Point(763, 318)
        Me.rbReceiveReply.Name = "rbReceiveReply"
        Me.rbReceiveReply.Size = New System.Drawing.Size(120, 17)
        Me.rbReceiveReply.TabIndex = 46
        Me.rbReceiveReply.Text = "Send and Received"
        Me.rbReceiveReply.UseVisualStyleBackColor = True
        Me.rbReceiveReply.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbMessageType)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.cmdSetModem)
        Me.GroupBox3.Controls.Add(Me.cmbBaudRate)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.cmbMemory)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.cmbPort)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.rdGlobe)
        Me.GroupBox3.Controls.Add(Me.rbSmart)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Location = New System.Drawing.Point(127, 20)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(395, 147)
        Me.GroupBox3.TabIndex = 52
        Me.GroupBox3.TabStop = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(559, 90)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(118, 25)
        Me.Button4.TabIndex = 62
        Me.Button4.Text = "Stop Result Posting"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'cmbMessageType
        '
        Me.cmbMessageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMessageType.FormattingEnabled = True
        Me.cmbMessageType.Location = New System.Drawing.Point(114, 66)
        Me.cmbMessageType.Name = "cmbMessageType"
        Me.cmbMessageType.Size = New System.Drawing.Size(147, 21)
        Me.cmbMessageType.TabIndex = 61
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 69)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 13)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "Message Type :"
        '
        'cmdSetModem
        '
        Me.cmdSetModem.Location = New System.Drawing.Point(271, 9)
        Me.cmdSetModem.Name = "cmdSetModem"
        Me.cmdSetModem.Size = New System.Drawing.Size(118, 25)
        Me.cmdSetModem.TabIndex = 59
        Me.cmdSetModem.Text = "SET MODEM"
        Me.cmdSetModem.UseVisualStyleBackColor = True
        '
        'cmbBaudRate
        '
        Me.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaudRate.FormattingEnabled = True
        Me.cmbBaudRate.Location = New System.Drawing.Point(114, 38)
        Me.cmbBaudRate.Name = "cmbBaudRate"
        Me.cmbBaudRate.Size = New System.Drawing.Size(147, 21)
        Me.cmbBaudRate.TabIndex = 58
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(37, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 57
        Me.Label7.Text = "Baud Rate :"
        '
        'cmbMemory
        '
        Me.cmbMemory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMemory.FormattingEnabled = True
        Me.cmbMemory.Location = New System.Drawing.Point(114, 12)
        Me.cmbMemory.Name = "cmbMemory"
        Me.cmbMemory.Size = New System.Drawing.Size(147, 21)
        Me.cmbMemory.TabIndex = 42
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Memory Type :"
        '
        'cmbPort
        '
        Me.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPort.FormattingEnabled = True
        Me.cmbPort.Location = New System.Drawing.Point(114, 93)
        Me.cmbPort.Name = "cmbPort"
        Me.cmbPort.Size = New System.Drawing.Size(147, 21)
        Me.cmbPort.TabIndex = 38
        '
        'rbSmart
        '
        Me.rbSmart.AutoSize = True
        Me.rbSmart.Location = New System.Drawing.Point(173, 124)
        Me.rbSmart.Name = "rbSmart"
        Me.rbSmart.Size = New System.Drawing.Size(52, 17)
        Me.rbSmart.TabIndex = 31
        Me.rbSmart.Text = "Smart"
        Me.rbSmart.UseVisualStyleBackColor = True
        '
        'rdGlobe
        '
        Me.rdGlobe.AutoSize = True
        Me.rdGlobe.Checked = True
        Me.rdGlobe.Location = New System.Drawing.Point(114, 124)
        Me.rdGlobe.Name = "rdGlobe"
        Me.rdGlobe.Size = New System.Drawing.Size(53, 17)
        Me.rdGlobe.TabIndex = 32
        Me.rdGlobe.TabStop = True
        Me.rdGlobe.Text = "Globe"
        Me.rdGlobe.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(50, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Port No. :"
        '
        'btnTestConnection
        '
        Me.btnTestConnection.Location = New System.Drawing.Point(559, 62)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(118, 25)
        Me.btnTestConnection.TabIndex = 39
        Me.btnTestConnection.Text = "Send Verification"
        Me.btnTestConnection.UseVisualStyleBackColor = True
        Me.btnTestConnection.Visible = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(969, 34)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(103, 25)
        Me.Button3.TabIndex = 40
        Me.Button3.Text = "Get Verification"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblreceivercount)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.ProgressBar1)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtMessage)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtMobileNumber)
        Me.GroupBox2.Controls.Add(Me.btnSend)
        Me.GroupBox2.Controls.Add(Me.btnContacts)
        Me.GroupBox2.Location = New System.Drawing.Point(13, 231)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(509, 247)
        Me.GroupBox2.TabIndex = 51
        Me.GroupBox2.TabStop = False
        '
        'lblreceivercount
        '
        Me.lblreceivercount.AutoSize = True
        Me.lblreceivercount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreceivercount.Location = New System.Drawing.Point(78, 13)
        Me.lblreceivercount.Name = "lblreceivercount"
        Me.lblreceivercount.Size = New System.Drawing.Size(0, 13)
        Me.lblreceivercount.TabIndex = 59
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(7, 196)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 13)
        Me.Label8.TabIndex = 58
        Me.Label8.Text = "Text Message : 0"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(334, 90)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(169, 13)
        Me.ProgressBar1.TabIndex = 32
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(334, 58)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 26)
        Me.Button1.TabIndex = 31
        Me.Button1.Text = "Clear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Send To :"
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(9, 106)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(494, 87)
        Me.txtMessage.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Message :"
        '
        'txtMobileNumber
        '
        Me.txtMobileNumber.Location = New System.Drawing.Point(10, 27)
        Me.txtMobileNumber.MaxLength = 32000
        Me.txtMobileNumber.Multiline = True
        Me.txtMobileNumber.Name = "txtMobileNumber"
        Me.txtMobileNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMobileNumber.Size = New System.Drawing.Size(318, 60)
        Me.txtMobileNumber.TabIndex = 27
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(12, 213)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(491, 28)
        Me.btnSend.TabIndex = 29
        Me.btnSend.Text = "Send Message"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnContacts
        '
        Me.btnContacts.Location = New System.Drawing.Point(334, 27)
        Me.btnContacts.Name = "btnContacts"
        Me.btnContacts.Size = New System.Drawing.Size(103, 26)
        Me.btnContacts.TabIndex = 30
        Me.btnContacts.Text = "Select Contact"
        Me.btnContacts.UseVisualStyleBackColor = True
        '
        'bgwAutosend
        '
        Me.bgwAutosend.WorkerReportsProgress = True
        Me.bgwAutosend.WorkerSupportsCancellation = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbInbox)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.rbReply)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.rbReceive)
        Me.GroupBox1.Controls.Add(Me.btnStart)
        Me.GroupBox1.Location = New System.Drawing.Point(539, 256)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(205, 247)
        Me.GroupBox1.TabIndex = 50
        Me.GroupBox1.TabStop = False
        '
        'cmbInbox
        '
        Me.cmbInbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInbox.Enabled = False
        Me.cmbInbox.FormattingEnabled = True
        Me.cmbInbox.Location = New System.Drawing.Point(57, 62)
        Me.cmbInbox.Name = "cmbInbox"
        Me.cmbInbox.Size = New System.Drawing.Size(110, 21)
        Me.cmbInbox.TabIndex = 68
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 66)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 67
        Me.Label12.Text = "Inbox :"
        '
        'rbReply
        '
        Me.rbReply.AutoSize = True
        Me.rbReply.Location = New System.Drawing.Point(90, 35)
        Me.rbReply.Name = "rbReply"
        Me.rbReply.Size = New System.Drawing.Size(52, 17)
        Me.rbReply.TabIndex = 50
        Me.rbReply.Text = "Reply"
        Me.rbReply.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 15)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Pigeon Clocking System"
        '
        'btnStart
        '
        Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStart.Location = New System.Drawing.Point(39, 120)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(128, 102)
        Me.btnStart.TabIndex = 33
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnDeactivate
        '
        Me.btnDeactivate.Location = New System.Drawing.Point(186, 19)
        Me.btnDeactivate.Name = "btnDeactivate"
        Me.btnDeactivate.Size = New System.Drawing.Size(154, 27)
        Me.btnDeactivate.TabIndex = 48
        Me.btnDeactivate.Text = "DEACTIVATE BROADCAST"
        Me.btnDeactivate.UseVisualStyleBackColor = True
        '
        'btnActivation
        '
        Me.btnActivation.Location = New System.Drawing.Point(545, 198)
        Me.btnActivation.Name = "btnActivation"
        Me.btnActivation.Size = New System.Drawing.Size(124, 27)
        Me.btnActivation.TabIndex = 47
        Me.btnActivation.Text = "Activate System"
        Me.btnActivation.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 29)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(106, 138)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 53
        Me.PictureBox1.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Controls.Add(Me.btnDeactivate)
        Me.GroupBox4.Enabled = False
        Me.GroupBox4.Location = New System.Drawing.Point(12, 173)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(376, 57)
        Me.GroupBox4.TabIndex = 54
        Me.GroupBox4.TabStop = False
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(15, 18)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(152, 28)
        Me.Button2.TabIndex = 49
        Me.Button2.Text = "ACTIVATE BROADCAST"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(730, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 13)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "Activation Code :"
        Me.Label5.Visible = False
        '
        'txtActivationCode
        '
        Me.txtActivationCode.Location = New System.Drawing.Point(733, 37)
        Me.txtActivationCode.Name = "txtActivationCode"
        Me.txtActivationCode.Size = New System.Drawing.Size(172, 20)
        Me.txtActivationCode.TabIndex = 50
        Me.txtActivationCode.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(451, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 63
        Me.Label10.Text = "Version 1.0"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(560, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 13)
        Me.Label11.TabIndex = 65
        Me.Label11.Text = "Modem ID :"
        Me.Label11.Visible = False
        '
        'cmbModemID
        '
        Me.cmbModemID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbModemID.FormattingEnabled = True
        Me.cmbModemID.Location = New System.Drawing.Point(559, 35)
        Me.cmbModemID.Name = "cmbModemID"
        Me.cmbModemID.Size = New System.Drawing.Size(165, 21)
        Me.cmbModemID.TabIndex = 69
        Me.cmbModemID.Visible = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 490)
        Me.Controls.Add(Me.btnActivation)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.cmbModemID)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.rbReceiveReply)
        Me.Controls.Add(Me.txtActivationCode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnTestConnection)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pilipinas Kalapati Clocking (BroadCast Messaging)"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rbReceive As System.Windows.Forms.RadioButton
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents rbReceiveReply As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPort As System.Windows.Forms.ComboBox
    Friend WithEvents rbSmart As System.Windows.Forms.RadioButton
    Friend WithEvents rdGlobe As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnTestConnection As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMobileNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnContacts As System.Windows.Forms.Button
    Friend WithEvents bgwAutosend As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnDeactivate As System.Windows.Forms.Button
    Friend WithEvents btnActivation As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents rbReply As System.Windows.Forms.RadioButton
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtActivationCode As System.Windows.Forms.TextBox
    Friend WithEvents cmdSetModem As System.Windows.Forms.Button
    Friend WithEvents cmbBaudRate As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbMemory As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbMessageType As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblreceivercount As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbInbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbModemID As System.Windows.Forms.ComboBox
End Class

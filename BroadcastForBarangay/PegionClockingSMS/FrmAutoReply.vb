Imports System.ComponentModel
Imports DLL_Class

Public Class FrmAutoReply
    Private portNameValue As String = ""
    Private modemTypeValue As String = ""
    Private activationCodeValue As String = ""
    Private memoryTypeValue As String = ""
    Private messageTypeValue As String = ""
    Private baudRateTypeValue As String = ""
    Private modemIDvalue As String = ""
    Private inboxIDvalue As String = ""
    Private IsReplyValue As Boolean = False
    Private MyThreadReadSMS As Threading.Thread
    Private SMS As SMSBIZ

    Private bw As BackgroundWorker = New BackgroundWorker

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
            Return portNameValue
        End Get
        Set(ByVal value As String)
            portNameValue = value
        End Set
    End Property
    Public Property ModemType As String
        Get
            Return modemTypeValue
        End Get
        Set(ByVal value As String)
            modemTypeValue = value
        End Set
    End Property
    Public Property IsReply As Boolean
        Get
            Return IsReplyValue
        End Get
        Set(ByVal value As Boolean)
            IsReplyValue = value
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
    Public Property InboxID As String
        Get
            Return inboxIDvalue.Trim
        End Get
        Set(ByVal value As String)
            inboxIDvalue = value.Trim
        End Set
    End Property
    Private Sub FrmAutoReply_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            'KillProcess()
            'MyThreadReadSMS.Abort()
        Catch ex As Exception
            End
        End Try
    End Sub
    Private Sub FrmAutoReply_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CheckForIllegalCrossThreadCalls = False
        Me.Text = ModemID
        If IsReply Then
            Me.Button1.Text = "Stop Clocking System (Reply Mode " + InboxID + ")"
        Else
            Me.Button1.Text = "Stop Clocking System (Recieve Mode)"
        End If

        'MyThreadReadSMS = New Threading.Thread(AddressOf MyThreadSMS)
        'MyThreadReadSMS.IsBackground = True
        'MyThreadReadSMS.Start()
        If Not bw.IsBusy = True Then
            bw.RunWorkerAsync()
            'If Not (IsReply) Then StartProcess()
        End If
    End Sub
    Private Sub MyThreadSMS()
        Do
            SMS = New SMSBIZ()
            SMS.PortName = PortName
            SMS.ModemType = ModemType
            SMS.IsSendReply = IsReply
            SMS.ActivationCode = ActivationCode
            SMS.ModemID = ModemID
            If (IsReply) Then
                SMS.GetRecordForReply()
            Else
                SMS.ReadSMS()
            End If
            System.Threading.Thread.Sleep(2000)
        Loop
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Me.Close()
        If bw.WorkerSupportsCancellation = True Then
            bw.CancelAsync()
        End If
    End Sub

    Private Sub StartProcess()
        Dim StartResultPosting As Process = New Process()
        StartResultPosting.StartInfo.FileName = My.Application.Info.DirectoryPath.ToString() + "\PostResult.exe"
        StartResultPosting.Start()
    End Sub

    Private Sub KillProcess()
        Dim proc = Process.GetProcessesByName("PostResult")
        For i As Integer = 0 To proc.Count - 1
            proc(i).Kill()
        Next i
    End Sub

#Region "Background Worker"
    Public Sub New()
        InitializeComponent()
        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.ProgressChanged, AddressOf bw_ProgressChanged
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
    End Sub
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        Dim a As Integer = 0
        While a = 0
            If bw.CancellationPending = True Then
                e.Cancel = True
                a = 1
            Else
                ' Perform a time consuming operation and report progress.
                System.Threading.Thread.Sleep(500)
                SMS = New SMSBIZ()
                SMS.PortName = PortName
                SMS.ModemType = ModemType
                SMS.IsSendReply = IsReply
                SMS.ActivationCode = ModemID
                SMS.MessageType = MessageType
                SMS.MemoryType = MemoryType
                SMS.BaudRate = BaudRate
                SMS.ModemID = ModemID
                SMS.InboxType = InboxID
                If (IsReply) Then
                    SMS.GetRecordForReply()
                Else
                    SMS.ReadSMS()
                    'System.Threading.Thread.Sleep(3000)
                End If
                'System.Threading.Thread.Sleep(2000)
                'bw.ReportProgress(1)
            End If
        End While
        'Me.Close()
    End Sub
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled = True Then Me.Close()
        'ElseIf e.Error IsNot Nothing Then
        '    Me.tbProgress.Text = "Error: " & e.Error.Message
        'Else
        '    Me.tbProgress.Text = "Done!"
        'End If
    End Sub
    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        'Me.tbProgress.Text = e.ProgressPercentage.ToString() & "%"
    End Sub
#End Region
End Class
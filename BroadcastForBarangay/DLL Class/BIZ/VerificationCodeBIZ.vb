Imports System.Windows.Forms

Public Class VerificationCodeBIZ
    Private verificationCode As VerificationCodeDAL
    Private smsMod As SMSModule.SMSComponents
    Private sms As SMSDAL

#Region "Variables"
    Private portNameValue As String = ""
    Private modemTypeValue As String = ""
    Private activationCodeValue As String = ""
#End Region

#Region "Properties"
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
#End Region

#Region "Private Methods"
    Public Function GenerateVerificationCode() As String
        Try
            Dim code As String = ""
            verificationCode = New VerificationCodeDAL()
            code = verificationCode.GenerateVerificationCode(PortName)
            Return code
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetPortNoByActivationCode() As String
        Try
            verificationCode = New VerificationCodeDAL()
            PortName = verificationCode.VerifyVerificationCode(ActivationCode)
            Return PortName
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Public Methods"
    Public Sub SendGenerationCode()
        Try
            Dim mobileNo As String
            Dim msg As String = "Verification Code Sent!"
            Dim SendingMessage As String = ""
            Dim verificationCode As String = ""
            smsMod = New SMSModule.SMSComponents()
            mobileNo = InputBox("Please Enter Correct Mobile Number" + vbCrLf + "ex. 09173540062", "Verification Code")
            'mobileNo = "09173540062"

            If mobileNo <> "" Then
                verificationCode = GenerateVerificationCode()
                If verificationCode <> "" Then
                    msg = "Verification Code :" + vbCrLf + verificationCode
                    smsMod.PortNo = PortName
                    smsMod.ModemType = ModemType
                    If smsMod.SendSMS(False, 0, mobileNo, msg) Then
                        SendingMessage = "Verification Code Sent!"
                    Else
                        SendingMessage = "Error Detected, Verification code not sent"
                    End If
                Else
                    SendingMessage = "Verification code not generated, Please Try Again"
                End If
                sms = New SMSDAL()
                sms.OutBoxSave(SendingMessage, mobileNo, "Success", "Sending Verification", 0, 0)
                MessageBox.Show(SendingMessage, "Sending Verification Code")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
    Public Sub ActivateVerificationCode()
        Try
            Dim activationMessage As String = ""
            ActivationCode = InputBox("Please Enter Activation Code", "Activation Code")
            If ActivationCode <> "" Then
                PortName = GetPortNoByActivationCode()
                If PortName <> "" Then
                    activationMessage = "System is now activated"
                Else
                    activationMessage = "Invalid Activation"
                End If
                MessageBox.Show(activationMessage, "Activation System")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
#End Region
End Class

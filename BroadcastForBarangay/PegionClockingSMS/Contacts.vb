Imports DLL_Class

Public Class Contacts

#Region "Variables"
    Private contactBIZ As ContactBIZ
    Private collectionContactValue As String = ""
    Private clubNAme As String = ""
#End Region
#Region "Properties"
    Public Property ContactCollection As String
        Get
            Return collectionContactValue.Trim
        End Get
        Set(ByVal value As String)
            collectionContactValue = value.Trim
        End Set
    End Property
#End Region
#Region "Events"
    Private Sub Contacts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetClubList()
        GetContactCategory()
        GetContacts(False, ContactCollection, clubNAme, Me.ComboBox1.Text)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        GetContacts(False, "", clubNAme, Me.ComboBox1.Text)
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        GetContacts(False, ContactCollection, clubNAme, Me.ComboBox1.Text)
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click
        Me.Close()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        GetContacts(False, ContactCollection, clubNAme, Me.ComboBox1.Text)
    End Sub
    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        GetContacts(True, ContactCollection, clubNAme, Me.ComboBox1.Text)
    End Sub
#End Region
#Region "Private Methods"
    Private Sub GetClubList()
        Try
            Dim dtResult As String
            contactBIZ = New ContactBIZ()
            dtResult = contactBIZ.GetClubList()

            Me.Text = "Contact List of (" + dtResult + ")"
            clubNAme = dtResult

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetContacts(ByVal isSelectAll As Boolean, ByVal contactCollection As String, ByVal clubname As String, ByVal category As String)
        Try
            contactBIZ = New ContactBIZ()
            contactBIZ.GetContacts(Me.DataGridView1, isSelectAll, contactCollection, clubname, category)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetContactCategory()
        Try
            Dim dtResult As DataSet = New DataSet()
            contactBIZ = New ContactBIZ()
            dtResult = contactBIZ.GetContactCategory(clubNAme)

            Me.ComboBox1.Items.Clear()
            Me.ComboBox1.Items.Add("ALL")
            If (dtResult.Tables.Count > 0) Then
                If (dtResult.Tables(0).Rows.Count > 0) Then
                    For Each dr As DataRow In dtResult.Tables(0).Rows
                        Me.ComboBox1.Items.Add(dr("Name").ToString().ToUpper())
                    Next
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
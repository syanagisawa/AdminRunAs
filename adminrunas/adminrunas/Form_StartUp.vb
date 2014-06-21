Imports System.IO
Imports System.IO.File

Public Class Form_StartUp

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim vfilename As String
        Dim vworkingdirectory As String

        'Identify selected items
        vfilename = ListView1.SelectedItems(0).SubItems(0).Text
        vworkingdirectory = ListView1.SelectedItems(0).SubItems(1).Text
        'Call to run as administrator
        Module1_Run.runlikeadmin(vfilename, vworkingdirectory)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Get Computer and Username Logon
        Dim strUser As String = My.User.Name

        'Complete Form
        Label4.Text = strUser
        'Date and Time
        Timer1.Enabled = True

        ' Show "hidden" text
        ListView1.ShowItemToolTips = True
        ' Set columnar mode
        ListView1.View = View.Details
        ' Set column header
        ListView1.Columns.Clear()
        ListView1.Columns.Add("FileName", 340)
        ListView1.Columns.Add("Working Directory", 240)
        ' Remove previous items
        ListView1.Items.Clear()

        Module1_Run.readxmlconf()
        Module1_Run.SaveLog(strUser, "Started")

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label5.Text = Date.UtcNow 'Date.Now.ToLongTimeString
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        AboutBox1.Show()
    End Sub
End Class

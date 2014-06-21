Imports System.Security
Imports System.Security.Principal
Imports System.Diagnostics.Process
Imports System.IO
Imports System.Xml

Module Module1_Run

    Private Function CreateSecureString(ByVal str As String) As SecureString
        Dim s = New SecureString
        For Each c As Char In str
            s.AppendChar(c)
        Next
        Return s
    End Function

    Public Sub runlikeadmin(ByVal vfilename As String, ByVal vworkingdirectory As String)

        Try
            'Set the authentication user to work
            Dim username As String = "winelevate"
            Dim domain As String = Nothing
            Dim filename As String = vfilename

            'Set arguments for the process
            Dim pprep As New ProcessStartInfo
            pprep.FileName = filename
            pprep.Arguments = ""
            pprep.UserName = username
            'Protect password
            pprep.Password = CreateSecureString("Elevate")
            pprep.UseShellExecute = False
            pprep.WorkingDirectory = vworkingdirectory
            pprep.WindowStyle = ProcessWindowStyle.Normal
            Dim proc As Process = Process.Start(pprep)

            'Loggin
            Module1_Run.SaveLog(Form_StartUp.Label4.Text, "Ejecute - " + filename + " - Comment: " + Form_StartUp.TextBox1.Text)

        Catch ex As Exception
            'Error trapping
            MsgBox("Ups! something went wrong", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Public Sub readxmlconf()

        Try
            Dim m_xmld As XmlDocument
            Dim m_nodelist As XmlNodeList
            Dim m_node As XmlNode

            'Create Document
            m_xmld = New XmlDocument()
            'Load File
            Dim sIni As String = Application.StartupPath & "\runasadminconf.xml"
            m_xmld.Load(sIni)
            'Read node list
            m_nodelist = m_xmld.SelectNodes("/usuarios/name")

            'Start reading
            For Each m_node In m_nodelist
                'Get code
                Dim mCodigo = m_node.Attributes.GetNamedItem("codigo").Value

                If mCodigo = Form_StartUp.Label4.Text Then
                    'Get element
                    Dim mFileName = m_node.ChildNodes.Item(0).InnerText
                    'Get element
                    Dim mWorkingDirectory = m_node.ChildNodes.Item(1).InnerText

                    'Write data        
                    Dim TempStr(1) As String
                    Dim TempNode As ListViewItem

                    ' Add two items
                    TempStr(0) = mFileName
                    TempStr(1) = mWorkingDirectory
                    TempNode = New ListViewItem(TempStr)
                    Form_StartUp.ListView1.Items.Add(TempNode)
                End If
            Next

        Catch ex As Exception
            'Error trapping
            MsgBox("Ups! something went wrong", MsgBoxStyle.Exclamation)
        End Try

    End Sub

    Public Sub SaveLog(ByVal douser As String, ByVal slog As String)
        'Set path for logfile
        Dim Logpath As String = Application.StartupPath & "\adminrunas.log"

        Try
            Dim oSW As New StreamWriter(Logpath, True)
            Dim scomando As String = String.Empty
            oSW.WriteLine(Now & " adminrunas: " & douser & ": - " & slog)
            oSW.Flush()
            oSW.Close()

        Catch ex As Exception
            'Error trapping
            MsgBox("Ups! something went wrong", MsgBoxStyle.Exclamation)
        End Try

    End Sub

End Module

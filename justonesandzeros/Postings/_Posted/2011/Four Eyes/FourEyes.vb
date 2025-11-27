Imports Autodesk.Connectivity.WebServices
Imports Autodesk.Connectivity.WebServicesTools
Imports Autodesk.Connectivity.Extensibility.Framework

<Assembly: ApiVersion("4.0")> 
<Assembly: ExtensionId("EF55A79B-90DC-47AF-8894-332D536E4164")> 

Public Class FourEyesChecker
    Implements IWebServiceExtension

    ' implement IWebServiceExtension
    Public Sub OnLoad() Implements Autodesk.Connectivity.WebServices.IWebServiceExtension.OnLoad
        AddHandler DocumentServiceExtensions.UpdateFileLifecycleStateEvents.GetRestrictions, _
            AddressOf UpdateFileLifecycleStateEvents_GetRestrictions
    End Sub

    ' event handler
    Private Sub UpdateFileLifecycleStateEvents_GetRestrictions(ByVal sender As Object, ByVal e As UpdateFileLifeCycleStateCommandEventArgs)
        Try
            Dim service As IWebService = TryCast(sender, IWebService)
            If service Is Nothing Then
                Return
            End If

            Dim cred As New WebServiceCredentials_bugfix(service)
            Using mgr As New WebServiceManager(cred)
                Dim currentUserId As Long = mgr.SecurityService.SecurityHeader.UserId

                Dim defs As LfCycDef() = mgr.DocumentServiceExtensions.GetAllLifeCycleDefinitions()
                Dim releaseProcess As LfCycDef = defs.FirstOrDefault(Function(n) n.SysName = "Flexible Release Process")

                Dim reviewState As LfCycState = releaseProcess.StateArray.FirstOrDefault(Function(n) n.DispName = "For Review")

                Dim releaseState As LfCycState = releaseProcess.StateArray.FirstOrDefault(Function(n) n.DispName = "Released")

                Dim fileCollection As FileArray() = mgr.DocumentService.GetFilesByMasterIds(e.FileMasterIds)

                For i As Integer = 0 To fileCollection.Length - 1
                    CheckFile(fileCollection(i).Files, e.ToStateIds(i), currentUserId, reviewState, releaseState, e)
                Next
            End Using
        Catch
        End Try
    End Sub

    ' checks for a four eyes violation for a given file history
    Private Sub CheckFile(ByVal files As File(), ByVal toStateId As Long, ByVal currentUserId As Long, ByVal reviewState As LfCycState, ByVal releaseState As LfCycState, ByVal eventArgs As WebServiceCommandEventArgs)
        ' if we are not moving to released, don't event bother with the check
        If toStateId <> releaseState.Id Then
            Return
        End If

        Dim maxFile As File = files.First(Function(n) n.MaxCkInVerNum = n.VerNum)
        If maxFile.FileRev Is Nothing Then
            Return
        End If

        ' gather all the files in the revision and arrange them by version
        Dim filesInRev As IEnumerable(Of File) = From n In files Where n.FileRev.RevId = maxFile.FileRev.RevId Order By n.VerNum

        Dim filesArray As File() = filesInRev.ToArray()

        Dim reviewUserId As Long = -1
        For i As Integer = 1 To filesArray.Length - 1
            Dim f1 As File = filesArray(i - 1)
            Dim f2 As File = filesArray(i)

            ' compare two concecutive file versions to determine 
            ' where a state changed happened
            If f1.FileLfCyc IsNot Nothing AndAlso f2.FileLfCyc IsNot Nothing AndAlso f1.FileLfCyc.LfCycStateName <> f2.FileLfCyc.LfCycStateName AndAlso f2.VerNum - f1.VerNum = 1 Then
                ' f2 is a version where the state changed
                If f2.FileLfCyc.LfCycStateName = reviewState.DispName Then
                    reviewUserId = f2.CreateUserId
                End If
            End If
        Next

        If reviewUserId > 0 AndAlso currentUserId = reviewUserId Then
            ' the same person reviewed the file in an earlier version
            eventArgs.AddRestriction(New ExtensionRestriction(maxFile.Name, "File cannot be reviewed and released by the same person"))
        End If
    End Sub

End Class


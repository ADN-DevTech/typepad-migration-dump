---
layout: "post"
title: "Parameters, Snippets, Batch Mode and Self-Signing"
date: "2022-06-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2023"
  - "Batch"
  - "Cloud"
  - "Git"
  - "Journal"
  - "Logging"
  - "Open Source"
  - "Parameters"
  - "Security"
  - "Server"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/06/parameters-snippets-batch-mode-and-self-signing.html "
typepad_basename: "parameters-snippets-batch-mode-and-self-signing"
typepad_status: "Publish"
---

<p>Picking up some specially interesting topics from
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and
elsewhere:</p>

<ul>
<li><a href="#2">Revit 2023 parameters service cloud</a></li>
<li><a href="#3">Roll your own verified publisher</a></li>
<li><a href="#3b">Reset the unsigned add-in security warning</a></li>
<li><a href="#3c">Remove the code signing certificate</a></li>
<li><a href="#4">Revit API code snippet repository</a></li>
<li><a href="#5">Batch processing and monitoring progress</a></li>
</ul>

<h4><a name="2"></a> Revit 2023 Parameters Service Cloud</h4>

<p>In Revit 2023, a new cloud-based parameters service is under evaluation.</p>

<p>Learn all about it in the two-and-a-half-minute video
on <a href="https://youtu.be/cRz7kQz88mA">Revit 2023 Parameters Service</a>.</p>

<blockquote>
  <p>Manage your parameters library more efficiently across projects and locations using the Parameters Service with Revit and the Autodesk Construction Cloud...</p>
</blockquote>

<p>It’s currently in a preview phase with no API, yet.
API support will presumably be coming along after the evaluation phase.</p>

<h4><a name="3"></a> Roll Your Own Verified Publisher</h4>

<p>Are you getting tired of struggling with the Revit add-in security warning about an unsigned add-in saying, <em>the publisher of this add-in could not be verified</em>?</p>

<p>Konrad Sobon of <a href="https://archi-lab.net">archi+lab</a> explains how
to <a href="https://archi-lab.net/creating-a-self-signed-code-signing-certificate">create a self-signed code signing certificate</a> for yourself for free:</p>

<blockquote>
  <p>... <i>[detailed explanation]</i> ... That’s it!
  That should create your PFX file that you can now use with signtool, and code sign your Revit plugins for free!
  This self-signed code signing certificate won’t expire for another 17 years so you should be good to go for a while.
  Now, be aware of the fact that this self-signed code signing certificate is not the same as one issued to you by a 3rd party.
  I guess the level of “trust” here would be a little different, but in this particular case, I don’t think it matters to me.
  I am fed up with paying money to companies that have just atrocious customer support.
  If you are using these code signing certificates literally to just sign Revit plugins, then there is no reason to obtain one from a 3rd party and pay a hefty price for it on top of all the hoops that they will make you jump through.
  I hope this helps some of the AEC development community out there save some money and time.</p>
</blockquote>

<p>Thank you very much, Konrad for digging in so deep and sharing this very helpful approach!</p>

<p>For the sake of completeness, you can also check out
Konrad's previous post on <a href="http://archi-lab.net/code-signing-of-your-revit-plug-ins">code signing of your Revit plug-ins</a>,
the main <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/code-signing-of-revit-addins/m-p/5981560">Code signing of Revit Addins</a>
and previous articles on this by The Building Coder:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/whats-new-in-the-revit-2017-api.html#2.4">What's New in the Revit 2017 API &ndash; Code signing of Revit Addins</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/09/trusted-signature-motivation-and-fishing.html#2">How Does Code Signing of Revit Add-Ins Increase Security?</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/09/code-signing-preview-and-element-type-predicates.html#2">Revit Add-In Code Signing YAML</a> to automate code signing in CI</li>
</ul>

<h4><a name="3b"></a> Reset the Unsigned Add-In Security Warning</h4>

<p>If, on the contrary, you wish to see the message, and accidentally disabled it, Eatrevitpoopcad's discovery how 
to <a href="https://forums.autodesk.com/t5/revit-api-forum/reset-the-unsigned-add-in-security-warning/td-p/11090662">reset the unsigned add-in security warning</a> will
come in useful:</p>

<p><strong>Question:</strong> Does anyone know how to reset the security warning Revit gives you when loading unsigned add-ins?</p>

<p>After you click <em>Always Load</em> and you never see it again?</p>

<p>Let's say I pressed <em>Always Load</em> and then changed my mind and would like to keep seeing that message:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a30d3e4dcd200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a30d3e4dcd200b img-responsive" alt="Unsigned add-in security warning message" title="Unsigned add-in security warning message" src="/assets/image_a33bcc.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> I found it in the registry!</p>

<p>Here is how to reset code signing warnings:</p>

<ul>
<li>Go to START </li>
<li>Enter REGEDIT</li>
<li>Go to the following registry key:
<em>HKEY_CURRENT_USER\SOFTWARE\Autodesk\Revit\Autodesk Revit 2022\CodeSigning</em></li>
</ul>

<p>You will see a bunch of GUID value names with data values 0 or 1.
Each one is for a different plugin you codesigned.
Select them all and delete them.</p>

<p>This is for Revit 2022; for other versions of Revit, go up a folder and pick the Revit version for which you would like to clear code signing entries.</p>

<p>Many thanks to Eatrevitpoopcad for sharing this!</p>

<h4><a name="3c"></a> Remove the Code Signing Certificate</h4>

<p>Matthew Taylor adds:</p>

<p>If you want to completely remove the code signing certificate, you can follow these steps:</p>

<ul>
<li>From a cmd prompt, run MMC, the Microsoft Management Console. 
You may also type MMC after bringing up the Windows ‘Start’ menu.</li>
<li>File &gt; Add/Remove Snap-in... &gt; Certificates &gt; Add &gt; My user account &gt; Okay.</li>
<li>Navigate to Certificates - Current User &gt; Trusted Publishers &gt; Certificates.</li>
<li>You should now see a list of signatures.</li>
<li>Proceed with extreme caution.</li>
<li>To delete a certificate, just right-click &gt; Delete.</li>
<li>To close the console: File &gt; Close &gt; No.</li>
</ul>

<p>Thank you, Matt!</p>

<h4><a name="4"></a> Revit API Code Snippet Repository</h4>

<p>Maycon Freitas, architect, Dynamo, Revit API Developer and Forge enthusiast at <a href="https://www.blossomconsult.com">Blossom Consult</a>,
shares a new collection of Revit API code snippets and invites the community to join in:</p>

<blockquote>
  <p>I'm creating the <a href="https://github.com/mayconrfreitas/RevitAPISnippets">RevitAPISnippets GitHub repository</a> to
  share Revit API code snippets with our Revit developer community.
  If you're interested in contributing somehow, I would really appreciate.
  The idea is to create an open source project to help developers to improve coding performance.</p>
</blockquote>

<p>More about this project in the two-and-a-half-minute video
on <a href="https://youtu.be/moD7CYUkJHw">RevitAPISnippets: 170+ code lines in 2 minutes (Revit API)</a>.</p>

<h4><a name="5"></a> Batch Processing and Monitoring Progress</h4>

<p>Questions on batch processing BIMs come up on a regular basis, and surfaced again discussing 
a <a href="https://forums.autodesk.com/t5/revit-api-forum/way-to-check-if-family-is-corrupt/m-p/11174180">way to check if family is corrupt</a> with
<a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/311888">Phillip Miller</a> and
Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas:</p>

<p><strong>Question:</strong> I have a simple addon that iterates over all the families in a document and exports them to a folder amongst other things, using <code>fam.SaveAs(path, options)</code>.</p>

<p>All works as expected until the very rare case when it hits a family that is corrupt.
This doesn't happen often, but it does happen.
When it hits such a family there are no warning messages, doesn't crash, just hangs.</p>

<p>My question: Is there a way that I can check for this situation or a way to skip on to the next family?</p>

<p><strong>Answer:</strong> I don't think so.</p>

<p>You can't cancel the process either so also no possibility of setting a time limit. Only thing you could do is create a logging system where the last item that caused the issue along with those already exported up to that point can be skipped on the next run of the Add-in.</p>

<p>For these kind of issues, all you can really do from an API developer perspective is get end user to open a support case with Autodesk regarding the attached project file (or you could open one).
A brief review of the journal indicates an issue with many short curves.</p>

<p>Regarding end user support case they can easily replicate the issue outside of the API by attempting to edit the family in the UI.
It seems to be getting stuck in a regeneration loop and may eventually resolve.
I wouldn't classify it as document corruption because that tends to have an abrupt end, i.e., file would not open and error message would be shown in UI.
Seems to be an issue with how Revit responds to a certain circumstance that the file causes, i.e., problem with Revit.</p>

<p><strong>Response:</strong> Thanks for your reply.</p>

<p>It was what I thought but just wanted to make sure I wasn't missing anything.</p>

<p>This is meant to be a totally automated process with no user interaction so is a bit of a shame I can't get around this catch and move on.  What I will have to do is set a timer and if there is a long period of time opening a family, pop up a message box of some sort to notify the user of the issue when they get back to their workstation.</p>

<p><strong>Answer:</strong> Should also mention the <code>ProgressChangedEventArgs.Cancel</code> of <code>Application.ProgressChanged</code>.</p>

<p>This reports the progress of the little green bar that appears in the bottom left.
This has a cancel button next to it that is available when opening a family.
So, it may be possible to cancel via that if you spot a recurring pattern of progress as appears to happen with this family, e.g., how many times it goes to 25% after 50% etc.</p>

<p>ProgressChangedEventArgs.Caption may also report <code>Regenerating</code>, so you could also count those and set limits on that.</p>

<p>This all hinges on if ProgressChangedEventArgs.Cancel works, i.e., it works in the UI immediately for the family in question.</p>

<p>Below is an example that seems to work well for English Language Revit.</p>

<p>When you call cancel on the event it throws an exception for the calling method so you can catch that and move onto the next item. The below works by counting the occurrences of "Regenerating", a count of around 2200 is about right for that family (when it starts to loop).</p>

<p>You could as an alternative use the progress Position property but to get a percentage you need to divide the Position property by the UpperRange property. This approach doesn't seem as good as the regen caption approach because the percentages encountered are more random and not a good indicator of a pattern of repetition (would have to store more in an array to check against).</p>

<pre class="prettyprint">
  Private IntRegenCount As Integer = 0

  Private Sub ProgressChanged(a As Object, e As Autodesk.Revit.DB.Events.ProgressChangedEventArgs)
    If e.Caption = "Regenerating" Then
      IntRegenCount += 1
    End If
    If IntRegenCount > 2200 Then
      'IntRegenCount = 0
      e.Cancel()
    End If
  End Sub

  Public Function Obj_220516d(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result
    Dim IntUIApp As UIApplication = commandData.Application
    Dim IntUIDoc As UIDocument = commandData.Application.ActiveUIDocument
    Dim IntDoc As Document = IntUIDoc.Document

    AddHandler commandData.Application.Application.ProgressChanged, AddressOf ProgressChanged
    Const NM As String = "Vantage Metro Series Hinged Door Jamb"

    Dim FEC As New FilteredElementCollector(IntDoc)
    Dim ECF As New ElementClassFilter(GetType(Family))
    Dim F As Family = FEC.WherePasses(ECF) _
      .Where(Function(x) x.Name = NM) _
      .Cast(Of Family) _
      .FirstOrDefault

     Dim FDoc As Document = Nothing

    Try
      FDoc = IntDoc.EditFamily(F)
    Catch ex As Exception
      Return Result.Cancelled
    Finally
      RemoveHandler commandData.Application.Application.ProgressChanged, AddressOf ProgressChanged
    End Try

    Return Result.Succeeded

  End Function
</pre>

<p>This below non-language dependant example appears to work also.
In below I attempt to edit the problem family before moving on to a second family in the project file that does open.</p>

<p>Again, you have to look at the numbers for variables N and T below.
The limit of T below often occurs first and seems to give adequate time allowance to edit problem family.</p>

<p>For either case, you have to handle the DialogBoxShowing event to cancel the dialogue that appears after cancelling the progress changed event.</p>

<pre class="prettyprint">
  Private IntProgressLog As Integer() = New Integer(20) {}

  Private Sub ProgressChanged(s As Object, e As Autodesk.Revit.DB.Events.ProgressChangedEventArgs)
    Dim ULim As Double = e.UpperRange
    Dim Pos As Double = e.Position
    Dim Perc As Integer = ((Pos / ULim) * 20)
    IntProgressLog(Perc) += 1

    Dim N As Integer = IntProgressLog.Max
    Dim T As Integer = IntProgressLog.Sum
    If N > 2000 OrElse T > 15000 Then
      e.Cancel()
    End If
  End Sub
  Private Sub MsgBoxShowing(s As Object, e As Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs)
    Dim IntUIApp As UIApplication = s
    IntProgressLog = New Integer(20) {} 'reset the variable for next family
    e.OverrideResult(2)
  End Sub

  Public Function Obj_220516d(commandData As ExternalCommandData, ByRef message As String, elements As ElementSet) As Result
    Dim IntUIApp As UIApplication = commandData.Application
    Dim IntUIDoc As UIDocument = commandData.Application.ActiveUIDocument
    Dim IntDoc As Document = IntUIDoc.Document

    AddHandler IntUIApp.Application.ProgressChanged, AddressOf ProgressChanged
    AddHandler IntUIApp.DialogBoxShowing, AddressOf MsgBoxShowing

    Dim NM As String() = New String(1) {"Vantage Metro Series Hinged Door Jamb", "_ Callout Head"}

    Dim FDoc As Document = Nothing
    For i = 0 To 1
      Dim K As Integer = i
      Dim FEC As New FilteredElementCollector(IntDoc)
      Dim ECF As New ElementClassFilter(GetType(Family))
      Dim F As Family = FEC.WherePasses(ECF) _
        .Where(Function(x) x.Name = NM(K)) _
        .Cast(Of Family) _
        .FirstOrDefault

      Try
        FDoc = IntDoc.EditFamily(F)
      Catch ex As Exception
      End Try
      If FDoc IsNot Nothing Then
        Debug.WriteLine("Edit of: " & NM(i))
      End If
    Next

    RemoveHandler IntUIApp.Application.ProgressChanged, AddressOf ProgressChanged
    RemoveHandler IntUIApp.DialogBoxShowing, AddressOf MsgBoxShowing

    Return Result.Succeeded
  End Function
</pre>

<p><strong>Answer 2:</strong> Following up on some of your initial thoughts, I would assume that this can be totally automated after all, like this:</p>

<ul>
<li>Run an external Windows app that monitors progress and can kill Revit.exe by terminating the process via native OS calls</li>
<li>Loop through all the families and call SaveAs on each, logging progress to an external file (or wherever you like)</li>
<li>The log file helps keep track of where you are and where you need to proceed next</li>
<li>If the external file has not been updated for a while (a minute? ten minutes?), kill Revit.exe and restart with the next family to export, logging and skipping the corrupt one</li>
</ul>

<p>Sound feasible? It is a pretty standard approach to run a batch process.
Revit is not designed for batch processing dozens of files, let alone hundreds or thousands, so crashes of all kinds are to be expected, perfectly normal, and need to be handled gracefully by batch processing workflows.
Check out <a href="https://thebuildingcoder.typepad.com/blog/batch">The Building Coder 'batch' category</a>.</p>

<p><strong>Response:</strong> Thank you so much.</p>

<p>You have gone way above and beyond with providing examples etc, and I thank you for that.</p>

<p>I'm back working on this project next week and I will let you know how I get on.</p>

<p>Many thanks to Phillip for raising the issue and Richard for his indefatigable help.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a2eec699ed200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a2eec699ed200d img-responsive" style="width: 320px; display: block; margin-left: auto; margin-right: auto;" alt="Programming progress" title="Programming progress" src="/assets/image_48cb03.jpg" /></a><br /></p>

<p></center></p>

---
layout: "post"
title: "Purge Unused using Performance Adviser"
date: "2018-08-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - "BIM"
  - "Deletion"
  - "Element Relationships"
  - "Performance"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/08/purge-unused-using-performance-adviser.html "
typepad_basename: "purge-unused-using-performance-adviser"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>We repeatedly looked at ways to detect and purge unused elements. 
A list of some previous discussions of the topic was given last time we looked
at <a href="http://thebuildingcoder.typepad.com/blog/2017/11/purge-and-detecting-an-empty-view.html">purge and detecting an empty view</a>.</p>

<p>Matt Taylor, associate and CAD developer at <a href="https://www.wsp.com">WSP</a>,
was <a href="http://thebuildingcoder.typepad.com/blog/2018/08/ten-years-anniversary-and-revit-api-with-mvvm-wpf-and-winform.html#comment-4053631853">the first to congratulate</a>
on <a href="http://thebuildingcoder.typepad.com/blog/2018/08/ten-years-anniversary-and-revit-api-with-mvvm-wpf-and-winform.html">The Building Coder's ten-year anniversary</a>.</p>

<p>He now adds something really special to celebrate this:</p>

<ul>
<li><a href="#2">Purge Unused using the Performance Adviser</a> </li>
<li><a href="#3">PurgeTool.vb implements <code>GetPurgeableElements</code></a> </li>
<li><a href="#4">PurgeUnused.vb External Command</a> </li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad3669205200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad3669205200c img-responsive" style="width: 201px; display: block; margin-left: auto; margin-right: auto;" alt="Broomstick" title="Broomstick" src="/assets/image_a0b840.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> Purge Unused using the Performance Adviser</h4>

<p>I’m sharing with you a new discovery of mine.</p>

<p>Apparently, nobody has previously publicly discovered a simple and effective way of purging all unused elements.</p>

<p>I now found one:</p>

<p>I have successfully used
the <a href="http://help.autodesk.com/view/RVT/2019/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Advanced_Topics_Performance_Adviser_html">Performance Adviser</a> to
do a similar job to the native <code>Purge Unused</code> command.</p>

<p>Please refer to
my <a href="https://gitlab.com/MattTaylor/RevitPurgeUnused">RevitPurgeUnused GitLab repository</a>.</p>

<p>While the code will compile back to Revit 2012, it actually throws an <code>InternalException</code> for versions 2012-2016 (in my experience).</p>

<p>It doesn’t do a perfect job (e.g., it doesn’t purge materials and material assets), but it is very, very good, and quite fast.</p>

<p>I also added a note of my solution to some of the existing threads on this topic in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/purge-unused-via-the-api/m-p/8229573">Purge Unused Via the API</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/cf-1201-purge-unused-objects/m-p/8229574">CF-1201 <em>Purge unused objects</em></a></li>
</ul>

<p>Very many thanks to Matt for sharing this solution to one of the top developer wish list items!</p>

<h4><a name="3"></a> PurgeTool.vb implements GetPurgeableElements</h4>

<p>Here is Matt's VB.NET code
in <a href="https://gitlab.com/MattTaylor/RevitPurgeUnused/blob/master/PurgeTool.vb">PurgeTool.vb</a>,
defining the long-sought-after <code>GetPurgeableElements</code> method:</p>

<pre class="prettyprint">
#Region "Imported Namespaces"
Imports System
Imports System.Collections.Generic
Imports Autodesk.Revit.DB
#End Region

Public Class PurgeTool
  ''' &lt;summary&gt;
  ''' The guid of the 'Project contains unused families and types' PerformanceAdviserRuleId.
  ''' &lt;/summary&gt;
  Const PurgeGuid As String = "e8c63650-70b7-435a-9010-ec97660c1bda"

  ''' &lt;summary&gt;
  ''' Get all purgeable elements.
  ''' Intended for Revit 2017+ as versions up to and including Revit 2016 throw an InternalException.
  ''' &lt;/summary&gt;
  ''' &lt;param name="doc"&gt;&lt;/param&gt;
  ''' &lt;param name="purgeableElementIds"&gt;&lt;/param&gt;
  ''' &lt;returns&gt;True if successful.&lt;/returns&gt;
  Shared Function GetPurgeableElements(doc As Document, ByRef purgeableElementIds As ICollection(Of ElementId)) As Boolean
    purgeableElementIds = New List(Of ElementId)()

    Try
      'create a new list of rules.
      Dim ruleIds As IList(Of PerformanceAdviserRuleId) = New List(Of PerformanceAdviserRuleId)
      Dim ruleId As PerformanceAdviserRuleId = Nothing
      'find the intended rule.
      If GetPerformanceAdvisorRuleId(PurgeGuid, ruleId) Then
        'add the rule to the new list.
        ruleIds.Add(ruleId)
      Else
        'cannot find rule.
        Return False
      End If
      'execute our chosen rule only.
      Dim failureMessages As IList(Of FailureMessage) = PerformanceAdviser.GetPerformanceAdviser().ExecuteRules(doc, ruleIds)
      If failureMessages.Count &gt; 0 Then
        'If there are any purgeable elements, we should have a failure message.
        'the failure message should have a collection of failing elements - set to our byref collection
        purgeableElementIds = failureMessages.Item(0).GetFailingElements
      End If
      'no errors - return true.
      Return True
    Catch ex As Autodesk.Revit.Exceptions.InternalException
      'this exception gets thrown a lot in earlier versions of Revit - up to and including Revit 2016.

    End Try
    'likely thrown an internal exception
    Return False
  End Function

  ''' &lt;summary&gt;
  ''' Find a PerformanceAdviserRuleId with a guid that matches a supplied guid.
  ''' &lt;/summary&gt;
  ''' &lt;param name="guidStr"&gt;&lt;/param&gt;
  ''' &lt;param name="ruleId"&gt;&lt;/param&gt;
  ''' &lt;returns&gt;true if successful, along with the rule as a byref.&lt;/returns&gt;
  Private Shared Function GetPerformanceAdvisorRuleId(ByVal guidStr As String, ByRef ruleId As PerformanceAdviserRuleId) As Boolean
    ruleId = Nothing
    Dim guid As Guid = New Guid(guidStr)
    For Each rule As PerformanceAdviserRuleId In PerformanceAdviser.GetPerformanceAdviser().GetAllRuleIds
      'check if the rule Id matches our rule guid
      If rule.Guid.Equals(guid) Then
        'it does - set rule to our byref object
        ruleId = rule
        Return True
      End If
    Next
    'failed to find the rule matching our guid.
    Return Nothing
  End Function
End Class
</pre>

<h4><a name="4"></a> PurgeUnused.vb External Command</h4>

<p>The result is used like this in the external command defined 
by <a href="https://gitlab.com/MattTaylor/RevitPurgeUnused/blob/master/PurgeUnused.vb">PurgeUnused.vb</a>:</p>

<pre class="prettyprint">
  Dim purgeableElements As ICollection(Of ElementId) = Nothing
  If PurgeTool.GetPurgeableElements(doc, purgeableElements) AndAlso purgeableElements.Count &gt; 0 Then
    Using transaction As New Transaction(doc, "Purge Unused")
      transaction.Start()
      doc.Delete(purgeableElements)
      transaction.Commit()
      Return Result.Succeeded
    End Using
  Else
    Return Result.Failed
  End If
</pre>

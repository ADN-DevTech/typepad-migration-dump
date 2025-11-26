---
layout: "post"
title: "RevitiAPI: How to create revision cloud?"
date: "2015-06-16 00:15:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/06/revitiapi-how-to-create-revision-cloud.html "
typepad_basename: "revitiapi-how-to-create-revision-cloud"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/46310659">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>Again, use RevitLookup, we can see the related class is RevisionCloud, so the creation method is either in Document.Create or in the static methods of the class itself.</p>
<p>Fortunately, there is a static method called Create:</p>
<p>public static RevisionCloud Create(Document document, View view, ElementId revisionId, IList&lt;Curve&gt; curves)</p>
<p>We should know the meaning of most of the arguments, except revisionId, where can we get it?</p>
<p>&#0160;</p>
<p>I tried to pass ElementId.InvalidElementId, it throws:</p>
<p>Autodesk.Revit.Exceptions.ArgumentException: revisionId is not a valid Revision. &#0160;Parameter name: revisionId</p>
<p>So we have to pass something meaningful.</p>
<p>At this moment, I found there is a property RevisionCloud.RevisionId which should be relevant, and that id is a Revision.</p>
<p>Inspect Revision, it also has a static Create method, now the solution is obvious, and simple:</p>
<pre class="csharp prettyprint">var revision = Revision.Create(RevitDoc);
var revisionCloud = RevisionCloud.Create(RevitDoc,
    RevitDoc.ActiveView, ElementId.InvalidElementId,
    new List&lt;Curve&gt;() {
    Line.CreateBound(XYZ.Zero, new XYZ(10,0,0))
});</pre>
<p>&#0160;</p>

---
layout: "post"
title: "RevitAPI:Do not add or delete elements when iterating document"
date: "2015-03-30 01:19:43"
author: "Aaron Lu"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2015/03/revitapido-not-add-or-delete-elements-when-iterating-document.html "
typepad_basename: "revitapido-not-add-or-delete-elements-when-iterating-document"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/44751251">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>A customer reported a problem that when loading family via API, and if the project file is an upgraded file rather than a newly created file, Revit will crash.</p>
<p>His code looks like this:</p>
<pre class="csharp prettyprint">UIApplication rvtApp = commandData.Application;
UIDocument rvtDoc = rvtApp.ActiveUIDocument;

FilteredElementCollector collector =
    new FilteredElementCollector(rvtDoc.Document)
    .OfClass(typeof(Family));
FilteredElementIterator itr =
    collector.GetElementIterator();
while (itr.MoveNext())
{
    Element elem = (Element)itr.Current;
    ReloadFamily(rvtApp, rvtDoc, elem);
}</pre>
<p><br /> First filter all Families, then look for corresponding .rfa file, if found, call Document.LoadFamily to load the .rfa file.</p>
<p>I can reproduce the problem, and tried to analyze what&#39;s wrong.</p>
<p>First print out the ids and names of the filtered families before calling ReloadFamily</p>
<pre class="csharp prettyprint">while (itr.MoveNext())
{
    Element elem = (Element)itr.Current;
    WriteLog(elem.Id + &quot;:&quot; + elem.Name);
    ReloadFamily(rvtApp, rvtDoc, elem);
}</pre>
<p>Looking at the output, I found some ids appeard twice and then Revit crashed.</p>
<p>That being said, the &quot;appear twice&quot; is the &quot;criminal&quot;.</p>
<p>I thought if there are duplicated elements in the collector which causes the &quot;appear twice&quot; problem. But I&#39;m wrong, there are no duplications after some examination.</p>
<p>So I tried &quot;foreach&quot; to replace &quot;Iterator&quot;,</p>
<pre class="csharp prettyprint">foreach (var elem in collector.ToElements())
{
    ReloadFamily(rvtApp, rvtDoc, elem);
}</pre>
<p>And miracle happened, no crash any more.</p>
<p>You must know that it is not &quot;foreach&quot; that turns the tables but the method ToElements() which returns a new list from collector.</p>
<p>So the reason is obvious: while we were iterating the collector, we did change to the Document, i.e. add some new elements, which caused the crash.</p>
<p>Let&#39;s see a similar case for .NET collection:</p>
<pre class="csharp prettyprint">List&lt;int&gt; ids = new List&lt;int&gt;() { 1, 2, 3, 4 };
foreach (int id in ids)
{
    ids.Add(5); //Exception!!!
}</pre>
<p>We all know that when we iterate ids list, and add items to it, exception will be thrown.</p>
<p>Same happens to Revit Document, when we iterate elements in Document, and in the meantime, adding or deleting elements will cause Revit unstable.</p>
<p>Conclusion: Do not add or delete elements when iterating elements in document, unless you iterate a copy of the element or element id set. </p>

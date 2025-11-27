---
layout: "post"
title: "How to defer updates in Inventor using VB/VBA?"
date: "2012-05-15 08:08:04"
author: "Gary Wassell"
categories:
  - "Gary Wassell"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/how-to-defer-updates-in-inventor-using-vbvba.html "
typepad_basename: "how-to-defer-updates-in-inventor-using-vbvba"
typepad_status: "Publish"
---

<p><strong>Issue</strong></p>
<p>I would like to open my drawing document in a way that it does not get updated, it opens the way it was last saved. How could I do it?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>Drawing documents have a property called &#39;Defer Updates&#39; which can be used to tell Inventor not to update the document when we open it.</p>
<p>If this property is set then Inventor will bring up a dialog warning us that the document will not get updated. <br />Therefore in both versions we need to use SilentOperation.</p>
<p>Following code works Inventor 10</p>
<p>You can either set the &#39;Defer Update&#39; property of the drawing using the following code:</p>
<pre><br />Dim oASC As New ApprenticeServerComponent<p>Dim oASD As ApprenticeServerDocument<br />Set oASD = oASC.Open(&quot;C:\Inventor\part.idw&quot;)<br />&#0160;<br /> it&#39;s in the &quot;Design Tracking Properties&quot;<br />oASD.PropertySets(&quot;{32853F0F-3444-11d1-9E93-0060B03C1CA6}&quot;).ItemByPropId(kDrawingDeferUpdateDesignTrackingProperties).Value = True</p>
<p>oASD.PropertySets.FlushToFile<br />oASC.Close</p></pre>
<p><br />and then you open it</p>
<pre><br />ThisApplication.SilentOperation = True<br />ThisApplication.Documents.Open (&quot;C:\Inventor\part.idw&quot;)<br />ThisApplication.SilentOperation = False<br /></pre>
<p>Following code works with Inventor 11 and higher versions.</p>
<p>Through API, &quot;Defer update&quot; property can be Set/Get with &quot;DrawingDocument.DrawingSettings.DeferUpdates&quot;</p>
<p>Through the UI, &quot;Defer update&quot; property is in &#39;Tools-&gt;Document Settings...-&gt;Drawing&#39; tab.</p>
<p>There is another way that we can get what we need through calling to &#39;OpenWithOptions&#39; function, which can be called with a NamedValueMap argument.</p>
<pre><br />Dim oNVM As NameValueMap<br />Set oNVM = ThisApplication.TransientObjects.CreateNameValueMap
<p>Call oNVM.Add(&quot;DeferUpdates&quot;, True)
</p><p>Dim oD As Document<br />ThisApplication.SilentOperation = True<br />Set oD = ThisApplication.Documents.OpenWithOptions(&quot;C:\Inventor\part.idw&quot;, oNVM)<br />ThisApplication.SilentOperation = False</p><p>&#0160;</p><p>Note: Above way is same workable with VB.NET. Define a global variable &quot;ThisApplication&quot; to</p><p>      delegate the Application object of Inventor, then copy below code to run:</p><p>&#0160;</p><p>Dim oNVM As NameValueMap = ThisApplication.TransientObjects.CreateNameValueMap
</p><p>oNVM.Add(&quot;DeferUpdates&quot;, True)
</p><p>ThisApplication.SilentOperation = True<br />Dim oD As Document = ThisApplication.Documents.OpenWithOptions(&quot;C:\Inventor\part.idw&quot;, oNVM)<br />ThisApplication.SilentOperation = False</p></pre>

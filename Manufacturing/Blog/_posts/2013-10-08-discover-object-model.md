---
layout: "post"
title: "Discover object model"
date: "2013-10-08 09:05:35"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html "
typepad_basename: "discover-object-model"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>One of the easiest ways to find out where to look for information inside the object you&#39;re interested in is to check it in <strong>VBA Watches</strong> window.</p>
<p>This article talks about the other options as well: <a href="http://modthemachine.typepad.com/my_weblog/2008/09/inventor-api-fundamentals-003---the-object-model.html" target="_self">http://modthemachine.typepad.com/my_weblog/2008/09/inventor-api-fundamentals-003---the-object-model.html</a></p>
<p>You just have to select the object in the user interface, then debug into a simple code like this and then you can hunt for the property you need:</p>
<pre>Sub TestConstraint()
    Dim obj As Object
    Set obj = ThisApplication.ActiveDocument.SelectSet(1)
End Sub</pre>
<p>If, for example, you want to get to the first document whose instance is involved in a constraint, just select the constraint in the user interface, and when debugging into the code you can find two ways to get to the PartDocument:<br />1) Constraint &gt; EntityOne &gt; ContainingOccurrence &gt; Definition &gt; Document
<br />2) Constraint &gt; AffectedOccurrenceOne &gt; Definition &gt; Document</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affdbfe0d970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="FlushConstraint" class="asset  asset-image at-xid-6a0167607c2431970b019affdbfe0d970d" src="/assets/image_b4fc31.jpg" style="width: 450px;" title="FlushConstraint" /></a></p>
<p>&#0160;</p>

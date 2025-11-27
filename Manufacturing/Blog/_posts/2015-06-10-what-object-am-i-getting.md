---
layout: "post"
title: "What object am I getting?"
date: "2015-06-10 15:44:19"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/what-object-am-i-getting.html "
typepad_basename: "what-object-am-i-getting"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The easiest way to check is by debugging into the code and see what object type the <strong>Watches</strong> window &#0160;shows.&#0160;</p>
<p>E.g. the following code ...</p>
<pre>Sub TestObjects()
  Dim f As Face
  Set f = ThisApplication.ActiveDocument.SelectSet(1)

  Dim paramRange As Box2d
  Set paramRange = f.Evaluator.ParamRangeRect
  
  Dim oc As ObjectCollection
  Set oc = f.Evaluator.GetIsoCurve(paramRange.MinPoint.X, True)
End Sub</pre>
<p>... would show this in the <strong>Watches</strong> window when I select the side face of a cylinder:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c79c6639970b-pi" style="display: inline;"><img alt="ObjectType" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c79c6639970b image-full img-responsive" src="/assets/image_ddbaa7.jpg" title="ObjectType" /></a></p>
<p>In case of <strong>VB</strong> and <strong>VB.NET</strong> you can also use the <strong>TypeName</strong> function to find out the type name of an object:</p>
<pre>Sub TestObjects()
  Dim f As Face
  Set f = ThisApplication.ActiveDocument.SelectSet(1)

  Dim paramRange As Box2d
  Set paramRange = f.Evaluator.ParamRangeRect
  
  Dim oc As ObjectCollection
  Set oc = f.Evaluator.GetIsoCurve(paramRange.MinPoint.X, True)
  
  Dim item As Object
  Set item = oc(1)
  
  Call MsgBox(TypeName(item))
End Sub</pre>

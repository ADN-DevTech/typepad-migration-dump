---
layout: "post"
title: "Accessing Dimension Line Weight"
date: "2018-12-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Dimensioning"
  - "Element Relationships"
  - "Parameters"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/12/accessing-dimension-line-weight.html "
typepad_basename: "accessing-dimension-line-weight"
typepad_status: "Publish"
---

<p>Today we share a quickie from 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on how to <a href="https://forums.autodesk.com/t5/revit-api-forum/access-line-weight-for-dimension-lines/m-p/8463046">access the line weight for dimension lines</a>:</p>

<p><strong>Question:</strong> I want to programmatically access a dimension line's line weight.</p>

<p>Here is a screenshot of what I am after:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad381d6a8200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad381d6a8200c image-full img-responsive" alt="Dimension line weight" title="Dimension line weight" src="/assets/image_954fb5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p style="font-size: 80%; font-style:italic">Dimension line weight</p>

<p></center></p>

<p><strong>Answer:</strong> Before you do anything else whatsoever, install <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a>.</p>

<p>It is an interactive Revit BIM database exploration tool to view and navigate element properties and relationships.</p>

<p>Use that to discover the relationships between the dimension lines, their styles, and the properties you are looking for.</p>

<p>Once you have that installed, you will quickly be able to determine that the dimension line weight is stored in a parameter value on the dimension type.</p>

<p>You can access it like this using the built-in parameter enumeration value <code>BuiltInParameter.LINE_PEN</code>:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ElementId</span>&nbsp;dimTypeId&nbsp;=&nbsp;dimension.GetTypeId();
&nbsp;&nbsp;<span style="color:#2b91af;">Element</span>&nbsp;dimType&nbsp;=&nbsp;document.GetElement(&nbsp;dimTypeId&nbsp;);

&nbsp;&nbsp;<span style="color:#2b91af;">Parameter</span>&nbsp;lineWeight&nbsp;=&nbsp;dimType.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.LINE_PEN&nbsp;);
</pre>

<p>Many thanks to <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4145125">Bardia Jahangiri</a> for the precise detailed answer.</p>

---
layout: "post"
title: "Edit Wall Length"
date: "2010-08-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Diving"
  - "External"
  - "Getting Started"
  - "Installation"
  - "Training"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/08/edit-wall-length.html "
typepad_basename: "edit-wall-length"
typepad_status: "Publish"
---

<p>I recently posted Augusto Gon&ccedil;alves 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html">
DevTV add-in templates</a>, 

which by the way have been updated based on some recent feedback.

Here are the updated versions and the directories to plug them in to:

<ul>
<li>
<span class="asset  asset-generic at-xid-6a00e553e1689788330133f2ec13d6970b"><a href="http://thebuildingcoder.typepad.com/files/templaterevitarchaddincs-1.zip">TemplateRevitArchAddinCS.zip</a></span>:

[My Documents]\Visual Studio 2008\Templates\ProjectTemplates\Visual C#

<li>
<span class="asset  asset-generic at-xid-6a00e553e1689788330134860f8e0e970c"><a href="http://thebuildingcoder.typepad.com/files/templaterevitarchaddinvb-1.zip">TemplateRevitArchAddinVB.zip</a></span>:

[My Documents]\Visual Studio 2008\Templates\ProjectTemplates\Visual Basic
</ul>

Augusto has already done quite a bit of other work on the Revit API as well, such as preparing the DevTV presentation for the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/12/updated-devtv-introduction-to-revit-programming.html">
Revit 2010 API</a>.

He will be giving his first 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/api-training-and-aec-devcamp-material.html">
Revit API training</a> in Brazil later this week.

<p>The best of luck to you with that, Augusto!

<p>He also recently stepped into the next phase of supporting the Revit API by answering several DevTech support cases in this area.
Here is one of his recent cases:

<p><strong>Question:</strong> I am working on an add-in that creates window families through the Revit API. 
These families sometimes consist of a single window unit, but also may be combinations of windows, in which case the add-in creates nested window families and inserts instances of the nested families into the combination family. 

<p>I want to be able to increase the length and/or height of the wall in the family document, because the window combinations can be quite large in dimensions. 
I am able to increase the height of the wall using the "Unconnected Height" parameter. 
However, the "Length" parameter of the wall is read-only, and my code gets an InvalidOperationException if Set is called on the "Length" parameter. 

<p>Is there some other way to increase the length of this wall? 

<p><strong>Answer:</strong> You are right, the Length is read-only.
You can use the LocationCurve.Curve instead, which is write enabled. 
The following code snippet shows how to move the point and update the location:

<pre class="code">
&nbsp; <span class="green">// get the current wall location</span>
&nbsp;
&nbsp; <span class="teal">LocationCurve</span> wallLocation = myWall.Location 
&nbsp; &nbsp; <span class="blue">as</span> <span class="teal">LocationCurve</span>;
&nbsp;
&nbsp; <span class="green">// get the points</span>
&nbsp; <span class="teal">XYZ</span> pt1 = wallLocation.Curve.get_EndPoint( 0 );
&nbsp; <span class="teal">XYZ</span> pt2 = wallLocation.Curve.get_EndPoint( 1 );
&nbsp;
&nbsp; <span class="green">// change one point, e.g. move 1000 mm on X axis</span>
&nbsp;
&nbsp; pt2 = pt2.Add( <span class="blue">new</span> XYZ( mmToFeet( 1000 ), 0, 0 ) );
&nbsp;
&nbsp; <span class="green">// create a new LineBound</span>
&nbsp; <span class="teal">Line</span> newWallLine = app.Create.NewLineBound( 
&nbsp; &nbsp; pt1, pt2 );
&nbsp;
&nbsp; <span class="green">// update the wall curve</span>
&nbsp; wallLocation.Curve = newWallLine;
</pre>

<h4>More From Ko Tao</h4>

<p>I am still on Ko Tao. 
You may be surprised to hear that one of the reasons for me to come here was to practice Italian.
My friend Daniela invited me to join her here for a diving holiday.
I took a 

<a href="http://www.cmas.org">
CMAS</a> diving exam 

in swimming pools with a final test in the Zurich Lake several years ago, and never ever made any use of it by going diving in natural waters, and I also wanted to continue practicing Italian, so it sounded like a perfect idea.
Once here, we even met her friend Maurizio, another diver, more or less by coincidence, so now the three of us often hang out.
Here are Daniela and Maurizio in Hin Wong Bay, where we went for a very nice snorkelling tour:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330134860f957c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330134860f957c970c image-full" alt="Maurizio and Daniela in Hin Wong Bay" title="Maurizio and Daniela in Hin Wong Bay" src="/assets/image_c83e8b.jpg" border="0"  /></a> <br />

</center>

<p>In that bay I had my first very impressive experience of swimming in the middle of a huge school of tiny fish, maybe a couple of hundred thousand of them, several cubic metres, swirling around me in all directions, like a single living mass.

<p>And here is Daniela watching some chickens in front of the 

<a href="http://www.divingcourseskohtao.com">
Alvaro Diving</a> office 

in Alok Baan Kao Bay with the Buddha Rock in the background:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f2ec10cc970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f2ec10cc970b image-full" alt="Daniela with chickens and the Buddha Rock" title="Daniela with chickens and the Buddha Rock" src="/assets/image_8323f9.jpg" border="0"  /></a> <br />

</center>

<p>Finally, here is a sweet little hut on a boat right next to the hut we were staying in until yesterday:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f2ec1fcf970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f2ec1fcf970b image-full" alt="Hut on a boat" title="Hut on a boat" src="/assets/image_fac7b7.jpg" border="0"  /></a> <br />

</center>

<p>Now we moved to a more tranquil spot at Jul Julea Beach, away from noise and music and bars and people.

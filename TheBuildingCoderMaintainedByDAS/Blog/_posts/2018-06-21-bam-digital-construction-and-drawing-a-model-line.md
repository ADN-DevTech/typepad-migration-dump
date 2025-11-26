---
layout: "post"
title: "BAM Digital Construction and Drawing a Model Line"
date: "2018-06-21 06:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "BIM"
  - "Climbing"
  - "Cloud"
  - "Data Access"
  - "Element Creation"
  - "Events"
  - "Forge"
  - "Getting Started"
  - "Insight360"
  - "Mobile"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/06/bam-digital-construction-and-drawing-a-model-line.html "
typepad_basename: "bam-digital-construction-and-drawing-a-model-line"
typepad_status: "Publish"
---

<p>I am attending the BAM <i>Digital Construction Live</i> event in the UK and presenting on Forge for that domain.
You can see what's going on here looking for
the <a href="https://twitter.com/search?q=%23bamdigital">#bamdigital</a> hashtag.</p>

<p>On the way here, I visited my brother and passed by the interesting climbing areas
at <a href="https://en.wikipedia.org/wiki/Cheddar_Gorge">Cheddar Gorge</a>
and <a href="https://en.wikipedia.org/wiki/Symonds_Yat">Symonds Yat</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad354f77c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad354f77c200c img-responsive" style="width: 200px; display: block; margin-left: auto; margin-right: auto;" alt="Cheddar Gorge" title="Cheddar Gorge" src="/assets/image_ad1b38.jpg" /></a><br /></p>

<p></center></p>

<p>In the latter, we climbed the Long Rock Pinnacle via Whitt:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad39acc5e200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad39acc5e200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Long Rock Pinnacle" title="Long Rock Pinnacle" src="/assets/image_4f2be7.jpg" /></a><br /></p>

<p></center></p>

<p>Today, I'll share my slide deck from this event and welcome my colleague Xiaodong answering his first Revit API cases:</p>

<ul>
<li><a href="#2">Forge for Digital Construction</a> </li>
<li><a href="#3">Welcome Xiaodong and invoking the <em>Draw Model Line</em> command</a> </li>
</ul>

<h4><a name="2"></a> Forge for Digital Construction</h4>

<p>As said, I am attending this event with my Autodesk colleagues Az Jasat and Manu Venugopal, presenting on Insight and Connection.</p>

<p>Insight is covered by Manu, discussing Project IQ and machine learning enhanced analytics for automated risk assessment.</p>

<p>I am presenting on Forge for the digital construction process and connecting to the BIM360 products.</p>

<p>I already explained the main concepts from my point of view in 
the <a href="http://thebuildingcoder.typepad.com/blog/2018/06/forge-for-aec-and-bim360-overview.html">overview of Forge for AEC and BIM360</a> and
the <a href="http://thebuildingcoder.typepad.com/blog/2018/06/bim360-and-forge-for-aec-real-message-and-samples.html">BIM360 and Forge for AEC message and samples</a>.</p>

<p>Here is the final slide deck summarising those points in
the
<a href="http://thebuildingcoder.typepad.com/files/bam_bim360_forge_aec_slides.pdf">BAM Forge for Digital Construction slides</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad37aeb7e200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad37aeb7e200d image-full img-responsive" alt="Manu and Az at BAM Digital Construction Live" title="Manu and Az at BAM Digital Construction Live" src="/assets/image_275272.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Welcome Xiaodong and Invoking the <em>Draw Model Line</em> Command</h4>

<p>Many congratulations to my colleague Xiaodong Liang for diving into the Revit API and starting to answer cases!</p>

<p>Here is one of his first:</p>

<p><strong>Question:</strong> How can I programmatically invoke the <em>Draw Model Line</em> command?</p>

<p><strong>Answer:</strong> If your workflow is to simply to invoke the built-in <em>Draw Model Line</em> command as is, you could find the command id and execute it using <code>PostCommand</code>:</p>

<pre class="code">
&nbsp;&nbsp;Autodesk.Revit.UI.<span style="color:#2b91af;">RevitCommandId</span>&nbsp;cmd_id
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:#2b91af;">RevitCommandId</span>.LookupPostableCommandId(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PostableCommand</span>.ModelLine&nbsp;);

&nbsp;&nbsp;uiapp.PostCommand(&nbsp;cmd_id&nbsp;);
</pre>

<p>If your workflow is to create a model line yourself with the parameters the user inputs, you can use the following:</p>

<pre class="code">
  <span style="color:#2b91af;">UIApplication</span>&nbsp;uiApp&nbsp;=&nbsp;commandData.Application;
  <span style="color:#2b91af;">Application</span>&nbsp;rvtApp&nbsp;=&nbsp;uiApp.Application;
  <span style="color:#2b91af;">UIDocument</span>&nbsp;uiDoc&nbsp;=&nbsp;uiApp.ActiveUIDocument;
  <span style="color:#2b91af;">Document</span>&nbsp;doc&nbsp;=&nbsp;uiDoc.Document;

  <span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;transaction&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
  {
  &nbsp;&nbsp;transaction.Start(&nbsp;<span style="color:#a31515;">&quot;Create&nbsp;Model&nbsp;Line&nbsp;By&nbsp;Me&quot;</span>&nbsp;);

  &nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;startPoint&nbsp;=&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero;
  &nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;endPoint&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">XYZ</span>(&nbsp;10,&nbsp;10,&nbsp;0&nbsp;);

  &nbsp;&nbsp;<span style="color:#2b91af;">Line</span>&nbsp;geomLine&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;startPoint,&nbsp;endPoint&nbsp;);

  &nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;geometry&nbsp;plane&nbsp;in&nbsp;Revit&nbsp;application&nbsp;memory</span>

  &nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;origin&nbsp;=&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero;
  &nbsp;&nbsp;<span style="color:#2b91af;">XYZ</span>&nbsp;normal&nbsp;=&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisZ;
  &nbsp;&nbsp;<span style="color:#2b91af;">Plane</span>&nbsp;geomPlane&nbsp;=&nbsp;<span style="color:#2b91af;">Plane</span>.CreateByNormalAndOrigin(
  &nbsp;&nbsp;&nbsp;&nbsp;normal,&nbsp;origin&nbsp;);

  &nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;a&nbsp;sketch&nbsp;plane&nbsp;in&nbsp;current&nbsp;document</span>

  &nbsp;&nbsp;<span style="color:#2b91af;">SketchPlane</span>&nbsp;sketch&nbsp;=&nbsp;<span style="color:#2b91af;">SketchPlane</span>.Create(
  &nbsp;&nbsp;&nbsp;&nbsp;doc,&nbsp;geomPlane&nbsp;);

  &nbsp;&nbsp;<span style="color:#2b91af;">ModelLine</span>&nbsp;modelLine&nbsp;=&nbsp;doc.Create.NewModelCurve(
  &nbsp;&nbsp;&nbsp;&nbsp;geomLine,&nbsp;sketch&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">ModelLine</span>;

  &nbsp;&nbsp;transaction.Commit();
  }
</pre>

<p>Xiaodong adds:</p>

<blockquote>
  <p>Though it is a simple question, it took me some time to test out the working codes, and I learned some valuable knowledge of Revit API.</p>
  
  <p>Stumblingly at the starting point of the journey into the Revit API &nbsp; :-)</p>
</blockquote>

<p>Many thanks to Xiaodong for the nice and comprehensive answer!</p>

<p>It looks like a great start!</p>

<p>I wish you much success and lots of fun going further.</p>

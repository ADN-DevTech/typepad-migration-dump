---
layout: "post"
title: "Creating a Rectangular RevCloud using acedCommandS"
date: "2015-10-08 04:37:07"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/10/creating-a-rectangular-revcloud-using-acedcommands.html "
typepad_basename: "creating-a-rectangular-revcloud-using-acedcommands"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When running the _RevCloud command from a script or using acedCommandS, the options presented in AutoCAD's command window did not match the options that one gets when running _RevCloud command in AutoCAD UI. The option to set the Revcloud type was not appearing when invoked from acedCommandS.</p>
<p>Here are screenshots of the options to show how they differed :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087e9599970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb087e9599970d img-responsive" style="margin: 0px 5px 5px 0px;" title="ThroughScript" src="/assets/image_286131.jpg" alt="ThroughScript" /></a></p>
<p><a class="asset-img-link"  href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d164860b970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d164860b970c img-responsive" style="margin: 0px 5px 5px 0px;" title="ThroughUI" src="/assets/image_644002.jpg" alt="ThroughUI" /></a></p>
<p></p>
<p>The following reason for this behavior was provided by Markus Kraus from our AutoCAD engineering team :
<blockquote>
<em>When driven from apps or from script, most AutoCAD commands default to their first command version (the version where the command has been introduced the first time in AutoCAD)</em>
</blockquote>
 </p>
<p>
Here is a small code snippet that set the command version to get this working : 
</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Adesk::UInt8 cmdVerNew = 2;</pre>
<pre style="margin:0em;"> Adesk::UInt8 cmdVerOld </pre>
<pre style="margin:0em;"> 	= acedInitCommandVersion(cmdVerNew);</pre>
<pre style="margin:0em;"> AcGePoint2d ll(0.0, 0.0); </pre>
<pre style="margin:0em;"> AcGePoint2d ur(4.0, 5.0);</pre>
<pre style="margin:0em;"> acedCommandS(RTSTR, _T(<span style="color:#a31515">&quot;_.revcloud&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 				RTSTR, _T(<span style="color:#a31515">&quot;_R&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 				RTSTR, _T(<span style="color:#a31515">&quot;_A&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 				RTREAL, 0.25, RTREAL, 0.75, </pre>
<pre style="margin:0em;"> 				RTPOINT, ll, RTPOINT, ur, RTNONE);</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>
Such the command versioning can also be implemented for custom commands that you create. To do this, please refer to the ObjectARX documentation on "acedGetCommandVersion". Based on the command version, a custom command can vary in its functioning.
</p>

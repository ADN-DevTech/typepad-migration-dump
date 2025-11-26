---
layout: "post"
title: "Is this COGO Point a Survey Point or Project Point?"
date: "2012-06-22 01:22:46"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/is-this-cogo-point-a-survey-point-or-project-point.html "
typepad_basename: "is-this-cogo-point-a-survey-point-or-project-point"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>When you select a COGO Point in Civil 3D Model space and look into the Properties dialog, under &quot;Data&quot; node, there is &quot;<strong>Survey Point</strong>&quot; property and with a value True or False. If the COGO Point is part of Survey Database, it will be shown as &#39;True&#39; -</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016767ba063b970b-pi" style="display: inline;"><img alt="COGO_02" class="asset  asset-image at-xid-6a0167607c2431970b016767ba063b970b" src="/assets/image_50bbad.jpg" title="COGO_02" /></a></p>
<p>&#0160;</p>
<p>Similarly, if the COGO Point is from Vault Project Points, You would see associated &#39;Project version&#39; in OPM window -</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017615af739d970c-pi" style="display: inline;"><img alt="COGO_03" class="asset  asset-image at-xid-6a0167607c2431970b017615af739d970c" src="/assets/image_ad0583.jpg" title="COGO_03" /></a></p>
<p>We can use the following CogoPoint properties to find out if it is Survey Point, project point or normal COGO point object created in the Civil 3D drawing file.&#0160;</p>
<p><strong>CogoPoint.IsSurveyPoint</strong> property indicates whether this Cogo Point is a <span style="text-decoration: underline;">Survey Point</span>.</p>
<p><strong>CogoPoint.IsProjectPoint</strong> property indicates whether the CogoPoint is a <span style="text-decoration: underline;">project point</span>.</p>
<p>&#0160;</p>
<p>Here is the relevant C# code snippet :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//open the COGO Point </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;"> cogoPoint = trans.GetObject(per.ObjectId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Access COGO Point Properties</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// CogoPoint.IsSurveyPoint property indicates whether this Cogo Point is a Survey Point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cogoPoint.IsSurveyPoint)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// DO your stuff</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelected COGO Point is a Survey Point&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//&#0160; CogoPoint.IsProjectPoint property indicates whether the CogoPoint is a project point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//&#0160; i.e. if the COGO point is from Vault Project Points</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cogoPoint.IsProjectPoint)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// DO your stuff</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelected COGO Point is a Project Point&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// normal COGO point object created in the Civil 3D drawing file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// DO your stuff</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelected COGO Point is neither Survey nor Project Point&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; } </span></p>
</div>
<p>Hope this is useful to you!</p>

---
layout: "post"
title: "How Green and Sustainable is my Civil 3D Infrastructure project?"
date: "2013-04-01 02:30:00"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/how-green-and-sustainable-is-my-civil-3d-infrastructure-project.html "
typepad_basename: "how-green-and-sustainable-is-my-civil-3d-infrastructure-project"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This new
release of CIVIL 3D has a brand new API to evaluate how green and sustainable
is the new project which you are designing in Civil 3D. This Feature is
available only through .NET API. How do we use this API to evaluate my current
project ? </p>
<p>Here is a c#
code snippet -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> cDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">GreenEvaluate</span><span style="line-height: 140%;"> checkGreenEvl = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">GreenEvaluate</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Call GreenEvaluatation to compute the Green factor and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Sustainablility factor for the current CivilDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; checkGreenEvl.GreenEvaluatation(cDoc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Check the Green factor and Sustainability factor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nIs this Project Green ? :&quot;</span><span style="line-height: 140%;"> + checkGreenEvl.GreenFactor.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nIs this Project Sustainable&#0160; :&quot;</span><span style="line-height: 140%;"> + checkGreenEvl.SustainableFactor.ToString());&#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nException: &quot;</span><span style="line-height: 140%;"> + ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }&#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>
If you have any
questions on this particular API, let&#39;s come back on the <strong>same day</strong>&#0160;&#39;next year&#39; to
discuss about this!</p>
<p>Have a nice day! :)</p>

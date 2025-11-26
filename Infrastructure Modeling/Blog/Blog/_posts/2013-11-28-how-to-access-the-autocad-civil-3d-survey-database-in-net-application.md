---
layout: "post"
title: "How to access the AutoCAD Civil 3D Survey Database in .NET Application?"
date: "2013-11-28 03:16:09"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/how-to-access-the-autocad-civil-3d-survey-database-in-net-application.html "
typepad_basename: "how-to-access-the-autocad-civil-3d-survey-database-in-net-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D COM API, using <strong>AeccSurveyDatabase:: Projects</strong>, we can access the collection of the existing Survey projects. If you want to access a particular Survey Project e.g. (Test_Survey_DB) as shown in the screenshot below, we can use the <strong>AeccSurveyProjects:: FindItem(</strong><em>VARIANT varIndex</em>).</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01c7d462970d-pi" style="display: inline;"><img alt="Civil3D_Survey_Database" class="asset  asset-image at-xid-6a0167607c2431970b019b01c7d462970d" src="/assets/image_9ac3df.jpg" title="Civil3D_Survey_Database" /></a></p>
<p>&#0160;</p>
<p>Here is a C# .NET code snippet which demonstrates how to access a particular Survey Project using the COM API &#0160;:&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the AutoCAD Editor</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the Survey Application and AeccSurveyDatabase</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccSurveyApplication</span><span style="line-height: 140%;"> aeccSurveyApp = </span><span style="color: blue; line-height: 140%;">new</span><span style="color: #2b91af; line-height: 140%;">AeccSurveyApplication</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">aeccSurveyApp.Init((</span><span style="color: #2b91af; line-height: 140%;">AcadApplication</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.AcadApplication);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccSurveyDatabase</span><span style="line-height: 140%;"> aeccSurveydb = (</span><span style="color: #2b91af; line-height: 140%;">AeccSurveyDatabase</span><span style="line-height: 140%;">)aeccSurveyApp.ActiveDocument.Database;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AeccSurveyProjects</span><span style="line-height: 140%;"> surveyProjs = aeccSurveydb.Projects;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Accessing a Particular Survey Project</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// IAeccSurveyProjects:: FindItem </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Gets the Survey Project with the given name. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">AeccSurveyProject</span><span style="line-height: 140%;"> surveyProj = surveyProjs.FindItem(</span><span style="color: #a31515; line-height: 140%;">&quot;Test_Survey_DB&quot;</span><span style="line-height: 140%;">);</span></strong></span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (surveyProj != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// now we can access the various Props of AeccSurveyProject object&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSurvey Project Name :&#0160; &quot;</span><span style="line-height: 140%;"> + surveyProj.Name.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSurvey Project Path :&#0160; &quot;</span><span style="line-height: 140%;"> + surveyProj.Path.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nNext available point number&#0160;&#0160; : &quot;</span><span style="line-height: 140%;"> + surveyProj.GetNextWritablePointNumber().ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01c76bb3970b-pi" style="display: inline;"><img alt="Civil3D_Survey_Database_Result" class="asset  asset-image at-xid-6a0167607c2431970b019b01c76bb3970b" src="/assets/image_0aced7.jpg" title="Civil3D_Survey_Database_Result" /></a></span></p>
<p style="margin: 0px;">&#0160;</p>
</div>

---
layout: "post"
title: "Setting the Working Folder for Civil 3D Survey Projects using .NET API"
date: "2014-02-24 20:40:53"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/02/setting-the-working-folder-for-civil-3d-survey-projects-using-net-api.html "
typepad_basename: "setting-the-working-folder-for-civil-3d-survey-projects-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Using AutoCAD Civil 3D UI tools we can conveniently set the working folder for Survey Projects.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d808c96970d-pi" style="display: inline;"><img alt="Civil3D_Survey_SetWorkingFolder" class="asset  asset-image at-xid-6a0167607c2431970b01a73d808c96970d img-responsive" src="/assets/image_224aa4.jpg" title="Civil3D_Survey_SetWorkingFolder" /></a><br />&#0160;</p>
<p>&#0160;</p>
<p>If you want to do the same using Civil 3D .NET API, you need to use <strong>SurveyProjectCollection.WorkingFolder</strong> API. Using this API we can <em><strong>get</strong> </em>or <em><strong>set</strong></em> the working folder for survey projects.</p>
<p>&#0160;</p>
<p>Here is a C# .NET code snippet on how to use this API :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SurveyProjectCollection</span><span style="line-height: 140%;"> surveyProjColl = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.SurveyProjects;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// print the current working folder name</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSurvey Projects Working Folder Before Change : &quot;</span><span style="line-height: 140%;"> + surveyProjColl.WorkingFolder.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now change the working folder location</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="line-height: 140%;">surveyProjColl.WorkingFolder = </span><span style="color: #a31515; line-height: 140%;">&quot;c:\\Temp&quot;</span><span style="line-height: 140%;">;</span></strong></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// print the current working folder name after change </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSurvey Projects Working Folder After Change : &quot;</span><span style="line-height: 140%;"> + surveyProjColl.WorkingFolder.ToString());</span></p>
</div>

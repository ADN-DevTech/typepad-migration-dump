---
layout: "post"
title: "Accessing Survey Database Points using AutoCAD Civil 3D COM API"
date: "2013-08-07 00:11:31"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/08/accessing-survey-database-points-using-autocad-civil-3d-com-api.html "
typepad_basename: "accessing-survey-database-points-using-autocad-civil-3d-com-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Using
<strong>IAeccSurveyProject:: GetPointByNumber</strong> API we can access a Survey Point instance
with the input point number. Once we get the specific <strong>AeccSurveyPoint</strong> object we
can access its various properties and methods. Most of the properties in COM
API are read only i.e. we can get the value, but can set them using API except
few e.g. -</p>
<p><strong>IAeccSurveyPoint::
Name</strong> Property - &gt; Gets or sets the name of the Point.</p>
<p>Here is a
VB.NET sample code which demonstrates how to access Survey Database Points
using COM API in Civil 3D 2014 :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> DemoAccessSurveyPoints()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; This sample Demonstrates How to access Survey Database Points using COM API in Civil 3D 2014</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; Created by Partha Sarkar - DevTech, Autodesk</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ed </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> = Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAcadApp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Interop.</span><span style="color: #2b91af; line-height: 140%;">AcadApplication</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAeccSurveyApp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AECC.Interop.UiSurvey.</span><span style="color: #2b91af; line-height: 140%;">AeccSurveyApplication</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAeccSurveyDoc </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AECC.Interop.UiSurvey.</span><span style="color: #2b91af; line-height: 140%;">AeccSurveyDocument</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAeccSurveyDB </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AECC.Interop.Survey.</span><span style="color: #2b91af; line-height: 140%;">AeccSurveyDatabase</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> trans </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> = </span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase.TransactionManager.StartTransaction()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> oAcadApp </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; oAcadApp = GetObject(, </span><span style="color: #a31515; line-height: 140%;">&quot;AutoCAD.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(ex.Message)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; If you are using the COM API in Civil 3D 2014</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; you need to update the object version to 10.3 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; http://adndevblog.typepad.com/infrastructure/2013/03/whats-new-in-autocad-civil-3d-2014-api.html</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; oAeccSurveyApp = oAcadApp.GetInterfaceObject(</span><span style="color: #a31515; line-height: 140%;">&quot;AeccXUiSurvey.AeccSurveyApplication.10.3&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; oAeccSurveyDoc = oAeccSurveyApp.ActiveDocument</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; oAeccSurveyDB = oAeccSurveyApp.ActiveDocument.Database</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oSurveyProjects </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccSurveyProjects</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">CType</span><span style="line-height: 140%;">(oAeccSurveyDB.Projects, </span><span style="color: #2b91af; line-height: 140%;">AeccSurveyProjects</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; get the 1st Project from the Survey Projects collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="background-color: #ffff00;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oSurveyProject </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccSurveyProject</span><span style="line-height: 140%;"> = oSurveyProjects.Item(0)</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Not</span><span style="line-height: 140%;"> (oSurveyProject </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;">) </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Get the Project Name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Projcet Name : &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot; &quot;</span><span style="line-height: 140%;"> + oSurveyProject.Name.ToString())</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; IAeccSurveyProject:: GetPointByNumber </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Gets the Survey Point instance with the input point number.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="background-color: #ffff00;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oSurveyPoint </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccSurveyPoint</span><span style="line-height: 140%;"> = oSurveyProject.GetPointByNumber(1)</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Check the Point Properties Name, Easting, Northing &amp; Elevation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Point Name : &quot;</span><span style="line-height: 140%;"> + oSurveyPoint.Name.ToString() )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Easting : &quot;</span><span style="line-height: 140%;"> + oSurveyPoint.Easting.ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot; &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Northing : &quot;</span><span style="line-height: 140%;"> + oSurveyPoint.Northing.ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot; &quot;</span><span style="line-height: 140%;"> + </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;Elevation : &quot;</span><span style="line-height: 140%;"> + oSurveyPoint.Elevation.ToString())</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;">&#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; trans.Commit()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Error : &quot;</span><span style="line-height: 140%;">, ex.Message &amp; vbCrLf)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
<p>&#0160;</p>
<p>Here is a
screenshot of the result from this code snippet :</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac68c09b970d-pi" style="display: inline;"><img alt="AutoCAD_Civil3D_Survey_Points_Access" class="asset  asset-image at-xid-6a0167607c2431970b0192ac68c09b970d" src="/assets/image_07c52f.jpg" title="AutoCAD_Civil3D_Survey_Points_Access" /></a><br /><br /></p>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>

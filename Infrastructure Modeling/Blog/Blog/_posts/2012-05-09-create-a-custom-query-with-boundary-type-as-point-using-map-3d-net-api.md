---
layout: "post"
title: "Create a custom Query with Boundary Type as 'Point' using Map 3D .NET API"
date: "2012-05-09 06:31:39"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/create-a-custom-query-with-boundary-type-as-point-using-map-3d-net-api.html "
typepad_basename: "create-a-custom-query-with-boundary-type-as-point-using-map-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Presume you have some DWG files attached to your project in AutoCAD Map 3D and you want to run a Query with Boundary Type as &#39;Point&#39;. What you do in Map 3D?&#0160;</p>
<p>You bring up the &#39;Define Query of Attached Drawing(s)&#39; dialog box and select the &#39;Location&#39; button. In &#39;Location Condition&#39; dialog box, select &#39;Point&#39; (see the screenshot below) and define the location and finally execute the Query. You get to see the desired result in Map 3D Model space.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676658705d970b-pi" style="display: inline;"><img alt="IMG1" class="asset  asset-image at-xid-6a0167607c2431970b01676658705d970b" src="/assets/image_9c1a71.jpg" title="IMG1" /></a></p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676658711f970b-pi" style="display: inline;"><img alt="IMG2" class="asset  asset-image at-xid-6a0167607c2431970b01676658711f970b" src="/assets/image_c2ae60.jpg" title="IMG2" /></a><br /><br /></p>
<p>In a complicated project you don&#39;t want user to select the Point location arbitrarily and you simply want them to run a predefined custom command to execute the query with that particular point location. Here is a VB.NET code snippet which uses Map 3D managed API to run a custom query:</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; Create the query using ProjectModel.CreateQuery().</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> dwg <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span> = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> database <span style="color: blue;">As</span> <span style="color: #2b91af;">Database</span> = dwg.Database</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> PROJECT <span style="color: blue;">As</span> Project.<span style="color: #2b91af;">ProjectModel</span> = <span style="color: #2b91af;">HostMapApplicationServices</span>.Application.ActiveProject</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> qryModel <span style="color: blue;">As</span> <span style="color: #2b91af;">QueryModel</span> = PROJECT.CreateQuery()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; Clear the exisitng Query</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qryModel.Clear()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; Create one or more query conditions</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> locationCondition <span style="color: blue;">As</span> Query.<span style="color: #2b91af;">LocationCondition</span> = <span style="color: blue;">New</span> Query.<span style="color: #2b91af;">LocationCondition</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> qryCondition <span style="color: blue;">As</span> <span style="color: #2b91af;">LocationCondition</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">LocationCondition</span>()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; This point location is specific to a test DWG file</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pt3d <span style="color: blue;">As</span> <span style="color: #2b91af;">Point3d</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">Point3d</span>(3080168.7995, 1271284.8294, 0.0)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pntBdry <span style="color: blue;">As</span> <span style="color: #2b91af;">PointBoundary</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">PointBoundary</span>(pt3d)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qryCondition.Boundary = pntBdry</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> qryBranch <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">QueryBranch</span>(<span style="color: #2b91af;">JoinOperator</span>.OperatorAnd)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qryBranch.AppendOperand(qryCondition)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39;Create the query definition by passing the root query branch to QueryModel.Define().</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qryModel.Define(qryBranch)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qryModel.Mode = <span style="color: #2b91af;">QueryType</span>.QueryDraw</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; qryModel.Run()</p>
</div>
<p><br /><br />Hope this is useful to you!</p>

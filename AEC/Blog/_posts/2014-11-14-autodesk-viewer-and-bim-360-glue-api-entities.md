---
layout: "post"
title: "Autodesk Viewer and BIM 360 Glue API Entities"
date: "2014-11-14 14:20:54"
author: "Jaime Rosales"
categories:
  - "AutoCAD Architecture"
  - "AutoCAD MEP"
  - "Cloud"
  - "DevTV"
  - "Jaime Rosales"
original_url: "https://adndevblog.typepad.com/aec/2014/11/autodesk-viewer-and-bim-360-glue-api-entities.html "
typepad_basename: "autodesk-viewer-and-bim-360-glue-api-entities"
typepad_status: "Publish"
---

<p>This week I got to play a bit with the new Autodesk View &amp; Data API (available in Beta from http://developer.autodesk.com). Many other functions can be used when in full screen mode, like exploding the model and being able to see all parts that conform the model (this happens only if the model format allows it). Many of our other blogs are talking about this exciting new technology and since we are AEC here is a little house for you guys to play with.&#0160;</p>
<p>Also you will need a WebGL-enabled browser. You can use this&#0160;<a href="http://doesmybrowsersupportwebgl.com/" target="_blank">site</a>&#0160;to test.</p>
<p>Here is a link to download the recording of our recent webcast explaining how to get started.</p>
<p><a href="http://adndevblog.typepad.com/cloud_and_mobile/Autodesk%20View%20and%20Data%20API%2010-23-14%208.05%20AM.mov">Download Autodesk View and Data API 10-23-14 8.05 AM.mov (129231.5K)</a></p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="480" mozallowfullscreen="" src="http://viewer.autodesk.io/node/gallery/embed/54464d43af600b5c0a87254c" webkitallowfullscreen="" width="480"> </iframe></p>
<p>The current loaded model is NWD file which contains information on the selected object such as the Entity Handle Value, which brings me to the next topic for this week.&#0160;</p>
<p>This week I had a question from a developer regarding the use of Entities and GUID in AutoCAD, BIM 360 Glue and Navisworks. Here is the question.&#0160;</p>
<p><strong>Question:&#0160;</strong>When I looked at the entity properties in Navisworks and BIM 360 Glue I can see a GUID for each entity. Does the GUID assignment happen when the entity is created in AutoCAD or by the Navisworks viewer?&#0160;</p>
<p>What about when the model is published to Glue from AutoCAD via the 360 Glue plugin?<br /> Does the Glue plugin use the Navisworks exporter? Also, is the GUID stored on the entity in the drawing?</p>
<p><strong>Answer</strong>: Thank you for your questions here are a couple of answers to that.</p>
<p><strong>-What about when the model is published to Glue from AutoCAD via the 360 Glue plugin?</strong></p>
<p>What BIM 360 Glue uses when a model gets glued from AutoCAD is the entity handle that AutoCAD has and this gets transferred with the DWG during the Gluing process.</p>
<p><strong>-Does the Glue plugin use the Navisworks exporter?&#0160;</strong></p>
<p>Glue does not use the Navisworks exporter, Glue is able to open the DWG model due to the Navisworks Viewer.&#0160;</p>
<p><strong>-Also, is the GUID stored on the entity in the drawing?</strong></p>
<p>So when you see the model you sent from AutoCAD to Glue, you can see the entity handle displayed at each element followed by the Element ID. When you have a model that goes from either AutoCAD or Glue to Navisworks the exporter assigns the GUID you are seeing on Navisworks.&#0160;</p>
<p>AutoCAD to Glue will not have a GUID is an Entity Handle what is using. But when you send the DWG from Glue to Navisworks the GUID will get added using the Navisworks exporter to be displayed in Navisworks. Hope it makes sense.&#0160;</p>
<p><strong>Response</strong>:&#0160;Thanks for the answers. Now I have a few more questions.<br /> I want to implement my own GUID using the entity extension dictionary, is the dictionary accessible through the Glue API? How about AutoCAD Groups?</p>
<p><strong>Answer</strong>:&#0160;Currently the Glue API does not allow such access to either AutoCAD Groups or Entity Extension Dictionary, but bare with me, there&#0160;is a highly demanded feature and there is already a wish logged&#0160;to enhance the API to something that might help you with your desire workflow. The enhancement wished for is to add ability to get object properties from models in the API, if your dictionary is included in the properties you will be able to query it.</p>
<p><a href="http://www.autodesk.com/products/bim-360-glue/overview">BIM 360 Glue</a> is a very exciting software to use, I have a personal attachment to it since I come from the initial steps of creation of this technology, and the API is getting bigger and bigger everyday. If I had started your curiosity regarding this product don&#39;t wait anymore and go check it out.&#0160;</p>
<p>Thank you for reading and until next time.</p>

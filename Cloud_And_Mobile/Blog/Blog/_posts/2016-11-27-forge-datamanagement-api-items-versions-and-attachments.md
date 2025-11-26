---
layout: "post"
title: "Forge Data Management API: items, versions and attachments"
date: "2016-11-27 23:47:01"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "Data Management"
  - "NodeJS"
  - "Philippe Leefsma"
  - "Storage"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/11/forge-datamanagement-api-items-versions-and-attachments.html "
typepad_basename: "forge-datamanagement-api-items-versions-and-attachments"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" rel="noopener noreferrer" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" rel="noopener noreferrer" target="_blank">(@F3lipek)</a></span></p>
<p>&nbsp; &nbsp; This post shows the part of my server application that deals with items, versions and attachments. You can find the detailed curl commands that you need to send exposed by the step-by-step section of the API documentation: <a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener noreferrer" target="_blank">upload a file</a>, however I thought it could be useful to take a look at a concrete implementation in my node.js &nbsp;sample.&nbsp;</p>
<p>&nbsp; &nbsp; The first thing to understand is that the Data Management API is composed by several services, the lowest level one being Object Storage Service (OSS). This service is exposed for 2-legged and 3-legged OAuth applications but with some differences: for 2-legged your app gets access to private buckets and objects that you are creating at your will. For 3-legged, the Data Service still relies on OSS, however your app needs to let that service decide which bucket and object keys to use when it needs to upload a new file or create a new version of an existing item.</p>
<p>&nbsp; &nbsp; The diagram below summarises the situation, the "OSS Private bucket" (lower-right) is the 2-legged part of the service, the rest is 3-legged:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d23e8cde970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d23e8cde970c image-full img-responsive" title="Screen Shot 2016-11-28 at 07.42.29" src="/assets/image_fb5359.jpg" alt="Screen Shot 2016-11-28 at 07.42.29" width="937" height="616" border="0" /></a></p>
<p>&nbsp; &nbsp; Each item can have at least one or multiple versions. A version is really a logical representation of the actual file stored of the cloud. There is a direct link between version and file.</p>
<p>&nbsp; &nbsp; The step-by-step tutorial shows how to create a new item with the service and how to create a new version of an existing item.&nbsp;What I want to illustrate here is how to handle that workflow concretely. The code below illustrates how to upload a file to the service: if no item with the same display name exists under the target folder, it will create a new one, otherwise it will create a new version of that item and append to it.&nbsp;</p>
<script src="https://gist.github.com/leefsmp/2f7b019999dc4b2cd77258e0954940e4.js"></script>
<p>&nbsp; &nbsp; You can refer to the complete code by looking at the implementation of my <a href="https://github.com/Autodesk-Forge/forge-boilers.nodejs/blob/master/6%20-%20viewer%2Bserver%2Bdata-mng%2Bderivatives/src/server/api/services/DMSvc.js">DMSvc</a> service. I split the code on my server in those multiple micro-services which makes it easy to reuse in different parts of the app or even other projects.&nbsp;</p>
<p>&nbsp; &nbsp; The other&nbsp;feature I added to my sample recently is about attachments: an attachment is a reference between two versions under the same project. It is limited to version-version at the moment but should be extended to link a version to an item or folder in the future. The step-by-step tutorial from the help file can be found <a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/create-attachment/" rel="noopener noreferrer" target="_blank">there</a>. Below is the code that shows how to get and create version attachments:</p>
<script src="https://gist.github.com/leefsmp/fb843b4868d34e3d72ab71c9cd2a8d48.js"></script>
<p>&nbsp; &nbsp; All this can be tested in the live demo of my Data Management sample at:&nbsp;<a href="https://dm.autodesk.io" rel="noopener noreferrer" target="_blank">https://dm.autodesk.io</a>. As illustrated in the picture below, you can visualise versions and attachments info for the selected item (by clicking on the clock icon in upper treeview). From there it is possible to either drop or pick a file to attach to this version. If the attached&nbsp;file doesn't exist in the target folder, a new item will be created and the first version will be attached, otherwise a new version will be added to the item.</p>
<p>&nbsp; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b4c0c6970b-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b4c0c6970b image-full img-responsive" title="Screen Shot 2016-11-27 at 20.04.46" src="/assets/image_75f2af.jpg" alt="Screen Shot 2016-11-27 at 20.04.46" border="0" /></a></p>
<p>&nbsp;The UI also allows to create an attachment by specifying "manually" the versionId: right-click on Attachments and select "Attach by version Id":</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d23e8fcf970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d23e8fcf970c image-full img-responsive" title="Screen Shot 2016-11-28 at 08.41.05" src="/assets/image_3738da.jpg" alt="Screen Shot 2016-11-28 at 08.41.05" border="0" /></a></p>
<p>You can easily find out what is the version Id you are looking for by selecting another item, display its versions and use the context menu command again: "Show version details"</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b4c155970b-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b4c155970b image-full img-responsive" title="Screen Shot 2016-11-28 at 08.42.38" src="/assets/image_0712b9.jpg" alt="Screen Shot 2016-11-28 at 08.42.38" border="0" /></a></p>
<p>This will display the actual payloads returned by the API, for example:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0957ca2e970d-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0957ca2e970d image-full img-responsive" title="Screen Shot 2016-11-28 at 08.45.32" src="/assets/image_d75f30.jpg" alt="Screen Shot 2016-11-28 at 08.45.32" border="0" /></a></p>

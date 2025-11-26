---
layout: "post"
title: "Get the LandXML from Cloud to create a TIN Surface in Civil 3D"
date: "2012-12-05 01:01:30"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Cloud"
  - "Mobile"
  - "Partha Sarkar"
  - "Web"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/12/get-the-landxml-from-cloud-to-create-a-tin-surface-in-civil-3d.html "
typepad_basename: "get-the-landxml-from-cloud-to-create-a-tin-surface-in-civil-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In my
previous <a href="http://adndevblog.typepad.com/infrastructure/2012/12/storing-landxml-in-cloud-.html">blog
post</a> we saw how quickly and easily we can upload and store a LandXML file
in Windows Azure Cloud storage. In this post we will see how we can access our
LandXML files stored in Cloud and use the same to create a TIN Surface in Civil
3D.</p>
<p>Here is the
C# code snippet with minimal error handlers :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> LandXMLDownLoadFromCloudStorage()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the AutoCAD Editor</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// declare the Windows Azure storage account connection string</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//string connStr = &quot;DefaultEndpointsProtocol=https; &quot; +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//&#0160; &quot; AccountName=your_storage_account_name; &quot; +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//&#0160; &quot;AccountKey=your_storage_account_key&quot;;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> connStr = </span><span style="color: #a31515; line-height: 140%;">&quot;DefaultEndpointsProtocol=https; &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot; AccountName=your_storage_account_name; &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;AccountKey=your_storage_account_key&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// get the Storage account&#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudStorageAccount</span><span style="line-height: 140%;"> cloudStorageAccount = </span><span style="color: #2b91af; line-height: 140%;">CloudStorageAccount</span><span style="line-height: 140%;">.Parse(connStr);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Create the blob client that provides authenticated access to the Blob service.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudBlobClient</span><span style="line-height: 140%;"> blobClient = cloudStorageAccount.CreateCloudBlobClient();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get the container reference.&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudBlobContainer</span><span style="line-height: 140%;"> blobContainer = blobClient.GetContainerReference(</span><span style="color: #a31515; line-height: 140%;">&quot;c3dlandxmlsurface&quot;</span><span style="line-height: 140%;">);&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get a reference to the blob.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudBlob</span><span style="line-height: 140%;"> blob = blobContainer.GetBlobReference(</span><span style="color: #a31515; line-height: 140%;">&quot;Civil3DLand.xml&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Download the file to local system</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> downloadedLandXmlFile = </span><span style="color: #a31515; line-height: 140%;">@&quot;c:\Temp\Civil3DLandXMLSurface.xml&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; blob.DownloadToFile(downloadedLandXmlFile);&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Call Civil 3D Function to Import the LandXML</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// We need to use Civil 3D COM API IAeccSurfaces:: ImportXML() to Import a LandXML</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">File</span><span style="line-height: 140%;">.Exists(downloadedLandXmlFile))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; Civil3DImportLandXML(downloadedLandXmlFile);&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">StorageClientException</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Storage Client Error : &quot;</span><span style="line-height: 140%;"> + e.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Exit here ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Error encountered: &quot;</span><span style="line-height: 140%;"> + ex.Message);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>For the implementation of <strong>Civil3DImportLandXML</strong>(filename)function,
refer to my previous blog post topic <a href="http://adndevblog.typepad.com/infrastructure/2012/12/creating-a-tin-surface-from-landxml-file.html">Creating
a TIN surface from LandXML file</a></p>
<p>&#0160;</p>
<p>Hope this is
useful to you.</p>

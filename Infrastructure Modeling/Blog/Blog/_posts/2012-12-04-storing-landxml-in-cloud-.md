---
layout: "post"
title: "Storing LandXML in Cloud "
date: "2012-12-04 01:01:33"
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
original_url: "https://adndevblog.typepad.com/infrastructure/2012/12/storing-landxml-in-cloud-.html "
typepad_basename: "storing-landxml-in-cloud-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In my
previous <a href="http://adndevblog.typepad.com/infrastructure/2012/12/creating-a-tin-surface-from-landxml-file.html">blog
post</a> I indicated to experiment with storing LandXML files in Cloud so that
we can access the same anytime and anywhere. I thought of trying to use Windows
Azure Blob storage. If you are in the learning phase of Cloud services and want
to quickly build a sample to understand how it works, the following could be a
good starting point <a href="http://www.windowsazure.com/en-us/develop/net/how-to-guides/blob-storage/">How
to use the Windows Azure Blob Storage Service in .NET</a>. This how-to-guide
gives you step by step detail on how to build an application using Windows
Azure Blob Storage Service in .NET, hence I am not writing down each steps
here, instead let’s see how we can extend this learning to create a simple .NET
application for Civil 3D to upload a LandXML to cloud and in the later part we
will see how to access and get the same LandXML in Civil 3D and create a TIN
surface.</p>
<p>Here is a screenshot
of the assemblies I added to my C3DLandXMLCloudStorageSample project –</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c344242ea970b-pi" style="display: inline;"><img alt="C3DLandXML_Cloud01" class="asset  asset-image at-xid-6a0167607c2431970b017c344242ea970b" src="/assets/image_83a722.jpg" title="C3DLandXML_Cloud01" /></a></p>
<p>And here is
the C# code snippet with minimal error handlers :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> LandXMLUpload2CloudStorage()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get the AutoCAD Editor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// declare the Windows Azure storage account connection string&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//string connStr = &quot;DefaultEndpointsProtocol=https; &quot; +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//&#0160; &quot; AccountName=your_storage_account_name; &quot; +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//&#0160; &quot;AccountKey=your_storage_account_key&quot;;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> connStr = </span><span style="color: #a31515; line-height: 140%;">&quot;DefaultEndpointsProtocol=https; &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot; AccountName=your_storage_account_name; &quot;</span><span style="line-height: 140%;"> +</span><span style="line-height: 140%;">&#0160; &#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</span><span style="color: #a31515; line-height: 140%;">&quot;AccountKey=your_storage_account_key&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// get the Storage account</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudStorageAccount</span><span style="line-height: 140%;"> cloudStorageAccount = </span><span style="color: #2b91af; line-height: 140%;">CloudStorageAccount</span><span style="line-height: 140%;">.Parse(connStr);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Create the blob client that provides authenticated access to the Blob service.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudBlobClient</span><span style="line-height: 140%;"> blobClient = cloudStorageAccount.CreateCloudBlobClient();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get the container reference.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// All letters in a container name must be lowercase.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// http://msdn.microsoft.com/en-us/library/dd135715.aspx</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudBlobContainer</span><span style="line-height: 140%;"> blobContainer = blobClient.GetContainerReference(</span><span style="color: #a31515; line-height: 140%;">&quot;c3dlandxmlsurface&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Create the container if it does not exist.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; blobContainer.CreateIfNotExist();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Set permissions on the container.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">BlobContainerPermissions</span><span style="line-height: 140%;"> containerPermissions = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">BlobContainerPermissions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// In this sample, I am using a public access for the blob. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// However, your application may need a different permisson. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; containerPermissions.PublicAccess = </span><span style="color: #2b91af; line-height: 140%;">BlobContainerPublicAccessType</span><span style="line-height: 140%;">.Blob;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; blobContainer.SetPermissions(containerPermissions);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Get a reference to the blob.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">CloudBlob</span><span style="line-height: 140%;"> blob = blobContainer.GetBlobReference(</span><span style="color: #a31515; line-height: 140%;">&quot;Civil3DLand.xml&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Upload a file from the local system to the blob.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nStarting to upload the LandXML file...&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; blob.UploadFile(</span><span style="color: #a31515; line-height: 140%;">@&quot;c:\Temp\Existing Ground Surface.xml&quot;</span><span style="line-height: 140%;">);&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCivil 3D Surface LandXML file is uploaded to Cloud Storage :&#0160; &quot;</span><span style="line-height: 140%;"> + blob.Uri);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">StorageClientException</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Storage Client Error : &quot;</span><span style="line-height: 140%;"> + e.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Exit here ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Error encountered: &quot;</span><span style="line-height: 140%;"> + ex.Message);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Next we will
see how to access this LandXML from cloud and get it in Civil 3D to create a
TIN surface.</p>

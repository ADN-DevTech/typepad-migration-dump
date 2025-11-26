---
layout: "post"
title: "Forge Data Management API: [Upload, Download, Manage] your Design Data on the Autodesk Cloud"
date: "2016-07-25 09:11:19"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "Data Management"
  - "Frontend"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "Storage"
  - "Viewer"
  - "Web Development"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/forge-data-management-api-upload-download-manage-your-design-data-on-the-autodesk-cloud.html "
typepad_basename: "forge-data-management-api-upload-download-manage-your-design-data-on-the-autodesk-cloud"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" target="_blank">(@F3lipek)</a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;My previous Forge-focused blog last week was dealing with the OAuth authentication workflow, in case you missed it here it is:</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;"><a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/07/landing-your-forge-oauth-authentication-workflow.html" target="_blank">Landing your Forge OAuth authentication workflow</a> </span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;If you practiced it a bit, authenticating through the Autodesk OAuth server should now be a breeze for you ;) ... This time we can start having some real fun because we are going to use the actual Forge API&#39;s !</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;Most likely, the first thing you may want to do is to upload your own models to the Autodesk Cloud, in order to load them at a later time into the viewer or extract some metadata. If for some reason you&#39;ve got no design data at all, keep reading we&#39;ve got you covered with a bunch of free sample models ...</span></p>
<p><em><strong><span style="font-family: arial, helvetica, sans-serif;">I - OSS: Autodesk Object Storage Service</span></strong></em></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;Let&#39;s get started with the easiest part, OSS API. What we now call the Forge Data&#0160;Management API (see official documentation <a href="https://developer.autodesk.com/en/docs/data/v2/overview/" target="_blank">here</a>) is actually a set of two distinct API&#39;s which may&#0160;be used independently based on what you need to do. If you used the View &amp; Data API in the past, chances are you&#39;re probably already familiar with OSS. </span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;It is a rather basic file storage REST API that lets you host&#0160;securely any data on the Autodesk Cloud. Typically that&#39;s an App-context API, which means you need a 2-legged OAuth token to use it. But for some workflows that we are going to cover in section&#0160;II of this article, you may use a 3-legged token. The way OSS API works is pretty straightforward:</span></p>
<ol>
<li><span style="font-family: arial, helvetica, sans-serif;">Obtain a valid&#0160;OAuth token in order to perform authorized REST API calls</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">Create a <em>bucket</em>: a logical storage unit like a folder</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">Upload any file to the bucket (a file is called an <em>object)</em></span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">Download your objects if needed&#0160;</span></li>
</ol>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;Now here are the tricks: a bucket is created with a specific policy, this defines </span><span style="text-decoration: underline;">how long the files are being kept on that bucket</span><span style="font-family: arial, helvetica, sans-serif;">.&#0160;&#0160;For info: </span></p>
<ul style="list-style-type: disc;">
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>transient</strong></em>: your file remains 24h on the bucket</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>temporary</strong></em>: 1month</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>persistent</strong></em>: until you decide to delete them&#0160;</span></li>
</ul>
<p><span style="font-family: arial, helvetica, sans-serif;"><span style="text-decoration: underline;"><strong><em>The bucket itself is never deleted</em></strong></span>, I just need to emphasis on it because this has been a&#0160;common&#0160;misunderstanding in the past!&#0160;</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;&#0160;The bucket name has to be <span style="text-decoration: underline;"><strong><em>lower case</em></strong></span>, with <span style="text-decoration: underline;"><strong><em>no special characters</em></strong></span> and has to be <span style="text-decoration: underline;"><strong><em>unique service-wide</em></strong></span>, across ALL the users of the service, not just you. So if you first try the API and attempt to create a bucket named <em>&quot;bucket1&quot; </em>... unfortunately there are chances that some other developer already owns that name (you will get a <em>409 - conflict</em> error in such case). So you can either come up with really fancy names or append some guid-like prefix/suffix to your bucket names in order to avoid collisions.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; The object keys can be anything - no special characters though - but need to be unique on a per-bucket basis, otherwise uploading an object with a name that already exists on the same bucket will simply override the resource.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; There also has been often requested how many buckets you can create, how many objects a bucket can contain, how large can be each object... there are no theoretical&#0160;bounds to the API, sky&#39;s the limit. We could advise you create just three buckets (</span><span style="font-family: arial, helvetica, sans-serif;">1 transient, 1&#0160;</span><span style="font-family: arial, helvetica, sans-serif;">temporary&#0160;and 1</span><span style="font-family: arial, helvetica, sans-serif;">persistent) but that&#39;s really up to you. If</span><span style="font-family: arial, helvetica, sans-serif;">&#0160;you plan to create multiple buckets based on some workflow, just think about how your logic will scale: creating one bucket per user may not scale nicely once you get a large customer base...&#0160;</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; If you used the OSS API version1, here are the enhancements for the v2:</span></p>
<ul style="list-style-type: disc;">
<li><span style="font-family: arial, helvetica, sans-serif;">You can now delete an object on a bucket</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">Deleting a bucket is doable but NOT for everybody, because that&#39;s a dangerous operation, we decided to whitelist users who can do that.</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">You can iterate the list of buckets you created from your set of API keys</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">Similarly you can iterate the objects stored in each bucket</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;">You can create signed resources, but I will tackle that in another post (can&#39;t get all the fun at once!)</span></li>
</ul>
<p>&#0160;&#0160;&#0160;&#0160;Below is the implementation of my server-side node&#0160;service that wraps the OSS API calls. It&#39;s pretty straightforward and doesn&#39;t do anything fancy. At the time of the writing, I didn&#39;t implemented yet the resumable upload that allows you to upload large files by multiple chucks. The reason is that we are working on auto-generated API wrappers for our REST APIs, so I&#39;m waiting for that to be ready. Once the wrapper is&#0160;available, I simply replace the request calls by the wrapper methods inside the service. The impact on the rest of my server code will be inexistent.</p>
<script src="https://gist.github.com/leefsmp/f6888e95b3f40129911fb94b3e531178.js"></script>
<p>&#0160;&#0160;&#0160;&#0160;Using that, the REST API exposed by my server for its client application is just a thin Express wrapper around the service methods. For example below is the &quot;GET /buckets&quot; implementation. For full implementation, take a look there:&#0160;<a href="https://github.com/leefsmp/forge/blob/master/src/server/api/endpoints/oss.js" target="_blank">oss endpoint</a>&#0160;</p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 10pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> router = express.Router()
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 
 3 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">// GET /buckets
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 7 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 </span><span style="background-color: #ffffff;">router.get(</span><span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;/buckets&#39;</span><span style="background-color: #ffffff;">, async (req, res) =&gt;{
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 
10 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">try</span><span style="background-color: #ffffff;"> {
</span><span style="color: #800000; background-color: #f0f0f0;">11 
12 </span>    <span style="color: #808080; background-color: #ffffff; font-style: italic;">// obtain forge service
</span><span style="color: #800000; background-color: #f0f0f0;">13 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> forgeSvc = ServiceManager.getService(
</span><span style="color: #800000; background-color: #f0f0f0;">14 </span>      <span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;ForgeSvc&#39;</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">15 
16 </span>    <span style="color: #808080; background-color: #ffffff; font-style: italic;">// request 2legged token
</span><span style="color: #800000; background-color: #f0f0f0;">17 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> token = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span><span style="background-color: #ffffff;"> forgeSvc.getToken(</span><span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;2legged&#39;</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">18 
19 </span>    <span style="color: #808080; background-color: #ffffff; font-style: italic;">// obtain oss service
</span><span style="color: #800000; background-color: #f0f0f0;">20 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> ossSvc = ServiceManager.getService(</span><span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;OssSvc&#39;</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">21 
22 </span>    <span style="color: #808080; background-color: #ffffff; font-style: italic;">// get list of bucket by passing valid token
</span><span style="color: #800000; background-color: #f0f0f0;">23 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> response = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span><span style="background-color: #ffffff;"> ossSvc.getBuckets(
</span><span style="color: #800000; background-color: #f0f0f0;">24 </span><span style="background-color: #ffffff;">      token.access_token)
</span><span style="color: #800000; background-color: #f0f0f0;">25 
26 </span>    <span style="color: #808080; background-color: #ffffff; font-style: italic;">// send json-formatted response
</span><span style="color: #800000; background-color: #f0f0f0;">27 </span><span style="background-color: #ffffff;">    res.json(response)
</span><span style="color: #800000; background-color: #f0f0f0;">28 
29 </span><span style="background-color: #ffffff;">  } </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">catch</span><span style="background-color: #ffffff;"> (ex) {
</span><span style="color: #800000; background-color: #f0f0f0;">30 </span>    
<span style="color: #800000; background-color: #f0f0f0;">31 </span><span style="background-color: #ffffff;">    res.status(ex.statusCode || </span><span style="color: #0000ff; background-color: #ffffff;">500</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">32 </span><span style="background-color: #ffffff;">    res.json(ex)
</span><span style="color: #800000; background-color: #f0f0f0;">33 </span><span style="background-color: #ffffff;">  }
</span><span style="color: #800000; background-color: #f0f0f0;">34 </span><span style="background-color: #ffffff;">})<br /></span></pre>
<p>You can refer to <a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/app-managed-bucket/" target="_blank">this page</a>&#0160;for the official documentation to see how to create a bucket and upload a file to it.</p>
<p><em><strong><span style="font-family: arial, helvetica, sans-serif;">II -&#0160;A360 Data Management API</span></strong></em></p>
<p><em><strong><span style="font-family: arial, helvetica, sans-serif;">&#0160;&#0160;&#0160;</span></strong></em><span style="font-family: arial, helvetica, sans-serif;">&#0160;The second set of endpoints within the DataManagement API provides a programmatic access to A360, the Autodesk Cloud at&#0160;<a href="https://a360.autodesk.com" target="_blank" title="https://a360.autodesk.com">https://<strong>a360</strong>.<strong>autodesk</strong>.com</a>.&#0160;That&#39;s a 3-legged OAuth API that let&#39;s your application access and manage files of a user once it has been authorized through a web interface.&#0160;</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; If you don&#39;t have an A360 account yet, you will need to signup for one in order to test the API with your data. A set of basic design files will be provisioned automatically to your account upon first sign in.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; The underlying file storage system is based on the OSS API described above but A360 adds an extra layer of features on top of it, such as sharing your data with other users, versioning your files, attaching metadata to it, and many more features to come... It is therefore more complex to use than OSS.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">Your data is organized as follow inside A360:</span></p>
<ul style="list-style-type: disc;">
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>Hubs</strong></em>: the highest logical data storage on A360. Each user has it&#39;s own hub by default but&#0160;you ca n create additional team hubs and share that with other users</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>Projects</strong></em>: under each hub, you have projects, which can be seen as root folders containing your data</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>Folders</strong></em>: A logical sub-folder inside a project or another folder</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>Items</strong></em>: a&#0160;file that you uploaded to a project or folder</span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><em><strong>Versions</strong></em>: each item may contain one or multiple versions of the file. You can then select programmatically&#0160;which version you want to access, download or load in the viewer</span></li>
</ul>
<p><span style="font-family: arial, helvetica, sans-serif;">In order to upload a file to an A360 user account, you have&#0160;to follow&#0160;several steps:</span></p>
<ol>
<li>Your web application needs to obtain a valid 3-legged token by requesting user approval</li>
<li>Determine projectId and folderId which identify where on A360 you want to upload the data</li>
<li>Create a storage location: basically the API will determine the underlying OSS bucketKey and objectKey you have to use to upload the file</li>
<li>Upload the data using OSS API</li>
<li>If the file doesn&#39;t exist create a new item, otherwise create a new version and add it to the corresponding item</li>
</ol>
<p>Here is how the implementation of that workflow looks like in my service:</p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 10pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">// Upload file to create new item or new version
</span><span style="color: #800000; background-color: #f0f0f0;"> 3 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 </span><span style="background-color: #ffffff;">upload (token, projectId, folderId, file, displayName = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">null</span><span style="background-color: #ffffff;">) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 
 7 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">return</span> <span style="color: #000080; background-color: #ffffff; font-weight: bold;">new</span><span style="background-color: #ffffff;"> Promise(async(resolve, reject) =&gt; {
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 
 9 </span>    <span style="color: #000080; background-color: #ffffff; font-weight: bold;">try</span><span style="background-color: #ffffff;"> {
</span><span style="color: #800000; background-color: #f0f0f0;">10 
11 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> filename = file.originalname
</span><span style="color: #800000; background-color: #f0f0f0;">12 
13 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> storage = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span> <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.createStorage(
</span><span style="color: #800000; background-color: #f0f0f0;">14 </span><span style="background-color: #ffffff;">        token, projectId, folderId, filename)
</span><span style="color: #800000; background-color: #f0f0f0;">15 
16 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> ossSvc = ServiceManager.getService(</span><span style="color: #008000; background-color: #ffffff; font-weight: bold;">&#39;OssSvc&#39;</span><span style="background-color: #ffffff;">)
</span><span style="color: #800000; background-color: #f0f0f0;">17 
18 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> objectId = ossSvc.parseObjectId(storage.id)
</span><span style="color: #800000; background-color: #f0f0f0;">19 
20 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> object = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span><span style="background-color: #ffffff;"> ossSvc.putObject(
</span><span style="color: #800000; background-color: #f0f0f0;">21 </span><span style="background-color: #ffffff;">        token,
</span><span style="color: #800000; background-color: #f0f0f0;">22 </span><span style="background-color: #ffffff;">        objectId.bucketKey,
</span><span style="color: #800000; background-color: #f0f0f0;">23 </span><span style="background-color: #ffffff;">        objectId.objectKey,
</span><span style="color: #800000; background-color: #f0f0f0;">24 </span><span style="background-color: #ffffff;">        file)
</span><span style="color: #800000; background-color: #f0f0f0;">25 
26 </span>      <span style="color: #808080; background-color: #ffffff; font-style: italic;">// look for items with the same displayName
</span><span style="color: #800000; background-color: #f0f0f0;">27 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> items = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span> <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.findItemsWithAttributes(
</span><span style="color: #800000; background-color: #f0f0f0;">28 </span><span style="background-color: #ffffff;">        token,
</span><span style="color: #800000; background-color: #f0f0f0;">29 </span><span style="background-color: #ffffff;">        projectId,
</span><span style="color: #800000; background-color: #f0f0f0;">30 </span><span style="background-color: #ffffff;">        folderId, {
</span><span style="color: #800000; background-color: #f0f0f0;">31 </span><span style="background-color: #ffffff;">          displayName: filename
</span><span style="color: #800000; background-color: #f0f0f0;">32 </span><span style="background-color: #ffffff;">        })
</span><span style="color: #800000; background-color: #f0f0f0;">33 
34 </span>      <span style="color: #000080; background-color: #ffffff; font-weight: bold;">if</span><span style="background-color: #ffffff;">(items.length &gt; </span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">) {
</span><span style="color: #800000; background-color: #f0f0f0;">35 
36 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> item = items[</span><span style="color: #0000ff; background-color: #ffffff;">0</span><span style="background-color: #ffffff;">]
</span><span style="color: #800000; background-color: #f0f0f0;">37 
38 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> version = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span> <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.createVersion(
</span><span style="color: #800000; background-color: #f0f0f0;">39 </span><span style="background-color: #ffffff;">          token,
</span><span style="color: #800000; background-color: #f0f0f0;">40 </span><span style="background-color: #ffffff;">          projectId,
</span><span style="color: #800000; background-color: #f0f0f0;">41 </span><span style="background-color: #ffffff;">          item.id,
</span><span style="color: #800000; background-color: #f0f0f0;">42 </span><span style="background-color: #ffffff;">          storage.id,
</span><span style="color: #800000; background-color: #f0f0f0;">43 </span><span style="background-color: #ffffff;">          filename)
</span><span style="color: #800000; background-color: #f0f0f0;">44 
45 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> response = {
</span><span style="color: #800000; background-color: #f0f0f0;">46 </span><span style="background-color: #ffffff;">          version,
</span><span style="color: #800000; background-color: #f0f0f0;">47 </span><span style="background-color: #ffffff;">          storage,
</span><span style="color: #800000; background-color: #f0f0f0;">48 </span><span style="background-color: #ffffff;">          object,
</span><span style="color: #800000; background-color: #f0f0f0;">49 </span><span style="background-color: #ffffff;">          item
</span><span style="color: #800000; background-color: #f0f0f0;">50 </span><span style="background-color: #ffffff;">        }
</span><span style="color: #800000; background-color: #f0f0f0;">51 
52 </span><span style="background-color: #ffffff;">        resolve(response)
</span><span style="color: #800000; background-color: #f0f0f0;">53 
54 </span><span style="background-color: #ffffff;">      } </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">else</span><span style="background-color: #ffffff;"> {
</span><span style="color: #800000; background-color: #f0f0f0;">55 
56 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> item = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">await</span> <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.createItem(
</span><span style="color: #800000; background-color: #f0f0f0;">57 </span><span style="background-color: #ffffff;">          token,
</span><span style="color: #800000; background-color: #f0f0f0;">58 </span><span style="background-color: #ffffff;">          projectId,
</span><span style="color: #800000; background-color: #f0f0f0;">59 </span><span style="background-color: #ffffff;">          folderId,
</span><span style="color: #800000; background-color: #f0f0f0;">60 </span><span style="background-color: #ffffff;">          storage.id,
</span><span style="color: #800000; background-color: #f0f0f0;">61 </span><span style="background-color: #ffffff;">          filename,
</span><span style="color: #800000; background-color: #f0f0f0;">62 </span><span style="background-color: #ffffff;">          displayName)
</span><span style="color: #800000; background-color: #f0f0f0;">63 
64 </span>        <span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> response = {
</span><span style="color: #800000; background-color: #f0f0f0;">65 </span><span style="background-color: #ffffff;">          storage,
</span><span style="color: #800000; background-color: #f0f0f0;">66 </span><span style="background-color: #ffffff;">          object,
</span><span style="color: #800000; background-color: #f0f0f0;">67 </span><span style="background-color: #ffffff;">          item
</span><span style="color: #800000; background-color: #f0f0f0;">68 </span><span style="background-color: #ffffff;">        }
</span><span style="color: #800000; background-color: #f0f0f0;">69 
70 </span><span style="background-color: #ffffff;">        resolve(response)
</span><span style="color: #800000; background-color: #f0f0f0;">71 </span><span style="background-color: #ffffff;">      }
</span><span style="color: #800000; background-color: #f0f0f0;">72 
73 </span><span style="background-color: #ffffff;">    } </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">catch</span><span style="background-color: #ffffff;"> (ex) {
</span><span style="color: #800000; background-color: #f0f0f0;">74 
75 </span><span style="background-color: #ffffff;">      reject(ex)
</span><span style="color: #800000; background-color: #f0f0f0;">76 </span><span style="background-color: #ffffff;">    }
</span><span style="color: #800000; background-color: #f0f0f0;">77 </span><span style="background-color: #ffffff;">  })
</span><span style="color: #800000; background-color: #f0f0f0;">78 </span><span style="background-color: #ffffff;">}</span></pre>
<p>&#0160;&#0160;&#0160;&#0160;The complete implementation is available there: <a href="https://github.com/leefsmp/forge/blob/master/src/server/api/services/DMSvc.js" target="_blank">DM Service</a></p>
<p>&#0160;&#0160;&#0160;&#0160;In order to download a file, you first need to determine the available versions and select the version you want, then obtain the OSS objectId of that version and download the file using OSS API.</p>
<p>&#0160;&#0160;&#0160;&#0160;Here is how an <a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-items-item_id-versions-GET/" target="_blank">item versions</a> response may look like, the OSS objectId we are interested is available in <em><strong>relationships.storage.data.id </strong></em>(Line #66). The bucketKey to use would be <em>&quot;wip.dm.prod&quot;</em> and the objectKey <em>&quot;3d249b40-b15d-46a2-b684-001d4534129f.dwfx&quot;</em>&#0160;</p>
<script src="https://gist.github.com/leefsmp/79f3c190db56b4cd60dfca4bcbb65dc2.js"></script>
<p><em><strong>III - Implementing the UI</strong></em></p>
<p>&#0160;&#0160;&#0160;&#0160;Implementing a good web UI that interacts with your server in order to let your users upload, download and visualize their data is not straightforward at all. I spent a large portion of the development time working on the following control panel that displays the list of all the hubs/projects/folders/items for the signed-in user, the OSS buckets &amp; objects&#0160;linked to my app account and also lets the user download or upload files either by drag and drop or file picking. Each item in the treeview has also a context menu that provides some actions based on the type of the item.&#0160;If the item is a design data supported by the viewer, you can double click on it to import it into the viewer.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0922d99e970d-pi" style="display: inline;"><img alt="Screen Shot 2016-07-25 at 12.21.05" class="asset  asset-image at-xid-6a0167607c2431970b01bb0922d99e970d img-responsive" src="/assets/image_5b4536.jpg" title="Screen Shot 2016-07-25 at 12.21.05" /></a></p>
<p>&#0160; &#0160; I used the following libraries to achieve this control:</p>
<ul style="list-style-type: disc;">
<li><a href="http://www.dropzonejs.com/" target="_blank">DropzoneJS</a>&#0160;on the client side &#0160;+ <a href="https://www.npmjs.com/package/multer" target="_blank">Multer npm package</a> for node. The setup is very easy and they offer flexible options for customization. For having tried multiple other libraries, I highly recommend this combination!</li>
</ul>
<ul style="list-style-type: disc;">
<li>The treeview is the one provided by the viewer API: <em><strong>Autodesk.Viewing.UI.Tree</strong></em> and <em><strong>Autodesk.Viewing.UI.TreeDelegate</strong></em> objects. It&#39;s a pretty interesting component because it allows to load the data associated with each hub/folder/item asynchronously so you can see the tree will populate progressively but you can start interacting with it directly. You don&#39;t have to wait for a huge payload of all your items to initialize the whole tree or load the subfolder only once the user expands them. It would deserve a blogpost on its own to explain all the tricks you can achieve with that control ... You can find the implementation of the tree in&#0160;<a href="https://github.com/leefsmp/forge/blob/master/src/client/Components/Viewer/extensions/Viewing.Extension.Storage/Viewing.Extension.Storage.Panel.js" target="_blank">Viewing.Extension.Storage.Panel.js</a></li>
</ul>
<ul style="list-style-type: disc;">
<li>The TabManager that lets you switch between hubs is a complete custom control. You can find it&#39;s implementation in&#0160;<a href="https://github.com/leefsmp/forge/tree/master/src/client/utils/TabManager" target="_blank">here</a>.</li>
</ul>
<ul style="list-style-type: disc;">
<li>You can reorder the Tabs by simple drag and drop, this feature is using the <a href="https://github.com/bevacqua/dragula" target="_blank">dragula library</a>, a must!</li>
</ul>
<ul style="list-style-type: disc;">
<li>Complete code for that panel is packed into a viewer extension:&#0160;<a href="https://github.com/leefsmp/forge/tree/master/src/client/Components/Viewer/extensions/Viewing.Extension.Storage">Viewing.Extension.Storage</a></li>
</ul>
<p>&#0160;&#0160;&#0160;&#0160;That&#39;s it for today! The DataManagemt API official documentation is available <a href="https://developer.autodesk.com/en/docs/data/v2/overview/" target="_blank">here</a>, the complete source code of my ongoing project is <a href="https://github.com/leefsmp/forge" target="_blank">there</a> and the live sample runs pretentiously at&#0160;<a href="https://forge.autodesk.io" target="_blank">https://forge.autodesk.io</a>&#0160;;) Feel free to try it today with your own A360 account!</p>
<p>&#0160;&#0160;&#0160;&#0160;In a&#0160;next post I will described the work I did with the <a href="https://developer.autodesk.com/en/docs/model-derivative/v2" target="_blank">Model Derivative API</a> which lets you&#0160;access metadata and properties of your model, convert design data into viewables that can be loaded into the viewer, or export the geometry to different CAD formats.</p>

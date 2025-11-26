---
layout: "post"
title: "Buckets in Autodesk View and Data API"
date: "2015-01-16 00:04:48"
author: "Daniel Du"
categories:
  - "Cloud"
  - "Daniel Du"
  - "Storage"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/01/buckets-in-autodesk-view-and-data-api.html "
typepad_basename: "buckets-in-autodesk-view-and-data-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>
<p>I believe you’ve already heard of Autodesk View and Data API, but if you haven’t, here is the idea, the View &amp; Data API enables web developers to very easily display 3D (and 2D) models on a WebGL-enabled browser. Please check out <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/01/what-is-this-view-data-api-thing-anyway.html">this post</a> for details.</p>
<p>To view your 3D or 2D models in viewer, you need to upload them to Autodesk cloud and get it translated with REST API. Of course you need a container in cloud for the model to be translated. This container is a “bucket”. Before uploading a file, create a bucket and set a retention policy if this is your first time to use the View and Data API.&#0160;</p>
<p>About the bucket, here are something you need to know.</p>
<p><strong>Life cycle of bucket:</strong></p>
<p>Briefly, bucket has following 3 retention policies:</p>
<ul>
<li>Transient: cache-like storage that persists for only 24 hours, ideal for intermittent objects.</li>
<li>Temporary: storage that persists for 30 days. Good for data that is uploaded and accessed, then not needed later. This type of bucket storage will save your service money.</li>
<li>Persistent: storage that persists until it’s deleted. Items that have not been accessed for 2 years may be archived.</li>
</ul>
<p>For transient buckets and temporary buckets, objects in bucket and the bucket itself will be deleted once the time frame is out. For persistent bucket, objects and buckets will be persistent until you delete them explicitly. One exception is that, if you delete your app consumer key from <a href="http://developer.autodesk.com">http://developer.autodesk.com</a>, your data will be deleted.&#0160;</p>
<p><strong>Naming of bucket:</strong></p>
<p>There are also some restrictions on the characters used in the bucket name.</p>
<ul>
<li>The bucket key (i.e. bucket name) must match “^[-_.a-z0-9]{3,128}$”. That is bucket must be between 3 to 128 characters long and contain only lowercase letters, numbers and the symbols . _ –&#0160;</li>
<li>Bucket keys are unique within the data center or region in which they were created, that means you cannot create a bucket with a name has been take by others. So best practice is the incorporate your company name/domain name or even the consumer key into the bucket name. If you prefer to use the consumer key(should convert to lowercase first) as part of bucket name, please pay attention to the length of it, which should be less than 128.</li>
<li>A bucket key cannot be changed once it is created.</li>
</ul>
<p><strong>Permission of bucket:</strong></p>
<p>Buckets are arbitrary spaces created and owned by services. The service creating the bucket is the owner of the bucket, and the owning service will always have full access to a bucket. In another word, the app with specify consumer key/secret key is the owner of it’s bucket. Other apps with different consumer key/secret key do not have the permission to read/write to the bucket by default. This is to protect your properties in cloud, no one can access your data without your authorization. Furthermore, if you have registered 2 apps on <a href="http://developer.autodesk.com">http://developer.autodesk.com</a>, one app cannot access the models which are uploaded/registered by another app.</p>
<p><strong>FAQs:</strong></p>
<p><strong>Q: Do I need to create a bucket every time when I update my models?</strong></p>
<p>A: No, a bucket can contain many objects. We suggest to create only one bucket for one App.</p>
<p><strong>Q: When I query the existence of bucket before creating one, it exists, but I get 403 forbidden error when I try to upload models to it?</strong></p>
<p>A: Refer to the bucket name policy and permission policy. One possible reason is the bucket with the name has been created by others while you do not have access to it. When you get info about the bucket, it also provides the ID of the owner. So you could check based on that if it is the same as your consumer key. If it’s not then you’re not the owner and you should try to create a new bucket:</p>
<pre>{</pre>
<pre>&#0160;&#0160;&#0160; &quot;key&quot; : &quot;mybucket&quot;,</pre>
<pre>&#0160;&#0160;&#0160; &quot;owner&quot; : &quot;obQDn8P0GanGFQha4ngKKVWcxwyvFAGE”, &lt;&lt; consumer key</pre>
<pre>&#0160;&#0160;&#0160; &quot;createDate&quot; : 1401735235495,</pre>
<pre>&#0160;&#0160;&#0160; &quot;permissions&quot; : [{</pre>
<pre>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;serviceId&quot; : &quot;obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&quot;,</pre>
<pre>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;access&quot; :&#0160; &quot;full&quot;</pre>
<pre>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</pre>
<pre>&#0160;&#0160;&#0160; ],</pre>
<pre>&#0160;&#0160;&#0160; &quot;policyKey&quot; : &quot;transient&quot;</pre>
<pre>}</pre>
<p><strong>Q: How do I enumerate the objects in my buckets?</strong></p>
<p>A: Currently there is no such APIs to iterate through the bucket. Best practice is maintaining a local database, saving the object list into your local database before uploading it.</p>
<p><strong>Q: How do I delete a bucket?</strong></p>
<p>A: Currently there is no public APIs to do that. You can use the undocumented &quot;Delete&quot; REST API to delete a bucket at your own risk.</p>
<p><strong>Q: How do I delete an object in one bucket?</strong></p>
<p>A: Currently there is no public APIs to do that.You can use the undocumented &quot;Delete&quot; REST API to delete a bucket at your own risk.</p>
<p><strong>Q: If you upload same file into same bucket, will it overwrite the existing one?</strong> <strong>Does it support versioning?</strong></p>
<p>A: The Upload API supports uploading objects as a single file (the entire POST body is treated as file content) as well as resumable uploads for large files. If you are uploading object using single file post,&#0160; and during upload it is determined that the bucket key and object key combination already exists, then the uploaded content will overwrite what is already in this bucket key / object key location. OSS does not support versioning.</p>
<p><strong>Q: What happened to my data in persistent bucket if I do not need it anymore?</strong></p>
<p>A: Object Storage Service(OSS) still retains the right to archive items in a persistent bucket that have not been accessed in 2 years. Objects of this age will be archived, and the applications using Persistent buckets, will need to handle the archived response and method of retrieving.</p>
<p><strong>Q: Will my transient bucket or temporary bucket be removed after 24 hours or 30 days?</strong></p>
<p>A: No. Buckets themselves never expire, only the objects in them do. &#0160;An object would expire after 24 hours in a transient bucket , or after 30 days if it is in a temporary bucket.</p>
<p><strong>Q : Will my translated viewable contents also expire if they are translated from transient or temporary bucket?&#0160;</strong></p>
<p>A: No, translated viewable contents(knows as derivatives) do not expire with the seed objects in transient or temporary bucket.</p>
<p>&#0160;</p>

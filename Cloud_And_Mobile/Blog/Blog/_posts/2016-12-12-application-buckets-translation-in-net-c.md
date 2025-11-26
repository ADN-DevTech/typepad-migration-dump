---
layout: "post"
title: "Application buckets & Translation in .NET C#"
date: "2016-12-12 04:12:30"
author: "Augusto Goncalves"
categories:
  - "ASP .NET"
  - "Augusto Goncalves"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/application-buckets-translation-in-net-c.html "
typepad_basename: "application-buckets-translation-in-net-c"
typepad_status: "Publish"
---

<p>By <a href="https://twitter.com/augustomaia">Augusto Goncalves</a></p>
<p>Yet another project that uses Autodesk Forge implemented in .NET C#: this time using application buckets on your webapp.&#0160;</p>
<p>This solution consists in 3 projects: a class library that wraps some OSS and Model Derivative APIs, plus Authentication; a ASP.NET project that implement WebAPI endpoints with a pure HTML5 &amp; JavaScript UI (no .aspx pages); and finally a Testing project.&#0160;</p>
<p>Most libraries have a raw implementation of endpoints, so this sample tries to organize them in a meaningful way: first instantiate a app buckets, from where you can either get a list of buckets or create new ones, then using this bucket object you can upload and translate files. As everything is strongly typed (i.e. no random strings), this approach helps avoid problems passing wrong types.</p>
<p>Please note this is not our official library, but yet an experiment that still under development, so <strong>feedback is very welcome</strong>!&#0160;</p>
<p>Below is a sample piece of code that demonstrate the idea:</p>
<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #008000;">// authenticate</span>
OAuth.OAuth oauth = <span style="color: #0000ff;">await</span> OAuth2LeggedToken.AuthenticateAsync(<span style="color: #a31515;">&quot;Your client ID&quot;</span>, <span style="color: #a31515;">&quot;Your client secret&quot;</span>,
<span style="color: #0000ff;">new</span> Scope[] { Scope.BucketRead, Scope.BucketCreate, Scope.DataRead, Scope.DataCreate, Scope.DataWrite });

<span style="color: #008000;">// create bucket and get list of buckets in different conditions</span>
AppBuckets app = <span style="color: #0000ff;">new</span> AppBuckets(oauth);
IEnumerable&lt;Bucket&gt; buckets = <span style="color: #0000ff;">await</span> buckets.GetBucketsAsync(10);

<span style="color: #008000;">// create a random bucket</span>
<span style="color: #2b91af;">string</span> bucketKey = <span style="color: #2b91af;">string</span>.Format(<span style="color: #a31515;">&quot;test{0}&quot;</span>, DateTime.Now.ToString(<span style="color: #a31515;">&quot;yyyyMMddHHmmss&quot;</span>));
Bucket bucket = <span style="color: #0000ff;">await</span> app.CreateBucketAsync(bucketKey, PolicyKey.Transient);

<span style="color: #008000;">// upload new object</span>
OSS.Object newObject = <span style="color: #0000ff;">await</span> bucket.UploadObjectAsync(testFile);
<span style="color: #008000;">// this URN can be used on the viewer</span>
<span style="color: #008000;">// but need to translate first...</span>
<span style="color: #2b91af;">string</span> newObjectURN = newObject.ObjectId.Base64Encode();

<span style="color: #008000;">// the list after should have 1 object...</span>
IEnumerable&lt;OSS.Object&gt; objectsAfter = <span style="color: #0000ff;">await</span> bucket.GetObjectsAsync(<span style="color: #2b91af;">int</span>.MaxValue);
<span style="color: #0000ff;">foreach</span> (OSS.Object obj <span style="color: #0000ff;">in</span> objectsAfter)
{
    <span style="color: #2b91af;">string</span> urn = obj.ObjectId;
}

<span style="color: #008000;">// translate</span>
HttpStatusCode res = <span style="color: #0000ff;">await</span> newObject.Translate(<span style="color: #0000ff;">new</span> SVFOutput[] { SVFOutput.Views3d, SVFOutput.Views2d });
<span style="color: #008000;">// now this newObject is ready for Viewer</span>
</pre>
</div>

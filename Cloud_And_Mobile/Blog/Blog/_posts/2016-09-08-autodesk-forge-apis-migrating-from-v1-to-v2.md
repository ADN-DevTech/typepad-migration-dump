---
layout: "post"
title: "Autodesk Forge APIs: Migrating from v1 to v2"
date: "2016-09-08 05:11:15"
author: "Shiya Luo"
categories:
  - "Data Management"
  - "Forge"
  - "Model Derivative"
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/09/autodesk-forge-apis-migrating-from-v1-to-v2.html "
typepad_basename: "autodesk-forge-apis-migrating-from-v1-to-v2"
typepad_status: "Publish"
---

<p>By <a href="https://twitter.com/ShiyaLuo">@ShiyaLuo</a></p>
<p>Forge DevCon announced a new version of APIs to upload, translate and view your CAD files. Many of our early adopters have started with developing with View and Data API, which is actually a set of RESTful APIs along with a JavaScript viewer. This post will focus on how to migrate from v1 to v2. It’s not going to talk about the new features of v2 that v1 did not implement, just a guide to change your code from v1 so it will run with v2 API when v1 is eventually deprecated.</p>
<p>In v2, the View and Data API have separated into three parts:</p>
<ol>
<li>Data Management API</li>
<li>Model Derivative API</li>
<li>Viewer</li>
</ol>
<h2><a id="Scoped_Tokens_9"></a>Scoped Tokens</h2>
<p>First and foremost, the biggest change that is going to affect the most number of developers is scoped tokens. After the Forge DevCon, every new app created will required a scoped token. A scoped token is retrieved when you include a scope value in the body of a <code>x-www-form-urlencoded</code> string.</p>
<h3><a id="v1_12"></a>v1</h3>
<p>In v1, a token is requested like this:</p>
<pre><code>POST /authentication/v1/authenticate HTTP/1.1
Host: developer.api.autodesk.com
Content-Type: application/x-www-form-urlencoded

client_id=YOUR-ID&amp;client_secret=YOUR-SECRET&amp;grant_type=client_credentials
</code></pre>
<h3><a id="v2_23"></a>v2</h3>
<p>In v2, add the scope of the token you’d like to retrieve. There’s <a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/scopes/">a detailed post about scopes</a>. The best practice is to only include the scopes you need, especially not to use a data:write or bucket:write scoped token in the client-side viewer.</p>
<pre><code>POST /authentication/v1/authenticate HTTP/1.1
Host: developer.api.autodesk.com
Content-Type: application/x-www-form-urlencoded

client_id=YOUR-ID&amp;client_secret=YOUR-SECRET&amp;grant_type=client_credentials&amp;scope=data%3Aread+data%3Acreate+data%3Awrite+bucket%3Aread+bucket%3Acreate
</code></pre>
<h2><a id="Object_Storage_Service_OSS_34"></a>Object Storage Service (OSS)</h2>
<p>OSS in the View and Data API is now under the <a href="https://developer.autodesk.com/en/docs/data/v2/overview/">Data Management API</a>.</p>
<p>Migrating from v1 to v2 for APIs from OSS is fairly straight forward. The paths did not change except for version numbers. For example, create a bucket used to have a path of <code>/oss/v1/buckets</code>, it just changed to <code>/oss/v2/buckets</code>.</p>
<p>Scopes required in OSS is usually <code>bucket:create</code> or <code>bucket:read</code>.</p>
<h2><a id="Reference_Service_41"></a>Reference Service</h2>
<p>Reference service is not an API you call anymore with v2. You no longer have to upload individual files, set references and translate the master file. With v2, just upload the files as a zip file, and declare the root file name in the body. <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/">A more detailed explanation</a> is posted on the developer portal.</p>
<h2><a id="Viewing_Service_44"></a>Viewing Service</h2>
<p>Viewing service is now under <a href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/">model derivative API</a>. The paths have changed from <code>/viewingservice/</code> to <code>/modelderivative/</code>.</p>
<p>To migrate from v1 to v2, change the path to the following APIs:</p>
<ol>
<li>Supported formats</li>
</ol>
<ul>
<li>v1: <code>GET /viewingservice/v1/supported</code></li>
<li>v2: <code>GET /modelderivative/v2/designdata/formats</code></li>
</ul>
<ol start="2">
<li>
<p>Register for translation (Post a job)</p>
<p>The register for translation API change since we can now translate one file formats into multiple different outputs, as opposed to let viewer 3D consume it.</p>
</li>
</ol>
<ul>
<li>
<p>v1: <code>POST /viewingservice/v1/register</code></p>
<p>request body:</p>
<pre><code>{
  &quot;urn&quot;: &quot;{{base64Urn}}&quot;
}
</code></pre>
</li>
<li>
<p>v2: <code>POST /modelderivative/v2/designdata/job</code></p>
<p>request body:</p>
<pre><code>{
  &quot;input&quot;: {
      &quot;urn&quot;: &quot;{{base64Urn}}&quot;
  },
  &quot;output&quot;: {
      &quot;destination&quot;: {
          &quot;region&quot;: &quot;us&quot;
      },
      &quot;formats&quot;: [
      {
          &quot;type&quot;: &quot;svf&quot;,
          &quot;views&quot;:[&quot;2d&quot;, &quot;3d&quot;]
      }]
  }
}
</code></pre>
</li>
</ul>
<p>Happy coding!</p>

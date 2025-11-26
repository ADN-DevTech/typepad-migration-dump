---
layout: "post"
title: "List Bucket API Now Available in Production for View and Data API"
date: "2015-12-13 17:00:00"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/12/list-bucket-api-now-available-in-production-for-view-and-data-api.html "
typepad_basename: "list-bucket-api-now-available-in-production-for-view-and-data-api"
typepad_status: "Publish"
---

<p>Hi guys, the List Bucket API is now available.</p>
<p>List Bucket API is a GET request that lists the buckets you have created with your account.&#0160;</p>
<p>The format is:</p>
<pre><code>GET /oss/v2/buckets[?region={region}&amp;limit={limit}&amp;startAt={startAt}]</code></pre>
<p>region</p>
<p>is the region where the bucket resides. Currently defaults to the US. This function will be more useful when we start storing data in different continents for better performance.</p>
<p>limit</p>
<p>is the number of buckets you&#39;d like to retrieve from this request. Minimum 1, maximum 100. Defaults to 10.&#0160;</p>
<p>startAt</p>
<p>is the point where you continue from. No default value. Used for pagination.</p>
<h3>Sample request:</h3>
<pre><code>GET /oss/v2/buckets?limit=10
Host: developer.api.autodesk.com
Authorization: Bearer YOUR_TOKEN<br /></code></pre>
<h3>Sample Response:</h3>
<pre><code>Status: 200 OK<br />{
  &quot;items&quot;: [
    {
      &quot;bucketKey&quot;: &quot;test-bucket-000&quot;,
      &quot;createDate&quot;: 1445334118731,
      &quot;policyKey&quot;: &quot;temporary&quot;
    }
  ]
}</code></pre>

---
layout: "post"
title: "Setting the scope for the access token"
date: "2016-07-04 04:47:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Web Development"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/setting-the-scope-for-the-token.html "
typepad_basename: "setting-the-scope-for-the-token"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>One thing that sometimes people miss is that the scope needs to be set as a single string with the scope values separated by space - instead of as a list of values.</p>
<p>If you do the following then it will succeed, but the scope values will be ignored:</p>
<pre>curl \
-v "https://developer.api.autodesk.com/authentication/v1/authenticate" \
-X "POST" \
-H "Content-Type: application/x-www-form-urlencoded" \
-d "client_id=&lt;client id&gt;&amp;client_secret=&lt;client secret&gt;&amp;grant_type=client_credentials&amp;scope(0)=bucket:create&amp;scope(1)=bucket:read&amp;scope(2)=data:write"</pre>
<p>After this if you try to do something like this:</p>
<pre>curl \
-v "https://developer.api.autodesk.com/oss/v2/buckets" \
-X "POST" \
-H "Content-Type: application/json" \
-H "Authorization: Bearer &lt;token&gt;" \
-d "{\"bucketKey\":\"mynewbucket\",\"policyKey\":\"transient\"}"</pre>
<p>Then you will get a reply like&nbsp;this:</p>
<pre>Token scope not set. This request does not have the required privilege.</pre>
<p>But if you set the required scopes the correct way in a single string then all will be fine. You just have to URL encode the space characters to %20:</p>
<pre>curl \
-v "https://developer.api.autodesk.com/authentication/v1/authenticate" \
-X "POST" \
-H "Content-Type: application/x-www-form-urlencoded" \
-d "client_id=&lt;client id&gt;&amp;client_secret=&lt;client secret&gt;&amp;grant_type=client_credentials&amp;scope=bucket:create%20bucket:read%20data:write"</pre>
<p>&nbsp;</p>

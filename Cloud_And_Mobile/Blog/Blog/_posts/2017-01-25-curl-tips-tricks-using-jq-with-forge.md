---
layout: "post"
title: "cURL Tips & Tricks Using JQ with Forge"
date: "2017-01-25 12:16:49"
author: "Jaime Rosales"
categories:
  - "Data Management"
  - "Forge"
  - "Jaime Rosales Duque"
  - "Other"
  - "Script"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/curl-tips-tricks-using-jq-with-forge.html "
typepad_basename: "curl-tips-tricks-using-jq-with-forge"
typepad_status: "Publish"
---

<p>by Jaime Rosales (<a href="https://twitter.com/AfroJme" rel="noopener noreferrer" target="_blank">@afrojme</a>)</p>
<p>For quite sometime I&#39;ve been playing with cURL and the Forge Platform, and to start this 2017 I will be posting about my research using cURL and JQ processor together with the Forge Platform. &#0160;</p>
<p>Using Terminal I found a quicker and simpler way for myself to use different workflows when using the Authentication API, such actions as obtaining 2 legged &amp; 3 legged access tokens. With the Data Management API, I&#39;ve been able to create, upload, request translation, access to Hubs and add new files to specific projects that I&#39;m part of. Everything from the Terminal using cURL. But I know what you can be thinking &quot;<em>cURL from terminal, is so unorganized, so hard to read, so easy to mess up.</em>&quot; I could not agree more with some of these thoughts. When your response has more than 1 line of JSON data returned, I agree it can be messy and hard to read.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2586050970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2017-01-25 at 3.02.41 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2586050970c image-full img-responsive" src="/assets/image_860172.jpg" title="Screen Shot 2017-01-25 at 3.02.41 PM" /></a></p>
<p>I know others have decided to use REST Apps such as Paw or Postman, since the fear of messing up one character in your cURL can give you problems and at the same time, you get a better organized JSON result. But bare with me,&#0160;I found out about JQ while using cURL and since then It has made quite a difference when testing the api&#39;s.&#0160;</p>
<p>What is JQ?&#0160;jq is a lightweight and flexible command-line JSON processor. A jq program is a “filter”: it takes an input, and produces an output. There are a lot of builtin filters for extracting a particular field of an object, or converting a number to a string, or various other standard tasks. It lets you visualize the JSON response in a organized way and it becomes easier to read when using terminal. How to use it? It requires a basic installation, JQ can be download for different OS platforms from <a href="https://stedolan.github.io/jq/download/">here</a>. After installation has been performed you should restart your Terminal and you would be ready for testing. You can check your version of JQ by simply typing &quot;jq --version&quot; in your terminal, which will assure you JQ was successfully installed.</p>
<p>Let&#39;s look at how the previous cURL actions to obtain a 2 legged access token and create a bucket looks after using JQ from Terminal.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d258605e970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2017-01-25 at 3.18.48 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d258605e970c image-full img-responsive" src="/assets/image_ea3303.jpg" title="Screen Shot 2017-01-25 at 3.18.48 PM" /></a></p>
<p>As we can see JQ structures and color codes the result JSON from our REST call using cURL. Later on I will be posting the entire workflow on how to start from obtaining a 2 legged access token up to translating a file and get back the URN ready to be displayed in the Viewer. Followed by a 3rd post on how to access my a360 hubs and add a file to my project or another project I&#39;m part of, all using cURL and the JQ processor.&#0160;</p>
<p>Thank you for reading.&#0160;</p>
<p class="p1">&#0160;</p>

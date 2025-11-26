---
layout: "post"
title: "Encode Filename When Uploading with View and Data API"
date: "2015-01-21 23:18:46"
author: "Daniel Du"
categories:
  - "ASP .NET"
  - "Browser"
  - "Daniel Du"
  - "Javascript"
  - "Script"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/01/encode-filename-when-uploading-with-view-and-data-api.html "
typepad_basename: "encode-filename-when-uploading-with-view-and-data-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>We had some issue with our viewer live sample - <a href="http://sapdemo.autodesk.io/">SAP integration demo</a> - last week. After investigation, it turns out that it is just due to a small annoy thing – the encoding of filename. I am sharing my experience so that you don’t walk into the same issue latter.</p>  <p>To view the model, we need to upload the model file to Autodesk cloud, and register it for translation. The file identifier in cloud will be an URL like baseurl/bucket/{bucket_key}/objects/{object_key}. Generally speaking, the object key is the file name of model. As you know, we need to encode the filename to serve as object key, right? </p>  <p>I firstly use following JavaScript code to do encoding: </p>  <p>var objectKey = escape(filename);</p>  <p>In most time, it works just fine. But recently, we run into trouble, because one filename has a special character plus (‘+’). escape() does not encode the ‘+’ character. Following are some testing: </p>  <p>var filename = 'file name + somethig.txt';   <br />console.log(<strong>escape</strong>(filename));    <br />&gt;&#160; file%20name%20+%20somethig.txt    <br />console.log(<strong>encodeURI</strong>(filename));     <br />&gt;&#160; file%20name%20+%20somethig.txt    <br />console.log(<strong>encodeURIComponent</strong>(filename));&#160; <br />&gt;&#160; file%20name%20%2B%20somethig.txt</p>  <p>Form the result, you notice that only <strong>encodeURIComponent</strong> encodes the plus sign(‘+’) to “%2B”. So we need to use encodeURIComponent to encode filenames when uploading with View and Data API, otherwise you will run into problem. Not a big deal but it takes time to figure out why :s</p>  <p>If you encode from server side with C#, the commonly used HttpUtility.UrlEncode() is also problematic for this scenario. We should use <strong>Uri.EscapeDataString</strong>() instead.</p>  <p>Hope this helps some if you run into the same issue. </p>

---
layout: "post"
title: "How to Set References with Revit Files for View and Data API"
date: "2015-07-31 11:23:23"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/how-to-set-references-with-revit-files-for-view-and-data-api.html "
typepad_basename: "how-to-set-references-with-revit-files-for-view-and-data-api"
typepad_status: "Publish"
---

<p style="text-align: left;">By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/shiya-luo.html">Shiya Luo</a></p>
<p>When you have multiple files for the same model, you’ll have to upload the files and link them together to display them all with the View and Data. Thus we have the Reference API.</p>
<p>This post will walk you through how to set reference for Revit Files.</p>
<p>To display your entire model with multiple files, here are the steps you’ll have to take:<br />1. upload the individual files to your bucket<br />2. link the files together using reference API<br />3.&#0160;register them individually for translation</p>
<p>Here are a few helpful resources to use to get this working:<br />Use Postman or some other REST API tools, or this <a href="http://developer-autodesk.github.io/LmvQuickStart" target="_self">interactive guide</a> that contains the workflow of View and Data APIs.<br />Read the <a href="https://developer.autodesk.com/api/view-and-data-api/" target="_self">documentation</a>.</p>
<h2>1. A very simple Revit File</h2>
<p>To demonstrate how to do this, I drew a very simple Revit drawing with a room and a wall. The file “basic_walls.rvt” are the four outside walls and the file “full_project.rvt” contains the room divider and the door in the middle of the room.</p>
<p>Here&#39;s how the model looks like in Revit:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b21a26970b-pi" style="display: inline;"><img alt="Screen Shot 2015-07-21 at 2.24.05 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b21a26970b image-full img-responsive" src="/assets/image_0f7e1d.jpg" title="Screen Shot 2015-07-21 at 2.24.05 PM" /></a></p>
<p><a href="http://adndevblog.typepad.com/files/simple-revit-drawing.zip">Download simple-revit-drawing</a></p>
<p>The files are also linked in Revit:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b21bfb970b-pi" style="display: inline;"><img alt="Screen Shot 2015-07-21 at 2.35.43 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b21bfb970b img-responsive" src="/assets/image_add8a8.jpg" title="Screen Shot 2015-07-21 at 2.35.43 PM" /></a></p>
<p>&#0160;</p>
<div>Upload the files individually, you should get a response where the id is the urn of the file you just uploaded.&#0160;Next you need to register each file individually for translation, by sending base64 encoded urn.&#0160;After all the files are uploaded you&#39;re ready to link the files together with reference API.</div>
<p>With the reference API, you need to specify the master file, and all the children files associated with.</p>
<p>With this file the particular request looks like this:</p>
<pre><span style="font-size: 10pt;"><code>
POST /references/v1/setreference HTTP/1.1
Host: developer.api.autodesk.com
Authorization: Bearer 7SMViMGoOwsDvVnpocAJIddNFBIE
Content-Type: application/json

{
&quot;master&quot; : &quot;urn:adsk.objects:os.object:shiyas-bucket-10/full_project.rvt&quot;,
&quot;dependencies&quot; : [
  { &quot;file&quot; : &quot;urn:adsk.objects:os.object:shiyas-bucket-10/basic_walls.rvt&quot;,
    &quot;metadata&quot; : {
        &quot;childPath&quot; : &quot;basic_walls.rvt&quot;,
        &quot;parentPath&quot; : &quot;full_project.rvt&quot;
    }
  }
 ]
}</code></span></pre>
<p>If the request was successful, the response should be 200 with no body.</p>
<p>After setting the reference, register the individual urns for translation. Remember to base64 encode the urn.</p>
<p>&#0160;Now test it by viewing! Go to the interactive guide, put your token in step 3 and base64 encoded urn in step 13, the model should render!</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b29e58970b-pi" style="display: inline;"><img alt="Screen Shot 2015-07-22 at 2.30.57 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b29e58970b image-full img-responsive" src="/assets/image_493c6d.jpg" title="Screen Shot 2015-07-22 at 2.30.57 PM" /></a></p>
<h2>2. Multi-leveled Revit File</h2>
<p>This time I added some walls and linked them with attachment.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0856b6a4970d-pi" style="display: inline;"><img alt="Screen Shot 2015-07-22 at 3.12.44 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0856b6a4970d image-full img-responsive" src="/assets/image_6683ce.jpg" title="Screen Shot 2015-07-22 at 3.12.44 PM" /></a></p>
<p>The relationship is like so:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b2a209970b-pi" style="display: inline;"><img alt="Screen Shot 2015-07-22 at 3.18.02 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b2a209970b img-responsive" src="/assets/image_23a0f5.jpg" title="Screen Shot 2015-07-22 at 3.18.02 PM" /></a>&#0160;</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb085bf5cf970d img-responsive"><a href="http://adndevblog.typepad.com/files/multi-tiered-revit-files.zip">Download Multi-tiered-revit-files</a></span></p>
<p>When there are more than one tier, the reference needs to be set at each level. The request body looks like this:&#0160;</p>
<pre><span style="font-size: 10pt;"><code>{
&quot;master&quot; : &quot;urn:adsk.objects:os.object:shiyas-bucket-12345678/full_project.rvt&quot;,
&quot;dependencies&quot; : [
  { &quot;file&quot; : &quot;urn:adsk.objects:os.object:shiyas-bucket-12345678/basic_walls.rvt&quot;,
    &quot;metadata&quot; : {
        &quot;childPath&quot; : &quot;basic_walls.rvt&quot;,
        &quot;parentPath&quot; : &quot;full_project.rvt&quot;
    }
  }
 ]
}

{
&quot;master&quot; : &quot;urn:adsk.objects:os.object:shiyas-bucket-12345678/basic_walls.rvt&quot;,
&quot;dependencies&quot; : [
  { &quot;file&quot; : &quot;urn:adsk.objects:os.object:shiyas-bucket-12345678/more_walls.rvt&quot;,
    &quot;metadata&quot; : {
        &quot;childPath&quot; : &quot;more_walls.rvt&quot;,
        &quot;parentPath&quot; : &quot;basic_walls.rvt&quot;
    }
  }
 ]
}<br /></code></span></pre>
<p>&#0160;</p>
<p>After setting the reference, register the files for translation. When translation is finished, put the urn through the viewer to test, it should look like this:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b7efe4970b-pi" style="display: inline;"><img alt="Screen Shot 2015-07-31 at 11.13.42 AM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b7efe4970b image-full img-responsive" src="/assets/image_bedd8d.jpg" title="Screen Shot 2015-07-31 at 11.13.42 AM" /></a></p>
<h2>Conclusion</h2>
<p>This is how you would set reference for Revit files for the View and Data API. The main takeaways from this post is:</p>
<p>1. set references before translating the files.<br />2. set references tier by tier, not all at once.</p>
<p>Ask questions here in the comments or on the <a href="http://forums.autodesk.com/t5/view-and-data-api/bd-p/95" target="_self">forum</a> if there are any questions.</p>

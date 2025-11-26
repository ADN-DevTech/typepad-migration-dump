---
layout: "post"
title: "How to Set References with Inventor Files for View and Data API"
date: "2015-09-25 17:52:02"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/09/how-to-set-references-with-inventor-files-for-view-and-data-api.html "
typepad_basename: "how-to-set-references-with-inventor-files-for-view-and-data-api"
typepad_status: "Publish"
---

<p style="text-align: left;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/shiya-luo.html">Shiya Luo</a></p>
<p style="text-align: left;">Outputs from Inventor can have one file, or multiple files with one master assembly and one or more sub-assemblies. These sub-assemblies can also be master assembly for other assemblies.&nbsp;</p>
<p style="text-align: left;">This post will walk you through how to set references with Inventor files that contains multiple assembly files with some examples.</p>
<p style="text-align: left;">Here I have downloaded Inventor 2016 Sample Files&nbsp;from the&nbsp;<a href="http://knowledge.autodesk.com/support/inventor-products/downloads/caas/downloads/content/inventor-sample-files.html" target="_self">Autodesk Inventor Sample Files Page</a>.&nbsp;</p>
<p style="text-align: left;">To display your entire model with multiple files, here are the steps you’ll have to take:<br />1. upload the individual files to your bucket<br />2. link the files together using reference API<br />3.&nbsp;register them individually for translation</p>
<p style="text-align: left;">Here are a few helpful resources to use to get this working:<br />Use Postman or some other REST API tools, or this&nbsp;<a href="http://developer-autodesk.github.io/LmvQuickStart" target="_self">interactive guide</a>&nbsp;that contains the workflow of View and Data APIs.<br />Read the&nbsp;<a href="https://developer.autodesk.com/api/view-and-data-api/" target="_self">documentation</a>.</p>
<p>The example I'm demonstrating is&nbsp;Models/Assemblies/Scissors. Opening up scissors.iam in inventor you'll get something that looks like this:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086b5d37970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb086b5d37970d image-full img-responsive" title="Screen Shot 2015-08-31 at 2.47.11 PM" src="/assets/image_b591a4.jpg" alt="Screen Shot 2015-08-31 at 2.47.11 PM" border="0" /></a>&nbsp;</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d150fea3970c img-responsive">Here's a download link for those who aren't using windows and can't extract .exe:</span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d150fea3970c img-responsive"><a href="http://adndevblog.typepad.com/files/scissors.zip">Download Scissors</a></span></p>
<p>Here, the master file is "scissors.iam", which has three sub-assemblies or children files called "blade_main.ipt", "blade_top.ipt" and "scissor_spring.ipt". On the left panel in Inventor here is the relationship:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c70f09970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c70f09970b img-responsive" title="Screen Shot 2015-08-31 at 2.51.47 PM" src="/assets/image_91e4f4.jpg" alt="Screen Shot 2015-08-31 at 2.51.47 PM" border="0" /></a></p>
<p>One tiered relationship is easy, just set the parent file as master, and list all three dependencies like so:</p>
<pre class="brush:csharp;"><code>
POST /references/v1/setreference HTTP/1.1
Host: developer.api.autodesk.com
Authorization: Bearer &lt;access-token&gt;
Content-Type: application/json
Cache-Control: no-cache

{
"master" : "urn:adsk.objects:os.object:shiyas-bucket-100/scissors.iam",
"dependencies" : [
  { "file" : "urn:adsk.objects:os.object:shiyas-bucket-100/blade_main.ipt",
    "metadata" : {
        "childPath" : "blade_main.ipt",
        "parentPath" : "scissors.iam"
    }
  },
  { "file" : "urn:adsk.objects:os.object:shiyas-bucket-100/blade_top.ipt",
    "metadata" : {
        "childPath" : "blade_top.ipt",
        "parentPath" : "scissors.iam"
    }
  },
  { "file" : "urn:adsk.objects:os.object:shiyas-bucket-100/scissor_spring.ipt",
    "metadata" : {
        "childPath" : "scissor_spring.ipt",
        "parentPath" : "scissors.iam"
    }
  }
 ]
}
</code></pre>
<p>Each JSON object in the "dependencies" array should be a child of the parent assembly. If you've set your references correctly, you should get something that looks like this:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086b5e73970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb086b5e73970d image-full img-responsive" title="Screen Shot 2015-08-31 at 3.43.52 PM" src="/assets/image_24ad5f.jpg" alt="Screen Shot 2015-08-31 at 3.43.52 PM" border="0" /></a></p>

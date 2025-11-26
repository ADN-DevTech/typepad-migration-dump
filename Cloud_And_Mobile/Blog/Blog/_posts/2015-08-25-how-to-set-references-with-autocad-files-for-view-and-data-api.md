---
layout: "post"
title: "How to Set References with AutoCAD Files for View and Data API"
date: "2015-08-25 18:06:23"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/how-to-set-references-with-autocad-files-for-view-and-data-api.html "
typepad_basename: "how-to-set-references-with-autocad-files-for-view-and-data-api"
typepad_status: "Publish"
---

<p style="text-align: left;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/shiya-luo.html">Shiya Luo</a></p>
<p>AutoCAD files are connected to each other as xref (short for external references). Files are linked like a graph. File A can reference file B, which can reference file A back. It’s a bit meta, like reference inception. What’s different however, is that you only have to set the parent children relationship once, if two files reference each other, you can stop as long as you have linked the two files together once. The translation service will go in and look for the particular reference inside your .dwg files.&nbsp;</p>
<p>Again, the workflow is:</p>
<div>1.&nbsp;upload&nbsp;the&nbsp;individual&nbsp;files&nbsp;to&nbsp;your&nbsp;bucket</div>
<div>2.&nbsp;link&nbsp;the&nbsp;files&nbsp;together&nbsp;using&nbsp;reference&nbsp;API</div>
<div>3.&nbsp;register&nbsp;them&nbsp;individually&nbsp;for&nbsp;translation</div>
<div>&nbsp;</div>
<div>Similar to <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/07/how-to-set-references-with-revit-files-for-view-and-data-api.html" target="_self">set references for Revit files</a>, the tiers are done in different levels.</div>
<div>&nbsp;</div>
<p>Some useful resources before you start:<br />Use Postman or some other REST API tools, or this <a href="http://developer-autodesk.github.io/LmvQuickStart" target="_self">interactive guide</a> that contains the workflow of View and Data APIs.<br />Read the <a href="https://developer.autodesk.com/api/view-and-data-api/" target="_self">documentation</a>.&nbsp;</p>
<div>I have a multi-tierd AutoCAD file drawn. It looks like this in AutoCAD:&nbsp;</div>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7bdab80970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7bdab80970b image-full img-responsive" title="Screen Shot 2015-08-10 at 2.37.09 PM" src="/assets/image_bf141b.jpg" alt="Screen Shot 2015-08-10 at 2.37.09 PM" border="0" /></a></p>
<div>There are three different files here:&nbsp;</div>
<div><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0868b3d1970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0868b3d1970d img-responsive" title="Screen Shot 2015-08-25 at 5.26.05 PM" src="/assets/image_23b198.jpg" alt="Screen Shot 2015-08-25 at 5.26.05 PM" width="132" height="97" border="0" /></a></div>
<div>&nbsp;</div>
<div>
<div>Their relationship looks like this:&nbsp;</div>
<div><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0868b3dd970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0868b3dd970d img-responsive" title="AutoCAD" src="/assets/image_8dac7c.jpg" alt="AutoCAD" border="0" /></a></div>
<div>&nbsp;</div>
<div>&nbsp;</div>
<div>If you'd like to test with my files, here they are:&nbsp;</div>
<div><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb0868b546970d img-responsive"><a href="http://adndevblog.typepad.com/files/autocad_reference_sample.zip">Download AutoCAD_Reference_Sample</a></span></div>
<div>&nbsp;</div>
<div>First upload the files, then you can start setting references.</div>
<div>&nbsp;</div>
<div>Each tier requires a different request, the first one looks like this:</div>
<pre class="brush:csharp;">
POST /references/v1/setreference HTTP/1.1
Host: developer.api.autodesk.com
Authorization: Bearer &lt;your-token&gt;
Content-Type: application/json

{
    "master" : "urn:adsk.objects:os.object:shiyas-bucket-12345/Cylinder1.dwg",
    "dependencies" : [{ 
            "file" : "urn:adsk.objects:os.object:shiyas-bucket-12345/Cylinder2.dwg",
            "metadata" : {
                "childPath" : "Cylinder2.dwg",
                "parentPath" : "Cylinder1.dwg"
                }
        }
    ]
}
</pre>
<div>&nbsp;The second one,</div>
<pre class="brush:csharp;">
POST /references/v1/setreference HTTP/1.1
Host: developer.api.autodesk.com
Authorization: Bearer 
Content-Type: application/json
{
    "master" : "urn:adsk.objects:os.object:shiyas-bucket-12345/Box.dwg",
    "dependencies" : [{ 
            "file" : "urn:adsk.objects:os.object:shiyas-bucket-12345/Cylinder1.dwg",
            "metadata" : {
                "childPath" : "Cylinder1.dwg",
                "parentPath" : "Box.dwg"
                }
        }
    ]
}
</pre>
After setting the reference, fire them off for translation. In the end you should get something that looks like this:</div>
<div><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0868b599970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0868b599970d image-full img-responsive" title="Screen Shot 2015-08-10 at 4.50.03 PM" src="/assets/image_94700f.jpg" alt="Screen Shot 2015-08-10 at 4.50.03 PM" border="0" /></a></div>
<div>&nbsp;</div>
<p>Ask questions here in the comments or on the <a href="http://forums.autodesk.com/t5/view-and-data-api/bd-p/95" target="_self">forum</a> if you have any additional questions.</p>

---
layout: "post"
title: "Multiline Tool-tip in Autodesk Infrastructure Map Server / MapGuide Enterprise"
date: "2012-04-09 23:13:19"
author: "Partha Sarkar"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "MapGuide Enterprise 2011"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/multiline-tool-tip-in-autodesk-infrastructure-map-server-mapguide-enterprise.html "
typepad_basename: "multiline-tool-tip-in-autodesk-infrastructure-map-server-mapguide-enterprise"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p style="text-align: justify;">If you are looking for a way to create a multiline mouse over Tool tip for your map in Autodesk Infrastructure Map Server / MapGuide Enterprise, here is the trick.</p>
<p style="text-align: justify;">You can use '\n' to create a new line for mouse-over tool tip. Here is an example expression :</p>
<p style="text-align: justify;"><strong>concat("APN", concat('\nArea : ', concat("AREA", concat('\nAcres: ', concat("ACRES", concat('\nValue: $', ( "LAND_VALUE")))))))</strong></p>
<p style="text-align: justify;"><strong>&nbsp;</strong> <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016303eb7267970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016303eb7267970d image-full" title="Tool tip exp" src="/assets/image_db091d.jpg" border="0" alt="Tool tip exp" /></a><br /><br /><br />Here is the screenshot of multiline mouse over tool tip created using the above expression -</p>
<p style="text-align: justify;">&nbsp;</p>
<p style="text-align: justify;"><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016303eb7302970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016303eb7302970d image-full" title="Tooltip" src="/assets/image_7212a9.jpg" border="0" alt="Tooltip" /></a><br /><br /><br /></p>

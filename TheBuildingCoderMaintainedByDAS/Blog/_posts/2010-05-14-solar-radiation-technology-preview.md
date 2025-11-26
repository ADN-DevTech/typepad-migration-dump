---
layout: "post"
title: "Solar Radiation Technology Preview"
date: "2010-05-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "News"
  - "RME"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/05/solar-radiation-technology-preview.html "
typepad_basename: "solar-radiation-technology-preview"
typepad_status: "Publish"
---

<p>The 

<a href="http://labs.autodesk.com/utilities/ecotect">
solar radiation technology preview</a> for Revit 2011

is now available. 
It helps understand and quantify solar radiation on the surfaces of a conceptual building model.
This can help make informed design decisions about building shape, orientation, and shading strategies early on when changes are least expensive.

<p>A new version was just released, including many improvements and closer inntegrated in the Revit user interface:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133ed963403970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133ed963403970b image-full" alt="Solar radiation technology preview" title="Solar radiation technology preview" src="/assets/image_c4ef60.jpg" border="0"  /></a> <br />

</center>

<p>One aspect of interest to application developers is that this plug-in is written entirely using the public Revit API, in addition to the Solar Radiation logic from Ecotect wrapped in a library that is also used from the plug-in code. 
While there is no direct API access to call the functionality programmatically, it does showcase the new Revit 2011 APIs:

<ul>
<li>Dynamic Update API
<li>Analysis Visualization Framework
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/04/idling-event.html">Idling Event</a>
<li>Sun Path API
</ul>

<p>We will be discussing these in greater depth anon.</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013480c8f68b970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013480c8f68b970c" alt="Solar radiation" title="Solar radiation" src="/assets/image_cc1add.jpg" border="0"  /></a> <br />

</center>

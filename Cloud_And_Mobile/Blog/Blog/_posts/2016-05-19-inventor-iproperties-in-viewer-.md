---
layout: "post"
title: "Inventor iProperties in viewer "
date: "2016-05-19 03:57:24"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/05/inventor-iproperties-in-viewer-.html "
typepad_basename: "inventor-iproperties-in-viewer-"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>A developer just asked this question so here is the answer. When you upload an <strong>Inventor</strong>&#0160;<strong>part</strong> or <strong>assembly</strong>&#0160;file to our service then the <strong>iProperties</strong> with values will also be translated and made available in the viewer - easiest to test this in the <a href="https://a360.autodesk.com/viewer/">A360 viewer service</a>. However, when you are selecting objects in the viewer, by default you&#39;ll be picking the <strong>Solid</strong> objects which won&#39;t have those properties. You&#39;ll have to check the parent for the property. In this case I have added my custom <strong>iProperty</strong> named &quot;<strong>MyProp</strong>&quot; to my part file &quot;<strong>Holes2017b.ipt</strong>&quot; and when I select the correct component in the assembly tree then I can see it:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0902a194970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="IProperties" class="asset  asset-image at-xid-6a0167607c2431970b01bb0902a194970d img-responsive" src="/assets/image_ccf8d0.jpg" title="IProperties" /></a><br />&#0160;</p>
<p>&#0160;</p>

---
layout: "post"
title: "Manipulating layer filters using the -layer command"
date: "2012-05-28 08:19:57"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/manipulating-layer-filters-using-the-layer-command.html "
typepad_basename: "manipulating-layer-filters-using-the-layer-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I've seen that using the hidden option of the -_LAYER command called '_FI' I can create layer filters, but I did not find the exact property setting names to use.</p>
<p>Could you please list them?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>The layer filter related settings can be found in the Extension Dictionary of the Layer Table. <br />To check the options you could create a layer filter in the user interface and then look into the appropriate part of the database to see how the options have been stored. <br />You could use the ArxDbg.arx application for this, which is part of the ObjectARX SDK.</p>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016305eb7115970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016305eb7115970d image-full" alt="_arxdbg2" title="_arxdbg2" src="/assets/image_203044.jpg" border="0" /></a><br />
<p>I’ve created a filter where I set all options apart from Plot Style and got the following string: <br />"USED==\"TRUE\" AND VPOVERRIDES==\"FALSE\" AND NAME==\"A*\" AND OFF==\"False\" AND FROZEN==\"True\" AND LOCKED==\"True\" AND COLOR==\"10\" AND LINETYPE==\"DASHED\" AND LINEWEIGHT==\"0.18\" AND PLOTTABLE==\"True\" AND NEWVPFROZEN==\"True\"" <br />Once you know which options you want to use then you can place it in a LISP command like this: <br />(command "_LAYER" "_FI" "_N" "_P" "" "USED==\"TRUE\" AND NAME==\"A*\"" "MyFilter" "")</p>

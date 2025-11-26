---
layout: "post"
title: "CAdUiCoupledGroupCtrl and CAdUiGroupCtrl in a custom palette"
date: "2012-07-03 08:03:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/caduicoupledgroupctrl-and-caduigroupctrl-in-a-custom-palette.html "
typepad_basename: "caduicoupledgroupctrl-and-caduigroupctrl-in-a-custom-palette"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to add grouped controls to my custom palette, but when adding a CDialog derived window to the CAdUiGroupCtrl, CAdUiCoupledGroupCtrl does not seem to work properly. Also the dialog color does not match the AutoCAD palette color and the combo box placed on the dialog does not seem to send the selection changed messages. How could I solve these problems?</p>
<p><strong>Solution</strong></p>
<p>The attached sample (<span class="asset  asset-generic at-xid-6a0167607c2431970b016767d85312970b"><a href="http://adndevblog.typepad.com/files/mycustompalettevs2008.zip">Download Mycustompalettevs2008</a></span>) shows how you can do it.</p>
<p>It has a dialog panel called MyPanel derived from CDialog which is added directly to the palette and also to the grouped controls that the coupled groups control contain.</p>
<p>The dialog handles resizing and also overrides the color of the dialog to match the AutoCAD palette color using CAdUiThemeManager and CAdUiTheme.</p>
<p>It also shows messages in the Command Window when the selection changes in the combo or tree view control.</p>
<p>This is what it looks like:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017742b35ab3970d-pi" style="display: inline;"><img alt="Custompalette" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017742b35ab3970d" src="/assets/image_401069.jpg" title="Custompalette" /></a><br /><br /></p>

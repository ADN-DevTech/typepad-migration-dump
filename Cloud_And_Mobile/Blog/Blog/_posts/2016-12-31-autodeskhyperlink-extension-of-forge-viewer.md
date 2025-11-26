---
layout: "post"
title: "Autodesk.Hyperlink Extension of Forge Viewer"
date: "2016-12-31 19:45:23"
author: "Xiaodong Liang"
categories:
  - "Viewer"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/autodeskhyperlink-extension-of-forge-viewer.html "
typepad_basename: "autodeskhyperlink-extension-of-forge-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>&#0160;<img alt="" src="/assets/Forge%20Viewer-2.11-green.svg" /></p>
<p>Autodesk.Hyperlink Extension of Forge Viewer is a feature that can display the corresponding view of the tagged geometries. It is introduced since <strong>Forge Viewer 2.10</strong>.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09657a8a970d-pi" style="display: inline;"><img alt="Hyperlink" class="asset  asset-image at-xid-6a0167607c2431970b01bb09657a8a970d img-responsive" src="/assets/image_e0d690.jpg" title="Hyperlink" /></a></p>
<p>The gif below is a demoing.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09657a94970d-pi" style="display: inline;"><img alt="Forge-viewer-hyperlink" class="asset  asset-image at-xid-6a0167607c2431970b01bb09657a94970d img-responsive" src="/assets/image_c6b047.jpg" title="Forge-viewer-hyperlink" /></a></p>
<p>When we wanted to test the feature, we hit an issue: with the sample model of Revit (rac_advanced.rvt),the links are available, but they are external id only. Debugging the code, it looks it cannot find a corresponding bubble, so it treats it like a real hyperlink with ‘http://’. Obviously, ‘http://’ + external id is an invalid link.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096578ce970d-pi" style="float: left;"><img alt="Error-hyperlink" class="asset  asset-image at-xid-6a0167607c2431970b01bb096578ce970d img-responsive" src="/assets/image_c155bc.jpg" style="margin: 0px 5px 5px 0px;" title="Error-hyperlink" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>With the guidance of engineer team, we got to know to work with this feature, it is required the linked views have been published with the model.This is the<a href="https://knowledge.autodesk.com/support/revit-products/learn-explore/caas/CloudHelp/cloudhelp/2015/ENU/Revit-CAR/files/GUID-09FBF9E2-6ECF-447D-8FA8-12AB16495BC3-htm.html"> tutorial of publishing views</a>.</p>
<p><a class="asset-img-link" href="http://a4.typepad.com/6a016764cbbcf9970b01bb096579b4970d-pi"><img alt="View-for-a360" class="asset  asset-image at-xid-6a016764cbbcf9970b01bb096579b4970d img-responsive" src="/assets/image_d16d30.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="View-for-a360" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>In addition, we need to use the skeleton of initializing Forge Viewer by ViewingApplication, which can ensure to load the related bubbles.</p>
<p>By default, the extension is loaded with initialization. If not, we can explicitly load it:</p>
<p>Viewer.loadeExtension(&#39;Autodesk.Hyperlink&#39;);</p>
<p>&#0160;</p>

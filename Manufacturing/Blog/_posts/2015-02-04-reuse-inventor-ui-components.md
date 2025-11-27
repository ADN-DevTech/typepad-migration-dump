---
layout: "post"
title: "Reuse Inventor UI components"
date: "2015-02-04 17:11:41"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/reuse-inventor-ui-components.html "
typepad_basename: "reuse-inventor-ui-components"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You may want to create command dialogs or palettes that look just like <strong>Inventor</strong>&#39;s, e.g. like the one the&#0160;<strong>Extrude</strong> command has. <br />Unfortunately, there is no direct way to do that; <strong>Inventor API</strong> does not provide a way to reuse components or customize existing dialogs. <br />Also, the icons being used are spread across different resource dll&#39;s so it could be difficult to find them. The easiest way to create a dialog similar to an <strong>Inventor</strong> command&#39;s is to get a screenshot of the <strong>UI</strong> that you are interested in and then recreate from that the png&#39;s needed for your buttons.&#0160;</p>
<p>There is a sample in the <strong>SDK</strong> that does that: &quot;<strong>C:\Users\Public\Documents\Autodesk\Inventor 2015\SDK\DeveloperTools\Samples\VCSharp.NET\AddIns\CustomCommand</strong>&quot;</p>
<p>I have updated it to use the latest <strong>UI</strong> look, inc. making the entity selection button&#39;s arrow white once the selection has been accomplished, use png&#39;s instead of ico&#39;s, and added a nice command icon as well -&#0160; <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0cf2368970c img-responsive"><a href="http://adndevblog.typepad.com/files/customcommand2015.zip">Download CustomCommand2015</a></span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cf2381970c-pi" style="display: inline;"><img alt="Rackface" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0cf2381970c image-full img-responsive" src="/assets/image_e6d1c1.jpg" title="Rackface" /></a><br />&#0160;</p>
<p>&#0160;</p>

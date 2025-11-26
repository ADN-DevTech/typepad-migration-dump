---
layout: "post"
title: "Maya, 3ds Max, SoftImage, Mudbox, MotionBuilder 2014 for developers"
date: "2013-04-11 03:15:11"
author: "Cyrille Fauvel"
categories:
  - "3ds Max"
  - "Autodesk"
  - "Cyrille Fauvel"
  - "Maya"
  - "MotionBuilder"
  - "Mudbox"
  - "SoftImage"
original_url: "https://around-the-corner.typepad.com/adn/2013/04/maya-3ds-max-softimage-mudbox-motionbuilder-2014-for-developers.html "
typepad_basename: "maya-3ds-max-softimage-mudbox-motionbuilder-2014-for-developers"
typepad_status: "Publish"
---

<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d42b4fa00970c-pi" style="display: inline;"><img alt="3dsmax" class="asset  asset-image at-xid-6a0163057a21c8970d017d42b4fa00970c" src="/assets/image_66ea84.jpg" style="width: 100px;" title="3dsmax" /></a>&#0160;&#0160;<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017eea29357d970d-pi" style="display: inline;"><img alt="Maya" class="asset  asset-image at-xid-6a0163057a21c8970d017eea29357d970d" src="/assets/image_e51e4f.jpg" style="width: 100px;" title="Maya" /></a>&#0160;&#0160;<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017eea293707970d-pi" style="display: inline;"><img alt="Softimage" class="asset  asset-image at-xid-6a0163057a21c8970d017eea293707970d" src="/assets/image_fc9123.jpg" style="width: 100px;" title="Softimage" /></a></p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017eea293707970d-pi" style="display: inline;">
</a><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017eea293852970d-pi" style="display: inline;"><img alt="Mudbox" class="asset  asset-image at-xid-6a0163057a21c8970d017eea293852970d" src="/assets/image_c6d67c.jpg" style="width: 100px;" title="Mudbox" /></a>&#0160;&#0160;<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c3885f586970b-pi" style="display: inline;"><img alt="Motionbuilder" class="asset  asset-image at-xid-6a0163057a21c8970d017c3885f586970b" src="/assets/image_6e0ad8.jpg" style="width: 100px;" title="Motionbuilder" /></a>&#0160;&#0160;<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d42b4ffa8970c-pi" style="display: inline;"><img alt="Gameware" class="asset  asset-image at-xid-6a0163057a21c8970d017d42b4ffa8970c" src="/assets/image_5fa946.jpg" style="width: 100px;" title="Gameware" /></a></p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d42b4ffa8970c-pi" style="display: inline;"></a>Following the new &quot;origami&quot; theme</p>
<p><br />Tomorrow will be the first day where the&#0160;2014 family of Autodesk products will be available around the world.&#0160;You’ll no doubt find lots of information on the interwebs regarding the product’s features (including the <a href="http://area.autodesk.com/" target="_self">AREA</a>), so I’m going to focus specifically on the opportunities – and requirements – the new release presents to developers.</p>
<p>Firstly, ADN members should refer to the (as ever) thorough information produced by the ADN team and posted under the “Events” section of the ADN web site.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d42b47d1b970c-pi" style="display: inline;"><img alt="Holding-my-breath" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d42b47d1b970c" src="/assets/image_af0564.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Holding-my-breath" /></a><br />I have been holding my breath for couple of weeks now, but I think it is time now to publically talk about what&#39;s coming.</p>
<p>In today’s post we’ll take a high-level look at 3ds Max 2014 / Maya 2014 for developers, and then drill into a couple of the specific areas over the coming days/weeks.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c38858275970b-pi" style="display: inline;"><img alt="3dsmax-low" class="asset  asset-image at-xid-6a0163057a21c8970d017c38858275970b" src="/assets/image_accd2b.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="3dsmax-low" /></a></p>
<h3>Binary <span style="color: #00bf00;">compatible</span> release for 3ds Max</h3>
<p>The first thing to mention is that this release is binary application compatible with 3ds Max 2013. (If you’re not sure what this means,&#0160;<a href="http://around-the-corner.typepad.com/adn/2012/06/compatibility-of-maya-plug-ins-between-releases.html" target="_blank">this ancient – at least from this blog’s perspective – post</a>&#0160;should help.) Applications built for 3ds Max 2013 should just work with 3ds Max 2014. It’s also worth noting that we’re still targeting .NET Framework 4.0 with this release.</p>
<p>Reading between the lines, you should realise that we’re still building 3ds Max 2014 with the C++ compiler provided in Visual Studio 2010 SP1, for now (although we’re using the VS 2012 IDE with the 2010 platform toolset, in case that’s of interest to people).</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d42b48c33970c-pi" style="display: inline;"><img alt="Maya-low" class="asset  asset-image at-xid-6a0163057a21c8970d017d42b48c33970c" src="/assets/image_1f4cb7.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Maya-low" /></a></p>
<h3>Binary <span style="color: #ff0000;">break</span> release for Maya (as always)</h3>
<p>Unlike 3ds Max, Maya is not binary compatible, so you will need to recompile your C++ plug-ins to get them work on Maya 2014. If you are programming using Python, the only change is that Maya 2014 is now using the Python 2.7.3 vs 2.6.4, but that should be a transparent change unless you need additional Python modules such as PyQt. We’re still building Maya 2014 with the C++ compiler provided in Visual Studio 2010 SP1 for Windows, for now (although we’re using the VS 2012 IDE with the 2010 platform toolset, in case that’s of interest to people). And gcc 4.1.2 for Linux, and Xcode 3.2.1 (gcc 4.2.1) for Mac OSX.</p>
<h3>Exchange Store for applications</h3>
<p>I’m pleased to announce that the&#0160;<a href="http://apps.exchange.autodesk.com/" target="_blank">Autodesk Exchange store</a>&#0160;is now open to all for app submissions for 3ds Max and Maya 2014. The Autodesk Exchnage store isn&#39;t new, it exists for almost 2 years now, but tomorrow we will officially launch it for anyone to submit plug-ins, scripts, etc...</p>
<p> Click on the link below to visit the store.</p>
<p><a href="http://apps.exchange.autodesk.com/" target="_blank"><img alt="exchange1" border="0" height="85" src="/assets/image_bf2d00.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="exchange1" width="244" /></a></p>
<p>Once you’re in the store of your choice, sign in using your Autodesk Single-Sign-On account and click on the ‘publisher’ link to start submitting your app:</p>
<p><a href="http://apps.exchange.autodesk.com/Publisher/Description" target="_blank"><img alt="image001" border="0" height="106" src="/assets/image_21902e.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="image001" width="244" /></a></p>
<p>Next week, I&#39;ll blog to explain more on the requirements and how to prepare the application to be submitted.</p>
<p>&#0160;</p>

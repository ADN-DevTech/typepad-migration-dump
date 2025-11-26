---
layout: "post"
title: "Using augmented reality to preview the output of Project Pinocchio"
date: "2013-03-25 06:36:28"
author: "Cyrille Fauvel"
categories:
  - "Animation"
  - "Augmented reality"
  - "C++"
  - "Cyrille Fauvel"
  - "FBX"
  - "Modeling"
  - "Reality capture"
original_url: "https://around-the-corner.typepad.com/adn/2013/03/using-augmented-reality-to-preview-the-output-of-project-pinocchio.html "
typepad_basename: "using-augmented-reality-to-preview-the-output-of-project-pinocchio"
typepad_status: "Publish"
---

<p>A huge thanks to Kean Walmsley, who was previously working with me in the ADN group but now an AutoCAD API architect based in Neuchatel - Switzerland, and provide the original article for this post.</p>
<p><a href="http://projectpinocchio.autodesk.com/" target="_blank">Project Pinocchio</a> is available on <a href="http://labs.autodesk.com/" target="_blank">Autodesk Labs</a>. Project Pinocchio is an online character generator that helps you&#0160;<em>“create, customize, and download your very own rigged 3D characters from a catalog of over 100 body types, outfits, hairstyles, and physical attributes in a few simple steps.”</em></p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee9bab9e1970d-pi" style="display: inline;"><img alt="Login" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee9bab9e1970d image-full" src="/assets/image_9514c2.jpg" title="Login" /></a></p>
<p>Once you’ve signed in, it’s a simple process to create a character. You start by choosing your ancestor:</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee9babef5970d-pi" style="display: inline;"><img alt="Pinocchio1" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee9babef5970d image-full" src="/assets/image_956f5e.jpg" title="Pinocchio1" /></a><br />And you then work through and choose between various physical and clothing options… (I’ve used the “Edit” option on an existing character to go back through the options I’ve already chosen which is why the various pages are pre-populated).</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d4246c7ba970c-pi" style="display: inline;"><img alt="Pinocchio2" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d4246c7ba970c image-full" src="/assets/image_c8a354.jpg" title="Pinocchio2" /></a></p>
<p>Once all has been set up as you’d like, it’s time to publish. Project Pinocchio exports to .MB and .FBX formats. We want to get to .OBJ to load inside PointCloud Browser, so we’ll go for .FBX and make use of&#0160;<a href="http://usa.autodesk.com/adsk/servlet/pc/item?siteID=123112&amp;id=10775855" target="_blank">a converter to get to .OBJ</a>.</p>
<p>Here are the export options I chose:</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c38179cb9970b-pi" style="display: inline;"><img alt="Pinocchio3" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c38179cb9970b image-full" src="/assets/image_5c3658.jpg" title="Pinocchio3" /></a><br />Conversion to .OBJ is straightforward via the FBX converter tool for Mac or Windows:</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c38179d7a970b-pi" style="display: inline;"><img alt="Pinocchio_fbx" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c38179d7a970b image-full" src="/assets/image_3a397b.jpg" title="Pinocchio_fbx" /></a></p>
<p>Now we have an .OBJ it needs to be hosted on a web-server along with the .MTL material definition file and the corresponding texture map. One quirk I found: the version of PointCloud Browser I used crashed with a .JPG texture map, so I converted it to .PNG and adjusted the .MTL (which is a simple text format) to point to that, instead.</p>
<p>Here’s the texture map extracted from the .FBX by the conversion process (and if you think this looks freaky, try exporting without clothes: it looks&#0160;<em>really</em>&#0160;<a href="http://en.wikipedia.org/wiki/The_Silence_of_the_Lambs_(film)" target="_blank">Silence of the Lambs</a>):</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee9bacb39970d-pi" style="display: inline;"><img alt="Pinocchio4" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee9bacb39970d image-full" src="/assets/image_bc7935.jpg" title="Pinocchio4" /></a></p>
<p>Converting to PNG stopped the crash but didn’t result in the material being applied properly, so we still get something that isn’t completely satisfactory (see below for the results). Without being able to debug or run diagnostics on the PointCloud Browser tool, it’s hard to tell whether the material is now just being ignored because it’s not a .JPG. I’ve posted&#0160;<a href="http://forum.pointcloud.io/discussion/111/viewing-textured-meshes-from-obj-files-in-pointcloud-browser" target="_blank">a question</a>&#0160;on the PointCloud Browser forum, to see whether I’m missing something obvious (quite likely).</p>
<p>Here is&#0160;the slightly updated HTML page&#0160;that will display the Project Pinocchio-generated mesh inside a PointCloud Browser scene:</p>
<pre class="brush: html; toolbar: false;">&lt;!DOCTYPE html&gt;
&lt;html&gt;
  &lt;head&gt;
    &lt;title&gt;Project Pinocchio Preview&lt;/title&gt;
    &lt;meta name=&quot;viewport&quot; content=&quot;user-scalable=no, width=device-width, initial-scale=1.0, maximum-scale=1.0&quot; /&gt;
    &lt;meta name=&quot;viper-init-options&quot; content=&quot;manual&quot; /&gt;
    &lt;link
      rel=&quot;viper-app-icon&quot; type=&quot;images/png&quot;
      href=&quot;resources/images/appicon.jpg&quot; /&gt;
    &lt;script type=&quot;text/xml&quot; id=&quot;scene&quot;&gt;
      &lt;scene base=&quot;relative-baseplane&quot;&gt;
        &lt;light id=&quot;main_light&quot;
          intensity=&quot;1.0&quot;
          fade=&quot;constant&quot;
          ambient=&quot;0.2, 0.2, 0.2, 0.2&quot;
          diffuse=&quot;1.0, 1.0, 1.0, 1.0&quot;
          specular=&quot;1.0, 1.2, 1.2, 1.0&quot;
          position=&quot;3, 0.5, 2, 0&quot; /&gt;
        &lt;node
          id=&quot;character_node&quot;
          position=&quot;0, 0, 0&quot;
          rotation=&quot;90, 270, 0&quot;
          scale=&quot;0.04&quot;
          static=&quot;true&quot;&gt;
          &lt;model src=&quot;Sixth_Attempt.obj&quot; id=&quot;character_model&quot; /&gt;
        &lt;/node&gt;
      &lt;/scene&gt;
    &lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot;&gt;
      function onAppLoaded() {
        viper.requireRealityMap();
      }
    &lt;/script&gt;
  &lt;/head&gt;
  &lt;body/&gt;
&lt;/html&gt;
</pre>
<p>&#0160;And here&#39;s what we get when loaded inside the PointCloud Browser iOS app:</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee9bad2f7970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="float: left;"><img alt="Pinocchio5" class="asset  asset-image at-xid-6a0163057a21c8970d017ee9bad2f7970d" src="/assets/image_c85602.jpg" style="width: 154px; margin: 0px 5px 5px 0px;" title="Pinocchio5" /></a>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee9bad7e7970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="float: left;"><img alt="Pinocchio6" class="asset  asset-image at-xid-6a0163057a21c8970d017ee9bad7e7970d" src="/assets/image_8d74ca.jpg" style="width: 154px; margin: 0px 5px 5px 0px;" title="Pinocchio6" />
</a><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c3817b105970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="float: left;"><img alt="Pinocchio7" class="asset  asset-image at-xid-6a0163057a21c8970d017c3817b105970b" src="/assets/image_23eb1a.jpg" style="width: 154px; margin: 0px 5px 5px 0px;" title="Pinocchio7" /></a></p>
<p style="clear: both;">&#0160;</p>
<p>Without a material being applied, the results look a little like a cross between Gandhi and&#0160;<a href="http://en.wikipedia.org/wiki/T-1000" target="_blank">the T-1000 from Terminator 2</a>. Hopefully I’ll find out if there’s some way to get the material applied properly, at which point I’ll post an update.</p>
<p><strong><em>Update:</em></strong></p>
<p>I tried this again in the office, this morning, after changing the texture from .TGA to .PNG (after switching from the original .JPG). And it worked! I’m not sure whether the change I made was the difference or not – because I’m 90% certain I’d tried that, previously – but anyway.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c3817bea4970b-pi" style="display: inline;"><img alt="Pinocchio8" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c3817bea4970b" src="/assets/image_db64f6.jpg" title="Pinocchio8" /></a><br /><br /></p>

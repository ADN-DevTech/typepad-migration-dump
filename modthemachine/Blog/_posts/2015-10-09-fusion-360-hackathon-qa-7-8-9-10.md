---
layout: "post"
title: "Fusion 360 Hackathon - Q&A #7 #8 #9 #10"
date: "2015-10-09 01:45:22"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/10/fusion-360-hackathon-qa-7-8-9-10.html "
typepad_basename: "fusion-360-hackathon-qa-7-8-9-10"
typepad_status: "Publish"
---

<p>Here are the questions we got this week so far:&#0160;&#0160;</p>
<p><strong>Q: Is there a method to get the sketch to model transform (like in Inventor)?&#0160;</strong></p>
<p>A: Yes, the <strong>Sketch</strong> object has a <strong>transform</strong> property that provides the transformation matrix for exactly that.&#0160;</p>
<p><strong>Q: xyConstructionPlane. Where does it belong on the chart?&#0160;</strong></p>
<p>A: The chart isn’t able to show every connection to every object. The <strong>xyConstructionPlane</strong> is a property supported by the <strong>Component</strong> object that returns a <strong>ConstructionPlane</strong> object.&#0160;</p>
<p><strong>Q: How do you create a collection in case I need a specific group of objects to use as input to another command? For example in the UI to create a split button in the navigation toolbar the second argument is a “Collection” of command definitions. It does not seem to accept [cmdDef1, cmdDef2, cmdDef3] as the argument. Do you have to explicitly create a “collection” of command definitions?&#0160;</strong>&#0160;&#0160;</p>
<p>A: You can create a collection using <strong>adsk.core.ObjectCollection.create</strong>() function. Then you can add items to this collection using its <strong>add</strong>() method.&#0160;&#0160;</p>
<p><strong>Q: How to activate intelli-sense / auto-complete in Spyder on Mac?&#0160;</strong></p>
<p>A: It should be working by default, but it seems there is an issue with this in case of the MAS (Mac App Store) version of Fusion. Web install version for both Windows and Mac seem to work fine.&#0160;&#0160;The issue is logged and will be investigated.&#0160;</p>
<p><strong>Q: Since ImageCommandInputs display PNG’s at their native size – what happens on a hiDPI screen? Is the image automatically doubled in size?&#0160;</strong></p>
<p>A: This works similar to how you provide the images for your buttons. High definition images should be named in a way that they end with <strong>“@2x</strong>”, e.g. <strong>MyImage@2x.png</strong><a href="mailto:MyImage@2x.png"><br /></a></p>
<p>If you pass in <strong>MyImage.png</strong> as the parameter for the <strong>addImageCommandInput</strong>() function on a high definition display (e.g. <strong>MacBook</strong> with <strong>Retina</strong> display) <strong>Fusion</strong> will look for a file name ending with “@2x” (i.e. <strong>MyImage@2x.png</strong>) and if it exists it will use that. If it does not, then the low quality image will be scaled up.&#0160;</p>
<p>As an example, if I have these files in my <strong>resources</strong> folder:&#0160;</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb087ee54f970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Imageinput1" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb087ee54f970d img-responsive" src="/assets/image_542122.jpg" title="Imageinput1" /></a></p>
<p>… and I have this code&#0160;</p>
<pre>inputs.addImageCommandInput(&#39;imageInput&#39;, &#39;&#39;, &#39;./resources/<strong>32x32.png</strong>&#39;)&#0160;</pre>
<p>... and I’m running <strong>Fusion</strong> on a computer with high definition display, then &#39;<strong>./resources/32x32@2x.png</strong>&#39; will be used:&#0160;</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d164d5c2970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Imageinput2" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d164d5c2970c img-responsive" src="/assets/image_834446.jpg" title="Imageinput2" /></a></p>
<p>If the high definition image cannot be found then the low quality image will be stretched to the same size:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d164d5d2970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Imageinput3" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d164d5d2970c img-responsive" src="/assets/image_665105.jpg" title="Imageinput3" /></a></p>
<p><strong>Q: Can we use HTML styled text in message boxes?</strong>&#0160;</p>
<p>A: No. Message boxes do not support HTML for the contents. For more advanced formatting you will need to use a command dialog where the <strong>TextBoxCommandInput</strong> does support basic HTML formatting. Look for the <strong>TextBoxCommandInput</strong> section in the&#0160;<a href="http://help.autodesk.com/view/NINVFUS/ENU/?guid=GUID-3922697A-7BF1-4799-9A5B-C8539DF57051" target="_self">help topic for commands</a>&#0160;</p>
<p><strong>Q: Is there a way to retrieve the Fusion user’s email address registered with Autodesk?</strong></p>
<p>A: Not programmatically. But if you published your app on the Autodesk App Store, and&#0160;a user buys your app then you&#39;ll get an email with the user&#39;s email address in it. You can&#0160;also access this information later on from the &quot;Publisher Corner&quot; where you can get the download records emailed to you.</p>
<p><strong>Q: How to create a cone?</strong></p>
<p>A: There is no primitive for cones inside Fusion. You can create cone geometry in multiple ways.&#0160;One of which could be extruding a circle with a taper angle. No matter how the geometry&#0160;got created, when investigating it using the BREP API you should find the same things if&#0160;the created geometry is the same.&#0160;&#0160;</p>
<p><strong>Q: How to create the plugging-holes-with-corks sample shown during the webcast?</strong></p>
<p>A: I’m still working on this sample and will post the completed code when it’s finished.</p>
<p><strong>Q: How to get the modified JavaScript code to run in Fusion after editing it in Brackets?</strong></p>
<p>A: There is an issue on the Mac with the Apple web component we are using, because it&#0160;caches the original JavaScript file and keeps using that instead of the modified version. Apple said&#0160;they will work on fixing this issue. &#0160;</p>
<p><strong>Q: Does Fusion support compiled Python files (*.pyc)?</strong></p>
<p>A: Yes, it does.</p>
<p>-Adam</p>

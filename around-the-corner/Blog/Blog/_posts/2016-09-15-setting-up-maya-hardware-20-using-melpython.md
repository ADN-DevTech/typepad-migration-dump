---
layout: "post"
title: "Setting up Maya Hardware 2.0 using MEl/Python"
date: "2016-09-15 04:31:08"
author: "Vijaya Prakash"
categories:
  - "Maya"
  - "MEL"
  - "Python"
  - "Rendering"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/09/setting-up-maya-hardware-20-using-melpython.html "
typepad_basename: "setting-up-maya-hardware-20-using-melpython"
typepad_status: "Publish"
---

<p>Changing the render setting in Maya is straight forward, you can change the Hardware renderer very easily using Maya Render settings Window as you could see in the below image.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb0936d280970d-pi" style="display: inline;"><img alt="Hardware-render-settings (1)" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01bb0936d280970d img-responsive" src="/assets/image_ccead0.jpg" title="Hardware-render-settings (1)" /></a></p>
<p>But, how do you achieve the same thing using script code?<br /> <br /> If you execute the following code, you will see that Maya renders using &quot;Maya Software&quot;.</p>
<p>&#0160;</p>
<p><strong>mel.eval(&#39;render -x 600 -y 800 &quot;Render_Cam&quot;&#39;)</strong></p>
<p>or the same code as just MEL, Maya still renders using &quot;Maya Software&quot;.&#0160;In addition to the above code, the below python code is an attempt to change the renderer.</p>
<p>&#0160;</p>
<p><strong>cmds.setAttr(&#39;defaultRenderGlobals.ren&#39;, &#39;mayaHardware2&#39;, type=&#39;string&#39;)</strong></p>
<p><strong>mel.eval(&#39;loadPreferredRenderGlobalsPreset(&quot;mayaHardware2&quot;)&#39;)</strong></p>
<p>&#0160;</p>
<p>But Maya still renders using &quot;Maya software&quot;. &#0160;The problem is, we have to set the current renderer.&#0160;The following MEL commands will change the current renderer to Hardware Renderer 2.</p>
<p>&#0160;&#0160;<strong>setCurrentRenderer &quot;mayaHardware2&quot;;</strong></p>
<p><strong>&#0160;setAttr hardwareRenderingGlobals.renderMode 2 ;</strong></p>

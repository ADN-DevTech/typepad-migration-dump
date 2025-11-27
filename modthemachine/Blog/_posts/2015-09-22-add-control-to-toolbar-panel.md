---
layout: "post"
title: "Add control to toolbar panel"
date: "2015-09-22 09:50:41"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Add-In Creation"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/09/add-control-to-toolbar-panel.html "
typepad_basename: "add-control-to-toolbar-panel"
typepad_status: "Publish"
---

<p>It&#39;s worth downloading existing apps from the <a href="https://apps.autodesk.com/FUSION/en/Home/Index" target="_self">Fusion 360 exchange store</a>&#0160;even just to see if they already implement a certain functionality that you are interested in. In which case you can look at the source code (if it&#39;s a <strong>JavaScript</strong> or <strong>Python</strong> add-in) and see how things are done.</p>
<p>E.g. the <strong>ExactFlat</strong> add-in creates its own toolbar panel and promotes its single button control to that panel.</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c7d1f098970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Exactflat" class="asset  asset-image at-xid-6a00e553fcbfc6883401b7c7d1f098970b img-responsive" src="/assets/image_63785.jpg" title="Exactflat" /></a></p>
<p>It&#39;s done using this part of the <strong>Python</strong> code:</p>
<pre># &quot;toolbarControlPanel_&quot; is the button control added to the panel 
toolbarControlPanel_.isPromoted = True
toolbarControlPanel_.isPromotedByDefault = True</pre>
<p><strong>isPromoted = True</strong> will make the control show up on the panel - just like when clicking the curved arrow (<strong>Add to toolbar</strong>) on a control in a panel menu:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d15bc423970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Addtotoolbar" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d15bc423970c img-responsive" src="/assets/image_666638.jpg" title="Addtotoolbar" /></a></p>
<p>But if the user clicks <strong>Reset Panel Customization</strong> on the given panel or <strong>Reset All Toolbar Customization</strong> anywhere on the toolbar then the button will disappear from the panel. &#0160;&#0160;</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb087620fc970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Reset_toolbar" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb087620fc970d img-responsive" src="/assets/image_851753.jpg" title="Reset_toolbar" /></a></p>
<p><strong>isPromotedByDefault = True</strong> will make sure that the button stays on the panel even if the toolbar is reset using either of the above mentioned ways.</p>

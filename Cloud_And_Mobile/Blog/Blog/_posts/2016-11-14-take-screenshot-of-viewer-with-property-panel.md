---
layout: "post"
title: "Take Screenshot of Viewer with Property Panel"
date: "2016-11-14 19:57:20"
author: "Xiaodong Liang"
categories:
  - "Javascript"
  - "Viewer"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/11/take-screenshot-of-viewer-with-property-panel.html "
typepad_basename: "take-screenshot-of-viewer-with-property-panel"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang </a></p>
<p>&#0160;<img alt="" src="/assets/Viewer-2.7.*-green.svg" /></p>
<div>Viewer3D exposes the method <strong>getScreenShot</strong> which captures the current screen image as Blob URL. It only takes the screen of viewer container without any other menus/toolbars.&#0160; Some customers wanted to get the screenshot of viewer and some other menus such as property panel or model structure panel. It will be very useful to share with the collaborators with more information, instead of a viewer only. e.g. we can share an image that indicates the highlighted object and its properties for reference.&#0160;</div>
<div>I do not find Viewer3D implemented a way to add more DOM element to the screenshot. And it looks hard to make a prototype of Viewer3D to reuse the workflow of getScreenShot method, unless rewriting the whole process. So I turned to look for a convenient way.&#0160;</div>
<div>In theory, a DOM element has to be converted to a canvas thus it could be rendered to an image. Viewer3D. getScreenShot does similar. I found a JavaScript library HTML2Canvas. It takes a screenshot on certain parts of the web page. The usage is quite easy.<br /><a href="http://html2canvas.hertzen.com/ ">http://html2canvas.hertzen.com/ </a></div>
<div><br />Since Viewer3D. getScreenShot has produced a nice image for viewer, I tried to firstly get screenshot of viewer container by Viewer3D. getScreenShot, then get a temporary canvas of property panel by html2canvas, finally merge them together.&#0160; It looks working.</div>
<div>&#0160;
<div class="photo-wrap photo-xid-6a016764cbbcf9970b01b8d239aef3970c" id="photo-xid-6a016764cbbcf9970b01b8d239aef3970c" style="display: block; margin-left: auto; margin-right: auto; width: 500px;"><a class="asset-img-link" href="http://a3.typepad.com/6a016764cbbcf9970b01b8d239aef3970c-pi"><img alt="screenshot of viewer" class="asset  asset-image at-xid-6a016764cbbcf9970b01b8d239aef3970c img-responsive" src="/assets/image_fa0ceb.jpg" title="screenshot of viewer" /></a>
<div class="photo-caption caption-xid-6a016764cbbcf9970b01b8d239aef3970c" id="caption-xid-6a016764cbbcf9970b01b8d239aef3970c">screenshot of viewer</div>
</div>
</div>
<div>&#0160;</div>
<div><br />But after screenshot, property panel cannot pop out anymore. It disappeared :( After digging into, I found the issue is because HTML2Canvas makes a clone of current document elements (all elements), and produces the canvas for the element that for screenshot.&#0160; Although I do not know why it does so, I found when it makes the clone, it does not copy the css style ( e.g. Viewer style.css) which was bound with the element initially. While HTML2Canvas will append the cloned elements to current body (with same id) . So original element with same id also lost any style. Thus the property panel looks disappeared. Actually it was there, yet no style.</div>
<div>&#0160;</div>
<div>Finally, I realized the root reason is because of the line 758 of HTML2Canvas.cs .</div>
<div>&#0160;</div>
<div><span style="font-size: 8pt;"><em>&#0160;documentClone.replaceChild(options.javascriptEnabled === true ? documentClone.<strong>adoptNode</strong>(documentElement) : removeScriptNodes(documentClone.<strong>adoptNode</strong>(documentElement)), documentClone.documentElement);</em></span></div>
<div>&#0160;</div>
<div>adoptNode will miss css style. So I changed it to importNode.&#0160; i.e.</div>
<div>&#0160;</div>
<div><span style="font-size: 8pt;">&#0160;documentClone.replaceChild(options.javascriptEnabled === true ? documentClone.<strong>importNode</strong>(documentElement) : removeScriptNodes(documentClone.<strong>importNode</strong>(documentElement)), documentClone.documentElement); &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;</span></div>
<div><span style="font-size: 8pt;">&#0160;</span></div>
<div>
<div>Now, everything works well :) The screenshot is taken, and the elements of Viewer are not affected.</div>
<div>&#0160;</div>
<div>The code snippet below assumes the viewer is loaded and the property panel is displayed. It also assumes your application has enclosed the updated html2canvas.js: <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c8afa380970b img-responsive"><a href="http://adndevblog.typepad.com/files/html2canvas-1.zip">Download Html2canvas</a></span>.It can also apply to other elements once replacing with the&#0160;element id.</div>
</div>
<p>
<script src="https://gist.github.com/xiaodongliang/fd517f9322972095d5c8c8b44c93821e.js"></script>
</p>

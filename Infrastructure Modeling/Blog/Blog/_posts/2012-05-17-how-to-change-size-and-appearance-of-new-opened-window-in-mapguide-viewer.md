---
layout: "post"
title: "How to change size and appearance of new opened window in MapGuide viewer?"
date: "2012-05-17 02:47:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/how-to-change-size-and-appearance-of-new-opened-window-in-mapguide-viewer.html "
typepad_basename: "how-to-change-size-and-appearance-of-new-opened-window-in-mapguide-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>In MapGuide Ajax viewer, I can open a new window if I specify the value for &quot;Display result in this target interface&quot; to &quot;New window&quot; or &quot;Specified frame&quot; in Web layout editor, but it always opens a 500*500 window without toolbar. How can I resize the window or change its appearance when it is opened?</p>  <p>The solution is simple. It can be edited in &lt;WebExetension Installation folder&gt;\www\viewerfiles\ajaxmappane.templ</p>  <p>For AIMS2012:&#160; C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\viewerfiles\ajaxmappane.templ</p>  <p>For MapGuide 2011: C:\Program Files\Autodesk\MapGuideEnterprise2011\WebServerExtensions\www\viewerfiles\ajaxmappane.templ, etc.</p>  <p>Edit the source code at about line 3785:</p>  <pre class="csharpcode"><span class="rem">//the code can be modified here,</span>
<span class="rem">//if necessary, to control the size and appearance of the</span>
<span class="rem">//new window. e.g. </span>
window.open(url, targetFrame, 
  <span class="str">&quot;toolbar=no,width=500,height=500,resizable,scrollbars&quot;</span>);
//window.open(linkURL, targetFrame);</pre>
<style type="text/css">
.csharpcode, .csharpcode pre
{
	font-size: small;
	color: black;
	font-family: consolas, "Courier New", courier, monospace;
	background-color: #ffffff;
	/*white-space: pre;*/
}
.csharpcode pre { margin: 0em; }
.csharpcode .rem { color: #008000; }
.csharpcode .kwrd { color: #0000ff; }
.csharpcode .str { color: #006080; }
.csharpcode .op { color: #0000c0; }
.csharpcode .preproc { color: #cc6633; }
.csharpcode .asp { background-color: #ffff00; }
.csharpcode .html { color: #800000; }
.csharpcode .attr { color: #ff0000; }
.csharpcode .alt 
{
	background-color: #f4f4f4;
	width: 100%;
	margin: 0em;
}
.csharpcode .lnum { color: #606060; }</style>

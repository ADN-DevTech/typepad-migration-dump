---
layout: "post"
title: "Disable Coordinate Label when Plotting in Ajax Viewer"
date: "2012-07-03 20:27:48"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/disable-coordinate-label-when-plotting-in-ajax-viewer.html "
typepad_basename: "disable-coordinate-label-when-plotting-in-ajax-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>When plotting in Ajax viewer with QuickPlot, the coordinate labels of left-upper corner and bottom-right corner always show up as screen-shot below:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743001272970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_e77c0f.jpg" width="469" height="300" /></a></p>  <p>I got a request to disable this coordinate label. After some investigation, here is the solution for your reference.</p>  <p>The simplest way is to change the CSS of coordinate label div, please refer to C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\viewerfiles\quickplot.js and seach &quot;createCoordinateLabel&quot;, edit the value of label.style.cssText to &quot;display:none;&quot;</p>  <pre class="csharpcode">        <span class="kwrd">var</span> label = <span class="kwrd">this</span>.innerDoc.createElement(<span class="str">&quot;div&quot;</span>);
        container.appendChild(label);
        label.id  = id;
        label.style.cssText = <span class="str">&quot;</span><span class="str">display:none;&quot;</span>;
        label.innerHTML     = <span class="str">&quot;X: &quot;</span> + cX + <span class="str">&quot; Y: &quot;</span> + cY;</pre>
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

<p>&#160;</p>

<p>Here is the screen-shot after modification, the coordinate label is disabled now.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176161a131c970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_aac054.jpg" width="462" height="291" /></a></p>





<p>This solution applies to AIMS 2013 as well.</p>

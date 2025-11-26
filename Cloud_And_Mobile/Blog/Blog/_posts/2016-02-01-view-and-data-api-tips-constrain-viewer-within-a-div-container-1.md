---
layout: "post"
title: "View and Data API Tips: Constrain Viewer Within a div Container"
date: "2016-02-01 14:45:00"
author: "Daniel Du"
categories: []
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/view-and-data-api-tips-constrain-viewer-within-a-div-container-1.html "
typepad_basename: "view-and-data-api-tips-constrain-viewer-within-a-div-container-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>When working with View and Data API, you probably want to contain viewer into a &lt;div&gt; tag, the position and size of &lt;div&gt; can be defined with CSS. The HTML can be simple as below, a &lt;div&gt; tag as a container, the position and style is defined in a CSS class named as “viewer”: </p>  <pre class="csharpcode">    &lt;h2&gt;Autodesk View and Data API&lt;/h2&gt;
    &lt;div id=<span class="str">&quot;viewer&quot;</span> <span class="kwrd">class</span>=<span class="str">&quot;viewer&quot;</span>&gt;

    &lt;/div&gt;</pre>
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

<p>For example, here is my css class, make the viewer container with 800px * 500px, and to make it easy to see, I also add a background color:</p>

<pre class="csharpcode">.viewer {

    background-color: darksalmon;
    height: 500px;
    width: 800px;
  }</pre>
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

<p>Here is how it looks like, seems good: </p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b3caf5970d-pi"><img title="Screen Shot 2016-01-31 at 2.29.30 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2016-01-31 at 2.29.30 PM" src="/assets/image_2616a3.jpg" width="499" height="285" /></a></p>

<p>Now let’s add viewer, the code snippet is simple, you can go to github for <a href="https://github.com/Developer-Autodesk/workflow-node.js-view.and.data.api/blob/master/www%2Findex.js#L75" target="_blank">complete code</a>: </p>

<pre class="csharpcode">        <span class="kwrd">var</span> viewerContainer = document.getElementById(containerId);
        <span class="kwrd">var</span> viewer = <span class="kwrd">new</span> Autodesk.Viewing.Private.GuiViewer3D(
            viewerContainer);</pre>
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

<p>But just with this style, the viewer is “overflow” out of the &lt;div&gt; container,:</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80efdac970b-pi"><img title="Screen Shot 2016-01-31 at 2.39.12 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2016-01-31 at 2.39.12 PM" src="/assets/image_311e07.jpg" width="495" height="496" /></a></p>

<p>Here is a tip to contains the viewer into the &lt;div&gt; container, just edit the CSS as below, add “position : relative” : </p>

<pre class="csharpcode">.viewer {

    background-color: darksalmon;
    height: 500px;
    width: 800px;
    <strong>position: relative;</strong>
}</pre>
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

<p>Here is how it looks after the change, the viewer is constrained within the div tag:</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b3cafe970d-pi"><img title="Screen Shot 2016-01-31 at 2.43.25 PM" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="Screen Shot 2016-01-31 at 2.43.25 PM" src="/assets/image_fbab35.jpg" width="592" height="442" /></a></p>

<p>Not a big deal, just a small tip in case you do not know. </p>

---
layout: "post"
title: "View and Data API Tips : Conversion between DbId and node"
date: "2016-01-21 06:00:52"
author: "Daniel Du"
categories:
  - "Daniel Du"
  - "Script"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/view-and-data-api-tips-conversion-between-dbid-and-node.html "
typepad_basename: "view-and-data-api-tips-conversion-between-dbid-and-node"
typepad_status: "Publish"
---

<h6>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></h6>  <p>In View and Data client side API, The assets in the Autodesk Viewer have an object tree, a tree structure that represents the model hierarchy. Each element in model can be representing as a node of model tree. Each node has a dbId, this is a unique id for the element in the model. There is a one-to-one correspondence between a node and a dbId. </p>  <p>In View and Data client API, some methods use dbId as parameter and some methods use node as parameter, in that case you will need to do the conversion. To get the node object from dbId, here is code snippet: </p>  <pre class="csharpcode"> <span class="kwrd">var</span> node = viewer.model.getData()
     .instanceTree.dbIdToNode[dbId];</pre>
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

<p><style type="text/css">
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
.csharpcode .lnum { color: #606060; }</style>To get dbId of a node, just use the dbId property: </p>

<pre class="csharpcode"><span class="kwrd">var</span> dbId = node.dbId</pre>
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

<p>Just a small tip in case you don’t know.</p>

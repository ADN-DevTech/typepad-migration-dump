---
layout: "post"
title: "View and Data API Tips: Hide elements in viewer completely"
date: "2016-01-21 06:32:07"
author: "Daniel Du"
categories:
  - "Daniel Du"
  - "View and Data API"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/view-and-data-api-tips-hide-elements-in-viewer-completely.html "
typepad_basename: "view-and-data-api-tips-hide-elements-in-viewer-completely"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>With View and Data API, you can hide some elements in viewer by calling “viewer.hide(dbIds)”, when the elements are hided, it actually make it transparent with a shallow mark to it, or make it ghosted. It is a nice feature as user probably need to know the existence of these elements even they are hided.&#160; But you may want to hide elements completely instead of ghosting some times. Here are a code snippet you can use if you want to hide some elements totally.</p>  <pre class="csharpcode">    Autodesk.Viewing.Viewer3D.prototype.turnOff = <span class="kwrd">function</span>(dbIds){

        <span class="kwrd">var</span> node ;

        <span class="kwrd">if</span> (Array.isArray(dbIds)) {
            <span class="kwrd">for</span> (<span class="kwrd">var</span> i = 0; i &lt; dbIds.length; i++) {
                <span class="kwrd">var</span> id = dbIds[i];

                node = viewer.model.getData().instanceTree.dbIdToNode[id];
                <span class="rem">//hide the node completedly</span>
                viewer.impl.visibilityManager.setNodeOff(node, <span class="kwrd">true</span>);

            }
            
        }
        <span class="kwrd">else</span>
        {
            node = viewer.model.getData().instanceTree.dbIdToNode[dbIds];
            <span class="rem">//hide the node completedly</span>
            viewer.impl.visibilityManager.setNodeOff(node, <span class="kwrd">true</span>);
         
        }
      
    };

    Autodesk.Viewing.Viewer3D.prototype.turnOn = <span class="kwrd">function</span>(dbIds) {

        <span class="kwrd">var</span> node ;

       <span class="kwrd">if</span> (Array.isArray(dbIds)) {
            <span class="kwrd">for</span> (<span class="kwrd">var</span> i = 0; i &lt; dbIds.length; i++) {
                <span class="kwrd">var</span> id = dbIds[i];

                node = viewer.model.getData(). instanceTree.dbIdToNode[id];
                <span class="rem">//show the node</span>
                viewer.impl.visibilityManager.setNodeOff(node, <span class="kwrd">false</span>);
            }
            
        }
        <span class="kwrd">else</span>
        {
            node = viewer.model.getData(). instanceTree.dbIdToNode[dbIds];
            <span class="rem">//show the node</span>
            viewer.impl.visibilityManager.setNodeOff(node, <span class="kwrd">false</span>);

        }

    };</pre>
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
.csharpcode .lnum { color: #606060; }</style><style type="text/css">

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

<p>The usage is very simple, just call “viewer.turnOff(arrayOfDbIds)” or “viewer.turnOf(arrayOfDbIds)”. Hope it helps. 
  </p>

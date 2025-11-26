---
layout: "post"
title: "Change color of elements with View and Data API"
date: "2015-12-08 06:38:36"
author: "Daniel Du"
categories:
  - "Browser"
  - "Cloud"
  - "Daniel Du"
  - "Javascript"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/12/change-color-of-elements-with-view-and-data-api.html "
typepad_basename: "change-color-of-elements-with-view-and-data-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html">Daniel Du</a></p>  <p>It is very commonly asked how to change color of specified element. Philippe created an extension named as “material” which can be demoed in <a href="http://gallery.autodesk.io" target="_blank">ADN gallery site</a>. It is comprehensive and well designed from end user’s perspective, including button UIs. </p>  <p>At the same time, many developers want this functions as an API, a common use story is that change the element color according to specified nodeId, an expected usage is like below:</p>  <pre class="csharpcode"><span class="rem">//load the extension </span>
viewer.loadExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.Color'</span>);
<span class="rem">// an array of node Id</span>
var elementIds= [1735, 1736];
<span class="rem">//set color to red</span>
viewer.setColorMaterial(elementIds,0xff0000);  
<span class="rem">//restore to original color </span>
viewer.restoreColorMaterial(elementIds);</pre>

<p>&#160;</p>

<p>So I make an API extension as below: </p>

<pre class="csharpcode"><span class="rem">///////////////////////////////////////////////////////////////////////////////</span>
<span class="rem">// Autodesk.ADN.Viewing.Extension.Color</span>
<span class="rem">//</span>
<span class="rem">///////////////////////////////////////////////////////////////////////////////</span>
AutodeskNamespace(<span class="str">&quot;Autodesk.ADN.Viewing.Extension&quot;</span>);
Autodesk.ADN.Viewing.Extension.Color = <span class="kwrd">function</span>(viewer, options) {

    Autodesk.Viewing.Extension.call(<span class="kwrd">this</span>, viewer, options);

    <span class="kwrd">var</span> overlayName = <span class="str">&quot;temperary-colored-overlay&quot;</span>;
    <span class="kwrd">var</span> _self = <span class="kwrd">this</span>;

    _self.load = <span class="kwrd">function</span>() {

        console.log(<span class="str">'Autodesk.ADN.Viewing.Extension.Color loaded'</span>);
        <span class="rem">///////////////////////////////////////////////////////////////////////////</span>
        <span class="rem">// Generate GUID</span>
        <span class="rem">//</span>
        <span class="rem">///////////////////////////////////////////////////////////////////////////</span>
        <span class="kwrd">function</span> newGuid() {
            <span class="kwrd">var</span> d = <span class="kwrd">new</span> Date().getTime();
            <span class="kwrd">var</span> guid = <span class="str">'xxxx-xxxx-xxxx-xxxx-xxxx'</span>.replace(/[xy]/g, <span class="kwrd">function</span>(c) {
                <span class="kwrd">var</span> r = (d + Math.random() * 16) % 16 | 0;
                d = Math.floor(d / 16);
                <span class="kwrd">return</span> (c == <span class="str">'x'</span> ? r : (r &amp; 0x7 | 0x8)).toString(16);
            });
            <span class="kwrd">return</span> guid;
        };

        <span class="rem">///////////////////////////////////////////////////////////////////////////</span>
        <span class="rem">// add new material</span>
        <span class="rem">//</span>
        <span class="rem">///////////////////////////////////////////////////////////////////////////</span>
        <span class="kwrd">function</span> addMaterial(color) {
            <span class="kwrd">var</span> material = <span class="kwrd">new</span> THREE.MeshPhongMaterial({
                color: color
            });
            <span class="rem">//viewer.impl.matman().addMaterial(newGuid(), material);</span>
            viewer.impl.createOverlayScene(overlayName, material, material);
            <span class="kwrd">return</span> material;
        }

        <span class="rem">///////////////////////////////////////////////////////////////////////////</span>
        <span class="rem">// Set color for nodes</span>
        <span class="rem">// objectIds should be an array of dbId</span>
        <span class="rem">// </span>
        <span class="rem">//</span>
        <span class="rem">///////////////////////////////////////////////////////////////////////////</span>
        Autodesk.Viewing.Viewer3D.prototype.setColorMaterial = <span class="kwrd">function</span>(objectIds, color) {
            <span class="kwrd">var</span> material = addMaterial(color);

            <span class="kwrd">for</span> (<span class="kwrd">var</span> i=0; i&lt;objectIds.length; i++) {

                <span class="kwrd">var</span> dbid = objectIds[i];

                <span class="rem">//from dbid to node, to fragid</span>
                <span class="kwrd">var</span> it = viewer.model.getData().instanceTree;

                it.enumNodeFragments(dbid, <span class="kwrd">function</span> (fragId) {

                    
                    <span class="kwrd">var</span> renderProxy = viewer.impl.getRenderProxy(viewer.model, fragId);
                    
                    renderProxy.meshProxy = <span class="kwrd">new</span> THREE.Mesh(renderProxy.geometry, renderProxy.material);

                    renderProxy.meshProxy.matrix.copy(renderProxy.matrixWorld);
                    renderProxy.meshProxy.matrixWorldNeedsUpdate = <span class="kwrd">true</span>;
                    renderProxy.meshProxy.matrixAutoUpdate = <span class="kwrd">false</span>;
                    renderProxy.meshProxy.frustumCulled = <span class="kwrd">false</span>;

                    viewer.impl.addOverlay(overlayName, renderProxy.meshProxy);
                    viewer.impl.invalidate(<span class="kwrd">true</span>);
                    
                }, <span class="kwrd">false</span>);
            }

        }


        Autodesk.Viewing.Viewer3D.prototype.restoreColorMaterial = <span class="kwrd">function</span>(objectIds) {
       
            <span class="kwrd">for</span> (<span class="kwrd">var</span> i=0; i&lt;objectIds.length; i++) {

                <span class="kwrd">var</span> dbid = objectIds[i];


                <span class="rem">//from dbid to node, to fragid</span>
                <span class="kwrd">var</span> it = viewer.model.getData().instanceTree;

                it.enumNodeFragments(dbid, <span class="kwrd">function</span> (fragId) {

                    
                     <span class="kwrd">var</span> renderProxy = viewer.impl.getRenderProxy(viewer.model, fragId);

                    <span class="kwrd">if</span>(renderProxy.meshProxy){

                      <span class="rem">//remove all overlays with same name</span>
                      viewer.impl.clearOverlay(overlayName);
                      <span class="rem">//viewer.impl.removeOverlay(overlayName, renderProxy.meshProxy);</span>
                      delete renderProxy.meshProxy;
                      

                      <span class="rem">//refresh the sence</span>
                      
                      viewer.impl.invalidate(<span class="kwrd">true</span>);


                    }
                                         
                }, <span class="kwrd">true</span>);
            }

  
        }

        _self.unload = <span class="kwrd">function</span>() {
            console.log(<span class="str">'Autodesk.ADN.Viewing.Extension.Color unloaded'</span>);
            <span class="kwrd">return</span> <span class="kwrd">true</span>;
        };
    };
};
Autodesk.ADN.Viewing.Extension.Color.prototype = Object.create(Autodesk.Viewing.Extension.prototype);
Autodesk.ADN.Viewing.Extension.Color.prototype.constructor = Autodesk.ADN.Viewing.Extension.Color;
Autodesk.Viewing.theExtensionManager.registerExtension(<span class="str">'Autodesk.ADN.Viewing.Extension.Color'</span>, Autodesk.ADN.Viewing.Extension.Color);</pre>
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

<p>The source code is also posted to <a href="https://github.com/duchangyu/project-arduivew/blob/master/www/js/Autodesk.ADN.Viewing.Extension.Color.js" target="_blank">github</a> and used by my IoT sample. Hope it helps if you happen to need it. </p>

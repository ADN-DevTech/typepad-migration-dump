---
layout: "post"
title: "View & Data API: Working with layers"
date: "2015-02-01 12:26:14"
author: "Philippe Leefsma"
categories:
  - "HTML"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/02/view-data-api-working-with-layers.html "
typepad_basename: "view-data-api-working-with-layers"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is a quickie for the weekend. The following code illustrates how to iterate all the layer groups and their child nodes, then how to isolate a specific layer based on its name similarly to a user clicking on that layer from the Layers panel in UI.</p>
<p>Below is the signature of the function used to isolate a layer:</p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; border-width: 0.01mm; border-color: #000000; border-style: solid; padding: 4px; font-size: 9pt;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/**
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> * Set visibility for a single layer, or for all layers.
</span><span style="color: #800000; background-color: #f0f0f0;"> 3 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> *
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> * Not yet implemented for 3D.
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> *
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> * </span><span style="text-decoration: underline; color: #808080;"><span style="color: #808080; background-color: #ffffff; font-weight: bold; font-style: italic;">@param</span></span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> {?Array} nodes - An array of layer nodes, 
</span><span style="color: #800000; background-color: #f0f0f0;"> 7 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> *        or a single layer node, or null for all layers
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> *        
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> * </span><span style="text-decoration: underline; color: #808080;"><span style="color: #808080; background-color: #ffffff; font-weight: bold; font-style: italic;">@param</span></span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> {boolean} visible - true to show, false to hide
</span><span style="color: #800000; background-color: #f0f0f0;">10 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> * 
</span><span style="color: #800000; background-color: #f0f0f0;">11 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> * </span><span style="text-decoration: underline; color: #808080;"><span style="color: #808080; background-color: #ffffff; font-weight: bold; font-style: italic;">@param</span></span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> {boolean=} [isolate] - true to isolate the layer
</span><span style="color: #800000; background-color: #f0f0f0;">12 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;"> */
</span><span style="color: #800000; background-color: #f0f0f0;">13 </span><span style="background-color: #ffffff;">Autodesk.Viewing.Viewer3D.prototype.setLayerVisible = 
</span><span style="color: #800000; background-color: #f0f0f0;">14 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">function</span><span style="background-color: #ffffff;"> (nodes, visible, isolate)</span></pre>
<p>I am implementing my code as an <a title="" href="http://adndevblog.typepad.com/cloud_and_mobile/2014/10/how-to-write-custom-extensions-for-the-large-model-viewer.html" target="_self">extension</a> so it can be very easily reused or tested on your side:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:9pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Layers viewer extension
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// by Philippe Leefsma, January 2015
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">AutodeskNamespace(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Autodesk.ADN.Viewing.Extension"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.Layers = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">    viewer,
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">    options) {
</span><span style="color:#800000;background-color:#f0f0f0;">11 
12 </span><span style="background-color:#ffffff;">    Autodesk.Viewing.Extension.call(
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">        viewer,
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">        options);
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _self = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">18 
19 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _viewer = viewer;
</span><span style="color:#800000;background-color:#f0f0f0;">20 
21 </span><span style="background-color:#ffffff;">    _self.load = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">22 
23 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> root = _viewer.model.getLayersRoot();
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(root == </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">) {
</span><span style="color:#800000;background-color:#f0f0f0;">26 
27 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"No layer information..."</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">29 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">30 
31 </span><span style="background-color:#ffffff;">        console.log(root);
</span><span style="color:#800000;background-color:#f0f0f0;">32 
33 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;"> (</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> i = </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">; i &lt; root.childCount; i++) {
</span><span style="color:#800000;background-color:#f0f0f0;">34 
35 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> group = root.children[i];
</span><span style="color:#800000;background-color:#f0f0f0;">36 
37 </span><span style="background-color:#ffffff;">            console.log(group);
</span><span style="color:#800000;background-color:#f0f0f0;">38 
39 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;"> (</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> j = </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">; j &lt; group.childCount; j++) {
</span><span style="color:#800000;background-color:#f0f0f0;">40 
41 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> layer = group.children[j];
</span><span style="color:#800000;background-color:#f0f0f0;">42 
43 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (layer.name ===
</span><span style="color:#800000;background-color:#f0f0f0;">44 </span><span style="background-color:#ffffff;">                    </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"ORN-A-B-01-BDYGRD-XX-XXXX-MAS|A-Cols"</span><span style="background-color:#ffffff;">) {
</span><span style="color:#800000;background-color:#f0f0f0;">45 
46 </span><span style="background-color:#ffffff;">                    _viewer.setLayerVisible(
</span><span style="color:#800000;background-color:#f0f0f0;">47 </span><span style="background-color:#ffffff;">                        [layer],
</span><span style="color:#800000;background-color:#f0f0f0;">48 </span><span style="background-color:#ffffff;">                        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">49 </span><span style="background-color:#ffffff;">                        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">50 </span><span style="background-color:#ffffff;">                }
</span><span style="color:#800000;background-color:#f0f0f0;">51 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">52 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">53 
54 </span><span style="background-color:#ffffff;">        console.log(
</span><span style="color:#800000;background-color:#f0f0f0;">55 </span><span style="background-color:#ffffff;">          </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Layers loaded'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">56 </span><span style="background-color:#ffffff;">        
</span><span style="color:#800000;background-color:#f0f0f0;">57 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">58 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">59 
60 </span><span style="background-color:#ffffff;">    _self.unload = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">61 
62 </span><span style="background-color:#ffffff;">        console.log(
</span><span style="color:#800000;background-color:#f0f0f0;">63 </span><span style="background-color:#ffffff;">          </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Layers unloaded'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">64 </span><span style="background-color:#ffffff;">        
</span><span style="color:#800000;background-color:#f0f0f0;">65 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">66 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">67 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;">68 
69 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.Layers.prototype =
</span><span style="color:#800000;background-color:#f0f0f0;">70 </span><span style="background-color:#ffffff;">    Object.create(Autodesk.Viewing.Extension.prototype);
</span><span style="color:#800000;background-color:#f0f0f0;">71 
72 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.Layers.prototype.constructor =
</span><span style="color:#800000;background-color:#f0f0f0;">73 </span><span style="background-color:#ffffff;">    Autodesk.ADN.Viewing.Extension.Layers;
</span><span style="color:#800000;background-color:#f0f0f0;">74 
75 </span><span style="background-color:#ffffff;">Autodesk.Viewing.theExtensionManager.registerExtension(
</span><span style="color:#800000;background-color:#f0f0f0;">76 </span><span style="background-color:#ffffff;">    </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Layers'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">77 </span><span style="background-color:#ffffff;">    Autodesk.ADN.Viewing.Extension.Layers);</span></pre>

<p>Layer groups are being dumped into the browser debug window as illustrated in that screenshot:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c74348eb970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c74348eb970b image-full img-responsive" title="Screen Shot 2015-01-30 at 1.54.55 PM" src="/assets/image_6454e3.jpg" alt="Screen Shot 2015-01-30 at 1.54.55 PM" border="0" /></a></p>

You can download the code from the following link:
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0ccd35e970c img-responsive"><a href="http://adndevblog.typepad.com/files/autodesk.adn.viewing.extension.layers.js">Autodesk.ADN.Viewing.Extension.Layers</a></span>

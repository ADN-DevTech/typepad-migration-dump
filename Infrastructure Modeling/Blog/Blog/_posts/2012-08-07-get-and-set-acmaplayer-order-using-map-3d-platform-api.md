---
layout: "post"
title: "Get and Set AcMapLayer order using Map 3D Platform API"
date: "2012-08-07 22:04:29"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/get-and-set-acmaplayer-order-using-map-3d-platform-api.html "
typepad_basename: "get-and-set-acmaplayer-order-using-map-3d-platform-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>I am modifying this post with updated details on how to set a FDO Layer Order in Display Manager. Thanks to one of our ADN Partners who pointed out that when we use SetOrder(int value) it is setting the Layer Order as seen under “Groups” but if we active the “Draw Order”, you will find the layer order is unchanged. Let me explain it with some screenshots and how we can actually set the Layer Order which will change the visual display order.</p>
<p>public virtual int <strong>GetOrder()</strong> -&gt; &#0160; &#0160;Member of Autodesk.Gis.Map.Platform.<strong>AcMapLayer</strong></p>
<p>public virtual void <strong>SetOrder(int value)</strong> -&gt; &#0160; &#0160; Member of Autodesk.Gis.Map.Platform.<strong>AcMapLayer</strong></p>
<p><strong><br /></strong></p>
<p>Let&#39;s assume we have three FDO layers as seen in the following screenshot:</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743fd021e970d-pi" style="display: inline;"><img alt="Layer_Order_1" class="asset  asset-image at-xid-6a0167607c2431970b017743fd021e970d" src="/assets/image_931f02.jpg" title="Layer_Order_1" /></a><br /><br /></p>
<p>If you build your application using the following C# code snippet, and run your custom command, you will see the Layer Order is changed under &quot;Groups&quot; -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (layer.Name == </span><span style="color: #a31515; line-height: 140%;">&quot;City_Boundary&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Set Layer Order</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; layer.SetOrder(4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">msg += </span><span style="color: #a31515; line-height: 140%;">&quot;Layer Name : &quot;</span><span style="line-height: 140%;"> + layer.Name + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; &#0160;&#0160; &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;Layer Order: &quot;</span><span style="line-height: 140%;"> + layer.GetOrder().ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot;\n&quot;</span><span style="line-height: 140%;">;</span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676921efbe970b-pi" style="display: inline;"><img alt="Layer_Order_2" class="asset  asset-image at-xid-6a0167607c2431970b01676921efbe970b" src="/assets/image_44c9d8.jpg" title="Layer_Order_2" /></a></p>
<p style="margin: 0px;">&#0160;</p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">but it&#39;s unchanged when you move to &quot;Draw Order&quot; tab:</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"><br /></span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01761716f38f970c-pi" style="display: inline;"><img alt="Layer_Order_3" class="asset  asset-image at-xid-6a0167607c2431970b01761716f38f970c" src="/assets/image_f7f60a.jpg" title="Layer_Order_3" /></a></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">Now the question is, how do I change the Draw Order to see the &quot;City_Boundary&quot; goes below and &quot;Roads&quot; come up ? To modify the draw order of a layer, we have to remove it from the layer collection and insert it at the desired index. Here is a relevant code snippet :</span></p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the Map Object</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> currentMap = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap();&#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//Get layer to Change the Order</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> layers = currentMap.GetLayers();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> myLayer = layers.GetItem(</span><span style="color: #a31515; line-height: 140%;">&quot;City_Boundary&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (layers.Remove(myLayer))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Int index value should be correct w.r.t number of Layers in Display Manager</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Otherwise you would see an message &quot; Index is out of range.&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// And the Layer won&#39;t be inserted</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; layers.Insert(2, myLayer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">And expected result :</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"><br /></span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01761716f8dc970c-pi" style="display: inline;"><img alt="Layer_Order_4" class="asset  asset-image at-xid-6a0167607c2431970b01761716f8dc970c" src="/assets/image_827f4a.jpg" title="Layer_Order_4" /></a><br /><br /></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">Hope this is useful to you!</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>

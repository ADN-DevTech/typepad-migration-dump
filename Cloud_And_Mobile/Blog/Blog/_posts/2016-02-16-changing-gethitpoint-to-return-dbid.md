---
layout: "post"
title: "Changing getHitPoint to return dbId"
date: "2016-02-16 04:17:43"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Javascript"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/changing-gethitpoint-to-return-dbid.html "
typepad_basename: "changing-gethitpoint-to-return-dbid"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>The original <a href="https://developer.autodesk.com/api/viewer-viewing-classes/">ViewingUtilities.Utilities.getHitPoint</a> method return the XY coordinate where the give coordinate (e.g. mouse position) hits the model (e.g. the face right over the mouse cursor). Although this quite useful, I needed to read the properties of that object. This is basically how to get the dbId of the highlighted object.</p>
<p>This modified version (renamed to getHitDbId) uses basically the same code, but instead returns the dbId.&#0160;First register for the mouse move event (requires <a href="https://jquery.com">jQuery</a>):</p>
<div>
<pre style="margin: 0; line-height: 125%;">$(_viewer.container).bind(<span style="color: #ba2121;">&quot;mousemove&quot;</span>, onMouseMove);
</pre>
</div>
<p>Then implement the event and the modified version of the code:</p>
<div>
<pre style="margin: 0; line-height: 125%;"><span style="color: #008000; font-weight: bold;">function</span> onMouseMove(e) {
    <span style="color: #008000; font-weight: bold;">var</span> screenPoint <span style="color: #666666;">=</span> {
        x<span style="color: #666666;">:</span> event.clientX,
        y<span style="color: #666666;">:</span> event.clientY
    };
    <span style="color: #008000; font-weight: bold;">var</span> n <span style="color: #666666;">=</span> normalize(screenPoint);
    <span style="color: #008000; font-weight: bold;">var</span> dbId <span style="color: #666666;">=</span> <span style="color: #408080; font-style: italic;">/*_viewer.utilities.getHitPoint*/</span> getHitDbId(n.x, n.y);
    <span style="color: #008000; font-weight: bold;">if</span> (dbId <span style="color: #666666;">==</span> <span style="color: #008000; font-weight: bold;">null</span>) <span style="color: #008000; font-weight: bold;">return</span>;

    _viewer.model.getProperties(dbId, <span style="color: #008000; font-weight: bold;">function</span> (props) {
        <span style="color: #408080; font-style: italic;">// do something here</span>
    });
};

<span style="color: #408080; font-style: italic;">// This is a built-in method getHitPoint, but the original returns</span>
<span style="color: #408080; font-style: italic;">// the hit point, so this modified version returns the dbId</span>
<span style="color: #008000; font-weight: bold;">function</span> getHitDbId(x, y) {
    y <span style="color: #666666;">=</span> <span style="color: #666666;">1.0</span> <span style="color: #666666;">-</span> y;
    x <span style="color: #666666;">=</span> x <span style="color: #666666;">*</span> <span style="color: #666666;">2.0</span> <span style="color: #666666;">-</span> <span style="color: #666666;">1.0</span>;
    y <span style="color: #666666;">=</span> y <span style="color: #666666;">*</span> <span style="color: #666666;">2.0</span> <span style="color: #666666;">-</span> <span style="color: #666666;">1.0</span>;

    <span style="color: #008000; font-weight: bold;">var</span> vpVec <span style="color: #666666;">=</span> <span style="color: #008000; font-weight: bold;">new</span> THREE.Vector3(x, y, <span style="color: #666666;">1</span>);

    <span style="color: #008000; font-weight: bold;">var</span> result <span style="color: #666666;">=</span> _viewer.impl.hitTestViewport(vpVec, <span style="color: #008000; font-weight: bold;">false</span>);
    <span style="color: #008000; font-weight: bold;">return</span> result <span style="color: #666666;">?</span> result.dbId <span style="color: #666666;">:</span> <span style="color: #008000; font-weight: bold;">null</span>;
};

<span style="color: #408080; font-style: italic;">// <a href="ttp://adndevblog.typepad.com/cloud_and_mobile/2015/11/animated-radial-menu-with-css-jquery.html">originally wrote by Philippe</a></span>
<span style="color: #008000; font-weight: bold;">function</span> normalize(screenPoint) {
    <span style="color: #008000; font-weight: bold;">var</span> viewport <span style="color: #666666;">=</span> _viewer.navigation.getScreenViewport();
    <span style="color: #008000; font-weight: bold;">var</span> n <span style="color: #666666;">=</span> {
        x<span style="color: #666666;">:</span> (screenPoint.x <span style="color: #666666;">-</span> viewport.left) <span style="color: #666666;">/</span> viewport.width,
        y<span style="color: #666666;">:</span> (screenPoint.y <span style="color: #666666;">-</span> viewport.top) <span style="color: #666666;">/</span> viewport.height
    };
    <span style="color: #008000; font-weight: bold;">return</span> n;
}
</pre>
</div>

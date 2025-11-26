---
layout: "post"
title: "Toggle visibility of Layer in Fusion does not work?"
date: "2012-04-17 04:24:51"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/toggle-visibility-of-layer-in-fusion-does-not-work.html "
typepad_basename: "toggle-visibility-of-layer-in-fusion-does-not-work"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html" target="_self">Daniel Du</a></p>
<p>In this topic, I would like to show you how to change the visibility of layer in AIMS Fusion Viewer or flexible web layout. The basic steps will be changing the “Visible” property of MgLayer, or calling MgLayer.SetVisible() method, then refresh the map.</p>
<p>&#0160;</p>
<p>We can refresh the map in body.onload() event. To refresh the map in fusion viewer, we can use the APIs provided by “MapGuideViewerAPI.js”. We import this script file by :</p>
<p style="margin: 0px;"><span style="line-height: 140%; background: yellow;">&lt;%</span><span style="line-height: 140%; color: #006400;">--reference the fusion viewer API javascript file--</span><span style="line-height: 140%; background: yellow;">%&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">language</span><span style="line-height: 140%; color: blue;">=&quot;javascript&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: red;">src</span><span style="line-height: 140%; color: blue;">=&quot;../mapserver2012/fusion/layers/MapGuide/MapGuideViewerApi.js&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&#0160;</span></p>
<p><span style="line-height: 140%; background: yellow;"><span style="background-color: #ffffff;">Here is the complete code for refresh, please note that we need to call <span style="line-height: 140%;">Fusion.getWidgetById(</span><span style="line-height: 140%; color: maroon;">&#39;Map&#39;</span><span style="line-height: 140%;">).reloadMap() to refresh both legend and map.</span></span></span></p>
<p><span style="line-height: 140%; background: yellow;">&lt;%</span><span style="line-height: 140%; color: blue;">@</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: maroon;">Page</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">Language</span><span style="line-height: 140%; color: blue;">=&quot;C#&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">AutoEventWireup</span><span style="line-height: 140%; color: blue;">=&quot;true&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">CodeFile</span><span style="line-height: 140%; color: blue;">=&quot;ToggleLayer.aspx.cs&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">Inherits</span><span style="line-height: 140%; color: blue;">=&quot;ToggleLayer&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; background: yellow;">%&gt;</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;!</span><span style="line-height: 140%; color: maroon;">DOCTYPE</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">html</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">PUBLIC</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">&quot;-//W3C//DTD XHTML 1.0 Transitional//EN&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">&quot;http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">html</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">xmlns</span><span style="line-height: 140%; color: blue;">=&quot;http://www.w3.org/1999/xhtml&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">head</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">id</span><span style="line-height: 140%; color: blue;">=&quot;Head1&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">runat</span><span style="line-height: 140%; color: blue;">=&quot;server&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">title</span><span style="line-height: 140%; color: blue;">&gt;&lt;/</span><span style="line-height: 140%; color: maroon;">title</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; background: yellow;">&lt;%</span><span style="line-height: 140%; color: #006400;">--reference the fusion viewer API javascript file--</span><span style="line-height: 140%; background: yellow;">%&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">language</span><span style="line-height: 140%; color: blue;">=&quot;javascript&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">src</span><span style="line-height: 140%; color: blue;">=&quot;../mapserver2012/fusion/layers/MapGuide/MapGuideViewerApi.js&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// set isFusion to false if you are using Ajax viewer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> isFusion = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> RefreshMap() {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (isFusion) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">//Refresh();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Fusion = window.top.Fusion;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">//following code does not work</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">//var legend = Fusion.getWidgetById(&quot;Legend&quot;);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">//legend.renderer.update();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">//reload the Map to refresh legend</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Fusion.getWidgetById(</span><span style="line-height: 140%; color: maroon;">&#39;Map&#39;</span><span style="line-height: 140%;">).reloadMap();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// for basic web layout, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// if using basic weblayout, referenceing </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// to MapGuideViewerApi.js should be removed</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; parent.parent.Refresh();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">head</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">body</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">onload</span><span style="line-height: 140%; color: blue;">=&quot;javascript:RefreshMap()&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">form</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">id</span><span style="line-height: 140%; color: blue;">=&quot;form1&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">runat</span><span style="line-height: 140%; color: blue;">=&quot;server&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">div</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">div</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">form</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">body</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Now let’s working on the code behind. this part of work is pretty straight forward, I paste the code as below:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web.UI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web.UI.WebControls;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> OSGeo.MapGuide;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">partial</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ToggleLayer</span><span style="line-height: 140%;"> : System.Web.UI.</span><span style="line-height: 140%; color: #2b91af;">Page</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Page_Load(</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> sessionId = Request[</span><span style="line-height: 140%; color: #a31515;">&quot;Session&quot;</span><span style="line-height: 140%;">].ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> mapName = Request[</span><span style="line-height: 140%; color: #a31515;">&quot;MapName&quot;</span><span style="line-height: 140%;">].ToString();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Utility</span><span style="line-height: 140%;"> utility = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Utility</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; utility.InitializeWebTier(Request);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; utility.ConnectToServer(sessionId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;"> siteConnection </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = utility.GetSiteConnection();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (siteConnection == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;fail to get site connection, exit&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;"> resService = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;">)siteConnection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .CreateService(</span><span style="line-height: 140%; color: #2b91af;">MgServiceType</span><span style="line-height: 140%;">.ResourceService);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgLayerBase</span><span style="line-height: 140%;"> tmpLayer = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;"> map = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map.Open(resService, mapName);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tmpLayer = map.GetLayers().GetItem(</span><span style="line-height: 140%; color: #a31515;">&quot;Districts&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tmpLayer.SetVisible(!tmpLayer.IsVisible());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tmpLayer.ForceRefresh();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map.Save(resService);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (tmpLayer.IsVisible())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;&lt;p&gt;&lt;b&gt; the Districts layer is turned on &lt;/b&gt;&lt;/p&gt;&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;&lt;p&gt;&lt;b&gt; the Districts layer is turned off &lt;/b&gt;&lt;/p&gt;&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">MgException</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(ex.GetDetails());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>It is quite simple, but when you running this code snippet, you may find it does not work, it is executed without any problem, but the visibility of layer does not change in map and legend. The reason is due to the page cache, so we need to clear the page cache by adding following code at the beginning of Page_Load function:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.CacheControl = </span><span style="line-height: 140%; color: #a31515;">&quot;no-cache&quot;</span><span style="line-height: 140%;">;</span></p>
</div>
<p>&#0160;</p>
<p>The complete code goes as below:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web.UI;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web.UI.WebControls;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> OSGeo.MapGuide;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">partial</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ToggleLayer</span><span style="line-height: 140%;"> : System.Web.UI.</span><span style="line-height: 140%; color: #2b91af;">Page</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Page_Load(</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// *IMPORTANT*, clear the cache</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.CacheControl = </span><span style="line-height: 140%; color: #a31515;">&quot;no-cache&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> sessionId = Request[</span><span style="line-height: 140%; color: #a31515;">&quot;Session&quot;</span><span style="line-height: 140%;">].ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> mapName = Request[</span><span style="line-height: 140%; color: #a31515;">&quot;MapName&quot;</span><span style="line-height: 140%;">].ToString();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Utility</span><span style="line-height: 140%;"> utility = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Utility</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; utility.InitializeWebTier(Request);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; utility.ConnectToServer(sessionId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;"> siteConnection </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = utility.GetSiteConnection();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (siteConnection == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;fail to get site connection, exit&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;"> resService = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;">)siteConnection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .CreateService(</span><span style="line-height: 140%; color: #2b91af;">MgServiceType</span><span style="line-height: 140%;">.ResourceService);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgLayerBase</span><span style="line-height: 140%;"> tmpLayer = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;"> map = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map.Open(resService, mapName);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tmpLayer = map.GetLayers().GetItem(</span><span style="line-height: 140%; color: #a31515;">&quot;Districts&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tmpLayer.SetVisible(!tmpLayer.IsVisible());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tmpLayer.ForceRefresh();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; map.Save(resService);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (tmpLayer.IsVisible())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;&lt;p&gt;&lt;b&gt; the Districts layer is turned on &lt;/b&gt;&lt;/p&gt;&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(</span><span style="line-height: 140%; color: #a31515;">&quot;&lt;p&gt;&lt;b&gt; the Districts layer is turned off &lt;/b&gt;&lt;/p&gt;&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">MgException</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Response.Write(ex.GetDetails());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>The implementation of Utility class goes as below:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Web;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Xml;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.IO;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Text;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> OSGeo.MapGuide;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">///</span><span style="line-height: 140%; color: green;"> </span><span style="line-height: 140%; color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">///</span><span style="line-height: 140%; color: green;"> Summary description for Utility.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">///</span><span style="line-height: 140%; color: green;"> Created by Daniel Du, DevTech</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: gray;">///</span><span style="line-height: 140%; color: green;"> </span><span style="line-height: 140%; color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Utility</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;"> siteConnection;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> InitializeWebTier(</span><span style="line-height: 140%; color: #2b91af;">HttpRequest</span><span style="line-height: 140%;"> Request)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> realPath = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Request.ServerVariables[</span><span style="line-height: 140%; color: #a31515;">&quot;APPL_PHYSICAL_PATH&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> configPath = realPath + </span><span style="line-height: 140%; color: #a31515;">&quot;../webconfig.ini&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MapGuideApi</span><span style="line-height: 140%;">.MgInitializeWebTier(configPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ConnectToServer(</span><span style="line-height: 140%; color: #2b91af;">String</span><span style="line-height: 140%;"> sessionID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MgUserInformation</span><span style="line-height: 140%;"> userInfo = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgUserInformation</span><span style="line-height: 140%;">(sessionID);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; siteConnection = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; siteConnection.Open(userInfo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;"> GetSiteConnection()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> siteConnection;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this helps you.</p>

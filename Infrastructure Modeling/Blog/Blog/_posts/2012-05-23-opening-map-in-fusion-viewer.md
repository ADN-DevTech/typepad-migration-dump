---
layout: "post"
title: "Opening map in fusion viewer"
date: "2012-05-23 14:21:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/opening-map-in-fusion-viewer.html "
typepad_basename: "opening-map-in-fusion-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>You may have been using following code snippet to open a map in basic layout (assuming a map resource named as “myMapName” is created in MapGuide Studio,):</p>  <pre class="csharpcode">MgMap map = <span class="kwrd">new</span> MgMap();
map.Open(resourceServices, “myMapName”);</pre>
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

<p>But it does not work in fusion viewer, the error message is :</p>

<p>Error : &quot; Invalid argument(s): [0] = &quot; &quot; The string cannot be empty &quot;</p>

<p>&#160;</p>

<p>The reason is the map name will be changed during run time in fusion viewer, so please do not use hard-coded map name when programing. </p>

<p>If you are using “Invoke URL” command to invoke the page, MapGuide will add some parameters for you including SESSION, MAPNAME, etc. The value of these parameters can be retrieved by Request object. Here is a demonstration of how to get map name and open a map in fusion viewer:</p>

<pre class="csharpcode"><span class="kwrd">string</span> mapname = Request[“MapName”].ToString()
map.open(resSvc,mapname);</pre>
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

<p>Firstly, create a custom command to &quot;invoke URL&quot; and point the URL to your custom page.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168ebb4ed4e970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_02f766.jpg" width="499" height="186" /></a></p>

<p>Specify the target as &quot;Task Pane&quot;: </p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766b36d32970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_4f6cae.jpg" width="496" height="186" /></a></p>

<p>Code snippet goes as below:</p>

<div style="font-family: ; color: ">
  <p style="margin: 0px"><font face="Courier New"><span style="background-image: none; line-height: 11pt; background-attachment: scroll; background-repeat: repeat; background-position: 0% 0%"><font style="background-color: #ffee62" color="#000000"><font style="font-size: 8pt">&lt;%</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">@</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#a31515">Page</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">Language</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">=&quot;C#&quot;</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">AutoEventWireup</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">=&quot;true&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> </font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#ff0000">CodeBehind</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">=&quot;MapInformation.aspx.cs&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> </font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#ff0000">Inherits</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">=&quot;AIMS2012WebApp.MapInformation&quot;</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span></font><span style="background-image: none; line-height: 11pt; background-attachment: scroll; background-repeat: repeat; background-position: 0% 0%"><font style="background-color: #ffee62; font-size: 8pt" color="#000000">%&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;!</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">DOCTYPE</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">html</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">PUBLIC</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">&quot;-//W3C//DTD XHTML 1.0 Transitional//EN&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> </font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;</font></font></span><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&quot;http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd&quot;&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">html</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">xmlns</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">=&quot;http://www.w3.org/1999/xhtml&quot;&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">head</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">runat</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">=&quot;server&quot;&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;</font></span><span style="line-height: 11pt; color: "><font color="#a31515">title</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">&gt;&lt;/</font></span><span style="line-height: 11pt; color: "><font color="#a31515">title</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;</font></span><span style="line-height: 11pt; color: "><font color="#a31515">script</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">language</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">=</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">&quot;javascript&quot;</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">type</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">=&quot;text/javascript&quot;&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">function</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> Init() {</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">var</font></span><span style="line-height: 11pt"><font color="#000000"> aa = </font></span><span style="line-height: 11pt; color: "><font color="#a31515">'&lt;%=mapNameXX %&gt;'</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> ;</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; alert(aa);</font></font></span></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;/</font></span><span style="line-height: 11pt; color: "><font color="#a31515">script</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;/</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">head</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">body</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">onload</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">=&quot;Init();&quot;&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;</font></span><span style="line-height: 11pt; color: "><font color="#a31515">form</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">id</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">=&quot;form1&quot;</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#ff0000">runat</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">=&quot;server&quot;&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;</font></span><span style="line-height: 11pt; color: "><font color="#a31515">div</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;/</font></span><span style="line-height: 11pt; color: "><font color="#a31515">div</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">&lt;/</font></span><span style="line-height: 11pt; color: "><font color="#a31515">form</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;/</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">body</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">&lt;/</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">html</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#0000ff">&gt;</font></span></font></p>
</div>

<p>Code behind:</p>

<div style="font-family: ; color: ">
  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Collections.Generic;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Linq;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Web;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Web.UI;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Web.UI.WebControls;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">namespace</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> AIMS2012WebApp</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">{</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">public</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">partial</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">class</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#2b91af">MapInformation</font></span><span style="line-height: 11pt"><font color="#000000"> : System.Web.UI.</font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#2b91af">Page</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; {</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">public</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">string</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> mapNameXX;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">protected</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">void</font></span><span style="line-height: 11pt"><font color="#000000"> Page_Load(</font></span><span style="line-height: 11pt; color: "><font color="#0000ff">object</font></span><span style="line-height: 11pt"><font color="#000000"> sender, </font></span><span style="line-height: 11pt; color: "><font color="#2b91af">EventArgs</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> e)</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160; {</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; UtilityClass util = </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">new</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> UtilityClass();</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; util.InitializeWebTier(Request);</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">string</font></span><span style="line-height: 11pt"><font color="#000000"> sessionId = Request[</font></span><span style="line-height: 11pt; color: "><font color="#a31515">&quot;Session&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">].ToString();</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; util.ConnectToServer(sessionId);</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">string</font></span><span style="line-height: 11pt"><font color="#000000"> mapName = Request[</font></span><span style="line-height: 11pt; color: "><font color="#a31515">&quot;MapName&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">].ToString();</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#008000">//public variable, alert from client side</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; mapNameXX = mapName;</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160; }</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; }</font></font></span></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">}</font></font></span></p>
</div>

<p>The implementation of Utility class goes as below:</p>

<div style="font-family: ; color: ">
  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Collections.Generic;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Web;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Collections;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Xml;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.IO;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> System.Text;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">using</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> OSGeo.MapGuide;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#808080"><font style="font-size: 8pt">///</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#008000"> </font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#808080">&lt;summary&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#808080"><font style="font-size: 8pt">///</font></font></span><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#008000"> Summary description for Utility.</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#808080"><font style="font-size: 8pt">///</font></font></span><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#008000"> Created by Daniel Du, DevTech</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#808080"><font style="font-size: 8pt">///</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#008000"> </font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#808080">&lt;/summary&gt;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt; color: "><font color="#0000ff"><font style="font-size: 8pt">public</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">class</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span></font><span style="line-height: 11pt; color: "><font style="font-size: 8pt" color="#2b91af">UtilityClass</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">{</font></font></span></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; MgSiteConnection siteConnection;</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">public</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">void</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> InitializeWebTier(HttpRequest Request)</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; {</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">string</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> realPath =</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Request.ServerVariables[</font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#a31515">&quot;APPL_PHYSICAL_PATH&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">];</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#2b91af">String</font></span><span style="line-height: 11pt"><font color="#000000"> configPath = realPath + </font></span><span style="line-height: 11pt; color: "><font color="#a31515">&quot;../webconfig.ini&quot;</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">;</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160; MapGuideApi.MgInitializeWebTier(configPath);</font></font></span></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; }</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">public</font></span><span style="line-height: 11pt"><font color="#000000"> </font></span><span style="line-height: 11pt; color: "><font color="#0000ff">void</font></span><span style="line-height: 11pt"><font color="#000000"> ConnectToServer(</font></span><span style="line-height: 11pt; color: "><font color="#2b91af">String</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> sessionID)</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; {</font></font></span></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160; MgUserInformation userInfo =</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">new</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> MgUserInformation(sessionID);</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; siteConnection = </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">new</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> MgSiteConnection();</font></span></font></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160;&#160;&#160;&#160;&#160; siteConnection.Open(userInfo);</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; }</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">public</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> MgSiteConnection GetSiteConnection()</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; {</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><span style="line-height: 11pt"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160;&#160;&#160;&#160;&#160; </font></font></span><font style="font-size: 8pt"><span style="line-height: 11pt; color: "><font color="#0000ff">return</font></span></font><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000"> siteConnection;</font></span></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; }</font></font></span></p>

  <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><span style="line-height: 11pt"><font face="Courier New"><font style="font-size: 8pt" color="#000000">}</font></font></span></p>
</div>

<p>The result will be similar as below, please the note the map name is different with the one defined in Infrastructure Studio.</p>

<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766b36dcb970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_67e507.jpg" width="497" height="322" /></a></p>

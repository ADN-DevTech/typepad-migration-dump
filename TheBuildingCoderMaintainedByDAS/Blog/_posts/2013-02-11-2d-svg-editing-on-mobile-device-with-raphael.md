---
layout: "post"
title: "2D SVG Editing on Mobile Device with Raphaël"
date: "2013-02-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "JavaScript"
  - "Mobile"
  - "SVG"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/02/2d-svg-editing-on-mobile-device-with-rapha%C3%ABl.html "
typepad_basename: "2d-svg-editing-on-mobile-device-with-raphaël"
typepad_status: "Publish"
---

<p>As I mentioned last week, I took another look at displaying a 2D view of a Revit model on mobile devices using SVG to enhance the

<a href="http://thebuildingcoder.typepad.com/blog/2012/10/room-polygon-and-furniture-picker-in-svg.html">
room polygon and furniture picker in SVG</a>.

<p>That implementation displays a read-only view of the model, useful for picking and identifying elements, e.g. for querying or adding metadata to them.</p>

<p>It would also be nice to be able to edit some data graphically, e.g. enable translation and rotation of the furniture and other family instances.</p>

<p>The long-term goal is to implement a cloud-based round-trip 2D Revit model editing on any mobile device using SVG.

<p>The advantage of SVG is that it enables display and edit of a 2D scene, e.g. a rendering of a Revit model, on any mobile device with no need for installation of any additional software whatsoever beyond a browser.

<p>The parts that I still want to explore include a Revit add-in to export polygon renderings of room boundaries and other elements such as furniture and equipment to a cloud-based repository, and querying the repository from the mobile device.

<p>To simplify editing the model, I wrapped all the SVG interaction in a JavaScript library.</p>

<p>After some research and comparison of different available open source SVG libraries and editors, I chose the

<a href="#0">Raphaël JavaScript library</a>.

I then worked through the following series of incremental implementation steps to first recreate the state I had already achieved previously, then add drag, rotate, and button support:</p>

<ol>
<li><a href="#1">Curves</a></li>
<li><a href="#2">Grid</a></li>
<li><a href="#3">Room</a></li>
<li><a href="#4">Reporting text and click handler</a></li>
<li><a href="#5">Furniture</a></li>
<li><a href="#6">Drag</a></li>
<li><a href="#7">Rotate</a></li>
<li><a href="#8">Button</a></li>
<li><a href="#9">Conclusion</a></li>
</ol>

<p>To immediately jump into my testing playground, you can simply access

<a href="http://thebuildingcoder.typepad.com/svg">The Building Coder SVG</a> folder.</p>



<a name="0"></a>

<h4>The Raphaël JavaScript Library</h4>

<p><a href="http://raphaeljs.com">Raphaël</a> ['ræfeɪəl] is

a small JavaScript library that simplifies work with vector graphics on the web.</p>

<p>Raphaël uses the SVG W3C Recommendation and VML as a base for creating graphics.
This means every graphical object is also a DOM object, so JavaScript event handlers can be attached and the objects can be dynamically modified.
The stated goal is to provide an adapter to make drawing vector art compatible cross-browser and easy.</p>

<p>Raphaël supports the current versions of Firefox, Safari, Chrome, Opera and Internet Explorer.</p>


<a name="1"></a>

<h4>Curves</h4>

<p>The first step I took was to extract one of the Raphaël demo pages, <a href="http://raphaeljs.com/curver.html">curver.html</a>, and host it on my own page to ensure that it has no other dependencies and continues to work in my environment:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/01-curves.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017ee8678278970d" alt="The Building Coder Raphaël curves demo" title="The Building Coder Raphaël curves demo" src="/assets/image_c5bff8.jpg" /></a><br/>
<a href="http://thebuildingcoder.typepad.com/svg/01-curves.htm">Curves</a>
</center>

<p>It shows how curves can be drawn and hooked up with event handlers to react to dragging.</p>

<p>To explore this or any other of the Raphaël demo implementations, simply navigate to it and view the source.</p>


<a name="2"></a>

<h4>Grid</h4>

<p>Once I had that in place and up and running, my next steps dealt with reimplementing the state of the existing

<a href="http://thebuildingcoder.typepad.com/blog/2012/10/room-polygon-and-furniture-picker-in-svg.html">
pure SVG equipment picker</a> using

Raphaël instead.

<p>The first step is easy, simply setting up the canvas and a rectangular grid:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/02-grid.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017d40f2d949970c" alt="Grid" title="Grid" src="/assets/image_ce4dbc.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/02-grid.htm">Grid</a>
</center>

<p>Here is the source code HTML achieving this (copy to a text editor or view source to see the truncated lines in full):</p>

<pre style='color:#000000;background:#ffffff;'>
<span style='color:#004a43; '>&lt;!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "</span><span style='color:#5555dd; '>http://www.w3.org/TR/html4/strict.dtd</span><span style='color:#004a43; '>"></span>
<span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>html</span><span style='color:#274796; '> </span><span style='color:#074726; '>lang</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"en"</span><span style='color:#a65700; '>></span>
  <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>head</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>meta</span><span style='color:#274796; '> </span><span style='color:#074726; '>http-equiv</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"Content-Type"</span><span style='color:#274796; '> </span><span style='color:#074726; '>content</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"text/html; charset=ascii"</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>title</span><span style='color:#a65700; '>></span>Jeremy's <span style='color:#008c00; '>2</span>D Mobile Device Revit Model Editor Grid<span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>title</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>link</span><span style='color:#274796; '> </span><span style='color:#074726; '>rel</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"stylesheet"</span><span style='color:#274796; '> </span><span style='color:#074726; '>href</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"demo.css"</span><span style='color:#274796; '> </span><span style='color:#074726; '>type</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"text/css"</span><span style='color:#274796; '> </span><span style='color:#074726; '>media</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"screen"</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>link</span><span style='color:#274796; '> </span><span style='color:#074726; '>rel</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"stylesheet"</span><span style='color:#274796; '> </span><span style='color:#074726; '>href</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"demo-print.css"</span><span style='color:#274796; '> </span><span style='color:#074726; '>type</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"text/css"</span><span style='color:#274796; '> </span><span style='color:#074726; '>media</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"print"</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>script src="raphael-min-jt.js" type="text/javascript" charset="ascii"</span><span style='color:#a65700; '>></span><span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>script</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>script type="text/javascript" charset="ascii"</span><span style='color:#a65700; '>></span>
      window<span style='color:#808030; '>.</span>onload <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>function</span> <span style='color:#808030; '>(</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
        <span style='color:#800000; font-weight:bold; '>var</span> w <span style='color:#808030; '>=</span> <span style='color:#008c00; '>600</span><span style='color:#808030; '>,</span> h <span style='color:#808030; '>=</span> <span style='color:#008c00; '>400</span><span style='color:#800080; '>;</span>
        <span style='color:#800000; font-weight:bold; '>var</span> r <span style='color:#808030; '>=</span> Raphael<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>holder</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> w<span style='color:#808030; '>,</span> h<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
        <span style='color:#808030; '>--</span>w<span style='color:#800080; '>;</span>
        <span style='color:#808030; '>--</span>h<span style='color:#800080; '>;</span>
        r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>0</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>0</span><span style='color:#808030; '>,</span> w<span style='color:#808030; '>,</span> h<span style='color:#808030; '>,</span> <span style='color:#008c00; '>10</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>#666</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
        <span style='color:#696969; '>// grid</span>
        <span style='color:#800000; font-weight:bold; '>var</span> d <span style='color:#808030; '>=</span> <span style='color:#008c00; '>50</span><span style='color:#808030; '>,</span> i<span style='color:#800080; '>;</span>
        <span style='color:#800000; font-weight:bold; '>for</span> <span style='color:#808030; '>(</span>i <span style='color:#808030; '>=</span> d<span style='color:#800080; '>;</span> i <span style='color:#808030; '>&lt;</span> h<span style='color:#800080; '>;</span> i <span style='color:#808030; '>+=</span> d<span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
          r<span style='color:#808030; '>.</span>path<span style='color:#808030; '>(</span><span style='color:#808030; '>[</span><span style='color:#808030; '>[</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>M</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>0</span><span style='color:#808030; '>,</span> i<span style='color:#808030; '>]</span><span style='color:#808030; '>,</span> <span style='color:#808030; '>[</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>L</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> w<span style='color:#808030; '>,</span> i<span style='color:#808030; '>]</span><span style='color:#808030; '>]</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>#666</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
        <span style='color:#800080; '>}</span>
        <span style='color:#800000; font-weight:bold; '>for</span> <span style='color:#808030; '>(</span>i <span style='color:#808030; '>=</span> <span style='color:#008c00; '>50</span><span style='color:#800080; '>;</span> i <span style='color:#808030; '>&lt;</span> <span style='color:#008c00; '>600</span><span style='color:#800080; '>;</span> i <span style='color:#808030; '>+=</span> <span style='color:#008c00; '>50</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
          r<span style='color:#808030; '>.</span>path<span style='color:#808030; '>(</span><span style='color:#808030; '>[</span><span style='color:#808030; '>[</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>M</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> i<span style='color:#808030; '>,</span> <span style='color:#008c00; '>0</span><span style='color:#808030; '>]</span><span style='color:#808030; '>,</span> <span style='color:#808030; '>[</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>L</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> i<span style='color:#808030; '>,</span> w<span style='color:#808030; '>]</span><span style='color:#808030; '>]</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>#666</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
        <span style='color:#800080; '>}</span>
      <span style='color:#800080; '>}</span><span style='color:#800080; '>;</span>
    <span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>script</span><span style='color:#a65700; '>></span>
  <span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>head</span><span style='color:#a65700; '>></span>
  <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>body</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>div</span><span style='color:#274796; '> </span><span style='color:#074726; '>id</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"holder"</span><span style='color:#a65700; '>></span><span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>div</span><span style='color:#a65700; '>></span>
    <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>p</span><span style='color:#274796; '> </span><span style='color:#074726; '>id</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"copy"</span><span style='color:#a65700; '>></span>Jeremy Tammik, Autodesk Inc<span style='color:#008c00; '>.</span> using the <span style='color:#a65700; '>&lt;</span><span style='color:#800000; font-weight:bold; '>a</span><span style='color:#274796; '> </span><span style='color:#074726; '>href</span><span style='color:#808030; '>=</span><span style='color:#0000e6; '>"http://raphaeljs.com"</span><span style='color:#a65700; '>></span>Raphael<span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>a</span><span style='color:#a65700; '>></span> JavaScript vector library<span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>p</span><span style='color:#a65700; '>></span>
  <span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>body</span><span style='color:#a65700; '>></span>
<span style='color:#a65700; '>&lt;/</span><span style='color:#800000; font-weight:bold; '>html</span><span style='color:#a65700; '>></span>
</pre>


<a name="3"></a>

<h4>Room Polygon</h4>

<p>Next, display the static room polygon on the grid:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/03-room.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017c36c44ca1970b" alt="Room" title="Room" src="/assets/image_b84b9d.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/03-room.htm">Room</a>
</center>

<p>Basically, we simply add the following line of code:</p>

<pre class="code">
  // room - outer loop anti-clockwise, inner clockwise

  r.path("M 40 40 L 440 40 440 300 300 300 300 200 40 200"
    + " Z M 320 240 L 320 280 420 280 420 240 Z")
    .attr({stroke: "blue", fill:"lightblue"});
</pre>

<p>There are several different ways to implement the hole.
Note that the oddeven fill rule is applied by default and requires strict adherence to the clockwise and anti-clockwise orientation of loop vertices.
I found the hints on

<a href="http://stackoverflow.com/questions/3349651/how-to-achieve-donut-holes-with-paths-in-raphael">
how to achieve donut holes with paths</a> useful.</p>


<a name="4"></a>

<h4>Reporting Text and Click Handler</h4>

<p>The main feature of the original furniture picker was the identification and dynamic reporting of the room polygon, furniture, or equipment picked on screen.
The reporting function could of course be expanded to display or edit any other information desired, including updating some repository with modified data.</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/04-text.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017ee8678db0970d" alt="Text" title="Text" src="/assets/image_900862.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/04-text.htm">Text</a>
</center>

<p>To achieve this in the Raphaël environment, I implemented a JavaScript event handler:</p>

<pre style='color:#000000;background:#ffffff;'>
  jOnClick <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>function</span> <span style='color:#808030; '>(</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    <span style='color:#800000; font-weight:bold; '>var</span> s <span style='color:#808030; '>=</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>Id </span><span style='color:#800000; '>"</span> <span style='color:#808030; '>+</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>id<span style='color:#808030; '>.</span><span style='color:#800000; font-weight:bold; '>toString</span><span style='color:#808030; '>(</span><span style='color:#808030; '>)</span>
      <span style='color:#808030; '>+</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>: </span><span style='color:#800000; '>"</span> <span style='color:#808030; '>+</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
    t<span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>text<span style='color:#800080; '>:</span>s<span style='color:#800080; '>}</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span>
</pre>

<p>The clicked object is displayed using a new text element, and both the background rectangle and the room polygon are assigned a 'jid' identifier and subscribe to the click event to trigger the event handler:

<pre class="code">
  // room - outer loop anti-clockwise, inner clockwise

  var room = r
    .path("M 40 40 L 440 40 440 300 300 300 300 200 40 200 Z"
      + "M 320 240 L 320 280 420 280 420 240 Z")
    .attr({stroke: "blue", fill:"lightblue"})
    .data("jid", "room")
    .click(jOnClick);

  // reporting text

  var t = r
    .text(300, 380, "Welcome. Please click around.")
    .attr({stroke: "#fff"});
</pre>



<a name="5"></a>

<h4>Furniture and Equipment</h4>

<p>Next, I added the furniture, consisting of table and chair placeholders:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/05-furniture.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017c36c4523f970b" alt="Furniture" title="Furniture" src="/assets/image_ea1141.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/05-furniture.htm"></a>
</center>

<p>Each one is implemented similarly to the room polygon, equipped with its own identifier, and obviously subscribing to the click event:</p>

<pre style='color:#000000;background:#ffffff;'>
  <span style='color:#696969; '>// furniture</span>

  r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>100</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>100</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>200</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>40</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>table</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

  r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>75</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>110</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair1</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

  r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>118</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair2</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

  r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>158</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair3</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

  r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>198</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair4</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

  r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>238</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair5</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
</pre>

<p>With that, we have arrived at the same stage of functionality as we already had using just pure SVG with no additional toolkit support.
Now comes the new fun stuff.</p>



<a name="6"></a>

<h4>Drag</h4>

<p>Raphaël supports dragging.
Here, the furniture has been dragged away from its original locations:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/06-drag.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017d40f2e54f970c" alt="Drag" title="Drag" src="/assets/image_276f9a.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/06-drag.htm">Drag</a>
</center>

<p>Drag event subscription requires event handlers for the drag initialisation, move and mouse up actions:</p>

<pre style='color:#000000;background:#ffffff;'>
  <span style='color:#696969; '>// drag support</span>

  <span style='color:#800000; font-weight:bold; '>var</span> dragger <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>function</span> <span style='color:#808030; '>(</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>ox <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>type <span style='color:#808030; '>==</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>rect</span><span style='color:#800000; '>"</span> <span style='color:#800080; '>?</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>x</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>:</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>cx</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
    <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>oy <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>type <span style='color:#808030; '>==</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>rect</span><span style='color:#800000; '>"</span> <span style='color:#800080; '>?</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>y</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>:</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>cy</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
    <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>animate<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>fill-opacity</span><span style='color:#800000; '>"</span><span style='color:#800080; '>:</span> <span style='color:#008000; '>.5</span><span style='color:#800080; '>}</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>500</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span><span style='color:#808030; '>,</span>
  move <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>function</span> <span style='color:#808030; '>(</span>dx<span style='color:#808030; '>,</span> dy<span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    <span style='color:#800000; font-weight:bold; '>var</span> att <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>type <span style='color:#808030; '>==</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>rect</span><span style='color:#800000; '>"</span>
      <span style='color:#800080; '>?</span> <span style='color:#800080; '>{</span>x<span style='color:#800080; '>:</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>ox <span style='color:#808030; '>+</span> dx<span style='color:#808030; '>,</span> y<span style='color:#800080; '>:</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>oy <span style='color:#808030; '>+</span> dy<span style='color:#800080; '>}</span>
      <span style='color:#800080; '>:</span> <span style='color:#800080; '>{</span>cx<span style='color:#800080; '>:</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>ox <span style='color:#808030; '>+</span> dx<span style='color:#808030; '>,</span> cy<span style='color:#800080; '>:</span> <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>oy <span style='color:#808030; '>+</span> dy<span style='color:#800080; '>}</span><span style='color:#800080; '>;</span>
    <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span>att<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
    r<span style='color:#808030; '>.</span>safari<span style='color:#808030; '>(</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span><span style='color:#808030; '>,</span>
  up <span style='color:#808030; '>=</span> <span style='color:#800000; font-weight:bold; '>function</span> <span style='color:#808030; '>(</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    <span style='color:#800000; font-weight:bold; '>this</span><span style='color:#808030; '>.</span>animate<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>fill-opacity</span><span style='color:#800000; '>"</span><span style='color:#800080; '>:</span> <span style='color:#008c00; '>1</span><span style='color:#800080; '>}</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>500</span><span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span><span style='color:#808030; '>,</span>
</pre>

<p>To subscribe to the drag event efficiently, I reimplemented the furniture as an array that I can iterate over uniformly:</p>

<pre style='color:#000000;background:#ffffff;'>
  <span style='color:#696969; '>// furniture</span>

  furniture <span style='color:#808030; '>=</span> <span style='color:#808030; '>[</span>
    r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>100</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>100</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>200</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>40</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>table</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#808030; '>,</span>
    r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>75</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>110</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair1</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#808030; '>,</span>
    r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>118</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair2</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#808030; '>,</span>
    r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>158</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair3</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#808030; '>,</span>
    r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>198</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair4</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span><span style='color:#808030; '>,</span>
    r<span style='color:#808030; '>.</span>rect<span style='color:#808030; '>(</span><span style='color:#008c00; '>238</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>150</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>22</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>16</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span><span style='color:#808030; '>)</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>jid</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>chair5</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
  <span style='color:#808030; '>]</span><span style='color:#800080; '>;</span>

  <span style='color:#800000; font-weight:bold; '>var</span> n <span style='color:#808030; '>=</span> furniture<span style='color:#808030; '>.</span>length<span style='color:#800080; '>;</span>

  <span style='color:#800000; font-weight:bold; '>for</span> <span style='color:#808030; '>(</span>i <span style='color:#808030; '>=</span> <span style='color:#008c00; '>0</span><span style='color:#800080; '>;</span> i <span style='color:#808030; '>&lt;</span> n<span style='color:#800080; '>;</span> <span style='color:#808030; '>++</span>i<span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    furniture<span style='color:#808030; '>[</span>i<span style='color:#808030; '>]</span><span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>stroke<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> fill<span style='color:#800080; '>:</span><span style='color:#800000; '>"</span><span style='color:#0000e6; '>blue</span><span style='color:#800000; '>"</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
      <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span>jOnClick<span style='color:#808030; '>)</span>
      <span style='color:#808030; '>.</span>drag<span style='color:#808030; '>(</span>move<span style='color:#808030; '>,</span> dragger<span style='color:#808030; '>,</span> up<span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span>
</pre>


<a name="7"></a>

<h4>Rotate</h4>

<p>I fiddled around for a while to implement the rotation.
Here is some dragged and rotated furniture:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/07-rotate.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017ee86791d0970d" alt="Rotate" title="Rotate" src="/assets/image_17f0db.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/07-rotate.htm">Rotate</a>
</center>

<p>In order to handle dragging, I ended up implementing a text element that triggers a rotation of the currently selected element.
A sibling text element reverses the rotation, i.e. rotates counter-clockwise.</p>

<p>The Raphaël rotation handling seems somewhat controversial, as far as I can tell from numerous discussions on the topic that I referred to.
When dragging a rotated element, I first unrotate it back to its original state.
Otherwise, the drag operation on a rotated element takes the rotation into account, causing the drag direction to differ from the visible cursor movement.</p>

<p>Here are the rotation handlers:</p>

<pre style='color:#000000;background:#ffffff;'>
  <span style='color:#696969; '>// rototion support</span>

  <span style='color:#800000; font-weight:bold; '>var</span> current_furniture <span style='color:#808030; '>=</span> <span style='color:#0f4d75; '>null</span><span style='color:#800080; '>;</span>

  <span style='color:#800000; font-weight:bold; '>function</span> rotate_item <span style='color:#808030; '>(</span><span style='color:#800000; font-weight:bold; '>item</span><span style='color:#808030; '>,</span> angle<span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    <span style='color:#800000; font-weight:bold; '>if</span><span style='color:#808030; '>(</span> <span style='color:#0f4d75; '>null</span> <span style='color:#808030; '>!=</span> <span style='color:#800000; font-weight:bold; '>item</span> <span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
      <span style='color:#800000; font-weight:bold; '>item</span><span style='color:#808030; '>.</span>rotate<span style='color:#808030; '>(</span> angle <span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
      <span style='color:#800000; font-weight:bold; '>item</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>angle</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span>
        <span style='color:#800000; font-weight:bold; '>item</span><span style='color:#808030; '>.</span>data<span style='color:#808030; '>(</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>angle</span><span style='color:#800000; '>"</span> <span style='color:#808030; '>)</span> <span style='color:#808030; '>+</span> angle <span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
    <span style='color:#800080; '>}</span>
  <span style='color:#800080; '>}</span>

  <span style='color:#800000; font-weight:bold; '>function</span> rotate_current_cw <span style='color:#808030; '>(</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    rotate_item<span style='color:#808030; '>(</span> current_furniture<span style='color:#808030; '>,</span> <span style='color:#008c00; '>5</span> <span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span>

  <span style='color:#800000; font-weight:bold; '>function</span> rotate_current_ccw <span style='color:#808030; '>(</span><span style='color:#808030; '>)</span> <span style='color:#800080; '>{</span>
    rotate_item<span style='color:#808030; '>(</span> current_furniture<span style='color:#808030; '>,</span> <span style='color:#808030; '>-</span><span style='color:#008c00; '>5</span> <span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
  <span style='color:#800080; '>}</span>
</pre>

<p>Here are the two text elements used to trigger rotation of the currently selected element:</p>

<pre style='color:#000000;background:#ffffff;'>
  <span style='color:#696969; '>// rotate selected element</span>

  <span style='color:#800000; font-weight:bold; '>var</span> t2 <span style='color:#808030; '>=</span> r
    <span style='color:#808030; '>.</span>text<span style='color:#808030; '>(</span><span style='color:#008c00; '>500</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>380</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>Rotate selected item</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>fill<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>#fff</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>font-size</span><span style='color:#800000; '>"</span><span style='color:#800080; '>:</span> <span style='color:#008c00; '>12</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span> rotate_current_cw <span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>

  <span style='color:#800000; font-weight:bold; '>var</span> t3 <span style='color:#808030; '>=</span> r
    <span style='color:#808030; '>.</span>text<span style='color:#808030; '>(</span><span style='color:#008c00; '>580</span><span style='color:#808030; '>,</span> <span style='color:#008c00; '>380</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>ccw</span><span style='color:#800000; '>"</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>attr<span style='color:#808030; '>(</span><span style='color:#800080; '>{</span>fill<span style='color:#800080; '>:</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>#fff</span><span style='color:#800000; '>"</span><span style='color:#808030; '>,</span> <span style='color:#800000; '>"</span><span style='color:#0000e6; '>font-size</span><span style='color:#800000; '>"</span><span style='color:#800080; '>:</span> <span style='color:#008c00; '>12</span><span style='color:#800080; '>}</span><span style='color:#808030; '>)</span>
    <span style='color:#808030; '>.</span>click<span style='color:#808030; '>(</span> rotate_current_ccw <span style='color:#808030; '>)</span><span style='color:#800080; '>;</span>
</pre>

<p>I had a problem with the rotation on the iPad: apparently, on that platform, the drag-move-up handler overrides the click handler, which initially made it impossible to click any element to select it for rotation.
The final solution I wound up with was adding functionality to the drag handler so that it handles the click event stuff as well.





<a name="8"></a>

<h4>Button</h4>

<p>Finally, a rather trivial enhancement to clarify the usage of the text rotation elements.
I simply added a rectangle around them to make it more obvious that they are actually buttons:</p>

<center>

<a href="http://thebuildingcoder.typepad.com/svg/08-button.htm"><img class="asset  asset-image at-xid-6a00e553e168978833017d40f2e881970c" alt="Button" title="Button" src="/assets/image_b56a44.jpg" /></a><br />
<a href="http://thebuildingcoder.typepad.com/svg/08-button.htm">Button</a>
</center>


<a name="9"></a>

<h4>Conclusion</h4>

<p>So far I am happy with everything I have learned about both SVG and the Raphaël toolkit.</p>

<p>My original plan to implement a cloud-based round-trip 2D Revit model editing workflow on any mobile device using SVG still seems feasible.</p>

<p>One possible next step might be to implement a Revit add-in to export room boundaries and family instances representing furniture or other equipment.
For the latter, a simplistic approach might export only the bounding box, or the largest area horizontal face, or, better still, the real outline looking from above.
Another step would be implementing a cloud-based repository for this data.
It can be retrieved and displayed using SVG on the mobile device and edited as shown above.
The editing can include updating the repository data.
The original Revit model can be updated on an explicit command call, or automatically, e.g. using the Idling event, e.g. via a

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/drive-revit-through-a-wcf-service.html">
WCF service</a>.

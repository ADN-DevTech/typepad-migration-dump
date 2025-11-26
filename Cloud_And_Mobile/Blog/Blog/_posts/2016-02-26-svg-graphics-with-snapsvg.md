---
layout: "post"
title: "SVG Graphics with Snap.svg"
date: "2016-02-26 03:28:40"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/svg-graphics-with-snapsvg.html "
typepad_basename: "svg-graphics-with-snapsvg"
typepad_status: "Publish"
---

<script src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/53148/snap.svg-min.js"></script>
  <style>

    @import url(http://fonts.googleapis.com/css?family=Share+Tech);

    body {
      background: #efefef;
    }

    .container.svg {
      width: 200px;
      height: 290px;
      position: relative;
      font-family: 'Share Tech', sans-serif;
      color: #444;
    }

    #percent{
      width: 200px;
      height: 200px;
      top: 173px;
      left: 18px;
    }

    #svg1, #svg2 {
      width: 200px;
      height: 200px;
      top: 0;
      left: 0;
    }

    #percent {
      line-height: 20px;
      height: 20px;
      width: 100%;
      top: 169px;
      font-size: 43px;
      text-align: center;
      color: #3da08d;
      opacity: 0.8;
      position: absolute
    }

    p.svg, .btn {
      display: block;
      text-transform: uppercase;
      font-size: 24px;
      top: 30px;
    }

    .btn {
      text-align: center;
      background: #5fc2af;
      color: #fff;
      width: 120px;
      height: 36px;
      line-height: 26px;
      cursor: pointer;
    }

    input.percent {
      border: 0;
      outline: 0;
      border-bottom: 1px solid #eee;
      width: 35px;
      font-family: helvetica;
      font-size: 24px;
      text-transform: capitalise;
      font-family: 'Share Tech', sans-serif;
      background: transparent;
    }

    .controls1 {
      margin-left: 38px;
      width: 140px;
    }

    input.clr {
      width: 170px;
    }

  </style>


<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek">(@F3lipek)</a></p>
<p>Here is some <em>entertainment</em> material to finish the week: I'm working on a project where I'm using some 2D markers overlaid on top of my View &amp; Data 3D models. To achieve that feature&nbsp;no doubt that <a href="https://en.wikipedia.org/wiki/Scalable_Vector_Graphics" target="_blank">SVG</a> is the best tool, this lets you produce beautiful graphics which can be animated and styled at will. There are plenty of JavaScript libraries that wraps svg and alleviate working with it. I used in the past <a href="http://dmitrybaranovskiy.github.io/raphael/">Raphaël</a>&nbsp;and <a href="http://paperjs.org/">Paper.js</a>&nbsp;but the new cool kid in town is <a href="http://snapsvg.io/">Snap.svg</a>&nbsp;a&nbsp;nice and friendly documented tool.</p>
<p>So what I needed is to easily create a simple custom symbol looking like a pie chart, so there are a bunch of libs out there that helps create graphs and charts. I used <a href="http://www.chartjs.org/">Chart.js</a> in one of my previous <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/07/integrating-a-charting-library-with-view-data-api.html">post</a>. However it looked like an overblown for my use case. An svg arc path can do the trick and ended up being fun and more flexible than relying on yet another lib.</p>
<p>Here is how an arc path has to be defined, see <a href="https://developer.mozilla.org/en/docs/Web/SVG/Tutorial/Paths">there</a> for more details about paths:</p>
<pre class="eval line-numbers  language-html"><code class=" language-html">A rx ry x-axis-rotation large-arc-flag sweep-flag x y
a rx ry x-axis-rotation large-arc-flag sweep-flag dx dy</code></pre>
<p>The <em>large-arc</em> and <em>sweep</em> flags might be a bit tricky to figure out, so here is a pictures that helps a lot:</p>

<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a4b63b970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a4b63b970c img-responsive" title="Arcs02" src="/assets/image_4fa2d7.jpg" alt="Arcs02" border="0" /></a>

<br>

<p>Browsing the web, I found <a href="http://codepen.io/rachsmith/pen/BqpCd">an&nbsp;example</a>&nbsp;by <a href="http://codepen.io/rachsmith/">Rachel Smith</a>&nbsp;that&nbsp;&nbsp;helped me getting started with snap.svg and arc paths. I tweaked the original code to be more flexible and allows creating a pie chart sector or donut sector by specifying arguments like inner/outer radius, start angle and delta angle:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Draws a pie sector
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> drawPieSector(snap, centre,
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="background-color:#ffffff;">                       rIn, rOut, startDeg, delta, attr) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> startOut = {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">    x: centre.x + rOut * Math.cos(Math.PI*(startDeg)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">    y: centre.y + rOut * Math.sin(Math.PI*(startDeg)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">)
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">  };
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> endOut = {
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">    x: centre.x + rOut * Math.cos(Math.PI*(startDeg + delta)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">    y: centre.y + rOut * Math.sin(Math.PI*(startDeg + delta)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">)
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="background-color:#ffffff;">  };
</span><span style="color:#800000;background-color:#f0f0f0;">17 
18 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> startIn = {
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">    x: centre.x + rIn * Math.cos(Math.PI*(startDeg + delta)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">    y: centre.y + rIn * Math.sin(Math.PI*(startDeg + delta)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">)
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">  };
</span><span style="color:#800000;background-color:#f0f0f0;">22 
23 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> endIn = {
</span><span style="color:#800000;background-color:#f0f0f0;">24 </span><span style="background-color:#ffffff;">    x: centre.x + rIn * Math.cos(Math.PI*(startDeg)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">25 </span><span style="background-color:#ffffff;">    y: centre.y + rIn * Math.sin(Math.PI*(startDeg)/</span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;">)
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">  };
</span><span style="color:#800000;background-color:#f0f0f0;">27 
28 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> largeArc = delta &gt; </span><span style="color:#0000ff;background-color:#ffffff;">180</span><span style="background-color:#ffffff;"> ? </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;"> : </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> path = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"M"</span><span style="background-color:#ffffff;"> + startOut.x + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + startOut.y +
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">          </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" A"</span><span style="background-color:#ffffff;"> + rOut + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + rOut + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" 0 "</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="background-color:#ffffff;">          largeArc + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">",1 "</span><span style="background-color:#ffffff;"> + endOut.x + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + endOut.y +
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="background-color:#ffffff;">          </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" L"</span><span style="background-color:#ffffff;"> + startIn.x + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + startIn.y +
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">          </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" A"</span><span style="background-color:#ffffff;"> + rIn + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + rIn + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" 0 "</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">35 </span><span style="background-color:#ffffff;">          largeArc + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">",0 "</span><span style="background-color:#ffffff;"> + endIn.x + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + endIn.y +
</span><span style="color:#800000;background-color:#f0f0f0;">36 </span><span style="background-color:#ffffff;">          </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" L"</span><span style="background-color:#ffffff;"> + startOut.x + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">","</span><span style="background-color:#ffffff;"> + startOut.y + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">" Z"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">37 
38 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> path = snap.path(path);
</span><span style="color:#800000;background-color:#f0f0f0;">39 
40 </span><span style="background-color:#ffffff;">  path.attr(attr);
</span><span style="color:#800000;background-color:#f0f0f0;">41 
42 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> path;
</span><span style="color:#800000;background-color:#f0f0f0;">43 </span><span style="background-color:#ffffff;">}</span></pre>

<p>The tweaked example from Rachel looks like this when rewritten with my pie sector function:</p>


<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Draws the percent counter
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> run(percent) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> snap = Snap(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#svg1'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> attr = {
</span><span style="color:#800000;background-color:#f0f0f0;">10 </span><span style="background-color:#ffffff;">    stroke: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#3da08d'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">    fill: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#3da08d'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">    fillOpacity: </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">    strokeWidth: </span><span style="color:#0000ff;background-color:#ffffff;">1
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">  };
</span><span style="color:#800000;background-color:#f0f0f0;">15 
16 </span><span style="background-color:#ffffff;">  Snap.animate(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, percent * </span><span style="color:#0000ff;background-color:#ffffff;">359.99</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (deg) {
</span><span style="color:#800000;background-color:#f0f0f0;">17 
18 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(sector) sector.remove();
</span><span style="color:#800000;background-color:#f0f0f0;">19 
20 </span><span style="background-color:#ffffff;">    sector = drawPieSector(snap,
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">            {x:</span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">, y:</span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">},
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">            </span><span style="color:#0000ff;background-color:#ffffff;">50</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">95</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">            </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, deg,
</span><span style="color:#800000;background-color:#f0f0f0;">24 </span><span style="background-color:#ffffff;">            attr);
</span><span style="color:#800000;background-color:#f0f0f0;">25 
26 </span><span style="background-color:#ffffff;">    percDiv.innerHTML = Math.round(deg/</span><span style="color:#0000ff;background-color:#ffffff;">360</span><span style="background-color:#ffffff;"> * </span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">) +</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%'</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">27 
28 </span><span style="background-color:#ffffff;">  }, </span><span style="color:#0000ff;background-color:#ffffff;">1000</span><span style="background-color:#ffffff;">, mina.easeinout);
</span><span style="color:#800000;background-color:#f0f0f0;">29 </span><span style="background-color:#ffffff;">}</span></pre>

This wouldn't be fun without an interactive example (change value in input and hit run again):

<p>
<div class="container svg" style="left:-180px;">

  <div class="controls1">
    <p class="svg">
      <label>Percent:</label>
      <input class="percent" maxlength="2" type="text" id="input" value="65"/>
    </p>
    <a class="btn" id="run">Run</a>
  </div>

  <div id="percent"></div>
  <svg id="svg1"></svg>

</div>
</p>

<p>
We can now easily write another utility method to draw an entire pie chart creating a sector for each entry in the data argument:
</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Draws a pie chart
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> drawPie(snap, centre, rIn, rOut, data) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> total = </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> data){
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">    total += data[key].value;
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">13 
14 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> startDeg = </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">15 
16 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> pie = snap.group();
</span><span style="color:#800000;background-color:#f0f0f0;">17 
18 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> data){
</span><span style="color:#800000;background-color:#f0f0f0;">19 
20 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> delta = </span><span style="color:#0000ff;background-color:#ffffff;">359.99</span><span style="background-color:#ffffff;"> * data[key].value / total;
</span><span style="color:#800000;background-color:#f0f0f0;">21 
22 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> sector = drawPieSector(snap, centre,
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">            rIn, rOut, startDeg, delta, data[key].attr);
</span><span style="color:#800000;background-color:#f0f0f0;">24 
25 </span><span style="background-color:#ffffff;">    pie.add(sector);
</span><span style="color:#800000;background-color:#f0f0f0;">26 
27 </span><span style="background-color:#ffffff;">    startDeg += delta;
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">29 
30 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> pie;
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">}</span></pre>


And to use our pie chart function a little test that displays three sector for RGB color values and blends the result at the centre:


<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Draws the color chart
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> drawColorChart() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> r = parseInt(document.getElementById(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'inputR'</span><span style="background-color:#ffffff;">).value);
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> g = parseInt(document.getElementById(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'inputG'</span><span style="background-color:#ffffff;">).value);
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> b = parseInt(document.getElementById(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'inputB'</span><span style="background-color:#ffffff;">).value);
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> data = {
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">    Red: {
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">      value: r,
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">      attr: {
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">        stroke: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb('</span><span style="background-color:#ffffff;"> + r + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">', 0, 0)'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">16 </span><span style="background-color:#ffffff;">        fill: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb('</span><span style="background-color:#ffffff;"> + r + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">', 0, 0)'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">17 </span><span style="background-color:#ffffff;">        fillOpacity: </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">        strokeWidth: </span><span style="color:#0000ff;background-color:#ffffff;">1
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">    },
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">    Green: {
</span><span style="color:#800000;background-color:#f0f0f0;">22 </span><span style="background-color:#ffffff;">      value: g,
</span><span style="color:#800000;background-color:#f0f0f0;">23 </span><span style="background-color:#ffffff;">      attr: {
</span><span style="color:#800000;background-color:#f0f0f0;">24 </span><span style="background-color:#ffffff;">        stroke: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb(0, '</span><span style="background-color:#ffffff;"> + g + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">', 0)'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">25 </span><span style="background-color:#ffffff;">        fill: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb(0, '</span><span style="background-color:#ffffff;"> + g + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">', 0)'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">26 </span><span style="background-color:#ffffff;">        fillOpacity: </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">27 </span><span style="background-color:#ffffff;">        strokeWidth: </span><span style="color:#0000ff;background-color:#ffffff;">1
</span><span style="color:#800000;background-color:#f0f0f0;">28 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">29 </span><span style="background-color:#ffffff;">    },
</span><span style="color:#800000;background-color:#f0f0f0;">30 </span><span style="background-color:#ffffff;">    Blue: {
</span><span style="color:#800000;background-color:#f0f0f0;">31 </span><span style="background-color:#ffffff;">      value: b,
</span><span style="color:#800000;background-color:#f0f0f0;">32 </span><span style="background-color:#ffffff;">      attr: {
</span><span style="color:#800000;background-color:#f0f0f0;">33 </span><span style="background-color:#ffffff;">        stroke: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb(0, 0, '</span><span style="background-color:#ffffff;"> + b + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">')'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">34 </span><span style="background-color:#ffffff;">        fill: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb(0, 0, '</span><span style="background-color:#ffffff;"> + b + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">')'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">35 </span><span style="background-color:#ffffff;">        fillOpacity: </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">36 </span><span style="background-color:#ffffff;">        strokeWidth: </span><span style="color:#0000ff;background-color:#ffffff;">1
</span><span style="color:#800000;background-color:#f0f0f0;">37 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">38 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">39 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">40 
41 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> snap = Snap(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#svg2'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">42 
43 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(chart) chart.remove();
</span><span style="color:#800000;background-color:#f0f0f0;">44 
45 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> pie = drawPie(snap,
</span><span style="color:#800000;background-color:#f0f0f0;">46 </span><span style="background-color:#ffffff;">    {x:</span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">, y:</span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">},
</span><span style="color:#800000;background-color:#f0f0f0;">47 </span><span style="background-color:#ffffff;">    </span><span style="color:#0000ff;background-color:#ffffff;">50</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">95</span><span style="background-color:#ffffff;">, data);
</span><span style="color:#800000;background-color:#f0f0f0;">48 
49 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> circle = snap.circle(</span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">100</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">50</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">50 
51 </span><span style="background-color:#ffffff;">  circle.attr({
</span><span style="color:#800000;background-color:#f0f0f0;">52 </span><span style="background-color:#ffffff;">    stroke: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb('</span><span style="background-color:#ffffff;"> + r + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">','</span><span style="background-color:#ffffff;"> + g + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">', '</span><span style="background-color:#ffffff;"> + b + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">')'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">53 </span><span style="background-color:#ffffff;">    fill: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'rgb('</span><span style="background-color:#ffffff;"> + r + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">','</span><span style="background-color:#ffffff;"> + g + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">', '</span><span style="background-color:#ffffff;"> + b + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">')'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">54 </span><span style="background-color:#ffffff;">    fillOpacity: </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">55 </span><span style="background-color:#ffffff;">    strokeWidth: </span><span style="color:#0000ff;background-color:#ffffff;">1
</span><span style="color:#800000;background-color:#f0f0f0;">56 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">57 
58 </span><span style="background-color:#ffffff;">  chart = snap.group(pie, circle);
</span><span style="color:#800000;background-color:#f0f0f0;">59 </span><span style="background-color:#ffffff;">}</span></pre>

Give it a try there:

<br>
<br>

<div class="container svg" style="height:400px; left: -180px;">
  <div class="controls2">
    <p class="svg">
      <label>R:</label>
      <input class="clr" type="range" id="inputR" value="200" min="1" max="255" step="1" onchange="drawColorChart()"/>
    </p>
    <p class="svg">
      <label>G:</label>
      <input class="clr" type="range" id="inputG" value="200" min="1" max="255" step="1" onchange="drawColorChart()"/>
    </p>
    <p class="svg">
      <label>B:</label>
      <input class="clr" type="range" id="inputB" value="200" min="1" max="255" step="1" onchange="drawColorChart()"/>
    <p class="svg">
  </div>
  <svg id="svg2"></svg>
</div>

<br>
<br>

Full source code for both sample below:

<br>
<br>

<script src="https://gist.github.com/leefsmp/f721678ee443f1031a74.js"></script>

<script>

  //Original version
  //http://codepen.io/rachsmith/pen/BqpCd

  var runBtn = document.getElementById('run'),
          percDiv = document.getElementById('percent'),
          input = document.getElementById('input');

  input.onkeyup = function(evt) {
    if(isNaN(input.value)) {
      input.value = '';
    }
  };

  runBtn.onclick = function() {
    run(input.value/100);
  };

  var sector = null;
  var chart = null;

  ///////////////////////////////////////////////////////////////////
  // Draws the percent counter
  //
  ///////////////////////////////////////////////////////////////////
  function run(percent) {

    var snap = Snap('#svg1');

    var attr = {
      stroke: '#3da08d',
      fill: '#3da08d',
      fillOpacity: 0.5,
      strokeWidth: 1
    };

    Snap.animate(0, percent * 359.99, function (deg) {

      if(sector) sector.remove();

      sector = drawPieSector(snap,
              {x:100, y:100},
              50, 95,
              -90, deg,
              attr);

      percDiv.innerHTML = Math.round(deg/360 * 100) +'%';

    }, 1000, mina.easeinout);
  }

  ///////////////////////////////////////////////////////////////////
  // Draws the color chart
  //
  ///////////////////////////////////////////////////////////////////
  function drawColorChart() {

    var r = parseInt(document.getElementById('inputR').value);
    var g = parseInt(document.getElementById('inputG').value);
    var b = parseInt(document.getElementById('inputB').value);

    var data = {
      Red: {
        value: r,
        attr: {
          stroke: 'rgb(' + r + ', 0, 0)',
          fill: 'rgb(' + r + ', 0, 0)',
          fillOpacity: 0.5,
          strokeWidth: 1
        }
      },
      Green: {
        value: g,
        attr: {
          stroke: 'rgb(0, ' + g + ', 0)',
          fill: 'rgb(0, ' + g + ', 0)',
          fillOpacity: 0.5,
          strokeWidth: 1
        }
      },
      Blue: {
        value: b,
        attr: {
          stroke: 'rgb(0, 0, ' + b + ')',
          fill: 'rgb(0, 0, ' + b + ')',
          fillOpacity: 0.5,
          strokeWidth: 1
        }
      }
    }

    var snap = Snap('#svg2');

    if(chart) chart.remove();

    var pie = drawPie(snap,
      {x:100, y:100},
      50, 95, data);

    var circle = snap.circle(100, 100, 50);

    circle.attr({
      stroke: 'rgb(' + r + ',' + g + ', ' + b + ')',
      fill: 'rgb(' + r + ',' + g + ', ' + b + ')',
      fillOpacity: 1,
      strokeWidth: 1
    });

    chart = snap.group(pie, circle);
  }

///////////////////////////////////////////////////////////////////
// Draws a pie sector
//
///////////////////////////////////////////////////////////////////
function drawPieSector(snap, centre,
                       rIn, rOut, startDeg, delta, attr) {

  var startOut = {
    x: centre.x + rOut * Math.cos(Math.PI*(startDeg)/180),
    y: centre.y + rOut * Math.sin(Math.PI*(startDeg)/180)
  };

  var endOut = {
    x: centre.x + rOut * Math.cos(Math.PI*(startDeg + delta)/180),
    y: centre.y + rOut * Math.sin(Math.PI*(startDeg + delta)/180)
  };

  var startIn = {
    x: centre.x + rIn * Math.cos(Math.PI*(startDeg + delta)/180),
    y: centre.y + rIn * Math.sin(Math.PI*(startDeg + delta)/180)
  };

  var endIn = {
    x: centre.x + rIn * Math.cos(Math.PI*(startDeg)/180),
    y: centre.y + rIn * Math.sin(Math.PI*(startDeg)/180)
  };

  var largeArc = delta > 180 ? 1 : 0;

  var path = "M" + startOut.x + "," + startOut.y +
          " A" + rOut + "," + rOut + " 0 " +
          largeArc + ",1 " + endOut.x + "," + endOut.y +
          " L" + startIn.x + "," + startIn.y +
          " A" + rIn + "," + rIn + " 0 " +
          largeArc + ",0 " + endIn.x + "," + endIn.y +
          " L" + startOut.x + "," + startOut.y + " Z";

  var path = snap.path(path);

  path.attr(attr);

  return path;
}

  ///////////////////////////////////////////////////////////////////
  // Draws a pie chart
  //
  ///////////////////////////////////////////////////////////////////
  function drawPie(snap, centre, rIn, rOut, data) {

    var total = 0;

    for(var key in data){

      total += data[key].value;
    }

    var startDeg = 0;

    var pie = snap.group();

    for(var key in data){

      var delta = 359.99 * data[key].value / total;

      var sector = drawPieSector(snap, centre,
              rIn, rOut, startDeg, delta, data[key].attr);

      pie.add(sector);

      startDeg += delta;
    }

    return pie;
  }

  run(input.value/100);
  drawColorChart();

</script>

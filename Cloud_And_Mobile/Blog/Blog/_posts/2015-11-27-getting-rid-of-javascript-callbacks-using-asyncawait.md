---
layout: "post"
title: "Getting rid of JavaScript callbacks using async/await"
date: "2015-11-27 11:51:06"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/11/getting-rid-of-javascript-callbacks-using-asyncawait.html "
typepad_basename: "getting-rid-of-javascript-callbacks-using-asyncawait"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>The awesome <a href="https://msdn.microsoft.com/en-us/library/hh191443.aspx" target="_self">async/await syntax</a> originally introduced by Microsoft in C# 4.5 is proposed as syntactic enhancement for <a href="http://kangax.github.io/compat-table/es7/" target="_self">ES7</a>, the next JavaScript standard specification. While this feature will not be supported out of the box by browsers before at least a few years, it is however possible to start using today in your code with the help you great tools called transpilers, which translate your ES7 syntax into usable JavaScript supported by current browsers.<a href="http://dailyjs.com/2015/02/26/babel/" target="_self"><br /></a></p>
<p>The setup is rather straightforward if you are using build tools like <a href="https://webpack.github.io/" target="_self">Webpack</a>&nbsp;then you can integrate seamlessly the great <a href="https://babeljs.io/" target="_self">Babel</a> transpiler in your build process. Babel 6 has just been released and it now supports async/await keywords. For comprehensive details on how to integrate Babel with Webpack, I recommend taking a look at <a href="http://jamesknelson.com/using-es6-in-the-browser-with-babel-6-and-webpack/?utm_source=javascriptweekly&amp;utm_medium=email" target="_self">this article</a>.</p>
<p>Now that you can automatically transpile your js code, let's take a look at how to add async/await to make your callbacks go away:</p>
<p>Let's use a viewer method that takes a callback as argument:</p>
<p><strong><em>viewer.getProperties(dbId, cb)</em></strong></p>
<p>The&nbsp;first step is to promisify that method. ES6 promises are also supported by Babel, so we can write the following code:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Async wrapper for viewer.getProperties
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">viewer.getPropertiesAsync = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(dbId) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Promise(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(resolve, reject) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    viewer.getProperties(dbId, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(result){
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (result.properties) {
</span><span style="color:#800000;background-color:#f0f0f0;">12 
13 </span><span style="background-color:#ffffff;">        resolve(result.properties);
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">      </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">        reject(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Error(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Error getting properties'</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">      }
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">20 </span><span style="background-color:#ffffff;">  });
</span><span style="color:#800000;background-color:#f0f0f0;">21 </span><span style="background-color:#ffffff;">}</span></pre>

That's it, we can now asynchronously invoke our wrapper in an async prefixed method as follow:

<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:10pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// async method
</span><span style="color:#800000;background-color:#f0f0f0;"> 3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">/////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="background-color:#ffffff;">async </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> dumpProperties(dbId) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 
 7 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">try</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;"> 8 
 9 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">let</span><span style="background-color:#ffffff;"> properties = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">await</span><span style="background-color:#ffffff;"> viewer.getPropertiesAsync(dbId);
</span><span style="color:#800000;background-color:#f0f0f0;">10 
11 </span><span style="background-color:#ffffff;">    properties.map((prop) =&gt; {
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">      console.log(prop)
</span><span style="color:#800000;background-color:#f0f0f0;">13 </span><span style="background-color:#ffffff;">    });
</span><span style="color:#800000;background-color:#f0f0f0;">14 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">15 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">catch</span><span style="background-color:#ffffff;">(ex){
</span><span style="color:#800000;background-color:#f0f0f0;">16 
17 </span><span style="background-color:#ffffff;">    console.log(ex);
</span><span style="color:#800000;background-color:#f0f0f0;">18 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">19 </span><span style="background-color:#ffffff;">}</span></pre>

Pretty neat right? 

You can find tons of other articles about the async/await feature in ES7, <a href="http://blogs.msdn.com/b/eternalcoding/archive/2015/09/30/javascript-goes-to-asynchronous-city.aspx">here</a> is one that I liked particularly.

The full source code of my ES7 Async extension for the viewer is available below and you can also test a live version from my gallery (output is dumped to the browser console): 

<a href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.es7Async">Autodesk.ADN.Viewing.Extension.ES7Async</a>

<br>
<br>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f21194970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f21194970b img-responsive" alt="Screen Shot 2015-11-27 at 11.48.22" title="Screen Shot 2015-11-27 at 11.48.22" src="/assets/image_d37186.jpg" /></a><br />

<script src="https://gist.github.com/leefsmp/4aa3eb69b5d3c3325bbe.js"></script>

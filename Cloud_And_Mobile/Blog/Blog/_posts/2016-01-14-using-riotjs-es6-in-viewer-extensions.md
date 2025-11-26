---
layout: "post"
title: "Using Riot.js + ES6 in viewer extensions"
date: "2016-01-14 20:01:48"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/using-riotjs-es6-in-viewer-extensions.html "
typepad_basename: "using-riotjs-es6-in-viewer-extensions"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>That's my first post of 2016, so a <em>Happy New Year!</em> is in order. During the break, I've been playing with a less popular, however rising, JS front-end framework called <a href="http://riotjs.com/">Riot.js</a>. Advertised on its own website as <em>"a React-like user interface micro-library"</em>, Riot lets you define <em>"custom tags"</em> (AKA web components) which can be inserted inside your html as elements with self-contained JavaScript logic.</p>
<p>To me, the most seducing feature of Riot is its tininess and simplicity of use. Here is a picture that worths thousand words:</p>
<p><a class="asset-img-link" style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08ac9668970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08ac9668970d img-responsive" style="margin: 0px 5px 5px 0px;" title="Screen Shot 2016-01-11 at 10.02.39" src="/assets/image_945101.jpg" alt="Screen Shot 2016-01-11 at 10.02.39" /></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>

<p>You can write Riot&nbsp;custom tags inside .tag files which are then referenced by your html the same way you reference a plain .js script. Here is an example extracted from the API documentation, for more details I invite to check the <a href="http://riotjs.com/api/">page</a> directly:</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:12pt;"><span style="color:#800000;background-color:#f0f0f0;">1 </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">my-tag</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">2 </span><span style="background-color:#ffffff;">  </span><span style="background-color:#efefef;">&lt;</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">h3</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">3 </span><span style="background-color:#ffffff;">    {opts.title}
</span><span style="color:#800000;background-color:#f0f0f0;">4 </span><span style="background-color:#ffffff;">  </span><span style="background-color:#efefef;">&lt;/</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">h3</span><span style="background-color:#efefef;">&gt;
</span><span style="color:#800000;background-color:#f0f0f0;">5 
6 </span><span style="background-color:#ffffff;">  var title = opts.title
</span><span style="color:#800000;background-color:#f0f0f0;">7 </span><span style="background-color:#ffffff;">  
</span><span style="color:#800000;background-color:#f0f0f0;">8 </span><span style="background-color:#efefef;">&lt;/</span><span style="color:#000080;background-color:#efefef;font-weight:bold;">my-tag</span><span style="background-color:#efefef;">&gt;</span></pre>

That's nice but another cool feature is the possibility to write the component as plain JavaScript using a template string. For a simple tag like this one the template would fit in a single line, however as your components become more complex, you would need to fit the html template on multiple lines to accommodate readability. That's were <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/template_strings">ES6 multiline strings</a> come into the game. The previous tag would look as follow in plain ES6 js:

<br>
<br>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:12pt;"><span style="color:#800000;background-color:#f0f0f0;"> 1 </span><span style="background-color:#ffffff;">riot.tag(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'my-tag'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 2 
 3 </span><span style="background-color:#ffffff;">  </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">`&lt;h3&gt;
</span><span style="color:#800000;background-color:#f0f0f0;"> 4 </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">    {title}
</span><span style="color:#800000;background-color:#f0f0f0;"> 5 </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">  &lt;/h3&gt;
</span><span style="color:#800000;background-color:#f0f0f0;"> 6 </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">  `</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 7 
 8 </span><span style="background-color:#ffffff;">  </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(opts) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 9 
10 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.title = opts.title
</span><span style="color:#800000;background-color:#f0f0f0;">11 </span><span style="background-color:#ffffff;">  }
</span><span style="color:#800000;background-color:#f0f0f0;">12 </span><span style="background-color:#ffffff;">);</span></pre>

From there, we can write more complex riot components fitting in a single viewer extension plain js file which can be loaded/unloaded dynamically. Here is a picture of the demo panel I created and a link where you can <a href="http://viewer.autodesk.io/node/gallery/embed?id=560c6c57611ca14810e1b2bf&extIds=Autodesk.ADN.Viewing.Extension.Riot">test it live</a>:

<br>
<br>

<a class="asset-img-link"  style="" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08ac9777970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08ac9777970d img-responsive" alt="Screen Shot 2016-01-11 at 09.51.44" title="Screen Shot 2016-01-11 at 09.51.44" src="/assets/image_75b418.jpg" style="margin: 0px 5px 5px 0px;" /></a>

<br>

The full implementation of the riot panel extension is here, as you can see zero jQuery in the code:

<br>
<br>

<script src="https://gist.github.com/leefsmp/049035e5de704493e6c2.js"></script>

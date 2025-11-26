---
layout: "post"
title: "DefinitionGroup Set Name and Iceland Blue Lagoon"
date: "2014-09-05 09:32:33"
author: "Jaime Rosales"
categories:
  - ".NET"
  - "Jaime Rosales"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/09/definitiongroup-set-name-and-iceland-blue-lagoon.html "
typepad_basename: "definitiongroup-set-name-and-iceland-blue-lagoon"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/jaime-rosales.html">Jaime Rosales</a></p>
<p>After a short week, Friday is here, and I want to take the opportunity for wishing all&#0160; of you a very relaxing first weekend of September, enjoy your activities and recharge those batteries for next week. I had an amazing trip for my honeymoon and still have my batteries fully recharged, how can I not, just check it out where I got mine like that.</p>
<p>Blue Lagoon, Iceland.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d064190a970c-pi"><img alt="IMG_2708" border="0" height="368" src="/assets/image_924954.jpg" style="display: inline; border-width: 0px;" title="IMG_2708" width="490" /></a></p>
<p>Back to Revit.</p>
<p>Will keep this post short since it’s Friday. Here is a snippet of code that I got from one of our Revit Engineers regarding the DefinitionGroup setting of its name. Unfortunately what one of our customers needed was to rename the name on the DefinitionGroup but since it’s a read-only property, that couldn’t be approached.&#0160;</p>
<p>Here I share the code for you to try it. Enjoy!</p>
<pre style="font-size: 13px; font-family: consolas; background: #1e1e1e; color: gainsboro;"><span style="color: #569cd6;">public</span>&#0160;<span style="color: #569cd6;">static</span>&#0160;<span style="color: #4ec9b0;">DefinitionGroup</span>&#0160;<span style="color: white;">GetOrCreateSharedParamsGroup</span><br />&#0160; (<span style="color: #4ec9b0;">DefinitionFile</span>&#0160;<span style="color: white;">defFile</span>, <span style="color: #569cd6;">string</span>&#0160;<span style="color: white;">grpName</span>)<br />{<br />&#0160; <span style="color: #569cd6;">try</span>&#0160;<span style="color: #608b4e;">// generic</span><br />&#0160; {<br />&#0160;&#0160; <span style="color: #4ec9b0;">DefinitionGroup</span>&#0160;<span style="color: white;">defGrp</span>&#0160;<span style="color: #b4b4b4;">=</span>&#0160;<span style="color: white;">defFile</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Groups</span><span style="color: #b4b4b4;">.</span><span style="color: white;">get_Item</span>(<span style="color: white;">grpName</span>);<br />&#0160;&#0160; <span style="color: #569cd6;">if</span> (<span style="color: #569cd6;">null</span>&#0160;<span style="color: #b4b4b4;">==</span>&#0160;<span style="color: white;">defGrp</span>) <span style="color: white;">defGrp</span>&#0160;<span style="color: #b4b4b4;">=</span>&#0160;<span style="color: white;">defFile</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Groups</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Create</span>(<span style="color: white;">grpName</span>);<br />&#0160;&#0160; <span style="color: #569cd6;">return</span>&#0160;<span style="color: white;">defGrp</span>;<br />&#0160; }<br />&#0160; <span style="color: #569cd6;">catch</span> (<span style="color: #4ec9b0;">Exception</span>&#0160;<span style="color: white;">ex</span>)<br />&#0160; {<br />&#0160;&#0160; <span style="color: #4ec9b0;">MessageBox</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Show</span>(<span style="color: #569cd6;">string</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Format</span>(<span style="color: #d69d85;">&quot;ERROR: Failed to get or</span><br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #d69d85;">&#0160; create Shared Params Group: {0}&quot;</span>, <span style="color: white;">ex</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Message</span>));<br />&#0160;&#0160; <span style="color: #569cd6;">return</span>&#0160;<span style="color: #569cd6;">null</span>;<br />&#0160; }<br />}</pre>
<p>Thank you for reading. Have a great weekend.</p>

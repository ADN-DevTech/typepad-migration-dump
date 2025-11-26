---
layout: "post"
title: "Finding Project Parameter and Aerial Trick or Treat"
date: "2014-10-31 12:31:04"
author: "Jaime Rosales"
categories:
  - ".NET"
  - "Jaime Rosales"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/10/finding-project-parameter-and-aerial-trick-or-treat.html "
typepad_basename: "finding-project-parameter-and-aerial-trick-or-treat"
typepad_status: "Publish"
---

<p style="text-align: left;">By <a href="http://adndevblog.typepad.com/aec/jaime-rosales.html">Jaime Rosales</a></p>
<p style="text-align: left;">Last day of October and that could only mean one thing, trick-or-treaters are making their rounds in NYC, looking for delicious candy and also the creativity can be seen on many costumes around the streets of the city. I haven’t dressed for today’s festivities yet but on my next post I will share what I end up doing for tonight. For now I will share a black and white aerial picture of Manhattan ( to make it a bit spooky, perhaps ?). Hope you guys like it.</p>
<p style="text-align: left;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fe1c26970b-pi"><img alt="IMG_0580" border="0" height="262" src="/assets/image_624432.jpg" style="display: inline; border-width: 0px;" title="IMG_0580" width="480" /></a></p>
<p style="text-align: left;">Back to Revit, This week I want to give a shout out to one of our API experts, Aaron who recently rejoined the DevTech China team and got to work on a great case this past week. Thank you Aaron and glad to have you back.</p>
<p style="text-align: left;"><strong>Question:</strong> Given an Element object in the C# API, is there a simple way to find Project Parameters that are associated with that Element? Thanks.</p>
<p style="text-align: left;"><strong>Aaron Answer</strong>:Given an Element, when you iterate it&#39;s parameter, you can check some properties of the parameter to determine if it is a project or shared parameter, please see below example code:</p>
<pre style="font-size: 13px; font-family: consolas; background: #1e1e1e; color: gainsboro;"><span style="color: #569cd6;">if</span> (<span style="color: white;">parameter</span><span style="color: #b4b4b4;">.</span><span style="color: white;">IsShared</span>)<br />{<br />&#0160; <span style="color: #608b4e;">//this is a shared parameter</span><br />}<br /><span style="color: #569cd6;">else</span><br />{<br />&#0160; <span style="color: #4ec9b0;">InternalDefinition</span>&#0160;<span style="color: white;">def</span>&#0160;<span style="color: #b4b4b4;">=</span>&#0160;<span style="color: white;">parameter</span><span style="color: #b4b4b4;">.</span><span style="color: white;">Definition</span>&#0160;<span style="color: #569cd6;">as</span>&#0160;<span style="color: #4ec9b0;">InternalDefinition</span>;<br />&#0160; <span style="color: #569cd6;">if</span> (<span style="color: white;">def</span>&#0160;<span style="color: #b4b4b4;">!=</span>&#0160;<span style="color: #569cd6;">null</span>)<br />&#0160; {<br />&#0160;&#0160;&#0160; <span style="color: #569cd6;">if</span> (<span style="color: white;">def</span><span style="color: #b4b4b4;">.</span><span style="color: white;">BuiltInParameter</span>&#0160;<span style="color: #b4b4b4;">==</span>&#0160;<span style="color: #b8d7a3;">BuiltInParameter</span><span style="color: #b4b4b4;">.</span><span style="color: white;">INVALID</span>)<br />&#0160;&#0160;&#0160; {<br />&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #608b4e;">// this is a project parameter</span><br />&#0160;&#0160;&#0160; }<br />&#0160; }<br />}</pre>
<p style="text-align: left;">Hope it helps, thanks.</p>
<p style="text-align: left;"><strong>Response:</strong>Thanks, Aaron,This helps.</p>
<p style="text-align: left;">Thank you Aaron for such a simple but also awesome way to solve this.</p>
<p style="text-align: left;">Thank you for reading and until next time.</p>

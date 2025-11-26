---
layout: "post"
title: "What's a callback and what's its purpose?"
date: "2012-08-13 02:08:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "Maya"
  - "MotionBuilder"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/whats-a-callback-and-whats-its-purpose.html "
typepad_basename: "whats-a-callback-and-whats-its-purpose"
typepad_status: "Publish"
---

<p>A callback is something you can setup to watch a specific behaviour, and when that behaviour is performed you do something special which is out of the normal behaviour or functionality of MotionBuilder (or Maya).</p>
<p>Here is my everyday English example of a callbacks :)</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017616ca6b54970c-pi" style="display: inline;"><img alt="Images" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017616ca6b54970c" src="/assets/image_f95417.jpg" title="Images" /></a><br />You assign me to watch a front door, and when people come in the door I hand them a visitor pass, without me watching the front door people would just come through the door and never get a visitors pass.</p>
<ul>
<li>Kristine = callback</li>
<li>front door = MotionBuilder</li>
<li>visitor pass = is the something special behaviour</li>
</ul>
<p>So in MotionBuilder (or Maya), let’s say I want everyone to follow a special naming convention for their scene files,&#0160;then I would set up a callback to watch anytime someone creates a new file. Then when this happens&#0160;MotionBuilder calls a function in my code to save the file with a special name.</p>
<p>A very important thing to point out is that callbacks are efficient in MotionBuilder and Maya,&#0160;so you do not need to be concerned, that they will&#0160;slow down the application even though a callback is always watching what you told it to watch.</p>
<p>Callbacks allow you to add&#0160;an personal level of customization to your production, studio or project. Since we cannot anticipate all&#0160;the different usages of callbacks we just keep it very open where you can monitor most things in MotionBuilder and Maya. But having&#0160;said this,&#0160;this also means you must set the callbacks up yourself, since the combination of things to watch and&#0160;things to do could be endless.</p>
<p>Another example is when someone changes a property, you maybe want to give them a warning…but how will you know when the user changes the property? They could do it at any time during their session usage, and you want the warning to be triggered right after they change it, not at a hard coded point of time... so here is where you want to setup a callback on property values, that will output a warning when this occurs.</p>
<p>Enjoy,</p>
<p>Kristine</p>
<p>&#0160;</p>

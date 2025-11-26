---
layout: "post"
title: "MotionBuilder code convention change"
date: "2013-01-09 00:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "MotionBuilder"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/01/motionbuilder-code-convention-change.html "
typepad_basename: "motionbuilder-code-convention-change"
typepad_status: "Publish"
---

<p>In the past, MotionBuilder classes may be prefixed with a &#39;H&#39;. The SDK now will enforce not using this syntax, and you will need to change your code accordingly. To avoid too much work here is a tip on doing it in VisualStudio or Notepad++ using Regular Expression.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c34cbeb26970b-pi" style="display: inline;"><img alt="Regexpr" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c34cbeb26970b" src="/assets/image_f1fc03.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Regexpr" /></a></p>
<p>In copy paste form: </p>
<p>{^|:b|\(|,|&lt;}H{FB:i*}{[&amp;*\)&gt;]*:b*}</p>
<p>\1\2*\3</p>
<p>&#0160;</p>

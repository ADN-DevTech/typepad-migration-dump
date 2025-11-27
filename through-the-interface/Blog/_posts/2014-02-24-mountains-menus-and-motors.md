---
layout: "post"
title: "Mountains, menus and motors"
date: "2014-02-24 09:34:46"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Morgan"
  - "Personal"
  - "User interface"
original_url: "https://www.keanw.com/2014/02/mountains-menus-and-motors.html "
typepad_basename: "mountains-menus-and-motors"
typepad_status: "Publish"
---

<p>I’m up in the mountains for the week as the kids have their winter half-term break (which in Switzerland is actually intended for kids to ski… very civilized :-). I’m going to take a few meetings and probably write a few blog posts, but otherwise I’m planning on hitting the slopes whenever I get the chance.</p>  <p>I did want to comment on how my research into right-click menus ended up, last week. I put together some code that made use of the technique shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/05/creating_a_part.html" target="_blank">this previous post</a> to create and load a partial CUIx file containing <a href="http://through-the-interface.typepad.com/through_the_interface/2014/02/adding-a-context-menu-item-with-an-icon-for-a-specific-autocad-object-type-using-net.html" target="_blank">our OBJECT_ACAD_TABLE shortcut menu</a>, but unfortunately it seems <a href="http://forums.augi.com/showthread.php?32551-How-to-get-custom-shortcuts-to-override-or-work-with-the-standard-AutoCAD-menus" target="_blank">AutoCAD doesn’t use such shortcut menus from “partial” files</a> (i.e. those that aren’t loaded as “main”). So the options are either to make our file the main one – loading <em>acad.cuix</em> etc. as partial files – or to do as we’ve done and add the options directly into <em>acad.cuix</em>. I’m far from being the CUI expert, though, so would love to have someone confirm this (<a href="https://twitter.com/lappenlocker" target="_blank">Bob Bell</a> – are you out there?).</p>  <p>Also, things are moving ahead with the project for the upcoming <a href="http://www.salon-auto.ch/en/" target="_blank">Geneva Motor Show</a>. I’ve received my exhibitor’s pass and will be heading over bright and early next Monday to help with the set-up of the Morgan stand (my part being largely software-related I can leave it until Monday – the stand will be built over the weekend and the show itself will kick-off with the press days on Tuesday and Wednesday). <a href="http://through-the-interface.typepad.com/through_the_interface/2013/03/geneva-motor-show-2013.html" target="_blank">I did go to the show last year</a> (and I’d been once or twice before that, some years ago), but this is the first time I’m participating in some way – it should be a very interesting experience. I’ll explain more of what it’s all about when the project is unveiled to the public, of course.</p>

---
layout: "post"
title: "Adding a global keyword menu to AutoCAD using WPF &ndash; Part 1"
date: "2015-02-23 10:19:04"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Notification / Events"
  - "Runtime"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2015/02/adding-a-global-keyword-menu-to-autocad-using-wpf-part-1.html "
typepad_basename: "adding-a-global-keyword-menu-to-autocad-using-wpf-part-1"
typepad_status: "Publish"
---

<p>I’m up in the mountains, supposedly on vacation, but as one of our children woke up with a fever, I’m skipping the morning session on the slopes to stay home with him. Which gives me the chance to start writing up a little project I’ve been working on for our Localization team.</p>
<p>Here’s the idea… apparently it’s relatively common in certain countries for AutoCAD users to learn the product in English but then end up working with a localized version of the software. While it’s always possible to use global commands and keywords by prefixing an underscore, it’s not always something people remember to do. Which can understandably leads to frustration.</p>
<p>Our Localization team is keen to find a way to help these users, such as by streamlining their ability to use global commands and keywords in a localized AutoCAD product. One suggestion was to build an app that displays a list of the global keywords they can use either as an aide-memoire or to launch the keyword itself.</p>
<p>This seemed like a fun little project, so I ended up putting together an initial version over the weekend. Here are a few comments on what I ended up building:</p>
<p>1. An app that uses WPF to display a Popup in the bottom right of the AutoCAD window.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7529423970b-pi" target="_blank"><img alt="Keyword popup" border="0" height="260" src="/assets/image_41734.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="Keyword popup" width="194" /></a></p>
<p>2. The list gets populated by global keywords that AutoCAD prompts for. Luckily there are events that provide us with this information.</p>
<p>3. When an item in the list is double-clicked the keyword gets sent to the command-line – with the underscore prefix, of course.</p>
<p>4. Some commands need to be treated as “special”: as they request input but then don’t complete straightaway, we need to use a timer to close the popup after a certain interval elapses. For example, the MTEXT command prompts for a window for the text area, but then launches the in-place editor (IPE) rather than completing or having AutoCAD become quiescent. We want to close our keyword popup while the IPE is active.</p>
<p>That’s basically the scope of the project – for now, at least. As it’s so far possible to implement using published APIs, I’ll go ahead and share the code during the course of this week.</p>

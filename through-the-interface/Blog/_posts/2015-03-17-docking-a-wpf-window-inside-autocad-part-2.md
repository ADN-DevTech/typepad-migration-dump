---
layout: "post"
title: "Docking a WPF window inside AutoCAD &ndash; Part 2"
date: "2015-03-17 18:49:42"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Notification / Events"
  - "Runtime"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2015/03/docking-a-wpf-window-inside-autocad-part-2.html "
typepad_basename: "docking-a-wpf-window-inside-autocad-part-2"
typepad_status: "Publish"
---

<p>I’m happy to say that the implementation I mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2015/03/docking-a-wpf-window-inside-autocad-part-1.html" target="_blank">the last post</a> ended up being pretty straightforward. Which is actually great, as I have some important posts to work on for next week. :-)</p>  <p>Today we’re going to take a look at the next stage of the “command-line helper” implementation: basic right-click movement of the global keywords dialog, so we can set a custom location for the dialog without needing to use the KWSDOCK command.</p>  <p>Here’s the code in action:</p>  <br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d0ee6fb0970c-pi"><img title="CmdLineHelper with right-click dialog move" style="float: none; margin-left: auto; display: block; margin-right: auto" alt="CmdLineHelper with right-click dialog move" src="/assets/image_3700.jpg" width="400" height="211" /></a>  <br />  <p>The main work for this stage was to add support for right-click, mouse move and right mouse-button up events, making sure that the dialog is displaced accurately irrespective of where the mouse gets moved. And that’s actually a really nice feature of this version: you can move the dialog off the main AutoCAD window, if you want to (this wasn’t supported in the KWSDOCK command as we’re using Editor.GetPoint() to let the user select custom locations).</p>  <p>I’ve turned off the mouse cursor in the above video, but you would see a “all-direction scrolling” cursor: the pan cursor isn’t a standard one in Windows, so I just used something that was good enough for this particular app.</p>  <p>There were some other minor things to work through, but hopefully the C# code in <a href="http://through-the-interface.typepad.com/files/CmdLineHelper3.zip" target="_blank">this version of the project</a> is straightforward to understand.</p>  <p>In the next part in this series we’re going to extend this implementation to support docking – and preview of dock locations – when you right-click drag the dialog close to corners of the drawing window.</p>

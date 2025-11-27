---
layout: "post"
title: "Docking a WPF window inside AutoCAD &ndash; Part 1"
date: "2015-03-16 18:12:58"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Notification / Events"
  - "User interface"
  - "WPF"
original_url: "https://www.keanw.com/2015/03/docking-a-wpf-window-inside-autocad-part-1.html "
typepad_basename: "docking-a-wpf-window-inside-autocad-part-1"
typepad_status: "Publish"
---

<p>During the course of this week we’re going to look at extending <a href="http://through-the-interface.typepad.com/through_the_interface/2015/03/adding-support-for-global-keywords-at-autocads-command-line-using-net-part-2.html" target="_blank">the command-line helper sample posted last week</a> by allowing our global keywords window to “dock” to the four corners of the drawing window as well as to remain fixed at a custom location somewhere on the screen. I use the term “dock” here loosely, as we’re really just placing it in one of the corners of the drawing window. If we wanted a modeless dialog that was properly docked into AutoCAD then we’d almost certainly want to use a PaletteSet.</p>  <p>Here’s a quick video demonstrating the KWSDOCK command, which allows the user to select one of the four corners or a custom location:</p>  <br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb08081829970d-pi"><img title="CmdLineHelper with docking" style="float: none; margin-left: auto; display: block; margin-right: auto" alt="CmdLineHelper with docking" src="/assets/image_450860.jpg" width="400" height="211" /></a>  <br />  <p>While the changes aren’t very extensive, it doesn’t make sense to embed the complete code in this post. Here’s a link to <a href="http://through-the-interface.typepad.com/files/CmdLineHelper2.zip" target="_blank">the updated C# project</a> for you to look at in depth.</p>  <p>Having flexibility around the location of this kind of dialog is pretty important: it would quickly get annoying if you weren’t able to adjust it (whether because the window was too far from the area of interest or obscuring something you really needed access to).</p>  <p>One suggestion was to have the dialog placed at the cursor. Of course you couldn’t have it follow the cursor – that would clearly make it impossible to use the mouse to select a keyword from the list – but even having it placed where the cursor was at the start of the command feels strange: you very often have the cursor near your area of interest when you start a command. Having the option of a “fixed” custom location is one way to mitigate against this.</p>  <p>In the next part we’re going to take a look at implementing this via a right-click/drag operation directly on the window, to avoid having to run a separate command. I see this happening in much the same way as AutoCAD or Visual Studio previews the docking of windows: they both preview the location when you drag the window close enough. I’m not sure exactly how I’m going to do it, yet, but I’m aiming to do something similar for this keywords window.</p>

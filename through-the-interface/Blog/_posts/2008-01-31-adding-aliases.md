---
layout: "post"
title: "Adding aliases for custom AutoCAD commands"
date: "2008-01-31 08:12:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Commands"
original_url: "https://www.keanw.com/2008/01/adding-aliases.html "
typepad_basename: "adding-aliases"
typepad_status: "Publish"
---

<p>I had an interesting question come in by email and thought I'd share it via this post. To summarise the request, the developer needed to allow command aliasing for their custom commands inside AutoCAD. The good news is that there's a standard mechanism that works for both built-in and custom commands: acad.pgp.</p>

<p>acad.pgp is now found in this location on my system:</p>

<p>C:\Documents and Settings\walmslk\Application Data\Autodesk\AutoCAD 2008\R17.1\enu\Support\acad.pgp</p>

<p>We can edit this text file to add our own command aliases at the end:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">NL,&nbsp; *NETLOAD</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">MCC, *MYCUSTOMCOMMAND</p></div>

<p>Here we've simply created an alias for the standard NETLOAD command (a simple enough change, but one that saves me lots of time when developing .NET modules) and for a custom command called MYCUSTOMCOMMAND. If I type NL and then MCC at the AutoCAD command-line, after saving the file and re-starting AutoCAD, I see:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">NL</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">NETLOAD</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">MCC</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Unknown command &quot;MYCUSTOMCOMMAND&quot;.&nbsp; Press F1 for help.</p></div>

<p>Now the fact that MCC displays an error is fine - I don't actually have a module loaded that implements the MYCUSTOMCOMMAND command - but we can see that it found the alias and used it (and the MYCUSTOMCOMMAND name could also be used to <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html">demand-load</a> an application, for instance).</p>

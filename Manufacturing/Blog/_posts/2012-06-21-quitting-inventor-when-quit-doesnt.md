---
layout: "post"
title: "Quitting Inventor when Quit() doesn't"
date: "2012-06-21 12:59:28"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/quitting-inventor-when-quit-doesnt.html "
typepad_basename: "quitting-inventor-when-quit-doesnt"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/augusto-goncalves.html" target="_blank">Augusto Goncalves</a></p>  <p><strong>Application.Quit()</strong> should normally terminate Inventor. If it does not happen, try kill the process using .NET <strong>System.Diagnostics.Process.GetCurrentProcess().Kill()</strong></p>  <p>From MSDN: Note that the Kill method executes asynchronously. After calling the Kill method, call the <strong>WaitForExit</strong> method to wait for the process to exit, or check the <strong>HasExited</strong> property to determine if the process has exited.</p>  <p>Visit <a href="http://msdn.microsoft.com/en-us/library/system.diagnostics.process.kill.aspx">MSDN Process.Kill</a> site for more information.</p>

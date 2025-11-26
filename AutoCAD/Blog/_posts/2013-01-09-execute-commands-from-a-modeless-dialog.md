---
layout: "post"
title: "Execute Commands from a Modeless dialog"
date: "2013-01-09 11:02:31"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/execute-commands-from-a-modeless-dialog.html "
typepad_basename: "execute-commands-from-a-modeless-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>As a modeless dialog in AutoCAD application run on session, we cannot execute commands, so below is a list of alternatives:</p>  <p>1. Use the function <strong>AcApDocManager::sendStringToExecute()</strong> (ensure one escape (ASCII 27) characters down first before the real string, this will cancel any previous running commands, if any).</p>  <p>2. Simulate menu selection by sending WM_COMMAND rather than WM_CHAR messages. Once the command is active you can then, of course, send WM_CHAR messages for the parameters. This would then only call commands that exist in the menu structure (though they could hidden/deeply nested).</p>  <p>3. Use the function <strong>int acedPostCommand(const ACHAR*) </strong>to send characters command-prompt.</p>  <p>4. Use the function <strong>int ads_queueexpr(const ACHAR *)</strong> to again send characters to the command prompt.</p>

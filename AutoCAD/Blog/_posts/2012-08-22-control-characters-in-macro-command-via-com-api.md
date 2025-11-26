---
layout: "post"
title: "Control characters in macro command via COM API"
date: "2012-08-22 02:10:39"
author: "Philippe Leefsma"
categories:
  - "ActiveX"
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/control-characters-in-macro-command-via-com-api.html "
typepad_basename: "control-characters-in-macro-command-via-com-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>I added a toolbar button with <em>IAcadToolbar::AddToolbarButton()</em> with the following macro command <em>&quot;^C^C^P_LINE\n^P&quot;.</em> When I press that button, AutoCAD respond the following:</p>  <p><em>Command: ^C^C^P_TI     <br />Unknown command &quot;^C^C^P_TI&quot;. Press F1 for help.</em></p>  <p>The same command written in a menu file will work ok. What is wrong there?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>The reason AutoCAD says the command is not valid is because you need to format your string with the proper control characters instead of using the menu syntax. For example, the menu file ^C should be replaced by character 0x03 and ^P should be replaced by character 0x10. The rules for this is to subtract 0x40 to any character controls preceded by ^. Note that you should replace the pair ^ + P by just character 0x10 and so on.</p>  <p>I.e.:   <br />&#160;&#160;&#160; Character P is ASCII 0x50    <br />&#160;&#160;&#160; 0x50 - 0x40 = 0x10    <br />&#160;&#160;&#160; So replace ^P by character 0x10</p>  <p>NB: If you are using C++ and ObjectARX API, you can then use the <em>CAcUiString::ConvertMenuExecString()</em> method which will do the work for you.</p>

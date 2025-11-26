---
layout: "post"
title: "Getting started with AccoreConsole"
date: "2012-04-04 22:45:03"
author: "Balaji"
categories:
  - "2013"
  - "Balaji Ramamoorthy"
  - "DevTV"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/getting-started-with-accoreconsole.html "
typepad_basename: "getting-started-with-accoreconsole"
typepad_status: "Publish"
---

<p>By <a target="_self" href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html">Balaji Ramamoorthy</a></p>
<p>AutoCAD 2013 has a command line version of AutoCAD that can help you with significantly faster batch processing of drawings. Please note that the use of this utility is not officially supported by Autodesk. Here is a quick introduction to get you started with it.</p>
<p>In the AutoCAD 2013 install folder, you can find the "AccoreConsole.exe". Just running it will display the command line switches that can be used with it.</p>
<p>It accepts four command line switches :</p>
<p>1) /i : Used to specify the drawing file path on which to run the script file</p>
<p>2) /s : Used to specify the path to the script file.</p>
<p>3) /l : If language packs are installed, you have the choice to invoke the language version of accoreconsole. The commands in the script file can then be in one of the languages that you have installed in your system.</p>
<p>4) /isolate : Used to prevent the changes to the system variables from affecting regular AutoCAD.</p>
<p>As an example, it is most common to set the "FILEDIA" system variable to 0 in your script files.</p>
<p>But if you do not want this system variable change to take effect when you start regular AutoCAD the next time, then you can use the "isolate" switch. This ensures that changes are kept local to accoreconsole.</p>
<p>Finally, if you need more details regarding AccoreConsole, have a look at the DevTV.</p>

<a href="http://adndevblog.typepad.com/autocad/Downloads/DevTV-AccoreConsole.zip">AccoreConsole DevTV</a> 

<p>Happy batch processing !</p>

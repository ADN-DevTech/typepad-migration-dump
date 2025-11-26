---
layout: "post"
title: "Running OEMMAKEWIZARD from Command Line"
date: "2018-05-01 21:25:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2018/05/running-oemmakewizard-from-command-line.html "
typepad_basename: "running-oemmakewizard-from-command-line"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p>


<p>This is small batch script may be useful for OEM Developers.</p>
Once you have created your project, you can run the AutoCAD OEM Make Wizard from the command line to automate building your product. 
In this mode, you can use any of the build options available on the Build Your Product page of the AutoCAD OEM Make Wizard. Enter the following command to display a Help screen describing the command-line options for the AutoCAD OEM Make Wizard:
<pre>oemmakewizard /? </pre>
<p> <strong>Build Options:</strong> </p>
<pre>Path Options  
   /PA:<path location="" oem="" autocad="" for="">  Specifies the path for AutoCAD OEM location  
   /PB:<additional for="" locations="" bitmap="" paths="">  Specifies the path for bitmap location  
<strong>Import Options</strong>  
   /IC:<file autocad="" for="" commands="" enabling\disabling="">  Specifies a file for enabling\disabling AutoCAD commands  
   /IAS:<file for="" commands="" settings="" and="" your="" adding="">  Specifies a file for adding your commands  
   /IRS:<file for="" commands="" your="" removing="">  Specifies a file that allows you to remove user specified commands or setvars  
<strong>Build Options</strong>  
   /BALL  Builds all (does not include the files executed by /BT command)  
   /BA  Binds ObjectARX/Core ObjectARX applications  
   /BB  Binds managed applications  
   /BBA  Binds managed ObjectARX applications  
   /BL  Binds AutoLISP applications  
   /BD  Binds DLL modules  
   /BV  Bind DVB macros  
   /BC  Copies files  
   /BT  Builds type libraries and registry files  
   /BT+  Registers associated registry files  
   /BI  Change icons  
   /BR  Rebuilds resources  
   /BR+  Rebuilds resources and uses settings with AutoCAD OEM  
   /BR-  Rebuilds resources, but does not bind the DLLs  
   /STOP  Interrupts batch mode when errors are encountered  
   /?  Displays the Help screen for all command options
</file></file></file></additional></path></pre>

<script src="https://gist.github.com/MadhukarMoogala/610c8fd96d622c2c935f9ee5bcf958ba.js"></script>

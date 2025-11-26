---
layout: "post"
title: "AcadStartup and AutoCAD OEM"
date: "2013-02-01 11:02:40"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/acadstartup-and-autocad-oem.html "
typepad_basename: "acadstartup-and-autocad-oem"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>I am having problems launching my VBA app in AutoCAD OEM.&#160; The problem is that VBA is not getting automatically loaded.&#160; If I manually load my VBA app, then VBA gets initialized.&#160; Since my product runs as a silent, unattended app, how can I force VBA to be initialized?&#160; </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>First, you will need to name your DVB to &lt;prog4&gt;.dvb, where &lt;prog4&gt; represents the first four letters of your AutoCAD OEM program's name.&#160; If you have a macro named ‘AcadStartup’, this macro will run when the VBA initialization is complete.&#160; However, the VBA initialization is not complete until AcVba.arx is done loading, so AcVba.arx needs to be loaded at startup.&#160; You can set your AcVba.arx module to demand load using the MakeWizard (by just selecting ‘Build Resources’ and NOT ‘Bind ARX’), but probably it is easier for you to use an RX file to load AcVba.arx at startup.</p>  <p>For an RX file, you will have to name it &lt;prog4&gt;.rx (to match the first four letters of your AutoCAD OEM program's name).&#160; This RX file is just a text file with a single line of ‘AcVba.arx’.&#160; Make the text file, place it in your project file location (\OEMInstall\Projects\&lt;prog4&gt;\...), and add it as a module in <em>Your Modules</em>, specifying ‘CopyFile’ for <em>Build With</em> and ‘Exe’ as <em>Build Destination</em>. This will also ensure that the file is created in your installer, if you use the OEMInstallerWizard.</p>

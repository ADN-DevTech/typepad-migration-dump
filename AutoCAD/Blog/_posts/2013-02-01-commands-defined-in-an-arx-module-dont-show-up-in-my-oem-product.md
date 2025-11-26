---
layout: "post"
title: "Commands defined in an ARX module don't show up in my OEM product"
date: "2013-02-01 11:38:30"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/commands-defined-in-an-arx-module-dont-show-up-in-my-oem-product.html "
typepad_basename: "commands-defined-in-an-arx-module-dont-show-up-in-my-oem-product"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>I've defined several commands in an ARX module, but when I load it into my OEM product none of them appear as callable commands. What's going wrong?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>This most likely caused by not specifying the commands on the &quot;Your Commands&quot; tab in the OEM Make Wizard. If the command doesn't appear in the list, OEM will not allow it to be registered, and you will not be able to use it in your product.</p>  <p>To fix this, add the command name to the list on the &quot;Your Commands&quot; tab in the OEM Make Wizard, or make sure the command is spelled properly, then rebuild your product's resources to incorporate the new command list. You should now see the commands. </p>  <p>Be sure to check how the commands should be defined, this is documented in the <strong>OEM Developers Guide</strong> (oemdev.chm) under the section <strong>Your Module Settings Page</strong></p>

---
layout: "post"
title: "Turn off the Automatic Save of dsd files when running the Publish command"
date: "2012-05-25 11:51:40"
author: "Wayne Brill"
categories:
  - "AutoCAD"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/turn-off-the-automatic-save-of-dsd-files-when-running-the-publish-command.html "
typepad_basename: "turn-off-the-automatic-save-of-dsd-files-when-running-the-publish-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>I have developed an application for one of my clients that uses the PUBLISH command.&#160; Every time I publish drawings I get error messages related to DSD files. How can I stop AutoCAD from saving DSD files automatically? </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>There is a setting in Options that will display the dialog that will ask if you want to save the dsd file. To change this do the following: Run the Options command and go to the System Tab. Click the Hidden Messages setting button. There will be a Publish – Save Sheet List&#160; checkbox.&#160; Check it and click Ok all the way out of options.</p>  <p>When you publish you should be prompted to save the current list of sheets. Click “Always perform my current choice” and select No. </p>

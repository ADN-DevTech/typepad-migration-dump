---
layout: "post"
title: "Determining Version of Roamer.exe used to create NWD from NWF"
date: "2013-02-14 16:31:16"
author: "Saikat Bhattacharya"
categories:
  - "COM"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2013/02/determining-version-of-roamerexe-used-to-create-nwd-from-nwf.html "
typepad_basename: "determining-version-of-roamerexe-used-to-create-nwd-from-nwf"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html">Saikat Bhattacharya</a></p>  <p>In the Auto_08.vbs sample in Navisworks Manage 2012 (or the AutoPublishScriptExample sample in Navisworks 2013), which can be located from the Navisworks install folder location (typically under Navisworks 2012\api\COM\examples location) - the VB Script file takes an input file and creates an expiring output file using the Publish API. </p>  <p>If a system has multiple versions of Navisworks installed and we want to determine which version of roamer.exe was used to do the publish, we can add the following simple lines of code and extract the required information:</p>  <p>ver=roamer.state.fileversion(arg_out)   <br />MsgBox(ver)</p>  <p>All that is being done here is to check the file version of the output file and determine which flavor and version of NW was used to generate the NWD file. Please do remember to declare the variable ver using the Dim statement, at the beginning of the sample.</p>

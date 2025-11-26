---
layout: "post"
title: "Does AutoCAD OEM Support PGP files"
date: "2012-07-09 16:29:45"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/does-autocad-oem-support-pgp-files.html "
typepad_basename: "does-autocad-oem-support-pgp-files"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>Does AutoCAD OEM support shortcut key aliases defined in a PGP file?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The PGP command alias definition file is supported by AutoCAD OEM. </p>  <p>You just have to make sure that the PGP file is the same name as the generated OEM master exe. For example a PGP file used with the OEM tutorial would be named poly.pgp and located in the support directory. It would need to have aliases defined that run commands that have been enabled. </p>  <p>   <br />If you try to run commands defined by shortcuts defined in the PGP, the alias is used to run the command, however if the command has not been enabled for the OEM product, the &quot;Unknown command&quot; message will be displayed on the command line.</p>

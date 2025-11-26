---
layout: "post"
title: "RealDWG support for Shape Files (SHP/SHX)"
date: "2012-07-20 16:10:16"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Fenton Webb"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/realdwg-support-for-shape-files-shpshx.html "
typepad_basename: "realdwg-support-for-shape-files-shpshx"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>Just to let all know, RealDWG does support Shape Files. </p>  <p>By default, the RealDWG SDK comes with a Fonts folder that contains a standard set of compiled SHX files which you can deploy with your RealDWG app.</p>  <p>Just make sure that your HostApplicationServices::FindFile() knows to search where you have your SHX files deployed. That way, when the DWG file is read the SHX files will be resolved.</p>

---
layout: "post"
title: "Deploying RealDWG application using WiX"
date: "2014-02-05 02:12:05"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "Balaji Ramamoorthy"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2014/02/deploying-realdwg-application-using-wix.html "
typepad_basename: "deploying-realdwg-application-using-wix"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
Here is a sample project from <a href="http://download.autodesk.com/media/adn/DevTV-Introduction-to-RealDWG-Programming/">RealDWG DevTV</a> that has been modified to use <a href="http://wixtoolset.org/">WiX</a> to deploy the application.
<p>To try this, please download and install the WiX Toolset.</p>
<p>There are two batch files included in the sample project folder. These batch files run as "Pre-build event" when the project is built. Please ensure that the path of the VC++ re-distributables, Fonts and other RealDWG merge modules are correctly specified in the batch files.</p>
<p>The purpose of the batch files is to copy all the files needed for the packaging to a common folder named as "ForPackaging". The WiX setup project builds the MSI by using this folder path without having to look for files in different paths.</p>

</p>

<span class="asset  asset-generic at-xid-6a0167607c2431970b01a73d6feee7970d img-responsive"><a href="http://adndevblog.typepad.com/files/myrealdwg_sample.zip">Download MyRealDWG_Sample</a></span>

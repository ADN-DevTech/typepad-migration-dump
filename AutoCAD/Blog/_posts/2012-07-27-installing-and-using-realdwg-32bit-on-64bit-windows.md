---
layout: "post"
title: "Installing and using RealDWG 32bit on 64bit Windows"
date: "2012-07-27 09:13:26"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "Fenton Webb"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/installing-and-using-realdwg-32bit-on-64bit-windows.html "
typepad_basename: "installing-and-using-realdwg-32bit-on-64bit-windows"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are developing a RealDWG application for both 32bit and 64bit Windows you have probably wondered if 32bit RealDWG is supported on Windows 64bit… </p>  <p>Let me answer some of your questions.</p>  <ol>   <li>Can you install the RealDWG 32bit SDK on 64bit Windows     <br />Yes you can, installing the RealDWG 32bit SDK is supported on 64bit Windows.&#160; <br /></li>    <li>Can I create a RealDWG 32bit application and install it on 64bit Windows.     <br />Yes you can, but as with all Autodesk products, where we have a 64bit version of the software we don’t support installing the 32bit version on Windows 64bit. The reason is because, although 32bit versions of our software are capable of running on 64bit Windows, due to the huge amount of work needed to test and release a product on a Windows platform, given the total number of user requests for 32bit Autodesk products on 64bit Windows, it doesn’t make business sense to support it.      <br /></li>    <li>Does RealDWG even need to be installed to use it?      <br />Actually, as long as you don’t use the RealDWG COM API, then you can use it just like you would the ObjectARX SDK – all I do is copy the RealDWG SDK files from machine to machine, I never install it.      <br /></li>    <li>How do I add the RealDWG components to my installer so that they get installed automatically with my runtime?     <br />It’s really easy; all you have to do is add all of the MSI Merge Modules (MSM) files to your installer MSI. Once you have done that, save the MSI. Next, go to the Property table of the MSI and edit the newly created ODBXHOSTAPPREGROOT property – make sure the value of that property reflects the same value that your RealDWG HostApplicationServices.GetRegistryProductRootKey() returns. Check out the Redistribution Requirements section of the readdbx.chm file found in the RealDWG SDK for more information.</li> </ol>

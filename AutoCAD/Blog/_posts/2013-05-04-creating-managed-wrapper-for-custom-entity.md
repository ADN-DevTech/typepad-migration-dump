---
layout: "post"
title: "Creating managed wrapper for custom entity"
date: "2013-05-04 02:46:26"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/05/creating-managed-wrapper-for-custom-entity.html "
typepad_basename: "creating-managed-wrapper-for-custom-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>ObjectARX 2009 SDK had a nice sample project called "SimpleSquare" that demonstrated the managed wrapper creation that allows access to the custom entity from a managed code. I have migrated this sample project to work with AutoCAD 2013 and Visual Studio 2010.</p>
<p>The changes are mainly to conform to the new CLR syntax as the original sample used the old CLR syntax. &nbsp;</p>
<p>Here is the&nbsp;sample project for download.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01901bd233a2970b"><a href="http://adndevblog.typepad.com/files/simplesquare.zip">Download SimpleSquare</a></span></p>
<p>To try this, place the "SimpleSquare" folder under "&lt;ObjectARX 2013 SDK folder&gt;\samples\entity"&nbsp;and follow the steps explained in the ReadMe.txt</p>

---
layout: "post"
title: "Setting the default Pipe Spec and Size in the Plant3d Ribbon for the PLANTPIPEADD command using C#.NET"
date: "2012-05-23 16:06:29"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/setting-the-default-pipe-spec-and-size-in-the-plant3d-ribbon-for-the-plantpipeadd-command-using-cnet.html "
typepad_basename: "setting-the-default-pipe-spec-and-size-in-the-plant3d-ribbon-for-the-plantpipeadd-command-using-cnet"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>In my previous post <a href="http://adndevblog.typepad.com/autocad/2012/05/how-to-obtain-a-list-of-all-pipe-specs-and-sizes-in-plant3d-using-net-c.html">How to obtain a list of all Pipe Specs and Sizes in Plant3d using .NET C#</a> I spoke about the Autodesk.ProcessPower.P3dUI.UISettings object.</p>
<p>If you want to control the default settings for the PLANTPIPEADD command as shown here:</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766bab3d1970b-pi"><img alt="clip_image002" border="0" height="250" src="/assets/image_722815.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="clip_image002" width="491" /></a></p>
<p>Simply use the UISettings.CurrentSpec and UISettings.CurrentSize, easy <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_724087.jpg" style="border-style: none;" /></p>

---
layout: "post"
title: "Fusion API: Default Paths to Load Add-In"
date: "2017-01-04 19:09:07"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/01/fusion-api-default-paths-to-load-add-in.html "
typepad_basename: "fusion-api-default-paths-to-load-add-in"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Question</strong>:</p>
<p>Will Fusion 360 load an add-in from the path (%AppData%) only? Is there any other paths I can configure?</p>
<p><strong>Solution</strong>:</p>
<p>A couple of other paths are available for Fusion to look for the add-in. This is missed in the API help.</p>
<p>%programfiles%\Autodesk\ApplicationPlugins<br />%programdata%\Autodesk\ApplicationPlugins<br />%appdata%\Roaming\Autodesk\ApplicationPlugins<br />%appdata%\Roaming\Autodesk\Autodesk Fusion 360\API\AddIns</p>

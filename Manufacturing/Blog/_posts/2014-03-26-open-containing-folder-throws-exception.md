---
layout: "post"
title: "[Open Containing Folder] throws exception"
date: "2014-03-26 18:06:31"
author: "Xiaodong Liang"
categories:
  - "iLogic"
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/03/open-containing-folder-throws-exception.html "
typepad_basename: "open-containing-folder-throws-exception"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><strong>Question</strong>    <br />When I wanted to create the global form, clicked [Open Containing Folder] , an exception occurred. This happened with any document and all 5 of our stations.&#160; <br /><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5118f8cc0970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="clip_image002[5]" border="0" alt="clip_image002[5]" src="/assets/image_4e5a70.jpg" width="244" height="151" /></a></p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcdfe411970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="clip_image002" border="0" alt="clip_image002" src="/assets/image_9010c2.jpg" width="290" height="210" /></a></p>  <p>   <br /><strong>Solution</strong></p>  <p>The global forms are stored in a subdirectory named iLogic\UI under the Design Data folder. e.g. for Inventor 2014, the default location is   <br /><em>C:\Users\Public\Documents\Autodesk\Inventor 2014\Design Data\iLogic\UI</em></p>  <p>The Design Data folder location can also be changed from the default within a particular project.</p>  <p>As a manual workaround, the user can open their Design Data folder in Windows Explorer and navigate to the subdirectory.</p>  <p>So, the user will need to find where the Design Data folder is? One possiblity is might be on an Office 365 drive.</p>

---
layout: "post"
title: "Rename Shared Parameter and Aerial Paradise"
date: "2014-10-17 10:54:37"
author: "Jaime Rosales"
categories:
  - ".NET"
  - "Jaime Rosales"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/10/rename-shared-parameter-and-aerial-paradise.html "
typepad_basename: "rename-shared-parameter-and-aerial-paradise"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/jaime-rosales.html">Jaime Rosales</a></p>
<p>This past weekend I had the chance to attend one of my best friend’s wedding in the Gorgeous Island of Puerto Rico. A place with so much culture amazing food and of course breath taking paradise. I had the chance to take a couple of Aerial Pictures from a Catamaran which was such an amazing experience. Let me share you one of my favorite pictures, you could use it as background, trust me, I have it as mine and it looks amazing on a 29 inch LED.</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07f5b08970c-pi"><img alt="FullSizeRender" border="0" height="360" src="/assets/image_549293.jpg" style="display: inline; border-width: 0px;" title="FullSizeRender" width="480" /></a></p>
<p>&#0160;</p>
<p>Back to Revit API, One case that caught my eye this time came from our&#0160; Community Forum.</p>
<p><a href="http://forums.autodesk.com/t5/revit-api/rename-shared-parameter/m-p/5337125#M7540" title="http://forums.autodesk.com/t5/revit-api/rename-shared-parameter/m-p/5337125#M7540">http://forums.autodesk.com/t5/revit-api/rename-shared-parameter/m-p/5337125#M7540</a></p>
<p><strong>Question:</strong> Is there a way to rename shared parameter already assigned to the project and/or family files? I would like to keep the GUID as it is but change the alias.</p>
<p><strong>Answer:</strong> Unfortunately renaming an existing shared parameter is not possible. This comes from the SDK documentation when the <strong>RenameParameter</strong> method is used : &quot; <em>This operation is valid only for Family Parameters, and is invalid for Shared Parameters and Built-in Parameters.&quot;</em></p>
<p>Some workaround that I could suggest is try to export all the values of&#0160; your existing shared parameter into a temporary storage mapping each element id to its shared parameter value, delete the old shared parameter in the project file, create a new shared parameter with the updated name, and repopulate the values again in that way you will keep the GUID that you need with your new renamed Shared Parameter. it might work.</p>
<p>Or another work around that another contributor (PhillipM) of the forum suggested is: <em>Change the SharedParameter to a family Parameter, Rename it and then convert it back to a SharedParameter. GUIDS etc do not change.</em></p>
<p>Thanks for reading and until next time.</p>

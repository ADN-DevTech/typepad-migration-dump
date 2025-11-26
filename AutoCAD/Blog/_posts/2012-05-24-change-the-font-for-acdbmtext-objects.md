---
layout: "post"
title: "Change the font for AcDbMText objects"
date: "2012-05-24 21:25:46"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/change-the-font-for-acdbmtext-objects.html "
typepad_basename: "change-the-font-for-acdbmtext-objects"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Changing the font in the TextStyleTableRecord which this object uses, changes the font of all the AcDbMText objects that use this text style in the drawing. To change the font of a particular AcDbMText object, use the MText format codes.</p>
<div>
<p>You can apply formatting to individual words or characters, such as underlining text, add a line over text, or create stacked text. You also can change the color, font, and text height, and the spaces between text characters or increase the width of the characters themselves.</p>
<p>The MText format codes are listed in the AutoCAD online help under "Format Multiline Text in an Alternate Text Editor". To change the font, use the \F format code (For example : "Autodesk \Ftimes; AutoCAD"). Alternatively, you can use the AcDbMText::fontChange() method to insert the Font Change formatting character.</p>
<p>To try out the various formatting characters before using it in your code, use the MTEXTED command in AutoCAD to set an alternate text editor such as notepad.exe. You can then experiment with the format codes to choose the right one that suits.</p>
</div>

---
layout: "post"
title: "Working with non-English version of AutoCAD"
date: "2012-07-07 20:29:51"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/working-with-non-english-version-of-autocad.html "
typepad_basename: "working-with-non-english-version-of-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>As an example, "acedSSGet(L":S", NULL, NULL, NULL, set)" in a non-English version of AutoCAD returns an error.</p>
<p>Here is an extract from the AutoLISP Developer's guide under the "Foreign Language Support" heading:</p>
<p>&lt;&lt;&lt;<br />If you develop AutoLISP programs that can be used with a foreign-language version of AutoCAD, the standard AutoCAD commands and key words are automatically translated if you precede each command or key word with an underscore (_).<br />&nbsp;<br />(command "_line" pt1 pt2 pt3 "_c")<br />&nbsp;<br />If you are using the dot prefix (to avoid using redefined commands), you can place the dot and underscore in either order. Both "._line" and "_.line" are valid."<br />&nbsp;<br />So, to ensure compatibility with all localized versions of AutoCAD, you should<br />use "acedSSGet(L"_:S", ..." instead of "acedSSGet(L":S", ..."<br />&gt;&gt;&gt;</p>
<p>The same principle applies for the other acedSSGet keywords.<br />&nbsp;<br />You can test your programs on an English version of AutoCAD for globalization problems by setting the undocumented GLOBCHECK system variable to 1.</p>
<p>After setting this system variable to 1, if you try running this :</p>
<p>(command "line" '(0 0 0) '(10 0 0) '(10 10 0) "_c")</p>
<p>AutoCAD recognises that this might cause problems in non-English version of AutoCAD.<br />Here is a screen capture of the output messsages.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01774322018f970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01774322018f970d image-full" title="1" src="/assets/image_467092.jpg" border="0" alt="1" /></a></p>

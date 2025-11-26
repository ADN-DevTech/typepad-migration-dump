---
layout: "post"
title: "AutoCAD OEM Error Message &ldquo;This object contains too many unknown fonts and cannot be accurately represented within the editor&rdquo;"
date: "2012-12-11 16:26:39"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/autocad-oem-error-message-this-object-contains-too-many-unknown-fonts-and-cannot-be-accurately-represented-within-the-editor.html "
typepad_basename: "autocad-oem-error-message-this-object-contains-too-many-unknown-fonts-and-cannot-be-accurately-represented-within-the-editor"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>When I call either ._MTEXT for new text or ._DDEDIT on existing text I get the   <br />error message:</p>  <p>&quot;This object contains too many unknown fonts and cannot be accurately   <br />represented within the editor.&quot;</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>You need to copy the file <strong>mtextmap.ini</strong> into the same directory as <strong>acmted.arx</strong> in-order for it to work properly.</p>

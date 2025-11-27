---
layout: "post"
title: "Renew thumbnail of document"
date: "2013-05-20 21:15:28"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/05/renew-thumbnail-of-document.html "
typepad_basename: "renew-thumbnail-of-document"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>When a document is saved, Inventor will follow [File Save Options] to save the thumbnail. in default, it is always ISO component&#160; view.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191025c9575970c-pi"><img style="border-bottom: 0px; border-left: 0px; display: inline; border-top: 0px; border-right: 0px" title="image" border="0" alt="image" src="/assets/image_6082d3.jpg" width="376" height="294" /></a> </p>  <p>API also provides the relevant method:</p>  <p><b>Document.SetThumbnailSaveOption(      <br />&#160; <b><i>SaveOption</i></b> As <a href="/HTML/ThumbnailSaveOptionEnum.htm">ThumbnailSaveOptionEnum</a>,&#160;&#160; [<b><i>ImageFullFileName</i></b>] As String )</b></p>  <p>- kActiveComponentIsoViewOnSave   <br />&#160; The isometric view of the document is set as thumbnail on save.</p>  <p>- kActiveWindow   <br />&#160; The currently active view is immediately set as thumbnail.</p>  <p>- kActiveWindowOnSave   <br />&#160; The active view of the document is set as thumbnail on save.</p>  <p>- kImportFromFile   <br />&#160; The specified image file is set as thumbnail.</p>  <p>- kNoThumbnail   <br />&#160; No thumbnail is set.</p>  <p>So to renew the thumbnail, you could change the camera to the view you desired and call Document.Save. You can even use external image as thumbnail, using SaveOption = kImportFromFile.</p>  <p><em>Sub test()     <br />&#160;&#160;&#160; Dim oDoc As PartDocument      <br />&#160;&#160;&#160; Set oDoc = ThisApplication.activeDocument      <br />&#160;&#160;&#160; ' change the current view      <br />&#160;&#160;&#160; Dim view As view      <br />&#160;&#160;&#160; Set view = ThisApplication.ActiveView      <br />&#160;&#160;&#160; Dim camera As camera      <br />&#160;&#160;&#160; Set camera = view.camera      <br />&#160;&#160;&#160; camera.ViewOrientationType = kRightViewOrientation      <br />&#160;&#160;&#160; camera.Fit      <br />&#160;&#160;&#160; camera.Apply      <br />&#160;&#160;&#160; ' set save option      <br />&#160;&#160;&#160; oDoc.SetThumbnailSaveOption kActiveWindowOnSave      <br />&#160;&#160;&#160; 'call saving      <br />&#160;&#160;&#160; oDoc.Save      <br />End Sub</em></p>

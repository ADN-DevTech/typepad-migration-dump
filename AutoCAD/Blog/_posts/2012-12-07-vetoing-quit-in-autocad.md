---
layout: "post"
title: "Vetoing QUIT in AutoCAD"
date: "2012-12-07 06:12:45"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/vetoing-quit-in-autocad.html "
typepad_basename: "vetoing-quit-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><strong>Q:</strong></p>  <p>How can I veto quit in AutoCAD?</p>  <p><strong>A:</strong></p>  <p>There are two situations to consider:</p>  <p>1) Subclass the AutoCAD main frame and watch for WM_CLOSE message. This message is generated when user tries to quit AutoCAD using any of the possible methods. In the sample, we veto the quitting of AutoCAD, but this can be modified to make changes in the documents, save them and then allow AutoCAD to quit. This way you can avoid the save dialog while quitting AutoCAD.</p>  <p>2) Using the event AcApDocManagerReactor::documentLockModeChanged() to trap the EXIT command that the user can issue at command prompt.</p>  <p>The attached arx sample implements both approaches.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:120c686d-3f53-4850-af33-bed3d111aadb" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/arxvetoquit.zip" target="_blank">ArxVetoQuit.zip</a></p></div>

---
layout: "post"
title: "JOIN Command and AcDbJoinEntityPE"
date: "2013-03-27 02:06:01"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/join-command-and-acdbjoinentitype.html "
typepad_basename: "join-command-and-acdbjoinentitype"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>Q: </p>  <p>Is is possible to implement AcDbJoinEntityPE extension protocol in my custom entities, so I can handle custom behavior when JOIN command is used on them?</p>  <p>A:</p>  <p>Unfortunately at the moment the JOIN command will not invoke the AcDbJoinEntityPE protocol on the involved entities. We have a change request logged against the behavior of the JOIN command, but it has not been addressed yet, sorry for the bad news.</p>  <p>Looking at the source code of the JOIN command, it treats each entity type in a different way and performs a static check on the entity type, hence your custom entity won’t be able to be processed by the JOIN command unless you replace it by your own redefined command.</p>  <p>I gave a quick try at a custom entity using my custom AcDbJoinEntityPE and it worked fine on the API side: calling the “AcDbJoinEntityPE::joinEntity” method using the PE returned by my entity is actually calling the custom “joinEntity” method of my defined PE. </p>  <p>The test sample is attached below.</p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:57577bda-c812-4a7f-a255-3f64cbef8590" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/arxjoinentitype.zip" target="_blank">ArxJoinEntityPE.zip</a></p></div>

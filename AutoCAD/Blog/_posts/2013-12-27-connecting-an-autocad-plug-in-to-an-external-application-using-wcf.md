---
layout: "post"
title: "Connecting an AutoCAD plug-in to an external application using WCF"
date: "2013-12-27 06:36:18"
author: "Philippe Leefsma"
categories:
  - ".NET"
  - "AutoCAD"
  - "Cloud"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2013/12/connecting-an-autocad-plug-in-to-an-external-application-using-wcf.html "
typepad_basename: "connecting-an-autocad-plug-in-to-an-external-application-using-wcf"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>We have many references on how to connect an AutoCAD plug-in to an external executable using COM, but as it becomes an antique technology as years are passing I though it would be useful to illustrate how to achieve the same (and potentially much more) using WCF.</p>  <p>The idea is to have a WCF server that is hosted by an AutoCAD .Net plug-in. One or several client executables running on the same machine will be able to connect to that server and exchange data. Eventually the server can also dispatch data send by one client to the other.</p>  <p>To make the thing more fun I created a little chat application where clients can send messages between each other. You will find the complete code attached with the download at the bottom of that blog post. For more details about the implementation, take a look at the initial post I created on our Cloud &amp; Mobile blog: </p>  <p><a href="http://adndevblog.typepad.com/cloud_and_mobile/2013/12/inter-process-communication-using-wcf.html">Inter-process communication using WCF</a></p>  <p>The AcadServer plug-in is net-loaded in AutoCAD and can start processing requests from the clients:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b03c02da4970d-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="acad" border="0" alt="acad" src="/assets/image_107997.jpg" width="497" height="433" /></a></p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:109eb09d-d474-4a7b-b5c3-1b92fb9b0b8f" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/adn-pipe-demo-2.zip" target="_blank">ADN Pipe Demo.zip</a></p></div>

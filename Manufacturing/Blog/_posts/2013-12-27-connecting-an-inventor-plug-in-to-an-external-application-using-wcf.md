---
layout: "post"
title: "Connecting an Inventor plug-in to an external application using WCF"
date: "2013-12-27 06:41:21"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/12/connecting-an-inventor-plug-in-to-an-external-application-using-wcf.html "
typepad_basename: "connecting-an-inventor-plug-in-to-an-external-application-using-wcf"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>We have many references on how to connect an Inventor plug-in to an external executable using COM, but as it becomes an antique technology as years are passing I though it would be useful to illustrate how to achieve the same (and potentially much more) using WCF.</p>  <p>The idea is to have a WCF server that is hosted by an Inventor .Net plug-in. One or several client executables running on the same machine will be able to connect to that server and exchange data. Eventually the server can also dispatch data send by one client to the other.</p>  <p>To make the thing more fun I created a little chat application where clients can send messages between each other. You will find the complete code attached with the download at the bottom of that blog post. For more details about the implementation, take a look at the initial post I created on our Cloud &amp; Mobile blog: </p>  <p><a href="http://adndevblog.typepad.com/cloud_and_mobile/2013/12/inter-process-communication-using-wcf.html">Inter-process communication using WCF</a></p>  <p>The InventorServer plug-in is loaded in Inventor and can start processing requests from the clients:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fb33b017970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="inventor" border="0" alt="inventor" src="/assets/image_acb9b5.jpg" width="485" height="496" /></a></p>  <div style="padding-bottom: 0px; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; float: none; padding-top: 0px" id="scid:fb3a1972-4489-4e52-abe7-25a00bb07fdf:0eb128b8-50ce-4b50-89d8-96e9f664369d" class="wlWriterEditableSmartContent"><p> <a href="http://adndevblog.typepad.com/adn-pipe-demo-4.zip" target="_blank">ADN Pipe Demo.zip</a></p></div>

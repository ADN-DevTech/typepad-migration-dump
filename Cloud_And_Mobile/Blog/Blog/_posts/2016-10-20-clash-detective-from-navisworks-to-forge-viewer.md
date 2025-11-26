---
layout: "post"
title: "Clash Detective from Navisworks to Forge Viewer"
date: "2016-10-20 07:40:57"
author: "Xiaodong Liang"
categories:
  - "Javascript"
  - "Web Development"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/clash-detective-from-navisworks-to-forge-viewer.html "
typepad_basename: "clash-detective-from-navisworks-to-forge-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang </a></p>
<p>&#0160;<img alt="" src="/assets/Viewer-2.7.*-green.svg" /></p>
<p>Clash Detective is a typical workflow in BIM. Forge platform has not exposed such web service. While we can still combine Navisworks with the web application to coordinate the clash. With this idea, I wrote a small demo sample.</p>
<p><a href="https://github.com/xiaodongliang/Forge-Navisworks-ClashTest">https://github.com/xiaodongliang/Forge-Navisworks-ClashTest</a>&#0160;</p>
<p>By Clash Test .NET API of Navisworks, almost all information of clash test and clash result are achievable. The only challenge is: current .NET API does not provide the suitable viewpoint of the clash result like UI does. Although COM API provides ( InwOclTestResult.GetSuitableViewPoint), I do not want to iterate the clash results twice by .NET and COM separately. So the sample gets the ClashResult.Center and ClashResult.ViewBounds. The former is the center of the clash. The latter is bounding box including the area clashing and some surrounding context. Then the target of camera would be the center, and we can pick a suitable point on the box as the position of camera.&#0160;&#0160;</p>
<p>&#0160;The source code of the sample also encloses the way of COM, just for reference.&#0160;The Navisworks plugin also defines the structure of the clash data, and sends them to the web application.</p>
<p>With Forge Viewer, we can setup a management web application with viewable model, and ask the collaborators to coordinate the model easily. The web application provides the services for receiving the clash data and downloading the data. On the client side, a panel will display the clash test and result. The user can select the result. The corresponding viewpoint is switched and the clashed objects will be isolated.</p>
<p>In reality, it would be more useful to notify a new clash test is ready, and compare two clashes. I hope I could find time to improve it in the future.</p>
<p>Enjoy it :)&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8a61dee970b-pi" style="float: left;"><img alt="Clash" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8a61dee970b img-responsive" src="/assets/image_bc3a85.jpg" style="margin: 0px 5px 5px 0px;" title="Clash" /></a></p>
<p><iframe allowfullscreen="" frameborder="0" height="590" src="https://screencast.autodesk.com/Embed/Timeline/7ea6b007-7af3-4d36-b146-3cf18aaf880c" webkitallowfullscreen="" width="640"></iframe></p>

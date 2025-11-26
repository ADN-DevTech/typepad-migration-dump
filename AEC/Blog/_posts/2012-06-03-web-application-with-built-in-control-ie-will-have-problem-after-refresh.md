---
layout: "post"
title: "web application with built-in control, IE will have problem after refresh"
date: "2012-06-03 23:49:45"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/web-application-with-built-in-control-ie-will-have-problem-after-refresh.html "
typepad_basename: "web-application-with-built-in-control-ie-will-have-problem-after-refresh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>Note: This applies to Navisworks Manage 2011 and Navisworks Simulate 2011. </p>  <p><b>Issue</b></p>  <p>I have one HTML,using built-in control. Open the HTML. The model is loaded correctly. </p>  <p>problem 1:&#160; F5 to refresh. The model is not load.&#160; <br />problem 2:&#160; Switch to other programs. Wait for some time (more than 2 minutes). An error occurs: <em>IE has problem and is trying to close</em>. Switch to IE again. The webpage shows: <em>Internet Explorer has closed this webpage to help protect your computer</em>. </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>These are the known issues</p>  <p>SP1 of Navisworks 2011 fixed:</p>  <p>1) problem 1 and problem 2 on 32bits machine.   <br />2) problem 2 is solved on 64, but problem 1 on 64bits is not</p>  <p>Integrated control of Navisworks solved all issues</p>  <p>SP1 of Navisworks 2011 is available at:</p>  <p><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=15689177&amp;linkID=10382102">http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=15689177&amp;linkID=10382102</a></p>

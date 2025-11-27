---
layout: "post"
title: "Fusion 360 API: Work with Custom Palette"
date: "2017-09-01 03:41:02"
author: "Xiaodong Liang"
categories:
  - "Fusion 360"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2017/09/fusion-360-api-work-with-custom-palette.html "
typepad_basename: "fusion-360-api-work-with-custom-palette"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>I played with custom palette these days. I am sharing some practices.&nbsp;</p>
<p>Firstly, the <a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-6C0C8148-98D0-4DBC-A4EC-D8E03A8A3B5B">API help sample</a> is a very good tutorial for us to get started with. Basically, the skeleton of palette is HTML, auxiliary js , css (except the js libraries of Fusion 360), the corresponding Fusion code (Python/C++) that consumes the HTML.&nbsp;</p>
<p>When I was playing, I happened to hit an issue. The HTML elements were not shown up. It was only a white page. After checking with engineer team, it is addressed as a problem: current API does not encode&nbsp;the path of the local html which contains invalid character(such as space). This fix will be available soon. If you are testing right now, please put the dataset on a path without invalid&nbsp;character.</p>
<p>As said above, the skeleton of palette can include auxiliary js , css, which means we could apply with some nicer UI such as Bootstrap and the frameworks such as Jquery. The usage is simply like what you do in other web application.&nbsp;</p>
<p>After the HTML is ready, I'd strongly suggest you open it to test it. It will be more easier to find out some problems. e.g. check the basic behavior of buttons, dropdown, checkbox, the workflows. The only that cannot test are the calls of Fusion 360., however you could at least check the parameters that are for communications with Fusion. In my test, I wanted to make a custom Export dialog. I have not made it work with [Choose Path] . The chooser does not show up. &nbsp;In addition, I am working to add the workflow &nbsp;[save to cloud].&nbsp;</p>
<p>&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09bef809970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb09bef809970d image-full img-responsive" alt="Screen Shot 2017-09-01 at 5.56.20 PM" title="Screen Shot 2017-09-01 at 5.56.20 PM" src="/assets/image_fe31b6.jpg" border="0" /></a></p>
<p>When the HTML and its workflow are ready, load the add-in to Fusion. If you do not want to debug, you could still view the HTML source or inspect element exactly like we program in web page. The browser engine Fusion uses is Chrome on Windows OS, Safari on Mac.&nbsp;</p>
<p><a class="asset-img-link" style="display: inline;" href="http://a1.typepad.com/6a016764cbbcf9970b01b8d2a62531970c-pi"><img class="asset  asset-image at-xid-6a016764cbbcf9970b01b8d2a62531970c img-responsive" alt="Screen Shot 2017-09-01 at 6.04.45 PM" title="Screen Shot 2017-09-01 at 6.04.45 PM" src="/assets/image_9dd13e.jpg" /></a></p>
<p>&nbsp; <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2a6255c970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d2a6255c970c image-full img-responsive" alt="Screen Shot 2017-09-01 at 6.08.18 PM" title="Screen Shot 2017-09-01 at 6.08.18 PM" src="/assets/image_e4943b.jpg" border="0" /></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><strong>HTML and Js code</strong></p> 
<script src="https://gist.github.com/xiaodongliang/0c9add5fe243f1e249305175e0294e0a.js"></script>

<p><strong>Fusion Add-In code</strong></p> 
<script src="https://gist.github.com/xiaodongliang/7e724ddc5ac77ace6310957ad27965f1.js"></script>

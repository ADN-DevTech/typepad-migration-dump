---
layout: "post"
title: "Setting UserInteractionDisabled to True can improve performance"
date: "2013-07-30 16:23:55"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/setting-userinteractiondisabled-to-true-can-improve-performance.html "
typepad_basename: "setting-userinteractiondisabled-to-true-can-improve-performance"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>Performance of out-of-process clients making multiple calls to the API will be slower than running in process like an Add-In does. One way to improve performance is to set the UserInterfaceManager.UserInteractionDisabled property to true. </p>  <p>Setting this property to True before a batch of changes can dramatically improve performance of out-of-process clients. </p>  <p><strong>Note:</strong> Be certain to set the UserInteractionDisabled property to false after your process is complete, otherwise the Inventor user interface will remain in a disabled state. </p>

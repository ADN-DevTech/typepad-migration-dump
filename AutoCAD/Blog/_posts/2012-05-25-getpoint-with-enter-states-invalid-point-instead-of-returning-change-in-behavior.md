---
layout: "post"
title: "GetPoint with Enter states &quot;Invalid Point&quot; instead of returning - Change in behavior"
date: "2012-05-25 10:37:28"
author: "Wayne Brill"
categories:
  - "AutoCAD"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/getpoint-with-enter-states-invalid-point-instead-of-returning-change-in-behavior.html "
typepad_basename: "getpoint-with-enter-states-invalid-point-instead-of-returning-change-in-behavior"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p><b>Issue</b></p>  <p>I am using the GetPoint method. (AutoCAD COM interface using late binding). In AutoCAD 2010 and in previous versions hitting enter when prompted for a point would return. In 2011 instead of returning, &quot;Invalid Point&quot; is printed on the command line. Is there a way to allow Enter to return using GetPoint () in AutoCAD 2011 and later versions?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>This change in behavior is considered to be correct. If you need Enter to be accepted with GetPoint make a call to InitializeUserInput(0) of the Utility object before using GetPoint().</p>

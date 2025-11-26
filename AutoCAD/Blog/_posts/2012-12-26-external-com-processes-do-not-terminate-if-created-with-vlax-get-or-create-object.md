---
layout: "post"
title: "External COM processes do not terminate, if created with (vlax-get-or-create-object)"
date: "2012-12-26 17:22:16"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/external-com-processes-do-not-terminate-if-created-with-vlax-get-or-create-object.html "
typepad_basename: "external-com-processes-do-not-terminate-if-created-with-vlax-get-or-create-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>It is recommended that developers call (vlax-release-object...) on all external objects after they are done with them. However, it should be noted that calling (vlax-release-object...) does not always release the object at that exact time. The object is not actually released until all LISP symbols referencing the server's COM objects go out of scope or are set to nil AND a garbage-collection (gc) happens. The actual COM call to &quot;release&quot; happens only in a (gc) and only if LISP agrees that the object is no longer referenced. </p>  <p>You can force a garbage-collection with the lisp function (gc). Before you invoke Excel's Quit method, insert the call to (gc) into your code. This should allow (vlax-release-object ...) to remove Excel.exe from the process list. See below:</p>  <p>&#160;</p>  <p>(defun Test ()   <br />&#160;&#160; (vl-load-com)    <br />&#160;&#160; (setq XLAPP (vlax-get-or-create-object &quot;Excel.Application&quot;))    <br />&#160;&#160; (vlax-dump-object XLAPP T)    <br />&#160;&#160; (gc) ; release temporary com objects     <br />&#160;&#160; (vlax-invoke-method XLAPP 'Quit)    <br />&#160;&#160; (vlax-release-object XLAPP)    <br />&#160;&#160; (setq XLAPP nil)    <br />&#160;&#160; (gc)    <br />)    <br /></p>  <p>While this technique is generally supported, it should be noted that forcing too many (gc) calls (i.e. within loops that iterate hundreds of times), will have a negative impact on performance. </p>  <p>NOTE: The VLIDE may be attached to many COM objects during a debug session. A COM server like Excel, will not be released until all of the internal VLIDE symbols referencing Excel objects go out of scope. The user/developer has no direct control over when the VLIDE internal symbols go out of scope. Closing the VLIDE does not make them go out of scope. The VLIDE never fully goes away once it has been started in a session. If developers are having trouble seeing the COM server released in a timely fashion, they should try running their application without ever having run the VLIDE in a session. </p>

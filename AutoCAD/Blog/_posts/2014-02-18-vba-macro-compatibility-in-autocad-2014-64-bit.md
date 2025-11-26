---
layout: "post"
title: "VBA macro compatibility in AutoCAD 2014 64-bit"
date: "2014-02-18 13:59:42"
author: "Madhukar Moogala"
categories:
  - "2014"
  - "ActiveX"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2014/02/vba-macro-compatibility-in-autocad-2014-64-bit.html "
typepad_basename: "vba-macro-compatibility-in-autocad-2014-64-bit"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/announcing-apphack-20.html" target="_blank">Stephen Preston</a></p>
<p>I&#39;ve been handling quite a few questions recently from customers migrating to AutoCAD 2014 64-bit from an earlier 64-bit AutoCAD version. The typical problem is something like this:</p>
<p><em>My VBA macro is referencing some additional OCX control or Type Library. It worked fine on AutoCAD 64-bit versions prior to AutoCAD 2014. But now my macro generates an error related to that Type Library or OCX control.</em></p>
<p>The explanation for this is that AutoCAD 2014 is the first AutoCAD release to use Microsoft&#39;s newer VBA 7.1 engine, where older AutoCAD versions used the VBA 6.3 engine. VBA 6.3 was available as a 32-bit component only. VBA 7.1 is available as 32-bit and 64-bit components - and (as you&#39;d expect) AutoCAD 2014 64-bit uses the 64-bit component. Your Type Library/OCX control can&#39;t be loaded because it is a 32-bit component. It is impossible to load any 32-bit component into a 64-bit process space. This is the reason for your error.</p>
<p>In order to make your macro work again, you must replace your 32-bit component with a 64-bit version - or refactor your project to use an alternative if the vendor of that component doesn&#39;t provide a 64-bit version.</p>
<p>The reason the same VBA macro worked in older AutoCAD 64-bit versions is because AutoCAD launched VBA 6.3 in a separate (32-bit) process to which AutoCAD marshals its COM calls. So, although AutoCAD was running 64-bit native, the VBA engine was still a 32-bit process and could happily load 32-bit components.</p>
<p>VBA being run in a separate process like this is also the reason why VBA macros used to be about 30 times slower running in AutoCAD 64-bit compared to AutoCAD 32-bit. The new VBA 7.1 engine fixes that problem for AutoCAD 2014 64-bit.</p>
<p>&#0160;</p>

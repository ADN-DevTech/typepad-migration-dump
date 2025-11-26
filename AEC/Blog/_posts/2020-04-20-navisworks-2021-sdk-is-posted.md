---
layout: "post"
title: "Navisworks 2021 SDK is posted"
date: "2020-04-20 19:32:53"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2020/04/navisworks-2021-sdk-is-posted.html "
typepad_basename: "navisworks-2021-sdk-is-posted"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>As usual, SDK of Navisworks 2021 has been posted on ADNOpen:<br /><a href="https://www.autodesk.com/developer-network/platform-technologies/navisworks">https://www.autodesk.com/developer-network/platform-technologies/navisworks</a></p>
<p>This SDK still keeps the similar contents like previous releases: materials of NET API, NWCreate API and COM API. Each of these materials includes samples and API documents:</p>
<p>A few notes:</p>
<ul>
<li>Navisworks 2021 is built against .NET Framework 4.7, which means the application with NET API supports .NET Framework 4.7 and above. The program will need to be compiled by Visual Studio 2019 and .NET Framework 4.7 and above.</li>
<li>The developer guide in NET API document has not been updated with the demo by latest Visual Studio and Framework, but the basic skeleton of plugin, net control and automation are not changed. The NET API samples can also be a reference on how the application is built.</li>
<li>The samples of NWCreate API configures to link the NWCreate lib with general name(say nwcreate.lib), but SDK names it with specific version number `nwcreate_18.lib`. So it will need to change the names manually, either sample project configuration or file name in SDK.</li>
<li>COM API samples are still available, but they base on raw COM API libraries. While some features of COM API are available with NET API already. If your program is also built by raw COM API, we encourage migrating to NET API , or plus COM Interop(if NET API has not supported), if they could fit the same requirement.&#0160;</li>
</ul>
<p>As always, please raise any questions on <a href="https://forums.autodesk.com/t5/navisworks-api/bd-p/600">Navisworks API forum</a>, or through <a href="http://adn.autodesk.io/">ADN portal</a>, or leave a message with this post. </p>

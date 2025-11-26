---
layout: "post"
title: "Get primitive from solid of Navisworks by Native COM"
date: "2018-09-30 07:34:14"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2018/09/get-primitive-from-solid-of-navisworks-by-native-com.html "
typepad_basename: "get-primitive-from-solid-of-navisworks-by-native-com"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>It has been known by how to&#0160;<a href="http://adndevblog.typepad.com/aec/2012/05/get-primitive-from-solid-of-navisworks.html">Get primitive from solid of Navisworks</a>. Because there is not native NET API, we will need to take advantage of COM Interop. While COM Interop means managed-native transitions, which will cause lot of the slowness when the model is huge. So&#0160;geometry extraction should be faster if it is all pure COM code. In another word,&#0160;some unmanaged COM code could potentially be quicker.</p>
<p>So I tried to make some codes. These are two plugins, one is written&#0160; C++/CLI code (by raw COM), another is NET code (by COM Interop). The stats of some sample Navisworks models and one customer specific model.</p>
<p><a href="https://github.com/xiaodongliang/Navisworks-Geometry-Primitives">https://github.com/xiaodongliang/Navisworks-Geometry-Primitives</a></p>
<table border="1" style="height: 106px; width: 730px; border-color: #000000;">
<tbody>
<tr>
<td style="width: 197px;"><strong>Model&#0160;&#0160;&#0160;&#0160;</strong></td>
<td style="width: 210px;"><strong>Geometry Nodes Count</strong></td>
<td style="width: 187px;"><strong>Fragments Count</strong></td>
<td style="width: 350px;"><strong>Time to Extract Primitive (ms)</strong></td>
</tr>
<tr>
<td style="width: 197px;">ClashTest.nwd</td>
<td style="width: 210px;">318</td>
<td style="width: 187px;">840</td>
<td style="width: 350px;">NET(334)&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;COM(227)</td>
</tr>
<tr>
<td style="width: 197px;">GateHouse.nwd</td>
<td style="width: 210px;">1354</td>
<td style="width: 187px;">2386</td>
<td style="width: 350px;">NET(688)&#0160;&#0160;&#0160;&#0160;&#0160; &#0160; COM(412)</td>
</tr>
<tr>
<td style="width: 197px;">One Customer Model</td>
<td style="width: 210px;">225360</td>
<td style="width: 187px;">353341</td>
<td style="width: 350px;">NET(2,186,333)&#0160; &#0160; COM(872,978)</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>The projects are built on Visual Studio 2017 (v15.7.5), Navisworks 2018, Win 10 in Windows Parallel of macOS Sierra version 10.12.6</p>
<p>Note: with pure COM project, build it and execute the button in <span style="color: #ff007f;"><strong>release</strong></span> mode. This is typical for performance testing. If in debugging mode, it will be extremely slow.&#0160;</p>
<p>&#0160;</p>

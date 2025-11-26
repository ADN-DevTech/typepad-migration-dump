---
layout: "post"
title: "COM API: open file using http whose server uses authentication NLTM"
date: "2012-07-29 19:11:23"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/07/com-api-open-file-using-http-whose-server-uses-authentication-nltm.html "
typepad_basename: "com-api-open-file-using-http-whose-server-uses-authentication-nltm"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue</b></p>  <p>When using the API the functions for OpenFile and AppendFile (COM Automation, Document.OpenFile), we get an exception when loading files from a http server. This server uses authentication NLTM.&#160; In UI and use the File Open and Append,&#160; there is no problem.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>In product, with interactive user   <br />&#160;&#160;&#160; 'Open File' and 'Append' both pop the Windows 'File Open' dialog. This effectively will use IE (and NLTM) to download the whole file in one go and then Roamer will try and open/append it from the local cache. </p>  <p>&#160;&#160;&#160; 'Open URL' will try and open NWD files in a custom manner by downloading chunks of it from the server using&#160; a &quot;GET&quot; &quot;Range: bytes=%s-%s&quot;. Server must support such operation. No authentication is supported.&#160;&#160;&#160;&#160; </p>  <p>The Automation and ActiveX control of COM API uses same code as 'Open URL'.&#160; So, Open/Append files by COM API from the server using NLTM is <strong>not</strong> supported. </p>

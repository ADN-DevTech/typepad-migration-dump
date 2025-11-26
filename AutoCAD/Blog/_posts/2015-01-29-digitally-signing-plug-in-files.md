---
layout: "post"
title: "Digitally signing plug-in files"
date: "2015-01-29 03:54:09"
author: "Virupaksha Aithal"
categories:
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/digitally-signing-plug-in-files.html "
typepad_basename: "digitally-signing-plug-in-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Digital signature is like an electronic security mark affixed to your plug-in file. The digital signature has the details about the publisher, the independent entity who can guarantee the publisher’s identity. It also has the cryptographic checksum which is used to verify that the content has not been tampered after signing or not.</p>
<p>Below are the steps involved in digitally signing your plug-ins</p>
<p><strong>Purchase digital signature</strong><br />There are different vendors from whom you can purchase the digital signatures.</p>
<p>Example : Verisign,&#0160;DigiCert, Thawte etc.</p>
<p>&#0160;<strong>Use signing tool</strong></p>
<p><strong>&#0160;</strong>Use <a href="http://msdn.microsoft.com/en-us/library/aa387764%28v=vs.90%29.aspx" target="_self">Signtool.exe</a> (Tool is located with Microsoft SDK toolkit) to sign&#0160;your C++ or .NET plug-in.</p>
<p>&#0160;<strong>example</strong> : signtool sign /f &lt;PFX file name&gt; /p &lt;Password&gt; FileToSign.dll<br />&#0160;where &lt;PFX file name&gt; is digital signature file (pfx file)<br />&#0160; &lt;Password&gt; - is private key of your digital signature.</p>
<p>&#0160; In case of AutoCAD Lisp files, use AcSignApply.exe. You can find this tool with acad.exe&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cad9ea970c-pi"><img alt="Acsigntool" src="/assets/image_48089.jpg" title="Acsigntool" /></a></p>
<p>Other useful links</p>
<p>Code signing <a href="http://en.wikipedia.org/wiki/Code_signing">http://en.wikipedia.org/wiki/Code_signing</a></p>
<p>Certificate authority <a href="http://en.wikipedia.org/wiki/Certificate_authority">http://en.wikipedia.org/wiki/Certificate_authority</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cad9ea970c-pi" style="float: left;">&#0160;</a></p>

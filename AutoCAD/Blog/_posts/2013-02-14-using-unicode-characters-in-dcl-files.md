---
layout: "post"
title: "Using Unicode characters in DCL files"
date: "2013-02-14 07:06:38"
author: "Adam Nagy"
categories:
  - "2013"
  - "Adam Nagy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/using-unicode-characters-in-dcl-files.html "
typepad_basename: "using-unicode-characters-in-dcl-files"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Some people experienced issues with their DCL files when using them in AutoCAD 2013. Their unicode characters (Cyrillic, Greek, etc.) were not displayed correctly. DCL files by default are saved in <a href="http://www.ehow.com/about_6361982_ansi-format_.html" target="_self">ANSI format</a> where the characters depend on the <a href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd317752(v=vs.85).aspx" target="_self">code page</a> being used. In previous versions the character set selection when dispaying the DCL dialogs was based on the OS settings, whereas in the new version it is based on the language version of AutoCAD. This change in behaviour is likely to be reverted.</p>
<p>To avoid this problem you can update your DCL file to <a href="http://en.wikipedia.org/wiki/UTF-8" target="_self">UTF-8 format</a> which does not depend on codepages, product language version or OS settings.&nbsp;<br />Note: UTF-8 DCL files can only be used from AutoCAD 2013 onwards.</p>
<p>To make your DCL file use the UTF-8 format, simply open it in <strong>Notepad</strong>, then in the <strong>Save As</strong> dialog set <strong>Encoding</strong> to <strong>UTF-8</strong>. Note that Visual Lisp editor doesn't show UTF-8 files properly, so you'll have to edit those in a unicode compatible editor such as Notepad.</p>
<p>I also tested using the UTF-8 DCL file inside a <strong>Visual Lisp Application</strong> that can be compiled into a VLX file. There are many posts on the net about <a href="http://www.caddigest.com/subjects/autocad/tutorials/select/parsai_vlx.htm" target="_self">creating a Visual Lisp Application</a>.&nbsp;</p>
<p>Once I compiled the application (File &gt;&gt; Make Application &gt;&gt; Make Application) then I got the VLX file that I could APPLOAD into AutoCAD. The <span class="asset  asset-generic at-xid-6a0167607c2431970b017c36e04b63970b"><a href="http://adndevblog.typepad.com/files/vlxtest.zip">attached sample application</a></span>&nbsp;includes a command called <strong>TestDcl</strong>, which should bring up a dialog. If it looks exactly like this then all is fine:</p>
<p>
<a class="asset-img-link" style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8837493970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee8837493970d" style="margin: 0px 5px 5px 0px;" title="DclTest" src="/assets/image_376452.jpg" alt="DclTest" /></a> <br /></p>

---
layout: "post"
title: "Access the default directory name in file-open dialog"
date: "2013-03-20 00:52:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/access-the-default-directory-name-in-file-open-dialog.html "
typepad_basename: "access-the-default-directory-name-in-file-open-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue      <br /></strong>Where is the variable stored for the default directory name that the File-Open dialog uses, and can it be accessed? This is so that a user can open a drawing from a directory that is not valid for my application.</p>
<p><a name="section2"></a></p>
<p><strong>Solution      <br /></strong>You can get the information about the current drawing in the editor by using these two system variables: DWGNAME and DWGPREFIX. DWGNAME contains the drawing name, including the .DWG extension. DWGPREFIX contains the path where the     <br />drawing resides. Combining these system variables gives the full name of the current drawing.</p>
<p>Note that DWGPREFIX does not always contain the directory that the File-Open dialog defaults to. Instead, it defaults to the path of the last opened drawing, even if that drawing has been closed. Note that there is an exception to this: AutoCAD will not change the default File-Open directory if a file is opened from the history list. AutoCAD writes this information to Windows registry table under the following KEY when quitting and reads it as its history file list when starting.</p>
<p>&quot;HKEY_CURRENT_USER\\Software\\Autodesk\\AutoCAD\\R17.0\\ACAD-xxx.xxx\\Recent File List&quot;&#0160; </p>
<p>Where xxx.xxx is the version number of pure AutoCAD or vertical AutoCAD.</p>
<p>When AutoCAD is running, it does not update this information in the registry table; therefore, it is not a practical way to retrieve this information using the following VLISP expression.</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">vl-registry-read</span>       <br /><span style="color: #ff00ff;">&quot;HKEY_CURRENT_USER\\Software\\Autodesk\\AutoCAD\\R17.0\\ACAD-5001:409\\Recent File List&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;File1&quot;</span><span style="color: #ff0000;">)</span></span></p>
<p>It also does not work to set the latest opened file&#39;s path to what you want because AutoCAD maintains this information in memory, therefore, the information has not been exposed to a system variable or an API.</p>
<p>Fortunately, you can use the following simple AutoLISP expression to mimic the AutoCAD OPEN command or you can write your own version of the OPEN command.</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span>getfiled <span style="color: #ff00ff;">&quot;Select File&quot;</span> &quot;c:<span style="color: #0000ff;">/</span>program files<span style="color: #0000ff;">/</span>AutoCAD 2012<span style="color: #ff00ff;">&quot; &quot;</span>dwg&quot; <span style="color: #008000;">8</span><span style="color: #ff0000;">)</span>      <br /></span></p>

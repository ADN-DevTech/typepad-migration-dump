---
layout: "post"
title: "Menu information in the registry"
date: "2013-01-04 16:42:41"
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
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/menu-information-in-the-registry.html "
typepad_basename: "menu-information-in-the-registry"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>If you are considering replacing or manipulating AutoCAD menus, here is some information that could be useful.</p>  <p>The menu information is stored in the user's profile (of the registry) in two locations:</p>  <p><font color="#000000" size="1">HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R19.0\ACAD-B001:409\Profiles\&lt;&lt;Unnamed Profile&gt;&gt;\General Configuration\MenuFile</font></p>  <p><font color="#000000" size="1">HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R19.0\ACAD-B001:409\Profiles\&lt;&lt;Unnamed Profile&gt;&gt;\Menus</font></p>  <p>Note that the AutoCAD Base Menu is stored in the MenuFile key, and the Partial Menu information is located in the Menus key.</p>  <p>Within the Menus key you have two sets of sub-keys: Groups and Pops.</p>  <p>- GroupX (where X denotes a number) are the menu groups, and each is a string consisting of the menu group name followed by the identification&#160; in the menu file. e.g.</p>  <p><font size="1">Group2 : &quot;CUSTOM custom&quot;</font></p>  <p>- PopX are the individual menus, and each is a string containing the menu group followed by the pop menu name. e.g.</p>  <p><font size="1">Pop8 : &quot;ACAD POP1&quot;</font></p>  <p>One point to note is that the registry should be manipulated when AutoCAD is not running. If done during the time AutoCAD is running, the information won't be valid for the current session, and could be erased or altered for the current user profile when AutoCAD is shutdown.</p>

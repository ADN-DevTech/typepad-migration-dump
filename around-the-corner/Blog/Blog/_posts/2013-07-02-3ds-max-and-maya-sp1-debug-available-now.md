---
layout: "post"
title: "3ds Max and Maya SP1 debug available now."
date: "2013-07-02 08:09:40"
author: "Cyrille Fauvel"
categories:
  - "3ds Max"
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Mac"
  - "Maya"
  - "MotionBuilder"
  - "Visual Studio"
  - "Windows"
  - "XCode"
original_url: "https://around-the-corner.typepad.com/adn/2013/07/3ds-max-and-maya-sp1-debug-available-now.html "
typepad_basename: "3ds-max-and-maya-sp1-debug-available-now"
typepad_status: "Publish"
---

<h2><span style="font-size: 1.17em;">What are symbols?</span></h2>
<p>Symbols or Program Database Files (PDBs) are a by-product of the build process and contain debugging information for specific binary modules (e.g. exe or&#0160;dll files).</p>
<p>By default, a C++ PDB file contains the following information:</p>
<ul>
<li>Public symbols (typically all functions, static and global variables)</li>
<li>A list of object files that are responsible for sections of code in the executable</li>
<li>Frame pointer optimization information (FPO)</li>
<li>Name and type information for local variables and data structures</li>
<li>Source file and line number information</li>
</ul>
<p>A lot of the information contained in a PDB is useful only when the source code of the corresponding module is available. Stripping out this information keeps them small while allowing the debugger to reconstruct the callstack and still preserving our intellectual property. By default, stripped PDB files contain the following information:</p>
<ul>
<li>Public symbols (typically only non-static functions and global variables)</li>
<li>A list of object files that are responsible for sections of code in the executable</li>
<li>Frame pointer optimization information (FPO)</li>
</ul>
<h2>3ds Max</h2>
<p>3ds Max debug symbols are available via a symbol server&#0160;<a href="http://symbols.autodesk.com/symbols">http://symbols.autodesk.com/symbols</a>.</p>
<p>We just posted symbols&#0160;for 3ds Max 2014 and 3ds Max 2014 SP1</p>
<h2>Maya</h2>
<p>Maya debug symbols for Maya 2014 and Maya 2014 SP1 are also available, but on the ADN portal which is restricted to ADN members unfortunately. Because Maya is running in 3 platforms, we could not post the symbols on the server symbols.</p>
<h2>MotionBuilder</h2>
<p>MotionBuilder debug symbols for MotionBuiler 2013 and MotionBuiler 2014 are also available on the ADN portal for both Windows and Linux platform.</p>
<p>&#0160;</p>

---
layout: "post"
title: "Advanced Visual Studio debugging: how to stop stepping into certain functions"
date: "2006-07-28 15:39:28"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Debugging"
  - "ObjectARX"
  - "Visual Studio"
original_url: "https://www.keanw.com/2006/07/advanced_visual_1.html "
typepad_basename: "advanced_visual_1"
typepad_status: "Publish"
---

<p>While debugging it's sometimes very frustrating to find yourself repeatedly stepping into an irrelevant function. For instance, complex statements pretty commonly include object constructors etc. that you know function perfectly well, but the debugger routinely takes you into them.</p>

<p>The Visual Studio IDE has an undocumented (and unsupported) mechanism to help with this. During the VC 6.0 timeframe it was implemented via our old friend the autoexp.dat file (see <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/advanced_visual.html">my previous post on this</a>), in a special section called [ExecutionControl]. Since VC 7.0 this has been moved to the Registry.</p>

<p>For VC 7.0 and 7.1, it was in the HKCU (HKEY_CURRENT_USER) section:</p><blockquote dir="ltr"><p><em>HKCU\Software\Microsoft\VisualStudio\7.0\NativeDE\StepOver<br />HKCU\Software\Microsoft\VisualStudio\7.1\NativeDE\StepOver</em></p></blockquote><p>For VC 8.0 (Visual Studio 2005), it shifted across to HKLM (HKEY_LOCAL_MACHINE):</p><blockquote dir="ltr"><p><em>HKLM\SOFTWARE\Microsoft\VisualStudio\8.0\NativeDE\StepOver</em></p></blockquote><p>Inside this key you will need to create string values that specify the functions and class methods to exclude (or include) while stepping through code in the debugger. For VC7.0/7.1 you needed to use a numeric identifier for the name, but with VC8.0 you can thankfully use something more meaningful:</p>

<p><em>Name&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; Value</em><br />AcDbOP cons&nbsp; &nbsp; AcDbObjectPointer\&lt;.*\&gt;\:\:AcDbObjectPointer.*=NoStepInto<br />App init&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; InitApplication=NoStepInto</p>

<p>[ <strong>Note:</strong> I would have used something even more meaningful than AcDbOP above, but I wanted the text to all fit on one line for clarity... :-) ]</p>

<p>The first part of the string value needs to be defined as a regular expression: the text I'm looking for in the first instance is really &quot;AcDbObjectPointer&lt;*&gt;::AcDbObjectPointer*&quot;, but I had to use an escape character to prefix some of the symbols. Check MSDN for more information on <a href="http://msdn.microsoft.com/library/en-us/cpguide/html/cpconregularexpressionsaslanguage.asp">regular expressions</a>.</p>

<p>This is also the string as you would enter it via the Registry Editor - if you're using a .reg file then you'll need to double-up the slashes.</p>

<p>The second part of the string value is likely to be &quot;=NoStepInto&quot; or &quot;=StepInto&quot;, depending on whether you are telling the debugger to exclude or include the locations defined by the expression. If you want to get really clever, you can use wildcards to exclude all the functions of a particular class, and then specifically re-include the ones that interest you. This <a href="http://www.codeguru.com/forum/archive/index.php/t-292148.html">CodeGuru forum post</a> shows some good examples of that. I'd also recommend reading this <a href="http://blogs.msdn.com/andypennell/archive/2004/02/06/69004.aspx">entry from Andy Pennell's blog</a>.</p>

<p>Incidentally, you don't have to restart Visual Studio for the changes to be picked up - both autoexp.dat and this section of the Registry are read in as a debug session is started (although the autoexp.dat timestamp needs to be more recent than the end of the last debugging session, interestingly enough... so if you make changes and save while a debug session is running, they won't be picked up in the next session unless a save is done between the sessions &lt;phew!&gt;).</p>

<p>When you come to execute your code, you should find that it no longer steps into the constructor of any class defined using the AcDbObjectPointer template (such as AcDbObjectPointer&lt;AcDbViewportTableRecord&gt;).</p>

<p>[ <strong>Side note:</strong> This template is one of the ObjectARX &quot;smart pointers&quot;, and are a great way to handle the opening and closing of persistent ObjectARX objects. I won't go into detail here, but they are well worth checking out in the ObjectARX SDK documentation. ]</p>

<p>When I say &quot;step into&quot;, I literally mean you would need to be stepping through the code using the debug toolbar or F11 - execution still stops in that particular function if you set a break-point there.</p>

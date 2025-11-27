---
layout: "post"
title: "Calling ObjectARX functions from a .NET application"
date: "2006-07-10 13:22:46"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "ObjectARX"
original_url: "https://www.keanw.com/2006/07/calling_objecta.html "
typepad_basename: "calling_objecta"
typepad_status: "Publish"
---

<p>One of the really compelling features of .NET is its ability to call &quot;legacy&quot; unmanaged C++ APIs. I say &quot;legacy&quot;, but we use this facility regularly to call APIs that are far from being considered defunct (the C++ version of ObjectARX is alive and kicking, believe me! :-).</p>

<p>Autodesk understands that our development partners have invested many years in application development, and can't afford to throw that investment away to support the latest &amp; greatest (and sometimes &quot;flavor of the month&quot;) programming technology. For example, over the years we've made sure it was possible to create a VB or VBA user-interface for an existing LISP application or now a .NET user-interface for an ObjectARX application. Sometimes we expose our own interoperability functions to help with this (such as LISP functions to call ActiveX DLLs), and in other cases we advise people on how best to leverage standard Microsoft platform technologies.</p>

<p>So... how do you call an ObjectARX function from VB.NET? The answer is <a href="http://msdn2.microsoft.com/en-us/library/0h9e9t7d.aspx">Platform Invoke</a> (or P/Invoke for short). Microsoft has not exposed the full functionality of the Win32 API through the .NET Framework - just as Autodesk has not exposed all of ObjectARX through AutoCAD's Managed API - but P/Invoke helps you get around this.</p>

<p>First, some background on what ObjectARX really is, and how P/Invoke can help us.</p>

<p>ObjectARX is a set of APIs that are exported from DLLs or EXEs. Most exported functions get &quot;<a href="http://msdn2.microsoft.com/en-us/library/56h2zst2.aspx">decorated</a>&quot; or &quot;mangled&quot; during compilation, unless there is a specific compiler directive not to (this is the case for all the old ADS functions, for instance - they are declared as extern &quot;C&quot; and are therefore not mangled). The compiler assigns a unique name based on the function signature, which makes sense: it is quite legal in C++ to have two functions with the same name, but not with identical arguments and return values. The decorated name includes the full function name inside it, which is why the below technique for finding the correct export works.</p>

<p>[ <strong>Note:</strong> this technique works well for C-style functions, or C++ static functions. It will not work on instance members (methods of classes), as it is not possible to instantiate an unmanaged object of the class that defines the class from managed code. If you need to expose a class method to managed code, you will need to write &amp; expose some native C++ code that instantiates the class, calls the method and returns the result. ]</p>

<p>To demonstrate the procedure we're going to work through the steps needed to call acedGetUserFavoritesDir() from C# and VB.NET. This function is declared in the ObjectARX headers as:</p><blockquote dir="ltr"><p><span style="color: #0000ff;">extern</span> Adesk::Boolean acedGetUserFavoritesDir( ACHAR* szFavoritesDir );</p></blockquote><p>According to the ObjectARX Reference, &quot;this function provides access to the Windows Favorites directory of the current user.&quot;</p><br /><p><strong>Step 1 - Identify the location of the export.</strong></p>

<p>Fenton Webb, from DevTech EMEA, provided this handy batch file he uses for just this purpose:</p>

<p>[ Copy and paste this into a file named &quot;findapi.bat&quot;, which you then place this into your AutoCAD application folder. You will need to run findapi from a command prompt which knows where to find dumpbin.exe - the Visual Studio Command Prompts created on installing VS will help you with this. ]</p><blockquote dir="ltr"><p>@echo off<br />if &quot;%1&quot; == &quot;&quot; goto usage<br />:normal<br />for %%i IN (*.exe *.dll *.arx *.dbx *.ocx *.ddf) DO dumpbin /exports %%i | findstr &quot;%%i %1&quot;<br />goto end<br />:usage<br />echo findapi &quot;function name&quot;<br />:end</p></blockquote><p dir="ltr">You can redirect the output into a text file, of course, for example:</p><blockquote dir="ltr"><p>C:\Program Files\AutoCAD 2007&gt;findapi acedGetUserFavoritesDir &gt; results.txt</p></blockquote><p>It'll take some time to work, as this batch file chunks through all the DLLs, EXEs, etc. in the AutoCAD application folder to find the results (it doesn't stop when it finds one, either - this enhancement is left as an exercise for the reader ;-).</p>

<p>Opening the text file will allow you to see where the acedGetUserFavoritesDir() function is exported:</p>

<p>[ from the results for AutoCAD 2007 ]</p><blockquote dir="ltr"><p>Dump of file acad.exe<br />&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 436&nbsp; 1B0 004B4DC0 ?acedGetUserFavoritesDir@@YAHPA_W@Z</p></blockquote><p dir="ltr"><strong>A word of warning:</strong> the decorated names for functions accepting/returning strings changed between AutoCAD 2006 and 2007, because we are now using Unicode for string definition. Here is the previous declaration for 2004/2005/2006 (which was probably valid for as long as the function was defined, back in AutoCAD 2000i, if I recall correctly):</p>

<p>[ from the results for AutoCAD 2006 ]</p><blockquote dir="ltr"><p>Dump of file acad.exe<br />&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 357&nbsp; 161 00335140 ?acedGetUserFavoritesDir@@YAHPAD@Z</p></blockquote><p>This is simply because the function signature has changed from taking a char* to an ACHAR* (a datatype which now resolves to a &quot;wide&quot; or Unicode string in AutoCAD 2007). A change in the function signature results in a change in the decorated name. This is straightforward enough, but it is worth bearing in mind the potential migration issue - a heavy dependency on decorated function names can lead to substantial migration effort if widespread signature changes are made in a release (as with AutoCAD 2007's support of Unicode).</p>

<p><strong>Another warning:</strong> you will find a number of other functions exported from the various DLLs/EXEs that do not have corresponding declarations in the ObjectARX headers. These functions - while exposed - are not supported. Which means that you may be able to work out how they can be called, but use them at your own risk (which can be substantial). Unsupported APIs are liable to change (or even disappear) without notice.</p>

<p>Now we've identified where and how the function is exposed, we can create a declaration of this function we can use in our code.</p><br /><p><strong>Step 2 - Declare the function correctly in your code.</strong></p>

<p>This is going to be slightly different depending on the programming language you're using.</p>

<p>VB developers will be used to using &quot;Declare&quot; to set-up P/Invoke from their projects. This ends up being translated by the compiler into calls to DllImport, which is also used directly in C#.</p>

<p>These declarations should be made at the class level (not within an individual function definition).</p>

<p>VB.NET</p><blockquote dir="ltr"><p><span style="color: #0000ff;">Private</span> <span style="color: #0000ff;">Declare</span> <span style="color: #0000ff;">Auto</span> <span style="color: #0000ff;">Function</span> acedGetUserFavoritesDir <span style="color: #0000ff;">Lib</span> <span style="color: #800000;">&quot;acad.exe&quot;</span> <span style="color: #0000ff;">Alias</span> <span style="color: #800000;">&quot;?acedGetUserFavoritesDir@@YAHPA_W@Z&quot;</span> (&lt;MarshalAs(UnmanagedType.LPWStr)&gt; <span style="color: #0000ff;">ByVal</span> sDir <span style="color: #0000ff;">As</span> StringBuilder) <span style="color: #0000ff;">As</span> <span style="color: #0000ff;">Boolean</span></p></blockquote><p>C#</p><blockquote dir="ltr"><p>[<span style="color: #008080;">DllImport</span>(<span style="color: #800000;">&quot;acad.exe&quot;</span>, EntryPoint = <span style="color: #800000;">&quot;?acedGetUserFavoritesDir@@YAHPA_W@Z&quot;</span>, CharSet = <span style="color: #008080;">CharSet</span>.Auto)]<br /><span style="color: #0000ff;">public</span> <span style="color: #0000ff;">static</span> <span style="color: #0000ff;">extern</span> <span style="color: #0000ff;">bool</span> acedGetUserFavoritesDir([<span style="color: #008080;">MarshalAs</span>(<span style="color: #008080;">UnmanagedType</span>.LPWStr)] <span style="color: #008080;">StringBuilder </span>sDir);</p></blockquote><p><em>Notes:</em></p>

<ol><li>It's worth specifying the character set as &quot;Auto&quot; - which is not the default setting. The compiler does a good job of working out whether to use Unicode or ANSI, so it's easiest to trust it to take care of this.</li>

<li>You will need to use the MarshalAs(UnmanagedType.LPWStr) declaration for Unicode string variables in 2007. This is true whether using Strings or StringBuilders.</li>

<li>Use a StringBuilder for an output string parameter, as standard Strings are considered immutable. Strings are fine for input parameters.</li></ol><br /><p><strong>Step 3 - Use the function in your code</strong></p>

<p>[ I've omited the standard using/import statements, as well as the class &amp; function declarations, to improve readability. ]</p>

<p>VB.NET</p><blockquote dir="ltr"><p><span style="color: #0000ff;">Dim </span>ed <span style="color: #0000ff;">As</span> Editor = Application.DocumentManager.MdiActiveDocument.Editor<br /><span style="color: #0000ff;">Dim</span> sDir <span style="color: #0000ff;">As</span> <span style="color: #0000ff;">New </span>StringBuilder(256) <br /><span style="color: #0000ff;">Dim</span> bRet <span style="color: #0000ff;">As</span> <span style="color: #0000ff;">Boolean</span> = acedGetUserFavoritesDir(sDir)<br /><span style="color: #0000ff;">If</span> bRet <span style="color: #0000ff;">And</span> sDir.Length &gt; 0 <span style="color: #0000ff;">Then</span><br />&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: #800000;">&quot;Your favorites folder is: &quot;</span> + sDir.ToString)<br /><span style="color: #0000ff;">End</span> <span style="color: #0000ff;">If</span></p></blockquote><p>C#</p><blockquote dir="ltr"><p><span style="color: #008080;">Editor</span> ed = <span style="color: #008080;">Application</span>.DocumentManager.MdiActiveDocument.Editor;<br /><span style="color: #008080;">StringBuilder</span> sDir = <span style="color: #0000ff;">new</span> <span style="color: #008080;">StringBuilder</span>(256);<br /><span style="color: #0000ff;">bool</span> bRet = acedGetUserFavoritesDir(sDir);<br /><span style="color: #0000ff;">if</span> (bRet &amp;&amp; sDir.Length &gt; 0)<br />&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="color: #800000;">&quot;Your favorites folder is: &quot;</span> + sDir.ToString());</p></blockquote><p><em>Note:</em> we declare the StringBuilder variable (sDir) as being 256 characters long. AutoCAD expects us to provide a sufficiently long buffer for the data to be copied into it.</p>

<p>On my system both code snippets resulted in the following being sent to AutoCAD's command-line:</p><blockquote dir="ltr"><p>Your favorites folder is: C:\My Documents\Favorites</p></blockquote><p>So that's it: you should now be able to call global ObjectARX functions from .NET. This technique can also be used to call your own functions exposed from DLLs... which is one way to allow you to create fancy UIs with .NET and leverage existing C++ code (there are others, such as exposing your own Managed API).</p>

<p>For additional information on using P/Invoke, particularly with Win32, here is a really <a href="http://pinvoke.net/">great resource</a>.</p>

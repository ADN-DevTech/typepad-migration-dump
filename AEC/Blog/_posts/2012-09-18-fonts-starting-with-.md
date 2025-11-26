---
layout: "post"
title: "Fonts starting with \"@\" "
date: "2012-09-18 22:29:23"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "Mikako Harada"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/09/fonts-starting-with-.html "
typepad_basename: "fonts-starting-with-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>How can I get a list of font names used in Revit and AutoCAD? In particular, I&#39;m interested in getting a list of font names starting with&#0160;&quot;@&quot;, such as &quot;@Arial Unicode MS&quot; and &quot;@Batang&quot;. They&#0160;appear at the top of the list in those Autodesk products. They are used to draw characters in sideways, makeing it possible to write, for example, Japanese characters from top to&#0160;bottom.) &#0160;I&#0160;tried System.Drawing.FontFamily.Families and System.Windows.Media.Fonts.SystemFontFamilies. But they don&#39;t list those fonts that start with&#0160;&quot;@&quot;. &#0160;</p>
<p><strong>Solution</strong></p>
<p>Revit and AutoCAD uses gdi32 call &quot;EnumFontFamiliesEx&quot; which is available for C++ API. Unfortunately, there is no equivalent function in C#.&#0160; A workaround is to use P/Invoke or platform invoke.&#0160;There are quite a few&#0160;examples&#0160;to do this&#0160;if you search the internet.&#0160;For example, the following is what I found&#0160;on the Visual Studio Developer Center: </p>
<p>Visual Studio Developer Center &gt; Visual C# Forums &gt;&#0160; Visual C# General &gt;&#0160;&#0160;<a href="http://social.msdn.microsoft.com/Forums/en/csharpgeneral/thread/f570b1a5-dcd9-4223-af04-be16e055a5f3" target="_self" title="EnumFontFamiliesEx C#">How can I get all active fonts in C#? Having issues with gdi32 callback.</a></p>
<p>Another&#0160;is at&#0160;this discussion forum:</p>
<p>GNT A Generation of New Technology &gt; Technology &gt; Forum &gt; Win32 Programmer &gt; GDI &gt; <a href="http://us.generation-nt.com/answer/enumfontfamiliesex-c-help-27455462.html" target="_self" title="EnumFontFamiliyEx in C# 2">EnumFontFamiliesEx in C#</a></p>
<p>Following the above posts, I did tried it myself, and I was able to obtains the names of fonts including ones&#0160;starting&#0160;with &quot;@&quot;.&#0160;</p>
<p>Since I took most of the code from the above links, I will refer to&#0160;them for the portion that I did not modify. The code below are to show you how&#0160;I adapted and modified from the original code for the purpose of my experiment. I hope you get an idea.&#0160;&#0160; </p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; </span><span style="color: blue; line-hight: 140%;">class</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">EnumFontFamiliesEx_CS</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160; &#0160; #region</span><span style="line-hight: 140%;"> EnumFont </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-hight: 140%;">DllImport</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;gdi32.dll&quot;</span><span style="line-hight: 140%;">, CharSet = </span><span style="color: #2b91af; line-hight: 140%;">CharSet</span><span style="line-hight: 140%;">.Auto)]</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">extern</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> EnumFontFamiliesEx(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> hdc,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; [</span><span style="color: #2b91af; line-hight: 140%;">In</span><span style="line-hight: 140%;">] </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> pLogfont,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">EnumFontExDelegate</span><span style="line-hight: 140%;"> lpEnumFontFamExProc,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> lParam,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">uint</span><span style="line-hight: 140%;"> dwFlags);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // Copy the following from the example in the first link:</span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // class LOGFONT, enum FontWeight, FontCharSet, </span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // FontPrecision, FontClipPrecision, FontQuality, </span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // FontPitchAndFamily, struct NEWTEXTMETRIC, FONTSIGNATURE, </span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // NEWTEXTMETRICES, ENUMLOGFONTEX, const, LOFFONT, </span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // EnumFontExDelegate, </span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;&#0160;&#0160; // ... </span></p>
<p style="margin: 0px;"><span style="color: #007f40; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160; &#0160; #endregion</span><span style="line-hight: 140%;"> EnumFont</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">EnumFontExDelegate</span><span style="line-hight: 140%;"> del1;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">List</span><span style="line-hight: 140%;">&lt;</span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;">&gt; EnumFont_Test()</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; fontList.Clear(); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">LOGFONT</span><span style="line-hight: 140%;"> lf = CreateLogFont(</span><span style="color: #a31515; line-hight: 140%;">&quot;&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> plogFont = </span><span style="color: #2b91af; line-hight: 140%;">Marshal</span><span style="line-hight: 140%;">.AllocHGlobal(</span><span style="color: #2b91af; line-hight: 140%;">Marshal</span><span style="line-hight: 140%;">.SizeOf(lf));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Marshal</span><span style="line-hight: 140%;">.StructureToPtr(lf, plogFont, </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> ret = 0;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> hPrinterDC;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">StringBuilder</span><span style="line-hight: 140%;"> dp = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">StringBuilder</span><span style="line-hight: 140%;">(256);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> size = dp.Capacity;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (GetDefaultPrinter(dp, </span><span style="color: blue; line-hight: 140%;">ref</span><span style="line-hight: 140%;"> size))</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Console</span><span style="line-hight: 140%;">.Write(</span><span style="color: #2b91af; line-hight: 140%;">String</span><span style="line-hight: 140%;">.Format(</span><span style="color: #a31515; line-hight: 140%;">&quot;Printer: {0}, name length{1}&quot;</span><span style="line-hight: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; dp.ToString().Trim(), size));</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Console</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;Error!&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; hPrinterDC = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; CreateDC(</span><span style="color: #a31515; line-hight: 140%;">&quot;WINSPOOL&quot;</span><span style="line-hight: 140%;">, dp.ToString(), </span><span style="color: blue; line-hight: 140%;">null</span><span style="line-hight: 140%;">, </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;">.Zero);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> P = hPrinterDC; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; del1 = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">EnumFontExDelegate</span><span style="line-hight: 140%;">(callback1);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; ret = EnumFontFamiliesEx(P, plogFont, del1, </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;">.Zero, 0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; System.Diagnostics.</span><span style="color: #2b91af; line-hight: 140%;">Trace</span><span style="line-hight: 140%;">.WriteLine(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;EnumFontFamiliesEx = &quot;</span><span style="line-hight: 140%;"> + ret.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; DeleteDC(P); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; System.Diagnostics.</span><span style="color: #2b91af; line-hight: 140%;">Trace</span><span style="line-hight: 140%;">.WriteLine(</span><span style="color: #a31515; line-hight: 140%;">&quot;Error!&quot;</span><span style="line-hight: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">finally</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Marshal</span><span style="line-hight: 140%;">.DestroyStructure(plogFont, </span><span style="color: blue; line-hight: 140%;">typeof</span><span style="line-hight: 140%;">(</span><span style="color: #2b91af; line-hight: 140%;">LOGFONT</span><span style="line-hight: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> fontList;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Save fonts </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">List</span><span style="line-hight: 140%;">&lt;</span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;">&gt; fontList = </span><span style="color: blue; line-hight: 140%;">new</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">List</span><span style="line-hight: 140%;">&lt;</span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;">&gt;();&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// This is the function that does the extraction of fonts </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> callback1(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ref</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">ENUMLOGFONTEX</span><span style="line-hight: 140%;"> lpelfe, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">ref</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">NEWTEXTMETRICEX</span><span style="line-hight: 140%;"> lpntme, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> FontType,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> lParam)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; { </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// Do something here. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// This will print out all the fonts. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">//Debug.WriteLine(lpelfe.elfFullName); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// For testing purpose, we pretent we are intersted in </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// only the ones which start with @.</span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; // Ommit s.StartsWith(&quot;@&quot;) condition if you want to list all. </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> s = lpelfe.elfFullName;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">if</span><span style="line-hight: 140%;"> (s.StartsWith(</span><span style="color: #a31515; line-hight: 140%;">&quot;@&quot;</span><span style="line-hight: 140%;">) &amp;&amp; !fontList.Contains(s))</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(s);</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; fontList.Add(s); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">catch</span><span style="line-hight: 140%;"> (</span><span style="color: #2b91af; line-hight: 140%;">Exception</span><span style="line-hight: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; System.Diagnostics.</span><span style="color: #2b91af; line-hight: 140%;">Trace</span><span style="line-hight: 140%;">.WriteLine(e.ToString());</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<span style="line-hight: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">return</span><span style="line-hight: 140%;"> 1; </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160; &#0160; #region</span><span style="line-hight: 140%;"> EnumFont2</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// This is from another test func</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// <a href="http://us.generation-nt.com/answer/"><span style="color: #007f40;">http://us.generation-nt.com/answer/</span></a></span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;&#0160;&#0160; // enumfontfamiliesex-c-help-27455462.htm</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-hight: 140%;">DllImport</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;gdi32.dll&quot;</span><span style="line-hight: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">extern</span><span style="line-hight: 140%;"> </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> CreateDC(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> lpszDriver, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> lpszDevice,</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160;&#0160; &#0160;</span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> lpszOutput, </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> lpInitData);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-hight: 140%;">DllImport</span><span style="line-hight: 140%;">(</span><span style="color: #a31515; line-hight: 140%;">&quot;gdi32.dll&quot;</span><span style="line-hight: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">extern</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">bool</span><span style="line-hight: 140%;"> DeleteDC(</span><span style="color: #2b91af; line-hight: 140%;">IntPtr</span><span style="line-hight: 140%;"> hdc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-hight: 140%;">DllImport</span><span style="line-hight: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #a31515; line-hight: 140%;">&quot;winspool.drv&quot;</span><span style="line-hight: 140%;">, CharSet = </span><span style="color: #2b91af; line-hight: 140%;">CharSet</span><span style="line-hight: 140%;">.Auto, SetLastError = </span><span style="color: blue; line-hight: 140%;">true</span><span style="line-hight: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">public</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">static</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">extern</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">bool</span><span style="line-hight: 140%;"> GetDefaultPrinter(</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-hight: 140%;">StringBuilder</span><span style="line-hight: 140%;">pszBuffer,&#0160;</span><span style="color: blue; line-hight: 140%;">ref</span><span style="line-hight: 140%;"> </span><span style="color: blue; line-hight: 140%;">int</span><span style="line-hight: 140%;"> size);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-hight: 140%;">&#0160; &#0160; #endregion</span><span style="line-hight: 140%;"> EnumFont2 </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Once you have defined this helper class, you can call it from your Revit command like as follows: </p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// For testing purpose, we are getting only the font </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// that starts with &quot;@&quot;. </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// To get the full list, please modify the EnumFont </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-hight: 140%;">// function yourself. </span></p>
<p style="margin: 0px;"><span style="color: green; line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">List</span><span style="line-hight: 140%;">&lt;</span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;">&gt; fontList = </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">EnumFontFamiliesEx_CS</span><span style="line-hight: 140%;">.EnumFont_Test();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fontList.Sort();</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-hight: 140%;">foreach</span><span style="line-hight: 140%;"> (</span><span style="color: blue; line-hight: 140%;">string</span><span style="line-hight: 140%;"> s </span><span style="color: blue; line-hight: 140%;">in</span><span style="line-hight: 140%;"> fontList)</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-hight: 140%;">Debug</span><span style="line-hight: 140%;">.WriteLine(s); </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">The result will look like following: </span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">@Arial Unicode MS<br />@Batang<br />@BatangChe<br />@DFKai-SB<br />@Dotum<br />@DotumChe<br />@FangSong</span></p>
<p style="margin: 0px;"><span style="line-hight: 140%;">...</span></p>
</div>

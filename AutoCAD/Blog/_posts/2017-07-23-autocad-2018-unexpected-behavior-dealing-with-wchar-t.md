---
layout: "post"
title: "AutoCAD 2018: Unexpected Behavior Dealing with wchar_t"
date: "2017-07-23 18:16:00"
author: "Madhukar Moogala"
categories:
  - "2017"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/07/autocad-2018-unexpected-behavior-dealing-with-wchar_t.html "
typepad_basename: "autocad-2018-unexpected-behavior-dealing-with-wchar_t"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar  Moogala</a></p> <p>Recently I had a request from an ADN partner troubleshooting a problem with reading values from a text file.</p><p>Assume we have a text file with following contents</p><p>Helloworld|Autodesk.</p><p>And, user would like to split string with pipe delimitation, so expected output would be </p><p>Helloworld and Autodesk.</p><p><br></p>
<pre style="background: rgb(0, 0, 0); color: rgb(209, 209, 209);"><span style="color: rgb(0, 128, 115);">#</span><span style="color: rgb(0, 128, 115);">define</span><span style="color: rgb(0, 128, 115);"> wprintf acutPrintf</span>
<span style="color: rgb(230, 97, 112); font-weight: bold;">void</span> readFile<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">)</span>
<span style="color: rgb(176, 96, 176);">{</span>
       <span style="color: rgb(230, 97, 112); font-weight: bold;">const</span> <span style="color: rgb(230, 97, 112); font-weight: bold;">wchar_t</span>  textFile<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(210, 205, 134);">]</span> <span style="color: rgb(210, 205, 134);">=</span> _T<span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">C:</span><span style="color: rgb(0, 128, 128);">\\</span><span style="color: rgb(0, 196, 196);">Temp</span><span style="color: rgb(0, 128, 128);">\\</span><span style="color: rgb(0, 196, 196);">TFile</span><span style="color: rgb(0, 128, 128);">\\</span><span style="color: rgb(0, 196, 196);">helloworld.txt</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
       <span style="color: rgb(230, 97, 112); font-weight: bold;">FILE</span> <span style="color: rgb(210, 205, 134);">*</span> pFile <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(0, 125, 69);">NULL</span><span style="color: rgb(176, 96, 176);">;</span>
       <span style="color: rgb(230, 97, 112); font-weight: bold;">wchar_t</span> f1<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">20</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(210, 205, 134);">,</span> f2<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">20</span><span style="color: rgb(210, 205, 134);">]</span><span style="color: rgb(176, 96, 176);">;</span>
       f1<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span> <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(0, 196, 196);">'\0'</span><span style="color: rgb(176, 96, 176);">;</span> f2<span style="color: rgb(210, 205, 134);">[</span><span style="color: rgb(0, 140, 0);">0</span><span style="color: rgb(210, 205, 134);">]</span> <span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(0, 196, 196);">'\0'</span><span style="color: rgb(176, 96, 176);">;</span>

       <span style="color: rgb(230, 97, 112); font-weight: bold;">if</span> <span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(230, 97, 112); font-weight: bold;">_wfopen_s</span><span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(210, 205, 134);">&amp;</span>pFile<span style="color: rgb(210, 205, 134);">,</span> textFile<span style="color: rgb(210, 205, 134);">,</span> <span style="color: rgb(2, 208, 69);">L"</span><span style="color: rgb(0, 196, 196);">r</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(0, 140, 0);">0</span> <span style="color: rgb(210, 205, 134);">&amp;</span><span style="color: rgb(210, 205, 134);">&amp;</span> pFile <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(0, 125, 69);">NULL</span><span style="color: rgb(210, 205, 134);">)</span>
       <span style="color: rgb(176, 96, 176);">{</span>
             <span style="color: rgb(230, 97, 112); font-weight: bold;">wprintf</span><span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">L"</span><span style="color: rgb(0, 196, 196);">failure opening file </span><span style="color: rgb(0, 121, 151);">%s</span><span style="color: rgb(0, 196, 196);"> !</span><span style="color: rgb(0, 128, 128);">\n</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">,</span> textFile<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
             <span style="color: rgb(230, 97, 112); font-weight: bold;">return</span><span style="color: rgb(176, 96, 176);">;</span>
       <span style="color: rgb(176, 96, 176);">}</span>
       <span style="color: rgb(153, 153, 169);">/*</span>
<span style="color: rgb(153, 153, 169);">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; %[^|] = store everything before '|' in place holder</span>
<span style="color: rgb(153, 153, 169);"></span>
<span style="color: rgb(153, 153, 169);">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; */</span>
       <span style="color: rgb(230, 97, 112); font-weight: bold;">while</span> <span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(230, 97, 112); font-weight: bold;">fwscanf</span><span style="color: rgb(210, 205, 134);">(</span>pFile<span style="color: rgb(210, 205, 134);">,</span> <span style="color: rgb(2, 208, 69);">L"</span><span style="color: rgb(0, 196, 196);">%[^|]|</span><span style="color: rgb(0, 121, 151);">%s</span><span style="color: rgb(0, 128, 128);">\n</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">,</span> f1<span style="color: rgb(210, 205, 134);">,</span> f2<span style="color: rgb(210, 205, 134);">)</span> <span style="color: rgb(210, 205, 134);">!</span><span style="color: rgb(210, 205, 134);">=</span> <span style="color: rgb(0, 125, 69);">EOF</span><span style="color: rgb(210, 205, 134);">)</span>
       <span style="color: rgb(176, 96, 176);">{</span>
             <span style="color: rgb(230, 97, 112); font-weight: bold;">wprintf</span><span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">L"</span><span style="color: rgb(0, 196, 196);">I have read f1 as : </span><span style="color: rgb(0, 121, 151);">%s</span><span style="color: rgb(0, 196, 196);">  </span><span style="color: rgb(0, 128, 128);">\n</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">,</span> f1<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
             <span style="color: rgb(230, 97, 112); font-weight: bold;">wprintf</span><span style="color: rgb(210, 205, 134);">(</span><span style="color: rgb(2, 208, 69);">L"</span><span style="color: rgb(0, 196, 196);">I have read f2 as : </span><span style="color: rgb(0, 121, 151);">%s</span><span style="color: rgb(0, 196, 196);">  </span><span style="color: rgb(0, 128, 128);">\n</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(210, 205, 134);">,</span> f2<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>

       <span style="color: rgb(176, 96, 176);">}</span>
       <span style="color: rgb(230, 97, 112); font-weight: bold;">fclose</span><span style="color: rgb(210, 205, 134);">(</span>pFile<span style="color: rgb(210, 205, 134);">)</span><span style="color: rgb(176, 96, 176);">;</span>
<span style="color: rgb(176, 96, 176);">}</span>
</pre>
<p>This gives garbage values in the placeholders, like shown in below pic</p>
<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c90e96e3970b-pi"><img width="244" height="127" title="garbageValues" style="margin: 0px; display: inline; background-image: none;" alt="garbageValues" src="/assets/image_977225.jpg" border="0"></a>
<p>The root cause of this problem lies in the preprocess macro define <blockquote>
_CRT_STDIO_ISO_WIDE_SPECIFIERS
=1</blockquote> in ObjectARXSDK 2018\inc\rxsdk_common.props
<p>To tackle issue with Visual Studio 2015, AutoCAD made a workaround, it is not a case anymore unfortunately this define lies in SDK and causing some other issues like I stated above.</p>
<p><font style="background-color: rgb(255, 255, 0);"> You can remove this define from your SDK to avoid unnecessary issues while dealing with strings.</font></p>

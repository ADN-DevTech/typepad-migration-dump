---
layout: "post"
title: "Get Profile settings"
date: "2012-07-22 02:00:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/get-profile-settings.html "
typepad_basename: "get-profile-settings"
typepad_status: "Publish"
---

<p>I&#39;d like to find out what the Current Profile is and get its settings, e.g. the main CUIx file it is using.</p>
<p><strong>Solution</strong></p>
<p>The Current Profile&#39;s name can be retrieved from CPROFILE variable and the profile settings can be accessed through the Registry.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Command1(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get current profile&#39;s name</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf resBuf;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acedGetVar(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;CPROFILE&quot;</span><span style="line-height: 140%;">), &amp;resBuf);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ACHAR profile[MAX_PATH];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; _tcscpy(profile, resBuf.resval.rstring); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; free(resBuf.resval.rstring); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get the profile&#39;s CUIx file from the registry </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR * regRoot = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; acdbHostApplicationServices()-&gt;getRegistryProductRootKey();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ACHAR keyPath[MAX_PATH];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; _stprintf(keyPath, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;%s\\Profiles\\%s\\General Configuration&quot;</span><span style="line-height: 140%;">), regRoot, profile);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; HKEY key;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; DWORD ret = RegOpenKeyEx(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; HKEY_CURRENT_USER, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; keyPath, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; KEY_READ, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &amp;key</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ret != ERROR_SUCCESS || key == NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ACHAR result[MAX_PATH];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; DWORD length = MAX_PATH;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; DWORD keyType;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ret = RegQueryValueEx(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; key, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;MenuFile&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &amp;keyType, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; (LPBYTE)(result), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &amp;length</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; RegCloseKey(key);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ACHAR menuPath[MAX_PATH];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ret = ExpandEnvironmentStrings(result, menuPath, MAX_PATH);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; _tcscat(menuPath, _T(</span><span style="color: #a31515; line-height: 140%;">&quot;.cuix&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Write the result to the Command Line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;The current profile is \&quot;%s\&quot; and its CUIx file is \&quot;%s\&quot;&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; profile,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; menuPath</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>In case of the Current Profile, the MenuFile property is the same as the MENUNAME variable.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Command2(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Get main CUIx file</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf resBuf;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acedGetVar(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;MENUNAME&quot;</span><span style="line-height: 140%;">), &amp;resBuf);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ACHAR cuiPath[MAX_PATH];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; _stprintf(cuiPath, _T(</span><span style="color: #a31515; line-height: 140%;">&quot;%s.cuix&quot;</span><span style="line-height: 140%;">), resBuf.resval.rstring); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; free(resBuf.resval.rstring);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Write the result to the Command Line</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;The CUIx file used by the current profile is \&quot;%s\&quot;&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; cuiPath</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>

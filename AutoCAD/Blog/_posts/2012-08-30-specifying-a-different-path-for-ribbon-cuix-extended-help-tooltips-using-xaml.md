---
layout: "post"
title: "Specifying a different path for Ribbon CUIX Extended Help Tooltips using XAML"
date: "2012-08-30 14:48:26"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "UI"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/specifying-a-different-path-for-ribbon-cuix-extended-help-tooltips-using-xaml.html "
typepad_basename: "specifying-a-different-path-for-ribbon-cuix-extended-help-tooltips-using-xaml"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>It’s possible to supply your own XAML Tooltips by supplying a loose WPF XAML file as the ContentSource </p>  <p>e.g.</p>  <p><strong>&lt;Header&gt;     <br />&#160; &lt;FileReferences&gt;      <br />&#160;&#160;&#160; &lt;ToolTipFileReferences&gt;      <br />&#160;&#160;&#160;&#160;&#160; &lt;ContentSources&gt;      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;ContentSource&gt;toolTips.xaml&lt;/ContentSource&gt;      <br />&#160;&#160;&#160;&#160;&#160; &lt;/ContentSources&gt;      <br />&#160;&#160;&#160; &lt;/ToolTipFileReferences&gt;      <br />&#160; &lt;/FileReferences&gt;      <br />&lt;/Header&gt;</strong></p>  <p>The ContentSource is actually a <a href="http://msdn.microsoft.com/en-us/library/aa970069.aspx">Uri</a> which means you can use the Uri pack format to specify different paths or even XAML files packaged into your own resource DLLs… </p>  <p>e.g. a DLL Resource locator</p>  <p><strong>&lt;Header&gt;     <br />&#160; &lt;FileReferences&gt;      <br />&#160;&#160;&#160; &lt;ToolTipFileReferences&gt;      <br />&#160;&#160;&#160;&#160;&#160; &lt;ContentSources&gt;      <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; &lt;ContentSource&gt;pack://application:,,,/MyApplicationDLL;resources/Tooltip.xaml&lt;/ContentSource&gt;</strong></p>  <p><strong>     <br />&#160;&#160;&#160;&#160;&#160; &lt;/ContentSources&gt;      <br />&#160;&#160;&#160; &lt;/ToolTipFileReferences&gt;      <br />&#160; &lt;/FileReferences&gt;      <br />&lt;/Header&gt;</strong></p>

---
layout: "post"
title: "Extension Methods are Your Friends"
date: "2011-08-18 15:20:29"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/08/extension-methods-are-your-friends.html "
typepad_basename: "extension-methods-are-your-friends"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>That&#39;s it.&#0160; I&#39;ve had it.&#0160; I&#39;m sick of constantly creating arrays with one object in them just so I can call Vault API functions.&#0160; No more, I say!&#0160; Thanks to the <strong>extension method</strong> feature of .NET, I can now create a method on <span style="color: #0000ff;">object</span> to easily do this for me.&#0160;</p>
<p>LINQ already gives me FirstOrDefault that lets me convert an array to a single value.&#0160; Here is a function that does the inverse.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin-bottom: 0pt; mso-layout-grid-align: none;"><span style="color: #0000ff; font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; color: blue; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">internal</span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"> <span style="color: #0000ff;"><span style="color: blue;">static</span></span> <span style="color: #0000ff;"><span style="color: blue;">class</span></span> <span style="color: #2b91af;"><span style="color: #2b91af;">ExtensionMethods                    <br /></span></span></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">{                <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span><span style="color: #0000ff;"><span style="color: blue;">internal</span></span> <span style="color: #0000ff;"><span style="color: blue;">static</span></span> T[] ToSingleArray&lt;T&gt;(<span style="color: #0000ff;"><span style="color: blue;">this</span></span> T obj)                 <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span>{                 <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: #0000ff;"><span style="color: blue;">return</span></span> <span style="color: #0000ff;"><span style="color: blue;">new</span></span> T[] { obj };                 <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span>}                 <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">} </span></span></p>
</td>
</tr>
</tbody>
</table>
<p>Here is a comparison between the default way and the extension method way:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin-bottom: 0pt; mso-layout-grid-align: none;"><span style="color: #008000; font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; color: green; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">// instead of this                <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">docSvc.FindLatestFilesByPaths(<span style="color: #0000ff;"><span style="color: blue;">new</span></span> <span style="color: #0000ff;"><span style="color: blue;">string</span></span>[] { <span style="color: #a31515;"><span style="color: #a31515;">&quot;$/drawing.dwg&quot;</span></span> });                 <br /> <br /></span></span><span style="color: #008000; font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; color: green; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">// I can use the extension method                <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">docSvc.FindLatestFilesByPaths(<span style="color: #a31515;"><span style="color: #a31515;">&quot;$/drawing.dwg&quot;</span></span>.ToSingleArray()); </span></span></p>
</td>
</tr>
</tbody>
</table>
<p>Looking at the code, it doesn&#39;t look like one is better than the other.&#0160; But when you are <em>typing</em> the code out, the extension method makes things much easier.&#0160; You don&#39;t have to fiddle with []{} characters and intellesense will type most of the function name for you.</p>
<p>This is just a basic example of a powerful feature.&#0160; There are tons of uses and misuses for extension methods, so make sure to read up on it in MSDN.&#0160;</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>Here is another method you might find useful when Vault passes back an array and you want to see if there is any data in it.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin-bottom: 0pt; mso-layout-grid-align: none;"><span style="color: #0000ff; font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; color: blue; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">internal</span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"> <span style="color: #0000ff;"><span style="color: blue;">static</span></span> <span style="color: #0000ff;"><span style="color: blue;">bool</span></span> IsNullOrEmpty&lt;T&gt;(<span style="color: #0000ff;"><span style="color: blue;">this</span></span> <span style="color: #2b91af;"><span style="color: #2b91af;">IEnumerable</span></span>&lt;T&gt; collection)                 <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">{                <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;"><span style="mso-spacerun: yes;">&#0160;&#0160;&#0160; </span><span style="color: #0000ff;"><span style="color: blue;">return</span></span> collection == <span style="color: #0000ff;"><span style="color: blue;">null</span></span> || collection.Count() == 0;                 <br /></span></span><span style="font-family: Consolas; font-size: x-small;"><span style="font-family: consolas; font-size: 9.5pt; mso-bidi-font-family: consolas; mso-bidi-language: ar-sa;">} </span></span></p>
</td>
</tr>
</tbody>
</table>
<p>If you have any good ones that are helpful when programming Vault, post them in the comments section.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>

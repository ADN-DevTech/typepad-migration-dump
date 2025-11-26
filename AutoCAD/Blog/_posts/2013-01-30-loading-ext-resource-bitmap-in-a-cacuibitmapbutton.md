---
layout: "post"
title: "Loading ext. resource bitmap in a CAcUiBitmapButton"
date: "2013-01-30 05:48:09"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/loading-ext-resource-bitmap-in-a-cacuibitmapbutton.html "
typepad_basename: "loading-ext-resource-bitmap-in-a-cacuibitmapbutton"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p><b>Issue     <br /></b>I am trying to create a group of buttons that will display bitmaps from an external source,but not from the resource file stored in the ARX commandWith the CAdUiBitmapButton and CAdUiToolButton button classes . How can I do this?</p>  <p><a name="section2"></a></p>  <p><b>Solution     <br /></b>There are two ways to do this.</p>  <p>Method #1   <br />Place the BITMAP string identifier as the button text, and switch the resource handle before calling the AutoLoad() method.</p>  <p>Method #2   <br />Create your own class derived from CAcUiBitmapButton, and implement a method like this:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 9pt">   <p style="margin: 0px">&#160;</p>    <p style="margin: 0px"><span style="line-height: 140%">BOOL CMyButton::LoadBitmapResource(LPCTSTR strResId, HINSTANCE hInst) </span></p>    <p style="margin: 0px"><span style="line-height: 140%">{</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; m_bmpResId = strResId;</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; CAcUiBitmapButton::LoadBitmapResource (strResId, m_bmp, hInst);</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; CalcBitmapSize();</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">if</span><span style="line-height: 140%">(GetAutoSizeToBitmap())</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160; SizeToBitmap();</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; CAdUiOwnerDrawButton::OnAutoLoad();</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%">(TRUE);</span></p>    <p style="margin: 0px"><span style="line-height: 140%">}</span></p> </div>  <p>Outside your derived class, you need to call LoadBitmapResource() to initialize your button with the correct HINSTANCE resource DLL that contains your bitmap resource.</p>

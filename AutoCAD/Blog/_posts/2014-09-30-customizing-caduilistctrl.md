---
layout: "post"
title: "Customizing CAdUiListCtrl"
date: "2014-09-30 22:42:46"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/09/customizing-caduilistctrl.html "
typepad_basename: "customizing-caduilistctrl"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you are using CAdUiListCtrl and wish to customize it more, you can do it by using custom draw. This is same as customizing any other MFC controls and a nice article on customizing a CListCtrl is here :</p>
<p></p>
<a href="http://www.codeproject.com/Articles/79/Neat-Stuff-to-Do-in-List-Controls-Using-Custom-Dra">Neat Stuff to Do in List Controls Using Custom Draw</a>
<p>Following the custom drawing on list control derived from CAdUiListCtrl, here is a code snippet for displaying color values.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// MyListCtrl.h</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#pragma</span><span style="color:#000000">  <span style="color:#0000ff">once</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">class</span><span style="color:#000000">  MyListCtrl : <span style="color:#0000ff">public</span><span style="color:#000000">  CAdUiListCtrl </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	DECLARE_DYNAMIC(MyListCtrl)</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Helper method</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	CString ColorNameFromIndex(<span style="color:#0000ff">int</span><span style="color:#000000">  colorIndex);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 	MyListCtrl();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  ~MyListCtrl();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">protected</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 	DECLARE_MESSAGE_MAP()</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Custom draw</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	afx_msg <span style="color:#0000ff">void</span><span style="color:#000000">  OnCustomDraw </pre>
<pre style="margin:0em;"> 		( NMHDR* pNMHDR, LRESULT* pResult );</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// MyListCtrl.cpp : implementation file</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;stdafx.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;MyListCtrl.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> IMPLEMENT_DYNAMIC(MyListCtrl, CAdUiListCtrl)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> MyListCtrl::MyListCtrl()<span style="color:#000000">{</span><span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> MyListCtrl::~MyListCtrl()<span style="color:#000000">{</span><span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> BEGIN_MESSAGE_MAP(MyListCtrl, CAdUiListCtrl)</pre>
<pre style="margin:0em;"> 	ON_NOTIFY_REFLECT ( NM_CUSTOMDRAW, OnCustomDraw)</pre>
<pre style="margin:0em;"> END_MESSAGE_MAP()</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Custom draw </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> afx_msg <span style="color:#0000ff">void</span><span style="color:#000000">  MyListCtrl::</pre>
<pre style="margin:0em;"> 	OnCustomDraw ( NMHDR* pNMHDR, LRESULT* pResult)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	NMLVCUSTOMDRAW* pLVCD </pre>
<pre style="margin:0em;"> 		= <span style="color:#0000ff">reinterpret_cast</span><span style="color:#000000"> &lt;NMLVCUSTOMDRAW*&gt;(pNMHDR);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Take the default processing unless we </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// set this to something else below.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     *pResult = CDRF_DODEFAULT;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// First thing - check the draw stage. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// If&#39;s the&#39;s prepaint</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// stage, then tell Windows we want messages </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// for every item.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  ( CDDS_PREPAINT </pre>
<pre style="margin:0em;"> 		== pLVCD-&gt;nmcd.dwDrawStage )</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         *pResult = CDRF_NOTIFYITEMDRAW;</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  ( CDDS_ITEMPREPAINT </pre>
<pre style="margin:0em;"> 		== pLVCD-&gt;nmcd.dwDrawStage )</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		*pResult = CDRF_NOTIFYPOSTPAINT;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  ( CDDS_ITEMPOSTPAINT </pre>
<pre style="margin:0em;"> 		== pLVCD-&gt;nmcd.dwDrawStage )</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		LVITEM rItem;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">     nItem = </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">static_cast</span><span style="color:#000000"> &lt;<span style="color:#0000ff">int</span><span style="color:#000000"> &gt;( pLVCD-&gt;nmcd.dwItemSpec);</pre>
<pre style="margin:0em;"> 		CDC*  pDC = CDC::FromHandle ( pLVCD-&gt;nmcd.hdc);</pre>
<pre style="margin:0em;"> 		CRect rcIcon;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// RGB color treating item row as a color index</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">long</span><span style="color:#000000">  acirgb, r,g,b;</pre>
<pre style="margin:0em;">         acirgb = acedGetRGB ( nItem );</pre>
<pre style="margin:0em;">         r = ( acirgb &amp; 0xffL );</pre>
<pre style="margin:0em;">         g = ( acirgb &amp; 0xff00L ) &gt;&gt; 8;</pre>
<pre style="margin:0em;">         b = acirgb &gt;&gt; 16; </pre>
<pre style="margin:0em;"> 		CBrush brush(RGB(r,g,b));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		CRect rcItem;</pre>
<pre style="margin:0em;"> 		GetSubItemRect(nItem, 1, rcItem);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">  w = rcItem.Width();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">  h = rcItem.Height();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		CRect clrBox((<span style="color:#0000ff">int</span><span style="color:#000000"> )rcItem.left + 0.1 * h, </pre>
<pre style="margin:0em;"> 					 (<span style="color:#0000ff">int</span><span style="color:#000000"> )rcItem.top + 0.1 * h, </pre>
<pre style="margin:0em;"> 					 (<span style="color:#0000ff">int</span><span style="color:#000000"> )rcItem.left + 0.9 * h, </pre>
<pre style="margin:0em;"> 					 (<span style="color:#0000ff">int</span><span style="color:#000000"> )rcItem.top + 0.9 * h);</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		CBrush* pOldBrush = pDC-&gt;SelectObject(&amp;brush);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// create and select a thick, black pen</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		CPen penBlack;</pre>
<pre style="margin:0em;"> 		penBlack.CreatePen(PS_SOLID, 1, RGB(0, 0, 0));</pre>
<pre style="margin:0em;"> 		CPen* pOldPen = pDC-&gt;SelectObject(&amp;penBlack);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pDC-&gt;Rectangle(clrBox);</pre>
<pre style="margin:0em;"> 		pDC-&gt;TextOutW(clrBox.right + 0.1*h, </pre>
<pre style="margin:0em;"> 					(<span style="color:#0000ff">int</span><span style="color:#000000"> )rcItem.top + 0.1 * h, </pre>
<pre style="margin:0em;"> 					ColorNameFromIndex(nItem));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// put back the old objects</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		pDC-&gt;SelectObject(pOldBrush);</pre>
<pre style="margin:0em;"> 		pDC-&gt;SelectObject(pOldPen);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		*pResult = CDRF_DODEFAULT;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Helper method</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> CString MyListCtrl::ColorNameFromIndex</pre>
<pre style="margin:0em;"> 							(<span style="color:#0000ff">int</span><span style="color:#000000">  colorIndex)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">switch</span><span style="color:#000000"> (colorIndex)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  0:</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  _T(<span style="color:#a31515">&quot;Black&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  1:</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  _T(<span style="color:#a31515">&quot;Red&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  2:</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  _T(<span style="color:#a31515">&quot;Yellow&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  3:</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  _T(<span style="color:#a31515">&quot;Green&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">case</span><span style="color:#000000">  4:</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  _T(<span style="color:#a31515">&quot;Cyan&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">default</span><span style="color:#000000">  :</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			CString str;</pre>
<pre style="margin:0em;"> 			str.Format(_T(<span style="color:#a31515">&quot;%d&quot;</span><span style="color:#000000"> ), colorIndex);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  str;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>To use the List control in a dialog, insert an MFC List control and add a member variable for it. Replace the CListCtrl with the custom List control class. Here is a code snippet :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// SampleDlg.h</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Member variable for the List control</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> MyListCtrl mMyList;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// SampleDlg.cpp</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> BOOL SampleDlg::OnInitDialog()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	CDialog::OnInitDialog();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Insert two columns</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	mMyList.InsertColumn(</pre>
<pre style="margin:0em;"> 		0, </pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;Layer Name&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		LVCFMT_LEFT, 90);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	mMyList.InsertColumn(</pre>
<pre style="margin:0em;"> 		1, </pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;Color&quot;</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		, LVCFMT_LEFT, 90);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Contents of column-1 will be </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// customized at runtime</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// so provided as empty</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  nIndex = </pre>
<pre style="margin:0em;"> 		mMyList.InsertItem(0, _T(<span style="color:#a31515">&quot;Layer 0&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	mMyList.SetItemText(nIndex, 1, _T(<span style="color:#a31515">&quot; &quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	nIndex </pre>
<pre style="margin:0em;"> 		= mMyList.InsertItem(1, _T(<span style="color:#a31515">&quot;Layer 1&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	mMyList.SetItemText(nIndex, 1, _T(<span style="color:#a31515">&quot; &quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	nIndex </pre>
<pre style="margin:0em;"> 		= mMyList.InsertItem(2, _T(<span style="color:#a31515">&quot;Layer 2&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	mMyList.SetItemText(nIndex, 1, _T(<span style="color:#a31515">&quot; &quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	nIndex </pre>
<pre style="margin:0em;"> 		= mMyList.InsertItem(3, _T(<span style="color:#a31515">&quot;Layer 3&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	mMyList.SetItemText(nIndex, 1, _T(<span style="color:#a31515">&quot; &quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	nIndex</pre>
<pre style="margin:0em;"> 		= mMyList.InsertItem(4, _T(<span style="color:#a31515">&quot;Layer 4&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	mMyList.SetItemText(nIndex, 1, _T(<span style="color:#a31515">&quot; &quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  TRUE;  </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is the customized list control displaying color values</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d074c645970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d074c645970c img-responsive" alt="Custom ListControl" title="Custom ListControl" src="/assets/image_172564.jpg" style="margin: 0px 5px 5px 0px;" /></a>

---
layout: "post"
title: "Combobox/mouse over/disappear and text size in docked bar"
date: "2012-12-31 01:36:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/comboboxmouse-overdisappear-and-text-size-in-docked-bar.html "
typepad_basename: "comboboxmouse-overdisappear-and-text-size-in-docked-bar"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>After successfully following the the CAcUiDockControlBar sample, I place a combo box or a CAcUiXXXComboBox at the bottom portion the dockbar. If I expand the combobox so the items are expanded, I can select an entry, but when I move the mouse cursor over the expanded portion, it disappears. The text size in the color combobox is larger than the tree control in the docked bar.</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>If you use CAcUiXXXComboBox(s), you&#39;ll need to make sure the following combobox styles are set:</p>
<p>- Type: Drop List   <br />- Owner draw: Fixed    <br />- Enable the &quot;Has strings&quot; option, and clear &quot;Sort&quot; and enable&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;Vertical scroll&quot;.</p>
<p>When the expanded portion disappears, it is by design. When you move the mouse cursor outside a docked bar, the cursor needs to be changed to be an Acad cursor type. Therefore, if you have a combo box that expands over any Acad&#39;s properties, Acad will get its cursor back. At the time when CAcUiDockControlBar is created, this issue existed. </p>
<p>There is a undocumented function CanFrameworkTakeFocus() of the CAcUiDockControlBar (comes with the SDK), which controls the behavior. To change it, override it and return false.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> CSampDialogBar : </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> CAcUiDockControlBar</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//class CSampDialogBar : public CAcUiDialogBar</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; DECLARE_DYNAMIC(CSampDialogBar);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Construction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CSampDialogBar();&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// standard constructor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CTreeCtrl&#0160;&#0160;&#0160; m_tree;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CComboBox&#0160;&#0160; m_MyCombo;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CAcUiColorComboBox&#0160;&#0160; m_combo;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> PaintControlBar(CDC* pDC);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Overrides</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// ClassWizard generated virtual function overrides</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_VIRTUAL(CSampDialogBar)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> BOOL Create(CWnd*pParent, LPCTSTR&#0160; lpszTitle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_VIRTUAL</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// this override is to make the any control</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// that extends beyond the dockbar to be</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// selectable. Otherwise, the default</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// is to restore Acad cursor so the portion</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// is repainted immediately.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; <span style="color: #ff0000; font-size: medium;"> </span></span><span style="color: #ff0000;"><span style="font-size: medium;"><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> CanFrameworkTakeFocus() { </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;}</span></span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Implementation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Generated message map functions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_MSG(CSampDialogBar)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; afx_msg </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> OnCreate (LPCREATESTRUCT lpCreateStruct);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_MSG</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; DECLARE_MESSAGE_MAP()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> SizeChanged (CRect *lpRect, BOOL bFloating, </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> flags);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
</div>
<p>Now you should be able to select the expanded items out of the combobox. </p>
<p>3. In regards to combobox&#39;s text size, it is too big because it will be whatever default is out there since itself doesn&#39;t have a font size set. To overcome it, just get a font you want from something and set to it, for example, in your OnCreate().</p>
<p>&#0160;</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CSampDialogBar::OnCreate (LPCREATESTRUCT lpCreateStruct) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span>&#0160;<span style="line-height: 140%; color: green;">// …. other codes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// this will make the font size the same as in the tree control</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// otherwise it is a bigger one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_MyCombo.SetFont(m_tree.GetFont());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_combo.SetFont(m_tree.GetFont());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Please refer to the attached project.
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3efd4a96970c"><a href="http://adndevblog.typepad.com/files/combobox-mouse-hover-vs2008.zip">Download Combobox-mouse-hover-VS2008</a></span></p>

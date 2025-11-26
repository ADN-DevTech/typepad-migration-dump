---
layout: "post"
title: "How to use CAdUiDockControlBar"
date: "2013-01-31 03:36:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/how-to-use-caduidockcontrolbar.html "
typepad_basename: "how-to-use-caduidockcontrolbar"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>CAdUiDockControlBar provided you to create dock panel. The basic steps are:</p>
<p>- Create a dialog in the normal way. </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee80dc76c970d-pi"><img alt="image" border="0" height="243" src="/assets/image_303651.jpg" style="display: inline; border: 0px;" title="image" width="429" /></a> </p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> CMyChildDialog : </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> CDialog</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Construction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CMyChildDialog(CWnd* pParent = NULL);&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// standard constructor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Dialog Data</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_DATA(CMyChildDialog)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">enum</span><span style="line-height: 140%;"> { IDD = IDD_CHILDDIALOG };</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// NOTE: the ClassWizard will add data members here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_DATA</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//………</span></p>
</div>
}
<p>   <br />- Add a member variable to you DockCtrlBar class which is a pointer to the dialog class. </p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> CMyDockControlBar : </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> CAcUiDockControlBar </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; DECLARE_DYNAMIC(CMyDockControlBar)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; <strong> <span style="color: #ff0000;">CMyChildDialog *m_childDlg;</span></strong></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CMyDockControlBar () ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_VIRTUAL(CMyDockControlBar)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> BOOL Create(CWnd*pParent, LPCTSTR lpszTitle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_VIRTUAL</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//{{AFX_MSG(CMyDockControlBar)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; afx_msg </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> OnCreate (LPCREATESTRUCT lpCreateStruct);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; afx_msg </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OnSysCommand(UINT nID, LPARAM lParam);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; afx_msg </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OnSize(UINT nType, </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> cx, </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> cy);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//}}AFX_MSG</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; DECLARE_MESSAGE_MAP()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> SizeChanged (CRect *lpRect, BOOL bFloating, </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> flags);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} ;</span></p>
</div>
<p>   <br />- In the DockCtrlBar::OnCreate() instantiate the dialog as a child of the DockCtrlBar. </p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">BOOL CMyDockControlBar::Create(CWnd*pParent, LPCTSTR lpszTitle) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CString strWndClass ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; strWndClass =AfxRegisterWndClass (CS_DBLCLKS, LoadCursor (NULL, IDC_ARROW)) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CRect rect (0, 0, 250, 200) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> ( !CAcUiDockControlBar::Create (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; strWndClass,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lpszTitle,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; WS_VISIBLE | WS_CHILD | WS_CLIPCHILDREN,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rect,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pParent, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (FALSE) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; SetToolID (&amp;clsCMyDockControlBar) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//----- TODO: Add your code here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (TRUE) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> CMyDockControlBar::OnCreate (LPCREATESTRUCT lpCreateStruct) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> ( CAcUiDockControlBar::OnCreate (lpCreateStruct) == -1 )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (-1) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// point to our resource</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CAcModuleResourceOverride resourceOverride;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// now create a new dialog with our stuff in it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_childDlg = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> CMyChildDialog;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// create it and set the parent as the dockctrl bar</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_childDlg-&gt;Create (IDD_CHILDDIALOG, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// move the window over so we can see the control lines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_childDlg-&gt;MoveWindow (0, 0, 100, 100, TRUE);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (0) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>   <br />- Disable the dialog&#39;s Ok and Cancel modes by overriding the CMyChildDialog::OnCommand().</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">BOOL CMyChildDialog::OnCommand(WPARAM wParam, LPARAM lParam) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">switch</span><span style="line-height: 140%;"> (wParam)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Enter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> 1:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Esc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">case</span><span style="line-height: 140%;"> 2: </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> CDialog::OnCommand(wParam, lParam);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>   <br />- Check the Dialog properties and make sure Style=Child, Border=None and TitleBar=False.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c366aa5af970b-pi"><img alt="image" border="0" height="285" src="/assets/image_657821.jpg" style="display: inline; border: 0px;" title="image" width="442" /></a> </p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee80dc811970d-pi"><img alt="image" border="0" height="492" src="/assets/image_35957.jpg" style="display: inline; border: 0px;" title="image" width="354" /></a></p>
<p>here is the demo project&#0160;
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d40991e0a970c"><a href="http://adndevblog.typepad.com/files/_arxdockcontrolbar_vs2008.zip">Download _ArxDockControlBar_VS2008</a></span></p>

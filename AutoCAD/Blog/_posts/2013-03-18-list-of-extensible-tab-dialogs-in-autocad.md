---
layout: "post"
title: "List of extensible tab dialogs in AutoCAD"
date: "2013-03-18 00:16:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/list-of-extensible-tab-dialogs-in-autocad.html "
typepad_basename: "list-of-extensible-tab-dialogs-in-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a>&#0160;</p>
<p><strong>Issue</strong>     <br />What are the dialog names that are extensible In AutoCAD 2007 through the TabExtensionManager? </p>
<p><strong>Solution</strong>     <br />There are three tabbed dialogs that are extensible in AutoCAD e..g. </p>
<p>- Options dialog (&quot;OptionsDialog&quot;)    <br />- Drawing Aids dialog (&quot;DrawingSettingsDialog&quot;)     <br />- Mtext editor (&quot;MTextEditor&quot;) </p>
<p>You need to pass the name in () is the string to acedRegisterExtendedTab() function.</p>
<pre>BOOL <strong>acedRegisterExtendedTab</strong>(
    LPCTSTR <strong>szAppName</strong>, 
    LPCTSTR <strong>szDialogName</strong>
);</pre>
<p>This function lets AutoCAD know that the calling application, identified by szAppName, wants to add tabs to the dialog whose published name is szDialogName. </p>
<p>The calling application should call this function from the AcRx::kInitAppMsg case of its acrxEntryPoint() function. </p>
<p>in the previous ObjectARX SDK, there is a sample called ExtendTabs. In current SDKs, it is removed. Here is the updated version of the sample. 
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c35ca8ab0970b"><a href="http://adndevblog.typepad.com/files/extendtabs-vs2010-acad2013.zip">Download ExtendTabs-VS2010-Acad2013<br /></a></span>The core code is as below:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">&#0160;</span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">CTab1 gTab1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">CTab1 gTab2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> addMyTabs(CAdUiTabExtensionManager* pXtabManager)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// Add the tabs here.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// When resource from this ARX app is needed, just</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// instantiate a local CAcModuleResourceOverride</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CAcModuleResourceOverride resOverride;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Add our tabs to the &#39;OPTIONS&#39; dialog</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; BOOL flag = pXtabManager-&gt;AddTab(_hdllInstance, IDD_TAB1,_T(</span><span style="line-height: 140%; color: #a31515;">&quot;My Tab1&quot;</span><span style="line-height: 140%;">), &amp;gTab1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab1.StretchControlXY(IDC_EDIT1, 50, 50);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab1.StretchControlXY(IDC_EDIT2, 50, 50);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab1.MoveControlX(IDC_EDIT2, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab1.MoveControlX(IDC_STATIC2, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; flag =&#0160; pXtabManager-&gt;AddTab(_hdllInstance, IDD_TAB2,_T(</span><span style="line-height: 140%; color: #a31515;">&quot;My Tab2&quot;</span><span style="line-height: 140%;">), &amp;gTab2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.StretchControlXY(IDC_LIST1, 50, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlX(IDC_RADIO1, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlX(IDC_RADIO2, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlX(IDC_RADIO3, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlX(IDC_STATIC1, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.StretchControlXY(IDC_LIST2, 50, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlX(IDC_LIST2, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlY(IDC_STATIC3, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlY(IDC_EDIT1, 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; gTab2.MoveControlY(IDC_BUTTON, 100);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//----- ObjectARX EntryPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> CExtendTabsApp : </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> AcRxArxApp {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CExtendTabsApp () : AcRxArxApp () {}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// need to override On_kInitDialogMsg which initializes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// dialogs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> AcRx::AppRetCode On_kInitDialogMsg(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *pkt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// You *must* call On_kInitDialogMsg here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcRx::AppRetCode retCode 
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; =AcRxArxApp::On_kInitDialogMsg (pkt) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// add my tabs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addMyTabs((CAdUiTabExtensionManager*)pkt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (retCode) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> AcRx::AppRetCode 
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; On_kInitAppMsg (</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *pkt)
      <br />&#0160;&#0160; {</span>&#0160; </p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// You *must* call On_kInitAppMsg here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcRx::AppRetCode retCode 
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; =AcRxArxApp::On_kInitAppMsg (pkt) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TODO: Add your initialization code here&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Express interest in adding tabs to the OPTIONS dialog.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The name of the OPTIONS dialog in AutoCAD is &quot;OptionsDialog&quot;. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acedRegisterExtendedTab(
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;EXTENDTABS.ARX&quot;</span><span style="line-height: 140%;">), 
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;OptionsDialog&quot;</span><span style="line-height: 140%;">)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acutPrintf(
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nCommand is \&quot;ExtendTabs\&quot;.&quot;</span><span style="line-height: 140%;">));</span> <span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (retCode) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> AcRx::AppRetCode 
      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; On_kUnloadAppMsg (</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> *pkt) {</span> 
    <br /><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// You *must* call On_kUnloadAppMsg here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcRx::AppRetCode retCode =AcRxArxApp::On_kUnloadAppMsg (pkt) ;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TODO: Unload dependencies here</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (retCode) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> RegisterServerComponents () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> MyGroupMyCommand () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Put your command code here</span></p>
<p style="margin: 0px;">&#0160; <span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span>&#0160; </p>
<p style="margin: 0px;"><span style="line-height: 140%;">} ;</span></p>
</div>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3ff96b45970c-pi"><img alt="image" border="0" height="344" src="/assets/image_520704.jpg" style="display: inline; border: 0px;" title="image" width="470" /></a></p>

---
layout: "post"
title: "Setting plot configuration name And Media Names using ObjectARX"
date: "2012-06-11 01:33:28"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/setting-plot-configuration-name-and-media-names-using-objectarx.html "
typepad_basename: "setting-plot-configuration-name-and-media-names-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>To query the all the available plot configurations you should use plotDeviceList() method of AcDbPlotSettingsValidator class and to get the list of available media names for a given plot configuration, use canonicalMediaNameList() method AcDbPlotSettingsValidator class.</p>
<p>The sample code below lists the available plot configurations and asks the user to select one. After user enters his choice, all the available media are listed and user can select one to set it current.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> fSetPlotMedia()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcApLayoutManager *pLayMan = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pLayMan = (AcApLayoutManager *) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acdbHostApplicationServices()-&gt;layoutManager();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//get the active layout</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbLayout *pLayout = pLayMan-&gt;findLayoutNamed(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pLayMan-&gt;findActiveLayout(TRUE),TRUE);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//get the PlotSettingsValidator</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbPlotSettingsValidator *pPSV =NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPSV = acdbHostApplicationServices()-&gt;plotSettingsValidator();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//refresh the Plot Config list</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPSV-&gt;refreshLists(pLayout);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//get all the Plot Configurations</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcArray&lt;</span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR *&gt; mDeviceList;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPSV-&gt;plotDeviceList(mDeviceList);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPlot Configuration List :&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nLength = mDeviceList.length();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nCtr = 0;nCtr&lt;nLength ;nCtr++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\n %i) - %s&quot;</span><span style="line-height: 140%;">),(nCtr + 1), mDeviceList.at(nCtr));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//get the user input for listing the Media Names</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nSel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> mRes =&nbsp; RTNONE;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;">(RTNORM != mRes)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acedInitGet((RSG_NONULL + RSG_NONEG + RSG_NOZERO),NULL);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mRes = acedGetInt(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect the Plot Configuration number: &quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;nSel);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (nSel &gt; nLength) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nEnter a number between 1 to %i&quot;</span><span style="line-height: 140%;">),nLength);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; mRes = RTNONE;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//select the selected Plot configuration</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPSV-&gt;setPlotCfgName(pLayout,mDeviceList.at(--nSel));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//list all the paper sizes in the given Plot configuration</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcArray&lt;</span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR *&gt; mMediaList;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> ACHAR *pLocaleName;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPSV-&gt;canonicalMediaNameList(pLayout,mMediaList);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nMedia list for Plot Configuration - %s:&quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; mDeviceList.at(nSel));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;nLength = mMediaList.length();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nCtr ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(nCtr = 0;nCtr&lt;nLength ;nCtr++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">//get the localename</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; pPSV-&gt;getLocaleMediaName(pLayout,mMediaList.at(nCtr),pLocaleName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\n %i)\n&nbsp;&nbsp; Name:&nbsp; %s \n&nbsp;&nbsp; Locale Name: %s &quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (nCtr + 1),mMediaList.at(nCtr),pLocaleName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;mRes =&nbsp; RTNONE;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;">(RTNORM != mRes)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acedInitGet((RSG_NONULL + RSG_NONEG + RSG_NOZERO),NULL);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mRes = acedGetInt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect the Media by entering the number: &quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;nSel);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (nSel &gt; nLength) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nEnter a number between 1 to %i&quot;</span><span style="line-height: 140%;">),nLength);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; mRes = RTNONE;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//set selected Media for the layout</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPSV-&gt;setCanonicalMediaName(pLayout,mMediaList.at(--nSel));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pLayout-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>

---
layout: "post"
title: "Navisworks ActiveX Controls"
date: "2012-05-17 20:30:14"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/navisworks-activex-controls.html "
typepad_basename: "navisworks-activex-controls"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>ActiveX controls allow Navisworks style navigation of files embedded within hosts such as web pages of end user applications. The aim is to allow users to embed Navisworks capabilities inside Web Pages, applications and other containers. The user can also access and manipulate the controls internal state. The controls can open files using absolute, URL or relative paths.</p>
<p>1. ActiveX control types before 2012 There are three types of ActiveX controls:</p>
<p style="padding-left: 30px;">(1) Built-in: lcodieD.dll - Also called Dynamic control<br />- Installed as part of main product install of Navisworks Manage/Simulate/Review(*) <br />e.g.,\lcodieD.dll - Supports textures.</p>
<p style="padding-left: 30px;">- Can open various non Navisworks CAD files. <br />- All API is fully enabled when main product is licensed.</p>
<p style="padding-left: 30px;">- This module is found in the main product install and is NOT redistributable. The customer is required to have the product installed.</p>
<p>*Note: Navisworks Review applies 2010 and earlier releases</p>
<p>(2) Full: lcodieDX.dll - Supports textures.</p>
<p style="padding-left: 30px;">- Supplied as .msm (active_dx.msm).<br />- Supplied as self installing .exe file for web/other usage (NavisworksFullActiveXSetup.exe).<br />- The API is not fully enabled, only very limited access is possible.<br />- This module (*.msm) is found under \api\COM\bin folder (64-bit version in \api\COM\bin\x64), and is allowed to redistribute with the developer&#39;s application.</p>
<p>(3) Lite: lcodieS.dll - No textures support.</p>
<p style="padding-left: 30px;">- Only Navisworks files can be opened. - Supplied as .msm (active_sx.msm).<br />- Supplied as .cab file for web usage (\api\COM\bin\nw_ax_lite.cab).<br />- The API is not fully enabled, only very limited access is possible.<br />- This may be installed as part of &#39;Navigator&#39;. This module (*.msm) is found under \api\COM\bin folder (64-bit version in \api\COM\bin\x64), and is allowed to redistribute with the developer&#39;s application.</p>
<p>2. ActiveX control types from Navisworks 2012</p>
<p>ActiveX controls have been re-organized as follows:</p>
<p style="padding-left: 30px;">(1) Navisworks Integrated API Library <br /> It is also known as ‘Dynamic control’ before 2012. It relies on main product installation, so no 32bit support on 64bit OS. It has two sub-type controls:</p>
<p style="padding-left: 30px;">- &quot;Navisworks Integrated Control 9 SDI&quot; : single instance use only<br />- &quot;Navisworks Integrated Control 9 MDI&quot; : multiple instance support, no plugins or textures</p>
<p>(2) Navisworks Redistributable API Library&#0160;</p>
<p>Also known as the ‘Full Control’ before 2012, this control, by default, is installed with product. And the standalone installer (Navisworks ActiveX Redistributable Setup.exe) is also provided in \api\COM\bin. Similarly, it has two sub-type controls:</p>
<p style="padding-left: 30px;">- &quot;Navisworks Redistributable Control 9 SDI&quot; : single instance use only<br />- &quot;Navisworks Redistributable Control 9 MDI&quot; : multiple instance support, no plugins or textures</p>
<p>SDI means the application can only use one control. MDI means the application can use multi controls.</p>
<p>3. Install</p>
<p>By default, Built-in and Integrated control are installed when you install Navisworks. To install Full or Redistributable control, you need to run the setup exe above.</p>
<p>In general, to work with Lite control, the web page will provide the nw_ax_lite.cab file. When the web page is visited in the first time, the cab file will be deployed and the Lite control will be redistributed.</p>
<p>For Lite, Full or Redistributable, you can also merge the modules file (*.msm) . Thus the controls will be redistributed when your application is being installed.</p>
<p>4. 32bits and 64bits</p>
<p>The built-in or integrated control has 32bits and 64bits. 32bits can only be installed in 32bits OS. 64bits can only be installed on 64bits OS. lite, full, or redistributable control has 32bits or 64bits, too. 64bits can only be installed on 64bits OS. 32bits can be installed in 32bits or 64bits.</p>
<p>4. Documentations and SDK Samples</p>
<p>&#0160;\api\COM contains the documentations and samples.</p>
<p style="padding-left: 30px;">- COM Interface.pdf : chapter 4 introduces ActiveX controls<br />- NavisWorksCOM.chm: help reference including ActiveX controls</p>
<p>Before 2013:</p>
<p style="padding-left: 30px;">- ACTX_01: control in HTML<br />- ACTX_02 ~ 06: control in VB6 application<br />- ACTX_07: control in C++ application</p>
<p>Since 2013:</p>
<p style="padding-left: 30px;">api\COM\examples\ActiveX: samples with control in C#</p>
<p>5. API Not Fully Enabled</p>
<p>By default what the Lite, Full or Redistributable controls can do is open a model, change navigation mode and select pre-defined viewpoints. Most APIs abilities are not enabled.</p>
<p>When you try to access the un-enabled methods/objects, you will receive a failure:</p>
<p style="padding-left: 30px;">&quot;Navisworks Error - API Unlicensed&quot;</p>
<p>For example, the following methods will not be available:</p>
<p style="padding-left: 30px;">- Find: InwOpFind, InwOpFindSpec, InwOpFindCondition, and InwOpSelection objects<br />- Selection and viewing: ZoomInCurViewOnSel, set_SelectionHidden, HiddenItemsResetAll<br />- Coloring objects: OverrideColor, OverrideResetAll</p>
<p>6. Tips for working with control</p>
<p style="padding-left: 30px;">(1) Each type control has its own GUID. The GUID will change in different releases. The GUID of the same type control is same on 32 &amp; 64bits.</p>
<p style="padding-left: 30px;">e.g. both Navisworks Integrated Control 9 SDI of 32 &amp; 64bits are<br />2B72955E-067A-4260-AEF5-44746A775C53.</p>
<p style="padding-left: 30px;">(2) The GUID can be found in the registry if they have been installed. e.g. searching with ‘Integrated Control’, you can get</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167669354d7970b-pi"><img alt="clip_image001[6]" border="0" height="189" src="/assets/image_166806.jpg" style="display: inline; border: 0px;" title="clip_image001[6]" width="658" /></a></p>
<p>&#0160;</p>
<p style="padding-left: 30px;">(3) If you want to find out which control you are using, try &quot;Ctrl + Alt + Shift + E&quot;. It will pop up a dialog with the information about the control you are using.</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb951034970c-pi"><img alt="image" border="0" height="344" src="/assets/image_916315.jpg" style="display: inline; border: 0px;" title="image" width="396" /></a></p>
<p style="padding-left: 30px;">(4) If you are using Visual Studio and create a Windows form application, something you need to be aware:</p>
<p style="padding-left: 30px;">- on 32bits, all the controls can be added from the Tool Box &gt;&gt; Choose Items &gt;&gt; COM tab, if they have been installed.</p>
<p style="padding-left: 30px;">- on 64bits, only 32bits control can be found because the VS designer is still 32bits. It cannot load 64bits control. The suggestion is to create your application on 32bits and copy to 64bits OS. The application will load 64bits on runtime.</p>

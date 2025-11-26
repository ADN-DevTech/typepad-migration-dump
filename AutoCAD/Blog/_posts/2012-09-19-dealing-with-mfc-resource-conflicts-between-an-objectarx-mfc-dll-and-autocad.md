---
layout: "post"
title: "Dealing with MFC Resource conflicts between an ObjectARX MFC DLL and AutoCAD"
date: "2012-09-19 14:58:28"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/dealing-with-mfc-resource-conflicts-between-an-objectarx-mfc-dll-and-autocad.html "
typepad_basename: "dealing-with-mfc-resource-conflicts-between-an-objectarx-mfc-dll-and-autocad"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>The CAcModuleResourceOverride class provides a nice way to switch the MFC resource pointer from AutoCAD’s resources to your own ARX DLL resources. </p>  <p>If you have to use AutoCAD User Interface functions in between your own resources, then you can still utilize the CAcModuleResourceOverride() class by relying on its destructor (which restores the MFC resource pointer back to AutoCAD)</p>  <p>Here’s what I mean…   <br />    <br /><font size="1" face="Consolas">class CMyDialog : public CDialog     <br />{      <br />…      <br />};</font></p>  <p>   <br /><font size="1" face="Consolas">void ftn()     <br />{      <br />&#160; {      <br />&#160;&#160;&#160; // here we need to make sure we are using the dll’s resources</font></p>  <p><font size="1" face="Consolas">&#160;&#160;&#160; CAcModuleResourceOverride resourceOverride;     <br />&#160;&#160;&#160; CMyDialog dlg;      <br />&#160;&#160;&#160; dlg.DoModal();</font></p>  <p><font size="1" face="Consolas">&#160; } // switch back to AutoCAD’s resources</font></p>  <p><font size="1" face="Consolas">&#160; // now safe to use this function     <br />&#160; acedGetFileD(...);</font></p>  <p><font size="1" face="Consolas">&#160; // now back to our dialog     <br />&#160; {      <br />&#160;&#160;&#160; // here we need to make sure we are using the dll’s resources, so use:      <br />&#160;&#160;&#160; CAcModuleResourceOverride resourceOverride;      <br />&#160;&#160;&#160; CMyDialog dlg;      <br />&#160;&#160;&#160; dlg.DoModal();      <br />&#160; }</font></p>  <p><font size="1" face="Consolas">}     <br /></font></p>

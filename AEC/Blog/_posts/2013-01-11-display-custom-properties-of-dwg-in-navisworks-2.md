---
layout: "post"
title: "Display Custom Properties of DWG in Navisworks - 2"
date: "2013-01-11 01:09:00"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/01/display-custom-properties-of-dwg-in-navisworks-2.html "
typepad_basename: "display-custom-properties-of-dwg-in-navisworks-2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>(continued with <a href=" http://adndevblog.typepad.com/aec/2013/01/display-custom-properties-of-dwg-in-navisworks-1.html">Display Custom Properties of DWG in Navisworks – 1</a>)</p>
<p>7. To Create a COM Wrapper for the custom entity, you can either place the COM implementation in an ARX project or in the same ObjectDBX project where your custom entity exists. Here we are going to see how to place the COM wrapper along with the custom entity itself. If you are interested to create a separate ARX, you can follow the same step explained to create a custom entity where instead of selecting a Project type as “ObjecDBX” , select the Application Type as “ObjectARX” (as mentioned in Step1 – Application Type Dialog) and the continue with the following steps.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c35544a42970b-pi"><img alt="image" border="0" height="333" src="/assets/image_697678.jpg" style="display: inline; border-width: 0px;" title="image" width="527" /></a> </p>
<p>8. Input the wrapper name as “NW_DBX_Curve_Wrapper”. In the COM wrapper tab, put the name of your DBX without the .dbx extension as &quot;AdskDemoCurve&quot; in the “DBX Classname” textbox . Select Ok to finish, and build to test for errors. Take a look NW_DBX_Curve_Wrapper.cpp and .h to see what the wizard has added for us.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c35544a6d970b-pi"><img alt="image" border="0" height="455" src="/assets/image_514761.jpg" style="display: inline; border-width: 0px;" title="image" width="535" /></a> </p>
<p>&#0160;</p>
<p>9. Add necessary code for the custom entity    </p>
<p>&#0160;&#0160; 9.1 AsdkDemoCurve.h</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// assign the CLSID of the wrapper to </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//be returned by the subGetClassID </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// method of our custom entity </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> Acad::ErrorStatus subGetClassID(CLSID * pClsid) </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//set the member variable - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Acad::ErrorStatus setPropertyLong(</span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> val) ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//get the member variable - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> getPropertyLong()</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//set the member variable - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Acad::ErrorStatus setPropertyDouble(</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> val) ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//get the member variable - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> getPropertyDouble()</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;">: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// member variable - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> m_propertylong; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//member variable - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> m_propertydouble;</span></p>
</div>
<p>9.2 AsdkDemoCurve.cpp</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// include the COM wrapper </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">#include</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;NW_AutoCAD_OE_Sample_i.c&quot;</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//initialize the member variables </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AsdkDemoCurve::AsdkDemoCurve () : AcDbCurve () </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_propertylong = 2; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_propertydouble = 10; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//draw some custom geometires. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//To similfy the sample, the code draws a </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//fixed circle and text. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::Boolean AsdkDemoCurve::</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; subWorldDraw (AcGiWorldDraw *mode) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertReadEnabled () ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; mode-&gt;geometry().circle(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGePoint3d(m_propertydouble,0,0),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 10,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGeVector3d(0,0,1)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; mode-&gt;geometry().text(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGePoint3d(m_propertydouble,0,0),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGeVector3d(0,0,1),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGeVector3d(1,0,0),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_propertylong,1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;MyCurve&quot;</span><span style="line-height: 140%;">)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (AcDbCurve::subWorldDraw (mode)) ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// return the COM wrapper ID </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus AsdkDemoCurve::subGetClassID(CLSID * pClsid) </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//locate the file &#39; NW_AutoCAD_OE_Sample_i.c &#39;, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// and open it in VS (this //file may not already be</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// included in the VC project).</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Look for and copy </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//the CLSID definition name (eg CLSID_NW_DBX_Curve_Wraper) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertReadEnabled(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; *pClsid=CLSID_NW_DBX_Curve_Wraper; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eOk; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//set the member variable - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus AsdkDemoCurve::setPropertyLong(</span><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> val) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertWriteEnabled(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_propertylong = val; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eOk ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//get the member variable - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> AsdkDemoCurve::getPropertyLong()</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertReadEnabled(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> m_propertylong; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//set the member variable - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus AsdkDemoCurve::setPropertyDouble (</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> val) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertWriteEnabled(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_propertydouble = val; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eOk ; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//get the member variable - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">long</span><span style="line-height: 140%;"> AsdkDemoCurve::getPropertyDouble()</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertReadEnabled(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> m_propertydouble; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>10. Now we provide the interface methods to communicate with those two custom properties of this entity, a long and a double. Switch to Class View, select INW_DBX_Curve_Wrapper and Add Property. Select the property type Long and provide the name “PropertyLong”. Then finish. Repeat the operation to provide a double property.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f833579970c-pi"><img alt="image" border="0" height="496" src="/assets/image_35945.jpg" style="display: inline; border: 0px;" title="image" width="301" /></a> </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c35544ac8970b-pi"><img alt="image" border="0" height="439" src="/assets/image_404124.jpg" style="display: inline; border: 0px;" title="image" width="513" /></a> </p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c35544ae9970b-pi"><img alt="image" border="0" height="447" src="/assets/image_305094.jpg" style="display: inline; border: 0px;" title="image" width="520" /></a> </p>
<p>&#0160;</p>
<p>10.1 NW_DBX_Curve_Wraper.h</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//configure in which category </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//(of AutoCAD) the properties would be displayed. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//&#0160;&#0160;&#0160; In this sample, we put them in Data category. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//IOPMPropertyExtension </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">BEGIN_OPMPROP_MAP() </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//----- Use the OPMPROP_ENTRY/OPMPROP_CAT_ENTRY macros for each of your properties </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; OPMPROP_ENTRY(0, 0x00000001, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; PROPCAT_Data, 0, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">), 0, 1, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_NULL, </span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; OPMPROP_ENTRY(0, 0x00000002, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; PROPCAT_Data, 0, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _T(</span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">), 0, 1, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_NULL, </span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">END_OPMPROP_MAP() </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//IOPMPropertyExtensionImpl </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// It is a pity VS does not generate the properties </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//Set/Get methods codes </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//automatically. We need to write as below: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;">: </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//INW_DBX_Curve_Wraper </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHOD(get_PropertyLong)(</span><span style="line-height: 140%; color: green;">/*[out, retval]*/</span><span style="line-height: 140%;"> LONG *pVal); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHOD(put_PropertyLong)(</span><span style="line-height: 140%; color: green;">/*[in]*/</span><span style="line-height: 140%;"> LONG newVal); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHOD(get_PropertyDouble)(</span><span style="line-height: 140%; color: green;">/*[out, retval]*/</span><span style="line-height: 140%;"> DOUBLE *pVal); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHOD(put_PropertyDouble)(</span><span style="line-height: 140%; color: green;">/*[in]*/</span><span style="line-height: 140%;"> DOUBLE newVal);</span></p>
</div>
<p>&#0160;</p>
<p>10.2 NW_DBX_Curve_Wraper.cpp</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//get property - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHODIMP CNW_DBX_Curve_Wraper::</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; get_PropertyLong(LONG *pVal) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Acad::ErrorStatus es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcAxObjectRefPtr&lt;AsdkDemoCurve&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pLine(&amp;m_objRef,AcDb::kForRead,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Adesk::kTrue); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">((es=pLine.openStatus()) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; *pVal = pLine-&gt;getPropertyLong(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> Acad::ErrorStatus) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Failed to open object.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,E_FAIL); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> HRESULT hr) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Invalid argument.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,hr); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> S_OK; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//set property - long </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHODIMP CNW_DBX_Curve_Wraper::</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; put_PropertyLong(LONG newVal) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Acad::ErrorStatus es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcAxObjectRefPtr&lt;AsdkDemoCurve&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pLine(&amp;m_objRef,AcDb::kForWrite,Adesk::kTrue); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">((es=pLine.openStatus()) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pLine-&gt;setPropertyLong(newVal); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> Acad::ErrorStatus) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Failed to set PropertyLong.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,E_FAIL); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> HRESULT hr) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Invalid argument.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,hr); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> S_OK; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//get property - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHODIMP CNW_DBX_Curve_Wraper::</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; get_PropertyDouble(DOUBLE *pVal) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Acad::ErrorStatus es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcAxObjectRefPtr&lt;AsdkDemoCurve&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pLine(&amp;m_objRef,AcDb::kForRead,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Adesk::kTrue); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">((es=pLine.openStatus()) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; *pVal = pLine-&gt;getPropertyDouble(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> Acad::ErrorStatus) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Failed to open object.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,E_FAIL); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> HRESULT hr) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Invalid argument.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,hr); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> S_OK; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">//set property - double </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHODIMP CNW_DBX_Curve_Wraper::</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; put_PropertyDouble(DOUBLE newVal) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Acad::ErrorStatus es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcAxObjectRefPtr&lt;AsdkDemoCurve&gt; pLine(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;m_objRef,AcDb::kForWrite,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Adesk::kTrue); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">((es=pLine.openStatus()) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> es; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pLine-&gt;setPropertyDouble(newVal); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> Acad::ErrorStatus) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Failed to set PropertyLong.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,E_FAIL); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> HRESULT hr) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Error(L</span><span style="line-height: 140%; color: #a31515;">&quot;Invalid argument.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IID_INW_DBX_Curve_Wraper,hr); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; } </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> S_OK; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>11. Now build the DBX to see if there is error.   <br />12. Run AutoCAD, and load our DBX. This will register the COM server automatically.    <br />13. Optional: We should now verify that the ObjectDBX registry entries have been made properly. Run RegEdit.exe to access the Windows registry, and navigate to HKEY_LOCAL_MACHINE&gt;SOFTWARE&gt;Autodesk&gt;ObjectDBX&gt;R18.1&gt;ActiveXCLSID. Look for a key called AsdkDemoCurve. The CLSID string value within it should be the same as the CLSID found in the IDL file for your custom wrapper, under INW_DBX_Curve_Wraper. Example:</p>
<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f8335f3970c-pi"><img alt="image" border="0" height="279" src="/assets/image_52056.jpg" style="display: inline; border: 0px;" title="image" width="588" /></a>
<p>&#0160;</p>
<p>We can now create a simple COM client to create and manipulate our custom line.. This will allow us to use objects defined by this TLB, namely, our custom wrapper. Create a VB.NET project, add the tlb and add the following code:</p>
<p>   <br />&lt;CommandMethod(&quot;addEnt&quot;)&gt; _    <br />&#0160;&#0160; Public Sub addEnt()    <br />&#0160;&#0160;&#0160;&#0160;&#0160; Dim oAcadApp As AcadApplication = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication    <br />&#0160;&#0160;&#0160;&#0160;&#0160; Dim myDemo As NW_DBX_Curve_Wraper    <br />&#0160;&#0160;&#0160;&#0160;&#0160; myDemo = oAcadApp.ModelSpace.AddCustomObject(&quot;AsdkDemoCurve&quot;)    <br />&#0160;&#0160; End Sub</p>
<p>   <br />Load the .NET dll and run command addEnt.</p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f833621970c-pi"><img alt="image" border="0" height="355" src="/assets/image_781108.jpg" style="display: inline; border: 0px;" title="image" width="562" /></a> </p>
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f7a434970d-pi"><img alt="image" border="0" height="419" src="/assets/image_66847.jpg" style="display: inline; border: 0px;" title="image" width="563" /></a></p>

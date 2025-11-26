---
layout: "post"
title: "Display Custom Properties of DWG in Navisworks - 1"
date: "2013-01-07 00:18:00"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/01/display-custom-properties-of-dwg-in-navisworks-1.html "
typepad_basename: "display-custom-properties-of-dwg-in-navisworks-1"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>Navisworks will convert some properties from original format of CAD file such as DWG. But for a custom entity, it requires some additional work to expose the properties. </p>
<p>The basic principle anyone adding properties to AutoCAD entities should follow is that if they can get it to appear in vanilla AutoCAD Property Manager via the usual routes of object enablers (OE), then it should work in Navisworks. In another word, if we want to see the properties in multiple DWG readers such as Navisworks, the corresponding OE exists and the properties are packaged by a COM wrapper.&#0160; For custom entities, it is fine because the OE is designed by yourself. For native AutoCAD entities or any 3rd parties’ OE, we cannot do anything with them. So, although you can use Dynamic Properties to add additional properties to native AutoCAD entity in AutoCAD context, there is no way if you want to see the additional properties in Navisworks.</p>
<p>When the DWG reader launches, it will load the available COM wrapper in the registry:   <br />HKEY_LOCAL_MACHINE&gt;SOFTWARE&gt;Autodesk&gt;ObjectDBX&gt;R**&gt;ActiveXCLSID    <br />    <br />** is the version number. e.g. AutoCAD 2011 will load from the key R18.1.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f74485970d-pi"><img alt="image" border="0" height="322" src="/assets/image_614760.jpg" style="display: inline; border: 0px;" title="image" width="549" /></a> </p>
<p>&#0160;</p>
<p>From the COM wrapper, the DWG reader knows the properties which can be loaded.   <br />Following are the steps to create the custom entity and COM wrapper for the custom entity. The sample is introduced with VS2008 + SP1 and tested with AutoCAD 2011. We also provide a sample project of VS2010 which can work with AutoCAD 2013. The steps are similar to the project of VS 2008 + SP1.</p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3f8304a4970c"><a href="http://adndevblog.typepad.com/files/nw_autocad_oe_sample_vs2008sp1_autocad2011.zip">Download Nw_autocad_oe_sample_VS2008SP1_AutoCAD2011</a></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b017d3f8304a4970c">
<span class="asset  asset-generic at-xid-6a0167607c2431970b017ee6f773ad970d"><a href="http://adndevblog.typepad.com/files/nw_cad_oe_sample-vs2010-cad2013.zip">Download NW_CAD_OE_Sample-VS2010-CAD2013</a></span><br /></span></p>
<p>1. First, create the custom entity project using the ObjectARX Wizard</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f82d711970c-pi"><img alt="image" border="0" height="389" src="/assets/image_851792.jpg" style="display: inline; border: 0px;" title="image" width="560" /></a> </p>
<p>2. Select &#39;ObjectDBX (custom object definition)&#39; as the Project Type. Better select the MFC support.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f74571970d-pi"><img alt="image" border="0" height="483" src="/assets/image_479377.jpg" style="display: inline; border: 0px;" title="image" width="567" /></a> </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3553eca1970b-pi"><img alt="image" border="0" height="484" src="/assets/image_653903.jpg" style="display: inline; border: 0px;" title="image" width="571" /></a> </p>
<p>3. In the COM options, tick COM Server. Make sure &#39;Use ATL&#39; and &#39;Use ATL Extensions for Custom Objects&#39; boxes are checked, and press Finish.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3f82d858970c-pi"><img alt="image" border="0" height="495" src="/assets/image_459104.jpg" style="display: inline; border: 0px;" title="image" width="576" /></a> </p>
<p>4. Add necessary head file folder and library file folder of Object ARX. Build to make sure there are no errors.   <br />5. Use the wizard to create the custom entity. Right click the project „NW_AutoCAD_OE_Sample‟ from the solution explorer and select Add Class button. Then select “ObjectDBX Object Wizard” from the available list.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee6f746fa970d-pi"><img alt="image" border="0" height="561" src="/assets/image_754533.jpg" style="display: inline; border: 0px;" title="image" width="564" /></a> </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3553ee54970b-pi"><img alt="image" border="0" height="271" src="/assets/image_27084.jpg" style="display: inline; border: 0px;" title="image" width="571" /></a> </p>
<p>6. Input the custom entity name and select OK to accept the entries, and return to the ARX ClassWizard.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3553eec5970b-pi"><img alt="image" border="0" height="475" src="/assets/image_414620.jpg" style="display: inline; border: 0px;" title="image" width="560" /></a> </p>
<p>(to be continued) </p>

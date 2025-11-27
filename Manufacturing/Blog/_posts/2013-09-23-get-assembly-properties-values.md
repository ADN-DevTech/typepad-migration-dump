---
layout: "post"
title: "Get AutoCAD Mechanical Assembly Properties values"
date: "2013-09-23 10:30:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/get-assembly-properties-values.html "
typepad_basename: "get-assembly-properties-values"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>It seems that <strong>GetPartAttribute</strong> and <strong>GetPartData</strong> do not retrieve the correct evaluated value for model space that contains the assembly data if it is based on an expression, e.g.:&#0160;<strong>=IF(ISBLANK(PART:NAME),BLOCK:NAME,PART:NAME)</strong></p>
<p><strong>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff8fc8ae970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Assemblyproperties" class="asset  asset-image at-xid-6a0167607c2431970b019aff8fc8ae970b" src="/assets/image_1c5612.jpg" style="width: 450px;" title="Assemblyproperties" /></a><br /></strong><br />The <strong>C++ API</strong>&#39;s <strong>AcmBOMManager</strong> has a function called <strong>getBomOverride</strong> which seems to retrieve in its <strong>currentValue</strong> parameter the correct value. Unfortunately, this does not seem to have an exact equivalent in the <strong>COM API</strong>, where you could pass in the model space object.&#0160;</p>
<p>If you have a <strong>.NET AddIn</strong> then you could access the <strong>C++</strong>&#0160;function from it like done here:&#0160;<a href="http://adndevblog.typepad.com/aec/2013/09/call-c-api-functions-from-net-addin.html" target="_self">http://adndevblog.typepad.com/aec/2013/09/call-c-api-functions-from-net-addin.html</a></p>
<p>So we need to create an <strong>ARX</strong> project with <strong>.NET</strong> and <strong>MFC</strong> support - you can use the <strong><a href="http://usa.autodesk.com/adsk/servlet/index?id=1911627&amp;siteID=123112" target="_self">ARX Wizard</a></strong> for that. Make sure all the <strong>include</strong> and <strong>library</strong> folders of the <strong><a href="http://usa.autodesk.com/adsk/servlet/index?id=14952981&amp;siteID=123112" target="_self">AutoCAD Mechanical SDK</a></strong> are set in the project and then can add the following in the project, e.g. in&#0160;<strong>acrxEntryPoint.cpp</strong>:</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #008f00;">// AcDbBlockTableRecordPointer</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #b4261a;"><span style="color: #0433ff;">#include</span><span style="color: #000000;"> </span>&quot;dbobjptr.h&quot;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #008f00;">// AutoCAD Mechanical</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #0433ff;">#include<span style="color: #000000;"> </span><span style="color: #b4261a;">&quot;acm.h&quot;</span></p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #b4261a;"><span style="color: #0433ff;">#include</span><span style="color: #000000;"> </span>&quot;acmdef.h&quot;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #b4261a;"><span style="color: #0433ff;">#include</span><span style="color: #000000;"> </span>&quot;bommgr.h&quot;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #008f00;">// You also have to add these to the &quot;Additional Dependencies&quot;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #008f00;">// of the project:</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #008f00;">// rxapi.lib;acge19.lib;ac1st19.lib;acdb19.lib;accore.lib;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #008f00;">// acad.lib;acgiapi.lib;acmsymbb.lib;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="color: #0433ff;">public</span> <span style="color: #0433ff;">ref</span> <span style="color: #0433ff;">class</span> MixedManagedUtils</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">{</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #0433ff;">public<span style="color: #000000;">:&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; <span style="color: #0433ff;">static</span> <span style="color: #0433ff;">void</span> AcmBOMManager_getBomOverride(</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; &#0160; System::String^ name,</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>System::String^% value)</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; {</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>AcDbBlockTableRecordPointer ptrMS(</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>ACDB_MODEL_SPACE,</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>acdbHostApplicationServices()-&gt;workingDatabase(),</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>AcDb::kForRead);</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span><span style="color: #0433ff;">if</span> (ptrMS.openStatus() != Acad::eOk) { ASSERT(0); <span style="color: #0433ff;">return</span>; }</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span><span style="color: #0433ff;">pin_ptr</span>&lt;<span style="color: #0433ff;">const</span> ACHAR&gt; strName = PtrToStringChars(name);</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>ACHAR* strOrigValue = NULL;&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>ACHAR* strOriData = NULL;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>ACHAR* strCurValue = NULL;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>ACHAR* strCurData = NULL;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>Acm::BomOverrideType ordtype;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span><span style="color: #0433ff;">bool</span> iseditable;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>acmBomMgr-&gt;getBomOverride(ptrMS-&gt;objectId(), strName,</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">      </span>strOrigValue, strOriData, strCurValue, strCurData,</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">      </span>ordtype, iseditable);</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="white-space: pre;">    </span>value = <span style="color: #0433ff;">gcnew</span> System::String(strCurValue);</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; }</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">};</p>
<p>Now you can use this <strong>mixed-managed dll</strong> from your <strong>AutoCAD .NET AddIn</strong>. Just have to add a reference to the compiled dll/assembly and you can call the utility function we wrote:</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #33a2bd;"><span style="color: #000000;">&lt;</span>CommandMethod<span style="color: #000000;">(</span><span style="color: #b4261a;">&quot;GETASSEMBLYPROPERTY&quot;</span><span style="color: #000000;">)&gt; _</span></p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;"><span style="color: #0433ff;">Public</span> <span style="color: #0433ff;">Sub</span> TESTSYMBBAUTO()</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; &#0160; <span style="color: #0433ff;">Dim</span> value <span style="color: #0433ff;">As</span> <span style="color: #0433ff;">String</span> = <span style="color: #b4261a;">&quot;&quot;</span></p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; &#0160; <span style="color: #33a2bd;">MixedManagedUtils</span>.AcmBOMManager_getBomOverride(<span style="color: #b4261a;">&quot;NAME&quot;</span>, value)</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas;">&#0160; &#0160; MsgBox(value)</p>
<p style="margin: 0px; font-size: 8pt; font-family: Consolas; color: #0433ff;">End<span style="color: #000000;"> </span>Sub</p>
<p>The utility <strong>dll</strong> needs to be loaded inside <strong>AutoCAD</strong> before you try to call its functions. There are multiple ways to load a <strong>.NET</strong> dll into <strong>AutoCAD</strong>, so you can pick the one you prefer: <a href="http://adndevblog.typepad.com/autocad/2012/04/load-additional-dlls.html" target="_self">http://adndevblog.typepad.com/autocad/2012/04/load-additional-dlls.html</a></p>
<p>The utility <strong>ARX</strong> project is here: 
<span class="asset  asset-generic at-xid-6a0167607c2431970b019aff8ffcbe970c"><a href="http://adndevblog.typepad.com/files/mixedmanagedutils_2013-09-23.zip">Download MixedManagedUtils_2013-09-23</a></span>&#0160;&#0160;</p>

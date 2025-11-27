---
layout: "post"
title: "Add bitmap to GraphicsImageSet from C++"
date: "2013-04-11 09:57:38"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/04/add-bitmap-to-graphicsimageset-from-c.html "
typepad_basename: "add-bitmap-to-graphicsimageset-from-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I started with the <strong>C:\Users\Public\Documents\Autodesk\Inventor 2013\SDK\DeveloperTools\Samples\VC++\AddIns\SimpleAddIn</strong> sample project. I added to the resources a bitmap file, which got the resource ID&#0160;<strong>IDB_BITMAP1</strong>&#0160;assigned and was using a palette - as mentioned in the code in that case it seems that the <strong>LR_CREATEDIBSECTION</strong> flag is needed when loading the resource. Then replaced the code inside one of the ButtonDefinitionEvents_OnExecute with the below code. I tested this on a 64 bit OS.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42b79123970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="My Bitmap" class="asset  asset-image at-xid-6a0167607c2431970b017d42b79123970c" src="/assets/image_b205a8.jpg" title="My Bitmap" /></a></p>
<div style="line-height: 120%;">
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">HRESULT hr;<br />CComPtr&lt;Document&gt; doc =</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">m_pApplication-&gt;ActiveDocument;</span></div>
<div><span style="color: #0433ff; font-family: Consolas; font-size: 8pt;">if</span><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;(doc)</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">{</span></div>
<div><span style="color: #008f00; font-family: Consolas; font-size: 8pt;">&#0160; // LR_CREATEDIBSECTION was needed for my bitmap with palette</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; HBITMAP hBmp = (HBITMAP) LoadImage(</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; &#0160; _AtlBaseModule.GetResourceInstance(), MAKEINTRESOURCE(IDB_BITMAP1),&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; &#0160; IMAGE_BITMAP, 0, 0, LR_CREATEDIBSECTION);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; ATLASSERT(hBmp);</span></div>
<div><span style="color: #0433ff; font-family: Consolas; font-size: 8pt;">&#0160; if</span><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;(NULL == hBmp)&#0160;</span><span style="color: #0433ff; font-family: Consolas; font-size: 8pt;">return</span><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;E_FAIL;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: #008f00; font-family: Consolas; font-size: 8pt;">&#0160; // Create the picture&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; PICTDESC pdesc;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; pdesc.cbSizeofstruct =&#0160;</span><span style="color: #0433ff; font-family: Consolas; font-size: 8pt;">sizeof</span><span style="color: black; font-family: Consolas; font-size: 8pt;">(pdesc);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; pdesc.picType = PICTYPE_BITMAP;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; pdesc.bmp.hbitmap = hBmp;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; pdesc.bmp.hpal = 0;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;IPictureDisp&gt; pPictureDisp;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = ::OleCreatePictureIndirect(<br />&#0160; &#0160; &amp;pdesc, IID_IPictureDisp, FALSE, (LPVOID*)&amp;pPictureDisp );</span></div>
<div><span style="color: #0433ff; font-family: Consolas; font-size: 8pt;">&#0160; if</span><span style="color: black; font-family: Consolas; font-size: 8pt;">(FAILED(hr))&#0160;</span><span style="color: #0433ff; font-family: Consolas; font-size: 8pt;">return</span><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;hr;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: #008f00; font-family: Consolas; font-size: 8pt;">&#0160; // Add to data set</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;GraphicsDataSets&gt; dataSet;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = doc-&gt;GraphicsDataSetsCollection-&gt;Add(_bstr_t(</span><span style="color: #b4261a; font-family: Consolas; font-size: 8pt;">&quot;MyDataSet&quot;</span><span style="color: black; font-family: Consolas; font-size: 8pt;">), &amp;dataSet);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;GraphicsImageSet&gt; imageSet;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = dataSet-&gt;CreateImageSet(2, &amp;imageSet);&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = imageSet-&gt;Add(1, pPictureDisp, vtMissing, 0, 0);&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: #008f00; font-family: Consolas; font-size: 8pt;">&#0160; // Add to custom graphics</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComQIPtr&lt;PartDocument&gt; partDoc = doc;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;ClientGraphics&gt; cg;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = partDoc-&gt;ComponentDefinition-&gt;ClientGraphicsCollection-&gt;Add(<br />&#0160; &#0160; _bstr_t(</span><span style="color: #b4261a; font-family: Consolas; font-size: 8pt;">&quot;MyGraphics&quot;</span><span style="color: black; font-family: Consolas; font-size: 8pt;">), &amp;cg); &#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;GraphicsNode&gt; gn;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = cg-&gt;AddNode(1, &amp;gn);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;PointGraphics&gt; pg;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = gn-&gt;AddPointGraphics(&amp;pg);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;GraphicsCoordinateSet&gt; cs;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = dataSet-&gt;CreateCoordinateSet(1, &amp;cs);&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; CComPtr&lt;Point&gt; pt;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = m_pApplication-&gt;TransientGeometry-&gt;CreatePoint(0, 0, 0, &amp;pt);&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; cs-&gt;Add(1, pt);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; pg-&gt;PutCoordinateSet(cs);&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; hr = pg-&gt;SetCustomImage(imageSet, 1);</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">&#0160; m_pApplication-&gt;ActiveView-&gt;Update();&#0160;</span></div>
<div><span style="color: black; font-family: Consolas; font-size: 8pt;">}</span></div>
</div>
<p>And here is the result:</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3888873e970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Mybmp2" class="asset  asset-image at-xid-6a0167607c2431970b017c3888873e970b" src="/assets/image_43ca4b.jpg" title="Mybmp2" /></a><br /><br /></p>

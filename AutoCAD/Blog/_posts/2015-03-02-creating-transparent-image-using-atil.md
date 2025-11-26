---
layout: "post"
title: "Creating transparent image using ATIL"
date: "2015-03-02 03:19:08"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/creating-transparent-image-using-atil.html "
typepad_basename: "creating-transparent-image-using-atil"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code that implements a custom ATIL image filter to create transparent images. The implementation sets the Alpha channel for Red color pixels in the image. You can modify it to use any other RGB value if you choose. The complete sample project can be downloaded here : 
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c757446e970b img-responsive"><a href="http://adndevblog.typepad.com/files/transparentsnapshotusingatil.zip">Download TransparentSnapShotUsingATIL</a></span></p>
<p>The sample project uses the "getSnapShot" to capture a snap shot of the entities selected. To try it, &nbsp;build the attached sample project and load it in AutoCAD 2015.&nbsp;Open any drawing and run “GenImg” command and select the entity when prompted. The red background is intentionally set to the AcGsDevice while generating the screenshot.&nbsp;This is because, we can then use Red as the color to turn transparent from the image filter. The transparent&nbsp;image is generated under “D:\Temp\Test_Arx.png”.</p>
<p>Here is the relevant code :</p>
<p><span style="color: #008000;">//MyRgbaTransparency.h</span></p>
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">#ifndef</span><span style="color: #000000;">  MYRGBATRANSPARENCY_H</span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">#define</span><span style="color: #000000;">  MYRGBATRANSPARENCY_H</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">class</span><span style="color: #000000;">  MyRgbaTransparency </span></pre>
<pre style="margin: 0em;"> 	: <span style="color: #0000ff;">public</span><span style="color: #000000;">  Atil::ImageFilter</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">public</span><span style="color: #000000;"> :</span></pre>
<pre style="margin: 0em;"> 	MyRgbaTransparency ( </pre>
<pre style="margin: 0em;"> 		Atil::RowProviderInterface* pInput, </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">int</span><span style="color: #000000;">  nKeyColors, </span></pre>
<pre style="margin: 0em;"> 		Atil::RgbColor* paKeyColors);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  ~MyRgbaTransparency ();</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  <span style="color: #0000ff;">int</span><span style="color: #000000;">  rowsRemaining ();</span></span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  <span style="color: #0000ff;">void</span><span style="color: #000000;">  getNextRow</span></span></pre>
<pre style="margin: 0em;"> 		(Atil::DataBuffer &amp;oneRow);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  <span style="color: #0000ff;">void</span><span style="color: #000000;">  convertColor</span></span></pre>
<pre style="margin: 0em;"> 		(Atil::ImagePixel&amp; color) <span style="color: #0000ff;">const</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">private</span><span style="color: #000000;"> :</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">enum</span><span style="color: #000000;">  By <span style="color: #000000;">{</span> kQuad, kTreble, kNon <span style="color: #000000;">}</span>;</span></pre>
<pre style="margin: 0em;"> 	By mBy;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">int</span><span style="color: #000000;">  mnRows;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">int</span><span style="color: #000000;">  mnColumns;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">int</span><span style="color: #000000;">  mnKeyColors;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">int</span><span style="color: #000000;">  mnRowsRemaining;</span></pre>
<pre style="margin: 0em;"> 	Atil::RgbColor* mpKeyColors;</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>;</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">#endif</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #008000;">//MyRgbaTransparency.cpp</span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">#include</span><span style="color: #000000;">  <span style="color: #a31515;">"stdafx.h"</span></span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">#include</span><span style="color: #000000;">  <span style="color: #a31515;">"MyRgbaTransparency.h"</span></span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> MyRgbaTransparency::MyRgbaTransparency</pre>
<pre style="margin: 0em;"> 	(Atil::RowProviderInterface* pInput, </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">int</span><span style="color: #000000;">  nKeyColors, </span></pre>
<pre style="margin: 0em;"> 	Atil::RgbColor* paKeyColors )</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	connectInput( pInput );</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	Atil::Size size(input(0)-&gt;size());</pre>
<pre style="margin: 0em;"> 	mnColumns = size.width;;</pre>
<pre style="margin: 0em;"> 	mnRows = size.height;</pre>
<pre style="margin: 0em;"> 	mnRowsRemaining = size.height;</pre>
<pre style="margin: 0em;"> 	mnKeyColors = nKeyColors;</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">switch</span><span style="color: #000000;"> (input(0)-&gt;dataModel().dataModelType())</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">case</span><span style="color: #000000;">  Atil::DataModelAttributes</span></pre>
<pre style="margin: 0em;"> 			::DataModelType::kRgbModel:</pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			mBy = kTreble;</pre>
<pre style="margin: 0em;"> 			init( size );</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">break</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">default</span><span style="color: #000000;"> :</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			mBy = kQuad;</pre>
<pre style="margin: 0em;"> 			init( size );</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">break</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> MyRgbaTransparency::~MyRgbaTransparency ()</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">int</span><span style="color: #000000;">  MyRgbaTransparency::rowsRemaining ()</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  mnRowsRemaining;</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">void</span><span style="color: #000000;">  MyRgbaTransparency::getNextRow</span></pre>
<pre style="margin: 0em;"> 	(Atil::DataBuffer &amp;oneRow)</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	input(0)-&gt;getNextRow(oneRow);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( mnRowsRemaining &gt; 0 ) </span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( mBy == kTreble ) </span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			Atil::RgbColor *pColor = </pre>
<pre style="margin: 0em;"> 				(Atil::RgbColor*) oneRow.dataPtr();</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  i=0; i&lt;mnColumns; ++i) </span></span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">if</span><span style="color: #000000;"> ( pColor[i].rgba.red == 255 &amp;&amp; </span></pre>
<pre style="margin: 0em;"> 					pColor[i].rgba.blue == 0 &amp;&amp; </pre>
<pre style="margin: 0em;"> 					pColor[i].rgba.green == 0)</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span><span style="color: #008000;">// Turn alpha to 0 only for Red values</span></pre>
<pre style="margin: 0em;"> 					pColor[i].rgba.alpha = 0;</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span> </pre>
<pre style="margin: 0em;"> 		--mnRowsRemaining;</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">void</span><span style="color: #000000;">  MyRgbaTransparency::convertColor</span></pre>
<pre style="margin: 0em;"> 	(Atil::ImagePixel&amp; color) <span style="color: #0000ff;">const</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	ImageFilter::convertColor(color);</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">switch</span><span style="color: #000000;">  ( color.type )</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">case</span><span style="color: #000000;">  Atil::DataModelAttributes::PixelType::kRgba:</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			Atil::RgbColor rgb( color.value.rgba );</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;"> ( rgb.rgba.red == 255 </span></pre>
<pre style="margin: 0em;"> 				&amp;&amp; rgb.rgba.blue == 0 </pre>
<pre style="margin: 0em;"> 				&amp;&amp; rgb.rgba.green == 0)</pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span><span style="color: #008000;">// Turn alpha to 0 only for Red values</span></pre>
<pre style="margin: 0em;"> 				rgb.packed = rgb.packed;</pre>
<pre style="margin: 0em;"> 				rgb.rgba.alpha = 0;</pre>
<pre style="margin: 0em;"> 				color.value.rgba = rgb;</pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">break</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #008000;">// Usage of the Transparency Image filter</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> Atil::RgbModel rgbModel(</pre>
<pre style="margin: 0em;"> 	Atil::RgbModelAttributes::k4Channels, </pre>
<pre style="margin: 0em;"> 	Atil::DataModelAttributes::kRedGreenBlueAlpha);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> Atil::ImagePixel initialColor(</pre>
<pre style="margin: 0em;"> 	Atil::DataModelAttributes::PixelType::kRgba);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> initialColor.setToZero();</pre>
<pre style="margin: 0em;"> initialColor.type = Atil::DataModelAttributes::kRgba;</pre>
<pre style="margin: 0em;"> initialColor.value.rgba = 0xff000000;</pre>
<pre style="margin: 0em;"> Atil::Image imgSource(</pre>
<pre style="margin: 0em;"> 	Atil::Size(width, height), </pre>
<pre style="margin: 0em;"> 	&amp;rgbModel, initialColor);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #008000;">// get a snapshot of the GsView</span></pre>
<pre style="margin: 0em;"> pView-&gt;getSnapShot(&amp;imgSource, screenRect.m_min);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> Atil::RowProviderInterface *pPipe </pre>
<pre style="margin: 0em;"> 	= imgSource.read(</pre>
<pre style="margin: 0em;"> 		imgSource.size(), </pre>
<pre style="margin: 0em;"> 		Atil::Offset(0,0),Atil::kBottomUpLeftRight);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">if</span><span style="color: #000000;"> (pPipe != NULL)</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	Atil::RgbColor aColors[1];</pre>
<pre style="margin: 0em;"> 	COLORREF crBkg = RGB(255, 0, 0); </pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	aColors[0] = Atil::RgbColor(</pre>
<pre style="margin: 0em;"> 		GetRValue(crBkg), </pre>
<pre style="margin: 0em;"> 		GetGValue(crBkg &amp; 0xffff), </pre>
<pre style="margin: 0em;"> 		GetBValue(crBkg), 0);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	pPipe = <span style="color: #0000ff;">new</span><span style="color: #000000;">  MyRgbaTransparency(</span></pre>
<pre style="margin: 0em;"> 		pPipe, </pre>
<pre style="margin: 0em;"> 		1, </pre>
<pre style="margin: 0em;"> 		aColors);</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pPipe != NULL &amp;&amp; pPipe-&gt;isValid()) </span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		TCHAR drive[_MAX_DRIVE];</pre>
<pre style="margin: 0em;"> 		TCHAR dir[_MAX_DIR];</pre>
<pre style="margin: 0em;"> 		TCHAR fname[_MAX_FNAME];</pre>
<pre style="margin: 0em;"> 		TCHAR ext[_MAX_EXT];	</pre>
<pre style="margin: 0em;"> 		<span style="color: #008000;">// find out what extension we have</span></pre>
<pre style="margin: 0em;"> 		_tsplitpath_s(</pre>
<pre style="margin: 0em;"> 			pFileName, </pre>
<pre style="margin: 0em;"> 			drive, dir, fname, ext);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 		Atil::ImageFormatCodec *pCodec = NULL;</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;">  (CString(ext) == _T(<span style="color: #a31515;">".png"</span><span style="color: #000000;"> ))</span></span></pre>
<pre style="margin: 0em;"> 			pCodec = <span style="color: #0000ff;">new</span><span style="color: #000000;">  PngFormatCodec();</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pCodec != NULL)</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// and it is compatible</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;">  (Atil::FileWriteDescriptor::</span></pre>
<pre style="margin: 0em;"> 				isCompatibleFormatCodec(</pre>
<pre style="margin: 0em;"> 				pCodec, &amp;(pPipe-&gt;dataModel()), </pre>
<pre style="margin: 0em;"> 				pPipe-&gt;size())) </pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// create a new file output object</span></pre>
<pre style="margin: 0em;"> 			Atil::FileWriteDescriptor fileWriter(pCodec);</pre>
<pre style="margin: 0em;"> 			Atil::FileSpecifier fs(</pre>
<pre style="margin: 0em;"> 				Atil::StringBuffer((lstrlen(pFileName)+1) </pre>
<pre style="margin: 0em;"> 				* <span style="color: #0000ff;">sizeof</span><span style="color: #000000;"> (TCHAR), </span></pre>
<pre style="margin: 0em;"> 				(<span style="color: #0000ff;">const</span><span style="color: #000000;">  Atil::Byte *)pFileName, </span></pre>
<pre style="margin: 0em;"> 				Atil::StringBuffer::kUTF_16), </pre>
<pre style="margin: 0em;"> 				Atil::FileSpecifier::kFilePath);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// if the file already exists</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// we better delete it because setFileSpecifier </span></pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// will fail otherwise</span></pre>
<pre style="margin: 0em;"> 			_tremove(pFileName);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;">  (fileWriter.setFileSpecifier(fs))</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				fileWriter.createImageFrame(</pre>
<pre style="margin: 0em;"> 					pPipe-&gt;dataModel(), </pre>
<pre style="margin: 0em;"> 					pPipe-&gt;size());</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 				<span style="color: #008000;">// At any rate you want to fetch the property</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #008000;">// from the write file descriptor then alter </span></pre>
<pre style="margin: 0em;"> 				<span style="color: #008000;">// it and set it in\uc1\u8230?</span></pre>
<pre style="margin: 0em;"> 				Atil::FormatCodecPropertyInterface *pProp </pre>
<pre style="margin: 0em;"> 					= fileWriter.getProperty(</pre>
<pre style="margin: 0em;"> 				Atil::FormatCodecPropertyInterface::kCompression);</pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pProp != NULL) </span></pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">if</span><span style="color: #000000;">  (CString(ext) == _T(<span style="color: #a31515;">".png"</span><span style="color: #000000;"> )) </span></span></pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 					PngCompression *pComp = </pre>
<pre style="margin: 0em;"> 						<span style="color: #0000ff;">dynamic_cast</span><span style="color: #000000;"> &lt;PngCompression*&gt;(pProp);</span></pre>
<pre style="margin: 0em;"> 					<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pComp != NULL) </span></pre>
<pre style="margin: 0em;"> 					<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 					pComp-&gt;selectCompression(</pre>
<pre style="margin: 0em;"> 						PngCompressionType::kHigh);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 					fileWriter.setProperty(pComp);</pre>
<pre style="margin: 0em;"> 					<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 				<span style="color: #008000;">// clean up</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pProp; </span></pre>
<pre style="margin: 0em;"> 				pProp = NULL;</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			Atil::FormatCodecPropertySetIterator* pPropsIter</pre>
<pre style="margin: 0em;"> 				= fileWriter.newPropertySetIterator();</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pPropsIter)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">for</span><span style="color: #000000;">  (pPropsIter-&gt;start(); </span></pre>
<pre style="margin: 0em;"> 					!pPropsIter-&gt;endOfList(); </pre>
<pre style="margin: 0em;"> 					pPropsIter-&gt;step())</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 					Atil::FormatCodecPropertyInterface* pProp</pre>
<pre style="margin: 0em;"> 						= pPropsIter-&gt;openProperty();</pre>
<pre style="margin: 0em;"> 					<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pProp-&gt;isRequired())</span></pre>
<pre style="margin: 0em;"> 					<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 						fileWriter.setProperty(pProp);</pre>
<pre style="margin: 0em;"> 					<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 					pPropsIter-&gt;closeProperty();</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pPropsIter;</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			Atil::FormatCodecPropertyInterface </pre>
<pre style="margin: 0em;"> 				*pTransparencyProp</pre>
<pre style="margin: 0em;"> 			= fileWriter.getProperty(</pre>
<pre style="margin: 0em;"> 			Atil::FormatCodecPropertyInterface::kTransparency);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;"> (pTransparencyProp)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				fileWriter.setProperty(pTransparencyProp);</pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			<span style="color: #008000;">// ok - ready to write it</span></pre>
<pre style="margin: 0em;"> 			fileWriter.writeImageFrame(pPipe);</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 			done = <span style="color: #0000ff;">true</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pCodec;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<!-- End block -->

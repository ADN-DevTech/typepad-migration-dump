---
layout: "post"
title: "Embedding an image in a drawing"
date: "2022-02-17 04:22:48"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2022/02/embedding-an-image-in-a-drawing.html "
typepad_basename: "embedding-an-image-in-a-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In this blog post we will look at creating a custom object derived from AcDbRasterImageDef that saves/loads the image data to/from the drawing using ATIL. This will ensure that your drawing is independent of the external image file and the image data will get loaded to the AcDbRasterImageDef if the arx is loaded in AutoCAD.</p>
<p>For other ways to embed an image in a drawing without having a dependency on an external image file, please refer to this blog post :</p>
<p><a href="http://adndevblog.typepad.com/autocad/2012/10/embedding-an-image-in-a-drawing.html">Embedding an image in a drawing</a></p>

<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> <span style="color: #008000;">// AcDbMyRasterImageDef.h</span> </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">class</span><span style="color: #000000;">  DLLIMPEXP AcDbMyRasterImageDef : </span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">public</span><span style="color: #000000;">  AcDbRasterImageDef </span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">protected</span><span style="color: #000000;"> :</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">static</span><span style="color: #000000;">  Adesk::UInt32 kCurrentVersionNumber;</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">public</span><span style="color: #000000;"> :</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	ACRX_DECLARE_MEMBERS(AcDbMyRasterImageDef) ;</pre>
<pre style="margin: 0em;"> 	</pre>
<pre style="margin: 0em;"> 	AcDbMyRasterImageDef () ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  ~AcDbMyRasterImageDef () ;</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  Acad::ErrorStatus </span></pre>
<pre style="margin: 0em;"> 		dwgOutFields (AcDbDwgFiler *pFiler) <span style="color: #0000ff;">const</span><span style="color: #000000;">  ;</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  Acad::ErrorStatus dwgInFields </span></pre>
<pre style="margin: 0em;"> 		(AcDbDwgFiler *pFiler) ;</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">//----- deepClone</span> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  Acad::ErrorStatus </span></pre>
<pre style="margin: 0em;"> 		subDeepClone (AcDbObject *pOwnerObject, </pre>
<pre style="margin: 0em;"> 		AcDbObject *&amp;pClonedObject, AcDbIdMapping &amp;idMap, </pre>
<pre style="margin: 0em;"> 		Adesk::Boolean isPrimary =<span style="color: #0000ff;">true</span><span style="color: #000000;"> ) <span style="color: #0000ff;">const</span><span style="color: #000000;">  ;</span></span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">//----- wblockClone</span> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">virtual</span><span style="color: #000000;">  Acad::ErrorStatus subWblockClone </span></pre>
<pre style="margin: 0em;"> 		(AcRxObject *pOwnerObject, AcDbObject *&amp;pClonedObject, </pre>
<pre style="margin: 0em;"> 		AcDbIdMapping &amp;idMap, Adesk::Boolean isPrimary =<span style="color: #0000ff;">true</span><span style="color: #000000;"> )</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">const</span><span style="color: #000000;">  ;</span></pre>
<pre style="margin: 0em;">     </pre>
<pre style="margin: 0em;"> 	Acad::ErrorStatus </pre>
<pre style="margin: 0em;"> 		setEmbeddedImage(<span style="color: #0000ff;">const</span><span style="color: #000000;">  ACHAR* imageFilePath);</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">private</span><span style="color: #000000;"> :</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">void</span><span style="color: #000000;">  *<span style="color: #0000ff;">operator</span><span style="color: #000000;">  <span style="color: #0000ff;">new</span><span style="color: #000000;"> [](size_t) <span style="color: #0000ff;">throw</span><span style="color: #000000;"> ()<span style="color: #000000;">{</span> <span style="color: #0000ff;">return</span><span style="color: #000000;">  (<span style="color: #0000ff;">void</span><span style="color: #000000;"> *)0;<span style="color: #000000;">}</span></span></span></span></span></span></span></pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">void</span><span style="color: #000000;">  <span style="color: #0000ff;">operator</span><span style="color: #000000;">  <span style="color: #0000ff;">delete</span><span style="color: #000000;"> [](<span style="color: #0000ff;">void</span><span style="color: #000000;">  *) <span style="color: #000000;">{</span><span style="color: #000000;">}</span>;</span></span></span></span></pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">void</span><span style="color: #000000;">  *<span style="color: #0000ff;">operator</span><span style="color: #000000;">  <span style="color: #0000ff;">new</span><span style="color: #000000;"> [](size_t , <span style="color: #0000ff;">const</span><span style="color: #000000;">  <span style="color: #0000ff;">char</span><span style="color: #000000;">  *, <span style="color: #0000ff;">int</span><span style="color: #000000;">  ) <span style="color: #0000ff;">throw</span><span style="color: #000000;"> ()</span></span></span></span></span></span></span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span> <span style="color: #0000ff;">return</span><span style="color: #000000;">  (<span style="color: #0000ff;">void</span><span style="color: #000000;"> *)0;<span style="color: #000000;">}</span></span></span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	Atil::Image *m_pAtilImage;</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span>;</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #008000;">// AcDbMyRasterImageDef.cpp</span> </pre>
<pre style="margin: 0em;"> AcDbMyRasterImageDef::AcDbMyRasterImageDef () </pre>
<pre style="margin: 0em;"> 	: AcDbRasterImageDef () </pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	m_pAtilImage = NULL;</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> AcDbMyRasterImageDef::~AcDbMyRasterImageDef() </pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (m_pAtilImage != NULL)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  m_pAtilImage;</span></pre>
<pre style="margin: 0em;"> 		m_pAtilImage = NULL;</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> Acad::ErrorStatus AcDbMyRasterImageDef::dwgOutFields</pre>
<pre style="margin: 0em;"> 	(AcDbDwgFiler *pFiler) <span style="color: #0000ff;">const</span><span style="color: #000000;">  <span style="color: #000000;">{</span></span></pre>
<pre style="margin: 0em;"> 	assertReadEnabled () ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">//----- Save parent class information first.</span> </pre>
<pre style="margin: 0em;"> 	Acad::ErrorStatus es =</pre>
<pre style="margin: 0em;"> 		AcDbRasterImageDef::dwgOutFields (pFiler) ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( es != Acad::eOk )</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  (es) ;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">//----- Object version number needs to be saved first</span> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( (es =pFiler-&gt;writeUInt32 </span></pre>
<pre style="margin: 0em;"> 		(AcDbMyRasterImageDef::kCurrentVersionNumber))</pre>
<pre style="margin: 0em;"> 		!= Acad::eOk )</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  (es) ;</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (m_pAtilImage)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		Atil::Size size = m_pAtilImage-&gt;size();</pre>
<pre style="margin: 0em;"> 		Int32 width = size.width;</pre>
<pre style="margin: 0em;"> 		Int32 height = size.height;</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 		pFiler-&gt;writeInt32(width);</pre>
<pre style="margin: 0em;"> 		pFiler-&gt;writeInt32(height);</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 		<span style="color: #008000;">// Write the image data on to the Atil image</span> </pre>
<pre style="margin: 0em;"> 		<span style="color: #008000;">// using an Image Context</span> </pre>
<pre style="margin: 0em;"> 		Atil::Offset upperLeft(0,0);</pre>
<pre style="margin: 0em;"> 		Atil::ImageContext *pImgContext </pre>
<pre style="margin: 0em;"> 			= m_pAtilImage-&gt;createContext(</pre>
<pre style="margin: 0em;"> 				Atil::ImageContext::kRead,</pre>
<pre style="margin: 0em;"> 				size,</pre>
<pre style="margin: 0em;"> 				upperLeft);</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pImgContext != NULL)</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  xf=0;xf&lt;width;xf++)</span></span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				<span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  yf=0;yf&lt;height;yf++)</span></span></pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 					Atil::RgbColor p;</pre>
<pre style="margin: 0em;"> 					p = pImgContext-&gt;get32(xf, yf);</pre>
<pre style="margin: 0em;"> 					pFiler-&gt;writeInt32(p.packed);</pre>
<pre style="margin: 0em;"> 				<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		pImgContext-&gt;flush();</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pImgContext;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  (pFiler-&gt;filerStatus ()) ;</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> Acad::ErrorStatus AcDbMyRasterImageDef::dwgInFields</pre>
<pre style="margin: 0em;"> 	(AcDbDwgFiler *pFiler) </pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	assertWriteEnabled () ;</pre>
<pre style="margin: 0em;"> 	Acad::ErrorStatus es =</pre>
<pre style="margin: 0em;"> 		AcDbRasterImageDef::dwgInFields (pFiler) ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( es != Acad::eOk )</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  (es) ;</span></pre>
<pre style="margin: 0em;"> 	</pre>
<pre style="margin: 0em;"> 	Adesk::UInt32 version =0 ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( (es =pFiler-&gt;readUInt32 (&amp;version)) </span></pre>
<pre style="margin: 0em;"> 		!= Acad::eOk )</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  (es) ;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  ( version &gt; </span></pre>
<pre style="margin: 0em;"> 		AcDbMyRasterImageDef::kCurrentVersionNumber)</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  (Acad::eMakeMeProxy) ;</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	Int32 width = 0;</pre>
<pre style="margin: 0em;"> 	Int32 height = 0;</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	pFiler-&gt;readInt32(&amp;width);</pre>
<pre style="margin: 0em;"> 	pFiler-&gt;readInt32(&amp;height);</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// Create an Atil::Image.</span> </pre>
<pre style="margin: 0em;">     <span style="color: #008000;">// The AcDbRasterImageDef::setImage requires it</span> </pre>
<pre style="margin: 0em;">     Atil::ImagePixel initialImage;</pre>
<pre style="margin: 0em;">     initialImage.setToZero();</pre>
<pre style="margin: 0em;">     initialImage.type </pre>
<pre style="margin: 0em;"> 		= Atil::DataModelAttributes::kBgra;</pre>
<pre style="margin: 0em;">     initialImage.value.rgba = 0xff000000;</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     Atil::Size size(width, height);</pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">const</span><span style="color: #000000;">  Atil::RgbModel *pDm </span></pre>
<pre style="margin: 0em;"> 		= <span style="color: #0000ff;">new</span><span style="color: #000000;">  Atil::RgbModel(</span></pre>
<pre style="margin: 0em;">         Atil::RgbModelAttributes::k4Channels,</pre>
<pre style="margin: 0em;">         Atil::DataModelAttributes::kBlueGreenRedAlpha);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (m_pAtilImage != NULL)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  m_pAtilImage;</span></pre>
<pre style="margin: 0em;"> 		m_pAtilImage = NULL;</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">     m_pAtilImage</pre>
<pre style="margin: 0em;"> 		= <span style="color: #0000ff;">new</span><span style="color: #000000;">  Atil::Image(size, pDm, initialImage);</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// Write the image data on to the Atil image</span> </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// using an Image Context</span> </pre>
<pre style="margin: 0em;"> 	Atil::Offset upperLeft(0,0);</pre>
<pre style="margin: 0em;"> 	Atil::ImageContext *pImgContext </pre>
<pre style="margin: 0em;"> 		= m_pAtilImage-&gt;createContext(</pre>
<pre style="margin: 0em;"> 				Atil::ImageContext::kWrite,</pre>
<pre style="margin: 0em;"> 				size,</pre>
<pre style="margin: 0em;"> 				upperLeft);</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  (pImgContext != NULL)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  xf=0;xf&lt;width;xf++)</span></span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  yf=0;yf&lt;height;yf++)</span></span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				Int32 value;</pre>
<pre style="margin: 0em;"> 				pFiler-&gt;readInt32(&amp;value);</pre>
<pre style="margin: 0em;"> 				pImgContext-&gt;put32(xf, yf, value);</pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	pImgContext-&gt;flush();</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pImgContext;</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	setImage (m_pAtilImage, NULL) ;</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  (pFiler-&gt;filerStatus ()) ;</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #008000;">//----- deepClone</span> </pre>
<pre style="margin: 0em;"> Acad::ErrorStatus AcDbMyRasterImageDef::subDeepClone </pre>
<pre style="margin: 0em;"> 	(AcDbObject *pOwnerObject, AcDbObject *&amp;pClonedObject, </pre>
<pre style="margin: 0em;"> 	AcDbIdMapping &amp;idMap, Adesk::Boolean isPrimary) <span style="color: #0000ff;">const</span>  </pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	assertReadEnabled () ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  (AcDbRasterImageDef::subDeepClone </span></pre>
<pre style="margin: 0em;"> 		(pOwnerObject, pClonedObject, idMap, isPrimary)) ;</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #008000;">//----- wblockClone</span> </pre>
<pre style="margin: 0em;"> Acad::ErrorStatus AcDbMyRasterImageDef::subWblockClone </pre>
<pre style="margin: 0em;"> 	(AcRxObject *pOwnerObject, AcDbObject *&amp;pClonedObject, </pre>
<pre style="margin: 0em;"> 	AcDbIdMapping &amp;idMap, Adesk::Boolean isPrimary) <span style="color: #0000ff;">const</span>  </pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	assertReadEnabled () ;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  (AcDbRasterImageDef::subWblockClone </span></pre>
<pre style="margin: 0em;"> 		(pOwnerObject, pClonedObject, idMap, isPrimary)) ;</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> Acad::ErrorStatus  AcDbMyRasterImageDef::setEmbeddedImage</pre>
<pre style="margin: 0em;"> 							(<span style="color: #0000ff;">const</span><span style="color: #000000;">  ACHAR* imageFilePath)</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">     AcString imagePath(imageFilePath);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     AcTcImage tc;</pre>
<pre style="margin: 0em;">     tc.Load(imagePath);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     HBITMAP bmp=0;</pre>
<pre style="margin: 0em;">     tc.GetHBITMAP(RGB(0xff,0xff,0xff),bmp);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">if</span><span style="color: #000000;">  (bmp == NULL)</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  Acad::eFileNotFound;</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     BITMAP _bmp=<span style="color: #000000;">{</span>0<span style="color: #000000;">}</span>;</pre>
<pre style="margin: 0em;">     GetObject(bmp,<span style="color: #0000ff;">sizeof</span><span style="color: #000000;">  BITMAP,&amp;_bmp);</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     HDC hdcScr=GetDC(NULL);</pre>
<pre style="margin: 0em;">     HDC    hdcMem=CreateCompatibleDC(hdcScr);</pre>
<pre style="margin: 0em;">     SelectObject(hdcMem,bmp);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     Atil::ImagePixel initialImage;</pre>
<pre style="margin: 0em;">     initialImage.setToZero();</pre>
<pre style="margin: 0em;"> 	initialImage.type = Atil::DataModelAttributes::kBgra;</pre>
<pre style="margin: 0em;">     initialImage.value.rgba = 0xff000000;</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     Atil::Size size(_bmp.bmWidth, _bmp.bmHeight);</pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">const</span><span style="color: #000000;">  Atil::RgbModel *pDm = <span style="color: #0000ff;">new</span><span style="color: #000000;">  Atil::RgbModel(</span></span></pre>
<pre style="margin: 0em;">         Atil::RgbModelAttributes::k4Channels,</pre>
<pre style="margin: 0em;">         Atil::DataModelAttributes::kBlueGreenRedAlpha);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (m_pAtilImage != NULL)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  m_pAtilImage;</span></pre>
<pre style="margin: 0em;"> 		m_pAtilImage = NULL;</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">     m_pAtilImage </pre>
<pre style="margin: 0em;"> 		= <span style="color: #0000ff;">new</span><span style="color: #000000;">  Atil::Image(size, pDm, initialImage);</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     Atil::Offset upperLeft(0,0);</pre>
<pre style="margin: 0em;">     Atil::ImageContext *pImgContext</pre>
<pre style="margin: 0em;">         = m_pAtilImage-&gt;createContext(</pre>
<pre style="margin: 0em;">                     Atil::ImageContext::kWrite,</pre>
<pre style="margin: 0em;">                     size,</pre>
<pre style="margin: 0em;">                     upperLeft);</pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">if</span><span style="color: #000000;">  (pImgContext != NULL)</span></pre>
<pre style="margin: 0em;">     <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">         <span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  xf=0;xf&lt;_bmp.bmWidth;xf++)</span></span></pre>
<pre style="margin: 0em;">         <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">             <span style="color: #0000ff;">for</span><span style="color: #000000;">  (<span style="color: #0000ff;">int</span><span style="color: #000000;">  yf=0;yf&lt;_bmp.bmHeight;yf++)</span></span></pre>
<pre style="margin: 0em;">             <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;">                 BYTE alpha= 0xff;</pre>
<pre style="margin: 0em;">                 COLORREF pix=GetPixel(hdcMem,xf,yf);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">                 BYTE rr = (pix&amp;0xff);</pre>
<pre style="margin: 0em;">                 BYTE gg = (pix&gt;&gt;8)&amp;0xff;</pre>
<pre style="margin: 0em;">                 BYTE bb = (pix&gt;&gt;16)&amp;0xff;</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">                 Atil::RgbColor p;</pre>
<pre style="margin: 0em;">                 p.set(rr, gg, bb, alpha);</pre>
<pre style="margin: 0em;">                 pImgContext-&gt;put32(xf, yf, p);</pre>
<pre style="margin: 0em;">             <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">         <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">     <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">     pImgContext-&gt;flush();</pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">delete</span><span style="color: #000000;">  pImgContext;</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     <span style="color: #0000ff;">bool</span><span style="color: #000000;">  isImageValid = m_pAtilImage-&gt;isValid();</span></pre>
<pre style="margin: 0em;">     assert(isImageValid);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     <span style="color: #008000;">// Cleanup</span> </pre>
<pre style="margin: 0em;">     DeleteDC(hdcMem);</pre>
<pre style="margin: 0em;">     ReleaseDC(NULL,hdcScr);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;">     DeleteObject( bmp);</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  setImage (m_pAtilImage, NULL) ;</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #008000;">// Embed image command</span> </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">static</span><span style="color: #000000;">  <span style="color: #0000ff;">void</span><span style="color: #000000;">  AdskMyTestEmbedImage(<span style="color: #0000ff;">void</span><span style="color: #000000;"> )</span></span></span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	AcDbMyRasterImageDef *pImageDef </pre>
<pre style="margin: 0em;"> 		= <span style="color: #0000ff;">new</span><span style="color: #000000;">  AcDbMyRasterImageDef();</span></pre>
<pre style="margin: 0em;"> 	pImageDef-&gt;setEmbeddedImage(</pre>
<pre style="margin: 0em;"> 		ACRX_T(<span style="color: #a31515;">&quot;D:\\TestFiles\\MyTexture.jpg&quot;</span><span style="color: #000000;"> ));</span></pre>
<pre style="margin: 0em;"> 	Acad::ErrorStatus es </pre>
<pre style="margin: 0em;"> 		= InsertImageInDwg(pImageDef);</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (es != Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pImageDef;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;"> ;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">static</span><span style="color: #000000;">  Acad::ErrorStatus </span></pre>
<pre style="margin: 0em;"> 	InsertImageInDwg(AcDbRasterImageDef *pImageDef)</pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	Acad::ErrorStatus es;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (! pImageDef-&gt;isLoaded())</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		es = pImageDef-&gt;load();</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;"> (es != Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">return</span><span style="color: #000000;">  es;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	AcApDocument *pActiveDoc </pre>
<pre style="margin: 0em;"> 		= acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin: 0em;"> 	AcDbDatabase *pDB = pActiveDoc-&gt;database();</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// Get the image dictionary</span> </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// Create it if not available already</span> </pre>
<pre style="margin: 0em;"> 	AcDbObjectId dictID</pre>
<pre style="margin: 0em;"> 	= AcDbRasterImageDef::imageDictionary(pDB);</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (dictID == AcDbObjectId::kNull)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		es = AcDbRasterImageDef::createImageDictionary</pre>
<pre style="margin: 0em;"> 			(pDB, dictID);</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;"> (es != Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">return</span><span style="color: #000000;">  es;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	AcDbDictionary* pDict;</pre>
<pre style="margin: 0em;"> 	es = acdbOpenObject(pDict, dictID, AcDb::kForWrite);</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;"> (es != Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">return</span><span style="color: #000000;">  es;</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	ACHAR *DICT_NAME = ACRX_T(<span style="color: #a31515;">&quot;RASTER_USING_BUFFER&quot;</span><span style="color: #000000;"> );</span></pre>
<pre style="margin: 0em;"> 	BOOL bExist = pDict-&gt;has(DICT_NAME);</pre>
<pre style="margin: 0em;"> 	AcDbObjectId objID = AcDbObjectId::kNull;</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  (!bExist)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		es = pDict-&gt;setAt(DICT_NAME, pImageDef, objID);</pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;"> (es != Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			pDict-&gt;close();</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">return</span><span style="color: #000000;">  es;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">else</span> </pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		pDict-&gt;getAt(DICT_NAME,</pre>
<pre style="margin: 0em;"> 					(AcDbObject*&amp;)pImageDef,</pre>
<pre style="margin: 0em;"> 					AcDb::kForWrite);</pre>
<pre style="margin: 0em;"> 		objID = pImageDef-&gt;objectId();</pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// close Dictionary and Definition.</span> </pre>
<pre style="margin: 0em;"> 	pDict-&gt;close();</pre>
<pre style="margin: 0em;"> 	pImageDef-&gt;close();</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// Create a raster image using the RasterImage Def</span> </pre>
<pre style="margin: 0em;"> 	AcDbRasterImage* pImage = <span style="color: #0000ff;">new</span><span style="color: #000000;">  AcDbRasterImage;</span></pre>
<pre style="margin: 0em;"> 	es = pImage-&gt;setImageDefId(objID);</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  (es != Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">delete</span><span style="color: #000000;">  pImage;</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">return</span><span style="color: #000000;">  es;</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	<span style="color: #008000;">// Add the raster image to the model space</span> </pre>
<pre style="margin: 0em;"> 	AcDbBlockTable* pBlockTable;</pre>
<pre style="margin: 0em;"> 	AcDbBlockTableRecord* pBTRecord;</pre>
<pre style="margin: 0em;"> 	es = acdbCurDwg()-&gt;getBlockTable(pBlockTable,</pre>
<pre style="margin: 0em;"> 							AcDb::kForRead);</pre>
<pre style="margin: 0em;"> 	assert(es == Acad::eOk);</pre>
<pre style="margin: 0em;"> 	es = pBlockTable-&gt;getAt(ACDB_MODEL_SPACE,</pre>
<pre style="margin: 0em;"> 							pBTRecord,</pre>
<pre style="margin: 0em;"> 							AcDb::kForWrite);</pre>
<pre style="margin: 0em;"> 	assert(es == Acad::eOk);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	es = pBTRecord-&gt;appendAcDbEntity(pImage);</pre>
<pre style="margin: 0em;"> 	assert(es == Acad::eOk);</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	pBTRecord-&gt;close();</pre>
<pre style="margin: 0em;"> 	pBlockTable-&gt;close();</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	AcDbObjectId entID = pImage-&gt;objectId();</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	AcDbObjectPointer&lt;AcDbRasterImageDefReactor&gt;</pre>
<pre style="margin: 0em;"> 						rasterImageDefReactor;</pre>
<pre style="margin: 0em;"> 	rasterImageDefReactor.create();</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	es = rasterImageDefReactor-&gt;setOwnerId(</pre>
<pre style="margin: 0em;"> 							pImage-&gt;objectId());</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">if</span><span style="color: #000000;">  (es == Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 		AcDbObjectId defReactorId;</pre>
<pre style="margin: 0em;"> 		es = curDoc()-&gt;database()-&gt;addAcDbObject(</pre>
<pre style="margin: 0em;"> 				defReactorId,</pre>
<pre style="margin: 0em;"> 				rasterImageDefReactor.object());</pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 		<span style="color: #0000ff;">if</span><span style="color: #000000;">  (es == Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 			pImage-&gt;setReactorId(defReactorId);</pre>
<pre style="margin: 0em;"> 			AcDbObjectPointer&lt;AcDbRasterImageDef&gt; </pre>
<pre style="margin: 0em;"> 				rasterImagedef</pre>
<pre style="margin: 0em;"> 				(pImage-&gt;imageDefId(), AcDb::kForWrite);</pre>
<pre style="margin: 0em;"> 			<span style="color: #0000ff;">if</span><span style="color: #000000;">  (rasterImagedef.openStatus() == Acad::eOk)</span></pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 				rasterImagedef-&gt;addPersistentReactor</pre>
<pre style="margin: 0em;"> 						(defReactorId);</pre>
<pre style="margin: 0em;"> 				</pre>
<pre style="margin: 0em;"> 			<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 		<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> 	<span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;">  </pre>
<pre style="margin: 0em;"> 	pImageDef-&gt;close();</pre>
<pre style="margin: 0em;"> 	pImage-&gt;close();</pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  Acad::eOk;</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
<pre style="margin: 0em;"> <span style="color: #0000ff;">virtual</span><span style="color: #000000;">  AcRx::AppRetCode On_kInitAppMsg (<span style="color: #0000ff;">void</span><span style="color: #000000;">  *pkt) </span></span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">{</span></pre>
<pre style="margin: 0em;"> 	AcRx::AppRetCode retCode =</pre>
<pre style="margin: 0em;"> 		AcRxArxApp::On_kInitAppMsg (pkt) ;</pre>
<pre style="margin: 0em;"> 	acrxDynamicLinker-&gt;loadModule</pre>
<pre style="margin: 0em;"> 		(L<span style="color: #a31515;">&quot;acismobj20.dbx&quot;</span><span style="color: #000000;"> ,<span style="color: #0000ff;">true</span><span style="color: #000000;"> ); </span></span></pre>
<pre style="margin: 0em;"> 	AcDbMyRasterImageDef::rxInit();</pre>
<pre style="margin: 0em;"> 	acrxBuildClassHierarchy();</pre>
<pre style="margin: 0em;"> 	<span style="color: #0000ff;">return</span><span style="color: #000000;">  (retCode) ;</span></pre>
<pre style="margin: 0em;"> <span style="color: #000000;">}</span></pre>
<pre style="margin: 0em;"> </pre>
</div>

<p>The sample project can be downloaded here :</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0fccf82970c img-responsive"><a href="http://adndevblog.typepad.com/files/embedimage.zip">Download EmbedImage</a></span></p>
<p>If you are looking to create an embedded raster image using the AutoCAD .Net API, a way to do it is by creating a managed wrapper for the custom object that we created. A .Net only solution is not possible at present since custom objects can only be created using C++. Also, ATIL is a C++ only library which we use in this case.</p>
<p>A way to access it in .Net is demonstrated in the following sample project. To try this sample, appload the .arx and netload the managed wrapper and custom .Net module. Run the &quot;EmbedImageMgd&quot; command.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13d9c0f970c img-responsive"><a href="http://adndevblog.typepad.com/files/customrasterimagedef.zip">Download CustomRasterImageDef</a></span></p>
<p>&#0160;</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13d9c0f970c img-responsive">Update:<br /></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13d9c0f970c img-responsive"> the broker code is fixed and updated to ObjectARX 2022, is posted in Github</span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d13d9c0f970c img-responsive">https://github.com/MadhukarMoogala/EmbedRasterImage</span></p>
<p>&#0160;</p>

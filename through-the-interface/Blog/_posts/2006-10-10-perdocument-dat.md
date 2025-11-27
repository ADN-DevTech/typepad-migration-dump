---
layout: "post"
title: "Per-document data in ObjectARX"
date: "2006-10-10 16:52:10"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://www.keanw.com/2006/10/perdocument_dat.html "
typepad_basename: "perdocument_dat"
typepad_status: "Publish"
---

<p>As discussed in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/10/some_background.html">the previous post</a>, AutoCAD is now largely an MDI application, and this can have an impact on the design of applications.</p>

<p>Let’s talk about the theoretical issue with migrating applications into a multiple-document environment. We used to highlight this nicely, back when we first started talking about MDI in AutoCAD 2000. We demonstrated a simple ObjectARX application that defined an alternative RECTANGLE command. The code used in the demo can still be found in the ObjectARX SDK, under <em>samples\editor\rect</em>. The original command&nbsp; - as defined in the badrectang.cpp file – makes use of static member variables to store information globally about the width of the rectangle's line segments, its fillet radius or chamfer (if applicable), etc.</p>

<p>There were three versions of the application that we showed: the first was a standard SDI application, the second was the same code simply declaring itself to be MDI-aware, and the third was a fully ported, MDI-aware application.</p>

<p><strong>The SDI Version</strong></p>

<p>When loaded, the first version of the application forces AutoCAD into SDI mode (the equivalent of the SDI variable being set to 1). That’s because modules need to declare themselves as MDI-aware by calling AcRxDynamicLinker::registerAppMDIAware() or acrxRegisterAppMDIAware() during their AcRx::kInitAppMsg handler (an initialization message received in a module’s acrxEntryPoint). Incidentally, if you’re using the ObjectARX Wizard these days you probably won’t see this call, as it’s hidden down in the guts of AcRxDbxApp::On_kInitAppMsg().</p>

<p>Here’s what you get when you try to load an SDI module into AutoCAD:</p><blockquote dir="ltr"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">ARX application : C:\Program Files\Autodesk\ObjectARX 2007\samples\editor\rect\Rectang.arx is NOT MDI aware.</p></div></blockquote><p>For a number of releases there was even a frowny face - :-( - in this message, but thankfully that was eventually removed (and none too soon).</p>

<p>This first version of the module forces AutoCAD into SDI mode – and AutoCAD takes a lowest common denominator approach in this regard – you would then have to unload the module to allow AutoCAD to return to MDI mode. Loading an SDI-only module changes the value of the SDI variable to be 2 (or 3, if the variable was previously set to 1). Being forced into SDI mode means that when a new drawing is created or loaded into the editor, AutoCAD needs to close the existing, open document. It also means there has to be exactly one document open in the editor at all times – you cannot close it to go to zero-doc mode.</p>

<p>Having this compatibility mode for applications that were not MDI-aware saved developers from having to invest immediately in an MDI port, but be warned – the variable is documented as no longer being supported, and it will go away at some point:</p><blockquote dir="ltr"><p>“Note In future releases of AutoCAD, the SDI system variable will be removed. At present, SDI is available but it is not supported.</p>

<p>Some commands and features are not available when you operate in single document interface mode.”</p></blockquote><p>So moving on…</p>

<p><strong>The Falsely MDI-Aware Version</strong></p>

<p>The next version of the application adds a call to acrxRegisterAppMDIAware(), and was really the fun one to demo. The code still makes use of static variables for all its settings, which raises the potential for very strange application behaviour. Let’s take this example:</p>

<p>Start drawing a rectangle in one drawing. You select one corner, and AutoCAD prompts for the second.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=496,height=445,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/rectangle_1a.png"></a></p>

<p><a onclick="window.open(this.href, '_blank', 'width=496,height=445,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/rectangle_1a_1.png"><img title="Rectangle_1a_1" height="269" alt="Rectangle_1a_1" src="/assets/rectangle_1a_1.png" width="300" border="0" /></a> </p>

<p>Rather than selecting it, you switch to another drawing loaded into the editor. This, in itself, is not a problem, but in this drawing you can launch the rectangle command again – while it is still active in the first drawing – and change the values of global variables that all instances of the command depend upon.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=497,height=444,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/rectangle_1b.png"><img title="Rectangle_1b" height="268" alt="Rectangle_1b" src="/assets/rectangle_1b.png" width="300" border="0" /></a> </p>

<p>So you can add a thickness to the second rectangle, which changes the global variable, and create some strange effects in the first rectangle – depending on the application logic for drawing it. Switching back, here’s an example of the funny behaviour you see: the fillet has been partially applied and the corner has jumped… all very peculiar.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=496,height=445,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/rectangle_1c.png"><img title="Rectangle_1c" height="269" alt="Rectangle_1c" src="/assets/rectangle_1c.png" width="300" border="0" /></a> </p>

<p><strong>The Properly MDI-Aware Version</strong></p>

<p>This is the fully ported version of the command, and behaves just like the RECTANG command in AutoCAD.</p>

<p>The trick is to maintain a set of variables representing the command’s settings (chamfer distance, fillet radius, segment width, etc.) for each open document.</p>

<p>The documentation regarding MDI-enabling your application talks about encapsulating your data into a class (fair enough) but then goes on to describe how to create a <a href="http://en.wikipedia.org/wiki/Linked_list">linked list</a> of these objects, one per document (ugh). At the time I remember finding this inelegant, so got to work on a utility class that made use of a more modern – and better suited – data structure, a map (which ultimately uses a <a href="http://en.wikipedia.org/wiki/Hash_table">hash table</a>).</p>

<p>At the time I hadn’t spent a great deal of time working with templates or maps, so my first attempt – although quite functional – was a mess. I had used an MFC container class to implement it, and it stretched over a number of header and implementation files. All proud, I circulated it to my colleagues around the world, and within a few hours received back a beautifully crafted version based on STL that took all of about 10 lines. That version – or a slightly tweaked version of it – is still in the SDK, in the AcApDMgr.h file. Here’s the relevant code from that header:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#include</span> <span style="COLOR: maroon">&quot;acdocman.h&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">template</span> &lt;<span style="COLOR: blue">class</span> T&gt; <span style="COLOR: blue">class</span> AcApDataManager : <span style="COLOR: blue">public</span> AcApDocManagerReactor {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span>:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; AcApDataManager () {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acDocManager-&gt;addReactor (<span style="COLOR: blue">this</span>) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ~AcApDataManager () {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> ( acDocManager != NULL )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;acDocManager-&gt;removeReactor (<span style="COLOR: blue">this</span>) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">virtual</span> <span style="COLOR: blue">void</span> documentToBeDestroyed (AcApDocument *pDoc) {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; m_dataMap.erase (pDoc) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; T &amp;docData (AcApDocument *pDoc) {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; std::map&lt;AcApDocument *, T&gt;::iterator i =m_dataMap.find (pDoc) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> ( i == m_dataMap.end () )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> (m_dataMap [pDoc]) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> ((*i).second) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; T &amp;docData () {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">return</span> (docData (acDocManager-&gt;curDocument ())) ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">private</span>:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; std::map&lt;AcApDocument *, T&gt; m_dataMap ;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">} ;</p></div>

<p>This helpful little template class allows you very easily to add per-document data to your application.</p>

<p>The first step we showed in the demo was the encapsulation of the data. The “bad” version of the app makes this easy for us, as it has the variables already encapsulated:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">class</span> CRectInfo {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span>:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; CRectInfo();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// First point selection.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcGePoint3d&nbsp; &nbsp; m_topLeftCorner;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// First Chamfer distance.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_first;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Second Chamfer distance.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_second;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Bulge value.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_bulge;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Elevation.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_elev;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Thickness.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_thick;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Width.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_width;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Fillet radius.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">double</span>&nbsp; m_radius;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Filleting or chamfering.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">bool</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_cornerTreatment;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">bool</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_elevHandSet;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Vector of chamfer.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcGeVector3d&nbsp; &nbsp; m_chamfDirUnitVec;&nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">};</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Definition in file scope.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">//</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_first;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_second;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_bulge;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_elev;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_thick;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_width;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">double</span> CRectInfo::m_radius; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">static</span> CRectInfo plineInfo;</p></div>

<p>This gets changed to:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">class</span> CRectInfo {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span>:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; CRectInfo();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// First point selection.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcGePoint3d&nbsp; &nbsp; m_topLeftCorner;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// First Chamfer distance.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_first;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Second Chamfer distance.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_second;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Bulge value.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_bulge;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Elevation.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_elev;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Thickness.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_thick;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Width.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_width;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Fillet radius.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">double</span>&nbsp; m_radius;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Filleting or chamfering.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">bool</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_cornerTreatment;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">bool</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;m_elevHandSet;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// Vector of chamfer.</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; AcGeVector3d&nbsp; &nbsp; m_chamfDirUnitVec;&nbsp; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">};</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">AcApDataManager&lt;CRectInfo&gt; rectDataMgr; <span style="COLOR: green">// MDI Safe</span></p></div>

<p>An important note is that the class needs to initialize its members properly in its constructor. The reason for this is mainly around the way the map works: when you first query for the custom data related to a particular document, if the document is not found as a key in the map, a new instance of the custom data class is created and returned, right there. So you need to make sure its default settings make sense.</p>

<p>Otherwise, there's not that much more to explain. All references of the old class' members, such as plineInfo.m_first - now need to make use of the AcApDataManagar class instance to access the data. To make life really, really easy, we used a #define to save the search &amp; replace:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#define</span> plineInfo rectDataMgr.docData()</p></div>

<p>This means plineInfo.m_first will automatically be expanded by the pre-processor to rectDataMgr.docData().m_first. The docData() call gets the right data container object for the current document. If you want the data for another document, just pass the AcApDocument pointer into docData().</p>

<p>One finaly point to mention: the SDK sample was added prior to the AcApDocManager class being officially added to the inc folder, so it uses its own version of the class, called AsdkDataManager defined in a local DataMgr.h file. You may also come across Wizard-generated projects creating local instances of this file (recent versions just use the AcApDMgr.h), along with DocData.h/.cpp files, ready to be populated with your application's per-document data.</p>

<p>Next up is how to handle per-document data in .NET. And don't worry - that'll be a much shorter topic... :-)</p>

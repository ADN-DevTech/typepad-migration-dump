---
layout: "post"
title: "Automatic closing of AutoCAD objects with ObjectARX SmartPointers"
date: "2008-04-03 13:54:37"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://www.keanw.com/2008/04/automatic-closi.html "
typepad_basename: "automatic-closi"
typepad_status: "Publish"
---

<p><em>Thank you to Fenton Webb, from DevTech Americas, for writing this article for the recently published ADN Platform Technologies Customization Newsletter. This article also talks about the new AcDbSmartObjectPointer class referenced in this overview of the <a href="http://through-the-interface.typepad.com/through_the_interface/2008/03/new-apis-in-aut.html" target="_blank">new APIs in AutoCAD 2009</a>.</em></p>

<p>Those of us who regular create ObjectARX code to manipulate the AutoCAD drawing database are fully aware of the mechanism for opening an object for read (to simply access data held inside it) or write (to update it with new data). Oh and I almost forgot - followed by a call to close() when you are done.</p>

<p>But here lies a very common problem illustrated by that last sentence; the problems start when you accidentally forget to close an object once you are finished with it. AutoCAD follows a strict set of rules which allows the checking-in/out of data inside of AutoCAD and these rules must be adhered to. If not, then AutoCAD will abort in order to do its best to save the previous valid state of the database.</p>

<p>“You must be very careful to close your objects when you are finished with them”. It’s very easy for me to say that, but even I, the person shaking his finger saying those infamous words will fail to remember my own advice time and time again, this is why using ObjectARX SmartPointers is a MUST.</p>

<p>So let’s look at this thing in ObjectARX called SmartPointers.</p>

<p>What are they? First take a look at the MSDN article on “<a href="http://msdn2.microsoft.com/en-us/library/y097fkab(VS.80).aspx" target="_blank">Template Classes</a>” as this explains the basic concept. Leading on from that article, and now in my own words, ObjectARX SmartPointers are C++ template classes which wrap an underlying AutoCAD AcDbObject derived class pointer, and simply provides automatic closure of that pointer, if valid, on destruction of the ObjectARX SmartPointer class (so the end of a function or closing brace “}”).</p>

<p>A question that often arises is on the usage of this class, in particular the way to access the member functions. The template class itself has been implemented so that if you reference a member function with the dot “.” operator<span style="FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: &quot;Arial&quot;,&quot;sans-serif&quot;"> </span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line.openStatus()</span></p>

<p>then, you reference the ObjectARX SmartPointer specific functions. If you reference a member function with the arrow “-&gt;” operator</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setStartPoint()</span></p>

<p>Then, because the arrow operator has been overridden to return the underlying AcDbObject pointer, you simply reference the underlying AcDbObject derived class, in this case the AcDbLine::setStartPoint().</p>

<p>So how do we use them then…? Let’s start by showing old ObjectARX code which adds an AcDbLine to the Current Space using open and close.</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// create a new line</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">AcDbLine *line = </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">new</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> AcDbLine();</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// set the properties for it</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setStartPoint(AcGePoint3d(10,10,0));</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setEndPoint(AcGePoint3d(20,30,0));</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// now add it to the current space</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">AcDbBlockTableRecord *curSpace = NULL;</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// open the current space for write</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">Acad::ErrorStatus es =</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; acdbOpenObject(</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; (AcDbBlockTableRecord *&amp;)curSpace, </span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; curDoc()-&gt;database()-&gt;currentSpaceId(),</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; AcDb::kForWrite</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; );</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// if ok&nbsp; &nbsp;&nbsp; &nbsp;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (es == Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">{</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// add it to the space</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; es = curSpace-&gt;appendAcDbEntity(line);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// check that everything was ok</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (es != Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; {</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">delete</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> line;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">return</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; }</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// now close everything</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; line-&gt;close();</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; curSpace-&gt;close();</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">}</span></p><br /><p>It's the 2 close statements at the end which are, first of all, very easy to forget to put in, but also notice they return just before which indicates a very rare failure, but just as importantly (and erroneously) bypasses the close of curSpace.</p>

<p>This is where ObjectARX SmartPointers not only provide automatic closure and cleanup but also peace of mind…</p>

<p>Let’s take a look at the same code, but this time using ObjectARX SmartPointers.</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// create a new line</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">AcDbObjectPointer&lt;AcDbLine&gt; line;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line.create();</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// set the properties for it</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setStartPoint(AcGePoint3d(10,10,0));</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setEndPoint(AcGePoint3d(20,30,0));</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// now add it to the current space</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">AcDbBlockTableRecordPointer</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; curSpace(</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; curDoc()-&gt;database()-&gt;currentSpaceId(), </span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; AcDb::kForWrite</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; );</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// if ok</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (curSpace.openStatus() == Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">{</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; Acad::ErrorStatus es =</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; curSpace-&gt;appendAcDbEntity(line);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// check that everything was ok</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (es != Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; {</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// no need for a delete as the smartpointer does this for us</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">return</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; }</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">}</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// everything will be closed automatically for us</span></p><br /><p>Not only is this ObjectARX code &quot;close&quot; safe, it is also memory leak-safe. Also, look how much tidier it is. Much more friendly in my opinion!</p>

<p>Here’s some more SmartPointer code which selects an Entity on screen and opens it for read, just as an example.</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">ads_name ename;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">ads_point pt;</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// pick an entity to check</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">int</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> res = acedEntSel (_T(</span><span style="FONT-SIZE: 8pt; COLOR: maroon; FONT-FAMILY: &quot;Courier New&quot;">&quot;\nPick a Line : &quot;</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">), ename, pt);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// if the user didn't cancel</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (res == RTNORM)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">{</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; AcDbObjectId objId;</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// convert the ename to an object id</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; acdbGetObjectId (objId, ename);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// open the entity for read</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; AcDbObjectPointer&lt;AcDbLine&gt;ent (objId, AcDb::kForRead);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// if ok</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (ent.openStatus () == Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; {</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; AcGePoint3d startPnt;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; ent-&gt;startPoint(startPnt);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// do something</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; }</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">}</span></p><br /><p>But what if you have reams and reams of existing code using old-style open and close, and you want to migrate to ObjectARX Smart Pointers with the least amount of effort? Well, we’ve tried to make it easy for you. Since ObjectARX 2007, in dbobjptr.h simply uncomment the #define DBOBJPTR_EXPOSE_PTR_REF and now life should be easy! (Well, with one exception - see **NOTE below).</p>

<p>Here’s the converted version of the original code we used at the beginning, converting to use ObjectARX SmartPointers couldn’t be easier (I’ve highlighted the changes in <strong>bold</strong>).</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// create a new line</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><strong><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">AcDbObjectPointer</span></strong><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&lt;AcDbLine&gt; line = </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">new</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> AcDbLine();</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// set the properties for it</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setStartPoint(AcGePoint3d(10,10,0));</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">line-&gt;setEndPoint(AcGePoint3d(20,30,0));</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// now add it to the current space</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><strong><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">AcDbBlockTableRecordPointer</span></strong><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> curSpace = NULL;</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// open the current space for write</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">Acad::ErrorStatus es =</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; acdbOpenObject(</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; (AcDbBlockTableRecord *&amp;)curSpace,</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; curDoc()-&gt;database()-&gt;currentSpaceId(), </span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; AcDb::kForWrite</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; );</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// if ok</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (es == Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">{</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// add it to the space</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; es = curSpace-&gt;appendAcDbEntity(line);</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// check that everything was ok</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">if</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> (es != Acad::eOk)</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; {</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">delete</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;"> line;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">return</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">;</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; }</span></p><br /><p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// now close everything</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; line-&gt;close();</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; curSpace-&gt;close();</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">} </span></p>

<p>Notice that I didn’t bother to remove the two close() calls at the end, there’s no need. If you close them by hand, or forget, it’s all good with ObjectARX SmartPointers.</p>

<p>**NOTE: So, in order to get the acdbOpenObject to accept the same code as before, in dbobjptr.h, at line 467 (ObjectARX 2009 SDK), there is an assert which needs to be omitted; either #define NDEBUG or I recommend that you simply change the assert to be enclosed by the #ifndef DBOBJPTR_EXPOSE_PTR_REF</p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span lang="FR-CH" style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR-CH">AcDbObjectPointerBase&lt;T_OBJECT&gt;::object(){</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span lang="FR-CH" style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR-CH">&nbsp; </span><span lang="FR-CH" style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR-CH">#ifndef</span><span lang="FR-CH" style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR-CH"> DBOBJPTR_EXPOSE_PTR_REF</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span lang="FR-CH" style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;; mso-ansi-language: FR-CH">&nbsp; &nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">assert(m_status == Acad::eOk);</span></p>

<p style="BACKGROUND: white; MARGIN: 0in 0in 0pt"><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp; </span><span style="FONT-SIZE: 8pt; COLOR: blue; FONT-FAMILY: &quot;Courier New&quot;">#endif</span><span style="FONT-SIZE: 8pt; COLOR: black; FONT-FAMILY: &quot;Courier New&quot;">&nbsp;</span><span style="FONT-SIZE: 8pt; COLOR: green; FONT-FAMILY: &quot;Courier New&quot;">// DBOBJPTR_EXPOSE_PTR_REF</span></p><br /><p>Last but not the least is the new AcDbSmartObjectPointer template class in ObjectARX 2009, defined in the header file dbobjptr2.h.</p>

<p>This new template class works in the same way as AcDbObjectPointer template class except that it works by NOT opening an object at all if its open state is already what was requested, or even closing an object multiple times before opening in the desired manner. It merely hands you the already opened object pointer for your use. This means that it is much more efficient and also much more powerful in its usage. It also treats kForNotify and kForRead in the same manner, which is effectively kForRead. </p>

<p>One feature of this new SmartPointer class that I’d like to talk about is the ability to multiply open an object for write, from different places, at the same time, a bit like a Transaction can – this is extremely powerful when you think about it. </p>

<p>At the same time though, I find thinking about the power that this can provide can start generating some other complex thoughts and scenarios that maybe we should be cautious of; the bottom line is that you should be very careful about multiply opening an object for write no matter how good the class that controls it. </p>

<p>An example of where this type of functionality really might be useful to us developers is in say an Object Reactor callback. Quite often you might want to modify the current object’s state but of course you can’t because it is already open for notify. Using this new SmartPointer class it makes it possible to modify the object as you see fit in this context, but be careful to handle the recursive object modified notifications that will be fired by doing this.</p>

<p>All in all a very exciting new addition to the ObjectARX API, make sure you check it out.</p>

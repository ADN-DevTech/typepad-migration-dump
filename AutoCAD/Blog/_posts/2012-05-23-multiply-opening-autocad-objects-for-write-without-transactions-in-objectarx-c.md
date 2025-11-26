---
layout: "post"
title: "Multiply Opening AutoCAD Objects for WRITE WITHOUT Transactions in ObjectARX C++"
date: "2012-05-23 07:57:36"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "ObjectARX"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/multiply-opening-autocad-objects-for-write-without-transactions-in-objectarx-c.html "
typepad_basename: "multiply-opening-autocad-objects-for-write-without-transactions-in-objectarx-c"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Remembering to close an Object after you have opened it in AutoCAD can be a pain, that&#39;s why we gave you the AcDbObjectPointer class template. However, what if you want the same autoclosing functionality, but with the added ability to multiply open objects for write just like you get with Transactions?</p>
<div class="sect_body_disp" id="cb2004_body_disp_14135899">
<p><strong>Introducing The all new AcDbSmartObjectPointer Template Class!!!</strong></p>
<p>Declared in dbobjptr2.h, this class is protocol-compatible with AcDbObjectPointer (so you can just replace AcDbObjectPointer with AcDbSmartObjectPointer) and has the added capability to avoid open conflicts to access an object when given an object id, in addition to the longstanding capability to always &quot;close&quot; an object or at least revert it to the open state it was in prior to being assigned to the pointer.</p>
<p>AcDbSmartObjectPointer works by NOT opening an object at all if it&#39;s open state is already what was requested, or even closing an object multiple times before opening in the desired manner. It also treats kForNotify and kForRead in the same manner, which is effectively kForRead. While this doesn&#39;t make the class foolproof, it does eliminate open conflicts cause of failure to obtain access to objects.</p>
<p>Some of the remaining conditions that can still cause failure to open are as follows, indicated by the associated errors:</p>
<ul>
<li>eNotThatKindOfClass Returned when the specified object id points to an object that is not the specified class. </li>
<li>ePermanentlyErased Returned when the specified object id has no underlying object, whether due to undo of creation or erase/delete. </li>
<li>eObjectWasErased Returned if the object is erased and the openErased flag is false. </li>
<li>eNullObjectId Returned when the input object id is null. </li>
<li>eWasNotifying Returned when the specified object is currently closing from kForWrite mode, is sending notification and kForWrite mode is requested again. At that point undo recording has been done, and all reactors get to see the same object state. The workaround for this status is to record that the notification happened then wait until the AcDbObject::objectClosed(AcDbObjectId) callback is made, at which point the object can be opened for write. </li>
</ul>
<p>Like AcDbObjectPointer, this template can also &quot;acquire&quot; non-database-resident (NDBR) objects and previously open objects, which it will close just once. If the DBOBJPTR_EXPOSE_PTR_REF option is enabled, then accessing the object pointer member disables the template&#39;s ability to symmetrically close or revert the object. Instead it is closed once, and the caller is responsible for it afterwards.</p>
<p>All members of this template have the same description and semantics of AcDbObjectPointer&#39;s corresponding members, except that open conflicts are eliminated.</p>
<p>One note before I leave this little topic, just to say that it is not always good to modify an object when another caller has it open for write. However, if you do want to use instances of this class to obtain read or write access to objects that it may already have open and knows that the interaction is safe, then use of this class is highly recommended by me. <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_564541.jpg" style="border-style: none;" /></p>
</div>

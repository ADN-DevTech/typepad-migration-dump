---
layout: "post"
title: "_COM_SMARTPTR_TYPEDEF declaration problem with #import"
date: "2012-06-27 21:59:14"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/_com_smartptr_typedef-declaration-problem-with-import.html "
typepad_basename: "_com_smartptr_typedef-declaration-problem-with-import"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>In the RxInventor.tlh the definition of some named interface object are declared like this:   <br /><em>&#160;&#160; _COM_SMARTPTR_TYPEDEF(GenericObject, __uuidof(IDispatch));</em>    <br />It is clear the IDispatch UUID is not one of the interface. This cause a lot of problem in our code.</p>  <p>Can you explain this?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>The problem with the smart pointer typedef generation is a problem with Microsoft's implementation of #import and there is nothing we can do about it. All of Inventor's IDispatch supporting interfaces are dispinterfaces, not dual interfaces like AutoCAD's. #import appears to only use the strong IID when the interface is a vtable or a dual. #import puts on dispinterfaces, and uses IDispatch as the interface IID.</p>  <p>This is just another of several reasons to avoid the _com_ptr_t smart pointers, especially since #import only half declares them. It is recommended to stick with CCom(QI)Ptr or CComPtr&lt;&gt; template class, since you will specify the right interface uuid.</p>

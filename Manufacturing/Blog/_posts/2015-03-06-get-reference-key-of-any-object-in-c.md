---
layout: "post"
title: "Get reference key of any object in C++"
date: "2015-03-06 17:57:27"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/03/get-reference-key-of-any-object-in-c.html "
typepad_basename: "get-reference-key-of-any-object-in-c"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Many different types of objects support the <strong>GetReferenceKey</strong> function that enables you to store information about a given object that will enable you to find them again using the <strong>ReferenceKeyManager</strong> of the <strong>Document</strong>. If you wanted to support all of them using <strong>early-binding</strong>, i.e. you explicitly declare all the object types you are supporting, that would be a lot of work. It&#39;s probably better to use<strong> late-binding</strong> in such a case - i.e. we do not care what type of object we are dealing with, we&#39;ll try to call <strong>GetReferenceKey</strong> on it. If it supports it then it will execute fine, otherwise the function will not be found on it.&#0160;</p>
<p>Just as it was discussed in <a href="http://adndevblog.typepad.com/manufacturing/2013/09/run-ilogic-rule-from-external-application.html" target="_self">this post</a>, in <strong>.NET</strong> it&#39;s quite easy now to do <a href="http://en.wikipedia.org/wiki/Late_binding" target="_self">late binding</a>, but in <strong>C++</strong> it&#39;s not, so we&#39;ll use the&#0160;<strong>AutoWrap</strong> function that was introduced in the other blog post.</p>
<p>It could also be tricky to figure out what exact <strong>VARIANT</strong> type combination a function expects. The simplest might be to check the&#0160;<strong>rxinventor.tli</strong> file that the compiler creates for us when our project is referencing the inventor type library. There you&#39;ll find all the functions, including this as well:</p>
<pre>#pragma implementation_key(7455)
inline HRESULT Sheet::<strong>GetReferenceKey</strong> ( 
  SAFEARRAY * * ReferenceKey, long KeyContext ) {
    return _com_dispatch_raw_method(
      this, 0x7f000016, DISPATCH_METHOD, VT_EMPTY, NULL, 
      <strong>L&quot;\x6011\x0003&quot;, ReferenceKey, KeyContext</strong>);
}</pre>
<p>It shows what types the <strong>IDispatch::Invoke</strong> will require for the given function. <strong>ReferenceKey</strong>&#0160;parameter&#39;s <strong>VARIANT</strong> type should be hexadecimal <strong>0x6011</strong>. You can check the values e.g. from the &quot;c:\Program Files (x86)\Windows Kits\8.0\Include\shared\<strong>wtypes.h</strong>&quot; file. Some values there are in decimal and some in hexadecimal, so easiest is to convert all of them to hexadecimal:<br /><strong>-</strong> <strong>ReferenceKey</strong>&#39;s type is&#0160;<strong>0x6011</strong>.<strong><strong>&#0160;</strong>VT_BYREF</strong> = <em>0x4000</em>,&#0160;<strong>VT_ARRAY</strong> = <em>0x2000</em>, <strong>VT_UI1</strong> = 17 = <em>0x0011</em>. If you add them together it&#39;s <strong>0x6011</strong><br /><strong>- KeyContext</strong>&#39;s type is <strong>0x003</strong>, which is&#0160;<strong>VT_I4</strong>.</p>
<p>If we already have an object e.g. a <strong>Face</strong> stored in <strong>pFace</strong>&#0160;and the context index in <strong>lContext</strong> then this is how we could get the face&#39;s reference key. This code shows how you could do it with <strong>SAFEARRAY</strong> or <strong>CComSafeArray</strong>:</p>
<pre>	// This is what the early-biding version would 
	// look like
	SAFEARRAY * refKeys = NULL;
	pFace-&gt;GetReferenceKey(&amp;refKeys, lContext);

        // This is the late-binding version

        // Important to set myarray to NULL otherwise we could
        // get an error
	SAFEARRAY * myarray = NULL; 
	VARIANT param1;
	VariantInit(&amp;param1);
	param1.vt = VT_ARRAY | VT_BYREF | VT_UI1;
	param1.pparray = &amp;myarray;

	VARIANT param2;
	VariantInit(&amp;param2);
	param2.vt = VT_I4;
	param2.lVal = lContext;

	VARIANT varResult;
	VariantInit(&amp;varResult);
	Result = AutoWrap(
	  DISPATCH_METHOD, &amp;varResult, pFace, 
          _T(&quot;GetReferenceKey&quot;), 
	  2, param2, param1);

	// You can also wrap the result in CComSafeArray
        // Need to include &quot;atlsafe.h&quot;
	CComSafeArray&lt;byte&gt; saRefKey;
	Result = saRefKey.Attach(myarray);</pre>
<p>It&#39;s useful to point out, because I missed it too before :), that the value you get back from <strong>CreateKeyContext</strong> and <strong>LoadContextFromArray</strong> is just an index of the context data, not the data itself. So if you want to make sure that you can find your <strong>BREP</strong> object in another session, then you&#39;ll have to follow all the steps mentioned in <a href="http://adndevblog.typepad.com/manufacturing/2012/12/maintaining-persistent-links-to-objects-across-inventor-sessions-using-referencekeys.html">this post</a>: get the context data using&#0160;<strong>SaveContextToArray</strong> and then save it somewhere.</p>

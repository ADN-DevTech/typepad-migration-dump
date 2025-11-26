---
layout: "post"
title: "Retrieving entity properties using AccoreConsole"
date: "2015-03-20 02:46:04"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/retrieving-entity-properties-using-accoreconsole.html "
typepad_basename: "retrieving-entity-properties-using-accoreconsole"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The "dumpallproperties" is an easy way to list all the properties of an entity using Lisp. Unfortunately, this does not work when used in the script file along with AccoreConsole.&nbsp;A simple way to workaround this and display the entity properties in AccoreConsole, is to create a CRX plugin that uses the Non-COM property system to display the entity properties.&nbsp;</p>
<p>If you are new to the Non-COM property system, please refer to this blog post : <a href="http://adndevblog.typepad.com/autocad/2012/04/devtv-non-com-property-system.html">DevTV : Non-COM Property System </a></p>
<p>The code in this blog post uses portions of the code from that DevTV in a CRX application.&nbsp;Here is the relevant code and the complete sample project can be downloaded here :</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb080b1e38970d img-responsive"><a href="http://adndevblog.typepad.com/files/entprops_crx.zip">Download EntProps_CRX</a></span></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ListEntityProperties(<span style="color:#0000ff">void</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	TCHAR entityHandle[133];</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (acedGetString(</pre>
<pre style="margin:0em;"> 		Adesk::kFalse, </pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;\\nEnter entity handle : &quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		entityHandle) != RTNORM)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\nInvalid entity handle.&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	CUtils::DisplayProperties(entityHandle);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  initApp() </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;">     acedRegCmds-&gt;addCommand(ACRX_T(<span style="color:#a31515">&quot;MY_COMMANDS&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;">                             ACRX_T(<span style="color:#a31515">&quot;EntProps&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;">                             ACRX_T(<span style="color:#a31515">&quot;EntProps&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;">                             ACRX_CMD_TRANSPARENT, </pre>
<pre style="margin:0em;">                             ListEntityProperties); </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;rxmember.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;rxvaluetype.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;rxattrib.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;rxprop.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;dbobjptr.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Utils.cpp</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  CUtils::DisplayProperties(LPCTSTR handleStr)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> 	AcApDocument *pActiveDoc </pre>
<pre style="margin:0em;"> 		= acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin:0em;"> 	AcDbDatabase *pDB = pActiveDoc-&gt;database();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId id = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 	AcDbHandle objHandle(handleStr);</pre>
<pre style="margin:0em;">     es = pDB-&gt;getAcDbObjectId(id, <span style="color:#0000ff">false</span><span style="color:#000000"> , objHandle);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (es != Acad::eOk) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         acutPrintf(</pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;\\nCould not translate handle to objectId&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectPointer&lt;AcDbEntity&gt; entity(id, AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcRxMemberIterator * iter = </pre>
<pre style="margin:0em;"> 		AcRxMemberQueryEngine::theEngine()-&gt;newMemberIterator</pre>
<pre style="margin:0em;"> 		(entity);   </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000">  (; !iter-&gt;done(); iter-&gt;next())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		printValues(entity, iter-&gt;current());</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  CUtils::getAttInfo(</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxAttribute * att, </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxObject * member, </pre>
<pre style="margin:0em;"> 	AcString &amp; attInfo)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (att-&gt;isA() == AcRxCOMAttribute::desc())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcRxCOMAttribute * a = AcRxCOMAttribute::cast(att);</pre>
<pre style="margin:0em;"> 		attInfo.format(_T(<span style="color:#a31515">&quot;\\n%s - %s&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 			att-&gt;isA()-&gt;name(), a-&gt;name()); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  (att-&gt;isA() == AcRxUiPlacementAttribute::desc())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcRxUiPlacementAttribute * a </pre>
<pre style="margin:0em;"> 			= AcRxUiPlacementAttribute::cast(att);</pre>
<pre style="margin:0em;"> 		attInfo.format(</pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;\\n%s - %s - %f&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		att-&gt;isA()-&gt;name(), </pre>
<pre style="margin:0em;"> 		a-&gt;getCategory(member),</pre>
<pre style="margin:0em;"> 		a-&gt;getWeight(member)); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		attInfo.format(_T(<span style="color:#a31515">&quot;\\n%s&quot;</span><span style="color:#000000"> ), att-&gt;isA()-&gt;name()); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  CUtils::printValues(</pre>
<pre style="margin:0em;"> 	AcRxObject * entity, </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxMember * member)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus err = Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcString strValue;</pre>
<pre style="margin:0em;"> 	AcRxProperty * prop = AcRxProperty::cast(member);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (prop != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcRxValue value;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  ((err = prop-&gt;getValue(entity, value)) </pre>
<pre style="margin:0em;"> 			== Acad::eOk) </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			ACHAR * szValue = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">int</span><span style="color:#000000">  buffSize = value.toString(NULL, 0);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000">  (buffSize &gt; 0)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				buffSize++;</pre>
<pre style="margin:0em;"> 				szValue = <span style="color:#0000ff">new</span><span style="color:#000000">  ACHAR[buffSize];</pre>
<pre style="margin:0em;"> 				value.toString(szValue, buffSize);</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			strValue.format(</pre>
<pre style="margin:0em;"> 				_T(<span style="color:#a31515">&quot;%s = %s&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 				value.type().name(), </pre>
<pre style="margin:0em;"> 				(szValue == NULL) ? _T(<span style="color:#a31515">&quot;none&quot;</span><span style="color:#000000"> ) : szValue);  </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000">  (szValue)</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">delete</span><span style="color:#000000">  szValue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			strValue.format(_T(<span style="color:#a31515">&quot;Error Code = %d&quot;</span><span style="color:#000000"> ), err);</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcString str;</pre>
<pre style="margin:0em;"> 	str.format(_T(<span style="color:#a31515">&quot;\\n%s - %s [%s]&quot;</span><span style="color:#000000"> ), member-&gt;isA()-&gt;name(), </pre>
<pre style="margin:0em;"> 		member-&gt;name(), strValue.kACharPtr());</pre>
<pre style="margin:0em;"> 	acutPrintf(str);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxAttributeCollection &amp; atts </pre>
<pre style="margin:0em;"> 		= member-&gt;attributes();</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  i = 0; i &lt; atts.count(); i++)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxAttribute * att = atts.getAt(i); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcString attInfo;</pre>
<pre style="margin:0em;"> 		getAttInfo(att, member, attInfo);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		acutPrintf(attInfo);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (member-&gt;children() != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  i = 0; </pre>
<pre style="margin:0em;"> 			i &lt; member-&gt;children()-&gt;length(); i++)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">const</span><span style="color:#000000">  AcRxMember * subMember </pre>
<pre style="margin:0em;"> 				= member-&gt;children()-&gt;at(i);</pre>
<pre style="margin:0em;"> 			printValues(entity, subMember);  </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
</p>
<p>The CRX plugin exposes the "EntProps" command which requires handle of the entity for which the properties are to be retrieved as its input.</p>
<p>Here is a sample script file to load the crx and invoke "EntProps" command.</p>
<p style="color:blue">(arxload "D:\\Temp\\CrxTest1.crx")<br />EntProps<br />4C4 &nbsp;&nbsp;;; Handle of the entity for which properties are to be displayed</p>
<p></p>

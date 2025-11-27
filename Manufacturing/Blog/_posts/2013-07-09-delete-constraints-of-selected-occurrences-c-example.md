---
layout: "post"
title: "Delete Constraints of selected Occurrences c++ example"
date: "2013-07-09 15:21:54"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/delete-constraints-of-selected-occurrences-c-example.html "
typepad_basename: "delete-constraints-of-selected-occurrences-c-example"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p>This C++ sample uses the selection set and deletes constraints found in the selected occurrences and their suboccurrences. When manipulating entities in the SelectSet, keep in mind that any change in the database resets the set. To avoid problems store the entities you want to change. This example uses an ObjectCollection to store the selected entities. </p>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01901e31a79b970b"><a href="http://adndevblog.typepad.com/files/delete_constraints_test.zip">Download Delete_Constraints_Test</a></span>&#0160;</p>
<p>Here is the code that stores the selected components:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">CComPtr&lt;ObjectCollection&gt; ObjColl = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; TransObj-&gt;CreateObjectCollection(vopt);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">for</span> (<span style="color: blue;">int</span> index = 1; index &lt;= count; index++)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// it could even be just a folder</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//get the selected entity</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComPtr&lt;IDispatch&gt; SelectedEntity = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SelSet-&gt;GetItem(index);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ObjColl-&gt;Add(SelectedEntity);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; } <span style="color: blue;">catch</span> (...) {}</p>
<p style="margin: 0px;">}</p>
</div>
<p>Here is the code that deletes the constraints:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">void</span> CTestDlg::deleteConstraints</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ComponentOccurrence* CompOcc, </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">bool</span> AlsoInSubOccurrences)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; CComPtr&lt;ComponentOccurrencesEnumerator&gt; </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SubOccs = NULL;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; CComPtr&lt;AssemblyConstraintsEnumerator&gt;&#0160; </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AssCons = CompOcc-&gt;GetConstraints();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">long</span> count = AssCons-&gt;GetCount();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">while</span> (count &gt; 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComPtr&lt;AssemblyConstraint&gt; AssCon = </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AssCons-&gt;GetItem(CComVariant(count)); </p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AssCon-&gt;Delete();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AssCons = CompOcc-&gt;GetConstraints();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; count = AssCons-&gt;GetCount();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (!AlsoInSubOccurrences)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; SubOccs = CompOcc-&gt;GetSubOccurrences();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; count = SubOccs-&gt;GetCount();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">long</span> i = 1; i &lt;= count; i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComPtr&lt;ComponentOccurrence&gt;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; SubOcc = SubOccs-&gt;GetItem(i);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; deleteConstraints(SubOcc, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>

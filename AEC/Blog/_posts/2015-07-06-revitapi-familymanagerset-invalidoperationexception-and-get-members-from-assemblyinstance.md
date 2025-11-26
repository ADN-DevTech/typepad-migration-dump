---
layout: "post"
title: "RevitAPI: FamilyManager.Set - InvalidOperationException and get members from AssemblyInstance"
date: "2015-07-06 15:00:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/07/revitapi-familymanagerset-invalidoperationexception-and-get-members-from-assemblyinstance.html "
typepad_basename: "revitapi-familymanagerset-invalidoperationexception-and-get-members-from-assemblyinstance"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/46684833">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p><strong>Q: Would you please confirm it is not allowed to set value family parameter if there is no type?</strong></p>
<p>In Family Editor, when calling FamillyManager.Set(FamilyParameter), it will throw InvalidOperationException: There is no current type</p>
<p>the code looks like this:</p>
<pre class="csharp prettyprint">var para = RevitDoc.FamilyManager.AddParameter(&quot;Length&quot;, BuiltInParameterGroup.INVALID, ParameterType.Length, false);
RevitDoc.FamilyManager.Set(para, 123.1);</pre>
<p>It allows us to create &quot;parameter&quot; without any types, but not able to set value of parameters, because it should be meanless to set a parameter which is not belong to any type.</p>
<p>So, we should check if FamilyManager.CurrentType is null or not before calling &quot;.Set&quot; method.</p>
<pre class="csharp prettyprint">if (familyMgr.CurrentType == null)
    familyMgr.NewType(&quot;A new type&quot;);
var para = familyMgr.AddParameter(&quot;Length&quot;, BuiltInParameterGroup.INVALID, ParameterType.Length, false);
RevitDoc.FamilyManager.Set(para, 123.1);</pre>
<p><br /><strong>Q: How to get elements which are part of a selected assembly?</strong></p>
<p>As you may have already tried that RevitLookup did not show useful information of parts in an assembly you selected, but when you look at the methods of AssemblyInstance, you will see a set of methods related with its parts:</p>
<ul>
<li>ICollection GetMemberIds();</li>
<li>void AddMemberIds(ICollection memberIds);</li>
<li>void RemoveMemberIds(ICollection memberIds);</li>
<li>void SetMemberIds(ICollection memberIds);</li>
</ul>
<p>those methods allows to:</p>
<ul>
<li>Get members of the assembly</li>
<li>Add more members into the assembly</li>
<li>Remove a set of members</li>
<li>Set a list of elements as its members</li>
</ul>
<p>so easy now, use GetMemberIds()</p>

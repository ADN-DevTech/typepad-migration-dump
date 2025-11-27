---
layout: "post"
title: "Passing a string to Inventor API methods in .NET 2.0 throws a NullReferenceException"
date: "2012-08-09 14:48:07"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/passing-a-string-to-inventor-api-methods-in-net-20-throws-a-nullreferenceexception.html "
typepad_basename: "passing-a-string-to-inventor-api-methods-in-net-20-throws-a-nullreferenceexception"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>This is an old issue but still could help someone. In my tests using .NET 4.0 with Inventor 2013 does not have this problem. </p>  <p><b>Issue</b></p>  <p>Passing a string to the Inventor API methods in .NET 2.0 always throws a NullReferenceException. For example, the following code snippet produces a NullReferenceException:</p>  <pre><p>CustomTable oCustomTable = invSheet.CustomTables.Add</p><p> (&quot;My Table&quot;, locationPoint, 3, 3, ref columnTitles2,</p><p>  content, columnsWidth,rowHeight, &quot;&quot;);</p>
<p>&#160;</p></pre>

<p><strong>Solution</strong></p>

<p>This is a known issue with Microsoft.Net 2.0. You may refer to MSDN for more information. In .NET 2.0, during COM Interop, BSTRs are treated differently.The workaround for the problem is to use InvokeMember to call the Add method of CustomTables instead of directly calling it. Following code uses the InvokeMember to add the custom table.</p>

<pre><p>// Initialize a ParameterModifier with the number of parameters.<br />ParameterModifier p = new ParameterModifier(9);<br />// Pass the fifth(ColumnTitles) parameter by reference.<br />p[4] = true;<br />// The ParameterModifier must be passed as the single element<br />// of an array.<br />ParameterModifier[] mods = { p };<br />CustomTable oCustomTable = (CustomTable)invSheet.CustomTables.GetType()</p><p>.InvokeMember(&quot;Add&quot;, System.Reflection.BindingFlags.InvokeMethod,<br />null, invSheet.CustomTables, <br />new Object[] { &quot;My Table&quot;, locationPoint, 3, 3, oTemp, content, columnsWidth, rowHeight, &quot;&quot; },<br />mods,null,null);</p></pre>

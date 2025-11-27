---
layout: "post"
title: "How to use EnumType Object"
date: "2012-07-19 23:15:28"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/07/how-to-use-enumtype-object.html "
typepad_basename: "how-to-use-enumtype-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>By <strong>EnumType</strong> object, you could get all possible values of the various enumerations in Inventor API, like the UnitsTypeEnum, ObjectTypeEnum and so on. This object is accessed by</p>  <p>Application.TestManager while TestManager is a hidden object currently. This means you could use it but we do not support it. The code below dumps all values of ObjectTyp​eEnum.</p>  <p>Sub GetEnumType()</p>  <p>&#160; Dim oEnumType As EnumType   <br />&#160; Set oEnumType = ThisApplication.TestManager.GetEnumType(&quot;ObjectTyp​eEnum&quot;)    <br />&#160; Dim index As Integer    <br />&#160; index = 0    <br />&#160; Do While index &lt; oEnumType.Count    <br />&#160;&#160;&#160; Debug.Print oEnumType.ValueName    <br />&#160;&#160;&#160; Debug.Print oEnumType.Value</p>  <p>&#160;&#160;&#160; oEnumType.MoveNext</p>  <p>&#160;&#160;&#160; index = index + 1   <br />&#160; Loop</p>  <p>End Sub</p>

---
layout: "post"
title: "Delete Map 3D Object Data Table using ActiveX Automation API "
date: "2012-08-06 00:28:31"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/delete-map-3d-object-data-table-using-activex-automation-api-.html "
typepad_basename: "delete-map-3d-object-data-table-using-activex-automation-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Currently there is no method exposed through Map 3D ActiveX Automation API to delete the Object Data table. We can use the following MAP LISP API functions as a workaround (<strong>ade_oddeletetab </strong><em>tabname</em>)</p>
<p>This function deletes the specified table. You can use <strong>SendCommand</strong> method of document object to invoke this function. The following example shows how to remove a OD Table in Map 3D -&#0160;</p>
<p>&#0160;</p>
<p>Sub f_deleteOdtable(ps_odTableName As String)</p>
<p>&#0160; ThisDrawing.SendCommand (&quot;(ade_oddeletetab &quot;&quot;&quot; &amp; ps_odTableName &amp; &quot;&quot;&quot;) &quot;)</p>
<p>End Sub&#0160;</p>
<p>&#0160;</p>
<p>Sub f_test()</p>
<p>&#0160; Call f_deleteOdtable(&quot;test&quot;)</p>
<p>&#0160; &#39;you must have an object data table by name &quot;test&quot;</p>
<p>End Sub</p>
<p>&#0160;</p>

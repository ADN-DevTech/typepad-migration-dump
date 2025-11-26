---
layout: "post"
title: "Want to know which Object Data Tables are in the current drawing file?"
date: "2012-05-07 02:32:07"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/want-to-know-which-object-data-tables-are-in-the-current-drawing-file.html "
typepad_basename: "want-to-know-which-object-data-tables-are-in-the-current-drawing-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In your current DWG file, you might have many other DWG files attached. The current drawing as well as the attached drawing has many <em><strong>Object Data Tables</strong></em> defined in them and you want to find out the Object Data Table names only from the current DWG file. How do you do that?&#0160;</p>
<p>The following C# .NET code snippet demonstrates how to find out the Object Data Table names only from the current DWG file.<br /><br /></p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af;">Tables</span> odTables = <span style="color: #2b91af;">HostMapApplicationServices</span>.Application.ActiveProject.ODTables;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">StringCollection</span> curDbTables = <span style="color: blue;">new</span> <span style="color: #2b91af;">StringCollection</span>();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> curDb = <span style="color: #2b91af;">HostApplicationServices</span>.WorkingDatabase;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">StringCollection</span> allDbTables = odTables.GetTableNames();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AttachedDrawings</span> attachedDwgs = <span style="color: #2b91af;">HostMapApplicationServices</span>.Application.ActiveProject.DrawingSet.AllAttachedDrawings;</p>
<p style="margin: 0px;"><span style="color: blue;">int</span> directDWGCount = <span style="color: #2b91af;">HostMapApplicationServices</span>.Application.ActiveProject.DrawingSet.DirectDrawingsCount;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">foreach</span> (<span style="color: #2b91af;">String</span> name <span style="color: blue;">in</span> allDbTables)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Autodesk.Gis.Map.ObjectData.<span style="color: #2b91af;">Table</span> table = odTables[name];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">bool</span> bTableExistsInCurDb = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; directDWGCount; ++i)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">AttachedDrawing</span> attDwg = attachedDwgs[i];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">StringCollection</span> attachedTables = attDwg.GetTableList(Autodesk.Gis.Map.Constants.<span style="color: #2b91af;">TableType</span>.ObjectDataTable);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (attachedTables.Contains(name))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; bTableExistsInCurDb = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (bTableExistsInCurDb)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; curDbTables.Add(name);</p>
<p style="margin: 0px;">}&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">ed.WriteMessage(<span style="color: #a31515;">&quot;Current Drawing Object Data Tables Names :\r\n&quot;</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">foreach</span> (<span style="color: #2b91af;">String</span> name <span style="color: blue;">in</span> curDbTables)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ed.WriteMessage(name + <span style="color: #a31515;">&quot;\r\n&quot;</span>);</p>
<p style="margin: 0px;">}</p>
</div>
<p>&#0160;</p>
<p>Hope this helps :)</p>

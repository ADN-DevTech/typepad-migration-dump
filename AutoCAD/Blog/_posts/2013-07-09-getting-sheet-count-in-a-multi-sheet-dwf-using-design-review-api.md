---
layout: "post"
title: "Getting Sheet Count in a multi Sheet DWF using Design Review API"
date: "2013-07-09 00:01:52"
author: "Partha Sarkar"
categories:
  - "ADR API"
  - "DWF"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/getting-sheet-count-in-a-multi-sheet-dwf-using-design-review-api.html "
typepad_basename: "getting-sheet-count-in-a-multi-sheet-dwf-using-design-review-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Following
VBScript code snippet demonstrates how to get the sheet count and activating
each of them for view. This code snippet is part of the &quot;<a href="https://projectpoint.buzzsaw.com/constructionmanagement/public/Sample/Viewer_API_test.zip?public">Viewer
API Test</a>&quot; sample available in our Developer Center Page <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=5801750">DWF
Code samples</a>.</p>
<pre><span style="font-size: 10pt;">function ShowPages</span><br /><span style="font-size: 10pt;">    dim page</span><br /><span style="font-size: 10pt;">	MsgBox Eview.Viewer.Pages.count()</span><br /><span style="font-size: 10pt;">    for each page in Eview.Viewer.Pages</span><br /><span style="font-size: 10pt;">      Eview.Viewer.Page = page.Name</span><br /><span style="font-size: 10pt;">      call Eview.Viewer.WaitForPageLoaded()</span><br /><span style="font-size: 10pt;">      MsgBox Eview.Viewer.Page.Name</span><br /><span style="font-size: 10pt;">    next</span><br /><span style="font-size: 10pt;">end function</span></pre>
<p>&#0160;</p>

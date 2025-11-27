---
layout: "post"
title: "Add Workgroup search path at first position"
date: "2014-07-18 03:55:43"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/add-workgroup-search-path-at-first-position.html "
typepad_basename: "add-workgroup-search-path-at-first-position"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>It seems that using the <strong>WorkgroupPaths.Add(Name, Path, Index)</strong> function there is no way to insert the new path at the first position in the list. If you pass in <strong>0</strong> as <strong>Index</strong>, then it&#39;s placed at the end, if you pass in <strong>1</strong>, then it&#39;s placed at the second position.</p>
<p>The workaround is to remove the item at the first position and reinsert it at the second position (<strong>Index</strong> = <strong>1</strong>):</p>
<pre>Sub AddWorkGroupSearchPathAtFirstPosition()
  Dim dpm As DesignProjectManager
  Set dpm = ThisApplication.DesignProjectManager

  Dim dp As DesignProject
  Set dp = dpm.ActiveDesignProject
  
  &#39; Add the new path: this will place it at
  &#39; second position
  Call dp.WorkgroupPaths.Add(&quot;CommonspaceNew&quot;, &quot;C:\Temp&quot;, 1)
  
  &#39; Workaround: remove the one currently at the 1st position
  &#39; and then add it back as second
  Dim pp1 As ProjectPath
  Set pp1 = dp.WorkgroupPaths(1)
  Dim name As String
  Dim path As String
  name = pp1.name
  path = pp1.path
  Call pp1.Delete
  Call dp.WorkgroupPaths.Add(name, path, 1)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73def7c88970d-pi" style="display: inline;"><img alt="WorkgroupSearchPaths" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73def7c88970d image-full img-responsive" src="/assets/image_9b8563.jpg" title="WorkgroupSearchPaths" /></a></p>
<p>&#0160;</p>

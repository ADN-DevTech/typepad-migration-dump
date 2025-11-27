---
layout: "post"
title: "Reorder browser pane nodes"
date: "2014-06-20 07:31:21"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/reorder-browser-pane-nodes.html "
typepad_basename: "reorder-browser-pane-nodes"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In the UI you can drag&amp;drop some of the nodes in the <strong>Browser Pane</strong> to reorder them. You can do the same through the API. This sample swaps the order of the first two relationships in the assembly:</p>
<pre>Sub ReorderConstraints()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  Dim bp As BrowserPane
  Set bp = doc.BrowserPanes(&quot;AmBrowserArrangement&quot;)
  
  &#39; &quot;Relationships&quot; node
  Dim rs As BrowserNode
  Set rs = bp.TopNode.BrowserNodes(1)
  
  Dim bn1 As BrowserNode
  Set bn1 = rs.BrowserNodes(1)
  
  Dim bn2 As BrowserNode
  Set bn2 = rs.BrowserNodes(2)
  
  Dim bn2d As BrowserNodeDefinition
  Set bn2d = bn2.BrowserNodeDefinition
  
  Call bp.Reorder(bn1, True, bn2)
  
  Call bp.Update
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd22228d970b-pi" style="display: inline;"><img alt="Reorder" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd22228d970b image-full img-responsive" src="/assets/image_122ae3.jpg" title="Reorder" /></a></p>
<p>&#0160;</p>

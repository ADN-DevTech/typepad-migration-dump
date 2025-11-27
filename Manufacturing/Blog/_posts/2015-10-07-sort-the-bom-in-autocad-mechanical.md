---
layout: "post"
title: "Sort the BOM in AutoCAD Mechanical"
date: "2015-10-07 03:29:05"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD Mechanical"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/sort-the-bom-in-autocad-mechanical.html "
typepad_basename: "sort-the-bom-in-autocad-mechanical"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>In case of the <strong>UI</strong> of <strong>AutoCAD Mechanical</strong> there are three keys you can set for the sort:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087de971970d-pi" style="display: inline;"><img alt="ApplySort" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087de971970d image-full img-responsive" src="/assets/image_a70dd9.jpg" title="ApplySort" /></a></p>
<p>Sorting based on keys is a <strong>UI</strong> only method. In the <strong>API</strong>&#0160;you don&#39;t really need that since there you have full control over how the items should be sorted. You can come up with any sorting logic, it just needs to set the&#0160;<strong>SortPriority</strong> property of each <strong>McadBOMItem</strong> before calling <strong>applySort</strong>().</p>
<p>The following <strong>VBA</strong> sample sorts based on the <strong>Item Number</strong> property of the <strong>BOM</strong> items:</p>
<pre>Public Sub SortBom()
    Dim symMgr As McadSymbolBBMgr
    Set symMgr = ThisDrawing.Application. _
        GetInterfaceObject(&quot;SymBBAuto.McadSymbolBBMgr&quot;)

    Dim BOMs As McadBOMs
    Set BOMs = symMgr.bommgr.GetAllBOMTables(False)
    
    Dim BOM As McadBOM
    Set BOM = BOMs.item(0)
    
    Dim item As McadBOMItem
    For Each item In BOM.Items
        Dim p As Long: p = Val(item.ItemNumber)
        If p &gt; 0 Then item.SortPriority = p
    Next
    
    BOM.applySort
End Sub</pre>
<p><strong>Note</strong>: the lower the <strong>SortPriority</strong>&#0160;value the earlier (more top in the table) the item will appear in the list. Also its value does not have to be unique.</p>

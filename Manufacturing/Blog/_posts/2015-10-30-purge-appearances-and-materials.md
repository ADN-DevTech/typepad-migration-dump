---
layout: "post"
title: "Purge Appearances and Materials"
date: "2015-10-30 06:35:35"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/10/purge-appearances-and-materials.html "
typepad_basename: "purge-appearances-and-materials"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>When you use an <strong>Asset</strong>, whether it&#39;s <strong>Material</strong> or <strong>Appearance</strong>, it gets copied into the document and can be accessed from the following properties of the <strong>Document</strong> object:<br />- <strong>AppearanceAssets</strong><br />- <strong>MaterialAssets</strong><br />- <strong>Assets</strong> (both <strong>Appearances</strong> and <strong>Materials</strong>)</p>
<p>If you want to get rid of unused items (i.e. purge the assets collection in the document) then you can just iterate through the items in the collections and remove the unused ones (where&#0160;<strong>IsUsed</strong> = <strong>False</strong>).<br />Note: there might be connections between the assets in which case you have to iterate through them multiple times, until all the items stay used.</p>
<pre>Sub PurgeMaterialsAndAppearances()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  &#39; There can be references between assets,
  &#39; so keep doing it until only used things remain
  Dim foundUnused As Boolean
  Do
    foundUnused = False
    Dim a As Asset
    For Each a In doc.Assets
      If Not a.IsUsed Then
        a.Delete
        foundUnused = True
      End If
    Next
  Loop While foundUnused
End Sub</pre>
<p>This is what we start off with in the document and what we end up with after running the above code:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16eea6d970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AssetAppearances" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16eea6d970c image-full img-responsive" src="/assets/image_bdfd76.jpg" title="AssetAppearances" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16eea76970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AssetMaterials" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16eea76970c image-full img-responsive" src="/assets/image_a29d62.jpg" title="AssetMaterials" /></a></p>
<p>&#0160;</p>

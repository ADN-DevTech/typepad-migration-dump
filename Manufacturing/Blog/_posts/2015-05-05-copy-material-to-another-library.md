---
layout: "post"
title: "Copy material to another library"
date: "2015-05-05 09:36:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/05/copy-material-to-another-library.html "
typepad_basename: "copy-material-to-another-library"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is quite similar to the solution shown <a href="http://adndevblog.typepad.com/manufacturing/2013/07/inventor-2014-api-set-part-material.html" target="_self">here</a>, with the main difference being that instead of adding the material to the document&#39;s library, we add it to another external library.</p>
<p>Let&#39;s say that my custom library is called &quot;<strong>MyLibrary</strong>&quot; and we want to add the material &quot;<strong>Copper</strong>&quot; to it. Then this <strong>VBA</strong> code should do the trick:</p>
<pre>Sub CopyMaterialToMyLibrary()
  Dim oDoc As PartDocument
  Set oDoc = ThisApplication.ActiveDocument
 
  Dim assetLibs As AssetLibraries
  Set assetLibs = ThisApplication.AssetLibraries
 
  Dim Name As String
  Name = &quot;Copper&quot;
 
  Dim localAsset As Asset
  On Error Resume Next
  Set localAsset = oDoc.Assets.Item(Name)
  
  Dim assetLib As AssetLibrary
  &#39; Use name ...
  &#39;Set assetLib = assetLib(&quot;Autodesk Material Library&quot;)
  &#39; ... or ID
  Set assetLib = assetLibs(&quot;AD121259-C03E-4A1D-92D8-59A22B4807AD&quot;)
     
  Dim myAssetLib As AssetLibrary
  Set myAssetLib = assetLibs(&quot;MyLibrary&quot;)
  
  &#39; Get an asset in the library
  Dim libAsset As Asset
  Set libAsset = assetLib.MaterialAssets(Name)
  
  &#39; Copy the asset to the other library
  Set localAsset = libAsset.CopyTo(myAssetLib)
End Sub</pre>
<p>Result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c785eac8970b-pi" style="display: inline;"><img alt="Mylibrary" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c785eac8970b image-full img-responsive" src="/assets/image_cd1494.jpg" title="Mylibrary" /></a></p>
<p>&#0160;</p>

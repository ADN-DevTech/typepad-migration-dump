---
layout: "post"
title: "Setting AppearanceAsset properties of newly created Revit Material"
date: "2022-10-04 00:53:59"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2022/10/setting-appearanceasset-properties-of-newly-created-revit-material.html "
typepad_basename: "setting-appearanceasset-properties-of-newly-created-revit-material"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p><strong>Question</strong>: One can change the AppearanceAsset properties of a UI-generated Revit material. However, the AppearanceAssetId value is null if the material is API-generated. The AppearanceAsset properties cannot be set for the material created by the API. Is it possible to modify or set the AppearanceAsset properties of an API-generated Material?</p>
<p><strong>Answer</strong>: Since the API-generated material lacks an AppearanceAssetId , it should be set explicitly. In this case, the simplest way to create a new material is to duplicate an existing one and then modify it.</p>
<p>In the code below, red carpet material has been duplicated, and the AppearanceAsset properties of the duplicated material have been modified.</p>
<p>&#0160;</p>
<pre class="prettyprint"><span style="font-size: 10pt;">public void DuplicateAndModifyMaterial(Material material)
      {
        ElementId appearanceAssetId = material.AppearanceAssetId;
        AppearanceAssetElement assetElem = material.Document<br />                .GetElement(appearanceAssetId) as AppearanceAssetElement;<br />
        ElementId duplicateAssetElementId = ElementId.InvalidElementId;
        using (Transaction t = new Transaction(material.Document))
         {
            t.Start(&quot;Duplicate Red Carpet Material&quot;);
           // Duplicate the material
            Material duplicateMaterial = material.Duplicate(&quot;Blue Carpet&quot;);
           <br />           // Duplicate the appearance asset and the asset in it
            AppearanceAssetElement duplicateAssetElement = assetElem.Duplicate(&quot;Blue Carpet&quot;);
           <br />           // Assign the asset element to the material
            duplicateMaterial.AppearanceAssetId = duplicateAssetElement.Id;
            duplicateAssetElementId = duplicateAssetElement.Id;
            t.Commit();
         }
       // Make changes to the duplicate asset
         using (Transaction t2 = new Transaction(material.Document))
          {
           t2.Start(&quot;Change blue carpet material assets&quot;);
           using (AppearanceAssetEditScope editScope <br />                    = new AppearanceAssetEditScope(assetElem.Document))
            {   </span><br /><span style="font-size: 10pt;">             // returns an editable copy of the appearance asset
              Asset editableAsset = editScope.Start(duplicateAssetElementId);   
                    </span><br /><span style="font-size: 10pt;">             // Description
              AssetPropertyString descriptionProperty =editableAsset<br />                   .FindByName(&quot;description&quot;) as AssetPropertyString;
              descriptionProperty.Value = &quot;blue carpet&quot;;
                    </span><br /><span style="font-size: 10pt;">             // Diffuse image
              AssetPropertyDoubleArray4d genericDiffuseProperty = editableAsset<br />                  .FindByName(&quot;generic_diffuse&quot;) as AssetPropertyDoubleArray4d;
              <br />              genericDiffuseProperty<br />                 .SetValueAsColor(new Autodesk.Revit.DB.Color(0x00, 0x00, 0xFF));
              <br />              Asset connectedAsset = genericDiffuseProperty.GetSingleConnectedAsset();
              AssetPropertyString bitmapProperty = connectedAsset<br />                   .FindByName(&quot;unifiedbitmap_Bitmap&quot;) as AssetPropertyString;
                    </span><br /><span style="font-size: 10pt;">             //Appearance tab &gt;&gt;&gt; Generic &gt;&gt;&gt; Image
              bitmapProperty.Value = @&quot;Your image Path here&quot;;
              editScope.Commit(true);
            }
           t2.Commit();
          }
      }

</span></pre>
<p><strong>Please note</strong>: Here in this instance, the &quot;generic_diffuse&quot; property has been accessed. However, not every AppearanceAsset will have the &quot;generic_diffuse&quot; property. So, please use the <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup tool</a> to explore the material element, find the relevant property, and modify it accordingly.</p>

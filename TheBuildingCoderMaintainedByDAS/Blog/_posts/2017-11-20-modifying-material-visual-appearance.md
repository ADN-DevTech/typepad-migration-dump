---
layout: "post"
title: "Modifying Material Visual Appearance"
date: "2017-11-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2018"
  - "AU"
  - "Material"
  - "SDK Samples"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/11/modifying-material-visual-appearance.html "
typepad_basename: "modifying-material-visual-appearance"
typepad_status: "Publish"
---

<p>Several queries concerning rendering issues were discussed recently and solved by the new Visual Materials API included in Revit 2018.1:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-20181-and-the-visual-materials-api.html">Revit 2018.1 and the Visual Materials API</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/08/revit-20181-and-the-visual-materials-api.html#2">Appearance Asset Editing SDK sample</a></li>
</ul>

<p>Here comes another one, answered more completely by Boris Shafiro's Autodesk University class on this topic:</p>

<ul>
<li>AU class catalogue entry: <a href="https://autodeskuniversity.smarteventscloud.com/connect/sessionDetail.ww?SESSION_ID=124625">SD124625 &ndash; New API to Modify Visual Appearance of Materials in Revit</a></li>
<li>Class handout and slides: <a href="#3">SD124625 &ndash; Visual Materials API</a></li>
</ul>

<p><strong>Question:</strong> How can I set the Material Render Appearance through the API in Revit 2018?</p>

<p>I can see there is the <code>Autodesk.Revit.DB.Visual.Asset</code> class, but how do I add to the list of <code>Autodesk.Revit.DB.Visual.AssetProperty</code> objects for a new material?</p>

<p>I noticed the forum thread
on how to <a href="https://forums.autodesk.com/t5/revit-api-forum/create-or-modify-a-rendering-asset/td-p/6244577">create or modify a rendering asset</a> that
seems to indicate limitations in this area...</p>

<p><strong>Answer:</strong> Unfortunately, this is not possible in Revit 2018.</p>

<p>The good news is that it <b><i>is</i></b> possible in Revit 2018.1 using the Visual Materials API.</p>

<p>Check out Boris Shafiro's class at AU to learn about it.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb09d8edd8970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb09d8edd8970d img-responsive" style="width: 442px; " alt="Visual Materials API" title="Visual Materials API" src="/assets/image_721182.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>SD124625 &ndash; Visual Materials API</h4>

<p>The ability to use the Revit API to modify visual appearances of materials was among the top customer requests for years. This new API has been implemented in Revit 2018.1. This class presents the new Visual Materials API, coding workflows, and usage of multiple schemas for the visualization properties of materials in Revit software &ndash; by Boris Shafiro, Software Development Manager, Autodesk:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/files/sd124625_visual_appearance_of_materials_api_boris_shafiro_handout.pdf">Handout document PDF</a></li>
<li><a href="http://thebuildingcoder.typepad.com/files/sd124625_visual_appearance_of_materials_api_boris_shafiro_slides.pdf">Slide deck PDF</a></li>
</ul>

<p>For the sake of completeness and search engine findability, here is the pure text copied out of the slide deck:</p>

<ul>
<li><a href="#3.1">Learning Objectives</a></li>
<li><a href="#4">The Basics</a>
<ul>
<li><a href="#4.1">Materials API</a></li>
<li><a href="#4.2">Terminology</a></li>
<li><a href="#4.3">Material API building blocks</a></li>
<li><a href="#4.4">Visual Materials UI</a></li>
</ul></li>
<li><a href="#5">New Editing Capabilities in Materials API</a>
<ul>
<li><a href="#5.1">Edit Scope</a></li>
<li><a href="#5.2">New Writable Properties</a></li>
<li><a href="#5.3">New Methods</a></li>
<li><a href="#5.4">Coding Workflow to Edit a Color</a></li>
<li><a href="#5.5">Connected Assets</a></li>
<li><a href="#5.6">Coding Workflow to Edit a Connected Asset</a></li>
</ul></li>
<li><a href="#7">Schemas and Property Names</a>
<ul>
<li><a href="#7.1">Standard Material Schemas</a></li>
<li><a href="#7.2">Advanced Material Schemas</a></li>
<li><a href="#7.3">Common Schema</a></li>
<li><a href="#7.4">Schemas for Connected Assets</a></li>
<li><a href="#7.5">UnifiedBitmap</a></li>
<li><a href="#7.6">Property Names</a></li>
<li><a href="#7.7">Special Cases</a></li>
</ul></li>
<li><a href="#8">SDK Sample</a>
<ul>
<li><a href="#8.1">AppearanceAssetEditing</a></li>
</ul></li>
</ul>

<h4><a name="3.1"></a>Learning Objectives</h4>

<p>Learn how to</p>

<ul>
<li>Use new API to modify visual appearance of Materials in Revit</li>
<li>Navigate coding workflow to edit appearance assets</li>
<li>Use multiple schemas for regular and advanced materials in Revit</li>
<li>Write a sample plug-in for basic modification of the visual appearance of Revit materials</li>
</ul>

<p><center></p>

<div style="border-style:solid; border-width:2px 0px 2px 0px">
<a name="4"></a><h3 style="font-weight: bold">The Basics</h3>
</div>

<p></center></p>

<h4><a name="4.1"></a>Materials API</h4>

<ul>
<li>Basic Element Info (name, tags)</li>
<li>Appearance properties</li>
<li>Shaded view graphics</li>
<li>Thermal &amp; energy-related properties</li>
<li>Physical &amp; structural properties</li>
</ul>

<h4><a name="4.2"></a>Terminology</h4>

<ul>
<li>Revit Material &ndash; An element representing a material, made of a collection of property sets</li>
<li>Asset &ndash; The class representing a package of properties</li>
<li>Appearance Asset &ndash; Asset representing visual material properties</li>
<li>Appearance Asset Element &ndash; An element that stores an appearance asset</li>
<li>Asset Property &ndash; One particular property of an asset</li>
</ul>

<h4><a name="4.3"></a>Material API building blocks</h4>

<ul>
<li>Namespace Revit.DB Namespace Revit.DB.Visual</li>
<li>Material &ndash; AppearanceAssetId</li>
<li>AppearanceAssetElement &ndash; GetRenderingAsset()</li>
<li>Asset &ndash; AssetProperty 1 ... AssetProperty N &ndash; [“name_string” ] or FindByName(name)</li>
<li>AssetProperty &ndash; GetSingleConnectedAsset()</li>
</ul>

<h4><a name="4.4"></a>Visual Materials UI</h4>

<p><center></p>

<div style="border-style:solid; border-width:2px 0px 2px 0px">
<a name="5"></a><h3 style="font-weight: bold">New Editing Capabilities in Materials API</h3>
</div>

<p></center></p>

<h4><a name="5.1"></a>Edit Scope</h4>

<ul>
<li>AppearanceAssetEditScope
<ul>
<li>Start()</li>
<li>Commit()</li>
<li>Cancel()</li>
</ul></li>
<li>Contains one Asset (plus all connected Assets)</li>
</ul>

<h4><a name="5.2"></a>New Writable Properties</h4>

<ul>
<li>AssetPropertyString.Value</li>
<li>AssetPropertyBoolean.Value</li>
<li>AssetPropertyInteger.Value</li>
<li>AssetPropertyDouble.Value</li>
<li>AssetPropertyFloat.Value</li>
<li>AssetPropertyEnum.Value</li>
<li>AssetPropertyDistance.Value (not always in feet)</li>
</ul>

<h4><a name="5.3"></a>New Methods</h4>

<ul>
<li>AssetPropertyDoubleArray3d.SetValueAsXYZ()</li>
<li>AssetPropertyDoubleArray4d.SetValueAsDoubles()</li>
<li>AssetPropertyDoubleArray4d.SetValueAsColor()</li>
<li>AssetPropertyList &ndash; add, insert, remove</li>
</ul>

<h4><a name="5.4"></a>Coding Workflow to Edit a Color</h4>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">AppearanceAssetEditScope</span>&nbsp;editScope
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">AppearanceAssetEditScope</span>(&nbsp;document&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset&nbsp;=&nbsp;editScope.Start(&nbsp;assetElem.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>&nbsp;genericDiffuseProperty
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;editableAsset[<span style="color:#a31515;">&quot;generic_diffuse&quot;</span>]
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>;
&nbsp;&nbsp;&nbsp;&nbsp;genericDiffuseProperty.SetValueAsColor(&nbsp;color&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;editScope.Commit(&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;}
</pre>

<h4><a name="5.5"></a>Connected Assets</h4>

<ul>
<li>AssetProperty.GetSingleConnectedAsset()</li>
<li>AssetProperty.RemoveConnectedAsset()</li>
<li>AssetProperty.AddConnectedAsset( String schemaId )</li>
<li>AssetProperty.AddCopyAsConnectedAsset( Asset renderingAsset )</li>
</ul>

<h4><a name="5.6"></a>Coding Workflow to Edit a Connected Asset</h4>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">AppearanceAssetEditScope</span>&nbsp;editScope
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">AppearanceAssetEditScope</span>(&nbsp;document&nbsp;)&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset&nbsp;=&nbsp;editScope.Start(&nbsp;assetElem.Id&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetProperty</span>&nbsp;bumpMapProperty&nbsp;=&nbsp;editableAsset[<span style="color:#a31515;">&quot;generic_bump_map&quot;</span>];
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;connectedAsset&nbsp;=&nbsp;bumpMapProperty.GetSingleConnectedAsset();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;connectedAsset&nbsp;!=&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyString</span>&nbsp;bumpmapBitmapProperty
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;connectedAsset[<span style="color:#a31515;">&quot;unifiedbitmap_Bitmap&quot;</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyString</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;bumpmapBitmapProperty.IsValidValue(&nbsp;bumpmapImageFilepath&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bumpmapBitmapProperty.Value&nbsp;=&nbsp;bumpmapImageFilepath;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;editScope.Commit(&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;}
</pre>

<p><center></p>

<div style="border-style:solid; border-width:2px 0px 2px 0px">
<a name="7"></a><h3 style="font-weight: bold">Schemas and Property Names</h3>
</div>

<p></center></p>

<h4><a name="7.1"></a>Standard Material Schemas</h4>

<ul>
<li>Ceramic</li>
<li>Concrete</li>
<li>Generic</li>
<li>Glazing</li>
<li>Hardwood</li>
<li>MasonryCMU</li>
<li>Metal</li>
<li>MetallicPaint</li>
<li>Mirror</li>
<li>PlasticVinyl</li>
<li>SolidGlass</li>
<li>Stone</li>
<li>WallPaint</li>
<li>Water</li>
</ul>

<h4><a name="7.2"></a>Advanced Material Schemas</h4>

<ul>
<li>AdvancedLayered</li>
<li>AdvancedMetal</li>
<li>AdvancedOpaque</li>
<li>AdvancedTransparent</li>
<li>AdvancedWood</li>
</ul>

<h4><a name="7.3"></a>Common Schema</h4>

<h4><a name="7.4"></a>Schemas for Connected Assets</h4>

<ul>
<li>BumpMap</li>
<li>Checker</li>
<li>Gradient</li>
<li>Marble</li>
<li>Noise</li>
<li>Speckle</li>
<li>Tile</li>
<li>UnifiedBitmap</li>
<li>Wave</li>
<li>Wood</li>
</ul>

<h4><a name="7.5"></a>UnifiedBitmap</h4>

<h4><a name="7.6"></a>Property Names</h4>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>&nbsp;genericDiffuseProperty&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;editableAsset[<span style="color:#a31515;">&quot;generic_diffuse&quot;</span>]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>;
</pre>

<p>Equivalent:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>&nbsp;genericDiffuseProperty&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;editableAsset[<span style="color:#2b91af;">Generic</span>.GenericDiffuse]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>;
</pre>

<h4><a name="7.7"></a>Special Cases</h4>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyString</span>&nbsp;path
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;asset[<span style="color:#2b91af;">UnifiedBitmap</span>.UnifiedbitmapBitmap]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyString</span>;
</pre>

<ul>
<li>Path is relative if inside default Material Library or in Options/Rendering/Additional Render Appearance Paths;</li>
<li>Path is absolute otherwise.</li>
</ul>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>&nbsp;color
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;asset[<span style="color:#2b91af;">Generic</span>.DiffuseColor]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>;
</pre>

<ul>
<li><p>The Value of this AssetProperty is ignored if there is a connected Asset.</p></li>
<li><p>AssetPropertyReference reference &ndash; Does not have a Value. Used only to have a connected Asset.</p></li>
</ul>

<p><center></p>

<div style="border-style:solid; border-width:2px 0px 2px 0px">
<a name="8"></a><h3 style="font-weight: bold">SDK Sample</h3>
</div>

<p></center></p>

<h4><a name="8.1"></a>AppearanceAssetEditing</h4>

<p>Edit appearance asset properties via a small control dialog &ndash; this sample demonstrates basic usage of the AppearanceAssetEditScope and AssetProperty classes to change the value of an asset property in a given material:</p>

<ul>
<li>Bring up a modeless dialog</li>
<li>Select a Painted Face</li>
<li>Get Appearance Asset</li>
<li>Get Tint Color AssetProperty</li>
<li>Increment red/green/blue</li>
</ul>

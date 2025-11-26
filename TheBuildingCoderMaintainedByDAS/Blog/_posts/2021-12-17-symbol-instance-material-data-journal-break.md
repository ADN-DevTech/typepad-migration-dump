---
layout: "post"
title: "Symbol, Instance, Material, Data, Journal, Break"
date: "2021-12-17 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Batch"
  - "Data Access"
  - "Element Relationships"
  - "Family"
  - "Geometry"
  - "Journal"
  - "Material"
  - "News"
  - "Storage"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/12/symbol-instance-material-data-journal-break.html "
typepad_basename: "symbol-instance-material-data-journal-break"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>This is probably my last post of the year, so let's wrap up with an eclectic mix of topics to close with:</p>

<ul>
<li><a href="#2">Symbol vs instance geometry clarification</a></li>
<li><a href="#3">Create new material with texture</a>
<ul>
<li><a href="#3.2">Updates from Devteam, Richard, and Harm</a></li>
</ul></li>
<li><a href="#4">RVT dashboard data access</a></li>
<li><a href="#5">Marking and retrieving a custom element</a></li>
<li><a href="#6">Advanced remote batch command processing</a></li>
<li><a href="#7">Midwinter break</a></li>
</ul>

<h4><a name="2"></a> Symbol vs Instance Geometry Clarification</h4>

<p>Richard <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1035859">RPThomas108</a> Thomas
clarifies some aspects of symbol versus instance geometry in an imported DWG file in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/getinstancegeometry-vs-getsymbolgeometry/m-p/10819201"><code>GetInstanceGeometry</code> vs <code>GetSymbolGeometry</code></a>:</p>

<p><strong>Question:</strong> ... about the methods GeometryInstance.GetInstanceGeometry() and GeometryInstance.GetSymbolGeometry().</p>

<p>In my file, I've got only one imported DWG with only one line inside it. </p>

<p>I analysed it using the following code:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">reference</span>&nbsp;=&nbsp;_selection.PickObject(ObjectType.PointOnElement);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">element</span>&nbsp;=&nbsp;_doc.GetElement(reference);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">options</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Options();
&nbsp;&nbsp;options.ComputeReferences&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;options.View&nbsp;=&nbsp;_doc.ActiveView;

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">geometryElement</span>&nbsp;=&nbsp;element.get_Geometry(options);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">geometryInstance</span>&nbsp;=&nbsp;geometryElement
&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;x&nbsp;<span style="color:blue;">is</span>&nbsp;GeometryInstance)&nbsp;<span style="color:blue;">as</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GeometryInstance;

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">instanceGeometry</span>&nbsp;=&nbsp;geometryInstance?.GetInstanceGeometry();
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">instanceCurve</span>&nbsp;=&nbsp;instanceGeometry?.FirstOrDefault(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;x&nbsp;<span style="color:blue;">is</span>&nbsp;Curve)&nbsp;<span style="color:blue;">as</span>&nbsp;Curve;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">instanceReference</span>&nbsp;=&nbsp;instanceCurve?.Reference;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">instanceRepresentation</span>&nbsp;=&nbsp;instanceReference?.ConvertToStableRepresentation(_doc);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">symbolGeometry</span>&nbsp;=&nbsp;geometryInstance?.GetSymbolGeometry();
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">symbolCurve</span>&nbsp;=&nbsp;symbolGeometry?.FirstOrDefault(<span style="color:#1f377f;">x</span>&nbsp;=&gt;&nbsp;x&nbsp;<span style="color:blue;">is</span>&nbsp;Curve)&nbsp;<span style="color:blue;">as</span>&nbsp;Curve;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">symbolReference</span>&nbsp;=&nbsp;symbolCurve?.Reference;
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;<span style="color:#1f377f;">symbolRepresentation</span>&nbsp;=&nbsp;symbolReference?.ConvertToStableRepresentation(_doc);
</pre>

<p>Executing this provides the following values:</p>

<ul>
<li><code>instanceRepresentation</code> = e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38:2:1:LINEAR </li>
<li><code>symbolRepresentation</code> = e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000c0f:0:INSTANCE:e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38:2:1:LINEAR</li>
</ul>

<p>These include two <code>UniqueId</code> values:</p>

<ul>
<li>e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000a38 &ndash; <code>CADLinkType</code></li>
<li>e558d96b-a4b0-449d-a84e-00d8c2768a5c-00000c0f &ndash; <code>ImportInstance</code></li>
</ul>

<p>For me, it seems like these two results are mixed up.
Looks like <code>instanceRepresentation</code> refers to symbol geometry, while <code>symbolRepresentation</code> refers to instance geometry.</p>

<p><strong>Answer:</strong> Seems logical in a way, no?
When you use symbol, it gives the full lineage of symbol and instance of that symbol.
When you use the copy (with method noted below), it just gives the symbol it was copied from.
There is no actual instance for it, because that function just creates a copy at the time for you (is a helper method for specific purposes).</p>

<p>Beyond CADLinks, you'll find that there are multiple versions of symbol geometries for a type, i.e., there is often a symbol to represent each structural framing length (with such lengths being driven by instance variations, not type variations).
So, equating symbol geometry to family symbols probably is confusing to start with.
That is to say they are all different ids and at time of extraction the form of symbol geometry you get is going to be partly decided by the instance variations not just the type variations.</p>

<p>Extract from <a href="https://www.revitapidocs.com/2022/22d4a5d4-dfc2-7227-2cae-b989729696ec.htm">RevitAPI.chm on GeometryInstance.GetInstanceGeometry</a>:</p>

<blockquote>
  <p>...This method returns a copy of the Revit geometry. It is suitable for use in a tool which extracts geometry to another format or carries out a geometric analysis; however, because it returns a copy the references found in the geometry objects contained in this element are not suitable for creating new Revit elements referencing the original element (for example, dimensioning). Only the geometry returned by GetSymbolGeometry() with no transform can be used for that purpose."</p>
</blockquote>

<p>Here is a simple example demonstrating these relationships, snooping the elements with RevitLookup:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302942f8e9266200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302942f8e9266200c img-responsive" style="width: 200px; display: block; margin-left: auto; margin-right: auto;" alt="Two beams of same family type" title="Two beams of same family type" src="/assets/image_0cf150.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Two beams of same family type</p>

<p></center></p>

<p>The short beam element id is 427840:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302788060d688200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302788060d688200d image-full img-responsive" alt="The short beam as id 427840" title="The short beam as id 427840" src="/assets/image_d28a80.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">The short beam as id 427840</p>

<p></center></p>

<p>The long beam element id is 427855:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302942f8e9272200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302942f8e9272200c image-full img-responsive" alt="The long beam as id 427855" title="The long beam as id 427855" src="/assets/image_53b4a3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">The long beam as id 427855</p>

<p></center></p>

<p>The <code>FamilySymbol</code> element id is 95037:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302942f8e927e200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302942f8e927e200c image-full img-responsive" alt="The FamilySymbol id 95037" title="The FamilySymbol id 95037" src="/assets/image_160b63.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">The FamilySymbol id 95037</p>

<p></center></p>

<p>If you check the bounding box extents for the two geometry symbols, you'll see they match the beam lengths.</p>

<p>This gets further complicated with cuts, but it demonstrates that Revit is storing geometrical symbol variations differently to how we think of the type-to-instance relationships based on type and instance parameters.</p>

<p>Many thanks to Richard for yet another insightful and illuminating explanation!</p>

<h4><a name="3"></a> Create New Material with Texture</h4>

<p>Harm van den Brand shares a new implementation of a suggestion by Rudi <em>Revitalizer</em> Honke to create a new material and set its texture in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/importing-and-displaying-satellite-images/m-p/10815534">importing and displaying satellite images</a>:</p>

<p>I'm building an add-in for Revit, and I would like to be able to import and display third-party satellite imagery in order to place buildings in their 'real' position.
I would like to be able to do this in a 3D view, but I don't know how.</p>

<p>The user workflow for my add-in is this:</p>

<ul>
<li>A user opens the add-in and is prompted to input a location through a WPF window.</li>
<li>Once a location is confirmed, a number of things are created/imported into the active project to make it look as close as possible to its actual real-life location. One of these things is the satellite image I'm seeking to import here.</li>
</ul>

<p>Essentially, my question is exactly <a href="">this one</a>, but instead doing that programmatically/automatically through an add-in. In that thread, a suggestion is made to create a decal with the desired image, but this does not seem to be supported through the API.</p>

<p>Another approach I found is to use <code>PostCommand</code> to create and place decals, but these commands are apparently only executed after exiting the API context and only one at a time.
As my add-in aims to perform a whole bunch of functionalities in one go, this seems ill-suited for my use case.
It seems to be possible to chain a bunch of <code>PostCommand</code> calls, but this is a little 'hacky' and not recommended, especially for commercial use.</p>

<p>Am I overlooking some existing functionality?
Is my use case just not supported in current Revit?
I'm new to programming for Revit, so it's very possible I've missed something.</p>

<p>I'm running / programming for Revit 2019 on Windows 10.</p>

<p><strong>Answer:</strong> What about creating a new material and setting its texture path as described
in <a href="https://thebuildingcoder.typepad.com/blog/2017/11/modifying-material-visual-appearance.html">modifying material visual appearance</a>,
then making a <code>TopoSurface</code> and assigning the material to it?</p>

<p>I don't know how to adjust the UV mapping for the TopoSurface, but if it worked, you would see your satellite image in 3D.</p>

<p><strong>Response:</strong> Thanks to all for the replies!
It took some time to try out the proposed solution (accessing AppearanceElements is convoluted!), so that's why it took me this long to reply.</p>

<p>In the end, though I had to work around some weird quirks with the API.</p>

<p>Adding the image as a texture to a topography through a material works great.</p>

<p>I ended up taking Revitalizer's suggested approach of creating a new material and setting its texture.</p>

<pre class="code">
&nbsp;&nbsp;Material&nbsp;<span style="color:#1f377f;">underlayMaterial</span>&nbsp;=&nbsp;Material.Create(
&nbsp;&nbsp;&nbsp;&nbsp;revitDocument,&nbsp;materialName);
</pre>

<p>To this material, I link a so-called <code>AppearanceAsset</code>:</p>

<pre class="code">
&nbsp;&nbsp;underlayMaterial.AppearanceAssetId&nbsp;=&nbsp;assetElement.Id;
</pre>

<p>(more on how I get this assetElement in a moment)</p>

<p>Then, I assign the path of a jpeg image to the texture asset:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">using</span>&nbsp;(AppearanceAssetEditScope&nbsp;<span style="color:#1f377f;">editScope</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;AppearanceAssetEditScope(revitDocument))
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;Asset&nbsp;<span style="color:#1f377f;">editableAsset</span>&nbsp;=&nbsp;editScope.Start(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;assetElement.Id);

&nbsp;&nbsp;&nbsp;&nbsp;AssetProperty&nbsp;<span style="color:#1f377f;">assetProperty</span>&nbsp;=&nbsp;editableAsset
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FindByName(<span style="color:#a31515;">&quot;generic_diffuse&quot;</span>);

&nbsp;&nbsp;&nbsp;&nbsp;Asset&nbsp;<span style="color:#1f377f;">connectedAsset</span>&nbsp;=&nbsp;assetProperty
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetConnectedProperty(0)&nbsp;<span style="color:blue;">as</span>&nbsp;Asset;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">// Edit&nbsp;bitmap</span>

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(connectedAsset.Name&nbsp;==&nbsp;<span style="color:#a31515;">&quot;UnifiedBitmapSchema&quot;</span>)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AssetPropertyString&nbsp;<span style="color:#1f377f;">path</span>&nbsp;=&nbsp;connectedAsset
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FindByName(UnifiedBitmap.UnifiedbitmapBitmap)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;AssetPropertyString;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(path.IsValidValue(imagePath))
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;path.Value&nbsp;=&nbsp;imagePath;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;You&nbsp;might&nbsp;have&nbsp;to&nbsp;fiddle&nbsp;a&nbsp;bit&nbsp;with&nbsp;the&nbsp;scale&nbsp;properties,</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;for&nbsp;example&nbsp;when&nbsp;your&nbsp;source&nbsp;uses&nbsp;centimeters:</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AssetPropertyDistance&nbsp;<span style="color:#1f377f;">scaleX</span>&nbsp;=&nbsp;connectedAsset
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FindByName(UnifiedBitmap.TextureRealWorldScaleX)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;AssetPropertyDistance;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AssetPropertyDistance&nbsp;<span style="color:#1f377f;">scaleY</span>&nbsp;=&nbsp;connectedAsset
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FindByName(UnifiedBitmap.TextureRealWorldScaleY)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;AssetPropertyDistance;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Because&nbsp;newly&nbsp;added&nbsp;bitmaps&nbsp;are&nbsp;displayed&nbsp;in&nbsp;inches.</span>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(scaleX.DisplayUnitType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;DisplayUnitType.DUT_DECIMAL_INCHES)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scaleX.Value&nbsp;/=&nbsp;2.54;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#8f08c4;">if</span>&nbsp;(scaleY.DisplayUnitType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==&nbsp;DisplayUnitType.DUT_DECIMAL_INCHES)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scaleY.Value&nbsp;/=&nbsp;2.54;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;editScope.Commit(<span style="color:blue;">true</span>);
&nbsp;&nbsp;}
</pre>

<p>I find this a bit 'hacky' because of how I retrieve the assetElement.</p>

<p>Instinctively, I would want to create a new, empty instance of Asset.
  Something like:</p>

<pre class="code">
&nbsp;&nbsp;AppearanceAssetElement&nbsp;<span style="color:#1f377f;">assetElement</span>
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;AppearanceAssetElement.Create();
</pre>

<p>However, this is not how Revit's material/texture API works.
We can only use those materials/textures/etc. that are present in Revit's libraries.
Therefore, we can only make copies of existing ones:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Retrieve&nbsp;asset&nbsp;library&nbsp;from&nbsp;the&nbsp;application</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;(this&nbsp;is&nbsp;the&nbsp;only&nbsp;source&nbsp;available;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;instantiating&nbsp;from&nbsp;zero&nbsp;is&nbsp;impossible)</span>

&nbsp;&nbsp;IList&lt;Asset&gt;&nbsp;<span style="color:#1f377f;">assetList</span>&nbsp;=&nbsp;commandData.Application
&nbsp;&nbsp;&nbsp;&nbsp;.Application.GetAssets(AssetType.Appearance);

&nbsp;&nbsp;<span style="color:green;">//&nbsp;Select&nbsp;arbitrary&nbsp;asset&nbsp;from&nbsp;library</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;(200&nbsp;works,&nbsp;not&nbsp;all&nbsp;do)</span>

&nbsp;&nbsp;Asset&nbsp;<span style="color:#1f377f;">asset</span>&nbsp;=&nbsp;assetList[200];

&nbsp;&nbsp;AppearanceAssetElement&nbsp;<span style="color:#1f377f;">assetElement</span>;
&nbsp;&nbsp;<span style="color:#8f08c4;">try</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;assetElement&nbsp;=&nbsp;AppearanceAssetElement
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Create(revitDocument,&nbsp;someNewName,&nbsp;asset);
&nbsp;&nbsp;}
</pre>

<p>Yes, I really just randomly tried indices in that assetList until I found one that worked, and hardcoded that one in. Not all AppearanceAssets in the list have the necessary "generic_diffuse" assetProperty to which we can bind a texture, so we have to select one that does.</p>

<p>If you are developing your addin for external parties, this is risky, because we can't ensure that the same libraries are available for any particular user.
It's probably best to somehow filter for valid AppearanceAssets.</p>

<p>Also, you can see that retrieving this appearanceAsset requires <code>ExternalCommandData</code> (which I named CommandData in the code given), which an addin retrieves via the 'Execute' method of an <code>IExternalCommand</code> implementing class.</p>

<p>Also, remember to wrap most of these snippets in transactions.</p>

<p>I hope this helps!</p>

<p>Many thanks to Revitalizer for the good suggestion and to Harm for the implementation and notes.</p>

<h4><a name="3.2"></a> Updates from Devteam, Richard, and Harm</h4>

<p><u>Richard</u> adds: You can find an asset that contains a certain named property via Linq e.g.:</p>

<pre class="code">
Dim Assets As List(Of Visual.Asset) = app.Application.GetAssets(Visual.AssetType.Appearance)

' Find an asset with a property matching Visual.Generic.GenericDiffuse

Dim J0 = Assets.FirstOrDefault(Function(x) x.FindByName(Visual.Generic.GenericDiffuse) IsNot Nothing)
</pre>

<p>This seems better than a fixed index since you don't know for sure how that number may change, e.g., if user manually adds an asset etc.</p>

<p>Obviously, the Assets library is quite large to search but there are existing Materials and Assets in the document which can be duplicated instead.
So, it is often better to know what you are looking for, e.g., load a Family with your known material and the Asset you want to manipulate (if you can't find it).
Most materials will have the appearance asset with the bitmap property though.</p>

<p>There is an example of this within <code>RevitAPI.chm</code> under <code>AppearanceAssetElement.Duplicate</code>.
As noted, if it isn't there in the document then it is straightforward to load something with it to add it.</p>

<p>The <u>development team</u> also responded, saying:</p>

<p>An asset is a resource with economic value that an individual, corporation or country owns or controls with the expectation that it will provide a future benefit.</p>

<p>In Revit, an (material) asset is owned by the libraries too, but currently the libraries are only data but not classes in code, so there's no way to create an asset from the libraries, but can only duplicate one from existing.</p>

<p>The user's problems is trying to find an asset that can assign an image; I suggest below steps:</p>

<ul>
<li>Duplicate a new asset (or material) from any existing ones that serves only for this purpose.</li>
<li>If the new asset contains "unified_bitmap", then change it as desired.</li>
<li>If not, then create a connected asset with AssetProperty.AddConnectedAsset() that should contains "unified_bitmap" in the schema.</li>
</ul>

<p>At last, exposing decal element to API should be the best way. (There may also contains some workaround or hacky way through external file mechanism, but I cannot confirm that.)</p>

<p><u>Harm</u> responds:</p>

<p>Thanks for the update.
If I understand correctly, my solution follows steps 1 and 2 of the dev-team's suggestion.
So it's good to know I'm not doing anything too weird.
Except for selecting a hardcoded element from the library by hand, but I'll happily use @RPTHOMAS108 's suggestion of selecting one through list filtering.
It'll prevent any errors that would occur if the asset libraries ever were to change.</p>

<p>Always good to go through old code again and find yourself thinking "this could be improved".</p>

<p>I'm satisfied with how I currently create/copy an asset (again, step 1 and 2).
It's just that when I first started digging through the api to get to the functionality I needed, it was a bit like groping around in the dark.
My first instinct when it came to the <code>AppearanceAsset</code> part of the problem, at the time, was to instantiate a new one.
It wasn't immediately clear to me that I couldn't do that, but that I could duplicate one.
I learn more with every feature I implement &nbsp; :-)</p>

<p>That having been said, things like your blog and the documentation at revitapidocs have helped out immensely, with this problem and with others, so thank you very much!</p>

<h4><a name="4"></a> RVT Dashboard Data Access</h4>

<p>Some notes from an internal discussion on how to access data in RVT for a dashboard:</p>

<p><strong>Question:</strong> I would like to collect data from Revit models for display in a dashboard.
I thought of using the model derivative APIs in Forge to retrieve the data, or use DA4R since the Revit model must be opened to access the database.
However, I do not have access to Forge or DA4R and would prefer a way to read the data without Revit.
I assume that Revit must be used to unpack the database to make it useable to collect data from.
Much of the content in Revit is created on demand dynamically and not necessarily stored in the file database.
Am I on the right track here?
I heard about some other app that can collect the Revit model data needed.
How can it read the contents without Revit?
Is there some cloud service utilizing DA4R or similar to process the model?</p>

<p><strong>Answer:</strong> The RVT file format is a structured storage file, so some content can be pulled without opening the file directly. 
Because of that format, some data is quite easy to access, e.g., using transmission data and basic file info, both of which can be read with tools like Structured Storage Viewer:</p>

<ul>
<li><a href="https://www.revitapidocs.com/2022/475edc09-cee7-6ff1-a0fa-4e427a56262a.htm">BasicFileInfo</a></li>
<li><a href="https://www.revitapidocs.com/2022/d78d1e9c-1cee-1336-88d5-b605dacd077d.htm">TransmissionData</a></li>
</ul>

<p>Other aspects are more difficult, e.g., how many light fixtures are in room number 2143, how many warnings are in the model, how many doors don't have a valid mark, etc.
Forge isn't necessarily a 'must use' as the data might be accessible via other means, e.g., upload to BIM360 or use the online viewer; use the model checker, or move to another file format for the final deliverable.
Also, might be able to mine data from the digital twin or an IFC instead of an RVT.
Short of those two, it's likely best to stick with forge.</p>

<h4><a name="5"></a> Marking and Retrieving a Custom Element</h4>

<p><strong>Question:</strong> My add-in creates its own view to show additional data within Revit.
I hit a bump around the view element id and wonder whether anything in the API might be able to help. <br />
I essentially want to create my own temporary view within Revit to show data that is not from the currently loaded model, i.e., from linked files or elsewhere.
Right now, I create a view, populate it and delete it on shutdown.
This is all fine.</p>

<p>However, if the view is the only one open in Revit on shutdown, it gets saved into the file.
This got me thinking that I could just save a default add-in view and look for it on next load.
However, I can't find a way to determine the view element id, so I don't know what to look for. </p>

<ul>
<li>Can I create a view with a read-only name, so the user can't edit it and I can search for that?</li>
<li>Can I define a Revit view ID using a GUID somehow?</li>
</ul>

<p>Is there anywhere I could store the view ID, so that I can retrieve it on load? 
I considered storing it in my own settings file, but that doesn't work if the file gets sent to another user.</p>

<p><strong>Answer:</strong> This is a prime case for <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.23">extensible storage</a> in my opinion.
Make a new schema and save the <code>UniqueId</code> of the view into it.
Users will delete that view, though, and if it doesn’t file into the project browser correctly, there will be push back.
Expect to delete and recreate the view often (even mid-session).
Also, ensure that you have good product documentation on why this is in the file, and how it can be worked with, and the like.
Otherwise you will have a LOT of support cases around the feature.</p>

<p><strong>Response:</strong> Fantastic.
Yes, documentation and the options we present to the user around how they use this feature are important.
We also need to be careful around the default name of the view, so it's purpose is obvious enough.
Thanks for the help and advice.</p>

<h4><a name="6"></a> Advanced Remote Batch Command Processing</h4>

<p>Several people recently raised questions on automating Revit workflows, and one possibility to consider is the use of Revit journal files.
David Echols, Senior Programmer at Hankins &amp; Anderson, Inc. shared some important insights and experience in this area in his Autodesk University 2014 class SD5980 on <em>Advanced Revit Remote Batch Command Processing</em>:</p>

<blockquote>
  <p>This class explains a process to run external commands in batch mode from a central server to remote Revit application workstations.
  It covers how to use client and server applications that communicate with each other to manage Revit software on remote workstations with WCF (Windows Communication Foundation) services, examines how to pass XML command data to the Revit application to open a Revit model and initiate batch commands, shows a specific use case for batch export of DWG files for sheets, examines a flexible system for handling Revit dialog boxes on the fly with usage examples and code snippets, and discusses the failure processing API in the context of bypassing warning and error messages while custom commands are running. Finally, it shows you how to gracefully close both the open Revit model and the Revit application.</p>
  
  <ul>
  <li><a href="https://thebuildingcoder.typepad.com/files/revitjournals.pdf">RevitJournals.pdf handout</a></li>
  </ul>
</blockquote>

<h4><a name="7"></a> Midwinter Break</h4>

<p>We are nearing the middle of winter here on the northern hemisphere, and Autodesk is celebrating company holidays in the last days of the calendar year.</p>

<p>I am looking forward to some peaceful time to recuperate during the end
of <a href="https://en.wikipedia.org/wiki/Advent">advent</a>,
followed by the <a href="https://en.wikipedia.org/wiki/Twelfth_Night_(holiday)">twelve-night</a> turning point of the year,
also known as <a href="https://de.wikipedia.org/wiki/Rauhnacht">Rauhnächte</a> or <em>raw nights</em> in German,
full of special depth and significance, related to the differences between
the <a href="https://en.wikipedia.org/wiki/Lunar_calendar">lunar</a> and solar cycles,
beginning with <a href="https://en.wikipedia.org/wiki/Christmas">Christmas</a>,
<a href="https://en.wikipedia.org/wiki/Hanukkah">Hanukkah</a>,
Celtic <a href="https://en.wikipedia.org/wiki/Samhain">Samhain</a>,
Druid <a href="https://en.wikipedia.org/wiki/Alban_Arthan">Alban Arthan</a>,
and many other sacred traditions.</p>

<p>A time of confusion, breaking things, going wrong, calming down, going slowly, contemplation, relaxing into peace and quiet and new beginnings.</p>

<p>I wish you a wonderful midwinter break full of light and warmth!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302942f8e925c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302942f8e925c200c img-responsive" style="width: 280px; display: block; margin-left: auto; margin-right: auto;" alt="Candlelight in snow" title="Candlelight in snow" src="/assets/image_3d1d56.jpg" /></a><br /></p>

<p></center></p>

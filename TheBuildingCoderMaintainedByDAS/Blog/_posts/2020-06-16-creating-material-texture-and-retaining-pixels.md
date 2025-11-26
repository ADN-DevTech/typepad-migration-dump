---
layout: "post"
title: "Creating Material Texture and Retaining Pixels"
date: "2020-06-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "DA4R"
  - "Export"
  - "Forge"
  - "Fun"
  - "Job"
  - "Material"
  - "Properties"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/06/creating-material-texture-and-retaining-pixels.html "
typepad_basename: "creating-material-texture-and-retaining-pixels"
typepad_status: "Publish"
---

<p>I have been quiet now for a while in shock and grief about violence and racism in the world.</p>

<p>Meanwhile, a bunch of interesting discussions on creating material with texture, modifying element level, cutting off image pixels and other things:</p>

<ul>
<li><a href="#2">Creating a material with texture in Revit and Forge</a></li>
<li><a href="#3">Export image cutting off pixels</a></li>
<li><a href="#4">Change level of existing element</a></li>
<li><a href="#5">Physics is cool</a></li>
<li><a href="#6">Forge job openings</a></li>
</ul>

<h4><a name="2"></a> Creating a Material with Texture in Revit and Forge</h4>

<p>This topic has been very much en vogue lately.
It came up again in the context of Forge in the StackOverflow question
on <a href="https://stackoverflow.com/questions/62297851/creating-a-material-with-texture-in-autodesk-revit-forge-design-automation">creating a material with texture in Autodesk Revit Forge Design Automation</a>,
where <a href="https://stackoverflow.com/users/12469767/maleficca">maleficca</a> very kindly shares a complete solution for both environments:</p>

<p><strong>Question:</strong> I'm currently working on some Revit API code which is running in the <strong>Autodesk Forge Design Automation cloud solution</strong>.</p>

<p>Basically, I'm trying to <strong>create a material and attach a texture to it</strong> via the following code:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;AddTexturePath(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetProperty</span>&nbsp;asset,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;texturePath&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;connectedAsset&nbsp;=&nbsp;<span style="color:blue;">null</span>;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;asset.NumberOfConnectedProperties&nbsp;==&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;asset.AddConnectedAsset(&nbsp;<span style="color:#a31515;">&quot;UnifiedBitmapSchema&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;connectedAsset&nbsp;=&nbsp;(<span style="color:#2b91af;">Asset</span>)&nbsp;asset.GetConnectedProperty(&nbsp;0&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyString</span>&nbsp;path&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(<span style="color:#2b91af;">AssetPropertyString</span>)&nbsp;connectedAsset.FindByName(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UnifiedBitmap</span>.UnifiedbitmapBitmap&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;!path.IsValidValue(&nbsp;texturePath&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">File</span>.Create(&nbsp;<span style="color:#a31515;">&quot;texture.png&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;texturePath&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.GetFullPath(&nbsp;<span style="color:#a31515;">&quot;texture.png&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;path.Value&nbsp;=&nbsp;texturePath;
&nbsp;&nbsp;}
</pre>

<p>This is actually working well, as the value for the texture path:</p>

<pre class="code">
  path.Value = texturePath;
</pre>

<p>Needs to be a reference to an existing file. I do not have this file on the cloud instance of Forge, because the path to the texture name is specified by the user when he sends the request for the work item.</p>

<p>The problem is that this sets the texture path for the material as something like this:</p>

<pre class="code">
  T:\Aces\Jobs\<workitem_id>\texture.png
</pre>

<p>Which is basically the working folder for the Workitem instance. This path is useless, because a material with texture path like this needs to be manually re-linked in Revit. </p>

<p>The perfect outcome for me would be if I could somehow map the material texture path to some user-friendly directory like <code>C:\Textures\texture.png</code> and it seems that the Forge instance has a <code>C:\</code> drive present (being probably a Windows instance of some sorts), but my code runs on low privileges, so it cannot create any kind of directories or files outside the working directory.</p>

<p>Does somebody have any idea how this could be resolved?
Any help would be greatly appreciated!</p>

<p><strong>Answer:</strong> Congratulations on getting to this point.
Would you like to share the code you use to create the material and attach the texture for the Revit API add-in developer community to enjoy, either here or in a new thread in the Revit API discussion forum?
People keep asking for such samples... Thank you!</p>

<p><strong>Response:</strong> Here is my own answer and code sample:</p>

<p>After a whole day of research, I pretty much arrived at a satisfying solution. Just for clarity &ndash; I am going to reference to <strong>Autodesk Forge Design Automation API for Revit</strong>, simply as <strong>"Forge"</strong>.</p>

<p>Basically, the code provided above is correct.
I did not find any possible way to create a file on Forge instance, in a directory different than the Workitem working directory which is:</p>

<pre class="code">
  T:\Aces\Jobs\<workitem_id>\texture.png
</pre>

<p>Interestingly, there is a <code>C:\</code> drive on the Forge instance, which contains Windows, Revit and .NET Framework installations (as Forge instance is basically some sort of Windows instance with Revit installed). It is possible to enumerate a lot of these directories, but none of the ones I've tried (and I've tried a lot &ndash; mostly the most obvious, public access Windows directories like <code>C:\Users\Public</code>, <code>C:\Program Files</code>, etc.) allow for creation of directories or files. This corresponds to what is stated in "Restrictions" area of the Forge documentation:</p>

<blockquote>
  <p>Your application is run with low privileges, and will not be able to freely interact with the Windows OS:</p>
  
  <ul>
  <li>Write access is typically restricted to the job’s working folder.</li>
  <li>Registry access is mostly restricted, writing to the registry should be avoided.</li>
  <li>Any sub-process will also be executed with low privileges.</li>
  </ul>
</blockquote>

<p>So, after trying to save the "dummy" texture file somewhere on the Forge <code>C:\</code> drive, I've found another solution &ndash; <strong>the texture path for your texture actually does not matter.</strong></p>

<p>This is because Revit offers an alternative for re-linking your textures.
If you fire up Revit, you can go to File &gt; Options &gt; Rendering, and under "Additional render appearance paths" field, you can specify the directories on your local machine, that Revit can use to look for missing textures.
With these, you can do the following operations in order to have full control on creating materials on Forge:</p>

<ol>
<li>Send Workitem to Forge, create the materials.</li>
<li>Create a dummy texture in working directory, with the correct file name.</li>
<li>Attach the dummy texture file to the material.</li>
<li>Output the resulting file (.rvt or .rfa, depending on what you're creating on Forge).</li>
<li>Place all textures into one folder (or multiple, this doesn't matter that much).</li>
<li>Add the directories with the textures to the Additional render appearance paths.</li>
<li>Revit will successfully re-link all the textures to new paths.</li>
</ol>

<p>I hope someone will find this useful!</p>

<p>Additionally, as per Jeremy's request, I post a code sample for creating material with texture and modifying different Appearance properties in Revit by using Revit API (in C#):</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetAppearanceParameters(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;project,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Material</span>&nbsp;mat,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MaterialData&nbsp;data&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;setParameters&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;project,&nbsp;<span style="color:#a31515;">&quot;Set&nbsp;material&nbsp;parameters&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;setParameters.Start();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AppearanceAssetElement</span>&nbsp;genericAsset&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;project&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">AppearanceAssetElement</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.ToElements()
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">AppearanceAssetElement</span>&gt;().Where(&nbsp;i
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&gt;&nbsp;i.Name.Contains(&nbsp;<span style="color:#a31515;">&quot;Generic&quot;</span>&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AppearanceAssetElement</span>&nbsp;newAsset&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;genericAsset.Duplicate(&nbsp;data.Name&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mat.AppearanceAssetId&nbsp;=&nbsp;newAsset.Id;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">AppearanceAssetEditScope</span>&nbsp;editAsset&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">AppearanceAssetEditScope</span>(&nbsp;project&nbsp;)&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset&nbsp;=&nbsp;editAsset.Start(&nbsp;newAsset.Id&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetProperty</span>&nbsp;assetProperty&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;editableAsset[&nbsp;<span style="color:#a31515;">&quot;generic_diffuse&quot;</span>&nbsp;];

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetColor(&nbsp;editableAsset,&nbsp;data.MaterialAppearance.Color&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetGlossiness(&nbsp;editableAsset,&nbsp;data.MaterialAppearance.Gloss&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetReflectivity(&nbsp;editableAsset,&nbsp;data.MaterialAppearance.Reflectivity&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SetTransparency(&nbsp;editableAsset,&nbsp;data.MaterialAppearance.Transparency&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;data.MaterialAppearance.Texture&nbsp;!=&nbsp;<span style="color:blue;">null</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;data.MaterialAppearance.Texture.Length&nbsp;!=&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AddTexturePath(&nbsp;assetProperty,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:maroon;">$@&quot;C:\</span>{data.MaterialIdentity.Manufacturer}<span style="color:maroon;">\textures\</span>{data.MaterialAppearance.Texture}<span style="color:maroon;">&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;editAsset.Commit(&nbsp;<span style="color:blue;">true</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;setParameters.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetTransparency(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;transparency&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDouble</span>&nbsp;genericTransparency&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;editableAsset[&nbsp;<span style="color:#a31515;">&quot;generic_transparency&quot;</span>&nbsp;]
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">AssetPropertyDouble</span>;

&nbsp;&nbsp;&nbsp;&nbsp;genericTransparency.Value&nbsp;=&nbsp;Convert.ToDouble(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;transparency&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetReflectivity(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;reflectivity&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDouble</span>&nbsp;genericReflectivityZero
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(<span style="color:#2b91af;">AssetPropertyDouble</span>)&nbsp;editableAsset[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;generic_reflectivity_at_0deg&quot;</span>&nbsp;];

&nbsp;&nbsp;&nbsp;&nbsp;genericReflectivityZero.Value&nbsp;=&nbsp;Convert.ToDouble(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reflectivity&nbsp;)&nbsp;/&nbsp;100;

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDouble</span>&nbsp;genericReflectivityAngle
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(<span style="color:#2b91af;">AssetPropertyDouble</span>)&nbsp;editableAsset[
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;generic_reflectivity_at_90deg&quot;</span>&nbsp;];

&nbsp;&nbsp;&nbsp;&nbsp;genericReflectivityAngle.Value&nbsp;=&nbsp;Convert.ToDouble(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reflectivity&nbsp;)&nbsp;/&nbsp;100;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetGlossiness(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>&nbsp;gloss&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDouble</span>&nbsp;glossProperty&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(<span style="color:#2b91af;">AssetPropertyDouble</span>)&nbsp;editableAsset[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;generic_glossiness&quot;</span>&nbsp;];

&nbsp;&nbsp;&nbsp;&nbsp;glossProperty.Value&nbsp;=&nbsp;Convert.ToDouble(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;gloss&nbsp;)&nbsp;/&nbsp;100;
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;SetColor(
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;editableAsset,
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">int</span>[]&nbsp;color&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>&nbsp;genericDiffuseColor&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(<span style="color:#2b91af;">AssetPropertyDoubleArray4d</span>)&nbsp;editableAsset[&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;generic_diffuse&quot;</span>&nbsp;];

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Color</span>&nbsp;newColor&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Color</span>(&nbsp;(<span style="color:blue;">byte</span>)&nbsp;color[&nbsp;0&nbsp;],&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span style="color:blue;">byte</span>)&nbsp;color[&nbsp;1&nbsp;],&nbsp;(<span style="color:blue;">byte</span>)&nbsp;color[&nbsp;2&nbsp;]&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;genericDiffuseColor.SetValueAsColor(&nbsp;newColor&nbsp;);
&nbsp;&nbsp;}

&nbsp;&nbsp;<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">void</span>&nbsp;AddTexturePath(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetProperty</span>&nbsp;asset,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;texturePath&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Asset</span>&nbsp;connectedAsset&nbsp;=&nbsp;<span style="color:blue;">null</span>;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;asset.NumberOfConnectedProperties&nbsp;==&nbsp;0&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;asset.AddConnectedAsset(&nbsp;<span style="color:#a31515;">&quot;UnifiedBitmapSchema&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;connectedAsset&nbsp;=&nbsp;(<span style="color:#2b91af;">Asset</span>)&nbsp;asset.GetConnectedProperty(&nbsp;0&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetProperty</span>&nbsp;prop&nbsp;=&nbsp;connectedAsset.FindByName(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UnifiedBitmap</span>.UnifiedbitmapBitmap&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">AssetPropertyString</span>&nbsp;path&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;(<span style="color:#2b91af;">AssetPropertyString</span>)&nbsp;connectedAsset.FindByName(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">UnifiedBitmap</span>.UnifiedbitmapBitmap&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;fileName&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.GetFileName(&nbsp;texturePath&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">File</span>.Create(&nbsp;fileName&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;texturePath&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.GetFullPath(&nbsp;fileName&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;path.Value&nbsp;=&nbsp;texturePath;
&nbsp;&nbsp;}
</pre>

<p>Hopefully it will come in handy to someone in the future!</p>

<p>Also, a huge thanks for The Building Coder; it has saved me a lot of hassle in my work with Revit API and enabled to create a lot of cool stuff!</p>

<p>A great thanks back to maleficca for kindly sharing the two solutions, both for Forge and the Revit desktop API!</p>

<h4><a name="3"></a> Export Image Cutting off Pixels</h4>

<p>This query just came up again in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/export-image-cutting-off-edge-pixels/m-p/9578304">export image cutting off edge pixels</a>:</p>

<p><strong>Question:</strong> I'm trying to export family type images from a family, but I'm getting the edge pixels cut off in my exported images.</p>

<p>Here is the original image:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e950dec4200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e950dec4200b img-responsive" style="width: 155px; display: block; margin-left: auto; margin-right: auto;" alt="Original image" title="Original image" src="/assets/image_7e4768.jpg" /></a><br /></p>

<p></center></p>

<p>This is what comes out:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330264e2e2f5b5200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330264e2e2f5b5200d img-responsive" style="width: 150px; display: block; margin-left: auto; margin-right: auto;"alt="Exported image" title="Exported image" src="/assets/image_f16732.jpg" /></a><br /></p>

<p></center></p>

<p>This is what I currently have for my export image options</p>

<pre class="code">
&nbsp;&nbsp;img&nbsp;=&nbsp;ImageExportOptions()
&nbsp;&nbsp;img.ZoomType&nbsp;=&nbsp;<span style="color:#2b91af;">ZoomFitType</span>.FitToPage
&nbsp;&nbsp;img.PixelSize&nbsp;=&nbsp;size
&nbsp;&nbsp;img.ImageResolution&nbsp;=&nbsp;<span style="color:#2b91af;">ImageResolution</span>.DPI_150
&nbsp;&nbsp;img.FitDirection&nbsp;=&nbsp;<span style="color:#2b91af;">FitDirectionType</span>.Vertical
&nbsp;&nbsp;img.ExportRange&nbsp;=&nbsp;<span style="color:#2b91af;">ExportRange</span>.SetOfViews
&nbsp;&nbsp;img.SetViewsAndSheets(&nbsp;viewIds&nbsp;)
&nbsp;&nbsp;img.HLRandWFViewsFileType&nbsp;=&nbsp;<span style="color:#2b91af;">ImageFileType</span>.PNG
&nbsp;&nbsp;img.FilePath&nbsp;=&nbsp;filepath
&nbsp;&nbsp;img.ShadowViewsFileType&nbsp;=&nbsp;<span style="color:#2b91af;">ImageFileType</span>.PNG
</pre>

<p>A solution was provided by <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/7761409">alexpaduroiu</a> in a previous conversation
on why <a href="https://forums.autodesk.com/t5/revit-api-forum/export-image-is-cutting-few-pixels-from-image-corners/m-p/9346019">export image is cutting a few pixels from image corners</a>:</p>

<p><strong>Question:</strong> I have a small problem regarding <code>Decoument.ExportImage(ImageExportOptions options)</code>.</p>

<p>I am trying to export a set of drafting views, but somehow the generated images cut the views edges.</p>

<p>A sample image:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263ec228045200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263ec228045200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Incomplete image" title="Incomplete image" src="/assets/image_eec16a.jpg" /></a><br /></p>

<p></center></p>

<p>This image has the bottom part not visible at all:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330264e2e2f5bf200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330264e2e2f5bf200d img-responsive" style="width: 279px; display: block; margin-left: auto; margin-right: auto;" alt="Incomplete image" title="Incomplete image" src="/assets/image_9325e9.jpg" /></a><br /></p>

<p></center></p>

<p>The cut is very small but very frustrating because I can't make it a whole or even offset the element to fit the images.</p>

<p>Image Export Options used:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">ImageExportOptions</span>&nbsp;imgExportOpts&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ImageExportOptions</span>()
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ZoomType&nbsp;=&nbsp;<span style="color:#2b91af;">ZoomFitType</span>.FitToPage,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PixelSize&nbsp;=&nbsp;500,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FilePath&nbsp;=&nbsp;rbrImagesDirectory&nbsp;+&nbsp;<span style="color:maroon;">@&quot;\&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FitDirection&nbsp;=&nbsp;<span style="color:#2b91af;">FitDirectionType</span>.Vertical,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HLRandWFViewsFileType&nbsp;=&nbsp;<span style="color:#2b91af;">ImageFileType</span>.PNG,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ShadowViewsFileType&nbsp;=&nbsp;<span style="color:#2b91af;">ImageFileType</span>.PNG,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageResolution&nbsp;=&nbsp;<span style="color:#2b91af;">ImageResolution</span>.DPI_72,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ShouldCreateWebSite&nbsp;=&nbsp;<span style="color:blue;">false</span>,
&nbsp;&nbsp;&nbsp;&nbsp;};

  imgExportOpts.ExportRange&nbsp;=&nbsp;<span style="color:#2b91af;">ExportRange</span>.SetOfViews;
</pre>

<p>I have tried to modify <code>FitDirectionType.Horizontal</code> and this makes it worse than it is now by cutting the bottom portion even more; in that case, for the first image, the bottom part of the bar is not visible at all.</p>

<p>The image doesn't have such a big cut in the edges but it will be nice to have some spaces there or at least to see the parts &nbsp; :-)</p>

<p>Is there any way to zoom out the element or move it in order to be arranged better in image?</p>

<p><strong>Answer:</strong> Well, I have solved the problem!</p>

<p>The problem was with the drafting view I was trying to export. After creating a group of details in the drafting view, somehow the outline of the view wasn't big enough to include my details. don't know for sure why, but what I have done to resolve the problem was the following:</p>

<pre class="code">
&nbsp;&nbsp;drafting.CropBoxActive&nbsp;=&nbsp;<span style="color:blue;">true</span>;
&nbsp;&nbsp;drafting.CropBoxVisible&nbsp;=&nbsp;<span style="color:blue;">true</span>;

<span style="color:blue;">private</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">void</span>&nbsp;ExtendViewCrop(
&nbsp;&nbsp;<span style="color:#2b91af;">View</span>&nbsp;drafting,
&nbsp;&nbsp;<span style="color:#2b91af;">Group</span>&nbsp;detail&nbsp;)
{
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;crop&nbsp;=&nbsp;(drafting&nbsp;!=&nbsp;<span style="color:blue;">null</span>
&nbsp;&nbsp;&nbsp;&nbsp;?&nbsp;drafting.CropBox
&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span style="color:blue;">null</span>);

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;crop&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;crop.Max&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;crop.Min&nbsp;==&nbsp;<span style="color:blue;">null</span>
&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;crop.Transform&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;detail&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;detailBox&nbsp;=&nbsp;detail.get_BoundingBox(&nbsp;drafting&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>&nbsp;extendedCrop&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">BoundingBoxXYZ</span>();
&nbsp;&nbsp;extendedCrop.Transform&nbsp;=&nbsp;crop.Transform;

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;detailBox&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;||&nbsp;detailBox.Max&nbsp;==&nbsp;<span style="color:blue;">null</span>
&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;detailBox.Min&nbsp;==&nbsp;<span style="color:blue;">null</span>&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>;
&nbsp;&nbsp;}
&nbsp;&nbsp;extendedCrop.Max&nbsp;=&nbsp;detailBox.Max
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;(extendedCrop.Transform.BasisX
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;extendedCrop.Transform.BasisY&nbsp;/&nbsp;2)&nbsp;*&nbsp;0.03;

&nbsp;&nbsp;extendedCrop.Min&nbsp;=&nbsp;detailBox.Min
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;(-extendedCrop.Transform.BasisX
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;-extendedCrop.Transform.BasisY&nbsp;/&nbsp;2)&nbsp;*&nbsp;0.03;

&nbsp;&nbsp;drafting.CropBox&nbsp;=&nbsp;extendedCrop;
}
</pre>

<p>So basically, extending the view crops max and min on their direction with a small value so the detail will fit in my drafting.</p>

<p>I am sure that there are lots of other better options doing this, but for now it did the trick.</p>

<p>Of course I am open to more solutions &nbsp; :-)</p>

<p>Many thanks to alexpaduroiu for this clear solution.</p>

<h4><a name="4"></a> Change Level of Existing Element</h4>

<p>Angelo Mastroberardino brought up this question in
his <a href="https://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html#comment-4952460602">comment</a>
on <a href="https://thebuildingcoder.typepad.com/blog/2014/03/creating-a-sloped-floor.html">creating a sloped floor</a>:</p>

<p><strong>Question:</strong> Is it possible to re-set the reference Level of a Floor, once it is created ?</p>

<p><strong>Answer:</strong>  In general, the <code>Level</code> property is read-only and thus cannot be set after an element has been created.
It is specified during creation only and cannot be modified later.</p>

<p>Here are some posts on levels, both general level handling issues and workarounds to set the level property on certain specific element types:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2014/11/webgl-goes-mobile-and-sorted-level-retrieval.html#3">Retrieve levels sorted by elevation</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2011/01/family-instance-missing-level-property.html">Family instances lacking or with invalid level property</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#3">Changing the level of a floor</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/03/assigning-a-level-to-an-element-missing-it.html">Calculate a level for an element lacking the property</a></li>
<li>A solution to <a href="https://thebuildingcoder.typepad.com/blog/2020/03/panel-schedule-slots-and-change-room-level.html">change the level of an existing room</a></li>
</ul>

<h4><a name="5"></a> Physics is Cool</h4>

<p>A very nice and surprising <a href="https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool">physics experiment to try out at home</a>:</p>

<p><center>
<!--</p>

<blockquote class="reddit-card">
<a href="https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool/">Physics is cool</a> 
</blockquote>

<p><script async src="//embed.redditmedia.com/widgets/platform.js" charset="UTF-8"></script>
--></p>

<p><a class="asset-img-link"  href="https://www.reddit.com/r/BeAmazed/comments/gxrq8p/physics_is_cool">
<img class="asset  asset-image at-xid-6a00e553e1689788330263ec228080200c img-responsive" style="width: 125px; display: block; margin-left: auto; margin-right: auto;" alt="Physics is cool" title="Physics is cool"  src="/assets/image_28e796.jpg" />
</a><br /></p>

<p></center></p>

<h4><a name="6"></a> Forge Job Openings</h4>

<p>Are you a critical thinker, problem solver, story teller, goal oriented, smart, curios, empathic, with experience in a cloud environment such as AWS?</p>

<p>If so, would you like to consider applying for a job in the Forge team?</p>

<ul>
<li><a href="https://rolp.co/SS7ji">20WD39627 – Senior Vendor Manager – San Francisco</a></li>
<li><a href="https://rolp.co/4Obwi">20WD38934 &ndash; Localization Software Engineer &ndash; Singapore</a></li>
<li><a href="https://rolp.co/Q6cUi">20WD37407 &ndash; Senior Product Manager, Data &ndash; Montreal</a></li>
<li><a href="https://rolp.co/pVKii">20WD40315 &ndash; Senior Data Engineer/Architect &ndash; Novi </a></li>
</ul>

<p>Good luck applying for one of these or the many other opportunities that you can find all over the world in
the <a href="https://www.autodesk.com/careers">Autodesk career site</a>!</p>

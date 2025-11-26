---
layout: "post"
title: "RevitAPI: How to get asset properties of material I want?"
date: "2015-03-10 02:26:10"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/03/revitapi-how-to-get-asset-properties-of-material-i-want.html "
typepad_basename: "revitapi-how-to-get-asset-properties-of-material-i-want"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/44082713">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p><strong>Question:</strong></p>
<p>I was asked about how to get xxx property from material several times, since there are too many properties, it is hard to find the exact one we want. and developers may stops here:</p>
<pre class="csharp prettyprint">private void CustomerApproach(Material material)
{
    ElementId appearanceId = material.AppearanceAssetId;
    AppearanceAssetElement appearanceElem = 
        RevitDoc.GetElement(appearanceId) as AppearanceAssetElement;
    Asset theAsset = appearanceElem.GetRenderingAsset();
    for (int idx = 0; idx &lt; theAsset.Size; idx++)
    {
        ////Too many properties, how to know which one is I want?
        AssetProperty ap = theAsset[idx];
    }
}</pre>
<p><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08026480970d-pi" style="display: inline;"><img alt="2FirstEdit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08026480970d image-full img-responsive" src="/assets/image_395088.jpg" title="2FirstEdit" /></a><br /><br /></p>
<p>Well, I don&#39;t know the answer either, but we can do a simple test, like this:</p>
<pre class="csharp prettyprint">private void DumpAppearanceAssetProperties(Material material)
{
    ElementId appearanceId = material.AppearanceAssetId;
    AppearanceAssetElement appearanceElem = material.Document.
        GetElement(appearanceId) as AppearanceAssetElement;
    Asset theAsset = appearanceElem.GetRenderingAsset();
    List&lt;AssetProperty&gt; assets = new List&lt;AssetProperty&gt;();
    for (int idx = 0; idx &lt; theAsset.Size; idx++)
    {
        AssetProperty ap = theAsset[idx];
        assets.Add(ap);
    }
    // order the properties!
    assets = assets.OrderBy(ap =&gt; ap.Name).ToList();
    for (int idx = 0; idx &lt; assets.Count; idx++)
    {
        AssetProperty ap = assets[idx];
        Type type = ap.GetType();
        object apVal = null;
        try
        {
            // using .net reflection to get the value
            var prop = type.GetProperty(&quot;Value&quot;);
            if (prop != null &amp;&amp; 
                prop.GetIndexParameters().Length == 0)
            {
                apVal = prop.GetValue(ap);
            }
            else
            {
                apVal = &quot;&lt;No Value Property&gt;&quot;;
            }
        }
        catch (Exception ex)
        {
            apVal = ex.GetType().Name + &quot;-&quot; + ex.Message;
        }

        if (apVal is DoubleArray)
        {
            var doubles = apVal as DoubleArray;
            apVal = doubles.Cast&lt;double&gt;().Aggregate(&quot;&quot;, (s, d) =&gt; s + Math.Round(d,5) + &quot;,&quot;);
        }
        Log(idx + &quot; : [&quot; + ap.Type + &quot;] &quot; + ap.Name + &quot; = &quot; + apVal);
    }
}</pre>
<p><br /> You might still ask even if we dump all the properties, we still can&#39;t find the one, because the output looks like this:</p>
<p>result1:</p>
<pre class="plain">0 : [APT_String] AdvancedUIDefinition = Mats/Generic/GenericAdvancedUI.xml 
1 : [APT_String] AssetLibID = AD121259-C03E-4A1D-92D8-59A22B4807AD 
2 : [APT_String] assettype = materialappearance 
3 : [APT_String] BaseSchema = GenericSchema 
4 : [APT_String] category = :Concrete:Default:Miscellaneous 
5 : [APT_Boolean] color_by_object = False 
6 : [APT_Integer] common_Shared_Asset = 1 
7 : [APT_DoubleArray4d] common_Tint_color = 0.315,0.315,0.315,1, 
8 : [APT_Boolean] common_Tint_toggle = False 
9 : [APT_String] description = Generic material. 
10 : [APT_String] ExchangeGUID =  
11 : [APT_Boolean] generic_ao_details = True 
12 : [APT_Double] generic_ao_distance = 4 
13 : [APT_Boolean] generic_ao_on = False 
14 : [APT_Integer] generic_ao_samples = 16 
15 : [APT_Boolean] generic_backface_cull = False 
16 : [APT_Double] generic_bump_amount = 9.39325797868605E-275 
17 : [APT_DoubleArray4d] generic_bump_map = 0,0,0,1, 
18 : [APT_Double] generic_cutout_opacity = 1 
19 : [APT_DoubleArray4d] generic_diffuse = 0.51353,0.52278,0.47005,1, 
20 : [APT_Double] generic_diffuse_image_fade = 1 
21 : [APT_Double] generic_glossiness = 0 
22 : [APT_Boolean] generic_is_metal = False 
23 : [APT_Integer] generic_refl_depth = 0 
24 : [APT_Integer] generic_reflection_glossy_samples = 12 
25 : [APT_Double] generic_reflectivity_at_0deg = 0.5 
26 : [APT_Double] generic_reflectivity_at_90deg = 0.5 
27 : [APT_Integer] generic_refr_depth = -1 
28 : [APT_Integer] generic_refraction_glossy_samples = 12 
29 : [APT_Double] generic_refraction_index = 1 
30 : [APT_Double] generic_refraction_translucency_weight = 0 
31 : [APT_Boolean] generic_roundcorners_allow_different_materials = False 
32 : [APT_Double] generic_roundcorners_radius = 0 
33 : [APT_Double] generic_self_illum_color_temperature = 6500 
34 : [APT_DoubleArray4d] generic_self_illum_filter_map = 1,1,1,1, 
35 : [APT_Double] generic_self_illum_luminance = 0 
36 : [APT_Double] generic_transparency = 0 
37 : [APT_Double] generic_transparency_image_fade = 1 
38 : [APT_Boolean] Hidden = False 
39 : [APT_String] ImplementationGeneric =  
40 : [APT_String] ImplementationMentalRay = Mats/Generic/MentalImage.xml 
41 : [APT_String] ImplementationOGS = Mats/Generic/OGS.xml 
42 : [APT_String] ImplementationPreview = Mats/Generic/PreviewColor.xml 
43 : [APT_String] keyword =  
44 : [APT_String] localname = Generic 
45 : [APT_String] localtype = Appearance 
46 : [APT_Integer] mode = 4 
47 : [APT_DoubleArray2d] PatternOffset = 0,0, 
48 : [APT_Integer] revision = 1 
49 : [APT_Integer] SchemaVersion = 4 
50 : [APT_String] swatch = Swatch-Cube 
51 : [APT_String] thumbnail = C:/Users/lua.ADS/AppData/Local/Temp/MaterialThumbnails_PID_1434/73c06a00Smooth Precast Structural.png 
52 : [APT_String] UIDefinition = Mats/Generic/GenericUI.xml 
53 : [APT_String] UIName = Smooth Precast Structural 
54 : [APT_Integer] version = 2 
55 : [APT_String] VersionGUID = B1E74BC9-150D-4CB2-BFF1-376BEAD5FEAE </pre>
<p><br /> Well, let&#39;s update some property and dump again, e.g. I changed properties under &quot;Reflectivity&quot; group, the result is:</p>
<pre class="plain">0 : [APT_String] AdvancedUIDefinition = Mats/Generic/GenericAdvancedUI.xml 
1 : [APT_String] AssetLibID = AD121259-C03E-4A1D-92D8-59A22B4807AD 
2 : [APT_String] assettype = materialappearance 
3 : [APT_String] BaseSchema = GenericSchema 
4 : [APT_String] category = :Concrete:Default:Miscellaneous 
5 : [APT_Boolean] color_by_object = False 
6 : [APT_Integer] common_Shared_Asset = 1 
7 : [APT_DoubleArray4d] common_Tint_color = 0.315,0.315,0.315,1, 
8 : [APT_Boolean] common_Tint_toggle = False 
9 : [APT_String] description = Generic material. 
10 : [APT_String] ExchangeGUID =  
11 : [APT_Boolean] generic_ao_details = True 
12 : [APT_Double] generic_ao_distance = 4 
13 : [APT_Boolean] generic_ao_on = False 
14 : [APT_Integer] generic_ao_samples = 16 
15 : [APT_Boolean] generic_backface_cull = False 
16 : [APT_Double] generic_bump_amount = 9.39325797868605E-275 
17 : [APT_DoubleArray4d] generic_bump_map = 0,0,0,1, 
18 : [APT_Double] generic_cutout_opacity = 1 
19 : [APT_DoubleArray4d] generic_diffuse = 0.51353,0.52278,0.47005,1, 
20 : [APT_Double] generic_diffuse_image_fade = 1 
21 : [APT_Double] generic_glossiness = 0 
22 : [APT_Boolean] generic_is_metal = False 
23 : [APT_Integer] generic_refl_depth = 0 
24 : [APT_Integer] generic_reflection_glossy_samples = 12 
25 : [APT_Double] generic_reflectivity_at_0deg = 0.2 
26 : [APT_Double] generic_reflectivity_at_90deg = 0.96 
27 : [APT_Integer] generic_refr_depth = -1 
28 : [APT_Integer] generic_refraction_glossy_samples = 12 
29 : [APT_Double] generic_refraction_index = 1 
30 : [APT_Double] generic_refraction_translucency_weight = 0 
31 : [APT_Boolean] generic_roundcorners_allow_different_materials = False 
32 : [APT_Double] generic_roundcorners_radius = 0 
33 : [APT_Double] generic_self_illum_color_temperature = 6500 
34 : [APT_DoubleArray4d] generic_self_illum_filter_map = 1,1,1,1, 
35 : [APT_Double] generic_self_illum_luminance = 0 
36 : [APT_Double] generic_transparency = 0 
37 : [APT_Double] generic_transparency_image_fade = 1 
38 : [APT_Boolean] Hidden = False 
39 : [APT_String] ImplementationGeneric =  
40 : [APT_String] ImplementationMentalRay = Mats/Generic/MentalImage.xml 
41 : [APT_String] ImplementationOGS = Mats/Generic/OGS.xml 
42 : [APT_String] ImplementationPreview = Mats/Generic/PreviewColor.xml 
43 : [APT_String] keyword =  
44 : [APT_String] localname = Generic 
45 : [APT_String] localtype = Appearance 
46 : [APT_Integer] mode = 4 
47 : [APT_DoubleArray2d] PatternOffset = 0,0, 
48 : [APT_Integer] revision = 1 
49 : [APT_Integer] SchemaVersion = 4 
50 : [APT_String] swatch = Swatch-Cube 
51 : [APT_String] thumbnail = C:/Users/lua.ADS/AppData/Local/Temp/MaterialThumbnails_PID_1434/70db4100Smooth Precast Structural(7).png 
52 : [APT_String] UIDefinition = Mats/Generic/GenericUI.xml 
53 : [APT_String] UIName = Smooth Precast Structural 
54 : [APT_Integer] version = 2 
55 : [APT_String] VersionGUID = E5E10B97-7ED0-4EE5-B64B-314565517119 </pre>
<p><br /> You got the idea?</p>
<p>Yes, compare the results!</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0802648b970d-pi" style="display: inline;"><img alt="2vs3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0802648b970d image-full img-responsive" src="/assets/image_79152.jpg" title="2vs3" /></a></p>
<p>&#0160;</p>
<p>It is now that obvious, those are the properties we changed:</p>
<p>generic_reflectivity_at_0deg</p>
<p>generic_reflectivity_at_90deg</p>
<p>&#0160;</p>
<p>Note：</p>
<ul>
<li>I put all AssetProperty in a list and order them by name, otherwise, it might be a mess and not situable to compare.</li>
<li>If AssetProperty is a AssetPropertyReference, we can call GetAllConnectedProperties() to iterate the sub-properties of it.</li>
</ul>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>

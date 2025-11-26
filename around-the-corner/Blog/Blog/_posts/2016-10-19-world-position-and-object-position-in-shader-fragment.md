---
layout: "post"
title: "World position and Object position in Shader Fragment"
date: "2016-10-19 23:51:27"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Maya"
  - "Shader"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/10/world-position-and-object-position-in-shader-fragment.html "
typepad_basename: "world-position-and-object-position-in-shader-fragment"
typepad_status: "Publish"
---

<head>
<style>
table, th, td {
    border: 1px solid black;
}
</style>
</head>
<p>When you write a Shader Fragment, you should use the right naming conventions. For example if you check parameters like “mayaUvCoordSemantic” the name should be “uvCoord”, for “tangent” the name should be “mayaTangentIn”.</p>
<p>Even if you check the “Object Space Position” the name and the semantic both should be “Pm”. Check the below example.</p>
<pre class="brush: cpp;toolbar: false;">&lt;fragment uiName=\"depthShaderPluginInterpolantFragment\" name=\"depthShaderPluginInterpolantFragment\" type=\"interpolant\" class=\"ShadeFragment\" version=\"1.0\"&gt;"
&lt;description&gt;&lt;![CDATA[Depth shader vertex fragment]]&gt;&lt;/description&gt;"
&lt;properties&gt;
&lt;float3 name=\"Pm\" semantic=\"Pm\" flags=\"varyingInputParam\" /&gt;
&lt;float4x4 name=\"worldViewProj\" semantic=\"worldviewprojection\" /&gt;
&lt;/properties&gt;
&lt;vertex_source&gt;&lt;![CDATA["
float idepthShaderPluginInterpolantFragment(float3 Pm, float4x4 worldViewProj)
{
float4 pCamera = mul(worldViewProj, float4(Pm, 1.0f));
return (pCamera.z - pCamera.w*2.0f); \n"
} ]]&gt;
&lt;/vertex_source&gt;
</pre>
<p>If you are defining, "World Space Position" the semantic and the name should be “Pw” instead of any other string like “out”, "outPosition",... like below. If you use any other string, Maya won't consider it as varyingInputParameters.</p>
<pre class="brush: cpp;toolbar: false;">&lt;float3 name=\"Pw\” semantic=\"Pw\” flags=\"varyingInputParam\" /&gt;"
...
&lt;![CDATA[float4 fragTexture(float2 uv, texture2D map, sampler2D textureSampler, float4x4 texMatrix, float3 Pw)
...
</pre>
<table>
<thead>
<tr>
<th>Semantic</th>
<th>Name</th>
<th>Type</th>
<th>Meaning</th>
</tr>
</thead>
<tbody>
<tr>
<td>Pm</td>
<td>Pm</td>
<td>3-float</td>
<td>Object space position</td>
</tr>
<tr>
<td>Pw</td>
<td>Pw</td>
<td>3-float</td>
<td>World space position</td>
</tr>
<tr>
<td>Pv</td>
<td>Pv</td>
<td>3-float</td>
<td>View space position</td>
</tr>
<tr>
<td>Nm</td>
<td>Nm</td>
<td>3-float</td>
<td>Object space normal</td>
</tr>
<tr>
<td>Nw</td>
<td>Nw</td>
<td>3-float</td>
<td>World space normal</td>
</tr>
</tbody>
</table>
<p>
<p>For more information please check <strong>“Varying Parameters and System Parameters”</strong> section in the following document</p>
<p><a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__files_GUID_585F5656_4069_4D82_B9BB_3D1AB2D0DFE6_htm" target="_blank">http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=<em><em>files</em>GUID</em>585F5656<em>4069<em>4D82</em>B9BB</em>3D1AB2D0DFE6_htm</a></p>

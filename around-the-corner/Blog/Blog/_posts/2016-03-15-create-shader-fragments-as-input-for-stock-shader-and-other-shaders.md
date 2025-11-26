---
layout: "post"
title: "Create shader fragments as input for stock shader and other shaders"
date: "2016-03-15 18:50:05"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Shader"
original_url: "https://around-the-corner.typepad.com/adn/2016/03/create-shader-fragments-as-input-for-stock-shader-and-other-shaders.html "
typepad_basename: "create-shader-fragments-as-input-for-stock-shader-and-other-shaders"
typepad_status: "Publish"
---

<style type="text/css" media="screen">

    pre{
        font-size:12px;
	font-family:consolas, courier-new, monospace;
    }</style>  <p>The shader fragment feature was introduced into Maya a few years ago and it is very useful for creating and extending shader features of Maya. Shader fragments can be used for creating input or output for existing hardware shaders. I am going explain how it works with two samples in our devkit.</p>  <p>The first sample is VP2BlinnShader and it uses blinn stock shader as hardware shader. Another sample is fileTexture and it uses shader fragment to implement a fileTexture node.</p>  <p>Our goal is to create a VP2BlinnTextureShader that accepts an image as input for the blinn stock shader.</p>  <p><img alt="Brief" src="/assets/ShaderBrief.png" /></p>  <p>We can use MShaderInstance::addInputFragment and MShaderInstnace::addOutputFragment to add an input or output shader fragment.</p>  <p>First, we need to get parameters of blinn stock shader to find the the parameter we need. We can use MShaderInstance::parameterList and MShaderInstance::parameterType to get parameters and their types. To add a texture for blinn stock shader, diffuseColor is the one we need. It is float4. So, we are going to add our fragment shader as input of diffuseColor.</p>  <p>Then, we are going to create a fragment shader. We can reuse the shader fragment in the fileTexture</p>  <pre>void initializeFragmentShaders()
{
    static const MString sFragmentName(&quot;fileTexturePluginFragment&quot;);
    static const char* sFragmentBody =
        &quot;&lt;fragment uiName=\&quot;fileTexturePluginFragment\&quot; name=\&quot;fileTexturePluginFragment\&quot; type=\&quot;plumbing\&quot; class=\&quot;ShadeFragment\&quot; version=\&quot;1.0\&quot;&gt;&quot;
        &quot;   &lt;description&gt;&lt;![CDATA[Simple file texture fragment]]&gt;&lt;/description&gt;&quot;
        &quot;   &lt;properties&gt;&quot;
        &quot;       &lt;float2 name=\&quot;uvCoord\&quot; semantic=\&quot;mayaUvCoordSemantic\&quot; flags=\&quot;varyingInputParam\&quot; /&gt;&quot;
        &quot;       &lt;texture2 name=\&quot;map\&quot; /&gt;&quot;
        &quot;       &lt;sampler name=\&quot;textureSampler\&quot; /&gt;&quot;
        &quot;   &lt;/properties&gt;&quot;
        &quot;   &lt;values&gt;&quot;
        &quot;   &lt;/values&gt;&quot;
        &quot;   &lt;outputs&gt;&quot;
        &quot;       &lt;float4 name=\&quot;output\&quot; /&gt;&quot;
        &quot;   &lt;/outputs&gt;&quot;
        &quot;   &lt;implementation&gt;&quot;
        &quot;   &lt;implementation render=\&quot;OGSRenderer\&quot; language=\&quot;Cg\&quot; lang_version=\&quot;2.100000\&quot;&gt;&quot;
        &quot;       &lt;function_name val=\&quot;fileTexturePluginFragment\&quot; /&gt;&quot;
        &quot;       &lt;source&gt;&lt;![CDATA[&quot;
        &quot;float4 fileTexturePluginFragment(float2 uv, texture2D map, sampler2D mapSampler) \n&quot;
        &quot;{ \n&quot;
        &quot;   uv -= floor(uv); \n&quot;
        &quot;   uv.y = 1.0f - uv.y; \n&quot;
        &quot;   float4 color = tex2D(mapSampler, uv); \n&quot;
        &quot;   return color.rgba; \n&quot;
        &quot;} \n]]&gt;&quot;
        &quot;       &lt;/source&gt;&quot;
        &quot;   &lt;/implementation&gt;&quot;
        &quot;   &lt;implementation render=\&quot;OGSRenderer\&quot; language=\&quot;HLSL\&quot; lang_version=\&quot;11.000000\&quot;&gt;&quot;
        &quot;       &lt;function_name val=\&quot;fileTexturePluginFragment\&quot; /&gt;&quot;
        &quot;       &lt;source&gt;&lt;![CDATA[&quot;
        &quot;float4 fileTexturePluginFragment(float2 uv, Texture2D map, sampler mapSampler) \n&quot;
        &quot;{ \n&quot;
        &quot;   uv -= floor(uv); \n&quot;
        &quot;   uv.y = 1.0f - uv.y; \n&quot;
        &quot;   float4 color = map.Sample(mapSampler, uv); \n&quot;
        &quot;   return color.rgba; \n&quot;
        &quot;} \n]]&gt;&quot;
        &quot;       &lt;/source&gt;&quot;
        &quot;   &lt;/implementation&gt;&quot;
        &quot;   &lt;implementation render=\&quot;OGSRenderer\&quot; language=\&quot;GLSL\&quot; lang_version=\&quot;3.0\&quot;&gt;&quot;
        &quot;       &lt;function_name val=\&quot;fileTexturePluginFragment\&quot; /&gt;&quot;
        &quot;       &lt;source&gt;&lt;![CDATA[&quot;
        &quot;float4 fileTexturePluginFragment(vec2 uv, sampler2D mapSampler) \n&quot;
        &quot;{ \n&quot;
        &quot;   uv -= floor(uv); \n&quot;
        &quot;   uv.y = 1.0f - uv.y; \n&quot;
        &quot;   vec4 color = texture(mapSampler, uv); \n&quot;
        &quot;   return color.rgba; \n&quot;
        &quot;} \n]]&gt;&quot;
        &quot;       &lt;/source&gt;&quot;
        &quot;   &lt;/implementation&gt;&quot;
        &quot;   &lt;/implementation&gt;&quot;
        &quot;&lt;/fragment&gt;&quot;;
    // Register fragments with the manager if needed
    //
    MHWRender::MRenderer* theRenderer = MHWRender::MRenderer::theRenderer();
    if (theRenderer)
    {
        MHWRender::MFragmentManager* fragmentMgr =
            theRenderer-&gt;getFragmentManager();
        if (fragmentMgr)
        {
            // Add fragments if needed
            bool fragAdded = fragmentMgr-&gt;hasFragment(sFragmentName);                   
            if (!fragAdded)
            {
                fragAdded = (sFragmentName == fragmentMgr-&gt;addShadeFragmentFromBuffer(sFragmentBody, false));
            }               
            if (fragAdded)
            {
                fFragmentName = sFragmentName;
                TRACE_API_CALLS(&quot;Added Fragment&quot;);
            }
        }
    }
    }</pre>

<p>The first two lines are creating a shader fragment called fileTexturePluginFragment, and defining the body of the fragment. In the fragment body, we define it as a ShadeFragment with three input parameters: uvCoords of float2, texture of map and textureSampler of sampler. diffuseColor is float4 parameter, so we define the output of our fragment as float4.</p>

<pre>&quot;&lt;fragment uiName=\&quot;fileTexturePluginFragment\&quot; name=\&quot;fileTexturePluginFragment\&quot; type=\&quot;plumbing\&quot; class=\&quot;ShadeFragment\&quot; version=\&quot;1.0\&quot;&gt;&quot;
            &quot;   &lt;description&gt;&lt;![CDATA[Simple file texture fragment]]&gt;&lt;/description&gt;&quot;
            &quot;   &lt;properties&gt;&quot;
            &quot;       &lt;float2 name=\&quot;uvCoord\&quot; semantic=\&quot;mayaUvCoordSemantic\&quot; flags=\&quot;varyingInputParam\&quot; /&gt;&quot;
            &quot;       &lt;texture2 name=\&quot;map\&quot; /&gt;&quot;
            &quot;       &lt;sampler name=\&quot;textureSampler\&quot; /&gt;&quot;
            &quot;   &lt;/properties&gt;&quot;
            &quot;   &lt;values&gt;&quot;
            &quot;   &lt;/values&gt;&quot;
            &quot;   &lt;outputs&gt;&quot;
            &quot;       &lt;float4 name=\&quot;output\&quot; /&gt;&quot;
            &quot;   &lt;/outputs&gt;&quot;</pre>

<p>For uvCoords, we can let Maya provide the system parameter as the input value. We can define it as mayaUvCoordSemantic and varyingInputParam. For more details about Maya semantics, please check &quot;<em><strong>Varying Parameters and System Parameters</strong></em>&quot; section in this <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__files_GUID_585F5656_4069_4D82_B9BB_3D1AB2D0DFE6_htm">document</a>.</p>

<p>Then we are going to implement the shader using a different API. We can provide the same shader in different shader languages like GLSL, HLSL, Cg.</p>

<pre>&quot;   &lt;implementation render=\&quot;OGSRenderer\&quot; language=\&quot;HLSL\&quot; lang_version=\&quot;11.000000\&quot;&gt;&quot;
        &quot;       &lt;function_name val=\&quot;fileTexturePluginFragment\&quot; /&gt;&quot;
        &quot;       &lt;source&gt;&lt;![CDATA[&quot;
        &quot;float4 fileTexturePluginFragment(float2 uv, Texture2D map, sampler mapSampler) \n&quot;
        &quot;{ \n&quot;
        &quot;   uv -= floor(uv); \n&quot;
        &quot;   uv.y = 1.0f - uv.y; \n&quot;
        &quot;   float4 color = map.Sample(mapSampler, uv); \n&quot;
        &quot;   return color.rgba; \n&quot;
        &quot;} \n]]&gt;&quot;
        &quot;       &lt;/source&gt;&quot;
        &quot;   &lt;/implementation&gt;&quot;</pre>

<p>For example, the code above implements a dx shader for Maya default Renderer(OGSRenderer). The shader source code is in the source tag. It is a texture pass through.</p>

<p>Now, we need to register our shader fragment.</p>

<pre>// Register fragments with the manager if needed            
MHWRender::MRenderer* theRenderer = MHWRender::MRenderer::theRenderer();
if (theRenderer)
{
    MHWRender::MFragmentManager* fragmentMgr =
        theRenderer-&gt;getFragmentManager();
    if (fragmentMgr)
    {
        // Add fragments if needed
        bool fragAdded = fragmentMgr-&gt;hasFragment(sFragmentName);                   
        if (!fragAdded)
        {
            fragAdded = (sFragmentName == fragmentMgr-&gt;addShadeFragmentFromBuffer(sFragmentBody, false));
        }               
        if (fragAdded)
        {
            fFragmentName = sFragmentName;
            TRACE_API_CALLS(&quot;Added Fragment&quot;);
        }
    }
}</pre>

<p>We also need to create custom mapping for shader fragment parameters.</p>

<pre>void createCustomMappings()
{
    // Set up some mappings for the parameters on the file texture fragment,
    // there is no correspondence to attributes on the node for the texture
    // parameters.
    MHWRender::MAttributeParameterMapping mapMapping(
        &quot;map&quot;, &quot;&quot;, false, true);
    mappings.append(mapMapping);

    MHWRender::MAttributeParameterMapping textureSamplerMapping(
        &quot;textureSampler&quot;, &quot;&quot;, false, true);
    mappings.append(textureSamplerMapping);
}</pre>

<p>Next, we can make our shader fragment as input parameter for our stock shader in the MPxShaderOverride::createShaderInstance.</p>

<pre>...
// Create texture fragment shader
initializeFragmentShaders();        
createCustomMappings();

if (!fColorShaderInstance)
{
    // We only need to add texture for color shader instance used by Texture enabled viewport.
    fColorShaderInstance = shaderMgr-&gt;getStockShader( MHWRender::MShaderManager::k3dBlinnShader );  
                // Make our shader fragment as input for diffuseColor of stock shader       
    fColorShaderInstance-&gt;addInputFragment(fFragmentName, MString(&quot;output&quot;), MString(&quot;diffuseColor&quot;));
}</pre>

<p>Now, we have successfully added our shader fragment as input and we need to update it properly. Maya will call MPxShaderOverride::updateDevice during DG evaluation. So we need to create an update function and call it during the color shader mode.</p>

<pre>if (fColorShaderInstance)
{
    // Update shader to mark it as drawing with transparency or not.
    fColorShaderInstance-&gt;setIsTransparent( isTransparent() );              
    fColorShaderInstance-&gt;setParameter(&quot;specularColor&quot;, &amp;fSpecular[0] );

    // Update texture shader
    updateTextureShaderFragment();
}</pre>

<p>updateTextureShaderFragment is almost the same as the fileTexture sample, except that we are going to generate a default texture when the file texture is not available.</p>

<pre>...
if (textureManager)
{
    TRACE_API_CALLS(&quot;Accquire file texture&quot;);
    TRACE_API_CALLS(fFileTexturePath);
    MHWRender::MTexture* texture =
        textureManager-&gt;acquireTexture(fFileTexturePath, name);
    if (texture)
    {                           
        assignTexture(texture, textureManager);

        // release our reference now that it is set on the shader
        textureManager-&gt;releaseTexture(texture);
    }
    else
    {

        MImage image;                           
        for (int i = 0; i &lt; 4; ++i)
        {
            fDiffuseByte[i] = fDiffuse[i] * 255;
        }
        image.setPixels(fDiffuseByte, 1, 1);

        if (!fDefaultTexture)
        {
            const MString vp2BlinnShaderDummyTextureName = MString(&quot;&quot;);
            MHWRender::MTextureDescription desc;
            desc.setToDefault2DTexture();
            desc.fHeight = 1;
            desc.fWidth = 1;
            desc.fFormat = MHWRender::kR8G8B8A8_UNORM;
            fDefaultTexture =
                textureManager-&gt;acquireTexture(vp2BlinnShaderDummyTextureName, desc, fDiffuseByte, false);
        }
        else
        {
            MStatus status = fDefaultTexture-&gt;update(image, false);
            cerr &lt;&lt; status &lt;&lt; endl;
        }
        if (fDefaultTexture)
        {
            cerr &lt;&lt; fDiffuse[0] &lt;&lt; &quot; &quot; &lt;&lt; fDiffuse[1] &lt;&lt; &quot; &quot; &lt;&lt; fDiffuse[2] &lt;&lt; &quot; &quot; &lt;&lt; fDiffuse[3] &lt;&lt; endl;
            // It is cached by OGS, update to make sure texture has been updated.                               
            TRACE_API_CALLS(&quot;Accquired default texture&quot;);
            assignTexture(fDefaultTexture, textureManager);
        }                           
    }...



// Update texture
void assignTexture(MHWRender::MTexture* texture, MHWRender::MTextureManager *textureManager)
{
    MHWRender::MTextureAssignment textureAssignment;
    textureAssignment.texture = texture;

    TRACE_API_CALLS(&quot;Update texture parameter&quot;);
    fFragmentTextureShader-&gt;setParameter(fResolvedMapName, textureAssignment);          
}</pre>

<p>Most of the work has been done now and it will run in Maya. However, the texture is will likely not display properly, so there is one more step.</p>

<p>Remember the uvCoord semantic we defined in our shader fragment earlier? Although we have defined the semantic, we still need to ask Maya to provide these system parameters before it can be used. We can call MPxShaderOverride::addGeometryRequirement or MPxShaderOverride::addGeometryRequirements to tell Maya to provide these parameters. We should call them during the initialization.</p>

<pre>MHWRender::MVertexBufferDescriptor positionDesc(
    empty,
    MHWRender::MGeometry::kPosition,
    MHWRender::MGeometry::kFloat,
    3);

MHWRender::MVertexBufferDescriptor normalDesc(
    empty,
    MHWRender::MGeometry::kNormal,
    MHWRender::MGeometry::kFloat,
    3);
MHWRender::MVertexBufferDescriptor textureDesc(
    empty,
    MHWRender::MGeometry::kTexture,
    MHWRender::MGeometry::kFloat,
    2);
addGeometryRequirement(positionDesc);
addGeometryRequirement(normalDesc);
addGeometryRequirement(textureDesc);</pre>

<p>The vp2FileTextureFragmentShader is fully working now. Enjoy! :)</p>

<p>The source code is available on the github of ADN, please visit (<a href="https://github.com/ADN-DevTech/MAYA-Vp2ShaderFragment">source code link</a>) to get it.</p>

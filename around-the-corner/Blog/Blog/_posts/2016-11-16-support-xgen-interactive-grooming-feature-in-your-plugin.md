---
layout: "post"
title: "Support XGen Interactive Grooming feature in your plugin"
date: "2016-11-16 21:04:00"
author: "Zhong Wu"
categories:
  - "C++"
  - "Maya"
  - "Plug-in"
  - "Rendering"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/support-xgen-interactive-grooming-feature-in-your-plugin.html "
typepad_basename: "support-xgen-interactive-grooming-feature-in-your-plugin"
typepad_status: "Publish"
---

<p>Starting from Maya 2017, the Maya team introduced a great improvement to the XGen feature, called “Interactive hair and fur grooming with XGen”. The improvement provides a full range of tools, including sculpting brushes, modifiers, and sculpting layers, dedicated to creating all styles of hair and fur.</p>  <p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b8d23cc8ef970c-pi"><img title="interactive-hair-grooming-xgen-large-1152x648" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; border-top-width: 0px; margin-right: auto" border="0" alt="interactive-hair-grooming-xgen-large-1152x648" src="/assets/image_c97d8a.jpg" width="477" height="270" /></a></p>  <p>Interactive grooming descriptions and modifiers are Maya-based nodes, and therefore they can be manipulated in the Node Editor. The cool thing is that these nodes are computed on your system's GPU instead of CPU, so your brush stokes can appear in real-time, providing you an interactive workflow that does not require preview generation. All of the interactive groom hair data is saved to Maya scene files without using any additional Ptex or XPD sidecar files. Also, you can save your grooms to Alembic-based cache files.</p>  <p>One thing you need to know is XGen Geometry Instancer descriptions (including default spline and groomable spline descriptions) are not compatible with interactive grooming tools or modifiers. But, Maya provides you a way to convert default spline descriptions to interactive grooming hairs. </p>  <p>Today, I am not going to talk in detail about this cool feature. If you are interested, please visit the <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=GUID-496603B0-F929-45CD-B607-1CFCD3283DBE">XGen Interactive Grooming Help </a>for more information. </p>  <p>Rather, I want to talk about how to support the XGen Interactive Grooming feature in any third party plug-ins, especially in a renderer like Arnold. </p>  <p>Before we talk about the API, I’d like to mention some MEL commands. The XGen interactive Grooming feature has many useful commands. Our engineering team is currently working on updating the XGen Tech Docs with these new commands. These commands would be quite helpful for TD’s who want to automate their tasks, or customize the pipeline. Our XGen development team is also actively working on them, if you have any problems or requirements, please feel free to contact us.</p>  <p>xgmSplineQuery is an important one, it provides many flags to help you get different values or results. Let’s take a look at them one by one.</p>  <p>· <b>xgmSplineQuery </b></p>  <p>1. -listSplineDescriptions;</p>  <p>Lists all the descriptions, the result would be: description1 description2. </p>  <p>2. -listSplineDescriptions -long;</p>  <p>Lists the long name of all the descriptions, the result would be: |description1 |description2 </p>  <p>3. -isSplineDescription descriptionNode;</p>  <p>Determines if the node is a spline description.</p>  <p>4. -splineCount descriptionNode;</p>  <p>Gets the spline count of the specified description. </p>  <p>5. -videoMemoryDedicated;</p>  <p>Gets the total available GPU memory.</p>  <p>6. -videoMemoryUsed;</p>  <p>Gets the GPU memory used by all the descriptions in scene.</p>  <p>7. -videoMemoryAvailable;</p>  <p>Gets the total GPU memory used by the selected description(s).</p>  <p>· <b>xgmSplineSelect </b></p>  <p>1. – convertToFreeze </p>  <p>Freezes hairs in the selected description. </p>  <p>2. – replaceBySelectedFaces</p>  <p>Replaces currently bound faces with the selected faces.</p>  <p>· <b>xgmSplineApplyRenderOverride descriptionNode</b></p>  <p>Lets you override the current hair density being rendered in Viewport with a different density at render time.</p>  <p>· <b>xgmExportSplineDataInternal </b></p>  <p>Lets you export all the description data, which is stored in the Out Render Data attribute of the xgmSplineDescription node, to a specified file. Use the -output flag as follows: </p>  <p><b>xgmExportSplineDataInternal -output &quot;D:/XGenAPI/test.txt&quot; description1_Shape.outRenderData;</b></p>  <p>Now let’s take a look at the XGen API. You can find the SDK in your Maya installation folder. For example, in Maya 2017 this would be: <a href="file:///\\Maya2017\plug-ins\xgen">\\Maya2017\plug-ins\xgen</a>.</p>  <p>Here, you can see the header files, lib directories and files, but the most important one for third-party developers is the devkit folder. The devkit contains a couple of samples. Among them, the xgenSplineArnoldExtension, this is the one that I am going to talk about today. It demonstrates how to support the XGen interactive grooming new feature within Arnold renderer. </p>  <p>The new XGen interactive grooming feature also uses splines to represent each hair or fur, and the data for each spline is stored in the Out Render Data attribute of xgmSplineDescription node. You can use the following MEL command output the result. </p>  <p><i>xgmExportSplineDataInternal -output &quot;D:/XGenAPI/test.txt&quot; description1_Shape.outRenderData</i><i></i></p>  <p>How do you use the API to access the data of XGen interactive grooming feature? In general, there are 3 steps.</p>  <p><b>Step 1</b></p>  <p>You need to get the spline data. The XgFnSpline is the class to help you operate on all splines data. It’s located in the header file XgSplineApi.h. Class XgFnSpline can loads the data from xgmSplineDescription.outRenderData plug in Maya. Translators use Maya's MPxData::writeBinary() method to serialize the plug data into a BLOB. XgFnSpline can interpret the data without Maya. </p>  <p>Let’s take a look at the sample code to load the data for XgFnSpline:</p>  <pre class="brush:cpp;toolbar: false;">MFnDagNode fnDagNode(m_dagPath);
// Stream out the spline data
std::string data;
MPlug       outPlug = fnDagNode.findPlug(&quot;outRenderData&quot;);
MObject     outObj  = outPlug.asMObject();
MPxData*    outData = MFnPluginData(outObj).data();
if (outData)
{
    std::ostringstream opaqueStrm;
    outData-&gt;writeBinary(opaqueStrm);
    data = opaqueStrm.str();
}
// Load the sample for i-th motion step
_splines.load(opaqueStrm, sampleSize, sampleTime)</pre>

<p><b>Step 2</b></p>

<p>After the data is loaded into XgFnSpline object, you need to allocate memory for the data. You need to use XgItSpline to iterate with the XgFnSpline object _splines, which we got in Step 1, and then calculate the number of points, curves and samples. We can then allocate the memory for these buffers depending on your needs. </p>

<p>Here is the sample code to support Arnold renderer. There are some special requirements as mentioned inline. You can also handle that depending on your specific needs. </p>

<pre class="brush:cpp;toolbar: false;">// Count the number of curves and the number of points
// Arnold's b-spline requires two phantom points.
unsigned int curveCount        = 0;
unsigned int pointCount        = 0;
unsigned int pointInterpoCount = 0;
for (XgItSpline splineIt = _splines.iterator(); !splineIt.isDone(); splineIt.next())
{
    curveCount        += splineIt.primitiveCount();
    pointCount        += splineIt.vertexCount();
    pointInterpoCount += splineIt.vertexCount() + splineIt.primitiveCount() * 2;
}
// Get the number of motion samples
const unsigned int steps = _splines.sampleCount();
// Allocate buffers for the curves
_numPoints  = AiArrayAllocate(curveCount, 1, AI_TYPE_UINT);
_points     = AiArrayAllocate(pointInterpoCount, steps, AI_TYPE_POINT);
_radius     = AiArrayAllocate(pointCount, 1, AI_TYPE_FLOAT);
_uCoord     = AiArrayAllocate(curveCount, 1, AI_TYPE_FLOAT);
_vCoord     = AiArrayAllocate(curveCount, 1, AI_TYPE_FLOAT);
_wCoord     = AiArrayAllocate(pointInterpoCount, 1, AI_TYPE_FLOAT);

unsigned int*   numPoints   = reinterpret_cast<unsigned int*>(_numPoints-&gt;data);
SgVec3f*        points      = reinterpret_cast<sgvec3f   *>(_points-&gt;data);
float*          radius      = reinterpret_cast<float   *>(_radius-&gt;data);
float*          uCoord      = reinterpret_cast<float   *>(_uCoord-&gt;data);
float*          vCoord      = reinterpret_cast<float   *>(_vCoord-&gt;data);
float*          wCoord      = reinterpret_cast<float   *>(_wCoord-&gt;data);</pre>

<p><b>Step 3</b></p>

<p>Now, we come to the last step, and it’s also the most important one. Here, we will fill in these objects with the correct values we evaluate from XgFnSpline object. XGen provides an iterator called XgItSpline. It’s also located in XgSplineApi.h and provides the functionalities to help access the internal data of each spline. Some important methods are listed as follows, and should provide all the geometry information:</p>

<p>· unsigned int primitiveInfoStride() const; </p>

<blockquote>
  <p>Stride of the primitive info array, </p>

  <p>[0]: Offset</p>

  <p>[1]: Length </p>

  <p>Offset and length determines the varying attribute location of each vertex in the varying array.</p>
</blockquote>

<p>· unsigned int primitiveCount() const;</p>

<blockquote>
  <p>Return the number of primitives</p>
</blockquote>

<p>· unsigned int vertexCount() const;</p>

<blockquote>
  <p>Return the number of vertices</p>
</blockquote>

<p>· SgBox3f boundingBox(int motion = 0) const;</p>

<blockquote>
  <p>Return the bounding box</p>
</blockquote>

<p>· SgBox3f motionBoundingBox() const;</p>

<blockquote>
  <p>Return the bounding box of motion vectors</p>
</blockquote>

<p>· const unsigned int* primitiveInfos() const; </p>

<blockquote>
  <p>Primitive info. See primitiveInfoStride()</p>
</blockquote>

<p><b>Varying (per-vertex) Attributes</b></p>

<p>· const SgVec3f* positions(int motion = 0) const; </p>

<blockquote>
  <p>Vertex positions</p>
</blockquote>

<p>· const SgVec2f* texcoords( int motion = 0) const;</p>

<blockquote>
  <p>Texcoords from root to tip, U is 0.0, V ranges from 0.0 (root vertex) to 1.0 (tip vertex)</p>
</blockquote>

<p>· const SgVec2f* patchUVs( int motion = 0) const;</p>

<blockquote>
  <p>Texcoords of the root vertex on the patch</p>
</blockquote>

<p>· const float* width( int motion = 0) const;</p>

<blockquote>
  <p>Get width</p>
</blockquote>

<p>With the information above, you can fetch whatever you need and fill into your data based on your requirement. Let’s take a look at the Arnold sample:</p>

<pre class="brush:cpp;toolbar: false;">// Fill the array buffers for motion step 0
for (XgItSpline splineIt = _splines.iterator(); !splineIt.isDone(); splineIt.next())
{
    const unsigned int  stride         = splineIt.primitiveInfoStride();
    const unsigned int  primitiveCount = splineIt.primitiveCount();
    const unsigned int* primitiveInfos = splineIt.primitiveInfos();
    const SgVec3f*      positions      = splineIt.positions(0);
    const float*        width          = splineIt.width();
    const SgVec2f*      texcoords      = splineIt.texcoords();
    const SgVec2f*      patchUVs       = splineIt.patchUVs();

    for (unsigned int p = 0; p &lt; primitiveCount; p++)
    {
        const unsigned int offset = primitiveInfos[p * stride];
        const unsigned int length = primitiveInfos[p * stride + 1];

        // Number of points
        *numPoints++ = length + 2;

        // Texcoord using the patch UV from the root point
        *uCoord++ = patchUVs[offset][0];
        *vCoord++ = patchUVs[offset][1];

        // Add phantom points at the beginning
        *points++ = phantomFirst(&amp;positions[offset], length);
        *wCoord++ = phantomFirst(&amp;texcoords[offset], length)[1];

        // Copy varying data
        for (unsigned int i = 0; i &lt; length; i++)
        {
            *points++ = positions[offset + i];
            *radius++ = width[offset + i] * 0.5f;
            *wCoord++ = texcoords[offset + i][1];
        }

        // Add phantom points at the end
        *points++ = phantomLast(&amp;positions[offset], length);
        *wCoord++ = phantomLast(&amp;texcoords[offset], length)[1];

    } // for each primitive
} // for each primitive batch

// Fill the array buffers for motion step &gt; 0
for (unsigned int key = 1; key &lt; steps; key++)
{
    for (XgItSpline splineIt = _splines.iterator(); !splineIt.isDone(); splineIt.next())
    {
        const unsigned int  stride         = splineIt.primitiveInfoStride();
        const unsigned int  primitiveCount = splineIt.primitiveCount();
        const unsigned int* primitiveInfos = splineIt.primitiveInfos();
        const SgVec3f*      positions      = splineIt.positions(key);

        for (unsigned int p = 0; p &lt; primitiveCount; p++)
        {
            const unsigned int offset = primitiveInfos[p * stride];
            const unsigned int length = primitiveInfos[p * stride + 1];

            // Add phantom points at the beginning
            *points++ = phantomFirst(&amp;positions[offset], length);

            // Copy varying data
            for (unsigned int i = 0; i &lt; length; i++)
            {
                *points++ = positions[offset + i];
            }

            // Add phantom points at the end
            *points++ = phantomLast(&amp;positions[offset], length);

        } // for each primitive
    } // for each primitive batch
} // for each motion step

// Set the buffers to the curves node
AiNodeSetArray(_curves, &quot;num_points&quot;, _numPoints);
AiNodeSetArray(_curves, &quot;points&quot;, _points);
AiNodeSetArray(_curves, &quot;radius&quot;, _radius);
AiNodeSetArray(_curves, &quot;uparamcoord&quot;, _uCoord);
AiNodeSetArray(_curves, &quot;vparamcoord&quot;, _vCoord);
AiNodeSetArray(_curves, &quot;wparamcoord&quot;, _wCoord);</pre>

<p>That’s how Arnold supports the XGen interactive grooming feature. You can also refer the <a href="https://gist.github.com/JohnOnSoftware/9ca01686525a368b765f3da247215a35">gist</a> for the core code. For more details, please refer the sample at &quot;Maya 2017 installation folder\plug-ins\xgen\devkit\xgenSplineArnoldExtension\xgenSpline”. </p>

<p>If you have any questions or requests, please feel free to contact me. As I mentioned, our development team is still actively working on the feature, and we are happy to hear any feedback. </p>

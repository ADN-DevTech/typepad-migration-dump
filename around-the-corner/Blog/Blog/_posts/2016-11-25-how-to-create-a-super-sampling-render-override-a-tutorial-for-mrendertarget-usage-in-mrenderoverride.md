---
layout: "post"
title: "How to create a Super sampling render override - a tutorial for MRenderTarget usage in MRenderOverride"
date: "2016-11-25 03:38:51"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Plug-in"
  - "Samples"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/how-to-create-a-super-sampling-render-override-a-tutorial-for-mrendertarget-usage-in-mrenderoverride.html "
typepad_basename: "how-to-create-a-super-sampling-render-override-a-tutorial-for-mrendertarget-usage-in-mrenderoverride"
typepad_status: "Publish"
---

<p>Recently we were asked how to implement super sampling for a viewport render override. It requires you to create new render targets to replace the original ones.</p>
<p>The MRT sample is very good at showing how to do it, however it is too complex to begin with. So we&#39;ll create a new one based on the simplest sample - viewOverrideSimple. The full code can be found on GitHub <a href="https://github.com/iamsleepy/Viewport-override-supersampling/" rel="noopener noreferrer" target="_blank">here</a>.</p>
<p>The original sample has three render operations: scene, HUD and present. To render in a higher resolution, we need to modify scene operation and add quad operation for down sampling the render output from the scene operation. The HUD and present will be left untouched.</p>
<p>First, let&#39;s begin with the render override itself:</p>
<p>We need to extend the array size of viewOverrideSimple::mOperations and viewOverrideSimple::mOperationNames, it has four operations now. And we need to add new members for storing render targets after operation iteration:</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>// Temporary of operation iteration
int mCurrentOperation;

// Supersampling render targets
MString mTargetOverrideNames[2];
MHWRender::MRenderTargetDescription* mTargetDescriptions[2];
MHWRender::MRenderTarget* mTargets[2];
</code></pre>
<p>The render targets are used for scene operation to render with, and worked as the input parameters for our bicubic shader in the quad operation.</p>
<p>Next, we need to create our render target descriptions inside the constructor:</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>mOperations[0] = mOperations[1] = mOperations[2] = mOperations[3] = NULL;
mOperationNames[0] = &quot;viewOverrideSimple_Scene&quot;;
mOperationNames[1] = &quot;viewOverrideSimple_Quad&quot;;
mOperationNames[2] = &quot;viewOverrideSimple_HUD&quot;;
mOperationNames[3] = &quot;viewOverrideSimple_Present&quot;;

//MSAA sample count, we don&#39;t want MSAA here
unsigned int sampleCount =1; 

MHWRender::MRasterFormat colorFormat = MHWRender::kR32G32B32A32_FLOAT;

//Create color buffer first
mTargetOverrideNames[0] = MString(&quot;_viewRender_SSAA_color&quot;);
mTargetDescriptions[0]  = new MHWRender::MRenderTargetDescription(mTargetOverrideNames[0], 256, 256, sampleCount, colorFormat, 1, false);
mTargets[0] = NULL;

//We also need to create a depth buffer
mTargetOverrideNames[1] = MString(&quot;_viewRender_SSAA_depth&quot;);
mTargetDescriptions[1]  = new MHWRender::MRenderTargetDescription(mTargetOverrideNames[1], 256, 256, sampleCount, MHWRender::kD24S8, 1, false);
mTargets[1] = NULL;
</code></pre>
<p>Both color and depth target are required here. Missing depth buffer will cause the depth test to fail in the scene operation.</p>
<p>In the destructor, we need to release our render target description and render target like below:</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>for(int i = 0; i &lt; 2; ++i)
{
    if(mTargetDescriptions[i])
    {
       delete mTargetDescriptions[i];
       mTargetDescriptions[i] = NULL;
    }
    MHWRender::MRenderer* theRenderer = MHWRender::MRenderer::theRenderer();
    if (theRenderer)
    {
        auto targetManager = theRenderer-&gt;getRenderTargetManager();
        if(targetManager)
        {
            targetManager-&gt;releaseRenderTarget(mTargets[i]);
            mTargets[i] = NULL;
        }
    }
}
</code></pre>
<p>The next part is setup, and we need to create our render targets at that point and set them as the output target for our scene render operation. We&#39;ll also let scene render use its following quad operation.</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>MStatus viewOverrideSimple::setup( const MString &amp; destination )
{
    if (!mOperations[0])
    {
        MHWRender::MRenderer *theRenderer = MHWRender::MRenderer::theRenderer();
        if (!theRenderer)
            return MStatus::kFailure;

        auto renderTargetManager = theRenderer-&gt;getRenderTargetManager();

        unsigned int targetWidth = 256;
        unsigned int targetHeight = 256;
        // Get current render target&#39;s output size
        theRenderer-&gt;outputTargetSize( targetWidth, targetHeight );

        // Double size the render target.
        targetHeight *= 2;
        targetWidth *= 2;

        // Create or update our render targets
        for(int i = 0; i &lt; 2; ++i)
        {
            mTargetDescriptions[i]-&gt;setWidth(targetWidth );
            mTargetDescriptions[i]-&gt;setHeight(targetHeight );

            if(!mTargets[i])
            {
                mTargets[i] = renderTargetManager-&gt;acquireRenderTarget(*mTargetDescriptions[i]);
            }
            else
            {
                mTargets[i]-&gt;updateDescription(*mTargetDescriptions[i]);
            }
        }

        mOperations[0] = (MHWRender::MRenderOperation *) new simpleViewRenderSceneRender( mOperationNames[1] );
        mOperations[1] = (MHWRender::MRenderOperation *) new simpleViewRenderQuadRender( mOperationNames[2] );
        mOperations[2] = (MHWRender::MRenderOperation *) new simpleViewRenderHudRender();
        mOperations[3] = (MHWRender::MRenderOperation *) new simpleViewRenderPresentRender( mOperationNames[3] );
    }
    if (!mOperations[0] ||
        !mOperations[1] || 
        !mOperations[2] ||
        !mOperations[3])
    {
        return MStatus::kFailure;
    }
    else
    {
        //Set custom render targets
        ((simpleViewRenderSceneRender*)mOperations[0])-&gt;setRenderTargets(mTargets, 2);
        ((simpleViewRenderSceneRender*)mOperations[0])-&gt;setQuadRender((simpleViewRenderQuadRender*)mOperations[1]);
    }

    return MStatus::kSuccess;
}
</code></pre>
<p>At last, we need to update the cleanup operations:</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>MStatus viewOverrideSimple::cleanup()
{
    mCurrentOperation = -1;
    auto quadOp = (simpleViewRenderQuadRender *)mOperations[1];
    if (quadOp)
    {
        quadOp-&gt;setColorTarget(NULL);
        quadOp-&gt;setDepthTarget(NULL);
        quadOp-&gt;updateTargets();
    }

    auto *sceneOp = (simpleViewRenderSceneRender *)mOperations[0];
    if (sceneOp)
    {
        sceneOp-&gt;setRenderTargets( NULL, 0 );
    }

    return MStatus::kSuccess;
}
</code></pre>
<p>The work for viewOverrideSimple has been done now, we have updated it in following ways:</p>
<ol>
<li>Add a quad operation</li>
<li>Create two render targets and render target descriptions in constructor</li>
<li>Release render targets and render target descriptions in the destructor</li>
<li>Create render targets and setup scene render operations with our render targets and quad operations</li>
<li>Cleanup</li>
</ol>
<p>The next part of the work takes in the the MSceneRender, and we&#39;ll need to override two functions:</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>virtual MHWRender::MRenderTarget* const* targetOverrideList(unsigned int &amp;listSize);
virtual void postSceneRender(const MHWRender::MDrawContext &amp; context);
</code></pre>
<p>In the MSceneRender::targetOverrideList, we need to return the render targets created earlier to let Maya render in new settings.</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>//Override render targets
MHWRender::MRenderTarget* const*  simpleViewRenderSceneRender::targetOverrideList(unsigned int &amp;listSize)
{
    if(mTargets)
    {
         listSize = numTargets;
        return mTargets;
    }
    listSize = 0;
    return NULL;
}
</code></pre>
<p>And in the MSceneRender::postSceneRender, we&#39;ll update our quad render with latest result, like this:</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>void simpleViewRenderSceneRender::postSceneRender(const MHWRender::MDrawContext &amp; context)
{
    if(mSimpleQuadRender)
    {                
        mSimpleQuadRender-&gt;setColorTarget(mTargets[0]);
        mSimpleQuadRender-&gt;setDepthTarget(mTargets[1]);
        mSimpleQuadRender-&gt;updateTargets();
    }
}
</code></pre>
<p>At last, it comes to the quad render. It is also very simple, we need to create a bicubic shader in the constructor and use it for rendering.</p>
<pre style="border: 1px solid #eaeaea; background-color: #f8f8f8; line-height: 1;"><code>simpleViewRenderQuadRender::simpleViewRenderQuadRender(const MString &amp;name)
    : MQuadRender( name )
    , mShaderInstance(NULL)
    , mColorTargetChanged(false)
    , mDepthTargetChanged(false)
    , fSamplerState(NULL)

{
    mDepthTarget.target = NULL;
    mColorTarget.target = NULL;
    if (!mShaderInstance)
    {
        MHWRender::MRenderer* renderer = MHWRender::MRenderer::theRenderer();
        const MHWRender::MShaderManager* shaderMgr = renderer ? renderer-&gt;getShaderManager() : NULL;
        if (shaderMgr)
        {
            //Create our bicubic shader
            mShaderInstance = shaderMgr-&gt;getEffectsFileShader( &quot;mayaBlitColorDepthBicubic&quot;, &quot;&quot; );
        }        
    }
}
</code></pre>
<p>Maya will add the extension name for the mayaBlitColorDepthBicubic based on your viewport settings. I&#39;ve only created an .ogsfx shader file, that is working for OpenGL core profile. The shader is following the BiCubic document on the wikipedia, you can check it out <a href="https://en.wikipedia.org/wiki/Bicubic_interpolation" rel="noopener noreferrer" target="_blank">here </a>if you want to. To use this, please put mayaBlitColorDepthBicubic.ogsfx inside Maya/bin/OGSFX folder.</p>
<p>That&#39;s all of the essential part of this sample. The rest of the code are setters and updates for shader parameters, please check <a href="https://github.com/ADN-DevTech/Viewport-override-supersampling" rel="noopener noreferrer" target="_blank">the full sample code</a> for more details.</p>

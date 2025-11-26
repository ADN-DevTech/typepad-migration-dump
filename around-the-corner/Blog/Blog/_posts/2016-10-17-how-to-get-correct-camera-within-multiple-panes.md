---
layout: "post"
title: "How to get correct camera within multiple panes"
date: "2016-10-17 17:33:00"
author: "Zhong Wu"
categories:
  - "C++"
  - "MotionBuilder"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/10/how-to-get-correct-camera-within-multiple-panes.html "
typepad_basename: "how-to-get-correct-camera-within-multiple-panes"
typepad_status: "Publish"
---

<p>To get the current camera of the view pane, usually, we can use the API FBRenderer::CurrentCamera to get the camera. But when there are multiple view panes, the API does not work as you expected, it always return back the camera of the 1st view pane, this is a known issue of Motionbuilder. So how to get the correct camera for the specified view pane? <br>There are 2 different situations. <br>First, let’s take a look at this example, if you have a customized shader which is derived from FBShader, and you want to display some special effects only when current camera is a specified camera, in general, you will write some code in the virtual method FBShader::ShadeModel() similar as follow: </p> <pre class="brush: cpp;toolbar: false;">
void MyShaderExample::ShadeModel( FBRenderOptions* pRenderOptions, FBShaderModelInfo* pShaderModelInfo, FBRenderingPass pPass )
{
    ... ...
    // Get current camera
    FBRenderer*  lRenderer   = mSystem.Renderer;
    lModelCam    = (FBCamera*)(lRenderer-&gt;CurrentCamera());
 
    std::string cameraName(lModelCam-&gt;LongName.AsString());
    std::string prefix("MyCustomizedCamera");
 
   if( ! lModelCam-&gt;SystemCamera &amp;&amp;
 strncmp(cameraName.c_str(), prefix.c_str(), strlen(prefix.c_str())) == 0 )
    {
        //add some special effects here.
    }
}
</pre><br>It works when there is only one view pane, but as I mentioned below, this will not work as expected when there are multiple view panes, the solution is to use the method FBRenderOptions::GetRenderingCamera() instead, it will provide you the correct camera for each different view pane. <br>Let's look at another scenario, how about if you just want to get the camera for the specified view pane without FBRenderOptions parameter? Within Motionbuilder 2017, we provided some methods including FBRenderer::GetCameraInPane() to help you get the camera for the specified view pane. If you are interested, please refer to the <a href="http://docs.autodesk.com/MOBPRO/2017/ENU/MotionBuilder-Developer-Help/index.html#!/url=./cpp_ref/class_o_r_s_d_k2017_1_1_f_b_renderer.html">online SDK help</a> for more details. <pre></pre>

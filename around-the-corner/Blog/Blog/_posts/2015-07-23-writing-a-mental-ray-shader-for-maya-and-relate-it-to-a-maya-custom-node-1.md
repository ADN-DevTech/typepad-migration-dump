---
layout: "post"
title: "Writing a Mental Ray shader for Maya and relate it to a Maya custom node"
date: "2015-07-23 18:31:20"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2015/07/writing-a-mental-ray-shader-for-maya-and-relate-it-to-a-maya-custom-node-1.html "
typepad_basename: "writing-a-mental-ray-shader-for-maya-and-relate-it-to-a-maya-custom-node-1"
typepad_status: "Publish"
---

<p>Mental Ray is the renderer shipped with Maya by default. To make your custom node working with Mental Ray becomes a natural question for every Maya developer.</p>
<p>The Mental Ray renderer can use the Maya shader network, but because it is a different rendering system (provided by NVIDIA), it cannot use the shader node in Maya directly. For this reason, Autodesk has developed a shader bridge between Mental Ray and Maya.</p>
<p>You can develop a Mental Ray shader easily and make it available in the Maya shader network without any effort. But it can’t be used directly by Maya or other renderers. That means you can’t preview it in the viewport.</p>
<p>For an end user, if you want your custom shader to be both accessed by Maya and Mental Ray, you have to develop both versions. There is plenty of information about how to develop a plug-in for Maya, so, we will skip the Maya part and focus on the Mental Ray side.</p>
<p>Since Maya 2013, you can relate a Maya custom shaders to a Mental Ray custom shaders for Maya by creating the Mental Ray shader and Maya custom shaders with the same name. Therefore they can use the same shader network.</p>
<p>In this article, we’ll create a Mental Ray shader for the fileTexture sample from the Maya devkit.<br /> A Mental Ray shader has two parts: the ‘mi’ include file and the ‘dll’ binary file. During Mental Ray initialization, Mental Ray will check the include file first and then loads the corresponding dll file. The include file must be encoded with ASCII or it will fail to load. To make this tutorial simple, we will put the include file and the dll file in the default path, which is the shaders and shaders/include folder inside the Mental Ray directory structure.</p>
<p>First, let’s take a look at the mi include file</p>
<pre class="brush: cpp; toolbar: false;">declare shader
  struct {
    color&quot;outColor&quot;,
    scalar &quot;outAlpha&quot;
  } &quot;fileTexture&quot; (
     string &quot;fileName&quot;,
     vector &quot;uvCoord&quot;
  )
  version 1
  apply texture
end declare
</pre>
<p>declare shader is the beginning of a Mental Ray shader definition and end declare is the finish. They are always in pairs.</p>
<p>&quot;fileTexture&quot; is the name of the Mental Ray shader. In this case, we want to relate it to our Maya fileTexture custom node and they must be identical.</p>
<p>Before &quot;fileTexture&quot; is the output of the shader and they should be the same as the Maya version.</p>
<pre class="brush: cpp; toolbar: false;">struct {
  color&quot;outColor&quot;,
  scalar &quot;outAlpha&quot;
}
</pre>
<p>Following the &quot;fileTexture&quot; is the input of the shader</p>
<pre class="brush: cpp; toolbar: false;">&quot;fileTexture&quot; (
  string &quot;fileName&quot;,
  vector &quot;uvCoord&quot;
)
</pre>
<p>Notice that, the Maya version has only two child parameters for uvCoord. There is no corresponding type for uvCoord, so you can use the vector type instead in Mental Ray.</p>
<p>“version 1” tells Mental Ray the version of this shader and “apply texture” tells Mental Ray this is a texture shader. To simplify the sample, we removed $ifndef for preventing repeat loading, but you should add it later.</p>
<p>Next, we’ll start to write the Mental Ray shader. The Mental Ray shader is a C DLL file. I’ve already put the code inside an extern “C” block to simplify the example.</p>
<p>The naming principle for Mental Ray shader code is a combination of your shader name and function name with a ‘_’. In this case, our shader’s name is fileTexture, so the version function of our shader will be fileShader_version.</p>
<p>According to the Mental Ray manual, the calling order of our fileTexture shader, during rendering in a frame the first time will be:</p>
<ol>
<li>fileTexture _init with a null parameter argument</li>
<li>fileTexture _init with non-null parameter arguments</li>
<li>fileTexture itself with the same non-null parameter arguments</li>
<li>more calls to fileTexture with the same parameter arguments, and calls to other instances of fileTexture _init and fileTexture with different parameter arguments,</li>
<li>one fileTexture_exit with a non-null parameter arguments for each corresponding fileTexture_init</li>
<li>finally one fileTexture _exit with a null parameter arguments.</li>
</ol>
<p>Like the Maya fileTexture sample, our shader node only passes the color through. But we don’t want to open and close the file every time our shader is called. So we need to load the file during the init and clean it up during the exit. We’ll create a user data struct to store necessary data.</p>
<p>First is the version part:</p>
<pre class="brush: cpp; toolbar: false;">// Version
DLLEXPORT int fileTexture_version(void) {
  return(1);
}</pre>
<p>It is very simple: just return the version number of this shader.</p>
<p>Next, let’s define the input, output and user data.</p>
<pre class="brush: cpp; toolbar: false;">// Define fileTexture input
typedef struct fileTexture_Paras {
  miTagfileName;
  miVectoruvCoord;
} fileTexture_Paras ;
// Define fileTexture result
typedef struct fileTexture_Results {
  miColoroutColor;
  miScalaroutAlpha;
} fileTexture_Results;
// Define userdata
typedef struct fileTexture_Userdata {
  miImg_file ifp;
  miImg_image *image;
} fileTexture_Userdata;</pre>
<p>Pretty straight forward, huh? The only thing that needs to be explained here is the miTag. It works like the HANDLE of win32. You get the miTag and retrieve the data with corresponding function. We’ll see it later in the init code.</p>
<p>The following is the init and cleanup functions. First, init:</p>
<pre class="brush: cpp; toolbar: false;">DLLEXPORT void fileTexture_init (
  miState *state,
  struct fileTexture_Paras *paras, // Our parameter
  miBoolean *inst_req // need instance init?
) {
  if (!paras) { // first without paras
    *inst_req = miTRUE; // We have parameters for our instance, so we want instance init.
  } else { // para is not null, instance init.
    void **user;
    miTagimageTag = 0;
    fileTexture_Userdata *data = 0;
    mi_query(miQ_FUNC_USERPTR, state, 0, &amp;user);
    *user = mi_mem_allocate(sizeof(fileTexture_Userdata));
    if (*user != NULL) {
      data = (fileTexture_Userdata *)*user;
      memset(data, 0, sizeof(fileTexture_Userdata)); // Try to get image filename
      imageTag = *mi_eval_tag(&amp;paras-&gt;fileName);
      char *fileName = (char*)mi_db_access(imageTag); // Open and read image.
      if (!(mi_img_open(&amp;(data-&gt;ifp), fileName, miIMG_TYPE_RGBA)))
        return;
      if (!(data-&gt;image = mi_img_image_alloc(&amp;(data-&gt;ifp)))) {
        mi_img_close(&amp;(data-&gt;ifp));
        return;
      }
      if (!(mi_img_image_read(&amp;(data-&gt;ifp), data-&gt;image))) {
        mi_mem_release(data-&gt;image);
        mi_img_close(&amp;(data-&gt;ifp));
        return;
      }
    }
  }
}
</pre>
<p>Mental Ray will first init with no parameters, and then after the inst_req is set to TRUE, it will call the init with parameters. So we can open the file during the second call.</p>
<p>First, let’s create user data in this shader context.</p>
<pre class="brush: cpp; toolbar: false;">mi_query(miQ_FUNC_USERPTR, state, 0, &amp;user);
*user = mi_mem_allocate(sizeof(fileTexture_Userdata));
</pre>
<p>If everything goes correctly, we’ll then try to get the filename with the miTag:</p>
<pre class="brush: cpp; toolbar: false;">      imageTag = *mi_eval_tag(&amp;paras-&gt;fileName);
      char *fileName = (char*)mi_db_access(imageTag); // Open and read image.
</pre>
<p>The rest of the code is trying to load the image file. For more details, please reference to the Mental Ray manual.</p>
<p>The cleanup code:</p>
<pre class="brush: cpp; toolbar: false;">DLLEXPORT void fileTexture_exit(
  miState *state,
  structmyshader *paras // for instance
) {
  if (!paras) { // the final step
  } else { // shader instance exit.
    // Get user data and close the file
    void **user;
    mi_query(miQ_FUNC_USERPTR, state, 0, &amp;user);
    if (*user != NULL) {
      fileTexture_Userdata* data = (fileTexture_Userdata*)*user;
      if (data-&gt;image != NULL) {
        mi_mem_release(data-&gt;image);
        mi_img_close(&amp;(data-&gt;ifp));
      }
      mi_mem_release(*user);
    }
  }
}
</pre>
<p>Just check whether file is loaded properly and do the cleanup job.</p>
<p>Finally our fileTexture shader code.</p>
<pre class="brush: cpp; toolbar: false;">#defineMAYA_CONNECTED(p) \
	(mi_has_connections(state) &amp;&amp; \
	!mi_is_nonshader_tag(mi_get_ghost(state,p)))
 
DLLEXPORT miBoolean fileTexture (
	fileTexture_Results *result,
	miState   *state,
	fileTexture_Paras *paras
) {
	miImg_image *image = 0;
	void **user;
	fileTexture_Paras *data = 0;

	// Get coord to read from image
	miVectorcoord = (MAYA_CONNECTED(&amp;paras-&gt;uvCoord)) ?
		*mi_eval_vector(&amp;paras-&gt;uvCoord) :
		state-&gt;tex_list[0];

	// Get user data
	mi_query(miQ_FUNC_USERPTR, state, 0, &amp;user);
	if (*user != NULL) {
		fileTexture_Userdata* data = (fileTexture_Userdata*)*user;
		image = data-&gt;image;               
		// If image opened successful, read image
		if (image) {
			if (coord.x &lt; 0.0f || coord.x &gt; 1.0f)
				coord.x -= floorf(coord.x);
			if (coord.y &lt; 0.0f || coord.y &gt; 1.0f)
				coord.y -= floorf(coord.y);
			intwidth = mi_img_get_width(image);
			intheight = mi_img_get_height(image);

			mi_img_get_color(
				image, &amp;result-&gt;outColor,
				(int)(width*coord.x),
				(int)(height*coord.y));

			result-&gt;outAlpha = result-&gt;outColor.a;

			return miTRUE;
		}
	}

	// Default routine, output black
	result-&gt;outColor.a = result-&gt;outColor.r = result-&gt;outColor.g = result-&gt;outColor.b = 0.0f;
	result-&gt;outColor.a = result-&gt;outAlpha = 1.0f;
	return miFALSE;
}
</pre>
<p>First get the uv coordinate. We use the macro to decide which one should be use.</p>
<pre class="brush: cpp; toolbar: false;">miVectorcoord = (MAYA_CONNECTED(&amp;paras-&gt;uvCoord)) ?
		*mi_eval_vector(&amp;paras-&gt;uvCoord) :
		state-&gt;tex_list[0];
</pre>
<p>Then get the image file from the user data</p>
<pre class="brush: cpp; toolbar: false;">            mi_query(miQ_FUNC_USERPTR, state, 0, &amp;user);
            if(*user != NULL) {
                fileTexture_Userdata* data = (fileTexture_Userdata*)*user;
                image = data-&gt;image;
</pre>
<p>The rest of the code is pretty much as the same as the fileTexture::compute, you will read the color from image and output.</p>
<p>Compile it and put the mi and dll file to the Mental Ray folder, and your fileTexture can be rendered in both the Viewport and Mental Ray now.</p>
<p>You can get the source code from my <a href="https://github.com/iamsleepy/fileTextureMentalRay" target="_self">github</a>.</p>
<p>If your plug-in is loaded after your Mental Ray shader, you’ll need to create a mel script for loading it properly. You can find it in the “Relate Maya custom shaders to Mental Ray for Maya custom shaders” on our website.</p>
<p><span style="text-decoration: underline;"><strong>References</strong></span>:</p>
<p>Relate Maya custom shaders to Mental Ray for Maya custom shaders:<br /><a href="http://help.autodesk.com/view/MAYAUL/2015/ENU/?guid=GUID-0966E345-CA9E-4C6D-B17A-0D0DA186A2A0" target="_self">http://help.autodesk.com/view/MAYAUL/2015/ENU/?guid=GUID-0966E345-CA9E-4C6D-B17A-0D0DA186A2A0</a></p>
<p>Mental Ray standalone manual:<br /><a href="http://docs.autodesk.com/MENTALRAY/2015/ENU/mental-ray-help/" target="_self">http://docs.autodesk.com/MENTALRAY/2015/ENU/mental-ray-help/</a></p>

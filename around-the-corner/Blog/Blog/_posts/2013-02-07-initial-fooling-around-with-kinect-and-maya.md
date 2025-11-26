---
layout: "post"
title: "Initial fooling around with Kinect and Maya"
date: "2013-02-07 10:01:43"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Kinect"
  - "Maya"
  - "Plug-in"
original_url: "https://around-the-corner.typepad.com/adn/2013/02/initial-fooling-around-with-kinect-and-maya.html "
typepad_basename: "initial-fooling-around-with-kinect-and-maya"
typepad_status: "Publish"
---

<p>In a previous post, we talked about the Maya device API and mentioned I may try and write a device plug-in for the Kinect. There is few things I want to try with the Kinect:</p>
<ol>
<li>Some simple model capture using the Kinect’s RGB-D camera</li>
<li>Generate a point cloud&#0160;</li>
<li>Simple gesture-based control</li>
<li>And finally using the latest Kinect Fusion SDK create object meshes</li>
</ol>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c36aba71a970b-pi" style="display: inline;"><img alt="Kinect" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c36aba71a970b" src="/assets/image_d1e5e8.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Kinect" /></a>The first seemed a fun approach to test out the Kinect interface, although ultimately the currently-available precision (the resolution has apparently been limited to 640 x 480 to keep the processing snappy) would probably not prove usable for any real-world model capture.</p>
<p>So, for this first try we will do a live feed from the Kinect sensor to display dynamically inside Maya. In order to do that we will use the <a href="http://around-the-corner.typepad.com/adn/2012/12/maya-device-api.html" target="_self">Maya Device</a> API, and display the image on a imagePlane. Because, I do not want to have to go via a temporary file, I&#39;ll make-up my custom image plane to read the image data directly from memory.&#0160;</p>
<pre class="brush:cpp; toolbar:false;">class KinectImagePlane : public MPxImagePlane {
public:
	static MTypeId id ;
	static MObject aColorImageData ;

public:
	KinectImagePlane () ;
	virtual ~KinectImagePlane () ;

	virtual MStatus loadImageMap (const MString &amp;fileName, int frame, MImage &amp;image) ;
	virtual bool getInternalValueInContext (const MPlug &amp;, MDataHandle &amp;, MDGContext &amp;) ;
	virtual bool setInternalValueInContext (const MPlug &amp;, const MDataHandle &amp;, MDGContext &amp;) ;

	static void *creator () ;
	static MStatus initialize () ;

} ;
</pre>
<p>The aColorImageData attribute will actually be a memory address pointing directly to the Kinect image buffer, and we will feed the MImage with the data directly.</p>
<pre class="brush:cpp; toolbar:false;">//-----------------------------------------------------------------------------
MTypeId KinectImagePlane::id (0x00081052) ;
MObject KinectImagePlane::aColorImageData ;

//-----------------------------------------------------------------------------
KinectImagePlane::KinectImagePlane () : MPxImagePlane () {
}

KinectImagePlane::~KinectImagePlane () {
}

//-----------------------------------------------------------------------------
MStatus KinectImagePlane::loadImageMap (const MString &amp;fileName, int frame, MImage &amp;image) {
	MPlug colorImageData (thisMObject (), aColorImageData) ;
	int addr =colorImageData.asInt () ;

	if ( addr == 0 ) {
		image.readFromFile (&quot;c:/Users/cyrille/Pictures/default.jpg&quot;) ;
		return (MStatus::kSuccess) ;
	}
	IFTImage *pColorImage =(IFTImage *)addr ;
	unsigned int width =pColorImage-&gt;GetWidth (), height =pColorImage-&gt;GetHeight () ;
	unsigned int size =width * height ;
	unsigned int channels =pColorImage-&gt;GetBytesPerPixel () ;

	image.create (width, height, channels) ;
	//image.setPixels (m_pColorImage-&gt;GetBuffer (), m_pColorImage-&gt;GetWidth (), m_pColorImage-&gt;GetHeight ()) ;
	unsigned char *pixels =image.pixels () ;
	BYTE *buffer =pColorImage-&gt;GetBuffer () ;
	for ( unsigned int i =0 ; i &lt; size ; i++, pixels +=channels, buffer +=channels ) {
		pixels [0] =buffer [0] ;
		pixels [1] =buffer [1] ;
		pixels [2] =buffer [2] ;
		pixels [3] =255 - buffer [3] ; // the alpha seems to go the other way around
	}
	image.verticalFlip () ; // flip the image as the Kinect give it to us downside

	return (MStatus::kSuccess) ;
}

//-----------------------------------------------------------------------------
bool KinectImagePlane::getInternalValueInContext (const MPlug &amp;plug, MDataHandle &amp;handle, MDGContext &amp;context) {
	return (MPxImagePlane::getInternalValueInContext (plug, handle, context)) ;
}

bool KinectImagePlane::setInternalValueInContext (const MPlug &amp;plug, const MDataHandle &amp;handle, MDGContext &amp;context) {
	if ( plug == aColorImageData || plug == frameExtension) {
		setImageDirty () ;  // Force the imagePlane to refresh
		//return (true) ;
	}
	return (MPxImagePlane::setInternalValueInContext (plug, handle, context)) ;
}

//-----------------------------------------------------------------------------
void *KinectImagePlane::creator() {
	return new KinectImagePlane;
}

MStatus KinectImagePlane::initialize() {
	MFnNumericAttribute nAttr ;

	aColorImageData =nAttr.create (&quot;colorImageData&quot;, &quot;cid&quot;, MFnNumericData::kLong, 0) ;
	//nAttr.setStorable (false) ;
	//nAttr.setHidden (false) ;
	//nAttr.setReadable (true) ;
	//nAttr.setWritable (false) ;
	addAttribute (aColorImageData) ;
	
	return (MS::kSuccess);
}</pre>
<p>The IFTImage type (Kinect image buffer) is very similar to the Maya MImage format, and while the MImage::setPixels (IFTImage::GetBuffer ()) would in theory has worked straight out of the box, the alpha value seems to go the other way. I haven;t really yet explored that area, but changing the value fixed it.</p>
<p>And the register/deregister calls:</p>
<pre class="brush:cpp; toolbar:false;">plugin.registerNode (&quot;KinectImagePlane&quot;, KinectImagePlane::id, KinectImagePlane::creator, KinectImagePlane::initialize, MPxNode::kImagePlaneNode) ;
plugin.deregisterNode (KinectDeviceNode::id) ;
</pre>
<p>Now that becomes interesting - while we now have a way to display a Kinect memory image without reading a file from disk. WE need to connect to the Kinect sensor and using the Maya Device thread node &#39;MPxThreadedDeviceNode&#39;.</p>
<pre class="brush:cpp; toolbar:false;">class KinectDeviceNode : public MPxThreadedDeviceNode {
public:
	static MTypeId id ;
	static MObject aColorImageData ;
	static MObject aDepthImageData ;
	static MObject aSeed ;

	KinectSensor m_KinectSensor ;
	IFTImage *m_pColorImage ;
	IFTImage *m_pDepthImage ;
	NUI_IMAGE_TYPE m_imageType ;
	NUI_IMAGE_RESOLUTION m_imageRes ;
	NUI_IMAGE_TYPE m_depthType ;
	NUI_IMAGE_RESOLUTION m_depthRes ;
	bool m_bNearMode ;
	bool m_bSeatedSkeletonMode ;
public:
	KinectDeviceNode () ;
	virtual ~KinectDeviceNode () ;
	virtual void postConstructor () ;
	virtual MStatus compute (const MPlug &amp;plug, MDataBlock &amp;data) ;
	virtual void threadHandler () ;
	virtual void threadShutdownHandler () ;
	static void *creator () ;
	static MStatus initialize () ;
} ;
</pre>
<p>Here you need to remember that unlike MotionBuilder, <a href="http://around-the-corner.typepad.com/adn/2012/09/maya-api-thread-safe.html" target="_self">Maya is not multithreaded</a>, nor thread safe for most of its API. And that means the MPxThreadDeviceNode while being a separate thread, will not interact with Maya main thread if Maya is busy doing something else. In our example, you&#39;ll notice that our imagePlane framerate will freeze anytime you start doing something in Maya and this is the expected behavior.</p>
<p>The register/deregister calls first. Note the MPxNode::kThreadedDeviceNode type here:</p>
<pre class="brush:cpp; toolbar:false;">plugin.registerNode (&quot;KinectDeviceNode&quot;, KinectDeviceNode::id, KinectDeviceNode::creator, KinectDeviceNode::initialize, MPxNode::kThreadedDeviceNode);
plugin.deregisterNode (KinectDeviceNode::id) ;
</pre>
<p>Constructor/Destructor</p>
<pre class="brush:cpp; toolbar:false;">//-----------------------------------------------------------------------------
MTypeId KinectDeviceNode::id (0x00081051) ;
MObject KinectDeviceNode::aColorImageData ;
MObject KinectDeviceNode::aDepthImageData ;
MObject KinectDeviceNode::aSeed ;

static const LPCTSTR RES_MAP[] ={ &quot;80x60&quot;, &quot;320x240&quot;, &quot;640x480&quot;, &quot;1280x960&quot; } ;
static const LPCTSTR IMG_MAP[] ={ &quot;PLAYERID&quot;, &quot;RGB&quot;, &quot;YUV&quot;, &quot;YUV_RAW&quot;, &quot;DEPTH&quot; } ;

//-----------------------------------------------------------------------------
KinectDeviceNode::KinectDeviceNode () : MPxThreadedDeviceNode (),
	m_imageType(NUI_IMAGE_TYPE_COLOR),
	m_imageRes(NUI_IMAGE_RESOLUTION_640x480),
	m_depthType(NUI_IMAGE_TYPE_DEPTH_AND_PLAYER_INDEX),
	m_depthRes(NUI_IMAGE_RESOLUTION_320x240),
	m_bNearMode(true),
	m_bSeatedSkeletonMode(false),
	m_pColorImage(NULL),
	m_pDepthImage(NULL)
{
	// Prints mode params
	MString szTitleComplete ;
	szTitleComplete.format (&quot;Kinect -- Depth:^1s:^2s Color:^3s:^4s NearMode:^5s, SeatedSkeleton:^6s&quot;,
		IMG_MAP [m_depthType],
		(m_depthRes &lt; 0) ? &quot;Error&quot; : RES_MAP [m_depthRes],
		IMG_MAP [m_imageType],
		(m_imageRes &lt; 0) ? &quot;Error&quot; : RES_MAP [m_imageRes],
		m_bNearMode ? &quot;On&quot; : &quot;Off&quot;,
		m_bSeatedSkeletonMode ? &quot;On&quot; : &quot;Off&quot;
	) ;
	cout &lt;&lt; szTitleComplete &lt;&lt; endl ;
}

KinectDeviceNode::~KinectDeviceNode () {
	destroyMemoryPools () ;
	if ( m_pColorImage != NULL ) {
		m_pColorImage-&gt;Release () ;
		m_pColorImage =NULL ;
	}
	if ( m_pDepthImage != NULL ) {
		m_pDepthImage-&gt;Release () ;
		m_pDepthImage =NULL ;
	}
}

//-----------------------------------------------------------------------------
void KinectDeviceNode::postConstructor () {
	MObjectArray attrArray ;
	//attrArray.append (KinectDeviceNode::aColorImageData) ;
	//attrArray.append (KinectDeviceNode::aDepthImageData) ;
	attrArray.append (KinectDeviceNode::aSeed) ;
	setRefreshOutputAttributes (attrArray) ;

	// We&#39;ll be reading one set of 2 IFTImage pointer&#39;s and a seed number at a time
	createMemoryPools (12, 3, sizeof (LONG_PTR)) ;
}
</pre>
<p>While setRefreshOutputAttributes() takes an array, the array size must be 1 :( this is the current implementation limitation, but there is an easy workaround for that. It is also important in postConstructor() to create the memoryPools for the data to go from the device thread into the Maya DG evaluation and in our device node compute() method. Here we do create a pool of 12, but really, as we are using only one common buffer for the images, it is only important for the &#39;seed&#39; attribute which I&#39;ll come back on later.</p>
<pre class="brush:cpp; toolbar:false;">MStatus KinectDeviceNode::initialize () {
	MFnNumericAttribute nAttr ;

	//aColorImageData =nAttr.createAddr (&quot;colorImageData&quot;, &quot;cid&quot;) ;
	aColorImageData =nAttr.create (&quot;colorImageData&quot;, &quot;cid&quot;, MFnNumericData::kLong, 0) ;
	//nAttr.setStorable (false) ;
	//nAttr.setHidden (false) ;
	//nAttr.setReadable (true) ;
	//nAttr.setWritable (false) ;
	addAttribute (aColorImageData) ;
	attributeAffects (live, aColorImageData) ;
	attributeAffects (frameRate, aColorImageData) ;

	//aDepthImageData =nAttr.createAddr (&quot;depthImageData&quot;, &quot;did&quot;) ;
	aDepthImageData =nAttr.create (&quot;depthImageData&quot;, &quot;did&quot;, MFnNumericData::kLong, 0) ;
	//nAttr.setStorable (false) ;
	//nAttr.setHidden (false) ;
	//nAttr.setReadable (true) ;
	//nAttr.setWritable (false) ;
	addAttribute (aDepthImageData) ;
	attributeAffects (live, aDepthImageData) ;
	attributeAffects (frameRate, aDepthImageData) ;

	aSeed =nAttr.create (&quot;seed&quot;, &quot;sd&quot;, MFnNumericData::kDouble, 0.0) ;
	//nAttr.setHidden (true) ;
	addAttribute (aSeed) ;
	attributeAffects (live, aSeed) ;
	attributeAffects (frameRate, aSeed) ;

	attributeAffects (aSeed, aColorImageData) ; // Workaround to setRefreshOutputAttributes() limitation
	attributeAffects (aSeed, aDepthImageData) ;

	return (MS::kSuccess) ;
}
</pre>
<p>The Kinect sensor delivers 2 images, the RGBa image (color image) and the Depth image. For this post our imagePlane only use the color image, but our device node is exposing both for future uses.</p>
<p>And the magic happens here:</p>
<pre class="brush:cpp; toolbar:false;">void KinectDeviceNode::threadHandler () {
	FT_CAMERA_CONFIG videoConfig ;
	FT_CAMERA_CONFIG depthConfig ;
	FT_CAMERA_CONFIG *pDepthConfig =NULL ;

	cout &lt;&lt; &quot;KinectSensor Initialized&quot; &lt;&lt; endl ;
	HRESULT hr =m_KinectSensor.Init (m_depthType, m_depthRes, m_bNearMode, true/*m_bFallbackToDefault*/, m_imageType, m_imageRes, m_bSeatedSkeletonMode) ;
	m_KinectSensor.GetVideoConfiguration (&amp;videoConfig) ;
	m_KinectSensor.GetDepthConfiguration (&amp;depthConfig) ;
	pDepthConfig =&amp;depthConfig ;

	if ( m_pColorImage == NULL ) {
		m_pColorImage =FTCreateImage () ;
		m_pColorImage-&gt;Allocate (videoConfig.Width, videoConfig.Height, FTIMAGEFORMAT_UINT8_B8G8R8X8) ;
	}
	if ( m_pDepthImage == NULL ) {
		m_pDepthImage =FTCreateImage () ;
		m_pDepthImage-&gt;Allocate (depthConfig.Width, depthConfig.Height, FTIMAGEFORMAT_UINT16_D13P3) ;
	}
	setDone (false) ;
	while ( !isDone () ) {
		// Skip processing if we are not live
		if ( ! isLive () )
			continue ;

		MCharBuffer buffer ;
		MStatus status =acquireDataStorage (buffer) ;
		if ( !status )
			continue ;

		beginThreadLoop () ;
		{
			if ( m_KinectSensor.GetVideoBuffer () ) {
				HRESULT hrCopy =m_KinectSensor.GetVideoBuffer ()-&gt;CopyTo (m_pColorImage, NULL, 0, 0) ;
				if ( SUCCEEDED(hrCopy) &amp;&amp; m_KinectSensor.GetDepthBuffer () )
					hrCopy =m_KinectSensor.GetDepthBuffer ()-&gt;CopyTo (m_pDepthImage, NULL, 0, 0) ;
			}
			LONG_PTR *data =reinterpret_cast(buffer.ptr ()) ;
			data [0] =(LONG_PTR)m_pColorImage ;
			data [1] =(LONG_PTR)m_pDepthImage ;
			data [2] =(LONG_PTR)rand () ;
			pushThreadData (buffer) ;
		}
		endThreadLoop () ;
	}
	setDone (true) ;

	m_KinectSensor.Release () ;
	cout &lt;&lt; &quot;KinectSensor Released&quot; &lt;&lt; endl ;
}

void KinectDeviceNode::threadShutdownHandler () {
	// Stops the loop in the thread handler
	setDone (true) ;
}

MStatus KinectDeviceNode::compute (const MPlug &amp;plug, MDataBlock &amp;block) {
	if ( plug == aColorImageData || plug == aDepthImageData || plug == aSeed ) {
		// Access the data and update the output attribute
		MCharBuffer buffer ;
		if ( popThreadData (buffer) ) {
			// Relative data coming in
			LONG_PTR *data =reinterpret_cast(buffer.ptr ()) ;

			MDataHandle outputHandle =block.outputValue (aColorImageData) ;
			//outputHandle.asAddr () =reinterpret_cast(data [0]) ;
			outputHandle.asLong () =(int)data [0] ;
			outputHandle.setClean () ;

			outputHandle =block.outputValue (aDepthImageData) ;
			outputHandle.asLong () =(int)data [1] ;
			outputHandle.setClean () ;

			outputHandle =block.outputValue (aSeed) ;
			outputHandle.asDouble () =(double)data[2] ;
			outputHandle.setClean () ;

			block.setClean (plug) ;
			releaseDataStorage (buffer) ;
			return (MS::kSuccess) ;
		}
	}
	return (MS::kUnknownParameter) ;
}
</pre>
<p>What happens is that if the device is live, the threadHandler() will push data in the memory pool we created (up to 12 item). If we aren&#39;t consuming them, it will wait before pushing anymore, so while it is not relevant here to have a lot more pool, it might be important for motion capture for example if we do not want to miss any frame. In the example we do update within the same image buffers, but we do set the &#39;seed&#39; attribute with a random value to somehow guarantee in our example it is unique and will force the update.</p>
<p>Then, the device thread will mark the &#39;seed&#39; attribute dirty for that pool, and because our seed attribute affects both image buffer pointer attributes, Maya will mark them dirty too, and will call compute from the DG thread if someone needs the data.</p>
<p>In compute(), we do pop the pool and update the attribute with the image buffer pointers that if connected our imageplane will update the viewport display.</p>
<p>Now, start Maya, and run that little MEL script, and you&#39;re live on screen.</p>
<pre class="brush:cpp; toolbar:false;">file -f -new;
loadPlugin &quot;C:/Users/cyrille/Documents/Visual Studio 2010/Projects/MayaKinect/x64/Debug/MayaKinect.mll&quot;;
createNode &quot;KinectDeviceNode&quot;;
createNode &quot;KinectImagePlane&quot;;
connectAttr -f &quot;KinectImagePlane1.message&quot; &quot;perspShape.imagePlane[0]&quot;;
connectAttr -f &quot;KinectDeviceNode1.seed&quot; &quot;KinectImagePlane1.frameExtension&quot;;
connectAttr -f &quot;KinectDeviceNode1.colorImageData&quot; &quot;KinectImagePlane1.colorImageData&quot;;
setAttr &quot;KinectDeviceNode1.live&quot; 1;
</pre>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee84ef1ea970d-pi" style="display: inline;"><img alt="Kinect-color" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee84ef1ea970d image-full" src="/assets/image_55a66f.jpg" title="Kinect-color" /></a><br /><br /></p>

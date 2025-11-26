---
layout: "post"
title: "Using the Microsoft Kinect SDK to bring a basic point cloud into Maya"
date: "2013-02-20 01:34:02"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Custom Nodes"
  - "Cyrille Fauvel"
  - "Kinect"
  - "Maya"
  - "Plug-in"
original_url: "https://around-the-corner.typepad.com/adn/2013/02/using-the-microsoft-kinect-sdk-to-bring-a-basic-point-cloud-into-maya.html "
typepad_basename: "using-the-microsoft-kinect-sdk-to-bring-a-basic-point-cloud-into-maya"
typepad_status: "Publish"
---

<p>Today’s post presents a very basic implementation of a point cloud – essentially equivalent to the code in&#0160;<a href="http://around-the-corner.typepad.com/adn/2013/02/initial-fooling-around-with-kinect-and-maya.html" target="_blank">this previous post</a>&#0160;– which makes use of the Microsoft Kinect SDK to bring the Kinect color image on a Maya image plan live. While for an image it was pretty straight forward to take the Kinect color image and use the Maya API MImage class to read that image. This time we are interested to get the depth information out of the Kinect sensor.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d412c946d970c-pi" style="display: inline;"><img alt="Depthmap" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d412c946d970c" src="/assets/image_797215.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Depthmap" /></a><br />The Kinect SDK provides a function – NuiTransformDepthImageToSkeleton() – which does everything for you. It transforms the image X, Y coordinates from the Depth image into space coordinates X, Y, Z. Nothing terrible for us to implement excepted the documentation is wrong telling to left shift the depth parameter by 3 bits ( see the comment in the code below ).&#0160;</p>
<p>No the real issue for us is to get it displayed into Maya properly. There is few options and as an exercise, we will play with all during this Kinect journey:</p>
<ol>
<li>the first one is to create a Maya locator: it has the advantage to be easy to implement, but does not render, nor to be very fast as you will need to redraw the locator very frequently.</li>
<li>use the Maya particle system (dynamics or nDynamics): this one sounds promising, but after a certain number of particles, there might be some limits which will impact Maya performance again. But it has the advantage to render as well as being standard to Maya.</li>
<li>use a specialized plug-in for point cloud like the AliceLabs Maya point cloud plug-in.</li>
</ol>
<p>Here is the Point Cloud acquisition code</p>
<pre class="brush: cpp; toolbar: false;">MPointArray &amp;KinectLocator::getPointCloud () {
	MPlug depthImageData (thisMObject (), KinectLocator::aDepthImageData) ;
	MPlug sizeData (thisMObject (), KinectLocator::size) ;

	int depthAddr =depthImageData.asInt () ;
	float multiplier =sizeData.asFloat () ;
	if ( depthAddr == 0 )
		return (mVertices) ;
	mVertices.clear () ;

	IFTImage *pDepthImage =(IFTImage *)depthAddr ;
	unsigned int depthWidth =pDepthImage-&gt;GetWidth (), depthHeight =pDepthImage-&gt;GetHeight () ;
	// Loop through the depth information
	USHORT *depthData =(USHORT *)pDepthImage-&gt;GetBuffer () ;
	for ( int y =0 ; y &lt; depthHeight ; y++ ) {
		for ( int x =0 ; x &lt; depthWidth ; x++, depthData++ ) {
			//usDepthValue
			//  Type: USHORT
			//  [in] The depth value in millimeters of the depth image pixel, shifted left by three bits. 
			//  The left shift enables you to pass the value from the depth image directly into this function.
			//
			//  http://social.msdn.microsoft.com/Forums/br/kinectsdknuiapi/thread/a657e563-b240-46e2-827d-02712b14ebc1
			//  You should pass the 16-bit value exactly as it appears in the depth frame, *without* applying a shift.
			//  d will be a short integer representing the distance in millimeters; s_z will be a float representing the
			//  same distance in meters. So if d is 1500, then s_z should be 1.5f.
			Vector4 realPoints ;
			if ( depthWidth == 320 )
				realPoints =NuiTransformDepthImageToSkeleton (x, y, *depthData) ;
			else if ( depthWidth == 640 )
				realPoints =NuiTransformDepthImageToSkeleton (x, y, *depthData, NUI_IMAGE_RESOLUTION_640x480) ;
			MPoint pt (realPoints.x * multiplier, realPoints.y * multiplier, realPoints.z * multiplier) ;
			mVertices.append (pt) ;
		}
	}
	return (mVertices) ;
}
</pre>
<p>Once you get the point array, you can displays these point in Maya .Based on the LocatorLib code from the <a href="http://labs.autodesk.com/utilities/ADN_plugins/catalog/" target="_self">Plug-in of the Month</a> available on <a href="http://labs.autodesk.com/utilities/ADN_plugins" target="_self">Autodesk Labs</a>, you modify the MPxLocatorNode::draw (M3dView &amp;view, const MDagPath &amp;path, M3dView::DisplayStyle style, M3dView::DisplayStatus status) method (or&#0160;myWireFrameDraw () /&#0160;myShadedDraw () methods in that sample) like below and you are done.</p>
<pre class="brush: cpp; toolbar: false;">void KinectLocator::myWireFrameDraw () {
	MPointArray vertices =getPointCloud () ;
	glPointSize (2) ;
	glBegin (GL_POINTS) ;
	for ( int i =0 ; i &lt; vertices.length () ; i++ ) {
		glVertex3f (vertices [i].x, vertices [i].y, vertices [i].z) ;
	}
	glEnd () ;
}
</pre>
<p>Here is finally the results:</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d412c8bb9970c-pi" style="display: inline;"><img alt="Kinect_depth1" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d412c8bb9970c" src="/assets/image_577bfa.jpg" style="width: 450px; display: block; margin-left: auto; margin-right: auto;" title="Kinect_depth1" /></a></p>
<p>and from a different Camera angle:</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c36fd32f9970b-pi" style="display: inline;"><img alt="Kinect_depth2" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c36fd32f9970b" src="/assets/image_903417.jpg" style="width: 450px; display: block; margin-left: auto; margin-right: auto;" title="Kinect_depth2" /></a><br />The data is still coming live from the Kinect Sensor via the&#0160;KinectDeviceNode node implemented in the previous post</p>
<p>Now I need to start looking at interpreting gestures, to see what can be done to supplement AutoCAD’s existing user interface features. Fun fun fun! :-)</p>
<p>&#0160;</p>

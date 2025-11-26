---
layout: "post"
title: "Maya Stereoscopic Cameras"
date: "2012-11-08 04:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "CG"
  - "Cyrille Fauvel"
  - "Films"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2012/11/maya-stereoscopic.html "
typepad_basename: "maya-stereoscopic"
typepad_status: "Publish"
---

<p>This post provides an in-depth discussion and review of stereoscopic cameras. We will focus specifically on Autodesk® Maya® software representations and how technical artists and programmers can take advantage of the core architecture to bring stereoscopic to their next production. This document will be very technical in focus and provides code samples, scripts, and high level mathematics for various components of the S3D pipeline. For those who do not have a programming background, code samples will be offset in separate boxes or included in the appendix so that they can be referenced later or skipped entirely.<br />Finally, we will skip the high level overview of stereoscopic 3D (S3D). For those readers that do not have a background about S3D or its business case, please refer to Stereoscopic Filmmaking White Paper [1] that provides a comprehensive introduction to S3D and the main business drivers. Additionally Ray Zone provides a history of S3D cinema [2].</p>
<p>An introduction to Maya Camera Model was posted <a href="http://around-the-corner.typepad.com/adn/2012/11/cameras-in-maya.html" target="_self">here</a>.</p>
<h2>Stereoscopic Camera Model</h2>
<p>The stereoscopic camera model is based on ability of the human perceptual image to take two unique perspective views and use that information to construct a 3D representation of the real world. It is not enough to simply have two perspective views and call the problem solved. There are human perceptual rules you must obey to create pleasing images. This section will not cover these human perceptual rules and assumes the reader has the necessary background before continuing [3] [1]. However we will provide formulas for some more advanced setups that allow higher level control over the stereo camera rig such that artists can more easily construct pleasing S3D imagery. This section focuses on how the concepts that are based on human binocular vision are replicated in a S3D world.</p>
<p>At the base of our representation is a two camera model. This makes sense because humans have two eyes. Therefore, we should create pairs of cameras to represent a set of eyes. A common trick in the industry is to represent this as a single camera with additional parameters to control pseudo second eye. The downside to this approach is that you have to add additional code to each place where knowledge about the camera is used and add these parameters. It is a better approach to augment existing work and simply build high-level tools that work on top of a single perspective camera model.</p>
<p>There are three different ways that we can position these cameras to achieve a stereoscopic effect.</p>
<ul>
<li><strong>Parallel</strong> – two cameras side-by-side look at infinity.</li>
<li><strong>Off-axis</strong> – using the film offset parameters that we described above.</li>
<li><strong>Toe-in</strong> – two cameras rotated in-ward focusing at a point in space.</li>
</ul>
<p>In S3D, we want to simulate a zero parallax effect with divergence and convergence without having to rotate cameras and create the toe-in or issues of vertical parallax [5]. We can use the math above to construct converging and diverging stereo images with zero parallax setting. Like all work before us, we assume two key parameters for this effect:</p>
<ul>
<li><strong>Zero Parallax</strong> (Z<sub>p</sub>) – point of convergence for a left and right stereo pair.</li>
<li><strong>Interaxial Distance</strong> (I<sub>d</sub>) – the instance between two virtual cameras.</li>
</ul>
<p>We see these parameters as the core definition to a S3D of which higher level tools to manipulate S3D cameras are constructed and other tools in the pipeline.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c333148c5970b-pi" style="display: inline;"><img alt="F1" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c333148c5970b" src="/assets/image_bafa77.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="F1" /></a><strong><span style="text-decoration: underline;">Equation 1: Off-axis offset formula</span></strong></p>
<p>The above formula computes the film offset value for the left camera in a stereo camera pair. This is the main formula for <strong>off-axis</strong> S3D projection. This formula may vary slightly depending on how artists set up their camera rigs from studio to studio. For instance, some studios will choose to have a single camera be the master for all of their 2D images and augment that camera with a second camera. This has the benefit of encoding stereo as a second image in the 2D pipeline. This type of setup only removes the 0.5 multiplier in the above formula. Parallel rigs are equivalent to having a zero parallax setting at infinity and everything exists in theatre space [1].</p>
<p>Additionally, studios may elect to construct a toe-in based stereo image. A toe-in rig is rarely used as it will produce trapezoidal distortion and does not produce a true zero parallax plane [5].</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee4d51701970d-pi" style="display: inline;"><img alt="F2" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee4d51701970d" src="/assets/image_7a5c93.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="F2" /></a><strong><span style="text-decoration: underline;">Equation 2: toe-in camera rotation value</span></strong></p>
<p>Nonetheless, occasions arise where the need for a toe-in rig is necessary. The equation above computes the camera rotation for the left and right camera pairs. In this equation we do not compute the film offset – the value <em>O</em> of is 0. We are simply computing the rotation for each camera using right triangles. The computed rotation value is then applied to the left cameras as -R and to the right camera as R.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee4d51bbb970d-pi" style="display: inline;"><img alt="Toe-in" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee4d51bbb970d image-full" src="/assets/image_1f8b9b.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Toe-in" /></a><span style="text-decoration: underline;"><strong>Figure 4: Toe-in</strong></span></p>
<p>Parallel or toe-in rigs will have their uses in different types of shots. The most common situation for toe-in rigs is in scenes where vertical parallax does not occur or is not evident. Good examples would be sequences where a single object is at the center of the screen with a black background or green screen.</p>
<p>Since there is only one place to look in the scene and the object is centered, there is minimal issue with vertical parallax. Although the topic is still very much debated [2] [6], in general the use of toe-in rigs is best avoided, since besides the potential for artifacts in the final images that can cause viewer eye strain, toe-in rigs also complicate processing further down the pipeline. Parallel rigs are often used for background objects where there is no obvious object drawing focus.</p>
<h2>Maya Stereo (S3D) Camera Rig</h2>
<p>In Autodesk Maya 2009, the stereo camera calculates the horizontal shift. Users specify the interaxial and zero parallax values, and the camera shift is computed to produce the appropriate parallax. The components for visualizing stereo images are completely abstracted from the components that define the camera rig, and Maya provides an API override the default camera rig. This allows users to customize Maya’s standard rig to a form that will fit their production pipeline. Also, the Maya rig was built so that it could be augmented with higher level tools. For instance, it is more intuitive for artists to use tools that pre-establish the I<sub>d</sub>&#0160;based on known limitations of the human visual system to fuse images. The Bercovitz formula [7] specifies the appropriate interaxial given the maximum desired separation, near object, the far object, the focal length and the focus distance, i.e., Z<sub>p</sub>. The Bercovitz model can be added into Autodesk Maya using the API and foundation math provided.</p>
<p>This section will summarize the behavior and structure of the Maya stereo camera and explain how to rigs can be hooked into the Maya application. Subsequent sections will introduce new camera rig models and illustrate by example how to add they camera model into Maya.</p>
<h3>Stereo Rig Structure</h3>
<p>A stereo camera rig is defined implicitly by the data that exists on the node and its position in the DAG hierarchy. So all UI automatically detects a stereo camera assuming you define your data accordingly. The definition of a stereo camera in Maya has 2 parts:</p>
<ul>
<li>All stereo camera rigs have a root transform and a camera shape hanging directly off of this camera transform. We call the camera shape the <strong>main camera</strong> and the root transform <strong>stereo rig root</strong>.</li>
<li>When Maya scans for cameras in the scene it checks to see if a camera is a stereo camera by going up one level to the stereo rig root and checks for the existence of the following attribute:<br />
<ul>
<li><em>stereoRigType</em> – a string attribute that defines the name of the stereo rig. Maya default stereo camera rig has the name ‘Stereo Camera’. Your rig type should be a unique string, ideally not something called ‘Stereo Camera’, although it is possible to name your camera ‘Stereo Camera’ and remove Maya’s stereo camera model.</li>
<li><em>centerCam</em> – is a message attribute that points to the main camera. This attribute does not necessarily have to point to a unique camera. In circumstances where the left camera is identical to the center camera this attribute can point to the left camera.</li>
<li><em>leftCam</em> – is a message attribute that points to the left camera.</li>
<li><em>rightCam</em> – is a message attribute that points to the right camera.</li>
</ul>
</li>
</ul>
<p>It is perfectly valid and possible to make a simple mono camera into a stereo camera by simply adding the attributes above.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017ee4d53973970d-pi" style="display: inline;"><img alt="Stereo-outliner" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017ee4d53973970d" src="/assets/image_8e3c10.jpg" style="float: right;" title="Stereo-outliner" /></a></p>
<p>Before continuing, it is important to note why we choose a data driven approach to representing stereo camera rigs. First, as a counter example, let’s describe a common, alternative, representation. The alternative implementation to this setup is to have a command / callback approach to querying information about the stereo camera. The user would register a series of commands with a manager. That manager would invoke those commands when ‘discovering’ or querying stereo rigs. The downside to this approach is that closes the access to the information to a specific script or API function so the information is not as transparent to the user. It also makes transporting the stereo rig in the pipeline more difficult because code must be written to support the rig in other software packages. In a data driven approach, the information about the stereo camera rig is transparent and easily transported to another software package without the need to transport the code that created the stereo cameras. This also makes it easy to augment existing tools with new tools without having the need to change custom tools.</p>
<p>The film pipeline is becoming more complex and it is necessary to transport the data created in one software package to another package transparently; so data driven representations are becoming increasingly more important. For stereo, in particular, the entire pipeline is affected and requires a holistic approach to dealing with the data transport [8].</p>
<p>The structure outlined above only makes stereo camera rigs visible to the Maya tools – particularly the stereoscopic visualization. However, it doesn’t provide an easy mechanism to create new cameras from within the Maya UI. Fortunately there is a manager that hooks into the creation UI to allow new camera rigs to be created from the Maya UI.</p>
<h3>Camera Creation Mechanism</h3>
<p>Camera creation is control via a stereoscopic rig manager (stereoRigManager). This is the only command / script registry that is used for S3D rigs. It is essential because no other representation exists in Maya to indicate how stereoscopic rigs are created. Everything is gathered implicitly from existing scene data as outline in the section above.</p>
<p>The Maya stereoscopic rig manager will accept Python or MEL scripts. New rig creation scripts are registered via the ‘-add’ flag on the stereo rig manager. The following text box below illustrates some examples.</p>
<pre class="brush: python; toolbar: false;"># Remember the default rig
defRigBefore = cmds.stereoRigManager(query=True, defaultRig=True)
# Register new rig types, using MEL or Python implementations.
cmds.stereoRigManager(add=[&#39;StereoCameraHier&#39;, &#39;Python&#39;, &#39;maya.app.stereo.stereoCameraHierarchicalRig.createRig&#39;])
cmds.stereoRigManager(add=[&#39;StereoCameraMulti&#39;, &#39;Python&#39;, &#39;maya.app.stereo.stereoCameraComplexRig.createRig&#39;])
cmds.stereoRigManager(add=[&#39;StereoCameraSimple&#39;, &#39;MEL&#39;, &#39;stereoCameraSimpleRig&#39;])
</pre>
<p style="text-align: center;"><strong><span style="text-decoration: underline;">Figure 5: Rig Manager Registration</span></strong></p>
<p>Via this rig manager, users can add scripts that create new stereo rigs. The responsibility of the script is to simply return the <strong>main camera</strong>, left camera, and right camera shapes. Since we assume that the <strong>stereo rig root</strong> is a direct parent of main camera we can find the rig root implicitly. When the command to create a new rig is issued from the UI the corresponding script is called. The script is expected to return the required camera shapes. The underlying tools will then add metadata attributes specified in the Stereo Rig Structure section automatically – it is not necessary for the script to create these attributes; however, if they exist then they are not overwritten.</p>
<p>There are a few important considerations when registering your own camera creation mechanism.</p>
<ul>
<li>The rig creation script is not saved out when a file is saved. So to integrate new tools into a Maya based S3D pipeline, it will be necessary to register a rig when Maya is first started. This can be customized in start-up scripts. This topic is outside the scope of this white paper, but there are many tools available to facilitate this process.</li>
<li>It is also possible to delete registered rigs using the ‘-delete’ flag. Users can even delete Maya’s default camera rig. However, when the default rig is deleted there must be another rig registered to replace it.</li>
</ul>
<h3>Simple Custom S3D Rig</h3>
<p>For illustrative purposes, we will outline a very simply camera rig structure. The structure will be a left / right camera with the right camera also acting as the master camera.</p>
<pre class="brush: cpp; toolbar: false;">global proc string[] stereoCameraSimpleRig()
{
string $basename = &quot;stereoCameraSimple&quot;;
// Create the root of the rig
//
string $root[] = `camera -name $basename`;
string $left[] = `camera -name ($basename+&quot;Left&quot;)`;
setAttr ($left[0]+&quot;.tx&quot;) -6.32;
parent $left[0] $root[0];
select -r $root[0];
return {$root[0], $left[1], $root[1]};
}
stereoRigManager -add &quot;simpleRig&quot; &quot;MEL&quot; &quot;stereoCameraSimpleRig&quot;;</pre>
<p style="text-align: center;"><strong><span style="text-decoration: underline;">Figure 6: Simple MEL script for Custom Rig</span></strong></p>
<h2>Visualizing</h2>
<p>A critical aspect to S3D production is visualizing the S3D image. Consideration must be given to how you will review the content. Maya has built-in support for visualizing a stereoscopic pair using several different viewing modes. In addition, it is important to have a process to review stereoscopic content. This process is outside the scope of this white paper but it is something that must be planned and considered. In some cases, there is a cost to consider as well.</p>
<p>At the very least, you should have a theatre-sized room where you can review the stereoscopic content comparable to the audience theatre. This is important because scale plays a big factor in the depth perception. A larger screen implies a larger separation between the projected images and scales the overall depth in the scene.</p>
<p>So to understand the scale of an S3D image you should always approve in mini-theatrical environment. Again, the Bercovitz formulas [7] provide mathematics to help set the appropriate separation based on the desired maximum pixel separation.</p>
<p>Viewing Methods</p>
<p>The technology keeps advancing and vendors are bringing 3D to the masses. At the current pace, technology companies want to install a 3D television in every household. It remains to be seen if this will happen. However, the technology by which to visualize S3D images varies greatly and is constantly changing. There are two main types of stereoscopic content:</p>
<ul>
<li><strong>Passive</strong> – the display or projector is altering the light such that each eye receives a unique perspective. The viewer typically wears a lightweight pair of glasses where each lens of the eyeglasses is sensitive to a different spectrum or polarization of the light.</li>
<li><strong>Active</strong> – this is where the glasses participate in the controlling what light reaches the eyes of the viewer. Active usually have electronics built directly into the glasses that are synchronized to an external source that is telling what lens should be open. The frequency by which this changes happens so rapidly that it is not perceived by the viewer.</li>
</ul>
<p>Maya supports several viewing methods covering the current spectrum of technologies including passive and active methods.</p>
<ol>
<li><strong>Anaglyph</strong> -- this is the traditional red / cyan glasses that you find in magazines or 3D movies. The nice thing about anaglyph is that it is cheap and easily accessible. Anaglyph in Maya is performed by simply filtering the green and blue color channels, drawing everything in the scene, then repeating the process with the right camera filter the red channel. Note that color bleeding can occur where the objects color passes through only one filter. There are alternative techniques to anaglyph that attempt to balance the colors so that they don’t bleed in the channels. Maya does not support this mode at the moment.</li>
<li><strong>Luminance Anaglyp</strong>h – this is similar to anaglyph mode although it converts all color channels to a luminance based model and then apply the red-cyan filtering.</li>
<li><strong>Stereo (Quadbuffer Mode)</strong> – this is the catch all mode for those who have dedicated hardware to visualize stereo. This is the stereo mode supported by mid-level to high-end NVidia Quadro based graphics cards. The output of this mode is controlled via graphics card driver. Maya can output directly to large screen environments using this mode. Typically the common viewing methods are<br /><ol type="a">
<li><strong>Shuttering glasses</strong> – an active based viewing system.</li>
<li><strong>Clone mode</strong> – each eye is sent on a different output of the graphics card.</li>
</ol></li>
<li><strong>Checkerboard</strong> – this mode is for displays that use an active method to display stereo. The pixels are interlaced in a checkerboard fashion and the display flickers between the two sets rapidly.</li>
<li><strong>Horizontal Interlaced</strong> – this mode is for displays that support a passive visualization of stereo content. This is achieved with a special manufacturing process on LCD screens that polarize the light depending on the pixel position.</li>
<li><strong>Freeview</strong> – this mode is for people who want to see a side-by-side view of the left and right cameras or those who are capable of ‘freeviewing’ stereo, i.e. look at a pair of images and fuse the images by controlling the ocular muscles of the eyes.</li>
</ol>
<p>These methods are the most common stereoscopic visualization techniques used at the time of the writing. Depending on the hardware will dictate the type of view mode.</p>
<h3>Hardware Considerations</h3>
<p>It is not practical to supply every artist with a machine that can display stereoscopic imagery. The focus should be on proper viewing rooms where groups of people can review the content. For the layout&#0160;artists, editorial, and compositors stereoscopic displays should be made available so that their work can be visualized as it is being completed and before it makes it to daily review. It is not the objective of this paper to recommend any particular piece of hardware, but rather to provide some guidelines to consider when making the investment in hardware:</p>
<ol>
<li><strong>Refresh Rate</strong> – the stereoscopic effect is achieved by showing each eye a unique perspective. So each eye must receive an image fast enough such that flicker is not visible. At least 120 Hz for vertical refresh rate is necessary to produce a smooth visual image.</li>
<li><strong>Polarized / Active</strong> – active glasses can be expensive. For occasional users on individual workstations they are nice. Some people also complain about eye fatigue with these types of glasses. Polarized are more on par with the final viewing medium and perfect for larger environments.</li>
<li><strong>Interlaced Displays</strong> – there is usually a loss of image resolution in these displays. For those artists who need to work with the original plates, i.e. editorial or DI, interlace is not the best suited display.</li>
<li><strong>Anaglyph is not that bad</strong> – for those artists who want to occasionally visualize the stereoscopic content, but it is not a necessary function, Analgyph is the way to go. It is cheap and easy to deploy. Sure it is a bit “retro”, but it works great for quick visualization.</li>
</ol>
<h2>Sharing S3D in the Pipeline</h2>
<p>This paper has only discussed the frontend scene construction of an S3D pipeline. Consideration must be given to stereoscopic scene construction as it is handed off to other parts of the pipeline. Particularly editing and editorial changes directly influence the stereoscopic setup in a scene. Human stereoscopic vision centers cannot handle the drastic changes in interocular, zero parallax value, and focal length. Any rapid cuts or edits of the former would likely cause an unpleasant viewing experience or severe headaches caused by the rapid transitions. Therefore if a series of sequences have been laid out and consideration was taken to make sure that the cuts did not cause visual processing problems by the audience, then any changes to a sequence must reconsider the sequence from a stereoscopic perspective as well. However, often in the editing process, re-rendering is not always a viable option.</p>
<p>Fortunately, it is possible to alleviate some of these issues by proper planning in the pipeline. For instance, it is possible to change the interaxial and zero parallax of rendered scene if you have the depth map and prior knowledge about the render settings, i.e. interaxial and zero parallax. Using various conversion formulas it is possible to adjust an image or a shot after it has been rendered to fit better with editorial transitions [8].</p>
<p>Editorial is just the beginning of the inter-relationships between the various production departments. Compositing now requires duplicating composites for the second camera. Digital intermediate requires color correction on both images to match. The Autodesk tool chain of Autodesk® Lustre® and Autodesk® Toxik™ help solve these problems by providing stereo tools that replicate work on a single image to the image’s other pair. Again, the pipeline must be prepared to handle this data.</p>
<ol>
<li>Be prepared for the extra storage cost – stereo requires another image to be rendered and will have double the frame cost in terms of storage. You may also need to transport the depth maps for each image to extract valuable depth information to re-adjust the stereo parallax.</li>
<li>Be prepared for the extra rendering cost – some renderers are not smart enough to keep the existing render state between rendering the other camera. Be sure to understand your rendering cost before starting production.</li>
<li>Use left or right image for mono version – because of the storage requirements, it is better to utilize the left or right eye for your mono channel, i.e. 2D version.</li>
<li>Keep the metadata – keep information about the zero parallax, interaxial, and stereo mode as you hand the data down the pipeline. This will help you make production tweaks to the images in compositing and DI stages.</li>
</ol>
<h2>Conclusion</h2>
<p>We have provided an in depth overview of the stereoscopic technology in Autodesk Maya software and how it can be leveraged to establish in an S3D pipeline. We discussed the Maya camera model and how this base can be used to construct stereoscopic cameras. We used this foundation to introduce the Maya stereo camera model and how that model can be extended with new tools. We reviewed the important metadata that must be transported in the pipeline to allow the greatest flexibility for change later. Finally we reviewed the display technology and some general considerations choosing S3D display medium.</p>
<h2>References</h2>
<p>Autodesk, Inc. (2008, October) www.autodesk.com. [<a href="http://images.autodesk.com/adsk/files/stereoscopic_whitepaper_final08.pdf" target="_self">Online</a>].</p>
<p>Ray Zone, Stereoscopic Cinema &amp; the Origins of 3-D Film, 1838-1952. USA: University Press of Kentucky, 2007.</p>
<p>Mason Woo, Jackie Neider, Tom Davis, and Dave Shreiner, OpenGL(R) Programming Guide Third Edition. Reading: Addison-Wesley, 1999.</p>
<p>Lenny Lipton, Foundations of the Stereoscopic Cinema.: Van Nostrand Reinhold, 1982.</p>
<p>Lenny Lipton. (2008, March) Lenny Lipton. [<a href="http://lennylipton.wordpress.com/2008/03/26/the-projection-dilemma-2/" target="_self">Online</a>].</p>
<p>František Mikšícek, &quot;Causes of Visual Fatigue and Its Improvements in Stereoscopy ,&quot; University of West Bohemia in Pilsen, Pilsen, Technical Report DCSE/TR-2006-04, 2006.</p>
<p>John Bercovitz, &quot;Image-side perspective and stereoscopy,&quot; in Stereoscopic Displays and Virtual Reality Systems, 1998, pp. 288-298.</p>
<p>Sebastian Sylwan, David MacDonald, and Jason Walter, &quot;Stereoscopic CG Camera Rigs and Associaed Metadata for Cinematic Production,&quot; in Stereoscopic Displays &amp; Applications, San Jose, 2009.</p>
<p>M. McKenna, &quot;Interactive viewpoint control and three-dimensional operations,&quot; in Symposium on Interactive 3D Graphics, New York, 1992, pp. 53-56.</p>
<p>H.S. Guo, Y. Asmuth, J. Kumar, R. Sawhney, &quot;Multi-view 3D estimation and applications to match move,&quot; in Multi-View Modeling and Analysis of Visual Scenes, 1999. (MVIEW &#39;99) Proceedings, Fort Collins, CO, USA, 1999, pp. 21-28.</p>
<h2>Appendix I – Camera Projection Code</h2>
<pre class="brush: cpp; toolbar:false;">void computeFilmFit(
	filmFit ffit,
	double filmFitOffset,
	double window_aspect,
	double aperture_x,
	double aperture_y,
	double &amp;scale_x,
	double &amp;scale_y,
	double &amp;translate_x,
	double &amp;translate_y )
	//
	// Description:
	// Helper method to compute the scale and translate ratios for the
	// given film fit.
	//
	// Objects will appear stretched - taller or wider -depending on
	// whether the aspect ratio of the film back is greater than or
	// smaller than the aspect ratio of the viewport. This behavior is
	// undesirable, but can be overcome by resizing the film back to
	// match the aspect ratio of the viewport. The user can make one of
	// four choices in resolving the film fit mis-match: always crop to
	// the smallest area (fill), always contain the horizontal or
	// vertical and crop in the opposite direction (horizontal and
	// vertical) or contain the largest area and include both horizontal
	// and vertical (overscan).
	//
{
	double film_aspect = aperture_x / aperture_y;
	switch (ffit) {
	case kFillFilmFit:
		if (window_aspect &lt; film_aspect) {
			scale_x = window_aspect / film_aspect;
		}
		else {
			scale_y = film_aspect / window_aspect;
		}
		break;
	case kHorizontalFilmFit:
		scale_y = film_aspect / window_aspect;
		if (scale_y &gt; 1.0) {
			translate_y = filmFitOffset *
				(aperture_y - (aperture_y * scale_y)) / 2.0;
		}
		break;
	case kVerticalFilmFit:
		scale_x = window_aspect / film_aspect;
		if (scale_x &gt; 1.0) {
			translate_x = filmFitOffset *
				(aperture_x - (aperture_x * scale_x)) / 2.0;
		}
		break;
	case kOverscanFilmFit:
		if (window_aspect &lt; film_aspect) {
			scale_y = film_aspect / window_aspect;
		}
		else {
			scale_x = window_aspect / film_aspect;
		}
		break;
	}
}

void computeViewingFrustum (
	double window_aspect,
	double&amp; left,
	double&amp; right,
	double&amp; bottom,
	double&amp; top,
	bool applyOverscan /* = false */,
	bool applySqueeze /* = false */) const
	//
	// Description:
	// Compute the position and size of the rectangular viewing
	// frustum in the near clipping plane. The result is ready to be
	// fed into the GL window() call (or to the OpenGL glFrustum()
	// call).
	//
{
	bool ortho = isOrtho();
	double squeeze = applySqueeze ? lensSqueezeRatio() : 1.0;
	double aperture_x, aperture_y;
	double offset_x, offset_y;
	double focal_to_near;
	if ( ortho ) {
		// There isn&#39;t really an aperture in orthographic view. User
		// define the width and height of the orthographic view.
		//
		aperture_x = orthoWidth();
		aperture_y = orthoWidth();
		if(shakeOverscanEnabled())
		{
			aperture_x *= shakeOverscan();
			aperture_y *= shakeOverscan();
		}
		offset_x = offset_y = 0.0;
		if(shakeEnabled())
		{
			offset_x += horizontalShake();
			offset_y += verticalShake();
		}
		if(stereoHorizontalImageTranslateEnabled())
		{
			offset_x += stereoHorizontalImageTranslate();
		}
		focal_to_near = 1.0;
	} else {
		if(shakeOverscanEnabled() ){
			aperture_x = horizontalFilmAperture() *
				shakeOverscan() * squeeze;
			aperture_y = verticalFilmAperture() * shakeOverscan();
		} else{
			aperture_x = horizontalFilmAperture() * squeeze;
			aperture_y = verticalFilmAperture();
		}
		offset_x = horizontalFilmOffset();
		offset_y = verticalFilmOffset();
		if( shakeEnabled() ){
			offset_x += horizontalShake();
			offset_y += verticalShake();
		}
		if(stereoHorizontalImageTranslateEnabled())
		{
			offset_x += stereoHorizontalImageTranslate();
		}
		offset_x *= squeeze;
		focal_to_near = nearClipPlane() /
			(focalLength() * MM_TO_INCH);
	}

	focal_to_near *= cameraScale();

	double scale_x = 1.0;
	double scale_y = 1.0;
	double translate_x = 0.0;
	double translate_y = 0.0;
	// Orthographic cameras have no notion of film fit offset.
	// Don&#39;t include film fit offset if we have an ortho
	// camera.
	//
	computeFilmFit( filmFit(),
		(ortho)?0.0:filmFitOffset(),
		window_aspect,
		aperture_x, aperture_y, scale_x, scale_y,
		translate_x, translate_y );
	if (applyOverscan) {
		double value = overscan();
		scale_x *= value;
		scale_y *= value;
	}
	left = focal_to_near *
		(-.5*aperture_x*scale_x+offset_x+translate_x);
	right = focal_to_near *
		( .5*aperture_x*scale_x+offset_x+translate_x);
	bottom = focal_to_near *
		(-.5*aperture_y*scale_y+offset_y+translate_y);
	top = focal_to_near *
		( .5*aperture_y*scale_y+offset_y+translate_y);
}

MMatrix postProjectionMatrix(double aspect/*=1.0)*/)
	//
	// Description:
	// Computes the matrix used for rotating the film back. This matrix
	// is used to postMultiply against the projection matrix.
	//
	// Arguments:
	// aspect - in most cases the aspect ratio scaling must be added
	// at the end of the post projection matrix. If we did it before
	// the postProjection then rotation would cause the image to skew
	// when scaled to the viewport. This parameter allows us to
	// add the aspect in at the end of our film roll calculations. It
	// has a default value of 1.0 when it is not used.
	//
	//
{
	MMatrix filmPMat, filmPInvMat, filmRoll,
		transMat, postMat, preMat;
	double frv = -filmRollValue();
	if ( fabs(frv) &gt; 0.0 ) {
		double hrp = horizontalRollPivot();
		double vrp = verticalRollPivot();
		filmRoll[0][0] = filmRoll[1][1] = cos(frv);
		filmRoll[1][0] = -(filmRoll[0][1] = sin(frv));
		filmPInvMat[3][0] = -( filmPMat[3][0] = hrp );
		filmPInvMat[3][1] = -( filmPMat[3][1] = vrp );
		filmRoll = filmPInvMat * filmRoll * filmPMat;
	}
	preMat[0][0] = preMat[1][1] = preScale();
	// During the perspective and orthographic projection matrix
	// construction the aspect ratio is stored in the computation.
	// When we add the post projection the 1.0/aspect will cancel the
	// aspect out and we will add it back in the end.
	//
	preMat[1][1] *= 1.0/aspect;
	postMat[0][0] = postMat[1][1] = postScale();
	// Add the aspect back in
	//
	postMat[1][1] *= aspect;
	transMat[3][0] = -filmTranslateH();
	transMat[3][1] = -filmTranslateV();
	if ( filmRollOrder() == kRotateTranslateFilmRoll ) {
		return preMat * filmRoll * transMat * postMat;
	} else {
		return preMat * transMat * filmRoll * postMat;
	}
}

MMatrix projectionMatrixNoPostPrj(
	double deviceAspectRatio) const
	//
	// Description:
	// Returns the projection matrix without the postProjectionMatrix
	// See T3dPort::perspective() and T3dPort::ortho().
	//
	// Arguments:
	// imageWidth - viewport width
	// imageHeight - viewport height
	// deviceAspectRatio - aspect ratio to squeeze image
	//
{
	MMatrix projection;
	float nearZ = (Tfloat)nearClipPlane();
	float farZ = (Tfloat)farClipPlane();
	if( (farZ - nearZ) &lt; 0.1f )
		farZ = nearZ + 0.1f;
	if ( isOrtho() ) {
		// Orthographic projection
		//
		double right, left, bottom, top;
		computeViewingFrustum( deviceAspectRatio,
			left, right, bottom, top, false, false );
		float diffRL = 1.0f / float(right - left);
		float diffTB = 1.0f / float(top - bottom);
		float diffFN = 1.0f / (farZ - nearZ);
		projection [0][0] = 2.0f * diffRL;
		projection [1][1] = 2.0f * diffTB;
		projection [3][0] = -float(right + left) * diffRL;
		projection [3][1] = -float(top + bottom) * diffTB;
		projection [2][2] = 2.0f * diffFN;
		projection [3][2] = (farZ + nearZ) * diffFN;
	}
	else {
		// Perspective projection
		//
		// Setup a perspective projection matrix using a rectangle // on the near clipping plane (left, right, bottom, top) and
		// distance to both clipping planes.
		double left, right, bottom, top;
		computeRenderingFrustum(deviceAspectRatio,
			left, right, bottom, top );
		float diffRL = float(right - left);
		diffRL = ( diffRL &lt; 0.000001f ) ? 100000.0f : 1.0f / diffRL;
		float diffTB = float(top - bottom);
		diffTB = ( diffTB &lt; 0.000001f ) ? 100000.0f : 1.0f / diffTB;
		float diffFN = 1.0f / (farZ - nearZ);
		float twoNear = 2.0f * nearZ;
		projection [0][0] = twoNear * diffRL;
		projection [1][1] = twoNear * diffTB;
		projection [2][0] = Tfloat(right + left) * diffRL;
		projection [2][1] = Tfloat(top + bottom) * diffTB;
		projection [2][3] = -1.0f;
		projection [3][3] = 0.0f;
		projection [2][2] = (farZ + nearZ) * diffFN;
		projection [3][2] = (twoNear * farZ) * diffFN;
	}
	return projection;
}

MMatrix projectionMatrix(
	int imageWidth,
	int imageHeight,
	double deviceAspectRatio) const
	//
	// Description:
	// Returns the projection matrix (with the post projection matrix).
	// Arguments:
	// imageWidth - viewport width
	// imageHeight - viewport height
	// deviceAspectRatio - aspect ratio to squeeze image
	//
{
	double aspect = (double) imageWidth / imageHeight;
	if (!isOrtho()) {
		aspect = deviceAspectRatio;
	}
	MMatrix prj = projectionMatrixNoPostPrj(deviceAspectRatio);
	return prj * postProjectionMatrix(aspect);
}
</pre>

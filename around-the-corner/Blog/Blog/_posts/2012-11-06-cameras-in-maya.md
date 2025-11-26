---
layout: "post"
title: "An introduction to Maya Camera Model"
date: "2012-11-06 02:20:40"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "CG"
  - "Cyrille Fauvel"
  - "Films"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2012/11/cameras-in-maya.html "
typepad_basename: "cameras-in-maya"
typepad_status: "Publish"
---

<p>This post is an introduction to the Maya Camera Model which is a preamble to&#0160;an in-depth discussion and review of stereoscopic cameras.</p>
<h2>The Maya Camera Model</h2>
<p>
Virtual cameras have been around since the beginning of computer graphics (CG). They are an essential aspect to the creation of the imagery that is seen today and the math mimics the basic principles of real-world cameras. They are the first step to bringing the director’s vision to life. Like real-world cameras, virtual cameras have a focal length, depth of field, f-stop and focus distance. Unlike real-world cameras, virtual cameras have to be defined in every aspect. There is no such thing as a 35mm camera in the virtual world. Instead, a virtual camera is employed that projects in a format equivalent to a real world 35mm film camera.
The benefit of being able to control every aspect of the camera provides ultimate creative control. For instance, a camera can be placed anywhere in a virtual environment without physical constraints. Furthermore, many limitations of real world cameras, (for example lens distortion, chromatic aberration, fringing, breathing, etc.) which can cause problems in S3D production, are not present in CG cameras.
To understand how virtual cameras allow us to create better stereoscopic imagery, we must take a look at parameters that are available on a camera and the correlation to real-world cameras.</p>
<h2>Camera Parameters</h2>
<p>One characteristic found in cameras is perspective foreshortening. This effect is crucial to the illusion of 3D on a 2D image and it is even more critical to a S3D image as the foreshortening actually helps the human visual system process the image and re-construct in 3D.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c33283509970b-pi" style="display: inline;"><img alt="Perspective-camera" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c33283509970b image-full" src="/assets/image_fbb7e8.jpg" title="Perspective-camera" /></a><br /><span style="text-decoration: underline;"><strong>Figure 1: Perspective Projection</strong></span></p>
<p>Maya software constructs a projection matrix similar to the OpenGL® specification using the 6 parameters that define the viewing frustum of the camera. Figure 1 illustrates these 6 parameters. The camera frustum is the entire perspective viewing area of the camera and is composed of a left, right, top, bottom, near and far parts [2]. The Maya camera parameters translate directly to these 6 values which form a projection matrix. The Maya camera model is a very comprehensive representation of a virtual camera. For the purposes of S3D production we will only focus on the following parameters, which are illustrated in Figure 2:</p>
<ul>
<li><strong>Near Clip Plane</strong> – closest point from the camera that is visible to the camera.</li>
<li><strong>Far Clip Plane</strong> – furthest point from the camera that is visible to the camera.</li>
<li><strong>Film Back</strong> – mimicking a real-world camera there is a virtual film onto which images are projected.</li>
<li><strong>Film Aperture</strong>&#0160;(A) – the format of the projected image area. Normally this is expressed recalling common film formats, i.e., super 8, 35 mm, academy, HD, etc...</li>
<li><strong>Focal Length</strong>&#0160;(FL) – the type of lens. In virtual cameras this is either specified as a field of view or by focal length. Since CG cameras are based on a pinhole camera model, there is a perfect equivalence between the two representations.</li>
<li><strong>Film Offset</strong>&#0160;(O) – offset parameters that are applied on top of the film back projection area, represented in inches. It represents the shift of film along the back of the camera: up, down, left or right. This shift skews the projected region as depicted in Figure 1. Specifying a positive or negative value on the horizontal shift is equivalent to a horizontal image translation [3]. In fact, Autodesk Maya supports horizontal image translation as a separate parameter; however, in the context of this paper this will be ignored and all stereo computation will be performed with the above parameters.</li>
</ul>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3d56a581970c-pi" style="display: inline;"><img alt="Pin-hole-camera" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3d56a581970c" src="/assets/image_3661a5.jpg" style="float: right;" title="Pin-hole-camera" /></a></p>
<p>The horizontal film offset allows us to shift the viewing region of the camera without actually moving the camera. The effect of this operation is a perspective shift that is equivalent to pixel shifting. However this pixel shifting is not a 2D effect. The entire digital scene has been shifted on the projected film back. In the digital world you are exposing a different part of an infinite piece of film. This allows us to manipulate the viewing region of a camera without physically moving the camera in space.<br />Often many people feel that the angle of view should be involved in the calculation; however, angle of view can be expressed as focal length with film aperture size. So Maya does not provide this as an attribute. Also, the shift provided by the Film Offset is not always equivalent in vertical and horizontal directions. For example, a .5 shift in the horizontal axis is not the same as a .5 shift in the vertical axis. This is due to the aspect ratio of the aperture which usually favors horizontal resolution over the vertical resolution.<br />It is important to consider the consequences of the viewing region when constructing the final projection matrix. Everything would be perfect if the aspect ratio of the viewport matched the aspect ratio of the projection matrix that we setup with the parameters specified above. Maya software users are free to resize the viewport to whatever resolution they feel necessary. This unfortunately can skew the image because the aspect ratio does not match the viewing area. Users can address this by factoring in the aspect ratio of the window to fit the viewing area. There are four different ways the fit can be performed and it is controlled by an attribute on the camera shape called <strong>Film Fit</strong>. (A complete implementation of the Maya projection matrix can be found in Appendix I – Camera Projection Code.)</p>
<h2>Mathematics</h2>
<p>Before we begin to describe the S3D camera, it is important review the formulas for shifting the frustum of a camera. We will use this perspective shift to create the effect of zero parallax with a pair of stereoscopic cameras. This formula will be the foundation for the S3D camera and is the basis for the projection matrix constructed in Appendix I.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c33281494970b-pi" style="display: inline;"><img alt="Fustrum" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c33281494970b" src="/assets/image_e8770b.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Fustrum" /></a></p>
<p>Each entry in the matrix is a side of the near plane illustrated in Figure 1. FL<sub>n</sub> is the distance to the focal plane to the near clip plane.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3d56ae42970c-pi" style="display: inline;"><img alt="Horz-offset" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3d56ae42970c image-full" src="/assets/image_c77c1f.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Horz-offset" /></a><span style="text-decoration: underline;"><strong>Figure 3: Horizontal Offset – Top (offset = 0.0), Bottom (offset = 0.25)</strong></span></p>
<p>The any offset value will shift the plane in space (see illustration in Figure 3). However, the shift does not affect the camera position. This will play an important part in S3D camera model which we will discuss next. Particularly it allows us to create the zero parallax effect where objects appear to appear in front of the screen.</p>
<p>Our eyes tend to converge; they rotate in-ward to focus on an object. This action is what our brain uses to build a 3D image in our brain. However, when projecting into a cinematic environment, not all audience members are focusing or sitting in the same location. So constructing images from rotated camera pairs will not provide the best image possible for an audience. There are occasions where a toe-in camera is acceptable, but that is a topic beyond this paper. We will only discuss the mathematics of a toe-in camera over a shifted stereoscopic camera.</p>
<p>On the next post, we have&#0160;an in-depth discussion and review of Maya stereoscopic cameras.</p>

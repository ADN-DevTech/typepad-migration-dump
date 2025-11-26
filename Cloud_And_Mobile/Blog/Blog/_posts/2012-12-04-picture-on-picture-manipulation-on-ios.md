---
layout: "post"
title: "Picture on picture manipulation on iOS"
date: "2012-12-04 08:03:16"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iOS"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2012/12/picture-on-picture-manipulation-on-ios.html "
typepad_basename: "picture-on-picture-manipulation-on-ios"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In preparation for my AU presentation I created a few samples. One of those enables you to take a picture then place an existing picture on top of that and manipulate it (rotate, scale, move) - I'll talk about the others in other posts.</p>
<p>We were looking for partner applications on mobile devices to show to attendees at our Developer Days conferences and at AU. One of those is a simple yet powerful application called JobViewer by FabCAD: <a href="https://itunes.apple.com/az/app/jobviewer/id526728663?mt=8" target="_self">https://itunes.apple.com/az/app/jobviewer/id526728663?mt=8</a>&nbsp;<br />It enables you to show what a given gate would look like at the exact future location.</p>
<p>Previously sales people would only provide brochures and so the buyers could only use a "poor man's augmented reality approach" :) - i.e. hold the brochure in front of them when looking at the place where the gate was planned to be placed. Now with this program people can take a picture of the place and then merge the picture of the gate on top of it. This makes it much easier to imagine/see how the gate will really fit in the environment. Not to mention that you can also easily take a screenshot on your mobile device and send this picture around to family or other people involved in deciding about the gate. This could work with many other products as well of course.</p>
<p>So let's see how you could write a program like that. &nbsp;</p>
<p>1) We can start with a <strong>Single View Application</strong> in <strong>Xcode</strong>&nbsp;</p>
<p>2) Then we can add an <strong>Image View</strong>, then a&nbsp;<strong>Toolbar</strong> and an extra <strong>Bar Button Item</strong> on our view in the storyboard&nbsp;</p>
<p>3) The <strong>Identifier</strong> of one of the button's can be set to <strong>Camera</strong> and the other one to <strong>Search</strong>&nbsp;</p>
<p>
<a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5dfacc2970d-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b017ee5dfacc2970d" title="Camera" src="/assets/image_451508.jpg" alt="Camera" /></a></p>
<p>4) We need to hook those up with the code. The easiest way to do that is if we set the <strong>Editor</strong> to <strong>Show the Assistant Editor&nbsp;
<a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3e6add1e970c-pi">
</a><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3e6ade5b970c-pi">
</a><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5df9e3a970d-popup">
</a><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c343bf71b970b-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b017c343bf71b970b" style="width: 40px;" title="Editor" src="/assets/image_7f5d00.jpg" alt="Editor" /></a></strong>, so that we can see two documents at the same time. Then we can open the <strong>storyboard</strong> in one view and open <strong>ViewController.h</strong> in the other one. Now we can simply <strong>Ctrl-drag</strong> the tool bar item into the code area. When the drag is finished a little dialog pops up where we can set if we want to use the control's event/action or create a reference to it (e.g. if we wanted to query or modify its properties later on)<br />This time we just need to handle the action, so let's go with that</p>
<p>
<a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee5dfa6f3970d-popup">
</a><a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d3e6ae7d0970c-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b017d3e6ae7d0970c" title="StartCamera" src="/assets/image_af7dcf.jpg" alt="StartCamera" /></a></p>
<p>5) We need to hook the other button up as well in a similar fashion. Let's call its action <strong>findPic</strong>, then create a reference to the <strong>Image View</strong> the same way but this time set <strong>Connection</strong> to <strong>Outlet</strong> and&nbsp;<strong>Name</strong> to <strong>imageView</strong></p>
<p>6) On the net we can find sample codes for both taking a picture and accessing the pictures on the mobile device. Using those we can implement the <strong>startCamera</strong> and <strong>findPic</strong> functions in the <strong>ViewController.m</strong> file</p>
<p>7) In order to handle the gestures we can simply drag and drop <strong>Gesture Recognizers</strong> onto the <strong>Image View</strong> in the storyboard&nbsp;</p>
<p>
<a class="asset-img-link" style="display: inline;" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c343c24b8970b-popup"><img class="asset  asset-image at-xid-6a0167607c2431970b017c343c24b8970b" title="Gestures" src="/assets/image_b4b912.jpg" alt="Gestures" /></a><br />8) Once the <strong>Rotation</strong>, <strong>Pan</strong> and <strong>Pinch Gesture Recognizers</strong> are added to the view we can hook them up to the code the same way as we did with the buttons, <strong>Ctrl-dragging</strong> them into the code area selecting <strong>Action</strong> as <strong>Outlet</strong> and naming the handler functions <strong>rotate</strong>, <strong>pan</strong>&nbsp;and <strong>pinch</strong>&nbsp;</p>
<p>9) There are multiple ways we can manipulate the image. This time we'll use <strong>setTransform</strong> for <strong>rotation</strong> and <strong>scaling</strong> (pinch), and <strong>setCenter</strong> for <strong>panning</strong> &nbsp;</p>
<p>Code of <strong>ViewController.h</strong>:</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; ViewController.h</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; PicOnPic</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; Created by Adam Nagy on 20/10/2012.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; Copyright (c) 2012 Adam Nagy. All rights reserved.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #78492a;">#import </span>&lt;UIKit/UIKit.h&gt;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@interface</span> ViewController : <span style="color: #703daa;">UIViewController</span>&lt;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span>UINavigationControllerDelegate<span style="color: #000000;">,</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span>UIImagePickerControllerDelegate<span style="color: #000000;">,</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span>UIPopoverControllerDelegate<span style="color: #000000;">&gt;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">IBAction</span>)startCamera:(<span style="color: #bb2ca2;">id</span>)sender;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">IBAction</span>)findPic:(<span style="color: #bb2ca2;">id</span>)sender;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@property<span style="color: #000000;"> (</span>weak<span style="color: #000000;">, </span>nonatomic<span style="color: #000000;">) </span>IBOutlet<span style="color: #000000;"> </span><span style="color: #703daa;">UIImageView</span><span style="color: #000000;"> * imageView;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@property</span> (<span style="color: #bb2ca2;">strong</span>, <span style="color: #bb2ca2;">nonatomic</span>) <span style="color: #703daa;">UIImageView</span> * overlayImageView;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@property</span> (<span style="color: #bb2ca2;">strong</span>, <span style="color: #bb2ca2;">nonatomic</span>) <span style="color: #703daa;">UIPopoverController</span> * popoverController;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">- (</span><span style="color: #bb2ca2;">IBAction</span><span style="color: #000000;">)rotate:(</span>UIRotationGestureRecognizer<span style="color: #000000;"> *)sender;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">- (</span><span style="color: #bb2ca2;">IBAction</span><span style="color: #000000;">)pinch:(</span>UIPinchGestureRecognizer<span style="color: #000000;"> *)sender;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">- (</span><span style="color: #bb2ca2;">IBAction</span><span style="color: #000000;">)pan:(</span>UIPanGestureRecognizer<span style="color: #000000;"> *)sender;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@end</p>
<span>
<p>Code of <strong>ViewController.m</strong>:</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; ViewController.m</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; PicOnPic</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; Created by Adam Nagy on 20/10/2012.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//&nbsp; Copyright (c) 2012 Adam Nagy. All rights reserved.</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;">//</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #78492a;">#import </span>"ViewController.h"</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #78492a;">#import </span>&lt;MobileCoreServices/MobileCoreServices.h&gt;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #bb2ca2;">@interface</span><span style="color: #000000;"> </span>ViewController<span style="color: #000000;"> ()</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@end</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@implementation</span> ViewController</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@synthesize</span> popoverController;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;"><span style="color: #bb2ca2;">@synthesize</span> overlayImageView;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)viewDidLoad</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"viewDidLoad"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; [</span><span style="color: #bb2ca2;">super</span><span style="color: #000000;"> </span>viewDidLoad<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)didReceiveMemoryWarning</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"didReceiveMemoryWarning"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; [</span><span style="color: #bb2ca2;">super</span><span style="color: #000000;"> </span>didReceiveMemoryWarning<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">IBAction</span>)startCamera:(<span style="color: #bb2ca2;">id</span>)sender</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"startCamera"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (</span>popoverController<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #4f8187;">popoverController</span><span style="color: #000000;"> </span>dismissPopoverAnimated<span style="color: #000000;">:</span><span style="color: #bb2ca2;">YES</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; </span>popoverController<span style="color: #000000;"> = </span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;">; &nbsp; &nbsp;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;"><span style="color: #000000;">&nbsp; &nbsp; </span>return<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> ([</span>UIImagePickerController<span style="color: #000000;"> </span><span style="color: #3d1d81;">isSourceTypeAvailable</span><span style="color: #000000;">:</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp;&nbsp; &nbsp; &nbsp; </span>UIImagePickerControllerSourceTypeCamera<span style="color: #000000;">])</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; </span>UIImagePickerController<span style="color: #000000;"> * imagePicker =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; [[</span>UIImagePickerController<span style="color: #000000;"> </span><span style="color: #3d1d81;">alloc</span><span style="color: #000000;">] </span><span style="color: #3d1d81;">init</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">delegate</span> = <span style="color: #bb2ca2;">self</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">sourceType</span> =</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; </span>UIImagePickerControllerSourceTypeCamera<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">mediaTypes</span> = [<span style="color: #703daa;">NSArray</span> <span style="color: #3d1d81;">arrayWithObjects</span>:</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (<span style="color: #703daa;">NSString</span> *) <span style="color: #703daa;">kUTTypeImage</span>,</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #bb2ca2;">nil</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">allowsEditing</span> = <span style="color: #bb2ca2;">NO</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;"> </span>presentViewController<span style="color: #000000;">:imagePicker</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #3d1d81;">animated</span>:<span style="color: #bb2ca2;">YES</span> <span style="color: #3d1d81;">completion</span>:<span style="color: #bb2ca2;">nil</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">IBAction</span>)findPic:(<span style="color: #bb2ca2;">id</span>)sender</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"findPic"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (</span>popoverController<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #4f8187;">popoverController</span><span style="color: #000000;"> </span>dismissPopoverAnimated<span style="color: #000000;">:</span><span style="color: #bb2ca2;">YES</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; </span>popoverController<span style="color: #000000;"> = </span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;">; &nbsp; &nbsp;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;"><span style="color: #000000;">&nbsp; &nbsp; </span>return<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> ([</span>UIImagePickerController<span style="color: #000000;"> </span><span style="color: #3d1d81;">isSourceTypeAvailable</span><span style="color: #000000;">:</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp;&nbsp; &nbsp; &nbsp; </span>UIImagePickerControllerSourceTypeSavedPhotosAlbum<span style="color: #000000;">])</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; </span>UIImagePickerController<span style="color: #000000;"> * imagePicker =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; [[</span>UIImagePickerController<span style="color: #000000;"> </span><span style="color: #3d1d81;">alloc</span><span style="color: #000000;">] </span><span style="color: #3d1d81;">init</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">delegate</span> = <span style="color: #bb2ca2;">self</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">sourceType</span> =</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>UIImagePickerControllerSourceTypePhotoLibrary<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">mediaTypes</span> = [<span style="color: #703daa;">NSArray</span> <span style="color: #3d1d81;">arrayWithObjects</span>:</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (<span style="color: #703daa;">NSString</span> *) <span style="color: #703daa;">kUTTypeImage</span>,</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span style="color: #bb2ca2;">nil</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; imagePicker.<span style="color: #703daa;">allowsEditing</span> = <span style="color: #bb2ca2;">NO</span>;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; </span><span style="color: #4f8187;">popoverController</span><span style="color: #000000;"> = [[</span>UIPopoverController<span style="color: #000000;"> </span><span style="color: #3d1d81;">alloc</span><span style="color: #000000;">]</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>initWithContentViewController<span style="color: #000000;">:imagePicker];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; </span>popoverController<span style="color: #000000;">.</span><span style="color: #703daa;">delegate</span><span style="color: #000000;"> = </span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #4f8187;">popoverController</span><span style="color: #000000;"> </span>presentPopoverFromBarButtonItem<span style="color: #000000;">:sender</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>permittedArrowDirections<span style="color: #000000;">:</span>UIPopoverArrowDirectionUp</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; <span style="color: #3d1d81;">animated</span>:<span style="color: #bb2ca2;">YES</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)imagePickerController:</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">(</span>UIImagePickerController<span style="color: #000000;"> *)picker</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">didFinishPickingMediaWithInfo:(<span style="color: #703daa;">NSDictionary</span> *)info</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"imagePickerController"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; [picker </span>dismissViewControllerAnimated<span style="color: #000000;">:</span><span style="color: #bb2ca2;">YES</span><span style="color: #000000;"> </span>completion<span style="color: #000000;">:</span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; <span style="color: #703daa;">UIImage</span> * image =</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; [info </span><span style="color: #3d1d81;">objectForKey</span><span style="color: #000000;">:</span>UIImagePickerControllerOriginalImage<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&nbsp; </span>// If it's a picture selection</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (</span>popoverController<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #4f8187;">popoverController</span><span style="color: #000000;"> </span>dismissPopoverAnimated<span style="color: #000000;">:</span><span style="color: #bb2ca2;">YES</span><span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; </span>popoverController<span style="color: #000000;"> = </span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (</span>overlayImageView<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; [</span><span style="color: #4f8187;">overlayImageView</span><span style="color: #000000;"> </span>removeFromSuperview<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; <span style="color: #4f8187;">overlayImageView</span> = [[<span style="color: #703daa;">UIImageView</span> <span style="color: #3d1d81;">alloc</span>] <span style="color: #3d1d81;">initWithImage</span>:image];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>imageView<span style="color: #000000;"> </span><span style="color: #3d1d81;">addSubview</span><span style="color: #000000;">:</span>overlayImageView<span style="color: #000000;">];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&nbsp; </span>// If it's taken by the camera</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;"><span style="color: #000000;">&nbsp; </span>else</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; [<span style="color: #bb2ca2;">self</span>.<span style="color: #4f8187;">imageView</span> <span style="color: #3d1d81;">setImage</span>:image];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">BOOL</span>)popoverControllerShouldDismissPopover:</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">(<span style="color: #703daa;">UIPopoverController</span> *)popoverController</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"popoverControllerShouldDismissPopover"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;"><span style="color: #000000;">&nbsp; </span>return<span style="color: #000000;"> </span>YES<span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">- (<span style="color: #bb2ca2;">void</span>)popoverControllerDidDismissPopover:(<span style="color: #703daa;">UIPopoverController</span> *)popoverController</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"popoverControllerDidDismissPopover"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>popoverController<span style="color: #000000;"> = </span><span style="color: #bb2ca2;">nil</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">- (</span><span style="color: #bb2ca2;">IBAction</span><span style="color: #000000;">)pinch:(</span>UIPinchGestureRecognizer<span style="color: #000000;"> *)sender</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"pinch"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">static</span><span style="color: #000000;"> </span>CGAffineTransform<span style="color: #000000;"> origTr;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (sender.</span><span style="color: #703daa;">state</span><span style="color: #000000;"> == </span>UIGestureRecognizerStateBegan<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; origTr = </span>overlayImageView<span style="color: #000000;">.</span><span style="color: #703daa;">transform</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">else</span><span style="color: #000000;"> </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (sender.</span><span style="color: #703daa;">state</span><span style="color: #000000;"> == </span>UIGestureRecognizerStateChanged<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; <span style="color: #703daa;">CGFloat</span> scale = [sender <span style="color: #3d1d81;">scale</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; </span>CGAffineTransform<span style="color: #000000;"> tr =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>CGAffineTransformConcat<span style="color: #000000;">(</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; origTr,</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; &nbsp; </span>CGAffineTransformMakeScale<span style="color: #000000;">(scale, scale));</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>overlayImageView<span style="color: #000000;"> </span><span style="color: #3d1d81;">setTransform</span><span style="color: #000000;">:tr];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">- (</span><span style="color: #bb2ca2;">IBAction</span><span style="color: #000000;">)pan:(</span>UIPanGestureRecognizer<span style="color: #000000;"> *)sender</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"pan"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; <span style="color: #bb2ca2;">static</span> <span style="color: #703daa;">CGPoint</span> prevPt;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (sender.</span><span style="color: #703daa;">state</span><span style="color: #000000;"> == </span>UIGestureRecognizerStateBegan<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; prevPt = [sender <span style="color: #3d1d81;">locationInView</span>:<span style="color: #bb2ca2;">self</span>.<span style="color: #4f8187;">imageView</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">else</span><span style="color: #000000;"> </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (sender.</span><span style="color: #703daa;">state</span><span style="color: #000000;"> == </span>UIGestureRecognizerStateChanged<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; <span style="color: #703daa;">CGPoint</span> pt = [sender <span style="color: #3d1d81;">locationInView</span>:<span style="color: #bb2ca2;">self</span>.<span style="color: #4f8187;">imageView</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; </span>CGAffineTransform<span style="color: #000000;"> tr =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>CGAffineTransformMakeTranslation<span style="color: #000000;">(</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; pt.<span style="color: #703daa;">x</span> - prevPt.<span style="color: #703daa;">x</span>,</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; pt.<span style="color: #703daa;">y</span> - prevPt.<span style="color: #703daa;">y</span>);</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; <span style="color: #703daa;">CGPoint</span> cp =</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #78492a;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>CGPointApplyAffineTransform<span style="color: #000000;">(</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span><span style="color: #4f8187;">overlayImageView</span><span style="color: #000000;">.</span><span style="color: #703daa;">center</span><span style="color: #000000;">, tr);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>overlayImageView<span style="color: #000000;"> </span><span style="color: #3d1d81;">setCenter</span><span style="color: #000000;">:cp];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; prevPt = pt;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">- (</span><span style="color: #bb2ca2;">IBAction</span><span style="color: #000000;">)rotate:(</span>UIRotationGestureRecognizer<span style="color: #000000;"> *)sender</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">{</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #d12f1b;"><span style="color: #000000;">&nbsp; </span><span style="color: #3d1d81;">NSLog</span><span style="color: #000000;">(</span>@"rotate"<span style="color: #000000;">);</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">static</span><span style="color: #000000;"> </span>CGAffineTransform<span style="color: #000000;"> origTr;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (sender.</span><span style="color: #703daa;">state</span><span style="color: #000000;"> == </span>UIGestureRecognizerStateBegan<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; origTr = </span>overlayImageView<span style="color: #000000;">.</span><span style="color: #703daa;">transform</span><span style="color: #000000;">;</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; </span><span style="color: #bb2ca2;">else</span><span style="color: #000000;"> </span><span style="color: #bb2ca2;">if</span><span style="color: #000000;"> (sender.</span><span style="color: #703daa;">state</span><span style="color: #000000;"> == </span>UIGestureRecognizerStateChanged<span style="color: #000000;">)</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; {</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; <span style="color: #703daa;">CGFloat</span> rotation = [sender <span style="color: #3d1d81;">rotation</span>];</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #008400;"><span style="color: #000000;">&nbsp; &nbsp; </span>// Scale</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #703daa;"><span style="color: #000000;">&nbsp; &nbsp; </span>CGAffineTransform<span style="color: #000000;"> tr =</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; </span>CGAffineTransformConcat<span style="color: #000000;">(</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; &nbsp; &nbsp; &nbsp; origTr,</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #3d1d81;"><span style="color: #000000;">&nbsp; &nbsp; &nbsp; &nbsp; </span>CGAffineTransformMakeRotation<span style="color: #000000;">(rotation));</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; min-height: 13px;">&nbsp;</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #4f8187;"><span style="color: #000000;">&nbsp; &nbsp; [</span><span style="color: #bb2ca2;">self</span><span style="color: #000000;">.</span>overlayImageView<span style="color: #000000;"> </span><span style="color: #3d1d81;">setTransform</span><span style="color: #000000;">:tr];</span></p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">&nbsp; }</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo;">}</p>
<p style="margin: 0px; font-size: 11px; font-family: Menlo; color: #bb2ca2;">@end</p>
</span>
<p>That is all really. The project is attached:&nbsp;<span class="asset  asset-generic at-xid-6a0167607c2431970b017c3444cbfa970b"><a href="http://adndevblog.typepad.com/files/piconpic_2012-12-04.zip">Download PicOnPic_2012-12-04</a></span>&nbsp;<br />You just need to place some images on your device, perferably with some transparent parts, and then off you go. :)</p>
<p>Note: I've only tested the code on iPad, since I have no iPhone.</p>
</span></span>

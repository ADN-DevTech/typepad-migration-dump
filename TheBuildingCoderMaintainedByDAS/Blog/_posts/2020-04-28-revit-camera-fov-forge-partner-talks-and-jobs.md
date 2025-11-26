---
layout: "post"
title: "Revit Camera FOV, Forge Partner Talks and Jobs"
date: "2020-04-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Cloud"
  - "Data Access"
  - "Forge"
  - "Job"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/04/revit-camera-fov-forge-partner-talks-and-jobs.html "
typepad_basename: "revit-camera-fov-forge-partner-talks-and-jobs"
typepad_status: "Publish"
---

<p>Today we resuscitate a five-year old Revit API answer, still as fresh and useful as ever, followed by Forge and job opportunities truly fresh off the presses:</p>

<ul>
<li><a href="#2">Determining the Revit Camera FOV</a></li>
<li><a href="#3">Forge Partner Talks</a></li>
<li><a href="#4">Jobs at Autodesk</a></li>
</ul>

<h4><a name="2"></a> Determining the Revit Camera FOV</h4>

<p>Many people have asked how you can retrieve the Revit camera field of view or FOV.</p>

<p>As Valerii Nozdrenkov points out in
his <a href="https://thebuildingcoder.typepad.com/blog/2019/06/revit-camera-settings-project-plasma-da4r-and-ai.html#comment-4891620499">comment</a>
on <a href="https://thebuildingcoder.typepad.com/blog/2019/06/revit-camera-settings-project-plasma-da4r-and-ai.html">mapping the Revit camera settings to the Forge viewer</a>,
The Building Coder already published a solution suggested by Arno&scaron;t L&ouml;bel reading the required data from
the <a href="https://thebuildingcoder.typepad.com/blog/2014/09/custom-exporter-getcamerainfo.html">custom exporter <code>GetCameraInfo</code></a>:</p>

<blockquote>
  <p>When a view is processed and run through a custom exporter context, its properties are used to populate a <code>ViewNode</code> instance.</p>
  
  <p>One of its methods is <code>GetCameraInfo</code>, which provides information that ought to cover everything you need to know about the view's camera.</p>
</blockquote>

<pre class="code">
  public RenderNodeAction OnViewBegin(ViewNode node)
  {
    CameraInfo cameraInfo = node.GetCameraInfo();

    var view = document.GetElement(node.ViewId);
    return RenderNodeAction.Proceed;
  }
</pre>

<p>It seems worthwhile to reiterate this, since the question keeps popping up...</p>

<p><center>
<!--
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b4adc0c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b4adc0c200c img-responsive" style="width: 240px; display: block; margin-left: auto; margin-right: auto;" alt="Camera focal length" title="Camera focal length" src="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b4adc0c200c-250wi" /></a></p>

<p style="font-size: 80%; font-style:italic">[By SharkD](http://commons.wikimedia.org/wiki/User:SharkD), [CC BY-SA 4.0](https://creativecommons.org/licenses/by-sa/4.0) &ndash; In this simulation, adjusting the angle of view and distance of the camera while keeping the object in frame results in vastly differing images. At distances approaching infinity, the light rays are nearly parallel to each other, resulting in a 'flattened' image. At low distances and high angles of view objects appear 'foreshortened'.</p>

<p>--></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833025d9b4adc59200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833025d9b4adc59200c img-responsive" alt="Camera angle of view" title="Camera angle of view" src="/assets/image_ba9834.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Forge Partner Talks</h4>

<p>The virtual Forge accelerators are leading to a large number of technical webinars and zoom meetings with great attendance and participation.</p>

<p>It is time to also hold some 'business creation' meetings with Forge partners.</p>

<p>Here are some upcoming webinars in this area that might be of interest to you:</p>

<ul>
<li><a href="https://autodesk.zoom.us/webinar/register/7415875742427/WN_UiMEtQNiTFiPH8T_ekwk4w">Forge Partner Talks: Digital Twins</a> (May 6 @ 7am PST/4pm CET)
&ndash; Digital twins were one of the top ten tech trends last year, according to Gartner and Deloitte. Come and hear from Forge partners who have built or leveraged successful digital twin solutions and see how you can too.</li>
<li><a href="https://autodesk.zoom.us/webinar/register/6515877525566/WN_W6abt1RSR5K3V44HV5CtXQ">Forge Partner Talks: AR/VR</a> (May 13 @ 7am PST/4pm CET)
&ndash; See how leading Autodesk partners are bringing Forge-powered AR/VR solutions to the design and make space and learn how these solutions can benefit you.</li>
<li><a href="https://autodesk.zoom.us/webinar/register/8415877525897/WN_01GeR_H9RKOGTOg67Unagg">Forge Partner Talks: Configurators</a> (May 20 @ 7am PST/4pm CET)
&ndash; Hear from Forge partners who have increased their customer satisfaction by creating Forge-powered configurators and see how providing similar customization tools to your customers can help you maintain your competitive edge.</li>
<li><a href="https://autodesk.zoom.us/webinar/register/7515877526195/WN_vDxsTlF4QgS3s_8qIXYEXg">Forge Partner Talks: Dashboards and Insights</a> (May 27@  7am PST/4pm CET) 
&ndash; It's all about the data! Discover some of the innovative dashboards our top Forge partners have built, and see how these types of solutions can help you better visualize your data, draw insights, and be more profitable.</li>
</ul>

<p>Looking forward to having you there!</p>

<h4><a name="4"></a> Jobs at Autodesk</h4>

<p>Finally... Would you like to work with the Forge development team?</p>

<p>We hope you’re all staying safe and healthy in your homes.</p>

<p>In spite of the current situation, Autodesk is still actively hiring.</p>

<p>Forge continues to search for top talent as our global workforce works remotely.</p>

<p>Would you be a good fit? </p>

<p>Here are a few roles highlighted in April and May:</p>

<ul>
<li><a href="https://rolp.co/68d5i">20WD39053 &ndash; Senior Technical Writer, Developer Content &ndash; This role can be located anywhere</a></li>
<li><a href="https://rolp.co/IUdfi">20WD39597 &ndash; Senior Software Engineer, Cloud Service &ndash; Shanghai</a></li>
<li><a href="https://rolp.co/Pc5Li">19WD36553 &ndash; Principal Engineer &ndash; UK</a></li>
<li><a href="https://rolp.co/JWHti">20WD38369 &ndash; Software Architect &ndash; Singapore</a></li>
<li><a href="https://rolp.co/RUAfi">20WD39627 &ndash; Senior Vendor Manager &ndash; San Francisco</a></li>
<li><a href="https://rolp.co/Dmtei">20WD38632 &ndash; Engineering Manager &ndash; Portland</a></li>
<li><a href="https://rolp.co/SrG6i">20WD38618 &ndash; Software Development Manager &ndash; Novi</a></li>
</ul>

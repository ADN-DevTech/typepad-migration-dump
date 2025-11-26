---
layout: "post"
title: "More on Structural Analysis Code Checking"
date: "2013-06-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Cloud"
  - "RST"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/06/more-on-structural-analysis-code-checking.html "
typepad_basename: "more-on-structural-analysis-code-checking"
typepad_status: "Publish"
---

<p>I am busy be making the final preparations for my trip to Moscow tomorrow.
However, I also received some new input to share with you first on the

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/structural-analytical-code-checking-and-results-builder.html">
Structural Analysis SDK</a> that

I mentioned last week, introduced in the

<a href="http://images.autodesk.com/adsk/files/Revit2014SDK_RTM.exe">Revit 2014 SDK</a>.

<p>It enables engineering experts to provide specialised structural code checking applications on top of Revit, and now also supports storage of analysis results.</p>

<p><a href="http://adndevblog.typepad.com/aec/augusto-goncalves.html">Augusto Goncalves</a> recently took

a more in-depth

<a href="http://adndevblog.typepad.com/aec/2013/05/structural-analysis-sdk-first-look.html">
first look</a> at

it from a technical point of view.

<p>The results storage API and code checking framework requires installation of the

<a href="http://apps.exchange.autodesk.com/RVT/Detail/Index?id=appstore.exchange.autodesk.com%3astructuralanalysisandcodecheckingtoolkit2014%3aen">
Structural Analysis and Code Checking Toolkit</a> from the Autodesk Exchange Apps, discussed in more detail in this

<a href="http://bimandbeam.typepad.com/bim_beam/2013/04/structural-analysis-and-code-checking-toolkit-2014-on-autodesk-exchange-apps.html">
BIM and Beam blog post</a>.

<p>Here is a sequence of images illustrating the code checking process:</p>


<a name="2"></a>

<h4>Code Checking Process in Revit</h4>

<p>Analytical results in Revit</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba0109970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba0109970c image-full" alt="Analytical results in Revit" title="Analytical results in Revit" src="/assets/image_99503a.jpg" border="0" /></a><br />

</center>

<p>Physical and analytical model</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba018e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba018e970c image-full" alt="Physical and analytical model" title="Physical and analytical model" src="/assets/image_57f462.jpg" border="0" /></a><br />

</center>

<p>Analysis results:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba020d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba020d970c image-full" alt="Analysis results" title="Analysis results" src="/assets/image_514710.jpg" border="0" /></a><br />

</center>

<p>Step 1 &ndash; code parameter settings:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ab828742970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ab828742970d image-full" alt="Step 1 &ndash; code parameter settings" title="Step 1 &ndash; code parameter settings" src="/assets/image_ba4c4d.jpg" border="0" /></a><br />

</center>

<p>Step 2 &ndash; element type parameter settings:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba04a5970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba04a5970c image-full" alt="Step 2 &ndash; element type parameter settings" title="Step 2 &ndash; element type parameter settings" src="/assets/image_c8b3db.jpg" border="0" /></a><br />

</center>

<p>Step 3 &ndash; type parameter assignment to elements:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba0523970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba0523970c image-full" alt="Step 3 &ndash; type parameter assignment to elements" title="Step 3 &ndash; type parameter assignment to elements" src="/assets/image_e76533.jpg" border="0" /></a><br />

</center>

<p>Step 4 &ndash; execute and explore reports:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba05f3970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba05f3970c image-full" alt="Step 4 &ndash; execute and explore reports" title="Step 4 &ndash; execute and explore reports" src="/assets/image_2585d5.jpg" border="0" /></a><br />

</center>

<p>Step 5 &ndash; explore graphical results:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ab828a96970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ab828a96970d image-full" alt="Step 5 &ndash; explore graphical results" title="Step 5 &ndash; explore graphical results" src="/assets/image_9e51cb.jpg" border="0" /></a><br />

</center>

<p>Collective approach for a code checking solution:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba0706970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba0706970c image-full" alt="Collective approach for a code checking solution" title="Collective approach for a code checking solution" src="/assets/image_49e8ba.jpg" border="0" /></a><br />

</center>

<p>Exchange Apps:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ab828b58970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ab828b58970d image-full" alt="Exchange Apps" title="Exchange Apps" src="/assets/image_a9127c.jpg" border="0" /></a><br />

</center>

<p>Software Development Kit for Code Checking Documentation:</p>

<ul>
<li>Getting Started</li>
<li>Step by Step instructions</li>
<li>Samples</li>
<li>API documentation</li>
</ul>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019103ba07d1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019103ba07d1970c image-full" alt="Software Development Kit for Code Checking" title="Software Development Kit for Code Checking" src="/assets/image_79d34e.jpg" border="0" /></a><br />

</center>

<p>Visual Studio:</p>

<ul>
<li>Data management</li>
<li>UI definition</li>
<li>Interactions</li>
<li>Report generation</li>
<li>Localization support</li>
</ul>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ab828c0b970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ab828c0b970d image-full" alt="Visual Studio" title="Visual Studio" src="/assets/image_f24d9e.jpg" border="0" /></a><br />

</center>

<p>Source code:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330192ab828cd8970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330192ab828cd8970d image-full" alt="Source code" title="Source code" src="/assets/image_04588e.jpg" border="0" /></a><br />

</center>


<a name="3"></a>

<h4>Closing Notes</h4>

<p>As a last little note, Robot is now available as part of the cloud-based

<a href="http://usa.autodesk.com/adsk/servlet/pc/index?id=20414035&siteID=123112&mktvar001=509406&mktvar002=509406">
Autodesk SIM 360</a> simulation

package providing access to the simulation functionality from the regular Revit Structure user interface.</p>

<p>Now I really have to get back to the final touches on my

<a href="http://www.autodesk.ru/adsk/servlet/pc/index?id=21516340&siteID=871736">
Moscow Revit DevCamp</a>

presentation preparations...</p>

---
layout: "post"
title: "Revit 2022 Migrates BIM360 Team to Docs"
date: "2021-04-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "2022"
  - "BIM"
  - "Cloud"
  - "Migration"
  - "RevitLookup"
  - "Update"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-migrates-bim360-team-to-docs.html "
typepad_basename: "revit-2022-migrates-bim360-team-to-docs"
typepad_status: "Publish"
---

<p>We continue the foray into Revit 2022 enhancements with a real-world migration tool using the new <code>SaveAsCloudModel</code> functionality and the flat migration of RevitLookup:</p>

<ul>
<li><a href="#2">Save as cloud model from BIM360 Team to Docs</a></li>
<li><a href="#3">RevitLookup 2022</a></li>
<li><a href="#4">A librarian's take on corona</a></li>
</ul>

<h4><a name="2"></a> Save as Cloud Model from BIM360 Team to Docs</h4>

<p>My colleague Zhong Wu published an enhancement to the Revit 2022 SDK sample <em>CloudAPISample</em>
to <a href="https://forge.autodesk.com/blog/migrate-revit-worksharing-models-bim-360-team-bim-360-docs-powered-revit-2022-cloud">migrate Revit Worksharing models from BIM 360 Team to BIM 360 Docs &ndash; powered by Revit 2022 Cloud Worksharing API</a>.</p>

<p>In his own words:</p>

<p>Revit 2022 was officially released on April 8th, 2021
with <a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-released.html">a host of new features</a>.</p>

<p>Support for saving a Revit worksharing central model to the cloud is one important enhancement in the Revit 2022 API, using the method</p>

<pre class="code">
  Document.SaveAsCloudModel(Guid, Guid, String, String)
</pre>

<p>I enhanced it to support uploading a local workshared file into BIM 360 Design as a Revit Cloud Worksharing central model.</p>

<p>The Revit 2022 SDK also includes a sample add-in <em>CloudAPISample</em> demonstrating how to use this API.</p>

<p>I made some improvements to make it easy to use and demonstrate how to migrate your Revit cloud worksharing model from BIM 360 Team to BIM 360 Docs.</p>

<p>It includes the following features:</p>

<ul>
<li>Access all the contents within BIM 360 Team and Docs by logging in with your Autodesk Account</li>
<li>Download the Revit models from BIM 360 Team to a specified local folder</li>
<li>Select a target folder by navigating from BIM 360 Docs</li>
<li>Upload the Revit models from the local folder to the target folder on BIM 360 Docs</li>
<li>Reload the links to the correct model in the cloud</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdecb6ebf200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdecb6ebf200c image-full img-responsive" alt="Migrating from BIM360 Team to BIM360 Docs" title="Migrating from BIM360 Team to BIM360 Docs" src="/assets/image_c76851.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<p style="font-size: 80%; font-style:italic">Revit Cloud Worksharing Model Migration Sample from BIM 360 Team to BIM 360 Docs</p>

<p></center></p>

<p>The sample tool source code, full documentation and demo is hosted in
the <a href="https://github.com/JohnOnSoftware/forge-rcw.file.migration-revit.addon">forge-rcw.file.migration-revit.addon GitHub repository</a>.</p>

<p>Enjoy coding with Revit, Forge and BIM360, and please feel free to enhance the sample based on your needs.
Pull requests are always welcome.</p>

<p>Ever so many thanks to Zhong for implementing and sharing this useful and important utility!</p>

<h4><a name="3"></a> RevitLookup 2022</h4>

<p>I performed a quick flat migration of RevitLookup to the Revit 2022 API.</p>

<p>I just encountered one error and two warnings.</p>

<p>The error is caused by code checking the <code>DisplayUnitType</code>, which was deprecated in Revit 2021:</p>

<pre class="code">
<span style="color:gray;">#pragma</span>&nbsp;<span style="color:gray;">warning</span>&nbsp;<span style="color:gray;">disable</span>&nbsp;CS0618
&nbsp;&nbsp;<span style="color:green;">//&nbsp;warning&nbsp;CS0618:&nbsp;`DisplayUnitType`&nbsp;is&nbsp;obsolete:&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;This&nbsp;enumeration&nbsp;is&nbsp;deprecated&nbsp;in&nbsp;Revit&nbsp;2021&nbsp;and&nbsp;may&nbsp;be&nbsp;removed&nbsp;in&nbsp;a&nbsp;future&nbsp;version&nbsp;of&nbsp;Revit.&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Please&nbsp;use&nbsp;the&nbsp;`ForgeTypeId`&nbsp;class&nbsp;instead.&nbsp;</span>
&nbsp;&nbsp;<span style="color:green;">//&nbsp;Use&nbsp;constant&nbsp;members&nbsp;of&nbsp;the&nbsp;`UnitTypeId`&nbsp;class&nbsp;to&nbsp;replace&nbsp;uses&nbsp;of&nbsp;specific&nbsp;values&nbsp;of&nbsp;this&nbsp;enumeration.</span>

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;2&nbsp;==&nbsp;parameters.Length&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;ParameterInfo&nbsp;p1&nbsp;=&nbsp;parameters.First();
&nbsp;&nbsp;&nbsp;&nbsp;ParameterInfo&nbsp;p2&nbsp;=&nbsp;parameters.Last();
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;p1.ParameterType&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;Field&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&amp;&amp;&nbsp;(p2.ParameterType&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;DisplayUnitType&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;p2.ParameterType&nbsp;==&nbsp;<span style="color:blue;">typeof</span>(&nbsp;ForgeTypeId&nbsp;));
&nbsp;&nbsp;}
<span style="color:gray;">#pragma</span>&nbsp;<span style="color:gray;">warning</span>&nbsp;<span style="color:gray;">restore</span>&nbsp;CS0618
</pre>

<p>Since <code>DisplayUnitType</code> is obsolete in Revit 2022, we have no choice but to remove it.</p>

<p>The two warnings are related to the deprecated <code>ParameterType</code> and can be left for the moment.</p>

<p>Here is the complete <a href="https://thebuildingcoder.typepad.com/files/revit_2022_revitlookup_errors_warnings_0.txt">error and warning log so far</a>.</p>

<p>I wish you easy sailing and much success in your own migration work.</p>

<h4><a name="4"></a> A Librarian's Take on Corona</h4>

<p>A quite poetic librarian's recommendation to stay healthy:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdecb6eb6200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdecb6eb6200c image-full img-responsive" alt="A librarian's take on Corona" title="A librarian's take on Corona" src="/assets/image_181715.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

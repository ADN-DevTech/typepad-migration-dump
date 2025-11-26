---
layout: "post"
title: "RevitLookup Update, Fuslogvw and Override Joins"
date: "2021-05-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2022"
  - "Debugging"
  - "Geometry"
  - "Getting Started"
  - "Labs"
  - "Migration"
  - "RevitLookup"
  - "SDK Samples"
  - "Training"
  - "Update"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/05/revitlookup-update-fuslogvw-and-override-joins.html "
typepad_basename: "revitlookup-update-fuslogvw-and-override-joins"
typepad_status: "Publish"
---

<p>Here are a couple of the interesting topics that came up in the last couple of days:</p>

<ul>
<li><a href="#2">Numerous RevitLookup enhancements</a></li>
<li><a href="#3">Revit API Labs training material 2022</a></li>
<li><a href="#4">Visual Studio Revit add-in templates 2022</a></li>
<li><a href="#5">The SetGeometryCurve <code>overrideJoins</code> argument</a></li>
<li><a href="#6">Exploring assembly reference DLL hell with Fuslogvw</a></li>
</ul>

<h4><a name="2"></a> Numerous RevitLookup Enhancements</h4>

<p>Numerous contributions with
important <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> enhancements
were submitted since
the <a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-migrates-bim360-team-to-docs.html#3">flat migration of RevitLookup to the Revit 2022 API</a>:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.1">2022.0.0.1</a> &ndash; integrated pull request #74 by @peterhirn setting up CI for Revit 2022</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.2">2022.0.0.2</a> &ndash; integrated pull request #75 by @peterhirn to fix CI for Revit 2022 and non-dotnet-core project file</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.3">2022.0.0.3</a> &ndash; integrated pull request #73 implementing temporary transaction and rollback allowing to snoop PlanTopology, reset Revit API assembly DLL references to Copy Local false and specified Configuration as 2022</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.4">2022.0.0.4</a> &ndash; upgraded to Visual Studio 2019 (from 2017) and adopted @peterhirn project and solution files</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.5">2022.0.0.5</a> &ndash; integrated pull request #76 by @peterhirn to fix CI for new VS 2019 Revit 2022 dotnet-core csproj</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.6">2022.0.0.6</a> &ndash; integrated pull request #77 by @RevitArkitek to get end points for curves</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.7">2022.0.0.7</a> &ndash; integrated pull request #78 by @RevitArkitek to handle TableData.GetSectionData</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.8">2022.0.0.8</a> &ndash; integrated pull request #80 by @WspDev to remove deprecated ParameterType usage</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.0.9">2022.0.0.9</a> &ndash; integrated pull request #81 by @CADBIMDeveloper enhancing ElementId and Revit 2022 extensible storage support:
<ul>
<li>fix broken schema fields values display</li>
<li>SpecTypeId.Custom is not a measurable spec (it represents double values), but requires UnitTypeId.Custom to get an entity field value</li>
<li>this allows to view extensible storage schema map fields (dictionaries)</li>
<li>ElementId could represent Revit model element or built-in parameter id or built-in category id. for the latter two, show id value instead of "null"</li>
<li>now keyvaluepair is a truly snoopable object</li>
<li>remove unused using</li>
<li>show value if ElementId represents built-in parameters or built-in category</li>
</ul></li>
</ul>

<p>In the latter,
Alexander <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478">@aignatovich</a> <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a> Ignatovich, aka Александр Игнатович, adds:</p>

<p>I fixed RevitLookup handling of extensible storage.
The <code>Extensible storages</code> fields were broken:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278802ad08c200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278802ad08c200d image-full img-responsive" alt="Extensible storages fields" title="Extensible storages fields" src="/assets/image_6dbbc0.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Now RevitLookup also supports <code>Dictionary</code> <code>KeyValuePairs</code> lookup.
That is useful to view extensible storage entity data:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bded2e114200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bded2e114200c image-full img-responsive" alt="Dictionary KeyValuePairs lookup" title="Dictionary KeyValuePairs lookup" src="/assets/image_fb39fa.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Plus another small improvement: each <code>ElementId</code> could represent a model element id, a built-in parameter or a built-in-category.
For the latter two, it is much more useful to see an integer value instead of <code>null</code>.</p>

<p>Many thanks to all contributors for your great enhancements!</p>

<p>In case you, dear reader, would like to add your own to these as well, please fork the repository, clone to your local system, modify at will, commit, push the changes back to your fork and submit a pull request for them to be included into the main code stream.</p>

<p>You can easily track and analyse the changes to see how each individual enhancement above interacts with main project by clicking the <code>Compare</code> button to view the difference between
the <a href="https://github.com/jeremytammik/RevitLookup/releases">individual releases</a>.</p>

<h4><a name="3"></a> Revit API Labs Training Material 2022</h4>

<p>My colleague <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5661631">Naveen Kumar</a> <a href="https://github.com/NK29">@NK29 T</a> is
steadily building up his collection of solutions in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>.</p>

<p>He also migrated
the <a href="https://github.com/ADN-DevTech/RevitTrainingMaterial">Revit API Labs Training Material</a> to
Revit 2022.
The <a href="https://github.com/ADN-DevTech/RevitTrainingMaterial/releases/latest">latest release</a> is
currently <a href="https://github.com/ADN-DevTech/RevitTrainingMaterial/releases/tag/2022.0.0.2">2022.0.0.2</a>.</p>

<p>Many thanks to Naveen for his contributions!</p>

<p>The closely related <a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra">AdnRevitApiLabsXtra</a> have
yet to be migrated to Revit 2022... coming up anon...</p>

<h4><a name="4"></a> Visual Studio Revit Add-in Templates 2022</h4>

<p>Prompted by both Naveen and 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
asking for <a href="https://forums.autodesk.com/t5/revit-api-forum/visualstudiorevitaddinwizard-2022/m-p/10233833">VisualStudioRevitAddinWizard 2022</a>, 
I performed a quick flat migration of that tool to the new version last week.</p>

<p>The <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/latest">latest release</a> is
currently <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2022.0.0.0">2022.0.0.0</a>.</p>

<h4><a name="5"></a> The SetGeometryCurve OverrideJoins Argument</h4>

<p>Stefano Menci very kindly shared some useful research results in
his <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
explaining <a href="https://forums.autodesk.com/t5/revit-api-forum/what-does-the-overridejoins-parameter-do-in-curveelement/m-p/10298762">what the <code>overrideJoins</code> parameter does in <code>CurveElement.SetGeometryCurve</code></a>:</p>

<p>The <a href="https://www.revitapidocs.com/2021.1/9deec90c-4efc-9aa6-245d-061669022aa2.htm">Revit API documentation</a> contents
itself with the statement that the <code>overrideJoins</code> argument provides:</p>

<blockquote>
  <p>an option to specify whether or not existing joins will affect setting the geometry of the <code>CurveElement</code>.
  Setting this parameter to <code>false</code> is essentially the same as directly setting the <code>GeometryCurve</code> property.</p>
</blockquote>

<p>Stefano's research provides a little bit more detail, explaining:</p>

<p>I figured it out, so I will post the question with the answer anyway, for anybody interested (and for me, the next time I need it, because sometimes I'm a little forgetful):</p>

<ul>
<li>If set to true, it will set the new geometry for the curve element</li>
<li>If set to false, after setting the geometry, it will try to satisfy any constraints associated with the element</li>
</ul>

<p>The following code:</p>

<pre class="code">
&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Test&nbsp;1&quot;</span>&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;plane&nbsp;=&nbsp;Plane.CreateByNormalAndOrigin(&nbsp;XYZ.BasisZ,&nbsp;XYZ.Zero&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;sketchPlane&nbsp;=&nbsp;SketchPlane.Create(&nbsp;doc,&nbsp;plane&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;p1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;0,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;p2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;10,&nbsp;5,&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;p3&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;20,&nbsp;0,&nbsp;0&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;p2Higher&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;XYZ(&nbsp;10,&nbsp;10,&nbsp;0&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;line1&nbsp;=&nbsp;doc.Create.NewModelCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Line.CreateBound(&nbsp;p1,&nbsp;p2&nbsp;),&nbsp;sketchPlane&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;line2&nbsp;=&nbsp;doc.Create.NewModelCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Line.CreateBound(&nbsp;p2,&nbsp;p3&nbsp;),&nbsp;sketchPlane&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;line3&nbsp;=&nbsp;doc.Create.NewModelCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Line.CreateBound(&nbsp;p2Higher,&nbsp;p3&nbsp;),&nbsp;sketchPlane&nbsp;);

&nbsp;&nbsp;tx.Commit();

&nbsp;&nbsp;tx.Start(&nbsp;<span style="color:#a31515;">&quot;Test&nbsp;2&quot;</span>&nbsp;);

&nbsp;&nbsp;line1.SetGeometryCurve(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Line.CreateBound(&nbsp;p1,&nbsp;p2Higher&nbsp;),&nbsp;&lt;&nbsp;<span style="color:blue;">false</span>&nbsp;|&nbsp;<span style="color:blue;">false</span>&nbsp;&gt;);

&nbsp;&nbsp;tx.Commit();
</pre>

<p>Will return the following result:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278802ad0a1200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278802ad0a1200d img-responsive" alt="SetGeometryCurve overrideJoins" title="SetGeometryCurve overrideJoins" src="/assets/image_a67e51.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Many thanks to Stefano for the analysis and explanation.</p>

<h4><a name="6"></a> Exploring Assembly Reference DLL Hell with Fuslogvw</h4>

<p>Sean Page of <a href="https://rdgusa.com">RDG Planning &amp; Design</a> shares another useful hint in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/my-revit-app-can-t-find-sqlite-dll/m-p/10323105">my Revit app can't find SQLite.dll</a>, saying:</p>

<blockquote>
  <p>I ran into issues recently in 2022 related to references that previously worked.
  Turning on and using
  the <a href="https://docs.microsoft.com/en-us/dotnet/framework/tools/fuslogvw-exe-assembly-binding-log-viewer"><code>Fuslogvw.exe</code> assembly binding log viewer</a> was
  a substantial help.</p>
</blockquote>

<p>Thank you, Sean, for pointing this out!</p>

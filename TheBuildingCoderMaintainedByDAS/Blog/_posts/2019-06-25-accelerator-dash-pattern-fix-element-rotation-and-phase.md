---
layout: "post"
title: "Accelerator, Dash Pattern Fix, Rotation and Phase"
date: "2019-06-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Accelerator"
  - "Data Access"
  - "Forge"
  - "Material"
  - "Parameters"
  - "Travel"
  - "Update"
  - "WPF"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/06/accelerator-dash-pattern-fix-element-rotation-and-phase.html "
typepad_basename: "accelerator-dash-pattern-fix-element-rotation-and-phase"
typepad_status: "Publish"
---

<p>I am participating in the Forge Accelerator in Barcelona this week.</p>

<p>We are hosting a large number of participants, split up into separate manufacturing and AEC related groups:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4936935200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4936935200d image-full img-responsive" alt="Forge Accelerator in Barcelona" title="Forge Accelerator in Barcelona" src="/assets/image_074054.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I arrived good and early, spending the weekend visiting my sister, who moved here last summer:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4936889200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4936889200d image-full img-responsive" alt="Barcelona view" title="Barcelona view" src="/assets/image_4ff8ca.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>My brother came too, and we went climbing, right in the middle of town,
in <a href="http://www.rockclimbing.com/routes/Europe/Spain/Catalunya/Las_Foixardas">Las Foixardas</a> in
the <a href="https://en.wikipedia.org/wiki/Montju%C3%AFc">Parc Montju%C3%AFc</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4b81209200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4b81209200b image-full img-responsive" alt="El tunel de las Foixardas" title="El tunel de las Foixardas" src="/assets/image_5c4e7b.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Let me mention some pending Revit API issues before diving into any accelerator topics:</p>

<ul>
<li><a href="#3">AddMaterials enhancements</a></li>
<li><a href="#4">Retrieving element rotation</a></li>
<li><a href="#5">Changing the phase created parameter</a></li>
</ul>

<h4><a name="3"></a> AddMaterials Enhancements</h4>

<p>The <a href="https://github.com/jeremytammik/AddMaterials">AddMaterials add-in</a> originally
just added materials to the Revit model from a list of properties defined in Excel.</p>

<p>It has since been enhanced to also visualise them in WPF.</p>

<p>I recently added a couple of enhancements to the it that have not been mentioned here yet:</p>

<ul>
<li>Integrated <a href="https://github.com/jeremytammik/AddMaterials/pull/4">pull request #4 by @ridespirals to handle 0 or negative DashPatterns</a></li>
<li>Flat migration from Revit 2016 to Revit 2020 API</li>
<li>Acted on
the <a href="https://thebuildingcoder.typepad.com/blog/2014/04/wpf-fill-pattern-viewer-control-benchmark.html#comment-4497075532">suggestion by Александр Пекшев</a> to
replace <code>FillPattern</code> = <code>'{Binding CutPattern}'</code> with <code>FillPattern</code> = <code>'{Binding CutPattern, IsAsync=True}'</code> to speed up the drawing of thumbnails</li>
</ul>

<p>In the pull request, John <a href="https://github.com/ridespirals">@ridespirals</a> Varga points out an important hint to handle errors caused by real number imprecision in dash or hash pattern definitions:</p>

<blockquote>
  <p>We are using similar code in a project of ours, and we ran into an issue where some fill patterns had blank preview images.
  Some of the patterns had very small negative values (such as -5.9211894646675012E-16), and DashPatterns must be greater than 0.
  Using <code>float.Epsilon</code> produces previews that actually look correct.</p>
</blockquote>

<p>Example preview we got by filtering out segments that were &lt;= 0 (Third Bond - Emporer Brick - partial fix):</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a49368a2200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a49368a2200d img-responsive" style="width: 256px; display: block; margin-left: auto; margin-right: auto;" alt="Segments almost zero" title="Segments almost zero" src="/assets/image_6101bb.jpg" /></a><br /></p>

<p></center></p>

<p>Example using the fix in this pull request, using <code>float.Epsilon</code> (Third Bond - Emporer Brick - fixed):</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a46a2cec200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a46a2cec200c img-responsive" style="width: 256px; display: block; margin-left: auto; margin-right: auto;" alt="Segments fixed using float.Epsilon" title="Segments fixed using float.Epsilon" src="/assets/image_a7db39.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> Retrieving Element Rotation</h4>

<p><strong>Question:</strong> How can I retrieve the rotation angle of an element that has been rotated, e.g., using the rotation tool or
the <a href="https://www.revitapidocs.com/2020/3968f4e8-759c-f975-6c1f-7de42be633ed.htm"><code>RotateElement</code> method</a>?</p>

<p><strong>Answer:</strong> <code>RotateElement</code> takes an angle parameter, but that angle is not necessarily stored in an element.</p>

<p>It depends on how the element's location is stored.</p>

<p>For many elements, <code>Element.Location</code> can be cast to a <code>LocationPoint</code>, and its <code>Rotation</code> property read.</p>

<p>Elements that inherit from <code>Instance</code> inherit a <code>GetTransform</code> method whose result can be parsed to read the rotation.</p>

<p>For elements that don't align with either of these options, the rotation is more implicit with other element properties.</p>

<p>For instance, a wall that is driven by endpoints and a curve, after rotating, is still driven by endpoints and a curve, just in different locations.</p>

<h4><a name="5"></a> Changing the Phase Created Parameter</h4>

<p>From the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/changing-an-element-s-phase-created-parameter/m-p/8808894">changing an element's phase created parameter</a>
and <a href="https://forums.autodesk.com/t5/revit-api-forum/setting-an-elements-phase/m-p/6224664">setting an element's phase</a>:</p>

<p><strong>Question:</strong> How can I set the 'Phase Created' or the 'Phase Demolished' of an element?</p>

<p>I know I can retrieve the 'Phase Created' like this:</p>

<pre class="code">
&nbsp;&nbsp;phaseCreated&nbsp;=&nbsp;element.Document.GetElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;element.CreatedPhaseId&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">Phase</span>;
</pre>

<p>However, I can't figure out how to change it to set the phase.</p>

<p><strong>Answer:</strong> You can use two built-in parameters:</p>

<pre class="code">
&nbsp;&nbsp;element.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.PHASE_CREATED&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Set(&nbsp;phaseCrea​ted.Id&nbsp;);

&nbsp;&nbsp;element.get_Parameter(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BuiltInParameter</span>.PHASE_DEMOLISHED&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Set(&nbsp;phaseC​reated.Id&nbsp;);
</pre>

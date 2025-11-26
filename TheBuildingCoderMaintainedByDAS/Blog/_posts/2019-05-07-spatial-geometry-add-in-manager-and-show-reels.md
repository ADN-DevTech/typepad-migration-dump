---
layout: "post"
title: "Spatial Geometry, Add-In Manager and Show Reels"
date: "2019-05-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2020"
  - "Analysis"
  - "Debugging"
  - "Fun"
  - "Geometry"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/05/spatial-geometry-add-in-manager-and-show-reels.html "
typepad_basename: "spatial-geometry-add-in-manager-and-show-reels"
typepad_status: "Publish"
---

<p>New Autodesk show reels, a solution to the lack of an add-in manager in the Revit 2020 SDK, an update for the SpatialElementGeometryCalculator and an interesting observation on English spelling:</p>

<ul>
<li><a href="#2">2019 Autodesk show reels</a> </li>
<li><a href="#3">The Add-In Manager for Revit 2019 still works</a> </li>
<li><a href="#4">Spatial element geometry calculator update</a> </li>
<li><a href="#4.1">Håvard's new suggestion</a></li>
<li><a href="#5">English spelling</a> </li>
</ul>

<h4><a name="2"></a> 2019 Autodesk Show Reels</h4>

<p>Autodesk produces show reels highlighting exciting uses of the products in various domains.</p>

<p>The 2019 updates have now been released on YouTube and may come in handy to fill some time waiting for the main show to begin:</p>

<ul>
<li><a href="https://youtu.be/KWvPfmjwjOM">Corporate</a></li>
<li><a href="https://youtu.be/bUwbe7oIMxU">M&amp;E &ndash; Media and Entertainment </a></li>
<li><a href="https://youtu.be/361wG7e8lCg">MFG &ndash; Manufacturing </a></li>
<li><a href="https://youtu.be/Kuqg0OitrSc">AEC &ndash; Architecture Engineering and Construction</a></li>
</ul>

<h4><a name="3"></a> The Add-In Manager for Revit 2019 Still Works</h4>

<p>As several people already pointed out, the Add-In Manager used by many developers in their application testing workflow is missing in the Revit 2020 SDK.</p>

<p>A simple solution was now suggested and confirmed in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-2020-addin-manager-missing/m-p/8774075">Revit 2020 add-in manager missing</a>:</p>

<p><strong>Question:</strong> I downloaded the Revit 2020 SDK from the <a href="https://www.autodesk.com/developer-network/platform-technologies/revit">Revit development centre</a>.</p>

<p>I see that the package is missing the Add-In Manager.</p>

<p>Does anyone know where I can find it for Revit 2020?</p>

<p><strong>Answer:</strong> I am using the old version.</p>

<p>It can also be used on Revit 2020.</p>

<p>You can try it out yourself.</p>

<p>The installation path is the same: <em>C:\ProgramData\Autodesk\Revit\Addins\2020</em>.</p>

<p>Please notify if successful.</p>

<p><strong>Response:</strong> Thanks for the reply and pointing in the right direction.</p>

<p>I copied the <code>.dll</code> and <code>.addin</code> from the 2019.2 SDK into <em>C:\ProgramData\Autodesk\Revit\Addins\2020</em>, and all works as it should.</p>

<h4><a name="4"></a> Spatial Element Geometry Calculator Update</h4>

<p>Dan pointed out an inconsistency in
the <a href="https://github.com/jeremytammik/SpatialElementGeometryCalculator">SpatialElementGeometryCalculator</a>
in <a href="https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-4452599622">his comment</a>
on <a href="https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html">determining wall opening areas per room</a> that led me to migrate the add-in to Revit 2020 and integrate his code fix:</p>

<p>Dan: If I understand the sample model provided correctly, the room height is taken into account and not the full wall height.
So, in room 7, even though the wall is 4 m high, only 3 m will be taken into account, since that is the height of the room.
Fair enough; this would mean 30 m<sup>2</sup> gross for each wall, since they are all 10 m long inside the room.
However, for the wall with the opening in room 7, shouldn't the gross area be 30 m<sup>2</sup>, and the net 2 m<sup>2</sup> less (28 m<sup>2</sup>)?</p>

<p>If so, for the total output, I would expect these values:</p>

<pre>
Room 7; Wall3(308817): net 28; opening 2; gross 30
</pre>

<p>instead of</p>

<pre>
Room 7; Wall3(308817): net 30; opening 2; gross 32
</pre>

<p>I fixed the issue in the following line by subtracting the opening area, since <code>GetSubface().Area</code> apparently returns the gross area, not the net:</p>

<pre class="code">
  spatialData.dblNetArea&nbsp;=&nbsp;<span style="color:#2b91af;">Util</span>.sqFootToSquareM(
  &nbsp;&nbsp;spatialSubFace.GetSubface().Area&nbsp;-&nbsp;openingArea&nbsp;);&nbsp;
</pre>

<p>Many thanks to Dan for pointing this out!</p>

<p>It prompted me to migrate the sample from Revit 2016 to Revit 2020 and add his fix
in <a href="https://github.com/jeremytammik/SpatialElementGeometryCalculator/releases/tag/2020.0.0.2">SpatialElementGeometryCalculator release 2020.0.0.2</a>.</p>

<p>Here are the diffs:</p>

<ul>
<li><a href="https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2016.0.0.3...2020.0.0.0">Migration from Revit 2016 to Revit 2020 API</a></li>
<li><a href="https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2020.0.0.0...2020.0.0.1">Fixing the assembly architecture mismatch warning</a></li>
<li><a href="https://github.com/jeremytammik/SpatialElementGeometryCalculator/compare/2020.0.0.1...2020.0.0.2">Integrating Dan's code fix</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a45b6893200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a45b6893200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="SpatialElementGeometryCalculator test model 3D view" title="SpatialElementGeometryCalculator test model 3D view" src="/assets/image_52bf0c.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="4.1"></a> Håvard's New Suggestion</h4>

<p>Håvard Leding of <a href="https://www.symetri.com">Symetri</a> {whose last name used to be Dagsvik) very
kindly <a href="https://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html#comment-4454441990">answered Dan's comment</a>,
pointing out that the current implementation might possibly bear some fundamental improvement:</p>

<blockquote>
  <p>Hi Dan, seems you found a bug there... sorry about that :-) &nbsp;
  In our final app, the <code>openingArea</code> property is applied later on.
  I missed that here in the shortened version.
  Glad its still of use to someone.
  Doing this again today, I might use the <code>GetDependentElements</code> method more.
  First, get the openings like this:</p>
</blockquote>

<pre class="code">
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;Return&nbsp;wall&nbsp;openings&nbsp;using&nbsp;GetDependentElements</span>
&nbsp;&nbsp;<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
&nbsp;&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;&nbsp;GetOpenings(&nbsp;<span style="color:#2b91af;">Wall</span>&nbsp;wall&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>&nbsp;emcf
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementMulticategoryFilter</span>(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">List</span>&lt;<span style="color:#2b91af;">ElementId</span>&gt;()&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(<span style="color:#2b91af;">BuiltInCategory</span>.OST_Windows),
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">ElementId</span>(<span style="color:#2b91af;">BuiltInCategory</span>.OST_Doors)&nbsp;}&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;wall.GetDependentElements(&nbsp;emcf&nbsp;);
&nbsp;&nbsp;}
</pre>

<blockquote>
  <p>Then, for each dependent of interest, use <code>GetDependentElements</code> again to get the <code>openingSolid</code> as described here in the recent suggestion how
  to <a href="https://thebuildingcoder.typepad.com/blog/2019/03/determine-exact-opening-by-demolishing.html">determine exact opening by demolishing</a>.</p>
</blockquote>

<h4><a name="5"></a> English Spelling</h4>

<p>Let's finish off on a completely different topic, the baffling idiosyncrasies of English spelling.</p>

<p>The most extreme example I know is the suggestion that the English word 'fish' could just as well be spelled 'ghoti':</p>

<ul>
<li>f = gh from tough</li>
<li>i = o from women</li>
<li>sh = ti from satisfaction</li>
</ul>

<p>English spelling sure is pretty arbitrary sometimes...</p>

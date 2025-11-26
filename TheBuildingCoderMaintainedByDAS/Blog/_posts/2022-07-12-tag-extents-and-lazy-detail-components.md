---
layout: "post"
title: "Tag Extents and Lazy Detail Components"
date: "2022-07-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
  - "Open Source"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/07/tag-extents-and-lazy-detail-components.html "
typepad_basename: "tag-extents-and-lazy-detail-components"
typepad_status: "Publish"
---

<p>Today, let's highlight two really nice contributions from the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<ul>
<li><a href="#2">Determining tag extents</a>
<ul>
<li><a href="#2.2">Unrotate rotated tags</a></li>
</ul></li>
<li><a href="#3">One-click detail family generator</a></li>
</ul>

<p>First, though, a little aphorism to ponder:</p>

<p class="quote">Yesterday, I was clever and tried to change the world.
<br/>Today, I am wise and try to change myself.</p>

<p class="author">&ndash; Rumi</p>

<h4><a name="2"></a> Determining Tag Extents</h4>

<p>We repeatedly discussed how to ensure that tags do not overlap, both here and in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>,
e.g., in the threads
on <a href="https://forums.autodesk.com/t5/revit-api-forum/tags-without-overlapping/m-p/11275579">tags without overlapping</a>
and <a href="https://forums.autodesk.com/t5/revit-api-forum/auto-tagging-without-overlap/td-p/9996808">auto tagging without overlap</a>.</p>

<p>A hard-coded algorithm to achieve partial success was presented in the latter and reproduced 
in <a href="https://thebuildingcoder.typepad.com/blog/2021/02/splits-persona-collector-region-tag-modification.html#5">Python and Dynamo autotag without overlap</a>.
A more complete solution using a more advanced algorithm is now available commercially,
called <a href="https://bimlogiq.com/products/smart-annotataion">Smart Annotation</a> by <a href="https://bimlogiq.com">BIMLOGiQ</a>.</p>

<p>One prerequisite for achieving this task is determining the extents of a tag.</p>

<p><a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/9455666">AmitMetz</a> very
kindly shares sample code for a method to achieve this in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/tag-width-height-or-accurate-boundingbox-of-independenttag/m-p/11274095">tag width/height or accurate <code>BoundingBox</code> of <code>IndependentTag</code></a>.
Says he:</p>

<p>Following the helpful comments above, here is a method that returns tag dimensions.</p>

<p>A few comments on the implementation:</p>

<ul>
<li>First, we need to make sure the LeaderEndCondition is free in order to find the LeaderEndPoint.</li>
<li>Move the tag and it's elbow to LeaderEndPoint.</li>
<li>We get the correct BoundingBox only after moving the tag and it's elbow, and committing the Transaction.</li>
<li>I tried to use an unwrapped <code>transaction.rollback</code> without <code>TransactionGroup</code>, but it didn't work.
So, if we want to keep the tag in its original location, we have to commit the transaction and then roll back the transaction group.</li>
</ul>

<pre class="code">
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;Determine&nbsp;tag&nbsp;extents,&nbsp;width&nbsp;and&nbsp;height</span>
<span style="color:gray;">///</span><span style="color:green;">&nbsp;</span><span style="color:gray;">&lt;/</span><span style="color:gray;">summary</span><span style="color:gray;">&gt;</span>
<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;Tuple&lt;<span style="color:blue;">double</span>,&nbsp;<span style="color:blue;">double</span>&gt;&nbsp;<span style="color:#74531f;">GetTagExtents</span>(
&nbsp;&nbsp;IndependentTag&nbsp;<span style="color:#1f377f;">tag</span>)
{
&nbsp;&nbsp;Document&nbsp;<span style="color:#1f377f;">doc</span>&nbsp;=&nbsp;tag.Document;

&nbsp;&nbsp;<span style="color:green;">//Dimension&nbsp;to&nbsp;return</span>
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">tagWidth</span>;
&nbsp;&nbsp;<span style="color:blue;">double</span>&nbsp;<span style="color:#1f377f;">tagHeight</span>;

&nbsp;&nbsp;<span style="color:green;">//Tag&#39;s&nbsp;View&nbsp;and&nbsp;Element</span>
&nbsp;&nbsp;View&nbsp;<span style="color:#1f377f;">sec</span>&nbsp;=&nbsp;doc.GetElement(tag.OwnerViewId)&nbsp;<span style="color:blue;">as</span>&nbsp;View;
&nbsp;&nbsp;XYZ&nbsp;<span style="color:#1f377f;">rightDirection</span>&nbsp;=&nbsp;sec.RightDirection;
&nbsp;&nbsp;XYZ&nbsp;<span style="color:#1f377f;">upDirection</span>&nbsp;=&nbsp;sec.UpDirection;
&nbsp;&nbsp;Reference&nbsp;<span style="color:#1f377f;">pipeReference</span>&nbsp;=&nbsp;tag.GetTaggedReferences().First();
&nbsp;&nbsp;<span style="color:green;">//Reference&nbsp;pipeReference&nbsp;=&nbsp;tag.GetTaggedReference();&nbsp;//Older&nbsp;Revit&nbsp;Version</span>

&nbsp;&nbsp;<span style="color:blue;">using</span>&nbsp;(TransactionGroup&nbsp;<span style="color:#1f377f;">transG</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;TransactionGroup(doc))
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;transG.Start(<span style="color:#a31515;">&quot;Determine&nbsp;Tag&nbsp;Dimension&quot;</span>);

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">using</span>&nbsp;(Transaction&nbsp;<span style="color:#1f377f;">trans</span>&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;Transaction(doc))
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;trans.Start(<span style="color:#a31515;">&quot;Determine&nbsp;Tag&nbsp;Dimension&quot;</span>);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tag.LeaderEndCondition&nbsp;=&nbsp;LeaderEndCondition.Free;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XYZ&nbsp;<span style="color:#1f377f;">leaderEndPoint</span>&nbsp;=&nbsp;tag.GetLeaderEnd(pipeReference);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tag.TagHeadPosition&nbsp;=&nbsp;leaderEndPoint;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tag.SetLeaderElbow(pipeReference,&nbsp;leaderEndPoint);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;trans.Commit();
&nbsp;&nbsp;&nbsp;&nbsp;}

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//Tag&nbsp;Dimension</span>
&nbsp;&nbsp;&nbsp;&nbsp;BoundingBoxXYZ&nbsp;<span style="color:#1f377f;">tagBox</span>&nbsp;=&nbsp;tag.get_BoundingBox(sec);
&nbsp;&nbsp;&nbsp;&nbsp;tagWidth&nbsp;=&nbsp;(tagBox.Max&nbsp;-&nbsp;tagBox.Min).DotProduct(rightDirection);
&nbsp;&nbsp;&nbsp;&nbsp;tagHeight&nbsp;=&nbsp;(tagBox.Max&nbsp;-&nbsp;tagBox.Min).DotProduct(upDirection);

&nbsp;&nbsp;&nbsp;&nbsp;transG.RollBack();
&nbsp;&nbsp;}
&nbsp;&nbsp;<span style="color:#8f08c4;">return</span>&nbsp;Tuple.Create(tagWidth,&nbsp;tagHeight);
}
</pre>

<p>Many thanks to Amit for this nice implementation!</p>

<h4><a name="2.2"></a> Unrotate Rotated Tags</h4>

<p>Steven Micaletti, VDC Software &amp; Technology Developer added
a <a href="https://www.linkedin.com/feed/update/urn:li:activity:6952994276876169216?commentUrn=urn%3Ali%3Acomment%3A%28activity%3A6952994276876169216%2C6953115731991433216%29">comment on this method</a>, saying:</p>

<blockquote>
  <p>Good stuff; however, the GetTagExtents() implementation is missing a critical step &ndash; rotating the Pipe.
  The Pipe in the thread is at an angle, while the tag is not, and this is not generally how MEP elements are tagged.
  MEP tag families usually have the "rotate with component" option enabled, and in that scenario the bounding box returned is unusable.
  We must first disconnect and rotate the pipe to be model axis aligned before we set the TagHeadPosition, get the BoundingBox, and RollBack() the Transaction.</p>
</blockquote>

<p>Thank you, Steven, for pointing this out!</p>

<h4><a name="3"></a> One-Click Detail Family Generator</h4>

<p>Another nice solution and entire open source sample add-in is shared by
Peter <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/12564927">PitPaf</a> of <a href="https://www.zurawarchitekt.pl">Piotr Żuraw Architekt</a>
presenting <a href="https://forums.autodesk.com/t5/revit-api-forum/one-click-convert-detail-elements-to-detail-family/td-p/11230155">one click convert detail elements to detail family</a>:</p>

<blockquote>
  <p>I'm working on a Revit add-in to automate and simplify creation of detail Components families.</p>
  
  <p>This is helps create detail components on the fly just in model view.</p>
  
  <p>It allows the user to draw parts of a detail with lines and fill regions in model view and change it to a component without opening the family editor.</p>
  
  <p>Here I want to share with you the first version of this add-in, including source code and compiled install files:</p>
  
  <p style="text-align:center"><a href="https://github.com/PitPaf/LazyDetailComponent">github.com/PitPaf/LazyDetailComponent</a></p>
  
  <p>Feel free to use it if you find it interesting. I appreciate all your comments.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a2eecc986c200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a2eecc986c200d img-responsive" style="width: 438px; display: block; margin-left: auto; margin-right: auto;" alt="Lazy detail component" title="Lazy detail component" src="/assets/image_dc206c.jpg" /></a><br /></p>

<p></center></p>

<p>Many thanks to Peter for implementing, documenting and sharing this nice solution!</p>

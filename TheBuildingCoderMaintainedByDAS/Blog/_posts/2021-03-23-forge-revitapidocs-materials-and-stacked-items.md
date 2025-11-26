---
layout: "post"
title: "Forge, RevitApiDocs, Materials and Stacked Items"
date: "2021-03-23 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Docs"
  - "Forge"
  - "Material"
  - "News"
  - "Ribbon"
  - "Training"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/03/forge-revitapidocs-materials-and-stacked-items.html "
typepad_basename: "forge-revitapidocs-materials-and-stacked-items"
typepad_status: "Publish"
---

<p>Here are some exciting Forge, Revit API and other topics for today:</p>

<ul>
<li><a href="#2">Forge online training in April 2021</a></li>
<li><a href="#3">RevitApiDocs support for Revit 2021</a></li>
<li><a href="#4">Welcome, AEC BIM Tools</a></li>
<li><a href="#5">Visual materials API in Dynamo</a></li>
<li><a href="#6">24x24 stacked ribbon items</a></li>
<li><a href="#7">Innovative drone fly-through film</a></li>
</ul>

<h4><a name="2"></a> Forge Online Training in April 2021</h4>

<p>Are you interested in getting started with the Autodesk Forge platform development, perhaps in a more interactive, guided way?
Or maybe you already have experience with our platform, and are just interested in honing your skills?
If so, come and join us for another series of Forge Training webinars from April 13th until April 16th.
During these days, our dev advocates will guide your through the development of sample applications (using Node.js or .NET) leveraging different parts of Forge, and answer your questions along the way as you develop these applications yourself.
You can refer to Petr Broz's blog post for full details and register
either <a href="https://www.eventbrite.com/e/forge-online-training-april-13-16-2021-registration-145580133097">here</a> or
there:</p>

<p><center>
<a href="https://forge.autodesk.com/blog/forge-online-training-april-2021">Forge Online Training: April 2021</a>
</center></p>

<h4><a name="3"></a> RevitApiDocs Support for Revit 2021</h4>

<p>Back to the Revit API, Gui Talarico just announced an update to the online Revit API documentation for Revit 2021.1:</p>

<blockquote>
  <p><a href="https://apidocs.co">ApiDocs.co</a> was updated last month and <a href="https://www.revitapidocs.com">RevitApiDocs.com</a> last Friday!</p>
</blockquote>

<p>Very many thanks to Gui for the great news and all his work on this invaluable resource.</p>

<p>We hope to reduce the turn-around time for the next release &nbsp; :-)</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdec6086f200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdec6086f200c image-full img-responsive" alt="RevitApiDocs for Revit 2021" title="RevitApiDocs for Revit 2021" src="/assets/image_657585.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> Welcome, AEC BIM Tools</h4>

<p>Simon Jones was one of the first and foremost AEC oriented people at Autodesk for a couple of decades in the previous millennium.
Simon recently left Autodesk after over 35 years with us and launched <a href="https://www.aecbimtools.com">AEC BIM Tools</a>.
He now published his first own Revit add-in, a <a href="https://www.aecbimtools.com/sharedparameterinspector">Shared Parameter Inspector for Revit</a>.
You can download and test run a trial version.
If you find it useful, you can donate what you think it is worth to you.</p>

<p>Good luck and much success in your new adventure, Simon!</p>

<h4><a name="5"></a> Visual Materials API in Dynamo</h4>

<p>Konrad K Sobon published
a <a href="https://archi-lab.net/few-more-comments-about-materials-in-revit-dynamo-apis-etc">few more comments about materials in Revit, Dynamo, APIs etc</a>.</p>

<p>It explores the use of Dynamo to create materials in Revit from something like an Excel spreadsheet, presents several useful solutions and also points out weaknesses in the current API functionality.</p>

<p>The material appearance asset property mapping image at the end is especially useful and valuable:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdec60880200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdec60880200c image-full img-responsive" alt="Material appearance asset property mapping" title="Material appearance asset property mapping" src="/assets/image_319768.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="6"></a> 24x24 Stacked Ribbon Items</h4>

<p>Diving deeper into some practical coding, let's look at 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/24x24-stackeditems/m-p/10169950">24x24 StackedItems</a>:</p>

<p><strong>Question:</strong> This may be an easy one, but so far I am struggling to find anything specific about it.
How do you make a <code>StackedItem</code> where the icons are 24x24 when there are only 2 in the stack?
It seems like it should be possible as it is used multiple times in the modify tab, cf. this example:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdec6088b200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdec6088b200c img-responsive" style="width: 246px; display: block; margin-left: auto; margin-right: auto;" alt="2-stack 24x24 icons" title="2-stack 24x24 icons" src="/assets/image_4dd7bc.jpg" /></a><br /></p>

<p></center></p>

<p>I have been able to set the <code>ShowText</code> property to <code>false</code> to get the 3 stacked icons, but, when I use the same method with the 2 icon stack, it remains 16x16, regardless of the icon resolution.
I have tried to obtain and change the button's height and width, minWidth and minHeight through the Autodesk.Window.RibbonItem object to no avail.
Has anyone had any success in creating these icons?</p>

<p>Alexander <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/1257478">@aignatovich</a> <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a> Ignatovich, aka Александр Игнатович, presents an elegant solution with some very pretty code indeed:</p>

<p><strong>Answer:</strong> I succeeded &nbsp; :-) </p>

<p>With text:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e998b7c2200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e998b7c2200b img-responsive" style="width: 144px; display: block; margin-left: auto; margin-right: auto;" alt="2-stack 24x24 icons" title="2-stack 24x24 icons" src="/assets/image_8db03e.jpg" /></a><br /></p>

<p></center></p>

<p>Without text:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e998b7e3200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e998b7e3200b img-responsive" style="width: 86px; display: block; margin-left: auto; margin-right: auto;" alt="2-stack 24x24 icons" title="2-stack 24x24 icons" src="/assets/image_900873.jpg" /></a><br /></p>

<p></center></p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;revitRibbonItem&nbsp;=&nbsp;UIFramework.RevitRibbonControl
&nbsp;&nbsp;&nbsp;&nbsp;.RibbonControl.findRibbonItemById(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ribbonItem.GetId()&nbsp;);

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;useMediumIconSize&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;revitRibbonItem.Size&nbsp;=&nbsp;<span style="color:#2b91af;">RibbonItemSize</span>.Large;

&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;hideButtonCaption&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;revitRibbonItem.ShowText&nbsp;=&nbsp;<span style="color:blue;">false</span>;
</pre>

<p><code>GetId</code> is an extension method:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">internal</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">RibbonItemExtensions</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">static</span>&nbsp;<span style="color:blue;">string</span>&nbsp;GetId(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">this</span>&nbsp;<span style="color:#2b91af;">RibbonItem</span>&nbsp;ribbonItem&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;type&nbsp;=&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">RibbonItem</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;parentId&nbsp;=&nbsp;type
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetField(&nbsp;<span style="color:#a31515;">&quot;m_parentId&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BindingFlags</span>.Instance&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;<span style="color:#2b91af;">BindingFlags</span>.NonPublic&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;?.GetValue(&nbsp;ribbonItem&nbsp;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;??&nbsp;<span style="color:blue;">string</span>.Empty;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;generateIdMethod&nbsp;=&nbsp;type
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.GetMethod(&nbsp;<span style="color:#a31515;">&quot;generateId&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BindingFlags</span>.Static&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;<span style="color:#2b91af;">BindingFlags</span>.NonPublic&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;(<span style="color:blue;">string</span>)&nbsp;generateIdMethod?.Invoke(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ribbonItem,&nbsp;<span style="color:blue;">new</span>[]&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;parentId,&nbsp;ribbonItem.Name&nbsp;}&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>Many thanks to Alexander for the nice solution and the succinct, instructive and inspiring coding.</p>

<h4><a name="7"></a> Innovative Drone Fly-Through Film</h4>

<p>The one-and-a-half-minute drone video <a href="https://youtu.be/VgS54fqKxf0">Right Up Our Alley</a> may
help drive film making to new heights, lengths, curves and other experiences
&ndash; <a href="https://www.abc.net.au/news/2021-03-12/hollywood-drone-video-minnesota-bowling-alley/13241718">Hollywood bigwigs shower praise on creators of Minnesota bowling alley drone video</a>:</p>

<blockquote>
  <p>A single-take video shot with a drone flying through a Minnesota bowling alley has been hailed as "stupendous" by a string of celebrities and big-name film-makers.
  From there, the drone flies in and around bowlers in the lanes and drinkers at the bar, going in between legs and into the back compartment where the bowling pins are swept up and set up and all around &ndash; all in one shot.
  It finishes with something of a cliffhanger (SPOILER: No drones were seriously harmed in the making of the video).
  Key points:</p>
  
  <ul>
  <li>The video is all one-take</li>
  <li>It took about 10 to 12 attempts</li>
  <li>The only thing added in post-production is the audio</li>
  <li>The filmmakers are trying to encourage people to return to bars, restaurants and bowling alleys</li>
  <li>The near 90-second video titled <a href="https://youtu.be/VgS54fqKxf0">Right Up Our Alley</a> begins outside, the drone swoops in from across the street, through the doors, all around the bowling alley, bar, between people, next to the rolling bowling ball, ...</li>
  </ul>
</blockquote>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/VgS54fqKxf0" title="Right Up Our Alley" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

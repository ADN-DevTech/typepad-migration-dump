---
layout: "post"
title: "Splits: Persona, Collector, Region, Tag, Modification"
date: "2021-02-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "DMU"
  - "Dynamo"
  - "Filters"
  - "News"
  - "Python"
  - "RevitLookup"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/02/splits-persona-collector-region-tag-modification.html "
typepad_basename: "splits-persona-collector-region-tag-modification"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>I need to come to terms with the split personality recently foisted upon me.
Thank God, RevitLookup now handles split regions.</p>

<p>Meanwhile, lots more going on in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and
elsewhere in the world:</p>

<ul>
<li><a href="#2">Two Jeremys</a></li>
<li><a href="#3">Multiple collectors versus multiple filters</a></li>
<li><a href="#4">RevitLookup handles split region offsets</a></li>
<li><a href="#5">Python and Dynamo autotag without overlap</a></li>
<li><a href="#6">Custom errors and preventing changes</a></li>
<li><a href="#7">Ecological cost of crypto currency and art</a></li>
</ul>

<h4><a name="2"></a> Two Jeremys</h4>

<p>Apparently, the user account handling for
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
recently changed.</p>

<p>I can no longer log in with the non-standard Autodesk <code>jeremytammik</code> account that I have been using all these years; the login automatically switches that over to my standard Autodesk <code>jeremy.tammik</code> account with a dot <code>.</code> instead.</p>

<p>As a result, we now have two active Jeremys in the forum:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e98d8fa4200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e98d8fa4200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Two Jeremys" title="Two Jeremys" src="/assets/image_5fb3ee.jpg" /></a><br /></p>

<p></center></p>

<p>I guess the previous one will fade away as time goes on.</p>

<p>I wish I could meet him in person before he disappears.</p>

<h4><a name="3"></a> Multiple Collectors versus Multiple Filters</h4>

<p>This question arose repeatedly in the past few weeks, so let's reiterate it in detail, prompted by 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/multiple-collectors/m-p/10046666">multiple collectors</a></p>

<p><strong>Question:</strong> I noticed that if I create multiple collectors in the same script, they don't work properly and most likely end up empty.
I've tried to use <code>Dispose</code> before creating the second collector to see if it can sort of "reset" the collector, but I always get this error:</p>

<ul>
<li>Exception : Autodesk.Revit.Exceptions.InvalidObjectException: The managed object is not valid.</li>
</ul>

<p>What am I missing?</p>

<p>Here is a simple example where I collect all shared parameters in a project first, so I can use their GUIDs to collect data from them in families.</p>

<pre class="code">
  collector = FilteredElementCollector(doc)

  # Find GUID of desired shared parameters

  sharedPars = collector.OfClass(SharedParameterElement)

  # Collect data from families based on parameter GUID.

  families = collector.OfClass(FamilyInstance)
    .WhereElementIsViewIndependent()
</pre>

<p><strong>Answer:</strong> You are not in fact creating multiple collectors.</p>

<p>You are creating one single collector and applying multiple filters to that.</p>

<p>Applying several different filters to one single collector does exactly what it should:</p>

<p>Every single filter is applied to the collector results.</p>

<p>If the filters are mutually exclusive, you end up with an empty result.</p>

<p>For a previous explanation, please read the discussion
on <a href="https://thebuildingcoder.typepad.com/blog/2019/11/design-automation-api-stacks-collectors-and-links.html#4">reinitialising the filtered element collector</a>.</p>

<p>The same question also came up in a few other recent threads, e.g.,
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-extract-the-geometry-and-the-texts-of-the-title-block-in/m-p/9943738">how to extract the geometry and the texts of the title block in a sheet view</a>,
summarised in the blog post 
on <a href="https://thebuildingcoder.typepad.com/blog/2021/01/sheet-view-xform-coords-img-export-and-title-block.html#2">extracting title block geometry and text</a>.</p>

<p>In your sample code snippet, simply create two separate collectors for shared parameters and family instances.</p>

<p><strong>Response:</strong> I appreciate the reply and support.</p>

<p>I definitely understand what I did wrong now; I can't reuse the same collector variable as the filters just pile up, which obviously causes the collector to be empty, so simple.</p>

<p>It does worry me that after so much research I couldn't find the answer anywhere.</p>

<p><strong>Answer:</strong> Thank you for your appreciation. Happy to hear that the problem is solved and the solution clear and simple.</p>

<p>I'll spell it out in the blog again and hope that will be easier to find in case anyone runs into this again in the future.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e98d1a87200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e98d1a87200b img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Rain water collector" title="Rain water collector" src="/assets/image_201954.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="4"></a> RevitLookup Handles Split Region Offsets</h4>

<p>Thanks to Michael <a href="https://github.com/RevitArkitek">@RevitArkitek</a> Coffey, RevitLookup can now handle split region offsets.</p>

<p>He submitted the <a href="https://github.com/jeremytammik/RevitLookup/issues/68">issue #68 &ndash; split region offsets (2021)</a> and a
subsequent <a href="https://github.com/jeremytammik/RevitLookup/pull/69">pull request &ndash; adds handler for <code>GetSplitRegionOffsets</code></a>,
explaining:</p>

<blockquote>
  <p>The <code>ViewCropRegionShapeManager</code> method <code>GetSplitRegionOffset</code> was added in 2021.
  This returns an <code>XYZ</code> but requires an integer index parameter.
  A list of XYZs can be returned, named by the index that was used.</p>
</blockquote>

<p>This enhancement is captured
in <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.12">RevitLookup release 2021.0.0.12</a>.</p>

<p>Many thanks to Michael for implementing and sharing this!</p>

<h4><a name="5"></a> Python and Dynamo Autotag Without Overlap</h4>

<p>Christopher Kepner shared a nice brute force Python solution
implementing <a href="https://forums.autodesk.com/t5/revit-api-forum/auto-tagging-without-overlap/m-p/10036344">Auto Tagging without overlap</a>,
explaining the algorithm like this:</p>

<p>A python script to auto-tag all doors and place them without clashing with other tags or doors, using a custom smart tag that is much bigger than typical door tag.</p>

<p>It starts with a list of doors in the variable <code>doorFiltered</code>. </p>

<p>The location point of the first door in the list is fed into the function below to provide a test point to see if it overlaps any location points in the list of doors:</p>

<pre class="prettyprint">
  def move_right(x,y,z):
    n = scaleFactor
    return x+n, y, z

  def move_down(x,y,z):
    n = scaleFactor
    return x,y-n,z

  def move_left(x,y,z):
    n = scaleFactor
    return x-n,y,z

  def move_up(x,y,z):
    n = scaleFactor
    return x,y+n,z

  moves = [move_right, move_down, move_left, move_up]

  def shift(end, point):
    from itertools import cycle
    _moves = cycle(moves)
    n = 1
    pos = point
    times_to_move = 1

    yield pos

    while True:
      for _ in range(2):
        move = next(_moves)
        for _ in range(times_to_move):
          if n >= end:
            return
          pos = move(*pos)
          n+=1
          yield pos

      times_to_move+=1
</pre>

<p>If the point lands too close to any door locations in the list, the code adds a integer to the function and runs again to provide the next test point. Each time the function is re-run, the next point follows a spiral pattern from the origin (location point of the first door):</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e98d1a7c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e98d1a7c200b img-responsive" style="width: 200px; display: block; margin-left: auto; margin-right: auto;" alt="Autotagging spiral" title="Autotagging spiral" src="/assets/image_8d347d.jpg" /></a><br /></p>

<p></center></p>

<p>Once a point is found that is far enough from the list of door locations, a tag is placed and the tag location is added to the list of door locations.</p>

<p>The process loops to the next door, checking against the list of door location plus the new tag location.</p>

<p>It's a working concept, but the output is inconsistent.</p>

<p>Issues include:</p>

<ul>
<li>Tags occasionally overlap with each other.</li>
<li>The process takes a while. there's tons of points it tests that fail.</li>
<li>Tag location it finds does not work well with leaders. the tags land in every direction from the door creating overlap of leaders. It might work better with smaller tags.</li>
</ul>

<p><strong>Answer:</strong> LOL. If you make the tags small enough, the problem will disappear entirely, along with the tags.</p>

<p>Thank you very much for the explanation. Brute force and effective, given time. I love that straightforward approach!</p>

<p>Another recent tagging conversation
on <a href="https://forums.autodesk.com/t5/revit-api-forum/tags-without-overlapping/m-p/7750631">tags without overlapping</a>
mentions a couple of other useful possibilities.</p>

<p>More complex approaches are discussed on the Internet under the
term '<a href="https://duckduckgo.com/?q=map+labelling+algorithm">map labelling algorithms</a>'.</p>

<p>Finally, Konrad Sobon of <a href="https://archi-lab.net">archi+lab</a>
discussed <a href="https://archi-lab.net/element-tagging-with-dynamo">element tagging with dynamo</a>
to create roof plans for a glass canopy system and tag each panel with its unique <code>Mark</code> value:</p>

<blockquote>
  <p>basically, it's a Revit’s Tag All tool, but with extra control over where the tag actually gets placed.</p>
</blockquote>

<h4><a name="6"></a> Custom Errors and Preventing Changes</h4>

<p>Harry Mattison presents a nice solution
implementing <a href="https://boostyourbim.wordpress.com/2021/01/28/custom-errors-preventing-specific-changes-to-the-revit-model">custom errors &ndash; preventing specific changes to the Revit model</a>,
explaining:</p>

<blockquote>
  <p>Let's say there is a specific list of View Scales that you want allowed in your Revit projects.
  Or certain naming conventions that should be used.
  Or something else like that where you'd like to automate the process of checking a user's change and determining if it should be allowed, prevented, or trigger a warning.</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e98d1a74200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e98d1a74200b image-full img-responsive" alt="Custom error" title="Custom error" src="/assets/image_6a8036.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<blockquote>
  <p>This can be achieved with two pieces of Revit API functionality &ndash; Updater and Custom Failures...</p>
</blockquote>

<p>Many thanks to Harry for sharing this nice explanation and implementation!</p>

<h4><a name="7"></a> Ecological Cost of Crypto Currency and Art</h4>

<p>I was intrigued and astounded at some of the information shared by Memo Akten in the analysis
of <a href="https://memoakten.medium.com/the-unreasonable-ecological-cost-of-cryptoart-2221d3eb2053">the unreasonable ecological cost of #CryptoArt</a>,
and crypto-currencies as well.</p>

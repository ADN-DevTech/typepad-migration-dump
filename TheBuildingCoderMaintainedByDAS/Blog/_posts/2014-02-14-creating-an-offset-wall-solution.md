---
layout: "post"
title: "Creating an Offset Wall Solution"
date: "2014-02-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Parameters"
  - "Photo"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/02/creating-an-offset-wall-solution.html "
typepad_basename: "creating-an-offset-wall-solution"
typepad_status: "Publish"
---

<p>Happy

<a href="http://en.wikipedia.org/wiki/Valentine%27s_Day">
St. Valentine's Day</a>!</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a5116c7096970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a5116c7096970c img-responsive" style="width: 430px; " alt="A heart of hands" title="A heart of hands" src="/assets/image_a2f5b2.jpg" /></a><br />

</center>

<p>As we all know and have known for a long time from the exploration of the

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-compound-layers.html">
wall compound layers</a>,

the Revit API wall location line is at the centre of the wall.</p>

<p>There is no way to change that, and it is important to note that the API location line is completely separate from the one whose location can be controlled through the user interface, and also through the built-in parameter WALL_KEY_REF_PARAM, but only after the wall has been created.</p>

<p>When creating a new wall, the location line you specify is always the wall centre line.</p>

<p>This has sometimes been an issue for people wishing to programmatically

<a href="http://thebuildingcoder.typepad.com/blog/2013/09/creating-an-offset-wall.html">
create an offset wall</a> along

an edge, e.g. on top of a given slab, so that the wall finish coincides with the slab edge.</p>

<p>Well, the best solutions are always the simplest, and now Simon Moreau seems to have come up with the long-awaited ultimate one for this situation in his

<a href="http://thebuildingcoder.typepad.com/blog/2013/09/creating-an-offset-wall.html?cid=6a00e553e16897883301a73d7387f4970d#comment-6a00e553e16897883301a73d7387f4970d">
comment</a> on

the topic pointing out how he solves this to

<a href="http://bim42.com/2014/02/09/modellingskirtingboards">model skirting boards</a>:</p>

<p>"I just found a workaround for creating walls with the location line set to 'Finish Face' (for example) on creation.
I just first create a wall two times thicker, change the location line position, and finally change my wall type to its final thickness."</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73d77c376970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73d77c376970d img-responsive" alt="Wall creation workaround" title="Wall creation workaround" src="/assets/image_7fe25e.jpg" /></a><br />

</center>

<p>Many thanks to Simon for his clever simple idea and sharing this thought!</p>

<p>I bet some readers wish they had thought of this themselves when needing it...</p>

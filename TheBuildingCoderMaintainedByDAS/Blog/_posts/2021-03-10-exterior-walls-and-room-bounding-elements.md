---
layout: "post"
title: "Exterior Walls and Room Bounding Elements"
date: "2021-03-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Analysis"
  - "Element Relationships"
  - "Filters"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/03/exterior-walls-and-room-bounding-elements.html "
typepad_basename: "exterior-walls-and-room-bounding-elements"
typepad_status: "Publish"
---

<p>Let's look at the outer boundaries of both buildings and rooms today;
note that both of these topics are continued in the subsequent follow-up post
on <a href="https://thebuildingcoder.typepad.com/blog/2021/03/boundary-elements-and-stirrup-constraints.html">boundary elements</a>:</p>

<ul>
<li><a href="#2">Finding exterior walls continued</a></li>
<li><a href="#3">Retrieving room bounding elements</a></li>
<li><a href="#4">Comic Sans is a public good</a></li>
</ul>

<h4><a name="2"></a> Finding Exterior Walls Continued</h4>

<p>We discussed some approaches
to <a href="https://thebuildingcoder.typepad.com/blog/2018/05/drive-revit-via-a-wcf-service-wall-directions-and-parameters.html#8">retrieve all exterior walls</a> three years ago using different approaches:</p>

<ul>
<li>The built-in wall function parameter <code>FUNCTION_PARAM</code>, tested by the <code>IsExterior</code> API method</li>
<li>Using the <code>BuildingEnvelopeAnalyzer</code> class</li>
<li>Placing room separation lines outside the building envelope and creating a huge room around the entire building</li>
</ul>

<p>Further aspects were added a week later in a second round
on <a href="https://thebuildingcoder.typepad.com/blog/2018/05/filterrule-use-and-retrieving-exterior-walls.html#2">retrieving all exterior walls revisited</a>.</p>

<p>The discussion now continued between Александр Пекшев or Alexander Pekshev of <a href="https://modplus.org/en">ModPlus</a> and
Lucas Moreira in a series of <a href="https://thebuildingcoder.typepad.com/blog/2018/05/filterrule-use-and-retrieving-exterior-walls.html#comment-5289806219">comments</a>
on that post:</p>

<p>Александр: There is one simple enough algorithm that allows you to find the exterior walls, even in an open loop.
The algorithm consists of two parts:</p>

<ol>
<li>The main part &ndash; 
From the centre of the LocationCurve of the wall, two perpendicular rays (long lines) are shot on either side of the LocationCurve.
Determine the number of intersections of these rays with other walls.
If the number of intersections on one side is zero, this is an outer wall.</li>
<li>An additional part &ndash; 
The first part will not find all the walls, so the second pass should check the remaining walls at the intersections with the found exterior walls: if the wall with its ends connects to the two outer walls, it means it is also external</li>
</ol>

<p>The working capacity of this algorithm is almost ideal.</p>

<p>Here are the results of my algorithm:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdec2f757200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdec2f757200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Exterior walls" title="Exterior walls" src="/assets/image_3ede5d.jpg" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e995a901200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e995a901200b img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Exterior walls" title="Exterior walls" src="/assets/image_fee1b2.jpg" /></a><br />
</center></p>

<p>I describe the algorithm in more detail in my article
on <a href="https://blog.modplus.org/index.php/11-revitapi/10-revit-find-external-walls-algorithm">Revit: Exterior Wall Search Algorithm</a>.</p>

<p>The code is provided in
the <a href="https://github.com/Pekshev/RevitFindExteriorWalls">RevitFindExteriorWalls GitHub repository</a>.</p>

<p>Lucas: how do you solve the problem of having an interior wall that touches 2 exterior walls?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e995a90c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e995a90c200b img-responsive" style="width: 600px; display: block; margin-left: auto; margin-right: auto;" alt="Exterior walls" title="Exterior walls" src="/assets/image_1c4894.jpg" /></a><br /></p>

<p></center></p>

<p>Александр: Today, I figured out the problem in your example.
In this case, it is necessary to improve the second part of the check and introduce some additional checks.
There will always be special cases.
The screenshot shows that three walls are connected there in one place &ndash; it seems to me that this needs to be taken into account.</p>

<p>In the 'main part' of the algorithm, if there are intersections on both sides of the wall, then it is internal.
And the connections at the ends of the wall are no longer important.</p>

<p>Lucas: Thanks for your response.
What I did is use 3 rays at different angles from each side: 45, 90 and 135 degrees.
If at least one of the rays doesn't hit anything, it's an exterior wall.
This took care of the special cases I had so far.
I believe shallower angles might be best.
So, maybe something like (5, 90, 175) will probably yield better, more correct results.
Another thing is I am not levelling the location lines.
Meaning that walls have to be on the same level to intersect each other.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278801ae278200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278801ae278200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Exterior walls" title="Exterior walls" src="/assets/image_eb061e.jpg" /></a><br /></p>

<p></center></p>

<p>All blue walls are exterior, and the garage in the top right is on a different level of the rest.</p>

<p>Another approach that worked is using a convex hull to find at least one exterior wall (any location line that intersects the hull is an outermost wall).
Then, from the selected exterior wall, move counterclockwise, always trying to find a right turn; this is a bit slower, but needs no special cases to work with all the projects I've tested.</p>

<p>Александр: Convex hull is suitable if the outer walls form a precise chain of connections.
But it is not always the case.
In addition, there it will be necessary to collect these very connections, which is also not an easy task (there are many nuances).</p>

<p>While writing the message, another idea came to mind:</p>

<ol>
<li>Determine the overall contour of all selected walls and find the midpoint</li>
<li>Around the contour with an indent from the contour, throw the rays towards the midpoint. Throw the rays through a certain step.</li>
</ol>

<p>The first wall that intersects with the ray is the outer one.</p>

<p>Interesting idea! Need to try it out.</p>

<p>So, it is necessary to clarify that this is not a 100% solution and some special cases turn out to be expensive.</p>

<p>Many thanks to Alexander and Lucas for the interesting discussion.</p>

<h4><a name="3"></a> Retrieving Room Bounding Elements</h4>

<p>Moving inwards from the exterior walls into the building interior, I summarise the interesting discussion between
Samuel Arsenault-Brassard and Yien Chao, Architect, BIM Director and Computational BIM Manager
at <a href="https://www.msdl.ca">MSDL architectes</a> on how
to <a href="https://forums.autodesk.com/t5/revit-api-forum/get-the-walls-ceiling-and-floor-of-a-room/m-p/9915923">get the walls, ceiling and floor of a room</a>:</p>

<p><strong>Question:</strong> I've been able to get a list of all the elements that are in a room, but I am not able to figure out how to automatically obtain the walls, ceiling and floor that are associated with these rooms.</p>

<p>Is this information possible through the API?</p>

<p>And yes, I do understand that a wall, floor and ceiling may have a relationship with multiple rooms, not just one.</p>

<p><strong>Answer:</strong> Try
the <a href="https://www.revitapidocs.com/2020/1fbe1cff-ed94-4815-564b-05fd9e8f61fe.htm">BoundingBoxIntersectsFilter</a>, maybe?</p>

<p>A simple bounding box filter and a multi-category filter... and voila!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e995a916200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e995a916200b image-full img-responsive" alt="Room bounding elements code" title="Room bounding elements code" src="/assets/image_3cf4aa.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Response:</strong> One last question, is the bounding box actually a square box?
I am wondering if it will capture rogue elements if the room is not square, for example a serpentine corridor.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278801ae28a200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278801ae28a200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Room bounding elements bb" title="Room bounding elements bb" src="/assets/image_4aca93.jpg" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> The <a href="https://www.revitapidocs.com/2020/3c452286-57b1-40e2-2795-c90bff1fcec2.htm"><code>BoundingBoxXYZ</code> class</a>
is a three-dimensional rectangular box at an arbitrary location and orientation within the Revit model.</p>

<p>However, the bounding box returned by the BIM element property is always a rectangular box, or, more precisely, a rectangular cuboid, with X, Y and Z axis-aligned faces.</p>

<p><strong>Response:</strong> So, it would create a problem for a non-rectangular room right? (trying to detect related walls, floors and ceilings of a room).</p>

<p><strong>Answer:</strong> I would try different approach for that.
You could try to use the <a href="https://www.revitapidocs.com/2020/96e29ddf-d6dc-0c40-b036-035c5001b996.htm"><code>IsPointInRoom</code> method</a> instead? </p>

<p>For ceiling, wall and floor, you can just take the centre points and project the points to the room.</p>

<p><strong>Response:</strong> You say, 'project the points to room.'
I'm not sure I understand this part.
I guess you can draw a line from the centroid of the room to the centroid of the walls?</p>

<p>The inherent problem I see is that the centroid of each wall/floor/ceiling is going to be outside each room since it is its shell. It's almost like we need to offset each room's volume to encompass the centroids of its shells.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278801ae291200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278801ae291200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Wall middle" title="Wall middle" src="/assets/image_93295a.jpg" /></a><br /></p>

<p></center></p>

<p>Typo:</p>

<ul>
<li>blue line = extent of <em>room</em></li>
</ul>

<p><strong>Answer:</strong> Example: choose centre face of wall, then project the point according to normal by .
Then, use the <code>IsPointInRoom</code> method for each point; you should have 2 rooms for a single wall.
It is easier with ceilings and floor finishes.</p>

<p><strong>Response:</strong> Interesting.</p>

<p>I can imagine lots of problems with this approach; for instance, a corridor that borders 20 rooms will only detect one room on each side.</p>

<p>Same with a floor or ceiling being shared by multiple rooms.
Especially if the designers were lazy and modelled the floors/ceilings to go through walls.</p>

<p>It's a very interesting problem, I will keep pondering it.
I'm actually surprised there's no direct way to do this directly in the API or in Revit schedules.
Feels like every walls should know what rooms accost them and all rooms should know what surfaces bound them.</p>

<p><strong>Answer:</strong> I think you can start another thread on that particular topic.</p>

<p>In the meantime, I think the first question has been resolved.</p>

<p>Many thanks to Samuel and Yien for the interesting discussion, and to Yien for all his good suggestions!</p>

<h4><a name="4"></a> Comic Sans is a Public Good</h4>

<p>Moving away for the Revit API for a moment, I am a bit of a typography junkie and pay far too much fanatic attention to that aspect of a text.</p>

<p>I sometimes even feel compelled to reformat a text more nicely to suit my taste just to make it more readable before I even start to take it in.</p>

<p>Until now, I have always gone for pretty traditional fonts and avoided Comic Sans.</p>

<p>I was interested and surprised to learn that there are good reasons not to do so, though, reading
about <a href="https://www.thecut.com/2020/08/the-reason-comic-sans-is-a-public-good.html">the reason Comic Sans is a public good</a>.</p>

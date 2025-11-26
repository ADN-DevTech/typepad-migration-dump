---
layout: "post"
title: "API Access to Family Instance References"
date: "2017-05-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2018"
  - "Data Access"
  - "Family"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/05/api-access-to-family-instance-references.html "
typepad_basename: "api-access-to-family-instance-references"
typepad_status: "Publish"
---

<p>Here is another Revit 2018 API enhancement that is obviously worth highlighting, since it immediately answers the question
on <a href="https://forums.autodesk.com/t5/revit-api-forum/get-specific-reference-from-family/m-p/7085619">getting a specific reference from a family instance</a> raised in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<h4><a name="2"></a>Question</h4>

<p>Is it possible to get a named reference plane from a family without going into the family editor API?</p>

<p>For example, I would like to have users be able to name a reference plane '<em>my arbitrary location</em>' when they are creating a family in the family editor.</p>

<p>Then, when it's loaded in the project, I would like to find an instance of it, be able to find the '<em>my arbitrary location</em>' in the project, and use that location data for other things in relation to the placement in the project.</p>

<p>I've seen the post 
on <a href="http://thebuildingcoder.typepad.com/blog/2015/05/how-to-retrieve-dimensioning-references.html">how to retrieve dimensioning references</a>, 
but that basically says you would have to use some type of geometry analysis to figure out if it's the correct one or not.</p>

<p>Unfortunately, it's user (or at least company) specific and varied so there is no way to identify it geometrically, but it would be easy for the user to label the reference plane with a name if I could get that data out...</p>

<h4><a name="3"></a>Answer</h4>

<p>You are in luck.</p>

<p>A similar question was asked in a previous case, 12730663 <em>Create dimension for detail items in drafting view</em>:</p>

<p>[Q] I want to dimension the detail item in drafting view. But the geometry of the family instance only can be retrieved by GetOriginalGeometry method, and there is any reference can be got. How can I add the dimension to detail items by API?</p>

<p>[A] For Revit 2018, we have new capabilities to get the standard references for family instances. I haven’t tested specifically with detail items, but I believe this should work for them as well:</p>

<p>Check out the
section <a href="http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html#3.19">API access to <code>FamilyInstance</code> references</a>
in <a href="http://thebuildingcoder.typepad.com/blog/2017/04/whats-new-in-the-revit-2018-api.html">What's New in the Revit 2018 API</a>:</p>

<blockquote>
  <p>The following new methods have been added to enable easy access to FamilyInstance references that correspond to reference planes and reference lines in the family. Some use the options in the new enumeration FamilyInstanceReferenceType as input to identify "Strong" or "Weak" references or specific positional references in each of the 3 coordinate directions (as determined by the possible values of parameter "Is Reference" of reference planes and parameter "Reference" of reference lines in families).</p>
</blockquote>

<ul>
<li>FamilyInstance.GetReferences()</li>
<li>FamilyInstance.GetReferenceByName()</li>
<li>FamilyInstance.GetReferenceType()</li>
<li>FamilyInstance.GetReferenceName()</li>
</ul>

<p>Prior to Revit 2018, I don’t believe there is a technique to get at the available references.</p>

<p>This also resolves the older case 11909355 <em>Parametric family instance placement</em> and the wish list item CF-1271 <em>API wish: need a way to relate view specific geometry in project with particular ref plane in family</em> that it gave rise to, which has now been closed as fixed.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8f932b7970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8f932b7970b img-responsive" style="width: 400px; " alt="Family instance reference plane" title="Family instance reference plane" src="/assets/image_7749a0.jpg" /></a><br /></p>

<p></center></p>

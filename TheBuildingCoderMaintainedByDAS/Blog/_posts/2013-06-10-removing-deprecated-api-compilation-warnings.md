---
layout: "post"
title: "Removing Deprecated API Compilation Warnings"
date: "2013-06-10 04:00:00"
author: "Jeremy Tammik"
categories:
  - "2014"
  - "Filters"
  - "Migration"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/06/removing-deprecated-api-compilation-warnings.html "
typepad_basename: "removing-deprecated-api-compilation-warnings"
typepad_status: "Publish"
---

<p>I completed

<a href="http://thebuildingcoder.typepad.com/blog/2013/04/migrating-the-building-coder-samples-to-revit-2014.html">
The Building Coder sample migration to Revit 2014</a> back

in April, but it was still generating

<span class="asset  asset-generic at-xid-6a00e553e16897883301901d30c6d8970b"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2014_d.txt">111 warnings</a></span> due

to use of obsolete API calls.

<p>After

<a href="http://thebuildingcoder.typepad.com/blog/2013/06/migrating-the-adn-training-labs-to-revit-2014.html">
migrating the ADN training lab material</a> last

week, I thought I could now turn my attention back to resolving these warnings.</p>

<p>I replaced 27 calls to the get_EndPoint method, which actually refers to the EndPoint property, by the new GetEndPoint method.</p>

<p>That reduced the count to

<span class="asset  asset-generic at-xid-6a00e553e1689788330192aaef371f970d"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2014_e.txt">84 warnings</a></span>.

<p></p>Replacing the obsolete creation application NewLineBound and NewLineUnbound calls and adding an element id to the argument to all calls to Document.Delete

reduced the error list to


<span class="asset  asset-generic at-xid-6a00e553e16897883301910326f0d8970c"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2014_f.txt">48 warnings</a></span>.</p>

<p>Replacing the calls to NewSketchPlane by SketchPlane.Create reduces it to

<span class="asset  asset-generic at-xid-6a00e553e16897883301910326f20d970c"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2014_g.txt">24 warnings</a></span>.</p>

<p>Addressing almost all the rest produced the final result for today with just


<span class="asset  asset-generic at-xid-6a00e553e1689788330192aaef39dc970d"><a href="http://thebuildingcoder.typepad.com/files/bc_migr_2014_h.txt">5 warnings</a></span>,

three of which are the infamous 'mismatch between the processor architecture of the project being built "MSIL" and the processor architecture' of the referenced Revit API assemblies that I cannot fix myself.</p>

<p>One of the other two refers to a call to the obsolete Document.TitleBlocks, which is intentionally left in to compare with the new approach using the filtered element collector instead.

<p>The second warns about use of the obsolete FindReferencesWithContextByDirection method.
It should be converted to use the ReferenceIntersector class instead, which we postpone to some other time.</p>

<p>Here is the result,

<span class="asset  asset-generic at-xid-6a00e553e16897883301901d30ced4970b"><a href="http://thebuildingcoder.typepad.com/files/bc_14_100_3.zip">version 2014.0.100.3</a></span> of

The Building Coder samples.

<p>Many of the changes have been marked with a trailing comment stating '2013' and '2014', respectively.</p>

<p>Enjoy!</p>

---
layout: "post"
title: "ForgeTypeId and Units Revisited"
date: "2021-06-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2022"
  - "Architecture"
  - "Forge"
  - "Migration"
  - "Units"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/06/forgetypeid-and-units-revisited.html "
typepad_basename: "forgetypeid-and-units-revisited"
typepad_status: "Publish"
---

<p>We already discussed quite a few aspects of the Revit 2022 unit handling API and  <code>ForgeTypeId</code> usage:</p>

<!-- 1835 1836 1853 1861 1871 1899 1900 1901 1902 1903 -->

<ul>
<!--
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/revitlookup-2021-with-multi-release-support.html">RevitLookup 2021 with Multi-Release Support</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/04/whats-new-in-the-revit-2021-api.html">What's New in the Revit 2021 API</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/07/virtual-au-and-aec-hackathon-units-and-das-job.html">Virtual AU and AEC Hackathon, Units and DAS Job</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/08/custom-parameters-and-tile-packing.html">Custom Parameters and Tile Packing</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/10/firerevit-deprecated-api-and-elbow-centre-point.html">FireRevit, Deprecated API and Elbow Centre Point</a></li>
-->
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-released.html">Revit 2022 released</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/pdf-export-forgetypeid-and-multi-target-add-in.html#2">Replacing deprecated ParameterType with ForgeTypeId
</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-migrates-bim360-team-to-docs.html">Removal of DisplayUnitType in RevitLookup 2022</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-sdk-and-the-building-coder-samples.html">ParameterType and ForgeTypeId in the Revit 2022 SDK and The Building Coder samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/whats-new-in-the-revit-2022-api.html">What's New in the Revit 2022 API</a></li>
</ul>

<p>Here are some more related questions that came up since then:</p>

<ul>
<li><a href="#2"><code>FixtureUnit</code> ParameterType</a></li>
<li><a href="#3">Revit 2022 unit handling API in Dynamo</a></li>
<li><a href="#4">String values for Forge units</a></li>
<li><a href="#5">Unit conversion without knowing</a></li>
<li><a href="#6">How will we live together?</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e108ae32200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e108ae32200b img-responsive" style="width: 480px; display: block; margin-left: auto; margin-right: auto;" alt="Yardstick" title="Yardstick" src="/assets/image_c0283e.jpg" /></a><br /></p>

<p></center></p>

<p>Before diving into this topic, let us congratulate the China team in their celebration of the <a href="https://en.wikipedia.org/wiki/Dragon_Boat_Festival">Dragon Boat festival</a> today.</p>

<ul>
<li>Traditional sport : dragon boating race</li>
<li>Customary food: Zongzi, a kind of rice dumpling, packaged in bamboo or reed leaves, sweet or salty, even in ice cream (by Starbucks &nbsp; :-)</li>
<li>Memorials on the sage <a href="https://en.wikipedia.org/wiki/Qu_Yuan">Qu Yuan</a>, poet and politician, known for his patriotism</li>
<li>Prayer for health and peace by hanging calamus or mugwort on the door</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e108ae8c200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e108ae8c200b image-full img-responsive" alt="Dragon Boat festival" title="Dragon Boat festival" src="/assets/image_af6b40.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> FixtureUnit ParameterType</h4>

<p>David Becroft comes to the rescue again answering 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on how to <a href="https://forums.autodesk.com/t5/revit-api-forum/convert-parametertype-fixtureunit-to-forgetypeid/m-p/10268488">convert <code>ParameterType.FixtureUnit</code> to <code>ForgeTypeId</code></a></p>

<p><strong>Question:</strong> I am trying to port some legacy code to Revit 2022 and I am running into the problem converting some <code>ParameterType</code> enum values to a <code>ForgeTypeId</code>.</p>

<p><code>FixtureUnit</code> and <code>Invalid</code> (the result for <code>ElementId</code> parameters) are the most pressing ones.</p>

<p>There does not seem to be anything Fixture or Piping related in the <code>SpecTypeId</code> class that may apply.
<code>PipeDimension</code> comes closest but is not, I think, the same?
Plus, it also exists separately in the ParameterType enum which makes it unlikely that it got a double meaning in Revit 2022.</p>

<p>According to the release documentation there should be a deprecated method in the Parameter class that can help porting ParameterType enum values to ForgeTypeId objects, but Intellisense does not seem to be aware of the existence of such a method.</p>

<p><strong>Answer:</strong> These methods are in <code>SpecUtils</code>, <code>GetSpecTypeId(ParameterType)</code> and <code>GetParameterType(ForgeTypeId)</code>.</p>

<p>In place of <code>ParameterType.FixtureUnit</code>, you can use <code>SpecTypeId.Number</code>.</p>

<p><code>ParameterType.Invalid</code> is equivalent to an empty, default-constructed <code>ForgeTypeId</code>, i.e. <code>new ForgeTypeId()</code>.</p>

<h4><a name="3"></a> Revit 2022 Unit Handling API in Dynamo</h4>

<p>Konrad Sobon does a great job explaining how to deal with this in Dyname and Python in his discussion
of <a href="https://archi-lab.net/handling-the-revit-2022-unit-changes">handling the Revit 2022 unit changes</a>.</p>

<h4><a name="4"></a> String Values for Forge Units</h4>

<p>The unit handling changes also affected some Forge apps:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e108ae18200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e108ae18200b image-full img-responsive" alt="RVT ForgeTypeId in Forge" title="RVT ForgeTypeId in Forge" src="/assets/image_8a4a04.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>This was discussed and resolved in the StackOverflow question
on <a href="https://stackoverflow.com/questions/63992151/autodesk-forge-is-returning-odd-measurement-data">Autodesk Forge returning odd measurement data</a></p>

<p>I ended up implementing
the <a href="https://github.com/jeremytammik/the_building_coder_samples/blob/master/BuildingCoder/BuildingCoder/Util.cs#L1306-L1367">method <code>ListForgeTypeIds</code> in The Building Coder samples</a> to help resolve the issue.</p>

<p>I also shared this in the discussion 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/displayunittype-in-revit-2022/m-p/10320697">DisplayUnitType in Revit 2022</a>.</p>

<h4><a name="5"></a> Unit Conversion Without Knowing</h4>

<p>In a related vein, how can you implement a unit conversion without knowing the internal Revit unit?</p>

<p>This question has been around since the beginnings of The Building Coder and was first addressed in blog post #11
on <a href="https://thebuildingcoder.typepad.com/blog/2008/09/units.html">units</a> in September 2008.</p>

<p>It came up again in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on a <a href="https://forums.autodesk.com/t5/revit-api-forum/unit-conversion-question/m-p/9840917">unit conversion question</a>:</p>

<p>Simply set the value that you are analysing to 1.0 manually through the user interface using your current display units.</p>

<p>Then, use RevitLookup to analyse what value <code>V</code> actually gets stored.</p>

<p>The ratio between those two values, i.e., <code>V</code>/1.0 == <code>V</code>, immediately provides the conversion factor from display unit to internal database unit (or v.v.).</p>

<h4><a name="6"></a> How Will We Live Together?</h4>

<p>Moving from Revit to the topic of architecture in general, 
at <a href="https://www.labiennale.org">La Biennale di Venezia</a>,
the <a href="https://www.labiennale.org/en/architecture/2021">17th International Architecture Exhibition</a>
is focussed on the theme of <em>How will we live together?</em></p>

<blockquote>
  <p>We need a new spatial contract.
  In the context of widening political divides and growing economic inequalities, we call on architects to imagine spaces in which we can generously live together.</p>
</blockquote>

<p>At the biennale,
the <a href="https://www.floornature.com/blog/biennale-di-venezia-german-pavilion-looks-back-future-16269">German pavilion looks back from the future</a>:</p>

<blockquote>
  <p><a href="https://2038.xyz">2038</a> is the name of the German Pavilion in the 17th International Architecture Exhibition at Biennale di Venezia.
  A look back from the future, which the curators envision as a world where many of today’s problems have been overcome.
  Digital technology makes it possible to visit the pavilion from anywhere in the world: <a href="https://2038.xyz">2038.xyz</a></p>
</blockquote>

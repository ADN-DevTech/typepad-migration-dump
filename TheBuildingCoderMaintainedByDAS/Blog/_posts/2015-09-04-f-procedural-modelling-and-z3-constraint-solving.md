---
layout: "post"
title: "F# Procedural Modelling and Z3 Constraint Solving"
date: "2015-09-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "BIM"
  - "Dynamo"
  - "Element Relationships"
  - "External"
  - "F#"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/09/f-procedural-modelling-and-z3-constraint-solving.html "
typepad_basename: "f-procedural-modelling-and-z3-constraint-solving"
typepad_status: "Publish"
---

<p>Matthew <a href="https://twitter.com/moloneymb">@moloneymb</a> Moloney is excited by the potential of
<a href="https://twitter.com/moloneymb/status/638497862330941440">interactive F# coding in Revit</a>.</p>

<p>The current implementation consists of a Revit add-in, like the Tsunami Rhino plugin, with a number of improvements.</p>

<p>Mantis is similar to the Revit Python Shell but for the F# programming language. This brings with it a number of specific advantages including full code completion, error checking, performance, and design scalability.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb086d2573970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb086d2573970d img-responsive" style="width: 512px; " alt="Interactive F# Mantis shell in Revit" title="Interactive F# Mantis shell in Revit" src="/assets/image_2a7862.jpg" /></a><br /></p>

<p></center></p>

<p>You can I try it out yourself in <a href="https://gist.github.com/moloneymb/5e5608c129337cfefa40">beta</a>.</p>

<p>Let Matt know if you need any help getting started.</p>

<p>Feedback would be greatly appreciated.</p>

<p>The use of functionally generated content for structures opens the door to a wonderful world of potential, freeing up the designer from the mundane while still being able to scale to complex models that Visual BIM systems have such a hard time with.</p>

<p>Says Matt:</p>

<blockquote>
  <p>If something like Mantis helps, I'd like to continue working in the space. I have a ton of additional ideas for things to build on top of it &ndash; e.g., procedural generation libraries, the Z3 constraint solver, using type providers to import product catalogues, etc. I can even do an Rx Observable graph to update objects based on UI controls which would allow for interactive / live updates similar to the Dynamo experience. Instead of embedding code in a graph you embed the graph in the code and only visualize the UI inputs if and when you need them. It would be a code first approach. All of this is possible right out of the box with Mantis. I just need to explain and give demos on how it can be done :)</p>
  
  <p>This is an experiment to see if advanced language techniques is something architects actually want. I can build stuff just fine a vacuum by myself but there is little point to that :) I'm not in the business of building buildings so I can't make use of it myself. Much better to be helping other people.</p>
  
  <p>Which means that I need feedback on the basics before I can justify investing more time into it. If it doesn't look like it will get much traction then I'll focus on something else.</p>
  
  <p>It is ideal for users:</p>
  
  <ul>
  <li>Currently using Python</li>
  <li>Using the Edit and Continue feature in Visual Studio to avoid restarting Revit</li>
  <li>Wanting to automate more of their workflow</li>
  </ul>
</blockquote>

<p>More background information from Matt:</p>

<h4><a name="2"></a>Examples in Procedural Modelling</h4>

<p>Most of these approaches use an external domain-specific language (<a href="https://en.wikipedia.org/wiki/Domain-specific_language">DSL</a>) that gets interpreted. External DSLs are hard to extend, domain specialise, and compose - things that you almost always want to do. For example, you could take a building DSL and an arch DSL one to make a gothic building DSL that uses lots of archways.  Using a functional programming language you can use internal DSLs that are trivial to extend, compose, and domain specialise to your specific domain as it is all written in the same language.</p>

<ul>
<li><a href="https://www.youtube.com/watch?v=KENm7IsOlCw">Procedural Building in Unreal Engine 4.0</a></li>
</ul>

<h4><a name="3"></a>Z3 Constraint Solver</h4>

<p>The <a href="https://github.com/Z3Prover/z3">Z3 Constraint Solver</a> is mostly only used by academics and there is not much information out there on how to use it in other domains. In effect, it is an efficient way to automate a set of tedious tasks that would normally take far too long for a computer to figure out but is now way faster due to all the great research done in generalised solvers. Solvers used to be poor quality and very expensive but Microsoft recently made one of the best solvers available for free as open source. An example application would be to build your own routing algorithm for industrial piping that could take your specific inputs into account.  E.g. available parts, budget, safety, and regulations. If you move a tank you can just rerun the route function and the rest is automatic.</p>

<ul>
<li><a href="https://www.youtube.com/watch?v=zXBAthLSxSQ">Procedural Modelling of Structurally Sound Masonry Buildings</a>, presented at SIGGRAPH Asia 2009, using Procedural Modelling, a solver, and a simulator to great effect.</li>
<li><a href="https://www.youtube.com/watch?v=4pGyLPxangY">SolidWorks Routing</a></li>
</ul>

<h4><a name="4"></a>Type Provider Product catalogue</h4>

<p>Instead of going to a website and manually doing a search for a component you can simply find and reference the model directly in the code as if you already had it and wrapped it in an API. For instance, instead of downloading and managing shared folders of models and then referencing the models in functions via error prone path strings the type provider will do the fetching, caching, and loading the model behind the scenes. The API for this is exposed via static members e.g. Catalog.Kitchen.Sink2. If Sink3 is later added to the catalogue it's easy to change to the new sink, as it will appear as another static parameter. If Sink2 is later removed from the online catalogue (because it is no longer made) then the static member will cease to exist and the code will no longer compile.</p>

<p>Example Type Provider:</p>

<ul>
<li><a href="https://www.youtube.com/watch?v=7r2-B-5H_io">Type Provider for the World Bank Data Catalogue</a></li>
</ul>

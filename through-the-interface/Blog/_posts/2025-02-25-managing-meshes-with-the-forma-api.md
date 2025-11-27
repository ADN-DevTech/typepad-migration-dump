---
layout: "post"
title: "Managing meshes with the Forma API"
date: "2025-02-25 18:51:37"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Forma"
  - "VASA"
original_url: "https://www.keanw.com/2025/02/managing-meshes-with-the-forma-api.html "
typepad_basename: "managing-meshes-with-the-forma-api"
typepad_status: "Publish"
---

<p>Last week I spent quite a bit of time on the slopes snowboarding, but mostly in the mornings: I was back working at my computer in the afternoons. And while the mornings weren’t meant to be spent working, I actually found that some of my best ideas came to me while sitting on a chair-lift.</p>
<p><a href="/assets/image_710885.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="Thinking on a chair-lift" src="/assets/image_710885.jpg" alt="Thinking on a chair-lift." width="500" height="375" border="0" /></a></p>
<p>One such idea was around efficient mesh management inside Forma.</p>
<p>We’ve been working on a prototype web-based multi-agent simulation system where the results are displayed in either Forma or Tandem. This will form much of what I'll talk about in my session at the upcoming <a href="https://www.keanw.com/2025/02/learn-about-combining-aps-and-webassembly-at-autodesk-devcon-europe.html">Autodesk DevCon in Amsterdam</a>.</p>
<p>As with earlier prototypes where we’ve integrated VASA into Forma, one of my favourite mechanisms for displaying calculated information is to render it as a Forma mesh. We did this for the visibility dome and shadows shown in <a href="https://conferences.autodesk.com/flow/autodesk/au2024/sessioncatalog/page/inperson/session/1714403202332001pGmI">my AU 2024 talk with Håvard Høiby</a> (see page 24 of <a href="https://static.rainfocus.com/autodesk/au2024/sess/1714403202332001pGmI/srchandout/AU-Handout-Extending-Forma_1727788328701001e5Nc.pdf">the handout</a> to see what I mean).</p>
<p>I wanted to do something similar for our multi-agent system, where we have agents moving around a building along paths calculated using VASA. We’d have a mesh for the ghosted building geometry - so that we can have see-through walls - plus a mesh for each of the agents and perhaps one for each of the 3D paths they’re travelling along. Etc., etc.</p>
<p>This image should give a sense of what I’m talking about:</p>
<p><a href="/assets/image_110365.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="ForMAS" src="/assets/image_110365.jpg" alt="" width="500" height="375" border="0" /></a></p>
<p>This worked quite well but presented a number of interesting challenges: for an agent to be visible through the walls of a translucent building, the building needs to be rendered <em>after</em> the agent. So whenever you add an agent, you need to remove and re-render the building. This is also true for each path that gets created - and these are created even more frequently than the agents themselves!</p>
<p>To start with I built a complex system of re-generating/re-rendering the building geometry whenever new agents or paths were rendered. It was an unholy mess. Then, on a chair-lift, I realised a couple of things:</p>
<p>1) We could avoid having multiple meshes for agents and paths by maintaining one for all the agents and one for all the paths. A mesh in Forma is just a vertex buffer with a bunch of vertex colors - there’s nothing that says the triangles need to be adjacent. You can use one mesh to represent a number of objects throughout your scene. (While Forma uses THREE.js to display create and display geometry, this level of interface - at the mesh definition level - means you can use your own version of THREE.js - or another library entirely - to generate your mesh content. This was actually a very sensible architectural decision.)</p>
<p>2) We could add these central meshes - with zero vertices! - to Forma during initialization and then update them to have more vertices whenever appropriate. As long as we added the “dummy” meshes in the right order (agents, paths, building) then we could avoid all that silly removing and re-adding. Whenever we want to hide a mesh temporarily, rather than removing it - our previous approach - we just remove the vertices that represent it from the mesh. Or choose not to include them when we next create the mesh, of course.</p>
<p>This approach has worked <strong>perfectly</strong>. The rendering logic is intuitive and I have been able to rip out all the scaffolding that supported the removal and re-adding of meshes.</p>
<p>And the system scales well, too. I was worried about the fact we have to update an aggregate mesh whenever a single entity represented in the mesh moved, but it seems that the overhead for doing this is lower that that of managing and re-rendering multiple meshes. It’s both simpler and more performant!</p>
<p>I imagine that some of you will either already be using an approach such as this, and yet others will be saying “well, how else would you do it?”, but I’m posting this on the off-chance that other people like me are out there who would benefit from this technique.</p>
<p>Do come to the Autodesk DevCon to see this in action!</p>

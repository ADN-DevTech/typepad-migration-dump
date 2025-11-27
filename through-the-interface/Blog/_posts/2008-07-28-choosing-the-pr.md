---
layout: "post"
title: "Choosing the programming language to use for an AutoCAD development project"
date: "2008-07-28 11:18:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2008/07/choosing-the-pr.html "
typepad_basename: "choosing-the-pr"
typepad_status: "Publish"
---

<p>I just received this excellent question from a developer in Italy:</p><blockquote><p><em>We’ve been developing a quite complex AutoCAD application based on ObjectARX (gear) and VB (GUI) and we’re wondering if, after a migration in C#(?) we could maintain&nbsp; same results in term of entities control, performances and so on. In few words, is it time to leave the thorny and painful ARX world to embrace the .NET heaven :-) ?</em></p></blockquote><p>Before answering this question, I’m going to take a step back for a second…</p>

<p>Over the years I’ve been asked many times what “the best” API is to use for an AutoCAD development project. The answer has been pretty consistent, even if the technologies themselves have shifted. I almost always say “it depends” (and yes, I know that makes me sound like a lawyer ;-). The choice depends on a number of factors, some of which I’ve attempted to list here, in no particular order:</p>

<ul><li><strong>Legacy codebase</strong> – is there an existing legacy codebase, and does it work well for your customers? If so, are there interoperability paths to/from the legacy language to the language you're considering adopting? </li>

<li><strong>Functionality coverage</strong> – what type of functionality is required from the language? Does it require hardcore number-crunching capabilities or is it all about a fancy or intuitive GUI? How do these requirements match up with the capabilities of a) the language and b) the API-set exposed via that particular API for AutoCAD? </li>

<li><strong>Performance</strong> - related to the previous question is the question or performance. Does what you know about the performance of the language you're considering match the performance required by your customers? </li>

<li><strong>Integration with other technologies</strong> – do you have a requirement to integrate with other products or services, and does the language have a track record of being able to support such integrations? </li>

<li><strong>Version support</strong> - are the platforms you're required to support (both OS and AutoCAD version) all allow use of this language? </li>

<li><strong>Skills</strong> – do you and your colleagues have experience of/competence in/confidence you’ll be able to learn the language? How available are those skills on the market today – if you had to hire someone to work on your code, would it be easy to find the skills, or would their scarcity add a premium? </li>

<li><strong>Vendor commitment</strong> – does the vendor (in this case we could consider Microsoft and Autodesk as being the primary technology providers) have a long-term commitment to that technology – not just in its availability but in its evolution? One obvious example here is VBA, which is – unless things have changed while I was napping – not being made available as a 64-bit version. Which effectively means there is little to no long-term commitment from Microsoft’s side for this technology, which clearly limits the possible commitment from downstream vendors such as Autodesk. </li>

<li><strong>Support</strong> – is there a solid knowledgebase available to you on this technology, and qualified, experienced support personnel? If you need to get out of a hole at short notice, is there someone you can call?</li></ul>

<p>OK, so far I’ve managed to answer a question with about 17 others. :-) Let’s try to tackle this one more directly…</p>

<p>I’ve personally always been an advocate of the right tool for the job. I believe that for professional programmers it’s highly beneficial in today’s world to know a number of different programming languages and methodologies. It increases your flexibility as the world inevitably shifts around you. I also believe that having a blend of technologies used on a major project – just as this Italian developer has described – is pragmatic. Having hardcore C++-heads work on the core engine while the UI developers work in VB would have been exactly the choice I’d have made several years ago (which is probably when the project was initiated). My choice today – if implementing a “green field” development project, with no legacy codebase to consider – would be oriented towards one or more .NET languages (given the performance profile of managed code inside AutoCAD, which is as close to native C++ as doesn’t matter for most purposes, coupled with the UI possibilities provided by WinForms and – increasingly – WPF). The reason I say “one or more”, is that there may be arguments for having different modules implemented in different languages, depending on the domain: C# and VB are pretty comparable at most levels (the decision tends to be made on religious grounds ;-) but throwing F# into the mix might be increasingly valid for certain domain areas where functional programming excels (mostly those domains with strong connections to the world of mathematics, but there are others, such as the example I showed in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/a-simple-3d-log.html">the last post</a>).</p>

<p>Would I also choose ObjectARX? I might, depending on what was needed. Today it's still true that custom objects can only be implemented using ObjectARX (i.e. C++). I would consider the alternatives available to me carefully before heading down the path of implementing custom objects (we are continuing to work on ways to reduce the currently very significant resource investment needed to implement functionality currently only available through custom objects). I would personally invest in ObjectARX only if there was an absolute requirement for custom objects.</p>

<p>Having a homogeneous codebase does bring benefits, of course: all the code is in a particular language, uses the same tools and developers are able to dip in and out of different areas of the code without as much effort as might otherwise be required. But that’s really an idealized scenario, and in this particular developer’s case I’d look very carefully at the benefits that would be derived from migrating the entire codebase: I suspect the ObjectARX component would benefit very little from being ported to .NET, other than broadening the skillbase available to the company (and potentially reducing maintenance costs in the long run), but there might be more tangible benefit in refreshing the VB UI with something more modern-looking. The interoperability between native C++ and managed code is pretty comprehensive, so that should make life easier, especially if the ObjectARX components are already being exposed through COM for use from VB.</p>

<p>While this doesn’t actually answer the question – which I can’t do without knowing a lot more about the specific scenario – this does come a little closer, and hopefully gives a feel for what I would do when assessing the various options.</p>

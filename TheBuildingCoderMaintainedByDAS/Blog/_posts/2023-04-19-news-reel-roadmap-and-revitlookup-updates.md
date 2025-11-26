---
layout: "post"
title: "News Reel, Roadmap and RevitLookup Updates"
date: "2023-04-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2024"
  - "AI"
  - "Book"
  - "Docs"
  - "RevitLookup"
  - "Roadmap"
  - "Training"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/04/news-reel-roadmap-and-revitlookup-updates.html "
typepad_basename: "news-reel-roadmap-and-revitlookup-updates"
typepad_status: "Publish"
---

<p>More news and updates related to Revit 2024 and some little titbits on AI and literature:</p>

<ul>
<li><a href="#2">News reel and AEC roadmaps</a></li>
<li><a href="#3">Revit API training</a></li>
<li><a href="#4">RevitLookup 2024 updates</a></li>
<li><a href="#5">Free Dolly open-source instruction-tuned LLM</a></li>
<li><a href="#6">Websites powered by AI</a></li>
<li><a href="#7">Walkaway by Cory Doctorow</a></li>
</ul>

<h4><a name="2"></a> News Reel and AEC Roadmaps</h4>

<p>To get a quick overview of what's new in the Revit 2024 product, you can check out the news reel:</p>

<ul>
<li>00:00 <a href="https://youtu.be/qA74NHN8lh0">Introduction</a></li>
<li>00:30 <a href="https://youtu.be/qA74NHN8lh0?t=30">For Everyone</a></li>
<li>01:29 <a href="https://youtu.be/qA74NHN8lh0?t=89">Architects</a></li>
<li>02:37 <a href="https://youtu.be/qA74NHN8lh0?t=157">MEP</a></li>
<li>03:46 <a href="https://youtu.be/qA74NHN8lh0?t=226">Structure</a></li>
<li>05:00 <a href="https://youtu.be/qA74NHN8lh0?t=300">Model Coordination</a></li>
<li>05:24 <a href="https://youtu.be/qA74NHN8lh0?t=324">Documentation</a></li>
</ul>

<p>To find out where it is going from here forward, explore
the updated <a href="https://blogs.autodesk.com/revit/roadmap/">Autodesk AEC Public Roadmaps</a> and
take the opportunity to contribute your own requirements and ideas.</p>

<h4><a name="3"></a> Revit API Training</h4>

<p>I was asked once again about Revit API training:</p>

<p><strong>Question:</strong> Do you teach or have you ever taught Revit in an online mode? I could use that.</p>

<p><strong>Answer:</strong> We used to teach face-to-face courses before concentrating harder on making all material  publicly available to larger audiences world-wide.
Now, the material we used back then has been published and maintained on GitHub:</p>

<ul>
<li><a href="https://github.com/ADN-DevTech/RevitTrainingMaterial">ADN RevitTrainingMaterial</a></li>
<li><a href="https://github.com/jeremytammik/AdnRevitApiLabsXtra">AdnRevitApiLabsXtra</a></li>
</ul>

<p>I never did online courses myself.
I know <a href="https://www.youtube.com/user/BoostYourBIM">Harry Mattison</a> did, though.</p>

<h4><a name="4"></a> RevitLookup 2024 Updates</h4>

<p>We already have two RevitLookup updates to share, 2024.0.1 and 2024.0.2:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.2">RevitLookup 2024.0.2</a>
&ndash; Fixed Fatal Error on Windows 10 <a href="https://github.com/jeremytammik/RevitLookup/issues/153">#153</a>
&ndash; Accent colour sync with OS now only available in Windows 11 and above
&ndash; Many thanks to <a href="https://t.me/a_negus">Aleksey Negus</a> for testing builds.</li>
<li><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.1">RevitLookup 2024.0.1</a> &ndash; see below:</li>
</ul>

<p>Breaking changes:</p>

<ul>
<li>Added option to enable hardware acceleration (experimental)
<br/>The user interface is now more responsive. Revit uses software acceleration by default. Contact us if you encounter problems with your graphics cards
<br/>Known issue: rendering performance drops on selection. This is especially evident on roofs,
cf. <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-2024-rendering-performance-drops-on-selection/td-p/11878396">Revit 2024 rendering performance drops on selection</a></li>
<li>Added button to enable RevitLookup panel on Modify tab by @ricaun in <a href="https://github.com/jeremytammik/RevitLookup/pull/152">#152</a>
<br/>Disabled by default. Thanks for voting! <a href="https://github.com/jeremytammik/RevitLookup/discussions/151">#151</a></li>
<li>Opening RevitLookup window only when the Revit runtime context is available <a href="https://github.com/jeremytammik/RevitLookup/issues/155">#155</a></li>
</ul>

<p>Improvements:</p>

<ul>
<li>Added shortcuts support for the Modify tab <a href="https://github.com/jeremytammik/RevitLookup/issues/150">#150</a></li>
<li>Added EvaluatedParameter support</li>
<li>Added Category.get_Visible support</li>
<li>Added Category.get_AllowsVisibilityControl support</li>
<li>Added Category.GetLineWeight support</li>
<li>Added Category.GetLinePatternId support</li>
<li>Added Category.GetElements extension</li>
<li>Added Reference.ConvertToStableRepresentation support</li>
</ul>

<p>Bugs:</p>

<ul>
<li>Fixed rare crashes in EventMonitor on large models</li>
<li>Fixed Curve.Evaluate resolver using EndParameter as values</li>
</ul>

<p>Other:</p>

<ul>
<li>Added installers for <a href="https://github.com/jeremytammik/RevitLookup/wiki/Versions">previous RevitLookup versions</a></li>
</ul>

<p>Many thanks to Roman <a href="https://github.com/Nice3point">Nice3point</a> for his tremendous work maintaining and improving RevitLookup!</p>

<h4><a name="5"></a> Free Dolly Open-Source Instruction-Tuned LLM</h4>

<p>In spite of its name, <a href="https://en.wikipedia.org/wiki/OpenAI">OpenAI</a> is not open.</p>

<p>Hence, this is exciting, welcome  and positive news for the open community:</p>

<ul>
<li><a href="https://www.databricks.com/blog/2023/04/12/dolly-first-open-commercially-viable-instruction-tuned-llm">Free Dolly: Introducing the World's First Truly Open Instruction-Tuned LLM</a></li>
</ul>

<p>Coming up soon is a webinar on how
to <a href="https://www.databricks.com/resources/webinar/build-your-own-large-language-model-dolly">build your own large language model like Dolly</a> detailing
how to fine-tune and deploy your custom LLM.</p>

<h4><a name="6"></a> Websites Powered by AI</h4>

<p>To experience some helpful AI in action yourself, you can check
out <a href="https://twitter.com/heyBarsee/status/1646161514682884099?s=20">12 secret websites powered by AI to finish hours of work in minutes</a>.</p>

<h4><a name="7"></a> Walkaway by Cory Doctorow</h4>

<p>I discovered a new author and a new favourite book,
<a href="https://en.wikipedia.org/wiki/Walkaway_(Doctorow_novel)">Walkaway</a>
by <a href="https://en.wikipedia.org/wiki/Cory_Doctorow">Cory Doctorow</a>.</p>

<p>For me, it is a brilliant philosophical scifi reminiscent
of <a href="https://thebuildingcoder.typepad.com/blog/2021/10/sci-fi-languages-and-pipe-insulation-retrieval.html#4">William Gibson's Agency</a> that I was so thrilled by in 2021.</p>

<p>It is also focused on community, cooperation, communication, appreciation, relationships, exploitation, rich exploiters fighting to maintain a world of inequality, walkaways proving that we live in a world of surplus, not shortage, realising a post-scarcity gift economy, on-site fabrication of everything you need, and finally moving towards real visionary scifi ideas like eternal life using digital simulations of the human mind.</p>

<p>It also taught me some new vocabulary,
such as <span style="font-weight: bold; font-style: italic;">pwn</span>,
as in <a href="https://en.wikipedia.org/wiki/Leet#Owned_and_pwned">owned and pwned</a>.
That in turn led me to discover <a href="https://en.wikipedia.org/wiki/Leet">leet</a> and
<a href="https://duckduckgo.com/?q=leetspeak+translator">leetspeak translators</a>.
Not awfully useful in everyday life, but cool stuff to be aware of.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b685343998200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b685343998200d img-responsive" alt="Walkaway by Cory Doctorow" title="Walkaway by Cory Doctorow" src="/assets/image_cd1ee2.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

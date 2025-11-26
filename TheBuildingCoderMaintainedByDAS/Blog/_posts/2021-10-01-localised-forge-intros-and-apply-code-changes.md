---
layout: "post"
title: "Localised Forge Intros and Apply Code Changes"
date: "2021-10-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Accelerator"
  - "AU"
  - "Debugging"
  - "Forge"
  - "Getting Started"
  - "Hackathon"
  - "Mac"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/10/localised-forge-intros-and-apply-code-changes.html "
typepad_basename: "localised-forge-intros-and-apply-code-changes"
typepad_status: "Publish"
---

<p>Today, I highlight our new local language Forge classes and the renewed ability to easily edit and continue while debugging a Revit add-in:</p>

<ul>
<li><a href="#2">Non-mobile after computer crash</a></li>
<li><a href="#3">Local language Forge classes</a></li>
<li><a href="#4">Apply code changes debugging Revit add-in</a></li>
</ul>

<p>Before diving in, here is a nice little snippet of wisdom, courtesy
of <a href="https://twitter.com/eirannejad">Ehsan @eirannejad</a>:</p>

<blockquote>
<p><i>Every man has two lives; the second starts when he realizes he has just one.</i></p>
<p style="text-align: right; font-style: italic">&ndash; Confucius</p>
</blockquote>

<h4><a name="2"></a> Non-Mobile after Computer Crash</h4>

<p>My computer crashed, quite literally, falling several metres onto a stone floor and hitting its right front corner:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e1258cf1200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e1258cf1200b img-responsive" style="width: 359px; display: block; margin-left: auto; margin-right: auto;" alt="Computer crash" title="Computer crash" src="/assets/image_113f65.jpg" /></a><br /></p>

<p></center></p>

<p>Unsurprisingly, the screen broke:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdef5414d200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdef5414d200c img-responsive" style="width: 359px; display: block; margin-left: auto; margin-right: auto;" alt="Computer crash" title="Computer crash" src="/assets/image_e61fb4.jpg" /></a><br /></p>

<p></center></p>

<p>That forced me off-line for a while... </p>

<p>More to my surprise, the rest remained intact; so, I am now happily up and running again with peripherals: external screen, keyboard and mouse:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278804d217b200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278804d217b200d img-responsive" style="width: 359px; display: block; margin-left: auto; margin-right: auto;" alt="Computer crash" title="Computer crash" src="/assets/image_180ff0.jpg" /></a><br /></p>

<p></center></p>

<p>Could have been worse...</p>

<h4><a name="3"></a> Local Language Forge Classes</h4>

<p>Back to topics of more general interest, we are running a Forge hackathon this week and have Autodesk University coming up next, so there is a lot of exciting activity going on at that front.</p>

<p>If you are interested in learning more about Forge and your primary language is not English, one of our new local language classes may be for you:</p>

<!--
Automation & Jumeaux Numériques pour l‘industrialisation de la construction (SD500073) (French)
Digitaler Zwilling und Automation auf dem Weg zum digitalen Bau (AS500379) (German)
Uso de Forge para la transformación digital en arquitectura y construcción (CS500234) (Spanish)
Be sure to check one of them out if your preferred language is French, German or Spanish.
-->

<ul>
<li><a href="https://events-platform.autodesk.com/event/autodesk-university-2021/planning/UGxhbm5pbmdfNjcwMjAy" target="_blank">Automation &amp; Jumeaux numériques pour l'industrialisation de la construction (SD500073)</a> (French)</li>
<li><a href="https://events-platform.autodesk.com/event/autodesk-university-2021/planning/UGxhbm5pbmdfNjcwMjMy" target="_blank">Digitaler Zwilling und Automation auf dem Weg zum digitalen Bau (AS500379)</a> (German)</li>
<li><a href="https://events-platform.autodesk.com/event/autodesk-university-2021/planning/UGxhbm5pbmdfNjcwMTQz" target="_blank">Uso de Forge para la transformación digital en arquitectura y construcción (CS500234)</a> (Spanish)</li>
</ul>

<p>Please check them out if your preferred language is French, German or Spanish.</p>

<p>For more information on the current Forge hackathon and Autodesk University, you can look at
the <a href="https://www.autodesk.com/autodesk-university">AU website</a> and
Kean's article
on <a href="https://www.keanw.com/2021/09/at-the-forge-hackathon-counting-down-to-au2021.html">the Forge Hackathon and counting down to AU2021</a>.</p>

<h4><a name="4"></a> Apply Code Changes Debugging Revit Add-In</h4>

<p>Chris Hildebran pointed out that 'Apply code changes' now works when debugging and editing a Revit add-in:</p>

<p>I'm writing as a result of a discovery I saw in Visual Studio today.
That discovery is the 'Apply Code Changes' button located to the right of the Start/Continue button:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330278804d218c200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330278804d218c200d img-responsive" style="width: 348px; display: block; margin-left: auto; margin-right: auto;" alt="Apply code changes" title="Apply code changes" src="/assets/image_66e40c.jpg" /></a><br /></p>

<p></center></p>

<p>I gather it has been available for C++ for quite a while, but just recently for .NET projects, as of Visual Studio Version 16.11.0 Preview 1.0.</p>

<p>While debugging, I thought I'd see if this would work in Revit add-in development.</p>

<p>Initial testing confirmed that it does indeed apply code changes that can be seen in my video demonstrating the modification of an add-in tool I'm working on &ndash; at least in C#; still need to test <code>.xaml</code>.</p>

<p>Here is my <a href="https://www.screencast.com/t/5oCj1jBJha">two-minute video</a> demonstrating the initial test, which I hope is clear enough to see.</p>

<p>I had planned to implement a solution Josh Lumley proposed, but if this continues to work, I will continue using this feature to drastically speed up development.</p>

<p>Perhaps I'm late to the party, but I thought I'd mention it anyway.</p>

<p>Here is
the Microsoft article <a href="https://devblogs.microsoft.com/dotnet/introducing-net-hot-reload">introducing the .NET Hot Reload experience for editing code at runtime</a>.</p>

<p>Many thanks to Chris for sharing this!</p>

<p>For completeness, The Building Coder topic group
on <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49">debugging without restart and live development</a> discusses
how 'Edit and Continue' used to work way back in Revit 2008 and various other solutions suggested in the meantime.</p>

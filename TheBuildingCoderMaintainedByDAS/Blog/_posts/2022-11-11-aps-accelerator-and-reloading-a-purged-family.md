---
layout: "post"
title: "APS Accelerator and Reloading a Purged Family"
date: "2022-11-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "APS"
  - "Cloud"
  - "Family"
  - "Purge"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/11/aps-accelerator-and-reloading-a-purged-family.html "
typepad_basename: "aps-accelerator-and-reloading-a-purged-family"
typepad_status: "Publish"
---

<p>We wrap up the week announcing a new date for the Dublin onsite APS accelerator and with hints on handling the frequent problem of reloading a purged family:</p>

<ul>
<li><a href="#2">APS accelerator Dublin Dec 5-9</a></li>
<li><a href="#3">Reloading a purged family</a></li>
<li><a href="#4">The CSS OKLCH colour space</a></li>
</ul>

<h4><a name="2"></a> APS (ex-Forge) Accelerator Dublin Dec 5-9</h4>

<p>Quick update on behalf of Jaime:
we decided to push the date of the upcoming Dublin Hybrid Accelerator in the Autodesk Dublin Office to next month.</p>

<p>We are looking for customers interested in joining us onsite to work with the Developer advocates team in coding solutions using Autodesk Platform Services (formerly Forge).
In the past (pre-covid), we regularly hosted such events in the EMEA region to have software developers join us for the week.
We have an ongoing registration and would love to have a full house for the event:</p>

<ul>
<li><a href="https://www.eventbrite.com/e/autodesk-platform-services-aps-accelerator-dublin-november-14-18-2022-registration-440477168067">Registration for APS Accelerator Dublin December 5-9, 2022</a></li>
<li><a href="https://forge.autodesk.com/accelerator-program">More details about this and other APS Accelerators</a></li>
</ul>

<h4><a name="3"></a> Reloading a Purged Family</h4>

<p>My solution engineer colleague Lejla Secerbegovic explored how to trigger the re-loading of family types after having (accidentally) purged them.</p>

<p>Here is a 46-second video showing the <a href="https://youtu.be/bHi_9Z3srqo">issue reloading purged family in Revit</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/bHi_9Z3srqo" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<p>It seems that Revit only reloads the family if it can see any changes &ndash; however, when you purge, the .rfa doesn’t change. <br />
From my understanding, I need to get Revit to trigger the following dialog (which doesn’t appear if you don’t edit the .rfa):</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302af1c8f4116200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302af1c8f4116200d img-responsive" style="width: 437px; display: block; margin-left: auto; margin-right: auto;" alt="Reload purged family" title="Reload purged family" src="/assets/image_d67961.jpg" /></a><br /></p>

<p></center></p>

<p>After some discussion, we moved this forward from 'it’s by design' to a workaround suggestion to create an API-based Dynamo process to open the family, create an invisible edit and then reload it. The edit would trigger the GUID change.</p>

<p>Here are the details of that conversation:</p>

<p><strong>Question:</strong> Is there any way to restore the family types that were removed by purging?
As you can see in the video, if I purge the unused types and then try reloading the family, it will not restore the types (probably because it thinks nothing in the family has changed?)</p>

<p><strong>Answer:</strong> Family types can be created in the project and are not sent back to the rfa file unless you save out a new version of the family.
So, if you load the family, make types in the project, then delete or purge them in the project, they are gone.
Reloading the rfa will just load it before the types ever existed.
You would need a backup of the projects before the purge; then you could save out the families and load them to the purged project... I think...</p>

<p><strong>Response:</strong> Sorry if it wasn’t clear in the video: the types exist in the .rfa and were not created in the project.
Users sometimes purge “accidentally”, which removes all unused types, and this is why we are looking for a way to easily restore them from the .rfa.</p>

<p>I’m not using any type catalogues, but families with different types created in the family environment and this happens with EVERY family.
Please note you need to reload the family from the original location, which is typically what a user would do.
Steps to reproduce:</p>

<ul>
<li>Load a family with different types created in the family environment into a project</li>
<li>Place one instance / one type of the family in the project</li>
<li>Purge and check the not used types have been deleted</li>
<li>Reload the same family from the same location without changing it any way</li>
<li>RESULT: the purged types won’t be restored</li>
</ul>

<p>Here are the 2 families I tested this with:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/files/ls_m_desk.rfa">M_Desk.rfa</a> from our official library</li>
<li><a href="https://thebuildingcoder.typepad.com/files/ls_lego_brick.rfa">LEGO_Brick.rfa</a> custom Lego brick used in the video</li>
</ul>

<p><strong>Answer:</strong> With your steps I can reproduce.</p>

<p>According to our developer, this is by design and we will not need to discuss it further.</p>

<p>A workaround for your customer if they want to get the types back without deleting any existing placed families would be to:</p>

<ul>
<li>Open the family they are trying to load that has the types</li>
<li>Change something small like visual style in a view</li>
<li>Use the load into project button to load the family</li>
<li>Now you should see the overwrite message and get your family types back</li>
</ul>

<p>Regarding the original post &ndash; correct!
If you did not change the family in any way, this results in a NOP.
You can also see that there is no transaction in the Undo dropdown.</p>

<p>This is how it is by design.</p>

<p><strong>Response:</strong> Thanks for the response &ndash; what would then be the recommendation for the following scenario:</p>

<ul>
<li>Unexperienced users sometimes purge “accidentally” (there is no way to disable the function in Revit and there is no warning telling you what it really means)</li>
<li>BIM Managers  need to able to restore missing types for ALL families &ndash; do we really tell them to copy the library somewhere else / edit every family?</li>
</ul>

<p>Is there any way to force reload through the API or Dynamo?</p>

<p><strong>Answer:</strong> The API or Dynamo could make the invisible modification allowing the reload, yes.
I envision:</p>

<ul>
<li>User runs tool</li>
<li>Selects family file to ‘reload’</li>
<li>Family is opened in the background</li>
<li>Invisible edit is made</li>
<li>Family is loaded into the project closing without saving</li>
</ul>

<p>Because of the invisible modification, the family has a change and can be reloaded with all types.
They could save if they wanted to, but not much need.</p>

<p>Question is what is an acceptable invisible edit.
I’d personally use something like an extensible storage entity that just gets incremented by one, but that’s hard for many to self-manage.
They could use a hidden parameter with a toggle value in a similar way though.</p>

<p>That would be a fairly straight forward tool they can self-build if so inclined.</p>

<p>The change can also be unmade after the load.
So, it could be a Boolean "Reload Me" parameter that gets changed to true before the reload and false after.</p>

<p>Thanks to Angel Velez, Ivan Dobrianov, Jacob Small, Lejla Secerbegovic, Mariah Hanning and Steven Crotty for the fruitful discussion.</p>

<h4><a name="4"></a> The CSS OKLCH Colour Space</h4>

<p>Moving to a completely different topic: just half a year ago, I pointed out
the <a href="https://thebuildingcoder.typepad.com/blog/2022/05/analysis-of-macros-journals-and-add-in-manager.html#5">advantages of the HSL colour space in CSS</a>.</p>

<p>That can be yet further improved on, cf. <a href="https://evilmartians.com/chronicles/oklch-in-css-why-quit-rgb-hsl">OKLCH in CSS: why we moved from RGB and HSL</a>.</p>

<blockquote>
  <p>In CSS, we’ve been encoding colours with <code>rgb()</code> or hex (mostly for historical reasons).
  However, the new <a href="https://www.w3.org/TR/css-color-4/">CSS Color 4</a> specification
  adds many better ways of declaring colours in CSS.
  Of these, <code>oklch()</code> is the most interesting one &ndash; this article explains why.</p>
</blockquote>

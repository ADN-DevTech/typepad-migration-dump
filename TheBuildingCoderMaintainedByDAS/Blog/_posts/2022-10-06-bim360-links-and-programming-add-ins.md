---
layout: "post"
title: "BIM360 Links and Programming Add-Ins"
date: "2022-10-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "360"
  - "AppStore"
  - "BIM"
  - "Getting Started"
  - "Git"
  - "Mac"
  - "Material"
  - "Template"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/10/bim360-links-and-programming-add-ins.html "
typepad_basename: "bim360-links-and-programming-add-ins"
typepad_status: "Publish"
---

<p>Today, we share Naveen Kumar T's first Revit API blog post, an important solution to fix a problem loading BIM 360 links, and lots of tools and advice on programming Revit add-ins in general:</p>

<ul>
<li><a href="#2">New Revit material appearance asset</a></li>
<li><a href="#3">BIM 360 links not found solution</a></li>
<li><a href="#4">AppStore guidelines for Revit add-in</a></li>
<li><a href="#5">Coding Revit add-ins the e-verse way</a></li>
<li><a href="#6">VS wizard template location</a></li>
</ul>

<h4><a name="2"></a> New Revit Material Appearance Asset</h4>

<p>Naveen Kumar T shared a blog post in
the <a href="https://adndevblog.typepad.com/aec">DAS AEC DevBlog</a> showing
how to <a href="https://adndevblog.typepad.com/aec/2022/10/setting-appearanceasset-properties-of-newly-created-revit-material.html">set <code>AppearanceAsset</code> properties of a newly created Revit material</a>.</p>

<p>Many thanks to Naveen, and looking forward to many future posts.</p>

<h4><a name="3"></a> BIM 360 Links Not Found Solution</h4>

<p>Luiz Henrique <a href="https://github.com/ricaun">@ricaun</a> Cassettari shared a helpful 
<a href="https://forums.autodesk.com/t5/revit-api-forum/bim-360-links-not-found-fix/td-p/11463147">BIM 360 links not found fix</a>:</p>

<p>A user reports to me that some of my plugins had problems with BIM360 links; after testing a lot of things, I found out what is causing the problem. It's a known problem here in the forum. It is caused by referencing a higher version of the <code>Newtonsoft.Json</code> package.</p>

<p>In Revit 2021, the default version of <code>Newtonsoft.Json</code> is 11; when an application or plugin requests to load <code>Newtonsoft.Json</code> version 13 and it is not found in the plugins folder, something strange happens.</p>

<p>Instead of ignoring and using the already loaded <code>Newtonsoft.Json</code> version 11, for some reason, Revit loads a <code>Newtonsoft.Json</code> file from the Autodesk <code>PnIDModeler</code> add-in, and it is a version 11.</p>

<p>Now, if you have two copies of <code>Newtonsoft.Json</code> with the same version loaded in Revit, when BIM360 tries to initialize (usually happens when you open a Revit file that requires BIM360), it will throw an exception in the Journal and the BIM360 service fails. Without the service, Revit can't load BIM360 link. It displays this message:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302a308e2936e200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302a308e2936e200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Cannot load BIM360 link" title="Cannot load BIM360 link" src="/assets/image_f437bc.jpg" /></a><br /></p>

<p></center></p>

<p>A quick fix would be to remove or change the name of the <code>Newtonsoft.Json.dll</code> file in the Autodesk <code>PnIDModeler</code> add-in.</p>

<p>Or, if you are a developer using an old version like version 9, it should work; Revit will load the default version instead. But if you need to use a specific version of <code>Newtonsoft.Json</code>, be aware that it could cause some issues in Revit.</p>

<p>Here is Luiz' seven-minute video explanation 
on <a href="https://youtu.be/gNnZZjbBVlU">how to fix BIM360 link not found problem</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/gNnZZjbBVlU" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>

<p>Related references:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/bim-360-quot-linked-document-is-not-found-quot-error/m-p/10638072">BIM 360 "Linked document is not found" error</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/bim-360-links-issue/m-p/10536461">BIM 360 Links issue</a></li>
<li><a href="https://archi-lab.net/dll-hell-is-real/">archi+lab DLL Hell is Real</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/02/getting-started-once-again.html#6">Newtonsoft Json.dll Version Conflict</a></li>
</ul>

<p>Many thanks to Luiz Henrique for documenting and sharing this important solution!</p>

<h4><a name="4"></a> AppStore Guidelines for Revit Add-In</h4>

<p>Now, let's move on to some generic add-in hints:
if you are thinking about programming a Revit add-in at all, and especially if you are thinking about the AppStore, you should check out the AppStore guidelines and its numerous useful general development recommendations:</p>

<ul>
<li><a href="https://www.autodesk.com/developer-network/app-store/revit">The Autodesk App Store &ndash; information for Revit developers</a></li>
</ul>

<h4><a name="5"></a> Coding Revit Add-Ins the E-Verse Way</h4>

<p>Going much further and on into the modern development process, a comprehensive guide explaining more complex and intricate aspects of Revit add-in coding is presented by
Francisco Maranchello of <a href="https://e-verse.com">e-verse</a> in his article
on <a href="https://blog.e-verse.com/build/coding-revit-add-ins-the-e-verse-way">coding Revit add-ins the e-verse way</a>, saying:</p>

<blockquote>
  <p>I'm an architect and coder and have worked on many Revit addins these past few years, alongside other stuff like recently building and scaling <a href="https://e-verse.com/">e-verse</a>.
  I wanted to share an article we recently published on our blog to see if it could help the community, shedding some light on the structure we use for Revit addins:
  <a href="https://blog.e-verse.com/build/coding-revit-add-ins-the-e-verse-way">blog.e-verse.com/build/coding-revit-add-ins-the-e-verse-way</a>.
  I know you've covered this subject and shared many templates over the years since I've been a reader of The Building Coder for a while now, so I figured maybe we could contribute to that if you find anything you think can add value there.
  There's more to come as well.
  Thank you!</p>
</blockquote>

<p>Francisco covers the complete add-in ecosystem including:</p>

<ul>
<li>Git code repository</li>
<li>A .NET framework solution containing multiple projects</li>
<li>NuGet dependencies</li>
<li>Post-build events for debugging and releasing </li>
<li>A configuration file to manage settings and environment variables outside the main logic</li>
<li>CI/CD pipelines leveraging GitHub Actions</li>
</ul>

<p>Very many thanks to Francisco for sharing his experience and in-depth advice!</p>

<h4><a name="6"></a> VS Wizard Template Location</h4>

<p>One of the templates mentioned above is the very simple and minimal
<a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard">Visual Studio Revit add-in template</a> for
C# and VB.</p>

<p>I recently resolved an issue I had getting it to work on my system using Parallels on the Mac.
My template was displayed, and I had a hard time discovering why.
For instance, the thread
on how to <a href="https://kwilson.io/blog/fix-visual-studio-when-templates-disappear-from-your-add-new-item-dialogue">fix Visual Studio when templates disappear from your Add New Item dialogue</a> did
not help.</p>

<p>Finally, I discovered that the Parallels setup had surreptitiously modified my VS project template location settings:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302acc60ece8e200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302acc60ece8e200b image-full img-responsive" alt="Visual Studio project template location" title="Visual Studio project template location" src="/assets/image_01a96a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Once that was understood, the issue was easy to resolve, setting the location back to the default folder on the C: drive.</p>

<p>I also added a corresponding note to the <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard#user-project-template-location">readme</a> for
my own and others' future reference.</p>

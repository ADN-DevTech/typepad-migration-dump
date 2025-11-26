---
layout: "post"
title: "BIM 360 Docs, Add-In Folders, Stallman and Abc"
date: "2016-02-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "Apps"
  - "Docs"
  - "Events"
  - "Exchange"
  - "Installation"
  - "News"
  - "Philosophy"
  - "Plugin"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/02/bim-360-docs-add-in-folders-stallman-and-the-abc-conjecture.html "
typepad_basename: "bim-360-docs-add-in-folders-stallman-and-the-abc-conjecture"
typepad_status: "Publish"
---

<p>I am going to the University of Bern this afternoon to listen to Richard Stallman
speak <a href="http://www.digitale-nachhaltigkeit.unibe.ch/veranstaltungen/richard_m_stallman/index_ger.html">For A Free Digital Society</a>.</p>

<h4><a name="3"></a>Richard Stallman in Switzerland</h4>

<p>Here are some other recent and not-so-recent topics:</p>

<ul>
<li><a href="#2">BIM 360 Docs</a></li>
<li><a href="#3">Richard Stallman in Switzerland</a></li>
<li><a href="#4">Is the <em>abc</em> conjecture proven?</a></li>
<li><a href="#5">Add-In Folders</a></li>
</ul>

<h4><a name="2"></a>BIM 360 Docs</h4>

<p><a href="http://www.autodesk.com/products/bim-360-docs/overview">BIM 360 Docs</a> is
the new Autodesk platform for construction document management.</p>

<p>The <a href="http://www.autodesk.com/products/bim-360-docs/overview">BIM 360 Docs</a> web
service ensures that the entire project team is always building from the correct version of documents, plans, and models.</p>

<p>This is obviously absolutely fundamental to save time, reduce risk, and mitigate errors in construction projects.</p>

<p>It was previously available as preview technology and is now unleashed as a real product,
so <a href="http://www.engineering.com/BIM/ArticleID/11434/BIM-360-Docs-Not-a-Preview-Anymore.aspx">not a preview any more</a>.</p>

<p>You can <a href="https://bim360docs.autodesk.com/session">sign in</a> for a test run right away.</p>

<p>Long-term, in my wildest fantasies, I can imagine BIM 360 Docs growing into something like a synthesis of Navisworks, the current BIM 360 platform, Vault, and more.</p>

<p>Pretty cool dream, isn't it?</p>

<p><a class="asset-img-link"  style="float: right;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08b650cc970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08b650cc970d img-responsive" style="width: 200px; margin: 0px 0px 5px 5px;" alt="Richard Stallman" title="Richard Stallman" src="/assets/image_1e6020.jpg" /></a></p>

<h4><a name="3"></a>Richard Stallman in Switzerland</h4>

<p><a href="https://de.wikipedia.org/wiki/Richard_Stallman">Richard Stallman</a> of
the <a href="https://www.fsf.org">Free Software Foundation FSF</a> is visiting and giving talks in Switzerland:</p>

<ul>
<li><a href="https://www.fsf.org/events/rms-20160205-bern">Bern University</a>
&ndash; <a href="http://www.digitale-nachhaltigkeit.unibe.ch/veranstaltungen/richard_m_stallman/index_ger.html">For A Free Digital Society</a></li>
<li><a href="https://www.fsf.org/events/rms-20160208-zurich">Zurich ImpactHub</a></li>
<li><a href="https://www.fsf.org/events/rms-20160210-sierre">Sierre</a></li>
<li><a href="https://www.fsf.org/events/rms-speeches.html">More events with Stallman</a></li>
<li><a href="https://www.fsf.org/events/all.html">More FSF events</a></li>
</ul>

<p>I am going to the presentation in Bern this afternoon, and hoping to meet my colleague
<a href="http://through-the-interface.typepad.com/through_the_interface/about-the-author.html">Kean Walmsley</a> there
too.</p>

<h4><a name="4"></a>Is the <em>abc</em> Conjecture Proven?</h4>

<p>Talking about universities and academics, a local newspaper just mentioned that
the <a href="https://en.wikipedia.org/wiki/Abc_conjecture">abc Conjecture</a> has now been proved
by <a href="https://en.wikipedia.org/wiki/Shinichi_Mochizuki">Shinichi Mochizuki</a>.</p>

<p>The <a href="https://en.wikipedia.org/wiki/Abc_conjecture">Wikipedia article</a> does
indeed mention his efforts and how challenging it is to verify them.</p>

<p>Interesting stuff.
I love diving into pure maths just a little bit now and then.</p>

<h4><a name="5"></a>Add-In Folders</h4>

<p>Back to Revit again. This rather dated question that a whole bunch of my colleagues from ADN chipped in to answer from
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread on
the <a href="http://forums.autodesk.com/t5/revit-api/autoloader-folder-applicationplugins/td-p/5556540">auto-loader folders for application plugins</a> has
been hanging around for a while in my to-do list:</p>

<p><strong>Question:</strong> It appears that the folders used for AutoCAD add-ins is also valid for use with Revit.</p>

<p>I can therefore use this path:</p>

<blockquote>
  <p>C:\ProgramData\Autodesk\ApplicationPlugins\MySuperApp.bundle</p>
</blockquote>

<p>instead of the Revit specific add-in folder:</p>

<blockquote>
  <p>C:\ProgramData\Autodesk\Revit\Addins\2014\MySuperApp.bundle</p>
</blockquote>

<p>Is this correct?</p>

<p>I would much rather have a common installation folder for both AutoCAD and Revit applications managed via the XML manifest file.</p>

<p><strong>Answer:</strong> Yes, the ApplicationPlugins folder should work for both AutoCAD and Revit (on the current versions).</p>

<p>Looking forward, we are thinking about moving the default to the <code>Program Files</code> folder for the AutoCAD environment.</p>

<p>The Revit API help file RevitAPI.chm does not have anything additional to say on this subject, as you can see under Developers &gt; Revit API Developers Guide &gt; Introduction &gt; Add-In Integration
&gt; <a href="http://help.autodesk.com/view/RVT/2015/ENU/?guid=GUID-4FFDB03E-6936-417C-9772-8FC258A261F7">Add-in Registration</a>.</p>

<p>My colleagues concur, saying:</p>

<p>Yes, that is the theory; Max, Maya and AutoCAD (at least) use</p>

<blockquote>
  <p>C:\ProgramData\Autodesk\ApplicationPlugins</p>
</blockquote>

<p>As long as the host app looks in that folder, it should work.</p>

<p>I just checked, and that location works for Inventor 2015 AddIns as well.</p>

<p>The Inventor API Help describes these four recommended locations:</p>

<ul>
<li>All Users, Version Independent
<ul>
<li>Windows 7 - %ALLUSERSPROFILE%\Autodesk\Inventor Addins\</li>
<li>Windows XP - %ALLUSERSPROFILE%\Application Data\Autodesk\Inventor Addins\</li>
</ul></li>
<li>All Users, Version Dependent
<ul>
<li>Windows 7 - %ALLUSERSPROFILE%\Autodesk\Inventor 2013\Addins\</li>
<li>Windows XP - %ALLUSERSPROFILE%\Application Data\Autodesk\Inventor 2013\Addins\</li>
</ul></li>
<li>Per User, Version Dependent
<ul>
<li>Both Window 7 and XP - %APPDATA%\Autodesk\Inventor 2013\Addins\</li>
</ul></li>
<li>Per User, Version Independent
<ul>
<li>Both Window 7 and XP - %APPDATA%\Autodesk\ApplicationPlugins</li>
</ul></li>
</ul>

<p>Here is some additional information on this from the AppStore perspective:</p>

<p>These two folder locations were added to support our Exchange Store apps:</p>

<ul>
<li>%AppData%\Autodesk\ApplicationPlugins</li>
<li>%ProgramData%\Autodesk\ApplicationPlugins</li>
</ul>

<p>You can find more information on the XML files from
the <a href="http://www.autodesk.com/developapps">App Store Developer Centre</a>,
including <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=20143032">specifics about publishing Revit apps</a>.</p>

<p>Further down, the page provides a table with recordings and PowerPoint slide decks.
You may want to take a look at the XML tags mentioned there.
This is the location to place exchange apps for all the different products.
Make sure that you provide all relevant information in the XML file and structure your apps as self-contained archive file bundles as described in the publisher page.</p>

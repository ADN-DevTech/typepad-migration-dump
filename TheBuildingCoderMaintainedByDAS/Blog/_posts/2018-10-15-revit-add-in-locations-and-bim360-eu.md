---
layout: "post"
title: "Revit Add-In Locations and BIM360 EU"
date: "2018-10-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "AppStore"
  - "BIM"
  - "Docs"
  - "Getting Started"
  - "Installation"
  - "News"
  - "Philosophy"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/10/revit-add-in-locations-and-bim360-eu.html "
typepad_basename: "revit-add-in-locations-and-bim360-eu"
typepad_status: "Publish"
---

<p>I am just getting ready to leave for Darmstadt, for the Forge DevCon and German Autodesk University.</p>

<p>Here are some quick recent topics I want to share before jumping on the train:</p>

<ul>
<li><a href="#2">BIM 360 Docs API change for European data centre access</a> </li>
<li><a href="#3">Revit add-in locations</a> </li>
<li><a href="#4">Autodesk AppStore bundle format</a> </li>
<li><a href="#5">Juli Zeh über das <em>Turbo-Ich</em></a> </li>
<li><a href="#6">YouTube video subtitles and auto-translation</a></li>
</ul>

<h4><a name="2"></a> BIM 360 Docs API Change for European Data Centre Access</h4>

<p>BIM 360 Docs recently started using a European data centre.</p>

<p>Note that the BIM 360 API is already using different US and EU endpoints for some resources, such as the Account Administration feature.</p>

<p>Unfortunately, a breaking change occurred in the BIM360 Docs API that you need to be aware of if you are accessing the European data centre; you will need to modify the code with new endpoints.</p>

<p>The Data Management API is not affected.</p>

<p>For more details, please refer to
the <a href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/">BIM 360 Account Admin API HTTP specification</a> and
Mikako Harada and Augusto Goncalves' article
on <a href="https://forge.autodesk.com/blog/bim-360-docs-api-changes-access-data-european-data-center">BIM 360 Docs: API Changes to Access Data in European Data Center</a>.</p>

<h4><a name="3"></a> Revit Add-In Locations</h4>

<p>Several questions on Revit add-in locations were raised in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>,
most recently in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-addin-file-locations-for-production-release-and-test-debug/m-p/8325021">Revit add-in file locations for production release and test/debug</a>:</p>

<p><strong>Question:</strong> Where are the  appropriate file installation locations for Release and Test/Debug Revit add-in components?</p>

<p>I have noticed a variety of locations where Revit add-in components are deployed for Release and Test/Debug purposes.</p>

<p>For example, the Hello World sample for Revit 2017 recommends installing <code>HelloWorld.addin</code> to <em>C:\ProgramData\Autodesk\Revit\Addins\2017</em>, whereas the Addin Wizard generates a script that automatically loads the <code>*.addin</code> file to <em>C:\UserName\AppData\roaming\AppData\Roaming\Autodesk\Revit\Addins\2017</em>.</p>

<p>I have also noticed that the official release add-in installation programs that I have historically gotten back from the Autodesk Revit team seem to install the <code>*.addin</code>, <code>*.dll</code>, and other supporting files in numerous locations depending on:</p>

<ul>
<li>Revit version(s) supported</li>
<li>All users vs. current user installation</li>
<li>Roaming profiles vs. non-roaming profiles</li>
<li>Version of Windows (?)</li>
<li>Possibly other criteria</li>
</ul>

<p>First, is there documentation that describes how this should be done for commercial products?</p>

<p>Second, in the specific example of installing an add-in for Revit 2017 running on Windows 10 for the current user only without roaming, where would I install the production components (<code>myaddin.addin</code>, <code>myaddin.dll</code>, <code>myaddindependent.dll</code>)?</p>

<p>Can I utilize the location for the <code>.addin</code> file for Test and Debug?</p>

<p>I assume that I will need to point the <code>myaddin.addin</code> file to the Debug version of the <code>myaddin.dll</code> file in order to Debug?</p>

<p><strong>Answer:</strong> The official documentation for this can be found in the Revit API developer guide; go to:</p>

<ul>
<li><a href="http://help.autodesk.com/view/RVT/2017/ENU">Revit online help</a></li>
<li>Developers</li>
<li><a href="http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42">Revit API Developers Guide</a></li>
<li><a href="http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-C574D4C8-B6D2-4E45-93A5-7E35B7E289BE">Introduction</a></li>
<li><a href="http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-4BE74935-A15C-4536-BD9C-7778766CE392">Add-In Integration</a></li>
<li><a href="http://help.autodesk.com/view/RVT/2017/ENU/?guid=GUID-4FFDB03E-6936-417C-9772-8FC258A261F7">Add-in Registration</a></li>
</ul>

<p>The Building Coder also discusses
some <a href="http://thebuildingcoder.typepad.com/blog/2016/02/bim-360-docs-add-in-folders-stallman-and-the-abc-conjecture.html#5">additional undocumented add-in folder paths</a>.</p>

<h4><a name="4"></a> Autodesk AppStore Bundle Format</h4>

<p>More information on using the Autodesk AppStore bundle format was shared by Nachikethan S:</p>

<p>I can understand your concern of noticing a variety of locations where Revit Add-in components are deployed.</p>

<p>There is a reason for this. Let me explain:</p>

<ul>
<li><em>%AppData%\Autodesk\Revit\Addins\<year></em> &ndash; This location is for per-user only, and it is recommended for per-user specific applications.</li>
<li><em>%ProgramData%\Autodesk\Revit\Addins\<year></em> &ndash; This location is for multiple users, so all the users can have access to the application.</li>
</ul>

<p>A few apps are placed in Program Files, too, depending on need.</p>

<p>In the Autodesk App Store, we normally recommend using the following paths:</p>

<ul>
<li><em>%ProgramData%\Autodesk\ApplicationPlugins</em> (for multiple users) </li>
<li><em>%AppData%\Autodesk\ApplicationPlugins</em> (for per user) </li>
</ul>

<p>In order to use these paths, you need to use the bundle format, wherein the add-in and its dependent files are stored in a bundle folder.</p>

<p>Revit autoloads the add-in module based on the entries in the <code>PackageContents.xml</code> file placed in the bundle folder.</p>

<p>For more details, please refer to
the <a href="http://thebuildingcoder.typepad.com/files/3_autodesk_exchange_publish_revit_apps_preparing_apps_for_the_store_guidelines.pptx">slide deck PPT with guidelines on preparing Revit apps for the store</a> from slide 6 onward, describing the bundle format.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad399063f200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad399063f200d image-full img-responsive" alt="AppStore publishing overview" title="AppStore publishing overview" src="/assets/image_ae5fb9.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Here is the
<a href="http://thebuildingcoder.typepad.com/files/3_autodesk_exchange_publish_revit_apps_preparing_apps_for_the_store_guidelines.pdf">same slide deck in PDF format</a> in
order to enhance its Internet discoverability and readability.</p>

<p>You can utilise these locations for Testing and Debugging, and, yes, you can point to the <code>myaddin*.addin</code> file to the Debug version of the <code>myaddin.dll</code> file in order to Debug.</p>

<p>If you need any help on the bundle format, please feel free
to <a href="mailto:appsubmissions@autodesk.com">email us @appsubmissions@autodesk.com</a>.</p>

<h4><a name="5"></a> Juli Zeh Über das Turbo-Ich</h4>

<p>I end with a non-technical note that is basically of interest to all humans living in modern society and pondering today's state of politics, social media und everyday behaviour.</p>

<p>However, it is in German language, which presumably limits the audience somewhat, at least until the automatic translation and subtitling of YouTube videos is perfected.</p>

<p>This is a pointer to a brilliant cultural, social, political, critical analysis by my favourite German author and thinker Juli Zeh.</p>

<p>I am touched and impressed by her deep understanding and clear explanation of the interaction of modern society, social media, culture, politics and everyday life.</p>

<p>Ich bin beeindruckt und berührt von Juli Zehs tiefblickende Analyse von dem Wechselspiel von Social Media und aktuelle gesellschaftliche und politische Zustände.</p>

<p>Juli Zeh spricht über die Macht von Stimmungen und die Zukunft der Demokratie in dem Vortrag <a href="https://youtu.be/-5djf2rZMD4?t=870">Das Turbo-Ich - Der Mensch im Kommunikationszeitalter</a>, zu Gast bei der Tübinger Mediendozentur CampusTV Tübingen, 12 Juli 2018:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/-5djf2rZMD4?start=872" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
</center></p>

<h4><a name="6"></a> YouTube Video Subtitles and Auto-Translation</h4>

<p>Wow, here is yet another note after all, of technical nature this time.</p>

<p>I just tested turning on auto-generated subtitles for this video and requesting them to be auto-translated to English, with astonishingly good results.</p>

<p>Go to YouTube and enable these two options in the right-hand bottom corner.</p>

<p>Nope, looking a bit further, there is still considerable room for improvement.</p>

<p>This talk is still too complex for the functionality available today...</p>

<p>It is pretty impressive anyway, though.</p>

---
layout: "post"
title: "Model Group and DA4R Language in Forge"
date: "2021-02-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "360"
  - "DA4R"
  - "Data Access"
  - "Forge"
  - "Group"
  - "I18n"
  - "JavaScript"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/02/model-group-and-da4r-language-in-forge.html "
typepad_basename: "model-group-and-da4r-language-in-forge"
typepad_status: "Publish"
---

<p>Today, let's look at two Japanese Forge questions, on model groups and the Revit engine language, another RevitLookup enhancement, and, while we're talking about languages, a surprising scientific discovery on naked mole-rat dialects:</p>

<ul>
<li><a href="#2">Retrieving Revit model group in Forge</a></li>
<li><a href="#3">Specifying the Revit UI language in DA4R</a></li>
<li><a href="#4">RevitLookup supports <code>ScheduleDefinition</code> <code>GetField</code></a></li>
<li><a href="#5">Naked mole-rats speak in community dialects</a></li>
<li><a href="#6">Van Gogh 360</a></li>
</ul>

<h4><a name="2"></a> Retrieving Revit Model Group in Forge</h4>

<p>Let's start with the query <em>8473 &ndash; question about model group for Forge</em>:</p>

<p><strong>Question:</strong> I have a question about Revit model groups.</p>

<p>Is it possible to get the group information in Forge?</p>

<p>For example, if I select Group 1 in the UI, I would like to select all the objects in it, determine the group name and other information about group member elements.</p>

<p>So, does the Forge viewer support selecting a Revit model group? Is there any way to retrieve the model group information in Forge, or is it 'lost in translation'?</p>

<p><strong>Answer:</strong> The viewer doesn't support it out of the box, but the information is still preserved in the Forge translation of the RVT model, not lost in translation.</p>

<p>You can navigate Groups and their Members by descending down the non-graphical property hierarchy, starting at the root/model element. Then, it is also possible to programmatically simulate group selection by selecting group members.</p>

<p>Here is an example of a group (ID=50) with one member (ID=389), which is a chair element in the Revit sample project rac_basic_sample_project.rvt:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263e98f40b5200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263e98f40b5200b image-full img-responsive" alt="Forge viewer group properties" title="Forge viewer group properties" src="/assets/image_8b6d7e.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>The StackOverflow discussion
on <a href="https://stackoverflow.com/a/61998004">grouping elements cannot find in Forge viewer</a> shows 
a pretty neat code snippet by Eason Kang implementing this functionality.</p>

<p>Many thanks to Traian Stanev and Eason for their valuable help in addressing this.</p>

<h4><a name="3"></a> Specifying the Revit UI Language in DA4R</h4>

<p>The next Japanese Forge issue is <em>8492 &ndash; language settings of DA4R engine</em>:</p>

<p><strong>Question:</strong> I have a question about DA4R, Design Automation for Revit.</p>

<p>Can I specify the language of the Engine of DR4R?
I would like to use the Japanese Engine &ndash; is the default engine English?
My add-in seems to depend on the Japanese Engine.</p>

<p><strong>Answer:</strong> Good programming practice would ensure that the add-in is not language dependent.</p>

<p>Since every DA4R add-in is a DB-only application, it should in theory be language independent automatically.</p>

<p>Unfortunately, Revit itself does not completely follow this practice in all points.</p>

<p>Furthermore, every Revit BIM relies on many component that can be modified and added by end users, e.g., family definitions, which are pretty hard to make language independent.</p>

<p>So, I understand your challenge.</p>

<p>As a first step, let's look at the situation in the desktop Revit API and search
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> for
the term 'language'; that turns up two useful entries:</p>

<ul>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-change-revit-language-using-api/m-p/9156524">How to change Revit language using API?</a></li>
<li><a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-the-language-of-revit-user-interface/m-p/6840139">How to get the language of Revit user interface?</a></li>
</ul>

<p>The Revit API provides
the <a href="https://www.revitapidocs.com/2020/2b1d8b80-a11c-2a57-63bd-6c0d67691879.htm"><code>Application.Language</code> property</a> to
determine the language used in the current session of Revit, so that part is easy.</p>

<p>However, the API does not provide any method to control the language being used.</p>

<p>On the desktop, you can use the language switch in the Revit.exe command line, cf. the discussions
on <a href="https://thebuildingcoder.typepad.com/blog/2017/01/distances-switches-kiss-ing-and-a-dino.html#3">Revit command-line switches</a>
and <a href="https://thebuildingcoder.typepad.com/blog/2017/02/multiple-language-resx-resource-files.html">multiple language RESX resource files</a></p>

<p>Now, trying to apply this insight to the DA4R environment, there are two questions:</p>

<ul>
<li>Can we specify command line argument to the Revit engine in DA4R?</li>
<li>Will the DA4R Revit engine react to the language switch in the command line passed in?</li>
</ul>

<p>I believe the answer to both of these is yes.</p>

<p>Searching for 'commandline' and 'revitcoreconsole' turns up <a href="https://stackoverflow.com/questions/63128763/error-application-revitcoreconsole-exe-exits-with-code-19088744-which-indicate">an example of passing in a command line argument to the DA4R engine</a>.</p>

<p>And yes, indeed, the same language command line switch works with an optional <code>/l</code> argument to <code>revitcoreconsole.exe</code>.</p>

<p>It is described in more detail with a sample code snippet in the official documentation
on <a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step5-publish-activity/#additional-notes">publishing an activity</a>.</p>

<p><strong>Response:</strong> I tried the <code>/l</code> option, and then my add-in completed the process with no error:</p>

<pre class="code">
  revitcoreconsole.exe /l JPN (arguments...)
</pre>

<p>Thanks much for your support and information!</p>

<p>Thanks also to Rahul Bhobe for helpful advice.</p>

<h4><a name="4"></a> RevitLookup Supports ScheduleDefinition GetField</h4>

<p>Following up on his enhancement
enabling <a href="https://thebuildingcoder.typepad.com/blog/2021/02/splits-persona-collector-region-tag-modification.html#4">RevitLookup to handle split region offsets</a> last
week, Michael <a href="https://github.com/RevitArkitek">@RevitArkitek</a> Coffey submitted a
new <a href="https://github.com/jeremytammik/RevitLookup/issues/70">issue #70 &ndash; <code>ScheduleDefinition.GetField</code> method not showing</a> and
the subsequent <a href="">pull request #71  adds handler for GetSplitRegionOffsets</a>, saying:</p>

<blockquote>
  <p>The method <code>ScheduleDefinition.GetField</code> does not show because it requires an integer index parameter.
  A list of ScheduleFields can be returned, named by the index that was used.</p>
  
  <p>I have this working and can submit a pull request.
  I have an issue though, in that there are two <code>GetField</code> methods, the other taking in an id.
  I have not found a way to filter out the second method, so when viewing the <code>ScheduleDefinition</code> properties there will be two <code>GetField</code> entries.
  If you know of a way to filter out that second method you can let me know or you could add it.
  Otherwise, it could be left as is or put on hold.</p>
</blockquote>

<p><strong>Answer:</strong> That sounds great, very useful!</p>

<p>Thank you very much for the offer!</p>

<p>That would require analysing the complete <code>GetField</code> method signature.</p>

<p>The two overloads <code>GetField(Int32)</code> and <code>GetField(ScheduleFieldId)</code> have
different <a href="https://www.c-sharpcorner.com/UploadFile/puranindia/method-signatures-in-C-Sharp">method signatures</a>.</p>

<p>They can be distinguished using by checking their parameter types using .NET Reflection, as explained
in <a href="https://stackoverflow.com/questions/5152346/get-only-methods-with-specific-signature-out-of-type-getmethods">how to get only methods with a specific signature out of <code>Type.GetMethods</code></a>.</p>

<p>The new functionality is captured
in <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.13">RevitLookup release 2021.0.0.13</a>.</p>

<h4><a name="5"></a> Naked Mole-Rats Speak in Community Dialects</h4>

<p>A surprising scientific analysis of their vocalisations demonstrates
that <a href="https://www.treehugger.com/naked-mole-rats-speak-dialects-5101265">naked mole-rats speak in community dialects</a>:</p>

<blockquote>
  <p>Sharing a dialect strengthens cohesiveness in the colony</p>
  
  <p>... helps with group solidarity and connection</p>
  
  <p>In any social group, including our own, having a rapid way of identifying who belongs to the group and who is excluded is useful for many practical reasons.</p>
</blockquote>

<h4><a name="6"></a> Van Gogh 360</h4>

<p>For something not related to programming or science, let's take a moment to simply savour this beautifully crafted and implemented
stitched-together <a href="https://static.kuula.io/share/79QMS">van Gogh 360</a>:</p>

<!--
js

<center>


27ec91d6a25120864e5b202efb734d23


<center>

iframe

-->

<p><center></p>

<iframe width="100%" height="640" style="width: 100%; height: 640px; border: none; max-width: 100%;" frameborder="0" allowfullscreen allow="xr-spatial-tracking; gyroscope; accelerometer" scrolling="no" src="https://kuula.co/share/79QMS?fs=1&vr=0&sd=1&thumbs=1&info=1&logo=0"></iframe>

<p></center></p>

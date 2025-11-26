---
layout: "post"
title: "BCF API and Manually Setting Up an Add-In"
date: "2016-05-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2017"
  - "BIM"
  - "External"
  - "Getting Started"
  - "Migration"
  - "REST"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/05/bcf-api-and-manually-setting-up-an-add-in.html "
typepad_basename: "bcf-api-and-manually-setting-up-an-add-in"
typepad_status: "Publish"
---

<p>The time is overdue to migrate
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20">Visual Studio Revit Add-In Wizards</a> to
Revit 2017.</p>

<p>This time, instead of a simple flat migration like in previous years, I decided to <a href="#3">set up a new Visual Studio C# .NET Revit add-in project manually</a>, by hand, completely from scratch, just to see whether anything significant changed since I did that last, and to ensure that the wizard is really using all the required Visual Studio settings.</p>

<p>I tried it out in by implementing a <a href="http://www.buildingsmart-tech.org">buildingSMART</a> <a href="#2">BCF API sample client</a>.</p>

<p>Basically, that requires the following steps:</p>

<ul>
<li>Create a new pure Visual Studio class library</li>
<li>Rename <code>Class1</code> to <code>Command</code></li>
<li>Add references to the Revit API assemblies</li>
<li>Implement an external command</li>
<li>Implement an add-in manifest</li>
<li>Define an add-in GUID</li>
<li>Implement a post-build event to auto-install the add-in</li>
</ul>

<h4><a name="2"></a>The BCF API and a Sample Client</h4>

<p>To try out these steps, I made use of a brand new sample project that I implemented in response to
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread inquiring
about <a href="http://forums.autodesk.com/t5/revit-api/support-for-bcf-api/m-p/6282780">support for the BCF API</a>:</p>

<p><strong>Question:</strong> Can anyone answer if Revit supports the BCF API to allow for third party integrations?</p>

<p><strong>Answer:</strong> I searched the Internet for 'bcf api' and found
the <a href="https://github.com/BuildingSMART/BCF-API">BuildingSMART BCF-API GitHub repository</a>.</p>

<p>Is that what you are talking about?</p>

<p>Being a REST API, you can use it perfectly well from your own .NET add-ins.</p>

<p>The BCF API GitHub repo points to a C# client,
the <a href="https://github.com/rvestvik/BcfApiExampleClient">BcfApiExampleClient</a>.</p>

<p>You can try integrating that very code into a Revit add-in and see how it goes.</p>

<p>Out of interest, I took a slightly deeper look.</p>

<p>I see that BCF stands for <a href="http://iug.buildingsmart.org/resources/abu-dhabi-iug-meeting/IDMC_017_1.pdf">BIM Collaboration Format</a>.</p>

<p>I also see that the C# sample can easily be integrated into a Revit add-in.</p>

<p><strong>Question:</strong> Does the Revit API create the JSON for BCF or would we be required to create it ourselves via the add-in?</p>

<p><strong>Answer:</strong> I was not aware of BCF until you raised this question, and I am pretty sure that Revit does nothing to support it.</p>

<p>If it is BCF specific, you will probably have to create it with your own custom add-in.</p>

<p>I created a new project to try out converting the BcfApiExampleClient sample to a Revit add-in,
<a href="https://github.com/jeremytammik/RvtBcfApiExampleClient">RvtBcfApiExampleClient</a>.</p>

<p>It is a pure and simple REST API client.</p>

<p>I had some issues getting both the original sample and my Revit version up and running.</p>

<p>They were resolved over the week-end, however, and both the stand-alone sample client and the new Revit add-in version now work fine.</p>

<p>Here are some screen snapshots running the Revit add-in version:</p>

<p>The first step is the log in to the BIM-IT server:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08fd2ef5970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08fd2ef5970d img-responsive" style="width: 467px; " alt="BIM-IT login" title="BIM-IT login" src="/assets/image_9a06e5.jpg" /></a><br /></p>

<p></center></p>

<p>As usual, this authorisation process returns an access token:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c859a11f970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c859a11f970b img-responsive" style="width: 379px; " alt="BIM-IT access token" title="BIM-IT access token" src="/assets/image_304387.jpg" /></a><br /></p>

<p></center></p>

<p>Finally, the sample simply lists all existing projects, of which I possess exactly zero:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d1e3680f970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d1e3680f970c img-responsive" style="width: 366px; " alt="BIM-IT project list" title="BIM-IT project list" src="/assets/image_9b4dac.jpg" /></a><br /></p>

<p></center></p>

<p>If you are interested in diving deeper into the BCF API, please note
the <a href="https://github.com/rvestvik/BcfApiExampleClient/issues/2">comment by Georg Dangl</a>,
saying, "in December we had another Hackathon in Munich where Veni Lillkall created
a <a href="https://github.com/BIMit/BCF-Hackathon-Munich/tree/Team_C%23">small sample app in C#</a>.
That might give you some ideas how to use the BCF API itself."</p>

<h4><a name="2.1"></a>The Kayak Framework &ndash; an Easy Way to Speak HTTP with .NET</h4>

<p>One interesting aspect of the BCF-API sample is its use
of <a href="http://dotnetslackers.com/articles/aspnet/The-Kayak-Framework-An-easy-way-to-speak-HTTP-with-NET.aspx">the Kayak framework &ndash; an easy way to speak HTTP with .NET</a>, 
published in 2010 by <a href="http://dotnetslackers.com/community/members/bvanderveen.aspx">Benjamin van der Veen</a>,
including <a href="http://dotnetslackers.com/code/KayakCMSExample.zip">Kayak sample code</a>:</p>

<ul>
<li>Kayak is a lightweight HTTP server for the CLR, and the Kayak Framework is a utility for mapping HTTP requests to C# method invocations. With Kayak, you can skip the bulk, hassle, and overhead of IIS and ASP.NET. Kayak enables you to do more with less syntax, and is easy to configure to work in any way you care to dream up.</li>
<li>Kayak is a simple server with an easy-to-use request framework. It automatically maps HTTP verb/path combinations to your methods, deserialises arguments to invocations of those methods from the query string or JSON request body, and serialises the return values as JSON. It's behaviour is configurable, yet simple to use and understand thanks to its limited scope.</li>
</ul>

<h4><a name="3"></a>Manual Setup of a Revit Add-In</h4>

<p>As said, I used the BCF API client sample project to record exactly how to manually set up a Visual Studio C# .NET Revit add-in from scratch.</p>

<p>Here are the detailed individual steps I performed for this, with links to the corresponding commits in the GitHub repository:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/7fb713de0efb0940191ba20dba7b7b08220c7e62">Added pure Visual Studio class library</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/4a98cd78157be9e36abda9b9b85a3156d8fed911">Renamed Class1 to Command and edited properties</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/ccd0c0f6469425c059567e3b26b75acbc37602b2">Added Revit API assemblies and implemented external command</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/1f9f74e83af6cdbbea4541ab03cdbe15a6868754">Added add-in manifest</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/667a7d8b51292ae3a84bbf59df8d7d2ad9a64886">Added add-in manifest</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/4982403505a9baf44e46e36cf5b500d3375af7b9">Added GUID using guidize.exe</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/9101faa26d071d6247714c6f4af94171a9814f52">Implemented post-build event to auto-install add-in for Revit to find</a></li>
<li><a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/commit/1912b9586bcb2ec0219f89bc0fc4d8aa98992bd9">Successfully tested</a></li>
</ul>

<p>The result is <a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/releases/tag/2017.0.0.0">RvtBcfApiExampleClient release 2017.0.0.0</a>, which is just a naked Revit 2017 C# .NET add-in skeleton.</p>

<p>The final working version presented above after fixing the initial problems with the login process is captured
in <a href="https://github.com/jeremytammik/RvtBcfApiExampleClient/releases/tag/2017.0.0.4">release 2017.0.0.4</a>.</p>

<p>As said, next I will use the generated files to update
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.20">Visual Studio Revit add-in wizard</a> for Revit 2017.</p>

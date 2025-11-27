---
layout: "post"
title: "AU Handouts: AutoCAD&reg; .NET - Developing for AutoCAD&reg; Using F# - Part 1"
date: "2008-10-23 11:09:17"
author: "Kean Walmsley"
categories:
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Training"
original_url: "https://www.keanw.com/2008/10/au-handouts-aut.html "
typepad_basename: "au-handouts-aut"
typepad_status: "Publish"
---

<p><em>As mentioned in </em><a href="http://through-the-interface.typepad.com/through_the_interface/2008/10/freewheel-fun.html"><em>my previous post</em></a><em>, I've been beavering away on the handout for a new class </em><a href="http://through-the-interface.typepad.com/through_the_interface/2008/10/coming-to-this.html"><em>I'm delivering at this year's Autodesk University</em></a><em>. Here is the first part of this handout.</em></p>

<p><strong>Introduction</strong></p>

<p>F# is a new programming language from Microsoft, due to become a first-class .NET citizen (joining its siblings C# and VB.NET) and fully integrated with Visual Studio 2010. In this class we’ll introduce many of the concepts behind the F# language, and look at examples where we use it to create applications inside AutoCAD.</p>

<p>At the time of writing, F# is available as a Community Technology Preview (CTP), installable on Visual Studio 2008. Prior versions of F# provide some integration with Visual Studio 2005, but in this session I’ll primarily be using the superior integration with Visual Studio 2008.</p>

<p>The various versions of F# are available from: <a href="http://research.microsoft.com/fsharp">http://research.microsoft.com/fsharp</a></p>

<p>For Visual Studio 2005, install <a href="http://research.microsoft.com/research/downloads/Details/7ac148a7-149b-4056-aa06-1e6754efd36f/Details.aspx">1.9.4.19</a></p>

<p>For Visual Studio 2008: install <a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=61ad6924-93ad-48dc-8c67-60f7e7803d3c&amp;displaylang=en">1.9.6.2, the F# September 2008 CTP</a></p>

<p>For these samples, as well as other examples of integrating F# with AutoCAD, visit my blog: <a href="http://through-the-interface.typepad.com/through_the_interface/f">http://through-the-interface.typepad.com/through_the_interface/f</a></p>

<p><strong>Creating our first F# project</strong></p>

<p>Let’s start by creating an F# project and adding some very simple code.</p>

<p>In Visual Studio 2005, you’ll find the F# project type under “Other Project Types”, assuming you’ve installed a version of F# that integrates with VS 2005, of course:</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20New%20F_%20project%20-%20VS%202005.png"><img height="303" alt=" New F# project - VS 2005" src="/assets/AU%202008%20-%20New%20F_%20project%20-%20VS%202005_thumb.png" width="448" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p align="center"><em>Figure 1 – Creating a new F# project in Visual Studio 2005</em></p>

<p align="left">In Visual Studio 2008 we can already see an example of a tighter integration - the F# project types are included at a higher level in the tree-view of project types:</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20New%20F_%20project%20-%20VS%202008.png"><img height="306" alt="New F# project - VS 2008" src="/assets/AU%202008%20-%20New%20F_%20project%20-%20VS%202008_thumb.png" width="453" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a></p>

<p align="center"><em>Figure 2 – Creating a new F# project in Visual Studio 2008</em></p>

<p align="left">The blank project in Visual Studio 2005 is just that – it doesn’t even include a source file. If you right-click the project in the solution explorer and <em>Add</em> -&gt; <em>New Item</em>, you’ll get a dialog showing the various types of source file that you can add:</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20New%20F_%20file%20-%20VS%202005.png"><img height="276" alt="New F# file - VS 2005" src="/assets/AU%202008%20-%20New%20F_%20file%20-%20VS%202005_thumb.png" width="458" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p align="center"><em>Figure 3 – Adding a new F# source file to our blank project in Visual Studio 2005</em></p>

<p align="left">The initial F# source file created in Visual Studio 2005 already contains a lot of code intended to introduce you to various F# language features. To get the equivalent code in Visual Studio 2008, you need to create a “F# Tutorial” project type.</p>

<p align="left">The standard file created by default in a new Visual Studio 2008 F# project – Program.fs – contains only this text, which we’ll use as a starting point:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">#light</span></p></div>

<p align="left">This directive tells the F# compiler that we’ll be using “light” syntax, which essentially means a lighter-weight version of the language that relies more heavily on indentation and reduces the need for additional keywords in your source files. The light syntax is very much the standard: almost all F# samples you’ll come across - except for those designed to show the heavyweight syntax – make use of this convention.</p>

<p align="left">Before we write some code, let’s launch the “F# Interactive” (FSI) window in Visual Studio – a very useful explorative environment for developing and testing F# code.</p>

<p align="left">In Visual Studio 2005, you’ll find this under the Add-In manager (<em>Tools</em> -&gt; <em>Add-in Manager</em>):</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20Launching%20F_%20interactive%20-%20VS%202005.png"><img height="296" alt="AU 2008 - Launching F# interactive - VS 2005" src="/assets/AU%202008%20-%20Launching%20F_%20interactive%20-%20VS%202005_thumb.png" width="456" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a></p>

<p align="center"><em>Figure 4 – Launching F# Interactive in Visual Studio 2005</em></p>

<p align="left">In Visual Studio 2008 this has, once again, become more tightly integrated:</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20Launching%20F_%20interactive%20-%20VS%202008.png"><img height="352" alt="Launching F# interactive - VS 2008" src="/assets/AU%202008%20-%20Launching%20F_%20interactive%20-%20VS%202008_thumb.png" width="352" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p align="center"><em>Figure 5 - Launching F# Interactive in Visual Studio 2008</em></p>

<p align="left">Once launched, you should see the FSI window at the bottom of Visual Studio. FSI is an integrated component that allows on-the-fly compilation and evaluation of F# code. It doesn’t actually interpret the code, but it can often feel as though that’s what’s happening.</p>

<p align="left">OK, now we’re ready top write our first F# function. Type the following code into the F# code window:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> f a b = a + b</p></div>

<p align="left">The let operator binds a value or a function to a symbol. In this case the function is called <span face="Courier New">f</span>, and takes two arguments, <span face="Courier New">a</span> and <span face="Courier New">b</span>. The result of this function is the sum of <span face="Courier New">a</span> and <span face="Courier New">b</span>.</p>

<p align="left">You’ll probably notice something interesting about this function, right off the bat: there are no types declared. These are inferred by the system, based on how the code is used. The default will be to consider <span face="Courier New">f</span> to take two integers and return an integer, but if your code uses it with real numbers then the arguments will be inferred to be of type <span face="Courier New">float</span>.</p>

<p align="left">It is also possible to specify the type of the arguments by saying, for instance:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> f (a : float) (b : float) = a + b</p></div>

<p align="left">This can be useful, but makes your code less composable (i.e. harder to copy and paste and use elsewhere). Trusting F#’s type inference to determine the right types is the best approach, where possible: there is no performance penalty as the types are inferred at compile-time, not at runtime.</p>

<p align="left">Before we see this thrilling function in action, let me point out a few more significant facts about it. This function is <em>pure</em>, which means a few things:</p>

<ol><li><div align="left">Its outputs are a simple function of its inputs</div></li>

<li><div align="left">It attempts no side-effects</div>

<ul><li><div align="left">No shared state is modified</div></li>

<li><div align="left">It doesn’t attempt to write to a file or to the screen</div></li>

<li><div align="left">If you execute this function multiple times with the same arguments, you will always get the same result</div></li></ul></li></ol>

<p align="left">Pure code has a number of advantages: it can more easily be parallelized (farmed out to multiple computing cores), and is also more composable. Having whole programs that are pure isn’t realistic, though: some side-effects are needed to let the user know what’s happening, for instance :-), but having pure sections of code is going to become an increasingly important goal over the coming years.</p>

<p align="left">OK, let’s now highlight our code and press Alt-Enter to load it into FSI:</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20My%20first%20F_%20function.png"><img height="282" alt="Our first F# function" src="/assets/AU%202008%20-%20My%20first%20F_%20function_thumb.png" width="482" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p align="center"><em>Figure 6 – Loading our first F# function into F# Interactive</em></p>

<p align="left">After loading the function we can test it by typing this at the command-line:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">f 1 2;;</p></div>

<p align="left">This calls the function <span face="Courier New">f</span> with arguments <span face="Courier New">1</span> and <span face="Courier New">2</span> (you can see the result above). You need to terminate code in the FSI window with a double semi-colon – if your code doesn’t appear to be executing properly, then it’s most likely you’re forgetting to do that.</p>

<p align="left">You can see that the type of the function has been inferred as:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">val f : int -&gt; int -&gt; int</p></div>

<p align="left">This means it takes two integers and returns an integer. You may find the type format somewhat strange: there’s no real distinction between the arguments and the returned value, other than their order in the “pipeline”. This allows us to do things like partial application and currying – an example of partial application being if we define an “increment” function called <span face="Courier New">inc</span>:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> inc = f 1</p></div>

<p align="left">Here we have just stated that calling <span face="Courier New">inc </span>on an integer is like calling <span face="Courier New">f</span> on <span face="Courier New">1</span> and that integer. It may seem strange – once again – that we don’t write it like this:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> inc a = f 1 a</p></div>

<p align="left">In my view one of the beauties of functional programming is that this is <strong>not</strong> needed – you can define functions simply by “pinning down” arguments of other functions.</p>

<p align="left"><strong>Creating our first AutoCAD application in F#</strong></p>

<p><em>The code used in this example is available </em><a href="http://through-the-interface.typepad.com/through_the_interface/files/text-extraction-2005.fs"><em>here for 1.9.6.19 and before</em></a><em> and </em><a href="http://through-the-interface.typepad.com/through_the_interface/files/text-extraction-2008.fs"><em>here for 1.9.6.2 onwards</em></a><em>.</em></p>

<p align="left">We’re here to find out how to create F# code that works with AutoCAD, so let’s go ahead and do that. We’re going to create an AutoCAD command that goes through the contents of our drawing and creates a list of all the words used (removing duplicates along the way). As you’ll see in this code, processing lists of data is very easy from a functional programming language.</p>

<p align="left">At this point I’m going to stop explaining how to do most things in the pre-CTP versions of F# - a VS 2005-compatible version of the code has been posted to my blog, as there will now start to be an increasing number of differences both at the project level and in the source code that we’re going to create. Code written against the eventual released version of F# is much more likely to code written against the CTP, so that’s what we’ll focus on here.</p>

<p align="left">A word of caution: F# Interactive is not hosted inside AutoCAD’s process, so calling F# functions that access AutoCAD objects via the .NET API from FSI <strong>will not work</strong>. That said, we will create some core functions that are capable of being tested within FSI, so there is some value to understanding how this is possible.</p>

<p align="left">Let’s now make sure our project is going to create a Class Library (DLL) rather than a Windows Application (EXE). We do this by modifying the project properties (using either the main pull-down menus, <em>Project</em> -&gt; <em>FirstFSharpProject Properties…</em>, or right-clicking the FirstFSharpProject project in the Solution Explorer and selecting <em>Properties</em> from the context menu).</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20Make%20%20our%20project%20create%20a%20Class%20Library.png"><img height="257" alt=" Making our project create a Class Library" src="/assets/AU%202008%20-%20Make%20%20our%20project%20create%20a%20Class%20Library_thumb.png" width="478" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p align="center"><em>Figure 7 – Changing our project settings to create a Class Library</em></p>

<p align="left">Let’s now replace our code inside the main file (in my case called Program.fs) with this:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">#light</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: green">// Partial application of split which can then be </span></p>

<p style="MARGIN: 0px"><span style="COLOR: green">// applied to a string to retrieve the contained words</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> words =</p>

<p style="MARGIN: 0px">&nbsp; String.split [<span style="COLOR: maroon">' '</span>]</p></div>

<p align="left">What we have here is a function called words which is simply a call to the <span face="Courier New">String.split</span> function (a static function belonging to the <span face="Courier New">String</span> class – this is where we start to use F#’s object-oriented capabilities, as well as its functional ones). The <span face="Courier New">words</span> function still needs an argument, and, of course, we could also write it like this, if we chose:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> words text =</p>

<p style="MARGIN: 0px">&nbsp; String.split [<span style="COLOR: maroon">' '</span>] text</p></div>

<p align="left">But we won’t, as the previous definition is more elegant. :-)</p>

<p align="left">OK, so let’s give this a try in FSI. Select the whole contents of the file and hit Alt-Enter.</p>

<p align="left">All being well we should see an error such as this:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">C:\...\ FirstFSharpProject\Program.fs(7,10): error FS0039: The value, constructor, namespace or type 'split' is not defined. A construct with this name was found in FSharp.PowerPack.dll, which contains some modules and types that were implicitly referenced in some previous versions of F#. You may need to add an explicit reference to this DLL in order to compile this code.</p></div>

<p align="left">That’s actually a very accurate and helpful error message: to make use of <span face="Courier New">String.split</span> in our project we need to add a reference to it. And to use it in FSI we need to do the same.</p>

<p align="left">Add a reference via the pull-down menu or the Solution Explorer, selecting this module:</p>

<p align="center"><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20Adding%20a%20reference%20to%20the%20F_%20PowerPack%20assembly.png"><img height="320" alt="Adding a reference to the F# PowerPack assembly" src="/assets/AU%202008%20-%20Adding%20a%20reference%20to%20the%20F_%20PowerPack%20assembly_thumb.png" width="380" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p align="center"><em>Figure 8 – Adding a project reference to the F# PowerPack</em></p>

<p align="left">That works for the project, but FSI will give us exactly the same error as before. Within FSI, type the following instruction, which will add a reference to the assembly (and is, in fact, the easiest way to add references to your project when working with pre-CTP F#):</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">#r &quot;FSharp.PowerPack&quot;;;</p></div>

<p align="left">We should get this response:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">--&gt; Referenced 'C:\Program Files\FSharp-1.9.6.2\bin\FSharp.PowerPack.dll'</p></div>

<p align="left">And when we attempt to reload the code into FSI using Alt-Enter, we should see:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">val words : string -&gt; string list</p></div>

<p align="left">Which means we have a function called words that takes a string and returns a list of strings. Let’s give it a try:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&gt; words &quot;The quick brown fox jumped over the lazy dog.&quot;;;</p>

<p style="MARGIN: 0px">val it : string list</p>

<p style="MARGIN: 0px">= [&quot;The&quot;; &quot;quick&quot;; &quot;brown&quot;; &quot;fox&quot;; &quot;jumped&quot;; &quot;over&quot;; &quot;the&quot;; &quot;lazy&quot;; &quot;dog.&quot;]</p>

<p style="MARGIN: 0px">&gt;</p></div>

<p align="left">Great! But right now we’re only using the space character to separate words. There are a number of characters that make sense to use as separators in the context of text found in AutoCAD drawings, for example, along with space, I would also choose tab and a number of symbols such as:&nbsp; ~`!@#$%^&amp;*()-=_+{}|[]\;':&quot;&lt;&gt;?,./</p>

<p align="left">So how do we do that? Well, the ugly way is to pass that in as a list of characters into our function:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> words =</p>

<p style="MARGIN: 0px">&nbsp; String.split [<span style="COLOR: maroon">' '</span>;<span style="COLOR: maroon">'\t'</span>;<span style="COLOR: maroon">'~'</span>;<span style="COLOR: maroon">'`'</span>;<span style="COLOR: maroon">'!'</span>;<span style="COLOR: maroon">'@'</span>;<span style="COLOR: maroon">'#'</span>;<span style="COLOR: maroon">'$'</span>;<span style="COLOR: maroon">'%'</span>;<span style="COLOR: maroon">'^'</span>;<span style="COLOR: maroon">'&amp;'</span>;<span style="COLOR: maroon">'*'</span>;<span style="COLOR: maroon">'('</span>;<span style="COLOR: maroon">')'</span>;<span style="COLOR: maroon">'-'</span>;<span style="COLOR: maroon">'='</span>;<span style="COLOR: maroon">'_'</span>;<span style="COLOR: maroon">'+'</span>;<span style="COLOR: maroon">'{'</span>;<span style="COLOR: maroon">'}'</span>;<span style="COLOR: maroon">'|'</span>;<span style="COLOR: maroon">'['</span>;<span style="COLOR: maroon">']'</span>;<span style="COLOR: maroon">'\\'</span>;<span style="COLOR: maroon">';'</span>;<span style="COLOR: maroon">'\''</span>;<span style="COLOR: maroon">':'</span>;<span style="COLOR: maroon">'\&quot;'</span>;<span style="COLOR: maroon">'&lt;'</span>;<span style="COLOR: maroon">'&gt;'</span>;<span style="COLOR: maroon">'?'</span>;<span style="COLOR: maroon">','</span>;<span style="COLOR: maroon">'.'</span>;<span style="COLOR: maroon">'/'</span>]</p></div>

<p align="left">Yes, that’s certainly ugly. A better approach would be to just have a string that we decompose into a list (a relatively inexpensive operation that is insignificant when compared with the code maintenance benefits).</p>

<p align="left">Let’s create an intermediate value called <span face="Courier New">seps</span> and assign our string to it. We can then use the string’s <span face="Courier New">ToCharArray()</span> method to generate a nice array of characters, which we then turn into a list using <span face="Courier New">Array.to_list</span>:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> words =</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> seps = <span style="COLOR: maroon">&quot; \t~`!@#$%^&amp;*()-=_+{}|[]\\;':\&quot;&lt;&gt;?,./&quot;</span> </p>

<p style="MARGIN: 0px">&nbsp; String.split (Array.to_list (seps.ToCharArray()))</p></div>

<p align="left">It should be obvious how we’re using brackets to choose the execution order of the above code.</p>

<p align="left">To test this out, let’s pass in a few paragraphs from this document, to see how it copes:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&gt; words &quot;We’re here to find out how to create F# code that works with AutoCAD, so let’s go ahead and do that. We’re going to create an AutoCAD command that goes through the contents of our drawing and creates a list of all the words used (removing duplicates along the way). As you’ll see in this code, processing lists of data is very easy from a functional programming language.</p>

<p style="MARGIN: 0px">At this point I’m going to stop explaining how to do most things in the pre-CTP versions of F# - a VS 2005-compatible version of the project has been posted to my blog, as there will now start to be an increasing number of differences both at the project level and in the source code that we’re going to create. Code written against the eventual released version of F# is much more likely to code written against the CTP, so that’s what we’ll focus on here.</p>

<p style="MARGIN: 0px">A word of caution: F# Interactive is not hosted inside AutoCAD’s process, so calling F# functions that access AutoCAD objects from FSI will not work. That said, we will create some core functions that are capable of being tested within FSI, so there is some value to understanding how this is possible.</p>

<p style="MARGIN: 0px">Let’s now make sure our project is going to create a Class Library (DLL) rather than a Windows Application (EXE). We do this by modifying the project properties (using either the main pull-down menus, Project -&gt; FirstFSharpProject Properties…, or right-clicking the FirstFSharpProject project in the Solution Explorer and selecting Properties from the context menu).&quot;;;</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">val it : string list</p>

<p style="MARGIN: 0px">= [&quot;We’re&quot;; &quot;here&quot;; &quot;to&quot;; &quot;find&quot;; &quot;out&quot;; &quot;how&quot;; &quot;to&quot;; &quot;create&quot;; &quot;F&quot;; &quot;code&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;that&quot;; &quot;works&quot;; &quot;with&quot;; &quot;AutoCAD&quot;; &quot;so&quot;; &quot;let’s&quot;; &quot;go&quot;; &quot;ahead&quot;; &quot;and&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;do&quot;; &quot;that&quot;; &quot;We’re&quot;; &quot;going&quot;; &quot;to&quot;; &quot;create&quot;; &quot;an&quot;; &quot;AutoCAD&quot;; &quot;command&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;that&quot;; &quot;goes&quot;; &quot;through&quot;; &quot;the&quot;; &quot;contents&quot;; &quot;of&quot;; &quot;our&quot;; &quot;drawing&quot;; &quot;and&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;creates&quot;; &quot;a&quot;; &quot;list&quot;; &quot;of&quot;; &quot;all&quot;; &quot;the&quot;; &quot;words&quot;; &quot;used&quot;; &quot;removing&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;duplicates&quot;; &quot;along&quot;; &quot;the&quot;; &quot;way&quot;; &quot;As&quot;; &quot;you’ll&quot;; &quot;see&quot;; &quot;in&quot;; &quot;this&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;code&quot;; &quot;processing&quot;; &quot;lists&quot;; &quot;of&quot;; &quot;data&quot;; &quot;is&quot;; &quot;very&quot;; &quot;easy&quot;; &quot;from&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;a&quot;; &quot;functional&quot;; &quot;programming&quot;; &quot;language&quot;; &quot;\nAt&quot;; &quot;this&quot;; &quot;point&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;I’m&quot;; &quot;going&quot;; &quot;to&quot;; &quot;stop&quot;; &quot;explaining&quot;; &quot;how&quot;; &quot;to&quot;; &quot;do&quot;; &quot;most&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;things&quot;; &quot;in&quot;; &quot;the&quot;; &quot;pre&quot;; &quot;CTP&quot;; &quot;versions&quot;; &quot;of&quot;; &quot;F&quot;; &quot;a&quot;; &quot;VS&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;2005&quot;; &quot;compatible&quot;; &quot;version&quot;; &quot;of&quot;; &quot;the&quot;; &quot;project&quot;; &quot;has&quot;; &quot;been&quot;;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&quot;posted&quot;; &quot;to&quot;; ...]</p>

<p style="MARGIN: 0px">&gt;</p></div>

<p align="left">The display of the returned value has been truncated to the first 100 elements, but it is all there. You may also see additional characters that could be added to the list of separators, but that is left as an exercise for the reader. :-)</p>

<p align="left">So far the words we have are neither sorted nor de-duplicated. Let’s now look into doing that, by defining a new <span face="Courier New">sortedWords</span> function that takes a list of strings (as it’s going to make sense later to pass in a list of strings, rather than just having one monolithic string as the input) and returns a sorted, de-duplicated list of all the words contained within.</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> sortedWords x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; x |&gt;</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.map words |&gt;&nbsp; <span style="COLOR: green">// Get the words from each string</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.concat |&gt;&nbsp; &nbsp; <span style="COLOR: green">// No need for the outer list</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.of_list |&gt;&nbsp; &nbsp; <span style="COLOR: green">// Create a set from the list</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.to_list&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Create a list from the set</span> </p></div>

<p align="left">We have a new operator in the above code, and it’s one you’ll see a lot in F# code: the pipeline operator, “<span face="Courier New">|&gt;</span>”. This operator is actually conceptually very simple, but makes it possible to create elegant code where the data flows from the beginning of the pipeline to its end, going from function to function.</p>

<p align="left">This function is actually equivalent to this:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> sortedWords x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.map words x |&gt;&nbsp; <span style="COLOR: green">// Get the words from each string</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.concat |&gt;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// No need for the outer list</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.of_list |&gt;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create a set from the list</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.to_list&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create a list from the set</span></p></div>

<p align="left">The choice is really about readability. To understand the pipeline operator, it can help to see its definition:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> (|&gt;) x f = f x</p></div>

<p align="left">This means our code is actually equivalent to the (in my opinion) less readable:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> sortedWords x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.to_list (Set.of_list (List.concat (List.map words x)))</p></div>

<p align="left">If you look back to the definition of words, we see it follows a similar form. Let’s re-write it to use pipelines:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> words =</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> seps = <span style="COLOR: maroon">&quot; \t~`!@#$%^&amp;*()-=_+{}|[]\\;'’:\&quot;&lt;&gt;?,./&quot;</span></p>

<p style="MARGIN: 0px">&nbsp; seps.ToCharArray() |&gt; Array.to_list |&gt; String.split </p></div>

<p align="left">Now let’s go back to our <span face="Courier New">sortedWords</span> definition. In all versions of the function we start by passing our only argument – <span face="Courier New">x</span>, a list of strings – into a function that “maps” a function across a list, creating a list containing the results of the various operations. Here’s a simple example using the <span face="Courier New">inc</span> function we created earlier:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> f a b = a + b</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> inc = f 1</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> x = List.map inc [1; 2; 3; 4; 5; 6; 7]</p></div>

<p align="left">When we load the code into FSI and check the value of x, we see:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">val f : int -&gt; int -&gt; int</p>

<p style="MARGIN: 0px">val inc : (int -&gt; int)</p>

<p style="MARGIN: 0px">val x : int list</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&gt; x;;</p>

<p style="MARGIN: 0px">val it : int list = [2; 3; 4; 5; 6; 7; 8] </p></div>

<p align="left">As you can see, the <span face="Courier New">map</span> function applies a function (in our case <span face="Courier New">inc</span>) to each member of the input list and has placed the results in an output list of the same length.</p>

<p align="left">If we really wanted to get clever we could use a lambda (i.e. anonymous) function to avoid having to define <span face="Courier New">inc</span> in the first place:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">let</span> x = List.map (<span style="COLOR: blue">fun</span> i <span style="COLOR: blue">-&gt;</span> i+1) [1; 2; 3; 4; 5; 6; 7]</p></div>

<p align="left">Lambda functions are just one example of a functional programming concept that has made its way into a mainstream, general-purpose language such as C#.</p>

<p align="left">Back to our pipeline... as we’re mapping the <span face="Courier New">words</span> function to a list, and the words function itself returns a list, each time it is called, we’re going to have a list of lists of strings on our hands. So we’re going to call <span face="Courier New">List.concat</span> to concatenate the contents of the outer list – we therefore end up with a single list of all the words across our strings.</p>

<p align="left">This “gross” list of words then gets passed into the <span face="Courier New">Set.of_list</span> function, which creates a set from our list, and sorts/de-duplicates it in the process. Very handy.</p>

<p align="left">As we want to get back to a list of words on the output, we simply call <span face="Courier New">Set.to_list</span> on our set to generate our “net” list.</p>

<p align="left">Here’s the <span face="Courier New">sortedWords</span> function working on a simple list of strings:</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&gt; sortedWords [&quot;this is my first sentence&quot;; &quot;this is my second sentence&quot;; &quot;this is my third and final sentence&quot;];;</p>

<p style="MARGIN: 0px">val it : string list</p>

<p style="MARGIN: 0px">= [&quot;and&quot;; &quot;final&quot;; &quot;first&quot;; &quot;is&quot;; &quot;my&quot;; &quot;second&quot;; &quot;sentence&quot;; &quot;third&quot;; &quot;this&quot;]</p></div>

<p align="left">Now all that remains is to create a command inside AutoCAD which passes the various objects in a drawing to this function.<br /><br /><em>To be continued... :-)</em></p>

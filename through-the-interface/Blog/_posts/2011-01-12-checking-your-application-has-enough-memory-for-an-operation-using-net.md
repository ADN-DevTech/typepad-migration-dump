---
layout: "post"
title: "Checking your application has enough memory for an operation using .NET"
date: "2011-01-12 06:08:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Kinect"
  - "Runtime"
original_url: "https://www.keanw.com/2011/01/checking-your-application-has-enough-memory-for-an-operation-using-net.html "
typepad_basename: "checking-your-application-has-enough-memory-for-an-operation-using-net"
typepad_status: "Publish"
---

<p>From time to time I listen to the <a href="http://www.dotnetrocks.com" target="_blank">.NET Rocks</a> show – I don’t get the chance to listen every week, but I do browse through the RSS items I get for the show episodes and find some time to put those I find of particular interest on in the background while I’m working on something else.</p>
<p><a href="http://www.dotnetrocks.com/default.aspx?showNum=627" target="_blank">Yesterday’s episode</a> caught my eye, as it focused on <a href="http://nkinect.codeplex.com/" target="_blank">the nKinect project</a>, exposing the Kinect’s capabilities to .NET. And ’m really (and I mean <em>really</em>) excited about the Xbox Kinect. I mentioned it in passing in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/03/reality-check.html" target="_blank">a post from last March</a> (it was still called Project Natal, back then) and it’s pretty clear to me that this device – and those like it – are set to change the industry. I’ve had a Kinect on order since the beginning of December (frustratingly you can’t get the Xbox 360 S 250Gb Kinect bundle, here, <a href="http://idioms.thefreedictionary.com/for+love+nor+money" target="_blank">for love nor money</a>), but in the meantime I’m having to be content with seeing what other people are managing to do with them. In particular <a href="http://idav.ucdavis.edu/~okreylos/ResDev/Kinect/" target="_blank">the work of Oliver Kreylos</a> is really worth checking out. There there&#39;s all the fun stuff on <a href="http://kinecthacks.net" target="_blank">Kinect Hacks</a>... from the ability to <a href="http://kinecthacks.net/minority-report-software-for-kinect/" target="_blank">control applications, Minority Report-style</a> to the potentially even more useful ability to&#0160;<a href="http://kinecthacks.net/transform-yourself-into-a-japanese-superhero/" target="_blank">transform yourself into a Japanese Super-Hero</a>. :-)</p>
<p>While it’s the nKinect interview – which turned out to be a little disappointing due to its slight lack of fluidity – that caused me to listen to the episode, there was one piece of information earlier in the show that really got my attention: in the regular “Better Know a Framework” segment, Carl Franklin mentioned <a href="http://msdn.microsoft.com/en-us/library/system.runtime.memoryfailpoint.aspx" target="_blank">System.Runtime.MemoryFailPoint</a>, a .NET Framework class that can be used to check for available memory prior to calling a memory-consuming operation.</p>
<p>I decided to give it a go, wrapping it into a helper function to simplify the memory check.</p>
<p>Here’s the C# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> MemoryHog</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MemoryConsumingApp</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Helper function to check for adequate memory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> CanGetMemory(</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> megabytes)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MemoryFailPoint</span><span style="line-height: 140%;"> mfp = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MemoryFailPoint</span><span style="line-height: 140%;">(megabytes);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">InsufficientMemoryException</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;MEM&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CheckForMemoryBeforeRunning()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Ask for the amount of memory for which to check</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptIntegerOptions</span><span style="line-height: 140%;"> pio =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">PromptIntegerOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nEnter amount of memory (in megabytes) to check for: &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pio.AllowNegative = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pio.AllowNone = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pio.AllowZero = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptIntegerResult</span><span style="line-height: 140%;"> pir = ed.GetInteger(pio);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Check for the memory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> canProceed = CanGetMemory(pir.Value);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\n{0}ufficient memory to complete operation.&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; canProceed ? </span><span style="line-height: 140%; color: #a31515;">&quot;S&quot;</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #a31515;">&quot;Ins&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!canProceed) </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Perform operation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// ...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>I ended up adding some code to prompt the user for the amount of memory (in megabytes) for which to check, mainly as it gives the chance to vary the amount and is therefore more interesting to play around with. In your applications you’ll have the interesting job of calculating the amount of memory a particular task will require.</p>
<p>I structured the code at the end of the command to show the kind of logic I would use to test for sufficient memory and then return from the command, should it not be available.</p>
<p>Here’s some fun I had using this MEM command to determine how much memory is available to my application during an AutoCAD session with various other applications running on my 32-bit Vista system:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command: <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">50</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">100</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">150</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">300</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">200</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">250</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">280</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">290</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">295</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">298</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">299</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">300</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">301</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">310</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">320</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">330</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">340</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">360</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">380</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">400</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">500</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">450</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">470</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">480</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">490</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">485</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">483</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">482</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">481</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Insufficient memory to complete operation.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Command:&#0160; <span style="color: #ff0000;">MEM</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Enter amount of memory (in megabytes) to check for: <span style="color: #ff0000;">480</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Sufficient memory to complete operation.</span></p>
</div>
<p>I tried to zero in on the amount of memory available, but as you can see (and as is logical), this amount fluctuates according to (or inversely to, I suppose) memory usage. I started zeroing in on a number at around 300Mb, but the number I eventually ended up with was 480Mb (and this was after a session where the application only had around 70Mb available to it).</p>
<p>This really isn’t intended as an interactive tool to determine what memory is available, but I thought I’d just mention these results as an aside.</p>

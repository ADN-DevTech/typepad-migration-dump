---
layout: "post"
title: "Performance Profiling"
date: "2010-03-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Algorithm"
  - "Debugging"
  - "Filters"
  - "Performance"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/03/performance-profiling.html "
typepad_basename: "performance-profiling"
typepad_status: "Publish"
---

<p>I should be preparing my Revit API training in Warsaw, which is coming up next week, and instead I still find myself working on other more interesting topics such as performance profiling at the same time.
I hope the training participants will find this interesting as well.

<h4>New Filtering Motivates Benchmarking</h4>

<p>The reason why I am so urgently interested in this right now is that the Revit 2011 API provides a whole new filtering API, i.e. for extracting certain specific groups of elements from the Revit database.
This is a task that almost every application faces.
The new API is extremely powerful and flexible and provides a huge number of classes and methods that can be used and put together in many different ways to achieve the same result.
I am very interested in discovering how to make optimal use of this, and you should probably be too.
One important tool in order to be able to measure and compare the performance of different approaches is a profiler, which leads me to this post.

<h4>Quick and Slow Filters</h4>

<p>Actually, I am rather getting ahead of myself here, because there are so many basic issues that should really be addressed and discussed first.
One of the most fundamental ones is that the Revit 2011 filtering API provides a number of filter classes, and they are divided into quick and slow filters.
A quick filter is fast and can process an element without fully expanding it in memory, whereas a slow filter needs to read in the entire element data into memory before it can be processed and thus requires more time.
Obviously, the trick in performant filtering in Revit 2011 is to apply as many and specific quick filters as possible before resorting to the slow ones, if your search requires them at all.
Once the filter has done its job, you have a collection of elements, and in some cases, you may want to postprocess these further to search for characteristics that are not directly supported by any filters, or harder to implement using them.
Anyway, we will discuss these topics more in depth real soon now.

<p>To give you a quick first impression of what Revit 2011 API filters can look like, here are two helper methods used in the code presented below.
The first one returns the first family symbol found with a built-in category OST_StructuralColumns, which we use to create lots of new column instances in the model:

<pre class="code">
<span class="teal">FamilySymbol</span> GetColumnType()
{
&nbsp; <span class="teal">FilteredElementCollector</span> columnTypes 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( m_document );
&nbsp;
&nbsp; columnTypes.OfCategory( 
&nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_StructuralColumns );
&nbsp;
&nbsp; columnTypes.OfClass( <span class="blue">typeof</span>( <span class="teal">FamilySymbol</span> ) );
&nbsp; <span class="blue">return</span> columnTypes.FirstElement() <span class="blue">as</span> <span class="teal">FamilySymbol</span>;
}
</pre>

<p>The second returns a list of all levels in the model:

<pre class="code">
<span class="teal">IList</span>&lt;<span class="teal">Level</span>&gt; GetLevels()
{
&nbsp; <span class="teal">FilteredElementCollector</span> a 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( m_document );
&nbsp;
&nbsp; a.OfCategory( <span class="teal">BuiltInCategory</span>.OST_Levels );
&nbsp; a.OfClass( <span class="blue">typeof</span>( <span class="teal">Level</span> ) );
&nbsp; <span class="blue">return</span> a.ToElements().Cast&lt;<span class="teal">Level</span>&gt;().ToList&lt;<span class="teal">Level</span>&gt;();
}
</pre>

<p>Both of these use quick filters exclusively.

<h4>Profiling Tool</h4>

<p>So in order to enable you to immediately do some research on and profiling of the new filtering on your own, I want to get this basic profiling tool set up and available to you as soon as possible.
This post was prompted by Marcelo Quevedo of

<a href="http://www.hsb-cad.com">
hsbSOFT</a>, 

starting with the following conversation:

<p><strong>[M]</strong> I am investigating performance, because we received a huge Revit file and our framing Revit command is spending too much time on it.

<p>I am using a very manual mode to identify the delays. 
I created a timer by using the QueryPerformanceCounter and QueryPerformanceFrequency methods from the Windows Kernel32.dll.

<p>I call the timer and add the seconds for each call to some of the Revit API functions.

<p>I tried to use the JetBrains dotTrace  profiling tool for .Net, but it doesnt work with Revit 2011.
If you know of a profiling tool that works with Revit 2011, please let me know.

<p><strong>[J]</strong> Thank you very much for your nice manual profiling tools and examples!

<p>No, I do not know of a profiling tool that works for Revit, which is why I was curious.

<p>By the way, I am sure that you can simplify the calling and usage of the timer very significantly 
by some clever use of constructors and destructors.

<p><strong>[M]</strong> I followed your recommendations and changed the timer. 
Instead of using a clever destructor, I am using the System.IDisposable interface so that you can use the using statement to identify the delay of a source code 
portion. 

<p>I attached a C# project in which this manual profiling tool is used. 
This project defines a simple Revit command that creates hundreds of structural columns and groups them in order to test the delay in various Revit API methods.
Here are the resulting two groups:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133ec59f4ed970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133ec59f4ed970b" alt="Profiling model groups" title="Profiling model groups" src="/assets/image_22e0dc.jpg" border="0"  /></a> <br />

</center>

<p>Here are the columns, which are only visible individually when we zoom in a bit closer:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301310fffffdd970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301310fffffdd970c" alt="Profiling columns" title="Profiling columns" src="/assets/image_dd865c.jpg" border="0"  /></a> <br />

</center>

<p>The performance timer implementation makes use of the CodeProject

<a href="http://www.codeproject.com/KB/cs/highperformancetimercshar.aspx">
High-Performance Timer in C#</a>.

<p><strong>[J]</strong> I cleaned it up a bit more:

<ul>
<li>Modified the collection to use a dictionary instead of manually searching for entries by key.
<li>Rewrote the GetColumnType and GetLevels methods.
<li>Sorted the output by percentage of time.
</ul>

<p>Here is the implementation of the basic Timer class that we use:

<pre class="code">
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">Timer</span>
{
&nbsp; [<span class="teal">DllImport</span>( <span class="maroon">&quot;Kernel32.dll&quot;</span> )]
&nbsp; <span class="blue">private</span> <span class="blue">static</span> <span class="blue">extern</span> <span class="blue">bool</span> QueryPerformanceCounter(
&nbsp; &nbsp; <span class="blue">out</span> <span class="blue">long</span> lpPerformanceCount );
&nbsp;
&nbsp; [<span class="teal">DllImport</span>( <span class="maroon">&quot;Kernel32.dll&quot;</span> )]
&nbsp; <span class="blue">private</span> <span class="blue">static</span> <span class="blue">extern</span> <span class="blue">bool</span> QueryPerformanceFrequency(
&nbsp; &nbsp; <span class="blue">out</span> <span class="blue">long</span> lpFrequency );
&nbsp;
&nbsp; <span class="blue">private</span> <span class="blue">long</span> startTime, stopTime;
&nbsp; <span class="blue">private</span> <span class="blue">long</span> freq;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Constructor</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> Timer()
&nbsp; {
&nbsp; &nbsp; startTime = 0;
&nbsp; &nbsp; stopTime = 0;
&nbsp; &nbsp; <span class="blue">if</span>( !QueryPerformanceFrequency( <span class="blue">out</span> freq ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">Win32Exception</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;high-performance counter not supported&quot;</span> );
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Start the timer</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">void</span> Start()
&nbsp; {
&nbsp; &nbsp; <span class="teal">Thread</span>.Sleep( 0 ); <span class="green">// let waiting threads work</span>
&nbsp; &nbsp; QueryPerformanceCounter( <span class="blue">out</span> startTime );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green">Stop the timer </span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">void</span> Stop()
&nbsp; {
&nbsp; &nbsp; QueryPerformanceCounter( <span class="blue">out</span> stopTime );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Return the duration of the timer in seconds</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">double</span> Duration
&nbsp; {
&nbsp; &nbsp; <span class="blue">get</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> ( <span class="blue">double</span> ) ( stopTime - startTime )
&nbsp; &nbsp; &nbsp; &nbsp; / ( <span class="blue">double</span> ) freq;
&nbsp; &nbsp; }
&nbsp; }
}
</pre>

<p>Marcelo implemented a PerfTimer class add some syntactic sugar and the IDisposable wrapper to the Timer class and make it easier and more automatic to start and stop for a specific call with a minimum of effort and coding. Here is the PerfTimer implementation:

<pre class="code">
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">PerfTimer</span> : <span class="teal">IDisposable</span>
{
&nbsp; <span class="blue">private</span> <span class="blue">string</span> _key;
&nbsp; <span class="blue">private</span> <span class="teal">Timer</span> _timer;
&nbsp; <span class="blue">private</span> <span class="blue">double</span> _duration = 0;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Gets time in seconds</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">double</span> Duration
&nbsp; {
&nbsp; &nbsp; <span class="blue">get</span> { <span class="blue">return</span> _duration; }
&nbsp; }
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Performance timer</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;what_are_we_testing_here&quot;&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Key describing code to be timed</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="blue">public</span> PerfTimer( <span class="blue">string</span> what_are_we_testing_here )
&nbsp; {
&nbsp; &nbsp; _key = what_are_we_testing_here;
&nbsp; &nbsp; _timer = <span class="blue">new</span> <span class="teal">Timer</span>();
&nbsp; &nbsp; _timer.Start(); <span class="green">//starts the time</span>
&nbsp; }
&nbsp;
&nbsp; <span class="blue">void</span> <span class="teal">IDisposable</span>.Dispose()
&nbsp; {
&nbsp; &nbsp; <span class="green">// When the using statement block finishes, </span>
&nbsp; &nbsp; <span class="green">// the timer is stopped, and the time is </span>
&nbsp; &nbsp; <span class="green">// registered</span>
&nbsp;
&nbsp; &nbsp; _timer.Stop();
&nbsp; &nbsp; _duration = _timer.Duration;
&nbsp; &nbsp; <span class="teal">TimeRegister</span>.AddTime( _key, _duration );
&nbsp; }
}
</pre>

<p>After preparing all this, I noticed the following comment on the CodeProject Timer class: "System.Diagnostics.Stopwatch class: .NET 2.0 now provides this functionality as part of the framework. See class: System.Diagnostics.Stopwatch in System.dll."
I rewrote the PerfTimer class to make use of the built-in stopwatch instead of reinventing the wheel, and it now looks like this:

<pre class="code">
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">PerfTimer</span> : <span class="teal">IDisposable</span>
{
<span class="grey">&nbsp; #region Internal TimeRegistry class
&nbsp; // . . .
&nbsp; #endregion // Internal TimeRegistry class</span>
&nbsp;
&nbsp; <span class="blue">string</span> _key;
&nbsp; <span class="teal">Stopwatch</span> _timer;
&nbsp; <span class="blue">double</span> _duration = 0;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Performance timer</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;what_are_we_testing_here&quot;&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Key describing code to be timed</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="blue">public</span> PerfTimer( <span class="blue">string</span> what_are_we_testing_here )
&nbsp; {
&nbsp; &nbsp; _key = what_are_we_testing_here;
&nbsp; &nbsp; _timer = <span class="teal">Stopwatch</span>.StartNew();
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Automatic disposal when the the using statement </span>
&nbsp; <span class="gray">///</span><span class="green"> block finishes: the timer is stopped and the </span>
&nbsp; <span class="gray">///</span><span class="green"> time is registered.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">void</span> <span class="teal">IDisposable</span>.Dispose()
&nbsp; {
&nbsp; &nbsp; _timer.Stop();
&nbsp; &nbsp; _duration = _timer.Elapsed.TotalSeconds;
&nbsp; &nbsp; <span class="teal">TimeRegistry</span>.AddTime( _key, _duration );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> <span class="blue">void</span> Report()
&nbsp; {
&nbsp; &nbsp; <span class="teal">TimeRegistry</span>.WriteResults( _duration );
&nbsp; }
}
</pre>

<p>The internal TimeRegistry class was initially defined by Marcelo  and manages a collection of individual timer instances for measuring the time required by the various different Revit API methods.
I pretty much rewrote it from scratch in various iterations.
At the end of the session, it reports the total times of the various operations, for instance like this:

<pre>

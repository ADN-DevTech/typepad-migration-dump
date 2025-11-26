---
layout: "post"
title: "Command and Conquer When Switching Views"
date: "2011-02-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "Automation"
  - "External"
  - "News"
  - "Ribbon"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/02/command-and-conquer-when-switching-views.html "
typepad_basename: "command-and-conquer-when-switching-views"
typepad_status: "Publish"
---

<p>Today, February 3, is the first day of the 

<a href="http://en.wikipedia.org/wiki/Chinese_New_Year">
Chinese New Year</a> and the beginning of the spring festival, which lasts for fifteen days.

This is a year of the 

<a href="http://en.wikipedia.org/wiki/Rabbit_%28zodiac%29">
Rabbit zodiac sign</a>.

Here is a 

<a href="http://en.wikipedia.org/wiki/Chunlian">
duilian</a>, 

a pair of lines of poetry, or actually a special form called 

<a href="http://en.wikipedia.org/wiki/Chunlian">
chunlian</a>, used 

as a New Year's decoration that expresses happy and hopeful thoughts for the coming year sent us by Joe Ye from Beijing:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e23d0495970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e23d0495970b" alt="Year of the rabbit chunlian" title="Year of the rabbit chunlian" src="/assets/image_e788d6.jpg" border="0" /></a> <br />

</center>

<p>The meanings of these lines are:

<ul>
<li>Top: Lucky Star is shining.
<li>Left: Good family, good person, and good luck.
<li>Right: Abundant happiness, abundant fortune, and prosperous day.
</ul>

<p>Continuing our exploration of ways to automate Revit, Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a> recently 

presented his results of exploring the Revit ribbon internals using UISpy,

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/ribbon-spying-and-ui-automation.html">
driving Revit using UI Automation</a>, 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/subscribing-to-ui-automation-events.html">
subscribing to UI Automation events</a>, and

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/further-ideas-for-using-ui-automation.html">
automating Revit window switching</a>.

It is fascinating to be able to drive Revit pretty reliably from outside by simulating the user interaction with the ribbon and other user interface elements.

<p>Here is the source code and Visual Studio project for the sample application 

<span class="asset  asset-generic at-xid-6a00e553e1689788330148c8461833970c"><a href="http://thebuildingcoder.typepad.com/files/drivingrevitviauiautomation.zip">DrivingRevitViaUiAutomation</a></span>

that Rudolf used to implement the following three functionalities from a 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/ribbon-spying-and-ui-automation.html#app">
stand-alone executable</a>:

<ul>
<li>Open a Revit document.
<li>Close the active document with or without saving it.
<li>Switch the current ribbon tab.
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c8461e1a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c8461e1a970c image-full" alt="UI automation sample application with populated ribbon tabs" title="UI automation sample application with populated ribbon tabs" src="/assets/image_514b93.jpg" border="0" /></a> <br />

</center>

<p>He continues with some more thoughts on setting active windows and views:

<p>I suppose that setting the active view via GUI cannot be performed on command level, so it must be performed at application level.
Just imagine this situation: 
First, you press your button to call your command. 
Then, this command changes the view or the document.
If your command changes your document, your transaction (which is per document &ndash; also the undo stack is per document, of course) may become invalid.
In Revit, changing the view affects the availability of the 'ADD_INS_TAB'.

<p>In addition, it makes sense to invalidate the command context after switching the active view, situations can occur where the new view is of ViewType.Schedule.
In this sort of view, neither can external commands be executed nor will the OnIdling event be fired.

<p>So, how can we perform this, though?

<p>I suggest an approach which invokes a command twice, in fact.

<p>The idea is to provide an OnIdling event handler that invokes commands after setting the new active view or document.
For example, first you invoke your command.
In this command, you have two possibilities.
If the active view (and/or the active document) is right, do the work as usual.
If not, activate the OnIdling event and fill a global variable of type 'AutomationElement' with your corresponding command button. 
Next time Revit idles, this button will be invoked, so your command will be performed in the right document and view, without violating the transaction mechanism.

<p>After this, the global variable can be set to null and the OnIdling event handler can be deactivated again.

<p>It would of course also be possible to let the event remain alive all the time while Revit is running, but why waste performance?

<p>Another point:

<p>Invoking commands can be performed in other ways than by pressing buttons.
Using the IExternalCommand interface just means that an Execute method with the appropriate signature must be implemented:

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; <span class="teal">ExternalCommandData</span> revit,
&nbsp; <span class="blue">ref</span> <span class="teal">String</span> message,
&nbsp; <span class="teal">ElementSet</span> elements )
{
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
}
</pre>

<p>You could just as well define a placeholder Execute method and call another method with different parameters from it, such as this:

<pre class="code">
<span class="blue">public</span> <span class="teal">Result</span> Execute( <span class="teal">UIApplication</span> revit )
{
&nbsp; <span class="green">// do some useful stuff here&#8230;</span>
&nbsp;
&nbsp; <span class="blue">return</span> <span class="teal">Result</span>.Succeeded;
}
</pre>

<p>It could be invoked like this:

<pre class="code">
<span class="blue">void</span> application_Idling( 
&nbsp; <span class="blue">object</span> sender, 
&nbsp; IdlingEventArgs e )
{
&nbsp; <span class="teal">Application</span> app = sender <span class="blue">as</span> <span class="teal">Application</span>;
&nbsp;
&nbsp; <span class="teal">UIApplication</span> uiApp = <span class="blue">new</span> <span class="teal">UIApplication</span>( app );
&nbsp;
&nbsp; Command_Example cmdExample = <span class="blue">new</span> Command_Example();
&nbsp;
&nbsp; cmdExample.Execute( uiApp ); <span class="green">// invoke command here</span>
}
</pre>

<p>So, instead of a global 'AutomationElement' for the next button to be pressed, another type could be chosen, and no button needs to be pressed, which may be faster, by the way.
It may be necessary to set a pause between ending the first command and invoking the second one.
To be continued...</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e23d0375970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e23d0375970b image-full" alt="Happy New Year of the Rabbit" title="Happy New Year of the Rabbit" src="/assets/image_2941a3.jpg" border="0" /></a> <br />

</center>

<p>Happy New Year of the Rabbit!

<p><strong>Addendum:</strong> Arno&scaron;t L&ouml;bel adds that an alternative and simpler solution would be to subscribe to the ViewActivated event instead of Idling.  
This event is much better suited for this particular problem.
In addition, it is much less of a performance hit, because ViewActivated is raised only when a view is actually activated, while Idling can be raised several times per second.
Even if the handler does nothing at all, simply the fact that the code needs to go from native to managed and back to native again means a significant slowdown.

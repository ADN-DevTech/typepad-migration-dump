---
layout: "post"
title: "Adding a Stacked Split Button to the Ribbon"
date: "2015-12-16 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "External"
  - "Ribbon"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/12/adding-a-stacked-split-button-to-the-ribbon.html "
typepad_basename: "adding-a-stacked-split-button-to-the-ribbon"
typepad_status: "Publish"
---

<p>I have been rather busy on the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> these last few days.</p>

<p>One of the issues I got involved with was Michał Helt's thread
on <a href="http://forums.autodesk.com/t5/revit-api/ribbonpanel-addstackeditems-and-splitbutton/m-p/5949953">RibbonPanel.AddStackedItems and SplitButton</a>,
discussing how to add a split button to a ribbon panel using the AddStackedItems method.</p>

<p><strong>Question:</strong> Currently, Split Buttons cannot be created in the ribbon using the AddStackedItems method.
Only PushButtons, PulldownButtons, ComboBoxes and TextBoxes can be added this way.
Would it be possible to remove this limitation?
I would like to achieve a button similar to this selected one:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c7fb0e38970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c7fb0e38970b img-responsive" style="width: 438px; " alt="Split button" title="Split button" src="/assets/image_6a31e6.jpg" /></a><br /></p>

<p></center></p>

<p>I've already overcome this using the ribbon features from AdWindows.dll, but I am curious whether the lack of it in RevitAPIUI.dll is intentional or not.</p>

<p><strong>Answer:</strong> Thank you for reporting this!</p>

<p>This issue has already been logged as REVIT-71373 <em>As a Revit add-in developer, I would like to be able to add a split button to a stacked section of buttons, so I can create the UI I need for my application</em> and is currently being addressed, so you will have official access to this functionality in future.</p>

<p><strong>Response:</strong> Thank you.</p>

<p>Here is my current workaround making use of
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#4">unsupported</a> <code>AdWindows.dll</code> functionality:</p>

<pre class="code">
&nbsp; <span class="blue">var</span> bd0 = <span class="blue">new</span> <span class="teal">PulldownButtonData</span>( <span class="maroon">&quot;A&quot;</span>, <span class="maroon">&quot;A&quot;</span> );
&nbsp; <span class="blue">var</span> bd1 = <span class="blue">new</span> <span class="teal">PulldownButtonData</span>( <span class="maroon">&quot;B&quot;</span>, <span class="maroon">&quot;B&quot;</span> );
&nbsp; <span class="blue">var</span> bd2 = <span class="blue">new</span> <span class="teal">PulldownButtonData</span>( <span class="maroon">&quot;C&quot;</span>, <span class="maroon">&quot;C&quot;</span> );
&nbsp;
&nbsp; <span class="blue">var</span> stackedItems = ribbonPanel.AddStackedItems(
&nbsp; &nbsp; bd0, bd1, bd2 );
&nbsp;
&nbsp; <span class="blue">var</span> button0 = (<span class="teal">PulldownButton</span>) stackedItems[0];
&nbsp;
&nbsp; <span class="blue">string</span> sid = <span class="blue">string</span>.Join(
&nbsp; &nbsp; <span class="maroon">&quot;%&quot;</span>,
&nbsp; &nbsp; <span class="maroon">&quot;CustomCtrl_&quot;</span>,
&nbsp; &nbsp; <span class="maroon">&quot;CustomCtrl_&quot;</span>,
&nbsp; &nbsp; ribbonTabName,
&nbsp; &nbsp; ribbonPanel.Name,
&nbsp; &nbsp; button0.Name );
&nbsp;
&nbsp; <span class="blue">var</span> splitButton = (Autodesk.Windows.<span class="teal">RibbonSplitButton</span>)
&nbsp; &nbsp; UIFramework.RevitRibbonControl.RibbonControl
&nbsp; &nbsp; &nbsp; .findRibbonItemById( sid );
&nbsp;
&nbsp; splitButton.IsSplit = <span class="blue">true</span>;
&nbsp; splitButton.IsSynchronizedWithCurrentItem = <span class="blue">true</span>;
</pre>

<p>That's the cleanest solution that I have found so far.</p>

<p>Thank you, Michał, for raising the issue and sharing this solution.</p>

<p>Don't forget to replace this by the official solution once the new functionality mentioned above becomes available.</p>

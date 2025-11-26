---
layout: "post"
title: "24x24 StackedItem, Ribbon Utils, Journal Encoding"
date: "2020-02-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "I18n"
  - "Journal"
  - "Ribbon"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/02/24x24-stackeditem-ribbonitem-utils-and-journal-encoding.html "
typepad_basename: "24x24-stackeditem-ribbonitem-utils-and-journal-encoding"
typepad_status: "Publish"
---

<p>Let's start the week with some ribbon button item and encoding topics:</p>

<ul>
<li><a href="#2">How to create 24x24 stacked ribbon items</a></li>
<li><a href="#3">Update on moving a ribbon button between panels</a></li>
<li><a href="#4">Revit journal file character encoding</a></li>
</ul>

<h4><a name="2"></a>How to Create 24x24 Stacked Ribbon Items</h4>

<p>Jameson <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4918309">jnyp</a> Nyp raised and solved this issue in 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/24x24-stackeditems/m-p/9337695">24x24 StackedItems</a>:</p>

<p><strong>Question:</strong> This may be an easy one, but so far I am struggling to find anything specific about it.
How do you make a <code>StackedItem</code> where the icons are 24x24 when there are only 2 in the stack?
It seems like it should be possible as it is used multiple times in the modify tab (see example below).</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4eb1e72200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4eb1e72200d img-responsive" style="width: 246px; display: block; margin-left: auto; margin-right: auto;" alt="Icon sizes" title="Icon sizes" src="/assets/image_935fe3.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Icon sizes</p>

<p></center></p>

<p>I have been able to set the <code>ShowText</code> property to false to get the 3 stacked icons, but when I use the same methodology with the 2 icon stack it remains 16x16 regardless of the icon resolution.
I have tried to obtain and change the button's height and width, minWidth and minHeight through the Autodesk.Window.RibbonItem object to no avail.
Has anyone had any success in creating these icons?</p>

<p><strong>Solution:</strong> I found a solution.
In order to display the button at the 24x24 size the Autodesk.Windows.RibbonItem.Size needs to be manually set to Autodesk.Windows.RibbonItemSize.Large enum and a 24x24 icon needs to be set to the LargeImage property of the button.
I have included a code example below.
Forgive me for any poor coding techniques.
I am only a couple months into my C# developer life.</p>

<pre class="code">
<span style="color:blue;">using</span>&nbsp;Autodesk.Revit.UI;
<span style="color:blue;">using</span>&nbsp;Autodesk.Windows;
<span style="color:blue;">using</span>&nbsp;System.Collections.Generic;
<span style="color:blue;">using</span>&nbsp;System.IO;
<span style="color:blue;">using</span>&nbsp;System.Reflection;
<span style="color:blue;">using</span>&nbsp;System.Windows.Media.Imaging;
<span style="color:blue;">using</span>&nbsp;YourCustomUtilityLibrary;

<span style="color:blue;">namespace</span>&nbsp;ReallyCoolAddin
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">StackedButton</span>
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">IList</span>&lt;Autodesk.Revit.RibbonItem&gt;&nbsp;Create(&nbsp;<span style="color:#2b91af;">RibbonPanel</span>&nbsp;ribbonPanel&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;Assembly</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">Assembly</span>&nbsp;assembly&nbsp;=&nbsp;<span style="color:#2b91af;">Assembly</span>.GetExecutingAssembly();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;assemblyLocation&nbsp;=&nbsp;assembly.Location;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Get&nbsp;DLL&nbsp;Location</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;executableLocation&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.GetDirectoryName(&nbsp;assemblyLocation&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;dllLocationTest&nbsp;=&nbsp;<span style="color:#2b91af;">Path</span>.Combine(&nbsp;executableLocation,&nbsp;<span style="color:#a31515;">&quot;TestDLLName.dll&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Image</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb1Image&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.16x16_Button1.ico&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb2Image&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.16x16_Button2.ico&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb1LargeImage&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.24x24_Button1.ico&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">BitmapSource</span>&nbsp;pb2LargeImage&nbsp;=&nbsp;UTILImage.GetEmbeddedImage(&nbsp;assembly,&nbsp;<span style="color:#a31515;">&quot;Resources.24x24_Button2.ico&quot;</span>&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Button&nbsp;Name</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;buttonName1&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ButtonTest1&quot;</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;buttonName2&nbsp;=&nbsp;<span style="color:#a31515;">&quot;ButtonTest2&quot;</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;push&nbsp;buttons</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;buttondata1&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;buttonName1,&nbsp;buttonTextTest,&nbsp;dllLocationTest,&nbsp;<span style="color:#a31515;">&quot;Command1&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata1.Image&nbsp;=&nbsp;pb1Image;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata1.LargeImage&nbsp;=&nbsp;pb1LargeImage;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">PushButtonData</span>&nbsp;buttondata2&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">PushButtonData</span>(&nbsp;buttonName2,&nbsp;buttonTextTest,&nbsp;dllLocationTest,&nbsp;<span style="color:#a31515;">&quot;Command2&quot;</span>&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata2.Image&nbsp;=&nbsp;pb2Image;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;buttondata2.LargeImage&nbsp;=&nbsp;pb2LargeImage;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Create&nbsp;StackedItem</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;Autodesk.Revit.RibbonItem&gt;&nbsp;ribbonItem&nbsp;=&nbsp;ribbonPanel.AddStackedItems(&nbsp;buttondata1,&nbsp;buttondata2&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Find&nbsp;Autodes.Windows.RibbonItems</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UTILRibbonItem&nbsp;utilRibbon&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;UTILRibbonItem();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;btnTest1&nbsp;=&nbsp;utilRibbon.getButton(&nbsp;<span style="color:#a31515;">&quot;Tab&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Panel&quot;</span>,&nbsp;buttonName1&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;btnTest2&nbsp;=&nbsp;utilRibbon.getButton(&nbsp;<span style="color:#a31515;">&quot;Tab&quot;</span>,&nbsp;<span style="color:#a31515;">&quot;Panel&quot;</span>,&nbsp;buttonName2&nbsp;);

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Set&nbsp;Size&nbsp;and&nbsp;Text&nbsp;Visibility</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest1.Size&nbsp;=&nbsp;<span style="color:#2b91af;">RibbonItemSize</span>.Large;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest1.ShowText&nbsp;=&nbsp;<span style="color:blue;">false</span>;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest2.Size&nbsp;=&nbsp;<span style="color:#2b91af;">RibbonItemSize</span>.Large;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;btnTest2.ShowText&nbsp;=&nbsp;<span style="color:blue;">false</span>;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:green;">//&nbsp;Return&nbsp;StackedItem</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;ribbonItem;
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
}
</pre>

<p><strong>Question:</strong> Hi Jameson, in your code above, you use a <code>UTILRibbonItema</code> class.</p>

<p>What is the that?</p>

<p>I wasn't able to find it anywhere on the Internet.</p>

<p><strong>Answer:</strong> The <code>UTILRibbonItem</code> class is a helper class that I use to go find RibbonItems through the Autodesk.Windows (AW) API.</p>

<p>It goes in to the AW and recursively searches through the tabs, panels and buttons to find the button you feed to it.</p>

<p>Taking all of the logic and putting it in its own class allows for easier reuse.</p>

<p>A larger discussion of what that class contains can be found in the other discussion thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/add-a-new-custom-ribbon-panel-to-a-revit-built-in-tab/td-p/5538772">adding a new custom ribbon panel to a Revit built-in tab</a></p>

<p>Here is a basic implementation:</p>

<pre class="code">
  <span style="color:blue;">using</span>&nbsp;AW&nbsp;=&nbsp;Autodesk.Windows;

  <span style="color:blue;">public</span>&nbsp;AW.<span style="color:#2b91af;">RibbonItem</span>&nbsp;GetButton(&nbsp;<span style="color:blue;">string</span>&nbsp;tabName,&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;panelName,&nbsp;<span style="color:blue;">string</span>&nbsp;itemName&nbsp;)
  {
  &nbsp;&nbsp;AW.<span style="color:#2b91af;">RibbonControl</span>&nbsp;ribbon&nbsp;=&nbsp;AW.<span style="color:#2b91af;">ComponentManager</span>.Ribbon;
  &nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;AW.<span style="color:#2b91af;">RibbonTab</span>&nbsp;tab&nbsp;<span style="color:blue;">in</span>&nbsp;ribbon.Tabs&nbsp;)
  &nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;tab.Name&nbsp;==&nbsp;tabName&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;AW.<span style="color:#2b91af;">RibbonPanel</span>&nbsp;panel&nbsp;<span style="color:blue;">in</span>&nbsp;tab.Panels&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;panel.Source.Title&nbsp;==&nbsp;panelName&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;panel.FindItem(&nbsp;<span style="color:#a31515;">&quot;CustomCtrl_%CustomCtrl_%&quot;</span>&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;+&nbsp;tabName&nbsp;+&nbsp;<span style="color:#a31515;">&quot;%&quot;</span>&nbsp;+&nbsp;panelName&nbsp;+&nbsp;<span style="color:#a31515;">&quot;%&quot;</span>&nbsp;+&nbsp;itemName,&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">true</span>&nbsp;)&nbsp;<span style="color:blue;">as</span>&nbsp;AW.<span style="color:#2b91af;">RibbonItem</span>;
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;&nbsp;&nbsp;}
  &nbsp;&nbsp;}
  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:blue;">null</span>;
  }
</pre>

<p>Just beware that the AW API is not a documented API, so use it at your own risk as it can be changed without letting anyone know.</p>

<p>Many thanks to Jameson for implementing and sharing this nice clean solution, and congratulations on such prowess "only a couple months into his C# developer life"!</p>

<h4><a name="3"></a>Update on Moving a Ribbon Button Between Panels</h4>

<p>Jameson's links above prompted me to revisit 
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/add-a-new-custom-ribbon-panel-to-a-revit-built-in-tab/td-p/5538772">adding a new custom ribbon panel to a Revit built-in tab</a> 
that I referred to here on the blog in 2014 in the article
on <a href="https://thebuildingcoder.typepad.com/blog/2014/07/moving-an-external-command-button-within-the-ribbon.html">moving an external command button within the ribbon</a>.</p>

<p>I noticed that the thread was updated after the initial publication.
Above all, the link to the sample code provided back then is no longer valid, so here is
a <a href="https://thebuildingcoder.typepad.com/files/ribbonmoveexample.zip">local copy of it, RibbonMoveExample.zip</a>.</p>

<p>Thanks again to Scott for sharing it back then.</p>

<h4><a name="4"></a>Revit Journal File Character Encoding</h4>

<p>Finally, a quick note on the Revit journal file character encoding shared
by Андрей <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/7264445">apavlovY5SDS</a> Павлов (Andrey Pavlov) in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-journal-file-encoding/m-p/9330501">Revit journal file encoding</a>:</p>

<p><strong>Question:</strong> What is the Revit journal file encoding?</p>

<p>The default file path is <em>C:\Users\ {Username} \AppData\Local\Autodesk\Revit\Autodesk Revit 2020\Journals</em>.</p>

<p>I have trouble decoding Cyrillic characters.</p>

<p><strong>Answer:</strong> Windows-1251, confidence 0.9824519, tested
with <a href="https://github.com/errepi/ude">errepi/ude</a>,
a C# port of the Mozilla Universal Charset Detector.</p>

<p>Many thanks to Andrey for raising and clarifying this issue.</p>

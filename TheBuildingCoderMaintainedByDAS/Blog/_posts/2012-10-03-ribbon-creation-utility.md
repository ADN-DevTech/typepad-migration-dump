---
layout: "post"
title: "Ribbon Creation Utility"
date: "2012-10-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Ribbon"
  - "User Interface"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/10/ribbon-creation-utility.html "
typepad_basename: "ribbon-creation-utility"
typepad_status: "Publish"
---

<p>Creating and populating a ribbon panel is not a very complicated matter, although it normally does require instantiating and manipulating quite a number of different cooperating class instances.

<p>Now Victor Chekalin, or Виктор Чекалин, presents a much simpler solution for this using dedicated wrapper classes.

<p>In his own words:

<a name="2"></a>

<h4>Create Buttons without Effort</h4>

<p>As you know, you can create your own buttons or even your own tab in the Revit ribbon to perform a command. 

<p>For me, creating button always wasn't easy. 
Especially  I didn't like to pass the assembly location and name of the external Command class to perform when I click the button. 
Also, the standard API requires me to create an ImageSource for each button image while I have an image stored in the assembly resources.

<p>To avoid these troubles and ease my life I decided to create a utility which helps me to create my Revit ribbon buttons. 
The utility I created is really useful for me and I want to share it with everybody.

<p>Here are the main features:

<ul>
<li>Fluent interface:
You can create all your buttons in one single line of code. 
<li>Command name as generic parameter:
You don't need to write the command name as text and set the assembly location.
<li>Images from resource:
You can easily use images from resources.
</ul>

<p>The sample below illustrates all this.

<p>This is the result in the Revit ribbon:</p>
 
<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d3c755b3c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d3c755b3c970c" alt="Victor's ribbon utility sample" title="Victor's ribbon utility sample" src="/assets/image_0c14d8.jpg" border="0" /></a><br />

</center>

<p>Look at the source  code generating it:

<pre class="code">
&nbsp; <span class="teal">Ribbon</span> ribbon = <span class="blue">new</span> <span class="teal">Ribbon</span>( a );
&nbsp;
&nbsp; ribbon.Tab( <span class="maroon">&quot;MyTab&quot;</span> )
&nbsp; &nbsp; .Panel( <span class="maroon">&quot;Panel1&quot;</span> )
&nbsp;
&nbsp; &nbsp; .CreateButton( <span class="maroon">&quot;btn1&quot;</span>, <span class="maroon">&quot;Button1&quot;</span>, 
&nbsp; &nbsp; &nbsp; <span class="blue">typeof</span>( Command1 ),
&nbsp; &nbsp; &nbsp; btn =&gt; btn
&nbsp; &nbsp; &nbsp; &nbsp; .SetLargeImage( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Resources._1348119708_face_monkey_32 )
&nbsp; &nbsp; &nbsp; &nbsp; .SetSmallImage( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Resources._1348119708_face_monkey_16 ) )
&nbsp;
&nbsp; &nbsp; .CreateSeparator()
&nbsp;
&nbsp; &nbsp; .CreateButton&lt;Command2&gt;( <span class="maroon">&quot;btn2&quot;</span>, <span class="maroon">&quot;Button2&quot;</span>,
&nbsp; &nbsp; &nbsp; btn =&gt; btn
&nbsp; &nbsp; &nbsp; &nbsp; .SetLongDescription( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;This is a description of the button2&quot;</span> )
&nbsp; &nbsp; &nbsp; &nbsp; .SetLargeImage( 
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Resources._1348119643_face_glasses_32 ) )
&nbsp;
&nbsp; &nbsp; .CreateStackedItems( si =&gt; si
&nbsp;
&nbsp; &nbsp; &nbsp; .CreateButton&lt;Command3&gt;( <span class="maroon">&quot;btn3&quot;</span>, <span class="maroon">&quot;Button3&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; btn =&gt; btn.SetSmallImage( Resources
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ._1348119594_preferences_system_16 ) )
&nbsp;
&nbsp; &nbsp; &nbsp; .CreateButton&lt;Command4&gt;( <span class="maroon">&quot;btn4&quot;</span>, <span class="maroon">&quot;Button4&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; btn =&gt; btn.SetSmallImage( Resources
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ._1348119621_internet_web_browser_16 ) ) )
&nbsp;
&nbsp; &nbsp; .CreateSeparator()
&nbsp;
&nbsp; &nbsp; .CreateStackedItems( si =&gt; si
&nbsp;
&nbsp; &nbsp; &nbsp; .CreateButton&lt;Command3&gt;( <span class="maroon">&quot;btn3_1&quot;</span>, <span class="maroon">&quot;Button3&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; btn =&gt; btn.SetSmallImage( Resources
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ._1348119594_preferences_system_16 ) )
&nbsp;
&nbsp; &nbsp; &nbsp; .CreateButton&lt;Command4&gt;( <span class="maroon">&quot;btn4_1&quot;</span>, <span class="maroon">&quot;Button4&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; btn =&gt; btn.SetSmallImage( Resources
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ._1348119621_internet_web_browser_16 ) )
&nbsp;
&nbsp; &nbsp; &nbsp; .CreateButton&lt;Command1&gt;( <span class="maroon">&quot;btn1_1&quot;</span>, <span class="maroon">&quot;Button1&quot;</span>,
&nbsp; &nbsp; &nbsp; &nbsp; btn =&gt; btn.SetSmallImage( Resources
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ._1348119553_face_smile_big_16 ) ) );
</pre>

<p>Pretty easy, isn't it?

<p>At first I set the tab, where I want to create my buttons. 
I can specify my own tab or one of the system tabs, e.g. Autodesk.Revit.UI.Tab.AddIns. 
If the tab doesn't exist, it will be created. 
So, you can easily use a tab from a different add-in. 
You specify a panel to group the buttons on the tab. 
Next, the most important part, create the buttons. 
I won't describe how to create the buttons. 
The code is self-explanatory.

<p>My utility currently supports push button, push button as stacked item and separator. 

<p>Here is the 

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee3eab8ff970d"><a href="http://thebuildingcoder.typepad.com/files/vcrevitribbonutil.zip">compiled assembly</a></span>.

<p>The source code is available on 

<a href="https://github.com/chekalin-v/VCRevitRibbonUtil">
GitHub</a>.

You can also download it as a 

<a href="https://github.com/chekalin-v/VCRevitRibbonUtil/zipball/master">
zip archive file</a>.

<p>It provides the namespaces VCRevitRibbonUtil and VCRevitRibbonUtil.Helpers containing the following classes:

<ul>
<li>StackedItem
<li>Button
<li>Panel
<li>Ribbon
<li>Tab
<li>Helpers.BitmapSourceConverter
</ul>

<p>Many thanks to Victor for this neat time and labour saving utility!

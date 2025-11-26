---
layout: "post"
title: "Installing a Macro and Closing the Active Document"
date: "2012-12-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Macro"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/12/closing-the-active-document.html "
typepad_basename: "closing-the-active-document"
typepad_status: "Publish"
---

<p>You can close an open Revit document programmatically.
Unfortunately, though, this only works for all except the last one.

<p>People keep running into this issue, of course.
It even motivated Ren&eacute; Gerlach to implement a workaround using SendKeys.SendWait to send an Alt-F4 keystroke to Revit to

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html">
close the active window</a>,

and Arno&scaron;t L&ouml;bel to

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html#2">
warn gravely</a> against its use and even publish a

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/closing-the-active-document-and-why-not-to.html#3">
Windows message workaround disclaimer</a>.</p>

<p>Well, happily, such an adventurous workaround is not at all required.
Another workaround making use only of fully supported API calls is obvious and simple, once you think  of it: if you really want to close the current document, and it happens to be the last one, all you have to do is open some other document first.
You can use a dummy document for this purpose.
Once it has been opened, the other 'real' document can be closed.</p>

<p>Steven Mycynek of the Revit API development team provided a sample add-in named UiClose implementing this.
Here is the description in his own words:</p>

<blockquote>

<p>Enclosed is some sample code I use to close the active document through the API.
I am sure it has some limitations and bugs, but it's a nice start.

<p>To use, simply install the UiClose macro to your macros folder and place the _placeHolder_.rvt dummy model into C:/uiclose or some other location where the main macro can find it.

<p>The demo application closes the active document, opening _placeholder_.rvt when necessary, and will not accidentally close the placeholder document.

<p>It might also be possible to do something with events in an application to make this more automatic, but this wraps it up for now.

</blockquote>

<p>Steve implemented this as a SharpDevelop macro.</p>

<p>The macro files need to be placed in the appropriate

<a href="http://wikihelp.autodesk.com/Revit/enu/2013/Help/00001-Revit_He0/3032-Customiz3032/3207-Automati3207/3208-Getting_3208/3209-Upgradin3209">
macro directory</a>:</p>

<ul>
<li>Windows XP: C:\Documents and Settings\All Users\Application Data\Autodesk\Revit\Macros\&lt;release&gt;\&lt;product&gt;\VstaMacros</li>
<li>Windows 7: C:\ProgramData\Autodesk\Revit\Macros\&lt;release&gt;\&lt;product&gt;\VstaMacros</li>
</ul>

<p>In my case, this resolves to</p>

<ul>
<li>C:\Documents and Settings\All Users\Application Data\Autodesk\Revit\Macros\2013\Revit\VstaMacros</li>
</ul>

<p>Actually, to see the UiClose macro in the SharpDevelop IDE, I had to place the UiClose folder into the AppHookup subdirectory, so its full path ends up as:</p>

<ul>
<li>C:\Documents and Settings\All Users\Application Data\Autodesk\Revit\Macros\2013\Revit\VstaMacros\AppHookup\UiClose</li>
</ul>

<p>Now I see the UiClose module and its CloseActiveDocDemo macro in the Manage &gt; Macros &gt; Macro Manager:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee63517e2970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee63517e2970d image-full" alt="UiClose CloseActiveDocDemo macro" title="UiClose CloseActiveDocDemo macro" src="/assets/image_845339.jpg" border="0" /></a><br />

</center>

<p>When I run the macro, the current active document is closed.
If it is the last one, the placeholder document is opened in its stead.</p>

<p>If it has unsaved changes, a dialogue box prompting me whether to save or not is displayed.
The macro could obviously be improved to check for that before trying to close the document to avoid such an interruption, or handle it more gracefully.</p>

<p>The macro itself is a trivial one-liner, since it just calls the CloseAndSave method provided by the CloseHelper helper class:</p>

<pre class="code>">
&nbsp; <span class="blue">public</span> <span class="blue">void</span> CloseActiveDocDemo()
&nbsp; {
&nbsp; &nbsp; Autodesk.Revit.UI.<span class="teal">UIDocument</span> doc
&nbsp; &nbsp; &nbsp; = <span class="blue">this</span>.ActiveUIDocument;
&nbsp;
&nbsp; &nbsp; CloseHelper.CloseAndSave( doc );
&nbsp; }
</pre>

<p>The rest of the ThisApplication class methods deal with setting up and managing the helper class:</p>

<pre class="code>">
<span class="blue">public</span> <span class="blue">partial</span> <span class="blue">class</span> <span class="teal">ThisApplication</span>
{
&nbsp; <span class="blue">private</span> <span class="blue">void</span> Module_Startup(
&nbsp; &nbsp; <span class="blue">object</span> sender,
&nbsp; &nbsp; <span class="teal">EventArgs</span> e )
&nbsp; {
&nbsp; &nbsp; m_CloseHelper = <span class="blue">new</span> UiCloseHelper(
&nbsp; &nbsp; &nbsp; <span class="blue">this</span>,
&nbsp; &nbsp; &nbsp; <span class="maroon">@&quot;C:\\uiClose\\_placeholder_.rvt&quot;</span> );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">private</span> <span class="blue">void</span> Module_Shutdown(
&nbsp; &nbsp; <span class="blue">object</span> sender,
&nbsp; &nbsp; <span class="teal">EventArgs</span> e )
&nbsp; {
&nbsp; }
&nbsp;
<span class="blue">&nbsp; #region</span> Revit Macros generated code
&nbsp; <span class="blue">private</span> <span class="blue">void</span> InternalStartup()
&nbsp; {
&nbsp; &nbsp; <span class="blue">this</span>.Startup += <span class="blue">new</span> System.<span class="teal">EventHandler</span>(
&nbsp; &nbsp; &nbsp; Module_Startup );
&nbsp;
&nbsp; &nbsp; <span class="blue">this</span>.Shutdown += <span class="blue">new</span> System.<span class="teal">EventHandler</span>(
&nbsp; &nbsp; &nbsp; Module_Shutdown );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">public</span> UiCloseHelper CloseHelper
&nbsp; {
&nbsp; &nbsp; <span class="blue">get</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> m_CloseHelper;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="blue">private</span> UiCloseHelper m_CloseHelper;
<span class="blue">&nbsp; #endregion</span>
}
</pre>

<p>So finally, let's take a look at the UiCloseHelper implementation in all its glory:</p>

<pre class="code>">
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
<span class="gray">///</span><span class="green"> A helper class to keep a placeholder document </span>
<span class="gray">///</span><span class="green"> open in Revit to allow closing the active </span>
<span class="gray">///</span><span class="green"> document in Revit.</span>
<span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
<span class="blue">public</span> <span class="blue">class</span> <span class="teal">UiCloseHelper</span>
{
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Constructor -- initializes object with </span>
&nbsp; <span class="gray">///</span><span class="green"> a UIApplication and location of a </span>
&nbsp; <span class="gray">///</span><span class="green"> placeholder document.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;uiAppInterface&quot;&gt;</span><span class="green">The Revit UI Application, pass either a UIApplication or an ApplicationEntryPoint</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;placeholderPath&quot;&gt;</span><span class="green">Path to a dummy .rvt file.</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="blue">public</span> UiCloseHelper(
&nbsp; &nbsp; <span class="blue">dynamic</span> uiAppInterface,
&nbsp; &nbsp; <span class="blue">string</span> placeholderPath )
&nbsp; {
&nbsp; &nbsp; m_uiAppInterface = uiAppInterface;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( m_uiAppInterface == <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentNullException</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;m_uiAppInterface&quot;</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( ( !( uiAppInterface <span class="blue">is</span> Autodesk.Revit.UI.<span class="teal">UIApplication</span> ) )
&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; ( !( uiAppInterface <span class="blue">is</span> Autodesk.Revit.UI.Macros.<span class="teal">ApplicationEntryPoint</span> ) ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentException</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Must pass a UIApplication or ApplicationEntryPoint.&quot;</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; SetPlaceHolderPath( placeholderPath );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Closes a UIDocument with the same </span>
&nbsp; <span class="gray">///</span><span class="green"> interface as UIDocument.Close</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;uiDoc&quot;&gt;</span><span class="green">The document to close</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;returns&gt;</span><span class="green">True if successful, false otherwise.</span><span class="gray">&lt;/returns&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">bool</span> CloseAndSave(
&nbsp; &nbsp; Autodesk.Revit.UI.<span class="teal">UIDocument</span> uiDoc )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( uiDoc == <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentNullException</span>( <span class="maroon">&quot;uiDoc&quot;</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; RejectClosingPlaceHolder( uiDoc );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( !EnsureActivePlaceHolder( m_uiAppInterface ) )
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> uiDoc.SaveAndClose();
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Closes a DB.Document with the same </span>
&nbsp; <span class="gray">///</span><span class="green"> interface as DB.Document.Close</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;doc&quot;&gt;</span><span class="green">The DB.Document to close</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;returns&gt;</span><span class="green">True if successful, false otherwise.</span><span class="gray">&lt;/returns&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">bool</span> Close( Autodesk.Revit.DB.<span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="blue">return</span> Close( doc, <span class="blue">true</span> );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Closes a DB.Document with the same </span>
&nbsp; <span class="gray">///</span><span class="green"> interface as DB.Document.Close</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;doc&quot;&gt;</span><span class="green">The DB.Document to close</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;saveModified&quot;&gt;</span><span class="green">true to save the document if modified, false to not save.</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;returns&gt;</span><span class="green">True if successful, false otherwise.</span><span class="gray">&lt;/returns&gt;</span>
&nbsp; <span class="blue">public</span> <span class="blue">bool</span> Close(
&nbsp; &nbsp; Autodesk.Revit.DB.<span class="teal">Document</span> doc,
&nbsp; &nbsp; <span class="blue">bool</span> saveModified )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( doc == <span class="blue">null</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentNullException</span>( <span class="maroon">&quot;doc&quot;</span> );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; RejectClosingPlaceHolder( doc );
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( !EnsureActivePlaceHolder( m_uiAppInterface ) )
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> doc.Close( saveModified );
&nbsp; }
&nbsp;
<span class="blue">&nbsp; #region</span> Helper methods
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Throws an exception if the user </span>
&nbsp; <span class="gray">///</span><span class="green"> passes the placeholder document.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;doc&quot;&gt;</span><span class="green">The document to test to ensure it is not the placeholder document.</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="blue">private</span> <span class="blue">void</span> RejectClosingPlaceHolder(
&nbsp; &nbsp; Autodesk.Revit.DB.<span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( doc.Title == System.IO.<span class="teal">Path</span>.GetFileName(
&nbsp; &nbsp; &nbsp; <span class="blue">this</span>.m_placeHolderPath ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentException</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Cannot close placeholder doc: &quot;</span> + doc.Title );
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Throws an exception if the user </span>
&nbsp; <span class="gray">///</span><span class="green"> passes the placeholder document.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;doc&quot;&gt;</span><span class="green">The document to check</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="blue">private</span> <span class="blue">void</span> RejectClosingPlaceHolder(
&nbsp; &nbsp; Autodesk.Revit.UI.<span class="teal">UIDocument</span> uiDoc )
&nbsp; {
&nbsp; &nbsp; RejectClosingPlaceHolder( uiDoc.Document );
&nbsp; }
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Ensures that the placeholder document is open </span>
&nbsp; <span class="gray">///</span><span class="green"> and active to that another open document can be closed.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="blue">private</span> <span class="blue">bool</span> EnsureActivePlaceHolder(
&nbsp; &nbsp; <span class="blue">dynamic</span> application )
&nbsp; {
&nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( m_placeHolderDoc == <span class="blue">null</span> )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; m_placeHolderDoc = application
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OpenAndActivateDocument( m_placeHolderPath );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">else</span>
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; m_placeHolderDoc.SaveAndClose();
&nbsp; &nbsp; &nbsp; &nbsp; m_placeHolderDoc = application
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; .OpenAndActivateDocument( m_placeHolderPath );
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">true</span>;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">false</span>;
&nbsp; &nbsp; }
&nbsp; }
&nbsp;
&nbsp;
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> Sets the path of the placeholder document.</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;/summary&gt;</span>
&nbsp; <span class="gray">///</span><span class="green"> </span><span class="gray">&lt;param name=&quot;path&quot;&gt;</span><span class="green">Path to placeholder/dummy document.</span><span class="gray">&lt;/param&gt;</span>
&nbsp; <span class="blue">private</span> <span class="blue">void</span> SetPlaceHolderPath( <span class="blue">string</span> path )
&nbsp; {
&nbsp; &nbsp; m_placeHolderPath = path;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( !System.IO.<span class="teal">File</span>.Exists( m_placeHolderPath ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentException</span>(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;File not found: &quot;</span> + path );
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">if</span>( !( System.IO.<span class="teal">Path</span>.GetExtension( m_placeHolderPath ) != <span class="maroon">&quot;rvt&quot;</span> ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">throw</span> <span class="blue">new</span> <span class="teal">ArgumentException</span>( <span class="maroon">&quot;Placeholder: &quot;</span>
&nbsp; &nbsp; &nbsp; &nbsp; + path + <span class="maroon">&quot; is not a revit file.&quot;</span> );
&nbsp; &nbsp; }
&nbsp; }
<span class="blue">&nbsp; #endregion</span>
&nbsp;
<span class="blue">&nbsp; #region</span> Data
&nbsp; <span class="blue">private</span> Autodesk.Revit.UI.<span class="teal">UIDocument</span> m_placeHolderDoc;
&nbsp; <span class="blue">private</span> <span class="blue">string</span> m_placeHolderPath;
&nbsp; <span class="blue">private</span> <span class="blue">dynamic</span> m_uiAppInterface;
<span class="blue">&nbsp; #endregion</span>
}
</pre>

<p>It reads a lot harder than it actully is  :-)</p>

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e168978833017d3ec09abe970c"><a href="http://thebuildingcoder.typepad.com/files/uiclose.zip">UiClose.zip</a></span> 

containing the macro source code and its associated

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee63514ae970d"><a href="http://thebuildingcoder.typepad.com/files/_placeholder_.rvt">placeholder</a></span>

project document.

<p>Many thanks to Steve for providing this useful and interesting workaround!</p>

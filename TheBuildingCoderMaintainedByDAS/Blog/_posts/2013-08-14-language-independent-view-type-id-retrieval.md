---
layout: "post"
title: "Language Independent Section View Type Id Retrieval"
date: "2013-08-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Filters"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/08/language-independent-view-type-id-retrieval.html "
typepad_basename: "language-independent-view-type-id-retrieval"
typepad_status: "Publish"
---

<p>I already discussed

<a href="http://thebuildingcoder.typepad.com/blog/2013/01/changing-viewport-type.html">
changing the viewport type</a> and

mentioned Steve Mycynek's all-encompassing AU class

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/au-classes-on-the-view-mep-and-link-apis.html#2">
CP3133 Using the Revit Schedule and View APIs</a> explaining

everything else there is to know about the topic of generating new views.

<p>Here is an important language independence enhancement for this provided by H&aring;kan Wikemar of

<a href="http://www.aec.se">
AEC AB</a>, who asked:</p>

<p><strong>Question:</strong> I ran into a little problem trying to create some section views, in which a view type element id has to be specified:</p>

<pre class="code">
&nbsp; NyViewSectionB = <span class="teal">ViewSection</span>.CreateSection(
&nbsp; &nbsp; doc.Document, SectionId, bbNewSectionF );
</pre>

<p>When I try to find a section view family type element id, the section I was looking for was not always available in all projects, so the search failed.</p>

<p>I was making use of the GetElementByName method provided by Steven in his class.</p>


<p>When the section view type is present, it is sometimes not named 'Building Section' as in the example we looked at in the View Class at AU. It could be named 'Section 1' if there is no inserted section and whatever name I specified myself if I already created a section in the  project.</p>

<p>Is it at all possible to retrieve the section view type id consistently language independently instead of searching by name?</p>

<p>How should one handle this in a file without any sections available?</p>

<p>I would have preferred a Method just taking the an argument such as: 'ViewType.Section'.</p>

<p>Here are some sample view types from an existing project displayed by RevitLookup snoop:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901ec9678c970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901ec9678c970b" alt="Old view types" title="Old view types" src="/assets/image_d11a60.jpg" border="0" /></a><br />

</center>

<p>This is the corresponding list in a new project:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019104bf52da970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019104bf52da970c" alt="New view types" title="New view types" src="/assets/image_1412fb.jpg" border="0" /></a><br />

</center>

<p><strong>Answer:</strong> How about simply iterating over all view types and picking the first one of ViewType.Section?</p>

<!--
<p>In his AU Lab 'CP3133 - Using the Autodesk' Revit' Schedule and View APIs', Steven creates sections by using a dialog where you can specify a 'ViewTypeName'.</p>

<p>The given name is the used to call his GetElementByName helper method, defined like this:

<pre class="code">
&nbsp; <span class="blue">public</span> RevitElement GetElementByName&lt;RevitElement&gt;(
&nbsp; &nbsp; <span class="blue">string</span> name ) <span class="blue">where</span> RevitElement : <span class="teal">Element</span>
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">string</span>.IsNullOrEmpty( name ) )
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> fec
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( Doc() );
&nbsp;
&nbsp; &nbsp; fec.OfClass( <span class="blue">typeof</span>( RevitElement ) );
&nbsp;
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Element</span>&gt; elements = fec.ToElements();
&nbsp;
&nbsp; &nbsp; <span class="teal">Element</span> result = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; result = GetElementByName( elements, name );
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> result <span class="blue">as</span> RevitElement;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">private</span> <span class="teal">Element</span> GetElementByName(
&nbsp; &nbsp; <span class="teal">IList</span>&lt;<span class="teal">Element</span>&gt; elements,
&nbsp; &nbsp; <span class="blue">string</span> name )
&nbsp; {
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">Element</span> e <span class="blue">in</span> elements )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; System.Diagnostics.<span class="teal">Debug</span>.WriteLine( e.Name );
&nbsp; &nbsp; }
&nbsp;
&nbsp; &nbsp; <span class="teal">Element</span> result = <span class="blue">null</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">try</span>
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; result = elements.First(
&nbsp; &nbsp; &nbsp; &nbsp; element =&gt; element.Name == name );
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">catch</span>( <span class="teal">Exception</span> )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> result;
&nbsp; }
</pre>

-->

<p><strong>Response:</strong> Yes, absolutely.
The view name can vary depending on project.
I needed a more consistent way of getting a section definition element id.
I implemented the following and it seems to do the trick:</p>

<pre class="code">
&nbsp; <span class="teal">ElementId</span> SectionId = GetViewTypeIdByViewType(
&nbsp; &nbsp; <span class="teal">ViewFamily</span>.Section );
&nbsp;
&nbsp; <span class="teal">ViewSection</span> NyViewSectionR
&nbsp; &nbsp; = <span class="teal">ViewSection</span>.CreateSection(
&nbsp; &nbsp; &nbsp; doc.Document, SectionId, bbNewSectionR );
&nbsp;
&nbsp; <span class="teal">ElementId</span> GetViewTypeIdByViewType(
&nbsp; &nbsp; <span class="teal">ViewFamily</span> viewFamily )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FilteredElementCollector</span> fec
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>(
&nbsp; &nbsp; &nbsp; &nbsp; m_app.ActiveUIDocument.Document );
&nbsp;
&nbsp; &nbsp; fec.OfClass( <span class="blue">typeof</span>( <span class="teal">ViewFamilyType</span> ) );
&nbsp;
&nbsp; &nbsp; <span class="blue">foreach</span>( <span class="teal">ViewFamilyType</span> e <span class="blue">in</span> fec )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; System.Diagnostics.<span class="teal">Debug</span>.WriteLine( e.Name );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( e.ViewFamily == viewFamily )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; <span class="blue">return</span> e.Id;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; &nbsp; <span class="blue">return</span> <span class="blue">null</span>;
&nbsp; }
</pre>

<p>If needed, I could continue the iteration when several suitable types are found and let the user choose, but that has not yet proved necessary.</p>

<p>Many thanks to H&aring;kan for sharing this!</p>

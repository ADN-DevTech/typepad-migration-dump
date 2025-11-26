---
layout: "post"
title: "10.000.000.000th Post and Element Type Parameters"
date: "2013-09-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Family"
  - "Migration"
  - "Parameters"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/09/10000000000th-post-and-element-type-parameters.html "
typepad_basename: "10000000000th-post-and-element-type-parameters"
typepad_status: "Publish"
---

<p>This is the 1024th post on The Building Coder, just to top off our recent

<a href="http://thebuildingcoder.typepad.com/blog/2013/08/happy-birthday-dear-building-coder.html">
5-year and 1000th post celebration</a>.</p>

<p>The decimal number 1024 equals 2^10, i.e. 10.000.000.000 in binary format, hence the large number of zeroes in the title  :-)</p>

<p>I am also still away on

<a href="http://thebuildingcoder.typepad.com/blog/2013/09/appstore-advice-and-zooming-in-a-preview-control.html#5">
vacation</a> and

this is the second post in my absence &ndash; or 10th, in binary format  :-) &ndash; dealing with

<a href="#2">retrieving ElementType parameters</a>, the
<a href="#3">ADN Xtra labs built-in parameter checker</a> and a
<a href="#4">BipChecker update for Revit 2014</a>.</p>

<p>Meanwhile, I hope you are enjoying the break as much as I am  :-)</p>


<a name="2"></a>

<h4>Retrieving ElementType Parameters</h4>

<p>I want to present a small enhancement to the built-in parameter checker included in the ADN Xtra labs.</p>

<p>The reason for looking at it again is to answer the following frequently recurring question:</p>

<p><strong>Question:</strong> I know how to retrieve the element properties form an object, e.g. a column instance, using the Element.Parameters collection.</p>

<p>However, how can I access the column type properties, please?</p>

<p><strong>Answer:</strong> For a given element E, you can ask for the element id of its type T by calling the GetTypeId method.
Pass that in to the document GetElement method, access the T object instance itself, and retrieve the Element.Parameters collection from that.


<a name="3"></a>

<h4>The ADN Xtra Labs Built-in Parameter Checker</h4>

<p>The ADN Xtra labs built-in parameter checker loops over all defined BuiltInParameter enumeration entries and checks to see whether a value can be retrieved for each of the corresponding parameters on a selected element.</p>

<p>Please be aware that an enhanced version of this built-in parameter checker was published as a separate

<a href="http://thebuildingcoder.typepad.com/blog/2011/09/unofficial-parameters-and-bipchecker.html">
BipChecker add-in</a> back

in 2011.
We'll take another look at that below.</p>

<p>The user is prompted to select an element using the

<a href="http://thebuildingcoder.typepad.com/blog/2010/05/pre-post-and-pick-select.html">
GetSingleSelectedElementOrPrompt</a> method,

which supports all conceivable selection facilities, including:</p>

<ul>
<li>Pre-selection before launching the command.</li>
<li>Post-selection after launching the command.</li>
<li>Entering a numeric element id to select an invisible element.</li>
</ul>

<p>It achieves this by presenting a small prompt message:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff639e5e970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019aff639e5e970d" alt="Element selection prompt" title="Element selection prompt" src="/assets/image_b63838.jpg" border="0" /></a><br />

</center>

<p>The prompt is obviously only displayed if no pre-selection was made.</p>

<p>The code also initialises the isSymbol flag to false:</p>

<pre class="code">
&nbsp; <span class="teal">Element</span> e
&nbsp; &nbsp; = <span class="teal">LabUtils</span>.GetSingleSelectedElementOrPrompt(
&nbsp; &nbsp; &nbsp; uidoc );
&nbsp;
&nbsp; <span class="blue">bool</span> isSymbol = <span class="blue">false</span>;
</pre>

<p>The previous code was implemented before the introduction of the Element.GetTypeId method, so it just checked for a family instance like this:</p>

<pre class="code">
&nbsp; <span class="green">//</span>
&nbsp; <span class="green">// for a family instance, ask user whether to </span>
&nbsp; <span class="green">// display instance or type parameters;</span>
&nbsp; <span class="green">// in a similar manner, we could add dedicated </span>
&nbsp; <span class="green">// switches for Wall --&gt; WallType,</span>
&nbsp; <span class="green">// Floor --&gt; FloorType etc. ...</span>
&nbsp; <span class="green">//</span>
&nbsp; <span class="blue">if</span>( e <span class="blue">is</span> <span class="teal">FamilyInstance</span> )
&nbsp; {
&nbsp; &nbsp; <span class="teal">FamilyInstance</span> inst = e <span class="blue">as</span> <span class="teal">FamilyInstance</span>;
&nbsp; &nbsp; <span class="blue">if</span>( <span class="blue">null</span> != inst.Symbol )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> symbol_name
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">LabUtils</span>.ElementDescription(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inst.Symbol, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> family_name
&nbsp; &nbsp; &nbsp; &nbsp; = <span class="teal">LabUtils</span>.ElementDescription(
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inst.Symbol.Family, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">string</span> msg =
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;This element is a family instance, so it &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;has both type and instance parameters. &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;By default, the instance parameters are &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;displayed. If you select 'No', the type &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;parameters will be displayed instead. &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;Would you like to see the instance &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;parameters?&quot;</span>;
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !<span class="teal">LabUtils</span>.QuestionMsg( msg ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; e = inst.Symbol;
&nbsp; &nbsp; &nbsp; &nbsp; isSymbol = <span class="blue">true</span>;
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>I updated the code to be more generic and handle all kinds of element type relationships by checking whether the GetTypeId method returns a valid element type id like this:</p>

<pre class="code">
&nbsp; <span class="teal">ElementId</span> idType = e.GetTypeId();
&nbsp;
&nbsp; <span class="blue">if</span>( <span class="teal">ElementId</span>.InvalidElementId != idType )
&nbsp; {
&nbsp; &nbsp; <span class="green">// The selected element has a type; ask user </span>
&nbsp; &nbsp; <span class="green">// whether to display instance or type </span>
&nbsp; &nbsp; <span class="green">// parameters.</span>
&nbsp;
&nbsp; &nbsp; <span class="teal">ElementType</span> typ = doc.GetElement( idType )
&nbsp; &nbsp; &nbsp; <span class="blue">as</span> <span class="teal">ElementType</span>;
&nbsp;
&nbsp; &nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != typ,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;expected to retrieve a valid element type&quot;</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> type_name = <span class="teal">LabUtils</span>.ElementDescription(
&nbsp; &nbsp; &nbsp; typ, <span class="blue">true</span> );
&nbsp;
&nbsp; &nbsp; <span class="blue">string</span> msg =
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;This element has an ElementType, so it has &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;both type and instance parameters. By &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;default, the instance parameters are &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;displayed. If you select 'No', the type &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;parameters will be displayed instead. &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;Would you like to see the instance &quot;</span>
&nbsp; &nbsp; &nbsp; + <span class="maroon">&quot;parameters?&quot;</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">if</span>( !<span class="teal">LabUtils</span>.QuestionMsg( msg ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; e = typ;
&nbsp; &nbsp; &nbsp; isSymbol = <span class="blue">true</span>;
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>If an element that has a valid type assigned to it is selected, e.g. a wall, the code detects this and prompts the user to choose whether to display its instance or type properties:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff62f6de970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019aff62f6de970b" alt="Element type message" title="Element type message" src="/assets/image_6e0984.jpg" border="0" /></a><br />

</center>

<p>If instance properties are chosen, the following list of parameters on the wall itself is displayed:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff62f582970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019aff62f582970b image-full" alt="List of instance parameters" title="List of instance parameters" src="/assets/image_cf9a3d.jpg" border="0" /></a><br />

</center>

<p>If type properties are chosen, the parameters are retrieved from the wall type instead:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833019aff62f48e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833019aff62f48e970b image-full" alt="List of element type parameters" title="List of element type parameters" src="/assets/image_8df26b.jpg" border="0" /></a><br />

</center>

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e168978833019aff6321d7970c"><a href="http://thebuildingcoder.typepad.com/files/adn_labs_2014_3.zip">version 2014.0.0.3</a></span> of

the ADN Training Labs for Revit 2014 including the updated built-in parameter checker.</p>


<a name="4"></a>

<h4>BipChecker Update for Revit 2014</h4>

<p>I went on planning to implement the same enhancement in the stand-alone BipChecker add-in, only to discover two things:</p>

<ol>
<li>It has not been updated since its original publication in the year 2011, for Revit 2012.</li>
<li>It has already implemented a more sophisticated check than the one I describe above.</li>
</ol>

<p>To see the more sophisticated check for various kinds of element types implemented by BipChecker, please search for 'CanHaveTypeAssigned' in the

<a href="http://thebuildingcoder.typepad.com/blog/2011/09/unofficial-parameters-and-bipchecker.html">
initial BipChecker publication</a>.</p>

<p>I updated it for Revit 2014, fixing some compilation errors and

<a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">
disabling the architecture mismatch warning</a>;

here is

<span class="asset  asset-generic at-xid-6a00e553e168978833019aff62eff6970b"><a href="http://thebuildingcoder.typepad.com/files/bipchecker_2014.zip">BipChecker_2014.zip</a></span> containing the new version.</p>

<p>Back to my vacation again...
Meanwhile, I wish you a wonderful time as well!</p>

---
layout: "post"
title: "Unique Names and the NamingUtils Class"
date: "2014-09-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Filters"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/09/unique-names-and-the-namingutils-class.html "
typepad_basename: "unique-names-and-the-namingutils-class"
typepad_status: "Publish"
---

<p>The Revit API is still full of surprises.</p>

<p>Here is another one that leads us to look at a utility class that you may not have noticed:</p>

<p><strong>Question:</strong> I am encountering a strange problem with the name checking functionality when creating ParameterFilterElements.</p>

<p>The following code snippet creates two ParameterFilterElements with all identical settings except for a slight naming difference:</p>

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="blue">void</span> ParameterFilterElementError(
&nbsp; &nbsp; <span class="teal">Document</span> doc )
&nbsp; {
&nbsp; &nbsp; <span class="teal">ICollection</span>&lt;<span class="teal">ElementId</span>&gt; ceilingCategory
&nbsp; &nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">List</span>&lt;<span class="teal">ElementId</span>&gt;();
&nbsp;
&nbsp; &nbsp; ceilingCategory.Add(
&nbsp; &nbsp; &nbsp; doc.Settings.Categories.get_Item(
&nbsp; &nbsp; &nbsp; &nbsp; <span class="teal">BuiltInCategory</span>.OST_Ceilings ).Id );
&nbsp;
&nbsp; &nbsp; <span class="blue">using</span>( <span class="teal">Transaction</span> tr = <span class="blue">new</span> <span class="teal">Transaction</span>( doc ) )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; tr.Start( <span class="maroon">&quot;Test&quot;</span> );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">ParameterFilterElement</span>.Create( doc,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;HygienKlass 2&quot;</span>, ceilingCategory );
&nbsp;
&nbsp; &nbsp; &nbsp; <span class="teal">ParameterFilterElement</span>.Create( doc,
&nbsp; &nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Hygienklass 2&quot;</span>, ceilingCategory );
&nbsp;
&nbsp; &nbsp; &nbsp; tr.Commit();
&nbsp; &nbsp; }
&nbsp; }
</pre>

<p>One is called "Hygienklass 2" and the other "HygienKlass 2". This throws an exception saying that the names are equal.</p>

<p>This happens both in Revit 2014 and Revit 2015.</p>

<p>Is there any fix for this?</p>



<p><strong>Answer:</strong> The API is consistent with the Revit UI in this case.</p>

<p>The user interface displays the following message when you use two names that differ only by case:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a73e131d23970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a73e131d23970d image-full img-responsive" alt="Duplicate filter name" title="Duplicate filter name" src="/assets/image_bdb86d.jpg" border="0" /></a><br />
</center>

<p>This restriction around Revit unique naming is currently not highlighted anywhere in the Revit API help file, even though it applies to more than just filters.</p>

<p>We may enhance the error message to clarify this in future.</p>

<p>The Revit rules around what constitutes a unique name and how names are sorted in lists and trees can definitely cause surprises for some folk.</p>

<p>For this reason, the Revit API actually exposes the NamingUtils.CompareNames method, and also because some non-standard comparisons are used, especially around breaking up sections of numeric and non-numeric sequences.</p>

<p>Please take a look at the NamingUtils class, which provides a collection of utilities related to element naming.</p>

<p>Currently, it implements the following two static member methods:</p>

<ul>
<li>CompareNames &ndash; compares two object name strings using Revit's comparison rules.</li>
<li>IsValidName &ndash; identifies if the input string is valid for use as an object name in Revit.</li>
</ul>

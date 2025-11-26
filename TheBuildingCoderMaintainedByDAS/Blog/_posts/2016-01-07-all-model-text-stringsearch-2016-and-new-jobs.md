---
layout: "post"
title: "All Model Text, StringSearch 2016 and New Jobs"
date: "2016-01-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Filters"
  - "Migration"
  - "Modeless"
  - "Parameters"
  - "Plugin"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/01/all-model-text-stringsearch-2016-and-new-jobs.html "
typepad_basename: "all-model-text-stringsearch-2016-and-new-jobs"
typepad_status: "Publish"
---

<p>I migrated another one of my samples to Revit 2016: StringSearch.</p>

<p>That was prompted by a question on <a href="#3">extracting all visible text from the Revit model</a> that I will take a closer look at below.</p>

<h4><a name="2"></a>ADN Internship and Other Jobs</h4>

<p>First, however, let me point out that there are a number of open job offers at Autodesk right now, including
an <a href="https://autodesk.taleo.net/careersection/adsk_cmp/jobdetail.ftl?job=15WD19623">internship position 15WD19623 as Developer Evangelist</a> in
ADN, the Autodesk Developer Network, in our San Francisco office.</p>

<p>Feel free to check out that link yourself or share any candidates you feel would be a great fit for the position.</p>

<p>Anyone wishing to be considered can visit our careers site
and <a href="https://autodesk.taleo.net/careersection/adsk_cmp/jobsearch.ftl">apply directly online</a>.</p>

<p>We also have a bunch of openings in Europe right now, e.g., for Associate Customer Success Managers in France, Germany, UK and Sweden.
Get in touch if you are interested.</p>

<h4><a name="3"></a>Extracting all Visible Text from the Revit Model</h4>

<p><strong>Question:</strong> I am trying to extract all the visible text strings or attributes data from a Revit file to use for indexing. My code must run out-of-process &ndash; no user interaction is possible.</p>

<p>I have been searching for a coding sample or snippet but have been unable to find anything. Is there something like this available?</p>

<p><strong>Answer:</strong> You are basically asking two separate questions, and I would advise you to address and implement them in two separate steps.</p>

<ol>
<li><p>How do I extract all visible text from a Revit model?</p></li>
<li><p>How do I drive this functionality programmatically from outside?</p></li>
</ol>

<p>We have already discussed the second question in other threads, and it is addressed in full by The Building Coder topic group
on <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.28">Idling and External Events for Modeless Access and Driving Revit from Outside</a>.</p>

<p>The first question is initially very simple, so I can leave it up to you to flesh out.</p>

<p>Start off by implementing, testing and debugging it in a macro or a simple external command before you even think of driving it programmatically, i.e. addressing question 2.</p>

<p>I would use one or several filtered element collectors to retrieve all the elements you are interested in and their properties that you wish to extract.</p>

<p>In the simplest case, it might just be text notes and their visible text strings.</p>

<p>Possibly you are also interested in other strings that can be found in specific element parameters or elsewhere.</p>

<p>That should all be pretty simple to determine and access, e.g. using RevitLookup or other,
more <a href="http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html">intimate database exploration tools</a>.</p>

<p>Once you have the extraction functionality up and running to your satisfaction, it should be easy to encapsulate that in a stand-alone function and call it from an external event.</p>

<p><strong>Response</strong> I know how to drive from outside &ndash; I only included that information so that I did not get a solution that provided user interaction.</p>

<p>I think the RevitLookup may help me &ndash; my biggest issue is that I don't know all the element types that can have visible text.</p>

<p>I was able to use FilteredElementCollector to find TextElement:</p>

<pre>
  FilteredElementCollector a = Util.GetElementsOfType(
    doc,
    typeof( TextElement ),
    BuiltInCategory.OST_TextNotes );
</pre>

<p>I know if there are other elements that show text.</p>

<p>Do you have any suggestions how I should determine all element types that would have visible text appearing on the display?</p>

<p><strong>Answer:</strong> I am glad all that is clear.</p>

<p>To determine all element types that have visible text appearing on the display, all I can suggest is creating a collection of a number of super simple sample models that demonstrate and enable you to test the cases that you are aware of up front, and then adding new sample files to that collection as you come across examples of test that they do now cover.</p>

<p>Trial and error, basically, and gathering experience as you go along.</p>

<p>You might also be interested in
the <a href="http://thebuildingcoder.typepad.com/blog/2011/10/string-search-adn-plugin-of-the-month.html">StringSearch Plug-in of the Month utility</a> that
I implemented as the  a couple of years ago.</p>

<p>I later <a href="http://thebuildingcoder.typepad.com/blog/2014/12/devdays-github-stl-and-obj-model-import.html#4">updated and created a GitHub repository</a> for
it as well.</p>

<p>It searches for all strings in the model.</p>

<p>It does not differentiate whether the strings it finds are displayed graphically, though.</p>

<p><strong>Response:</strong> Thanks Jeremy, I download the sample and have been playing with it a bit. I think it will be very helpful. I apologize for my stupid questions. I am just not as comfortable with Revit as other products so many of my problems are no doubt caused by not fully understanding the file/element structure and terminology.</p>

<p>One question about the StringSearch &ndash; it doesn't seem to find dumb text placed in the files. It finds text that is a custom parameter &ndash; like part of the title block. It does not find a dumb text note placed in a view or sheet.</p>

<p>Is this intentional or am I just doing something wrong?</p>

<p><strong>Answer:</strong> I am glad that you find StringSearch a potentially useful starting point.</p>

<p>It will also find a simple string in a text note. You just have to set it up to access the right parameters.</p>

<p>StringSearch only searches for strings in various combinations of element parameter values.</p>

<p>You could add code to it to search for other properties as well, though, e.g. the TextElement.Text property.</p>

<p>That is not necessary, though, because the text note string value is also accessible through the TEXT_TEXT built-in parameter on the TextNote element.</p>

<p>It can be kind of complex to define all the potential combinations of parameter values that you are interested in, and the user interface offers a quite huge range of optional settings that influence the search.</p>

<p>I would simply debug through it and see what it does. The code is not very complex and quite well structured.</p>

<p>As said, you can also easily find out how to retrieve the simple text note string values directly using RevitLookup.</p>

<p>Actually, the answer to that is already provided in what has been said above.</p>

<p>The shortest way to retrieve all TextNote elements string values might be something like this:</p>

<pre class="code">
&nbsp; <span class="teal">List</span>&lt;<span class="blue">string</span>&gt; testStrings = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">TextNote</span> ) )
&nbsp; &nbsp; .Cast&lt;<span class="teal">TextNote</span>&gt;()
&nbsp; &nbsp; .Select&lt;<span class="teal">TextNote</span>, <span class="blue">string</span>&gt;( tn =&gt; tn.Text )
&nbsp; &nbsp; .ToList&lt;<span class="blue">string</span>&gt;();
</pre>

<h4><a name="3"></a>StringSearch for Revit 2016</h4>

<p>Prompted by this discussion, I migrated
the <a href="https://github.com/jeremytammik/StringSearch">StringSearch sample</a> to
Revit 2016 and published the result
as <a href="https://github.com/jeremytammik/StringSearch/releases/tag/2016.0.0.0">release 2016.0.0.0</a>.</p>

<p>Here is the StringSearch ribbon panel in all its glory in Revit 2016:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c803e72d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c803e72d970b img-responsive" style="width: 107px; " alt="StringSearch in Revit 2016" title="StringSearch in Revit 2016" src="/assets/image_ae85ec.jpg" /></a><br /></p>

<p></center></p>

<p>I added a sample TextNote to the model for it to search for:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08a89876970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08a89876970d img-responsive" style="width: 451px; " alt="StringSearch TextNote target" title="StringSearch TextNote target" src="/assets/image_b57163.jpg" /></a><br /></p>

<p></center></p>

<p>As said, the TextNote text string is available through the <code>Text</code> property or via the <code>TEXT_TEXT</code> built-in parameter, so I enable all built-in parameters for the search:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c803e714970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c803e714970b img-responsive" style="width: 247px; " alt="StringSearch dialogue and options" title="StringSearch dialogue and options" src="/assets/image_c2c789.jpg" /></a><br /></p>

<p></center></p>

<p>As a result, the target is found and listed in the modeless search hit navigator:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb08a89861970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb08a89861970d image-full img-responsive" alt="StringSearch results" title="StringSearch results" src="/assets/image_e2714e.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>I hope you find this useful as well.</p>

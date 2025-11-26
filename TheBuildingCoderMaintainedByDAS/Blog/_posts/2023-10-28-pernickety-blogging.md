---
layout: "post"
title: "Pernickety Blogging"
date: "2023-10-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Docs"
  - "HTML"
  - "Markdown"
  - "Philosophy"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/10/pernickety-blogging.html "
typepad_basename: "pernickety-blogging"
typepad_status: "Publish"
---

<p>I am still in hospital, convalescent with 9 fractured ribs and the right broken hip screwed back together again front and back.
The main screw is 7.3 mm x 180 mm.
They would have preferred to use 190 mm length but didn't have any available.
I have big bones.</p>

<p>My friend Madlee has a nice saying in her smartphone email footer:</p>

<ul>
<li>Alles isch gued; wenn's no nid gued isch denn isch es au no nid am End aacho!
<span style="text-align: right; font-style: italic">&ndash;  <a href="https://en.wikipedia.org/wiki/Alemannic_German">Alemannic</a> saying
&ndash; All is well; if it's not well, it's not over yet</span></li>
</ul>

<p>That applies to my broken hip, basically to all of life, and also to every blog post.</p>

<p>My colleague George published his first Revit-API-related blog post last week,
<a href="https://adndevblog.typepad.com/aec/2023/10/how-to-use-toelements-method-correctly.html">How to use ToElements Method correctly</a>.</p>

<p>Congratulations on that, George, and many thanks for your work and contributions!
It is perfect in every way.
And yet, it also provides an opportunity for me to share one slightly crazy aspect of my personality: exaggerated perfectionism.</p>

<p>Before getting to the exaggerated perfectionism, let me point out
the more important <a href="https://thebuildingcoder.typepad.com/blog/2014/07/wishlist-blogging-smartgeometry-dynamo-and-formit.html">blogging tips and tricks</a> that
I listed for earlier colleagues getting started with this.</p>

<p>I checked the post in advance before publication and gave it my OK.
However, to me, it makes a total difference checking it in advance or actually seeing it in print, in its final published version.
The effect of that difference on my perception is tremendous and astounding.
I use this effect myself writing my own blog posts, correcting, previewing, checking, fixing, twiddling and often making a huge number of minute corrections and improvements in the final stages of publication, just before hitting the ultimate publish button.</p>

<p>In this case, I noticed a typo to correct.
When I was about to tell George, I noticed another little detail to improve, and another.
I was astounded by the number and irrelevance of the improvement possibilities that struck my eye.</p>

<p>Except for the two typos, all my suggestions can be ignored.
And yet, re-reading them, I decided to share them both with George and Carol, who is also just starting to blog, and with you, dear reader, to ponder; please also feel perfectly free to ignore, refute, reject and ridicule:</p>

<p>The post currently looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d39d3941200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d39d3941200c image-full img-responsive" alt="Pernickety blogging" title="Pernickety blogging" src="/assets/image_2df536.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<!--

Still, I made a note of one or two things to improve, e.g., the typo in one of the repetitions of the methos name.
Once I'd started, I fiound it hard to stop. One thing added to another, and I ended up with an absolutely shocking list of possible enhancement.
Since I want to praise George and not criticise in any way whatsoever, I pondered my options and ended up deciding that I am crazy and willing to share the fact including this list of suggestions for pernickety blogging:
-->

<p>Here are my pernickety suggestions for enhancement:</p>

<ul>
<li>Title case in title, Capital U and C: How to Use ToElements Method Correctly</li>
<li>Plural 'couple of questions': There <em>have</em> been a couple of questions</li>
<li>Missing 'the'</li>
<li><code>ToElements</code> is code, so should be noted as such typographically, e.g., using a monospace font such as Courier</li>
<li>In HTML, code is normally tagged using <code>pre</code> or <code>code</code>;
in <a href="https://en.wikipedia.org/wiki/Markdown">Markdown</a>, you can use a backtick, i.e., <code>&grave;ToElements&grave;</code></li>
<li>The word 'method' is lowercase: usage of <code>ToElements</code> Method</li>
<li>The word 'class' is lowercase: <code>FilteredElementCollector</code> class</li>
<li>Typo, missing 'n': ToElemets</li>
<li>Typo, 'examples' is plural: examples that demonstrate</li>
<li>Code indentation: leading spaces inside the loops</li>
<li>Code colourisation: C# syntax and keywords in different colours</li>
<li>Avoid long lines in sample code; add line breaks to improve readability</li>
<li>Readable link, not just the naked raw URL</li>
</ul>

<p>My corrected version ends up looking like this:</p>

<hr/>

<h4><a name="3"></a> How to Use ToElements Method Correctly</h4>

<p>There have been a couple of questions regarding the usage of the <code>ToElements</code> method while filtering elements using the <code>FilteredElementCollector</code> class.
The <code>ToElements</code> method in the <code>FilteredElementCollector</code> class returns the complete set of elements that meet the specified filter criteria as a generic <code>IList</code>.</p>

<p>However, it's also worth noting that some members of the Revit API community tend to use the ToElemets method after using the FilteredElementCollector which in turn increases memory usage and slows down the performance of the same.</p>

<p>One reason for using ToElements is to obtain the element count. However, that can also be achieved by calling GetElementCount.</p>

<p>Another more valid reason is to access the elements in the list by index, e.g., you have 1000 elements in the list and you want to read their data in a specific order, e.g., #999, #1, #998, #2 or whatever. Then, you need the index provided by the list, and cannot just iterate over them on the predefined order provided by the enumerator.</p>

<p>Here are examples that demonstrate the usage of the two:</p>

<p>Example 1, using FilteredElementCollector alone to iterate over all Wall elements:</p>

<pre class="prettyprint">
  IEnumerable walls
    = new FilteredElementCollector(doc)
      .OfClass(typeof(Wall));

  foreach (Element item in walls)
  {
    ElementId id = item.Id;
  }
</pre>

<p>Example 2, using both FilteredElementCollector and ToElements to iterate over all Wall elements:</p>

<pre class="prettyprint">
  IList wallList
    = new FilteredElementCollector(doc)
      .OfClass(typeof(Wall))
        .ToElements();

  foreach (Element item in wallList)
  {
    ElementId id = item.Id;
  }
</pre>

<p>More details and links to further related discussions are provided in the analysis of
the <a href="https://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html#2">performance</a>
in <a href="https://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html">how to distinguish redundant rooms</a>.</p>

<hr/>

<p>Here is a link to the markdown source code for this blog post,
<a href="https://github.com/jeremytammik/tbc/blob/gh-pages/a/2013_pernickety.md">2013_pernickety.md</a>,
where you can see the Markdown text I edited to produce this.</p>

<p>I have used a variety
of <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.36">tools for C&#35;, Python and VB code colourisation</a> in
the past.</p>

<p>In this post, I just employed the <a href="https://github.com/googlearchive/code-prettify">Google JavaScript code prettifier <code>code-prettify</code></a> instead.</p>

<p>However, this tool is no longer being maintained, so it is actually time to switch to yet another solution...</p>

<p>Another reason to replace it would be that it requires me to manually replace <code>&lt;</code> and <code>&gt;</code> signs with their HTML escape characters <code>&amp;lt;</code> and <code>&amp;gt;</code>.</p>

<p>So, to wrap up:</p>

<p>Please excuse me for being pernickety, George and Carol.
It seems to be my nature, so best accept it and let it be...
Does this extra work have any advantages?
Is it worth the effort?
Up to each and every person to decide for herself, I would say...</p>

<p><a href="https://www.linkedin.com/feed/update/urn:li:activity:7124104311315279872?commentUrn=urn%3Ali%3Acomment%3A%28activity%3A7124104311315279872%2C7124255520466554880%29&amp;dashCommentUrn=urn%3Ali%3Afsd_comment%3A%287124255520466554880%2Curn%3Ali%3Aactivity%3A7124104311315279872%29">Jonathon Broughton adds</a>:</p>

<p>The second paragraph of your corrected version contains a typo.
<code>ToElemets</code> should be <code>ToElements</code>.</p>

<p>It takes a persnickety practitioner to know one 😎</p>

<p><em>Response:</em> Great.
I wonder should I leave it for other practitioners to pick up as well?
I think it would be appreciated...
I'll add your correction to the post and out myself (yet again) as a pernickety failure.</p>

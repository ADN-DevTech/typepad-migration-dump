---
layout: "post"
title: "Wall Elevation Profiles in The Building Coder Samples"
date: "2015-01-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Geometry"
  - "Git"
  - "Migration"
  - "Utilities"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/01/wall-elevation-profiles-in-the-building-coder-samples.html "
typepad_basename: "wall-elevation-profiles-in-the-building-coder-samples"
typepad_status: "Publish"
---

<p>Last week, I presented a stand-alone command by my colleague Katsuaki Takamizawa to

<a href="http://thebuildingcoder.typepad.com/blog/2015/01/getting-the-wall-elevation-profile.html">
retrieve wall elevation profiles</a>.</p>

<p>His implementation provides a nice little example of using the

<a href="http://thebuildingcoder.typepad.com/blog/2015/01/exporterifcutils-curve-loop-sort-and-validate.html">
ExporterIFCUtils.SortCurveLoops method</a> and

differentiates between outer and inner loops.</p>

<p>It can be used in almost exactly the same way as the

<a href="http://thebuildingcoder.typepad.com/blog/2008/11/wall-elevation-profile.html">
first wall elevation profile</a> implementation

presented in 2008.</p>

<p>After publication, Katsu-san and I discussed what to do next and unearthed a couple of interesting aspects:</p>

<p><strong>Response:</strong> Thank you very much for publishing the sample code.</p>

<p>I migrated the original wall profile sample. There were a number of places that needed to be changed for this migration.</p>

<p>I see many other Building Coder samples. It would be ideal to migrate them all, but this would require a good effort.</p>

<p>Maybe we can migrate frequently used samples and have developers migrate the rest by themselves as needed?</p>

<p><strong>Answer:</strong> The Building Coder samples are already completely migrated, and the most up-to-date version is always available from

<a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples GitHub repository</a>.</p>

<p>I am not thinking of re-migrating the old code, since that is already done, but of replacing the current migrated version in The Building Coder samples by your new implementation, since you say it is simpler and better.</p>

<p><strong>Response:</strong> I was not aware that all the old samples have already been migrated and put on GitHub. That is really great!</p>

<p>I migrated the old code from the original blog post. I am afraid that some developers might be doing the same.</p>

<p>Is this link visible and accessible from your blog?</p>

<p><strong>Answer:</strong> Basically, I keep pointing out that the most up-to-date version is always available from

<a href="https://github.com/jeremytammik/the_building_coder_samples">The Building Coder samples GitHub repository</a>, as above.</p>

<p>I followed your suggestion and integrated the new implementation in parallel with the original one.</p>

<p>The main entry point now looks like this:</p>

<pre class="code">
&nbsp; <span class="blue">public</span> <span class="teal">Result</span> Execute(
&nbsp; &nbsp; <span class="teal">ExternalCommandData</span> commandData,
&nbsp; &nbsp; <span class="blue">ref</span> <span class="blue">string</span> message,
&nbsp; &nbsp; <span class="teal">ElementSet</span> elements )
&nbsp; {
&nbsp; &nbsp; <span class="green">// Choose which implementation to use.</span>
&nbsp;
&nbsp; &nbsp; <span class="blue">bool</span> use_execute_2 = <span class="blue">true</span>;
&nbsp;
&nbsp; &nbsp; <span class="blue">return</span> use_execute_2
&nbsp; &nbsp; &nbsp; ? Execute2( commandData, <span class="blue">ref</span> message, elements )
&nbsp; &nbsp; &nbsp; : Execute1( commandData, <span class="blue">ref</span> message, elements );
&nbsp; }
</pre>

<p>You can switch between the two by toggling the value of the Boolean variable <code>use_execute_2</code> in the debugger.</p>

<p>Here is the result of running the new implementation in the sample model you provided:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c73fad20970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c73fad20970b img-responsive" style="width: 258px; " alt="Wall elelvation profiles produced by the new implementation" title="Wall elelvation profiles produced by the new implementation" src="/assets/image_6dc0f4.jpg" /></a><br />

</center>

<p>The result of the original implementation looks like this:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d0c93542970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d0c93542970c img-responsive" style="width: 223px; " alt="Wall elelvation profiles produced by the original implementation" title="Wall elelvation profiles produced by the original implementation" src="/assets/image_774735.jpg" /></a><br />

</center>

<p>I published the version including the new command implementation as

<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2015.0.116.7">
release 2015.0.116.7</a>.</p>

<p>The integration of the two required changing the transaction mode of the external command from automatic to manual.</p>

<p>This really ought to be done in all of the external command samples.</p>

<p>Another overdue task is to completely remove all obsolete API usage.
I last took a stab at it in November,

<a href="http://thebuildingcoder.typepad.com/blog/2014/11/determining-intersecting-elements-and-continued-futureproofing.html#2">
eliminating more obsolete API usage</a>.</p>

<p>I also want to update the copyright message to reflect the new year 2015.</p>

<p>In fact, I just completed the latter right away, and published the result as

<a href="https://github.com/jeremytammik/the_building_coder_samples/releases/tag/2015.0.116.8">
release 2015.0.116.8</a>.</p>

<p>Many thanks to Katsu-san for this useful discussion and clarification!</p>

---
layout: "post"
title: "Some miscellaneous BrowsePhotosynth changes"
date: "2010-10-08 13:55:07"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2010/10/some-miscellaneous-browsephotosynth-changes.html "
typepad_basename: "some-miscellaneous-browsephotosynth-changes"
typepad_status: "Publish"
---

<p>I just thought I’d report back on a few changes made to the <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/octobers-plugin-of-the-month-browsephotosynth-for-autocad.html" target="_blank">BrowsePhotosynth</a> <a href="http://labs.autodesk.com/utilities/ADN_plugins" target="_blank">Plugin of the Month</a> during the course of this week. The updated version has just been <a href="http://labs.blogs.com/its_alive_in_the_lab/2010/10/updated-browsephotosynth-for-autocad-plugin-now-available.html" target="_blank">announced on Scott Sheppard’s blog</a> and I thought I’d share some of the specific implementation details.</p>
<p>The first one (in the 1.0.1 update) was a really interesting problem and I owe a big thanks both to Alberto Venturini for <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/octobers-plugin-of-the-month-browsephotosynth-for-autocad.html#comments" target="_blank">reporting it</a> and to Marat Mirgaleev, from our DevTech team in Moscow, for helping test on a comparable OS. The problem was that on all the systems upon which Alberto had tested, the various points in a Photosynth point cloud would end up on a single axis, essentially at varying distances along a line. We narrowed it down to being locale-related, and then (which I was sleeping on Monday night) <a href="http://www.usingenglish.com/reference/idioms/the+penny+dropped.html" target="_blank">the penny dropped</a> and I realised it had to be due to the widespread use of comma as the decimal separator in non-English locales.</p>
<p>The application takes a number of files stored on the Photosynth servers and downloads/processes them, combining the resultant points into a single text file which then gets processed in a LAS file, which they gets indexed as a PCG and attached into AutoCAD. Phew. Here’s that in graphical form (click to enlarge):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133f4ed907e970b-pi"><img alt="BrowsePhotosynth flow chart" border="0" height="115" src="/assets/image_463713.jpg" style="margin: 20px auto; display: block; float: none; border: 0px;" title="BrowsePhotosynth flow chart" width="480" /></a></p>
<p>The issue was between the F# processor and the txt2las tool: we were clearly writing decimals with comma as the decimal separator to the comma-delimited <em>points.txt</em> file (while txt2las expects period-/full stop-delimited decimals in a comma-delimited file, irrespective of the current locale).</p>
<p>The fix was actually straightforward and applies as well to VB.NET and C# as it does to F#. Here’s an example of an incorrect call:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">x.ToString()</span></p>
</div>

<p>and here’s what we needed to do, instead, to make sure decimals use a period/full-stop as the decimal point, irrespective of the current locale settings:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">x.ToString(CultureInfo.InvariantCulture)</span></p>
</div>

<p>We also needed to add the System.Globalization namespace, but that’s about it. Oh, and as we are in F# – which is type-inferred – we had to pin down the values being passed into the function to be of type float32, as the generic object-level ToString() doesn’t have a version taking an IFormatProvider (which CultureInfo.InvariantCulture happens to be).</p>
<p>The next two changes were in the 1.0.2 update that has just been posted: the F# processor’s IsComplete property previously checked the number of files that had been processed. There was an additional callback – AllCompleted – which took care of adding the number of points from each file into a total. So if the loop checked for IsComplete broke and the code went on to check the number of points downloaded – before the AllCompleted event fired – then we would incorrectly report that 0 points had been downloaded and finish the command right there. The fix was to add a boolean flag in the code to be exposed via IsComplete which only gets set to true at the end of AllCompleted.</p>
<p>The second of the 1.0.2 changes was simply to bring down the primary point cloud and none of the additional ones. This is the original implementation I had created, but ended up extending it to pull down multiple point clouds for each Photosynth. The trouble is, the point clouds are kept separate for a good reason: they are not spatially connected (well they actually might be, but that’s a different story and not for today’s post :-) and so have different coordinate systems. I was blindly downloading them and <a href="http://www.thefreedictionary.com/plonking" target="_blank">plonking</a> them down into the same coordinate space, which – even if we had more points – led to messier results.</p>
<p>Other Photosynth exporters do provide more information on the various sizes of the point clouds available for each synth, but I’ve opted for simplicity and so just take the first one (which is usually the largest). The code could easily be extended to provide more choice for the user in this area, should anyone feel the need to do so.</p>
<p>Which actually means the results of importing <a href="http://photosynth.net/view.aspx?cid=6468bbba-cc37-499e-92b5-ae9f5a4fd438" target="_blank">the Menzi Muck synth</a> from <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/some-impressive-technology.html" target="_blank">the last post</a> (and also <a href="http://photosynth.net/view.aspx?cid=39b428dd-0ef0-44ad-90aa-e4372c53f6e5" target="_blank">it’s new big brother</a>) look better than I’d previously reported.</p>
<p>Hopefully that’s it for the code changes to this tool, for now, but please do <a href="mailto:labs.plugins@autodesk.com" target="_blank">let us know</a> if you experience any further issues with it.</p>

---
layout: "post"
title: "Retrieving a C# out Argument Value in Python"
date: "2016-07-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Dynamo"
  - "Python"
original_url: "https://thebuildingcoder.typepad.com/blog/2016/07/retrieving-a-c-out-argument-value-in-python.html "
typepad_basename: "retrieving-a-c-out-argument-value-in-python"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?autoload=true" defer="defer"></script>

<p>Here is a short note on two interesting little items that just cropped up:</p>

<ul>
<li><a href="#2">Retrieving a C# <code>out</code> argument value in Python</a></li>
<li><a href="#3">ETH Zurich Sandstone Vault at the Venice Architecture Biennale</a></li>
</ul>

<h4><a name="2"></a>Retrieving a C# <code>out</code> Argument Value in Python</h4>

<p>This issue was raised and solved by Peter aka KOP in
the <a href="http://forums.autodesk.com/t5/revit-api/bd-p/160">Revit API discussion forum</a> thread
on <a href="http://forums.autodesk.com/t5/revit-api/door-window-areas/m-p/5535565">door and window areas</a>:</p>

<p><strong>Question:</strong> I understand that this code returns the curve loop of a cutout:</p>

<pre class="prettyprint">
  curveLoop = I.ExporterIFCUtils
    .GetInstanceCutoutFromWall(
      doc, wall, familyInstance, out basisY );
</pre>

<p>unfortunately, i am trying to achieve the result from the python side.</p>

<p>my efforts end in errors for the <code>out basisY</code>.</p>

<p>as my coding skills are still limited, can anyone help me out on this?</p>

<p><strong>Answer:</strong> Issue is solved for the Python code required.</p>

<p>my solution went like:</p>

<pre class="prettyprint">
  for i in openingIds:
    try:
      bounding, orient = I.ExporterIFCUtils.GetInstanceCutoutFromWall(doc, element, doc.GetElement(i),)
      print "success"
    except:
      print (" failed for wall %s and opening %s" %(element.Id, i))
</pre>

<p>Many thanks to Peter for sharing this useful result.</p>

<p>By the way, here is another explanation of writing
an <a href="http://stackoverflow.com/questions/2857287/writing-iron-python-method-with-ref-or-out-parameter">IronPython method with <code>ref</code> or <code>out</code> parameter</a>,
not related to Revit.</p>

<h4><a name="3"></a>ETH Zurich Sandstone Vault at the Venice Architecture Biennale</h4>

<p>I recently mentioned
my <a href="http://thebuildingcoder.typepad.com/blog/2016/03/adn-becomes-forge-and-barcelona-accelerator.html#4">visit to the Block Research Group at ETHZ</a> and
the fascinating architectural research they are practicing there, on building extremely efficient material-saving vaults.</p>

<p>Now they are <a href="http://www.designboom.com/architecture/venice-architecture-biennale-beyond-bending-eth-zurich-block-research-group-05-26-2016">exhibiting custom beautiful vaults at the Venice Architecture Biennale</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb091c4de7970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb091c4de7970d img-responsive" style="width: 480px; " alt="BRG at the Venice Architecture Biennale 2016" title="BRG at the Venice Architecture Biennale 2016" src="/assets/image_16d53b.jpg" /></a><br /></p>

<p></center></p>

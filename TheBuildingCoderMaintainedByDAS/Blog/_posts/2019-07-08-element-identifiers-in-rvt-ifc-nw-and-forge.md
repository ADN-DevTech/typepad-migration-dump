---
layout: "post"
title: "Element Identifiers in RVT, IFC, NW and Forge"
date: "2019-07-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Element Relationships"
  - "Export"
  - "Forge"
  - "IFC"
  - "JSON"
  - "Photo"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/07/element-identifiers-in-rvt-ifc-nw-and-forge.html "
typepad_basename: "element-identifiers-in-rvt-ifc-nw-and-forge"
typepad_status: "Publish"
---

<p>A recent internal discussion clarifies Revit element identification in Forge, Navisworks and IFC, some new thoughts on consciousness versus AI, and a couple of topics of personal interest:</p>

<ul>
<li><a href="#2">Two nice summer mountain hikes</a></li>
<li><a href="#3">Revit element ids in Forge via Navisworks and IFC</a></li>
<li><a href="#4">Some aspects of consciousness may be beyond reach of AI</a></li>
<li><a href="#5">Trees might help against global warming</a></li>
<li><a href="#6">Holidays ahead</a></li>
</ul>

<h4><a name="2"></a> Two Nice Summer Mountain Hikes</h4>

<p>I spent the last two weekends in the mountains north and south of the Gotthard pass.</p>

<p>First, I crossed the ridges between Grossgand, Ruchälplistock and Jakobiger with my friend Nik, and we spent the night in a pleasant, warm, windless bivouac on the latter at 2505 metres before descending for coffee in the Leutschachhütte next morning  ([https://flic.kr/s/aHsmEUnYFd](photo album)).</p>

<p>The weekend after, I hiked with Moni from Mergugno over Brissago up to the Rifugio al Legn overlooking the Lago Maggiore.
I continued alone from there up to Fumadiga and through the upper part of Bocchetta di Valle to Monte Limidario, aka Gridone.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4bb3337200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4bb3337200b img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Jeremy on Jakobiger" title="Jeremy on Jakobiger" src="/assets/image_545f36.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Revit Element Ids in Forge via Navisworks and IFC</h4>

<p>This question prompted me to create a new topic group
on <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.56">element identification using <code>ElementId</code> and <code>UniqueId</code> RVT, IFC, NW and Forge</a>:</p>

<p><strong>Question:</strong> I created a Navisworks file from a Revit file and submitted it to Forge.
The returning translated JSON looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a496912d200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a496912d200d image-full img-responsive" alt="RVT to NW element id in Forge" title="RVT to NW element id in Forge" src="/assets/image_6956b1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>What is this <code>externalId</code> and where does it come from?</p>

<p>The <code>Id</code> property maps to the Revit element id.</p>

<p>Can you please confirm?</p>

<p>Where is the source Revit element unique id?</p>

<p>Is it lost during this process?</p>

<p><strong>Answer:</strong> The <code>externalId</code> is the path through the model selection tree to that item.
As far as I know, Revit doesn't have per-element GUIDs, but instead each element has a unique id.
This is what you have highlighted at the bottom of the image.</p>

<p>This is how we expect NWD files to look like when converted to SVF.</p>

<p>You cannot expect the data to look exactly the same as the RVT file.</p>

<p><strong>Response:</strong> This is what the source Revit file looks like in JSON:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4bb3356200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4bb3356200b image-full img-responsive" alt="RVT element ids in Forge" title="RVT element ids in Forge" src="/assets/image_848b6a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>In this case, the <code>externalId</code> is the <code>Element.UniqueID</code> GUID.</p>

<p>The <code>Name</code> property contains the <code>Element.Id</code>.</p>

<p><strong>Answer:</strong> Yes, I know they are different, which you would expect, really, as you are converting different types of files.</p>

<p>Revit creates a unique id by appending together
the <a href="https://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html#2">document-level EpisodeId GUID with the Element ID in hex</a>:</p>

<pre>
  0088CA1F hex = 8964639 decimal
</pre>

<p><strong>Response:</strong> In this case, in order to find the Revit element in the converted Navisworks file, we actually need to parse the name and get the Id out of it? I was expecting a property to hold this value.</p>

<p>I find strange that in the translated source Revit file we don't actually have an "Id" property.</p>

<p>I thought that the <code>"</code>externalId` in the translated source RVT file, was the actual id to look for in the translated NWD file.</p>

<p>I need to consistently identify elements in NWD and IFC files generated out of a source RVT file.</p>

<p>Looking at the Forge model derivative JSON files, it seems we can have</p>

<ul>
<li>Take the <code>Id</code> out of the <code>Name</code> for the RVT</li>
<li>The <code>Element.Id</code> for the NWD</li>
<li>The <code>IfcTag</code> for the IFC</li>
</ul>

<p>Here is an image showing all three:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a46d5222200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a46d5222200c image-full img-responsive" alt="Element ids in Forge RVT, NW and IFC files" title="Element ids in Forge RVT, NW and IFC files" src="/assets/image_9f01b2.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> Yes.</p>

<p>The Revit extractor puts the Element ID in the object name, so you can get it from there.</p>

<p>The NW extractor has the Element ID as a specific property (in the Element category).</p>

<p>The IFC file has the Element ID in the IfcTag attribute.</p>

<p>In NW, we try to read as much property data as we can. From there, if the property is visible in NW, it is exported on into the SVF file. The NW to SVF extractor implements special handling for different formats. Once the file has been converted to NWC/NWD, we don't really know what source file type it was originally. Furthermore, because not all file formats have unique ids for elements, NW doesn't have a concept of unique elements ids either; hence, it can only output the path through the tree for the <code>externalId</code> property.</p>

<h4><a name="4"></a> Some Aspects of Consciousness May be Beyond Reach of AI</h4>

<p>A very interesting overview by Steve Paulson,
<a href="http://nautil.us/issue/47/consciousness/roger-penrose-on-why-consciousness-does-not-compute">Roger Penrose on why consciousness does not compute</a>,
describing some of Penrose's theories on consciousness that cannot simply be simulated by AI.</p>

<h4><a name="5"></a> Trees Might Help Against Global Warming</h4>

<p>The prestigious Zurich university ETH just published some positive news on a possible approach to the climate change; their research
shows <a href="https://ethz.ch/en/news-and-events/eth-news/news/2019/07/how-trees-could-save-the-climate.html">how trees could save the climate</a>.</p>

<h4><a name="6"></a> Holidays Ahead</h4>

<p>Similar to last year, I am heading off to France for some camping and a meditation retreat
in <a href="http://thebuildingcoder.typepad.com/blog/2018/07/mindful-living-and-smiling-to-myself.html">Plum Village</a>,
a Buddhist monastery near Bordeaux.</p>

<p>On the way there, I'm also visiting my friend George in a camping ground on the beach of the Atlantic for a few days.</p>

<p>So I will be less active in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> in
the coming weeks.</p>

<p>I wish you a wonderful summer!</p>

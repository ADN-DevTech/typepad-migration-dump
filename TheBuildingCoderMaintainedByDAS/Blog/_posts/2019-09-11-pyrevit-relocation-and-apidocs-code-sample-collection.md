---
layout: "post"
title: "pyRevit Home and ApiDocs Code Sample Collection"
date: "2019-09-11 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Docs"
  - "Python"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/09/pyrevit-relocation-and-apidocs-code-sample-collection.html "
typepad_basename: "pyrevit-relocation-and-apidocs-code-sample-collection"
typepad_status: "Publish"
---

<p>Two exciting Revit API related news announcements from Ehsan Iran-Nejad and Gui Talarico:</p>

<ul>
<li><a href="#2">New comprehensive pyRevit home page</a></li>
<li><a href="#3">ApiDocs.co code search sample collection</a>
<ul>
<li><a href="#3.1">How does it work?</a></li>
<li><a href="#3.2">Limitations</a></li>
<li><a href="#3.3">How to contribute</a></li>
<li><a href="#3.4">RevitApiDocs and ApiDocs comparison</a></li>
</ul></li>
</ul>

<h4><a name="2"></a> New Comprehensive pyRevit Home Page</h4>

<p>Ehsan <a href="https://twitter.com/eirannejad">@eirannejad</a> Iran-Nejad
<a href="https://twitter.com/eirannejad/status/1170576981538172928?ref_src=twsrc%5Etfw">unified the pyRevit online experience</a>,
creating a new <a href="http://wiki.pyrevitlabs.io">pyRevit home page</a> that
takes you through all project related aspects:</p>

<!--
<center>


3cd7e1e5780bf86429f6f5c578831281


<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</center>
-->

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4823738200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4823738200c image-full img-responsive" alt="pyRevit home page" title="pyRevit home page" src="/assets/image_558821.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> ApiDocs.co Code Search Sample Collection</h4>

<p>Gui <a href="https://twitter.com/gtalarico">@gtalarico</a> Talarico, the author of both 
the <a href="https://www.revitapidocs.com">online Revit API documentation revitapidocs.com</a> and the more
general <a href="https://apidocs.co">Apidocs.co</a> covering Grasshopper, Navisworks and Rhino as well,
<a href="https://twitter.com/gtalarico/status/1170473246275145729?ref_src=twsrc%5Etfw">announced an expanded search functionality</a> in
the latter that provides code samples directly within its pages by searching a whole collection of samples hosted in the
new <a href="https://github.com/gtalarico/apidocs.samples">ApiDocs.co code search sample repository</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4ab7e68200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4ab7e68200d image-full img-responsive" alt="Apidocs.co" title="Apidocs.co" src="/assets/image_beb284.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<!--
<center>


a37645d4a811e424f029c49c97f6a45d


<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
</center>
-->

<h5><a name="3.1"></a> How Does it Work?</h5>

<p><a href="https://apidocs.co">Apidocs.co</a> uses the GitHub Code Search API against this repo to provide Code Samples directly within pages.</p>

<p>Because the GitHub Code Search API is limited to a single user or repo, this repository aggregates multiple relevant repos so they can all be searchable in a single request.</p>

<h5><a name="3.2"></a> Limitations</h5>

<ul>
<li>It's plain text search &ndash; some generic names like <code>Application</code> can trigger many false positives</li>
<li>It's limited to certain entity types (e.g., <code>Class</code>, <code>Method</code>, <code>Property</code>, etc.)</li>
</ul>

<h5><a name="3.3"></a> How to Contribute</h5>

<ul>
<li>Fork this repo</li>
<li>Add a relevant repo to <code>repos.json</code></li>
<li>Run <code>python update.py</code></li>
<li>Send a <a href="https://github.com/gtalarico/apidocs.samples/pulls">Pull Request</a></li>
</ul>

<h5><a name="3.4"></a> RevitApiDocs and ApiDocs Comparison</h5>

<p><strong>Question:</strong> Could the new code sample search functionality be added to
both <a href="http://apidocs.co">apidocs.co</a>
and <a href="https://www.revitapidocs.com">revitapidocs</a>?
It is tricky to know when to use which...</p>

<p><strong>Answer:</strong> Users can use whichever they prefer.
While <a href="https://www.revitapidocs.com">revitapidocs</a> will likely continue to get new API versions, it will not get new features &ndash; the code base has not aged well and adding new features to it is no fun.</p>

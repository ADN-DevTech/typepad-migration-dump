---
layout: "post"
title: "Accessing Discipline and Duplicating View Template"
date: "2014-08-19 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2015"
  - "Data Access"
  - "Discipline"
  - "Element Creation"
  - "VB"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/08/accessing-discipline-and-duplicating-view-template.html "
typepad_basename: "accessing-discipline-and-duplicating-view-template"
typepad_status: "Publish"
---

<p>I am back again in the land of the living... working, blogging.</p>

<p>I'll jump right in with two recent cases related to disciplines and views:</p>

<ul>
<li><a href="#2">Accessing view and project browser disciplines</a></li>
<li><a href="#3">Duplicating a view template</a></li>
</ul>


<a name="2"></a>

<h4>Accessing View and Project Browser Disciplines</h4>

<p><strong>Question:</strong> When changing the Revit project browser organization in Revit to 'Discipline', the project browser shows different subfolders depending on the document template.</p>

<p>For example, in a project created from an architectural template, it shows ‘Architectural':</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c6ceadae970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c6ceadae970b img-responsive" style="width: 306px; " alt="Project browser discipline label Architecture" title="Project browser discipline label Architecture" src="/assets/image_831f05.jpg" /></a><br />

</center>

<p>If the project was created from a structural template, it shows ‘Structural':</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c6ceadb9970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c6ceadb9970b img-responsive" style="width: 306px; " alt="Project browser discipline label Structural" title="Project browser discipline label Structural" src="/assets/image_16a9ce.jpg" /></a><br />

</center>

<p>Is it possible to retrieve this discipline name programmatically?</p>


<p><strong>Answer:</strong> My understanding is that this is for organization.
If you choose 'discipline', it shows discipline as a top categorization.</p>

<p>If you change the discipline of a view, say from 'Architecture' to 'Structure' for an elevation, it will show up under 'Structure':</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a511f9168c970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a511f9168c970c image-full img-responsive" alt="View discipline" title="View discipline" src="/assets/image_91ee35.jpg" border="0" /></a><br />

</center>

<p>This depends on the current setting of the View.Discipline property providing a ViewDiscipline enumeration value.</p>



<p><strong>Response:</strong> Yes, View.Discipline returns the discipline for a given view.</p>

<p>The returned discipline label is English text, even in a localised (e.g., Japanese) version of Revit.</p>

<p>I implemented this test code to list the disciplines of all views in the current document:</p>

<pre class="code">
&nbsp; <span class="teal">FilteredElementCollector</span> collector
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc )
&nbsp; &nbsp; &nbsp; .OfClass( <span class="blue">typeof</span>( <span class="teal">View</span> ) );
&nbsp;
&nbsp; <span class="blue">string</span> s = <span class="maroon">&quot;&quot;</span>;
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">View</span> v <span class="blue">in</span> collector )
&nbsp; {
&nbsp; &nbsp; <span class="blue">if</span>( ( v.ViewType == <span class="teal">ViewType</span>.Elevation ||
&nbsp; &nbsp; &nbsp; v.ViewType == <span class="teal">ViewType</span>.FloorPlan ||
&nbsp; &nbsp; &nbsp; v.ViewType == <span class="teal">ViewType</span>.ThreeD ) &amp;&amp;
&nbsp; &nbsp; &nbsp; v.CanBePrinted )
&nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; <span class="blue">if</span>( !s.Contains( v.Discipline.ToString() ) )
&nbsp; &nbsp; &nbsp; {
&nbsp; &nbsp; &nbsp; &nbsp; s = s + <span class="maroon">&quot;\n&quot;</span> + v.Discipline.ToString();
&nbsp; &nbsp; &nbsp; }
&nbsp; &nbsp; }
&nbsp; }
&nbsp; <span class="teal">TaskDialog</span>.Show( <span class="maroon">&quot;&#23554;&#38272;&#20998;&#37326;&#21517;&quot;</span>, s );
</pre>

<p>Is there any other more direct way to retrieve the discipline label displayed in the project browser?</p>


<p><strong>Answer:</strong> The following code retrieves the string '構造' (Structural) for the current view (Level 1, in this case) in the structural discipline:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c6ceadcf970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c6ceadcf970b img-responsive" style="width: 258px; " alt="Project browser discipline label" title="Project browser discipline label" src="/assets/image_ec08d4.jpg" /></a><br />

</center>

<pre class="code">
&nbsp; <span class="teal">BrowserOrganization</span> bo = <span class="teal">BrowserOrganization</span>
&nbsp; &nbsp; .GetCurrentBrowserOrganizationForViews( doc );
&nbsp;
&nbsp; IList&lt;<span class="teal">FolderItemInfo</span>&gt; folderItems
&nbsp; &nbsp; = bo.GetFolderItems( doc.ActiveView.Id );
&nbsp;
&nbsp; <span class="blue">foreach</span>( <span class="teal">FolderItemInfo</span> folder <span class="blue">in</span> folderItems )
&nbsp; {
&nbsp; &nbsp; <span class="blue">string</span> name = folder.Name;
&nbsp; }
</pre>

<p>Please note that the

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/whats-new-in-the-revit-2015-api.html#3.13">
BrowserOrganization class</a> was

added in Revit 2015.</p>


<a name="3"></a>

<h4>Duplicating a View Template</h4>

<p>Madhu Das raised the issue of duplicating a view template in a

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/filter-for-views-and-istemplate-predicate.html?cid=6a00e553e16897883301a73e03bcff970d#comment-6a00e553e16897883301a73e03bcff970d">comment</a> on

the discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/filter-for-views-and-istemplate-predicate.html">filtering for views and the IsTemplate predicate</a>:</p>

<p><strong>Question:</strong> How can I programmatically duplicate a view template?</p>

<p>The following code returns False if v1 is a view template:</p>

<pre class="code">
  v1.CanViewBeDuplicated(<span class="teal">ViewDuplicateOption</span>.Duplicate)
</pre>


<p><strong>Answer:</strong> We currently have an open wish list item CF-1509 [API wish: create view template -- 09693106] for programmatic creation of a view template.</p>

<p>For duplication of an existing one, you may be able to make use of the copy and paste API.</p>

<p>Look at the numerous

<a href="http://thebuildingcoder.typepad.com/blog/2013/05/copy-and-paste-api-applications-and-modeless-assertion.html">
copy and paste API usage examples</a>.</p>


<p><strong>Response:</strong> Thank you very much for the help.</p>

<p>It is possible by using the Copy and Paste API.</p>

<p>I successfully tried the following code:</p>

<pre class="code">
&nbsp; <span class="blue">Dim</span> elementIds <span class="blue">As</span> <span class="blue">New</span> List(<span class="blue">Of</span> <span class="teal">ElementId</span>)
&nbsp;
&nbsp; elementIds.Add(v.Id)
&nbsp;
&nbsp; copiedIds = <span class="teal">ElementTransformUtils</span>.CopyElements( _
&nbsp; &nbsp; activeDoc, elementIds, activeDoc, _
&nbsp; &nbsp; <span class="teal">Transform</span>.Identity, Options)
&nbsp;
&nbsp; vNew = activeDoc.GetElement(copiedIds(0))
&nbsp;
&nbsp; vNew.Name = <span class="maroon">&quot;New View Template&quot;</span>
</pre>

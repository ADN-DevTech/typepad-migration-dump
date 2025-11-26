---
layout: "post"
title: "Element Id &ndash; Export, Unique, Navisworks and Other Ids"
date: "2014-04-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "DWF"
  - "Element Relationships"
  - "Export"
  - "IFC"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/04/element-id-export-unique-navisworks-and-other-ids.html "
typepad_basename: "element-id-export-unique-navisworks-and-other-ids"
typepad_status: "Publish"
---

<p>Here are a couple of discussions on the topic of Revit element ids, unique ids, and export ids.
Some have been hanging around for a while, while others are more recent.</p>

<p>We already looked at some aspects of this in the discussion on

<a href="http://thebuildingcoder.typepad.com/blog/2012/09/ifc-guid-generation-and-uniqueness.html">
IFC GUID generation</a>.</p>

<p>Let's go through another iteration now:</p>

<ul>
<li><a href="#2">Unique Id versus ElementId to store in external database</a></li>
<li><a href="#3">Local Uniqueness of the Revit Unique Id</a></li>
<li><a href="#4">Navisworks versus Revit Object Unique Ids</a></li>
<li><a href="#5">Negative element ids and property drop-down list enumerations</a></li>
<li><a href="#6">Revit element id and unique id lost on reimporting</a></li>
<li><a href="#7">Autodesk University 2014 calls for proposals</a></li>
</ul>

<!-- http://forums.autodesk.com/t5/Revit-API/Unique-Id-vs-ElementId-which-identifier-to-stored-in-the/m-p/4981920 -->

<a name="2"></a>

<h4>Unique Id versus ElementId to Store in External Database</h4>

<p>Here is a summary of the recent Revit API forum discussion thread on

<a href="http://forums.autodesk.com/t5/Revit-API/Unique-Id-vs-ElementId-which-identifier-to-stored-in-the/m-p/4981920">
Unique Id vs ElementId</a> and

which identifier to store in an external database:</p>
<p><strong>Question:</strong> I am developing a Revit extension to export/import a Revit building model to structural analysis software.
In the external database, I am not sure which identifier to be stored in order to retrieve the same element after successive import/export call between Revit and analysis software.</p>

<p>I wish to implement something like this:</p>

<ol>
<li>After structural analysis software made changes to the building model, it exports these changes to an external database. My extension will load the external database, retrieve the 'changed' element based on the identifier (i.e. document.get_element(ElementId/UniqueId)) in Revit and apply the changes to the retrieved element. However these ids are used internally in Revit and I don't know how Revit generates them, which makes it hard to identify which element is changed outside Revit.</li>
<li>Revit user can also made changes to the building model and send these back to the structural analysis software. However, in the structural analysis software, there is no unique identifier for each element like in Revit. Hence, I think I need to create one, e.g. using a GUID. But this identifier is not the same as the one used in Revit. How do I generate an identifier to identify the element in both Revit and structural analysis software?</li>
</ol>

<p>I already received some suggestions, such as:</p>

<ol>
<li>On export from Revit, create a set of GUIDs for the exported elements. Besides that, each exported element's ElementId must be saved as well.</li>
</ol>

<p>How do I generate a GUID to identify the exported element?</p>

<ol start="2">
<li>On import to Revit, check for an existing object with the standard GUID or else create a new one.</li>
</ol>

<p>So, the structural analysis software needs to generate the GUID in the same way as described in (1) so that it is identical in both Revit and Analysis software. Please correct me if I am wrong or something I have missed out.</p>

<p>Does anyone have other suggestion? Any help is appreciated.</p>

<p><strong>Answer:</strong> The Document class has some GetElement methods to access Elements by is, which can be either an ElementId or a UniqueId.</p>

<p>The UniqueId is represented as a string, so you can retrieve an element like this:</p>

<pre>
  string uniqueId = something;
  Element fetch = doc.GetElement(uniqueId);
</pre>

<p>The opposite is:</p>

<pre>
  string elementGuid = element.UniqueId;
</pre>

<!-- by Arnošt Löbel: -->

<p>Note that Revit's UniqueId is not a GUID.
It is a string.
Part of the string is a GUID, but the whole thing is not &ndash; it needs to be treated as a string identifier.</p>

<p>To get a unique Id for an element use: Element.UniqueId</p>

<p>To retrieve an element by its unique Id: document.GetElement( uniqueId )</p>

<p>By the way, the difference between UniqueId and regular element Id is its stability (or instability, respectively) in a work-sharing environment. While regular element Ids are unique only in documents that are not work-shared, Unique Ids are stable and still unique even in document that are shared (and modified by) several local copies and then synchronizes with the server version.</p>

<p>Neither of the identifiers types are stable across individual Revit documents &ndash; it means that elements in different, independent Revit models may have the same Ids (as well as UniqueIds) assigned.</p>

<p>To keep track of structural element in a building model in both Revit and structural analysis software after successive import/export call between Revit and analysis software as described above, you can do like this:</p>

<ul>
<li>If the elements originate in Revit and come equipped with a unique id, I would use that to identify them in both environments.</li>

<li>If the elements originate outside of Revit and you need to assign a perpetual unique identifier to them before they automatically become equipped with a Revit unique id, I would use a standard GUID in addition, just like you described.
This makes things much more complicated than just using the Revit unique id, though.</li>
</ul>

<p>To <a href="http://lmgtfy.com/?q=.net+guid+generate">generate a GUID</a>, you can simply use the standard Windows or .NET API.</p>


<a name="3"></a>

<h4>Local Uniqueness of the Revit Unique Id</h4>

<p><strong>Question:</strong> Some people expect all the Element.UniqueId are formed of a GUID that should be unique for every file, and an element id snapshot taken in the moment of the creation, this would came very handy to developers to identify elements and projects, BUT, projects created by a template are merely a copy of the template, leaving the GUID and all pre-existent elements the same UniqueId...</p>

<p>I think this should be considered a bug and fixed, maybe changing the GUID during the saveAs method, instead copying a file from file system can be considered a misuse.</p>

<p><strong>Answer:</strong> It is not considered a bug, actually, no matter how odd might that seem to some. Revit does not promise uniqueness of its "unique" IDs across documents. It would be rather impossible, in fact, for Revit has no control of how files get created and manipulated outside of Revit (by copying and renaming, for example). The purpose and main advantage of unique Ids over regular element Ids is to give API users some stability over element identification in work-sharing environments. While standard Element Ids (integers) can be modified when synchronizing changes from local models to corresponding central file, unique Ids of those same changed elements will not be affected by work-sharing operations. It means that if one user took a hold of an element's unique Id before synchronizing with central, the same element would be identifiable by the same Id after the synchronization. On the other hand, using a regular Element Id obtained before synchronization may identify a totally different element after synchronization.</p>

<p><strong>Response:</strong> This caused us issues initially as we chose the use GUID as the unique identifier in our database.  This immediately causes issues, as the GUID's did not change from what was defined in a users template, so all their projects had the same GUID's.  At least that's how I vaguely remember it...  It could have a been a save as though...</p>

<p>If it doesn't, starting a new project from a template should reset all GUID's.</p>

<p><strong>Answer:</strong> I am sorry for the confusion, but like I said we have never promised ultimate uniqueness of elements across documents. Remapping Ids upon creation of new documents from a template would be quite an undertaking on our side, and even if we could pull it off it would still not guarantee absolute uniqueness of all elements across all documents.</p>

<p>When you populate certain elements in a new target project file, you could consider using the copy and paste API to bring them over from the source template project.</p>

<p>That should generate new unique ids for them.</p>



<a name="4"></a>

<h4>Navisworks versus Revit Object Unique Ids</h4>

<p><strong>Question:</strong> Is the UniqueId of a Revit element preserved in a Navisworks file?
Can it be accessed programmatically from Navisworks?</p>

<p><strong>Answer:</strong> Yes and no.
You can see the standard Revit element id in the properties pane in the Navisworks user interface.
Generally, everything in the properties pane can be also accessed via the API.</p>

<p>The Item tab in the NW properties pane also displays a 'GUID' property, also provided by Revit.
It can be queried in the Revit API using the ExportUtils.GetExportId method.
However, it is not the same as the Revit UniqueId.
Nowadays, the easiest way to correlate the export id with the Revit element id and UniqueId is to use the ExportUtils.GetExportId method.
Before its introduction, the

<a href="http://thebuildingcoder.typepad.com/blog/2009/02/uniqueid-dwf-and-ifc-guid.html">IFC GUID algorithm</a> provided

similar functionality.

Revit export IDs, used e.g. for DWF and IFC, are different in both purpose and meaning from the UniqueId.</p>

<p>The NW Element tab also displays a property called 'Id' which is the standard Revit element id.</p>

<p>You do not need to worry about the GUID and can use the Id of an element if all you do is reading from a document that is not being opened in an active worksharing session.
However, in a worksharing session, you need to use the UniqueId instead.
If you only hold on to an Id, you may get different elements for it at different times of the worksharing session, since element ids can be renumbered when synchronizing with the central model.</p>


<a name="5"></a>

<h4>Negative Element Ids and Element Property Drop-down List Enumerations</h4>

<p><strong>Question:</strong> I sometimes encounter a negative element id.
What is that?</p>

<p><strong>Answer:</strong> Revit uses negative element ids in various places.
They mostly refer to pre-defined constant values, e.g. built-in parameter or category enumeration values.
These may still refer to real elements in the model.</p>

<p>In some cases, negative element ids are also used to enumerate various parameter value options displayed in property palette drop-down lists.</p>

<p>In some cases, the Revit API does not expose any direct method to determine the string values corresponding to these negative element ids.
If so, you can find the matching relationship using RevitLookup.
Select the various target options one by one in the parameter drop-down list and use RevitLookup to see the resulting parameter values. Use this procedure to build a mapping between display strings and the corresponding integer parameter values.</p>



<a name="6"></a>

<h4>Revit Id and UniqueId Lost On Reimporting Revised Model</h4>

<p><strong>Question:</strong> We are having trouble reimporting a revised IFC file into Revit, because the id and unique id allocation is unreliable.</p>

<p>As a result, all references like dimensioning made to the referenced model (out of IFC) are lost after reimporting a revised model, which forces us to repeat the detailing of the imported model.</p>

<p>Is there any way to avoid this problem?
Better still, is there an algorithm to convert the IFC GUID into a UniqueId or Revit element ID?</p>

<p>I looked at the

<a href="http://thebuildingcoder.typepad.com/blog/2010/06/ifc-guid-algorithm-in-c.html">IFC GUID algorithm</a> and

the ExportUtils.GetExportId method.
These seem to apply to export from Revit but not import back in.
Is that the same algorithm?</p>

<p>Through our test we found out that each time a revised model is imported, the plugin and even the standard Revit IFC import allocates a new RevitID and UniqueId although it is the same object.
Even with the same model it's not granted that the ID's are stringently the same.</p>

<p>There must be a way &ndash; check out <a href="http://matteocominetti.com/category/bcf">Metteo Cominetti BCF</a>.</p>


<p><strong>Answer:</strong> The algorithm is the same, of course.</p>

<p>When reimporting an IFC model, you could also create a mapping element id --&gt; export id, and then invert that relationship to find the Revit elements from your export ids.</p>

<p>If you only have the export ids, then you need to create a mapping between Revit unique id and Revit export id beforehand, and use that to reimport.</p>

<p>Alternatively, store the Revit unique id in your export together with the export id.</p>

<p>You cannot use the export id to retrieve the Revit element, afaik.</p>

<!--
<p>Maybe I should add some more information.</p>

<p>I'm talking about importing a Tekla IFC Model. We are using the Tekla plugin which is under revision right now through Tekla. In our company both Revit and Tekla are used to generate models.</p>

<p>So my idea was to use the algorithm to avoid this problem which I already told Tekla. And I'm sure that it works out because otherwise the young guy from Finnland could not work out the BCF thing for Revit.</p>

<p>http://matteocominetti.com/category/bcf/</p>

<p>I'm myself just the idea generator not the one working on the code.</p>

<p>Anyhow I will let the guys from Tekla know your link so they should work on it.</p>
-->




<a name="7"></a>

<h4>Autodesk University 2014 Calls for Proposals</h4>

<p>Time for you to put down your thoughts, and me mine: the

<a href="http://auspeaker.wordpress.com/2014/04/23/the-au-2014-call-for-proposals-is-open">AU 2014 call for proposals is open</a>.</p>

<p>Submissions begin now, i.e. from April 23, 2014, onwards, and end at the proposals deadline on May 23, 2014.</p>

<p>As I

<a href="http://thebuildingcoder.typepad.com/blog/2014/04/revitlookup-for-ur1-adn-aec-and-au-news.html#5">
mentioned</a>,

the

<a href="https://cluster.ems-secure.de/registrations/autodesk/au.2014/ems.registration.php?s=main.summary.welcome&amp;l=de&amp;xid=8icbqrtfhpc93lu58imeuju7i4&amp;tracking=D0000000000&amp;s=main.lecture.welcome">
call for proposals for Autodesk University Germany 2014</a> is

already open, I plan to submit some there as well, and the deadline for that is June 2, 2014.</p>

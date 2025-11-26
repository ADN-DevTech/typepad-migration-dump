---
layout: "post"
title: "Family Symbols versus Types and SelectionFilterElement"
date: "2013-01-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "Family"
  - "Filters"
  - "Vasari"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/01/family-symbols-versus-types.html "
typepad_basename: "family-symbols-versus-types"
typepad_status: "Publish"
---

<p>Here is a question that has been asked a couple of times recently and thus seems worthwhile clarifying:

<p><strong>Question:</strong> If I access the OwnerFamily.Symbols in a family document, i.e. an RFA file, it returns an empty collection.
If I load that same family into a project and access the Family.Symbols property, it returns the correct expected family types.
Why does this property not work in the family editor?

<p>Put differently, the following code snippet will show zero in the message box:

<pre>
  Document fdoc = app.OpenDocumentFile(
    "C:/Family.rfa" );

  Family fam = fdoc.OwnerFamily;

  MessageBox.Show(fam.Symbols.Size.ToString());
</pre>

<p>Loading the same family into a project and then querying the symbol count returns the correct number:</p>

<pre>
  Document doc = app.OpenDocumentFile(
    "C:/Project.rvt" );

  Family fam = null;

  doc.LoadFamily( "C:/Family.rfa",
    new FamilyOption(), out fam );

  MessageBox.Show(fam.Symbols.Size.ToString());
</pre>

<p>Again, why does the Family.Symbols property not work in the family context?</p>


<p><strong>Answer:</strong> Why things are the way they are is often quite difficult to answer.
For that situation, I quite like the adage "Mine is not to wonder why, mine is but to do or die."

<p>However, if I am allowed to rephrase you question constructively and ask "How can I access the symbols defined by a family in the family editor context?", the answer is clear and easy:

<p>Use the FamilyManager class.
The family manager object is used to manage the family types and parameters within a family document.
You can access it through the Document.FamilyManager property on the family document object.

<p>Getting back to the why and wherefore, it is helpful to understand that the family does not contain any symbols.
It only contains types.
The types can be compared to records in a database.
The family parameters define the database fields, i.e. columns, and the types represent the records, i.e. rows.
You could go even further and compare the individual family parameters with the database fields.

<p>The symbols do not exist in the RFA.
They only spring into existence when the family is loaded into a project.

<p>Here is a slightly more detailed explanation:

<p>Family types and symbols are completely different objects with different purposes.

<p>There can be FamilySymbols in a family document.
If so, these are the symbols defined by other families that are loaded into the document.

<p>Family types live in the family where they are defined, whereas family symbols in a family always come from some other family loaded into it.

<ul>
<li>Types can add parameters, formulas, etc.  Symbols are limited to the definition in the source family.
<li>Types can be added and deleted at run time.  Symbols cannot, other than by special operations like Duplicate.
<li>Symbols can be instantiated in the document they live in.  Types cannot.
</ul>

<p>The FamilyType class in a family is not inherited from the Revit Element class, so types are not elements.
It offers the ability to access parameters either for read or write in conjunction with the FamilyManager.

<p>For certain operations in a family, such as getting an image from a type, you can set the type to be active using the FamilyManager.CurrentType property, then generate an image from a view of the family using the Document.Export method and an appropriate ImageExportOptions argument.


<a name="2"></a>

<h4>SelectionFilterElement</h4>

<p>Two posts of Harry Mattison's on the SelectionFilterElement introduced in the Revit 2013 API just caught my attention and also seem worthwhile pointing out here.</p>

<p>The SelectionFilterElement is a filter element that stores an explicit list of ElementIds.
Only elements whose ElementIds are contained in this list will pass the filter.

<p>I mentioned the

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/revit-2013-and-its-api.html#6">
existence of this class</a>

briefly and rather cryptically in the initial overview of the

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/revit-2013-and-its-api.html">
Revit 2013 API</a>.

<p>Harry now presents two running snippets of sample code using a SelectionFilterElement to

<a href="http://boostyourbim.wordpress.com/2012/12/11/save-a-set-of-elements-with-selectionfilterelement">
save a set of elements</a> and

subsequently

<a href="http://boostyourbim.wordpress.com/2012/12/15/retrieving-and-selecting-elements-in-a-selectionfilterelement">
retrieve and highlight them</a>.

<p>Definitely worth keeping in mind if you have a specific set of elements that you wish to access quickly.</p>



<a name="3"></a>

<h4>Dynamo and Vasari Holiday Hacking</h4>

<p>If the above is not enough for you, here is an invitation to venture further afield, into the realm of the Dynamo visual programming add-in for Revit:</p>

<ul>
<li><a href="http://autodeskvasari.com/profiles/blogs/dynamo-caution-elves-at-work">Dynamo holiday project overview</a></li>
<li><a href="http://autodeskvasari.com/profiles/blogs/dynamo-holiday-relaxation-aid">Dynamic Relaxation via Dynamo</a></li>
<li><a href="http://autodeskvasari.com/profiles/blogs/dynamo-new-video-relaxation-part-deux">Youtube video of results</a></li>
</ul>

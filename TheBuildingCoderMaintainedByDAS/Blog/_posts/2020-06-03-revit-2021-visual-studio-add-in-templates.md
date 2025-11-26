---
layout: "post"
title: "Revit 2021 Visual Studio Add-in Templates"
date: "2020-06-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2021"
  - "Forge"
  - "Migration"
  - "Parameters"
  - "Update"
  - "Wizard"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/06/revit-2021-visual-studio-add-in-templates.html "
typepad_basename: "revit-2021-visual-studio-add-in-templates"
typepad_status: "Publish"
---

<p>Our tidbits for today:</p>

<ul>
<li><a href="#2">Revit 2021 Visual Studio add-in templates</a></li>
<li><a href="#3">Four important design patterns</a></li>
<li><a href="#4">Invisible shared parameters don't make it into Forge</a></li>
</ul>

<h4><a name="2"></a> Revit 2021 Visual Studio Add-in Templates</h4>

<p>Wizard migration &ndash; <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard">VisualStudioRevitAddinWizard</a> is
now <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2021.0.0.0">available for Revit 2021</a>,
thanks to cflux, aka <a href="https://github.com/maltezc">@maltezc</a>, prompted by <a href="https://thebuildingcoder.typepad.com/blog/2019/04/revit-2020-c-and-vb-visual-studio-add-in-wizards.html#comment-4937289914">his initial comment</a>:</p>

<p><strong>Question:</strong> Is there an addin wizard for Revit 2021?</p>

<p><strong>Answer:</strong> Thank you for your motivating question.</p>

<p>Nope, I have not migrated it yet.</p>

<p>You asking for it significantly raises the chances of it happening soon, though.</p>

<p>Alternatively, of course, you can fork the repo, migrate it, and submit a pull request to share with the rest of the community.</p>

<p>Thank you!</p>

<p>Very kindly,
cflux <a href="https://thebuildingcoder.typepad.com/blog/2019/04/revit-2020-c-and-vb-visual-studio-add-in-wizards.html#comment-4938205631">went ahead</a>
and <a href="https://thebuildingcoder.typepad.com/blog/2019/04/revit-2020-c-and-vb-visual-studio-add-in-wizards.html#comment-4938471258">did it themselves</a>,
submitting <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/pull/12">pull request #12 &ndash; 2021 migration</a>.</p>

<p>I tested and confirmed that it works well for me.</p>

<p>Here is the <a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/compare/2020.0.0.5...2021.0.0.0">diff to the previous version</a>.</p>

<p>Many thanks to cflux for this helpful contribution!</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330263ec1f770f200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330263ec1f770f200c image-full img-responsive" alt="Wizard migration" title="Wizard migration" src="/assets/image_8682d7.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Four Important Design Patterns</h4>

<p>Here is a nice article
on <a href="https://www.freecodecamp.org/news/4-design-patterns-to-use-in-web-development">4 design patterns you should know for web development: observer, singleton, strategy, and decorator</a>.</p>

<p>In fact, they are just as important and useful in the context of the Revit API as well!</p>

<h4><a name="4"></a> Invisible Shared Parameters don't make it into Forge</h4>

<p>By the way, talking about wizards above quickly leads to the topic of magic and the question invisible objects...</p>

<p>In general, all Revit element properties are translated and available in the Forge viewer environment.</p>

<p>However, unfortunately for some, that is not always true.</p>

<p>This question just came up and was confirmed again as follows:</p>

<p><strong>Question:</strong> One of my family parameters is missing after translation from RVT to Forge.</p>

<p>Examining this parameter inside the family document in Revit using RevitLookup, I see that the missing parameter has a <code>Visible</code>: <code>false</code> setting in its definition.</p>

<p>Does this mean that the Revit extractor will ignore invisible parameters while exporting the property database?</p>

<p><strong>Answer:</strong> Yes indeed, Revit Extractor only handles visible parameters:</p>

<pre class="code">
  InternalDefinition^ idef
    = dynamic_cast<InternalDefinition^>(paramDef);

  if (idef)
  {
    if (!idef->Visible)
    {
      continue;
    }
    ...
  }
</pre>

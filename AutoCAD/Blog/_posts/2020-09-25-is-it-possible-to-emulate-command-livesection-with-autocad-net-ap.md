---
layout: "post"
title: "Is it possible to emulate command LIVESECTION with AutoCAD .NET AP"
date: "2020-09-25 03:00:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2020/09/is-it-possible-to-emulate-command-livesection-with-autocad-net-ap.html "
typepad_basename: "is-it-possible-to-emulate-command-livesection-with-autocad-net-ap"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a>
</p>
<p> This question is coming from one of our beloved Forum contributor and Mentor <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/481027">Alexander Rivilis</a> </p>
<blockquote>
	<p> I am working with code which change color of Intersection Fill of Live Section. But although the color is changed, but this is not visible on the screen until I turn off and turn LIVESECTION: </p>
</blockquote>
<p>I made slight changes to have this working</p>
<p> <strong>Note: To work this make sure LIVESECTION is not turned on</strong> </p>
<p>This code will change color of SECTION which converted to LIVESECTION</p> 
<pre class="prettyprint">    public void SetSectionColor()
       {
          Document doc = Application.DocumentManager.MdiActiveDocument;
          if (doc == null) return;
          Editor ed = doc.Editor;

          PromptEntityOptions peo = 
                new PromptEntityOptions("\nSelect section: ");
          peo.SetRejectMessage("\nIt is not a section!");
          peo.AddAllowedClass(typeof(Section), true);
          PromptEntityResult per = ed.GetEntity(peo);
          if (per.Status != PromptStatus.OK) return;
          ObjectId idSecSets = ObjectId.Null;
          using (Transaction tr =
          doc.TransactionManager.StartOpenCloseTransaction())
          {
              Section sec =
              tr.GetObject(per.ObjectId,
                           OpenMode.ForWrite) as Section;
              sec.IsLiveSectionEnabled = true;                
              idSecSets = sec.Settings;
              tr.Commit();
          }
        using (Transaction tr = doc.TransactionManager
                                     .StartOpenCloseTransaction())
        {
          SectionSettings secset = tr.GetObject(idSecSets, 
                                   OpenMode.ForWrite) as SectionSettings;
          secset.CurrentSectionType = SectionType.LiveSection;
          Color clr = secset.Color(SectionType.LiveSection,
                                   SectionGeometry.IntersectionFill);
          ColorDialog cd = new ColorDialog
          {
              Color = clr
          };
          System.Windows.Forms.DialogResult dr = cd.ShowDialog();
          if (dr != System.Windows.Forms.DialogResult.OK) return;
          ed.WriteMessage("\nSelected Color: " + cd.Color.ToString());
          clr = cd.Color;
          // Define that color we change
          secset.SetColor(SectionType.LiveSection,
          SectionGeometry.IntersectionFill, clr);
          tr.Commit();
        }  
    }
</pre>
<p>Gif</p>
<p>
<a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be414a919200d-pi"><img width="749" height="548" title="LiveSectioning" style="display: inline;" alt="LiveSectioning" src="/assets/image_265925.jpg"></a></p>

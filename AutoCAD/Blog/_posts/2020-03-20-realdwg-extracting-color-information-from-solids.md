---
layout: "post"
title: "RealDWG: Extracting Color Information from Solids"
date: "2020-03-20 13:26:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2020/03/realdwg-extracting-color-information-from-solids.html "
typepad_basename: "realdwg-extracting-color-information-from-solids"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>Using RealDWG SDK,&nbsp; following code extracts the color information of 3d solids in AutoCAD drawings.</p><p>To extract color from sub elements of Solid we will use BREP api, you need to refer AcDbMgdBrep.dll module from sdk.</p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a518dc4a200b-pi"><img width="154" height="244" title="acdbmgdbrep" style="display: inline; background-image: none;" alt="acdbmgdbrep" src="/assets/image_246536.jpg" border="0"></a></p><p>You can use – <a href="https://github.com/MadhukarMoogala/MyBlogs/blob/master/TestFiles/testBox.dwg">Testdrawing</a>&nbsp;</p>
<pre class="prettyprint lang-cs">        private void DumpSolid3d(Transaction tr, ObjectId solId)
        {
            using (OpenCloseTransaction oct = new OpenCloseTransaction())
            {
                Solid3d solid = tr.GetObject(solId, OpenMode.ForRead) as Solid3d;

                ObjectId[] ids = new ObjectId[] { solid.ObjectId };

                FullSubentityPath path =
                  new FullSubentityPath(
                    ids,
                    new SubentityId(SubentityType.Null, IntPtr.Zero)
                  );

                // For storing SubentityIds of cylindrical faces

                List<subentityid> subentIds = new List<subentityid>();

                using (Brep brep = new Brep(path))
                {
                    foreach (Autodesk.AutoCAD.BoundaryRepresentation.Face face in brep.Faces)
                    {
                        /*
                        // How to get colors of only cylinderical
                         
                        Autodesk.AutoCAD.Geometry.Surface surf = face.Surface;
                        var ebSurf = surf as Autodesk.AutoCAD.Geometry.ExternalBoundedSurface;
                        if (ebSurf != null &amp;&amp; ebSurf.IsCylinder)
                        {
                            subentIds.Add(face.SubentityPath.SubentId);
                        }
                        
                         */
                        subentIds.Add(face.SubentityPath.SubentId);
                    }
                }

                if (subentIds.Count &gt; 0)
                {
                    List<keyvaluepair><color  , subentityid="">&gt; ColorSubEntityPairs
		     = GetColors(solid, subentIds);
                    foreach (var item in ColorSubEntityPairs)
                    {
                        var color = item.Key as Color;
                        SubentityId id = item.Value;
                        Console.WriteLine($"\nColor - {color.ColorNameForDisplay}
					 | SubEntity Index - {id.IndexPtr}");
                    }
                }
               oct.Commit();
            }
        }

        List&lt; KeyValuePair&lt; Color,SubentityId &gt;&gt; 
	GetColors(Solid3d solid, List<subentityid> subentIds)
        {
            List &lt; KeyValuePair &lt; Color, SubentityId &gt;&gt; ColorSubEntityPairs
		= new List&lt; KeyValuePair <color  , subentityid="">&gt;();
            foreach (SubentityId subentId in subentIds)
            {
                try
                {
                    Color col = solid.GetSubentityColor(subentId);
                    ColorSubEntityPairs.Add(new KeyValuePair<color  , subentityid="">(col, subentId));
                }
                catch (Autodesk.AutoCAD.BoundaryRepresentation.Exception ex)
                {
                    if(ex.ErrorStatus == ErrorStatus.NotApplicable)
                    continue;
                }
                

                
            }
            return ColorSubEntityPairs;
        }

</color,></color,></subentityid></keyvaluepair<color  ,></subentityid></subentityid></pre>


<p>
Output 
<p>
<pre class="prettyprint lang-bsh">SolidColors.exe "C:\rd2020\RealDWG 2020\Samples\SolidColors\testBox.dwg"

BlockTable -*Model_Space

Color - 250 | SubEntity Index - 1

Color - 250 | SubEntity Index - 2

Color - 250 | SubEntity Index - 3

Color - 250 | SubEntity Index - 4

Color - 250 | SubEntity Index - 5

Color - 250 | SubEntity Index - 6

Color - 250 | SubEntity Index - 7

Color - 50 | SubEntity Index - 8

Color - 50 | SubEntity Index - 9

Color - 50 | SubEntity Index - 10

Color - 50 | SubEntity Index - 11

Color - 50 | SubEntity Index - 12

Color - 50 | SubEntity Index - 13

BlockTable -*Paper_Space

BlockTable -*Paper_Space0

</pre>

<p> <a href="https://github.com/MadhukarMoogala/MyBlogs/blob/master/SolidColors.zip">Source code</a></p>

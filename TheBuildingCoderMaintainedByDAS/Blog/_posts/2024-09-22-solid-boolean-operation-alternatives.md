---
layout: "post"
title: "Solid Boolean Operation Alternatives"
date: "2024-09-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "External"
  - "Geometry"
  - "Precision"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/09/solid-boolean-operation-alternatives.html "
typepad_basename: "solid-boolean-operation-alternatives"
typepad_status: "Publish"
---

<p><link href="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/themes/prism.min.css" rel="stylesheet" /></p>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/components/prism-core.min.js"></script>

<script src="https://cdn.jsdelivr.net/npm/prismjs@1.29.0/plugins/autoloader/prism-autoloader.min.js"></script>

<p><style> code[class*=language-], pre[class*=language-] { font-size : 90%; } </style></p>

<p>A recurring topic in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> concerns
problems with solid Boolean operations:</p>

<ul>
<li><a href="#2">Revit Booleans and OpenCascade</a></li>
<li><a href="#3">CGAL solid Booleans</a></li>
</ul>

<h4><a name="2"></a> Revit Booleans and OpenCascade</h4>

<p>The question came up once again recently in the query
on <a href="https://forums.autodesk.com/t5/revit-api-forum/booleanoperationsutils-executebooleanoperation/m-p/12971195"><code>BooleanOperationsUtils</code> <code>ExecuteBooleanOperation</code> <code>InvalidOperationException</code> cause</a>:</p>

<p><strong>Question:</strong>
<code>BooleanOperationsUtils.ExecuteBooleanOperation</code>: When I use this method, I often get an <code>InvalidOperationException</code>.
I would like to know if there are specific criteria for causing that error.
For example, it occurs when elements intersect at an angle below a certain angle, etc.
I hope there are rules for this or some documentation I can refer to.</p>

<p><strong>Answer:</strong>
Sorry about that.
It has been mentioned before that <code>ExecuteBooleanOperation</code> can run into issues.
Unfortunately, afaik, there is no list of the exact criteria which might cause a problem.
Various ways of handling the situation and some possible workarounds have been discussed here in the past.
You can search
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>
for <a href="https://forums.autodesk.com/t5/forums/searchpage/tab/message?advanced=false&amp;allow_punctuation=false&amp;filter=location&amp;location=forum-board:160&amp;q=ExecuteBooleanOperation">ExecuteBooleanOperation</a> or
just <a href="https://forums.autodesk.com/t5/forums/searchpage/tab/message?filter=location&amp;q=Boolean&amp;noSynonym=false&amp;location=forum-board:160&amp;collapse_discussion=true">Boolean</a> to
find some of them.</p>

<p>In Revit, one also encounters situations like this:</p>

<p><center>
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3bd63d6200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3bd63d6200c img-responsive" style="width: 273px; display: block; margin-left: auto; margin-right: auto;" alt="Red object to subtract from white object" title="Red object to subtract from white object" src="/assets/image_488bcb.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Red object to subtract from white object</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3bd63f1200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3bd63f1200c img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Zoom to the corner" title="Zoom to the corner" src="/assets/image_b55288.jpg" /></a></p>

<p style="font-size: 80%; font-style:italic">Zoom to the corner</p>

<p></center></p>

<p>The development team are aware of these issues.
<a href="https://forums.autodesk.com/t5/revit-api-forum/boolean-operation-fail/m-p/12839281">Boolean operation fail</a> is
an exhaustive and long-ongoing discussion of the topic including a suggestion
by Tommy <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/4635064">@tommy.stromhaug</a> Strømhaug
for a non-trivial workaround using <a href="https://dev.opencascade.org/">OpenCascade</a>.</p>

<h4><a name="3"></a> CGAL Solid Booleans</h4>

<p>Andrey <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/11836042">@ankofl</a> Kolesov
recently shared a solution using
the <a href="https://en.wikipedia.org/wiki/CGAL">Computational Geometry Algorithms Library CGAL</a> and
the <a href="https://en.wikipedia.org/wiki/OFF_(file_format)">OFF file format</a> to
perform Boolean operations on solids, presented in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-execute-booleanoperations-on-revit-solid-by-autocad/m-p/13005223">how to execute Boolean operations on Revit solid by AutoCAD</a>:</p>

<p>After extensive discussion and in-depth research on transferring the solids to AutoCAD, OpenCascade, or some other library (check out the discussion threads mentioned above for that), Andrey opted for a different solution, saying:</p>

<p>Well, it seems I'm finally close to a solution that suits me:</p>

<p>Create <code>.off</code> file from Revit solid:</p>

<pre><code class="language-cs">public static bool WriteOff(
  this Solid solid,
  out List&lt;string&gt; listString)
  {
    listString = ["OFF"];

    if(solid.CreateMesh(out var listVectors, out var listTri))
    {
      listString.Add($"{listVectors.Count} {listTri.Count} 0");
      listString.Add($"");

      foreach (var p in listVectors)
      {
        listString.Add(p.Write());
      }

      foreach (var v in listTri)
      {
        listString.Add($"3  {v.iA} {v.iB} {v.iC}");
      }

      return true;
    }
    return false;
  }</code></pre>

<p>And this:</p>

<pre><code class="language-cs">public static bool CreateMesh(
  this Solid solid,
  out List&lt;XYZ&gt; listVectors,
  out List&lt;Tri&gt; listTri)
  {
    double k = UnitUtils.ConvertFromInternalUnits(1, UnitTypeId.Meters);
    listVectors = [];
    listTri = [];

    bool allPlanar = true;

    int indV = 0;
    foreach (Face face in solid.Faces)
    {
      if (face is PlanarFace pFace)
      {
        Mesh mesh = pFace.Triangulate();
        for (int tN = 0; tN &lt; mesh.NumTriangles; tN++)
        {
          var tri = mesh.get_Triangle(tN);

          var pT = new int[3];

          for (int vN = 0; vN &lt; 3; vN++)
          {
            var p = tri.get_Vertex(vN) * k;

            if (p.Contain(listVectors, out XYZ pF, out int index))
            {
              pT[vN] = index;
            }
            else
            {
              pT[vN] = indV;
              listVectors.Add(p);
              indV++;
            }
          }

          listTri.Add(new(pT[2], pT[1], pT[0]));
        }
      }
      else
      {
        allPlanar = false;
      }
    }

    return allPlanar;
  }</code></pre>

<p>Load <code>.off</code> file</p>

<pre><code class="language-cs">#pragma once

bool load_from(const char* path, Mesh& output) {
  output.clear();
  std::ifstream input;
  input.open(path);
  if (!input) {
      return false;
  }
  else if (!(input &gt;&gt; output)) {
      return false;
  }

  input.close();
  return true;
}</code>></pre>

<p>Execute Boolean</p>

<pre><code class="language-cs">#pragma once

bool boolean_simple(Mesh m1, Mesh m2, b_t type, Mesh& out) {
  out.clear();
  int code = 0;
  if (!CGAL::is_triangle_mesh(m1)) {
    PMP::triangulate_faces(m1);
  }
  if (!CGAL::is_triangle_mesh(m2)) {
    PMP::triangulate_faces(m2);
  }
  if (type == b_t::join) {
    if (!PMP::corefine_and_compute_union(m1, m2, out)){
      std::cout &lt;&lt; "fail_join ";
      return false;
    }
  }
  else if (type == b_t::inter) {
    if (!PMP::corefine_and_compute_intersection(m1, m2, out)){
      std::cout &lt;&lt; "fail_inter ";
      return false;
    }
  }
  else if (type == b_t::dif) {
    if (!PMP::corefine_and_compute_difference(m1, m2, out)) {
      std::cout &lt;&lt; "fail_dif ";
      return false;
    }
  }
  else {
    throw;
  }
  return true;
}</code></pre>

<p>Save <code>.off</code> file:</p>

<pre><code class="language-cs">#pragma once
#include &lt;CGAL/Polygon_mesh_processing/IO/polygon_mesh_io.h&gt;

bool save_to(const std::string path, Mesh input) {
  if (!CGAL::is_valid_polygon_mesh(input)) {
    return false;
  }
  try {
    if (CGAL::IO::write_polygon_mesh(path + ".off", input,
      CGAL::parameters::stream_precision(17)))
    {
      return true;
    }
    else {
      return false;
    }
  }
  catch (const std::exception& e) {
    std::cout &lt;&lt; "save_to: exception!" &lt;&lt; std::endl;
  }
  return false;
}</code></pre>

<p>Then you can upload the <code>.off</code> file back to Revit, or do other manipulations with it. However, as far as I know, API Revit does not allow you to create a full-fledged Solid object, but only a triangular grid, i.e. you can upload the grid obtained through CGAL to Revit for viewing, but you will not be able to perform further operations on solid with it, but only view its geometry through DirectShape.</p>

<p>Currently, my main task is creating an energy model of a building and calculating heat loss, the energy required to maintain the internal temperature in rooms at a given temperature outside, taking into account the thermal resistance to heat transfer of building structures.
I implemented the intersection of 3D surfaces in 2D and converting the result back to 3D to determine the intersection of indoor surfaces with outdoor space:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3c0eb11200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3c0eb11200b image-full img-responsive" alt="Intersection of indoor surfaces with outdoor space" title="Intersection of indoor surfaces with outdoor space" src="/assets/image_bab428.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></p>

<p>Many thanks to Andrey for sharing this solution that will hopefully help many others struggling with problematic solid Boolean operations.</p>

<p>Looking at the history of <a href="https://en.wikipedia.org/wiki/CGAL">CGAL</a>, I see that it also includes the very powerful <a href="https://en.wikipedia.org/wiki/Library_of_Efficient_Data_types_and_Algorithms">LEDA Library of Efficient Data types and Algorithms</a> that I looked into myself a long time ago, before it was merged into CGAL. LEDA is very impressive in itself, so CGAL must be quite something.</p>

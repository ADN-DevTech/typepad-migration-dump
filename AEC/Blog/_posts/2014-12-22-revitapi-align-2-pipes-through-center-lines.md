---
layout: "post"
title: "RevitAPI: Align 2 pipes through center lines"
date: "2014-12-22 02:46:10"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2014/12/revitapi-align-2-pipes-through-center-lines.html "
typepad_basename: "revitapi-align-2-pipes-through-center-lines"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
</p>
<p>A customer wants to use RevitAPI to align 2 pipes through their centerlines, I think it is feasible, here is what I plan to do:</p>
<ol>
<li>Get center line geomerty objects from pipes via Element.get_Geometry()</li>
<li>Create the alignment via RevitDoc.Create.NewAlignment()</li>
</ol>
<p>But however, I got an ArgumentException saying: One of the conditions for the inputs was not satisfied. Consult the documentation for requirements for each argument.</p>
<p>Headache...</p>
<p>With the help of Phil from development team in China, I made it work, the key point is that I should first move the pipe to align with the other physically. Just like what documentation says:</p>
<blockquote>These references must be already geometrically aligned (this function will not force them to become aligned).</blockquote>
<p>I did so, and it works, happy!</p>
<p>Here are the keys of the achievement:</p>
<ol>
<li>Make sure geometry options&#39;s <strong>ComputeReferences</strong> property is set to true, this ensures we can get the reference of the geometry objects retrieved.</li>
<li>Make sure geometry options&#39;s <strong>IncludeNonVisibleObjects</strong> is set to true and <strong>View</strong> property is set to plan view, this ensures we can get the geometry of the hidden centerline.</li>
<li>Make sure the pipes are geometrically aligned, e.g. by calling ElementTransformUtils.MoveElement() method.</li>
</ol>
<p>Here is the whole code:</p>
<pre class="prettyprint">//Assume the 2 pipes are parallel
//Assume the ActiveView is a plan view
private static Dimension
    Align2PipesViaCenterline(Pipe pipeBase, Pipe pipe)
{
    Dimension dimension = null;
    Document doc = pipeBase.Document;
    View view = doc.ActiveView;
    Line baseLine = GetCenterline(pipeBase);
    if (baseLine == null) return null;
    Line line = GetCenterline(pipe);
    if (line == null) return null;
    var clone = line.Clone();
    clone.MakeUnbound();
    IntersectionResult result = clone.Project(baseLine.Origin);
    if (result != null)
    {
        var point = result.XYZPoint;
        var translate = baseLine.Origin - point;
        using (Transaction transaction = new Transaction(doc))
        {
            try
            {
                transaction.Start(&quot;Align pipes&quot;);
                ElementTransformUtils.MoveElement(
                    doc, pipe.Id, translate);
                dimension = doc.Create.NewAlignment(view, 
                    baseLine.Reference, line.Reference);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                transaction.RollBack();
            }
        }
    }
    return dimension;
}

private static Line GetCenterline(Pipe pipe)
{
    Options options = new Options();
    options.ComputeReferences = true; //!!!
    options.IncludeNonVisibleObjects = true; //!!! 
    if (pipe.Document.ActiveView != null)
        options.View = pipe.Document.ActiveView;
    else
        options.DetailLevel = ViewDetailLevel.Fine;<br />
    var geoElem = pipe.get_Geometry(options);
    foreach (var item in geoElem)
    {
        Line lineObj = item as Line;
        if (lineObj != null)
        {
            return lineObj;
        }
    }
    return null;
}
</pre>

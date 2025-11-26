---
layout: "post"
title: "Types not supported by ElementClassFilter"
date: "2014-12-11 03:02:36"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2014/12/types-not-supported-by-elementclassfilter.html "
typepad_basename: "types-not-supported-by-elementclassfilter"
typepad_status: "Publish"
---

<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>When we are using ElementClassFilter to filter elements, we may get this kind of Exception:</p>
<blockquote>
<p>Autodesk.Revit.Exceptions.ArgumentException: Input type(Autodesk.Revit.DB.DetailLine) is of an element type that exists in the API, but not in Revit&#39;s native object model. Try using Autodesk.Revit.DB.CurveElement instead, and then postprocessing the results to find the elements of interest.</p>
</blockquote>
<p>As the exception message suggested, we can use its base class to filter them, and then post process the result, let&#39;s see an example code:</p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<pre class="prettyprint">var lineFilter = 
    new ElementClassFilter( typeof( CurveElement));
FilteredElementCollector filteredElements = 
    new FilteredElementCollector (familyDoc );
filteredElements = filteredElements.WherePasses( lineFilter);
foreach (CurveElement element in filteredElements )
{
    DetailLine detailLine = element as DetailLine;
    if ( detailLine != null)
    {
        var curv = detailLine .Location as LocationCurve;
        var lin = curv. Curve as Line;
        if ( lin != null)
        {
            // do what you like
        }
    }
}
</pre>
<p>After a short investigation, I found all of those Element derived types and their substitutions as for <strong>Revit 2015</strong>, see below list:</p>
<table border="1">
<tbody>
<tr>
<td>Structure.AreaReinforcementCurve</td>
<td>CurveElement</td>
</tr>
<tr>
<td>CurveByPoints</td>
<td>CurveElement</td>
</tr>
<tr>
<td>DetailCurve</td>
<td>CurveElement</td>
</tr>
<tr>
<td>DetailArc</td>
<td>CurveElement</td>
</tr>
<tr>
<td>DetailEllipse</td>
<td>CurveElement</td>
</tr>
<tr>
<td>DetailLine</td>
<td>CurveElement</td>
</tr>
<tr>
<td>DetailNurbSpline</td>
<td>CurveElement</td>
</tr>
<tr>
<td>ModelCurve</td>
<td>CurveElement</td>
</tr>
<tr>
<td>ModelArc</td>
<td>CurveElement</td>
</tr>
<tr>
<td>ModelEllipse</td>
<td>CurveElement</td>
</tr>
<tr>
<td>ModelHermiteSpline</td>
<td>CurveElement</td>
</tr>
<tr>
<td>ModelLine</td>
<td>CurveElement</td>
</tr>
<tr>
<td>ModelNurbSpline</td>
<td>CurveElement</td>
</tr>
<tr>
<td>SymbolicCurve</td>
<td>CurveElement</td>
</tr>
<tr>
<td>Architecture.Room</td>
<td>SpatialElement</td>
</tr>
<tr>
<td>Area</td>
<td>SpatialElement</td>
</tr>
<tr>
<td>Mechanical.Space</td>
<td>SpatialElement</td>
</tr>
<tr>
<td>Architecture.RoomTag</td>
<td>SpatialElementTag</td>
</tr>
<tr>
<td>AreaTag</td>
<td>SpatialElementTag</td>
</tr>
<tr>
<td>Mechanical.SpaceTag</td>
<td>SpatialElementTag</td>
</tr>
<tr>
<td>AnnotationSymbolType</td>
<td>FamilySymbol</td>
</tr>
<tr>
<td>AreaTagType</td>
<td>FamilySymbol</td>
</tr>
<tr>
<td>Architecture.RoomTagType</td>
<td>FamilySymbol</td>
</tr>
<tr>
<td>Mechanical.SpaceTagType</td>
<td>FamilySymbol</td>
</tr>
<tr>
<td>Structure.TrussType</td>
<td>FamilySymbol</td>
</tr>
<tr>
<td>AnnotationSymbol</td>
<td>FamilyInstance</td>
</tr>
<tr>
<td>Mullion</td>
<td>FamilyInstance</td>
</tr>
<tr>
<td>Panel</td>
<td>FamilyInstance</td>
</tr>
<tr>
<td>Architecture.Fascia</td>
<td>HostedSweep</td>
</tr>
<tr>
<td>Architecture.Gutter</td>
<td>HostedSweep</td>
</tr>
<tr>
<td>SlabEdge</td>
<td>HostedSweep</td>
</tr>
<tr>
<td>CombinableElement</td>
<td>a combination of GenericForm and GeomCombination</td>
</tr>
</tbody>
</table>
<p><strong>Note:</strong> A special case is <strong>Element</strong>, which can not be used either, one alternative I like is using LogicalOrFilter, example:</p>
<pre class="prettyprint">new LogicalOrFilter (new ElementIsElementTypeFilter( false), 
    new ElementIsElementTypeFilter (true ));
</pre>
<p><a href="http://blog.csdn.net/lushibi/article/details/41870743">中文链接</a></p>

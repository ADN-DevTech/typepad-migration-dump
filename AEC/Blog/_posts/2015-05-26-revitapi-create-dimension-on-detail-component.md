---
layout: "post"
title: "RevitAPI: Create dimension on detail component"
date: "2015-05-26 03:21:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/05/revitapi-create-dimension-on-detail-component.html "
typepad_basename: "revitapi-create-dimension-on-detail-component"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/45825291">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>Below are 2 detail components, each is actually a detail line in the family.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78c21e9970b-pi" style="display: inline;"><img alt="CreateDimensionOnDetailComponent_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c78c21e9970b img-responsive" src="/assets/image_473999.jpg" title="CreateDimensionOnDetailComponent_0" /></a></p>
<p>What dimension we want to create is like this:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78c21f0970b-pi" style="display: inline;"><img alt="CreateDimensionOnDetailComponent" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c78c21f0970b img-responsive" src="/assets/image_650747.jpg" title="CreateDimensionOnDetailComponent" /></a></p>
<p>We know the Document.Create.NewDimension method is:</p>
<pre class="csharp prettyprint">Dimension NewDimension(View view, 
    Line line, ReferenceArray references)</pre>
<p>First 2 arguments are easy: one is the view to create on, the other is the position of the dimension,</p>
<p>So the last one ReferenceArray is the references of the geometry object on these 2 components, but how to get them?</p>
<p>Obviously, using (FamilyInstance.Location as LocationCurve).Curve.Reference, we can only get null reference</p>
<p>So, only Element.get_Geometry way is left, then here is the code:</p>
<pre class="csharp prettyprint">private static Line GetReferenceOfDetailComponent(Element element,
    View view)
{
    Options options = new Options();
    options.ComputeReferences = true;
    options.IncludeNonVisibleObjects = true;
    if (view != null)
        options.View = view;
    else
        options.DetailLevel = ViewDetailLevel.Fine;
    var geoElem = element.get_Geometry(options);
    foreach (var item in geoElem)
    {
        Line line = item as Line;
        if (line != null)
        {
            //in this case, code will never be executed to here
        }
        else
        {
            GeometryInstance geoInst = item as GeometryInstance;
            if (geoInst != null)
            {
                GeometryElement geoElemTmp = 
                    geoInst.GetSymbolGeometry();
                foreach (GeometryObject geomObjTmp in geoElemTmp)
                {
                    Line line2 = geomObjTmp as Line;
                    if (line2 != null)
                    {
                        //check if it is what you want
                        if (line2.GraphicsStyleId.IntegerValue == 355)
                        {
                            return line2;
                        }
                    }
                }
            }
        }
    }
    return null;
}</pre>
<p><br /> Note:</p>
<ul>
<li>Options.ComputeReferences must be set to true, otherwise the Reference of any geometry object is null</li>
<li>When iterating GeometryElement, if we fount a GeometryInstance, then call GetSymbolGeometry rather than GetInstanceGeometry to get more geometry objects, otherwise, they are still not suitable to create dimension.</li>
</ul>

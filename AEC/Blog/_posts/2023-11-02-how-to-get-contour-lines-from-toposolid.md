---
layout: "post"
title: "How to get contour lines from Toposolid"
date: "2023-11-02 05:43:15"
author: "Caroline Gitonga"
categories:
  - ".NET"
  - "Caroline Gitonga"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2023/11/how-to-get-contour-lines-from-toposolid.html "
typepad_basename: "how-to-get-contour-lines-from-toposolid"
typepad_status: "Publish"
---

<p>By <a href="https://adndevblog.typepad.com/aec/carol.html">Carol Gitonga</a></p>
<p>Toposolid elements represent the topography and site conditions of your model.</p>
<p>Let us explore on how to get the contour lines from the Toposolid using the Revit API. Toposolid contour lines can be accessed via view-specific geometry. For a quick observation from the Revit UI before even diving into coding, we can access the displaying view and use Revit Lookup to retrieve the Toposolid contour lines via Revit <code>GeometryElement</code> class.</p>
<p>See the RevitLookup snippet below:</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a1b655200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="Lookup (1)" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a1b655200b image-full img-responsive" src="/assets/image_227620.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Lookup (1)" /></a></p>
<p>The snippet provides us with all the information about the Toposolid we would be interested with, together with the associated contour line. As show, you are able to drill down to the actual contour line and its properties. With the above information at hand, its straightforward to explore the same same programmatically.&#0160;</p>
<p>We first need to define the geometry option settings since Toposolid we have realized from RevitLookup that Toposolid has a solid geometry of its own.</p>
<p><strong>An important note:</strong></p>
<p>Make sure geometry options <code>View</code> property is set to <code>doc.ActiveView</code> this ensures we can get the geometry of the centerline in the Revit active view.</p>
<pre class="prettyprint">            Options options = app.Create.NewGeometryOptions();<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;options.View = doc.ActiveView;
</pre>
<p>An interesting observation has come up from Ning Zhou on the forum post: <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-contour-lines-from-toposolid/m-p/12315476" rel="noopener" target="_blank">https://forums.autodesk.com/t5/revit-api-forum/how-to-get-contour-lines-from-toposolid/m-p/12315476</a></p>
<p>Using the same code approach as below, TopographySurface implementation for getting contour lines is straightforward as seen from the RevitLookUp.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39da887200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="TopographySurface" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39da887200c img-responsive" src="/assets/image_973980.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="TopographySurface" /></a></p>
<p>However, using the same code approach for Toposolid, the contour lines are not visible as seen on the RevitLookUp.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a1b635200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="Toposolid" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a1b635200b img-responsive" src="/assets/image_133922.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Toposolid" /></a></p>
<p>Using <code>options.View = doc.ActiveView;</code> makes the Toposolid contour lines visible in the code implementation as shown:</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a1b66d200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false"><img alt="Toposolid success" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a1b66d200b img-responsive" src="/assets/image_344328.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Toposolid success" /></a></p>
<p>Acknowledgment to <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-contour-lines-from-toposolid/m-p/12315476">Ning Zhou</a> for&#0160; a working sample on how to get contour lines from Toposolid.</p>
<p>The sample below shows the full implementation on how to set the geometry settings, retrieving the number of contour lines from a Toposolid as well as getting <code>GraphicsStyle</code> of each contour line.</p>
<pre class="prettyprint">using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace GetTopoSolidContourLines
{
    [Transaction(TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, 
                                   ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //Define a reference Object to accept the pick result
            Reference pickedref = null;

            //Pick a group
            Selection sel = uiapp.ActiveUIDocument.Selection;
            pickedref = sel.PickObject(ObjectType.Element, <br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;Please select a toposolid&quot;);
            Element elem = doc.GetElement(pickedref);

            if (elem != null)
            {
                Toposolid ts = elem as Toposolid;
                GetTopoSolidContourLines(doc, ts, app);
            }


            return Result.Succeeded;
        }

        private void GetTopoSolidContourLines(Document doc, 
                                              Toposolid ts, 
                                              Application app)
        {

            string lineGs = &quot;&quot;;
            Options options = app.Create.NewGeometryOptions();
            options.View = doc.ActiveView;
            
            GeometryElement geomElement =ts.get_Geometry(options);
            TaskDialog.Show(&quot;Number of contour lines on the Toposolid is: &quot;, 
                                           geomElement.Count().ToString());

            foreach(GeometryObject geometry in geomElement)
            {
                Line line = geometry as Line;
                if (line != null)
                {
                    ElementId gsId = line.GraphicsStyleId as ElementId;
                    GraphicsStyle gs = doc.GetElement(gsId) as GraphicsStyle;

                    string gsName = gs.Name;
                    lineGs += gsName + &quot;\n&quot;;
                }
               
            }
            TaskDialog.Show(&quot;Revit Toposolid Contour lines Graphic Styles&quot;, lineGs);

            ;
        }
    }
}

</pre>
<p>Thanks to my colleague Jeremy for the detailed explanation on the topic in his blog: The Building Coder: <a href="https://thebuildingcoder.typepad.com/blog/2010/05/curtain-wall-geometry.html">https://thebuildingcoder.typepad.com/blog/2010/05/curtain-wall-geometry.html</a> and the forum post on Revit community forum by Ning Zhou : <a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-contour-lines-from-toposolid/m-p/12315476">Solved: Re: how to get contour lines from toposolid - Autodesk Community - Revit Products&#0160;</a><a href="https://forums.autodesk.com/t5/revit-api-forum/how-to-get-contour-lines-from-toposolid/m-p/12315476"></a> that has led to discussion of this topic further</p>

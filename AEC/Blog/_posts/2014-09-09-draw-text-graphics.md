---
layout: "post"
title: "Draw Text  Graphics"
date: "2014-09-09 21:58:31"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/aec/2014/09/draw-text-graphics.html "
typepad_basename: "draw-text-graphics"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>I do not see Navisworks 2015 API has not yet exposed the ability to draw text with <a href="http://adndevblog.typepad.com/aec/2013/05/navisworks-2014-api-new-feature-renderplugin.html">Renderplugin</a> and <a href="http://adndevblog.typepad.com/aec/2013/06/navisworks-2014-api-new-feature-toolplugin.html">ToolPlugin</a>. This reminds me of the workaround I shared with my colleague Miro in consulting team early this year.</p>
<p>We can draw the text ourselves. The challenge is to get the strokes of the text. Fortunately, Windows API provides something: FormattedText, PathGeometry etc , by which, I drew the text as below. Not too bad, isn’t it? </p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6dd225c970b-pi"><img alt="CustomText" height="264" src="/assets/image_638623.jpg" style="display: inline;" title="CustomText" width="341" /></a></p>
<p>By different Font (typeface), the strokes would be much different. So to have the simplest scenario, I use TXT in which most strokes are polyline only (at least for the alphanumeric and some basic letters). If using other Font, you will have to handle the scenarios of arc, even poly bezier etc.</p>
<p>There will be more works ahead, to make the text appearing nicely according to your requirement, but this demo could be a start. Of course, if API exposes text graphics, that will be much nicer.</p>
<p align="left" class="MsoNormal" style="line-height: normal; margin: 0cm 0cm 0pt; mso-layout-grid-align: none;">&#0160;&#0160;</p>
<pre><code>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Drawing;

using System.Windows.Media;

using System.Globalization;

using System.Windows;





using Autodesk.Navisworks.Api;

using Autodesk.Navisworks.Api.Plugins;

namespace navisdrawtext {
 //tool plugin which draws custom text

 [Plugin(&quot;ToolPluginCustomText&quot;, &quot;ADSK&quot;)]

 public class ToolPluginTest: ToolPlugin

 {

  ModelItem clickedModel = null;

  int mouse_click_x;

  int mouse_click_y;





  public override bool MouseDown(View view,

   KeyModifiers modifiers,

   ushort button,

   int x,

   int y,

   double timeOffset)

  {

   // get current selection

   PickItemResult itemResult =

    view.PickItemFromPoint(x, y);

   if (itemResult != null)

   {

    clickedModel =

     itemResult.ModelItem;

    Autodesk.Navisworks.Api.Application.ActiveDocument.ActiveView.

    RequestDelayedRedraw(ViewRedrawRequests.Render);



    mouse_click_x = x;

    mouse_click_y = y;

   }

   return false;

  }





  //2014：

  //public override void OverlayRenderWindow(View view, Autodesk.Navisworks.Api.Graphics graphics)

  //2015 :

  public override void OverlayRender(View view, Autodesk.Navisworks.Api.Graphics graphics)

  {

   if (clickedModel != null)

   {

    //red color

    graphics.Color(Autodesk.Navisworks.Api.Color.Red, 0.7);



    // Create the formatted text based on the properties set.

    FormattedText formattedText = new FormattedText(

     &quot;Hello 12345678 :-)&quot;,

     CultureInfo.GetCultureInfo(&quot;en-us&quot;),

     System.Windows.FlowDirection.LeftToRight,

     new Typeface(&quot;Txt&quot;), 30,

     System.Windows.Media.Brushes.Black

    );



    Geometry geo = formattedText.BuildGeometry(new System.Windows.Point(mouse_click_x, mouse_click_y));

    PathGeometry pathgeo = geo.GetOutlinedPathGeometry();

    PathFigureCollection pathfigcoll = pathgeo.Figures;



    //draw the text with the paths one by one, simply connecting the points by line.

    //please search more elegant ways on internet.

    foreach(PathFigure pathfig in pathfigcoll)

    {

     foreach(PathSegment seg in pathfig.Segments)

     {



      if (seg is LineSegment)

      {

       LineSegment lineseg = seg as LineSegment;

      } else if (seg is PolyLineSegment)

      {

       PolyLineSegment polyline = seg as PolyLineSegment;

       for (int i = 0; i &lt; polyline.Points.Count - 1; i++)

       {

        int base_y = mouse_click_y;

        double actual_y_1 = 2 * base_y - polyline.Points[i].Y;

        double actual_y_2 = 2 * base_y - polyline.Points[i + 1].Y;



        graphics.Line(new Point2D(polyline.Points[i].X, actual_y_1),

         new Point2D(polyline.Points[i + 1].X, actual_y_2));



       }



      } else if (seg is PolyBezierSegment)

      {

       PolyBezierSegment polyBseg = seg as PolyBezierSegment;

       for (int i = 0; i &lt; polyBseg.Points.Count - 1; i++)

       {

        int base_y = mouse_click_y;

        double actual_y_1 = 2 * base_y - polyBseg.Points[i].Y;

        double actual_y_2 = 2 * base_y - polyBseg.Points[i + 1].Y;



        graphics.Line(new Point2D(polyBseg.Points[i].X, actual_y_1),

         new Point2D(polyBseg.Points[i + 1].X, actual_y_2));



       }



      }





     }



    }

   }

  }



 }



 // plugin that enables tool plugin

 [Plugin(&quot;EnableToolPluginCustomText&quot;, &quot;ADSK&quot;,

  DisplayName = &quot;EnableToolPluginCustomText&quot;)]

 public class EnableToolPluginExample: AddInPlugin

 {

  static bool enable = false;

  public override int Execute(params string[] parameters)

  {

   if (enable)

   {

    //switch to the native tool

    Autodesk.Navisworks.Api.Application.MainDocument.Tool.Value = Tool.Select;

   } else

   {

    //switch to custom tool

    ToolPluginRecord toolPluginRecord =

     (ToolPluginRecord) Autodesk.Navisworks.Api.Application.Plugins.FindPlugin(

      &quot;ToolPluginCustomText.ADSK&quot;);

    Autodesk.Navisworks.Api.Application.MainDocument.Tool.

    SetCustomToolPlugin(toolPluginRecord.LoadPlugin());

   }

   enable = !enable;

   return 0;

  }

 }
}
</code></pre>

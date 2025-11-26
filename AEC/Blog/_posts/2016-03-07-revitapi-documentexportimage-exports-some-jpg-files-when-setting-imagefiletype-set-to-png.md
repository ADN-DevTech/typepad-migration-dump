---
layout: "post"
title: "RevitAPI: Document.ExportImage exports some .jpg files when setting ImageFileType set to PNG"
date: "2016-03-07 22:00:12"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/03/revitapi-documentexportimage-exports-some-jpg-files-when-setting-imagefiletype-set-to-png.html "
typepad_basename: "revitapi-documentexportimage-exports-some-jpg-files-when-setting-imagefiletype-set-to-png"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/50826603">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>Image export feature is available in RevitAPI, that is: Document.ExportImage method, a real example is:</p>
<pre class="csharp prettyprint">FilteredElementCollector FEC_Views = new FilteredElementCollector(OpenDoc).OfClass(typeof(View));
FEC_Views.OfCategory(BuiltInCategory.OST_Views);
StringBuilder sb = new StringBuilder();
foreach (View View in FEC_Views)
{
    if (View.IsTemplate) continue;
    IList&lt;ElementId&gt; ImageExportList = new List&lt;ElementId&gt;();
    ImageExportList.Clear();
    ImageExportList.Add(View.Id);
    var NewViewName = View.Name.ToString().Replace(&quot;.&quot;, &quot;-&quot;);
    var BilledeExportOptions_3D_PNG = new ImageExportOptions
    {
        ZoomType = ZoomFitType.FitToPage,
        PixelSize = 2024,
        FilePath = ParentFolder + @&quot;\&quot; + NewViewName,
        FitDirection = FitDirectionType.Horizontal,
        HLRandWFViewsFileType = ImageFileType.PNG,
        ImageResolution = ImageResolution.DPI_600,
        ExportRange = ExportRange.SetOfViews,
    };

    BilledeExportOptions_3D_PNG.SetViewsAndSheets(ImageExportList);
    try
    {
        OpenDoc.ExportImage(BilledeExportOptions_3D_PNG);
    }
    catch (Exception ex)
    {
        sb.AppendLine(View.Id.ToString());
        sb.AppendLine(ex.ToString());
    }
}</pre>
<p>This program can export all the exportable views into .png file.</p>
<p>In general, there is no problem, but for some special revit files, it will export some .jpg files as well, why that happens even if we set the exporting format to .png using HLRandWFViewsFileType = ImageFileType.PNG?</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1aac732970c-pi" style="display: inline;"><img alt="Exported_result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1aac732970c image-full img-responsive" src="/assets/image_766176.jpg" title="Exported_result" /></a></p>
<p>The reason is: ImageExportOptions has different settings for two types of views: (1) hidden line and wireframe (HLRandWFViewsFileType property) and (2) shadow views (ShadowViewsFileType property), so we have to set ShadowViewsFileType to .png as well, then the problem got solved</p>
<pre class="csharp prettyprint">    var BilledeExportOptions_3D_PNG = new ImageExportOptions
    {
        ZoomType = ZoomFitType.FitToPage,
        PixelSize = 2024,
        FilePath = ParentFolder + @&quot;\&quot; + NewViewName,
        FitDirection = FitDirectionType.Horizontal,
        HLRandWFViewsFileType = ImageFileType.PNG,
        ShadowViewsFileType = ImageFileType.PNG,
        ImageResolution = ImageResolution.DPI_600,
        ExportRange = ExportRange.SetOfViews,
    };</pre>
<p>&#0160;</p>

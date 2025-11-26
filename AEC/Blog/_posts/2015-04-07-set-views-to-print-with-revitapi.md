---
layout: "post"
title: "Set views to print with RevitAPI"
date: "2015-04-07 01:57:16"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/04/set-views-to-print-with-revitapi.html "
typepad_basename: "set-views-to-print-with-revitapi"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/44922281">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>If we want to change Revit print settings, the entry point is Document.PrintManager property, and the PrintManager.ViewSheetSetting is used to set which views you want to print.</p>
<p>However when calling PrintManager.ViewSheetSetting, we may get this kind of excpetion:</p>
<p>InvalidOperationException &quot;This property is only available when user choose Select of Print Range.&quot;</p>
<p>The solution is to set &quot;Print Range&quot; to &quot;Selected views/sheets&quot; in the dialog appeared after clicking &quot;Print&quot; menu.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0817e1e6970d-pi" style="display: inline;"><img alt="Select View Range - en" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0817e1e6970d image-full img-responsive" src="/assets/image_512865.jpg" title="Select View Range - en" /></a></p>
<p>After fix the problem above, we now can use ViewSheetSetting.InSession.Views to set the views to print.</p>
<p>Steps:</p>
<p>1. Create a new ViewSet</p>
<p>2. Filter all views</p>
<p>3. Check if the view is printable using View.CanBePrinted</p>
<p>4. Add the view to print to the new ViewSet</p>
<p>5. Set ViewSheetSetting.InSession.Views with the new ViewSet (note: remember to put this inside a transaction)</p>
<p>Code example:</p>
<pre class="csharp">RevitDoc = commandData.Application.ActiveUIDocument.Document;

var pm = RevitDoc.PrintManager;
try
{
    var vss = pm.ViewSheetSetting;
    ViewSet set = new ViewSet();
    var classFilter = new ElementClassFilter(typeof(View));
    FilteredElementCollector views =
        new FilteredElementCollector(RevitDoc);
    views = views.WherePasses(classFilter);
    foreach (View view in views)
    {
        if (view.CanBePrinted)
        {
            set.Insert(view);
        }
    }
    using (Transaction transaction = new Transaction(RevitDoc))
    {
        transaction.Start(&quot;Set in-session views&quot;);
        vss.InSession.Views = set;
        transaction.Commit();
    }
}
catch (Exception ex)
{
    TaskDialog.Show(&quot;ERROR&quot;, ex.ToString());
}</pre>

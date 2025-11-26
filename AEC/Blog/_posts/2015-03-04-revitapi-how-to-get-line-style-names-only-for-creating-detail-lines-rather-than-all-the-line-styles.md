---
layout: "post"
title: "RevitAPI: How to get Line Style names ONLY for creating Detail Lines rather than getting all the line styles"
date: "2015-03-04 02:28:35"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/03/revitapi-how-to-get-line-style-names-only-for-creating-detail-lines-rather-than-all-the-line-styles.html "
typepad_basename: "revitapi-how-to-get-line-style-names-only-for-creating-detail-lines-rather-than-all-the-line-styles"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/44063459">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>
<p><strong>Question:</strong></p>
<p>I have the code that extracts all the Line Style names that are shown in the Revit: Manage tab-&gt;Settings pane-&gt;Additional Settings button-&gt;Line Styles menu item.</p>
<p>If you start the creation of a Detail Line in Revit (Annotation tab-&gt;Detail pane-&gt;Detail Line button), in the Properties window click the Line Styles item (right) you will see there are less Line Styles in this list. How do I only get this list of Line Styles not all of them?</p>
<pre class="csharp prettyprint">
private List<string> LineStyleNamesGet(Document doc)
{
    List<string> lineStyles = new List<string>();
    List<Category> All_Categories = 
        doc.Settings.Categories.Cast<Category>().ToList();
    Category Line_Category = All_Categories[1];
    foreach (Category one_cat in All_Categories)
    {
        if (one_cat.Name == "Lines")
            Line_Category = one_cat;

    }
    if (Line_Category.CanAddSubcategory)
    {
        CategoryNameMap All_Styles = Line_Category.SubCategories;

        foreach (Category one_category in All_Styles)
        {
            lineStyles.Add(one_category.Name);
        }
    }

    lineStyles.Sort();
    return lineStyles;
}</pre>

<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c75930eb970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c75930eb970b image-full img-responsive" title="Dismatch1" src="/assets/image_210418.jpg" alt="Dismatch1" border="0" /></a></p>
<p><strong>Answer:</strong> What about using detailLine.GetLineStyleIds()?</p>
<p><strong>Reply:</strong> I’m not creating a detail line at this point in my logic (I need the user to select a valid Line Style first). I do not see a way to get GetLineStyleIds() to work in this case. Please advise.</p>
<p><strong>Answer</strong> (by Miroslav): You may have to do a "dummy-transaction" (the one you always Abort at the end) and use detailLine.GetLineStyleIds() therein. It’s a valid technique we use in a few other Revit API sage patterns…</p>
<p>Example Code:</p>
<pre class="csharp prettyprint">
private List<string> LineStyleNamesGet(Document doc)
{
    List<string> lineStyles = new List<string>();
    Transaction transaction = new Transaction(doc,
    "Create detail line");
    transaction.Start();
    try
    {
        var view = doc.ActiveView;
        // make sure the view is 2D view
        var modelLine = doc.Create.NewDetailCurve(view,
            Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0)));
        var styles = modelLine.GetLineStyleIds();
        foreach (var styleId in styles)
        {
            var styleEle = doc.GetElement(styleId);
            lineStyles.Add(styleEle.Name);
        }
        transaction.RollBack();
    }
    catch (Exception ex)
    {
        transaction.RollBack();
    }
    return lineStyles;
}</pre>
<p>&nbsp;</p>

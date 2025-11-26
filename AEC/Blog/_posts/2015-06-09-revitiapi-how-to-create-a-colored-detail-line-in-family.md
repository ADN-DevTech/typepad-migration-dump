---
layout: "post"
title: "RevitiAPI: How to create a colored detail line in family?"
date: "2015-06-09 02:57:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/06/revitiapi-how-to-create-a-colored-detail-line-in-family.html "
typepad_basename: "revitiapi-how-to-create-a-colored-detail-line-in-family"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/45919559">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>As we know, to create a colored detail line in family, we have to:</p>
<ol>
<li>Create a new family project, e.g. based on &quot;Generic Annotation.rft&quot;</li>
<li>Click &quot;Create&quot; &gt; &quot;Line&quot;, draw a line</li>
<li>Select the new line, click &quot;Manage&quot; &gt; &quot;Object Styles&quot;, you will see a list of categories and patterns like this: <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c78ea5a8970b-pi" style="display: inline;"><img alt="GraphicsLineStyle_Category2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c78ea5a8970b image-full img-responsive" src="/assets/image_236740.jpg" title="GraphicsLineStyle_Category2" /></a><br /><br /></li>
<li>Select &quot;Generic Annotations&quot;, &quot;Modify Subcategories&quot; &gt; &quot;New&quot; is available now, click it and create a new sub-category, and set color and weight</li>
<li>Close all dialogs, and select the line again</li>
<li>Under &quot;Modify | Lines&quot;, you can see the new category appears under the combobox in &quot;Subcategory&quot; group. We&#39;ve done the job :)</li>
</ol>
<div>&#0160;</div>
<div>So how to do that via API?</div>
<div>&#0160;</div>
<div><ol>
<li>First, what we&#39;ve drawn is a DetailLine, so we should use Document.FamilyCreate.NewDetailCurve to create it.</li>
<li>To set the line style, use DetailLine.LineStyle property.</li>
<li>Create a new subcategory, use method Categories.NewSubcategory(Category parentCategory, string name)</li>
<li>So the only question left is how to get the parentCategory argument in the above method.</li>
</ol></div>
<div>Now we can make use of RevitLookup tool, as the line has the LineStyle property, we can inspect what its category is, let&#39;s select it, and run &quot;RevitLookup&quot; &gt; &quot;Snoop Current Selection...&quot;</div>
<div>&#0160;</div>
<div><img alt="" src="/assets/image_973229.jpg" /></div>
<div>&#0160;</div>
<div>As we can see, OST_GenericAnnotation is the &quot;Generic Annotations&quot; listed in the first image, we can use Categories.get_Item(BuiltInCategory.OST_GenericAnnotation) to get the category.</div>
<div>&#0160;</div>
<div>Here comes the code, remember put it in a transaction:</div>
<div>
<pre class="csharp prettyprint">public void CreateSubCategoryAndDetailLine(Document doc)
{
    var categories = doc.Settings.Categories;
    var subCategoryName = &quot;MySubCategory&quot;;
    Category category = doc.Settings.Categories.
        get_Item(BuiltInCategory.OST_GenericAnnotation);
    Category subCategory = null;
    if (!category.SubCategories.Contains(subCategoryName))
    {
        subCategory = categories.NewSubcategory(category, 
            subCategoryName);
        var newcolor = new Color(250, 10, 0);
        subCategory.LineColor = newcolor;
        subCategory.SetLineWeight(10, GraphicsStyleType.Projection);
    }
    else
        subCategory = category.SubCategories.get_Item(subCategoryName);

    Line newLine = Line.CreateBound(
        new XYZ(0, 1, 0), new XYZ(-1, 0, 0));
    var detailLine = doc.FamilyCreate.NewDetailCurve(
        doc.ActiveView, newLine);
    detailLine.LineStyle = subCategory.GetGraphicsStyle(
        GraphicsStyleType.Projection);
}</pre>
<br /> Result:</div>
<div><img alt="" src="/assets/image_109348.jpg" /></div>

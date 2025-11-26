---
layout: "post"
title: "Category Analysis with and without Python"
date: "2014-03-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Python"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2014/03/category-analysis-with-and-without-python.html "
typepad_basename: "category-analysis-with-and-without-python"
typepad_status: "Publish"
---

<script src="https://thebuildingcoder.typepad.com/google-code-prettify/run_prettify.js"></script>

<p>Here is a useful approach for analysing categories by Alexander Ignatovich of

<a href="http://www.iv-com.ru">
Investicionnaya Venchurnaya Companiya</a>,

originating from the following query:</p>

<p><strong>Question:</strong> How can I check that a given family is an annotation?</p>

<p>For example, I have a family with BuiltInCategory.OST_GridHeads family category.</p>

<p>Revit knows that this is an annotation category and shows it inside the corresponding group in the project browser.</p>

<p>However, there are no annotation "super groups" in the BuiltInCategory enumerable, and family.FamilyCategory.Parent is null.</p>


<p><strong>Answer:</strong> I am not aware of any way to achieve this in Revit 2014.</p>

<p>As you certainly thought of yourself, you could implement a workaround by hard-coding a list enumerating and manually classifying all the categories that you are interested in.</p>

<p>For that, it might also be helpful to keep in mind that instances of annotation categories are generally view-specific, and model categories are generally not. This requires instances to be present that you can interrogate.</p>


<p><strong>Response:</strong> It is actually more important for me to know, that a family is <b>not</b> an annotation.</p>

<p>I created the following hack to help determine this.</p>

<p>Instead of looking at more than 700 categories one by one, I do the following:</p>

<ul>
<li>Manually create a project parameter.</li>
<li>Add check marks for it to all the categories I am interested in.</li>
<li>Iterate over the project parameter bindings.</li>
<li>For every binding of my project parameter, for every category bound to it, list the built-in category enumeration name and underlying integer value.</li>
</ul>

<p>The latter two steps are quickly implemented by this IronPython code in the RevitPythonShell:</p>

<pre class="prettyprint">
  from System import *
  bindings = doc.ParameterBindings
  it = bindings.ForwardIterator()
  while it.MoveNext():
    if it.Key.Name == 'my': # project parameter name
      for cat in it.Current.Categories:
        print Enum.ToObject(BuiltInCategory, cat.Id.IntegerValue)
</pre>

<p>For instance, I wanted to get the list of all "model" categories; there are about 800 categories in the BuiltInCategory enumeration. Project parameters can be applied to all "model" categories and also to some system and analytical model categories. It is really good way to extract the desired category list: just select all categories and deselect system and analytical model categories. This is especially helpful when you are working with a localized version of the program, because sometimes the translation is not perfect :-)</p>

<p>Here is a picture to clarify:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301a3fce34982970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301a3fce34982970b image-full img-responsive" alt="Selected project parameter categories" title="Selected project parameter categories" src="/assets/image_db2ba2.jpg" border="0" /></a><br />

</center>

<p>Many thanks to Alexander for sharing this powerful idea!</p>

<p>It shows once again how handy and effective it is to use the tools and functionality provided by Revit as far as possible, which is a long way, and enhance them with

<a href="http://thebuildingcoder.typepad.com/blog/2013/11/intimate-revit-database-exploration-with-the-python-shell.html">
interactive Python for in-depth Revit database exploration</a>.</p>

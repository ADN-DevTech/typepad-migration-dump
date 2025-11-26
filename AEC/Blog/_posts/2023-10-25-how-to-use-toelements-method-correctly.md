---
layout: "post"
title: "How to use ToElements Method correctly"
date: "2023-10-25 06:33:59"
author: "Moturi Magati George"
categories:
  - ".NET"
  - "Moturi George"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2023/10/how-to-use-toelements-method-correctly.html "
typepad_basename: "how-to-use-toelements-method-correctly"
typepad_status: "Publish"
---

<p>By <a href="https://adndevblog.typepad.com/aec/moturi-magati-george.html" target="_self">Moturi Magati George</a></p>
<p>There have been a couple of questions regarding the usage of <span style="font-family: &#39;courier new&#39;, courier;">ToElements</span> Method while filtering elements using the <span style="font-family: &#39;courier new&#39;, courier;">FilteredElementCollector</span> Class. It is good to note that the <span style="font-family: &#39;courier new&#39;, courier;">ToElements</span> Method in the <span style="font-family: &#39;courier new&#39;, courier;">FilteredElementCollector</span> Class returns the complete set of elements that meet the specified filter criteria as a generic IList.</p>
<p>However, it&#39;s worth noting that some members of the Revit API community tend to use the ToElements Method after using the <span style="font-family: &#39;courier new&#39;, courier;">FilteredElementCollector</span> which in turn increases memory usage and slows down the performance of the same.</p>
<p>One reason for using <span style="font-family: &#39;courier new&#39;, courier;">ToElements</span> is to obtain the element count. However, that can also be achieved by calling <span style="font-family: &#39;courier new&#39;, courier;">GetElementCount</span>.</p>
<p>Another and more valid reason is to access the elements in the list by index, e.g., you have 1000 elements in the list and you want to read their data in a specific order, e.g., #999, #1, #998, #2 or whatever. Then, you need the index provided by the list, and cannot just iterate over them on the predefined order provided by the enumerator.</p>
<p>Here are examples that demonstrate the usage of the two:</p>
<pre>//Example 1.
//In this example, we use only the FilteredElementCollector to retrieve Wall elements.
IEnumerable walls = new FilteredElementCollector(doc).OfClass(typeof(Wall));
foreach (Element item in walls)
{
ElementId id = item.Id;
}
</pre>
<pre>//Example 2.
//Here, we use both the FilteredElementCollector and the ToElements Method to collect Wall elements.
IList wallList = new FilteredElementCollector(doc).OfClass(typeof(Wall)).ToElements();
foreach (Element item in wallList)
{
ElementId id = item.Id;
}
</pre>
<p>Here is a sample blog where the same has been discussed.</p>
<p><a href="https://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html#2" rel="noopener" target="_blank">https://thebuildingcoder.typepad.com/blog/2016/04/how-to-distinguish-redundant-rooms.html#2</a></p>

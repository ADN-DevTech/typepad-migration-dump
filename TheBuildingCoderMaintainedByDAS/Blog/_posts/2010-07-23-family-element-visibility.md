---
layout: "post"
title: "Family Element Visibility"
date: "2010-07-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Element Creation"
  - "Family"
  - "Parameters"
  - "Settings"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/07/family-element-visibility.html "
typepad_basename: "family-element-visibility"
typepad_status: "Publish"
---

<p>Here is a question on programmatically setting the visibility of an element in a family document:

<p><strong>Question:</strong> I need to assign a Yes/No parameter to the visibility property of a family feature, e.g. an extrusion element.
I need to do this to suppress a specific extrude feature for certain types.
I know how to do it manually, by selecting the feature and then clicking the visibility toggle in the element properties.
How can I achieve it through the API?

<p><strong>Answer:</strong> I created a sample family to test this by adding an extrusion element in a new family document using the Metric Generic Model template. 
On the extrusion, I can see the property you refer to in the element properties as the Properties &gt; Graphics &gt; Visible toggle, which is displayed as a Boolean check box in the user interface:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330134859d2631970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330134859d2631970c" alt="Family extrusion element visible property" title="Family extrusion element visible property" src="/assets/image_58fe0b.jpg" border="0"  /></a> <br />

</center>

<p>If I look at the extrusion element using RevitLookup, I see that this property is available through the element parameters: Add-Ins &gt; Revit Lookup &gt; Snoop Current Selection &gt; Extrusion &gt; Parameters &gt; Visible with the corresponding built-in parameter IS_VISIBLE_PARAM:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330134859d2743970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330134859d2743970c image-full" alt="Family extrusion element visible parameter" title="Family extrusion element visible parameter" src="/assets/image_a6f248.jpg" border="0"  /></a> <br />

</center>

<p>You can access this using the standard get_Parameter method on the extrusion element.

<p>We made significant use of this property and showed how to hook it up with an externally accessible family parameter so that it can be controlled from outside on individual instances of the family inserted into the project file in the discussion on 

<a href="http://thebuildingcoder.typepad.com/blog/2009/07/dwg-and-dwf-family-creation.html">
DWG and DWF family creation</a>.

<p>Note that if you wish have more detailed control over the visibility of the feature, you can also use its visibility property returning an instance of the FamilyElementVisibility class:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f2783e39970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f2783e39970b image-full" alt="Family extrusion element visibility property" title="Family extrusion element visibility property" src="/assets/image_37db72.jpg" border="0"  /></a> <br />

</center>

<p>The Revit API help file RevitAPI.chm description of the FamilyElementVisibility class includes some sample code on using this.
Its use to set up different visibility of family elements for different detail levels of the inserted instances is also demonstrated by step 4 of the 

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/revit-family-creation-api-labs.html#4">
Family API labs</a>.

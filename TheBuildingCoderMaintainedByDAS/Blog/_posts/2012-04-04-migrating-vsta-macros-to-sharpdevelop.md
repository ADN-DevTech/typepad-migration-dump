---
layout: "post"
title: "Migrating VSTA Macros to SharpDevelop"
date: "2012-04-04 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2013"
  - "Getting Started"
  - "Migration"
  - "Training"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/04/migrating-vsta-macros-to-sharpdevelop.html "
typepad_basename: "migrating-vsta-macros-to-sharpdevelop"
typepad_status: "Publish"
---

<p>As you probably noticed, we held a very successful and exciting Revit API training course and DevLab 

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-day-one.html">
recently</a>

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-day-two.html">
in</a>

<a href="http://thebuildingcoder.typepad.com/blog/2012/03/melbourne-devlab.html">
Melbourne</a>.

<p>More face-to-face hands-on trainings are planned, as you can see from the ADN

<a href="http://www.adskconsulting.com/adn/cs/api_course_sched.php">
Training Course Schedule</a>,

entering "Revit API" as the course name.


<a name="1"></a>

<h4>Revit API Hands-On Training in Munich</h4>

<p>The next scheduled class that I will be conducting is taking place in Munich, Germany, on April 25-26

(<a href="http://usa.autodesk.com/adsk/servlet/item?id=6703509&siteID=123112&cname=Revit%20API,%20Munich,%20Apr%2025%202012,%20201213">register</a>).

<p>If you are interested in participating in a class, you will benefit enormously by 

<a href="http://thebuildingcoder.typepad.com/blog/2012/01/preparing-for-a-hands-on-revit-api-training.html">
preparing appropriately</a>. 

The material we provide for that can also be used very well to learn the basics of the Revit API for yourself all on your own.

<p>Meanwhile, here are some notes on the changes required to migrate our existing 

<a href="http://thebuildingcoder.typepad.com/blog/2011/05/my-first-revit-plug-in.html">
My First Revit Plugin</a> 

(<a href="http://thebuildingcoder.typepad.com/blog/2011/11/my-first-revit-plug-in-in-vb.html">VB</a>)

code from Revit 2012 to 2013, reported by Saikat Bhattacharya:


<a name="2"></a>

<h4>Migrating VS 2010 Express Code</h4>

<p>The only change was to update the following line of code using the obsolete Document.Element property:

<pre>
  Element elem = pickedRef.Element;
</pre>

<p>This needs to be changed to call the Document.GetElement method:

<pre>
  Element elem = doc.GetElement(pickedRef);
</pre>

<h4>Migrating VSTA to SharpDevelop</h4> 

<p>There were a couple of steps in this migration:

<p><strong>1.</strong> Open the RVT files with embedded macros in SharpDevelop.

<p><strong>2.</strong> Change the .NET framework version to 4.0:

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016303ab80c3970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016303ab80c3970d image-full" alt="Set .NET Framework version" title="Set .NET Framework version" src="/assets/image_fbf70e.jpg" border="0" /></a><br />

</center>

<p><strong>3.</strong> Remove existing references to the Revit API assembly DLLs and reference the Revit 2013 ones instead.

<p><strong>4.</strong> Delete the following line, since VSTA no longer exists in the Revit namespace:

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330168e9a14ab9970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330168e9a14ab9970c image-full" alt="Remove VSTA add-in id" title="Remove VSTA add-in id" src="/assets/image_e95053.jpg" border="0" /></a><br />

</center>

<p><strong>5.</strong> Make the API changes in the code, essentially just the one line mentioned above for the VS Express change:

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833016303ab8346970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833016303ab8346970d image-full" alt="Update Element property to GetElement method" title="Update Element property to GetElement method" src="/assets/image_952021.jpg" border="0" /></a><br />

</center>

<p><strong>5.</strong> Update the description of the macro in the MacroManager dialogue.

<p>I repeated the same set of steps for all the existing VSTA samples for C# and VB.NET for them to work with SharpDevelop.

<p>Many thanks to Saikat for this report!

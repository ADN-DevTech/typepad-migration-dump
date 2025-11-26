---
layout: "post"
title: "Setting a Locked Parameter"
date: "2010-08-27 08:00:00"
author: "Jeremy Tammik"
categories:
  - "Family"
  - "Parameters"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/08/setting-a-locked-parameter.html "
typepad_basename: "setting-a-locked-parameter"
typepad_status: "Publish"
---

<p>I arrived back in Europe and am still working through my pile of email.
Here is another interesting tidbit that arose in the course of some correspondence with Anthony Forlong, Associate and CAD Manager at 

<a href="http://www.cbp.co.nz">Clendon Burns & Park Ltd</a> over the past few days:

<p><strong>Question:</strong> I've written an application that lets our users search our network and insert Revit families and load types from type catalogues.
To help increase performance I have been using the duplicate method if the family exists, and then setting the parameters to their values from the type catalogue.

<p>I am facing the following problem: there doesn't seem to be a method for setting locked parameters in the document. 
Is that correct?  
The call to the Set method fails.

<p>The following user interface dialogue provides access to the parameter locking:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f35c4a31970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f35c4a31970b image-full" alt="Family Type Parameter Locking User Interface" title="Family Type Parameter Locking User Interface" src="/assets/image_49e260.jpg" border="0" /></a> <br />

</center>

<p>If I manually clear the check box, my function works fine. 
Is there away to set the parameter when it is ticked?

<p><strong>Answer:</strong> The following Revit 2011 API FamilyManager methods should work for this case.
The documentation states that they work for Conceptual Mass and Curtain Panel families.
My colleague Phil Xia tried other families and found that they worked there as well:

<ul>
<li>IsParameterLockable: For Conceptual Mass and Curtain Panel families, indicate whether the specified parameter can be locked. 
<li>IsParameterLocked: For Conceptual Mass and Curtain Panel families, indicate whether the specified dimension-driving parameter is locked. 
<li>SetParameterLocked: For Conceptual Mass and Curtain Panel families, lock or unlock a dimension-driving parameter. 
</ul>

<p>Thanks to Anthony and Phil for raising and clarifying this issue!

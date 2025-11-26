---
layout: "post"
title: "System Versus User Family Category"
date: "2009-10-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Family"
  - "RME"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/10/system-versus-user-family-category.html "
typepad_basename: "system-versus-user-family-category"
typepad_status: "Publish"
---

<p>Here is a simple question and suggestion on classifying categories into built-in system versus user defined.

<p><strong>Question:</strong> Given an arbitrary element, how can I determine whether it belongs to a system family as opposed to a user-defined family?
I thought I might look at its category, but how can I see whether that is system-defined?
For example, I see that a duct has a category id of -2008000.
Can I depend on the fact that this category id value is negative?
Or is there any other way to determine this?</p>

<p><strong>Answer:</strong> There is currently no API method to distinguish system categories from user defined ones short of creating your own hardcoded list.
All built-in categories have negative values, but this includes system family types, family types, and subcategories of each, so that will not help you resolve this issue.</p>

<p>How could such a list be created?</p>

<p>Unfortunately, that would have to be done manually.
This Family Category and Parameters dialogue lists all the family based categories:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a60084d5970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a60084d5970b" alt="Family categories" title="Family categories" src="/assets/image_61e64c.jpg" border="0"  /></a> <br />

</center>

<p>If the category is a top level category and not in this list, then it is probably a system family category.</p>

---
layout: "post"
title: "DevLab and Birthday"
date: "2010-06-11 10:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Events"
  - "News"
  - "Transaction"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/06/devlab-and-birthday.html "
typepad_basename: "devlab-and-birthday"
typepad_status: "Publish"
---

<p>It is Saikat's birthday today, and he is far away from home and family.
We prepared a small celebration with flags and cake and a birthday song for him last night:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013483f59363970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013483f59363970c image-full" alt="Saikat and his birthday cake" title="Saikat and his birthday cake" src="/assets/image_2bc3ab.jpg" border="0"  /></a> <br />

</center>

<p>Here is the part of the ADN DevTech AEC workgroup that is present to support the DevLab here in Waltham.
From the left: Augusto, Partha, Saikat, Michael (Navisworks team), Mikako, Adam, Jeremy:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013483f594fd970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013483f594fd970c image-full" alt="Saikat's birthday party" title="Saikat's birthday party" src="/assets/image_6362fc.jpg" border="0"  /></a> <br />

</center>

<p>Happy birthday Saikat!

<p>Meanwhile, we are back at DevLab again with a host of exciting developers and their issues, lots going on and many things entering the pipeline for future posts.
One little item that we talked about yesterday and that I have heard about a couple of times in the past is the requirement to open a transaction:

<h4>Transaction Required</h4>

<p>In Revit 2010, one had the possibility and sometimes the need to 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/extra-transaction-required.html">
open an own transaction</a>.

In general, this was not necessary in Revit 2010 when working within the active document, since Revit automatically started up a transaction when launching a new command.

<p>In Revit 2011, this behaviour is still available if the command is compiled with the automatic transaction mode attribute.
However, you have more control over transactions if you manage them yourself, setting the manual mode instead.

<p>And you obviously still have no choice but to start your own transaction when manipulating other documents than the current active one.

<p>A number of people have reported seeing the error message 'Sub-Transaction can only be active inside an open Transaction' and other ones similar to it.
In every case, the reason really has been the lack of a transaction.
One of the DevLab participants was struggling with such an error yesterday, saying:
"I am sure I have a transaction open, and yet I get this error message about lacking transactions when I try to add new elements to the family document."

<p>Again, there really was a missing transaction, because the open transaction was for the active project document.
The family document opened in the background is a completely different animal, and if you wish to modify it in any way, it will require its own transaction before you can do so.
That fixed the problem.

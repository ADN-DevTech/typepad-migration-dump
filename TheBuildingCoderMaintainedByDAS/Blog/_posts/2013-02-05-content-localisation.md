---
layout: "post"
title: "Content Localisation"
date: "2013-02-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Family"
  - "Parameters"
  - "RME"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/02/content-localisation.html "
typepad_basename: "content-localisation"
typepad_status: "Publish"
---

<p>How to manage families and content creation is a crucial topic for a large number of Revit add-ins.</p>

<p>Brian Johnson, technical specialist on structural engineering solutions at Autodesk in Austin, Texas, discusses how to efficiently address some important aspects of this below.</p>

<p>First, however, let me wish everybody a Happy New Year of the Snake!</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d40c81934970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d40c81934970c image-full" alt="Year of the Snake" title="Year of the Snake" src="/assets/image_30b50d.jpg" border="0" /></a><br />

</center>

<p>A year for reflection, planning and searching answers.
A good time for shrewd dealings, political affairs and coups d'état.
People will be more likely to scheme and ponder over matters before acting on them.
An auspicious year for commerce and industry.</p>

<p>Let's hope so, indeed, and return to the issue of content localisation:</p>


<a name="2"></a>

<h4>Content Localisation</h4>

<p><strong>Question:</strong> We have a large number of library items that we would like to work with in Revit, and they are used in many different countries and languages.

<p>How can we manage this efficiently, please?

<p>We would like to avoid duplicating any of the content, of course.

<p>The differences include things like product name, description, part code, cost, and many other properties.</p>

<p>As far as I see, a Revit family cannot hold different values for a single property such as the item description.

<p>What are your advice and the best practice for creating and maintaining a Revit family library for thousands of items and numerous languages?

<p><strong>Answer:</strong> Just as you say, Revit is not equipped to localise parameter names and types nor family names themselves.

<p>Some level of automation will probably help significantly to solve or at least simplify this task.</p>

<p>A proof-of-concept for generating content from data and organizing content can be found in the Content Generator Extension available from the Autodesk Subscription centre.
Note that all the Revit Extensions are bundled into a single download:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c369987f2970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c369987f2970b image-full" alt="Content Generator Extension" title="Content Generator Extension" src="/assets/image_95b1a5.jpg" border="0" /></a><br />

</center>

<p>The content generator is intended for structural content from around the world using the section and material libraries (XML files) that also form the libraries for Robot Structural Analysis Professional and AutoCAD Structural Detailing:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee83cdd2c970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee83cdd2c970d image-full" alt="Content Generator Extension" title="Content Generator Extension" src="/assets/image_26e250.jpg" border="0" /></a><br />

</center>

<p>I describe some of the inner workings of the Content Generator on pages 7-9 in the hand-out of my Autodesk University 2012 course

<a href="http://au.autodesk.com/?nd=class&session_id=10570">
SE3904 &ndash; Linking Revit Structure and Robot Structural Analysis: Beyond the Basics</a>.

For your convenience as a valued reader of The Building Coder, here is a

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee83cdeb2970d"><a href="http://thebuildingcoder.typepad.com/files/localise_content_se3904_handout.pdf">direct download link</a></span>.

<p>I suggest installing the Extensions and trying it out.
Your version of this might pull country specific data from your libraries/databases and generate a specific element for a specific language.

<p>When considering creating your own custom families, my suggestion is to create the content with the best-suited family.
If you use a specialised category like Structural Framing, you will get built-in parameters and behaviours that you may or may not want.
The Generic Model set of family templates is the most basic and may be the most appropriate.
I suggest populating the Identity Data and creating Shared Parameters for each family type and use those to filter, sort, and group the Schedule views in Revit.

<p>As always, it is of utmost importance to discuss your plans with some of your Revit users and to get some training on how things are done in the typical workflows in Revit.
This will help you understand what can be done with Revit out-of-the-box and what parts might use more automation.

<p>Thank you, Brian, for the valuable advice!</p>




<a name="3"></a>

<h4>MEP Family Editor Part Type Classes</h4>

<p>While we are talking about content creation, here is another MEP specific question on the different classes of fitting types:</p>

<p><strong>Question:</strong> I would like to know the definition for some of the part types of the fittings in the family editor.</p>

<p>Simple classes of fitting types like cap, cross, elbow, multiport, tap, transition, tee and union is clear, but I am uncertain about the more complex and less clear-cut parts.</p>

<p>Could you please clarify the following?</p>

<ol>
<li>What is the difference between wye and pants?
<li>Lateral tee and lateral cross are probably tees with an angle other than 90 degrees. They can also be rounded. Is this correct?
<li>Offset is probably a kind of S-bend. Is this correct? Are eccentric transitions also included in this group?
</ol>


<p><strong>Answer:</strong> Sure:</p>

<p><strong>1.</strong> What is the difference between wye and pants?</p>

<p>It is no difference between creating wye and pants families.
We just choose a part type that is close to the family name, which will makes sense if users check the part type.</p>

<p>Examples for wye:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c36998c0d970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c36998c0d970b" alt="Wye" title="Wye" src="/assets/image_a49037.jpg" border="0" /></a><br />

</center>

<p>Examples for pants:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee83ce13d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee83ce13d970d" alt="Pants" title="Pants" src="/assets/image_b381bf.jpg" border="0" /></a><br />

</center>

<p><strong>2.</strong> Lateral tee and lateral cross are probably tees with an angle other than 90 degrees. They can also be rounded. Is this correct?</p>

<p>Again, lateral tee and lateral cross are used when the family name has similar words.
The rule for using 'Lateral tee', 'Lateral cross' and 'Wye' is that the branch angle will be fixed when using the family in the project, so if the branch pipe has a different angle with the fitting, an elbow will be automatically added.
If the part type is 'tee' or 'cross', the fitting will have a flexible angle branch itself.</p>

<p>Examples for lateral tee:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017ee83ce258970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017ee83ce258970d" alt="Tee" title="Tee" src="/assets/image_44a615.jpg" border="0" /></a><br />

</center>

<p>Example for lateral cross:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d40c82125970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d40c82125970c" alt="Cross" title="Cross" src="/assets/image_b1907c.jpg" border="0" /></a><br />

</center>

<p>Lateral tee and lateral cross are also used as part types for pipe fittings, so they can be round as well.</p>

<p><strong>3.</strong> Offset is probably a kind of S-bend.
Is this correct?
Are eccentric transitions also included in this group?</p>

<p>Examples for offset:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017c36998fbc970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017c36998fbc970b" alt="Offset" title="Offset" src="/assets/image_51d232.jpg" border="0" /></a><br />

</center>

<p>An eccentric transition should use the 'Transition' part type.</p>

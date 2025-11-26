---
layout: "post"
title: "Family instance joined ? and Guatemala Independence day!"
date: "2014-09-15 10:42:46"
author: "Jaime Rosales"
categories:
  - ".NET"
  - "Jaime Rosales"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2014/09/family-instance-joined-and-guatemala-independence-day.html "
typepad_basename: "family-instance-joined-and-guatemala-independence-day"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/jaime-rosales.html">Jaime Rosales</a></p>
<p>As some of you might know I’m originally from Guatemala, and today we are celebrating 193 years of being Independent from the Spanish colonization. I will share you an aerial picture that I took at the beginning of August of one of the famous volcanoes that can be seen from the city of Guatemala and Antigua Guatemala.</p>
<p>Volcan de Agua – “Water Volcano”</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6e0a238970b-pi"><img alt="DCIM\101GOPRO" border="0" height="296" src="/assets/image_861135.jpg" style="display: inline; border-width: 0px;" title="DCIM\101GOPRO" width="485" /></a></p>
<p>Back to Revit. This time I will talk to you about a question that came from our Autodesk Forum, regarding the check for joined family instances.</p>
<p><a href="http://forums.autodesk.com/t5/revit-api/is-family-instance-joined/m-p/5272857#M7228" title="http://forums.autodesk.com/t5/revit-api/is-family-instance-joined/m-p/5272857#M7228">http://forums.autodesk.com/t5/revit-api/is-family-instance-joined/m-p/5272857#M7228</a></p>
<p><strong>Question:</strong> I can&#39;t see anything using RevitLookup but is there a way to tell if a family instance has been joined to something?</p>
<p>When transforming an edge I&#39;m getting different results after joining the family to another family or a wall, etc.</p>
<p><strong>Answer:</strong> I raised the question to our engineering team, and here is the response from one of them.</p>
<p>I think there are a couple of things to try:</p>
<p>1.) JoinGeometryUtils.GetJoinedElements()</p>
<p>For this one you could check out the post on Jeremy Tammik blog:</p>
<h5><a href="http://thebuildingcoder.typepad.com/blog/2014/02/getting-two-different-kinds-of-joined-elements.html" target="_blank">Getting Two Different Kinds of Joined Elements</a></h5>
<p>2.) For concrete framing family instances and walls, you can get the Element’s Location, cast to LocationCurve, and look at ElementsAtJoin.</p>
<p>And for this 2nd suggestion you can look at this link.</p>
<p>Look for the post with title <a href="http://thebuildingcoder.typepad.com/blog/transaction/page/2/" target="_blank">Wall Joins and Geometry</a>.</p>
<p>There is also Element.GetGeneratingElementIds() which tells you for a given piece of geometry from an element what element causes this geometry to form.</p>
<p>Let us know how it goes, enjoy.</p>
<p><strong>User Response: </strong>The first method worked for me.Thanks.</p>
<p>Thank you for reading, until next time.</p>

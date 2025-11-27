---
layout: "post"
title: "ComponentOccurrence contexts and Reference keys"
date: "2012-12-13 01:46:52"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/12/componentoccurrence-contexts-and-reference-keys.html "
typepad_basename: "componentoccurrence-contexts-and-reference-keys"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>  <p><b>Issue</b></p>  <p>I seem to have some problems with Occurrences, SubOccurrences and Definitions. E.g. why is the reference key different for an Occurrence depending on how I get to it?</p>  <p><b>Solution</b></p>  <p>In our example we'll use the following document structure:</p>  <p><font face="Lucida Console">Asm1      <br />+ SubAsm1:1       <br />&#160;&#160;&#160;&#160;&#160; + Part1       <br />+ SubAsm1:2       <br />&#160;&#160;&#160;&#160;&#160; + Part1</font></p>  <p>Whenever you use the Definition property of a ComponentOccurrence, it takes you out of the context of the Document where the ComponentOccurrence resides and has the same effect as opening the Document the ComponentOccurrence references and using that Document's ComponentDefinition directly.</p>  <p><font size="1" face="Lucida Console">Asm1.ComponentDefinition.Occurrences(1).Definition = SubAsm1.ComponentDefinition</font></p>  <p>When a ComponentOccurrence is reached directly from a Definition and not from an Occurrence of that Definition, then it does not have any information about any assemblies that contain it, and therefore the ComponentOccurrence's Parent property returns Nothing or Null. Only SubOccurrences have Parents.</p>  <p>As an example if you want to get the transformation matrix of SubAsm1:1-&gt;Part1 that shows how this instance of Part1 is placed inside Asm1's coordinate system, then you should not break the Occurrences.SubOccurrences chain with a reference to a Definition property. Instead do it like this: Asm1.ComponentDefinition.Occurrences(1).SubOccurrences(1).Transformation</p>  <p>NOTE: As an alternative, you can also use Definition property in combination with the CreateGeometryProxy function.</p>  <p>The same applies to all other ComponentOccurrence properties including GetReferenceKey(). If you get a reference key for the Part1 occurrence in SubAsm1's context, it provides a key that is suitable to identify Part1 inside SubAsm1. This is not the same key that you get for SubAsm1:1-&gt;Part1 in Asm1, which obviously needs to be different from SubAsm1:2-&gt;Part1 as well, since this is for identifying a given instance of Part1 in Asm1 and not only in SubAsm1.</p>

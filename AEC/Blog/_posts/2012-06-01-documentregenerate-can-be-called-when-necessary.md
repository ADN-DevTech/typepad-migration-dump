---
layout: "post"
title: "Document.Regenerate() can be called when necessary"
date: "2012-06-01 10:19:00"
author: "Joe Ye"
categories:
  - ".NET"
  - "Joe Ye"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2012/06/documentregenerate-can-be-called-when-necessary.html "
typepad_basename: "documentregenerate-can-be-called-when-necessary"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/joe-ye.html">Joe Ye</a></p>  <p>Question: </p>  <p>Can I call Document.Regenerate() method in the middle of a external command that has manual regeneration option?</p>  <p>Answer:</p>  <p>From Revit 2011, external command needs to have a specific regeneration option mode attribute. It can be Regeneration.Manual or Regeneration.Automatic.&#160;&#160; </p>  <p>If the command’s regeneration option is manual,&#160; the Document.Regenerate method at least be called once explicitly in the code or implicitly by Revit when submitting the transaction. </p>  <p>When the followed immediate API calls need to take place on the latest model, we need to call Regenerate() method to make all changes to the model effect.   <br />For example, a manual regeneration external command does two tasks in its Execute() method. Move up a beam, then add an opening to the center of the beam.&#160; The opening center coordinates is relative to the beam’s new position. If Regenerate is not called, then add opening&#160; immediately, the opening cannot be inserted to the target position. It might be in the beam’s upper part or cannot cut the beam at all depending on the moving distance. The correct order is after moving up the beam, call Regenerate method to regenerate the model, then adding the opening.&#160; </p>  <p>Regenerate() method can only be called in manual mode external command. If the external command is in automatic regeneration mode, it is prohibited to call this method. When submitting a transaction, Revit automatically calls this method to regenerate the model.</p>

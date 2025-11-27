---
layout: "post"
title: "AddDesignVisualizationAttachmentBadAttachmentOrder error"
date: "2014-10-02 10:57:40"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/10/adddesignvisualizationattachmentbadattachmentorder-error.html "
typepad_basename: "adddesignvisualizationattachmentbadattachmentorder-error"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>When using AddDesignVisualizationFileAttachment() to attach a DWF to an IDW this error may occur.</p>  <p>The error means the call is trying to create “out of order” DWFs </p>  <p>For example let’s say a file has 4 versions, where version 1, 2, &amp; 4 have a DWF, and you want add a DWF to version 3.</p>  <p>&#160;</p>  <p>Starting out, the data could look something like:    <br />F(1) -&gt; DWF(1)     <br />F(2) -&gt; DWF(2)     <br />F(3)     <br />F(4) -&gt; DWF(3)</p>  <p>&#160;</p>  <p>Vault does not allow DWF(4) to be an attachment to F(3) since that would be confusing so a AddDesignVisualizationAttachmentBadAttachmentOrder exception is thrown.</p>  <p>&#160;</p>  <p>What Vault Explorer does in that situation is to download DWF(1), DWF(2) and DWF(3), delete the DWF entirely, and then re-create the versions so that the end result will look like:    <br />F(1) -&gt; DWF(1)     <br />F(2) -&gt; DWF(2)     <br />F(3) -&gt; DWF(3)     <br />F(4) -&gt; DWF(4)</p>

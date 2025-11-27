---
layout: "post"
title: "Inventor: Dirty flag of Inventor document"
date: "2013-08-13 04:57:50"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/08/inventor-dirty-flag-of-inventor-document.html "
typepad_basename: "inventor-dirty-flag-of-inventor-document"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>  <p><strong>Q</strong>: Has API any settings that may influence the Dirty flag of the Inventor documents?</p>  <p><strong>A</strong>: Since Inventor 11 there is a setting that is affecting the true/false behavior of the dirty property. In UI this setting is on the Save tab of the Application Options: &quot;Prompt to save for re-computable updates&quot;. This flag is unchecked by default. It specifies whether a prompt to save the document should be displayed when the document is closed without being explicitly saved after any recomputable changes. The API exposes this option as SaveOptions.PromptSaveForRecomputableChanges.</p>

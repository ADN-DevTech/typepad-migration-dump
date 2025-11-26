---
layout: "post"
title: "Loading custom contextual tab selector rules"
date: "2012-06-04 19:44:25"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/loading-custom-contextual-tab-selector-rules.html "
typepad_basename: "loading-custom-contextual-tab-selector-rules"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>After creating a custom ContextualTabSelectorRules xaml file and copying it to the AutoCAD install folder, you may still not see the custom rule in the CUI Editor after launching AutoCAD.</p>
<div>
<p>This is a known behaviour in AutoCAD. AutoCAD loads the custom rules only if it recognises that the time stamp of the xaml file is more recent as compared to the time it had last read the rules from disk. To workaround this behaviour, following are the two possibilities :</p>
<p>1)  After you copy the xaml file to the AutoCAD install folder, change the modified timestamp of the file to indicate the current time.</p>
<p>2) Delete the "ContextualTabSelectorRules.dll" that was last created by AutoCAD in the User's Roaming folder.</p>
<p>Any of the above two workarounds will ensure that AutoCAD loads the custom rule file.</p>
</div>

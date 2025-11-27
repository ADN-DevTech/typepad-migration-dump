---
layout: "post"
title: "Inventor API: Change Size of CC Component Placed as Custom"
date: "2016-01-08 09:40:53"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/01/inventor-api-change-size-of-cc-component-placed-as-custom.html "
typepad_basename: "inventor-api-change-size-of-cc-component-placed-as-custom"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>
<p>Q: I may use the Change Size command to change a key parameter for a Content Center part placed as a Custom component. But how can I do it programmatically?</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08a937f7970d-pi"><img alt="Change Size command" border="0" height="140" src="/assets/image_0260b4.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="Change Size command" width="402" /></a></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08a937fb970d-pi"><img alt="Table View" border="0" height="289" src="/assets/image_6d08be.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="Table View" width="402" /></a></p>
<p>A: If we talk about “Change Size as custom”, there is no existed API to support currently. There is a series of internal logic that are implemented in CC.</p>
<p>For example,</p>
<p>• Get the target CC table row</p>
<p>• Get all parameters you need to change</p>
<p>• Get the new values</p>
<p>• Batch Edit the parameters</p>
<p>• …</p>
<p>• Update Part</p>
<p>In this case Inventor changes existing part document via Parameters API and iProperties API directly – no new part, no file replacement. Of course, it is always better to have an appropriate API function, but in this particular case we are able to implement this workflow ourselves.</p>
<p>We access our CC custom component saved locally, then get its current row in the CC family table and suppose we know the new target row number.</p>
<p>User parameter’s names in the CC component are the same as the corresponding InternalNames of the ContentTableColumns in the ContentFamily. So we may read target parameter’s values from the cells in the target ContentTableRow and then update the user parameters in the file on disk.</p>
<p>We should also update several iProperties – the part number, stock number, MemberId and may be other iProperties as well. Finally we should rename component in the browser (name is usually based on part number) and then update both the part and assembly documents.</p>
<p>It is not easy to implement a general purpose utility that could work with any CC family, but fortunately in most cases you work with several particular families (e.g., structural shapes). This could simplify this specific implementation.</p>
<p>The attached VBA code demonstrates the workflow that mimics the “Change Size” UI command for the CC component placed as Custom. This code reads family data from the last component in the browser assuming that it is a CC member and asks the user to enter the new row number. New model parameters and iProperties values are applied to the member file on disk. New file is not created.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08a93863970d img-responsive"><a href="http://adndevblog.typepad.com/files/mod_cc_4-1.zip">Download Mod_CC_4</a></span></p>
<p>As a ”proof of concept” this code contains a lot of debug printing and was tested with some structural profiles families only — angles, channels, and i-beams. Hope it could be modified to process other CC families.</p>

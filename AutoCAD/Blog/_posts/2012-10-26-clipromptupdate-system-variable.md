---
layout: "post"
title: "CLIPROMPTUPDATE System variable"
date: "2012-10-26 04:01:31"
author: "Virupaksha Aithal"
categories:
  - "2013"
  - "AutoCAD"
  - "LISP"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/clipromptupdate-system-variable.html "
typepad_basename: "clipromptupdate-system-variable"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>From AutoCAD 2013, CLIPROMPTUPDATE system variable controls the whether the command line displays the progress as a command or script is run. <br />When CLIPROMPTUPDATE is set to 1, AutoCAD updates the command line during the running of command or lisp routine. With Value set as 0, AutoCAD only updates the command line at the end of command or end of lisp routine<br />Try <a href="http://adndevblog.typepad.com/autocad/2012/05/princ-r-does-not-update-the-command-line-anymore.html?cid=6a0167607c2431970b01676718637b970b" target="_self">DevBlog (princ &quot;\r&quot;) does not update the command line anymore</a> with CLIPROMPTUPDATE with value 0 and 1 to find the difference.</p>

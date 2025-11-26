---
layout: "post"
title: "Behavior of M2P snap changed"
date: "2012-05-25 10:12:07"
author: "Wayne Brill"
categories:
  - "LISP"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/behavior-of-m2p-snap-changed.html "
typepad_basename: "behavior-of-m2p-snap-changed"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>
<p><strong>Issue</strong></p>
<p>I am using this menu macro: _M2P;ENDP;\ENDP; In AutoCAD 2010 and later, it does not work the same as it did in AutoCAD 2009 and previous versions. In previous versions I can start the line command, click the menu item with the macro and I am prompted for the two points. The line is then started at the midpoint of the two selected points. In AutoCAD 2010 and later after selecting the points I am still prompted for another point. Is there a way to get the macro to start the line after selecting the two points?&#0160;</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>A way to get the behavior as in the previous releases is to use lisp transparently. Below is an example. The (command ...) function call below does not include a command name, so this can be used while any command is prompting for a point. The menu macro would be this: &#39;SNAPPY</p>
<p>NOTE: There is no preceding ^C and it also includes the leading single quote &#39;.</p>
<p>(defun c:snappy ( / osm ) <br />(if (not (equal (getvar &quot;cmdnames&quot;) &quot;&quot;)) <br />&#0160;&#0160;&#0160;&#0160; (progn <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (setq osm (getvar &quot;osmode&quot;)) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (setvar &quot;osmode&quot; 1) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (command &quot;_m2p&quot; pause) <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (setvar &quot;osmode&quot; osm) <br />&#0160;&#0160;&#0160;&#0160; ) <br />);if <br />(princ) <br />)</p>
<p>&#0160;</p>
<p>There is one drawback to the approach above and that is: Lisp cannot be used transparently to run commands when another lisp routine is prompting for input. (not directly anyway) <br />(defun c:foo () <br />&#0160; (command &quot;_line&quot; pause pause &quot;&quot;) <br />)</p>
<p>This will not work properly due to re-entrancy issues with lisp.</p>
<p>(FOO <br />&#39;snappy</p>
<p>There is a way around this but it involves more code. If you use the sample (link below) then the following macro can be used in conjunction with any command and/or any lisp routine. <br />&#39;_midof;_endp;\_endp</p>
<p>or <br />&#39;_thirdof;_endp;\_endp</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0168ebc9aae6970c"><a href="http://adndevblog.typepad.com/files/midof.zip">Download Midof</a></span></p>

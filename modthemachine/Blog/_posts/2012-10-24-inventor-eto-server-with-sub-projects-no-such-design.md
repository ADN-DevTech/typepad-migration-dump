---
layout: "post"
title: "Inventor ETO Server with Sub projects &ndash; &ldquo;No Such design&rdquo;"
date: "2012-10-24 23:02:41"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/10/inventor-eto-server-with-sub-projects-no-such-design.html "
typepad_basename: "inventor-eto-server-with-sub-projects-no-such-design"
typepad_status: "Publish"
---

<p>Here is a solution for a problem that can occur with ETO Server with a project that has sub projects. You can get a “No such design” error just using the “Test Application Services&quot; button in the Inventor ETO Server Configurator. When the designs are in one project with no sub projects the error does not occur.</p>
<p>To use the IPJ with subprojects from ETO Server, the following two things need to be done:</p>
<p>1. Ensure that the Main.IPJ contains the valid paths. It can be achieved with the XML editor (or notepad)</p>
<blockquote>
<p>a. Find this node in the ipj file:</p>
<p>InventorProject\AddInConfigs\AutodeskIntent\Dependents</p>
</blockquote>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3cf64264970c-pi"><img alt="clip_image002" border="0" height="171" src="/assets/image_131077.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image002" width="482" /></a></p>
<blockquote>
<p>b. Update the paths in the &lt;string&gt; sub-nodes to point to <strong>existing</strong> <strong>IPJ files</strong> on the server.</p>
</blockquote>
<blockquote>
<p>c. Save Main.IPJ</p>
</blockquote>
<p>2. Ensure that shortcuts to these IPJ files are created</p>
<p>&#0160;&#0160;&#0160; a. You have to do that on behalf of <strong>InventorETOServices</strong> user<a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c32c7bba9970b-pi"><img alt="clip_image002[5]" border="0" height="185" src="/assets/image_770441.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image002[5]" width="487" /></a></p>
<blockquote>
<p>b. In the DOS window, enter the command:</p>
</blockquote>
<blockquote>
<p>RunAS /user:InventorETOServices cmd</p>
</blockquote>
<blockquote>
<p>c. You will need to enter the valid password for the InventorETOServices user</p>
</blockquote>
<blockquote>
<p>[default is:&#0160; &quot;ETOServices123!@#&quot; with no quotes, see the <a href="http://wikihelp.autodesk.com/Inventor_ETO/enu/2013/Help/1240-Inventor1240/1248-Intent_S1248/1250-IntentSe1250">wiki help</a> for more info]</p>
</blockquote>
<blockquote>
<p>d. The new DOS window will appear</p>
</blockquote>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3cf6428a970c-pi"><img alt="clip_image004" border="0" height="227" src="/assets/image_287465.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image004" width="447" /></a></p>
<blockquote>
<p>e. Change directory to c:\Program Files\Autodesk\Inventor ETO Server 2013\Bin</p>
</blockquote>
<blockquote>
<p>f. <strong>For each dependent IPJ,</strong> enter the command, similar to the following:</p>
</blockquote>
<blockquote>
<p>InventorETOServer.exe -p &quot;c:\_someDir\xxxx\DataFiles\Directory of ipj files\some-name.ipj&quot;</p>
</blockquote>
<blockquote>
<p>That will create a shortcut in the Inventor Server Documents folder for the InventorETOServices user.</p>
</blockquote>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3cf642a6970c-pi"><img alt="clip_image006" border="0" height="172" src="/assets/image_612302.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="clip_image006" width="472" /></a></p>
<p>&#0160;</p>
<p>This issue is under review by engineering and the problem should be handled in a more graceful manner in one of the future releases.</p>
<p>-Wayne</p>

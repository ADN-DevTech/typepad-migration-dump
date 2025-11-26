---
layout: "post"
title: "Unknown command \"MYGROUP._MYGLOBALCMDNAME\""
date: "2012-07-18 07:32:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/unknown-command-mygroup_myglobalcmdname.html "
typepad_basename: "unknown-command-mygroup_myglobalcmdname"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have a command definied with these properties: groupname: MYGROUP, local command name: MYLOCALCMDNAME, global command name: _MYGLOBALCMDNAME. In my application I used to envoke this command using &quot;MYGROUP._MYGLOBALCMDNAME&quot; but it does not seem to work anymore and AutoCAD says that it&#39;s an unknown command. Why is that?</p>
<p><strong>Solution</strong></p>
<p>Underscore &#39;_&#39; and/or dot &#39;.&#39; should not be used as part of a command name definition/declaration - i.e. don&#39;t include these prefixes in calls to addCommand(), and don&#39;t use them in demandload registry entries.</p>
<p>These characters are used as clues for how to perform a lookup on a given command:</p>
<ul>
<li>A prefix of &#39;_&#39; means: look in the global command list for a match&#0160;</li>
<li>A prefix of &#39;.&#39; means: include undefined commands in the search</li>
</ul>
<p>There used to be an issue in the ARX Wizard that the automatically generated global command name was prefixed with an underscore &#39;_&#39;, but this has been fixed.</p>

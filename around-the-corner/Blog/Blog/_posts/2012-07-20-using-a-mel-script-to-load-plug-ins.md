---
layout: "post"
title: "Using a MEL/Python Script to Load Plug-Ins"
date: "2012-07-20 03:05:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "Maya"
  - "MEL"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/using-a-mel-script-to-load-plug-ins.html "
typepad_basename: "using-a-mel-script-to-load-plug-ins"
typepad_status: "Publish"
---

<p>You may come across a time where you want to tell Maya to  load/unload a plug-in during &quot;plug-in load time&quot; (i.e. while Maya is  launching) or maybe you have certain plug-ins that conflict with each other,  and you would like to have an automatic way of unloading (or &quot;not&quot; loading)  various plug-ins.</p>
<p>To go about doing this there are four options, I highly recommend option 1, as it is the most perdictable and safest.</p>
<h3>Option 1: Using Maya API</h3>
<p>Within the class MSceneMessage there are callbacks for monitoring  plug-in loading:</p>
<ol>
<li>kBeforePluginLoad</li>
<li>kAfterPluginLoad</li>
<li>kBeforePluginUnload</li>
<li>kAfterPluginUnload</li>
</ol>
<p>Whether you use the method addCallback which just allows you  to add additional functionality after one of the above actions or you use the  method addCheckCallback which allows you to abort the above actions all  together and have Maya do something different (like load another plug-in).</p>
<h3>Option 2: Modifying  userPrefs.mel</h3>
<p>Maya determines which plug-ins should be loaded or not from the userPrefs.mel  file (found in the folder: /username/maya/x.x/prefs/). The lines that do this  are at the bottom of the file. They go something like:  evalDeferred(&quot;autoLoadPlugin( ..... ); So a simple but messy solution is  to comment out the lines which load plug-ins you don&#39;t want loaded, in the  userPrefs.mel file.</p>
<h3>Option 3: Modifying initialPluginLoad.mel</h3>
<p>If the first solution is too messy and is not automated (i.e. you have to  re-edit the userPrefs.mel file each time), another possible but solution is to  edit the file: /scripts/startup/initialPluginLoad.mel and add something like  this at the end of the initialPluginLoad function: if (`exists userPluginLoad`)  eval &quot;source userPluginLoad&quot;; Then put userPluginLoad.mel somewhere  on your script path, and this script file should contain something like:</p>
<p>if (`about -batch`)<br /> {<br /> &#0160;&#0160;&#0160; catch(`loadPlugin  &quot;batchPlugin1&quot;`);<br /> &#0160;&#0160;&#0160; catch(`loadPlugin  &quot;batchPlugin2&quot;`);<br /> &#0160;&#0160;&#0160; // ...<br /> }<br /> else<br /> {<br /> &#0160;&#0160;&#0160; catch(`loadPlugin  &quot;interactivePlugin1&quot;`);<br /> &#0160;&#0160;&#0160; catch(`loadPlugin  &quot;interactivePlugin2&quot;`); <br /> &#0160;&#0160;&#0160; // ...<br /> }</p>
<p>By doing this, please note the following:</p>
<ul>
<li>This bypasses the standard autoload feature of plug-ins. If  you set a plug-in to autoload, it will be loaded before Maya gets to  userPluginLoad.mel.</li>
<li>These files normally should NOT be edited, and great caution  must be taken if you decide to proceed in doing so.</li>
<li>The userPluginLoad.mel script should just be MEL code, not a  MEL procedure. The above example assumes that sourcing the script will do all  the work. </li>
</ul>
<h3>Option 4: Modifying autoLoadPlugin.mel</h3>
<p>Plug-ins that are set to autoload are loaded by the MEL script  &quot;autoLoadPlugin.mel&quot;. This script is actually invoked directly from  C++ code or by the user&#39;s preference file, so there is no easy way that you can  avoid having this script invoked... BUT you can change what it does. If you  rewrite autoLoadPlugin.mel to simply ignore some plug-ins if Maya is in batch  mode, then you may be able to do what you want. The other alternative is to  modify the user&#39;s preferences (userPrefs.mel) to remove the offending  autoLoadPlugin command, as indicated in the first solution. This is more  complicated because you have to restore the user&#39;s preferences again, after you  are done with the batch mode. It may not be too bad, if all you&#39;ve got to do is  use the `system` MEL command to invoke OS shell command (e.g. cp, mv, etc...)  to create a &quot;backup&quot; of the original prefs to restore in the end.</p>
<p>Enjoy,</p>
<p>Kristine</p>

---
layout: "post"
title: "Maya Programming: The Beginning"
date: "2012-07-11 13:18:37"
author: "Kristine Middlemiss"
categories:
  - "Kristine Middlemiss"
  - "Maya"
  - "MEL"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/maya-programming-the-beginning.html "
typepad_basename: "maya-programming-the-beginning"
typepad_status: "Publish"
---

<p>…Computer Programming…. Does that sound scary and yet inviting all at the same time?</p>
<p>Are you ever curious to dive into programming and Maya? Are you a power user of Maya, or maybe interested in customizing or automating work flows? Are looking to take your work to the next level and challenge yourself, but not sure where to start… Here I will provide you with some places to start so you can get familiar with scripting so that you are more comfortable and less overwhelmed.</p>
<p>&quot;Maya User, I would like to introduce you to the Script Editor&quot;, it is found here in the UI, with a default install it is placed at the bottom-right of the Maya application:</p>
<p><img alt="ScriptEditorLocation" border="0" src="/assets/image_32db6f.jpg" /></p>
<p>When you click on the Script Editor you see this UI pop-up:</p>
<p><img alt="ScriptEditor" border="0" src="/assets/image_53b442.jpg" /></p>
<p>The Script Editor has numerous features and functionality, the documentation covers what each button does, so look into the help documentation for more information if you are curious to know this UI (User Interface)&#0160;inside and out.</p>
<p>We like to think of the Script Editor as having two halves, top half (Command History) and bottom half (Command Editor). The key now it making these two halves work for you, to help you learn how to script.</p>
<p><strong>Step One: Monitoring the Command History.</strong></p>
<p>Something that Maya does that is pretty special is for&#0160;almost all actions that you perform in the UI; Maya outputs the equivalent MEL command into the Command History pane of the Script Editor. Super Cool!!</p>
<p>In addition the&#0160;Command History pane, displays any feedback such as results, warnings, or errors returned by Maya, which can be helpful not only when learning scripting!</p>
<p>So in other words, for every work flow in the UI, it tells you the MEL solution.&#0160;In time, you&#0160;will figure out how&#0160;we get from Point A (Maya Action)&#0160;to Point B (MEL Command for the Maya Action), knowing where you are going is half the battle, doing it on your own is the other half the battle.</p>
<p>If you don’t see the MEL Commands executed you need to make sure you have not suppressed them and that the History Output is selected for both MEL and Python.</p>
<p><img alt="6-14-2010 10-35-41 AM" border="0" src="/assets/image_ca0c5b.jpg" /></p>
<p><strong>Step Two: Piecing together code in the Command Editor.</strong></p>
<p>This is where the famous copy and paste comes in handy, copy from the top panel and paste in the bottom panel to piece together your custom automation work flow.</p>
<p>However this is the part where you need to be curious, and what I am mean by curious is that you&#0160;have to want to look up all the parts of the command so that you can start to learn what commands do what and what the flag and flag parameter syntax is about. This is the fundamental structure of scripting, so it’s important to understand it. To look up the different commands you can find these in the Help Documentation, hot-key F1.</p>
<p>No one is expecting you to memorize anything, you don&#39;t need to pull out of your head&#0160;a MEL command at the drop of a hat, that&#39;s what command docs are for, there over 600 Maya commands so don&#39;t stress about learning them, the key is learning how to use the documentation efficiently.</p>
<p>Once you get more comfortable with using Maya Commands and the syntax you can start diving into programming basics such as data types, variables, for loops, etc. With these programming structures you can create scripts that are what we like to call more intelligent, because they can do different things based on what is happening in the scene. But we will leave that for another day.</p>
<p><strong>Step Three: Saving your beautiful scripts for Later.</strong></p>
<p>Once you have put together a script, you can drag it into the shelf inside the Maya UI to save it for later and re-use it over and over. To complete this highlight your new script:</p>
<p><img alt="6-14-2010 11-13-08 AM" border="0" src="/assets/image_b8ae91.jpg" /></p>
<p>Press the &#39;Alt&#39; key while dragging your highlighted script into the Shelf tab of your choice, you are then prompted with whether the code is MEL or Python:</p>
<p><img alt="6-14-2010 11-07-22 AM" border="0" src="/assets/image_ee5b1c.jpg" /></p>
<p>The final result now is your code in the Shelf; press the new button to execute it:</p>
<p><img alt="6-14-2010 11-15-26 AM" border="0" src="/assets/image_9184e8.jpg" /></p>
<p><strong>Real World Example: Applying Step One through Three.</strong></p>
<p>An example of a real life work flow could be to set up a scene the way you want it every time, so all you have to do when you come into Maya is click one button to get&#0160;all the default things you want in your scene&#0160;setup.<span style="color: black; mso-fareast-font-family: &#39;Times New Roman&#39;; mso-bidi-font-family: &#39;Courier New&#39;;"> </span></p>
<p><span style="color: black; mso-fareast-font-family: &#39;Times New Roman&#39;; mso-bidi-font-family: &#39;Courier New&#39;;">If we want to ensure nothing is in the scene, let’s do a file new, and we see this line in the Script Editor, let’s copy it ‘file -f -new;’, transpose it into Python, we need to make sure we import the maya.cmds module:</span></p>
<p>import maya.cmds as cmds</p>
<p>cmds.file(f=True, new=True)</p>
<p>Now we want to create two spheres, so we copy those MEL commands and transpose them into Python:</p>
<p>cmds.polySphere (ch=True, o=True, r=27.080953)</p>
<p>cmds.polySphere (ch=True, o=True, r=5.580194)</p>
<p>&#0160;</p>
<p>Full Script:</p>
<p>&#0160;</p>
<p>import maya.cmds as cmds</p>
<p>cmds.file(f=True, new=True)</p>
<p>cmds.polySphere (ch=True, o=True, r=27.080953)</p>
<p>cmds.polySphere (ch=True, o=True, r=5.580194)</p>
<p>I realize this sample is somewhat easy, but starting to script can be this easy, just remember be curious always look up flags, for example, what is the flag &#39;ch on the command PolySphere, what type of value does it take, etc.</p>
<p>Enjoy</p>
<p>Kristine</p>

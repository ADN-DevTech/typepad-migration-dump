---
layout: "post"
title: "Transcript feature in Inventor"
date: "2012-06-01 00:57:50"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/transcript-feature-in-inventor.html "
typepad_basename: "transcript-feature-in-inventor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p><b>Q:</b></p>  <p>Where is the Transcript feature located in Inventor ?</p>  <p><a name="section2"></a></p>  <p><b>A:</b></p>  <p>The Transcript feature has been removed in Inventor 5 and is no longer supported. A transcript is a script similar to VBA (but not VBA) which contains instructions to play operations multiple time. This feature will record all your actions inside Inventor and can be replayed later. You cannot activate this feature from the user interface (as it is removed) and you need to work into the registry. Find below the instructions on how to proceed.   <br />You have to edit registry keys directly (using regedit or similar) to turn transcripting on. Without Inventor running you should set the registry key (Inventor 2013):</p>  <p><em><strong>HKEY_CURRENT_USER\Software\Autodesk\Inventor\RegistryVersion17.0\System\Preferences\Transcript\TranscriptingOn</strong></em>    <br />to 1</p>  <p>Then you should set the following if you wish to have an user interface to play the file:</p>  <p><strong><em>HKEY_CURRENT_USER\Software\Autodesk\Inventor\RegistryVersion17.0\System\Preferences\Transcript\AllowReplay</em></strong>    <br />to 1.</p>  <p>   <br />Then run Inventor and create your drawing. This creates a transcript file (*.tf) in the location specified in the registry key two keys above the one specifying TranscriptingOn. On my machine, this is the key:</p>  <p>   <br /><em><strong>C:\Program Files\Autodesk\Inventor 2013\Bin\Inventor.exe:TransDir</strong></em></p>  <p>   <br />It might be different on yours. You can manually edit this *.tf file if you wish.    <br />To run the macro, start Inventor with no document open, then    <br />*&#160; drag and drop the macro (*.tf) file onto the Inventor window.    <br />* or from the &quot;Extra Tools&quot; pull down menu, choose &quot;Replay Transcript&quot;.    <br />(You might want to edit the file to remove the last couple of lines, which actually close the drawing).</p>  <p>NOTE: Transcripting feature has been removed in Inventor R5 and is not officially supported and has to be used at your own risk.</p>

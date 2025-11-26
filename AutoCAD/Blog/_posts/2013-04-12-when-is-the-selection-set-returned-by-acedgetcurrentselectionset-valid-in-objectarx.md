---
layout: "post"
title: "When is the selection set returned by acedGetCurrentSelectionSet() valid in ObjectARX?"
date: "2013-04-12 13:59:55"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/when-is-the-selection-set-returned-by-acedgetcurrentselectionset-valid-in-objectarx.html "
typepad_basename: "when-is-the-selection-set-returned-by-acedgetcurrentselectionset-valid-in-objectarx"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>I'm confused when acedGetCurrentSelectionSet() should return me a valid Selection Set, can you explain please? </p>  <p>Also, how do I clear the PickFirst Selection Set?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The function acedGetCurrentSelectionSet() fills an selection set with the object IDs of all entities in the Current Selection Set within AutoCAD. The &quot;Current Selection Set&quot; may be one of the following: a pickfirst set, a selection set selected by the select command or any other command that does a selection (that is, similar to the &quot;Previous&quot; selection option), or the most recent set from calling the function ssget(). </p>  <p>You may not get a result calling acedGetCurrentSelectionSet() from a command which does not have the ACRX_CMD_USEPICKSET flag defined. The problem is that you must tell AutoCAD to allow your command to receive the PICK FIRST selection set, otherwise AutoCAD will simply clear it when the command is invoked. Simply add the ACRX_CMD_USEPICKSET to your ACRX_CMD_TRANSPARENT defined command. Be sure to read the ObjectARX Reference guide regarding AcEdCommand::commandFlags for more details.   <br />To clear the PICKFIRST selection set you just need to call acedSSSetFirst(NULL, NULL);</p>

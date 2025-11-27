---
layout: "post"
title: "Provide command parameters using PostPrivateEvent"
date: "2013-01-14 09:04:59"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/provide-command-parameters-using-postprivateevent.html "
typepad_basename: "provide-command-parameters-using-postprivateevent"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You can execute Inventor commands by calling Execute/Execute2 on their ControlDefinition.</p>
<p>Sometimes you may also want to avoid the dialog that the command brings up to get input. <br />Many commands support taking the input provided by PostPrivateEvent, so that the input dialog will not appear.&nbsp;</p>
<p>Here is a small VBA sample that shows how to do this, e.g. running the "Place" command inside an assembly document:</p>
<pre>Sub TestPlace()

    Dim cm As CommandManager
    Set cm = ThisApplication.CommandManager
    
    Call cm.PostPrivateEvent(kFileNameEvent, "C:\temp\test.ipt")

    Dim cd As ControlDefinition
    Set cd = cm.ControlDefinitions("AssemblyPlaceComponentCmd")

    Call cd.Execute
    
End Sub</pre>

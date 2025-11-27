---
layout: "post"
title: "Do selection from iLogic"
date: "2013-11-05 13:33:11"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/11/do-selection-from-ilogic.html "
typepad_basename: "do-selection-from-ilogic"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The only way you can do a selection from iLogic is using the Pick function. However, you can keep calling that in a loop to enable the user to select multiple components as well. Then you can do something with the selected components - in this specific case, we&#39;ll delete them:</p>
<pre>Dim comps As ObjectCollection
Dim comp As Object

comps = ThisApplication.TransientObjects.CreateObjectCollection

While True
	comp = ThisApplication.CommandManager.Pick(
		SelectionFilterEnum.kAssemblyOccurrenceFilter, 
		&quot;Select a component&quot;) 
		
	&#39; If nothing gets selected then we&#39;re done	
	If IsNothing(comp) Then Exit While
	
	comps.Add(comp) 
End While

&#39; If there are selected components we can do something
For Each comp In comps
	comp.Delete()
Next</pre>

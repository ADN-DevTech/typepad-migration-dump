---
layout: "post"
title: "Unused sketch geometry"
date: "2013-09-06 07:21:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/09/unused-sketch-geometry.html "
typepad_basename: "unused-sketch-geometry"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you need to find all the sketch entities that are not used then you can simply iterate through the sketches and their entities and check which ones are not part of a profile yet:</p>
<pre>Public Sub HighlighUncosumedSketchEntities()
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument
    
    Dim oSel As SelectSet
    Set oSel = oDoc.SelectSet
    
    Dim oSketches As PlanarSketches
    Set oSketches = oDoc.ComponentDefinition.Sketches
    
    Dim oUnconsumed As ObjectCollection
    Set oUnconsumed =&#0160;<br />        ThisApplication.TransientObjects.CreateObjectCollection</pre>
<pre>    
    Dim oSketch As PlanarSketch
    For Each oSketch In oSketches
        &#39; First collect all the sketch entities<br />        Dim oEnt As SketchEntity
        For Each oEnt In oSketch.SketchEntities
            Call oUnconsumed.Add(oEnt)
        Next
                 
        &#39; If the sketch is consumed then some of its
        &#39; entities are already used for something
        &#39; so we need to remove those from the collection
        If oSketch.Consumed Then
            Dim oProfile As Profile
            For Each oProfile In oSketch.Profiles
                Dim oPath As ProfilePath
                For Each oPath In oProfile
                    Dim oPEnt As ProfileEntity
                    For Each oPEnt In oPath
                        Call oUnconsumed.RemoveByObject( _<br />                            oPEnt.SketchEntity)
                    Next
                Next
            Next
        End If
    Next
    
    Call oSel.SelectMultiple(oUnconsumed)
End Sub</pre>

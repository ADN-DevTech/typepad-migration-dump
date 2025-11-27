---
layout: "post"
title: "Inventor API: Detect a Content Center part is inserted as standard or custom"
date: "2018-01-04 02:57:24"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2018/01/inventor-api-detect-a-content-center-part-is-inserted-as-standard-or-custom.html "
typepad_basename: "inventor-api-detect-a-content-center-part-is-inserted-as-standard-or-custom"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>When we place a part to the assembly, it could be a common part, or a part from content center yet as standard, or the content center part as custom. Sometimes we would need to know which type part is inserted.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9423f27970b-pi" style="display: inline;"><img alt="Screen Shot 2018-01-04 at 6.52.28 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9423f27970b image-full img-responsive" src="/assets/image_31f21b.jpg" title="Screen Shot 2018-01-04 at 6.52.28 PM" /></a></p>
<p>To differentiate the UI &gt;&gt; [Place] or [Place from Content Center], the first I thought of is the event:&#0160;UserInputEvents::OnActivateCommand. For UI&gt;&gt;[Place], it can tell a meaningful name, but for UI &gt;&gt;&#0160;[Place from Content Center], it only tells a command &quot;InteractionEvents&quot;&#0160; which will also happen with some other UI operations. It cannot either tell as standard or as custom. In addition, the placing would be performed by an API code, the command name cannot over everything.&#0160;</p>
<p>So I turned to check the other event:&#0160;AssemblyEvents::OnNewOccurrence. It can provide the final component definition after the part is inserted, by the definition, we could check if it is a content center part as standard by&#0160;PartComponentDefinition::IsContentMember. As to the content center part as custom, it looks we have to check the properties of the document because a content center part will have some special property sets than the common part, even if it has been converted to as custom part. The <a href="http://adndevblog.typepad.com/manufacturing/2012/06/determine-whether-a-part-is-a-content-center-part-or-not.html">other blog</a> tells more.&#0160;</p>
<p>Based on the investigation, I produced a demo VBA code as below. Hope it helps.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d2cc8c55970c img-responsive"><a href="http://adndevblog.typepad.com/files/testproject.ivb">Download TestProject</a></span></p>
<pre><code>
&#39;module

Dim oClass As TestEvenClass
Sub startEvent()
    Set oClass = New TestEvenClass
    
    oClass.ini
    
    Do While True
        DoEvents
    Loop

End Sub

&#39;event class
Public WithEvents oAssEvents As AssemblyEvents 

Public Sub ini()
  Set oAssEvents = ThisApplication.AssemblyEvents 
End Sub
  

Private Sub oAssEvents_OnNewOccurrence(ByVal DocumentObject As AssemblyDocument, ByVal Occurrence As ComponentOccurrence, ByVal BeforeOrAfter As EventTimingEnum, ByVal Context As NameValueMap, HandlingCode As HandlingCodeEnum)

If BeforeOrAfter = kAfter Then
    
    For Index = 1 To Context.Count

        Dim oContextName As String
        oContextName = Context.Name(Index)
            
        If oContextName = &quot;ComponentDefinition&quot; Then
        
            Dim oDef As ComponentDefinition
            Set oDef = Context.Value(Context.Name(Index))
            
            If TypeOf oDef Is PartComponentDefinition Then
            
                Dim oPartDef As PartComponentDefinition
                Set oPartDef = oDef
                
                If oPartDef.IsContentMember Then
                    &#39;this is a part from content center in standard way
                    Debug.Print &quot;This is a part from Content Center &quot;
                Else
                
                   &#39; check if it is common part of custom part of Content Center
                   
                   Dim isCustomPart As Boolean
                   isCustomPart = False
                   
                   Dim oPropertySets As PropertySets
                   Set oPropertySets = oPartDef.Document.PropertySets
                   
                   Dim oEachProSet As PropertySet
                   Dim oEachP As Property

                    For i = 1 To oPropertySets.Count

                        &#39; use the 6th propertyset of [ContentCenter].
                        &#39; If it is a Custom part, there is only one property within this property set.
                        &#39; The name is “IsCustomPart”. While if it is a Standard CC part, there are some properties
                        &#39; to indicate the information of the CC such as family, member etc..
                        
                        &#39;the internal name of this propertyset is not same across different content center part
                        &#39;so, it looks we have to use display name, which assumes no user defined property set with the same name.

                        Set oEachProSet = oPropertySets(i)
                        &#39;If oEachProSet.InternalName = &quot;{5E21919D-4082-492A-8A30-039424CD07B5}&quot; Then
                        If oEachProSet.DisplayName = &quot;ContentCenter&quot; Then
                     
                            For k = 1 To oEachProSet.Count
                                Set oEachP = oEachProSet(k)
                                If oEachP.Name = &quot;IsCustomPart&quot; Then
                                    Dim oValue As String
                                    oValue = oEachP.Value
                                    
                                    If oValue = &quot;1&quot; Then
                                        isCustomPart = True
                                        Exit For
                                    End If
                                    
                                End If
                            
                            Next
                     
                        End If
                        
                        If isCustomPart Then
                            Exit For
                        End If
                       
                    Next
                    If isCustomPart Then
                        Debug.Print &quot;This is a custom part from content center&quot;
                             
                    Else
                        Debug.Print &quot;This is a common part&quot;
                    End If
                   
                End If
            End If
        End If

    Next
    
End If

End Sub
 
</code></pre>

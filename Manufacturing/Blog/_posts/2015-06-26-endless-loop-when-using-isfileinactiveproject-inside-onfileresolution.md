---
layout: "post"
title: "Endless loop when using IsFileInActiveProject inside OnFileResolution"
date: "2015-06-26 05:49:53"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/endless-loop-when-using-isfileinactiveproject-inside-onfileresolution.html "
typepad_basename: "endless-loop-when-using-isfileinactiveproject-inside-onfileresolution"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>It seems that in <strong>Inventor 2016</strong> the behaviour of&#0160;<strong>IsFileInActiveProject</strong> has been modified in a way that now it triggers the&#0160;<strong>OnFileResolution</strong> event. Which of course means that if you are calling&#0160;<strong>IsFileInActiveProject </strong>from inside<strong>&#0160;<strong>OnFileResolution </strong></strong>then you are creating an endless loop that will bring<strong><strong> Inventor </strong></strong>down.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08490ef2970d-pi" style="display: inline;"><img alt="OnFileResolution" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08490ef2970d image-full img-responsive" src="/assets/image_b23582.jpg" title="OnFileResolution" /></a></p>
<p>I&#39;m not sure yet if this change in behaviour is by-design or not, but it could be. I can imagine that now we allow add-ins to chip in when finding files even in case of using&#0160;<strong>IsFileInActiveProject</strong>.</p>
<p>In order to avoid the endless loop, you just need to use a flag variable to signal if <strong>OnFileResolution</strong> has been called as a result of&#0160;<strong>IsFileInActiveProject&#0160;</strong>or not. Here is my <strong>VBA</strong> <strong>clsEvents</strong> class that shows what I mean:</p>
<pre>Dim WithEvents faEvents As FileAccessEvents

&#39; To avoid recursion
Private <strong>inside</strong> As Boolean

Private Sub Class_Initialize()
  Set faEvents = ThisApplication.FileAccessEvents
  inside = False
End Sub

Private Sub faEvents_OnFileResolution( _
ByVal RelativeFileName As String, _
ByVal LibraryName As String, _
CustomLogicalName() As Byte, _
ByVal BeforeOrAfter As EventTimingEnum, _
ByVal Context As NameValueMap, _
FullFileName As String, _
HandlingCode As HandlingCodeEnum)
    Dim str As String
    Dim loc As LocationTypeEnum
    
    &#39; To avoid recursion we use a flag variable
    &#39; called &quot;inside&quot;
    If Not <strong>inside</strong> Then
        <strong>inside</strong> = True
        
        Dim dpm As DesignProjectManager
        Set dpm = ThisApplication.DesignProjectManager
        
        Call dpm.IsFileInActiveProject( _
            &quot;1.ipt&quot;, _
            loc, _
            str)
            
        <strong>inside</strong> = False

        HandlingCode = HandlingCodeEnum.kEventHandled
    End If
End Sub</pre>

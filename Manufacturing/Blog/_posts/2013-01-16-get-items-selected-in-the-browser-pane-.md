---
layout: "post"
title: "Get items selected in the browser pane "
date: "2013-01-16 03:15:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/get-items-selected-in-the-browser-pane-.html "
typepad_basename: "get-items-selected-in-the-browser-pane-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to find out which items in the browser are selected, then you can simply iterate through all the items in the tree recursively and check each node's <strong>Selected</strong> property.</p>
<p>The following VBA sample shows this in case of a drawing document:&nbsp;</p>
<pre>Function GetSelectedItems(nodes As BrowserNodesEnumerator) As String
    Dim node As BrowserNode
    For Each node In nodes
        If node.Selected Then
            GetSelectedItems = GetSelectedItems _
                + vbCrLf + node.BrowserNodeDefinition.Label
        End If
        
        GetSelectedItems = GetSelectedItems _
            + GetSelectedItems(node.BrowserNodes)
    Next
End Function

Sub GetSelectedItemsInBrowser()
    Dim doc As Document
    Set doc = ThisApplication.ActiveDocument

    If Not TypeOf doc Is DrawingDocument Then
        MsgBox "Open a drawing document first"
        Exit Sub
    End If

    Dim bp As BrowserPane
    Set bp = doc.BrowserPanes("DlHierarchy")
    
    Dim items As String
    items = GetSelectedItems(bp.TopNode.BrowserNodes)
    
    MsgBox items
End Sub</pre>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d400872c5970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017d400872c5970c image-full" alt="Selection" title="Selection" src="/assets/image_941451.jpg" border="0" /></a><br />

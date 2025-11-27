---
layout: "post"
title: "Managing Model States Edit Scope in Autodesk Inventor 2026 API"
date: "2025-04-30 01:09:18"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/04/managing-model-states-edit-scope-in-autodesk-inventor-2026-api.html "
typepad_basename: "managing-model-states-edit-scope-in-autodesk-inventor-2026-api"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>When working with Model States in Autodesk Inventor, controlling which states are actively being edited becomes crucial—especially when automating complex design workflows. The Inventor API provides powerful control through two key properties:</p>
<ul>
<li><strong>MemberEditScope</strong></li>
<li><strong>ModelStatesInEdit</strong></li>
</ul>
<p>This article explains how to use these features to manage model states programmatically in <strong>Autodesk Inventor 2026</strong>, which introduces an important enhancement:</p>
<blockquote><strong>🔧 Inventor 2026 now allows write access to <code>ModelStatesInEdit</code>, enabling developers to directly modify which model states are in edit mode—something not supported in earlier versions.</strong></blockquote>
<hr />
<h3>🧩 Understanding <code>MemberEditScope</code></h3>
<p>The <code>MemberEditScope</code> property determines which model states are affected during an edit operation. It can be one of the following:</p>
<table border="1" cellpadding="5">
<tbody>
<tr>
<th>Constant</th>
<th>Description</th>
</tr>
<tr>
<td><code>kEditActiveMember</code></td>
<td>Only the currently active model state is edited.</td>
</tr>
<tr>
<td><code>kEditAllMembers</code></td>
<td>All model states are edited.</td>
</tr>
<tr>
<td><code>kEditMultipleMembers</code></td>
<td>Multiple (but not all) model states are in edit mode.</td>
</tr>
</tbody>
</table>
<p><strong>Note:</strong> If <code>MemberEditScope</code> returns <code>kEditMultipleMembers</code>, use <code>ModelStatesInEdit</code> to identify the specific model states being edited.</p>
<hr />
<h3>🎯 Understanding <code>ModelStatesInEdit</code></h3>
<p>The <code>ModelStatesInEdit</code> property is a read/write collection that explicitly lists which model states are in edit mode.</p>
<p><strong>Behavior Rules:</strong></p>
<ul>
<li>If it contains only the active model state → <code>MemberEditScope = kEditActiveMember</code></li>
<li>If it contains all model states → <code>MemberEditScope = kEditAllMembers</code></li>
<li>If it includes the active model state plus others → <code>MemberEditScope = kEditMultipleMembers</code></li>
</ul>
<p><strong>✅ Best Practice:</strong> Always include the active model state when assigning <code>ModelStatesInEdit</code>.</p>
<hr />
<h3>🔍 Example 1: Print the Current <code>MemberEditScope</code></h3>
<pre><code>Sub PrintEditScope()
    Dim modelStates As ModelStates
    Set modelStates = ThisApplication.ActiveDocument.ComponentDefinition.modelStates

    Select Case modelStates.MemberEditScope
        Case kEditActiveMember
            Debug.Print &quot;kEditActiveMember&quot;
        Case kEditAllMembers
            Debug.Print &quot;kEditAllMembers&quot;
        Case kEditMultipleMembers
            Debug.Print &quot;kEditMultipleMembers&quot;
    End Select
End Sub
</code></pre>
<p><strong>Output Illustration – Example 1</strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fffb49200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MemberEditScope" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fffb49200d image-full img-responsive" src="/assets/image_40cf06.jpg" title="MemberEditScope" /></a></p>
<hr />
<h3>📃 Example 2: List Model States in Edit Mode</h3>
<pre><code>Sub PrintModelStatesInEdit()
    Dim modelStates As ModelStates
    Set modelStates = ThisApplication.ActiveDocument.ComponentDefinition.modelStates

    Dim inEdit As ObjectCollection
    Set inEdit = modelStates.ModelStatesInEdit

    Dim ms As ModelState
    For Each ms In inEdit
        Debug.Print ms.Name
    Next
End Sub
</code></pre>
<hr />
<h3>✏️ Example 3: Modify the Model States in Edit Mode <em>(New in Inventor 2026)</em></h3>
<p>Thanks to Inventor 2026, we can now dynamically update which model states are in edit mode programmatically:</p>
<pre><code>Sub ModifyMultipleModelStatesSample()
    Dim oModelStates As ModelStates
    Set oModelStates = ThisApplication.ActiveDocument.ComponentDefinition.modelStates

    Dim oCol As ObjectCollection
    Set oCol = ThisApplication.TransientObjects.CreateObjectCollection

    &#39; Activate Model State 2
    Dim oMS As ModelState
    Set oMS = oModelStates.Item(2)
    oMS.Activate

    &#39; Include ModelState2 to ModelState4 in edit mode
    Dim i As Integer
    For i = 2 To 4
        oCol.Add oModelStates.Item(i)
    Next i

    oModelStates.ModelStatesInEdit = oCol
End Sub
</code></pre>
<p><strong>Output Illustration – Example 3</strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d254ee200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ChangeMemberEditScope" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d254ee200c img-responsive" src="/assets/image_24a180.jpg" title="ChangeMemberEditScope" /></a></p>
<h4>🔄 Before and After:</h4>
<ul>
<li><strong>Before running:</strong> Only Model State 1 is in edit mode.</li>
<li><strong>After running:</strong>
<ul>
<li>Model State 2 becomes active.</li>
<li>Model States 2, 3, and 4 are now in edit mode.</li>
</ul>
</li>
</ul>
<hr />
<h3>🔑 Key Takeaways</h3>
<ul>
<li><code>MemberEditScope</code> defines how many model states are being edited.</li>
<li><code>ModelStatesInEdit</code> gives precise control over which model states are in edit mode.</li>
<li><strong>Inventor 2026</strong> enhances this by allowing <strong>write access</strong> to <code>ModelStatesInEdit</code>.</li>
<li>Always include the active model state when modifying the collection.</li>
<li>Use these properties to automate <em>configuration-driven modeling scenarios</em>.</li>
</ul>
<hr />
<h3>💡 Why This Matters</h3>
<p>Model States are essential for representing different design variants or manufacturing stages within a single Inventor file. Controlling how and which states are edited programmatically is vital for:</p>
<ul>
<li>Efficient model management</li>
<li>Automating large assemblies</li>
<li>Dynamically generating manufacturing configurations</li>
</ul>
<p>By leveraging <code>MemberEditScope</code> and the <strong>new write-enabled <code>ModelStatesInEdit</code></strong> in Inventor 2026, you gain full control over your Inventor automation workflows.</p>

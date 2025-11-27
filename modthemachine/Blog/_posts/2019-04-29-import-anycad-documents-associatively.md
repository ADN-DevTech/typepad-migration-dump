---
layout: "post"
title: "Import AnyCAD documents associatively"
date: "2019-04-29 15:23:08"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Assemblies"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/04/import-anycad-documents-associatively.html "
typepad_basename: "import-anycad-documents-associatively"
typepad_status: "Publish"
---

<p>There are two main ways to import a <strong>non-Inventor</strong> document: use &quot;<strong>Reference Model</strong>&quot; or &quot;<strong>Convert Model</strong>&quot;</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a45870dc200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Import1" class="asset  asset-image at-xid-6a00e553fcbfc688340240a45870dc200c img-responsive" src="/assets/image_792715.jpg" title="Import1" /></a></p>
<p>In case of the &quot;<strong>Convert Model</strong>&quot;, for each component of the original document an <strong>Inventor</strong> model (assembly or part) will be created. It&#39;s the same for &quot;<strong>Reference Model</strong>&quot;, but instead of storing those files separately on the hard drive in a special folder (default folder is &quot;<strong>C:\Users\&lt;user name&gt;\Documents\Inventor</strong>&quot;), those documents will be embedded in the <strong>Inventor</strong> model. Not only that, but there will be a <strong>reference</strong> to the original model and if that gets updated, then <strong>Inventor</strong> will offer you to update the host assembly as well.&#0160;</p>
<p>If you simply add a <strong>non-Inventor</strong> model as a new occurrence then the &quot;<strong>Convert Model</strong>&quot; option will be used:<br />(Note: I simply opened the <strong>Stapler</strong> sample (<strong>Inventor</strong> sample models are available from <a href="https://knowledge.autodesk.com/support/inventor-products/troubleshooting/caas/downloads/content/inventor-sample-files.html">here</a>), exported it as a <strong>Step</strong> file and used that in the below code)</p>
<pre>Sub ImportAnyCADToAssembly()
    Dim oDoc As AssemblyDocument
    Set oDoc = ThisApplication.ActiveDocument
 
    Dim oAssyCompDef As AssemblyComponentDefinition
    Set oAssyCompDef = oDoc.ComponentDefinition
    
    Dim oMatrix As Matrix
    Set oMatrix = ThisApplication.TransientGeometry.CreateMatrix()
 
    Dim oOcc As ComponentOccurrence
    Set oOcc = oAssyCompDef.Occurrences.Add( _
        &quot;C:\Autodesk\autodesk_inventor_2019_samples_dlm\Models\Assemblies\Stapler\Stapler.stp&quot;, _
        oMatrix)
End Sub</pre>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4587178200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Import2" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4587178200c img-responsive" src="/assets/image_495200.jpg" title="Import2" /></a></p>
<p>If you want to import the model using the &quot;<strong>Reference Model</strong>&quot; option then you have to go through the <strong>ImportedComponents</strong> collection. There is a <a href="https://help.autodesk.com/view/INVNTOR/2019/ENU/?guid=GUID-DC63C2B0-5CB2-4CBF-A3B9-3AECF1DC3005">sample</a> for that in the <strong>Inventor API Help</strong>, but here it is as well:</p>
<pre>Sub AssociativelyImportAnyCADToAssembly()
    Dim oDoc As AssemblyDocument
    Set oDoc = ThisApplication.ActiveDocument
 
    Dim oAssyCompDef As AssemblyComponentDefinition
    Set oAssyCompDef = oDoc.ComponentDefinition
 
    &#39;Create the ImportedGenericComponentDefinition based on a Step file
    Dim oImportedGenericCompDef As ImportedGenericComponentDefinition
    Set oImportedGenericCompDef = oAssyCompDef.ImportedComponents.CreateDefinition( _
        &quot;C:\Autodesk\autodesk_inventor_2019_samples_dlm\Models\Assemblies\Stapler\Stapler.stp&quot;)
 
    &#39;Set the ReferenceModel to associatively import the Step file
    &#39;True is the default value
    oImportedGenericCompDef.ReferenceModel = True
 
    &#39;Import the Step file to assembly
    Dim oImportedComp As ImportedComponent
    Set oImportedComp = oAssyCompDef.ImportedComponents.Add(oImportedGenericCompDef)
    Dim oOcc As ComponentOccurrence
    &#39;The last occurrence should be the one we just added
    Set oOcc = oAssyCompDef.Occurrences(oAssyCompDef.Occurrences.Count)
    
    &#39;If you want to add more instances, you can do that e.g. through using the
    &#39;occurrence&#39;s definition
    &#39;Dim oMatrix As Matrix
    &#39;Set oMatrix = ThisApplication.TransientGeometry.CreateMatrix()
    &#39;Set oOcc = oAssyCompDef.Occurrences.AddByComponentDefinition(oOcc.Definition, oMatrix)
End Sub</pre>
<p>Now we got the model embedded in the assembly:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a45871f0200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Import3" class="asset  asset-image at-xid-6a00e553fcbfc688340240a45871f0200c img-responsive" src="/assets/image_777712.jpg" title="Import3" /></a></p>
<p>You can see that the <strong>icons</strong> for the occurrence are different in this case from the usual <strong>assembly</strong> and <strong>part</strong> document icons.</p>
<p>For completeness&#39; sake, here is another way of importing <strong>non-Inventor</strong> documents in an <strong>associative</strong> way. This time going through the <strong>translator</strong> add-ins:</p>
<pre>Sub AssocImport()
    Dim sFilename As String
    sFilename = &quot;C:\Autodesk\autodesk_inventor_2019_samples_dlm\Models\Assemblies\Stapler\Stapler.stp&quot;
       
    Dim sClientID As String
    sClientID = &quot;{90AF7F40-0C01-11D5-8E83-0010B541CD80}&quot; &#39;GetTranslatorCLSID for STEP
   
    Dim oTransAddIn As TranslatorAddIn
    Set oTransAddIn = ThisApplication.ApplicationAddIns.ItemById(sClientID)
   
    oTransAddIn.Activate
   
    &#39;Get the transient object, take it as a factory to produce other objects
    Dim oTransientObj As TransientObjects
    Set oTransientObj = ThisApplication.TransientObjects
   
    &#39;Prepare the 1st parameter for Open(), the file name
    Dim oFile As DataMedium
    Set oFile = oTransientObj.CreateDataMedium
    oFile.FileName = sFilename
 
    &#39;Prepare the 2nd parameter for Open(), the open type, for convenience set it as drag&amp;drop
    Dim oContext As TranslationContext
    Set oContext = oTransientObj.CreateTranslationContext
    oContext.Type = kFileBrowseIOMechanism
   
    &#39;Prepare the 3rd parameter for Open(), the import options
    Dim oOptions As NameValueMap
    Set oOptions = oTransientObj.CreateNameValueMap
   
    Dim oDefaultOptions As NameValueMap
    Set oDefaultOptions = oTransientObj.CreateNameValueMap
     
    Dim bHasOpt As Boolean
    bHasOpt = oTransAddIn.HasOpenOptions(oFile, oContext, oDefaultOptions)
    oOptions.Value(&quot;AssociativeImport&quot;) = True
 
    Dim oSourceObj As Object
    oTransAddIn.Open oFile, oContext, oOptions, oSourceObj
   
    Dim oTransDoc As Document
    Set oTransDoc = oSourceObj
   
    If oTransDoc.DocumentType = kAssemblyDocumentObject Then
        Dim oAsm As AssemblyDocument: Set oAsm = oTransDoc
        Debug.Print oAsm.ComponentDefinition.ImportedComponents(1).Name
    ElseIf oTransDoc.DocumentType = kPartDocumentObject Then
        Dim oPart As PartDocument: Set oPart = oTransDoc
        Debug.Print oPart.ComponentDefinition.ReferenceComponents.ImportedComponents(1).Name
    End If
End Sub</pre>
<p>There are two useful code samples in this blog post that can be useful concerning translator add-ins (getting a list of them with their ID&#39;s and listing all the available settings they provide): <a href="https://adndevblog.typepad.com/manufacturing/2014/02/get-option-names-and-values-supported-by-inventor-translator-addins-via-api.html">https://adndevblog.typepad.com/manufacturing/2014/02/get-option-names-and-values-supported-by-inventor-translator-addins-via-api.html</a><br /><br />-Adam</p>

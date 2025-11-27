---
layout: "post"
title: "Add to \"Frequently Used Subfolders\" collection"
date: "2016-11-28 11:29:10"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/11/add-to-frequently-used-subfolders-collection.html "
typepad_basename: "add-to-frequently-used-subfolders-collection"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a>&#0160;(<a href="https://twitter.com/adamthenagy">@AdamTheNagy</a>)</p>
<p>As mentioned in <a href="http://forums.autodesk.com/t5/inventor-customization/add-user-path-to-projet-file/m-p/6493668">this</a> forum post, unfortunately, the relevant&#0160;<strong>API</strong> does not work :(</p>
<p>Until it gets sorted, you could use the following workaround: edit directly the <strong>Project file</strong> (*.<strong>ipj</strong>), which is in fact an <strong>xml</strong> file.</p>
<p>The <strong>&lt;Path&gt;</strong> needs to starts with &quot;<strong>Workspace\</strong>&quot; and then continue with the path to the <strong>subfolder</strong> you want to add.&#0160;</p>
<p>I&#39;m showing how to do it in <strong>VBA</strong>, but <strong>.NET</strong> (also accessible from <strong>iLogic</strong>) has similar support for <strong>xml</strong> editing. &#0160;Just add a reference to the &quot;<strong>Microsoft XML, v6.0</strong>&quot; library:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d23ede74970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MicrosoftXml" class="asset  asset-image at-xid-6a0167607c2431970b01b8d23ede74970c img-responsive" src="/assets/image_30959f.jpg" title="MicrosoftXml" /></a></p>
<pre>Sub AddFrequentlyUsedSubfolder()
    Dim oProjectManager As DesignProjectManager
    Set oProjectManager = ThisApplication.DesignProjectManager
    
    Dim oProject As DesignProject
    Set oProject = oProjectManager.ActiveDesignProject

    &#39; Use &#39;Microsoft XML, v6.0&#39;
    Dim xmlDoc As DOMDocument60
    Set xmlDoc = New DOMDocument60
    xmlDoc.async = False
    
    Dim strProjectPath As String
    strProjectPath = oProject.FullFileName
    
    &#39; Load the project file (*.ipj)
    Call xmlDoc.Load(strProjectPath)
    
    &#39; Select the  node
    Dim xmlProjectPaths As IXMLDOMNode
    Call xmlDoc.setProperty(&quot;SelectionLanguage&quot;, &quot;XPath&quot;)
    Set xmlProjectPaths = xmlDoc.selectSingleNode(&quot;/InventorProject/ProjectPaths&quot;)
    
    &#39; Add the new subfolder
    &#39; We need to add something like this:
    &#39; &lt;ProjectPath pathtype=&quot;FrequentlyUsedFolder&quot;&gt;
    &#39;   &lt;PathName&gt;MyFolder&lt;/PathName&gt;
    &#39;   &lt;Path&gt;Workspace\MySubfolder&lt;/Path&gt;
    &#39; &lt;/ProjectPath&gt;
    Dim xmlProjectPath As IXMLDOMNode
    Set xmlProjectPath = xmlDoc.createNode(1, &quot;ProjectPath&quot;, &quot;&quot;)
    
    Dim xmlPathType As IXMLDOMAttribute
    Set xmlPathType = xmlDoc.createAttribute(&quot;pathtype&quot;)
    xmlPathType.value = &quot;FrequentlyUsedFolder&quot;
    Call xmlProjectPath.Attributes.setNamedItem(xmlPathType)
    
    Dim xmlPathName As IXMLDOMNode
    Set xmlPathName = xmlDoc.createNode(1, &quot;PathName&quot;, &quot;&quot;)
    xmlPathName.Text = &quot;MyFolder&quot;
    Call xmlProjectPath.appendChild(xmlPathName)
    
    Dim xmlPath As IXMLDOMNode
    Set xmlPath = xmlDoc.createNode(1, &quot;Path&quot;, &quot;&quot;)
    xmlPath.Text = &quot;Workspace\&quot; + &quot;MySubfolder&quot;
    Call xmlProjectPath.appendChild(xmlPath)
    
    Call xmlProjectPaths.appendChild(xmlProjectPath)
    
    &#39; Make another project active so that we can save the changes
    Call oProjectManager.DesignProjects(1).Activate(False)
    
    &#39; Save the changes
    Call xmlDoc.Save(strProjectPath)
    
    &#39; Make our project active again
    Call oProject.Activate(False)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8b513dc970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="MyFolder" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8b513dc970b img-responsive" src="/assets/image_0f51f0.jpg" title="MyFolder" /></a></p>
<p>&#0160;</p>

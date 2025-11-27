---
layout: "post"
title: "The Relationship Between iProperties and Model States in Autodesk Inventor 2026 API"
date: "2025-04-08 11:37:43"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/04/the-relationship-between-iproperties-and-model-states-in-autodesk-inventor-api.html "
typepad_basename: "the-relationship-between-iproperties-and-model-states-in-autodesk-inventor-api"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>iProperties and Model States are two key features in Autodesk Inventor that streamline workflows, enhance customization, and optimize performance. In this blog, we’ll dive into these two features and explore how they work together to improve the design and manufacturing process.</p>
<h3>What Are iProperties?</h3>
<p>In Autodesk Inventor, iProperties are a core feature that provides essential metadata for parts and assemblies. These properties can include part numbers, materials, descriptions, and even custom user-defined fields. Through the Inventor API, you can access, modify, and automate these properties to ensure consistency throughout the design and streamline the documentation process.</p>
<h4>Why iProperties Matter</h4>
<p>iProperties reduce the need for manual input and the risk of errors, which is especially valuable when working with large, intricate designs. They also allow for seamless integration with business systems like ERP (Enterprise Resource Planning) and PLM (Product Lifecycle Management), ensuring that design data flows smoothly across production stages. By automating the assignment of properties, iProperties help improve the efficiency and accuracy of your designs.</p>
<h4>Key Benefits of iProperties:</h4>
<ul>
<li><strong>Automation:</strong> Automatically assign and update properties across parts and assemblies.</li>
<li><strong>Customization:</strong> Tailor property sets to meet the unique requirements of your project.</li>
<li><strong>Integration:</strong> Link design metadata to external systems, ensuring consistency across workflows.</li>
<li><strong>Simplified Management:</strong> Streamline data management, particularly for large assemblies and complex designs.</li>
</ul>
<p>With iProperties, Autodesk Inventor users can efficiently handle metadata, reducing complexity in both design and manufacturing processes.</p>
<h3>What Are Model States?</h3>
<p>Model States, introduced in Inventor 2022, provide a new workflow that allows users to create multiple configurations or variations of a part or assembly within a single file. This feature simplifies managing different stages of manufacturing, product simplifications, or creating flexible product families—without needing separate files for each configuration.</p>
<h4>Why Model States Matter</h4>
<p>Model States provide greater control over your designs by enabling different configurations with varying dimensions, features, components, iProperties, and BOM (Bill of Materials). This not only optimizes performance, especially for large assemblies, by allowing components to be suppressed and memory usage to be reduced but also improves the documentation process. Model States capture various configurations or stages in a design, ensuring accurate representations throughout the design lifecycle.</p>
<h4>Key Features of Model States:</h4>
<ul>
<li><strong>Multiple Configurations:</strong> Create different variations of parts or assemblies within a single file.</li>
<li><strong>Performance Optimization:</strong> Suppress components and reduce memory usage, improving overall performance.</li>
<li><strong>Customization:</strong> Modify features, dimensions, and components in each Model State to suit different design iterations.</li>
<li><strong>Collaboration:</strong> Work with multiple instances of the same part in different states within an assembly.</li>
<li><strong>Integration with Representations:</strong> Seamlessly integrate with other representations like View and Positional Representations to enhance flexibility.</li>
</ul>
<p>With Model States, Inventor users can manage complex configurations more efficiently, all within a single file.</p>
<h3>Key Concepts Related to Model States</h3>
<p>To fully appreciate the interaction between iProperties and Model States, it’s important to understand some key concepts related to Model States:</p>
<h4>1. Model States</h4>
<p>Model States allow users to create different configurations or variations of a part or assembly within a single file. These configurations can include variations in features, components, iProperties, and BOM. Through the Inventor API, different Model States can be activated to set or retrieve property values, demonstrating how iProperties can be tied to specific states of the model. This enables users to manage complex designs in a more streamlined and organized manner.</p>
<p><strong data-end="2080" data-start="2055">New in Inventor 2026:</strong></p>
<p>The <code data-end="2118" data-start="2087">ModelStates.ModelStatesInEdit</code> property has been introduced to <strong data-end="2214" data-start="2151">get or set the Model States that are currently in edit mode</strong>. This allows developers to more precisely control which states are being modified during automation. When using this property, the <code data-end="2363" data-start="2346">MemberEditScope</code> will return <code data-end="2398" data-start="2376">kEditMultipleMembers</code>, confirming that multiple states are active for editing. This enhancement greatly expands flexibility when working across several Model States at once.</p>
<h4>2. MemberEditScope</h4>
<p>The MemberEditScope is a critical concept when working with Model States (For more info, refer this documentation link - <a href="https://help.autodesk.com/view/INVNTOR/2026/ENU/?guid=ModelStates_MemberEditScope" rel="noopener" target="_blank">Autodesk Inventor MemberEditScope</a>). It determines whether edits are applied to only the active member (the currently selected configuration) or to all members in a multi-member model. By setting the MemberEditScope to <code>kEditActiveMember</code>, changes are isolated to the active Model State, preventing unintended modifications to other configurations. This concept allows users to make precise edits to specific configurations without affecting the entire model.</p>
<p><strong data-end="3072" data-start="3047">Complementary to this</strong>, the new <code data-end="3101" data-start="3082">ModelStatesInEdit</code> capability in <strong>Inventor 2026</strong> makes it easier to switch between editing single or multiple members programmatically—helpful when updating shared metadata like iProperties across selected configurations.</p>
<h4>3. Global vs. Local Properties</h4>
<p>Model States can include both global and local properties. Global properties affect all Model States, while local properties are specific to the active Model State. In the provided code, certain iProperties (such as &quot;Title&quot; or &quot;Subject&quot;) can be modified either globally or locally, depending on how the Model State is activated. Understanding the distinction between global and local properties is essential for managing how changes are applied across different configurations.</p>
<p><strong>Note:</strong> Properties include iProperty, BOM (BOM structure), etc.</p>
<h3>The Power of iProperties and Model States Together</h3>
<p>When iProperties and Model States are used together, they significantly enhance Autodesk Inventor’s capabilities. iProperties handle document metadata, while Model States manage design configurations, stages, and performance optimizations. Together, these features help streamline workflows, improve collaboration, and provide customization across large assemblies and complex designs.</p>
<h3>Let’s explore some code examples that demonstrate how iProperties and Model States work in the Autodesk Inventor API.</h3>
<h4>Sample Code Walkthrough</h4>
<h4>1. Setting iProperties in Different Model States</h4>
<p>This example demonstrates how to modify iProperties for a specific Model State while isolating the changes to the active member. The code below changes the &quot;Title&quot; iProperty within the second Model State and isolates the change to that specific state by setting the MemberEditScope to <code>kEditActiveMember</code>.</p>
<pre><code>Sub prop1_SetFromPropWithOthers()<br />
    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument
    <br />&#0160;&#0160;&#0160;&#0160;Debug.Print oDoc.ComponentDefinition.ModelStates.MemberEditScope
    <br />&#0160;&#0160;&#0160;&#0160;oDoc.ComponentDefinition.ModelStates.MemberEditScope = kEditActiveMember
    oDoc.ComponentDefinition.ModelStates.Item(2).Activate
    oDoc.PropertySets.Item(1).Item(&quot;Title&quot;).Value = &quot;SetToOthers&quot;
    <br />&#0160;&#0160;&#0160;&#0160;Debug.Print oDoc.FilePropertySets.Item(1).Item(&quot;Title&quot;).Value
End Sub
</code></pre>
<h4>2. Propagating iProperties Across All Model States</h4>
<p>Here, the &quot;Subject&quot; iProperty is updated globally across all Model States. The example below shows how properties not part of the Model State Table, like global properties, can propagate across all Model States.</p>
<pre><code>Sub prop2_NotInTable_Global()
<br />    Dim oDoc As PartDocument
    Set oDoc = ThisApplication.ActiveDocument
    <br />&#0160;&#0160;&#0160;&#0160;Debug.Print oDoc.ComponentDefinition.ModelStates.MemberEditScope
<br />    oDoc.ComponentDefinition.ModelStates.MemberEditScope = kEditActiveMember
    oDoc.ComponentDefinition.ModelStates.Item(2).Activate
    oDoc.FilePropertySets.Item(1).Item(&quot;Title&quot;).Value = &quot;Set77&quot;
    <br />&#0160;&#0160;&#0160;&#0160;Debug.Print oDoc.FilePropertySets.Item(1).Item(&quot;Title&quot;).Value
<br />    &#39;Not in table property
    oDoc.FilePropertySets.Item(1).Item(&quot;Subject&quot;).Value = &quot;Should behave global2&quot;
    Dim oMS As ModelState
<br />    For Each oMS In oDoc.ComponentDefinition.ModelStates
        oMS.Activate
        Debug.Print oMS.Document.PropertySets.Item(1).Item(&quot;Subject&quot;).Value
    Next<br />
End Sub
</code></pre>
<h4>3. Modifying Properties Across Occurrences</h4>
<p>In assembly documents, you can modify properties of part occurrences. The code below updates the &quot;Date Checked&quot; property for a part document accessed through an assembly occurrence, ensuring the update is reflected across all Model States.</p>
<pre><code>Sub prop3_Occ1_ChangeMember_Global()<br />
    Dim oAssyDoc As AssemblyDocument
    Set oAssyDoc = ThisApplication.ActiveDocument
<br />    Dim oDoc As PartDocument
    Set oDoc = oAssyDoc.ComponentDefinition.Occurrences(1).Definition.Document
<br />    oDoc.FilePropertySets.Item(3).Item(&quot;Date Checked&quot;).Value = &quot;2/1/2020&quot;
<br />    Dim oFacDoc As PartDocument
    Set oFacDoc = ThisApplication.Documents.Open(oDoc.FullFileName)
<br />    For Each oMS In oDoc.ComponentDefinition.ModelStates
        Debug.Print oMS.Document.FilePropertySets.Item(3).Item(&quot;Date Checked&quot;).Value
    Next
    oFacDoc.Close True<br />
End Sub
</code></pre>
<h3>Conclusion</h3>
<p data-end="3816" data-start="3368">Both iProperties and Model States in Autodesk Inventor play pivotal roles in enhancing the efficiency and flexibility of design and manufacturing workflows. iProperties help automate metadata management, reduce errors, and integrate seamlessly with external systems like ERP and PLM. Meanwhile, Model States allow users to manage different configurations, optimize performance, and simplify the handling of complex designs—all within a single file.</p>
<p data-end="4078" data-start="3818">With the addition of the <code data-end="3865" data-start="3846">ModelStatesInEdit</code> property in <strong data-end="3895" data-start="3878">Inventor 2026</strong>, users now have granular control over which Model States are in edit mode—enabling more advanced automation scenarios and bulk updates across multiple configurations with confidence.</p>
<p data-end="4411" data-start="4080">By understanding key concepts like Model States, MemberEditScope, and the differences between global and local properties, Inventor users can take full advantage of these features to streamline their design processes, improve collaboration, and create highly customizable product configurations without the need for multiple files.</p>
<p data-end="4596" data-start="4413">The combination of iProperties and Model States in Autodesk Inventor provides a robust, efficient, and scalable solution for managing intricate 3D designs and manufacturing workflows.</p>

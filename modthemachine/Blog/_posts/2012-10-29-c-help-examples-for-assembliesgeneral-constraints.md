---
layout: "post"
title: "C# Help Examples for Assemblies&ndash;General &amp; Constraints"
date: "2012-10-29 23:11:41"
author: "Wayne Brill"
categories:
  - "Assemblies"
  - "C#"
  - "Inventor"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/10/c-help-examples-for-assembliesgeneral-constraints.html "
typepad_basename: "c-help-examples-for-assembliesgeneral-constraints"
typepad_status: "Publish"
---

<p>Here are two more sections of VBA examples converted to C#. These functions are related to constraints and other demos for assemblies that did not fit in one of the other sections. This is the seventh post with converted Help examples.</p>
<p>Note: One of the VBA examples caused a serious error and I logged a Change Request for it. The converted code for this macro is in the C# project but the function name is not added to the combo box. See this <a href="http://modthemachine.typepad.com/my_weblog/2012/08/c-help-examples-for-sketch-part-1.html" target="_blank">post</a> for more information about these projects.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d3d198734970c-pi"><img alt="image" border="0" height="356" src="/assets/image_539740.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="433" /></a></p>
<p>This project has the following functions:</p>
<p>&#0160;<span class="asset  asset-generic at-xid-6a00e553fcbfc68834017c32eb0071970b"><a href="http://modthemachine.typepad.com/files/inventor_help_examples_csharp_asmb_gen_constraints.zip">Download Inventor_Help_Examples_CSharp_Asmb_Gen_Constraints</a></span></p>
<p>FixAllOccurrences <br />CreateShrinkwrapSubstitute <br />AssociativeBodyCopy <br />InsertConstraint <br />MateConstraint <br />MateConstraintOfWorkPlanes <br />MateConstraintWithLimits <br />CreateiMateDefinitionSample <br />iMateResultCreationSample</p>
<p>&#0160;</p>
<p><strong>Here is the InsertConstraint function <br /></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">//&#0160;&#0160;&#0160; Assembly Insert Constraint Add API Sample </span></p>
<p style="margin: 0px;"><span style="color: green;">//Description </span></p>
<p style="margin: 0px;"><span style="color: green;">//This sample demonstrates the creation of an </span></p>
<p style="margin: 0px;"><span style="color: green;">//assembly insert constraint.</span></p>
<p style="margin: 0px;"><span style="color: green;">//Before running the sample, you need to open an</span></p>
<p style="margin: 0px;"><span style="color: green;">//assembly that contains at least two parts. </span></p>
<p style="margin: 0px;"><span style="color: green;">// Select circular edges on the two parts that </span></p>
<p style="margin: 0px;"><span style="color: green;">//will be used for the constraint and run the </span></p>
<p style="margin: 0px;"><span style="color: green;">//sample code. (Set the priority of the Select</span></p>
<p style="margin: 0px;"><span style="color: green;">//command and use the Shift-Select to select </span></p>
<p style="margin: 0px;"><span style="color: green;">//multiple edges.)</span></p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> InsertConstraint()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the active assembly</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyDocument</span> oDoc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">AssemblyDocument</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AssemblyComponentDefinition</span> oAsmCompDef =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">AssemblyComponentDefinition</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oDoc.ComponentDefinition;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Set a reference to the select set.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SelectSet</span> oSelectSet = <span style="color: blue;">default</span>(<span style="color: #2b91af;">SelectSet</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oSelectSet =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ThisApplication.ActiveDocument.SelectSet;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Validate the correct data is in the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//select set.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (oSelectSet.Count != 2)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;Select two circular edges for the insert.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Get the two edges from the select set.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edge</span> oEdge1 = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Edge</span> oEdge2 = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdge1 = (<span style="color: #2b91af;">Edge</span>)oSelectSet[1];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oEdge2 = (<span style="color: #2b91af;">Edge</span>)oSelectSet[2];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">catch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;Select two circular edges for the insert.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//Note: this code does not test to see if </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//the edges are circular edges (they need to be)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Create the insert constraint </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//between the parts.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">InsertConstraint</span> oInsert =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">default</span>(<span style="color: #2b91af;">InsertConstraint</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oInsert = oAsmCompDef.Constraints.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AddInsertConstraint</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (oEdge1, oEdge2, <span style="color: blue;">true</span>, 0);</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>

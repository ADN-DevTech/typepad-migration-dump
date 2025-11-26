---
layout: "post"
title: "Maya 2016, Parallel Evaluation and New API’s"
date: "2015-07-22 02:19:26"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Custom Nodes"
  - "Maya"
  - "Plug-in"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2015/07/maya-2016-parallel-evaluation-and-new-apis.html "
typepad_basename: "maya-2016-parallel-evaluation-and-new-apis"
typepad_status: "Publish"
---

<p>When you start Maya 2016, you will automatically be in parallel evaluation mode. This is new default evaluation mode, replacing the legacy DG-based evaluation. Maya 2016 uses the DG’s dirty propagation mechanism to build the <span style="color: #40a0ff;">Evaluation Graph(EG)</span>. Dirty propagation is the process of walking through the DG, from animation curves to renderable objects, and marking attributes on DG nodes as needing to be re-evaluated (i.e., dirty). Unlike previous version of Maya that propagated dirty on every frame, Maya 2016 disables the dirty propagation once the EG is built, re-using the existing EG until it becomes invalid.</p>
<p>If your plug-in relies on custom dependency management, you’ll need to use new API extensions to ensure a correct evaluation. Since the EG is created using the legacy dirty-propagation mechanism, you’ll need to make sure your custom logic in the <span style="color: #bf00bf;">MPxNode::setDependentsDirty()</span> method override are accounted for.</p>
<p>&#0160;</p>
<p><span style="color: #0060bf;"><strong>New MPxNode::preEvaluation API</strong></span></p>
<p>To avoid performing expensive calculations every time <span style="color: #bf005f;">MPxNode::compute()</span> is called, one strategy plug-in developers can &#0160;use is to store the results from previous evaluations and then relying on <span style="color: #bf005f;">MPxNode::setDependentsDirty()</span> to trigger re-computation. Once the EG has been built, the dirty propagation is disabled and the EG is re-used. Therefore, any custom logic in your plug-in that depends on <span style="color: #bf005f;">setDependentsDirty()</span> will no longer apply.</p>
<p><span style="color: #bf005f;">MPxNode::preEvaluation()</span> allows your plug-in to determine which plugs/attributes are dirty and if any action is needed. This is usually only done within the <span style="color: #bf005f;">MDGContext::fsNormal</span> context. The new MEvaluationNode class can be used to determine what has been dirtied.</p>
<p>See the <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_simple_evaluation_node_2simple_evaluation_node_8cpp_example_html" target="_blank" title="simpleEvaluationNode"><span style="color: #bf005f;"><strong><span style="color: #00bfbf;">simpleEvaluationNode</span> </strong></span></a>devkit example to understand how to use <span style="color: #bf005f;">MPxNode::preEvaluation()</span> method.</p>
<p>&#0160;</p>
<p><span style="color: #0060bf;"><strong>New MPxNode::postEvaluation API</strong></span></p>
<p>Until now it was difficult to determine when all processing for a particular node instance was complete. Users sometimes resorted to complex bookkeeping/callbacks schemes to detect this situation and perform additional work, such as custom rendering. This mechanism was cumbersome and error prone. A new method, <span style="color: #bf005f;">MPxNode::postEvaluation(),</span> is called once all computations have been performed on a specific node instance. Since this method is called from a worker thread, it is possible to perform calculations for downstream graph operations, without blocking other Maya processing tasks.</p>
<p>See the <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_simple_evaluation_draw_2simple_evaluation_draw_8cpp_example_html" target="_blank" title="simpleEvaluationDraw"><span style="color: #bf005f;"><strong><span style="color: #00bfbf;">simpleEvaluationDraw</span> </strong></span></a>devkit example to understand how to use <span style="color: #bf005f;">MPxNode::postEvaluation()</span> method.</p>

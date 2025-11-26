---
layout: "post"
title: "Maya 2017 : New MPxSelectionContext APIs"
date: "2016-11-22 23:27:51"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Mac"
  - "Maya"
  - "Samples"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/maya-2017-new-mpxselectioncontext-apis.html "
typepad_basename: "maya-2017-new-mpxselectioncontext-apis"
typepad_status: "Publish"
---

<p>In Maya 2017, there are 4 new API’s are exposed in <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU//?guid=__cpp_ref_class_m_px_selection_context_html" target="_blank">MPxSelectionContext </a>class namely setAllowPreSelectHilight(), setAllowSoftSelect(), setAllowSymmetry(), setAllowDoubleClickAction().</p>
<p>Please go through the following link for how to use the selection tool. <a href="https://knowledge.autodesk.com/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2017/ENU/Maya/files/GUID-60FD5F79-AC1D-46DE-B66D-2FBE73E15A30-htm.html" target="_blank">https://knowledge.autodesk.com/support/maya/learn-explore/caas/CloudHelp/cloudhelp/2017/ENU/Maya/files/GUID-60FD5F79-AC1D-46DE-B66D-2FBE73E15A30-htm.html</a></p>
<p>In UI method, you can enable/disable and use all the selection methods. However, it is possible to use the same functionalities using MEL and C++ API.</p>
<p><strong>MEL way to enable/disable Soft Selection:</strong></p>
<p>Soft modelling is an option that allows for reflection of basic manipulator actions such as move, rotate, and scale.</p>
<pre class="brush: cpp;toolbar: false;">// Enable soft selection
softSelect -sse on;

// Disable soft selection
softSelect -sse off;
</pre>
<p><strong>MEL way to enable/disable symmetry:</strong> Symmetric modelling is an option that allows for reflection of basic manipulator actions such as move, rotate, and scale.</p>
<pre class="brush: cpp;toolbar: false;">// Enable Symmetry
symmetricModelling -symmetry on;

//Disable Symmetry
symmetricModelling -symmetry off;
</pre>
<p><strong>C++ API for pre selection highlight:</strong></p>
<pre class="brush: cpp;toolbar: false;">//This method enables the support of pre-selection highlight for this context.
MStatus setAllowPreSelectHilight()   
</pre>
<p><strong>C++ API for Soft Selection:</strong></p>
<pre class="brush: cpp;toolbar: false;">//This method enables the support of soft selection for this context.
MStatus setAllowSoftSelect() 
</pre>
<p><strong>C++ API for Symmetry:</strong></p>
<pre class="brush: cpp;toolbar: false;">//This method enables the support of symmetrical selection for this context.
MStatus setAllowSymmetry() 
</pre>
<p><strong>C++ API for Double Click Action:</strong></p>
<pre class="brush: cpp;toolbar: false;">//This method enables the support of double click smart selection for this context.
MStatus setAllowDoubleClickAction()          
</pre>
<p>If you check all the documents in Maya there is no example how to use setAllowPreSelectHilight(), setAllowSoftSelect(), setAllowSymmetry(), setAllowDoubleClickAction() C++ APIs.</p>
<p>Here is the example for selection methods in C++.</p>
<p>Any user-defined class which is inherited from MPxSelectionContext class will have toolOnSetup() virtual method. You can override toolOnSetup() method and add all the above mentioned C++ APIs as below.</p>
<pre class="brush: cpp;toolbar: false;">void richSelectionContext::toolOnSetup( MEvent &amp;event )
{
MPxSelectionContext::toolOnSetup( event );

setAllowPreSelectHilight();
setAllowSoftSelect();
setAllowSymmetry();
setAllowDoubleClickAction();
}
</pre>
<p>How to test the functionalities of selection methods? Please download the sample from <a href="https://github.com/vijayaprakash/richSelectionTool" target="_blank">github</a></p>
<p>Steps to test:</p>
<ol>
<li>Build the <a href="https://github.com/vijayaprakash/richSelectionTool" target="_blank">sample</a></li>
<li>Copy richSelectionProperities.mel and richSelectionValues.mel to Maya script folder “/Applications/Autodesk/maya2017/Maya.app/Contents/scripts/others” (OSX)</li>
<li>Launch Maya 2017</li>
<li>Load richSelectionTool plug-in</li>
<li>Run mel scripts:
<ol>
<li><strong><em>richSelectionContext richSelectionContext1;</em></strong></li>
<li><strong><em>setToolTo richSelectionContext1;</em></strong></li>
<li><strong><em>toolPropertyWindow;</em></strong></li>
</ol>
</li>
<li>Switch to component selection mode (vertex/edge/face)</li>
<li>There you should be on this rich selection tool and could change the soft selection and symmetrical selection options. Double clicking to select shell, edge loop/rings should also works.</li>
</ol>

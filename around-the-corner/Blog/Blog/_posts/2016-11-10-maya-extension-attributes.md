---
layout: "post"
title: "Maya Extension Attributes"
date: "2016-11-10 22:57:30"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "JSON"
  - "Maya"
  - "MEL"
  - "Plug-in"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/11/maya-extension-attributes.html "
typepad_basename: "maya-extension-attributes"
typepad_status: "Publish"
---

<p>Extension attributes are really helpful when plugin developers want to add more attributes to a plugin without modifying the existing plugin code. In Maya, there are many ways to add/remove the extension attributes like MEL way, Python way and C++ API way as well.</p>
<p><strong>MEL Way to add/remove extension attributes:</strong></p>
<ul>
<ul>
<li>To add extension attribute, use <a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-Tech-Docs/Commands/addExtension.html" target="_blank">addExtension</a> command</li>
<li>To remove extension attribute, use <a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-Tech-Docs/Commands/deleteExtension.html" target="_blank">deleteExtension</a> command</li>
<li>To list only extension attribute of an object use &quot;<a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-Tech-Docs/Commands/listAttr.html" target="_blank">listAttr -ex object</a>&quot;</li>
</ul>
</ul>
<pre class="brush: cpp;toolbar: false;">Example:
// Create unknown Node
createNode &quot;unknown&quot;;

// Add extension attribute to node type &quot;unknown&quot;
addExtension -nt &quot;unknown&quot; -shortName &quot;te&quot; -longName &quot;testExtAttr&quot; -at double;

// List all the attributes of unknown1 object;
listAttr unknown1;

// List only extension attribute of &quot;unknown&quot; node
listAttr -ex unknown1;

// Delete the extension attribute of node type &quot;unknown&quot;
deleteExtension -nt &quot;unknown&quot; -at &quot;testExtAttr&quot; -forceDelete on;

// List all the attributes of unknown1 object
listAttr unknown1;
</pre>
<p>The MEL way to add/delete extension is straight-forward and users can easily watch you have done in the MEL script. However, if you want to hide all the extension attribute implementation, you can create your own plugin using C++ API.</p>
<p><strong>C++ API Way to add/remove extension attributes:</strong></p>
<p>To add extension attribute, you can use <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">addExtensionAttribute()</a> API. If you want to remove the extension attribute you can use <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">removeExtensionAttribute()</a> API. If you closely look at the MEL way to add/delete extension attribute you have to use particular nodeType each time while you add/remove extension attributes. In C++ API, there is a way to add an extension attribute to your plugin which may contain different nodeTypes. For example a plugin can have nodeType1, nodeType2,.. You can add extension attribute to a plugin regardless of nodeTypes. It means that the extension attribute is common for all nodeTypes within plugin. You can do this using <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">linkExtensionAttributeToPlugin()</a> API.</p>
<pre class="brush: cpp;toolbar: false;">MDGModifier fCmd; 
MNodeClass* fNodeType;
MFnPlugin   plugin( obj ); 
...
...
MFnCompoundAttribute compAttr;
MFnNumericAttribute numAttr1;
MFnNumericAttribute numAttr2;
MObject extensionChild1 = numAttr1.create( kChild1NameLong, kChild1NameShort, MFnNumericData::kFloat, 0, &amp;status );
CHECK_MSTATUS(status);
MObject extensionChild2 = numAttr2.create( kChild2NameLong, kChild2NameShort, MFnNumericData::kFloat, 0, &amp;status );
CHECK_MSTATUS(status);
MObject extensionParent = compAttr.create( kParentNameLong, kParentNameShort, &amp;status );
CHECK_MSTATUS(status);
compAttr.addChild( extensionChild1 );
compAttr.addChild( extensionChild2 );

// Add parent extension attribute to node
status = fCmd.addExtensionAttribute( *fNodeType, extensionParent );
CHECK_MSTATUS(status);

// Link the extension attribute to plugin regardless of nodeTypes
status = fCmd.linkExtensionAttributeToPlugin(plugin,extensionParent); 
CHECK_MSTATUS(status);
</pre>
<p>Users can delete the extension attribute using <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">removeExtensionAttribute</a><em><a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">()</a> API. Moreover, you can delete the extension attributes regardless of nodeTypes as well for that you have to use <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">unlinkExtensionAttributeToPlugin</a></em><a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">()</a> API.</p>
<pre class="brush: cpp;toolbar: false;">MObject parentAttr = fNodeType-&gt;attribute( kParentNameLong, &amp;status );
CHECK_MSTATUS(status);

// Remove the extension attribute for a particular node type
status = fCmd.removeExtensionAttribute( *fNodeType, parentAttr );
CHECK_MSTATUS(status);

// Unlink the extension attribute from plugin regardless of nodeTypes
status = fCmd.unlinkExtensionAttributeFromPlugin(plugin, parentAttr);
CHECK_MSTATUS(status);
</pre>
<p>Remember Maya never remove or unlink the attribute from node and from plugin automatically. User has to do the above remove and unlink steps manually.</p>
<p>BTW, if you need to list all the extension attributes of a node, here is the way using C++ API:</p>
<pre class="brush: cpp;toolbar: false;">MObjectArray allAttrs;
status = fNodeType-&gt;getAttributes(allAttrs);
CHECK_MSTATUS(status);
int attrCount = fNodeType-&gt;attributeCount();
for (int i=0; i &lt; attrCount; i++)
{
MFnAttribute attr;
attr.setObject(allAttrs[i]);
if ( attr.isExtension() )
cout &lt;&lt; &quot;Attribute is extension, name is &lt;&quot; &lt;&lt; attr.name() &lt;&lt; &quot;&gt;&quot;&lt;&lt; endl;
}
</pre>
<p>In both MEL and C++ way, you have to add the extension attributes one by one i.e, you have to call <a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-Tech-Docs/Commands/addExtension.html" target="_blank">addExtension</a> MEL command or <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_class_m_d_g_modifier_html" target="_blank">addExtensionAttribute()</a> API for each attribute. Let&#39;s say I want to add bunch of extension attributes but I don&#39;t want to add them one be one, yes there is a way in Maya 2017. You can create a attribute pattern file where you can add all the extension attributes with default values. The attribute pattern file is basically a JSON file which is the simple and easiest way to add extension attributes to Maya. Please check the below link for reference. <a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=GUID-B103DD61-8ED8-440E-AB55-64F70E09F7D3" target="_blank">http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=GUID-B103DD61-8ED8-440E-AB55-64F70E09F7D3</a></p>

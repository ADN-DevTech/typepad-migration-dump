---
layout: "post"
title: "Creating a New Attribute from MPlug"
date: "2012-08-27 02:08:00"
author: "Kristine Middlemiss"
categories:
  - "C++"
  - "Kristine Middlemiss"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/creating-new-attribute-from-mplug.html "
typepad_basename: "creating-new-attribute-from-mplug"
typepad_status: "Publish"
---

<p>Have you ever wanted to create a new attribute from the definition of an existing one, such as kTransform? However you cannot get the proper type from the attribute since MDataHanle::type() returns kInvalid:</p>
<pre class="brush: cpp; toolbar: false;">
// Create attribute for future access
MDataHandle hData =plug.asMDataHandle();

MFnTypedAttribute typedAttr;
MFnAttribute fnAttr;
fnAttr.setObject( plug.attribute() );
MString sName =fnAttr.name();
MString sShortName =fnAttr.shortName();
MFnData::Type type =hData.type();
MObject newAttr =typedAttr.create( sName, sShortName, type, MObject::kNullObj, &st );
if ( MS::kSuccess == st ) {
   // Add a dynamic attribute to procfx
   st = MFnDependencyNode( pNode-&gt;thisMObject() ).addAttribute( newAttr, MFnDependencyNode::kLocalDynamicAttr );
   if ( MS::kSuccess  != st ) {
      assert( false );
   }
}
</pre>
<p>MDataHandle will only give you information about the data currently in an attribute, not about the attribute itself. A generic attribute may accept multiple data types.</p>
<p>Along a similar vein, attributes of more complex types, such as kMesh, often don't have a default values. If such an attribute has not yet been given a value, then MDataHandle will come back empty, with a type of kInvalid.</p>
<p>So you really need to stick with MFnAttribute and its derivatives, as well as the various Maya commands for querying attributes: attributeQuery, attributeInfo and getAttr.</p>
<p>For the specific task you're trying to perform - copying an existing attribute from one node to another - the simplest approach is to use MFnAttribute::getAddAttrCmd() to get the 'addAttr' command required to recreate the source attribute, then apply that command to the target node.</p>
<p>Failing that, you can take an MObject containing the attribute to be copied, call its apiType() method and compare that against the various attribute type constants in MFn.h (kNumericAttribute, kDoubleAngleAttribute, kEnumAttribute, etc). Then load the attribute into the corresponding functionset (MFnNumericAttribute, MFnUnitAttribute, MFnEnumAttribute, etc) which will allow you query the remaining info you need to recreate the attribute on another node.</p>
<p>One place where this latter approach may get tricky is if the attribute is using a plug-in data type. MObject::apiType() will return MFn::kTypedAttribute, but I don't know of any sure-fire way of determining the MTypeId of the data type, other than going back to MFnAttribute::getAddAttrCmd() and parsing it out of the returned command string.</p>
<p>You could use MDataHandle::typeId(), but as I pointed out above you might end up getting kInvalid back if the attribute does not yet have a value. What we should really do here is add an attrTypeId() method to MFnTypedAttribute because MFnTypedAttribute has two versions of its create() method: one takes an MFnData constant and creates an attribute of the corresponding standard data type, the other takes an MTypeId and is used to create an attribute of the corresponding plugin data type...one day....</p>
<p>Enjoy,</p>
<p>Kristine</p>

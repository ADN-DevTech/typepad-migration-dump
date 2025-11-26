---
layout: "post"
title: "The trap while working with API MFnSet::isMember()"
date: "2016-10-24 12:28:00"
author: "Zhong Wu"
categories:
  - "C++"
  - "Maya"
  - "Python"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2016/10/the-trap-while-working-with-api-mfnsetismember.html "
typepad_basename: "the-trap-while-working-with-api-mfnsetismember"
typepad_status: "Publish"
---

<p>While working with Maya, sometimes, we create a set to handle multiple elements as a whole. We can add DG nodes, DAG components or plugs to the set and MFnSet is used to operate with the set, but there is one trap with the API that you need to know. </p>  <p>   <br />Most of time, the API works well, the issue only happens when checking for a given DAG node component by MFnSet::isMember(const MDagPath&amp; path, const MObject&amp; component, MStatus* st). Currently, if one component of DAG node is in the set, the API will always return true even you are checking other components of that DAG node. For example, If I add a single component face of a polygonal mesh to a set, then all component faces appear to be part of that set if you use this API. This is a known issue, and there is a bug logged for this problem already. </p>  <p>   <br />The workaround for this issue is to create a selection list, use MFnSet::getMembers to put the members in the selection list and then use API MSelectionList::hasItem(MDagPath path, MObject component) to check if the DAG node component is a part of the set. And here is the snip code for the workaround:</p>  <pre class="brush: python;toolbar: false;">  import maya.api.OpenMaya as om  
     
  msh_sel = om.MSelectionList()  
  msh_sel.add('pPlaneShape1')  
  msh_dag = msh_sel.getDagPath(0)  
  msh_mfn = om.MFnMesh(msh_dag)  
  msh_itr = om.MItMeshPolygon(msh_dag)  
     
  set_sel = om.MSelectionList()  
  set_sel.add('mySet')  
  set_obj = set_sel.getDependNode(0)  
  set_mfn = om.MFnSet(set_obj)  
     
  members = set_mfn.getMembers(True)  
     
  while not msh_itr.isDone():  
        msh_fac_obj = msh_itr.currentItem()  
        if   members.hasItem((msh_dag, msh_fac_obj)):    
            print msh_itr.index(), set_mfn.name()  
        msh_itr.next(0) </pre>

---
layout: "post"
title: "All about modifying reference file in Motionbuilder"
date: "2017-01-11 09:43:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "C++"
  - "MotionBuilder"
  - "Python"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2017/01/all-about-modifying-reference-file-in-motionbuilder.html "
typepad_basename: "all-about-modifying-reference-file-in-motionbuilder"
typepad_status: "Publish"
---

<p>I believe most of you are familiar with File Reference System, it’s commonly used in different Autodesk products, including Motionbuilder. </p>  <p>Recently, some partners are working with file reference in Motionbuilder, they have some confusion and also find some issues. I reviewed this feature on <a href="http://docs.autodesk.com/MOBPRO/2017/ENU/MotionBuilder-Developer-Help/index.html#!/url=./files/GUID-76FBC919-01B2-4DF5-9CAA-EC1FB43047E1.htm">Mobu SDK help</a> again, and I think it worth a post to mention some important point about modifying referenced file within Motionbuilder, we will start with some questions.&#160; </p>  <p>First of all, there are following 2 terms you need to understand:    <br /></p>  <ul>   <li><b><b>Referenced Object</b></b> – A referenced object is a top level object that contains zero or more referenced items. </li>    <li><b><b>Referenced Item</b></b> – A referenced item is any node (in a hierarchy or alone) within a referenced object. </li> </ul>  <p><b>Question 1: How to modify a referenced item/object?</b> </p>  <p>Ok, when an external FBX file is referenced into the scene, how to modify the items in referenced object? Actually, it’s pretty simple, you can modify them almost like a native object, for example, we can change scale of a cube in referenced file as follow: </p>  <pre class="brush:python;toolbar: false;">FBSystem().Scene.NamespaceImport( &quot;NS&quot;, &quot;C:/cube.fbx&quot;, True )
my_cube = FBFindObjectByFullName( &quot;Model::NS:Cube&quot; )
my_cube.Scaling = FBVector3d(50, 20, 10)</pre>

<p>But, for a referenced item/object, unlike a native object in the scene, there are a couple of limitations you need to be aware of including: </p>

<ul>
  <li>A referenced item cannot be renamed. </li>

  <li>A referenced item cannot be deleted. </li>

  <li>An item within a referenced hierarchy cannot be re-parented. </li>

  <li>Referenced objects cannot be nested. </li>

  <li>Keyframe animation is not possible on referenced items unless those items are animated using the Story tool. </li>

  <li>Connections between items within a referenced object are not allowed . </li>
</ul>

<p><b>Question 2: How to keep/save the modification for referenced object?</b> </p>

<p>When we made modifications to the referenced items, can we save it for later use? The answer is “Yes”. When you modify items within a referenced object, MotionBuilder writes the changes in Python script and stores them within the FBX file. This enables the changes to persist when you save and reopen the FBX scene that contains the referenced object. </p>

<p>For example, with above sample code, we scaled the “NS:Cube” in the scene, when the file is saved, the changes and the file reference object are stored inside the FBX scene file. The next time when this scene is opened, the file reference object is created while loading and the changes are also applied to it. You can view the reference edit by using following codes: </p>

<pre class="brush:python;toolbar: false;">lFileReference = FBFindObjectByFullName( “FileReference::NS” )
print lFileReference.GetRefEdit()</pre>

<p>The result should be: </p>

<pre class="brush:python;toolbar: false;">&gt;&gt;&gt; 
from pyfbsdk import *
 
###########################################################################
#pArgList[0] indicates if instancing is happening or not.
#pArgList[1:] indicates the namespace list that the reference edit will affect.
###########################################################################
def FBApplyReferenceEdits( pArgList ):
    for pNamespace in pArgList[1:]:
        lComp = FBFindObjectByFullName( &quot;Model::&quot; + pNamespace + &quot;:Cube&quot; )
        if lComp &lt;&gt; None:
            lProperty = lComp.PropertyList.Find(&quot;Lcl Scaling&quot;)
            if lProperty &lt;&gt; None:
                lProperty.SetString( &quot;{50,20,10}&quot; )
            
        FBSystem().Scene.Evaluate()
###########################################################################
#pArgList[0] indicates if instancing is happening or not.
#pArgList[1:] indicates the namespace list that the reference edit will affect.
###########################################################################
def FBRevertReferenceEdits( pArgList ):
    for pNamespace in pArgList[1:]:
        lComp = FBFindObjectByFullName( &quot;Model::&quot; + pNamespace + &quot;:Cube&quot; )
        if lComp &lt;&gt; None:
            lProperty = lComp.PropertyList.Find(&quot;Lcl Scaling&quot;)
            if lProperty &lt;&gt; None:
                lProperty.SetString( &quot;{1,1,1}&quot; )
            
        FBSystem().Scene.Evaluate()
&gt;&gt;&gt; </pre>

<p><b>Question 3: Can we really save the changes to the source referenced file?</b> </p>

<p>Sometimes, people asks if they can save the modification to the original referenced file, and the method <i>FBFileReference::BakeRefEditToFile(const char *pFilePath=((void *) 0))</i> is very likely to do such things. I did some test with this method, but unfortunately, Motionbuilder will crash when I try to bake the edit. Currently, a change request is logged to track this issue. So unless this is improved, currently, we can not save the changes directly to the source file. </p>

<p><b>Question 4: Can we track changes of Source File?</b> </p>

<p>The answer is “Yes”, the event <i>FBFileMonitoringManager.OnFileChangeFileReference</i> is provided to monitor the change of source file, you can refer <a href="http://docs.autodesk.com/MOBPRO/2017/ENU/MotionBuilder-Developer-Help/index.html#!/url=./files/GUID-B90256BC-DA6C-423F-9945-4F25FF2D2607.htm">here </a>if you are not familiar with that. </p>

<p><b>Questions 5: Can we track changes of referenced object/items?</b> </p>

<p>The answer is still “Yes”, MotionBuilder provides a query based system using <i>dirty flags</i> for retrieving a list of changes. You can get a list of objects and their property changes. The two functions: <code><code>GetSelfModified</code></code> and <code><code>GetContentModified</code></code> are used for checking the dirty flags of a reference object or a reference item. Still use the code above, since we changed the scale of Cube, we will get “True” if we use the following methods to check the status: </p>

<pre class="brush:python;toolbar: false;">file_reference_object.GetContentModified(FBPlugModificationFlag.kFBContentDataModified)
my_cube.PropertyList.Find(&quot;Lcl Scaling&quot;).GetSelfModified( FBPlugModificationFlag.kFBSelfDataModified )  </pre>

<p>For some other flag, please refer the enum <b><b><a href="http://docs.autodesk.com/MOBPRO/2017/ENU/MotionBuilder-Developer-Help/index.html#!/url=./cpp_ref/namespace_o_r_s_d_k2017.html">FBPlugModificationFlag </a></b></b>for more details. </p>

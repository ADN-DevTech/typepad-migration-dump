---
layout: "post"
title: "Entity Name of an Xref SubEntity From its Handle"
date: "2012-06-18 22:36:23"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/entity-name-of-an-xref-subentity-from-its-handle.html "
typepad_basename: "entity-name-of-an-xref-subentity-from-its-handle"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>You can't access an xref'd sub-entity by passing its handle.&nbsp; There could be a conflict with an object in the drawing which already has that handle assigned.&nbsp; Instead it is necessary to check each block (after verifying that it is an xref) to see if one of its sub-entities' handle matches the handle argument which you passed to the lisp function.&nbsp; The following examples prompt the user to type in a handle at the command-line, although it could just as easily have been passed in from another program:</p>
<p>;;;This example examines each xref in the blocks collection<br />;;;for a sub-entity whose handle matches the argument.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">;;;Function (Get_XREF_SubEn)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(defun Get_XREF_SubEnt (sHandleID docCurrent / colBlocks isFound objBlk objSub xrDb)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;;get the blocks collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (setq colBlocks (vla-get-blocks docCurrent))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vlax-for objBlk colBlocks</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; ;;Is the block an xref?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; (if (and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; (= (vlax-get-property objBlk 'IsXref) :vlax-true)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; (not isFound)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; ) ;and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; (vlax-for objSub objBlk</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ;;Does the sub-entity handle match the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ;;handle passed to the function?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (if (and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; (= (vlax-get-property objSub 'Handle) sHandleID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; (not result)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ) ;and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; ;;Then get the xref database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; (setq isFound T</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; xrDb (vlax-get-property objBlk 'XRefDatabase)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; ;;Get the vla-object# of the sub-entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; result (vlax-invoke-method xrDb 'HandleToObject sHandleID)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; ) ;setq</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (vlax-release-object objSub)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; ) ;vlax-for</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; ) ;if</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; (vlax-release-object objBlk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ) ;vlax-for</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vlax-release-object colBlocks)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;;return value to calling function</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; result</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) ;Get_XREF_SubEnt</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">;;;Calling function</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(defun c:Find_Subents ( / appAcad docActive sHandle objSubEnt result)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vl-load-com) ;load ActiveX</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (setq appAcad (vlax-get-acad-object)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; docActive (vla-get-ActiveDocument appAcad)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; sHandle (strcase (getstring &quot;\Enter a handle: &quot;))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ) ;setq</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;;call the function</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (setq objSubEnt(Get_XREF_SubEnt sHandle docActive))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;;display the result</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (if (/= objSubEnt nil)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (alert</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; (strcat &quot;Handle \&quot;&quot; sHandle &quot;\&quot; = AutoCAD Object: &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; (vlax-get-property objSubEnt 'ObjectName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; ) ;strcat</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ) ;alert</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;;release objects from memory</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vlax-release-object docActive)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vlax-release-object appAcad)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">) ;c:Find_Subents</span></p>
</div>

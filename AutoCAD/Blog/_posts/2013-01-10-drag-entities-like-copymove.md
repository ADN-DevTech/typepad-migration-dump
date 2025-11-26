---
layout: "post"
title: "Drag entities like Copy/Move"
date: "2013-01-10 19:35:22"
author: "Xiaodong Liang"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2013/01/drag-entities-like-copymove.html "
typepad_basename: "drag-entities-like-copymove"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>One of the solutions to drag entities like AutoCAD does is using the AcEd global function <strong>acedDragGen</strong>. It prompts the user to modify a selection set by graphically dragging its entities. The method is defined as (from ObjectARX help) : </p>  <pre><strong>int</strong> <strong>acedDragGen</strong>(
    <strong>const</strong> ads_name <strong>ss</strong>, 
    <strong>const</strong> ACHAR * <strong>pmt</strong>, 
    <strong>int</strong> <strong>cursor</strong>, 
    <strong>int</strong> (*<font color="#ff0000">scnf</font>) (ads_point pt, ads_matrix mt), 
    ads_point <strong>p</strong>
);</pre>

<p></p>

<table border="0" cellspacing="0" cellpadding="2" width="400"><tbody>
    <tr>
      <td valign="top" width="200">
        <p>const ads_name ss </p>
      </td>

      <td valign="top" width="200">Selection set obtained by acedSSGet() or acedSSAdd() </td>
    </tr>

    <tr>
      <td valign="top" width="200">
        <p>const ACHAR * pmt </p>
      </td>

      <td valign="top" width="200">Optional prompt string that acedDragGen() displays before pausing for user input; for no prompt, pass a NULL pointer </td>
    </tr>

    <tr>
      <td valign="top" width="200">
        <p>int cursor </p>
      </td>

      <td valign="top" width="200">Form of cursor to display while the user drags the selection set (*scnf) (ads_point pt, ads_matrix mt) : Pointer to the function thatacedDragGen() calls whenever the user moves the cursor </td>
    </tr>

    <tr>
      <td valign="top" width="200">
        <p>ads_point p </p>
      </td>

      <td valign="top" width="200">Value of the cursor's final location after the user finishes dragging the selection set </td>
    </tr>
  </tbody></table>

<p>&#160;</p>

<p>Only entities from the current drawing's model space and paper space can be manipulated by this function. It does not manipulate non-graphical objects or entities in other block definitions. </p>

<p>The <font color="#ff0000">scnf</font> argument must be a pointer to a function whose declaration is compatible with the following:&#160; </p>

<div style="font-family: courier new; background: white; color: black; font-size: 9pt">
  <p style="margin: 0px"><span style="line-height: 140%; color: blue">int </span><span style="line-height: 140%">sample_fcn( </span><span style="line-height: 140%">ads_point pt,</span><span style="line-height: 140%"> ads_matrix mt);</span></p>

  <p style="margin: 0px"><span style="line-height: 140%"></span></p>

  <p style="margin: 0px"><span style="line-height: 140%">&#160;</span></p>
</div>
This function specifies a transformation that acedDragGen() applies to the selection set. The acedDragGen() function calls it every time the user moves the cursor. The first argument, pt, is the current cursor location represented in the entity coordinate system that corresponds to the current UCS. The function should not modify pt. The second, mt, is a 4(4 transformation matrix. The scnf function can transform the selected entities by modifying this matrix, which acedDragGen() then applies to the selection set. By passing the same matrix to acedGrVecs(), you can display vectors that are transformed in the same way as the selection set. This can be useful when the selection set is large and would take a long time to regenerate. 

<p>Following is a code demo. It also shows how to if you want to control the display mode of the original entities while dragging.</p>

<p>&#160;</p>

<div style="font-family: courier new; background: white; color: black; font-size: 9pt">
  <p style="margin: 0px"><span style="line-height: 140%; color: green">// Global, so it can be used in the </span></p>

  <p style="margin: 0px"><span style="line-height: 140%; color: green">// DragGen callback function... </span></p>

  <p style="margin: 0px"><span style="line-height: 140%">ads_point basePt;</span></p>

  <p style="margin: 0px"><span style="line-height: 140%"></span></p>

  <div style="font-family: courier new; background: white; color: black; font-size: 9pt">
    <p style="margin: 0px"><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">void</span><span style="line-height: 140%"> myDrag() </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">{ </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; ads_name ss; </span>&#160; <br />&#160;&#160;&#160;&#160; <span style="line-height: 140%; color: green">// Get the Selection Set </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; acedSSGet(NULL,NULL,NULL,NULL,ss); 
        <br /><span style="line-height: 140%; color: green">&#160;&#160;&#160;&#160; // Base Point for Moving... </span></span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; acedGetPoint(NULL,
        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; L</span><span style="line-height: 140%; color: #a31515">&quot;Select Base Point&quot;</span><span style="line-height: 140%">,
        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; basePt); </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">long</span><span style="line-height: 140%"> len; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; acedSSLength(ss,&amp;len); </span></p>

    <p style="margin: 0px">&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// if you want to control the display mode of </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// the ORIGINAL entities while draging</span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//for(int c=0;c&lt;len;c++) </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//{ </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160; ads_name ent; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160; acedSSName(ss,c,ent);&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%; color: green">&#160;&#160;&#160;&#160; //&#160;&#160;&#160;&#160; e.g. hide the original entities
        <br /><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160; acedRedraw(ent,2);</span></span>&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// mode:</span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//1&#160; Redraw entity&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// 2&#160; Undraw entity (blank it out)&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// 3&#160; Highlight entity&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// 4&#160; Unhighlight entity&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//} </span></p>

    <p style="margin: 0px">&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//Value of the cursor's final location 
        <br />&#160;&#160;&#160;&#160;&#160; //after the user finishes</span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// dragging the selection set&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; ads_point final_pt; </span></p>

    <p style="margin: 0px">&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Call DragGen with the callback function</span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; acedDragGen(ss,
        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; L</span><span style="line-height: 140%; color: #a31515">&quot;Drag Me Around&quot;</span><span style="line-height: 140%">,
        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; 0, 

        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; dragGenCallback,

        <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; final_pt); </span></p>

    <p style="margin: 0px">&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//restore the mode of original entities</span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//for(int c=0;c&lt;len;c++) </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//{ </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160; ads_name ent; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160; acedSSName(ss,c,ent);&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//&#160;&#160;&#160;&#160; acedRedraw(ent,1);</span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">//} </span></p>

    <p style="margin: 0px">&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// get the final matirx and do your work 
        <br />&#160;&#160;&#160;&#160; // such as copy or move

        <br />&#160;&#160;&#160;&#160; // </span><span style="line-height: 140%; color: green"> with the value of final_pt</span>&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%">}&#160; </span></p>

    <p style="margin: 0px">&#160;</p>

    <p style="margin: 0px"><span style="line-height: 140%; color: green">// Here is the callback... </span></p>

    <p style="margin: 0px"><span style="line-height: 140%; color: blue">static</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">int</span><span style="line-height: 140%"> dragGenCallback(ads_point pt,ads_matrix mt) </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">{ </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; AcGeVector3d vec(pt[0]-basePt[0],pt[1]-basePt[1],pt[2]-basePt[2]); </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; AcGeMatrix3d mat; </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// Set our matrix to translate the mouse movements... </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; mat.setToTranslation(vec); </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: green">// And place the results in mt </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">for</span><span style="line-height: 140%">(</span><span style="line-height: 140%; color: blue">int</span><span style="line-height: 140%"> c=0;c&lt;4;c++) </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; { </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">for</span><span style="line-height: 140%">(</span><span style="line-height: 140%; color: blue">int</span><span style="line-height: 140%"> cd=0;cd&lt;4;cd++) </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; mt[c][cd]=mat(c,cd); </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; } </span></p>

    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160;&#160; </span><span style="line-height: 140%; color: blue">return</span><span style="line-height: 140%"> RTNORM; </span></p>
  </div>
</div>

<div style="font-family: courier new; background: white; color: black; font-size: 9pt">}</div>

---
layout: "post"
title: "Plant 3D: How can I control Report Creator&rsquo;s execution?"
date: "2013-02-22 08:56:59"
author: "Marat Mirgaleev"
categories:
  - "Marat Mirgaleev"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/plant-3d-how-can-i-control-report-creators-execution.html "
typepad_basename: "plant-3d-how-can-i-control-report-creators-execution"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>Does Report Creator have any command line options?</em></p>  <p><strong>Solution</strong></p>  <p>Here are the Report Creator command line options:</p>  <p>   <table border="0" cellspacing="0" cellpadding="2" width="585"><tbody>       <tr>         <td valign="top" width="200">/CONFIG &lt;Filename&gt;</td>          <td valign="top" width="383">· Specify report configuration file to use.</td>       </tr>        <tr>         <td valign="top" width="200">           <p>/DWG &lt;filename1[,filename2,filename3, …]&gt;</p>         </td>          <td valign="top" width="383">           <p>· Specify the drawings to include in the report.</p>         </td>       </tr>        <tr>         <td valign="top" width="200">           <p>/HIDDEN</p>         </td>          <td valign="top" width="383">           <p>· Hides the main dialog and generates the report(s) or preview.</p>            <p>· If you don’t specify this, then we show the main dialog with all the other settings filled in, like report configuration file, drawings to select, and which project.</p>         </td>       </tr>        <tr>         <td valign="top" width="200">           <p>/PREVIEW</p>         </td>          <td valign="top" width="383">           <p>· Show report preview.</p>         </td>       </tr>        <tr>         <td valign="top" width="200">           <p>/PROJECT &lt;Filename&gt;</p>         </td>          <td valign="top" width="383">           <p>· Specify the Plant project file to use.</p>         </td>       </tr>        <tr>         <td valign="top" width="200">           <p>/QUIET</p>         </td>          <td valign="top" width="383">           <p>· Be quiet. Turns off error dialogs.</p>         </td>       </tr>     </tbody></table></p>

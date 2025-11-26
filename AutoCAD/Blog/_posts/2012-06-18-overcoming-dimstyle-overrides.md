---
layout: "post"
title: "Overcoming Dimstyle Overrides"
date: "2012-06-18 22:16:45"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/overcoming-dimstyle-overrides.html "
typepad_basename: "overcoming-dimstyle-overrides"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>After using entmod to change a dimstyle, the original settings modified become overrides of the newly modified dimstyle.&nbsp; Here is a way&nbsp;to avoid the creation of overrides.</p>
<p>Example: If the old extension line color for the dimstyle was 6, and the new one is&nbsp;2</p>
<p>&nbsp; - The modified dimstyle has extension lines with color 2<br />&nbsp; - The modified dimstyle now has an override DIMCLRE = 6</p>
<p></p>
<p>Assume there is a dimstyle "MyNewStyle" with DIMCLRE = 6.&nbsp; Executing the following code will modify the dimstyle "MyNewStyle" but will also create a dimstyle override DIMCLRE = 6.</p>
<p></p>
<div>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(setq myDS-Info (entget (tblobjname &quot;dimstyle&quot; &quot;MyNewStyle&quot;))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; myDS-Info (subst</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (cons 177 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (assoc 177 myDS-Info)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; myDS-Info</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; myDS-Info-New (entmod myDS-Info))</span></p>
</div>
<p>To get rid of the Style Overrides, it is necessary to save them to the dimstyle.&nbsp; This can be achieved in two ways:<br />1. The above code should be followed with</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(command &quot;-DIMSTYLE&quot; &quot;R&quot; &quot;MyNewStyle&quot;)</span></p>
</div>
<p></p>
2. The following example uses ActiveX functions to change the dimension variable and then 'vla-CopyFrom' to update the current dimstyle.
<p>To test it, first create the style "MyNewStyle" with DIMCLRE set to 6, and set the style current.&nbsp; After running the code, any subsequent dimensions will have color 2 on the extension lines, and DIMCLRE = 6 will no longer list as an override.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(vl-load-com)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">(defun c:modCurDimStyle ()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;; Get the current dimstyle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (setq acadApp (vlax-get-acad-object)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; curDoc&nbsp; (vla-get-ActiveDocument acadApp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; curDimStyle (vla-get-ActiveDimstyle curDoc)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ) ;setq</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;; Modify the current dimstyle.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;; This is done by changing the current dimvars</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;; and by saving the dimvars in the dimstyle.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;; Change a dimvar.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vla-SetVariable curDoc &quot;DIMCLRE&quot; 2)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; ;; Save the current dim vars in the current dim style.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (vla-CopyFrom curDimStyle curDoc)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (princ)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>

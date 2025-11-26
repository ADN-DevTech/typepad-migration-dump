---
layout: "post"
title: "Delete custom FamilyParameter"
date: "2015-08-13 11:58:44"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/08/delete-custom-familyparameter.html "
typepad_basename: "delete-custom-familyparameter"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/augusto-goncalves.html">Augusto Goncalves</a> (@<a href="https://twitter.com/augustomaia">augustomaia</a>)</p>  <p>Here is a quick code showing how to delete a family parameter using API. It’s not on the “obvious” way, but it’s possible.</p>  <pre style="font-size: 13px; font-family: consolas; background: white; color: black"><span style="color: #2b91af">UIApplication</span> uiapp = commandData.Application;<br /><span style="color: #2b91af">UIDocument</span> uidoc = uiapp.ActiveUIDocument;<br /><span style="color: #2b91af">Application</span> app = uiapp.Application;<br /><span style="color: #2b91af">Document</span> doc = uidoc.Document;<br /> <br /><span style="color: blue">if</span> (doc.IsFamilyDocument)<br />{<br />&#160; <span style="color: #2b91af">FamilyParameter</span> famParam = doc.FamilyManager.get_Parameter(<span style="color: #a31515">&quot;Custom_Param_Name&quot;</span>);<br />&#160; <span style="color: blue">if</span> (famParam != <span style="color: blue">null</span>)<br />&#160; {<br />&#160;&#160;&#160; <span style="color: #2b91af">Transaction</span> trans = <span style="color: blue">new</span>&#160;<span style="color: #2b91af">Transaction</span>(doc, <span style="color: #a31515">&quot;Erase custom family parameter&quot;</span>);<br />&#160;&#160;&#160; trans.Start();<br /><br />&#160;&#160;&#160; <span style="color: green">//doc.FamilyManager.Parameters.Erase(famParam); // cannot erase, read-only<br /><br />&#160;&#160;&#160; <span style="color: green">//doc.ParameterBindings.Remove(famParam.Definition); // not available on fam doc</span></span><br />&#160;&#160;&#160; doc.Delete(famParam.Id); <span style="color: green">// this works!</span><br />&#160;&#160;&#160; trans.Commit();<br />&#160; }<br />}<br /></pre>

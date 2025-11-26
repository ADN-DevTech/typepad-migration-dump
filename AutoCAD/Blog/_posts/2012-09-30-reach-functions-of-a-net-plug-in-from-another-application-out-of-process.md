---
layout: "post"
title: "Reach functions of a .NET plug-in from another application (out-of-process)"
date: "2012-09-30 12:19:47"
author: "Balaji"
categories:
  - ".NET"
  - "ActiveX"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/reach-functions-of-a-net-plug-in-from-another-application-out-of-process.html "
typepad_basename: "reach-functions-of-a-net-plug-in-from-another-application-out-of-process"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>I wrote a .NET&nbsp;plugin and created a COM interface for it to enable driving AutoCAD from my application. Some things seem to work, but e.g. MdiActiveDocument is null and Editor.WriteMessage returns eNotApplicable inside my .NET functions when called from another application through its COM interface. However, when these functions are called from VBA (in-process), then everything is fine. What is the problem?</p>
<div><strong>Solution</strong></div>
<p>The problem is that your COM object is not in the STA. So you are trying to access AutoCAD from a thread other than its main thread. This won't work.</p>
<p>You have 2 ways to solve this problem:</p>
<p>1. Derive your COM object from System.EnterpriseServices.ServicedComponent<br />2. Or transition into the main thread "manually" in each method. Something like this:<br />control.Invoke(realMethod); Where control is any arbitrary window that was created on the main thread.</p>
<p>Here is the implementation of the 1st solution:</p>
<p>This is the plugin's code:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> AcadCOM</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; [</span><span style="color: #2b91af; line-height: 140%;">Guid</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;16FF7627-8439-4ea5-A533-04FEDBD27B95&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">interface</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">iAcadInProc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; [</span><span style="color: #2b91af; line-height: 140%;">DispId</span><span style="line-height: 140%;">(1)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> Test();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; [</span><span style="color: #2b91af; line-height: 140%;">Guid</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;ACC23279-97EE-4ea3-AB08-0491889DEF34&quot;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">ClassInterface</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">ClassInterfaceType</span><span style="line-height: 140%;">.None)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AcadInProc</span><span style="line-height: 140%;"> : System.EnterpriseServices.ServicedComponent, </span><span style="color: #2b91af; line-height: 140%;">iAcadInProc</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> AcadInProc()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; doc.Editor.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nHello world from out of process.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> Test()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">DocumentCollection</span><span style="line-height: 140%;"> docs = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = docs.MdiActiveDocument;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Autodesk.AutoCAD.EditorInput.</span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Test1 in &quot;</span><span style="line-height: 140%;"> + doc.Name);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> doc.Name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>This is the testing application's code:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Interop;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Runtime.InteropServices;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> button1_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, EventArgs e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcadApplication AcApp </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = (AcadApplication)Marshal.GetActiveObject(</span><span style="color: #a31515; line-height: 140%;">&quot;Autocad.Application&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcadCOM.iAcadInProc inProc </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = (AcadCOM.iAcadInProc)AcApp.GetInterfaceObject(</span><span style="color: #a31515; line-height: 140%;">&quot;AcadCOM.AcadInProc&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> test = inProc.Test();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; MessageBox.Show(test);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>The sample project is attached.</p>
<p>1. NetLoad CsMgdAcad1.dll into AutoCAD<br />2. Start MyExternalApp.exe<br />3. Click on the "Call Test" button</p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b017c323d86b9970b"><a href="http://adndevblog.typepad.com/files/sample.zip">Download Sample</a></span>
<p></p>
<p>On a 64 bit system, registration of the COM wrapper needs an extra step. Please refer to <a href="http://adndevblog.typepad.com/autocad/2012/06/error-problem-in-loading-application-on-64-bit-os-when-using-getinterfaceobject.html">this</a> post for the details.</p>

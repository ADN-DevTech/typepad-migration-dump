---
layout: "post"
title: "DWFXOUT: A Scriptable Command to Convert AutoCAD 3D Model to DWFX"
date: "2021-01-20 05:21:56"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2021/01/dwfxout-a-scriptable-command-to-convert-autocad-3d-model-to-dwfx.html "
typepad_basename: "dwfxout-a-scriptable-command-to-convert-autocad-3d-model-to-dwfx"
typepad_status: "Publish"
---

<p>
 </p><p>There is an internal discussion that came up about scripting 3DDWF, the current command <a href="https://knowledge.autodesk.com/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2020/ENU/AutoCAD-Core/files/GUID-62099908-E16E-4BD2-ABE0-C46FB64F4162-htm.html">3DDWF</a> invokes a dialog to save the filename of the export model. The dialogs interrupt automation flow, to bypass and enable automation fluidity, here is the simple scriptable command.
</p><p>The code employees function pointer (<span style="color:black; font-family:Consolas; font-size:9pt">getSymbolAddress</span> ) technique allows retrieving the address of an exported function from the specified dynamic-link library (DLL).
</p><p><span style="color:gray; font-family:Consolas; font-size:9pt">#define<span style="color:black">
				<span style="color:#6f008a">PUBLISH_DLL<span style="color:black">     <span style="color:#6f008a">_T<span style="color:black">(<span style="color:#a31515">"AcPublish.crx"<span style="color:black">)
</span></span></span></span></span></span></span></span></p><p><span style="color:gray; font-family:Consolas; font-size:9pt">#define<span style="color:black">
				<span style="color:#6f008a">PUBLISH_SVC<span style="color:black">
						<span style="color:#6f008a">_T<span style="color:black">(<span style="color:#a31515">"AdskPublish"<span style="color:black">)
</span></span></span></span></span></span></span></span></p><p><span style="color:blue; font-family:Consolas; font-size:9pt">typedef<span style="color:black">
				<span style="color:blue">void<span style="color:black"> (*<span style="color:#2b91af">EXPORT3DDWF<span style="color:black">)(<span style="color:blue">bool<span style="color:black">, <span style="color:blue">const<span style="color:black">
												<span style="color:#2b91af">ACHAR<span style="color:black">*, <span style="color:blue">int<span style="color:black">);
</span></span></span></span></span></span></span></span></span></span></span></span></span></span></p><p>
 </p><p><span style="color:blue; font-family:Consolas; font-size:9pt">void<span style="color:black"> dwgoutcli(){
</span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:#2b91af">TCHAR<span style="color:black"> fileName[<span style="color:#6f008a">MAX_PATH<span style="color:black">] = <span style="color:#6f008a">_T<span style="color:black">(<span style="color:#a31515">""<span style="color:black">);
</span></span></span></span></span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">int<span style="color:black"> res = acedGetString(1, <span style="color:#6f008a">_T<span style="color:black">(<span style="color:#a31515">"\nPlease input the DWFX file name: "<span style="color:black">), fileName);
</span></span></span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">if<span style="color:black"> (res != <span style="color:#6f008a">RTNORM<span style="color:black">){
</span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">return<span style="color:black">;
</span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">    } 
</span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">bool<span style="color:black"> bCancelled = <span style="color:blue">false<span style="color:black">;
</span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">int<span style="color:black"> isFromCLI = 1;
</span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">if<span style="color:black"> (!acrxServiceIsRegistered(<span style="color:#6f008a">PUBLISH_SVC<span style="color:black">)) {
</span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">    <span style="color:blue">if<span style="color:black"> (!<span style="color:#6f008a">acrxDynamicLinker<span style="color:black">-&gt;loadModule(<span style="color:#6f008a">PUBLISH_DLL<span style="color:black">, <span style="color:blue">false<span style="color:black">, <span style="color:blue">true<span style="color:black">))
</span></span></span></span></span></span></span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">    <span style="color:blue">return<span style="color:black">;
</span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">    }
</span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:green">// Get the address of the export3dDWF service.<span style="color:black">
				</span></span></span></p><p>    
 </p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:#2b91af">EXPORT3DDWF<span style="color:black"> pFunc = (<span style="color:#2b91af">EXPORT3DDWF<span style="color:black">) <span style="color:#6f008a">acrxDynamicLinker<span style="color:black">-&gt;
</span></span></span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">        getSymbolAddress(<span style="color:#6f008a">PUBLISH_SVC<span style="color:black">, <span style="color:#a31515">"export3dDWF"<span style="color:black">);
</span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">if<span style="color:black"> (pFunc == <span style="color:#6f008a">NULL<span style="color:black">) {
</span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:#6f008a">assert<span style="color:black">(<span style="color:#6f008a">FALSE<span style="color:black">);
</span></span></span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">
			<span style="color:blue">return<span style="color:black">;
</span></span></span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">    }
</span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">    pFunc(bCancelled, fileName, isFromCLI);
</span></p><p><span style="color:black; font-family:Consolas; font-size:9pt">}</span>
	</p><p>
 </p><p><a href="https://github.com/MadhukarMoogala/dwfout"><span style="font-family:Consolas; font-size:9pt">Github Source</span></a></p>

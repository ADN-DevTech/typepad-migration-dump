---
layout: "post"
title: "How to resolve assemblies in ACAD 2015 SP2 [Serializat​ionExcepti​on Only Affecting AutoCAD 2015 SP2]"
date: "2014-11-17 19:30:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2014/11/how-to-resolve-assemblies-in-acad-2015-sp2-serializationexception-only-affecting-autocad-2015-sp2.html "
typepad_basename: "how-to-resolve-assemblies-in-acad-2015-sp2-serializationexception-only-affecting-autocad-2015-sp2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>With ACAD 2015 SP2 , application no longer resolves assemblies that is if an application is using any dependent DLL and are not located in host application directory ,the plugin application may crash as it is failed to load those dlls</p>
<p>I came to know from my engineering colleague Art, recently he has responded a query on similar issue on forum <a href="http://forums.autodesk.com/t5/net/serializationexception-only-affecting-autocad-2015-sp2/m-p/5387615#M42400">Serialization Exception</a></p>
<p>This blog intention is to target larger audience.</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">Imports</span> System</p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.Runtime</p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.DatabaseServices</p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.Geometry</p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> Autodesk.AutoCAD.EditorInput</p>
<p style="margin: 0px;"><span style="color: green;">&#39;dependent assembly</span></p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> ThreadSafe</p>
<p style="margin: 0px;"><span style="color: blue;">Imports</span> System.Reflection</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">&#39; This line is not mandatory, but improves loading performances</span></p>
<p style="margin: 0px;">&lt;<span style="color: blue;">Assembly</span>: <span style="color: #2b91af;">CommandClass</span>(<span style="color: blue;">GetType</span>(TestVB15.<span style="color: #2b91af;">MyCommands</span>))&gt;</p>
<p style="margin: 0px;"><span style="color: blue;">Namespace</span> TestVB15</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">MyCommands</span>&#0160; &lt;<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;MyGroup&quot;</span>, <span style="color: #a31515;">&quot;MyCommand&quot;</span>, <span style="color: #a31515;">&quot;MyCommandLocal&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.Modal)&gt; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> MyCommand()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">AddHandler</span> <span style="color: #2b91af;">AppDomain</span>.CurrentDomain.AssemblyResolve, _</p>
<p style="margin: 0px;"><span style="color: blue;">AddressOf</span> OnAssemblyResolve</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">&#39;TestObject is class from custom made ThreadSafe dll namespace</span></p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> TstObj <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">TestObject</span></p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> TstObjBts <span style="color: blue;">As</span> <span style="color: blue;">Byte</span>() = TstObj.ToBytes</p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> TstObj2 <span style="color: blue;">As</span> <span style="color: #2b91af;">TestObject</span> = _</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ThreadSafeDataClass</span>.FromBytes(<span style="color: blue;">Of</span> <span style="color: #2b91af;">TestObject</span>)(TstObjBts)</p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> TstObjs <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">TestObjects</span></p>
<p style="margin: 0px;">TstObjs.Add(TstObj)</p>
<p style="margin: 0px;">TstObjs.Add(TstObj2)</p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> TstObjsBts <span style="color: blue;">As</span> <span style="color: blue;">Byte</span>() = TstObjs.ToBytes</p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> TstObjs2 <span style="color: blue;">As</span> <span style="color: #2b91af;">TestObjects</span> =</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ThreadSafeDataClass</span>.FromBytes(<span style="color: blue;">Of</span> <span style="color: #2b91af;">TestObjects</span>)(TstObjsBts)</p>
<p style="margin: 0px;">TstObjs2.Add(<span style="color: blue;">New</span> <span style="color: #2b91af;">TestObject</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;<span style="color: blue;">Private</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Function</span> OnAssemblyResolve(</p>
<p style="margin: 0px;">sender <span style="color: blue;">As</span> <span style="color: blue;">Object</span>, args <span style="color: blue;">As</span> <span style="color: #2b91af;">ResolveEventArgs</span>) <span style="color: blue;">As</span> <span style="color: #2b91af;">Assembly</span></p>
<p style="margin: 0px;">&#0160;<span style="color: blue;">Dim</span> assembly <span style="color: blue;">As</span> <span style="color: #2b91af;">Assembly</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;<span style="color: blue;">Dim</span> assems <span style="color: blue;">As</span> <span style="color: #2b91af;">[Assembly]</span>() =_ <span style="color: #2b91af;">AppDomain</span>.CurrentDomain.GetAssemblies()</p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> shortName <span style="color: blue;">As</span> <span style="color: blue;">String</span> = _</p>
<p style="margin: 0px;">args.Name.Split(<span style="color: blue;">New</span> <span style="color: #2b91af;">[Char]</span>() {<span style="color: #a31515;">&quot;,&quot;</span>}).GetValue(0)</p>
<p style="margin: 0px;"><span style="color: green;">&#39;ThreasSafe dependent Assembly </span></p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> threadSafe <span style="color: blue;">As</span> <span style="color: blue;">String</span> = <span style="color: #a31515;">&quot;ThreadSafe&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;<span style="color: green;">&#39;check that the assembly being asked for is one of ours and if it is not, then return nothing</span></p>
<p style="margin: 0px;"><span style="color: blue;">If</span> shortName &lt;&gt; threadSafe <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;<span style="color: blue;">Return</span> assembly</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">For</span> <span style="color: blue;">Each</span> assem <span style="color: blue;">In</span> assems</p>
<p style="margin: 0px;"><span style="color: blue;">If</span> assem.GetName.Name = shortName <span style="color: blue;">Then</span></p>
<p style="margin: 0px;"><span style="color: blue;">Return</span> assem</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;"><span style="color: blue;">Next</span> assem</p>
<p style="margin: 0px;">&#0160;<span style="color: green;">&#39;if assembly is not loaded ,we &#39;ll explictly load this may\may not be required</span></p>
<p style="margin: 0px;"><span style="color: blue;">Dim</span> an = <span style="color: blue;">New</span> <span style="color: #2b91af;">AssemblyName</span>(args.Name)</p>
<p style="margin: 0px;"><span style="color: blue;">If</span> an.Name &lt;&gt; args.Name <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">assembly = <span style="color: blue;">DirectCast</span>(sender, <span style="color: #2b91af;">AppDomain</span>).Load(an.Name)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;"><span style="color: blue;">Return</span> assembly</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Function</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Class</span></p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Namespace</span></p>
</div>

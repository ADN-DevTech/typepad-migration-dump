---
layout: "post"
title: "Inventor Events using .NET&ndash; 3 examples"
date: "2013-07-18 21:59:44"
author: "Wayne Brill"
categories:
  - "Beginning API"
  - "C#"
  - "Inventor"
  - "VB.NET"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/07/inventor-events-using-net-3-examples.html "
typepad_basename: "inventor-events-using-net-3-examples"
typepad_status: "Publish"
---

<p>If you are getting started with the Inventor API and you need some examples for adding an event in an Inventor AddIn take a look at these examples. The code snippets below show how to handle ApplicationEvents.OnActivateDocument event using VB.Net and C#. The VB.Net allows use of traditional event handling using WithEvents statement, but also supports .NET style using delegates. There are 3 samples attached covering these ways to add events. The samples display the name of the document after the document is activated. The samples were created using Visual Studio .NET 2010. Please see this Topic in the Inventor API help file if you need instructions on how to get Inventor to load the dll files.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc688340191044a59ca970c-pi"><img alt="image" border="0" height="258" src="/assets/image_178178.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="481" /></a></p>
<p>&#0160;</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a00e553fcbfc688340192ac139ed5970d"><a href="http://modthemachine.typepad.com/files/inventor_events_examples..zip">Download Inventor_Events_Examples.</a></span></p>
<p><strong>VB.Net - WithEvents</strong> <br />1. Declare member variable of type ApplicationEvents.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">StandardAddInServer</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Implements</span> Inventor.<span style="color: #2b91af;">ApplicationAddInServer</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Inventor application object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Private</span> m_inventorApplication <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Application</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> <span style="color: blue;">WithEvents</span> m_AppEvents <span style="color: blue;">As</span> <span style="color: #2b91af;">ApplicationEvents</span></p>
</div>
<p>&#0160; <br />2. In the Activate method of your AddInServer class, assign ApplicationEvents to member variable:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> Activate(<span style="color: blue;">ByVal</span> addInSiteObject <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApplicationAddInSite</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ByVal</span> firstTime <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span>) _</p>
<p style="margin: 0px;"><span style="color: blue;">Implements</span> Inventor.<span style="color: #2b91af;">ApplicationAddInServer</span>.Activate</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; This method is called by Inventor when it </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; loads the AddIn.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; The AddInSiteObject provides access to the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Inventor Application object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; The FirstTime flag indicates if the AddIn </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; is loaded for the first time.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Initialize AddIn members.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_inventorApplication =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addInSiteObject.Application</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; TODO:&#0160; Add ApplicationAddInServer.Activate</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; implementation. e.g. event initialization, </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;command creation etc.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_AppEvents =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApplication.ApplicationEvents</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>&#0160; <br />3. Define event handler method. In Visual Studio IDE it&#39;s possible to use Class and Method combo boxes (at the top of open document) to automatically generate it.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> m_AppEvents_OnActivateDocument _</p>
<p style="margin: 0px;">(<span style="color: blue;">ByVal</span> DocumentObject <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">_Document</span>, _</p>
<p style="margin: 0px;"><span style="color: blue;">ByVal</span> BeforeOrAfter <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">EventTimingEnum</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160; <span style="color: blue;">ByVal</span> Context <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">NameValueMap</span>, _</p>
<p style="margin: 0px;"><span style="color: blue;">ByRef</span> HandlingCode <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">HandlingCodeEnum</span>) _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Handles</span> m_AppEvents.OnActivateDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> BeforeOrAfter &lt;&gt;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">EventTimingEnum</span>.kAfter <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (DocumentObject.DisplayName, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;OnActivateDocument VB - WithEvents)&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxButtons</span>.OK, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxIcon</span>.Information)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>&#0160;</strong></p>
<p><strong>&#0160;</strong></p>
<p><strong>VB.Net – Delegates</strong></p>
<p>1. Declare member variable of type ApplicationEvents.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">StandardAddInServer</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Implements</span> Inventor.<span style="color: #2b91af;">ApplicationAddInServer</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Inventor application object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Private</span> m_inventorApplication <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">Application</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> m_AppEvents <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApplicationEvents</span></p>
</div>
<p>&#0160; <br />2. In the Activate method of your AddInServer class, add delegate to handle ApplicationEvents.OnActivateDocument event:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> Activate(<span style="color: blue;">ByVal</span> addInSiteObject <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApplicationAddInSite</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ByVal</span> firstTime <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span>) _</p>
<p style="margin: 0px;">&#0160;<span style="color: blue;">Implements</span> Inventor.<span style="color: #2b91af;">ApplicationAddInServer</span>.Activate</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Initialize AddIn members.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_inventorApplication =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addInSiteObject.Application</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_AppEvents =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApplication.ApplicationEvents</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">AddHandler</span> m_AppEvents.OnActivateDocument,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">AddressOf</span> ApplicationEvents_OnActivateDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>3. Remove the event in the Deactivate method.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> Deactivate() <span style="color: blue;">Implements</span> _</p>
<p style="margin: 0px;">Inventor.<span style="color: #2b91af;">ApplicationAddInServer</span>.Deactivate</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; This method is called by Inventor </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; when the AddIn is unloaded.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; The AddIn will be unloaded either </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; manually by the user or</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; when the Inventor session is terminated.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; TODO:&#0160; Add ApplicationAddInServer.Deactivate </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; implementation()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Release objects.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_inventorApplication = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">RemoveHandler</span> m_AppEvents.OnActivateDocument,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">AddressOf</span> ApplicationEvents_OnActivateDocument</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_AppEvents = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">GC</span>.Collect()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">GC</span>.WaitForPendingFinalizers()</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>&#0160;</p>
<p>4. Define event handler method. It&#39;s possible to use TAB key to automatically generate it.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> ApplicationEvents_OnActivateDocument _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: blue;">ByVal</span> DocumentObject <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">_Document</span>, _</p>
<p style="margin: 0px;"><span style="color: blue;">ByVal</span> BeforeOrAfter <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">EventTimingEnum</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">ByVal</span> Context <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">NameValueMap</span>, _</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">ByRef</span> HandlingCode <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">HandlingCodeEnum</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> BeforeOrAfter &lt;&gt; <span style="color: #2b91af;">EventTimingEnum</span>.kAfter <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(DocumentObject.DisplayName, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;OnActivateDocument VB Delegates)&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxButtons</span>.OK, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBoxIcon</span>.Information)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>&#0160; <br /><strong>C# - Delegates</strong> <br />1. Declare member variable of type ApplicationEvents.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">StandardAddInServer</span> :</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApplicationAddInServer</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Inventor application object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> Inventor.<span style="color: #2b91af;">Application</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApplication;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> Inventor.<span style="color: #2b91af;">ApplicationEvents</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_appEvents;</p>
</div>
<p>&#0160; <br />2. In the Activate method of your AddInServer class, add delegate to handle ApplicationEvents.OnActivateDocument event:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">void</span> Activate(Inventor.<span style="color: #2b91af;">ApplicationAddInSite</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addInSiteObject, <span style="color: blue;">bool</span> firstTime)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// This method is called by Inventor when it </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//loads the addin. The AddInSiteObject provides</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// access to the Inventor Application object.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// The FirstTime flag indicates if the addin </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// is loaded for the first time.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Initialize AddIn members.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_inventorApplication =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; addInSiteObject.Application;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// TODO: Add ApplicationAddInServer.Activate </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// implementation. e.g. event initialization, </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//command creation etc.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_appEvents =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApplication.ApplicationEvents;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_appEvents.OnActivateDocument +=&#0160; <span style="color: blue;">new</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">ApplicationEventsSink_OnActivateDocumentEventHandler</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ApplicationEvents_OnActivateDocument);</p>
<p style="margin: 0px;">}</p>
</div>
<p>3. Remove the event in the Deactivate method</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;<span style="color: blue;">public</span> <span style="color: blue;">void</span> Deactivate()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Release objects.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApplication = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_appEvents.OnActivateDocument -= <span style="color: blue;">new</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">ApplicationEventsSink_OnActivateDocumentEventHandler</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (ApplicationEvents_OnActivateDocument);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_appEvents = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">GC</span>.Collect();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">GC</span>.WaitForPendingFinalizers();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
</div>
<p>4. Define event handler method. It&#39;s possible to use TAB key to automatically generate it.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">private</span> <span style="color: blue;">void</span> ApplicationEvents_OnActivateDocument</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (<span style="color: #2b91af;">_Document</span> DocumentObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">EventTimingEnum</span> BeforeOrAfter,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">NameValueMap</span> Context,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">out</span> <span style="color: #2b91af;">HandlingCodeEnum</span> HandlingCode)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; HandlingCode =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">HandlingCodeEnum</span>.kEventNotHandled;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (BeforeOrAfter !=</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">EventTimingEnum</span>.kAfter)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; HandlingCode =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">HandlingCodeEnum</span>.kEventHandled;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">MessageBox</span>.Show(DocumentObject.DisplayName,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;C# - OnActivateDocument&quot;</span>,</p>
<p style="margin: 0px;"><span style="color: #2b91af;">MessageBoxButtons</span>.OK, <span style="color: #2b91af;">MessageBoxIcon</span>.Information);</p>
<p style="margin: 0px;">}</p>
</div>
<p>-Wayne</p>

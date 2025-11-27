---
layout: "post"
title: "Inventor API Fundamentals 004 - Connecting to the API"
date: "2008-09-23 02:35:39"
author: "Adam Nagy"
categories:
  - "Beginning API"
  - "Brian"
original_url: "https://modthemachine.typepad.com/my_weblog/2008/09/in-the-last-posting-i-discussed-the-object-model-and-that-it-provides-a-structured-way-to-access-the-various-objects-that-mak.html "
typepad_basename: "in-the-last-posting-i-discussed-the-object-model-and-that-it-provides-a-structured-way-to-access-the-various-objects-that-mak"
typepad_status: "Publish"
---

<p>In the last posting I discussed the object model and that it provides a structured way to access the various objects that make up Inventor’s API. In order to use the object model you need to do two things; reference the library that describes Inventor’s object model and gain access to the Application object. (The Application object is the top-most object in the object model and through it you can access all the other objects.)</p>
<p>How you reference the API and access the Application object will differ slightly for the different languages. These are discussed below along with example code that demonstrates getting the Application object and calling its Caption property.</p>
<p><span style="FONT-SIZE: 19px; FONT-FAMILY: Arial Black">Inventor’s VBA</span><br />Because the VBA that’s delivered with Inventor is integrated with Inventor, it provides the easiest access to Inventor’s object model. In Inventor&#39;s VBA, the Inventor API library is already referenced and it provides an easy way of accessing the Application object by providing a global property called <em>ThisApplication</em>, which returns the Application object. All you need to display Inventor’s caption with Inventor’s VBA is the code below.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">MsgBox ThisApplication.Caption <br /></div>
<p><br /><span style="FONT-SIZE: 19px; FONT-FAMILY: Arial Black">External Application</span> <br /><em>External Application</em> refers to any application that runs outside of Inventor.&#0160; Typically these are Windows applications (.exe programs) but this also includes VBA programs written using VBA in another application (i.e. Word or Excel).&#0160; This term does not refer to Add-In applications. </p>
<p></p>
<p>How you Reference Inventor’s API varies for each of the programming environments and is discussed below for each one. The mechanics of gaining access to the Application object also varies with the different programming languages; however, for all languages there are two basic ways to connect. The first is to see if Inventor is running and get the Application object associated with it. The second is to start Inventor and get the associated Application object. Both are useful and which one to use depends on what you’re trying to accomplish. Connecting to a running instance of Inventor is probably the most common. Starting Inventor is common for batch processing types of programs.</p>
<p><strong><span style="FONT-SIZE: 16px; FONT-FAMILY: Arial">VBA / VB 6</span></strong><br /><strong>Referencing the API</strong><br />Use the “References…” command (in the Tools menu in VBA and the Project menu in VB 6) to add a reference to Inventor’s object library, as shown below.</p>
<p style="TEXT-ALIGN: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010534abd457970b-popup" onclick="window.open































(this.href,&#39;_blank&#39;,&#39;scrollbars=no,resizable=yes,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39;); return false" style="DISPLAY: inline"><img alt="References" class="at-xid-6a00e553fcbfc68834010534abd457970b " src="/assets/image_36582.jpg" title="References" /></a><br />Referencing the Inventor Object Library in VBA and VB 6.</p>
<p><strong>Connecting to a Running Instance of Inventor</strong></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Dim inventorApp As Inventor.Application<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">&#39; Attempt to get a reference to a running instance of Inventor.<br /></span>On Error Resume Next<br />Set inventorApp = GetObject(, &quot;Inventor.Application&quot;)<br />If Err Then<br />&#0160;&#0160;&#0160; MsgBox &quot;Inventor must be running.&quot;<br />&#0160;&#0160;&#0160; Exit Sub<br />End If<br />On Error GoTo 0<br /><br />MsgBox inventorApp.Caption<br /></div>
<p><br /><strong>Starting an Instance of Inventor</strong></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Dim inventorApp As Inventor.Application<br /><br />On Error Resume Next<br />Set inventorApp = CreateObject(&quot;Inventor.Application&quot;)<br />If Err Then<br />&#0160;&#0160;&#0160; MsgBox &quot;Error starting Inventor.&quot;<br />&#0160;&#0160;&#0160; Exit Sub<br />End If<br />On Error GoTo 0<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">&#39; Make Inventor visible.</span><br />inventorApp.Visible = True<br /><br />MsgBox inventorApp.Caption<br /></div>
<p><strong><span style="FONT-SIZE: 16px; FONT-FAMILY: Arial">VB.Net</span></strong><br /><strong>Referencing the API</strong><br />The .Net languages use something called an interop assembly to be able to call a COM Automation API. With Inventor 2009, Inventor installs an interop assembly (Primary Interop Assembly or PIA) for all developers to use. You reference this into your application using the “Add Reference…” command and selecting Autodesk.Inventor.Interop from the .Net tab as shown below.</p>
<p style="TEXT-ALIGN: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010534b3297c970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, 































&#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="DISPLAY: inline"><img alt="InteropReference" class="at-xid-6a00e553fcbfc68834010534b3297c970c " src="/assets/image_558419.jpg" /></a><br />Referencing the Inventor Object Library in VB.Net</p>
<p>For versions of Inventor previous to Inventor 2009 you need to select “Autodesk Inventor Object Library” from the COM tab of the Add Reference dialog. This will generate a local interop assembly for your project.</p>
<p style="TEXT-ALIGN: center"><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834010534ac3ff8970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, 































&#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="DISPLAY: inline"><img alt="ReferenceNet" class="at-xid-6a00e553fcbfc68834010534ac3ff8970b " src="/assets/image_135936.jpg" /></a><br />Referencing the Inventor Object Library in VB.Net (Inventor 2008 and earlier)</p><br />
<p><strong>Connecting to a Running Instance of Inventor</strong><br />The VBA / VB6&#0160;code above will work in VB.Net, but VB.Net also provides the <em>try catch</em> style of error handling which is usually nicer than the older On Error error handling.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Dim inventorApp As Inventor.Application<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">&#39; Attempt to get a reference to a running instance of Inventor.</span><br />Try<br />&#0160;&#0160;&#0160; inventorApp = GetObject(, &quot;Inventor.Application&quot;)<br />Catch ex As Exception<br />&#0160;&#0160;&#0160; MsgBox(&quot;Inventor must be running.&quot;)<br />&#0160;&#0160;&#0160; Exit Sub<br />End Try<br /><br />MsgBox(inventorApp.Caption)<br /></div>
<p><strong>Starting an Instance of Inventor</strong></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Dim inventorApp As Inventor.Application<br />Try<br />&#0160;&#0160;&#0160; inventorApp = CreateObject(&quot;Inventor.Application&quot;)<br />Catch ex As Exception<br />&#0160;&#0160;&#0160; MsgBox(&quot;Error starting Inventor.&quot;)<br />&#0160;&#0160;&#0160; Exit Sub<br />End Try<br /><br />inventorApp.Visible = True<br /><br />MsgBox(inventorApp.Caption)<br /></div>
<p><strong><span style="FONT-SIZE: 16px; FONT-FAMILY: Arial">C#</span></strong><br /><strong>Referencing the API</strong><br />Referencing the API in a C# application is exactly the same as for Visual Basic and the instructions above apply.&#0160; You&#39;ll need to add the following to the top of your project.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">using System.Runtime.InteropServices;</div><br />
<p><strong>Connecting to a Running Instance of Inventor</strong></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Inventor.Application inventorApp = null<br />try<br />{<br />&#0160;&#0160;&#0160;<span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// Attempt to get a reference to a running instance of Inventor.</span><br />&#0160;&#0160;&#0160;inventorApp =(Inventor.Application)Marshal.GetActiveObject(&quot;Inventor.Application&quot;);<br />}<br />catch</div>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">{<br />&#0160;&#0160;&#0160;MessageBox.Show(&quot;Unable to connect to Inventor.&quot;)<br />&#0160;&#0160;&#0160;return;<br />}<br /><br />MessageBox.Show(inventorApp.Caption);<br /></div>
<p><br /><strong>Starting an Instance of Inventor</strong></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">Inventor.Application inventorApp = null;<br /><br />try<br />{<br />&#0160;&#0160;&#0160;<span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// Start Inventor.</span><br />&#0160;&#0160;&#0160;System.Type oType = System.Type.GetTypeFromProgID(&quot;Inventor.Application&quot;);<br />&#0160;&#0160;&#0160;inventorApp = (Inventor.Application)System.Activator.CreateInstance(oType);<br /><br />&#0160;&#0160;&#0160;<span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// Make Inventor visible.</span><br />&#0160;&#0160;&#0160;inventorApp.Visible = true;<br />}<br />catch<br />{<br />&#0160;&#0160;&#0160;MessageBox.Show(&quot;Unable to start Inventor.&quot;);<br />&#0160;&#0160;&#0160;return<br />}<br /><br />MessageBox.Show(inventorApp.Caption);<br /><br /><br /></div>
<p><strong><span style="FONT-SIZE: 16px; FONT-FAMILY: Arial"></span></strong>&#0160;</p>
<p><strong><span style="FONT-SIZE: 16px; FONT-FAMILY: Arial">Visual C++</span></strong><br /><strong>Referencing the API</strong><br />Referencing the API for an unmanaged VC++ project is done by including a header file provided by Inventor’s SDK. This header file imports Inventor’s object library and includes some other useful header files.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">#include &quot;InventorUtils.h&quot;</div>
<p><strong>Connecting to a Running Instance of Inventor</strong><br />There are different flavors of VC++ (managed and unmanaged) and different ways to use Inventor’s API from VC++. The samples below represent one style.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">HRESULT Result = NOERROR;<br /><br />CLSID inventorClsid;<br />Result = CLSIDFromProgID (L&quot;Inventor.Application&quot;, &amp;inventorClsid);<br />if (FAILED(Result)) return Result;<br /><br />CComPtr<iunknown> pInventorAppUknown;<br />Result = ::GetActiveObject (inventorClsid, NULL, &amp;pInventorAppUknown);<br />if (FAILED (Result)){<br />&#0160;&#0160;&#0160;MessageBox(0, _T(&quot;Could not connect to Inventor.&quot;), _T(&quot;Error&quot;), 0);<br />&#0160;&#0160;&#0160;return Result;<br />}<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// QueryInterface for the Inventor Application object.</span><br />CComPtr<application> pInventorApp;<br />Result = pInventorAppUknown-&gt;QueryInterface(__uuidof(Application),<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;(void **) &amp;pInventorApp);<br />if (FAILED(Result)) return Result;<br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// Get and display the caption.</span><br />CComBSTR bstrCaption;<br />Result = pInventorApp-&gt;get_Caption(&amp;bstrCaption);<br />if (SUCCEEDED(Result))<br />&#0160;&#0160;&#0160;MessageBox(0, bstrCaption, _T(&quot;Inventor Caption&quot;), 0);<br /><br /></application></iunknown></div>
<p><br /><strong>Starting an Instance of Inventor</strong></p>
<div style="FONT-SIZE: 8pt; BACKGROUND: #eeeeee; COLOR: black; LINE-HEIGHT: 140%; FONT-FAMILY: courier new">HRESULT Result = NOERROR;<br /><br />CLSID inventorClsid;<br />Result = CLSIDFromProgID (L&quot;Inventor.Application&quot;, &amp;inventorClsid);<br />if (FAILED(Result)) return Result;<br /><br />CComPtr<iunknown> pInventorAppUknown;<br />Result = CoCreateInstance(inventorClsid, NULL, CLSCTX_LOCAL_SERVER,<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;__uuidof(IUnknown), (void **) &amp;pInventorAppUknown);<br />if (FAILED (Result)) {<br />&#0160;&#0160;&#0160;MessageBox(0, _T(&quot;Could not start Inventor.&quot;), _T(&quot;Error&quot;), 0);<br />&#0160;&#0160;&#0160;return Result;<br />}<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// QueryInterface for the Inventor Application object.</span><br />CComPtr<application> pInventorApp;<br />Result = pInventorAppUknown-&gt;QueryInterface(__uuidof(Application),<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;(void **) &amp;pInventorApp);<br />if (FAILED(Result)) return Result;<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// Make Inventor visible.</span><br />pInventorApp-&gt;Visible = VARIANT_TRUE;<br /><br /><span style="BACKGROUND: #eeeeee; COLOR: #0000ff">// Get and display the caption.</span><br />CComBSTR bstrCaption;<br />Result = pInventorApp-&gt;get_Caption(&amp;bstrCaption);<br />if (SUCCEEDED(Result))<br />&#0160;&#0160;&#0160;MessageBox(0, bstrCaption, _T(&quot;Inventor Caption&quot;), 0);<br /></application></iunknown></div>
<p></p>

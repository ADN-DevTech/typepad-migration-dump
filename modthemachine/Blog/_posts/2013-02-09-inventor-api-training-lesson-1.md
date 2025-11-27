---
layout: "post"
title: "Inventor API Training &ndash; Lesson 1"
date: "2013-02-09 00:57:36"
author: "Wayne Brill"
categories:
  - "Beginning API"
  - "Inventor"
  - "Training Material"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/02/inventor-api-training-lesson-1.html "
typepad_basename: "inventor-api-training-lesson-1"
typepad_status: "Publish"
---

<p>Developer Technical Services delivers Inventor API training and we have course material that we use. We thought it would be a good idea to post each section from the course. Here is the first one. (there are 21 sections)</p>
<p>If you are just getting started with the API these lessons will be good to go through after doing the <a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=16459234" target="_blank">My First Plug-in</a> training. Also there are recordings of webcasts available that cover the first 10 sections. (On the <a href="http://adndevblog.typepad.com/manufacturing/2013/05/api-webcast-archive.html" target="_blank">webcast archive</a> - spring of 2010)</p>
<p><strong>Basic concepts of the Inventor API</strong></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fc11970d-pi"><img alt="image" border="0" height="225" src="/assets/image_995169.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="301" /></a></p>
<p>In this section an overview of the Inventor API is provided and you learn about the Application object. Be sure to see the Lab instructions at the end of this section. (completed sample is available) Here is the agenda:</p>
<p><em>API COM model <br />How do I access the API? <br />&#0160; The Object model <br />&#0160; Object model tools: <br />&#0160;&#0160;&#0160; Object browser, VBA debugger <br />&#0160;&#0160;&#0160; Collection, Enumerator, Inheritance <br />&#0160; The Application Object <br />&#0160; How to access the Application Object?</em></p>
<p>&#0160;</p>
<p><strong>Inventor COM API Model:</strong></p>
<p>The Inventor API is based on COM. (Component Object Model) In this diagram we see how the Inventor COM API is between the Inventor Application and custom applications. You can see that the API can be used from many different programming languages.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fc35970d-pi"><img alt="image" border="0" height="319" src="/assets/image_996479.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="436" /></a></p>
<p>&#0160;</p>
<p><strong>How to access the Inventor API</strong>&#0160;</p>
<p>The white boxes represent components that provide the API. These are Autodesk Inventor and &quot;Apprentice Server.&quot; The blue cylinder at the bottom represents the Autodesk Inventor data you&#39;re accessing, i.e. parts, assemblies, etc. All of the yellow boxes represent programs that you write. When one box encloses another box this indicates that the enclosed box is running in the same process as the box enclosing it.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fc4d970d-pi"><img alt="image" border="0" height="334" src="/assets/image_194852.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="444" /></a></p>
<p><em>VBA: <br /></em>When deciding which method to use when programming Autodesk Inventor, there are a few advantages of using VBA to consider. First, VBA is delivered with Autodesk Inventor and does not require you to purchase an additional programming language. Second, you are able to embed programs within Autodesk Inventor documents. <br />If you have a program that is data-specific, this is a convenient way to keep the program with the data it is designed to use. Third, VBA runs in the same process as Autodesk Inventor so you gain the performance advantages of being in the same process.</p>
<p>Note: In the picture VBA has an X through it. This is there because we only recommend using VBA for quick prototyping or for small macros.</p>
<p><em>AddIn:</em> <br />Add-Ins are a special type of Autodesk Inventor program. An Add-In is able to do two things that none of the other methods of accessing the API is able to do. First, Autodesk Inventor starts the Add-In automatically whenever Autodesk Inventor is run. Second, an Add-In is able to create commands. Other than these two features, it has the same access and uses the API in the same way as programs written using any of the other API access methods. <br />You&#39;ll notice in the diagram above that Add-In is listed twice: once as a DLL and once as an EXE. With an Add-In, you have the choice of creating a DLL, which will run in the same process as Autodesk Inventor, or an EXE, which will run in a separate process. Almost all Add-Ins will be written as DLLs for increased performance benefits. The ability to have Add-Ins that are EXEs is primarily beneficial for debugging. <br />Add-Ins can be written using any language that supports the creation of ActiveX EXEs or DLLs, such as Visual C++ and .NET languages such as C# or VB.NET. Add-Ins cannot be created with VBA.</p>
<p><em>Exe:</em> <br />A standalone EXE is a program that runs on its own and connects to Autodesk Inventor. This type of program is typically used in the case where you have a program that uses Autodesk Inventor but has its own interface and doesn&#39;t require the user to interactively work with Autodesk Inventor. For example, a batch plot utility is typically a standalone EXE. The plot utility can be an EXE that runs independently of Autodesk Inventor and might monitor a database watching for new records to be added. <br />Standalone EXEs run out-of-process to Autodesk Inventor, so there is some performance penalty, but since they are not usually used for interactive processes it&#39;s rarely an issue.</p>
<p><em>Apprentice: <br /></em>Apprentice is an ActiveX server that can be used within other applications to provide access to Autodesk Inventor data. Apprentice is essentially a subset of Inventor that runs in process with other applications. Apprentice doesn&#39;t have a user interface. The only way to interact with Apprentice is through its API. Apprentice provides access to the assembly structure, B-Rep, geometry, and file properties. Most access to information through Apprentice is read-only; (a couple of exceptions to this are document properties and file references). <br />Apprentice Server is also available at no cost since it is delivered as part of <a href="http://usa.autodesk.com/adsk/servlet/pc/index?id=10535296&amp;siteID=123112" target="_blank">Autodesk Inventor View</a>, which is available on the public Autodesk website.</p>
<p><strong>Inventor SDK</strong></p>
<p>The Inventor SDK (Software Development Kit) Contains C++ header files, samples of C++, VB.NET and C#. The SDK is available for install when Inventor is installed. To install the SDK run the DeveloperTools.msi and UserTools.msi. See the SDK_Readme.htm for more information. Here are the location of the SDK install files:</p>
<p>Windows XP:&#0160; &lt;Inventor install folder&gt;\SDK</p>
<p>Windows Vista:&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160; C:\Users\Public\Documents\Autodesk\Inventor &lt;version&gt;\SDK Windows 7:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <br />&#0160;&#0160;&#0160;&#0160; C:\Users\Public\Documents\Autodesk\Inventor &lt;version&gt;\SDK</p>
<p><strong>API help reference</strong> <br />The API help will be very useful. Here are the locations of the .chm file:</p>
<p>Before 2013: &lt;Inventor install folder *&gt;\Help_Lite\admapi_*_0.chm</p>
<p>2013: &lt;Inventor install folder *&gt;\Local Help\admapi_*_0.chm <br />Where * is the version number</p>
<p><strong>API Objects and the Object Model</strong></p>
<p>In a COM Automation API the functionality is exposed as objects, where each object corresponds to something within the application. Each object supports various methods, properties, and possibly events. The objects are accessed through the object model. The top most object is the Application object</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fc6b970d-pi"><img alt="image" border="0" height="157" src="/assets/image_661380.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="446" /></a></p>
<p><strong>Basics of Object Oriented Programming</strong></p>
<p>We can use a chair and a chair order form to help explain object oriented programming. (Class, Object, Property, Method, Event)</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fc80970d-pi"><img alt="image" border="0" height="244" src="/assets/image_617378.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="212" /></a></p>
<p>An object represents a logical object.&#0160; A finished physical chair would be an example of an object. A <em>property</em> would be an attribute of the chair such as the Style, Color, and Size. A <em>method </em>is an action performed on the chair; move, fold, throw away. An <em>event</em> is a notification sent when something happens to the chair. A class is a template of an object. In this example you could think of the class as an order form for a chair.</p>
<p><strong>Inventor Object Model Example</strong></p>
<p>Inventor’s objects are accessed through the Object Model</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c36b5a6b8970b-pi"><img alt="image" border="0" height="246" src="/assets/image_83269.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="384" /></a></p>
<p>In this VB.NET example below notice how the hierarchy of the Object Model is used:</p>
<p>Application&gt;PartDocument&gt;PartComponentDefinition&gt;Features&gt;ExtrudeFeatures</p>
<p>This code uses properties of Objects that are shortcuts such as ActiveDocument. (m_inventorApp is an Application Object)</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> GetExtrudeFeature()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPartDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPartDoc = m_inventorApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oExtrude <span style="color: blue;">As</span> <span style="color: #2b91af;">ExtrudeFeature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oExtrude = oPartDoc.ComponentDefinition.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Features.ExtrudeFeatures(<span style="color: #a31515;">&quot;Extrusion1&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; MsgBox(<span style="color: #a31515;">&quot;Extrusion &quot;</span> &amp; oExtrude.Name &amp;</p>
<p style="margin: 0px;">&#0160;<span style="color: #a31515;">&quot; is suppressed: &quot;</span> &amp; oExtrude.Suppressed)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>&#0160;</p>
<p><strong>&#0160;</strong></p>
<p><strong>Object Model Tools – Object Browser</strong> <br />The Object Browser provides a user-interface to the contents of the type library. (Object Model)&#0160; <br />In VBA the Object Browser is accessed using the F2 function key. It is also the Object Browser command in the View menu. In .NET&#0160; using Ctrl+Alt+J, or the Object Browser command in the View menu will launch this utility. <br /><strong>&#0160;</strong></p>
<p><strong>Object Model Tools - VBA Debugger</strong></p>
<p>The VBA debugger provides a “live” view of the object model. <br />You can see the values of an objects properties at runtime. <br />The VBA debugger provides more information that .Net debugger. To use the debugger add a Watch to a variable and place a break point.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fcb0970d-pi"><img alt="image" border="0" height="332" src="/assets/image_633403.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="440" /></a></p>
<p><strong>Object Model Tools - .NET Debugger</strong></p>
<p>Provides a “live” view of the object model. <br />Shows the values of an object properties. <br />Shows the contents of collections.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fcdd970d-pi"><img alt="image" border="0" height="222" src="/assets/image_430088.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="486" /></a></p>
<p>&#0160;</p>
<p><strong>Collection Objects</strong></p>
<p>A collection is a special object that provides access to a list of objects. The count property of a collection returns number of objects that are in the collection. The Item property returns a specific object in the collection. You can specify the index of the object within the collection and the first item in the collection will be at an index of 1. This is true for all collections within Inventor API. (You may have experience with some other API that uses zero for the index of the the first item in a collection). In some cases you can specify the name of the object within the collection. (use a string instead of an number). In this diagram the collections are Documents, PartFeatures and ExtrudeFeatures.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c36b5a718970b-pi"><img alt="image" border="0" height="222" src="/assets/image_472473.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="353" /></a></p>
<p><strong>Collection vs. Enumerator Objects</strong></p>
<p>Some collections support the functionality to create new objects. <br />Enumerators are also collections but only support the Count and Item properties.</p>
<p><strong>Iterating Through a Collection</strong></p>
<p>This VB.NET example Iterates through a collection using Count and Item:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oExtrude <span style="color: blue;">As</span> <span style="color: #2b91af;">ExtrudeFeature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> i <span style="color: blue;">As</span> <span style="color: blue;">Long</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> i = 1 <span style="color: blue;">To</span> oExtrudeFeatures.Count</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(oExtrudeFeatures.Item(i).Name)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
</div>
<p>This example uses a For Each statement (more efficient):</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Dim</span> oExtrude <span style="color: blue;">As</span> <span style="color: #2b91af;">ExtrudeFeature</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">For</span> <span style="color: blue;">Each</span> oExtrude <span style="color: blue;">In</span> oExtrudeFeatures</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(oExtrude.Name)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Next</span></p>
</div>
<p><br /><strong>Derived Objects</strong> <br />This is similar to animal taxonomy or classification. All items under a specific classification share common traits. In this chart you can see that Cat, Dog and Human are all classified as Mammals. This means that they share things in common. In the same way AssemblyDocument, PartDocument, DrawingDocument and PresentationDocument have things in common, such as a DisplayName. (a property of any Document)</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d40e43765970c-pi"><img alt="image" border="0" height="401" src="/assets/image_383532.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="488" /></a></p>
<p><strong>Derived Objects – Example</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> SaveDoc()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get ActiveDocument, could be Part, </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Assembly or Drawing</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = m_inventorApp.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Call the Save method on the </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;generic() &#39;Document&#39; object</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc.Save()</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p>&#0160;</p>
<p><strong>&#0160;</strong></p>
<p><strong>The Application Object</strong> <br />This top level object represents the Inventor Application and provides access to the other API objects. Properties, methods and events of the Application object support general functionality not specific to a document.</p>
<p><strong>Application Window</strong> <br />The Application object provides access to the main window through various methods and properties such as:&#0160; <br />Caption,Left, Top, Width, Height, GetAppFrameExtents, Move <br />WindowState, Visible, MainFrameHwnd</p>
<p><strong>Utility Objects</strong> <br />SoftwareVersion – Provides information about the version of Inventor. <br />ChangeManager, CommandManager, FileManager, HelpManager, TransactionManager, MeasureTools, UserInterfaceManager – Provide access to functions related to a particular area. <br />TransientGeometry – Temporary geometry objects. <br />TransientObjects – Temporary utility objects <br />TransientBrep – Temporary Boundary Representation objects</p>
<p><strong>&#0160;</strong></p>
<p><strong>Shortcuts to “Active” Objects</strong> <br />Properties that provide direct access to various “Active” objects. <br />ActiveColorScheme, ActiveDocument, ActiveView <br />ActiveEnvironment, <br />ActiveEditObject – Returns the object currently being edited.&#0160; This can be a document that’s been opened, a document that’s been in-place edited, a sketch, a sheet, and a flat pattern. <br />ActiveEditDocument – Returns the document currently be edited</p>
<p><strong>Application Options <br /></strong>Provides access to objects that expose the various application options. <br /><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee858fd1c970d-pi"><img alt="image" border="0" height="434" src="/assets/image_221742.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="405" /></a></p>
<p><strong>Application Level Objects</strong></p>
<p>Documents – All currently open documents, including those that are referenced by other documents. <br />ApplicationAddIns – The available Add-Ins. <br />VBAProjects – VBA related objects.</p>
<p><strong>Events</strong> <br />Provides access to several different sets of events: <br />ApplicationEvents, AssemblyEvents, <br />FileAccessEvents, FileUIEvents <br />ModelingEvents, RepresentationEvents <br />SketchEvents, StyleEvents</p>
<p><strong>Miscellaneous</strong> <br />SilentOperation – Causes all dialogs to be suppressed and take the default behavior.&#0160; Useful for batch processing operations where warning dialogs would normally be displayed as files are opened, processed, and closed.</p>
<p>LanguageName, Locale – Language information.</p>
<p>MRUEnabled, MRUDisplay – Controls display of most recently used file list at the bottom of the File menu.</p>
<p>Ready – Indicates if Inventor is fully initialized.</p>
<p>StatusBarText – Text shown in the status bar.</p>
<p><strong>How to access the Application Object? <br /></strong>In Inventor’s VBA you can use the ThisApplication shortcut. <br />From&#0160; an AddIn: an application object is passed to the AddIn when the AddIn is loaded by Inventor. In a Standalone exe you can use the GetObject or CreateObject methods as in this VB.NET example:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Try</span> <span style="color: green;">&#39; Try to get an active instance of Inventor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.Runtime.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteropServices.<span style="color: #2b91af;">Marshal</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetActiveObject(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span> <span style="color: green;">&#39; If not active, create a new </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; Inventor session</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> inventorAppType <span style="color: blue;">As</span> <span style="color: #2b91af;">Type</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Type</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; GetTypeFromProgID(<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Activator</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateInstance(inventorAppType)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;Must be set visible explicitly</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp.Visible = <span style="color: blue;">True</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Catch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">MessageBox</span>.Show _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Error: couldn&#39;t create Inventor instance&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
</div>
<p>&#0160; <br /><strong>How to access the Application Object? – C# <br /></strong>Similar to VB.NET, access from outside Inventor using either the GetObject or CreateObject methods:</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">try</span> <span style="color: green;">//Try to get an active instance of Inventor</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.Runtime.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; InteropServices.<span style="color: #2b91af;">Marshal</span>.GetActiveObject</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">as</span> Inventor.<span style="color: #2b91af;">Application</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">catch</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Type</span> inventorAppType =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.<span style="color: #2b91af;">Type</span>.GetTypeFromProgID</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Inventor.Application&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp = System.<span style="color: #2b91af;">Activator</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateInstance(inventorAppType)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">as</span> Inventor.<span style="color: #2b91af;">Application</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">//Must be set visible explicitly</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m_inventorApp.Visible = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">catch</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">MessageBox</span>.Show</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;Error: couldn&#39;t create Inventor instance&quot;</span>);</p>
<p style="margin: 0px;">}</p>
</div>
<p>&#0160;</p>
<p><strong>Lab: Access the Application Object</strong> <br />Create an external Exe that tries to access the Inventor Application object at startup. If a running instance of Inventor is not found, then create a new instance using “CreateInstance” <br />Create some controls in order to:&#0160; <br />Ask user for Height and Width values and set the ActiveView to the entered values. Ask user for the Application caption and set it.</p>
<p>Here is a completed Lab if you need an example to help find something that is not working right.</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a00e553fcbfc68834017ee8590207970d"><a href="http://modthemachine.typepad.com/files/inventor_training_module_01_samples.zip">Download Inventor_Training_Module_01_Samples</a></span></p>
<p>-Wayne</p>

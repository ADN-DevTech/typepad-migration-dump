---
layout: "post"
title: "Inventor API Training &ndash; Lesson 12"
date: "2013-03-07 01:51:49"
author: "Wayne Brill"
categories:
  - "Inventor"
  - "Training Material"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/03/inventor-api-training-lesson-12.html "
typepad_basename: "inventor-api-training-lesson-12"
typepad_status: "Publish"
---

<p>Here is section twelve of the Inventor API training where you see how Apprentice is used to access Inventor Data without having to use Inventor. You also learn about the Inventor viewer control. This is the fourth post with the Inventor API training material. (<a href="http://modthemachine.typepad.com/my_weblog/2013/02/inventor-api-training-lesson-1.html">Section one is here</a>).</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c375e7135970b-pi"><img alt="image" border="0" height="292" src="/assets/image_747907.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="391" /></a></p>
<p>Here is the agenda for this section. Be sure to see the Lab instructions at the end of this section. (completed samples in VB.NET and C# are available)</p>
<p><em>Apprentice Definition <br />Apprentice Functionalities <br />Apprentice Guidelines <br />Apprentice vs. Inventor: Differences <br />Saving Files with Apprentice</em></p>
<p><em>Inventor View Control</em></p>
<p><strong>Apprentice Definition</strong></p>
<p>This diagram shows where the API for Apprentice fits in . You use a stand alone client application, reference the Inventor API and use the apprentice classes to access Inventor data.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee90187c2970d-pi"><img alt="image" border="0" height="297" src="/assets/image_458594.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="392" /></a></p>
<p>Apprentice provides a subset of the Inventor API and is&#0160; <br />free as it is installed as part of <a href="http://usa.autodesk.com/adsk/servlet/pc/item?siteID=123112&amp;id=18585712" target="_blank">Inventor View</a>. A simple way to think of Apprentice is that it&#39;s a smaller version of Autodesk Inventor that does not have a user interface. Because there is not a user interface the only way to access its functionality is by using its API. Apprentice can be very efficient for certain applications because it&#39;s smaller than the complete Autodesk Inventor application and because it runs in the same process as your application. </p>
<p><strong>Apprentice Functionalities</strong></p>
<p>Apprentice provides <em>read-only</em> access to the following:</p>
<blockquote>
<p>Assembly structure <br />B-Rep <br />Drawing sheets and views (limited access) <br />iParts <br />iAssemblies <br />BOM</p>
</blockquote>
<p>Apprentice provides <em>read / write</em> access to the following:</p>
<blockquote>
<p>iProperties <br />attributes <br />file references</p>
<p>&#0160;</p>
</blockquote>
<p><strong>Apprentice – Guidelines</strong></p>
<p>Apprentice should NOT be used in-process with Inventor such as in an Add-In or through Inventor’s VBA. (If the Add-In is an exe type then it is ok).</p>
<p>In past releases a type library specifically for Apprentice was delivered with both Autodesk Inventor and Apprentice. This type library contained the limited set of objects supported only by Apprentice. Now the Autodesk Inventor type library contains all of the Apprentice functionality and is used to access Apprentice. (You add a reference to Inventor.Interop for an Apprentice project). A version of the old Apprentice type library is still supplied for legacy reasons, but this will likely be discontinued in future releases.</p>
<p><strong>Apprentice vs. Inventor: differences</strong></p>
<p>Apprentice is instantiated using a “new ApprenticeServerComponent”. (Instantiating Inventor would be done using something like CreateObject)&#0160;</p>
<p>The ApprenticeServerComponent supports a few methods and properties that are unique to Apprentice.</p>
<blockquote>
<p>Open and Close methods, which are used to open and close documents within Apprentice</p>
</blockquote>
<blockquote>
<p>DisplayAffinity, which is used to optimize the behavior of Apprentice for viewer applications</p>
<p>MinimizeFileSize, which compresses files by removing versions</p>
<p>FileSaveAs, used to save files</p>
</blockquote>
<p>The document objects used within Apprentice are different from the document objects used in Autodesk Inventor. (Apprentice does not have a Documents collection) In Inventor there are the PartDocument, AssemblyDocument, DrawingDocument, and PresentationDocument objects. In Apprentice, the <em>ApprenticeServerDocument</em> object represents the part, assembly, and presentation documents and the <em>ApprenticeServerDrawingDocument </em>represents the drawing document.</p>
<p>Note: Apprentice cannot save a file from a previous version. You would need to migrate the file prior to saving it with Apprentice. (To migrate a file, open it with Inventor, save it and close it).</p>
<p><strong><em>VB.NET Example that uses Apprentice to open an ipt file</em></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> ApprenticeSample()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create Apprentice.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oApprentice <span style="color: blue;">As</span> <span style="color: #2b91af;">ApprenticeServerComponent</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oApprentice = <span style="color: blue;">New</span> <span style="color: #2b91af;">ApprenticeServerComponent</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Open a document.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">ApprenticeServerDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = oApprentice.Open(<span style="color: #a31515;">&quot;C:\Temp\Part1.ipt&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; MsgBox(<span style="color: #a31515;">&quot;Opened: &quot;</span> &amp; oDoc.DisplayName)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>Saving Files with Apprentice</strong></p>
<p>Use <em>FlushToFile</em> if the code is only modifying iProperties. This is&#0160;&#0160; more efficient because the document is not written back.</p>
<p><strong><em>VB.NET Example that uses FlushToFile after changing iProps</em></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> SetProperty(<span style="color: blue;">ByVal</span> author <span style="color: blue;">As</span> <span style="color: blue;">String</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oApprenticeDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">ApprenticeServerDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oApprenticeDoc = mApprenticeServer.Open _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;c:\Temp\MyPart.ipt&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get &quot;Inventor Summary Information&quot; PropertySet</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oPropertySet <span style="color: blue;">As</span> <span style="color: #2b91af;">PropertySet</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oPropertySet = oApprenticeDoc.PropertySets _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;{F29F85E0-4FF9-1068-AB91-08002B27B3D9}&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;Get Author property</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oProperty <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Property</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oPropertySet.Item(<span style="color: #a31515;">&quot;Author&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oProperty.Value = author</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oApprenticeDoc.PropertySets.FlushToFile()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oApprenticeDoc.Close()</p>
<p style="margin: 0px;"><span style="color: #0000ff;">End Sub</span></p>
</div>
<p><strong>VB.NET Example that uses the FileSaveAs to make a copy</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> saveToNewFile()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create an instance of Apprentice.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> apprentice <span style="color: blue;">As</span> <span style="color: blue;">New</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApprenticeServerComponent</span>()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Open a part</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> appDoc <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApprenticeServerDocument</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; = apprentice.Open _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;C:\Temp\Part1.ipt&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Save the file to a new name</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> myFileSaveAs <span style="color: blue;">As</span> <span style="color: #2b91af;">FileSaveAs</span> =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; apprentice.FileSaveAs</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myFileSaveAs.AddFileToSave _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (appDoc, <span style="color: #a31515;">&quot;C:\Temp\Part1_update.ipt&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myFileSaveAs.ExecuteSaveCopyAs()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; appDoc.Close()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><em>Transient Camera</em></p>
<p>You can create image files by getting the camera from the <em>TransientObjects</em> of the <em>ApprenticeServerComponent </em>CreateCamera method.</p>
<p><strong>VB.NET example that opens a part and creates a jpg file</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> saveMyImage()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create an instance of Apprentice.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> apprentice <span style="color: blue;">As</span> <span style="color: blue;">New</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApprenticeServerComponent</span>()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Open the specified file.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> appDoc <span style="color: blue;">As</span>&#0160; _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ApprenticeServerDocument</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; = apprentice.Open(<span style="color: #a31515;">&quot;C:\Temp\Part1.ipt&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create a camera.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> cam <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Camera</span> =</p>
<p style="margin: 0px;">&#0160; apprentice.TransientObjects.CreateCamera</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Associate the camera with the part&#39;s</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; component definition.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; cam.SceneObject =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; appDoc.ComponentDefinition</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Set the camera to the desired </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; orientation and position.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; cam.ViewOrientationType =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Inventor.<span style="color: #2b91af;">ViewOrientationTypeEnum</span>.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kIsoTopRightViewOrientation</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; cam.Fit()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; cam.ApplyWithoutTransition()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;cam.Apply()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oTOs <span style="color: blue;">As</span> <span style="color: #2b91af;">TransientObjects</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oTOs = apprentice.TransientObjects</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oTopColor <span style="color: blue;">As</span> <span style="color: #2b91af;">Color</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oTopColor =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTOs.CreateColor(255, 0, 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oBottomColor <span style="color: blue;">As</span> <span style="color: #2b91af;">Color</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oBottomColor =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oTOs.CreateColor(255, 255, 255)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Create an jpg file</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; cam.SaveAsBitmap(<span style="color: #a31515;">&quot;C:\Temp\Part1.jpg&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 800, 600, oTopColor, oBottomColor)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>Inventor View Control</strong></p>
<p>You can use a control that is installed with Inventor View to view Inventor files in your own application. The control is named InventorViewCtrl.ocx. To add it to your project in Visual Studio right click on the Toolbox and select “Choose&#0160; Items…” On the COM Components tab select it. Once it is in the Toolbox select it and place it on your form. <a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee90187e5970d-pi"><img alt="image" border="0" height="201" src="/assets/image_163349.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="463" /></a></p>
<p>&#0160;</p>
<p>Note: To work with Inventor View control of 2013 use .NET framework 3.5</p>
<p>Before Inventor 2012, InventorViewCtrl.ocx was registered automatically with product. Starting from 2012, the registry-free was introduced. So plug-in developers have to link the corresponding manifest files to their application if they want to use the OCX.</p>
<p>Two options: <br />1. Use mt.exe to generate manifest file by yourself and link the manifest file to your application. <br />2. Use old style. But you have to register InventorViewCtrl.ocx manually by running “Regsvr32 InventorViewCtrl.ocx”.</p>
<p>See the post here for more information about this:</p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2012/05/inventor-view-control-is-not-registered-on-inventor-2012.html" title="http://adndevblog.typepad.com/manufacturing/2012/05/inventor-view-control-is-not-registered-on-inventor-2012.html">http://adndevblog.typepad.com/manufacturing/2012/05/inventor-view-control-is-not-registered-on-inventor-2012.html</a></p>
<p><strong>Lab work with Apprentice API</strong></p>
<p>Create a Windows Forms application that has two buttons a label and a TextBox. When one of the buttons is clicked, provide the user with a way to select a part file (ipt). Once the file is selected create a jpg from the file. When the other button is selected open a file and update the Author iProperty with the value in the TextBox.&#0160;</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d418db0e9970c-pi"><img alt="image" border="0" height="304" src="/assets/image_35307.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="299" /></a></p>
<p><strong>Lab – use the view control</strong></p>
<p>Create a Windows Forms application that has the viewer control and a button. When the user clicks the button display the file in the viewer control.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee901881f970d-pi"><img alt="image" border="0" height="310" src="/assets/image_176868.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="image" width="306" /></a></p>
<p>Here are examples of the completed Labs. (VB.NET and C#) The zip also contains the ppt we use for the training.&#0160;</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a00e553fcbfc68834017ee9018cb5970d"><a href="http://modthemachine.typepad.com/files/inventor_training_module_12_samples.zip">Download Inventor_Training_Module_12_Samples</a></span></p>
<p>-Wayne</p>

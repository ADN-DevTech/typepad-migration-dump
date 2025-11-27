---
layout: "post"
title: "Inventor API Training &ndash; Lesson 2"
date: "2013-02-25 22:56:53"
author: "Wayne Brill"
categories:
  - "Beginning API"
  - "Inventor"
  - "Training Material"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2013/02/inventor-api-training-lesson-2.html "
typepad_basename: "inventor-api-training-lesson-2"
typepad_status: "Publish"
---

<p>Here is section two of the Inventor API training where you see how the API is used to create and access Inventor documents. This section also covers iProperties, working with units, and parameters. This is the third post with the Inventor API training material. (<a href="http://modthemachine.typepad.com/my_weblog/2013/02/inventor-api-training-lesson-1.html">Section one is here</a>).&#0160;</p>
<p><strong>Common Document Functionalities</strong></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d41470ae6970c-pi"><img alt="image" border="0" height="287" src="/assets/image_323048.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="383" /></a></p>
<p>Here is the agenda for this section. Be sure to see the Lab instructions at the end of this section. (completed samples in VB.NET and C# are available)</p>
<p><em>Document Types <br />Working with Documents <br />Document Settings <br />iProperties <br />Units of Measure <br />Parameters <br /></em></p>
<p><strong>Document Types</strong></p>
<p>Inventor has unique document types for different types of data.</p>
<p>Part Documents (*.ipt) <br />Assembly Documents (*.iam) <br />Drawing Documents (*.idw) <br />Presentation Documents (*.ipn)</p>
<p>The API represents each document type using a different type of object for each type of document.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cf21970b-pi"><img alt="image" border="0" height="277" src="/assets/image_411704.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="315" /></a></p>
<p>&#0160;</p>
<p><strong><em>Accessing Documents</em></strong></p>
<p><em>Documents.Add()</em> </p>
<p>To create a new document use the Add method of the Documents collection. This method takes a <em>DocumentTypeEnum</em> which determines which type of document to create. (first argument) You can have a file that you can use as a template. To use that file, you provide a string that is a path to the file. (second argument). The third argument of the Add method is a boolean that determines if the file is opened visible or not. The GetTemplateFile method of the FileManager object allows you to use the default template and change settings for the file that is being created. (You can use this for the second argument of the Add method)</p>
<p><em>Documents.Open()</em> </p>
<p>The Open method of the documents collection will open existing documents. The first argument is a string with the file path of the document and the second is a boolean that controls the visibility.</p>
<p><em>Documents.Item()</em> </p>
<p>You use the Item property to access currently open documents. The ActiveDocument property of the Application will get you the current document.&#0160;</p>
<p><strong><em>Opening and Creating Documents VB.NET examples</em></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">&#39;Opens an existing document.assume part1.ipt exists</span></p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> OpenDoc()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = _InvApplication.Documents.Open _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;C:\Temp\Part1.ipt&quot;</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">&#39;Creates a new document using a specified template.</span></p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> CreateDoc()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = _InvApplication.Documents.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; _InvApplication.FileManager.GetTemplateFile _</p>
<p style="margin: 0px;">&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject), <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">&#39;Creates a new document using internally </span></p>
<p style="margin: 0px;"><span style="color: green;">&#39;defined template. (Can be done in the UI by </span></p>
<p style="margin: 0px;"><span style="color: green;">&#39;using Ctrl-Shift when creating new document.)</span></p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> CreateDoc2()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = _InvApplication.Documents.Add _</p>
<p style="margin: 0px;">(<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject, , <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong><em>&#0160;</em></strong></p>
<p><strong><em>Saving Documents</em></strong></p>
<p>The first time a new document is saved you should use the SaveAs method with the SaveCopyAs flag set to False.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;<span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">PartDocument</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = _InvApplication.Documents.Add _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">DocumentTypeEnum</span>.kPartDocumentObject)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc.SaveAs(<span style="color: #a31515;">&quot;C:\Temp\SaveTest.ipt&quot;</span>, <span style="color: blue;"><strong>False</strong></span>)</p>
</div>
<p>For documents that have been saved to disk you can use the Save method or the SaveAs with the SaveCopyAs flag set to True to create a copy. An error will occur if the file name parameter is the same as the open document. (keep in mind that the method will overwrite existing files).</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc.Save()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc.SaveAs(<span style="color: #a31515;">&quot;C:\Temp\SaveTest2.ipt&quot;</span>, <span style="color: blue;"><strong>True</strong></span>)</p>
</div>
<p>You can use the <em>FileSaveCounter</em> property of the document to determine if the document has been saved.</p>
<p><strong><em>Closing Documents</em></strong></p>
<p>To close a document call the Close method. You can use the SkipSave parameter to close it without saving.</p>
<p><em>Document.Close([SkipSave As Boolean = False])</em></p>
<p>Using the SkipSave argument you can bypass the dialog asking to save the changes and force the document to close without saving.&#0160; This is useful in cases where you use a file as a template by opening the file, modifying it in some way, and using SaveCopyAs to save it as a new file. Using this you can close the original without saving and without forcing the user to interact with a dialog.</p>
<p>The API has a way to close all documents. Using this method will not save any changes to documents.</p>
<p><em>Documents.CloseAll([UnreferencedOnly As Boolean = False])</em></p>
<p><strong><em>Document Settings</em></strong></p>
<p>Document settings are exposed from various objects obtained from the document.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf056970d-pi"><img alt="image" border="0" height="296" src="/assets/image_179120.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="412" /></a></p>
<p>In this excerpt from the object model diagram you can see the objects that will allow you to get and update document settings.&#0160; Notice that these objects are accessed by using properties of the document.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cf3a970b-pi"><img alt="image" border="0" height="92" src="/assets/image_38812.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="470" /></a></p>
<p><strong>iProperties</strong></p>
<p><strong><em>iProperties in the User Interface</em></strong></p>
<p>iProperties are used to associate information with a document. There are a predefined set of properties are available through the iProperties dialog. End-users can create additional properties using the “Custom” tab of the iProperties dialog. These properties are supported by both Inventor and Apprentice.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf06c970d-pi"><img alt="image" border="0" height="324" src="/assets/image_594331.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="319" /></a></p>
<p><strong><em>iProperties – Property Sets</em></strong></p>
<p>The PropertySets object acts as the container for all of the properties.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cf5f970b-pi"><img alt="image" border="0" height="320" src="/assets/image_38568.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="335" /></a></p>
<p>The PropertySets object provides access to the individual PropertySet objects using the Item property.</p>
<p><strong><em>iProperties – Property Set</em></strong></p>
<p>The PropertySet object contains a group of properties.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cf72970b-pi"><img alt="image" border="0" height="358" src="/assets/image_646105.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="199" /></a></p>
<p>Most PropertySet objects roughly correspond to the tabs in the iProperties dialog. PropertySet objects are identified by the following: <br />InternalName (consistent) <br />Name (consistent) <br />DisplayName (may change)</p>
<p><strong><em>iProperties - Property</em></strong></p>
<p>Properties are named values and they are identified by: <br />ID (consistent) <br />Name (consistent) <br />DisplayName (may change)</p>
<p><strong><em>Property values</em>&#0160;</strong></p>
<p>Property values are stored internally as Variants. The following types are supported: Integer, Long, Double, String, Date, and Boolean. (With the exception of the thumbnail image which is IPictureDisp.)</p>
<p>PropId’s are defined in the various property related enums.</p>
<p><strong><em>&#0160;</em></strong></p>
<p><strong><em>iProperty Names</em></strong> <br /><span style="color: #0000ff;">Name</span> and <span style="color: #ffc000;">Internal Name</span> of Inventor defined property sets:</p>
<blockquote>
<p><span style="color: #0000ff;">Inventor Summary Information</span></p>
</blockquote>
<p><span style="color: #ffc000;">{F29F85E0-4FF9-1068-AB91-08002B27B3D9}</span></p>
<blockquote>
<p><span style="color: #0000ff;">Inventor Document Summary Information</span></p>
</blockquote>
<p><span style="color: #ffc000;">{D5CDD502-2E9C-101B-9397-08002B2CF9AE}</span></p>
<blockquote>
<p><span style="color: #0000ff;">Design Tracking Properties</span></p>
</blockquote>
<p><span style="color: #ffc000;">{32853F0F-3444-11D1-9E93-0060B03C1CA6}</span></p>
<blockquote>
<p><span style="color: #0000ff;">Inventor User Defined Properties</span></p>
</blockquote>
<p><span style="color: #ffc000;">{D5CDD505-2E9C-101B-9397-08002B2CF9AE}</span></p>
<p>InternalNames can be found using the Object Browser and in SDK\Include\PropFMTIDs.h</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d41470b52970c-pi"><img alt="image" border="0" height="316" src="/assets/image_227557.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="402" /></a></p>
<p><strong>VB.NET Example of changing an iProperty</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: green;">&#39; Access &quot;Design Tracking Properties&quot; </span></p>
<p style="margin: 0px;"><span style="color: green;">&#39; &quot;Designer&quot; and change its value</span></p>
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> iPropAccess()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDoc <span style="color: blue;">As</span> <span style="color: #2b91af;">Document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDoc = _InvApplication.ActiveDocument</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Access a particular property set.&#0160; </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Design tracking property set.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDTProps <span style="color: blue;">As</span> <span style="color: #2b91af;">PropertySet</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDTProps = oDoc.PropertySets.Item _</p>
<p style="margin: 0px;">(<span style="color: #a31515;">&quot;{32853F0F-3444-11d1-9E93-0060B03C1CA6}&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Access the same property set using</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the display name or name. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; DisplayName is not dependable </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; because it can be localized, so </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;the internal name or name is preferred.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDTProps = oDoc.PropertySets.Item _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Design Tracking Properties&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get a specific property, in this case</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the designer property.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oDesignerProp <span style="color: blue;">As</span> Inventor.<span style="color: #2b91af;">Property</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDesignerProp = oDTProps.ItemByPropId( _</p>
<p style="margin: 0px;">&#0160;<span style="color: #2b91af;">PropertiesForDesignTrackingPropertiesEnum</span> _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .kDesignerDesignTrackingProperties)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; You can also use the name or display name</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; the display name has the problem that </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; it can be changed.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDesignerProp = oDTProps.Item(<span style="color: #a31515;">&quot;Designer&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Show the display name and value.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Debug</span>.Print(oDesignerProp.DisplayName _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp; <span style="color: #a31515;">&quot; = &quot;</span> &amp; oDesignerProp.Value)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Change the designer name.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oDesignerProp.Value = <span style="color: #a31515;">&quot;Bill &amp; Ted&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong><em>iProperties - Creation</em></strong></p>
<p>To create new property set use the Add method of the PropertySets collection. (Name and InternalName must be unique with respect to other property sets in the document).</p>
<p><em>PropertySets.Add(Name As String, [InternalName]) As PropertySet <br /></em></p>
<p>To create and add new properties to the PropertySet use the Add method. Keep in mind that properties cannot be added to the predefined sets, except for the custom property set. Also the&#0160; <br />name and PropId must be unique with respect to other properties in the property set. The value type can be most Variant types except arrays and objects.</p>
<p><em>PropertySet.Add(PropValue, [Name], [PropId]) As Property <br /></em></p>
<p>Note: PropertySets and Properties can be created as hidden by using a name that begins with an underscore.&#0160; These will not be returned by indexing through a collection.&#0160; They can only be retrieved by asking for them by name.</p>
<p><strong>Units of Measure</strong></p>
<p>When you use Inventor manually there is a setting that controls the units that are used. When using the API numbers are always in the same internal units and your code needs to convert between units to get the correct behavior. (unless the document settings are using the same internal units).</p>
<p>All Inventor documents use these internal units. <br />Length: Centimeters <br />Angle: Radians <br />Time: Second <br />Mass: Kilogram</p>
<p>The units specified by the end-user in the Document Settings dialog are used to convert internal units to/from the units the end-user wants to use.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf0a8970d-pi"><img alt="image" border="0" height="294" src="/assets/image_388366.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="390" /></a></p>
<p><strong><em>UnitsOfMeasure object</em></strong></p>
<p>The<em> UnitsOfMeasure</em> object allows you to work with different units. It provides utilities to help with unit handling, primarily the conversion between strings and values. If you are working with numbers you will most likely need to use UnitsOfMeasure to get the results you expect.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cf98970b-pi"><img alt="image" border="0" height="74" src="/assets/image_94393.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="359" /></a></p>
<p>&#0160;</p>
<p>A unique UnitsOfMeasure object can be obtained from each document. The UnitsOfMeasure object obtained from a document can be used for unit conversion and also provides a way to control unit settings for the document. The UnitsOfMeasure obtained from the Application object can be used for unit conversion.</p>
<p><strong><em>Units of Measure - Unit Types <br /></em></strong>Whenever a unit type is specified within the API, it can be defined in two different ways:</p>
<p>1. As a value from UnitsTypeEnum with a specific unit type such as:</p>
<p><em>kInchLengthUnits, kMillimeterLengthUnits, kDegreeAngleUnits</em>.</p>
<p>The current default type specified by the end-user: <em>kDefaultDisplayLengthUnits, kDefaultDisplayAngleUnits</em>. <br />The internal base units: kDatabaseLengthUnits, kDatabaseAngleUnits.</p>
<p>2. As a string, i.e. “in”, “mm mm mm”, “m ^ 3”, “m /(s s)”</p>
<p><strong><em>Units of Measure – Internal Units</em></strong></p>
<p>Internally, Inventor uses a consistent set of units regardless of what the user has specified as the document default and the precision is always double-precision floating point,</p>
<p>Internal units used by Inventor for the various types of units</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf0c2970d-pi"><img alt="image" border="0" height="221" src="/assets/image_990284.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="309" /></a></p>
<p><strong><em>VB.NET - UnitsOfMeasure to get valid input from the user</em></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> m_oUOM <span style="color: blue;">As</span> <span style="color: #2b91af;">UnitsOfMeasure</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> UserForm_Initialize()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; get the UnitsOfMeasure of the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; current document</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; m_oUOM = _InvApplication.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ActiveDocument.UnitsOfMeasure</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> TextBox1_Change()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Check if the input string defines </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;a valid length.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> <span style="color: blue;">Not</span> m_oUOM.IsExpressionValid _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (TextBox1.Text, _</p>
<p style="margin: 0px;"><span style="color: #2b91af;">&#0160;&#0160; UnitsTypeEnum</span>.kDefaultDisplayLengthUnits) <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; The string is not valid so change</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; the text color to red.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TextBox1.ForeColor =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Drawing.<span style="color: #2b91af;">Color</span>.Red</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39; The string is&#0160; valid so change </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">&#39;the text color to the default color.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; TextBox1.ForeColor =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Drawing.<span style="color: #2b91af;">Color</span>.Black</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>VB.NET – UnitsOfMeasure - Using user Input</strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;<span style="color: green;">&#39; Get the real value of the input string.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> dValue <span style="color: blue;">As</span> <span style="color: blue;">Double</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; dValue = m_oUOM.GetValueFromExpression _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (txtInput.Text, _</p>
<p style="margin: 0px;">&#0160;&#0160; <span style="color: #2b91af;">UnitsTypeEnum</span>.kDefaultDisplayLengthUnits)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Compare the value with the length of </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; a sketch line.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">If</span> System.<span style="color: #2b91af;">Math</span>.Abs)</p>
<p style="margin: 0px;">(oSketchLine.Length - dValue)_ &lt; 0.00001 <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Line is equal to the input value&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Line is not equal to the input value&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
</div>
<p><strong>VB.NET UnitsOfMeasure - Displaying Values</strong></p>
<p>In this example a double (centimeters) is passed into a function&#0160; that gets the value of the double in the units that are current and returns it as a string.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> TestLength()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ShowLength(6.5)</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">Private</span> <span style="color: blue;">Sub</span> ShowLength(<span style="color: blue;">ByVal</span> dLength <span style="color: blue;">As</span> <span style="color: blue;">Double</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the string representation of the length.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> strLength <span style="color: blue;">As</span> <span style="color: blue;">String</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; strLength = m_oUOM.GetStringFromValue _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (dLength, _</p>
<p style="margin: 0px;">&#0160;&#0160; <span style="color: #2b91af;">UnitsTypeEnum</span>.kDefaultDisplayLengthUnits)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; MsgBox(<span style="color: #a31515;">&quot;The Length is: &quot;</span> &amp; strLength)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong>&#0160;</strong></p>
<p><strong>Parameters</strong></p>
<p><strong><em>Component Definition</em></strong></p>
<p>The Component definition will be discussed in following lessons. However we need to introduce it here because parameters are obtained form properties of the component definition. This excerpt from the Object Model diagram shows the property that will allow you to get the Component Definition of a document. (Drawings don’t have a ComponentDefinition).</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf0ce970d-pi"><img alt="image" border="0" height="150" src="/assets/image_10293.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="485" /></a></p>
<p><strong><em>Parameters - In the User Interface</em></strong></p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cfb6970b-pi"><img alt="image" border="0" height="322" src="/assets/image_591962.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="469" /></a></p>
<p><strong><em>Parameters – In the API</em></strong></p>
<p>Notice how the Parameters collection is obtained from a PartComponentDefinition or AssemblyComponentDefinition</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cfc7970b-pi"><img alt="image" border="0" height="77" src="/assets/image_905244.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="466" /></a></p>
<p>In this VB.NET code snippet the Parameters are accessed.</p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;<span style="color: green;">&#39; Get the Parameters collection object. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oParameters <span style="color: blue;">As</span> <span style="color: #2b91af;">Parameters</span> = _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; _InvApplication.ActiveDocument.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ComponentDefinition.Parameters</p>
</div>
<p><strong><em>Parameters – UI vs. API</em></strong></p>
<p>On the left in this picture you can see settings from the parameters dialog. The text on the right shows how a similar setting would be made using the API.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf110970d-pi"><img alt="image" border="0" height="320" src="/assets/image_595785.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="497" /></a></p>
<p><strong><em>VB.NET - Parameters - Setting Values</em></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;"><span style="color: blue;">Public</span> <span style="color: blue;">Sub</span> SetParameter()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the Parameters object. Assumes </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39;a part or assembly document is active.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oParameters <span style="color: blue;">As</span> <span style="color: #2b91af;">Parameters</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oParameters = _InvApplication.</p>
<p style="margin: 0px;">&#0160;&#0160; ActiveDocument.ComponentDefinition.Parameters</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Get the parameter named &quot;Length&quot;.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oLengthParam <span style="color: blue;">As</span> <span style="color: #2b91af;">Parameter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oLengthParam = oParameters.Item _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;Length&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Change the equation of the parameter.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oLengthParam.Expression = <span style="color: #a31515;">&quot;3.5 in&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">&#39; Update the document.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; _InvApplication.ActiveDocument.Update()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">End</span> <span style="color: blue;">Sub</span></p>
</div>
<p><strong><em>Parameters – Units</em></strong></p>
<p>The Unit property can be set using either a String or a value from <em>UnitsTypeEnum</em>. This enum is an API equivalent to the pre-defined unit types displayed in the Unit Type dialog.</p>
<p>Setting the unit type using a String is the same as defining a unit in the Unit Type dialog and allows you to define custom unit types by combining known unit types.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717cfe3970b-pi"><img alt="image" border="0" height="444" src="/assets/image_772631.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="398" /></a></p>
<p><strong><em>Parameters – Values</em></strong></p>
<p>Unlike in the user-interface, the API allows you to directly set the value of a parameter. The Value property sets the actual value of the parameter and will overwrite any existing equation.</p>
<p><span style="color: #ff0000;"><span style="color: #000000;"><strong>Note</strong>: <em>In API, Parameter values are always defined using internal units.</em></span><em> <span style="color: #000000;">(use UnitsOfMeasure to convert)</span> <br /></em></span>Length – Centimeters <br />Angle – Radians&#0160; <br />π radians = 180 degrees <br />π = Atn(1) * 4</p>
<p><strong><em>Parameters - Tolerances</em></strong> <br />Tolerances can be defined for Model parameters. The Tolerance object exposes functionalities of the Tolerance dialog.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d41470bcd970c-pi"><img alt="image" border="0" height="60" src="/assets/image_932204.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="127" /></a></p>
<p>The SetToDefault, SetToDeviation, SetToLimits, etc… methods of the Parameter object allow you to define the parameter’s tolerance. <br />&#0160;&#0160;&#0160; Call oParam.Tolerance.SetToDeviation(&quot;0.125 in&quot;, “-0.0625 in&quot;) <br />&#0160;&#0160;&#0160; Call oParam.Tolerance.SetToDeviation(2.54 / 8, -2.54 / 16)</p>
<p>The ModelValueType property sets which tolerance to use when computing the model value.The Precision property sets the number of decimal places to display.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d41470bde970c-pi"><img alt="image" border="0" height="319" src="/assets/image_11289.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="293" /></a></p>
<p>The SetAllToMax, SetAllToMedian, SetAllToMin, SetAllToNominal methods of the Parameters object provides the equivalent of the Parameter dialog’s “Reset Tolerance”.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017d41470bee970c-pi"><img alt="image" border="0" height="85" src="/assets/image_663129.jpg" style="background-image: none; margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="187" /></a></p>
<p><strong><em>Parameters – Parameter Types</em></strong></p>
<p>The Parameters collection returns all Parameter objects regardless of the type. The ModelParameters, ParameterTables, ReferenceParameters, and UserParameters objects provide access to specific types of parameters.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017c3717d005970b-pi"><img alt="image" border="0" height="429" src="/assets/image_791520.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="236" /></a></p>
<p><strong><em>Parameters – Parameter Creation</em></strong></p>
<p>Parameters are created by using methods on the collection for the specific type of parameter you want to create. In the user-interface you can only create user parameters.&#0160; All other types are indirectly created as a result of other actions. In the API you can directly create user, model, and reference parameters. TableParameters are created by importing an Excel worksheet. Parameters are created using either the AddByExpression or AddByValue methods</p>
<p><strong><em>VB.NET - Creating Parameters</em></strong></p>
<div style="font-family: consolas; background: #eeeeee; color: black; font-size: 10pt;">
<p style="margin: 0px;">&#0160;<span style="color: green;">&#39; add user parameters</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oUserParams <span style="color: blue;">As</span> <span style="color: #2b91af;">UserParameters</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oUserParams = oCompDef.Parameters.</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; UserParameters</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">Dim</span> oParam <span style="color: blue;">As</span> <span style="color: #2b91af;">Parameter</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oParam = oUserParams.AddByExpression _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;NewParam1&quot;</span>, <span style="color: #a31515;">&quot;3&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">UnitsTypeEnum</span>.kInchLengthUnits)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oParam = oUserParams.AddByExpression _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;NewParam1&quot;</span>, <span style="color: #a31515;">&quot;3&quot;</span>, <span style="color: #a31515;">&quot;in&quot;</span>)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oParam = oUserParams.AddByExpression _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;NewParam2&quot;</span>, <span style="color: #a31515;">&quot;3&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;<span style="color: #2b91af;">UnitsTypeEnum</span>.kDefaultDisplayLengthUnits)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oParam = oUserParams.AddByExpression _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;NewParam2&quot;</span>, <span style="color: #a31515;">&quot;3 in&quot;</span>, _</p>
<p style="margin: 0px;">&#0160;<span style="color: #2b91af;">UnitsTypeEnum</span>.kDefaultDisplayLengthUnits)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; oParam = oUserParams.AddByValue _</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #a31515;">&quot;NewParam3&quot;</span>, 3 * 2.54, _</p>
<p style="margin: 0px;">&#0160;<span style="color: #2b91af;">UnitsTypeEnum</span>.kDefaultDisplayLengthUnits)</p>
</div>
<p><strong><em>Parameters – API Only Functionality</em></strong></p>
<p>Creation of Model and Reference parameters. <br />Delete unused Model and Referenced Parameters. <br />DisabledActionTypes – Prohibit deletion of user parameters. <br />Dependents, DrivenBy – Provides dependency information between parameters. <br />Creation of custom parameter groups. <br />Change the type of a parameter.&#0160;</p>
<blockquote>
<p>Model to Reference <br />Reference to Model <br />User to Model <br />User to Reference</p>
</blockquote>
<p><strong>&#0160;</strong></p>
<p><strong>Lab: iProperties</strong></p>
<p>Write a .Net program that performs the following steps: <br />1. Create a new part document. <br />2. Edit the value of the author property to contain your name. <br />3. Create a new custom (user-defined) property</p>
<p>4. Save the document</p>
<p>5. Close the document.</p>
<p><strong>Lab: Parameters and UOM</strong> <br />1. Interactively create a simple part that contains a parameter named “Length” to control the size of the part.</p>
<p>2. Create a .Net program that contains a dialog that looks similar to this:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee8baf14b970d-pi"><img alt="image" border="0" height="128" src="/assets/image_63144.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="331" /></a></p>
<p>3. The end-user should have created a parameter named “Length” which is a numeric. By this code, he is able to enter any valid expression.&#0160; The text field should provide feedback when an invalid expression has been defined.</p>
<p>4. When the “Update” button is pressed the entered value should be assigned to the parameter</p>
<p>Here are examples of the completed Labs. (VB.NET and C#) It also contains the ppt we use for the training and another project with some sample code.</p>
<p>&#0160; <span class="asset  asset-generic at-xid-6a00e553fcbfc68834017c3717d58e970b"><a href="http://modthemachine.typepad.com/files/inventor_training_module_02_samples.zip">Download Inventor_Training_Module_02_Samples</a></span></p>
<p>-Wayne</p>

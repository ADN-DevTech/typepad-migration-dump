---
layout: "post"
title: "Get Type Id and Preview Image"
date: "2010-05-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2011"
  - "Family"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/05/get-type-id-and-preview-image.html "
typepad_basename: "get-type-id-and-preview-image"
typepad_status: "Publish"
---

<p>It seems like there is an infinite amount of new functionality in the Revit 2011 API.
Here is another little item that I was not aware of before, even though it is listed clearly in the What's New section of the Revit API help file RevitAPI.chm, which says:

<span style="color:darkblue">

<h4>Extract image of an ElementType</h4>

<p>The new method ElementType.GetPreviewImage extracts the preview image of an Element Type. This image is similar to what is seen in the Revit UI when selecting the type of an element. You can specify the size of the image in pixels.

</span>

<p>Here is a question that deals with retrieval of the preview image and also the use of the new Element.GetTypeId method, which returns the element id of the given element's type. 
That obviously only makes sense for an element that has a type assigned to it, for instance a family instance.

<p><strong>Question:</strong> I am trying to use the ElementType.GetPreviewImage method.
I doing so, I also ran into an issue with the Element.GetTypeId method.
Do you have a sample of using these two methods?

<p><strong>Answer:</strong> These two methods do not necessarily have anything to do with each other per se, but it is of course possible to grab all family instances in the model, ask each for its element type using GetTypeId, and then ask the element type for its preview image.

In case you are confused about the relationships of families, symbols and instances: a family is a collection of types. In the Revit 2011 API, they are represented by the ElementType class, which is a super class for all kinds of element types, and its subclass FamilySymbol, which specifically represents an element type that comes from a family. The ElementType class used to be named Symbol in previous versions of the API.

<p>When you insert an instance of a symbol into the model, it is represented by a family instance object. You can have many instances making use of the same element type, e.g. eight chairs or a hundred windows of the same type.

<p>The Element.GetTypeId method returns the element id of the given element's type, if that makes any sense for the given element. This does make sense for family instances.

<p>To demonstrate the use of the GetTypeId and GetPreviewImage methods, I implemented a new external command CmdPreviewImage in The Building Coder sample project, which does the following:

<ul>
<li>Retrieve all family instances using a filtered element collector.
<li>Query each instance for its element type using GetTypeId.
<li>Query each type for its preview image using GetPreviewImage.
<li>Encode the returned bitmap into JPEG format, store it in an external file, and display it to the user.
</ul>

<p>I had to add two new references to the PresentationCore and WindowsBase .NET assemblies to gain access to some .NET image processing functionality. The following helper method makes use of this to convert a Bitmap instance to a BitmapSource:

<pre class="code">
<span class="blue">static</span> <span class="teal">BitmapSource</span> ConvertBitmapToBitmapSource(
&nbsp; <span class="teal">Bitmap</span> bmp )
{
&nbsp; <span class="blue">return</span> System.Windows.Interop.<span class="teal">Imaging</span>
&nbsp; &nbsp; .CreateBitmapSourceFromHBitmap(
&nbsp; &nbsp; &nbsp; bmp.GetHbitmap(),
&nbsp; &nbsp; &nbsp; <span class="teal">IntPtr</span>.Zero,
&nbsp; &nbsp; &nbsp; <span class="teal">Int32Rect</span>.Empty,
&nbsp; &nbsp; &nbsp; <span class="teal">BitmapSizeOptions</span>.FromEmptyOptions() );
}
</pre>

<p>The command class is defined using a read-only transaction mode and a manual regeneration option, since no changes are applied to the model:

<pre class="code">
&nbsp; [<span class="teal">Transaction</span>( <span class="teal">TransactionMode</span>.ReadOnly )]
&nbsp; [<span class="teal">Regeneration</span>( <span class="teal">RegenerationOption</span>.Manual )]
</pre>

<p>Here is the command mainline code from the Execute method:

<pre class="code">
<span class="teal">UIApplication</span> uiapp = commandData.Application;
<span class="teal">UIDocument</span> uidoc = uiapp.ActiveUIDocument;
<span class="teal">Document</span> doc = uidoc.Document;
&nbsp;
<span class="teal">FilteredElementCollector</span> collector
&nbsp; = <span class="blue">new</span> <span class="teal">FilteredElementCollector</span>( doc );
&nbsp;
collector.OfClass( <span class="blue">typeof</span>( <span class="teal">FamilyInstance</span> ) );
&nbsp;
<span class="blue">foreach</span>( <span class="teal">FamilyInstance</span> fi <span class="blue">in</span> collector )
{
&nbsp; <span class="teal">Debug</span>.Assert( <span class="blue">null</span> != fi.Category,
&nbsp; &nbsp; <span class="maroon">&quot;expected family instance to have a valid category&quot;</span> );
&nbsp;
&nbsp; <span class="teal">ElementId</span> typeId = fi.GetTypeId();
&nbsp;
&nbsp; <span class="teal">ElementType</span> type = doc.get_Element( typeId ) 
&nbsp; &nbsp; <span class="blue">as</span> <span class="teal">ElementType</span>;
&nbsp;
&nbsp; <span class="teal">Size</span> imgSize = <span class="blue">new</span> <span class="teal">Size</span>( 200, 200 );
&nbsp;
&nbsp; <span class="teal">Bitmap</span> image = type.GetPreviewImage( imgSize );
&nbsp;
&nbsp; <span class="green">// encode image to jpeg for test display purposes:</span>
&nbsp;
&nbsp; <span class="teal">JpegBitmapEncoder</span> encoder 
&nbsp; &nbsp; = <span class="blue">new</span> <span class="teal">JpegBitmapEncoder</span>();
&nbsp;
&nbsp; encoder.Frames.Add( <span class="teal">BitmapFrame</span>.Create(
&nbsp; &nbsp; ConvertBitmapToBitmapSource( image ) ) );
&nbsp;
&nbsp; encoder.QualityLevel = 25;
&nbsp;
&nbsp; <span class="blue">string</span> filename = <span class="maroon">&quot;a.jpg&quot;</span>;
&nbsp;
&nbsp; <span class="teal">FileStream</span> file = <span class="blue">new</span> <span class="teal">FileStream</span>(
&nbsp; &nbsp; filename, <span class="teal">FileMode</span>.Create, <span class="teal">FileAccess</span>.Write );
&nbsp;
&nbsp; encoder.Save( file );
&nbsp; file.Close();
&nbsp;
&nbsp; <span class="teal">Process</span>.Start( filename ); <span class="green">// test display</span>
</pre>

<p>I ran this command in the following minimal sample model with a wall with a door, a window and an opening in it:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013480844ca1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013480844ca1970c" alt="Model of a wall with a door and a window" title="Model of a wall with a door and a window" src="/assets/image_a2de8a.jpg" border="0"  /></a> <br />

</center>

<p>It therefore includes just two family instances, one each for the door and the window, and displays one 200 x 200 image for each for them, just as we requested:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013480844c49970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013480844c49970c" alt="Door type preview image" title="Door type preview image" src="/assets/image_8da32e.jpg" border="0"  /></a> <br />

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833013480844bff970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833013480844bff970c" alt="Window type preview image" title="Window type preview image" src="/assets/image_9d612b.jpg" border="0"  /></a> <br />

</center>

<p>On my system, the call to Process.Start pops up the Windows fax and picture viewer for each preview image it displays.

<p>If you do not want to see the same image repeated multiple times for a type that is reused repeatedly in the model, you could add a check to skip ids of duplicate element types in the loop. 
You could also name each image file individually, for instance using the type name or other data.
I did not add that here to keep the code as simple as possible to highlight the main principles.

<p>Here is

<span class="asset  asset-generic at-xid-6a00e553e168978833013480844af4970c"><a href="http://thebuildingcoder.typepad.com/files/bc_11_66.zip">version 2011.0.66.0</a></span>

of The Building Coder sample source code and Visual Studio solution including the new command.


<a name="addendum"></a>

<p><strong>Addendum:</strong> In answer to Paul's comment below, Harry Mattison points out that Revit does not have preview images for detail components.
For instance, here is an example of brick detailing, which displays no preview in the user interface:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8952c0d7970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8952c0d7970d" alt="No preview image for brick detail" title="No preview image for brick detail" src="/assets/image_7d4bfe.jpg" border="0" /></a> <br />

</center>

<p>Model objects such as a desk, on the other hand, display the preview image when selected:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301543332b7ef970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301543332b7ef970c image-full" alt="Preview image of a desk" title="Preview image of a desk" src="/assets/image_1792b9.jpg" border="0" /></a> <br />
</center>

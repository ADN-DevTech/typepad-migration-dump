---
layout: "post"
title: "Object Relationships in VB"
date: "2010-03-17 06:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Debugging"
  - "Element Relationships"
  - "Transaction"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/03/object-relationships-in-vb.html "
typepad_basename: "object-relationships-in-vb"
typepad_status: "Publish"
---

<p>We recently presented Saikat Bhattacharya's neat little 

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/object-relationships.html">
object relationship</a> analyser and displayer, implemented in C#.

Yesterday I received a very friendly submission from Jose Guia saying "Thought I would share my code as well, ... I converted the project to VB for sharing."

<p>I compiled his solution and it works fine for me. 
I did run into two little issues which were easy to solve, both of them involving namespaces:

<ul>
<li>Different access to imported namespaces.
<li>Different namespace when exporting the external command.
</ul>

<p>Jose initially used explicit import statements in the VB source code to access the Revit API functionality:

<pre class="code">
<span class="blue">Imports</span> Autodesk
<span class="blue">Imports</span> Autodesk.Revit
<span class="blue">Imports</span> Autodesk.Revit.Elements
</pre>

<p>I much prefer that approach, since it is similar to the syntax used in C#.
However, when I tried to compile the code like that, it caused a number of errors like these (copy to an editor to see the full truncated lines):

<pre class="code">
'Element' is not accessible in this context because it is 'Friend'.
'Symbol' is not accessible in this context because it is 'Friend'.
'FamilyBase' is not accessible in this context because it is 'Friend'.
'ElementId' is not accessible in this context because it is 'Friend'.
</pre>

<p>The only way I found to resolve this was to remove the explicit import statements from the code and add global namespace importing requests to the project settings instead:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301310fae32e1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301310fae32e1970c image-full" alt="VB project references settings" title="VB project references settings" src="/assets/image_f5aee5.jpg" border="0"  /></a> <br />

</center>

<p>The global imports are defined at the bottom:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301310fae325d970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301310fae325d970c image-full" alt="VB project global import settings" title="VB project global import settings" src="/assets/image_38f14b.jpg" border="0"  /></a> <br />

</center>

<p>The second issue has to do with the exported namespace containing the external command.
The VB code specifies the same export namespace as the C# version in the source code, ObjRel:

<pre class="code">
<span class="blue">Namespace</span> ObjRel
&nbsp;&nbsp;&nbsp; <span class="blue">Public</span> <span class="blue">Class</span> Command
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span class="blue">Implements</span> IExternalCommand
</pre>

<p>In spite of this, the Visual Studio environment surreptitiously prepends another namespace specified in the global project settings, the so-called root namespace, in this case ObjRelationsVB:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a9473be6970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a9473be6970b image-full" alt="VB project root namespace settings" title="VB project root namespace settings" src="/assets/image_5a1fd9.jpg" border="0"  /></a> <br />

</center>

<p>Therefore, the class name entry in Revit.ini needs to be different for the C# and VB versions:

<pre>
ECName2=Object Relationship
ECDescription2=Object Relationship Sample by Saikat
ECClassName2=ObjRel.Command
ECAssembly2=C:\bin\ObjRel.dll

ECName3=Object Relationship VB
ECDescription3=Object Relationship Sample by Jose
ECClassName3=ObjRelationsVB.ObjRel.Command
ECAssembly3=C:\bin\ObjRelationsVB.dll
</pre>

<p>Alternatively, of course, you can set the VB root namespace to an empty string, I hope.
Or, alternatively, of course, you can use the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/03/addinmanager.html">
Add-In Manager</a> to 

load the application as suggested by Joe, and avoid dealing with Revit.ini at all.
Once loaded, the VB port obviously presents the same functionality as Saikat's original C# version.

<p>Here is Jose's complete 

<span class="asset  asset-generic at-xid-6a00e553e16897883301310fae303c970c"><a href="http://thebuildingcoder.typepad.com/files/objrelationsvb.zip">ObjRelationsVB</a></span>

source code and Visual Studio solution.</p>

<p>Very many thanks to Jose for sharing this code!

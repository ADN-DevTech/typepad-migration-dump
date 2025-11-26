---
layout: "post"
title: "Beam Maker Using a Void Extrusion to Cut"
date: "2010-07-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Family"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/07/beam-maker-using-a-void-extrusion-to-cut.html "
typepad_basename: "beam-maker-using-a-void-extrusion-to-cut"
typepad_status: "Publish"
---

<p>Here is an interesting and more than average advanced sample application 

<span class="asset  asset-generic at-xid-6a00e553e16897883301348595a229970c"><a href="http://thebuildingcoder.typepad.com/files/beammaker.zip">BeamMaker</a></span> by 

Marcelo Quevedo of 

<a href="http://www.hsb-cad.com">
hsbSOFT</a> demonstrating 

how to create a void in a beam in a family file.

<p>The family file is created and loaded into the current working project on the fly.

<p>The final result of the application is the following beam family instance created in the currently active document, which in this case was a new project:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301348595a38a970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301348595a38a970c" alt="Beam with a void extrusion cut out" title="Beam with a void extrusion cut out" src="/assets/image_fa5f3c.jpg" border="0"  /></a> <br />

</center>

<p>Here is Marcelo's description of his project:

<span style="color:darkblue">

<p>Using the default template that comes with Revit, the sample creates a 'structural framing' family that contains a void extrusion that cuts the solid (existing extrusion in the Revit template). 

<ul>
<li>The sample looks for the template, opens it, and creates a void extrusion in the position (0,0,0). It means that it is located in the center of the solid. 
<li>In order that the void extrusion can cut the solid, I am using the brilliant solution that Joe recommended me based on the CombineElements method.
<li>After that, it loads the new family into an existing Revit project via the LoadFamily method.
<li>Finally, using this new family, it creates a new instance in the existing project with the NewFamilyInstance method.
</ul>

</span>

<p>Here are some quick and dirty notes of my own from stepping through the application code in the debugger:

<ul>
<li>Check if the current document is a family document.
<li>If not, a new family document based on the 'Structural Framing - Beams and Braces' family template file is created. The template file already contains one extrusion representing the beam element solid body.
<li>Start a transaction.
<li>Create a void extrusion by calling the NewExtrusion method with its first argument, bool isSolid, set to false to create a void. This requires the prior creation of the appropriate profile curve array array and sketch plane.
<li>Collect all extrusions in the family document, e.g. the solid beam element and the void cut-out extrusion.
<li>Apply the CombineElements method to them to cut the void part out of the solid part.
<li>Load the newly created family into the current project.
<li>Commit the transaction.
<li>Insert a new family instance using the new family definition.
</ul>

<p>BeamMaker includes a whole bunch of useful utility functions, a real treasure trove of gems is contained in there.

<p>As a very simple example, here is the GetFamilyTemplatePath which determines the full path name of a given family template file and makes use of the FamilyTemplatePath property on the Revit application object to do so. 
It returns the full path of a default template for a given family template name:

<pre class="code">
<span class="blue">private</span> <span class="blue">string</span> GetFamilyTemplatePath( 
&nbsp; <span class="blue">string</span> strFamilyTemplateName )
{
&nbsp; <span class="blue">string</span> strPathForTemplates 
&nbsp; &nbsp; = _app.FamilyTemplatePath;
&nbsp;
&nbsp; <span class="blue">string</span> strTemplateFile 
&nbsp; &nbsp; = <span class="teal">Path</span>.Combine( strPathForTemplates, 
&nbsp; &nbsp; &nbsp; strFamilyTemplateName );
&nbsp;
&nbsp; <span class="blue">if</span>( !<span class="teal">File</span>.Exists( strTemplateFile ) )
&nbsp; {
&nbsp; &nbsp; strTemplateFile = <span class="teal">Path</span>.Combine(
&nbsp; &nbsp; &nbsp; strPathForTemplates,
&nbsp; &nbsp; &nbsp; <span class="maroon">&quot;Metric &quot;</span> + strFamilyTemplateName );
&nbsp; }
&nbsp;
&nbsp; <span class="blue">if</span>( !<span class="teal">File</span>.Exists( strTemplateFile ) )
&nbsp; {
&nbsp; &nbsp; strTemplateFile = <span class="maroon">&quot;&quot;</span>;
&nbsp; }
&nbsp;
&nbsp; <span class="blue">return</span> strTemplateFile;
}
</pre>

<p>Please download the 

<span class="asset  asset-generic at-xid-6a00e553e16897883301348595a229970c"><a href="http://thebuildingcoder.typepad.com/files/beammaker.zip">BeamMaker project</a></span> including

the source code, Visual Studio solution and add-in manifest files to look at the complete code.

<p>To wrap this up, here is the original discussion between Marcelo and Joe which prompted Marcelo to develop and provide this sample:

<p><strong>Question:</strong> Using the family API, I am creating a new structural framing family.
I am using the template that comes with Revit "Metric Structural Framing - Beams and Braces.rft". 
I am creating a void extrusion to cut the existing extrusion, but it does not cut the existing one. 
If you manually edit the family, you can see the existing extrusion and the new void extrusion, but the void extrusion does not subtract the geometry from the other one. 
How can I achieve this? 

<p><strong>Answer:</strong> Simply creating the void solid does not automatically cause it to cut any existing ones. 
In the Revit user interface, one needs to explicitly invoke the ‘Cut Geometry’ command for this to happen, i.e. to make the void cut the solid. 
It is accessible through the Modify tab and Geometry panel.
In a Revit plug-in, one can use the CombineElements method to make the void objects cut the solid objects.

<p>Many thanks to Joe and above all to Marcelo for providing this useful example and code!

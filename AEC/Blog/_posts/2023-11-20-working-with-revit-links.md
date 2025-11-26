---
layout: "post"
title: "Working with Revit Links"
date: "2023-11-20 05:18:03"
author: "Caroline Gitonga"
categories:
  - ".NET"
  - "Caroline Gitonga"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2023/11/working-with-revit-links.html "
typepad_basename: "working-with-revit-links"
typepad_status: "Publish"
---

<p>By <a href="https://adndevblog.typepad.com/aec/carol.html">Carol Gitonga</a></p>
<p>Many Revit developers often work on extensive projects that require the use of multiple separate Revit files. In such cases, the need arises to link these models together for efficiency and convenience. This is where Revit Links come into play, providing interoperability by allowing the linking of several Revit models into a single working file.</p>
<p>The Revit API offers a set of classes and methods to achieve this functionality. The <strong><a href="https://www.revitapidocs.com/2024/2204a5ab-6476-df41-116d-23dbe3cb5407.htm"><code>RevitLinkType</code></a></strong> class represents another Revit document ("link") brought into the current one ("host"), while the <strong><a href="https://www.revitapidocs.com/2018.2/a3a27c39-75bf-67d1-ae78-4cadd49a9c8e.htm"><code>RevitLinkInstance</code></a></strong> class represents an instance of a <strong><code>RevitLinkType</code></strong>.</p>
<p>To create new links programmatically, the <strong><a href="https://www.revitapidocs.com/2024/4cdb6058-0ae0-d584-24f7-52b72af617bc.htm"><code>RevitLinkType.Create()</code></a></strong> method is employed. This method is used to create a new Revit link type and load the linked document. It requires the host document, a model path to the file to be linked (which can be a path on the local disk, Revit Server, or the Cloud and must be a full path), and a <strong><a href="https://www.revitapidocs.com/2024/3f710983-5a4d-d515-a633-12b06a419b30.htm"><code>RevitLinkOptions</code></a></strong> object.</p>
<p>The <strong><a href="https://www.revitapidocs.com/2024/3f710983-5a4d-d515-a633-12b06a419b30.htm"><code>RevitLinkOptions</code></a></strong> class represents options for creating and loading a Revit link. These options may include whether Revit will store a relative or absolute path to the linked file and the workset configuration.</p>
<p>Once the linked document is loaded, the <strong><a href="https://www.revitapidocs.com/2018.2/ab484ddc-a219-1266-29c3-88cb00204342.htm"><code>RevitLinkInstance.Create()</code></a></strong> method is used to place an instance of the link in the model.</p>
<p>Below is an example using <strong><code>RevitLinkType.Create()</code></strong>, with the variable <strong><code>pathName</code></strong> representing the full path to the file on disk to be linked.</p>
<pre class="prettyprint">  
public ElementId CreateRevitLink(Document doc, string pathName)
{
    FilePath path = new FilePath(pathName);
    RevitLinkOptions options = new RevitLinkOptions(false);
    // Create new revit link storing absolute path to file
    LinkLoadResult result = RevitLinkType.Create(doc, path, options);
    return (result.ElementId);
}</pre>
<p>The resulting return value for successfully creating and loading a linked document is a <strong><a href="https://www.revitapidocs.com/2018.2/f846bfb0-b047-9332-567f-75ae880d8359.htm"><code>LinkLoadResult</code></a></strong> object that stores the outcomes of attempting to load a single linked model. The <strong><a href="https://www.revitapidocs.com/2018.2/f846bfb0-b047-9332-567f-75ae880d8359.htm"><code>LinkLoadResult</code></a></strong> provides a wide range of methods that you can use to gather more information about the loaded Revit link. In our case, from the code snippet above, we use:</p>
<p>result.ElementId</p>
<p>This retrieves the <strong><code>ElementId</code></strong> of the loaded Revit Link.</p>
<p>Upon successfully creating the Revit Link, instances can now be added to the document. It's important to note that until a <strong><code>RevitLinkInstance</code></strong> is created, the Revit link will appear in the Manage Links window, but the elements of the linked file will not be visible in any views.</p>
<p>The code snippet below demonstrates how to create new Revit Link instances. In this case, <strong>linkTypeId</strong> is the <strong>ElementId</strong> of the loaded Revit Link from the previous code snippet for creating the Revit Link:</p>
<pre class="prettyprint">public void CreateLinkInstances(Document doc, ElementId linkTypeId)
{
    // Create revit link instance at origin
    RevitLinkInstance.Create(doc, linkTypeId);
    RevitLinkInstance instance2 = RevitLinkInstance.Create(doc, linkTypeId);
    // Offset second instance by 100 feet
    Location location = instance2.Location;
    location.Move(new XYZ(0, -100, 0));
}
</pre>
<p>Two instances of <strong><code>RevitLinkType</code></strong> are created, with the second instance offset by 100 feet. It's important to note that instances will be placed origin-to-origin, and this function cannot be used to create instances of nested links.</p>
<p>Referring to a post in the Revit API forum: <a href="https://thebuildingcoder.typepad.com/blog/2008/12/linked-files.html">https://thebuildingcoder.typepad.com/blog/2008/12/linked-files.html</a> by community member <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/5528189">Andrea Tassera</a>, and incorporating contributions from fellow members, below is a complete working sample demonstrating how to load your Revit link for file browsing and add Revit Link instances.</p>
<pre class="prettyprint">using System;
using System.IO;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using System.Windows.Forms;

namespace RevitLinking
{

    [Transaction(TransactionMode.Manual)]
    public class Class1:IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, 
                                                    ref string message, ElementSet elements)
        {
            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            try
            {
                using (Transaction transaction = new Transaction(doc))
                {
                    // Link files in folder
                    transaction.Start("Link files");

                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.InitialDirectory = (@"C:\");
                    openFileDialog1.Filter = "RVT|*.rvt";
                    openFileDialog1.Multiselect = true;
                    openFileDialog1.RestoreDirectory = true;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                      
                        foreach (string path in openFileDialog1.FileNames)
                        {
                            FileInfo filePath = new FileInfo(path);

                            // debug ***********
                            MessageBox.Show("filePath.FullName.ToString() = " 
                                                            + filePath.FullName.ToString());
                            // debug ***********

                            ModelPath linkpath = ModelPathUtils.
                                ConvertUserVisiblePathToModelPath(filePath.FullName.ToString());
                            RevitLinkOptions options = new RevitLinkOptions(false);
                            LinkLoadResult result = RevitLinkType.Create(doc, linkpath, options);
                            RevitLinkInstance.Create(doc, result.ElementId);
                        }
                    }
                    
                    // Assuming that everything went right return Result.Succeeded
                    transaction.Commit();
                    
                }
                return Result.Succeeded;
            }
          
            catch (Exception ex)
            {
                // If something went wrong return Result.Failed
                Console.WriteLine("There was a problem!");
                Console.WriteLine(ex.Message);
                return Result.Failed;
            }
        }

     
    }
}<br /><br /></pre>
<p>Below are valuable sources for developers working with the Revit API and dealing with linked files. Here are the references formatted for clarity:</p>
<ol>
<li>Autodesk Revit API Developers Guide - Advanced Topics: Linked Files - <a href="https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Advanced_Topics_Linked_Files_Revit_Links_html">Link</a></li>
<li>Autodesk Revit API Forum Post on adding Revit Links with C# - <a href="https://forums.autodesk.com/t5/revit-api-forum/add-revit-links-to-project-with-c/td-p/7673938">Link</a></li>
<li>The Building Coder Blog Post on Linked Files (by Jeremy Tammik) - <a href="https://thebuildingcoder.typepad.com/blog/2008/12/linked-files.html">Link</a></li>
</ol>
<p>These references provide additional insights, examples, and discussions that can be beneficial for developers working with Revit and the Revit API.</p>

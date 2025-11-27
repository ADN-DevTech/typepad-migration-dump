---
layout: "post"
title: "AU Handouts: AutoCAD&reg; .NET - Developing for AutoCAD&reg; Using F# - Part 2"
date: "2008-10-27 06:42:00"
author: "Kean Walmsley"
categories:
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Concurrent programming"
  - "F#"
  - "Training"
original_url: "https://www.keanw.com/2008/10/au-handouts-a-1.html "
typepad_basename: "au-handouts-a-1"
typepad_status: "Publish"
---

<p><em>This post continues on from <a href="http://through-the-interface.typepad.com/through_the_interface/2008/10/au-handouts-aut.html">Part 1 of this series</a>. You'll find much of this content has been used before in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/more-fun-with-f.html">these</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/turning-autocad.html">previous</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2008/01/harnessing-f-as.html">posts</a>, although post does include content updated for F# 1.9.6.2 (the September 2008 CTP).</em></p>

<p>The first thing we need to do is – as with any AutoCAD .NET project – add project references to AutoCAD’s managed assemblies, acmgd.dll and acdbmgd.dll. With F#’s integration into Visual Studio 2008 you do this in exactly the same way as you would for a C# or VB.NET project, by selecting <em>Project</em> -&gt; <em>Add Reference...</em> from the pull-down menu or right-clicking the project inside the Solution Explorer and selecting <em>Add Reference...</em> from the context menu.</p>

<p>Here you then browse to the AutoCAD 2009 folder and filter for *mgd* files (at least this is the way I do it), and select the two we want:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20Add%20project%20references%20to%20AutoCAD's%20managed%20assemblies.png"><img height="333" alt="Add project references to AutoCAD's managed assemblies" src="/assets/image_982899.jpg's%20managed%20assemblies_thumb.png" width="395" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> <br /><br /><em>Figure 10 – Adding project references to AutoCAD’s managed assemblies</em></p>

<p>Now we need to make sure AutoCAD recognizes a module within a namespace, from which it is able to load commands. I found – by using <a href="http://en.wikipedia.org/wiki/.NET_Reflector">.NET Reflector</a> – that the appropriate structure is to declare your functions as the contents of a module (this needs to come after the #light directive):</p>

<div style="FONT-SIZE: 10pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><span style="COLOR: blue"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespace.MyApplication</p></div></span></div>

<p>Next we’re going to specify the .NET namespaces we’ll be using inside this application:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p></div>

<p>We’ll then skip past our definitions of words and sortedWords, and define our command function:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;Words&quot;</span>)&gt;]</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> listWords () =</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed = doc.Editor</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> db = doc.Database</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; db.TransactionManager.StartTransaction();</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(db.BlockTableId,OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTable</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ms =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.ModelSpace],</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ps =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.PaperSpace],</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p></div>

<p>Most of this section should be familiar to anyone who has used the .NET API to AutoCAD – there are really only a couple of ideas that may need explanation:</p>

<ol><li>The use keyword is just like C#’s using – but we don’t use curly braces to define scope. The scope gets defined as the remainder of the function in which the use statement has been used. Once the function completes the used object will be disposed of automatically. </li>

<li>We’re using the dynamic cast operator (:?&gt;) to specify the type of object we’re opening with GetObject(). This operator involves a query to check whether this is valid – if we wanted to do a static cast we could use :&gt; instead.</li></ol>

<p>Now we have opened our modelspace and paperspace objects (we could go further and open other layouts, but – once again – I’ll leave that as a follow-on exercise for those who feel the need to do it :-) we can look at the code we need to extract the text from our database-resident objects.</p>

<p>Let’s start by defining a local function which takes an ObjectId and uses it to open an object, and for a textual object (DBText or MText) it will return its contents:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// a list of the text contents, or an empty list.</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Note the valid use of tr, as it is in scope</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> extractText x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> obj = tr.GetObject(x,OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> obj <span style="COLOR: blue">with</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | :? MText <span style="COLOR: blue">as</span> m <span style="COLOR: blue">-&gt;</span> m.Contents</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | :? DBText <span style="COLOR: blue">as</span> d <span style="COLOR: blue">-&gt;</span> d.TextString</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | _ <span style="COLOR: blue">-&gt;</span> <span style="COLOR: maroon">&quot;&quot;</span></p></div>

<p>Once again we haven’t specified the type of the argument – this will be inferred by the system – but we could very easily do so. We’re using the transaction previously started in the listWords function – the reason for defining extractText local to it – which is quite valid, as it’s in scope.</p>

<p>After opening the object for read from its ID we’re using pattern-matching – a technique that is a huge timesaver for functional programmers – to check on the type of the object and return the appropriate property of it. This is just like a much cleaner switch statement in C#.</p>

<p>We could choose to match against any property of the object, but in our case we want to check the type, so use this operator: :?. The as keyword is a syntactic shortcut that defines a value we can then use to easily dereference the object and get at its properties and methods.</p>

<p>The final clause of the three is a wildcard: it will match all object that are not DBText or MText objects and return an empty string.<br />Now that we can get at the contents of our text objects, let’s write a quick recursive function to display the contents of the final list of words inside AutoCAD:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// A recursive function to print the contents of a list</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> printList x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> x <span style="COLOR: blue">with</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | h :: t <span style="COLOR: blue">-&gt;</span> ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span> + h); printList t</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span>)</p></div>

<p>Once again, this is a local function, so using our Editor (accessed via the ed value) is quite valid. We’re using pattern-matching again to create a recursive function (indicated via the rec keyword and then the recursive call to printList). When we find an empty list ([]) we simply print a newline, but when we find a list with a head (h) and a tail (t – which may well end up being empty, by the way, we’ll find out the next time we recurse into printList), we print the head and recurse with the tail.</p>

<p>One thing to look out for when defining recursive functions: they really need to be defined as tail-recursive, which means that the recursive call should be the last operation. This allows the compiler to perform tail call optimization, which replaces the declared recursion with a simply while loop inside the generated code.</p>

<p>Why does this matter? Well, calling a function does have some overhead, as stack space is required to store information about the function and its arguments, so if we have a list of 10,000 words to print and the function hasn’t been optimized, the recursion could cause problems. (The number could be 100,000 or 1,000,000, but the point is there is a number).</p>

<p>The above code does get optimized properly (even if the pattern for the empty list comes after the recursive call – it’s really about the position of the recursive call in the clause that recurses, rather than the overall program), and this is easy to check with .NET Reflector. In fact there’s an article on my blog covering just this:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/using-reflector.html">http://through-the-interface.typepad.com/through_the_interface/2008/02/using-reflector.html</a></p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&nbsp; Seq.to_list (Seq.cast ms) @&nbsp; &nbsp; <span style="COLOR: green">// Create a list of modelspace ids,</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Seq.to_list (Seq.cast ps) |&gt; <span style="COLOR: green">//&nbsp; appending those from paperspace</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.map extractText |&gt;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Extract the text from each object </span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; sortedWords |&gt;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get a sorted, canonical list of words</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; printList&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Print the resultant words</span></p></div>

<p>A couple of comments on the above plumbing: ms and ps are both IEnumerable types (which correspond to the Seq class in F#), but are both untyped. This means we have to cast them, to be able to access them properly from F#, and then we can simply call Seq.to_list to get the contents into a list. The @ operator appends the list of ObjectIds of objects in modelspace with those in paperspace, and we then pipe the list into a call to List.map which runs our extractText function on all the objects in the combined list. The results get piped into our sortedWords function, and we finally print them to the command-line using our recursive printList function.</p>

<p>Finally, we’re just going to call commit on our transaction object, as for performance reasons this is currently best practice:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// As usual, committing is cheaper than aborting</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; tr.Commit()</p></div>

<p>That’s it for our first AutoCAD application. Let’s see the entire listing:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="COLOR: blue">#light</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: green">// Declare a specific namespace and module name</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespace.MyApplication</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: green">// Partial application of split which can then be </span></p>

<p style="MARGIN: 0px"><span style="COLOR: green">// applied to a string to retrieve the contained words</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> words =</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> seps = <span style="COLOR: maroon">&quot; \t~`!@#$%^&amp;*()-=_+{}|[]\\;':\&quot;&lt;&gt;?,./&quot;</span></p>

<p style="MARGIN: 0px">&nbsp; seps.ToCharArray() |&gt; Array.to_list |&gt; String.split</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> sortedWords x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.map words x |&gt;&nbsp; <span style="COLOR: green">// Get the words from each string</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.concat |&gt;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// No need for the outer list</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.of_list |&gt;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create a set from the list</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Set.to_list&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Create a list from the set</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px"><span style="COLOR: green">// Now we define our command</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;Words&quot;</span>)&gt;]</p>

<p style="MARGIN: 0px"><span style="COLOR: blue">let</span> listWords () =</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed = doc.Editor</p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> db = doc.Database</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; db.TransactionManager.StartTransaction();</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(db.BlockTableId,OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTable</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ms =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.ModelSpace],</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ps =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.PaperSpace],</p>

<p style="MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp; OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Now the fun starts...</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// a list of the text contents, or an empty list.</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// Note the valid use of tr, as it is in scope</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> extractText x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> obj = tr.GetObject(x,OpenMode.ForRead)</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> obj <span style="COLOR: blue">with</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | :? MText <span style="COLOR: blue">as</span> m <span style="COLOR: blue">-&gt;</span> m.Contents</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | :? DBText <span style="COLOR: blue">as</span> d <span style="COLOR: blue">-&gt;</span> d.TextString</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | _ <span style="COLOR: blue">-&gt;</span> <span style="COLOR: maroon">&quot;&quot;</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// A recursive function to print the contents of a list</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> printList x =</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> x <span style="COLOR: blue">with</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | h :: t <span style="COLOR: blue">-&gt;</span> ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span> + h); printList t</p>

<p style="MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span>)</p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// And here's where we plug everything together...</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; Seq.to_list (Seq.cast ms) @&nbsp; &nbsp; <span style="COLOR: green">// Create a list of modelspace ids,</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; Seq.to_list (Seq.cast ps) |&gt; <span style="COLOR: green">//&nbsp; appending those from paperspace</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; List.map extractText |&gt;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Extract the text from each object </span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; sortedWords |&gt;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get a sorted, canonical list of words</span></p>

<p style="MARGIN: 0px">&nbsp; &nbsp; printList&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Print the resultant words</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; <span style="COLOR: green">// As usual, committing is cheaper than aborting</span></p>

<p style="MARGIN: 0px"></p>

<p style="MARGIN: 0px">&nbsp; tr.Commit()</p></div>

<p><strong>Introducing parallel processing in AutoCAD via F# Asynchronous Workflows</strong></p>

<p>As mentioned previously, pure functional code lends itself to be run on multiple computing cores in parallel. While the tools aren’t yet there to make this happen automatically – via <em>implicit parallelization</em> – this is a likely outcome, over the coming years. For now we have the possibility of writing code that uses <em>explicit parallelization</em> – where we specify the tasks we know can be executed at the same time and leave the language and runtime to take care of the coordination.</p>

<p>There are a couple of ways to do this, right now: the Parallel Extensions to .NET (also in CTP stage and due for inclusion in Visual Studio 2010) provide a number of parallel constructs, such as parallel versions of for and while loops. F# currently provides the capability to define and execute Asynchronous Workflows, which is what we’re going to look at now.</p>

<p>First, let’s take a look at a sample application that we’re going to parallelize. This sample goes through and queries, via RSS, the latest posts on a number of different blogs. It then generates AutoCAD geometry – text with a hyperlink – for each of these posts. So we turn AutoCAD into an RSS reader, for all intents and purposes.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 1</span> <span style="COLOR: green">// Use lightweight F# syntax</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 2</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 3</span> <span style="COLOR: blue">#light</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 4</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 5</span> <span style="COLOR: green">// Declare a specific namespace and module name</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 6</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 7</span> <span style="COLOR: blue">module</span> MyNamespace.MyApplication</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 8</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 9</span> <span style="COLOR: green">// Import managed assemblies</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;10</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;11</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;12</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;13</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;14</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;15</span> <span style="COLOR: blue">open</span> System.Xml</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;16</span> <span style="COLOR: blue">open</span> System.IO</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;17</span> <span style="COLOR: blue">open</span> System.Net</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;18</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;19</span> <span style="COLOR: green">// The RSS feeds we wish to get. The first two values are</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;20</span> <span style="COLOR: green">// only used if our code is not able to parse the feed's XML</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;21</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;22</span> <span style="COLOR: blue">let</span> feeds =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;23</span>&nbsp; &nbsp;[ (<span style="COLOR: maroon">&quot;Through the Interface&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;24</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;25</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://through-the-interface.typepad.com/through_the_interface/atom.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;26</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;27</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Don Syme's F# blog&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;28</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;29</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/rss.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;30</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;31</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Shaan Hurley's Between the Lines&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;32</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;33</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines/rss.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;34</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;35</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Scott Sheppard's It's Alive in the Lab&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;36</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/labs&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;37</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://labs.blogs.com/its_alive_in_the_lab/rss.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;38</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;39</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Volker Joseph's Beyond the Paper&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;40</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/beyond_the_paper&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;41</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://dwf.blogs.com/beyond_the_paper/atom.xml&quot;</span>) ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;42</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;43</span> <span style="COLOR: green">// Fetch the contents of a web page, synchronously</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;44</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;45</span> <span style="COLOR: blue">let</span> httpSync (url:string) = </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;46</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> req = WebRequest.Create(url) </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;47</span>&nbsp; &nbsp;<span style="COLOR: blue">use</span> resp = req.GetResponse()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;48</span>&nbsp; &nbsp;<span style="COLOR: blue">use</span> stream = resp.GetResponseStream() </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;49</span>&nbsp; &nbsp;<span style="COLOR: blue">use</span> reader = <span style="COLOR: blue">new</span> StreamReader(stream) </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;50</span>&nbsp; &nbsp;reader.ReadToEnd()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;51</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;52</span> <span style="COLOR: green">// Load an RSS feed's contents into an XML document object</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;53</span> <span style="COLOR: green">// and use it to extract the titles and their links</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;54</span> <span style="COLOR: green">// Hopefully these always match (this could be coded more</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;55</span> <span style="COLOR: green">// defensively)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;56</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;57</span> <span style="COLOR: blue">let</span> titlesAndLinks (name, url, xml) =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;58</span>&nbsp; &nbsp;<span style="COLOR: blue">try</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;59</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> xdoc = <span style="COLOR: blue">new</span> XmlDocument()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;60</span>&nbsp; &nbsp;&nbsp; xdoc.LoadXml(xml)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;61</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;62</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> titles =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;63</span>&nbsp; &nbsp;&nbsp; &nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes(<span style="COLOR: maroon">&quot;//*[name()='title']&quot;</span>)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;64</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">-&gt;</span> n.InnerText ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;65</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> links =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;66</span>&nbsp; &nbsp;&nbsp; &nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes(<span style="COLOR: maroon">&quot;//*[name()='link']&quot;</span>) <span style="COLOR: blue">-&gt;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;67</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> inn = n.InnerText</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;68</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span>&nbsp; inn.Length &gt; 0 <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;69</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; inn</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;70</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;71</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> href = n.Attributes.GetNamedItem(<span style="COLOR: maroon">&quot;href&quot;</span>).Value</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;72</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> rel = n.Attributes.GetNamedItem(<span style="COLOR: maroon">&quot;rel&quot;</span>).Value</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;73</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> href.Contains(<span style="COLOR: maroon">&quot;feedburner&quot;</span>) <span style="COLOR: blue">or</span> rel.Contains(<span style="COLOR: maroon">&quot;enclosure&quot;</span>) <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;74</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;&quot;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;75</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;76</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;href ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;77</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;78</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> descs =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;79</span>&nbsp; &nbsp;&nbsp; &nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;80</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;//*[name()='description' or name()='subtitle' or name()='summary']&quot;</span>)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;81</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">-&gt;</span> n.InnerText ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;82</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;83</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// A local function to filter out duplicate entries in</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;84</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// a list, maintaining their current order.</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;85</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Another way would be to use:</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;86</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; &nbsp; Set.of_list lst |&gt; Set.to_list</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;87</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// but that results in a sorted (probably reordered) list.</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;88</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;89</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> nub lst =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;90</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">match</span> lst <span style="COLOR: blue">with</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;91</span>&nbsp; &nbsp;&nbsp; &nbsp; | a::[] <span style="COLOR: blue">-&gt;</span> [a]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;92</span>&nbsp; &nbsp;&nbsp; &nbsp; | a::b <span style="COLOR: blue">-&gt;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;93</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> a = List.hd b <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;94</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; nub b</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;95</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;96</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; a::nub b</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;97</span>&nbsp; &nbsp;&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> []</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;98</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;99</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Filter the links to get (hopefully) the same number</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 100</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and order as the titles and descriptions</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 101</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 102</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> real = List.filter (<span style="COLOR: blue">fun</span> (x:string) <span style="COLOR: blue">-&gt;</span> x.Length &gt; 0)&nbsp; </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 103</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> lnks = real links |&gt; nub</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 104</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 105</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Return a link to the overall blog, if we don't have</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 106</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the same numbers of titles, links and descriptions</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 107</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 108</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> lnum = List.length lnks</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 109</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> tnum = List.length titles</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 110</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> dnum = List.length descs</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 111</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 112</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> tnum = 0 || lnum = 0 || lnum &lt;&gt; tnum || dnum &lt;&gt; tnum <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 113</span>&nbsp; &nbsp;&nbsp; &nbsp; [(name,url,url)]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 114</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 115</span>&nbsp; &nbsp;&nbsp; &nbsp; List.zip3 titles lnks descs</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 116</span>&nbsp; &nbsp;<span style="COLOR: blue">with</span> _ <span style="COLOR: blue">-&gt;</span> []</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 117</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 118</span> <span style="COLOR: green">// For a particular (name,url) pair,</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 119</span> <span style="COLOR: green">// create an AutoCAD HyperLink object</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 120</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 121</span> <span style="COLOR: blue">let</span> hyperlink (name,url,desc) =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 122</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> hl = <span style="COLOR: blue">new</span> HyperLink()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 123</span>&nbsp; &nbsp;hl.Name &lt;- url</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 124</span>&nbsp; &nbsp;hl.Description &lt;- desc</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 125</span>&nbsp; &nbsp;(name, hl)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 126</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 127</span> <span style="COLOR: green">// Download an RSS feed and return AutoCAD HyperLinks for its posts</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 128</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 129</span> <span style="COLOR: blue">let</span> hyperlinksSync (name, url, feed) =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 130</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> xml = httpSync feed</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 131</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> tl = titlesAndLinks (name, url, xml)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 132</span>&nbsp; &nbsp;List.map hyperlink tl</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 133</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 134</span> <span style="COLOR: green">// Now we declare our command</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 135</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 136</span> [&lt;CommandMethod(<span style="COLOR: maroon">&quot;rss&quot;</span>)&gt;]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 137</span> <span style="COLOR: blue">let</span> createHyperlinksFromRss() =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 138</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 139</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> starttime = System.DateTime.Now</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 140</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 141</span>&nbsp; &nbsp;<span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 142</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 143</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> doc =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 144</span>&nbsp; &nbsp;&nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 145</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> ed = doc.Editor</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 146</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> db = doc.Database</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 147</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 148</span>&nbsp; &nbsp;<span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 149</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 150</span>&nbsp; &nbsp;<span style="COLOR: blue">use</span> tr =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 151</span>&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 152</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 153</span>&nbsp; &nbsp;<span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 154</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 155</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> bt =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 156</span>&nbsp; &nbsp;&nbsp; tr.GetObject</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 157</span>&nbsp; &nbsp;&nbsp; &nbsp; (db.BlockTableId,OpenMode.ForRead)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 158</span>&nbsp; &nbsp;&nbsp; :?&gt; BlockTable</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 159</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> ms =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 160</span>&nbsp; &nbsp;&nbsp; tr.GetObject</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 161</span>&nbsp; &nbsp;&nbsp; &nbsp; (bt.[BlockTableRecord.ModelSpace],</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 162</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; OpenMode.ForWrite)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 163</span>&nbsp; &nbsp;&nbsp; :?&gt; BlockTableRecord</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 164</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 165</span>&nbsp; &nbsp;<span style="COLOR: green">// Add text objects linking to the provided list of</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 166</span>&nbsp; &nbsp;<span style="COLOR: green">// HyperLinks, starting at the specified location</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 167</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 168</span>&nbsp; &nbsp;<span style="COLOR: green">// Note the valid use of tr and ms, as they are in scope</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 169</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 170</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> addTextObjects (pt : Point3d) lst =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 171</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Use a for loop, as we care about the index to</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 172</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// position the various text items</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 173</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 174</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> len = List.length lst</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 175</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> index = 0 <span style="COLOR: blue">to</span> len - 1 <span style="COLOR: blue">do</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 176</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> txt = <span style="COLOR: blue">new</span> DBText()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 177</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> (name:string,hl:HyperLink) = List.nth lst index</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 178</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.TextString &lt;- name</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 179</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> offset =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 180</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> index = 0 <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 181</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0.0</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 182</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 183</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 1.0</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 184</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 185</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// This is where you can adjust:</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 186</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; the initial outdent (x value)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 187</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; and the line spacing (y value)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 188</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 189</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> vec =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 190</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> Vector3d</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 191</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (1.0 * offset,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 192</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;-0.5 * (Int32.to_float index),</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 193</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 194</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> pt2 = pt + vec</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 195</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.Position &lt;- pt2</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 196</span>&nbsp; &nbsp;&nbsp; &nbsp; ms.AppendEntity(txt) |&gt; ignore</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 197</span>&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(txt,<span style="COLOR: blue">true</span>)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 198</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.Hyperlinks.Add(hl) |&gt; ignore</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 199</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 200</span>&nbsp; &nbsp;<span style="COLOR: green">// Here's where we use the varous functions</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 201</span>&nbsp; &nbsp;<span style="COLOR: green">// we've defined</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 202</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 203</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> links =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 204</span>&nbsp; &nbsp;&nbsp; List.map hyperlinksSync feeds</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 205</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 206</span>&nbsp; &nbsp;<span style="COLOR: green">// Add the resulting objects to the model-space&nbsp; </span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 207</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 208</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> len = List.length links</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 209</span>&nbsp; &nbsp;<span style="COLOR: blue">for</span> index = 0 <span style="COLOR: blue">to</span> len - 1 <span style="COLOR: blue">do</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 210</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 211</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// This is where you can adjust:</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 212</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; the column spacing (x value)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 213</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; the vertical offset from origin (y axis)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 214</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 215</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> pt =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 216</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Point3d</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 217</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(15.0 * (Int32.to_float index),</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 218</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;30.0,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 219</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 220</span>&nbsp; &nbsp;&nbsp; addTextObjects pt (List.nth links index)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 221</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 222</span>&nbsp; &nbsp;tr.Commit()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 223</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 224</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> elapsed =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 225</span>&nbsp; &nbsp;&nbsp; &nbsp; System.DateTime.op_Subtraction(System.DateTime.Now, starttime)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 226</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 227</span>&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nElapsed time: &quot;</span> + elapsed.ToString())</p></div>

<p>I have numbered the lines, to make it easier for us to talk about the changes that are needed to introduce parallelism into this sample. Both <a href="http://through-the-interface.typepad.com/through_the_interface/files/sync-rss-to-hyperlinks-2008.fs">synchronous</a> and <a href="http://through-the-interface.typepad.com/through_the_interface/files/async-workflows-rss-to-hyperlinks-2008.fs">asynchronous</a> versions of this application are available on my blog.</p>

<p>I won’t go through the above code in detail, here: firstly, it’s not intended as a perfect implementation of an RSS consumer – there are too many variations in the way RSS is implemented by different sites, so I know for a fact that this code will not work for certain blogs – it’s really intended to be an example of a – potentially time-consuming – asynchronous (in this case network-based) activity that is easy to run in parallel.</p>

<p>A word of caution: AutoCAD is not thread-safe – it is very much a single-threaded application – so we need to coordinate the results of these tasks prior to making the changes to the AutoCAD database. Luckily F# makes this very easy for us to do, so that’s really not a problem.</p>

<p>Here is the updated source that makes use of Asynchronous Workflows, with the modified/new lines highlighted in <span style="color: #ff0000;">red</span> (with a grey background for those reading this in black &amp; white :-):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 1</span> <span style="COLOR: green">// Use lightweight F# syntax</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 2</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 3</span> <span style="COLOR: blue">#light</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 4</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 5</span> <span style="COLOR: green">// Declare a specific namespace and module name</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 6</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; <strong><span style="color: #ff0000;">7</span></strong></span> <span style="COLOR: blue">module</span> MyNamespace.MyApplicationAsync</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 8</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp; 9</span> <span style="COLOR: green">// Import managed assemblies</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;10</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;11</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;12</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;13</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;14</span> <span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;15</span> <span style="COLOR: blue">open</span> System.Xml</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;16</span> <span style="COLOR: blue">open</span> System.IO</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;17</span> <span style="COLOR: blue">open</span> System.Net</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;18</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;19</span> <span style="COLOR: green">// The RSS feeds we wish to get. The first two values are</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;20</span> <span style="COLOR: green">// only used if our code is not able to parse the feed's XML</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;21</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;22</span> <span style="COLOR: blue">let</span> feeds =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;23</span>&nbsp; &nbsp;[ (<span style="COLOR: maroon">&quot;Through the Interface&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;24</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/through-the-interface&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;25</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://through-the-interface.typepad.com/through_the_interface/atom.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;26</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;27</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Don Syme's F# blog&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;28</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;29</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.msdn.com/dsyme/rss.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;30</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;31</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Shaan Hurley's Between the Lines&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;32</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;33</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://autodesk.blogs.com/between_the_lines/rss.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;34</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;35</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Scott Sheppard's It's Alive in the Lab&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;36</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/labs&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;37</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://labs.blogs.com/its_alive_in_the_lab/rss.xml&quot;</span>);</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;38</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;39</span>&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;Volker Joseph's Beyond the Paper&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;40</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://blogs.autodesk.com/beyond_the_paper&quot;</span>,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;41</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;http://dwf.blogs.com/beyond_the_paper/atom.xml&quot;</span>) ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;42</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>43</strong></span> <span style="COLOR: green">// Fetch the contents of a web page, asynchronously</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;44</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>45</strong></span> <span style="COLOR: blue">let</span> httpAsync(url:string) = </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>46</strong></span>&nbsp; &nbsp;async { <span style="COLOR: blue">let</span> req = WebRequest.Create(url) </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>47</strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">use!</span> resp = req.GetResponseAsync()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>48</strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">use</span> stream = resp.GetResponseStream() </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>49</strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">use</span> reader = <span style="COLOR: blue">new</span> StreamReader(stream) </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;</span><span style="color: #ff0000;"><strong>50</strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> reader.ReadToEnd() }</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;51</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;52</span> <span style="COLOR: green">// Load an RSS feed's contents into an XML document object</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;53</span> <span style="COLOR: green">// and use it to extract the titles and their links</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;54</span> <span style="COLOR: green">// Hopefully these always match (this could be coded more</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;55</span> <span style="COLOR: green">// defensively)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;56</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;57</span> <span style="COLOR: blue">let</span> titlesAndLinks (name, url, xml) =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;58</span>&nbsp; &nbsp;<span style="COLOR: blue">try</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;59</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> xdoc = <span style="COLOR: blue">new</span> XmlDocument()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;60</span>&nbsp; &nbsp;&nbsp; xdoc.LoadXml(xml)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;61</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;62</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> titles =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;63</span>&nbsp; &nbsp;&nbsp; &nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes(<span style="COLOR: maroon">&quot;//*[name()='title']&quot;</span>)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;64</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">-&gt;</span> n.InnerText ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;65</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> links =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;66</span>&nbsp; &nbsp;&nbsp; &nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes(<span style="COLOR: maroon">&quot;//*[name()='link']&quot;</span>) <span style="COLOR: blue">-&gt;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;67</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> inn = n.InnerText</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;68</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span>&nbsp; inn.Length &gt; 0 <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;69</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; inn</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;70</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;71</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> href = n.Attributes.GetNamedItem(<span style="COLOR: maroon">&quot;href&quot;</span>).Value</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;72</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> rel = n.Attributes.GetNamedItem(<span style="COLOR: maroon">&quot;rel&quot;</span>).Value</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;73</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> href.Contains(<span style="COLOR: maroon">&quot;feedburner&quot;</span>) <span style="COLOR: blue">or</span> rel.Contains(<span style="COLOR: maroon">&quot;enclosure&quot;</span>) <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;74</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;&quot;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;75</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;76</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;href ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;77</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;78</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> descs =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;79</span>&nbsp; &nbsp;&nbsp; &nbsp; [ <span style="COLOR: blue">for</span> n <span style="COLOR: blue">in</span> xdoc.SelectNodes</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;80</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: maroon">&quot;//*[name()='description' or name()='subtitle' or name()='summary']&quot;</span>)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;81</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">-&gt;</span> n.InnerText ]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;82</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;83</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// A local function to filter out duplicate entries in</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;84</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// a list, maintaining their current order.</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;85</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Another way would be to use:</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;86</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; &nbsp; Set.of_list lst |&gt; Set.to_list</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;87</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// but that results in a sorted (probably reordered) list.</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;88</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;89</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> nub lst =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;90</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">match</span> lst <span style="COLOR: blue">with</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;91</span>&nbsp; &nbsp;&nbsp; &nbsp; | a::[] <span style="COLOR: blue">-&gt;</span> [a]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;92</span>&nbsp; &nbsp;&nbsp; &nbsp; | a::b <span style="COLOR: blue">-&gt;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;93</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> a = List.hd b <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;94</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; nub b</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;95</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;96</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; a::nub b</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;97</span>&nbsp; &nbsp;&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> []</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;98</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; &nbsp;99</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Filter the links to get (hopefully) the same number</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 100</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// and order as the titles and descriptions</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 101</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 102</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> real = List.filter (<span style="COLOR: blue">fun</span> (x:string) <span style="COLOR: blue">-&gt;</span> x.Length &gt; 0)&nbsp; </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 103</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> lnks = real links |&gt; nub</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 104</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 105</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Return a link to the overall blog, if we don't have</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 106</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// the same numbers of titles, links and descriptions</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 107</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 108</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> lnum = List.length lnks</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 109</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> tnum = List.length titles</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 110</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> dnum = List.length descs</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 111</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 112</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> tnum = 0 || lnum = 0 || lnum &lt;&gt; tnum || dnum &lt;&gt; tnum <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 113</span>&nbsp; &nbsp;&nbsp; &nbsp; [(name,url,url)]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 114</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 115</span>&nbsp; &nbsp;&nbsp; &nbsp; List.zip3 titles lnks descs</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 116</span>&nbsp; &nbsp;<span style="COLOR: blue">with</span> _ <span style="COLOR: blue">-&gt;</span> []</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 117</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 118</span> <span style="COLOR: green">// For a particular (name,url) pair,</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 119</span> <span style="COLOR: green">// create an AutoCAD HyperLink object</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 120</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 121</span> <span style="COLOR: blue">let</span> hyperlink (name,url,desc) =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 122</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> hl = <span style="COLOR: blue">new</span> HyperLink()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 123</span>&nbsp; &nbsp;hl.Name &lt;- url</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 124</span>&nbsp; &nbsp;hl.Description &lt;- desc</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 125</span>&nbsp; &nbsp;(name, hl)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 126</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; </span><span style="color: #ff0000;"><strong>127</strong></span> <span style="COLOR: green">// Use asynchronous workflows in F# to download</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">128</span></strong></span> <span style="COLOR: green">// an RSS feed and return AutoCAD HyperLinks</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; </span><span style="color: #ff0000;"><strong>129</strong></span> <span style="COLOR: green">// corresponding to its posts</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 130</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">131</span></strong></span> <span style="COLOR: blue">let</span> hyperlinksAsync (name, url, feed) =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">132</span></strong></span>&nbsp; &nbsp;async { <span style="COLOR: blue">let!</span> xml = httpAsync feed</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">133</span></strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> tl = titlesAndLinks (name, url, xml)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">134</span></strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span> List.map hyperlink tl }</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 135</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 136</span> <span style="COLOR: green">// Now we declare our command</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 137</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 138</span> [&lt;CommandMethod(<span style="COLOR: maroon">&quot;arss&quot;</span>)&gt;]</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 139</span> <span style="COLOR: blue">let</span> createHyperlinksFromRssAsync() =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 140</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 141</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> starttime = System.DateTime.Now</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 142</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 143</span>&nbsp; &nbsp;<span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 144</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 145</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> doc =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 146</span>&nbsp; &nbsp;&nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 147</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> ed = doc.Editor</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 148</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> db = doc.Database</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 149</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 150</span>&nbsp; &nbsp;<span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 151</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 152</span>&nbsp; &nbsp;<span style="COLOR: blue">use</span> tr =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 153</span>&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 154</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 155</span>&nbsp; &nbsp;<span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 156</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 157</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> bt =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 158</span>&nbsp; &nbsp;&nbsp; tr.GetObject</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 159</span>&nbsp; &nbsp;&nbsp; &nbsp; (db.BlockTableId,OpenMode.ForRead)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 160</span>&nbsp; &nbsp;&nbsp; :?&gt; BlockTable</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 161</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> ms =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 162</span>&nbsp; &nbsp;&nbsp; tr.GetObject</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 163</span>&nbsp; &nbsp;&nbsp; &nbsp; (bt.[BlockTableRecord.ModelSpace],</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 164</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; OpenMode.ForWrite)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 165</span>&nbsp; &nbsp;&nbsp; :?&gt; BlockTableRecord</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 166</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 167</span>&nbsp; &nbsp;<span style="COLOR: green">// Add text objects linking to the provided list of</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 168</span>&nbsp; &nbsp;<span style="COLOR: green">// HyperLinks, starting at the specified location</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 169</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 170</span>&nbsp; &nbsp;<span style="COLOR: green">// Note the valid use of tr and ms, as they are in scope</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 171</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 172</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> addTextObjects (pt : Point3d) lst =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 173</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// Use a for loop, as we care about the index to</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 174</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// position the various text items</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 175</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 176</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> len = List.length lst</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 177</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span> index = 0 <span style="COLOR: blue">to</span> len - 1 <span style="COLOR: blue">do</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 178</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> txt = <span style="COLOR: blue">new</span> DBText()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 179</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> (name:string,hl:HyperLink) = List.nth lst index</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 180</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.TextString &lt;- name</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 181</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> offset =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 182</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> index = 0 <span style="COLOR: blue">then</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 183</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0.0</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 184</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">else</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 185</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 1.0</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 186</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 187</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// This is where you can adjust:</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 188</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; the initial outdent (x value)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 189</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">//&nbsp; and the line spacing (y value)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 190</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 191</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> vec =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 192</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> Vector3d</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 193</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (1.0 * offset,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 194</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;-0.5 * (Int32.to_float index),</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 195</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 196</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">let</span> pt2 = pt + vec</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 197</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.Position &lt;- pt2</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 198</span>&nbsp; &nbsp;&nbsp; &nbsp; ms.AppendEntity(txt) |&gt; ignore</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 199</span>&nbsp; &nbsp;&nbsp; &nbsp; tr.AddNewlyCreatedDBObject(txt,<span style="COLOR: blue">true</span>)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 200</span>&nbsp; &nbsp;&nbsp; &nbsp; txt.Hyperlinks.Add(hl) |&gt; ignore</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 201</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">202</span></strong></span>&nbsp; &nbsp;<span style="COLOR: green">// Here's where we do the real work, by firing</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">203</span></strong></span>&nbsp; &nbsp;<span style="COLOR: green">// off - and coordinating - asynchronous tasks</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">204</span></strong></span>&nbsp; &nbsp;<span style="COLOR: green">// to create HyperLink objects for all our posts</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 205</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 206</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> links =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">207</span></strong></span>&nbsp; &nbsp;&nbsp; Async.Run</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">208</span></strong></span>&nbsp; &nbsp;&nbsp; &nbsp; (Async.Parallel</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">209</span></strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;[ <span style="COLOR: blue">for</span> (name,url,feed) <span style="COLOR: blue">in</span> feeds <span style="COLOR: blue">-&gt;</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">210</span></strong></span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; hyperlinksAsync (name,url,feed) ])</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; <strong><span style="color: #ff0000;">211</span></strong></span>&nbsp; &nbsp;&nbsp; |&gt; Array.to_list</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 212</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 213</span>&nbsp; &nbsp;<span style="COLOR: green">// Add the resulting objects to the model-space&nbsp; </span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 214</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 215</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> len = List.length links</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 216</span>&nbsp; &nbsp;<span style="COLOR: blue">for</span> index = 0 <span style="COLOR: blue">to</span> len - 1 <span style="COLOR: blue">do</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 217</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 218</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// This is where you can adjust:</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 219</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; the column spacing (x value)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 220</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: green">//&nbsp; the vertical offset from origin (y axis)</span></p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 221</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 222</span>&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> pt =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 223</span>&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Point3d</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 224</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(15.0 * (Int32.to_float index),</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 225</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;30.0,</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 226</span>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;0.0)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 227</span>&nbsp; &nbsp;&nbsp; addTextObjects pt (List.nth links index)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 228</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 229</span>&nbsp; &nbsp;tr.Commit()</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 230</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 231</span>&nbsp; &nbsp;<span style="COLOR: blue">let</span> elapsed =</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 232</span>&nbsp; &nbsp;&nbsp; &nbsp; System.DateTime.op_Subtraction(System.DateTime.Now, starttime)</p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 233</span> </p>

<p style="MARGIN: 0px"><span style="color: #2b91af;">&nbsp; 234</span>&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;\nElapsed time: &quot;</span> + elapsed.ToString())</p></div>

<p>Let's look at the specific changes:</p>

<ul><li>Line 7 has been changed to allow both files to be part of the same project. </li>

<li>Lines 45-50 implement a new, asynchronous function to download content from a URL. The async primitive coordinates a set of activities, while the let! and use! statements indicate that these right-hand side of the operation will be run asynchronously and that the results should be bound to the left. So here we're only getting the HTTP content asynchronously - the reading is to occur synchronously. </li>

<li>Lines 131-134 implement an asynchronous task that not only calls our asynchronous HTTP request function but coordinates the creation of AutoCAD geometry based on the contents received. </li>

<li>Lines 207-211 are where we make use of these newly-defined functions by firing them off in parallel (the framework will use the processing capabilities available to it to execute the tasks as efficiently as possible) and coordinating the results into a single array, which we convert to a list to maintain our previous processing code.</li></ul>

<p>When we run either the RSS or ARSS (its asynchronous version), we should see this kind of result:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/AU%202008%20-%20RSS%20feeds%20inside%20AutoCAD.png"><img height="166" alt="RSS feeds inside AutoCAD" src="/assets/AU%202008%20-%20RSS%20feeds%20inside%20AutoCAD_thumb.png" width="473" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p><em>Figure 11 – AutoCAD geometry created from our RSS feeds</em></p>

<p>Now let’s see how they compare in terms of performance. I executed the RSS and ARSS commands a number of times in sequence to get a feel for relative performance.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="MARGIN: 0px">Command: rss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:08.1958195</p>

<p style="MARGIN: 0px">Command: arss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:02.2802280</p>

<p style="MARGIN: 0px">Command: rss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:04.1264126</p>

<p style="MARGIN: 0px">Command: arss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:03.6343634</p>

<p style="MARGIN: 0px">Command: rss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:03.6563656</p>

<p style="MARGIN: 0px">Command: arss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:01.9891989</p>

<p style="MARGIN: 0px">Command: rss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:03.1673167</p>

<p style="MARGIN: 0px">Command: arss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:03.1223122</p>

<p style="MARGIN: 0px">Command: rss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:05.7375737</p>

<p style="MARGIN: 0px">Command: arss</p>

<p style="MARGIN: 0px">Elapsed time: 00:00:01.9391939</p></div>

<p>The first execution time is much higher due to an initial startup penalty or the need to fill some page cache with the content. On average, though, the asynchronous code runs in 60-70% of the time needed by the synchronous version. The code was run a dual-core notebook: some of the performance will be related to using both cores, but most will be due to the parallelization of asynchronous tasks that have some latency due to use of the network. With more accesses in parallel you would see this performance difference become increasingly exaggerated.</p>

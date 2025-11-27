---
layout: "post"
title: "More fun with F# and AutoCAD: string extraction and manipulation"
date: "2007-11-13 13:45:15"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
original_url: "https://www.keanw.com/2007/11/more-fun-with-f.html "
typepad_basename: "more-fun-with-f"
typepad_status: "Publish"
---

<p>I've been working through some draft chapters of Don Syme's Expert F# book (posted <a href="http://blogs.msdn.com/dsyme/archive/2006/12/18/DraftChaptersUpdateDec2006.aspx">here</a>, while the final version will be available in hardcover from early December). I'm definitely enjoying working with F#: the beauty of functional programming combined with the flexibility of .NET is a killer combination.</p>

<p>Before I dive into the sample I put together for today's post, I thought I'd scribble down some musings on the language, to help position the technology in comparison with more popular imperative/object-oriented languages...</p>

<p>Functional programming is great for deep mathematical problems, and so will play well with developers needing to perform complex scientific calculations (for example in the fields of analysis and simulation - domains that are increasing converging with and integrating into design). As I showed in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/a-mathematical-.html">this previous post</a>, you can very easily represent - and even display - complicated scientific functions using this a functional language such as F#.</p>

<p>The other aspect of great interest to me is how this fits with the increasing need to harness multi-processor &amp; multi-core environments: another of my fields of study (many moons ago) was <a href="http://en.wikipedia.org/wiki/Parallel_computing">parallel computing</a>, especially using <a href="http://en.wikipedia.org/wiki/Occam_programming_language">occam 2</a> to program <a href="http://en.wikipedia.org/wiki/Transputer">transputers</a>. That was great fun, but as a programmer working in this type of environment you end up spending a lot of time deciding which tasks are to be executed in parallel. What makes functional programming interesting with respect to concurrency - and this is clearly a major driver behind Microsoft's interest in developing languages in this area - is the ability to harness the power of multiple processing cores to run, in parallel, code that adopts the <a href="http://en.wikipedia.org/wiki/Purely_functional">purely functional</a> paradigm. &quot;Pure&quot; functional code does not create &quot;side-effects&quot;, such as when maintaining internal program state, which makes it perfect for distributing across multiple processors/cores. It's this ability to automatically farm out computing operations across your processing resources that is interesting: the manual way is simply too laborious to scale to real-world needs.</p>

<p>While F# is not a &quot;pure&quot; language (and frankly this lack of purity is what makes F# most interesting for AutoCAD programming, as it gives us the freedom to do fun things inside AutoCAD using F#), it is no doubt possible to enforce higher levels of purity in sections of code that can then make use of the increasingly parallel capabilities of modern processors. At least that's what I would expect. :-)</p>

<p>OK, so that's a look at the &quot;high end&quot; of functional programming. I also see a great deal of relevance for this type of technology at the &quot;low end&quot;, where you simply want to throw together some quick code to do something simple. And that's the case I'm presenting today.</p>

<p>As I've mentioned before, functional programming can be incredibly succinct. Here's some code F# that goes through the modelspace and paperspace of the current drawing inside AutoCAD, and prints a list of all the distinct words used inside the various MText objects:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Use lightweight F# syntax</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">(* Declare a specific namespace</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; and module name</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">*)</span> </p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespace.MyApplication</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Import managed assemblies</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2008&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Collections.Generic</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Now we declare our command</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;Words&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> listWords () =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> db = doc.Database;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; db.TransactionManager.StartTransaction();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(db.BlockTableId,OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTable</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ps =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.PaperSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Now the fun starts...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// a list of the text contents, or an empty list.</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Note the valid use of tr, as it is in scope</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> extractText (x : ObjectId) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> obj = tr.GetObject(x,OpenMode.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> obj <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | :? MText <span style="COLOR: blue">-&gt;</span> [(obj :?&gt; MText).Contents];</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | _ <span style="COLOR: blue">-&gt;</span> []</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// A recursive function to print the contents of a list</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> printList x =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> x <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | h :: t <span style="COLOR: blue">-&gt;</span> ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span> + h); printList t</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> ed.WriteMessage(<span style="COLOR: maroon">&quot;\n&quot;</span>)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Partial application of split which can then be </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// applied to a string to retrieve the contained words</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> words = String.split [' ']</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// And here's where we plug everything together...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Seq.untyped_to_list ms @ Seq.untyped_to_list ps |&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; List.map extractText |&gt; List.flatten |&gt; List.map words |&gt;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; List.flatten |&gt; Set.of_list |&gt; Set.to_list |&gt; printList</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// As usual, committing is cheaper than aborting</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tr.Commit()</p></div>

<p>Hopefully the beginning section is understandable, given what we usually need to do from a C# or VB.NET program. So I'll start my descriptions from the extractText function.</p>

<p><strong>extractText</strong> takes an ObjectId and uses the open transaction to open it for read. Then, depending on the type of the object, it gets the text contained and returns it within a list (currently containing either 0 or 1 member, depending). Currently this is only implemented for MText objects, but it could very easily be extended to handle other textual objects. For non-MText objects (matched by the wildcard character '_') an empty list ([]) is returned.</p>

<p><strong>printList</strong> is a recursive function which uses our old friend Editor.WriteMessage() to write the &quot;head&quot; of the list (h) to the command-line, and then recurses to print the &quot;tail&quot; of the list (t). When the list is empty, we simply print a newline character and return.</p>

<p><strong>words</strong> is a function defined by partial application of String.split, which typically takes two arguments - a list of characters to consider delimiters, plus a string to split. So you would call it using:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">String.Split [' '] <span style="COLOR: maroon">&quot;This is my string&quot;</span></p></div>

<p>which would return a list of strings:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[<span style="COLOR: maroon">&quot;This&quot;</span>, <span style="COLOR: maroon">&quot;is&quot;</span>, <span style="COLOR: maroon">&quot;my&quot;</span>, <span style="COLOR: maroon">&quot;string&quot;</span>]</p></div>

<p>Our definition of the function <strong>words</strong> actually allows us to get the same results by passing one argument:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">words <span style="COLOR: maroon">&quot;This is my string&quot;</span></p></div>

<p>Now we get to the guts of the command, which I'm going break down call-by-call:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Seq.untyped_to_list ms @ Seq.untyped_to_list ps |&gt;</p></div><blockquote dir="ltr"><p>Here we get a list of the ObjectIds from the modelspace and append it (@) to the ObjectIds of the contents of the first paperspace layout. BlockTableRecord implements IEnumerable - aka &quot;seq&quot; in F# - so we use untyped_to_list. If it implemented the more modern, generics-derived IEnumerable&lt;ObjectId&gt; we would use Seq.to_list instead. We then pass the results of this operation - a list of ObjectIds - to the next in the chain using the pipeline operator (|&gt;).</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">List.map extractText |&gt;</p></div><blockquote dir="ltr"><p>Here we call the <strong>extractText</strong> function on each of the items in the list passed in (the list of ObjectIds), and the results of this operation get returned in a new list. The extractText function returns a list of strings for each ObjectId, so the result of this &quot;map&quot; is a list of a list of strings. Which gets piped to the next function.</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">List.flatten |&gt;</p></div><blockquote dir="ltr"><p>As we have a list of a list of strings, we &quot;flatten&quot; it to only have a list of strings. And we pipe it on.</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">List.map words |&gt;</p></div><blockquote dir="ltr"><p>Now we map our words function to return the list of words contained in each string in the list. This - once again - gives us a list of a list of strings.</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">List.flatten |&gt;</p></div><blockquote dir="ltr"><p>Which - once again - we flatten to a list of strings (now a list of individual words).</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Set.of_list |&gt;</p></div><blockquote dir="ltr"><p>We create a &quot;set&quot; of this list, which creates an alphabetized, de-duplicated set of the words contained in our various MText objects.</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Set.to_list |&gt;</p></div><blockquote dir="ltr"><p>We now create a list of this set, which ultimately means we now have an ordered, minimal list of the words contained in the drawing.</p></blockquote><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">printList</p></div><blockquote dir="ltr"><p>And finally we print the contents to the command-line using our recursive function.</p></blockquote><p>To test the code, I created an MText object in modelspace with the imaginitive contents &quot;Here is some text in modelspace&quot;, followed by one in paperspace containing &quot;And now some in paperspace&quot;.</p>

<p>Here's what happens when we run our command:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">WORDS</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">And</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Here</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">in</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">is</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">modelspace</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">now</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">paperspace</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">some</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">text</p></div>

<p>That's it for this post. Hopefully you're able to see that F# is not only interesting for developing mathematics-intensive applications, but also for simpler operations on data such as lists and sets (it's ideal for text processing, for instance).</p>

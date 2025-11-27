---
layout: "post"
title: "Breaking it down - a closer look at the C# code for importing blocks"
date: "2006-08-21 11:40:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
original_url: "https://www.keanw.com/2006/08/breaking_it_dow.html "
typepad_basename: "breaking_it_dow"
typepad_status: "Publish"
---

<p>I didn't spend as much time as would have liked talking about the code in the <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/import_blocks_f.html">previous topic</a> (it was getting late on Friday night when I posted it). Here is a breakdown of the important function calls.</p>

<p>The first major thing we do in the code is to declare and instantiate a new Database object. This is the object that will represent our in-memory drawing (our side database). The information in this drawing will be accessible to us, but not loaded in AutoCAD's editor.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">Database</span> sourceDb = <span style="COLOR: blue">new</span> <span style="COLOR: teal">Database</span>(<span style="COLOR: blue">false</span>, <span style="COLOR: blue">true</span>);</p></div>

<p>Very importantly, the first argument (buildDefaultDrawing) is false. You will only ever need to set this to true in two situations. If you happen to pass in true by mistake when not needed, the function will return without an error, but the DWG will almost certainly be corrupt. This comes up quite regularly, so you really need to watch for this subtle issue.</p>

<p>Here are the two cases where you will want to set buildDefaultDrawing to true:</p>

<ol><li>When you intend to create the drawing yourself, and not read it in from somewhere</li>

<li>When you intend to read in a drawing that was created in R12 or before</li></ol>

<p>Although this particular sample doesn't show the technique, if you expect to be reading pre-R13 DWGs into your application in a side database, you will need to check the DWG's version and then pass in the appropriate value into the Database constructor. Here you may very well ask, &quot;but how do I check a DWG's version before I read it in?&quot; Luckily, the first 6 bytes of any DWG indicate its version number (just load a DWG into Notepad and check out the initial characters):</p>

<p>AC1.50 = R2.05<br />AC1002 = R2.6<br />AC1004 = R9<br />AC1006 = R10<br />AC1009 = R11/R12<br />AC1012 = R13<br />AC1014 = R14<br />AC1015 = 2000/2000i/2002<br />AC1018 = 2004/2005/2006<br />AC1021 = 2007</p>

<p>You'll be able to use the file access routines of your chosen programming environment to read the first 6 characters in - AC1009 or below will require a first argument of true, otherwise you're fine with false.</p>

<p>Next we ask the user for the path &amp; filename of the DWG file:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">sourceFileName =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ed.GetString(<span style="COLOR: maroon">&quot;\nEnter the name of the source drawing: &quot;</span>);</p></div>

<p>Nothing very interesting here, other than the fact I've chosen not to check whether the file actually exists (or even whether the user entered anything). The reason is simple enough: the next function call (to ReadDwgFile()) will throw an exception if the file doesn't exist, and the try-catch block will pick this up and report it to the user. We could, for example, check for that particular failure and print a more elegant message than &quot;Error during copy: eFileNotFound&quot;, but frankly that's just cosmetic - the exception is caught and handled well enough.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">sourceDb.ReadDwgFile(sourceFileName.StringResult,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;System.IO.<span style="COLOR: teal">FileShare</span>.Read,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">true</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">&quot;&quot;</span>);</p></div>

<p>This is the function call that reads in our drawing into the side database. We pass in the results of the GetString() call into the filename argument, specifying we're just reading the file (for the purposes of file-locking: this simply means that other applications will be able to read the DWG at the same time as ours but not write to it). We then specify that we wish AutoCAD to attempt silent conversion of DWGs using a code-page (a pre-Unicode concept related to localized text) that is different to the one used by the OS we're running on. The last argument specifies a blank password (we're assuming the drawing being opened is either not password protected or its password has already been entered into the session's password cache).</p>

<p>Next we instantiate a collection object to store the IDs of all the blocks we wish to copy across from the side database to the active one:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">ObjectIdCollection</span> blockIds = <span style="COLOR: blue">new</span> <span style="COLOR: teal">ObjectIdCollection</span>();</p></div>

<p>We then create a transaction which will allow us to access interesting parts of the DWG (this is the recommended way to access DWG content in .NET). Using the transaction we open the block table of the side database for read access, specifying that we only wish to access it if it has not been erased:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: teal">BlockTable</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (<span style="COLOR: teal">BlockTable</span>)tm.GetObject(sourceDb.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">false</span>);</p></div>

<p>From here - and this is one of the beauties of using the managed API to AutoCAD - we simply use a standard foreach loop to check each of the block definitions (or &quot;block table records&quot; in AcDb parlance).</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">foreach</span> (<span style="COLOR: teal">ObjectId</span> btrId <span style="COLOR: blue">in</span> bt)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">BlockTableRecord</span> btr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; (<span style="COLOR: teal">BlockTableRecord</span>)tm.GetObject(btrId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">OpenMode</span>.ForRead,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">false</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Only add named &amp; non-layout blocks to the copy list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> (!btr.IsAnonymous &amp;&amp; !btr.IsLayout)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; blockIds.Add(btrId);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; btr.Dispose();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>This code simply opens each block definition and only adds its ID to the list to copy if it is neither anonymous nor a layout (modelspace and each of the paperspaces are stored in DWGs as block definitions - we do not want to copy them across). We also call Dispose() on each block definition once we're done (this is a very good habit to get into).</p>

<p>And finally, here's the function call that does the real work:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">sourceDb.WblockCloneObjects(blockIds,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; destDb.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; mapping,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">DuplicateRecordCloning</span>.Replace,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">false</span>);</p></div>

<p>WblockCloneObjects() takes a list of objects and attempts to clone them across databases - we specify the owner to be the block table of the target database, and that should any of the blocks we're copying (i.e. their names) already exist in the target database, then they should be overwritten (&quot;Replace&quot;). You could also specify that the copy should not happen for these pre-existing blocks (&quot;Ignore&quot;).</p>

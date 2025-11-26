---
layout: "post"
title: "Running Network I/O in AutoCAD .NET Commands"
date: "2025-08-19 21:51:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/08/running-network-io-in-autocad-net-commands.html "
typepad_basename: "running-network-io-in-autocad-net-commands"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>

  <p>Traditionally, AutoCAD .NET plugins work with geometry and the drawing database. But modern workflows often require
    pulling data from the <strong>web</strong> — for example, fetching design inputs, configuration, or geometry
    parameters from a service.</p>
  <p>Since the AutoCAD .NET API is <strong>not thread-safe</strong>, you cannot call it directly from a background
    thread. This becomes tricky when performing <strong>network I/O</strong>, which is inherently asynchronous.</p>
  <p>The solution:</p>
  <ul>
    <li>
      <p>Perform network requests in a <strong>background task</strong> (<code>Task.Run</code> or
        <code>HttpClient</code> async calls).
      </p>
    </li>
    <li>
      <p>Use <code>SynchronizationContext.Current</code> captured on the AutoCAD UI thread to <strong>post results
          back</strong>.</p>
    </li>
    <li>
      <p>Modify the AutoCAD drawing safely on the main thread.</p>
    </li>
  </ul>
  <h3>Example: Drawing Lines from Web Data</h3>
  <p>We host a simple JSON file on GitHub Pages:</p>
  <pre class="prettyprint lang-json">    <code>
       {
      "lines": [
        { "start": { "x": 0, "y": 0, "z": 0 }, "end": { "x": 25, "y": 15, "z": 0 } },
        { "start": { "x": 10, "y": 5, "z": 0 }, "end": { "x": 40, "y": 30, "z": 0 } }
      ]
    }
    </code>   
</pre>
  <p>In AutoCAD, the command <code>WEBLINES</code> downloads this file and creates corresponding <code>Line</code>
    entities in ModelSpace:</p>
  <pre class="prettyprint lang-csharp">  <code>
 public async void CreateLinesFromWeb()
 {
     Document doc = Application.DocumentManager.MdiActiveDocument;
     Editor ed = doc.Editor;

     // Capture AutoCAD main thread SynchronizationContext
     System.Threading.SynchronizationContext uiContext = 
     Autodesk.AutoCAD.Runtime.SynchronizationContext.Current;

     try
     {
         // Fetch and parse JSON in background
         var lines = await Task.Run(async () =&gt;
         {
             using HttpClient client = new HttpClient();

             // JSON hosted on your GitHub Pages
             string url = "https://madhukarmoogala.github.io/linepoints.json";

             string json = await client.GetStringAsync(url);

             JsonDocument docRoot = JsonDocument.Parse(json);

             var lineList = 
             new System.Collections.Generic.List&lt;(Point3d start, Point3d end)&gt;();

             foreach (var lineElem in 
             docRoot.RootElement.GetProperty("lines").EnumerateArray())
             {
                 Point3d start = new Point3d(
                     lineElem.GetProperty("start").GetProperty("x").GetDouble(),
                     lineElem.GetProperty("start").GetProperty("y").GetDouble(),
                     lineElem.GetProperty("start").GetProperty("z").GetDouble()
                 );

                 Point3d end = new Point3d(
                     lineElem.GetProperty("end").GetProperty("x").GetDouble(),
                     lineElem.GetProperty("end").GetProperty("y").GetDouble(),
                     lineElem.GetProperty("end").GetProperty("z").GetDouble()
                 );

                 lineList.Add((start, end));
             }
             return lineList;
         });

         // Post back to UI thread to modify AutoCAD Database.
         uiContext.Post(_ =&gt;
         {
             using (doc.LockDocument())
             using (Transaction tr = 
             doc.Database.TransactionManager.StartTransaction())
             {
                 BlockTable bt = 
                 (BlockTable)tr.GetObject(doc.Database.BlockTableId,
                  OpenMode.ForRead);
                 BlockTableRecord ms = 
                 (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], 
                 OpenMode.ForWrite);

                 foreach (var linePair in lines)
                 {
                     Point3d start = linePair.start;
                     Point3d end = linePair.end;
                     Line line = new Line(start, end);
                     ms.AppendEntity(line);
                     tr.AddNewlyCreatedDBObject(line, true);

                     ed.WriteMessage($"\nCreated line from {start} → {end}");
                 }

                 tr.Commit();                       
                 ed.WriteMessage("\nLines created successfully from web data.");
                 ed.PostCommandPrompt();
             }
         }, null);
     }
     catch (Exception ex)
     {
         ed.WriteMessage($"\nError fetching/drawing lines: {ex.Message}");
     }
 }
  </code>
</pre>


  <p>This small pattern opens the door to connecting AutoCAD commands with <strong>cloud services, APIs, and dynamic
      data</strong> — safely mixing modern async programming with AutoCAD’s single-threaded API.</p>

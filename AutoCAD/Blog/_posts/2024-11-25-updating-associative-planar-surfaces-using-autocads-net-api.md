---
layout: "post"
title: "Updating Associative Planar Surfaces Using AutoCAD's .NET API"
date: "2024-11-25 02:47:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/11/updating-associative-planar-surfaces-using-autocads-net-api.html "
typepad_basename: "updating-associative-planar-surfaces-using-autocads-net-api"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>Associative surfaces in AutoCAD offer robust capabilities for maintaining dynamic links between geometric
    objects. Updating these surfaces programmatically requires careful handling of associated entities and profile
    curves. In this blog, we’ll explore how to automate the process of updating an associative planar surface with
    a new profile curve using AutoCAD .NET API. </p>
  <h4><strong>Scenario Overview</strong></h4>
  <p>The primary goal of this method is to update the profile curve of a planar surface while preserving its associative
    behavior. We assume the user selects a planar surface with a rectangular profile curve. If the surface is
    associative, the existing profile is replaced with a modified version.</p>
  <p><br></p>
  <h4><strong>Key Highlights</strong></h4>
  <ol>
    <li><strong>Surface Selection</strong>: Ensures the selected entity is a valid surface.</li>
    <li><strong>Associativity Check</strong>: Identifies whether the surface has an associated
      <code>AssocActionBody</code>.
    </li>
    <li><strong>Profile Extraction</strong>: Extracts edges from the associative surface and processes them as AutoCAD
      entities.</li>
    <li><strong>Profile Transformation</strong>: Demonstrates scaling the extracted profile using its centroid.</li>
    <li><strong>Input Path Update</strong>: Updates the planar surface with a transformed profile.</li>
    <li></li>
  </ol>
  <h5><strong>1. Surface Selection</strong></h5>
  <p>The method starts by prompting the user to select a surface. It validates the selection to ensure the entity is a
    <code>DBSurface</code>.
  </p>
  <pre class="prettyprint lang-csharp">    <code>
      PromptEntityOptions surfaceSelectionPrompt = new PromptEntityOptions("\nSelect a surface: ");
      surfaceSelectionPrompt.SetRejectMessage("Must be a Surface!");
      surfaceSelectionPrompt.AddAllowedClass(typeof(DBSurface), exactMatch: false);
      PromptEntityResult selectionResult = documentEditor.GetEntity(surfaceSelectionPrompt);
    </code>
  </pre>
  <h5><strong>2. Associativity Check</strong></h5>
  <p>To determine if the surface is associative, the code checks for a valid <code>AssocActionBody</code>.</p>
  <pre class="prettyprint lang-csharp">    <code>
      var surfaceCreationActionId = selectedSurface.CreationActionBodyId;
      bool isSurfaceAssociative = false;
      
      if (surfaceCreationActionId != ObjectId.Null)
      {
          AssocActionBody associatedActionBody = transaction.GetObject(surfaceCreationActionId, OpenMode.ForRead) as AssocActionBody;
          isSurfaceAssociative = associatedActionBody != null;
      }      
    </code>
  </pre>
  <h5><strong>3. Extracting and Transforming the Profile</strong></h5>
  <p>If the surface is associative, its input paths are processed. The extracted edges are converted into entities,
    modified (e.g., scaled), and used to update the surface.</p>
  <pre class="prettyprint lang-csharp">    <code>
      planeActionBody.GetInputPaths(out EdgeRef[][][] edgeReferenceLayers);
      foreach (var edgeReferences in edgeReferenceLayers.SelectMany(layer =&gt; layer))
      {
          foreach (var edgeReference in edgeReferences)
          {
              Entity extractedEntity = edgeReference.CreateEntity();
              extractedEntity.ColorIndex = 1;
              extractedEntity.SetDatabaseDefaults();
              ents.Add(extractedEntity);
          }
      }
      if (ents[0] is Polyline pl)
      {
          var center = GetCentroid(pl);
          profile.TransformBy(Matrix3d.Scaling(10, center));
          planeActionBody.UpdateInputPath(0, new PathRef(new EdgeRef[] { new EdgeRef(profile) }));
      }
    </code>
  </pre>
  <h5><strong>4. Evaluating the Associative Network</strong></h5>
  <p>Once the profile is updated, <code>AssocManager.EvaluateTopLevelNetwork</code> ensures all dependencies are
    recalculated.</p>
  <pre class="prettyprint lang-csharp">    <code>
      AssocManager.EvaluateTopLevelNetwork(activeDatabase, null, 0);
    </code>
  </pre>
  <h4><strong>Conclusion</strong></h4>
  <p>This approach offers a structured way to programmatically update associative planar surfaces in AutoCAD, making it
    a valuable tool for design automation. Whether you’re scaling a profile curve, transforming it, or replacing
    it entirely, understanding the associative network and leveraging the .NET API unlocks a wealth of possibilities for
    automation.</p><p><br></p>

  <script src="https://gist.github.com/MadhukarMoogala/57be825b2175027411d36bdfc27fe434.js"></script>

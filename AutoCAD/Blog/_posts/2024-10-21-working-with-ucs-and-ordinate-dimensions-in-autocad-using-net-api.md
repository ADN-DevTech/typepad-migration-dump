---
layout: "post"
title: "Working with UCS and Ordinate Dimensions in AutoCAD using .NET API"
date: "2024-10-21 23:44:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/10/working-with-ucs-and-ordinate-dimensions-in-autocad-using-net-api.html "
typepad_basename: "working-with-ucs-and-ordinate-dimensions-in-autocad-using-net-api"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>

  <p>In the world of CAD (Computer-Aided Design), <strong>precision is key</strong>.</p>
  <p>AutoCAD provides developers with powerful tools to manipulate objects and dimensions programmatically. One of the
    most critical concepts in AutoCAD development is the use of coordinate systems. This blog post will guide you
    through the process of working with the User Coordinate
    System (UCS) and creating ordinate dimensions using the AutoCAD .NET API.</p>

  <h3><strong>Understanding Coordinate Systems in AutoCAD</strong></h3>
  <p>AutoCAD operates using two main coordinate systems: the <strong>World Coordinate System (WCS)</strong> and the
    <strong>User Coordinate System (UCS)</strong>.
  </p>
  <ul>
    <li>
      <p><strong>World Coordinate System (WCS):</strong> This is the fixed global coordinate system in AutoCAD. All
        points, lines, and objects are placed relative to the WCS by default.</p>
    </li>
    <li>
      <p><strong>User Coordinate System (UCS):</strong> UCS allows you to define a custom coordinate system, aligning
        objects, views, or dimensions to any plane or orientation in 3D space. It’s particularly useful when
        working with objects at odd angles or creating dimensions aligned to specific geometric features.</p>
    </li>
  </ul>
  <p>By transforming points and objects to UCS, you can create more flexible and intuitive designs, and AutoCAD’s
    .NET API provides all the necessary tools to do this programmatically.</p>
  <h3><strong>Exploring Ordinate Dimensions</strong></h3>
  <p><strong>Ordinate dimensions</strong> are used to measure the X or Y coordinates of a point in a drawing. Unlike
    linear dimensions, ordinate dimensions don’t rely on start and end points. Instead, they measure the position
    relative to a predefined origin—making them perfect for designs that require a high degree of coordinate
    accuracy, such as mechanical parts or assembly plans.</p>
  <p>AutoCAD’s .NET API allows developers to create and customize ordinate dimensions with precision, ensuring
    that the correct values are displayed in the right locations.</p>
  <hr>
  <h3><strong>Code Walkthrough: Creating and Manipulating UCS and Ordinate Dimensions</strong></h3>
  <p>Now that we understand the concepts, let's dive into some code.</p>
  <h4><strong>1. Setting Up the UCS</strong></h4>
  <p>In AutoCAD, we can define a custom UCS and apply it to a viewport. This allows for precise control over the
    orientation of the drawing, aligning it with the real-world axes or a specific plane.</p>
  <p>Here’s how we create a UCS programmatically and apply it to a viewport:</p>
  <p><br></p>
  <pre class="prettyprint lang-csharp">    <code>
    public static void TestDimOrdinate()
    {
    var doc = Application.DocumentManager.MdiActiveDocument;
    var db = doc.Database;
    var ed = doc.Editor;

    using (var tr = db.TransactionManager.StartTransaction())
    {
        // Access the BlockTable and UCS Table
        var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
        var ucsTbl = (UcsTable)tr.GetObject(db.UcsTableId, OpenMode.ForRead);
        
        UcsTableRecord ucstr;
        if (!ucsTbl.Has("TestUcs"))
        {
            ucstr = new UcsTableRecord { Name = "TestUcs" };
            ucsTbl.UpgradeOpen();
            ucsTbl.Add(ucstr);
            tr.AddNewlyCreatedDBObject(ucstr, true);
        }
        else
        {
            ucstr = (UcsTableRecord)tr.GetObject(ucsTbl["TestUcs"], OpenMode.ForWrite);
        }

        // Define the UCS origin and axes
        ucstr.Origin = Point3d.Origin;
        ucstr.XAxis = new Vector3d(1, 1, 0);
        ucstr.YAxis = new Vector3d(-1, 1, 0);

        // Apply UCS to the active viewport
        var vptr = (ViewportTableRecord)tr.GetObject(ed.ActiveViewportId, OpenMode.ForWrite);
        vptr.SetUcs(ucstr.ObjectId);
        doc.Editor.UpdateTiledViewportsFromDatabase();
        
        tr.Commit();
    }
  }
  </code>  
  </pre>
  <p>Here, we create a new UCS named <code>"TestUcs"</code>, define its origin, X-axis, and Y-axis, and apply it to the
    active viewport. This UCS is then used to place ordinate dimensions based on the user’s input points.</p>
  <h4><strong>2. Handling User Input and Transforming Points</strong></h4>
  <p>To place ordinate dimensions, we first need to gather two points from the user and transform them from the WCS into
    the current UCS:</p>
  <pre class="prettyprint lang-csharp">    <code>
    PromptPointOptions pPtOpts = new PromptPointOptions("");
    Point3d[] point3Ds = new Point3d[2];

    for (int i = 0; i &lt; 2; i++)
    {
        pPtOpts.Message = $"\nEnter point {i + 1}: ";
        var pPtRes = doc.Editor.GetPoint(pPtOpts);

        if (pPtRes.Status != PromptStatus.OK)
        {
            doc.Editor.WriteMessage("\nPoint input was canceled or invalid.");
            break;
        }

        point3Ds[i] = pPtRes.Value;
    }

    // Transform points to UCS
    Matrix3d activeUCS = ed.CurrentUserCoordinateSystem;
    Point3d ucsDp = point3Ds[0].TransformBy(activeUCS);
    Point3d ucsLp = point3Ds[1].TransformBy(activeUCS);
    </code>
  </pre>
  <p><br></p>
  <p>This code collects two points from the user, transforming them from WCS to UCS using <code>TransformBy()</code>.
  </p>
  <h4><strong>3. Fixing Angles and Floating-Point Precision</strong></h4>
  <p>When dealing with floating-point operations, tiny precision errors can lead to discrepancies in different systems.
    To address this, we normalize angles to ensure they fall within the <code>[0, 2π]</code> range and fix
    close-to-zero negative values:</p>

  <pre class="prettyprint lang-csharp">    <code>
      const double ANGLE_EPSILON = 1E-15;

      public static bool IsNearZero(double num)
      {
          return num &lt; ANGLE_EPSILON &amp;&amp; num &gt; -ANGLE_EPSILON;
      }

      public static double FixAngle(double angle)
      {
          const double TWOPI = 2 * Math.PI;
          double retang = angle / TWOPI;
          retang = (retang - (int)retang) * TWOPI;

          if (retang &lt; 0 &amp;&amp; IsNearZero(retang))
          {
              retang = 0.0;
          }

          if (retang &lt; 0.0) retang += TWOPI;
          if (retang &gt;= TWOPI) retang -= TWOPI;

          return retang;
      }
    </code>
  </pre>
  <p>Here, we ensure angles like <code>-2E-16</code> are unified to zero, addressing differences in floating-point
    precision across different architectures.</p>
  <p>Though AutoCAD is designed for <strong>x64-based processor</strong> architectures only as of today, sake of brevity
    considered
    other architectures like <strong>ARM</strong></p>
  <h4><strong>4. Creating Ordinate Dimensions</strong></h4>
  <p>Finally, we can create ordinate dimensions based on the transformed points:</p>
  <pre class="prettyprint lang-csharp">    <code>
      double rotation = UserToLocalAngle(activeUCS.CoordinateSystem3d.Zaxis, activeUCS.CoordinateSystem3d.Xaxis);

      OrdinateDimension ordDim = new OrdinateDimension
      {
          DefiningPoint = ucsDp,
          LeaderEndPoint = ucsLp,
          Normal = activeUCS.CoordinateSystem3d.Xaxis.CrossProduct(activeUCS.CoordinateSystem3d.Yaxis),
          UsingXAxis = false,
          DimensionStyle = db.Dimstyle,
          HorizontalRotation = -rotation
      };

      btr.AppendEntity(ordDim);
      tr.AddNewlyCreatedDBObject(ordDim, true);

    </code>
  </pre>

  <p>This snippet defines an ordinate dimension using the points transformed into UCS, sets its dimension style, and
    applies the necessary rotation to ensure the dimension aligns correctly with the current UCS.</p>

  <p><strong>Note: we are setting a rotation angle to the dimension, so the Ordinate Dimension is correctly aligned with
      UCS</strong></p>
  <p>
    This is the angle from the dimension's positive horizontal axis (which is the X axis of the UCS in effect when the
    dimension was created) to the X axis of the dimension's OCS (as defined by the dimension's normal vector and the
    arbitrary axis algorithm). Positive angles are counterclockwise when looking down the OCS Z axis towards the OCS
    origin.

    The dimension's positive horizontal axis direction is used as the default left-to-right direction for the dimension
    text.
  </p>
  <h3><strong>Common Pitfalls and Best Practices</strong></h3>
  <ol>
    <li>
      <p><strong>Handling Floating-Point Precision:</strong></p>
      <ul>
        <li>Small errors can accumulate when performing geometric calculations. Always normalize your values and use an
          epsilon threshold for comparing floating-point numbers.</li>
      </ul>
    </li>
    <li>
      <p><strong>Working with UCS:</strong></p>
      <ul>
        <li>Always ensure you’re transforming points to the correct coordinate system before performing
          operations. This avoids misalignment issues, especially when working in 3D space.</li>
      </ul>
    </li>
    <li>
      <p><strong>Testing on Different Architectures *:</strong></p>
      <ul>
        <li>Differences in floating-point precision between architectures (Intel vs ARM) can affect calculations.
          Normalize close-to-zero values to avoid discrepancies.</li>
      </ul>
    </li>
  </ol>
  <hr>
  <h3><strong>Conclusion</strong></h3>
  <p>In this blog, we’ve explored how to create and manipulate UCS in AutoCAD using the .NET API, gathering user
    input, fixing floating-point precision issues, and creating ordinate dimensions. Understanding these concepts allows
    developers to create more complex and accurate drawings programmatically, especially when precision is essential.
  </p>
  <p>Experiment with the provided code, and see how you can enhance your AutoCAD projects by harnessing the power of the
    UCS and the .NET API. Happy coding!</p>

 
<p> <script src="https://gist.github.com/MadhukarMoogala/1d9501ab50cc3427ce1668791a1b9329.js"></script></p>

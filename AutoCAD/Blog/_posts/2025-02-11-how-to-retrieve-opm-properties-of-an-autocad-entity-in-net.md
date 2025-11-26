---
layout: "post"
title: "How to Retrieve OPM Properties of an AutoCAD Entity in .NET"
date: "2025-02-11 00:21:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2025/02/how-to-retrieve-opm-properties-of-an-autocad-entity-in-net.html "
typepad_basename: "how-to-retrieve-opm-properties-of-an-autocad-entity-in-net"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p><p><br></p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f948ba200d-pi"><img width="244" height="187" title="acad_opm_props" style="display: inline; background-image: none;" alt="acad_opm_props" src="/assets/image_926097.jpg" border="0"></a></p><p><br></p>
  <p>This is a common request from our developers: how to retrieve the properties of an AutoCAD entity that are
    displayed in the OPM (Object Property Manager), also known as the Property Palette.</p>
  <p>Generally, properties can be retrieved using the `get`/`set` methods of a specific entity. However, not all
    properties are exposed in .NET, especially dynamic properties that implement OPM’s `IDynamicProperty`.</p>
  <p>Every entity in AutoCAD integrates with the Properties Palette API, which is a combination of ObjectARX and COM
    APIs. Custom entities can also define their own properties and participate in the Property Inspector system.</p>

  <pre class="prettyprint lang-cs">  <code>
    //Autodesk.AutoCAD.Internal.PropertyInspector - Internal namespace - //acmgd.dll
    public static IDictionary<string  , object=""> GetOPMProperties(ObjectId id)
    {
        Dictionary<string  , object=""> map = new Dictionary<string  , object="">();
        IntPtr pUnk = ObjectPropertyManagerPropertyUtility.GetIUnknownFromObjectId(id);
  
        if (pUnk != IntPtr.Zero)
        {
            try
            {
                using (CollectionVector properties = ObjectPropertyManagerProperties.GetProperties(id, false, false))
                {
                    int cnt = properties.Count();
                    if (cnt != 0)
                    {
                        using (CategoryCollectable category = properties.Item(0) as CategoryCollectable)
                        {
                            CollectionVector props = category.Properties;
                            int propCount = props.Count();
                            for (int j = 0; j &lt; propCount; j++)
                            {
                                using (PropertyCollectable prop = props.Item(j) as PropertyCollectable)
                                {
                                    if (prop == null)
                                        continue;
                                    object value = null;
                                    if (prop.GetValue(pUnk, ref value) &amp;&amp; value != null)
                                    {
                                        if (!map.ContainsKey(prop.Name))
                                            map[prop.Name] = value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                Marshal.Release(pUnk);
            }
        }
        return map;
    }
  
  </string,></string,></string,></code>
 </pre>
  <p>A test method, to extract Properties of a Solid entity</p>
  <pre class="prettyprint lang-cs">    <code>
      [CommandMethod("GETSOLIDPROPS")]
      public static void GetSolidProps()
      {
          Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
          PromptEntityOptions entopts = new PromptEntityOptions("\nPick a solid entity from the drawing");
          entopts.SetRejectMessage("\nOnly Solid3d entities are allowed.");
          entopts.AddAllowedClass(typeof(Solid3d), false); // Ensure only Solid3d is selectable
     
          PromptEntityResult entityResult = ed.GetEntity(entopts);
     
          if (entityResult.Status != PromptStatus.OK)  // Handle user cancel or error
          {
              ed.WriteMessage("\nOperation canceled or invalid selection.");
              return;
          }
     
          ObjectId entid = entityResult.ObjectId;
          Database db = Application.DocumentManager.MdiActiveDocument.Database;
     
          using (Transaction t = db.TransactionManager.StartOpenCloseTransaction())
          {
              try
              {
                  Solid3d solid = t.GetObject(entid, OpenMode.ForRead) as Solid3d;
                  if (solid == null)
                  {
                      ed.WriteMessage("\nSelected entity is not a valid 3D solid.");
                      return;
                  }
     
                  var props = GetOPMProperties(entid);
                  foreach (var prop in props)
                  {
                      ed.WriteMessage($"\n{prop.Key} : {prop.Value}");
                  }
     
                  t.Commit();
              }
              catch (System.Exception ex)
              {
                  ed.WriteMessage($"\nError: {ex.Message}");
              }
          } 
      }
    </code>
  </pre>

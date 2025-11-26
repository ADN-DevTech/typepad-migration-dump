---
layout: "post"
title: "Safeguarding Database Pointers with AcDbDatabase&rsquo;s New Runtime ID API"
date: "2024-11-04 15:35:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/11/safeguarding-database-pointers-with-acdbdatabases-new-runtime-id-api.html "
typepad_basename: "safeguarding-database-pointers-with-acdbdatabases-new-runtime-id-api"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>

  
  <p>In Autodesk software, developers sometimes face an issue where a memory address, specifically an
    <code>AcDbDatabase</code> pointer, may get reused. If a database (DB) is deleted and a new one is created at the
    same memory location, how can we distinguish between the two instances?
  </p>
  <p>This question led us to introduce a new API—<code>runtimeId</code>.</p>
  <h3>Why the Change?</h3>
  <p>Our team noticed a pattern in crash reports related to stale <code>AcDbDatabase</code> pointers. Often, these
    issues stemmed from cached pointers that were no longer valid but hadn’t been refreshed. To help clients
    validate their pointers, we recommended using <code>acdbActiveDatabaseArray()</code> to search for active database
    pointers.</p>
  <p>While this method allowed clients to identify active pointers, it came with two limitations:</p>
  <ol>
    <li><strong>Performance Overhead:</strong> As the number of open databases increases, searching through an array can
      introduce delays.</li>
    <li><strong>Ambiguity of Validity:</strong> Even if a pointer is valid, it doesn’t guarantee it points to the
      originally intended database instance.</li>
  </ol>
  <p>This is where the <code>runtimeId</code> API comes in.</p>
  <h3>New API Methods for Safer Database Management</h3>
  <p>The new runtime ID API allows clients to confirm both the validity and uniqueness of a database pointer:</p>
  <ul>
    <li><strong><code>AcDbDatabase::runtimeId()</code></strong>: Retrieves a unique runtime ID for the current database
      instance, distinguishing it from any previous instance.</li>
    <li><strong><code>AcDbDatabase::dbIdFromPtr()</code></strong>: A static method that returns the runtime ID from a
      pointer, if valid.</li>
    <li><strong><code>AcDbDatabase::dbPtrFromId()</code></strong>: A static method that returns the database pointer
      from an ID, if valid.</li>
  </ul>
  <p>By using these methods, developers can cache the runtime ID for long-term usage, ensuring a reliable reference to
    the database, even if a pointer address is reused.</p>
  <h3>Future-Proofing with Runtime ID</h3>
  <p>This approach enables efficient, reliable database pointer management and protects against the issues caused by
    re-used memory locations. By using runtime IDs, developers can avoid stale pointers and maintain database integrity
    with minimal performance impact.</p>


  <code>
      <pre class="prettyprint lang-csharp">        #if DEBUG
    [DllImport("acpal.dll", CallingConvention = CallingConvention.Cdecl,
               CharSet = CharSet.Ansi, EntryPoint = "acutSetAssertDia")]
    static extern bool acutSetAssertDia(bool bSetDia);
      #endif
 

    [CommandMethod("TestRunTimeId")]
    public static void TestRunTimeId()
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        if (doc == null) return;

        var ed = doc.Editor;

        var firstDb = new Database(true, true);
        var transDb = new Database(true, true);

        // Validate layer table ID
        var id = firstDb.LayerTableId;
        if (!id.IsValid)
        {
            ed.WriteMessage("\nLayer ID is not valid.");
        }
        if (!id.IsWellBehaved)
        {
            ed.WriteMessage("\nLayer ID is not well-behaved.");
        }

        // Attempt to access object in the transaction
        using (Transaction t = transDb.TransactionManager.StartTransaction())
        {
            var obj = t.GetObject(id, OpenMode.ForRead, false);
            if (obj == null)
            {
                ed.WriteMessage("\nCannot open the layer table.");
            }
        }

        // Retrieve and compare runtime IDs
        long rid1 = firstDb.RuntimeId();
        if (rid1 &lt;= 0)
        {
            ed.WriteMessage("\nRuntimeId &lt; 0!");
        }
        if (rid1 != Database.IdFromDb(firstDb))
        {
            ed.WriteMessage("\nDatabase ID from pointer does not match runtime ID!");
        }

        long rid2 = transDb.RuntimeId();
        if (rid2 &lt;= 0)
        {
            ed.WriteMessage("\nRuntimeId &lt; 0!");
        }
        if (rid2 != Database.IdFromDb(transDb))
        {
            ed.WriteMessage("\nTransaction DB ID from pointer does not match runtime ID!");
        }
        if (Database.DbFromId(rid2) != transDb)
        {
            ed.WriteMessage("\nDbPtrFromId returned an incorrect pointer for transDb!");
        }
        if (rid1 == rid2)
        {
            ed.WriteMessage("\nDB and transDb have identical runtime IDs!");
        }

        // Validate null/zero scenarios
        if (Database.DbFromId(0) != null)
        {
            ed.WriteMessage("\nDbFromId(0) returned non-null!");
        }
        if (Database.IdFromDb(null) != 0)
        {
            ed.WriteMessage("\nIdFromDb(null) returned non-zero!");
        }

        // Delete firstDb and check pointer validity
        firstDb.Dispose();
        if (Database.DbFromId(rid1) != null)
        {
            ed.WriteMessage("\nDeleted DB's ID returned a non-null pointer!");
        }
        if (Database.IdFromDb(firstDb) != 0)
        {
            ed.WriteMessage("\nDeleted DB's stale pointer returned non-zero ID!");
        }

        // Check the validity and behavior of stale layer ID
        if (id.IsValid)
        {
            ed.WriteMessage("\nStale layer ID is still valid?");
        }
        if (!id.IsWellBehaved)
        {
            ed.WriteMessage("\nStale layer ID is not well-behaved?");
        }

        // Start a transaction to test stale ID access gracefully
        using (Transaction t = transDb.TransactionManager.StartTransaction())
        {
            acutSetAssertDia(false); // Disable asserts for this test
            try
            {
                var obj = t.GetObject(id, OpenMode.ForRead, false);
                if (obj != null)
                {
                    ed.WriteMessage("\nAccess to stale ID succeeded?");
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e)
            {

                ed.WriteMessage($"\nUnexpected GetObject() exception: {e.Message}");
            }
            acutSetAssertDia(true); // Re-enable asserts
        }

        // Clean up transDb
        transDb.Dispose();
        if (Database.DbFromId(rid2) != null)
        {
            ed.WriteMessage("\nDeleted transDb's ID returned a non-null pointer!");
        }
        if (Database.IdFromDb(transDb) != 0)
        {
            ed.WriteMessage("\nDeleted transDb's stale pointer returned non-zero ID!");
        }
    }
       </pre></code>

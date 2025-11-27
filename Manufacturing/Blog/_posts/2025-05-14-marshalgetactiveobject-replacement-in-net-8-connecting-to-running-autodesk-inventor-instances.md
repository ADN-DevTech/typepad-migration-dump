---
layout: "post"
title: "Marshal.GetActiveObject Replacement in .NET 8+: Connecting to Running Autodesk Inventor Instances"
date: "2025-05-14 10:26:03"
author: "Chandra Shekar Gopal"
categories:
  - "Chandra Shekar Gopal"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2025/05/marshalgetactiveobject-replacement-in-net-8-connecting-to-running-autodesk-inventor-instances.html "
typepad_basename: "marshalgetactiveobject-replacement-in-net-8-connecting-to-running-autodesk-inventor-instances"
typepad_status: "Publish"
---

<p>by <a href="https://adndevblog.typepad.com/manufacturing/chandra-shekar-gopal.html">Chandra shekar Gopal</a>,</p>
<p>As Autodesk Inventor continues evolving, developers targeting automation through its COM API face new challenges. One key issue is the deprecation of <code>System.Runtime.InteropServices.Marshal.GetActiveObject</code> in .NET Core and its obsolescence starting with Inventor 2025, which now supports .NET 8. This blog post focuses specifically on how to work with running Inventor instances in modern .NET environments using a reliable P/Invoke workaround.</p>
<p><strong>⚠️ Note:</strong> <code>Marshal.GetActiveObject</code> was fully supported in Inventor 2024 and previous releases. However, Inventor 2025 officially supports .NET 8, making this method obsolete and unsupported. Developers must adopt alternative approaches for interacting with live Inventor sessions.</p>
<hr />
<h3>What Was Marshal.GetActiveObject in Inventor Automation?</h3>
<p>In .NET Framework, <code>Marshal.GetActiveObject(&quot;Inventor.Application&quot;)</code> enabled developers to connect to a running instance of Autodesk Inventor. This capability was crucial for add-ins, scripts, or standalone tools automating tasks via Inventor&#39;s COM API.</p>
<p>Unfortunately, .NET Core and .NET 5/6+ do not support this method. To continue developing automation tools with modern .NET runtimes, an alternative method is needed.</p>
<hr />
<h3>The Solution: Using P/Invoke to Access Inventor&#39;s COM Object</h3>
<p>Platform Invocation Services (P/Invoke) allow managed .NET code to call native Windows API functions. By importing COM interop functions directly, we can replicate the behavior of <code>Marshal.GetActiveObject</code> and connect to Inventor instances.</p>
<h4>Sample Implementation</h4>
<p>Here is a practical C# workaround that allows you to retrieve the active Inventor session in .NET Core or .NET 5/6/8:</p>
<pre><code>
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace InventorInterop
{
    internal static class NativeMethods
    {
        private const string OLEAUT32 = &quot;oleaut32.dll&quot;;
        private const string OLE32 = &quot;ole32.dll&quot;;

        [DllImport(OLE32, PreserveSig = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

        [DllImport(OLE32, PreserveSig = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

        [DllImport(OLEAUT32, PreserveSig = false)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);
    }

    internal class InventorConnector
    {
        public static object GetInventorInstance()
        {
            Guid clsid;
            string progId = &quot;Inventor.Application&quot;;

            try
            {
                NativeMethods.CLSIDFromProgIDEx(progId, out clsid);
            }
            catch
            {
                NativeMethods.CLSIDFromProgID(progId, out clsid);
            }

            NativeMethods.GetActiveObject(ref clsid, IntPtr.Zero, out var obj);
            return obj;
        }
    }
}
</code></pre>
<hr />
<h3>Code Breakdown</h3>
<h4>NativeMethods Class:</h4>
<ul>
<li>Wraps required COM interop functions.</li>
<li>Includes methods for converting a ProgID to CLSID and accessing the Running Object Table (ROT).</li>
</ul>
<h4>InventorConnector Class:</h4>
<ul>
<li>Contains <code>GetInventorInstance</code>, a utility method specifically tailored to connect to Inventor.</li>
<li>Falls back to <code>CLSIDFromProgID</code> if <code>CLSIDFromProgIDEx</code> is unavailable.</li>
</ul>
<hr />
<h3>Why This Matters for Inventor Developers</h3>
<p>This workaround allows .NET Core and .NET 8+ developers to continue building powerful Inventor automation tools:</p>
<ul>
<li>Ensures compatibility with Inventor 2025 and future releases.</li>
<li>Unlocks modern .NET features while maintaining access to the Inventor COM API.</li>
<li>Offers a robust template for other Autodesk automation scenarios.</li>
</ul>
<hr />
<h3>Conclusion</h3>
<p>With Inventor 2025 moving forward on .NET 8, legacy features like <code>Marshal.GetActiveObject</code> are no longer viable. Fortunately, by using P/Invoke to access native COM APIs, you can still attach to running Inventor instances and build effective automation workflows.</p>
<p>This method provides a future-proof way to maintain your productivity tools while aligning with Autodesk&#39;s platform evolution.</p>

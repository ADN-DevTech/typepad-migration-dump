---
layout: "post"
title: "AutoCAD 2025 : Marshal.GetActiveObject .NET Core"
date: "2024-07-17 15:49:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/07/autocad-2025-marshalgetactiveobject-net-core.html "
typepad_basename: "autocad-2025-marshalgetactiveobject-net-core"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>

  The <code>GetActiveObject</code> method is deprecated in .NET 8, specifically starting from .NET Core 3.0.
  The <code>Marshal.GetActiveObject()</code> API is a simple wrapper over the Running Object Table (ROT) and relies on
  folllwing Win32 APIs that can easily be called via P/Invoke.
  <ul>
    <li>CLSIDFromProgIDEx</li>
    <li>GetActiveObject</li>
  </ul>
  An example of how to manually communicate with the ROT can be found at this <a href="https://github.com/AaronRobinsonMSFT/COMInterop/tree/master/OutOfProcDemo">GitHub repository</a>.
  It is unlikely that Microsoft will bring this API back.

  <p>Let's write a our own method <code>GetActiveObject</code></p>

  <pre class="prettyprint">    <code class="language-cs">
      [DllImport("ole32")]
      private static extern int CLSIDFromProgIDEx(
        [MarshalAs(UnmanagedType.LPWStr)] string lpszProgID,
        out Guid lpclsid);
      [DllImport("oleaut32")]
      private static extern int GetActiveObject(
        [MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
        IntPtr pvReserved,
        [MarshalAs(UnmanagedType.IUnknown)] out object ppunk);

      public static object? GetActiveObject(string progId,
                                           bool throwOnError = false)
      {
      if (progId == null)
          throw new ArgumentNullException(nameof(progId));

      var hr = CLSIDFromProgIDEx(progId, out var clsid);
      if (hr &lt; 0)
      {
          if (throwOnError)
              Marshal.ThrowExceptionForHR(hr);

          return null;
      }
      hr = GetActiveObject(clsid, IntPtr.Zero, out var obj);
      if (hr &lt; 0)
      {
          if (throwOnError)
              Marshal.ThrowExceptionForHR(hr);

          return null;
      }
      return obj;
      }
    </code>
    </pre>

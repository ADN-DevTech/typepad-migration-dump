---
layout: "post"
title: "Bridging the .NET Core and AutoCAD 2025: Exposing and Using COM Server Components with GetInterfaceObject"
date: "2024-12-18 17:03:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2024/12/bridging-the-net-core-and-autocad-2025-exposing-and-using-com-server-components-with-getinterfaceobject.html "
typepad_basename: "bridging-the-net-core-and-autocad-2025-exposing-and-using-com-server-components-with-getinterfaceobject"
typepad_status: "Publish"
---

<p>  
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
  </p>
  <p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
  <p>In this blog post, we will explore how to expose COM server components in .NET Core and utilize them in AutoCAD
    using <code>GetInterfaceObject</code>.</p>
  <h3>Why Use COM in .NET Core?</h3>
  <p>COM (Component Object Model) provides a standardized way for software components to communicate, making it a
    popular tool for extending AutoCAD. For decades, it has been used to create external out-of-process applications and
    automate AutoCAD workflows.</p>
  <p>While the .NET Framework offered seamless support for COM, achieving the same functionality in .NET Core or .NET 5+
    requires additional effort. This blog post addresses how to migrate and expose COM components in the modern .NET
    environment.</p>
  <p>We will reference <a href="https://through-the-interface.typepad.com/through_the_interface/2009/05/interfacing-an-external-com-application-with-a-net-module-in-process-to-autocad.html">Kean's
      old COM sample project</a>, which works well in .NET Framework 4.8 but encounters issues in
    .NET 8.0. This post aims to bridge that gap.</p>
  <p>As an example, we will create a COM server to calculate the value of Pi and use it within AutoCAD.</p>
  <h3>Step 1: Create a COM Server in .NET Core</h3>
  <h4>Define Interfaces</h4>
  <p>To make AutoCAD compatible, we implement a fake <code>IDispatch</code> interface:</p>
  <p><code>00020400-0000-0000-C000-000000000046</code> is special GUID of IDispatch</p>
  <pre class="prettyprint">  <code>
    [ComVisible(true)]
    [Guid("00020400-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IDispatch
    {
        [DispId(1)]
        void GetTypeInfoCount(out int pctinfo);
        [DispId(2)]
        void GetTypeInfo(int iTInfo, int lcid, out IntPtr info);
        [DispId(3)]
        void GetIDsOfNames(ref Guid riid, ref IntPtr rgszNames, int cNames, int lcid, ref IntPtr rgDispId);
        [DispId(4)]
        void Invoke(int dispIdMember, ref Guid riid,
         int lcid, short wFlags, ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
         out object pVarResult, ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo,
         out int puArgErr);
    }

    [ComVisible(true)]
    [Guid("BA9AC84B-C7FC-41CF-8B2F-1764EB773D4B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IServer : IDispatch
    {
        [DispId(1)]
        double ComputePi();
    }
  </code>
</pre>
  <h4>Implement the COM Server</h4>
  <p>The server calculates Pi using the Leibniz formula:</p>
  <pre class="prettyprint">    <code>
      [ComVisible(true)]
      [Guid("A8DAD545-A841-4EE4-B4DA-AAAC2FDDD305")]
      public class Server : IServer
      {
          double IServer.ComputePi()
          {
              double sum = 0.0;
              int sign = 1;
              for (int i = 0; i &lt; 1024; ++i)
              {
                  sum += sign / (2.0 * i + 1.0);
                  sign *= -1;
              }
              return 4.0 * sum;
          }

          void IDispatch.GetIDsOfNames(ref Guid riid, ref IntPtr rgszNames, int cNames, int lcid, ref IntPtr rgDispId)
           =&gt; throw new NotImplementedException();
          void IDispatch.GetTypeInfo(int iTInfo, int lcid, out IntPtr info)
           =&gt; throw new NotImplementedException();
          void IDispatch.GetTypeInfoCount(out int pctinfo)
           =&gt; throw new NotImplementedException();
          void IDispatch.Invoke(int dispIdMember, ref Guid riid, int lcid,
          short wFlags, ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
          out object pVarResult, ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo,
          out int puArgErr) =&gt; throw new NotImplementedException();
      }
    </code>
  </pre>
  <h3>Step 2: Register the COM Server</h3>
  <p>Use <code>regsvr32</code> to register the server <code>*.comhost.dll</code>.
    Ensure your assembly is visible to COM and has a unique GUID:</p>
  <pre class="prettyprint">    <code>
      [assembly: Guid("A8DAD545-A841-4EE4-B4DA-AAAC2FDDD305")]
      [assembly: TypeLibVersion(1, 0)] //Optional.
    </code>
  </pre>
  <h3>Step 3: Use the Server in AutoCAD</h3>
  <h4>Implement the AutoCAD Plugin</h4>
  <p>The plugin retrieves the COM server object and uses it to compute Pi:</p>
  <pre class="prettyprint">    <code>
      [CommandMethod("RunDLL")]
      public void RunDLL()
      {
          var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
          if (doc == null)
              return;
      
          var editor = doc.Editor;
          dynamic acadObj = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication;
      
          if (acadObj != null)
          {
              try
              {
                  IServer serverObj = acadObj.GetInterfaceObject("COMServer.Server") as IServer;
                  if (serverObj != null)
                  {
                      double pi = serverObj.ComputePi();
                      editor.WriteMessage("\nPi: " + pi);
                  }
              }
              catch (Exception ex)
              {
                  editor.WriteMessage("\nError: " + ex.Message);
              }
          }
      }
    </code>
  </pre>
  <h3>Key Takeaways</h3>
  <ol start="1" data-spread="false">
    <li>
      <p><strong>Fake IDispatch Interface:</strong> Necessary for AutoCAD’s compatibility.</p>
    </li>
    <li>
      <p><strong>COM Server Registration:</strong> Ensures discoverability by AutoCAD.</p>
    </li>
  </ol>
  <p>Please refer <a href="https://github.com/MadhukarMoogala/AcadCOMServer">GitHub repo</a> for further details.</p>

  <p>For more information, please refer to <a href="https://learn.microsoft.com/en-us/dotnet/core/native-interop/expose-components-to-com">Microsoft article on
      COM Component in .NET core</a></p>

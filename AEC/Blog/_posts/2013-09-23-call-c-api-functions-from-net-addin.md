---
layout: "post"
title: "Call C++ API functions from .NET AddIn"
date: "2013-09-23 09:00:05"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD Architecture"
original_url: "https://adndevblog.typepad.com/aec/2013/09/call-c-api-functions-from-net-addin.html "
typepad_basename: "call-c-api-functions-from-net-addin"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Sometimes a function is not available in a higher level API (COM or .NET) but only in the underlying C++ API. Not all is lost in such a case.&#0160;</p>
<p>In the following example we&#39;ll see how we can access the <strong>AecAppDbx::drawingPromoterAndIniter</strong> function of the OMF 2010 C++ API from a .NET AddIn. <br />Note: there is an easier way to get the drawing opened in the background fully loaded: <a href="http://adndevblog.typepad.com/aec/2012/09/problems-reading-an-aec-side-database-with-a-mode-other-than-sh_denyno.html" target="_self">http://adndevblog.typepad.com/aec/2012/09/problems-reading-an-aec-side-database-with-a-mode-other-than-sh_denyno.html</a>,&#0160;but that&#39;s not what this current article is about.</p>
<p><a href="http://adndevblog.typepad.com/aec/2012/09/problems-reading-an-aec-side-database-with-a-mode-other-than-sh_denyno.html" target="_self"></a>You have two options to access C++ functions from your .NET AddIn:
<br /><br />
<strong>1) Create a .NET wrapper for this function&#0160;</strong></p>
<p>
You can simply create a new <strong>ARX</strong> project with <strong>OMF</strong> and <strong>.NET</strong> support using the <strong>ObjectARX Wizard</strong> in <strong>Visual Studio</strong> and then add the following somewhere in the project, e.g. in <strong>acrxEntryPoint.cpp</strong>:</p>
<pre>public ref class Aen1AecUtils
{
public: 
  static void drawingPromoterAndIniter(
    Autodesk::AutoCAD::DatabaseServices::Database^ db, 
    System::Boolean sideDb)
  {
    AcDbDatabase* pDb = static_cast(db-&gt;UnmanagedObject.ToPointer());

    AecAppDbx::drawingPromoterAndIniter(pDb, sideDb);
  }
};</pre>
<p>Now you can reference the assembly/dll from your .NET application: </p>
<pre>[CommandMethod(&quot;OpenSideDatabase&quot;)]
public static void OpenSideDatabase()
{
  using (Database db = new Database(false, true))
  {
    db.ReadDwgFile(
      @&quot;C:\temp\test.dwg&quot;, 
      System.IO.FileShare.None, false, &quot;&quot;);

    Aen1AecUtils.drawingPromoterAndIniter(db, true);

    // read/write the database
  }
}</pre>
<p><strong>2) Use P/Invoke to call the OMF function from your .NET code</strong></p>
<p>Based on the structure of the <strong>OMF SDK</strong> you can find out which library you need to check for the entry point of <strong>AecAppDbx::drawingPromoterAndIniter()</strong></p>
<p>The function is declared in <strong>AecAppDbx.h</strong>, which is in the <strong>AecBase</strong> folder, so the function is inside the <strong>AecBase.lib (AecBase.dbx)</strong>, whose <strong>32 bit version</strong> resides in the <strong>lib-win32</strong> folder.</p>
<p>If you use <strong>depends.exe</strong> (<strong>Dependency Walker</strong>, part of Visual Studio) to see the exported function in <strong>AecBase.dbx</strong>, then you will not find this function listed. That is because it is exported with the <strong>NONAME</strong> option.</p>
<p>So instead you need to use <strong>dumpbin.exe</strong> (part of Visual Studio). The easiest thing is just to open up &quot;Microsoft Visual Studio 2008 &gt; Visual Studio Tools &gt; Visual Studio 2008 Command Prompt&quot; and use it from there to list the exported functions in a text file, where you can search for the function you need, e.g.:</p>
<pre>dumpbin &quot;c:\OMF2010.56.0.final\lib-win32\AecBase.lib&quot; <br />/EXPORTS /OUT:&quot;c:\AecBase.txt&quot;</pre>
<p>Since the function name is not exported, use the function&#39;s ordinal number <strong>[875]</strong> instead to P/Invoke it: </p>
<pre>[DllImport(&quot;AecBase.dbx&quot;, CallingConvention = CallingConvention.Cdecl, 
  CharSet = CharSet.Unicode, EntryPoint = &quot;#875&quot;)]
public extern static void drawingPromoterAndIniter(<br />  IntPtr db, bool sideDb);

[CommandMethod(&quot;OpenSideDatabase&quot;)]
public static void OpenSideDatabase()
{
  using (Database db = new Database(false, true))
  {
    db.ReadDwgFile(
      @&quot;C:\temp\test.dwg&quot;, 
      System.IO.FileShare.None, false, &quot;&quot;);

    drawingPromoterAndIniter(db.UnmanagedObject, true);

    // read/write the database
  }
}</pre>
<p>&#0160;</p>

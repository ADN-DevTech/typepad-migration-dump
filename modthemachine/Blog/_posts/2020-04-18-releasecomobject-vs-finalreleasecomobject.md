---
layout: "post"
title: "ReleaseComObject vs FinalReleaseComObject"
date: "2020-04-18 15:17:29"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2020/04/releasecomobject-vs-finalreleasecomobject.html "
typepad_basename: "releasecomobject-vs-finalreleasecomobject"
typepad_status: "Publish"
---

<p>When freeing up <strong>COM</strong> objects in your <strong>Inventor</strong> <strong>add-in</strong>, don&#39;t use <a href="https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.finalreleasecomobject">FinalReleaseComObject()</a> as it will badly affect other <strong>add-ins</strong>. It fully releases the object, so that other <strong>add-ins</strong> previously referencing it will not be able to access it either.</p>
<p>Here is an example.</p>
<p><span style="text-decoration: underline;"><strong>AddIn1</strong> does this when being unloaded:</span></p>
<pre>public void Deactivate()
{
  Marshal.FinalReleaseComObject(m_inventorApplication);
  m_inventorApplication = null;

  GC.Collect();
  GC.WaitForPendingFinalizers();
}</pre>
<p><span style="text-decoration: underline;"><strong>AddIn2</strong> has the following code executing when its button is clicked:</span></p>
<pre>private void M_buttonDefinition_OnExecute(NameValueMap Context)
{
  try
  {
    MessageBox.Show(m_inventorApplication.ActiveDocument.DisplayName, <br />      &quot;m_inventorApplication.ActiveDocument.DisplayName&quot;);
  }
  catch (Exception ex)
  {
    MessageBox.Show(ex.Message, &quot;Accessing m_inventorApplication&quot;);
  }
}
</pre>
<p>If I <strong>unload</strong> <strong>AddIn1</strong> and try to use <strong>AddIn2&#39;s command</strong>, I&#39;ll run into this:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4fcdac8200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="FinalReleaseComObject_Issue" border="0" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4fcdac8200d image-full img-responsive" src="/assets/image_162560.jpg" title="FinalReleaseComObject_Issue" /></a></p>
<p>(error message says: &quot;COM object that has been separated from its underlying RCW cannot be used.&quot;)</p>
<p>Instead of using <a href="https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.finalreleasecomobject">FinalReleaseComObject()</a>, you could use <a href="https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.releasecomobject">ReleaseComObject()</a> or even just setting the variable to <strong>null&#0160;</strong>might be enough.<br />You can still call <a href="https://docs.microsoft.com/en-us/dotnet/api/system.gc.collect">GC.Collect()</a> &amp; <a href="https://docs.microsoft.com/en-us/dotnet/api/system.gc.waitforpendingfinalizers">GC.WaitForPendingFinalizers()</a> to trigger the release of the objects.</p>
<p>-Adam</p>

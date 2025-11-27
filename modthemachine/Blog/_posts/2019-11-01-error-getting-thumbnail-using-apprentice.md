---
layout: "post"
title: "Error getting Thumbnail using Apprentice"
date: "2019-11-01 12:28:12"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/11/error-getting-thumbnail-using-apprentice.html "
typepad_basename: "error-getting-thumbnail-using-apprentice"
typepad_status: "Publish"
---

<p>I already have an article on problems with using <strong>Apprentice</strong> from a <strong>side thread</strong>, which also mentions that this <strong>side thread</strong> needs to be a <a href="https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-3.0/ms182351(v=vs.80)?redirectedfrom=MSDN">single-threaded apartment</a>. However, the <strong>main thread</strong> of your application might be a <strong>non single-threaded apartment</strong> (i.e. multi-threaded apartment) too - something to watch out for. <br />Depending on the language you are using the default is different:<br /><a href="https://docs.microsoft.com/en-us/dotnet/api/system.stathreadattribute?view=netframework-4.8">https://docs.microsoft.com/en-us/dotnet/api/system.stathreadattribute?view=netframework-4.8</a></p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4e63414200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="STA" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4e63414200b img-responsive" src="/assets/image_475500.jpg" title="STA" /></a></p>
<p>So, whatever thread you are creating and using your <a href="http://help.autodesk.com/view/INVNTOR/2018/ENU/?guid=GUID-255C01D4-EF33-4E40-B425-FB588D7C4B7A"><strong>ApprenticeServerComponent</strong></a> from, it needs to be a <strong>single-threaded apartment</strong> type. You can simply add the <strong>[STAThread]</strong> attribute to your application&#39;s <strong>Main</strong> function:</p>
<pre>namespace MyApp
{
  static class Program
  {
    <strong><span style="color: #ff0000;">[STAThread]</span></strong>
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MyForm());
    }
  }
}</pre>
<p>As a background info, <strong>Windows Form</strong> components (like <strong>AxHost</strong>)&#0160;require the use of <strong>single-threaded apartment</strong>. As mentioned in the <a href="https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-3.0/ms182351(v=vs.80)?redirectedfrom=MSDN#rule-description">Rule Description</a>:<br />&quot;... if it is omitted, the Windows components might not work correctly.&#0160;&quot;</p>
<p><br />&#0160;&#0160;</p>

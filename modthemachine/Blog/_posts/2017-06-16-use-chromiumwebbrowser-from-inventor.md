---
layout: "post"
title: "Use ChromiumWebBrowser from Inventor"
date: "2017-06-16 14:32:06"
author: "Adam Nagy"
categories:
  - "Adam"
  - "C#"
  - "Inventor"
original_url: "https://modthemachine.typepad.com/my_weblog/2017/06/use-chromiumwebbrowser-from-inventor.html "
typepad_basename: "use-chromiumwebbrowser-from-inventor"
typepad_status: "Publish"
---

<p><strong>ChromiumWebBrowser</strong> enables you to have a browser component in your app with <strong>Chrome</strong> capabilities. It requires an external process as well called&#0160;<strong>CefSharp.BrowserSubprocess.exe</strong>, which is generated as part of your output by the&#0160;<strong>CefSharp.WinForms NuGet</strong> component that you need to add to your project.</p>
<p>In case of having a <strong>standalone application</strong> the default settings are good enough because all the components will be created in the same output path next to your <strong>project&#39;s exe</strong>. However, in case of an <strong>Inventor</strong> <strong>add-in</strong> (or any other add-in for that matter) the add-in files will be located somewhere other than the <strong>main exe</strong> (in this case <strong>Inventor.exe</strong>), so you need to adjust the&#0160;<strong>CefSettings</strong> accordingly.</p>
<pre>private void InitializeChromium()
{
    CefSettings settings = new CefSettings();
    // Get the folder path of the add-in dll
    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    settings.BrowserSubprocessPath = Path.Combine(assemblyFolder, &quot;CefSharp.BrowserSubprocess.exe&quot;);
    Cef.Initialize(settings);

    ChromeWebBrowser = new ChromiumWebBrowser(&quot;http://www.google.com&quot;);
    ChromeWebBrowser.LoadError += ChromeWebBrowser_LoadError;

    this.Controls.Add(ChromeWebBrowser);
    ChromeWebBrowser.Dock = DockStyle.Fill;
}</pre>
<p>In this case you&#39;ll see that the sub-process gets started successfully:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d28d4dd7970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ChromiumBackgroundProcess" class="asset  asset-image at-xid-6a00e553fcbfc6883401b8d28d4dd7970c img-responsive" src="/assets/image_526505.jpg" title="ChromiumBackgroundProcess" /></a></p>
<p>And the <strong>Chromium</strong> browser component seems to work fine:</p>
<p><a class="asset-img-link" href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401bb09a6289c970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="InventorChromium" class="asset  asset-image at-xid-6a00e553fcbfc6883401bb09a6289c970d img-responsive" src="/assets/image_721029.jpg" title="InventorChromium" /></a></p>
<p>-Adam</p>

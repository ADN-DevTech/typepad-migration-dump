---
layout: "post"
title: "Integrating WebView2 with AutoCAD: A Rich UI for Data Visualization"
date: "2025-02-27 19:18:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2025/02/integrating-webview2-with-autocad-a-rich-ui-for-data-visualization.html "
typepad_basename: "integrating-webview2-with-autocad-a-rich-ui-for-data-visualization"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p dir="auto">AutoCAD&#39;s native UI is powerful, but sometimes we need <strong>modern, interactive, and web-based interfaces</strong> to enhance productivity.</p>
<p dir="auto">This sample demonstrates how to integrate <strong>WebView2</strong> inside AutoCAD using .NET, enabling developers to display rich HTML dashboards and visualize data extracted from AutoCAD drawings.</p>
<p dir="auto"><a href="/assets/image_690780.jpg"> <img alt="WebView-Data" border="0" height="391" src="/assets/image_690780.jpg" style="display: inline; background-image: none;" title="WebView-Data" width="747" /> </a></p>
<p dir="auto">&#0160;</p>
<div class="markdown-heading" dir="auto">
<h2 class="heading-element" dir="auto" tabindex="-1"><strong>Data Extraction Overview</strong></h2>
<a class="anchor" href="https://github.com/MadhukarMoogala/AcadWebView/tree/main#data-extraction-overview" id="user-content-data-extraction-overview"></a></div>
<p dir="auto">The extracted data is linked to an AutoCAD drawing. In this example, we use the <strong>AutoCAD 2025 Sample Drawing</strong>:<br /><code>\AutoCAD 2025\Sample\Mechanical Sample\Data Extraction and Multileaders Sample.dwg</code><br />This sample contains the <code>Grill Schedule.dxe</code> file, which stores extracted data in a <strong>binary format</strong>.</p>
<p dir="auto">Since <code>.dxe</code> files are binary, we use a new tool shipped with AutoCAD 2025 called <strong><code>BFMigrator.exe</code></strong> to convert them into a <strong>human-readable JSON file (<code>.dxex</code>)</strong>.</p>
<p dir="auto">The converted <code>.dxex</code> file is then parsed and sent to an embedded WebView2 instance, where a <strong>dynamic HTML dashboard</strong> is generated.</p>
<div class="markdown-heading" dir="auto">
<h3 class="heading-element" dir="auto" tabindex="-1">DXE to JSON Conversion</h3>
<a class="anchor" href="https://github.com/MadhukarMoogala/AcadWebView/tree/main#dxe-to-json-conversion" id="user-content-dxe-to-json-conversion"></a></div>
<p dir="auto">Below is the C# code snippet used to convert the binary <code>.dxe</code> file into a JSON <code>.dxex</code> file using <code>BFMigrator.exe</code>:</p>
<pre class="prettyprint lang-csharp"><code>
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = BFMigrator,
            Arguments = $&quot;\&quot;{dxeFile}\&quot; \&quot;{dxexFile}\&quot;&quot;,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        Process process = new Process { StartInfo = psi };
        process.Start();
        process.WaitForExit(); // Ensure migration is complete
      </code></pre>
<h3 dir="auto">Communication Between AutoCAD Plugin and WebView2:</h3>
<p>When embedding WebView2 in AutoCAD, we need to initialize the browser instance, configure it, and navigate to our HTML dashboard. And stores WebView2 cache/data locally instead of a system-protected folder.</p>
<ol>
<li>Initialize WebView2 environment</li>
<li>Ensure WebView2 is ready before setting the Source</li>
<li>Set the Source URL</li>
<li>Send JSON data to WebView2</li>
</ol>
<p>Below is the C# code snippet used to initialize WebView2, set the Source URL, and send JSON data to WebView2:</p>
<pre class="prettyprint lang-csharp"><code>
  string appDataFolderACAD = System.IO.Path.Combine(
  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
  &quot;AcadWebView&quot;
    );
    Directory.CreateDirectory(appDataFolderACAD);

    // Create WebView2 environment
    var env = await CoreWebView2Environment.CreateAsync(null,
      appDataFolderACAD);

    // Ensure WebView2 is ready before setting Source
    await Wv.EnsureCoreWebView2Async(env);
    Wv.CoreWebView2.Settings.IsWebMessageEnabled = true;

    // Now that WebView2 is initialized, set the Source URL
    Wv.CoreWebView2.Navigate(new Uri(_source).AbsoluteUri);
    Wv.CoreWebView2.NavigationCompleted += async (sender, args) =&gt;
    {
        if (args.IsSuccess)
        {
            await SendJsonToWebView();
        }
        else
        {
            Wv.CoreWebView2.PostWebMessageAsJson(
                &quot;{\&quot;error\&quot;: \&quot;Failed to load the web page\&quot;}&quot;
            );
        }
    };
    </code></pre>
<p>We will use <code>PostWebMessageAsString</code> to send JSON data to WebView2:</p>
<p>For more information, refer to the</p>
<p>Below is the C# code snippet used to send JSON data to WebView2:</p>
<ol>
<li>Check if the JSON file exists</li>
<li>Read the JSON file content</li>
<li>Parse the JSON content</li>
<li>Send the formatted JSON to WebView2</li>
<li>Handle exceptions</li>
</ol>
<pre class="prettyprint lang-csharp"><code>
      private async Task SendJsonToWebView()
      {
          if (!File.Exists(_json))
          {
              Console.WriteLine(&quot;JSON file not found.&quot;);
              string errorJson = &quot;{\&quot;error\&quot;: \&quot;JSON file not found\&quot;}&quot;;
              Wv.CoreWebView2.PostWebMessageAsString(errorJson);
              return;
          }    
          string jsonContent = await File.ReadAllTextAsync(_json);   
          try
          {
              var parsedJson = 
              System.Text.Json.JsonSerializer.Deserialize&lt;object&gt;(jsonContent);
              string formattedJson = 
              System.Text.Json.JsonSerializer.Serialize(parsedJson);  
              Wv.CoreWebView2.PostWebMessageAsString(formattedJson);
          }
          catch (Exception)
          {
              Wv.CoreWebView2.PostWebMessageAsString(&quot;{
                \&quot;error\&quot;: \&quot;Invalid JSON format\&quot;
              }&quot;);
          }
      }
    </code></pre>
<h3>Receiving Data in WebView2 on JS</h3>
<p>On the HTML side, the WebView2 receives JSON data and dynamically updates the dashboard.</p>
<ol>
<li>Listen for messages from the AutoCAD plugin</li>
<li>Parse the received JSON data</li>
<li>Update the dashboard</li>
</ol>
<pre class="prettyprint lang-js"><code>  
      window.chrome.webview.addEventListener(&quot;message&quot;, event =&gt; {
        try {
            console.log(&quot;Raw WebView2 Message (Before Parsing):&quot;, event.data);    
            // Ensure the received data is parsed correctly
            const jsonData = typeof event.data === &quot;string&quot; ? 
              JSON.parse(event.data) : event.data;
            console.log(&quot;Parsed JSON from C#:&quot;, jsonData);
        }
        catch (error) {
            console.error(&quot;Error parsing JSON:&quot;, error);
        }
      });    
    </code></pre>
<p>For more information, refer to the sample <a href="https://github.com/MadhukarMoogala/AcadWebView" title="GitHub Repo">AcadWebView</a> on Github.</p>

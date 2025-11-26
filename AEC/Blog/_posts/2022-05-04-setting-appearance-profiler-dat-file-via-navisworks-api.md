---
layout: "post"
title: "Setting Appearance Profiler dat file via Navisworks API"
date: "2022-05-04 00:01:56"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2022/05/setting-appearance-profiler-dat-file-via-navisworks-api.html "
typepad_basename: "setting-appearance-profiler-dat-file-via-navisworks-api"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a></p>
<p><span style="font-size: 11pt;"><strong>Description: </strong>The Appearance Profiler allows you to set up custom appearance profiles based on sets (search and selection) and property values. You can use them to color-code objects in the model to differentiate system types and visually identify their status. It can be saved as .dat files and shared between other Autodesk Navisworks users.</span></p>
<p><span style="font-size: 11pt;">Autodesk Navisworks 2023 provides a command for appearance profiles. You can use Navisworks automation API and plugin to set appearance profiler to multiple Navisworks files.</span></p>
<p><span style="font-size: 11pt;"><strong>Sample Code: </strong></span></p>
<pre class="prettyprint">class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            String runtimeName = Resolver.TryBindToRuntime(RuntimeNames.NavisworksManage);
            if (String.IsNullOrEmpty(runtimeName))
            {
                throw new Exception(&quot;Failed to bind to Navisworks runtime&quot;);
            }
            XMain();
        }

        private static void XMain()
        {
            NavisworksApplication navisApp = new NavisworksApplication();
            navisApp.AppendFile(@&quot;....\InputFile.nwd&quot;);
            navisApp.ExecuteAddInPlugin(&quot;AutoAppearanceLoader.Navisworks&quot;, @&quot;....\AppearanceProfiler.dat&quot;);
            navisApp.SaveFile(@&quot;....\OutputFile.nwd&quot;);
            navisApp.Dispose();
        }
    }

</pre>
<p><span style="font-size: 11pt;">The command can also work within plugin:</span></p>
<pre class="prettyprint">Autodesk.Navisworks.Api.Application.Plugins.ExecuteAddInPlugin(&quot;AutoAppearanceLoader.Navisworks&quot;, &quot;c:\\temp\\AppearanceProfiler .dat&quot;);
</pre>

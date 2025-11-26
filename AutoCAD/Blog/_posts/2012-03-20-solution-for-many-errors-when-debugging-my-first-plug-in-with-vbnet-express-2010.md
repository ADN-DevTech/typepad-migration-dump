---
layout: "post"
title: "Solution for many errors when debugging my first plug-in with VB.net Express 2010"
date: "2012-03-20 22:28:08"
author: "Daniel Du"
categories:
  - ".NET"
  - "AutoCAD"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/autocad/2012/03/solution-for-many-errors-when-debugging-my-first-plug-in-with-vbnet-express-2010.html "
typepad_basename: "solution-for-many-errors-when-debugging-my-first-plug-in-with-vbnet-express-2010"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/daniel-du.html" target="_self">Daniel Du</a></p>
<p>If you are new to programming and&#0160;AutoCAD&#0160;.net plug-in development, you will want to check the“<a href="http://www.autodesk.com/myfirstautocadplugin">My First AutoCAD Plug-in</a>”on&#0160;<a href="http://autodesk.com/developautocad">AutoCAD&#0160;Developer Center</a>, which is a self-paced tutorial guide for a smooth introduction into the programming world. It is a &quot;one-stop shop&quot; learning path for users who know&#0160;Autodesk&#0160;products but are absolutely new to programming and are thinking about taking the plunge. In this guide, you will be working with the&#0160;AutoCAD&#0160;.NET Application Programming Interface (API) and the Visual Basic .NET programming language to create a ‘plug-in’ – a module that loads into&#0160;AutoCAD&#0160;to extend its functionality. Once you have finished this tutorial, you will understand the basics of .NET programming and how they are applied to&#0160;AutoCAD.</p>
<p>You can create a simple&#0160;AutoCAD.net&#0160;plug-in&#0160;template with&#0160;<a href="http://images.autodesk.com/adsk/files/AutoCAD_2010-2012_dotNET_Wizards.zip">The AutoCAD 2010-2012 .NET Wizards</a>, it also allows you to launch&#0160;AutoCAD&#0160;from your debugger – something that isn’t possible through the Visual Studio Express user interface.&#0160; If you are using VB.net Express 2010, you many get many errors in Immediate Window when&#0160;debugging, it does not block your debugging process, but it takes time before you can actually start debug and test working.</p>
<p>The error messages are&#0160;similar&#0160;like below:</p>
<p>&#39;----Immediate Window--------------------------------------</p>
<p>System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;ToolBarCustomizeButton&#39;(HashCode=44123454); target element is&#0160;&#39;ToolBarToggleButton&#39;&#0160;(Name=&#39;mCustomizeButton&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#0160;&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;ToolBarCustomizeButton&#39;(HashCode=44123454); target element is&#0160;&#39;ToolBarToggleButton&#39;&#0160;(Name=&#39;mCustomizeButton&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#0160;&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;ToolBarCustomizeButton&#39;(HashCode=44123454); target element is&#0160;&#39;RibbonItemControl&#39;&#0160;(Name=&#39;&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;InfoCenterTextBox&#39;(HashCode=52380055); target element is&#0160;&#39;RibbonItemControl&#39;&#0160;(Name=&#39;&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;InfoCenterTextBox&#39;(HashCode=52380055); target element is&#0160;&#39;Button&#39;&#0160;(Name=&#39;PART_AcceptButton&#39;); target property is&#0160;&#39;Name&#39;(type&#0160;&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;WebServicesLoginButton&#39;(HashCode=720995); target element is&#0160;&#39;LoginButtonRibbonItemControl&#39;&#0160;(Name=&#39;mContainer&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#0160;&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;RibbonToggleButton&#39;(HashCode=43678569); target element is&#0160;&#39;RibbonItemControl&#39;&#0160;(Name=&#39;&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 5 : Value produced by&#0160;BindingExpression&#0160;is not valid for target property.; Value=&#39;&lt;null&gt;&#39;&#0160;BindingExpression:Path=AutomationName;&#0160;DataItem=&#39;WebServicesLoginButton&#39;(HashCode=720995); target element is&#0160;&#39;WebservicesLoginButtonControl&#39;&#0160;(Name=&#39;mDropDownButton&#39;); target property is&#0160;&#39;Name&#39;&#0160;(type&#0160;&#39;String&#39;)&#0160;<br />System.Windows.Data Error: 4 : Cannot find source for binding with reference&#0160;&#39;RelativeSource&#0160;FindAncestor,AncestorType=&#39;Autodesk.Private.Windows.ToolBars.ToolBarControl&#39;,&#0160;AncestorLevel=&#39;1&#39;&#39;.BindingExpression:Path=IsVisible;&#0160;DataItem=null; target element is&#0160;&#39;ImageControl&#39;&#0160;(Name=&#39;mImage&#39;); target property is&#0160;&#39;NoTarget&#39;&#0160;(type&#0160;&#39;Object&#39;)&#0160;<br />System.Windows.Data Error: 4 : Cannot find source for binding with reference &#39;RelativeSource FindAncestor, AncestorType=&#39;Autodesk.Private.Windows.ToolBars.ToolBarControl&#39;, AncestorLevel=&#39;1&#39;&#39;. BindingExpression:Path=IsVisible; DataItem=null; target element is &#39;ImageControl&#39; (Name=&#39;mImage&#39;); target property is &#39;NoTarget&#39; (type &#39;Object&#39;)&#0160;<br />System.Windows.Data Error: 4 : Cannot find source for binding with reference&#0160;&#39;RelativeSource&#0160;FindAncestor,AncestorType=&#39;Autodesk.Private.Windows.ToolBars.ToolBarControl&#39;,&#0160;AncestorLevel=&#39;1&#39;&#39;.BindingExpression:Path=IsVisible;&#0160;DataItem=null; target element is&#0160;&#39;ImageControl&#39;&#0160;(Name=&#39;mImage&#39;); target property is&#0160;&#39;NoTarget&#39;&#0160;(type&#0160;&#39;Object&#39;)&#0160;<br />System.Windows.Data Error: 4 : Cannot find source for binding with reference&#0160;&#39;RelativeSource&#0160;FindAncestor,AncestorType=&#39;Autodesk.Private.Windows.ToolBars.ToolBarControl&#39;,&#0160;AncestorLevel=&#39;1&#39;&#39;.BindingExpression:Path=IsVisible;&#0160;DataItem=null; target element is&#0160;&#39;ImageControl&#39;&#0160;(Name=&#39;mImage&#39;); target property is&#0160;&#39;NoTarget&#39;&#0160;(type&#0160;&#39;Object&#39;)&#0160;<br />System.Windows.Data Error: 4 : Cannot find source for binding with reference&#0160;&#39;RelativeSource&#0160;FindAncestor,AncestorType=&#39;Autodesk.Private.Windows.ToolBars.ToolBarControl&#39;,&#0160;AncestorLevel=&#39;1&#39;&#39;.</p>
<p>The solution is to add&#0160;&lt;system.diagnostics&gt;&#0160;into&#0160;Acad.exe.Config, which can be found in your&#0160;AutoCAD installation folder. Add following section right after &lt;/runtime&gt;:</p>
<pre>    &lt;system.diagnostics&gt;
    &lt;sources&gt;                                                                                                                                                                                      
      &lt;source name=&quot;System.Windows.Data&quot;               
              switchName=&quot;SourceSwitch&quot;&gt;
        &lt;listeners&gt;
          &lt;remove name=&quot;Default&quot; /&gt;
        &lt;/listeners&gt;
      &lt;/source&gt;
    &lt;/sources&gt;
  &lt;/system.diagnostics&gt;</pre>
<p>It will look like below(I am using Civil 3D):</p>
<pre>&lt;configuration&gt;
    &lt;startup useLegacyV2RuntimeActivationPolicy=&quot;true&quot;&gt;
        &lt;supportedRuntime version=&quot;v4.0&quot;/&gt;
    &lt;/startup&gt;
    &lt;!--All assemblies in AutoCAD are fully trusted <br />        so there&#39;s no point generating publisher evidence--&gt;
    &lt;runtime&gt;
        &lt;generatePublisherEvidence enabled=&quot;false&quot;/&gt;
        &lt;assemblyBinding xmlns=&quot;urn:schemas-microsoft-com:asm.v1&quot;&gt;          <br />          &lt;probing privatePath=&quot;bin\FDO;bin;Plugins\Workflow\Activities&quot;/&gt;<br />        &lt;/assemblyBinding&gt;<br />     &lt;/runtime&gt;
        
    &lt;system.diagnostics&gt;
    &lt;sources&gt;                                                                                                                                                                                      
      &lt;source name=&quot;System.Windows.Data&quot;               
              switchName=&quot;SourceSwitch&quot;&gt;
        &lt;listeners&gt;
          &lt;remove name=&quot;Default&quot; /&gt;
        &lt;/listeners&gt;
      &lt;/source&gt;
    &lt;/sources&gt;
  &lt;/system.diagnostics&gt;

&lt;/configuration&gt;</pre>
<p>This solution applies to AutoCAD vertical products as well, including Map 3D and Civil 3D. Hope this helps.</p>
<p>&#0160;</p>

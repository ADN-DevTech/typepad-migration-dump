---
layout: "post"
title: "How to Create a Custom Toggle Button"
date: "2022-11-04 02:33:00"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2022/11/how-to-create-a-custom-toggle-button.html "
typepad_basename: "how-to-create-a-custom-toggle-button"
typepad_status: "Publish"
---

<p>
    <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>
    <font size="2">By </font> <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">
        <font size="2">Madhukar Moogala</font>
    </a>
</p>
<p>
    <font face="Arial" size="2">Here in this blog I have shown how to create a Ribbon Toggle Button using AutoCAD Ribbon Runtime API.</font>
</p>
<p> <a href="https://adndevblog.typepad.com/autocad/2015/03/how-to-use-toggle-button-ribbon-api.html">
        <font face="Arial" size="2">https://adndevblog.typepad.com/autocad/2015/03/how-to-use-toggle-button-ribbon-api.html</font>
    </a> </p>
<p>
    <font face="Arial" size="2"> This code sample shows how can we register a custom ribbon control on AutoCAD Ribbon bar, to illustrate I created a simple toggle button, but however this logic can be extended to any other fancy controls. </font>
</p>
<h5>
    <font face="Arial" size="2">Logic:</font>
</h5>
<ul>
    <li>
        <p>
            <font face="Arial" size="2">Create a </font> <a href="https://github.com/MadhukarMoogala/SimpleToggleButton/blob/master">
                <font face="Arial" size="2">ResourceDictionary </font>
            </a>
            <font face="Arial" size="2">to hold resources and controls in XAML</font>
        </p>
    </li>
    <li>
        <p>
            <font face="Arial" size="2">Implement the binding logic to manage the state of ToggleButton.</font>
        </p>
        <ul>
            <li>
                <font face="Arial" size="2">Create a SystemVariable to hold the state of ToggleButton</font>
            </li>
        </ul>
        <pre class="prettyprint lang-csh"><font size="2">    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsChecked(value);
        }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOn = (bool)value;
            if (isOn &amp;&amp; !string.IsNullOrEmpty(OnMacro))
                RunMacro(OnMacro);
            else if (!isOn &amp;&amp; !string.IsNullOrEmpty(OffMacro))
                RunMacro(OffMacro);
            return DataBindings.DoNothing;
        }</font>

    </pre>
    </li>
    <li>
        <p>
            <font face="Arial" size="2">Load this XAML in to AutoCAD Runtime, so this custom dictionary gets merged in to the AutoCAD main resource dictionary and you will see a unfied UX.</font>
        </p>
    </li>
    <li>
        <p>
            <font face="Arial" size="2">If you are preparing CUIX using CUI editor, make sure, the <code>key </code>of ToggleButton should be same as the <code>Id</code> of ToggleButton in CUI. For example "XyzToggleButton" </font><font size="2">
        </font></p><font size="2">
    </font></li><font size="2">
</font></ul><font size="2">
</font><pre class="prettyprint lang-csh"><font size="2">	    string menuName = (string)Application.GetSystemVariable("MENUNAME");           
            CustomizationSection cs = new CustomizationSection(menuName + ".cuix");
            var ribbonRoot = cs.MenuGroup.RibbonRoot;
            var homeTab = ribbonRoot.FindTab("ID_TabHome");
            var elementId = "ID_TogglePanel";
            var ribbonPanelSourceReference = homeTab.Find(elementId);
            if(ribbonPanelSourceReference is null)
            {
                var panel = homeTab.AddNewPanel(elementId, "TogglePanel");
                var row = panel.AddNewRibbonRow();
                row.AddNewToggleButton("XyzToggleButton", "XYZSTATE\nToggle", null, RibbonButtonStyle.LargeWithText);
                cs.Save();
            }</font>
</pre>



<pre style="background: rgb(0, 0, 0); color: rgb(209, 209, 209);"><font size="2"><span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton</span> 
        <span style="color: rgb(0, 121, 151);">x</span><span style="color: rgb(176, 96, 176);">:</span>Uid<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">RibbonToggleButton-Xyz</span><span style="color: rgb(2, 208, 69);">"</span> 
        <span style="color: rgb(0, 121, 151);">x</span><span style="color: rgb(176, 96, 176);">:</span>Key<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">XyzToggleButton</span><span style="color: rgb(2, 208, 69);">"</span>
        Name<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">XYZ Toggle</span><span style="color: rgb(2, 208, 69);">"</span>
        Tag<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">XYZSTATE</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">&gt;</span>
        <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton.IsCheckedBinding</span><span style="color: rgb(255, 137, 6);">&gt;</span>
            <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">Binding</span> Source<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">{x:Static acmgd:Application.UIBindings}</span><span style="color: rgb(2, 208, 69);">"</span> Path<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">SystemVariables[XYZSTATE].Value</span><span style="color: rgb(2, 208, 69);">"</span> Converter<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">{StaticResource XyzToggleButtonConverter}</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">/&gt;</span>
        <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton.IsCheckedBinding</span><span style="color: rgb(251, 132, 0);">&gt;</span>
        <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton.Image</span><span style="color: rgb(255, 137, 6);">&gt;</span>
            <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">BitmapImage</span> <span style="color: rgb(0, 121, 151);">x</span><span style="color: rgb(176, 96, 176);">:</span>Uid<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">BitmapImage_1</span><span style="color: rgb(2, 208, 69);">"</span> UriSource<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">Resources/Toggle.bmp</span><span style="color: rgb(2, 208, 69);">"</span> <span style="color: rgb(255, 137, 6);">/&gt;</span>
        <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton.Image</span><span style="color: rgb(251, 132, 0);">&gt;</span>
        <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton.LargeImage</span><span style="color: rgb(255, 137, 6);">&gt;</span>
            <span style="color: rgb(255, 137, 6);">&lt;</span><span style="color: rgb(246, 193, 208);">BitmapImage</span> <span style="color: rgb(0, 121, 151);">x</span><span style="color: rgb(176, 96, 176);">:</span>Uid<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">BitmapImage_2</span><span style="color: rgb(2, 208, 69);">"</span> UriSource<span style="color: rgb(210, 205, 134);">=</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(0, 196, 196);">Resources/Toggle.bmp</span><span style="color: rgb(2, 208, 69);">"</span><span style="color: rgb(255, 137, 6);">/&gt;</span>
        <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton.LargeImage</span><span style="color: rgb(251, 132, 0);">&gt;</span>
    <span style="color: rgb(251, 132, 0);">&lt;/</span><span style="color: rgb(0, 221, 221);">adw</span><span style="color: rgb(176, 96, 176);">:</span><span style="color: rgb(246, 193, 208);">RibbonToggleButton</span><span style="color: rgb(251, 132, 0);">&gt;</span></font>
</pre>


<p><font size="2">Complete sample with source code is available at Github : </font><a href="https://github.com/MadhukarMoogala/SimpleToggleButton" target="_blank"><font size="2">SimpleToggleButton</font></a></p>

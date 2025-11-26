---
layout: "post"
title: "Setting up a timer in a .NET sample that uses the AutoCAD .NET API"
date: "2012-06-12 00:16:32"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/setting-up-a-timer-in-a-net-sample-that-uses-the-autocad-net-api.html "
typepad_basename: "setting-up-a-timer-in-a-net-sample-that-uses-the-autocad-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>&nbsp;If you&nbsp;want your AutoCAD .NET plug-in to perform some tasks at regular intervals, then it is important to ensure that the timer runs in the AutoCAD's main UI thread. Implementing the timer in a separate thread does not work because AutoCAD (atleast until 2013 release)&nbsp;does not support multithreaded applications.</p>
<p>To setup a timer to work properly, one possible way is to use a hidden modeless window that in-turn instantiates a timer (use the System.Windows.Forms.Timer). The System.Windows.Forms.Timer&nbsp;differs from the other timers provided by the .Net framework&nbsp;and&nbsp;is guranteed to run in the same UI thread which&nbsp;started it. For a comparison of the various timers available in the .Net framework,&nbsp;you may refer to <a href="http://msdn.microsoft.com/en-us/magazine/cc164015.aspx">this</a> MSDN magazine article.</p>
<p>Here is the sample code that displays a tray bubble at a specified time interval :</p>
<p></p>
<p>First lets look at the commands class</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Windows</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&lt;Assembly: CommandClass(</span><span style="color: blue; line-height: 140%;">GetType</span><span style="line-height: 140%;">(AdskMyTimerClass))&gt; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span><span style="line-height: 140%;"> AdskMyTimerClass</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Implements</span><span style="line-height: 140%;"> IExtensionApplication</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'System.Threading.Timer is a simple, lightweight timer that </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'uses callback methods and is served by thread pool threads. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'It is not recommended for use with Windows Forms, because </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'its callbacks do not occur on the user interface thread. </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'System.Windows.Forms.Timer is a better choice for use with </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'Windows Forms and its callbacks occur on the same UI thread. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> _timerForm </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> MyForm</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> Initialize() _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Implements</span><span style="line-height: 140%;"> IExtensionApplication.Initialize</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> Terminate() _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Implements</span><span style="line-height: 140%;"> IExtensionApplication.Terminate</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; StopTimerMethod()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &lt;CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;StartTimer&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> StartTimerMethod()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> _timerForm </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> MyForm</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm.Show()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm.Hide()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &lt;CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;StopTimer&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> StopTimerMethod()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> _timerForm </span><span style="color: blue; line-height: 140%;">IsNot</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm.Close()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm.Dispose()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _timerForm = </span><span style="color: blue; line-height: 140%;">Nothing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span></p>
</div>
<p></p>
<p>Now for the Form class that creates the timer :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Windows</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span><span style="line-height: 140%;"> MyForm</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> _myTimer </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Windows.Forms.Timer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> _ti </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> TrayItem</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> _icon </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Drawing.Icon</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> _cnt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Int32</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> MyForm_Load( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Object, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.EventArgs) _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">MyBase</span><span style="line-height: 140%;">.Load</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _myTimer = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> System.Windows.Forms.Timer()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _myTimer.Enabled = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _myTimer.Interval = 5000</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">AddHandler</span><span style="line-height: 140%;"> _myTimer.Tick, </span><span style="color: blue; line-height: 140%;">AddressOf</span><span style="line-height: 140%;"> MyBubbleTimer_Tick</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _myTimer.Start()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> MyBubbleTimer_Tick( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Object, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.EventArgs</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _cnt = _cnt + 1</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> sb </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> StatusBar = Application.StatusBar</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> tis </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> TrayItemCollection = sb.TrayItems</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> _ti </span><span style="color: blue; line-height: 140%;">Is</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _cnt = 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _ti = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> TrayItem()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _ti.Visible = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _icon = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> System.Drawing.Icon(</span><span style="color: #a31515; line-height: 140%;">&quot;C:\\Temp\\spintest.ico&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _ti.Icon = Icon</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _ti.ToolTipText = </span><span style="color: #a31515; line-height: 140%;">&quot;My Timer trigerred bubble !&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tis.Add(_ti)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; sb.Update()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _ti.CloseBubbleWindows()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> bubble </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> TrayItemBubbleWindow()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; bubble.Text = </span><span style="color: #a31515; line-height: 140%;">&quot;Hello !&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; bubble.Title = </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;">.Format(</span><span style="color: #a31515; line-height: 140%;">&quot;{0}&quot;</span><span style="line-height: 140%;">, _cnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; bubble.HyperLink = </span><span style="color: #a31515; line-height: 140%;">&quot;http://www.autodesk.com&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; bubble.HyperText = </span><span style="color: #a31515; line-height: 140%;">&quot;Autodesk&quot;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _ti.ShowBubbleWindow(bubble)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> MyForm_FormClosed( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Object, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> System.Windows.Forms.FormClosedEventArgs _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ) </span><span style="color: blue; line-height: 140%;">Handles</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">MyBase</span><span style="line-height: 140%;">.FormClosed</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">' Stop the timer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _myTimer.Stop()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _myTimer.Dispose()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">' Remove the tray item</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> _ti </span><span style="color: blue; line-height: 140%;">IsNot</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Nothing</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> sb </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> StatusBar = Application.StatusBar</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> tis </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> TrayItemCollection = sb.TrayItems</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tis.Remove(_ti)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; sb.Update()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span></p>
</div>

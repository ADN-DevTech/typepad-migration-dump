---
layout: "post"
title: "RevitAPI: Revit 2016 Events register/unregister behavior change"
date: "2015-09-14 19:45:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/09/revitapi-revit-2016-events-registerunregister-behavior-change.html "
typepad_basename: "revitapi-revit-2016-events-registerunregister-behavior-change"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/48312281">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
<p>In previous Revit version, we can register/unregister an event in a modeless dialog, but now Revit 2016 changed the behavior of Events, it is no longer allowed to do so, otherwise exception will be thrown, if that exception is not caught by your application, Revit may crash.</p>
<p>Revit 2016 API changes document says:</p>
<h2>API events - behavioral change</h2>
<p>Although the Revit API has never officially supported such a work-flow it is now enforced that registering to and unregistering from events must happen while executing on the main thread. An exception will be thrown if an external application attempts to register to (or unregister from) events from outside of valid API context.</p>
<p>The solutions can be:</p>
<ul>
<li>Use model dialog, or make sure you register/unregister events in the Execute function of IExternalCommand or OnStartup/OnShutdown function of IExternalApplication.</li>
<li>If you must use modeless dialog, you can use ExternalEvent.Raise() method, force the context switch to Revit main thread, and in the corresponding Execute method of IExternalEventHandler, register/unregister your events.</li>
</ul>
<div>&#0160;</div>
<h2>Code examples</h2>
<div>First, have a class implementing IExternalEventHandler, and in its Execute method, register a event, e.g. Application.DocumentChanged.</div>
<div>
<pre class="csharp prettyprint">public class EventRegisterHandler : IExternalEventHandler
{
    public void Execute(UIApplication app)
    {
        app.Application.DocumentChanged += Application_DocumentChanged;
    }

    void Application_DocumentChanged(object sender, Autodesk.Revit.DB.Events.DocumentChangedEventArgs e)
    {
        // do your stuff
    }

    public string GetName()
    {
        return &quot;EventRegisterHandler&quot;;
    }
}</pre>
When you want to make it happen, just create an instance of ExternalEvent, and call its Raise() method. e.g. in a modeless dialog, clicking a button to cause the registration of the event DocumentChanged. Code:</div>
<div>
<pre class="csharp prettyprint">private void button1_Click(object sender, EventArgs e)
{
    ExternalEvent _exEvent = null;
    EventRegisterHandler _exEventHandler = null;
    _exEventHandler = new EventRegisterHandler();
    _exEvent = ExternalEvent.Create(_exEventHandler);
    _exEvent.Raise();
}</pre>
</div>
<h3>Edited on 2016/3/8</h3>
<p>Someone found the above code &quot;ExternalEvent.Create&quot;&#0160;method throws&#0160;<strong>Autodesk.Revit.Exceptions.InvalidOperationException: Attempting to create an ExternalEvent outside of a standard API execution, </strong>that is a true bug in my code, sorry for that :-(. And the solution is easy: do not call&#0160;&quot;ExternalEvent.Create&quot; in modeless dialog but in &quot;IExternalCommand.Execute&quot; or &quot;IExternalApplication.OnStartup&quot;. Following is a full code example showing that: Run ExternalCommand &quot;EventRegistrationInModelessDialogViaExternalEvent&quot; and then click the button in the modeless dialog to register or unregister DocumentChanged event.&#0160;</p>
<pre class="csharp prettyprint">using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;

namespace TestScript
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class EventRegistrationInModelessDialogViaExternalEvent 
        : IExternalCommand
    {
        public Document doc;
        public Autodesk.Revit.ApplicationServices.Application RevitApp;
        ExternalEvent _exEvent;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            EventRegisterHandler _exeventHander = new EventRegisterHandler();
            _exEvent = ExternalEvent.Create(_exeventHander);
            MyForm form = new MyForm();
            form.ExEvent = _exEvent;
            form.Show();

            return Result.Succeeded;
        }
    }

    public class MyForm : System.Windows.Forms.Form
    {
        public MyForm()
            : base()
        {
            Button btn = new Button();
            btn.Text = &quot;Toggle DocumentChanged Event Registration&quot;;
            btn.Click += btn_Click;
            btn.Width = 250;
            this.Controls.Add(btn);
        }

        public ExternalEvent ExEvent { get; set; }

        void btn_Click(object sender, EventArgs e)
        {
            if (ExEvent != null)
                ExEvent.Raise();
            else
                MessageBox.Show(&quot;external event handler is null&quot;);
        }
    }

    public class EventRegisterHandler : IExternalEventHandler
    {
        public bool EventRegistered { get; set; }
        public void Execute(UIApplication app)
        {
            if (EventRegistered)
            {
                EventRegistered = false;
                app.Application.DocumentChanged -= Application_DocumentChanged;
            }
            else
            {
                EventRegistered = true;
                app.Application.DocumentChanged += Application_DocumentChanged;
            }
        }

        void Application_DocumentChanged(object sender, 
            Autodesk.Revit.DB.Events.DocumentChangedEventArgs e)
        {
            var sb = new StringBuilder();
            var added = &quot;added:&quot; + e.GetAddedElementIds()
                .Aggregate(&quot;&quot;, (ss, el) =&gt; ss + &quot;,&quot; + el).TrimStart(&#39;,&#39;);
            var modified = &quot;modified:&quot; + e.GetModifiedElementIds()
                .Aggregate(&quot;&quot;, (ss, el) =&gt; ss + &quot;,&quot; + el).TrimStart(&#39;,&#39;);
            var deleted = &quot;deleted:&quot; + e.GetDeletedElementIds()
                .Aggregate(&quot;&quot;, (ss, el) =&gt; ss + &quot;,&quot; + el).TrimStart(&#39;,&#39;);
            sb.AppendLine(added);
            sb.AppendLine(modified);
            sb.AppendLine(deleted);
            TaskDialog.Show(&quot;Changes&quot;, sb.ToString());
        }

        public string GetName()
        {
            return &quot;EventRegisterHandler&quot;;
        }
    }
}

</pre>

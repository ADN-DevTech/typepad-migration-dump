---
layout: "post"
title: "Revit 2018.3, SetValueString and External Events"
date: "2018-04-10 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2018"
  - "Algorithm"
  - "Events"
  - "External"
  - "Parameters"
  - "Python"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/04/revit-20183-setvaluestring-and-external-events.html "
typepad_basename: "revit-20183-setvaluestring-and-external-events"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>I installed the latest Revit update, and want to highlight two of the numerous interesting ongoing Revit API forum discussions:</p>

<ul>
<li><a href="#2">Revit 2018.3 update</a> </li>
<li><a href="#3">Avoid <code>SetValueString</code></a> </li>
<li><a href="#4">Passing Data via <code>ExternalEvent.Raise</code></a></li>
</ul>

<h4><a name="2"></a>Revit 2018.3 Update</h4>

<p>I installed the Revit 2018.3 update released yesterday, April 9, 2018, with the build numbers 20180329_1100 and 18.3.0.81 specified in Help &gt; About.
Here is
the <a href="http://up.autodesk.com/2018/RVT/9CA05B9A-1809-0510-A1B4-98696E090436/Autodesk_Revit_2018_3.exe">direct download link</a>.</p>

<p>This update is required for Revit 2018 to work in the new BIM 360 Design platform.</p>

<p>Furthermore, it includes other new functionality and contains the fixes included in the previous Revit hot fixes and updates. It updates Revit 2018 itself, Collaboration for Revit 2018, and Dynamo. </p>

<p>For details, please check
the <a href="https://up.autodesk.com/2018/RVT/Autodesk_Revit_2018_3_Readme.htm">readme</a>
and <a href="http://revit.downloads.autodesk.com/download/2018_3_RVT/Docs/RelNotes/Autodesk_Revit_2018_3_ReleaseNotes.html">release notes</a>.</p>

<p>I extracted the following API relevant items from the latter:</p>

<ul>
<li>Resolved Issues
<ul>
<li>Platform ...</li>
<li>Architecture ...</li>
<li>MEP ...</li>
<li>Structure ...</li>
<li>API: Improved stability when printing sheets through the API.</li>
</ul></li>
<li>Improvements: 
<ul>
<li>Added connection to Next Gen BIM 360 for cloud worksharing, which allows flexible access permissions for cloud workshared models within the same project.</li>
<li>Updated the Scope Box drop-down list in the Properties Panel to display the scope boxes in alpha-numeric order.</li>
<li>Improved the pipe sizing feature by basing the size of a single pipe attached with taps or analytical pipe connections on the worst case.</li>
</ul></li>
</ul>

<p>More information on improvements can be found in
the <a href="http://help.autodesk.com/view/RVT/2018/ENU/?guid=GUID-C81929D7-02CB-4BF7-A637-9B98EC9EB38B">What's New section</a> of
the Revit 2018 online help.</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb0a02ec2e970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb0a02ec2e970d image-full img-responsive" alt="About Revit 2018.3" title="About Revit 2018.3" src="/assets/image_5d51d1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a>Avoid SetValueString</h4>

<p>Returning to some pure Revit API issues, I generally recommend avoiding the use of <code>SetValueString</code> to control a parameter value, since I find its behaviour somewhat unpredictable.</p>

<p><code>GetValueString</code> returns a string representing the parameter value the way Revit thinks a user would expect to see it. For instance, it might convert an element id to the associated element name.
Trying to use that approach to set an element idea sounds like a really bad idea to me.</p>

<p>This question came up in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/editing-document-on-documentsaving-documentprinting/m-p/7913700">editing a document on <code>DocumentSaving</code> and <code>DocumentPrinting</code></a>:</p>

<p><strong>Question:</strong> Just before printing/saving I would like to save the smallest view scale on all sheets. This is done to circumvent the "As indicated" scale text in the title block once you add a legend to a sheet. We just want to display the scale of the view, not the legend. In case of multiple views, the smallest scale.</p>

<p>I'm trying to modify all sheets on catching these events by overriding a shared parameter. I can read the existing value, but not set a new one.</p>

<p>I tried with/without a transaction with no difference. Do these events allow modifications of the document?</p>

<p><strong>Answer:</strong> I checked
the <a href="http://www.revitapidocs.com/2018.1/86546cf5-eb2f-ffd7-3931-fc361f1264e2.htm"><code>DocumentPrinting</code> event documentation</a>.</p>

<p>It clearly states:</p>

<blockquote>
  <p>This event is raised when Revit is just about to print a view or ViewSet of the document.</p>
  
  <p>Handlers of this event are permitted to make modifications to any document (including the active document), except for documents that are currently in read-only mode.</p>
  
  <p>Event is cancellable. To cancel it, call the 'Cancel()' method of event's argument to True. Your application is responsible for providing feedback to the user about the reason for the cancellation.</p>
</blockquote>

<p>Therefore, either something is wrong with your code, or the documentation needs fixing.</p>

<p>Oh no, re-reading your code, I see another possible issue:</p>

<p>You are using <code>SetValueString</code>.</p>

<p>Are you sure that that works at all?</p>

<p>Please implement access to the <code>ManageSheetScale</code> method from an external command that you call manually yourself from the user interface just before printing or saving and ensure that this really works.</p>

<p>I recommend never using <code>SetValueString</code>.</p>

<p>Try to use the appropriate override of the <code>Parameter.Set</code> method instead.</p>

<p>Then you really know what you are doing.</p>

<p>With <code>SetValueString</code>, you have no idea.</p>

<h4><a name="4"></a>Passing Data via ExternalEvent.Raise</h4>

<p>Another interesting discussion ensued
about <a href="https://forums.autodesk.com/t5/revit-api-forum/externalevent-raise-should-accept-parameters/m-p/7912106"><code>ExternalEvent.Raise</code> should accept parameters</a>:</p>

<p><strong>Question:</strong> I would like to pass additional custom data when raising an external event, e.g., by passing in an own object.</p>

<p>It would be nice to have generics involved here, for instance, having an interface like this:</p>

<pre class="code">
  IExternalEventHandler&lt;T&gt;

  ExternalEvent&lt;T&gt; EE =  ExternalEvent.Create(
    handler IExternalEventHandler&lt;T&gt;);

  EE.Raise(T obj);
</pre>

<p><strong>Answer:</strong> Cyril Waechter solved this issue in Python and says:</p>

<p>I think I did something similar in Python.</p>

<p>Not a perfect solution, but well enough until I find better.</p>

<p>See
my <a href="http://pythoncvc.net">Python HVAC</a> article
on <a href="http://pythoncvc.net/?p=294">Revit batch view renaming, regular expressions, docstrings and a GUI</a>
and <a href="https://github.com/CyrilWaechter/pyRevitMEP/blob/master/pyRevitMEP.tab/Tools.panel/ViewRename.pushbutton/script.py">full Git6Hub code sample</a>:</p>

<p>Here is an extract:</p>

<pre class="prettyprint">
class CustomizableEvent:
  def __init__(self):
    self.function_or_method = None
    self.args = ()
    self.kwargs = {}

  def raised_method(self):
    """
    Method executed by IExternalEventHandler.Execute when ExternalEvent is raised by ExternalEvent.Raise.
    """
    self.function_or_method(*self.args, **self.kwargs)

  def raise_event(self, function_or_method, *args, **kwargs):
    """
    Method used to raise an external event with custom function and parameters
    Example :
    &gt;&gt;&gt; customizable_event = CustomizableEvent()
    &gt;&gt;&gt; customizable_event.raise_event(rename_views, views_and_names)
    """
    self.args = args
    self.kwargs = kwargs
    self.function_or_method = function_or_method
    custom_event.Raise()


customizable_event = CustomizableEvent()

# Create a subclass of IExternalEventHandler

class CustomHandler(IExternalEventHandler):
  """Input : function or method. Execute input in a IExternalEventHandler"""

  # Execute method run in Revit API environment.
  # noinspection PyPep8Naming, PyUnusedLocal
  def Execute(self, application):
    try:
      customizable_event.raised_method()
    except InvalidOperationException:
      # If you don't catch this exeption Revit may crash.
      print "InvalidOperationException catched"

  # noinspection PyMethodMayBeStatic, PyPep8Naming
  def GetName(self):
    return "Execute an function or method in a IExternalHandler"


# Create an handler instance and his associated ExternalEvent

custom_handler = CustomHandler()
custom_event = ExternalEvent.Create(custom_handler)
</pre>

<p>Mark Vulfson added a C# implementation as well, saying:</p>

<blockquote>
  <p>You can work around this by wrapping the Revit event.</p>
  
  <p>For example, you can do the following:</p>
</blockquote>

<pre class="code">
abstract public class RevitEventWrapper&lt;T&gt;
  : IExternalEventHandler 
{
  private object @lock;
  private T savedArgs;
  private ExternalEvent revitEvent;

  public RevitEventWrapper() 
  {
    revitEvent = ExternalEvent.Create(this);
    @lock = new object();
  }

  public void Execute(UIApplication app) 
  {
    T args;

    lock (@lock)
    {
      args = savedArgs;
      savedArgs = default(T);
    }
    Execute(app, args);
  }

  public string GetName()
  {
    return GetType().Name;
  }

  public void Raise(T args)
  {
    lock (@lock)
    {
      savedArgs = args;
    }
    revitEvent.Raise();
  }

  abstract public void Execute(
    UIApplication app, T args );
}
</pre>

<p></p>

<p>Then, you can implement a handler that takes arguments, like so:</p>

<pre class="code">
  public class EventHandlerWithStringArg
    : RevitEventWrapper&lt;string&gt;
  {
    public override void Execute(
      UIApplication uiApp,
      string args )
    {
      // Do your processing here with "args"
    }
  }
</pre>

<p>Finally, you can raise your event like this</p>

<pre class="code">
  EventHandlerWithStringArg myEvent
    = new EventHandlerWithStringArg();
  .
  .
  .
  myEvent.Raise( "this is an argument" );
</pre>

<p></p>

<p>There are threading pitfalls, of course, but these are outside the scope of this answer; this is the best you can do given Revit current architecture.</p>

<p>Many thanks to Cyril and Mark for these nice and powerful extension suggestions!</p>

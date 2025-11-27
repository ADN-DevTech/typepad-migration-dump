---
layout: "post"
title: "Communicate with add-in from custom web page"
date: "2024-02-09 15:30:59"
author: "Adam Nagy"
categories:
  - "Adam"
  - "C#"
  - "Inventor"
  - "JavaScript"
original_url: "https://modthemachine.typepad.com/my_weblog/2024/02/communicate-with-add-in-from-custom-web-page.html "
typepad_basename: "communicate-with-add-in-from-custom-web-page"
typepad_status: "Publish"
---

<p>This does not have much to do with <strong>Inventor</strong>, it&#39;s simply about using a web browser component (<a href="https://cefsharp.github.io/">CefSharp</a>,&#0160;<a href="https://developer.microsoft.com/en-us/microsoft-edge/webview2/">WebView2</a> or something else) and send messages from the displayed website to the app.</p>
<p>I created a very basic sample to prove this works and used <strong>WebView2</strong> just like my colleague for the <a href="https://adndevblog.typepad.com/manufacturing/2020/12/using-microsoft-edge-webview2-in-a-detailpanetab.html">Vault add-in sample</a>.</p>
<p>Took the <strong>SimpleAddIn</strong> sample available in the <strong>Inventor SDK</strong> under &quot;DeveloperTools\Samples\VCSharp.NET\AddIns\SimpleAddIn&quot;, added a <strong>Form</strong> to it that includes a <strong>WebView2</strong> component.</p>
<pre>using System.Windows.Forms;

namespace SimpleAddIn
{
    public partial class MyBrowserForm : Form
    {
        Inventor.Application _app;

        public MyBrowserForm(Inventor.Application app)
        {
            _app = app;

            // This seems to be needed
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            System.Environment.SetEnvironmentVariable(&quot;WEBVIEW2_USER_DATA_FOLDER&quot;, path);

            InitializeComponent();

            myWebView2.WebMessageReceived += MyWebView2_WebMessageReceived;
            myWebView2.CoreWebView2InitializationCompleted += MyWebView2_CoreWebView2InitializationCompleted;
            myWebView2.EnsureCoreWebView2Async();
        }

        private void MyWebView2_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            var htmlContent = @&quot;
&lt;html&gt;
&lt;body&gt;
&lt;script&gt;
function onClick() {
  window.chrome.webview.postMessage(&#39;Hello from web page&#39;);
}
&lt;/script&gt;
&lt;button onclick=&quot;&quot;onClick()&quot;&quot;&gt;Hello&lt;/button&gt;
&lt;/body&gt;
&lt;/html&gt;
            &quot;;
            myWebView2.NavigateToString(htmlContent);
        }

        private void MyWebView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var text = e.TryGetWebMessageAsString();
            var docName = _app.ActiveDocument.DisplayName;
            MessageBox.Show(text + &quot; for &quot; + docName);
        }
    }
}
</pre>
<p>Then show that <strong>Form</strong> when the &quot;<strong>Draw Slot</strong>&quot; button is clicked.</p>
<pre>override protected void ButtonDefinition_OnExecute(NameValueMap context)
{
    var myBrowserForm = new MyBrowserForm(InventorApplication);
    myBrowserForm.Show();
}</pre>
<p>When the user clicks the &quot;Hello&quot; button of the web page that will send a message back to the container.<br />The message can be caught using the <a href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.web.webview2.winforms.webview2.webmessagereceived">WebMessageReceived</a> event handler.</p>
<p>See it in action:</p>
<p><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/DpoqUrG0_Wk" width="480"></iframe></p>
<p>Source code:&#0160;<a href="https://github.com/adamenagy/InventorWebView2">https://github.com/adamenagy/InventorWebView2</a></p>

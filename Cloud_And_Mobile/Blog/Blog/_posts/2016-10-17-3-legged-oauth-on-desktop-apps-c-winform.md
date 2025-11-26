---
layout: "post"
title: "3-legged OAuth on desktop apps (C# & WinForm)"
date: "2016-10-17 09:41:56"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Client"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/10/3-legged-oauth-on-desktop-apps-c-winform.html "
typepad_basename: "3-legged-oauth-on-desktop-apps-c-winform"
typepad_status: "Publish"
---

<p>By Augusto Goncalves (<a href="https://twitter.com/augustomaia">@augustomaia</a>)</p>
<p>If you don&#39;t know OAuth or the differences between <a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/">2-legged</a> or <a href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/">3-legged</a> authentication on Forge, <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/09/introduction-to-oauth-and-data-management-api.html">please review this webinar</a>.</p>
<p>It&#39;s quite straightforward to implement OAuth on a web app: as everything is on the browser, including the redirect callback, it&#39;s almost natural. What about a desktop application? In this case is not quite direct to handle callbacks or implement micro servers to handle it.</p>
<p>This code is almost an hack, but works for our purposes. <strong>Note this implementation is not safe</strong>, meaning your developer ID and secret can be easily hacked/read by almost any end-user. The safest way still to perform authorization on server-side.&#0160;</p>
<p>Let&#39;s use the <a href="https://msdn.microsoft.com/en-us/library/system.windows.forms.webbrowser.aspx">System.Windows.Forms.WebBrowser</a>, which works fine, but it doesn&#39;t handle error in the way we want for this sample. At the bottom of this post you&#39;ll find the full WebBrowser2 implementation <a href="https://msdn.microsoft.com/en-us/library/system.windows.forms.webbrowser.createsink.aspx">copied from here</a>.</p>
<p>Now on your app, create a form (assuming a WinForm application). Add a&#0160;<strong>WebBrowser2</strong> control, say&#0160;<strong>wb</strong>. Prepare the Authorize URL (using your client ID, redirect URL and Scope) and navigate to this page. The end-user will be redirected to the Autodesk login page. When done, it will redirect to your callback URL, which is not possible or doesn&#39;t exist (at this sample, fake.com). This will throw a 404 error that we can capture. Voilà! Finally capture the <strong>code</strong> parameter of the query string and call the <a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/">gettoken</a> endpoint (e.g. using <a href="http://restsharp.org/">RestSharp</a>)&#0160;</p>
<div>
<pre style="margin: 0; line-height: 125%;">[PermissionSetAttribute(SecurityAction.Demand, Name = &quot;FullTrust&quot;)]
<span style="color: #0000ff;">public</span> <span style="color: #0000ff;">partial</span> <span style="color: #0000ff;">class</span> <span style="color: #2b91af;">oAuthForm</span> : Form
{
  <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">const</span> <span style="color: #2b91af;">string</span> FORGE_CLIENT_ID = <span style="color: #a31515;">&quot;XxXxXxXxXx&quot;</span>;
  <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">const</span> <span style="color: #2b91af;">string</span> FORGE_CLIENT_SECRET = <span style="color: #a31515;">&quot;XxXxXxXx&quot;</span>;
  <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">const</span> <span style="color: #2b91af;">string</span> FORGE_CALLBACK_URL = <span style="color: #a31515;">&quot;http://fake.com/api/forge/callback/oauth&quot;</span>;
  <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">const</span> <span style="color: #2b91af;">string</span> FORGE_BASE_URL = <span style="color: #a31515;">&quot;https://developer.api.autodesk.com&quot;</span>;
  <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">const</span> <span style="color: #2b91af;">string</span> FORGE_SCOPE = <span style="color: #a31515;">&quot;data:read data:write data:create data:search bucket:create bucket:read bucket:update bucket:delete&quot;</span>; <span style="color: #008000;">// assuming a full scope</span>

  <span style="color: #0000ff;">private</span> WebBrowser2 wb = <span style="color: #0000ff;">new</span> WebBrowser2();

  <span style="color: #0000ff;">public</span> oAuthForm()
  {
    InitializeComponent();

    wb.Dock = DockStyle.Fill;
    wb.NavigateError += <span style="color: #0000ff;">new</span> WebBrowserNavigateErrorEventHandler(wb_NavigateError);
    Controls.Add(wb);

    <span style="color: #008000;">// this is a basic code sample, quick &amp; dirty way to get the Authentication string</span>
    <span style="color: #2b91af;">string</span> authorizeURL = BASE_URL + <span style="color: #2b91af;">string</span>.Format(
        <span style="color: #a31515;">&quot;/authentication/v1/authorize?response_type=code&amp;client_id={0}&amp;redirect_uri={1}&amp;scope={2}&quot;</span>, 
        FORGE_CLIENT_ID, FORGE_CALLBACK_URL, System.Net.WebUtility.UrlEncode(FORGE_SCOPE));
        
    <span style="color: #008000;">// now let&#39;s open the Authorize page.</span>
    wb.Navigate(authorizeUrl);
  }

  <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">void</span> wb_NavigateError(
      <span style="color: #2b91af;">object</span> sender, WebBrowserNavigateErrorEventArgs e)
  {
    <span style="color: #008000;">// This will track errors: we want to track the 404 when the login</span>
    <span style="color: #008000;">// page redirects to our callback URL, let&#39;s check if is the error</span>
    <span style="color: #008000;">// we&#39;re tracking.</span>
    Uri callbackURL = <span style="color: #0000ff;">new</span> Uri(e.Url);
    <span style="color: #0000ff;">if</span> (e.Url.IndexOf(FORGE_CALLBACK_URL) == -1)
    {
      MessageBox.Show(<span style="color: #a31515;">&quot;Sorry, the authorization failed&quot;</span>, <span style="color: #a31515;">&quot;Error&quot;</span>, MessageBoxButtons.OK, MessageBoxIcon.Error);
      <span style="color: #0000ff;">return</span>;
    }
       
    <span style="color: #008000;">// extract the code</span>
    <span style="color: #2b91af;">var</span> query = HttpUtility.ParseQueryString(callbackURL.Query);
    <span style="color: #2b91af;">string</span> code = query[<span style="color: #a31515;">&quot;code&quot;</span>];
    
    <span style="color: #008000;">// now we have the code, let&#39;s make a Http call to </span>
    <span style="color: #008000;">// /authentication/v1/gettoken </span>
    <span style="color: #008000;">// and get the access_token...</span>
    
    <span style="color: #008000;">// you can use RestSharp for it, but I&#39;ll stop the sample here</span>

    <span style="color: #008000;">// you may want to close this form..</span>
    <span style="color: #0000ff;">this</span>.Close();
  }
}&#0160;</pre>
</div>
<p>Custom implementation of WebBrowser, let&#39;s call it WebBrowser2 (<a href="https://msdn.microsoft.com/en-us/library/system.windows.forms.webbrowser.createsink.aspx">original post</a>). Due our HTML/JavaScript implementation on the login page, some features are not natively supported by the WebBrowser (which, by default, have some features disables). To improve it, we need a set of registry keys (user level), which are well described at this <a href="http://stackoverflow.com/a/18333982/4838205">stackoverflow answer</a>.&#0160;</p>
<p>&#0160;</p>
<script src="https://gist.github.com/augustogoncalves/d0cdfa0eb81bf3ddd2af842f467981a7.js"></script>

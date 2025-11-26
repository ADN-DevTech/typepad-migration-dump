---
layout: "post"
title: "BIM 360 Glue API - Use cURL or PostMan to login and list projects"
date: "2015-04-28 03:02:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Cloud"
original_url: "https://adndevblog.typepad.com/aec/2015/04/bim-360-glue-api-use-curl-or-postman-to-login-and-list-projects.html "
typepad_basename: "bim-360-glue-api-use-curl-or-postman-to-login-and-list-projects"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/45030191">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<script type="text/javascript" src="https://adndevblog.typepad.com/files/run_prettify-3.js"></script>
<h1>BIM 360 Glue API</h1>
<p>Before developing application based on BIM 360 API, tools can help us to have a better understanding of RESFful API.</p>
<p>I'm going to use cURL and Postman to demonstrate how to call Glue API to login and query project list.</p>
<h1>Login</h1>
<p>Before login, we need to apply the API key and secret, if you don't know what are they, please contact&nbsp;<a title="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=473880" href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=473880" target="_self">ADN</a>&nbsp;to apply.</p>
<p>After login, server will return an auth_token which is used for future API calls to replace api secret as authentication information. So auth_token is very important, once the it is stolen, other developers can do harm to your project, so we need to protect it carefully.</p>
<p>Login is a POST request sent to <strong>https://b4.autodesk.com/api/security/v1/login.{format}</strong>, here {format} designates the format of return data. Glue API supports <strong>json</strong> and <strong>xml</strong> format. the POST content must contain the following parameters (other optional parameters is not listed here, refer to <a title="login api" href="https://b4.autodesk.com/api/security/v1/login/doc" target="_self">API document</a> for more information):</p>
<table border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr><th>Parameter</th><th>Meaning</th></tr>
<tr>
<td>login_name</td>
<td>Login name, i.e. Autodsek Id</td>
</tr>
<tr>
<td>password</td>
<td>Password</td>
</tr>
<tr>
<td>company_id</td>
<td>Company ID</td>
</tr>
<tr>
<td>api_key</td>
<td>API key</td>
</tr>
<tr>
<td>api_secret</td>
<td>API secret</td>
</tr>
<tr>
<td>timestamp</td>
<td>A timestamp, which is a number of seconds representing current time</td>
</tr>
<tr>
<td>sig</td>
<td>A md5 string, calculated from combined string of api_key, api_secret and timestamp</td>
</tr>
</tbody>
</table>
<p>The last 2 parameters, timestamp and sig, are needed by almost all API calls, and vary by time.</p>
<p>See more about how to <a title="Calculate timestamp and sig" href="#8" target="_self">Calcuate timestamp and sig</a>.</p>
<p>In this post, we will use json format, so the target url should be: <strong>https://b4.autodesk.com/api/security/v1/login.json</strong></p>
<p>Method is: POST</p>
<p>Post content:</p>
<pre class="prettyprint plain">login_name=Aaron.Lu@autodesk.com
&amp;company_id=adn
&amp;password=********
&amp;api_key=fe251558432bd3da0a70326ed169****
&amp;api_secret=70aadb2838cac9a739da11296d7f****
&amp;timestamp=1428647796
&amp;sig=6ec77c0448fcb16da9c1f03220a96ceb</pre>
<h2>using cURL command:</h2>
<p>Type following command at the prompt and press Enter:</p>
<pre class="prettyprint plain">curl --data "login_name=Aaron.Lu@autodesk.com
  &amp;company_id=adn&amp;password=********
  &amp;api_key=fe251558432bd3da0a70326ed169****
  &amp;api_secret=70aadb2838cac9a739da11296d7f****
  &amp;timstamp=1428647796
  &amp;sig=6ec77c0448fcb16da9c1f03220a96ceb"<br /> https://b4.autodesk.com/api/security/v1/login.json</pre>
<p><em><strong>--data</strong>&nbsp;</em> means we are going to send data with POST method.</p>
<p>Following is the return result:</p>
<pre class="prettyprint plain">{"auth_token":"35730ba86d48470593c1e63090610aa5",
"user_id":"****0b93-e919-****-9695-8b3733fa****",
"account_id":"****a18f-****-4b8a-****-8d762595****",
"account_hostname":"adn"}</pre>
<p><strong>NOTE:</strong> save the <strong>auth_token</strong>, we will use it later.</p>
<h2>using PostMan</h2>
<p>Choose method POST，fill form-data with parameters and values, click "Send", same result will show under "Body":</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1086278970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1086278970c image-full img-responsive" title="PostMan-Login" src="/assets/image_970388.jpg" alt="PostMan-Login" border="0" /></a></p>
<h1>Get project list</h1>
<p>The url is <strong>https://b4.autodesk.com/api/project/v1/list.{format}</strong></p>
<p>Method: GET</p>
<p>Parameter:</p>
<table border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr><th>Parameter</th><th>Meaning</th></tr>
<tr>
<td>company_id</td>
<td>Company ID</td>
</tr>
<tr>
<td>api_key</td>
<td>API key</td>
</tr>
<tr>
<td>timestamp</td>
<td>A timestamp, which is a number of seconds representing current time</td>
</tr>
<tr>
<td>sig</td>
<td>A md5 string, calculated from combined string of api_key, api_secret and timestamp</td>
</tr>
<tr>
<td>auth_token</td>
<td>a hash, returned by server after login successfully</td>
</tr>
</tbody>
</table>
<p>as we can see, api_secret, login_name and password are not needed, instead, auth_token is used.</p>
<p>Since we are using GET method, all the parameters should be included in the url, so the final url is:</p>
<p><strong>https://b4.autodesk.com/api/project/v1/list.json?company_id=adn &amp;api_key=fe251558432bd3da0a70326ed169**** &amp;timestamp=1428650893 &amp;sig=8d22b59569335394d7b219aae91990a7 &amp;auth_token=34dd71d838674c86a02da2fe329db24d</strong></p>
<h2>using cURL</h2>
<p>Type following command at the prompt and press Enter:</p>
<pre class="prettyprint plain">curl https://b4.autodesk.com/api/project/v1/list.json
?company_id=adn
&amp;api_key=fe251558432bd3da0a70326ed169****
&amp;timstamp=1428650893
&amp;sig=8d22b59569335394d7b219aae91990a7
&amp;auth_token=34dd71d838674c86a02da2fe329db24d</pre>
<p>Return result:</p>
<pre class="prettyprint javascript">{
  "project_list": [
    {
      "recent_model_info": [],
      "project_id": "1e7e5e8d-fea8-49cd-8e5b-76058f0ee3b6",
      "project_name": "AL+Sample+Project",
      "company_id": "adn",
      "created_date": "2015-03-19 03:26:51",
      "modify_date": "2015-04-01 07:15:15",
      "cmic_company_code": "",
      "cmic_project_code": "",
      "cw_project_code": "",
      "thumbnail_modified_date": "2015-03-19 04:41:45",
      "has_views": false,
      "has_markups": false,
      "has_clashes": false,
      "has_points": false,
      "total_member_count": 0,
      "total_project_admin_count": 0,
      "total_views_count": 0,
      "total_markups_count": 0,
      "last_activity_date": "2015-04-01 07:15:15",
      "permissions": [],
      "navisworks_version": "Nwd2014"
    }
  ],
  "page": 1,
  "page_size": 1,
  "total_result_size": 1,
  "more_pages": 0
}</pre>
<h2>using PostMan</h2>
<p>Choose GET, input url address, and click "Send", we can see the result in the bottom.</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1086280970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1086280970c image-full img-responsive" title="PostMan-ProjectListing" src="/assets/image_98859.jpg" alt="PostMan-ProjectListing" border="0" /></a></p>
<h1><a name="8"></a>Calculate timestamp and sig</h1>
<h2>timestamp</h2>
<p>timestamp is a integer, UNIXEpoch Time, quote from wikipedia:</p>
<blockquote>
<p>Unix time (also known as POSIX time or Epoch time) is a system for describing instants in time, defined as the number of seconds that have elapsed since 00:00:00 Coordinated Universal Time (UTC), Thursday, 1 January 1970, [note 1] not counting leap seconds.</p>
</blockquote>
<p>Pseudo code looks like this:</p>
<pre class="prettyprint plain">Seconds(CurrentTime-1970/1/1)</pre>
<p><br /> Following are the ways of getting timestamp in multiple platforms:</p>
<p>Powershell (Windows 7 advanced command propmt, input Powershell in windows search dialog you can find the application. Since we want to use cURL and command line to call API, we use Powershell for convinience)</p>
<pre class="prettyprint plain">$oriDate=Get-Date -Date "01/01/1970"
$nowDate=(Get-Date).ToUniversalTime()
$timestamp=[int](New-TimeSpan -Start $oriDate -End $nowDate)
    .TotalSeconds</pre>
<p>C#</p>
<pre class="prettyprint csharp">TimeSpan tSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1));
int timestamp = (int)tSpan.TotalSeconds;</pre>
<p>Node.js/Javascript</p>
<pre class="prettyprint javascript">var timestamp = Math.floor((new Date).getTime()/1000);</pre>
<p>Java</p>
<pre class="prettyprint java">long timestamp = System.currentTimeMillis() / 1000L;</pre>
<h2>sig</h2>
<p>sig is a md5, lower case string, without any dash (-), Glue API needs the md5 calculated from combination string of api_key, auth_token and timestamp. Pseudo code:</p>
<pre class="prettyprint plain">MD5 (api_key+auth_token+timestamp)</pre>
<p>&nbsp;</p>
<p>Following are how to get sig with multiple platforms:</p>
<p>Powershell</p>
<pre class="prettyprint plain">$identityString = $api_key + $api_secret + $timestamp
# compute hash
$md5 = new-object -TypeName System.Security.Cryptography
    .MD5CryptoServiceProvider
$utf8 = new-object -TypeName System.Text.UTF8Encoding
$signature = [System.BitConverter]::ToString($md5.ComputeHash(
    $utf8.GetBytes($identityString))).Replace("-","").ToLower()
</pre>
<p>C#</p>
<pre class="prettyprint csharp">// using System.Security.Cryptography;

public string ComputeMD5Hash(string identityString)
{
    // step 1, calculate MD5 hash from identityString
    MD5 md5 = System.Security.Cryptography.MD5.Create();
    byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(
        identityString);
    byte[] hash = md5.ComputeHash(inputBytes);
    // step 2, convert byte array to hex string
    StringBuilder sb = new StringBuilder();
    for (int i = 0; i &lt; hash.Length; i++)
    {
        sb.Append(hash[i].ToString("x2"));
    }
    return sb.ToString();
}</pre>
<p>Node.js/Javascript</p>
<pre class="prettyprint javascript">var crypto = require('crypto');
var hash = crypto.createHash('md5').update(identityString)
  .digest("hex");</pre>
<p>Java</p>
<pre class="prettyprint java">public String computeMD5Hash(String identityString) {
    java.security.MessageDigest md = java.security.MessageDigest
        .getInstance("MD5");
    byte[] array = md.digest(identityString.getBytes());
    StringBuffer sb = new StringBuffer();
    for (int i = 0; i &lt; array.length; ++i) {
      sb.append(Integer.toHexString((array[i] &amp; 0xFF) | 0x100)
          .substring(1,3));
    }
return sb.toString();
}</pre>
<h1>Trouble shooting</h1>
<p>If we see below problem, it probably caused by using wrong HTTP method, e.g. when it requires GET, but we are using POST.</p>
<pre class="prettyprint html">&lt;body&gt;
    &lt;div id="content"&gt;
        &lt;p class="heading1"&gt;Service&lt;/p&gt;
        &lt;p&gt;Method not allowed.&lt;/p&gt;
    &lt;/div&gt;
&lt;/body&gt;</pre>

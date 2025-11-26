---
layout: "post"
title: "BIM 360 Docs と Forge OAuth"
date: "2018-03-19 19:52:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/bim-360-docs-and-forge-oauth.html "
typepad_basename: "bim-360-docs-and-forge-oauth"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fc92dc970d-pi" style="float: right;"><img alt="Bim_360_data_access" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fc92dc970d img-responsive" src="/assets/image_883847.jpg" style="margin: 0px 0px 5px 5px;" title="Bim_360_data_access" /></a>オートデスクのクラウド サービスと連携するような Forge アプリ/サービスを開発する場合、3-legged OAuth を使って Forge アプリが認証を得た後、特定のユーザからの認可を得て、本来そのユーザしかアクセス出来ないストレージ領域にアクセスするのが一般的です。</p>
<p>この考え方は、過去のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html" rel="noopener noreferrer" target="_blank">Forge での OAuth 認証シナリオ</a></strong> でご案内していますが、Data Management API の <strong><a href="https://developer.autodesk.com/en/docs/data/v2/overview/basics/" rel="noopener noreferrer" target="_blank">API Basics</a></strong> ページの <strong>Authentication and Scopes</strong> セクションにも一般的なアクセス方法として記述されています（<span style="background-color: #ffff00;">黄色箇所</span>）。</p>
<hr />
<h2 style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 15pt;">Authentication and Scopes</span></h2>
<p style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif;">The Data Management API requires the use of OAuth2 bearer tokens. See the&#0160;<a class="reference external" href="https://developer.autodesk.com/en/docs/oauth/v2/">OAuth</a>&#0160;documentation for more information on authentication, obtaining a bearer token, and scopes.</span></p>
<p style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif; background-color: #ffff00;">In general, access to BIM 360 Team, BIM 360 Docs, Fusion Team, and A360 Personal data requires the use of a&#0160;<a class="reference external" href="https://developer.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token" style="background-color: #ffff00;">3-legged OAuth2 token</a>.</span></p>
<p style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif;">HTTP&#0160;<code class="docutils literal"><span class="pre">GET</span></code>&#0160;requests to the Project and Data services require the&#0160;<code class="docutils literal"><span class="pre">data:read</span></code>&#0160;scope.</span></p>
<p style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif;">HTTP&#0160;<code class="docutils literal"><span class="pre">POST</span></code>&#0160;requests to the Data service require the&#0160;<code class="docutils literal"><span class="pre">data:create</span></code>&#0160;scope, but can also be called with the&#0160;<code class="docutils literal"><span class="pre">data:write</span></code> scope.</span></p>
<hr />
<p>もちろん、このシナリオに沿って 3-legged OAuth を実装された Forge アプリ/サービスは、特定のユーザ領域にはアクセスすることが可能です。</p>
<p>ところが、Data Management API の <strong><a href="https://developer.autodesk.com/en/docs/data/v2/overview/basics/" rel="noopener noreferrer" target="_blank">API Basics</a></strong> ページ冒頭には、次のようにも書かれています（<span style="background-color: #ffff00;">黄色箇所</span>）。</p>
<hr />
<h1 style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 15pt;">API Basics</span></h1>
<p style="padding-left: 30px;"><span style="font-family: arial, helvetica, sans-serif;">There are two key data access paradigms that make up the Data Management API.</span></p>
<ul class="simple">
<li><span style="font-family: arial, helvetica, sans-serif;">Accessing data from Autodesk SaaS applications using any of the Data Management services.</span>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif;">For BIM 360 Team, Fusion Team, and A360 Personal, end users need to provide 3-legged&#0160;<a class="reference external" href="https://developer.autodesk.com/en/docs/oauth/v2/overview/basics">authentication</a>for your app to access the data.</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; background-color: #ffff00;">For BIM 360 Docs, an account administrator needs to add an integration with your app in BIM 360 Enterprise. You can access data using either 2-legged or 3-legged authentication.</span></li>
</ul>
</li>
<li><span style="font-family: arial, helvetica, sans-serif;">Managing and storing files from your app on the Forge platform, independent of any Autodesk SaaS application. You need to use the Object Storage service (OSS).</span>&#0160;</li>
</ul>
<hr />
<p>前半の BIM 360 Docs のアプリ統合の説明は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/06/bim-360-docs-and-data-management-api-access.html" rel="noopener noreferrer" target="_blank">BIM 360 Docs と Data Management API アクセス</a>&#0160;</strong>と&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/03/forge-and-bim-360-integration-tips.html" rel="noopener noreferrer" target="_blank">Forge アプリを BIM 360 と連携するための「カスタム統合機能」の注意点</a></strong>&#0160; に譲りますが、問題は後半の説明です。曰く、（BIM 360 Docs の）データにアクセスするには、2-legged 認証または、3-legged 認証のいずれかを使用出来る、とあります。</p>
<p>これは、BIM 360 Docs 統合アプリの役割を考えると自明です。Forge アプリ/サービスが特定のユーザ ストレージにあるデータしか扱えない場合、それは、ある意味、BIM 360 Docs と同じ情報しか提供することが出来ないことを意味します。</p>
<p>BIM 360 Docs 統合アプリには、開発者に選択によって、いずれかの振る舞いを持てるような実装方法が用意されています。</p>
<p>すなわち、2-legged OAuth を使って Access Token を得たアプリ/サービスは、BIM 360 Docs のすべてのデータにアクセスすることが出来ます。</p>
<p>また、3-legged OAuth を使ってトークンを Access Token を得たアプリ/サービスは、エンド ユーザ認可を得た特定のユーザ領域にあるデータのみにアクセスすることが出来ます。</p>
<p>昨年、Autodesk University Japan 2018 と併設した Forge DevCon Japan の&#0160;<strong><a href="https://reg31.smp.ne.jp/regist/is?SMPFORM=rfo-sfnfq-309d1facbbc6431226ed107f3f1ed90e" rel="noopener noreferrer" target="_blank">1G-4 BIM 360 &amp; Forge：建設におけるコラボレーションの将来に向けて</a></strong> セッション資料の P21で触れられているのはこの点です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9596101970b-pi" style="display: inline;"><img alt="Devcon_2018_japan_slide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9596101970b image-full img-responsive" src="/assets/image_184492.jpg" title="Devcon_2018_japan_slide" /></a></p>
<p>もちろん、&#0160;2-legged OAuth を使ってすべてのデータを得た場合でも、そのアプリのエンドユーザにすべてのデータを開示するか否か、開示する場合でも、どのように表示するかなどは、開発者が持つの意図に依存することになります。</p>
<p>By Toshiaki Isezaki</p>

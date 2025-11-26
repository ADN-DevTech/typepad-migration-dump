---
layout: "post"
title: "Manufacturing Data Model：Data Explorer での v2 利用設定"
date: "2024-02-26 00:02:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/02/mfg-data-modeld-use-of-v2-on-data-explorer.html "
typepad_basename: "mfg-data-modeld-use-of-v2-on-data-explorer"
typepad_status: "Publish"
---

<p>Beta の扱いですが、Manufacturing Data Model API が v2 になったことで、GraphQL をテストする MFG Data Explorer を v2 用のエンドポイントに変更する必要があります。</p>
<p>MFG Data Explorer は Fusion Team 内のデザイン データにアクセスするため（いまのところ）、3-legged 認証・認可のプロセスが必要になります。利用前に、<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html">APS API を利用するアプリの登録とキーの取得</a> の手順で新しいアプリを作成して、Client ID と Client Secret の取得するとともに、Callback URL を設定します。</p>
<p>Callback URL には <strong>https://mfgdatamodel-explorer.autodesk.io/callback/oauth</strong> を設定し、API access ドロップダウンで <strong>Manufacturing Data Model API</strong> にチェックしてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab3fca200b-pi" style="display: inline;"><img alt="Created_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab3fca200b image-full img-responsive" src="/assets/image_885035.jpg" title="Created_app" /></a><br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a76816200c-pi" style="display: inline;"><img alt="Api_access" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a76816200c image-full img-responsive" src="/assets/image_501887.jpg" title="Api_access" /></a></p>
<p>続いて&#0160;&#0160;<a href="MFG%20Data Explorer" rel="noopener" target="_blank">https://mfgdatamodel-explorer.autodesk.io/credentials</a> にアクセスして、Client ID と Client Secret に上記で取得した値をクリップボード経由で貼り付け、APS endpoint に<strong> https://developer.api.autodesk.com</strong> を、GraphQL endpoint に Manufacturing Data Model API v2 の <strong>https://developer.api.autodesk.com/beta/graphql</strong> にそれぞれ設定して <span style="background-color: #0d6efd;"><span style="color: #ffffff;">&#0160; Next&#0160;</span>&#0160;</span> をクリックします。</p>
<p>なお、GraphQL でも単一エンドポイントの呼び出し時にリクエスト ヘッダーへアクセス トークンの設定が必要です。APS endpoint 設定は、Authentication API（OAuth API）v2 エンドポイントの URL 解決に利用されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a76884200c-pi" style="display: inline;"><img alt="Mfg_data_explorer_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a76884200c img-responsive" src="/assets/image_99547.jpg" title="Mfg_data_explorer_settings" /></a></p>
<p>サインイン画面が表示されたら、Fusion Team にアクセス対象のデザイン データを持つ Autodesk ID（オートデスク アカウント）でサインイン、次の画面で&#0160;<span style="background-color: #111111; color: #ffffff;"> &#0160;許可&#0160; </span>&#0160;をクリックして MFG Data Explorer アプリが同ユーザーの所有するデータ領域にアクセスすることを認可します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab402d200b-pi" style="display: inline;"><img alt="Authorize" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab402d200b img-responsive" src="/assets/image_634921.jpg" title="Authorize" /></a></p>
<p>次回からは、この設定は不要です。</p>
<p>By Toshiaki Isezaki</p>

---
layout: "post"
title: "APS アプリ作成時のアプリケーション タイプ"
date: "2023-02-15 00:06:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/02/application-types-when-creating-aps-app.html "
typepad_basename: "application-types-when-creating-aps-app"
typepad_status: "Publish"
---

<p><a href="https://aps.autodesk.com/" rel="noopener" target="_blank">APS&#0160; ポータル（https://aps.autodesk.com/）</a> でアプリを作成してデベロッパーキー（Client Id と Client Secret）を取得する際、必ず Applications ページ（<a href="https://aps.autodesk.com/myapps/" rel="noopener" target="_blank">https://aps.autodesk.com/myapps/</a>）を利用することになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751706567200b-pi" style="display: inline;"><img alt="Applications" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751706567200b image-full img-responsive" src="/assets/image_755976.jpg" title="Applications" /></a></p>
<p>このアプリ作成時、新しくアプリ タイプを選択すようになりましたので、ご紹介しておきたいと思います。</p>
<hr />
<p>アプリ タイプの選択は、ページ上部の [+ Create application] をクリックした際に表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75194b6bc200c-pi" style="display: inline;"><img alt="Create_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75194b6bc200c image-full img-responsive" src="/assets/image_825104.jpg" title="Create_app" /></a></p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751706651200b-pi"><img alt="Arrow" class="asset  asset-image at-xid-6a0167607c2431970b02b751706651200b img-responsive" src="/assets/image_366700.jpg" style="width: 50px; display: block; margin-left: auto; margin-right: auto;" title="Arrow" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7517065cc200b-pi" style="display: inline;"><img alt="App_types" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7517065cc200b image-full img-responsive" src="/assets/image_913000.jpg" title="App_types" /></a></p>
<p>それぞれのアプリ タイプは、次のような用途を想定して用意されます。</p>
<p><strong>Traditional Web App</strong></p>
<p style="padding-left: 40px;">「Most flexiible」が示すように、これは最も柔軟なオプションで、Forge 世代を含め、過去の作成された全アプリで利用されてきたアプリ タイプです。使用する<a href="https://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html" rel="noopener" target="_blank">シナリオ</a>に合わせて、2-legged と 3-legged の OAuth をサポートするので、どのタイプを選択すべきか判断出来ない場合には、とりあえず、このタイプを選択することをお薦めします。</p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/App-types/traditionalweb/" rel="noopener" target="_blank">Traditional Web App</a><a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/App-types/native/"></a>&#0160;の内容もご確認ください。</p>
<p style="padding-left: 40px;">なお、過去の作成されたアプリは、このタイプに設定されているはずです。個々のアプリ内容には、Application Type 項が新設されていて、Traditional Web App になっているはずです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75194b81a200c-pi" style="display: inline;"><img alt="Existing_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75194b81a200c image-full img-responsive" src="/assets/image_433599.jpg" title="Existing_app" /></a></p>
<p style="padding-left: 40px;">既存のアプリのアプリ タイプを変更することは出来ません。</p>
<p><strong>Desktop, Mobile, Single-Page App</strong></p>
<p style="padding-left: 40px;">アプリをデスクトップ ネイティブ アプリ、あるいは、モバイル デバイスに特化したネイティブ アプリとして実行する場合に使用します。つまり、アプリの認証情報を保護できないシナリオに推奨されるアプリ タイプです。このタイプでは、セキュリティ強化のために <a href="https://oauth.net/2/pkce/" rel="noopener" target="_blank">Proof Key for Code Exchange（PKCE）</a>を使用します。</p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/App-types/native/" rel="noopener" target="_blank">Desktop, Mobile &amp; Single Page Web App</a>&#0160;の内容もご確認ください。</p>
<p><strong>Server-to-Server App</strong></p>
<p style="padding-left: 40px;">エンドユーザを持たないサーバー サイド アプリ用途に用意されたアプリ タイプです。このタイプでは、2-legged&#0160; OAuth のみをサポートします。アプリ内容にコールバック URL を指定することは出来ません。</p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/App-types/Machine-to-machine/" rel="noopener" target="_blank">Server-to-Server App</a>&#0160;の内容もご確認ください。</p>
<hr />
<p>アプリを作成してデベロッパーキー（Client Id と Client Secret）を取得する具体的な手順は、<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a>&#0160;のブログ記事を参照してください。</p>
<p>By Toshiaki Isezaki</p>

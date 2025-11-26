---
layout: "post"
title: "Design Automation API for Inventor - Postman Sample のセットアップ"
date: "2020-05-18 00:01:00"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-postman-sample-setup.html "
typepad_basename: "design-automation-api-for-inventor-postman-sample-setup"
typepad_status: "Publish"
---

<p><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">&#0160;</span><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e948300b200b-pi" style="display: inline;"><img alt="Autodesk-forge-logo-pms-color-black-stacked" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e948300b200b image-full img-responsive" src="/assets/image_657512.jpg" title="Autodesk-forge-logo-pms-color-black-stacked" /></a></span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">これまで本ブログでForgeのDesign Automationについて、サンプルやチュートリアル等を交えてご紹介をしてきたかと思いますが、InventorでのDesign Automationについては概要のご紹介に留まっていたかと思います。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">今回より、複数回に分けて、InventorでのDesign Automationのサンプルをご紹介していきたいと思います。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">今回は、InventorのDesign Automation APIをPostmanで動作を確認するための、Postman環境のセットアップ手順をご紹介いたします。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ご存知のように、Postmanは、HTTPリクエストをGUIを用いて簡単に送信し、Web APIの動作の確認をすることのできるツールとなります。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/inventor/" rel="noopener" target="_blank">Design Automation API for Inventorのチュートリアル</a>では、動作を確認するのに便利な、事前定義済みのHTTPリクエストのサンプルを Postman コレクションという形で公開しております。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postman コレクションは、タスクでグループ化されており、各タスクは<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/inventor/" rel="noopener" target="_blank">Design Automation API for Inventorのチュートリアル</a>での各タスクと同じ名前となっております。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9482bfc200b-pi" style="display: inline;"><img alt="Forge_portal_2_inventor_postman_menu_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9482bfc200b image-full img-responsive" src="/assets/image_503332.jpg" title="Forge_portal_2_inventor_postman_menu_01" /></a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">また、同様にタクス内の各リクエストは、チュートリアルでの各ステップと同じ名前が付けられています。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9482c22200b-pi" style="display: inline;"><img alt="Forge_portal_2_inventor_postman_menu_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9482c22200b image-full img-responsive" src="/assets/image_270881.jpg" title="Forge_portal_2_inventor_postman_menu_02" /></a></span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">なお、本記事では、Postmanコレクションのセットアップにあたり、以下は完了しているものといたします。</span></p>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postmanのインストール</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">必要となるForge アプリの作成と Client ID と Client Secret の取得</span></li>
</ul>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">もし、Forgeアプリの作成と、Client ID と Client Secret の取得をされていない場合は、以下の過去のブログ記事を参考に設定と取得をお願いいたします。</span></p>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html">Forge API を利用するアプリの登録とキーの取得</a></span></li>
</ul>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>１．Postman environmentの入手とインポート</strong></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postman environmentは、複数のHTTPリクエスト間で共通的に利用する値を格納する、名前付きのコンフィグレーションです。例としては、Forgeにリクエストを行う際のアクセストークンは、環境変数dapAccesssTokenに格納され、各HTTPリクエストで利用ができます。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">以下の手順に従い、チュートリアル向けのPostman environmentをインポートしてください。</span></p>
<ol style="list-style-type: lower-alpha;">
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0263e9482e25200b img-responsive"><a href="https://adndevblog.typepad.com/files/da4inventor-environment.postman_environment.json" rel="noopener" target="_blank">DA4Inventor-Environment.postman_environment</a></span>ファイルをダウンロード</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ダウンロードしたDA4Inventor-Environment.postman_environment.jsonファイルをPostmanにインポート</span>
<ol style="list-style-type: lower-roman;">
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postmanアプリケーションのヘッダーバーから、インポートをクリックしダイアログを表示</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ダウンロードしたファイルを、ダイアログにドラッグアンドドロップ</span></li>
</ol>
</li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">アプリケーション右上のEnvironmentドロップダウンから、DA4Inventorを選択し環境設定をロード</span></li>
</ol>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19cf8c200c-pi" style="display: inline;"><img alt="Postman_environment_dropdown" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19cf8c200c image-full img-responsive" src="/assets/image_508856.jpg" title="Postman_environment_dropdown" /></a></span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>２．チュートリアル用のPostman コレクションのインポート</strong></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postmanコレクションは事前設定済みのHTTPリクエストのセットです。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">以下の手順に従い、チュートリアル向けのPostman コレクションをインポートしてください。</span></p>
<ol style="list-style-type: lower-alpha;">
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0263ec19d1b4200c img-responsive"><a href="https://adndevblog.typepad.com/files/da4inventor-tutorial.postman_collection.json">DA4Inventor Tutorial.postman_collectionをダウンロード</a></span>ファイルをダウンロード</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ダウンロードしたDA4Inventor Tutorial.postman_collection.jsonファイルをPostmanにインポート</span>
<ol style="list-style-type: lower-roman;">
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postmanアプリケーションのヘッダーバーから、インポートをクリックしダイアログを表示</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ダウンロードしたファイルを、ダイアログにドラッグアンドドロップ</span></li>
</ol>
</li>
</ol>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">アプリケーションの左側に、インポートしたタスクが表示されます。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da47b8200d-pi" style="display: inline;"><img alt="Forge_portal_2_inventor_postman_menu_Tasks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2da47b8200d image-full img-responsive" src="/assets/image_319937.jpg" title="Forge_portal_2_inventor_postman_menu_Tasks" /></a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">以上でPostman サンプルの準備は完了となります。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">次回は、この Postman サンプルを使って動作の確認したいと思います。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">By Takehiro Kato</span></p>

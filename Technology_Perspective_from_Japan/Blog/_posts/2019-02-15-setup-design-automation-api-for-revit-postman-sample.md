---
layout: "post"
title: "Design Automation API for Revit - Postman Sample のセットアップ"
date: "2019-02-15 16:00:46"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/setup-design-automation-api-for-revit-postman-sample.html "
typepad_basename: "setup-design-automation-api-for-revit-postman-sample"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/understanding-steps-to-use-design-automation-api-for-revit.html">前回のブログ記事</a>では、Design Automation API for Revit の基本的な仕組み、用語、開発手順について解説いたしました。<br />今回は、実際の API について、Postman サンプルで動作を確認するためのセットアップ手順をご紹介します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39d25c8200c-pi" style="display: inline;"><img alt="DesignAutomationRevit22" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39d25c8200c image-full img-responsive" src="/assets/image_404485.jpg" title="DesignAutomationRevit22" /></a></p>
<p><a href="https://www.getpostman.com/">Postman</a>は、Web API ( RESTful API ) をテストするときに役立つツールです。</p>
<p>各種 OS 向けのクライアントアプリケーションや、 Chromeの拡張機能ツールとして提供されており、これらのツールを使用すれば、プログラムコードを作成することなく、HTTP リクエスト・レスポンスを確認することができます。</p>
<p>Web API をはじめて利用する際には、いきなり Webアプリケーションの開発を始めるのではなく、このようなツールを活用して、実際に API の動作を確認することで理解を深めることができます。</p>
<p>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2016/09/restful-api-and-testing-tools.html">このブログ記事</a>でも Postman による RESTful API のテストについてご紹介しました。</p>
<p>ここで簡単に Web API のおさらいをしておきましょう。</p>
<p>Web API のリクエストを実行すると、その API のエンドポイントを提供しているサーバーに対して何らかのアクションを実行できます。<br />これらのアクションは HTTP メソッドという形で定義されております。<br />最も一般的なアクションは、GET、POST、PUT、および DELETEです。</p>
<p>たとえば、GETを使用すると、サーバーからデータを取得できます。POSTを使用すると、サーバー内の既存のファイルまたはリソースにデータを追加できます。PUTを使用すると、サーバー内の既存のファイルまたはリソースを置き換えることができます。また、DELETEを使用すると、サーバーからデータを削除できます。</p>
<p><a href="https://www.getpostman.com/">Postman</a> は、この Web API のリクエストを簡単に送信するツールです。</p>
<p>Design Automation API for Revit のドキュメントページでは、この Postman のサンプル（ベータ版）を Postman コレクションという形で公開しております。</p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/postman/">Postman Samples (Beta)</a></li>
</ul>
<p>このドキュメントページに掲載されているセットアップ方法をご紹介します。</p>
<p><strong>1. Postman のインストール</strong></p>
<p>インストールの手順は下記の Postman 公式ドキュメントをご参照ください</p>
<ul>
<li><a href="https://learning.getpostman.com/docs/postman/launching_postman/installation_and_updates/">Installation and Updates</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c337eb200d-pi" style="display: inline;"><img alt="DesignAutomationRevit16" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c337eb200d image-full img-responsive" src="/assets/image_424800.jpg" title="DesignAutomationRevit16" /></a></p>
<p><strong>2. Forge アプリの作成と Client ID と Client Secret の取得</strong></p>
<p>Postman サンプルを利用するためには、Forge アプリを作成する必要があります。</p>
<p>Forge ポータルサイトでこれから作成する Forge アプリを登録して、 Forge Platform API の利用に必要な Client ID と Client Secret を取得します。この処理は、手動でおこなう必要があります。具体的な手順は、過去のブログ記事を参考にしてください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html">Forge API を利用するアプリの登録とキーの取得</a></li>
</ul>
<p>&#0160;</p>
<p><strong>3. Postman Collection の入手とインポート</strong></p>
<p>下記のリンクから Postman Collection をダウンロードして、Postman アプリケーションでインポートします。Postman アプリケーションの左上の Import ボタンを押すと、インポートダイアログが表示されます。先ほどダウンロードした Collection ファイルを選択します。</p>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b0240a49faa3a200b img-responsive"><a href="https://adndevblog.typepad.com/files/da4revit-collections.postman_collection-1.json">DA4Revit-Collections.postman_collectionをダウンロード</a></span></li>
</ul>
<p>Postman Collection とは、Postman で作成した Web API の複数の HTTP リクエストをコレクションとしてまとめて JSON 形式のテキストファイルで書き出したものです。</p>
<ul>
<li><a href="https://www.getpostman.com/docs/v6/postman/collections/intro_to_collections">Intro to collections</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e2e409200b-pi" style="display: inline;"><img alt="DesignAutomationRevit18" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e2e409200b img-responsive" src="/assets/image_838053.jpg" title="DesignAutomationRevit18" /></a></p>
<p>インポートが完了すると、下記のようにサイドバーにコレクションが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c33832200d-pi" style="display: inline;"><img alt="DesignAutomationRevit20" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c33832200d img-responsive" src="/assets/image_193706.jpg" title="DesignAutomationRevit20" /></a></p>
<p>&#0160;</p>
<p><strong>4. Postman Environment の入手とインポート</strong></p>
<p>下記のリンクから Postman Environment をダウンロードして、Postman アプリケーションでインポートします。先ほどと同じインポートダイアログで Environment ファイルを選択します。</p>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b0240a49faa4c200b img-responsive"><a href="https://adndevblog.typepad.com/files/da4revit-environment.postman_environment.json">DA4Revit-Environment.postman_environmentをダウンロード</a></span></li>
</ul>
<p>複数の HTTP リクエストで共通して利用するパラメータは、環境変数のような形式で管理することができ、これも Postman Environment として、同じ形式のテキストファイルで保存できます。</p>
<ul>
<li><a href="https://www.getpostman.com/docs/v6/postman/environments_and_globals/manage_environments">Manage environments</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e2e462200b-pi" style="display: inline;"><img alt="DesignAutomationRevit21" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e2e462200b img-responsive" src="/assets/image_499128.jpg" title="DesignAutomationRevit21" /></a></p>
<p>インポートした Postman Environment をドロップダウンリストから選択し、クイックルックツールで確認します。</p>
<p>&#0160;</p>
<p><strong>5. 環境変数の設定</strong></p>
<p>クイックルックで表示されている大ログの右上の Edit ボタンを押すと、MANAGE ENVIRONMENTS ダイアログが表示されるので、Client ID と Client Secret を設定します。</p>
<p>またここで、ニックネームも設定することができます。ニックネームはグローバルで一意でなければなりません。他のユーザーに既に使用されている場合は、そのニックネームを使用することはできません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3e2e473200b-pi" style="display: inline;"><img alt="DesignAutomationRevit23" class="asset  asset-image at-xid-6a0167607c2431970b022ad3e2e473200b img-responsive" src="/assets/image_380600.jpg" title="DesignAutomationRevit23" /></a></p>
<p><strong>6. Revit アドイン バンドルパッケージの準備</strong></p>
<p>Revit アドインのバンドルパッケージを用意します。</p>
<p>もしまだ Revit アドインをお持ちでない場合、あらかじめ用意されているサンプルのアドインをダウンロードして使用することができます。</p>
<p><span style="color: #ff0000;">※右側のリンクからダウンロードした場合、システムの都合上、ファイル名が全て小文字になってしまいます。お手数をおかけしますが、それぞれ左のファイル名に名前を変更してご利用ください。</span></p>
<ul>
<li><a href="https://s3.amazonaws.com/revitio/documentation/SketchItApp.zip">SketchItApp.zip</a>（リンクが切れている場合は <a href="https://adndevblog.typepad.com/files/sketchitapp.zip">こちら </a>からダウンロードできます。）</li>
<li><a href="https://s3.amazonaws.com/revitio/documentation/CountItApp.zip">CountItApp.zip</a>（リンクが切れている場合は <a href="https://adndevblog.typepad.com/files/countitapp.zip">こちら </a>からダウンロードできます。）</li>
<li><a href="https://s3.amazonaws.com/revitio/documentation/DeleteWallsApp.zip">DeleteWallsApp.zip</a>（リンクが切れている場合は <a href="https://adndevblog.typepad.com/files/deletewallsapp.zip">こちら </a>からダウンロードできます。）</li>
</ul>
<p>これで Postman サンプルの準備は完了です。</p>
<p>次回はこの Postman サンプルを使って動作を確認してみましょう。</p>
<p>By Ryuji Ogasawara</p>

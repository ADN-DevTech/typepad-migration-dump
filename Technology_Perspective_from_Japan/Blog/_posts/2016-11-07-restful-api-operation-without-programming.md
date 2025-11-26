---
layout: "post"
title: "プログラムレスで RESTful API を操作"
date: "2016-11-07 02:45:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/11/restful-api-operation-without-programming.html "
typepad_basename: "restful-api-operation-without-programming"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094fb101970d-pi" style="float: right;"><img alt="Dataspider_logo" class="asset  asset-image at-xid-6a0167607c2431970b01bb094fb101970d img-responsive" src="/assets/image_522253.jpg" style="width: 150px; margin: 0px 0px 5px 5px;" title="Dataspider_logo" /></a>Autodesk Forge では、クラウドとのコミュニケーションに RESTful API を多用します。通常、RESTful API の使用時にはコーディングと呼ばれるプログラミング作業が必要になりますが、プログラム知識がない場合でも、コーディングなしで一連の RESTful APIの呼び出しや応答を 「プログラム」 する<strong><a href="https://ja.wikipedia.org/wiki/Enterprise_application_integration" target="_blank">&#0160;</a></strong><span data-offset-key="emod0-0-0"><strong><a href="https://ja.wikipedia.org/wiki/Enterprise_application_integration" target="_blank">EAI (Enterprise Application Integration)</a></strong> と呼ばれる</span>ミドルウェアあります。</p>
<p><span data-offset-key="emod0-0-0">主要な EAI の1つには、オートデスクと同じく Mashup Awards に API スポンサーとして参加されていた&#0160;</span><strong><a href="https://www.appresso.com/" target="_blank">株式会社アプレッソ</a></strong>&#0160;の&#0160;<strong><a href="https://www.appresso.com/servista/" target="_blank">DataSpider Servista</a></strong>&#0160;があります。</p>
<p>DataSpider は、もともと RESTful API を提供するアプリケーションやシステム同士を繋げるためのもので、企業内システムの統合で利用されているそうです。会期中、DataSpider &#0160;で Forge を利用する手順を実現していただき、その具体的な手順を開発者向けの情報共有サイト <strong><a href="http://qiita.com" target="_blank">Quita</a></strong> で公開していただきましたので、ご案内したいと思います。</p>
<p style="padding-left: 30px;"><strong><a href="http://qiita.com/appresso_wakino/items/2dfb308af60aae142a44" target="_blank">DataSpiderを使ってAutodesk Forgeプラットフォーム連携を試してみる</a></strong></p>
<p>上記記事にあるように、ドラッグ&amp;ドロップ操作で一連の RESTful API 呼び出しをひとまとめにすることが出来ます。実運用で Forge を使ってみたいがプログラムに自信がないといった方は、一度、ご確認いただければと思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094fb13b970d-pi" style="display: inline;"><img alt="C5458314-976c-f4f3-1f5d-501b8b8388cc" class="asset  asset-image at-xid-6a0167607c2431970b01bb094fb13b970d img-responsive" src="/assets/image_56028.jpg" style="width: 650px;" title="C5458314-976c-f4f3-1f5d-501b8b8388cc" /></a></p>
<p>短い時間でプロセスの自動化が出来るので、IoT 機器との接続も含め、各地のハッカソンでも多数のチームが利用していました。</p>
<p>もし、開発時のテスト ツールとして Forge の RESTful API をお試しいただく場合には、以前、ご紹介した Postman も有用です。</p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/understanding-steps-to-use-viewer-on-postman.html" target="_blank">Postman による Viewer 利用手順の理解 - 2 legged 認証</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/restful-api-and-testing-tools.html" target="_blank">RESTful API とテスト ツール</a></strong></p>
<p>Toshiaki Isezaki</p>

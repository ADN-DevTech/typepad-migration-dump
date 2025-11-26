---
layout: "post"
title: "DevCon 2017 : Forge Platform API の拡張詳細"
date: "2017-11-14 13:12:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/11/forge-devcon-2017-expand-forge-platform-api-detail.html "
typepad_basename: "forge-devcon-2017-expand-forge-platform-api-detail"
typepad_status: "Draft"
---

<p>米国 Las Vegas で Forge DevCon 2017 が開催されています。キーノートでは、新機軸の発表がいくつかありましたが、既に告知されているものを含め、詳細をお知らせ出来るものだけ、ここで触れておきたいと思います。</p>
<hr />
<p><strong>Reality Capture API</strong></p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c933ccdd970b-pi" style="display: inline;"><img alt="Relity_capture_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c933ccdd970b image-full img-responsive" src="/assets/image_214622.jpg" title="Relity_capture_api" /></a></p>
<p style="padding-left: 30px;">いままで Beta 扱いだった&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/reality-capture-api-and-related-products.html" rel="noopener noreferrer" target="_blank">Reality Capture API</a></strong>&#0160;が正式にサポートされるようになっています。これを受けて、ドキュメントがデベロッパ ポータル&#0160; <strong><a href="https://developer.autodesk.com/en/docs/reality-capture/v1/overview/" rel="noopener noreferrer" target="_blank">https://developer.autodesk.com/en/docs/reality-capture/v1/overview/</a></strong>で公開され、Client ID と Client Secret を取得するアプリ登録でも、対象 API に Reality Capture API が選択出来るようになっています。</p>
<p style="padding-left: 30px;">サンプル コードは GitHub 上の&#0160;<strong><a href="https://github.com/Autodesk-Forge/photo-to-3d-sample">https://github.com/Autodesk-Forge/photo-to-3d-sample</a></strong> で公開されています。このサンプルを利用するには、3-leggeed OAuth で取得する Access Token が必要となります。デベロッパ ポータルのアプリ登録時に、適切なコールバック URL&#0160;<span class="x-content-block">http://localhost.autodesk.com/callback が設定され、Host ファイルで Localhost のマッピングが正しく設定されていることを確認してください（</span>127.0.0.1 localhost.autodesk.com）。なお、ReCap 360 の仕様変更に伴って、このサンプルも今後改訂される可能性があります。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" gesture="media" height="281" src="https://www.youtube.com/embed/PppGXEfc6p0?feature=oembed" width="500"></iframe></p>
<hr />
<p><strong>Design Automation API</strong></p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09d6f018970d-pi" style="display: inline;"><img alt="Design_automation_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09d6f018970d image-full img-responsive" src="/assets/image_411989.jpg" title="Design_automation_api" /></a></p>
<p style="padding-left: 30px;">Design Automation API で実行出来るコアエンジンに、従来の AutoCAD に加えて、Inventor と Revit が加わりました。ただ、両者の扱いは Private Beta 版としての扱いになりますので、お試しになりたい方は Inventor の場合は&#0160;<a href="mailto:Inventoronforge@autodesk.com" rel="noopener noreferrer" target="_blank">Inventoronforge@autodesk.com</a>&#0160;まで、Revit の場合は&#0160;<a href="mailto:revitonforge@autodesk.com" rel="noopener noreferrer" target="_blank">revitonforge@autodesk.com</a>&#0160;まで直接メールをお送りください（英語）。&#0160;</p>
<p style="padding-left: 30px;">下記の動画は Inventor コアエンジンを利用した Design Automation API の Web アプリケーション例です。Inventor 上でパーツに定義したねじ フィーチャは、データサイズを低減するためにねじ山部分がテクスチャ マップされた画像になっています。この例では、Inventor 上で実際のねじ山のモデリングをおこなう&#0160;<strong><a href="https://apps.autodesk.com/INVNTOR/en/Detail/Index?id=2540506896683021779" rel="noopener noreferrer" target="_blank">coolOrange threadModeler</a></strong> アドインを&#0160;Design Automation API を使ってクラウド上で稼働させ、元のパーツとネジ山が生成されたパーツを Forge Viewer 上で比較表示しています。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" gesture="media" height="281" src="https://www.youtube.com/embed/xxSygTWSDbM?feature=oembed" width="500"></iframe></p>
<p style="padding-left: 30px;">下記の動画は Revit コアエンジンを利用した Design Automation API の Web アプリケーション例です。 指定したパラメータに準じた階段を自動生成して、生成された Revit プロジェクト ファイルを A360 ストレージに保存しています。&#0160;</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" gesture="media" height="281" src="https://www.youtube.com/embed/02h6PpCUVpA?feature=oembed" width="500"></iframe>&#0160;</p>
<hr />
<p><strong>WebHooks API</strong></p>
<p style="padding-left: 30px;">&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09d6f01c970d-pi" style="display: inline;"><img alt="Webhooks_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09d6f01c970d image-full img-responsive" src="/assets/image_931207.jpg" title="Webhooks_api" /></a></p>
<p style="padding-left: 30px;">クラウド ストレージ上のイベントを監視して、指定したクライアントに通知するメカニズム、通称、WebHook が利用出来るようになります。A360 や BIM 360 ストレージ上のフォルダとファイルを監視して、それらの名前変更、削除、追加、コピー、移動のイベントを得る事が可能です。披露されたサンプルでは、<a href="https://twilio.kddi-web.com/" rel="noopener noreferrer" target="_blank"><strong>twilio</strong></a> Web サービス APIで電話をかけたり、<strong><a href="https://postmarkapp.com/" rel="noopener noreferrer" target="_blank">Postmark</a></strong> Web サービス API でメールで通知をしたり、チーム コミュニケーション ツール&#0160;<strong><a href="https://slack.com/" rel="noopener noreferrer" target="_blank">Slack</a></strong>&#0160;上に通知を発生させたりするなど、他のイベントのトリガとなる実装を見る事が出来るはずです。デモ動画がありますで、ご確認ください。他社の Web サービス API&#0160; との連携も含め、何に利用するかはアイデア次第です。WebHook&#0160; API をお試しになりたい方は、<a href="mailto:forge.webhooks.beta.appsupport@autodesk.com">forge.webhooks.beta.appsupport@autodesk.com</a>&#0160;まで直接メールをお送りください（英語）。&#0160;</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" gesture="media" height="344" src="https://www.youtube.com/embed/3TlWtMQ5jXs?feature=oembed" width="459"></iframe></p>
<hr />
<p><strong>BIM 360 API</strong></p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09de8e93970d-pi" style="display: inline;"><img alt="Bim_360_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09de8e93970d image-full img-responsive" src="/assets/image_605117.jpg" title="Bim_360_api" /></a></p>
<p style="padding-left: 30px;">BIM 360 Team や Docs ストレージにアクセス出来るようになったばかりでなく、BIM 360 Docs 上にフォルダを作成したりするこもも可能になりました。WebHooks API との連携も考慮すれば、BIM 360&#0160; との統合がより深化を期待出来ます。すでに数多くの BIM 360 連携アプリが Autodesk App Store で<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/10/autodesk-app-store-and-bim-360-apps.html" rel="noopener noreferrer" target="_blank">公開</a></strong>されています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://apps.autodesk.com/BIM360/ja/Home/Index" rel="noopener noreferrer" style="display: inline;" target="_blank"><img alt="Bim_360_apps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c5da1f970c image-full img-responsive" src="/assets/image_317626.jpg" title="Bim_360_apps" /></a></p>
<hr />
<p>By Toshiaki Isezaki</p>

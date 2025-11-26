---
layout: "post"
title: "Forge 課金について"
date: "2018-01-22 00:03:32"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html "
typepad_basename: "about-charging-to-forge"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">Autodesk Flex への移行に伴い、2022年3月29日からクラウドクレジットの価格が変更されています。詳しくは、<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/upcoming-forge-pricing-changes.html" rel="noopener" target="_blank"><strong>クラウドクレジット価格変更について</strong></a> の記事をご参照ください。下記のクラウドクレジット価格は旧価格になりますのでご注意ください。</span></p>
<p>リリース済の Forge Platform API には、いくつかの異なる API が用意されています。それぞれ、認証で利用する&#0160;<strong>Authentication API（OAuth API）</strong>、クラウド ストレージにデザイン データをアップロード/ダウンロードしたり、ストレージのデータ構造にアクセスする&#0160;<strong>Data Management API</strong>、アップロードしたデザイン データを Viewer 表示用や別のデザイン データ ファイル形式に変換する&#0160;<strong>Model Derivative API</strong>、変換されたデザイン データを Web ブラウザで表示、操作する&#0160;<strong>Viewer（Forge Viewer）</strong>、AutoCAD のコアプロセスをクラウド上で実行してバッチ処理を実現する&#0160;<strong>Design Automation API</strong>、空撮した写真から 3D メッシュモデルや点群、オルソ画像を生成する <strong>Reality Capture API</strong> があります。このうち、<strong>課金対象は Model Derivative API と Design Automation API、Reality Capture API の 3 種類</strong>のみで、Data Management API と BIM 360 API、Forge Viewer は無償です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c93d7126970b-pi" style="display: inline;"><img alt="Charge_target_apis" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c93d7126970b image-full img-responsive" src="/assets/image_931212.jpg" title="Charge_target_apis" /></a></p>
<p>さて、気になるクラウド クレジットの価格ですが、米国では 100 クラウド クレジット 100 US ドルなので、1 クラウド クレジットが 1 US ドルと考えることが出来ます。参考までに、日本では 100 クラウド クレジット 16,000 円（税抜き）で販売しているので、1 クラウド クレジット 160 円程度と考えることが出来ます。ちなみに、英国を除くヨーロッパでは、100 クラウド クレジットが 440 ユーロ、英国では 375 ポンドで販売されています。</p>
<p>それでは、API 毎に課金例を見ていきます。</p>
<hr />
<p><strong>Model Derivative API</strong></p>
<p>Model Derivative API では、デザイン ファイルを変換する度に特定のクラウド クレジットが消費されます。消費は、変換するデザイン ファイルの種類に応じてコンプレックス ジョブとシンプル ジョブに分けられます。現在、変換に時間を要する Revit の RVT ファイルと Navisworks の NWD ファイルがコンプレックス ジョブとして処理され、それ以外のデザイン ファイルは、すべてシンプル ジョブとして処理されます。コンプレックス ジョブの場合、1 回の変換処理で 1.5 クラウド クレジットが消費され、シンプル ジョブの場合、1 回の変換処理で 0.2 クラウド クレジットが消費されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09a1805b970d-pi"><img alt="Charge_detail_of_model_derivative_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09a1805b970d image-full img-responsive" src="/assets/image_748142.jpg" title="Charge_detail_of_model_derivative_api" /></a></p>
<p>続いて、いくつかの例をご紹介しましょう。<strong>毎週 AutoCAD の DWG ファイルを 500 個を変換して Forge Viewer で表示する場合</strong>、シンプル ジョブで処理されることになるので、週 5 日、月 4 週の稼働と想定すると、<strong>月 400 クラウド クレジットを消費</strong>することになり、<strong>400 US ドルが必要</strong>となる計算になります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c7c5d7970c-pi" style="display: inline;"><img alt="Model_derivative_api_example1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c7c5d7970c image-full img-responsive" src="/assets/image_140850.jpg" title="Model_derivative_api_example1" /></a></p>
<p><strong>毎日 Revit プロジェクト（RVT ファイル） を 10 個を IFC ファイルに変換して配布する場合</strong>、コンプレックス ジョブで処理されることになり、週 5 日、月 4 週の稼働と想定で、<strong>月 450 クラウド クレジットが消費</strong>されて、<strong>450 US ドルが必要</strong>となる計算になります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c93d718c970b-pi" style="display: inline;"><img alt="Model_derivative_api_example2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c93d718c970b image-full img-responsive" src="/assets/image_68564.jpg" title="Model_derivative_api_example2" /></a></p>
<p><strong>毎日 SolidWorks モデル（SLDPART ファイル、SLDASM ファイル） を 1,000 個を Viewer 表示用に変換して BOM 情報を取得する場合</strong>、シンプル ジョブで処理されるので、週 5 日、月 4 週の稼働では、<strong>月 4,000 クラウド クレジットを消費</strong>して&#0160;<strong>4,000 US ドルが必要</strong>となる計算になります。この場合、アセンブリ ファイルが複数のパーツ ファイルやサブ アセンブリ ファイルを参照していても、ZIP ファイルで 1 つに圧縮して 1 ジョブとして処理できるので、参照ファイルがいくつあっても、1 回の処理で消費されるのは 0.2 クラウド クレジットと考えることが出来ます。Inventor モデルや AutoCAD の DWG ファイルで複数の外部参照を持つ場合も同様です。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c7c5c8970c-pi" style="display: inline;"><img alt="Model_derivative_api_example3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c7c5c8970c image-full img-responsive" src="/assets/image_578114.jpg" title="Model_derivative_api_example3" /></a></p>
<p>注意が必要なのは、Viewer 機能を持つオートデスクのクラウド サービスと併用する場合です。もし、エンドユーザが A360 Team や Fusion Team、BIM 360 Team や BIM 360 Docs のストレージにデザイン ファイルをアップロードして管理している場合、デザイン ファイルのアップロード後、クラウド サービス自身が自動的に Viewer 表示用にファイルを変換処理します。この場合、Forge を利用するアプリ側で Model Derivative API を呼び出して変換処理する必要はありません。つまり、無償で Viewer 表示することが出来ます。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09e082e9970d-pi" style="display: inline;"><img alt="Model_derivative_api_example4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09e082e9970d image-full img-responsive" src="/assets/image_455598.jpg" title="Model_derivative_api_example4" /></a>&#0160;</p>
<hr />
<p><strong>Design Automation API</strong></p>
<p>Model Derivative API と同様に、Design Automation API もクラウド クレジットの消費で課金されます。ただし、明示的な変換処理毎にクラウド クレジットが消費されるのではなく、クラウド上で処理に要した時間（ 1 時間単位の CPU 時間）を消費対象とする点が異なりす。実際の処理時間は、AutoCAD アドインとしてアップロードしたカスタム処理の時間に依存することになります。1 時間あたりの消費クラウド クレジットは 4.0 となっています。</p>
<p>Design Automation API で実行させるバッチ処理は前述のアドインの内容に依りますが、ここでは、Viewer 表示用に拡張エンティティ データ（eXtened Entity Data、XData）付きで DWG ファイルを変換する処理を考えてみます。Viewer 表示用の変換（SVF ファイルに変換）は、本来、Model Derivative API の役割ですが、Model Derivative API で変換すると、DWG ファイル内の XData は一律に削除されてしまいます。Design Automation API には、XData 付きで SVF ファイル変換を実行する Activity が用意されています。</p>
<p>そこで、<strong>XData 付きの DWG → SVF 変換を毎日 450 回実施</strong>するために&#0160;<strong>2.5 CPU 時間必要</strong>と想定した場合、週 5 日、月 4 週の稼働で、<strong>月 200 クラウド クレジットを消費</strong>することになり、<strong>200&#0160;US ドルが必要</strong>となる計算が成り立ちます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09e082df970d-pi" style="display: inline;"><img alt="Design_automation_api_example1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09e082df970d image-full img-responsive" src="/assets/image_367134.jpg" title="Design_automation_api_example1" /></a>&#0160;</p>
<hr />
<p><strong>Reality Capture API</strong></p>
<p>長らく Beta 扱いだった Reality Capture APIですが、2017 年 11 月の段階で正式にリリースされて Forge Platform API に加わりました。同時に消費するクラウドクレジットも公開されています。Beta 版だった際には、3D モデル生成に必要な CPU 時間（演算時間）の適用が想定されていましたが、最終的に使用する写真素材の画素数と枚数とで相対的な数を積算する方法に変更されています。これによって、演算時のクラウド インフラのパフォーマンスに左右されることなく、常に同じクラウド クレジットが消費されることになります。</p>
<p>基本的考え方は、写真画素数と写真枚数の総計を 1 ギガピクセルあたり 3.5 クラウドクレジットを消費する、というものです。例えば、5 メガ ピクセルの画像を 1000 枚使用して 3D モデル（含む ルソ画像や点群）を生成する場合、総計で利用される画素数が 5 メガ ピクセル×1000&#0160; 枚＝5 ギガピクセルとなり、3.5 クラウドクレジット×5 で 17.5 クラウドクレジットを消費する計算です。金額は 17.5 US ドルになります。</p>
<p>同様に、40&#0160;メガ ピクセルの画像を 500 枚使用して 3D モデル（含む ルソ画像や点群）を生成する場合、総計で利用される画素数が 40 メガ ピクセル×500&#0160; 枚＝20 ギガピクセルとなり、3.5 クラウドクレジット×20 で 70 クラウドクレジットを消費する計算です。金額は 70 US ドルになります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c93d71a4970b-pi" style="display: inline;"><img alt="Reality_capture_api_example1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c93d71a4970b image-full img-responsive" src="/assets/image_847561.jpg" title="Reality_capture_api_example1" /></a></p>
<hr />
<p>Autodesk Forge では、従量課金によってクラウド クレジットを消費します。いくつかの例を見てきましたが、かなりヘビーな運用を想定した例と言えます。実際には、月に何回デザイン ファイルを変換するか（Model Derivative API）、月にどの程度 CPU 時間を使用したバッチ処理を実行するか（Design Automation API）、によって消費するクラウド クレジットが変わることになります。トライアル期間中は、Forge を実質無償で利用することが出来ます。なるべく早めに、実際の実稼働を想定したテスト運用で、実際の消費量を計測していただことをお勧めします。</p>
<p>By Toshiaki Isezaki</p>

---
layout: "post"
title: "欧米での Forge 課金の仕組み"
date: "2017-01-06 00:07:33"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/01/charge-scheme-for-forge-at-us-and-europe.html "
typepad_basename: "charge-scheme-for-forge-at-us-and-europe"
typepad_status: "Publish"
---

<p>以前、このブログでも&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/11/forge_business_model_and_changes_for_charging_scheme.html" rel="noopener noreferrer" target="_blank">ご案内</a>&#0160;</strong>しましたが、米国とヨーロッパでは、Autodesk Forge の利用課金が始まっています。課金の方法は、当初予定していたクレジットカードを使った現金による Subscription から、<strong><a href="https://knowledge.autodesk.com/ja/customer-service/account-management/subscription-management/subscription/cloud-services/autodesk-360-cloud-credits-faq" rel="noopener noreferrer" target="_blank">クラウド クレジット</a></strong>&#0160;による Subscription に変更されています。日本での課金開始時期は未定で、当面の間、無償、無制限で Forge をお使いいただくことが出来るのも、前回お知らせしたとおりです。今回は、参考までに、欧米で開始されたクラウド クレジットによる課金方法について、その詳細と、いくつかの例をご紹介しておきたいと思います。もちろん、日本での課金開始時には、内容が変更される可能性はありますのでご注意ください。</p>
<p>ご存じのとおり、現在、正式にリリース済の Forge Platform API には、いくつかの異なる API が用意されています。それぞれ、認証で利用する <strong>Authentication API（OAuth API）</strong>、クラウド ストレージにデザイン データをアップロード/ダウンロードしたり、ストレージのデータ構造にアクセスする <strong>Data Management API</strong>、アップロードしたデザイン データを Viewer 表示用や別のデザイン データ ファイル形式に変換する <strong>Model Derivative API</strong>、変換されたデザイン データを Web ブラウザで表示、操作する <strong>Viewer（Forge Viewer）</strong>、AutoCAD のコアプロセスをクラウド上で実行してバッチ処理を実現する <strong>Design Automation API</strong> があります。このうち、<strong>課金対象は Model Derivative API と Design Automation API の 2 種類</strong>のみで、Data Management API と Forge Viewer は、無償です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0966f64c970d-pi" style="display: inline;"><img alt="Charge_target_apis" class="asset  asset-image at-xid-6a0167607c2431970b01bb0966f64c970d img-responsive" src="/assets/image_133699.jpg" style="width: 700px;" title="Charge_target_apis" /></a></p>
<p>さて、気になるクラウド クレジットの価格ですが、米国では 100 クラウド クレジット 100 US ドルなので、1 クラウド クレジットが 1 US ドルと考えることが出来ます。参考までに、日本では 100 クラウド クレジット 16,000 円（税抜き）で販売しているので、1 クラウド クレジット 160 円程度と考えることが出来ます。ちなみに、英国を除くヨーロッパでは、100 クラウド クレジットが 440 ユーロ、英国では 375 ポンドで販売されています。</p>
<p>それでは、API 毎に課金例を見ていきます。</p>
<hr />
<p><strong>Model Derivative API</strong></p>
<p style="padding-left: 30px;">Model Derivative API では、デザイン ファイルを変換する度に特定のクラウド クレジットが消費されます。消費は、変換するデザイン ファイルの種類に応じてコンプレックス ジョブとシンプル ジョブに分けられます。現在、変換に時間を要する Revit の RVT ファイルと Navisworks の NWD ファイルがコンプレックス ジョブとして処理され、それ以外のデザイン ファイルは、すべてシンプル ジョブとして処理されます。コンプレックス ジョブの場合、1 回の変換処理で 1.5 クラウド クレジットが消費され、シンプル ジョブの場合、1 回の変換処理で 0.2 クラウド クレジットが消費されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09a1805b970d-pi" style="display: inline;"><img alt="Charge_detail_of_model_derivative_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09a1805b970d image-full img-responsive" src="/assets/image_748142.jpg" title="Charge_detail_of_model_derivative_api" /></a></p>
<p style="padding-left: 30px;">続いて、いくつかの例をご紹介しましょう。<strong>毎週 AutoCAD の DWG ファイルを 500 個を変換して Forge Viewer で表示する場合</strong>、シンプル ジョブで処理されることになるので、週 5 日、月 4 週の稼働と想定すると、<strong>月 400 クラウド クレジットを消費</strong>することになり、<strong>400 US ドルが必要</strong>となる計算になります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c4094b970b-pi" style="display: inline;"><img alt="Model_derivative_api_example1" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c4094b970b img-responsive" src="/assets/image_169700.jpg" style="width: 700px;" title="Model_derivative_api_example1" /></a></p>
<p style="padding-left: 30px;"><strong>毎日 Revit プロジェクト（RVT ファイル） を 10 個を IFC ファイルに変換して配布する場合</strong>、コンプレックス ジョブで処理されることになり、週 5 日、月 4 週の稼働と想定で、<strong>月 450 クラウド クレジットが消費</strong>されて、<strong>450 US ドルが必要</strong>となる計算になります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c40952970b-pi" style="display: inline;"><img alt="Model_derivative_api_example2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c40952970b img-responsive" src="/assets/image_479327.jpg" style="width: 700px;" title="Model_derivative_api_example2" /></a></p>
<p style="padding-left: 30px;"><strong>毎日 SolidWorks モデル（SLDPART ファイル、SLDASM ファイル） を 1,000 個を Viewer 表示用に変換して BOM 情報を取得する場合</strong>、シンプル ジョブで処理されるので、週 5 日、月 4 週の稼働では、<strong>月 4,000 クラウド クレジットを消費</strong>して&#0160;<strong>4,000 US ドルが必要</strong>となる計算になります。この場合、アセンブリ ファイルが複数のパーツ ファイルやサブ アセンブリ ファイルを参照していても、ZIP ファイルで 1 つに圧縮して 1 ジョブとして処理できるので、参照ファイルがいくつあっても、1 回の処理で消費されるのは 0.2 クラウド クレジットと考えることが出来ます。Inventor モデルや AutoCAD の DWG ファイルで複数の外部参照を持つ場合も同様です。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c40bdb970b-pi" style="display: inline;"><img alt="Model_derivative_api_example3" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c40bdb970b img-responsive" src="/assets/image_929028.jpg" style="width: 700px;" title="Model_derivative_api_example3" /></a></p>
<p style="padding-left: 30px;">注意が必要なのは、Viewer 機能を持つオートデスクのクラウド サービスと併用する場合です。もし、エンドユーザが A360 Team や Fusion Team、BIM 360 Team や BIM 360 Docs のストレージにデザイン ファイルをアップロードして管理している場合、デザイン ファイルのアップロード後、クラウド サービス自身が自動的に Viewer 表示用にファイルを変換処理します。この場合、Forge を利用するアプリ側で Model Derivative API を呼び出して変換処理する必要はありません。つまり、無償で Viewer 表示することが出来ます。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0966f91a970d-pi" style="display: inline;"><img alt="Model_derivative_api_example4" class="asset  asset-image at-xid-6a0167607c2431970b01bb0966f91a970d img-responsive" src="/assets/image_171281.jpg" style="width: 700px;" title="Model_derivative_api_example4" /></a>&#0160;</p>
<hr />
<p><strong>Design Automation API</strong></p>
<p style="padding-left: 30px;">Model Derivative API と同様に、Design Automation API もクラウド クレジットの消費で課金されます。ただし、明示的な変換処理毎にクラウド クレジットが消費されるのではなく、クラウド上で処理に要した時間（ 1 時間単位の CPU 時間）を消費対象とする点が異なりす。実際の処理時間は、AutoCAD アドインとしてアップロードしたカスタム処理の時間に依存することになります。1 時間あたりの消費クラウド クレジットは 4.0 となっています。</p>
<p style="padding-left: 30px;">Design Automation API で実行させるバッチ処理は前述のアドインの内容に依りますが、ここでは、Viewer 表示用に拡張エンティティ データ（eXtened Entity Data、XData）付きで DWG ファイルを変換する処理を考えてみます。Viewer 表示用の変換（SVF ファイルに変換）は、本来、Model Derivative API の役割ですが、Model Derivative API で変換すると、DWG ファイル内の XData は一律に削除されてしまいます。Design Automation API には、XData 付きで SVF ファイル変換を実行する Activity が用意されています。</p>
<p style="padding-left: 30px;">そこで、<strong>XData 付きの DWG → SVF 変換を毎日 450 回実施</strong>するために <strong>2.5 CPU 時間必要</strong>と想定した場合、週 5 日、月 4 週の稼働で、<strong>月 200 クラウド クレジットを消費</strong>することになり、<strong>200&#0160;US ドルが必要</strong>となる計算が成り立ちます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0966f920970d-pi" style="display: inline;"><img alt="Design_automation_api_example1" class="asset  asset-image at-xid-6a0167607c2431970b01bb0966f920970d img-responsive" src="/assets/image_275084.jpg" style="width: 700px;" title="Design_automation_api_example1" /></a>&#0160;</p>
<hr />
<p>Autodesk Forge では、従量課金によってクラウド クレジットを消費します。いくつかの例を見てきましたが、かなりヘビーな運用を想定した例と言えます。実際には、月に何回デザイン ファイルを変換するか（Model Derivative API）、月にどの程度 CPU 時間を使用したバッチ処理を実行するか（Design Automation API）、によって消費するクラウド クレジットが変わることになります。日本では、現在、無償、無制限で Forge を利用することが出来るので、なるべく早めに、実際の実稼働を想定したテスト運用で、実際の消費量を計測していただことをお勧めします。</p>
<p>By Toshiaki Isezaki</p>

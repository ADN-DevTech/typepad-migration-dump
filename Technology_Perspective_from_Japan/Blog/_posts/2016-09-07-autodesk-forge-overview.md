---
layout: "post"
title: "Autodesk Forge Platform API（2016年9月時点）"
date: "2016-09-07 00:31:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/09/autodesk-forge-overview.html "
typepad_basename: "autodesk-forge-overview"
typepad_status: "Publish"
---

<p>Autodesk Forge はクラウドを利用する &quot;接続された&quot; エコシステムを構築するためのクラウド プラットフォームです。オートデスクの クラウド サービスで使用しているさまざまな機能を、Web サービス API &#0160;として提供しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a211970c-pi" style="display: inline;"><img alt="Paas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a211970c image-full img-responsive" src="/assets/image_763591.jpg" title="Paas" /></a></p>
<p>クラウドをベースにした Forge の特徴には、オートデスク ソフトウェアが保存するデザイン データ形式のみではなく、一般的なデザイン データも一律に変換して Web ブラウザで利用できる環境の提供があります。現在、他社 CAD のデザイン データ形式を含め、約 60 種類のデザイン データ形式のファイル変換をサポートしています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb093e933d970d-pi" style="display: inline;"><img alt="Deta_translation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb093e933d970d image-full img-responsive" src="/assets/image_141147.jpg" title="Deta_translation" /></a></p>
<p>もちろん、デザイン データはクラウドに保存することになりますが、これによって、各種クライアントからデザイン データを中核としたワークフローの構築が可能になります。また、CAD ソフトウェアの経験がなくとも、Web 開発の手法でデザイン データを活用できる利点も生まれます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c89b225f970b-pi" style="display: inline;"><img alt="Forge_concept" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c89b225f970b image-full img-responsive" src="/assets/image_927192.jpg" title="Forge_concept" /></a></p>
<p>クラウド サービス（SaaS）と同様に、今後も継続して Forge Platform API （PaaS）を強化していく予定です。このため、その内容や機能が常に変化していくと考えることが出来ます。</p>
<p>ここでは、現時点での Autodesk Forge を、API 毎にまとめておきたいと思います。2016年9月現在、Forge Platform API には、次の API が提供されています。各 API は、単独で利用するのではなく、複数を同時に共用するような利用方法となります。&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>OAuth API&#0160;（Authentication API） <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb092e4d30970d-pi" style="float: right;"><img alt="Icon - oAuthen" class="asset  asset-image at-xid-6a0167607c2431970b01bb092e4d30970d img-responsive" src="/assets/image_801711.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - oAuthen" /></a><br /></strong></p>
<p style="padding-left: 60px;">Forge プラットフォームにアクセスするための認証と許可を得るためのオープン スタンダードです。3rd party デベロッパがユーザ資格情報を漏えいすることなく、制限された権限で特定機能の実行を可能にする“キー”を用いる安全な方法です。2-legged 認証、3-legged 認証をサポートします。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb092e4d44970d-pi" style="display: inline;"><img alt="3_legged_authentication_steps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb092e4d44970d image-full img-responsive" src="/assets/image_799148.jpg" title="3_legged_authentication_steps" /></a>&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Model Derivative API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88af0c6970b-pi" style="float: right;"><img alt="Icon - model derivative api" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88af0c6970b img-responsive" src="/assets/image_665286.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - model derivative api" /></a></strong></p>
<p style="padding-left: 60px;">Viewer を使ったオンライン表示準備の変換も含め、ある形式から他のデザイン ファイルに変換します。変換時には、モデルの持つ階層構造やジオメトリ データを展開をさせることも出来ます。展開データを他のアプリケーションに渡して、重要なデザイン情報のコミュニケーションに流用出来ます。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a254970c-pi" style="display: inline;"><img alt="Model_derivative_api" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a254970c img-responsive" src="/assets/image_919906.jpg" title="Model_derivative_api" /></a>&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Data Management API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88af0d3970b-pi" style="float: right;"><img alt="Icon - data management" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88af0d3970b img-responsive" src="/assets/image_452682.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - data management" /></a></strong></p>
<p style="padding-left: 60px;">A360、Fusion 360、BIM 360 Docs と Forge ネイティブなObject Storage Service（OSS）のデータを管理します。この&#0160;API は、1 つの一貫した方法で、異なるオートデスク製品から生成されたデータ ファイルのアップロードとダウンロードを可能にします。つまり、いままで実現出来なかった、A360 をはじめとするオートデスクの SaaS が利用するユーザ アカウント領域へのアク<br />セスが出来るようになります。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a268970c-pi" style="display: inline;"><img alt="Data_management_api" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a268970c img-responsive" src="/assets/image_59712.jpg" title="Data_management_api" /></a>&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Viewer <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a26a970c-pi" style="float: right;"><img alt="Icon - viewer" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a26a970c img-responsive" src="/assets/image_357690.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - viewer" /></a></strong></p>
<p style="padding-left: 60px;">50 種類を超えるデザイン データを Model Derivative API でクラウド上で変換して、オリジナル データが持つ属性情報や外観を維持したままストリーミング配信するビューアテクノロジです。</p>
<p style="padding-left: 60px;">配信データの閲覧には WebGL 対応の Web ブラウザがあれば何もインストールする必要はありません。属性抽出や検索、モデルの断面化や分解、環境光変更などの表示制御に JavaScript API を提供します。</p>
<p style="padding-left: 60px;">JavaScript モジュール単位で拡張できる&#0160;Extension フレームワークを利用すれば、グラフ集計や IoT 機器モニタ機能の追加など、標準のビューア機能に独自機能を組み込むことも容易です。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88af0f9970b-pi" style="display: inline;"><img alt="Viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88af0f9970b image-full img-responsive" src="/assets/image_413224.jpg" title="Viewer" /></a>&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Design Automation API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88af10b970b-pi" style="float: right;"><img alt="Icon - design automation api" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88af10b970b img-responsive" src="/assets/image_400493.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - design automation api" /></a></strong></p>
<p style="padding-left: 60px;">設計作業の定番である AutoCAD から、オーバヘッドとなる UI を除去して作成した実行形式 accoreconsole.exe をクラウド上で実行させるサービスです。ダイアログボックスなど、UI を表示しないアドイン モジュールを実行してバッチ処理させることが出来ます。AutoCAD .NET API(C＃ または、VB.NET)、ObjectARX(C++) を用いたカスタム処理を実装可能です。クラウドとのコミュニケーションには OData プロトコルを用います。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb092e4e78970d-pi" style="display: inline;"><img alt="Design_automation_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb092e4e78970d image-full img-responsive" src="/assets/image_873550.jpg" title="Design_automation_api" /></a></p>
<p style="padding-left: 30px;">&#0160;</p>
<p>ここまで記載した API は正式にリリースされている API ですが、 ベータ版として提供されている API も存在しています。これらは、まもなく、正式リリースとなる予定です。</p>
<p style="padding-left: 30px;">&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>BIM 360 API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88af20a970b-pi" style="float: right;"><img alt="Bim-360-badge-400px-social" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88af20a970b img-responsive" src="/assets/image_250006.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Bim-360-badge-400px-social" /></a><br /><br /></strong></p>
<p style="padding-left: 60px;">BIM 360 クラウドサービスは、複数の企業や組織が設計に参画する建設業において、一貫したデータ整合性の維持や管理、現場とのコラボレーションを含む、さまざまな機能を複数のサービスで提供します。</p>
<p style="padding-left: 60px;">BIM 360 API を利用すれば、 BIM 360 プラットフォームに接続して、RESTful API によって各種プロセスを自動化したり、独自のワークフローに組み込むシステム インテグレーションが可能です。&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Reality Capture API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c88af137970b-pi" style="float: right;"><img alt="Icon - reality capture" class="asset  asset-image at-xid-6a0167607c2431970b01b7c88af137970b img-responsive" src="/assets/image_842648.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon - reality capture" /></a></strong></p>
<p style="padding-left: 60px;">&#0160;対象物を異なる角度で撮影した複数の写真から、3D メッシュ モデルを生成するクラウド演算サービスです。生成されたメッシュモデルや点群をダウンロードすれば、CAD や CG ツールで 3D モデルを再利用することも出来ます。</p>
<p style="padding-left: 60px;">また、オルソ画像や写真ファイルのアップロードや品質の指定や演算進捗のチェック、メッシュデータのダウンロードなど、一連の処理を自動化させるための RESTful API を提供。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb092e4e10970d-pi" style="display: inline;"><img alt="Reality_capture_api" class="asset  asset-image at-xid-6a0167607c2431970b01bb092e4e10970d img-responsive" src="/assets/image_764369.jpg" title="Reality_capture_api" /></a></p>
<p style="padding-left: 30px;">&#0160;</p>
<p>ベータ版以外の API では、Viewer Extension として、次のような API も予定されています。</p>
<p>&#0160;</p>
<hr />
<p style="padding-left: 30px;"><strong>Rendering API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a514970c-pi" style="float: right;"><img alt="Icon-rendering-big" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a514970c img-responsive" src="/assets/image_34435.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon-rendering-big" /></a></strong></p>
<p style="padding-left: 60px;">AutoCAD やRevit、Navisworks、Fusion 360 で作成した 3D&#0160;デザイン データから、フォトリアリスティックなレンダリング画像を生成するクラウド演算サービスです。 Viewer の Extension として提供され、Rendering in A360 サービスの一部機能をRESTful API で利用できるようにする予定です。&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Markup API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a51f970c-pi" style="float: right;"><img alt="Icon-markup-big" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a51f970c img-responsive" src="/assets/image_932713.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon-markup-big" /></a></strong></p>
<p style="padding-left: 60px;">Viewer で表示した 3D モデルや 2D 図面にマークアップを記入する機能を提供します。 Viewer の Extension として提供される予定です。&#0160;</p>
<hr style="padding-left: 30px;" />
<p style="padding-left: 30px;"><strong>Comment API <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d214a525970c-pi" style="float: right;"><img alt="Icon-comments-big" class="asset  asset-image at-xid-6a0167607c2431970b01b8d214a525970c img-responsive" src="/assets/image_322039.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Icon-comments-big" /></a></strong></p>
<p style="padding-left: 60px;">Viewer で表示した 3D モデルや 2D 図面に関連付けられるコメントを作成する機能を提供します。 Viewer の Extension として提供される予定です。</p>
<p>&#0160;</p>
<p>今後とも、Autodesk Forge にご期待ください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>

---
layout: "post"
title: "Autodesk Forge プラットフォームとは"
date: "2016-01-27 02:35:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/01/autodesk-forge-platfotrm.html "
typepad_basename: "autodesk-forge-platfotrm"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b19481970d-pi" style="display: inline;"><img alt="Forge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b19481970d image-full img-responsive" src="/assets/image_901733.jpg" title="Forge" /></a></p>
<p><span style="background-color: #ffff00;">こちらに記載された情報は既に古くなっています。新しい情報は別の記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/05/about-autodesk-forge.html" rel="noopener noreferrer" target="_blank">Autodesk Forge とは </a></strong>をご確認ください。</span></p>
<p><strong>Autodesk&#0160;&#0160;Forge(<a href="http://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">http://forge.autodesk.com/</a>)</strong> は、クラウドを利用して ‘接続された’ エコ システムを構築する サービス や ツール、製品のためのプラットフォームです。</p>
<p>オートデスクのクラウド サービスを構成するさまざまなテクノロジを Web サービス API や SDK として提供する開発<strong>プラットフォーム</strong>、Forge を利用するアイデアや開発を助成する<strong>ファンド</strong>、コンファレンスやミートアップ、フォーラムを通して開発コミュニティをバックアップする<strong>デベロッパ プログラム</strong>で構成されています。</p>
<p>デザイン データの作成から活用、3D プリンタを利用した物理的な製作まで、様々な場面でオートデスクが標榜する <strong>Future</strong> <strong>Of</strong> <strong>Making</strong> <strong>Things</strong><strong> － 創造の未来 － </strong>をお手伝いします。<strong>Forge</strong>&#0160;<strong>プラットフォーム</strong>&#0160;では、5つのカテゴリ別に各種 API/SDK を提供します。オープンソースも含めた他の Web サービス API とも、用途に応じたマッシュアップが可能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b194ce970d-pi" style="display: inline;"><img alt="Forge_platfoem_category_explanation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b194ce970d image-full img-responsive" src="/assets/image_144095.jpg" title="Forge_platfoem_category_explanation" /></a></p>
<p>下記に、現段階で明確になっている API の概要をご紹介します。</p>
<hr />
<p><strong>デザイン</strong></p>
<p style="padding-left: 30px;"><strong>Fusion 360：</strong></p>
<p style="padding-left: 30px;">Fusion 360 は、Mac と Windows の両方での利用が可能な、今迄にない 3D CAD/CAM/CAE ツールです。製品開発に必要な全てのプロセスを、クラウド上のプラットフォームで繋ぐことができます。Fusion 360 内部で動作可能なスクリプトとアドインの両者をサポートする API を提供します。Python、JavaScript、C++ を使った開発が出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80cc08f970b-pi" style="display: inline;"><img alt="Fusion_image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80cc08f970b image-full img-responsive" src="/assets/image_510855.jpg" title="Fusion_image" /></a></p>
<p style="padding-left: 30px;"><strong>AutoCAD I/O：</strong></p>
<p style="padding-left: 30px;">設計作業の定番である AutoCAD から、オーバヘッドとなる UI を除去して作成した実行形式 accoreconsole.exe をクラウド上で実行させるサービスです。ダイアログボックスなど、UI を表示しないアドイン モジュールを実行してバッチ処理させることが出来ます。AutoCAD .NET API(C＃ または、VB.NET)、ObjectARX(C++) を用いたカスタム処理を実装可能です。クラウドとのコミュニケーションには OData プロトコルを用います。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d196dbd3970c-pi" style="display: inline;"><img alt="Autocad_io_image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d196dbd3970c image-full img-responsive" src="/assets/image_145090.jpg" title="Autocad_io_image" /></a>&#0160;</p>
<hr />
<p><strong>製作</strong></p>
<p style="padding-left: 30px;"><strong>Spark：</strong></p>
<p style="padding-left: 30px;">3D プリント ワークフローをサポートするオープンソース プラットフォームです。多様なデバイスや OS で 3D プリントを自動化するための API と SDK が用意されています。Spark の詳細は専用サイト <a href="https://spark.autodesk.com/developers">https</a><a href="https://spark.autodesk.com/developers">://</a><a href="https://spark.autodesk.com/developers">spark.autodesk.com/developers</a> でもご案内しています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d196dc27970c-pi" style="display: inline;"><img alt="Spark" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d196dc27970c image-full img-responsive" src="/assets/image_321484.jpg" title="Spark" /></a></p>
<hr />
<p><strong>ビジュアライズ</strong></p>
<p style="padding-left: 30px;"><strong>View</strong> <strong>and</strong> <strong>Data</strong> <strong>API</strong><strong>：</strong></p>
<p style="padding-left: 30px;">50 種類を超えるデザイン データをクラウド内で変換して、オリジナル データが持つ属性情報や外観を維持したままストリーミング配信する Viewer テクノロジです。配信データの閲覧には WebGL 対応の Web ブラウザがあれば何もインストールする必要はありません。クラウドとのコミュニケーションには RESTful API を、属性抽出や検索、モデルの断面化や分解、環境光変更などの表示制御には JavaScript API が提供します。</p>
<p style="padding-left: 30px;">独自機能を JavaScript モジュール単位で拡張する Extension フレームワークを利用すれば、 IoT 機器の管理インタフェースの実現や、業務システムへの組み込みも容易です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b195bb970d-pi" style="display: inline;"><img alt="View_and_data_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b195bb970d image-full img-responsive" src="/assets/image_221745.jpg" title="View_and_data_api" /></a></p>
<p style="padding-left: 30px;"><strong>ReCap</strong> <strong>Photo</strong><strong>：</strong></p>
<p style="padding-left: 30px;">異なる角度で対象物を撮影した複数の写真から、3D メッシュ モデルを生成するクラウド演算サービスです。生成されたメッシュ モデルをダウンロードすれば、CAD や CG ツールで 3D モデルを再利用することも出来ます。写真ファイルのアップロードや品質の指定や演算進捗のチェック、メッシュデータのダウンロードなど、一連の処理を自動化させるための RESTful API が提供されます。</p>
<p style="padding-left: 30px;"><strong>レンダリング（近日公開予定</strong><strong>）：</strong></p>
<p style="padding-left: 30px;">AutoCAD や Revit、Navisworks、Fusion 360で作成した 3D デザイン データから、フォトリアリスティックなレンダリング画像を生成するクラウド演算サービスです。Rendering in A360 サービスと同等の機能を RESTful API で提供する予定です。&#0160;</p>
<hr />
<p><strong>コラボレーション</strong></p>
<p style="padding-left: 30px;"><strong>BIM</strong> <strong>360</strong> <strong>Glue</strong><strong>：</strong></p>
<p style="padding-left: 30px;">複数の企業や組織が設計に参画する建設業では、施工現場での調整が必須です。そういったコラボレーションやワークフローを提供する BIM 360 Glue クラウド サービスを、RESTful API によって自動化したり、独自のワークフローに組み込むことが出来ます。</p>
<p style="padding-left: 30px;"><strong>マークアップ（近日公開予定</strong><strong>）：</strong></p>
<p style="padding-left: 30px;">View and Data API で表示した 3D モデルや 2D 図面にマークアップを記入する機能を提供します。View and Data API の Extension として提供される予定です。</p>
<p style="padding-left: 30px;"><strong>コメント（近日公開予定</strong><strong>）：</strong></p>
<p style="padding-left: 30px;">View and Data API で表示した 3D モデルや 2D 図面に関連付けられるコメントを作成する機能を提供します。View and Data API の Extension として提供される予定です。</p>
<hr />
<p>なお、Autodesk Forge には、今後も Web サービス API や SDK が順次追加されていく予定です。 まずは、今年6月に San Francisco で開催する予定の <strong><a href="https://forge.autodesk.com/devcon-2016" rel="noopener noreferrer" target="_blank">Forge Developer Conference</a></strong> に向けて、鋭意実装調整中です。</p>
<p><a class="asset-img-link" href="https://forge.autodesk.com/devcon-2016" rel="noopener noreferrer" style="display: inline;" target="_blank"><img alt="Forge_devcon" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80cc185970b image-full img-responsive" src="/assets/image_747380.jpg" title="Forge_devcon" /></a></p>
<p>今後の Autodesk Forge にご期待ください。</p>
<p>By Toshiaki Isezaki</p>

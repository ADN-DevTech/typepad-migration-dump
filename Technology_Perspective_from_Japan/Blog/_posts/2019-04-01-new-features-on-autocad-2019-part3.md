---
layout: "post"
title: "AutoCAD 2020 の新機能 ～ その3"
date: "2019-04-01 00:26:49"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-autocad-2019-part3.html "
typepad_basename: "new-features-on-autocad-2019-part3"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part2.html" rel="noopener" target="_blank">前回</a></strong>に引き続き、AutoCAD 2020 と AutoCAD LT 2020 に共通した機能として提供される具体的な内容についてご案内していきます。今回は、クラウドを使ったシームレスなワークフローに関係する機能をご案内していきます。</p>
<p><strong>AutoCAD Web アプリや</strong><strong>&#0160;AutoCAD モバイル アプリとの連携</strong></p>
<p>引き続き、Only One AutoCAD として、AutoCAD Web（英語版のみ）および AutoCAD モバイル アプリを使ったワークフローがサポートされています。実際に利用することになるのは、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-A7507398-50B1-4B00-B79B-EB99A068DACA" rel="noopener" target="_blank">SAVETOWEBMOBILE[Web およびモバイルに保存]</a></strong> コマンドと <strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-79C33702-07E6-4261-92E1-D69595ED1B6D">OPENFROMWEBMOBILE[Web およびモバイルから開く]</a> </strong>コマンド の 2 つです。</p>
<p>このワークフローでは、デスクトップからクラウドに書き出した図面を<strong><a href="https://web.autocad.com" rel="noopener" target="_blank"> AutoCAD Web アプリ（https://web.autocad.com）</a></strong>、ないし、<strong><a href="https://www.autodesk.co.jp/products/autocad-mobile/subscribe?ktvar002=knc_wwm_apac_jp_nc_ggl__autocad360-ex2018__ENGINE-_-GoogleJP__CAMPAIGN-_-JP_G_eStore_AutoCAD360_B__ADGROUP-_-AutoCAD360_B_Alone_Exact__KEYWORD-_-eStore_G_16719__MATCHTYPE-_-Exact__CREATIVE-_-263113230172__DEVICE-_-c&amp;mktvar002=knc_wwm_apac_jp_nc_ggl__autocad2019___ENGINE-_-GoogleJP__CAMPAIGN-_-JP_G_eStore_AutoCAD360_B__ADGROUP-_-AutoCAD360_B_Alone_Exact__CREATIVE-_-FY19Q1_OnlyOne2019__KEYWORD-_-autocad%20%E3%83%A2%E3%83%90%E3%82%A4%E3%83%AB%20%E3%82%A2%E3%83%97%E3%83%AA__DEVICE-_-c&amp;gclid=EAIaIQobChMIwNP8guec4QIVUa6WCh3sEQOTEAAYASAAEgLnpvD_BwE&amp;plc=AA360P&amp;term=1-YEAR&amp;support=ADVANCED&amp;quantity=1" rel="noopener" target="_blank">AutoCAD モバイル アプリ</a></strong> で利用して図面に編集を加え、再びクラウドからデスクトップで開いて編集を継続するもので、主に、図面の作成者がオフィス以外の場所で図面編集する目的て使用することを想定しています。余談となりますが、AutoCAD Web アプリでもダーク テーマを選択することも出来るようになっています。</p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/MAvTGOtpDoc?feature=oembed" width="500"></iframe></p>
<p>多数の関係者とのコラボレーションで図面自体に変更を加えられたくない場合には、クラウドで変換された読み取り専用の図面を利用する <strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-24E25A3A-3B2D-453A-BD23-D1AF2472114B" rel="noopener" target="_blank">共有ビュー</a></strong> の利用をお勧めします。共有ビューを使ったワークフローは、前バージョンの AutoCAD 2019 でご案内した動画がありますので、比較も含めてご確認ください。共有ビューは、AutoCAD 2020 でも [コラボレート] リボンタブから引き続き利用可能です。</p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/BBMw2fAMo_0?start=3&amp;feature=oembed" width="500"></iframe></p>
<p>なお、AutoCAD 2020 を使った AutoCAD Web および AutoCAD モバイル アプリを使ったワークフローでは、機能強化も加えられています。&#0160;1 点目は、外部参照のサポートです。AutoCAD で外部参照を含む図面を開き、&#0160;<a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-A7507398-50B1-4B00-B79B-EB99A068DACA"><strong>[Web およびモバイルに保存]</strong> </a>コマンドで図面を保存すると、外部参照図面を含めたファイルが ZIP 圧縮されたファイルが自動的に作成されて、クラウドに保存されます。圧縮されたファイルには拡張子が付いていませんが、AutoCAD Web や AutoCAD モバイル アプリで開いて編集することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a474bc1c200d-pi" style="display: inline;"><img alt="Autocad_web_xref" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a474bc1c200d image-full img-responsive" src="/assets/image_507503.jpg" title="Autocad_web_xref" /></a></p>
<p>機能強化の 2 点目は、CAD マネージャによる利用制限が可能になった点です。 AutoCAD 本体とは別インストールが必要な CAD マネージャ コントロール ユーティリティの [オンライン コンテンツ] タブには、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-A7507398-50B1-4B00-B79B-EB99A068DACA" rel="noopener" target="_blank">SAVETOWEBMOBILE[Web およびモバイルに保存]</a></strong> コマンドと <strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-79C33702-07E6-4261-92E1-D69595ED1B6D">OPENFROMWEBMOBILE[Web およびモバイルから開く]</a> </strong>コマンド を有効化するチェック ボックスが新設されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a473dc50200d-pi" style="display: inline;"><img alt="Cad_manager_tool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a473dc50200d img-responsive" src="/assets/image_639011.jpg" title="Cad_manager_tool" /></a></p>
<p>このチェックボックスからチェックを外すと、両コマンドは実行出来なくなる他、クイック アクセス ツールバーとアプリケーション メニューから該当するボタンとメニューが削除されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44aaa69200c-pi" style="display: inline;"><img alt="Cad_manager_control" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a44aaa69200c image-full img-responsive" src="/assets/image_872678.jpg" title="Cad_manager_control" /></a></p>
<p><strong>クラウド ストレージ サービスのサポート</strong></p>
<p>AutoCAD 2020 では、[保存]、[名前を付けて保存]、[開く]の各コマンドを使用するときに、複数のクラウド サービスプロバイダとのクラウド接続およびストレージがサポートされるようになりました。各種クラウド サービスの同期アプリケーションをインストールして、同期フォルダをファイル ダイアログに登録することで、サードパーティーのストレージ サービスに図面を保存したり、開いたりすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a474ba2d200d-pi" style="display: inline;"><img alt="Use_of_3rd_party_storage" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a474ba2d200d image-full img-responsive" src="/assets/image_59354.jpg" title="Use_of_3rd_party_storage" /></a></p>
<p>AutoCAD Web および AutoCAD モバイル アプリにもサードパーティーのストレージ サービスへの接続オプションが用意されているので、それらストレージを利用することで、オートデスク ドライブを使わずに、デスクトップと AutoCAD Web や AutoCAD モバイル アプリとの連携が可能になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44b86a1200c-pi" style="display: inline;"><img alt="3rd_party_storage_options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a44b86a1200c image-full img-responsive" src="/assets/image_164376.jpg" title="3rd_party_storage_options" /></a></p>
<hr />
<p>AutoCAD 2020 と AutoCAD LT 2020 に関する関連ドキュメントは、次のリンクから、それぞれダウンロードすることが出来ます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0240a4711975200d img-responsive"><a href="https://adndevblog.typepad.com/files/autocad-2020-autocad-lt-2020-comparison-matrix-ja.pdf"><strong>AutoCAD 2020 と AutoCAD LT 2020 の機能比較</strong> チラシをダウンロード</a></span></p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0240a471197d200d img-responsive"><a href="https://adndevblog.typepad.com/files/autocad-2020-release-comparison-matrix-ja.pdf"><strong>AutoCAD 2020 旧リリースとの機能比較</strong> チラシをダウンロード</a></span></p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0240a4711987200d img-responsive"><a href="https://adndevblog.typepad.com/files/autocad-lt-2020-release-comparison-matrix-a4-ja.pdf"><strong>AutoCAD® LT 2020 旧リリースとの機能比較</strong> チラシをダウンロード</a></span></p>
<hr />
<p>By Toshiaki Isezaki</p>

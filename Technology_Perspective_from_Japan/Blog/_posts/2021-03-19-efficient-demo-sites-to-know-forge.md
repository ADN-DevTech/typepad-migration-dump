---
layout: "post"
title: "Forge を知る効果的なデモ サイト"
date: "2021-03-19 02:59:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/03/efficient-demo-sites-to-know-forge.html "
typepad_basename: "efficient-demo-sites-to-know-forge"
typepad_status: "Publish"
---

<p>登場からまもなく 5 年を迎える Autodesk Forge、すでにさまざまに利用されています。ここでは、代表的な 3 つの利用形態、<strong>Visual Insight</strong>、<strong>BIM 360 統合</strong>、<strong>Design Automation</strong>、と各形態のデモ サンプルの例をご紹介します。触れてみて、実際に Forge の効果を感じていただければと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e997ad63200b-pi" style="display: inline;"><img alt="What_customers_built" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e997ad63200b image-full img-responsive" src="/assets/image_778035.jpg" title="What_customers_built" /></a></p>
<hr />
<p><strong>Visual Insight</strong></p>
<p style="padding-left: 40px;">「視覚化により洞察を得る」といった利用です。CAD ソフトウェアが作成する 2D 図面や 3D モデルは、単なる幾何データ（図形）で構成されているわけではありません。一般にメタデータと呼ばれる「属性」、あるいは「プロパティ」といった文字情報と複雑な関連性を保持している「データベース」と考えることが出来ます。</p>
<p style="padding-left: 40px;">そのようなメタデータは、CAD でデザイン ファイルを開いただけでは目にすることが出来るわけではありません。一定の操作をすることで、特定のユーザ インタフェースに表示させる必要があります。</p>
<p style="padding-left: 40px;">多くの方に Forge が評価されているのは、CAD ソフトウェアのライセンスを購入することなく、幾何データとメタデータを Web ブラウザで表示できる点です。この時、Forge API で提供していない機能を他の Web オープンソースとマッシュアップすることで、深い操作なしでメタデータを視覚化して幾何データと組み合わせて表現出来るようになります。パッと見ただけでは把握しにくい情報を得られるわけです。</p>
<p style="padding-left: 40px;">もっとも分かりやすいのが、建設業で Forge が広まるきっかけとなった <a href="https://autodesk-forge.github.io/viewer-javascript-visual.reports/" rel="noopener" target="_blank"><strong>BIM ダッシュボード</strong></a> のサンプル例です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://autodesk-forge.github.io/viewer-javascript-visual.reports/" rel="noopener" style="display: inline;" target="_blank"><img alt="Bim_dashboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec4fc0c200c image-full img-responsive" src="/assets/image_816122.jpg" title="Bim_dashboard" /></a></p>
<p style="padding-left: 40px;">画面左手のビューで図形を選択することで、2D 図面上の図形と 3D モデルの関連性を表現したり、画面右手には BIM モデルが保持している要素を集計、グラフ化することで、数量など、さまざまな洞察を得ることが出来ます。まさに、ビル竣工後の FM（設備管理）運用にうってつけです。</p>
<p style="padding-left: 40px;">こういったメタデータの視覚化は、機械系 CADソフトウェアのデザイン データや AutoCAD の 2D 図面データなどからも実現出来ます。図面内のブロックを集計して部品表に代わる BOM データを抽出したり、ブロック属性にある部品単価から見積もり書のもとになる情報を得たり、といった具合です。</p>
<p style="padding-left: 40px;">一方、最近ではデジタル ツイン、スマート ビルディングなど、センサーデータを視覚化する必要もあるかと思います。オートデスクは、しばらく IoT 関連のビジネスに直接タッチしていませんでしたが、Forge Viewer と WebSocket、Three.js 等のマッシュアップでデジタルツインを実現した <a href="https://forge-rcdb.autodesk.io/configurator?id=58adee163e6f342cf1e92dae" rel="noopener" target="_blank"><strong>サンプル例</strong></a> も古くから用意されています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://forge-rcdb.autodesk.io/configurator?id=58adee163e6f342cf1e92dae" rel="noopener" style="display: inline;" target="_blank"><img alt="Degital_twin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801ce5f7200d image-full img-responsive" src="/assets/image_802031.jpg" title="Degital_twin" /></a></p>
<p style="padding-left: 40px;">また、正式なリリースはしていませんが、今後、<a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-viewerdata-visualization-extension.html" rel="noopener" target="_blank"><strong>Data Visualization エクステンション</strong></a>でセンサーデータを効果的に視覚化することもアナウンスしています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://hyperion.autodesk.io/" rel="noopener" style="display: inline;" target="_blank"><img alt="Hyperion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801d9288200d image-full img-responsive" src="/assets/image_963271.jpg" title="Hyperion" /></a></p>
<hr />
<p><strong>BIM 360 統合</strong></p>
<p style="padding-left: 40px;">先にご紹介した BIM ダッシュボードやデジタルツインの例は、Forge アプリを開発する開発者用の領域にデータを保存して、Forge Viewer 用に変換、ストリーミング配信したものを使っています。つまり、Forge アプリにファイルのアップロードと変換機能を実装しない限り、データの可視化は開発者レベルの操作が必要になってしまいます。</p>
<p style="padding-left: 40px;">Forge は、HTML5 など、標準化された Web テクノロジをベースに作られているので、同様に Web テクノロジ ベースのさまざまな SaaS アプリやクラウド ストレージと繋いで利用することが出来ます。</p>
<p style="padding-left: 40px;">ご存じのとおり、オートデスク製の SaaS であり、かつ、ストレージ サービスに BIM 360 があります。Forge を使えば、BIM 360 の共通ドキュメント領域になっている BIM 360 Docs と統合することも可能です。これによって、BIM 360 Docs ユーザに Visual Insight なソリューションを提供することも容易なのです。もちろん、A360 や Fusion Team とのデータ統合が可能な点は言うまでもありません。</p>
<p style="padding-left: 40px;">BIM 360 統合をデモするサンプル例が、<strong><a href="https://bim360reports.autodesk.io/" rel="noopener" target="_blank">BIM 360 統合 BIM ダッシュボード</a>&#0160;</strong>です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://bim360reports.autodesk.io/" style="display: inline;"><img alt="Bim_360_integration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801ce72e200d image-full img-responsive" src="/assets/image_318868.jpg" title="Bim_360_integration" /></a></p>
<p style="padding-left: 40px;">この方法では、ご自身にアカウント（Autodesk ID）でアプリにサインインすることで、BIM 360 Docs に保存された（ご自身のアクセス件のある）データを使って、BIM 360 にはない Visual Insight を実装して利用出来ることがわかります。</p>
<p style="padding-left: 40px;">Visual Insight のほかにも、皆さんが欲する機能を付け加えることが出来ます。例えば、BIM 360 Docs 内の Revit プロジェクトから情報を抽出して Excel ブックのエクスポートする <a href="https://bim360xls.autodesk.io/" rel="noopener" target="_blank"><strong>https://bim360xls.autodesk.io/</strong></a>、BIM 360 Docs 上のフォルダやファイルの作成や削除、移動、変更を監視して通知を得る <a href="https://bim360notifier.autodesk.io/" rel="noopener" target="_blank"><strong>BIM 360 Notifier（https://bim360notifier.autodesk.io/）</strong></a>、BIM 360 の Issue を BIM 360 外部から作成、取得、管理する <a href="https://bim360issues.herokuapp.com/" rel="noopener" target="_blank"><strong>https://bim360issues.herokuapp.com/</strong></a> などもサンプル例として確認していただくことが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://bim360notifier.autodesk.io/" rel="noopener" style="display: inline;" target="_blank"><img alt="Bim_360_notifier" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e997b1f0200b image-full img-responsive" src="/assets/image_637739.jpg" title="Bim_360_notifier" /></a></p>
<p style="padding-left: 40px;">また、お使いの ERP システムや CRM システムとったクラウド サービスとの SaaS 統合も可能なはずです。</p>
<hr />
<p><strong>Design Automation</strong></p>
<p style="padding-left: 40px;">Forge には、クラウド上にオートデスクが用意した CAD コアエンジン（AutoCAD、Inventor、Revit、3ds Max）に、それぞれのエンジン用に作成したアドインをロードさせて実行することで、デザイン ファイルを生成したり、情報を抽出出来る Design Automation API があります。</p>
<p style="padding-left: 40px;">Visual Insight や BIM 360 統合（SaaS 統合）とは異なり、直接、CAD のデザイン ファイルを作成したり、解析したりすることが出来ます。</p>
<p style="padding-left: 40px;">通常、Design Automation API は、Web ページ上に入力されたパラメータに沿ってデザイン ファイル作成、編集して、ダウンロードしてデスクトップの CAD ソフトウェアで更に変更を加えるなどの運用がされています。こういった形態のアプリを <strong>コンフィギュレーター</strong> と呼ぶことがあります。言わば、設計の自動化を実現するわけです。</p>
<p style="padding-left: 40px;">Web ブラウザがインタフェースになるため、外出先、あるいは、CAD ライセンスを持たないエンドユーザ自身の好みの値やタイプを指定して製品を注文、背後で自動的に作成された 2D 図面や 3D モデルを使って製造工程に使用する、といったことが出来るようになります。</p>
<p style="padding-left: 40px;">Design Automation API for Inventor を使ったサンプル例は、<a href="https://inventor-config-demo.autodesk.io/" rel="noopener" target="_blank"><strong>https://inventor-config-demo.autodesk.io/</strong></a> で触れていただくことが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://inventor-config-demo.autodesk.io/" rel="noopener" style="display: inline;" target="_blank"><img alt="Da4i" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec4ff6b200c image-full img-responsive" src="/assets/image_737655.jpg" title="Da4i" /></a></p>
<p style="padding-left: 40px;">画面の右上でモデル タイプを選択して、左手のパネルでパラメータを変更後、[Update] ボタンをクリックすることで、クラウド上の Inventor コアエンジンにアドインがロードされて、パラメータに沿ったアセンブリが用意され、Forge Viewer に表示されます。画面上部のタブ リンクをクリックして BOM データを抽出したり、2D 図面を生成、各生成ファイルをダウンロードしたりすることも出来ます。</p>
<p style="padding-left: 40px;">Design Automation API for AutoCAD を使えば、パラメータに沿った DWG 図面を生成したり、DWG から PDF をエクスポートするようなことも出来ます。同様に、Design Automation API for Revit でプロジェクトを自動セットアップしたり、プロジェクト内にファミリ インスタンスを配置したりする、といった自動化を実現することも出来ます。 また、上記例のように Forge Viewer を組み込んだり、生成したデザイン ファイルを BIM 360 Docs に自動保存するようなことも可能です。Design Automation API for Revit では、モデルのメッシュの数を低減させてファイル サイズを小さくしたり、レンダリング画像を生成するなど出来ます。</p>
<hr />
<p>いかがでしょうか？</p>
<p>Forge は単一の開発ソリューションではありません。アイデアがあれば、Forge 外の各種 API ともマッシュアップして、いままで実現できなかったデジタル トランスフォーメーションに貢献します。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
<p>&#0160;</p>

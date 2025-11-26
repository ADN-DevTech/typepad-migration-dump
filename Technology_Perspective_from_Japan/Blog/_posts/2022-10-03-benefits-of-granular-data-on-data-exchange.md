---
layout: "post"
title: "データ交換に見る粒状データの効果"
date: "2022-10-03 00:58:45"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/10/benefits-of-granular-data-on-data-exchange.html "
typepad_basename: "benefits-of-granular-data-on-data-exchange"
typepad_status: "Publish"
---

<p>Application Programming Interface（API）の主要な役割の&#0160; 1 つにタスクの自動化があります。</p>
<p>対象となるタスクの多くは手動操作で実施している単純作業なので、生産性の向上が課題になり、さまざまな条件や規則を与えて自動化を目指すようになっていきます。</p>
<p>API を使った自動化では、真っ先にプログラミングを考えてしまいます。ただ、すべての人にプログラミングの知識を求めるのが現実的ではありません。</p>
<p>そこで、最近では、ローコード（Low Code）、ノーコード（No Code）ソリューションといった言葉とともに、プログラミング作業を極力排除した方法も散見されるようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4e182f200b-pi" style="display: inline;"><img alt="Automation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4e182f200b image-full img-responsive" src="/assets/image_474868.jpg" title="Automation" /></a></p>
<p>Revit カスタマイズでビジュアル プログラミングを実現する Dynamo は、ローコード ソリューションと言えるでしょう。Dynamo&#0160; では、C++ や C#、JavaScript などの「<a href="https://ja.wikipedia.org/wiki/%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0%E8%A8%80%E8%AA%9E" rel="noopener" target="_blank">プログラミング言語</a>」使わずにタスクを自動化することが出来ます。</p>
<p>また、一般的なノーコード ソリューションでは、定型化されたテンプレートを使用して、ドラッグ＆ドロップなどの簡単な操作でワークフローを自動化します。例えば、<a href="https://powerautomate.microsoft.com/ja-jp/" rel="noopener" target="_blank">Microsoft Power Automate</a> では、沢山用意されてるテンプレートから、タスクにあったものを選んで条件を設定するだけです。</p>
<p>今回は、最終的に「粒状データ」の効果を感じていただけるよう、まず、Power Automate を使った自動化とはどのようなものか、といったところからご紹介していきたいと思います。</p>
<p>現在、オートデスク社内では、クラウドストレージとして&#0160;<a href="https://www.microsoft.com/ja-jp/microsoft-365/onedrive/onedrive-for-business" rel="noopener" target="_blank">OneDrive for Business</a> を利用しています。社内でファイルを共有したり、ドキュメント管理する際にとても重宝しています。</p>
<p>ただ、複数の関係者間で共有フォルダを利用していると、フォルダ内容について、どのようなファイルがアップロードされているのか「共通認識」を持したい、といった場面にしばしば遭遇します。そこで、誰かがファイルをアップロードしたら、関係者に通知メールを送りたいと考えます。ところが、同自動化していいかわかない、そんな状況を考えてみます。</p>
<p>例えば、特定の共有フォルダ <em><strong>test</strong></em> にファイルがアップロードされたらメール通知が欲しい、と仮定してテンプレートを探します。共有フォルダ <em><strong>test</strong></em> を作成したら、ワークフローの自動化を定義していきます。「自動化」&gt;&gt;「フローの作成」を選択して目的にあったテンプレートを見つけていきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed6a95c200d-pi" style="display: inline;"><img alt="Pa" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed6a95c200d image-full img-responsive" src="/assets/image_544184.jpg" title="Pa" /></a></p>
<p>「その他のテンプレートを表示」を選択すると、さまざまなカテゴリ別にテンプレートが表示されます。検索ボックスでキーワード検索を利用することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4dfad2200b-pi" style="display: inline;"><img alt="Templates" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4dfad2200b image-full img-responsive" src="/assets/image_609741.jpg" title="Templates" /></a></p>
<p>「<a href="https://powerautomate.microsoft.com/ja-JP/templates/details/6759a6805f8011e6a8715d7d1f2fad64/onedrive-for-business-%E3%81%AB%E6%96%B0%E3%81%97%E3%81%84%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%81%8C%E8%BF%BD%E5%8A%A0%E3%81%95%E3%82%8C%E3%81%9F%E3%81%A8%E3%81%8D%E3%81%AB%E3%83%A1%E3%83%BC%E3%83%AB%E3%82%92%E9%80%81%E4%BF%A1%E3%81%99%E3%82%8B/" rel="noopener" target="_blank">OneDrive for Business に新しいファイルが追加されたときにメールを送信する </a>」テンプレートが目的にあっているようなので、テンプレートを選択して条件を設定します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308e0a4be200c-pi" style="display: inline;"><img alt="Rule" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308e0a4be200c image-full img-responsive" src="/assets/image_258257.jpg" title="Rule" /></a></p>
<p>誰にメール通知するのか、どのような通知内容にするのか、などの条件を保存すると、共有フォルダ <em><strong>test</strong></em> にファイルがアップロードされるたびに、通知メールを受け取れるようになります。プログラミング言語を使って自動化プログラムを作成するようなことはしていません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308e0a7e2200c-pi" style="display: inline;"><img alt="Notification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308e0a7e2200c image-full img-responsive" src="/assets/image_174674.jpg" title="Notification" /></a></p>
<p>Power Automate には、オートデスクが用意した <a href="https://make.powerautomate.com/environments/Default-67bff79e-7f91-4433-a8e5-c9252d2ddc1d/galleries/public/templates/979f9bce2788416eb76572866cc68138/extract-data-on-exchange-modification" rel="noopener" target="_blank">Extract data on exchange modification</a> テンプレートも用意されています。</p>
<p>このテンプレートを使うと、<strong>なぜインダストリー クラウドなのか？</strong> でもご紹介した粒状データを利用して、<a href="https://construction.autodesk.co.jp/" rel="noopener" target="_blank">Autodesk Construction Cloud</a>（ACC）にアップロードされた Revit プロジェクト ファイル（.rvt）から、必要な情報のみを抽出、指定した Excel テーブル（表）に書き出すことが出来ます。</p>
<p>このテンプレートには、Revit ファイルから粒状データを抽出する <a href="https://apps.autodesk.com/BIM360/ja/Detail/Index?id=8024981217381891083&amp;ln=en&amp;os=Web" rel="noopener" target="_blank">Autodesk® Data Exchange Connector for Power Automate</a> が利用されています。言うまでもなく、この Connector（コネクタ）は&#0160; Autodesk Platform Services（旧名 Autodesk Forge）の <a href="https://forge.autodesk.com/en/docs/fdx/v1/reference/quick_reference/">Data Exchange API [Beta]</a> を使って作られているものです。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d4df9f0200b-pi" style="display: inline;"><img alt="Fdx" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4df9f0200b image-full img-responsive" src="/assets/image_602534.jpg" title="Fdx" /></a></p>
<p>Power Automate では、テンプレートを使わずに、各種コネクタを活用して、独自のワークフローを作成することも出来ます。</p>
<p>先の Data Exchange Connector（データ交換コネクタ）を利用したカスタム フローの作成方法も、<a href="https://help.autodesk.com/view/BUILD/ENU/?guid=Excel_Automate_Data_Exchanges" rel="noopener" target="_blank">BUILD Help | Populate Excel with Data Exchange Properties | Autodesk</a>&#0160;で紹介されています。また、Microsoft 側のドキュメント（<a href="https://learn.microsoft.com/ja-jp/connectors/autodeskforgedataexc/" rel="noopener" target="_blank">Autodesk Forge Data Exchange - Connectors | Microsoft Learn</a>）にも詳細な記載があります。</p>
<p>Data Exchange API がベータ版扱いであるため、まだ「プレビュー」の位置づけになりますが、ファイル単位ではない粒状データの利点を把握していただけるはずです。</p>
<p>Power Automate のカスタム フロー作成手順が長くなってしまうので割愛しますが、<a href="https://help.autodesk.com/view/BUILD/ENU/?guid=Excel_Automate_Data_Exchanges" rel="noopener" target="_blank">BUILD Help | Populate Excel with Data Exchange Properties | Autodesk</a> の内容に沿って Data Exchange（デーや交換）を作成ておけば、ACC へのアップロードの度に（新しいバージョン毎に）、 作成した Data Exchange（データ交換）、すなわち、粒状データから、指定したデータのみを OneDrive 上のフォルダにある Excel テーブルへ自動的に書き出すことが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308e14868200c-pi" style="display: inline;"><img alt="Extract_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308e14868200c image-full img-responsive" src="/assets/image_233614.jpg" title="Extract_data" /></a></p>
<p>現在、Data Exchange（データ交換）の作成には、ACC のアプリ ギャラリーから「Power Automate」アプリを<a href="https://help.autodesk.com/view/BUILD/JPN/?guid=Power_Automate_Data_Exchanges" rel="noopener" target="_blank">有効化</a>して、ビューア上から「データ交換を作成する」を実行が必要になります。なお、アプリ ギャラリーの操作には Account Admin 権限が必要です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed74f2c200d-pi" style="display: inline;"><img alt="Enable_data_exchange" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed74f2c200d image-full img-responsive" src="/assets/image_163558.jpg" title="Enable_data_exchange" /></a></p>
<p>OneDrive 上のフォルダにある Excel テーブルに書き出す実際のフローは、次のようになります。（<a href="https://help.autodesk.com/view/BUILD/ENU/?guid=Excel_Automate_Data_Exchanges" rel="noopener" target="_blank">BUILD Help | Populate Excel with Data Exchange Properties | Autodesk</a> に沿って作成）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed6e464200d-pi" style="display: inline;"><img alt="Flow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed6e464200d image-full img-responsive" src="/assets/image_192583.jpg" title="Flow" /></a></p>
<p>ACC 上に新しいバージョンがアップロードされると、ビューやシートの抽出処理が始まります。Data Exchange（データ交換）を作成したビュー内の要素に変更があると、作成したデータ交換（粒状データ）が自動的に更新されて、粒状データの更新をトリガに Excel テーブルに指定したデータが書き出されていきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed6fb69200d-pi" style="display: inline;"><img alt="Data_exchange" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed6fb69200d image-full img-responsive" src="/assets/image_843385.jpg" title="Data_exchange" /></a></p>
<p>Revit プロジェクトは幾何情報を持つデータベースと言えるので、その内容は膨大なものです。言うまでもなく、特定の情報を人的手動操作で抽出するのは大変な労力が必要になります。粒状データを利用することで、ファイルをいちいち開かなくても、このような自動化を達成することが出来るようになります。今回、ご紹介した Power Automate は一例です。</p>
<p>Autodesk Platform Services（旧名 Autodesk Forge ）では、粒状データを使ったツール間のコミュニケーション用に Data Exchange API（現段階で外部の 3rd party 用途ではベータ版扱い）が用意されています。この API を使って特定の幾何情報をコラボレーションに利用する機能が、Revit 2023 ー ACC － Inventor 2023 の間で既に実現されています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/VNzmxkMze48" width="480"></iframe></p>
<p>Data Exchange API を使うと、特定のデータを扱えるようになります。Revit と <a href="https://www.rhino3d.com/jp/" rel="noopener" target="_blank">McNeel Rhino<sup>®</sup></a> 間のコネクタ（ベータ版）も <a href="https://blogs.autodesk.com/revit/2022/09/22/data-exchange-connector-for-mcneel-rhino-now-in-public-beta/" rel="noopener" target="_blank">Data Exchange Connector for McNeel Rhino® Now in Public Beta</a>でアナウンスされています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308e23202200c-pi" style="display: inline;"><img alt="Rhino" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308e23202200c image-full img-responsive" src="/assets/image_441549.jpg" title="Rhino" /></a></p>
<p>データ交換という視点では、上記、Revit ⇔ PoweAutomate、Revit ⇔ Inventor、Revit ⇔ Rhino のいずれのケースも、従来からあるデータのエクスポート（書き出し）/インポート（読み込み）機能と同じように見えてしまうかもしれません。ここでご注目いただきたいのは、「どのファイルを書き出し/読み込むか」ではなく、「どのデータを書き出し/読み込むか」になっている点です。</p>
<p>データに着目した共通プラットフォーム、Autodesk Platform Services の採用が進んでいくと、プログラミング言語を使って コネクタを開発する開発者だけではなく、プログラミング知識のない利用者のも利点が生まれるわけです。必要なデータのみを扱うカスタム ワークフローの作成したり、AI に機械学習させるデータ リソースとして活用したりするなど、従来と違った自動化につながっていくことも想像に難くありません。明らかに「データ」の活用の幅が広がります。</p>
<p>今後、さまざまなツールやソフトウェア、クラウド サービス用に、粒状データを利用したコネクタが増えていくよう期待したいところです。</p>
<p>By Toshiaki Isezaki</p>

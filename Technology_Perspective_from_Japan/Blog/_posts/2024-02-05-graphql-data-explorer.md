---
layout: "post"
title: "GraphQL Data Explorer"
date: "2024-02-05 00:19:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/02/graphql-data-explorer.html "
typepad_basename: "graphql-data-explorer"
typepad_status: "Publish"
---

<p>オートデスクはファイル形式に依存しない <a href="https://www.autodesk.co.jp/company/autodesk-platform" rel="noopener" target="_blank">オートデスク プラットフォーム -&#0160;Design &amp; Make（デザインと創造）プラットフォーム </a>の実装を進めています。オートデスク プラットフォームは、建設業向けの Autodesk Forma、製造業向けの Autodesk Fusion、メディア エンターテインメント向けの Autodesk Flow で構成されるので、「インダストリークラウド」とも呼ばれています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a9c1da200b-pi" style="display: inline;"><img alt="Indystry_cloud" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a9c1da200b image-full img-responsive" src="/assets/image_379388.jpg" title="Indystry_cloud" /></a></p>
<p>デザイン ファイルから抽出した情報を<a href="https://adndevblog.typepad.com/technology_perspective/2022/12/graph-database.html" rel="noopener" target="_blank">グラフデータベース</a>で管理するインダストリークラウドでは、個々の情報を「粒状データ」として扱います。API を使用する開発者目線では、Autodesk Platform Services の Autodesk Data Model API を使用して粒状データにアクセスすることになります。</p>
<p>インダストリークラウドのデータモデルは、Autodesk Platform Services の Autodesk Data Model 上に構築されていて、業種別に、建設業向けの AEC Data Model、製造業向けの Manufacturing Data Model（MFG Data Model）、メディア・エンターテイメント業向けの M&amp;E Data Model で構成されていて、それぞれインダストリークラウドの Autodesk Forma、Autodesk Fusion、Autodesk Flow に対応しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a9c271200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a9c271200b image-full img-responsive" src="/assets/image_857949.jpg" title="Aps" /></a></p>
<p>各データ モデルへのアクセスで利用するのが、従来、Autodesk Platform Services（旧 Forge）で利用されていた RESTful API（REST API）に代わる GraphQL です。粒状データを使ってデータ交換をおこなう Data Exchange も含め、各データモデルの GraphQL をテストする目的で Data Explorer と名付けられたツールが用意されています。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5fa51200c-pi" style="display: inline;"><img alt="Grahql" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5fa51200c image-full img-responsive" src="/assets/image_469617.jpg" title="Grahql" /></a></p>
<p>Data Explorer については、以前、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/graphql.html" rel="noopener" target="_blank">GraphQL</a> の記事で触れたことがありますが、大幅に機能拡張されているのと、新たに AEC Data Model 用の Data Explorer も追加されているので、改めてご紹介しておきたいと思います。（M&amp;E Data Model は実装中のため Data Explorer の公開がまだありません。）</p>
<ul>
<li><a href="https://aecdatamodel-explorer.autodesk.io/" rel="noopener" target="_blank">AEC Data Model Explorer</a></li>
<li><a href="https://mfgdatamodel-explorer.autodesk.io/" rel="noopener" target="_blank">MFG Data Explorer (v1)</a></li>
<li><a href="https://aps-dx-explorer.autodesk.io/" rel="noopener" target="_blank">Data Exchange Explorer</a></li>
</ul>
<p>上記、Data Explorer の実装は Data Model 毎に少しづつ異なります。ただし、ベースに使用しているのはオープンソースの GraphiQL で共通です。GraphiQL のコードは <a href="https://github.com/graphql/graphiql" rel="noopener" target="_blank">GraphiQL &amp; the GraphQL LSP Reference Ecosystem for building browser &amp; IDE tools</a>リポジトリ で公開されています。</p>
<p><strong>Data Explorer のユーザーインタフェース</strong></p>
<p>MFG Model Explorer を例に基本的なユーザーインターフェースと基本的な使用方法をご紹介します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a63772200c-pi" style="display: inline;"><img alt="Mfg_graphiql_ui" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a63772200c image-full img-responsive" src="/assets/image_5821.jpg" title="Mfg_graphiql_ui" /></a></p>
<p style="padding-left: 40px;">① プリセットされた GraphQL クエリー<br />② 実行する GraphQL 構文<br />③ 実行する GraphQL 構文内で参照される変数（JSON 形式）とリクエストヘッダー<br />④ GraphQL の実行（送信）<br />⑤ クエリーやミューテーション内容を確認するドキュメントリンクを表示<br />⑥ カラーテーマの変更とローカルキャッシュのクリアを指定する設定を表示<br />⑦ サインイン/サインアウト<br />⑧ <a href="https://mfgdatamodel-explorer.autodesk.io/voyager" rel="noopener" target="_blank">GraphQL Voyager</a> – スキーマ ヘルプを表示（MFG Data Explorer のみ）&#0160;<br />⑨ リクエストに対するレスポンス（JSON 形式）の表示領域</p>
<p><strong>基本的な使用方法</strong></p>
<p>現時点では、Manufacturing Data Model のデータは Fusion Team に、AEC Data Model&#0160; のデータは Autodesk Construction Cloud に、Data Exchange のデータは Autodesk Construction Cloud に作成した Exchange Item にアクセスすることになります。このため、APS アプリである Data Explorer が同データにアクセスするには、ユーザーによる 3-legged 認証・認可を受ける必要があります。まずは ⑦ でデータにアクセスするユーザー アカウントでサインインする必要があります。ここいうユーザー アカウントとは、Fusion 360 で利用している Fusion Team にアクセス可能な Autodesk ID（オートデスク アカウント）を指します。</p>
<p>サインインが完了してアプリがアクセスすることを認可したら、Fusion 360 で日頃利用している Hub を確認しておきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa1563200b-pi" style="display: inline;"><img alt="Hub_on_fusion360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa1563200b image-full img-responsive" src="/assets/image_936662.jpg" title="Hub_on_fusion360" /></a></p>
<p>MFG Model Explorer で ① のタブの中から「GetHubs」タブをアクティブにして、④ の実行ボタンをクリックして ② にプリセットされている GraphQL クエリーを実行してみましょう。⑨ に サインインしたアカウントがアクセス可能な Hub の一覧が表示されるので、Fusion 360 上で確認した Hub 名を持つ Hub ID を Ctrl＋C キー操作でクリップボードにコピーするか、メモ帳に書き留めておきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a63746200c-pi" style="display: inline;"><img alt="Get_hub" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a63746200c image-full img-responsive" src="/assets/image_777255.jpg" title="Get_hub" /></a></p>
<p>次に、① のタブの中から「GetProjects」タブをアクティブにして、③ の「Variables」にプリセットされている変数 hubId の値&#0160; &quot;yourhubid&quot; の部分を、先にコピーしておきたクリップボードから Ctrl＋V キー操作で貼り付けるか、書き留めておいたメモ帳などから上書きして GraphQL クエリーを実行すると、⑨ にプロジェクト一覧を含む レスポンスが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a637db200c-pi" style="display: inline;"><img alt="Get_projects" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a637db200c image-full img-responsive" src="/assets/image_855503.jpg" title="Get_projects" /></a></p>
<p>実行した GraphQL 構文の内容を把握したいっ場合には、⑤ のアイコンをクリックすることで情報を得ることが出来ます。例えば、GetProjects タブで実行したクエリーは、「Query」&gt;&gt;「projects」の順でリンクをクリックして、簡易的な内容を表示することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa7c56200d-pi" style="display: inline;"><img alt="Projects" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa7c56200d image-full img-responsive" src="/assets/image_671954.jpg" title="Projects" /></a></p>
<p>もちろん、従来通り、APS ポータル上で該当する GraphQL リファレンスにアクセスして、詳細を得ることも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa1639200b-pi" style="display: inline;"><img alt="Aps_document" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa1639200b image-full img-responsive" src="/assets/image_833195.jpg" title="Aps_document" /></a></p>
<p><strong>GraphQL Voyager</strong></p>
<p>MFG Model Explorer の ⑦ で表示するオープンソースの GraphQL Voyager を使うと、Introspection（イントロスペクション）クエリーを実行させて MFG Model Explorer が使っている Manufacturing Data Model の GraphQL スキーマを検査して、グラフの相関関係をグラフィカルに表示させて内容を理解することが出来ます。</p>
<p>GraphQL Voyager を表示させたら、ページ左上の [CHANGE SCHEMA] をクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a638c1200c-pi" style="display: inline;"><img alt="Graphql_voyager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a638c1200c image-full img-responsive" src="/assets/image_392519.jpg" title="Graphql_voyager" /></a></p>
<p>[INTROSPECTION] タブをアクティにしたら、[COPY INTROSPECTION QEURY] をクリックして、イントロスペクション クエリーの内容をクリップボードにコピーします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa7d12200d-pi" style="display: inline;"><img alt="Introspection1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa7d12200d image-full img-responsive" src="/assets/image_459536.jpg" title="Introspection1" /></a></p>
<p>MFG Model Explorer に戻って、① の一番右にある &lt;intitled&gt; タブをアクティブにしたら、② の GraohQL 構文の領域に、Ctrl＋ V キー操作でイントロスペクション クエリーを貼り付けます。貼り付けると同時に &lt;intitled&gt; タブ名が IntrospectionQuery に変わります。④ で実行させると、検査結果のレスポンスが ⑨ の領域に表示されるので、Ctrl＋A キー操作 レスポンス全体を選択して、Ctrl＋C キー操作で内容をクリップボードにコピーします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa7ce4200d-pi" style="display: inline;"><img alt="Introspection_query" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa7ce4200d image-full img-responsive" src="/assets/image_463288.jpg" title="Introspection_query" /></a></p>
<p>GraphQL Voyager に戻り、[INTROSPECTION] タブの「Paste Introspection Here」欄に Ctrl＋ V キー操作で内容を貼り付けて [DISPLAY] をクリックすます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a63975200c-pi" style="display: inline;"><img alt="Introspection2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a63975200c img-responsive" src="/assets/image_73700.jpg" title="Introspection2" /></a></p>
<p>グラフの相関関係が表示されたら、マウスホイールで Manufacturing Data Model GraphQL が持つオブジェクトを拡大縮小させながらフィールドをクリックして、関係を追跡していくことが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a63921200c-pi" style="display: inline;"><img alt="Graphql_voyager2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a63921200c image-full img-responsive" src="/assets/image_856102.jpg" title="Graphql_voyager2" /></a></p>
<p>By Toshiaki Isezaki</p>

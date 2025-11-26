---
layout: "post"
title: "Revit 2018 の新機能 その3"
date: "2017-05-19 01:35:52"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/05/new-features-on-revit-2018-part3.html "
typepad_basename: "new-features-on-revit-2018-part3"
typepad_status: "Publish"
---

<p>Revit 2018 の新機能について解説させて頂きます。今回は、専門分野共通の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><strong>モデル グループと RVT リンクの集計表を作成</strong></p>
<p>モデルを適切にドキュメント化するため、集計表の作成時に、選択できるカテゴリに[モデル グループ]、[RVT リンク]が追加されました。</p>
<p><strong>モデル グループ、RVT リンク、集計表にパラメータを追加</strong></p>
<p>[プロジェクト パラメータ]ダイアログで、選択できるカテゴリに[モデル グループ]、[RVT リンク]、[集計表]が追加され、これらにカスタム パラメータを作成することができるようになりました。 一定の条件に該当する場合は、カスタム パラメータをグローバル パラメータに関連付けることもできます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/V5ynad04vuU?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>参照面のサブカテゴリ</strong></p>
<p>[サブカテゴリ]ツールを使用して、参照面にサブカテゴリを追加できるようになりました。サブカテゴリを使用すると、異なる線の色と線種を持つ参照面を作成できるため、Revit ファミリ、複雑なビュー、複雑なモデルで参照面がどのように使用されているかを簡単に見分けることができます。</p>
<p><br /><strong>文字注記内で記号を使用</strong></p>
<p>注釈機能が強化され、ワークフローを中断することなく文字注記に記号と特殊文字を追加できるようになりました。 コンテキスト メニューで共通して使用される記号のリストから選択するか、または記号の Unicode 10 進コードを含むキー シーケンスを入力します。あまり頻繁に使用しない記号については、Revit 内から Windows® 付属の文字コード表にアクセスし、記号をコピーして文字注記に貼り付けます。</p>
<p><strong>ラベルのリッチ テキスト</strong></p>
<p>ドキュメント内の図面の整合性を高めるため、文字注記に適したリッチ テキスト形式をラベル内で使用できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/XMmIt9XeCTc?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>ファミリ拘束の検証</strong></p>
<p>モデル内のファミリの信頼性を高めるため、Revit のファミリ エディタでファミリを開いたときに、拘束のステータスが自動的に確認されるようになりました。 潜在的な問題が Revit によって検出されると、詳細な情報を示すダイログが表示されます。</p>
<p>&#0160;</p>
<p><strong class="ph b"><span class="ph msgph prodname" id="GUID-C81929D7-02CB-4BF7-A637-9B98EC9EB38B__GUID-733CAB04-2D2C-4B15-AE41-8E331D0647C0">Collaboration for Revit</span></strong></p>
<ul>
<li>Collaboration for Revit が Revit に付属してインストールされるようになりました。 これにより、クラウドでワークシェアされていないモデルでも[パブリッシュ設定]などのツールを使用できるようになりました。</li>
<li>Revit でクラウド共有モデルを開いたときに、そのモデルの起動プロセスの進行状況を示す情報が表示されるようになりました。</li>
<li>Revit で中央モデルとの同期操作を実行しているときに、同期操作の進行状況を示す情報が表示されるようになりました。</li>
<li>Collaboration for Revit モデルのパブリッシュ操作の安定性が向上しています。</li>
</ul>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ef0c1e970b-pi" style="display: inline;"><img alt="Revit2018_part3_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ef0c1e970b img-responsive" src="/assets/image_204117.jpg" title="Revit2018_part3_01" /></a></p>
<p>&#0160;</p>
<p>また Revit 2017.1 Update で追加された以下の機能も、Revit 2018 リリースでは、すべてのユーザがこれらの機能を利用できます。</p>
<p><br /><strong>Dynamo プレーヤ</strong></p>
<p>すべての Revit ユーザーは、スクリプト作成の経験がなくても、Dynamo プレーヤを使用することにより、さまざまなプロセスを自動化し、スクリプト作成の利点を活用することができます。 このツールを使用すると、Revit で Dynamo スクリプトを簡単かつ効率的に実行することができます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/R8usi9c2BVg?feature=oembed" width="500"></iframe></p>
<p><br /><strong>高解像度モニタ対応</strong></p>
<p>Revit は高解像度技術に基づいて、4K モニタ、Surface Pro などの高解像度スクリーンを使用する建築設計者、エンジニア、設計者向けに、高品質で鮮明な画像を提供します。 この機能は現在適切に動作するようになっており、高い DPI 設定(&gt;=200%)を使用して、ユーザー インターフェース コンポーネントを鮮明に表示させることができます。</p>
<p>&#0160;</p>
<p>次回は、構造設計分野の新機能についてご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>

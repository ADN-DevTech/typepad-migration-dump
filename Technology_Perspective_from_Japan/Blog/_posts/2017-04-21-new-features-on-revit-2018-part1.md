---
layout: "post"
title: "Revit 2018 の新機能 その1"
date: "2017-04-21 01:48:50"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part1.html "
typepad_basename: "new-features-on-revit-2018-part1"
typepad_status: "Publish"
---

<p>今年も Revit の新バージョンとなる Revit 2018 がリリースされました。</p>
<p>今回から複数回にわたって、Revit 2017.1 及び 2017.2 Update リリースの内容を含めながら、Revit 2018 の新機能と更新内容、API の対応状況をご紹介していきます。<br />Part.1 と Part.2 では建築設計分野、Part.3 は専門分野共通のコア機能、Part.4 と Part.5 で構造設計分野とMEP 設計分野の機能を取り上げる予定です。<br /><br /></p>
<p><strong>パースビューモデリング</strong></p>
<p><a href="http://adndevblog.typepad.com/technology_perspective/2016/11/revit-2017-update-1.html">Revit 2017.1 Upadate リリース</a>でもご紹介いたしましたが、<strong>パースビュー上でもモデリング作業</strong>を行うことができるようになりました。</p>
<p>パース ビューで直接作業することにより、<strong>平行投影ビューに切り替えずに、建物要素を追加、修正、移動することができる</strong>ため、モデリング ワークフローの効率性が向上します。</p>
<p>パースビューでのモデリングでは、ほとんどのタイプの要素を追加および修正することができるようになります。ただし、3D ビューで通常は表示されない要素(通芯、レベル、参照面、部屋、エリア他)などの一部の要素は、サポートされていません。また注釈もサポートされませんが、仮寸法は使用できます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/0LVC54XLFlk?feature=oembed" width="500"></iframe></p>
<p>3D ビューで実行可能なコマンドのほとんどをパース ビューで実行できるようになり、これに伴い、<strong>アドインの外部コマンドと外部アプリケーションが、パース ビューでもデフォルトで利用できる</strong>ようになります。マクロと Dynamo も同様に利用できます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8eea5ef970b-pi" style="display: inline;"><img alt="Revit2018_part1_01" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8eea5ef970b img-responsive" src="/assets/image_322619.jpg" title="Revit2018_part1_01" /></a></p>
<p><br /><strong>複数階に連続する階段</strong></p>
<p>ベースとなる階段コンポーネントから、連続させたい建物レベルを選択することで、<strong>複数階に連続する階段</strong>を作成することができるようになりました。<strong>レベルの高さが変更されると、自動的に階段も調整</strong>されます。階段作成中に複数階の階段を作成することも、既存の階段から後で複数階の階段を生成することもできます。</p>
<p>また複数階の階段の<strong>それぞれの階段に個別にタグを追加</strong>することができます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/KTYCCZuq1lI?feature=oembed" width="500"></iframe></p>
<p><br /><strong>手摺の強化</strong></p>
<p>手すりの修正プロセスが改善され、手すりの[タイプ プロパティ]ダイアログから直接上部手すりと補助手すりのタイプ プロパティにアクセスできるようになりました。また、[手すりのタイプ プロパティ]の[プレビュー]ペインで変更内容を表示することができるようになりました。</p>
<p>スケッチされた<strong>手すりを地盤面にホストできる</strong>ようになりました。この機能強化により、[手すり]ツールを使用して<strong>フェンスやガード レール</strong>などの要素をモデリングできるようになりました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0991c1d7970d-pi" style="display: inline;"><img alt="Revit2018_part1_02" class="asset  asset-image at-xid-6a0167607c2431970b01bb0991c1d7970d img-responsive" src="/assets/image_998037.jpg" title="Revit2018_part1_02" /></a></p>
<p>階段にホストされている手すりで、改善されたアタッチメント機能を使用できるようになりました。設計の意図に合わせて、<strong>手すりの始点と終点をコントロール</strong>することができます。</p>
<p>複数階の階段にホストされている手すりは、複数の階段とグループ化され、グループとして編集できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/iEZsVdn-seg?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>サブ要素</strong></p>
<p>Revit の Element オブジェクト(要素)は、パラメトリックなパラメータや集計、タグなどの付随機能を組み込むため、膨大な要素を作成・追加すると、そのモデルのパフォーマンスを低下させてしまいます。</p>
<p>このスケーラビリティの問題への対策として、<strong>サブ要素</strong>が追加されました。サブ要素は、モデルに要素を追加する際に必要となる<strong>オーバーヘッド（事前準備・管理・後処理）を省略</strong>して最小限に抑えつつも、<strong>実際の要素と同じような振る舞い</strong>を可能にするための要素パーツです。このサブ要素は、Revit が提供する一部の機能で内部的に利用されるため、ユーザインターフェース上で明示的にサブ要素という名称では表示されません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8eeaacf970b-pi" style="display: inline;"><img alt="Revit2018_part1_03" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8eeaacf970b img-responsive" src="/assets/image_492906.jpg" title="Revit2018_part1_03" /></a></p>
<p>サブ要素は、メイン要素のジオメトリの一部となり、メイン要素からそれぞれ独立して個別に選択することができます。そして、<strong>メイン要素とは異なるカテゴリとタイプをサブ要素に設定</strong>することができます。</p>
<p>今回ご紹介した、<strong>複数階に連続する階段</strong>や、<strong>手すり</strong>でも、このサブ要素が適用されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0991d0e5970d-pi" style="display: inline;"><img alt="Revit2018_part1_04" class="asset  asset-image at-xid-6a0167607c2431970b01bb0991d0e5970d img-responsive" src="/assets/image_467458.jpg" title="Revit2018_part1_04" /></a></p>
<p>&#0160;</p>
<p>API では、Subelement クラスが新たに追加され、Element オブジェクト、または特定のサブ要素の参照を設定してサブ要素オブジェクトを作成することができます。</p>
<ul>
<li>Subelement.Create(Document aDoc, Reference reference)</li>
</ul>
<p>Element クラスと Document クラスには、Subelement オブジェクトを取得、削除するメソッドが追加されました。</p>
<ul>
<li>Element.GetSubelements()</li>
<li>Document.GetSubelement(Reference)</li>
<li>Document.GetSubelement(String uniqueId)</li>
</ul>
<p>要素から取得したサブ要素のコレクションは、カテゴリやパラメータなどの属性情報に基づいて特定することができます。そして、それらのサブ要素を通常の要素と同じように扱うことができます。</p>
<p>サブ要素がメインの親要素のジオメトリの一部である場合は、親要素から独立して選択することができます。</p>
<ul>
<li>ObjectType.Subelement</li>
</ul>
<p>&#0160;</p>
<p><strong>ハッチング パターンのバックグラウンド カラーを DWG/DXF に書き出し</strong></p>
<p>読み込み可能な DWG/DXF 書き出しファイルをサポートするために、ハッチング パターンのソリッド バックグラウンド カラーを指定できるようになりました。&#0160;</p>
<p>&#0160;</p>
<p><strong>マテリアルの色を DWG/DXF および DGN に書き出し</strong></p>
<p>ソース モデルにより忠実な色情報を生成するために、DWG/DXF および DGN ファイルに書き出す際、オブジェクト サーフェス パターンの割り当てられているマテリアルの色から RGB 値を指定できるようになりました。</p>
<p>&#0160;</p>
<p>次回は、Navisworks® のファイルを Revit モデルの下敷参照図として使用できるコーディネーション モデルや、 SAT ファイルや Rhinoceros® ファイルから 3D 形状を読み込む機能、グリッド座標系を使用して DWG ファイルをリンクする機能などについて解説致します。</p>
<p>&#0160;By Ryuji Ogasawara</p>

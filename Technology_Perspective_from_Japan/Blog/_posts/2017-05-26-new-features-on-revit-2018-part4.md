---
layout: "post"
title: "Revit 2018 の新機能 その4"
date: "2017-05-26 01:46:17"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/05/new-features-on-revit-2018-part4.html "
typepad_basename: "new-features-on-revit-2018-part4"
typepad_status: "Publish"
---

<p>Revit 2018 の新機能について解説させて頂きます。これまでの解説記事は下記のリンクをご参照ください。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part1.html">Revit 2018 の新機能 その1</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/new-features-on-revit-2018-part2.html">Revit 2018 の新機能 その2</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2017/05/new-features-on-revit-2018-part3.html">Revit 2018 の新機能 その3</a></li>
</ul>
<p>今回は、構造設計分野の新機能、更新内容、API の対応状況を解説いたします。</p>
<p><strong>フリー フォームのコンクリート オブジェクトに鉄筋を配置</strong><br />コンクリート要素内の複雑なジオメトリ(曲線状の橋脚、橋床など)に合わせて鉄筋を配置できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/xbcmZO78xOs?feature=oembed" width="500"></iframe><br /><br /></p>
<p><strong>さまざまな鉄筋配置の機能改善</strong><br />作業の生産性を高め、設計意図を効率的に詳細化するため、フリー フォーム オブジェクトをはじめとする曲線サーフェスに沿って、さまざまな鉄筋セットを配置できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/-jbxkCpWs1k?feature=oembed" width="500"></iframe><br /><br /></p>
<p><strong>読み込んだコンクリート要素内に鉄筋を配置</strong><br />SAT ファイルや InfraWorks から読み込んだコンクリート要素内に鉄筋を配置できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/m4edhAGwMsc?feature=oembed" width="500"></iframe></p>
<p>Revit 2018 では、鉄筋は、形状駆動とフリーフォームの 2つのレイアウトオプションをサポートするようになりました。Revit 2017 までの全ての鉄筋要素は、形状駆動タイプという位置づけになります。</p>
<p>これに伴い、API に新しいメソッドが追加されております。</p>
<ul>
<li>Rebar.GetShapeDrivenAccessor()&#0160;<br />形状駆動タイプの鉄筋に対応するロジック（メソッド、プロパティ）へのインターフェースを取得します。</li>
<li>Rebar.GetFreeFormAccessor() <br />フリーフォームの鉄筋に対応するロジック（メソッド、プロパティ）へのインターフェースを取得します。</li>
<li>Rebar.IsRebarFreeForm/IsRebarShapeDriven <br />鉄筋が形状駆動かフリーフォームか確認します。</li>
</ul>
<p>&#0160;</p>
<p><strong>鉄筋の拘束を 3D ビューでグラフィック表示</strong><br />グラフィカルな鉄筋拘束エディタを 3D ビューで使用できるようになりました。 高度な鉄筋の配置や正確な鉄筋の配置を行う場合は、キャンバス内ツールを使用します。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/dIrGpVaA554?feature=oembed" width="500"></iframe><br /><br /></p>
<p><strong>追加の鉄骨接合</strong><br />Steel Connections for Revit アドインには、鉄骨詳細図用の 100 を超える新しい接合が付属しています。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/BSJkctIiPdc?feature=oembed" width="500"></iframe><br /><br /></p>
<p><strong>カスタムのフレーム ファミリをサポートする鉄骨接合</strong><br />構造接合をより効率的に統合する目的で、Revit でカスタムのフレーム要素を解析し、その要素に対する構造断面ジオメトリ パラメータを生成できるようになりました。 これにより、組織内のフレーム要素で鉄骨接合を簡単に実装することができます。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/yli17n2jFqA?feature=oembed" width="500"></iframe><br /><br /></p>
<p><strong>接合内での鉄骨要素の優先度</strong><br />鉄骨接合内における要素の優先度(メイン要素と 2 次要素の優先順位)を指定できるようになりました。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/tC1jqBd88T4?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p>また API では、StructuralSection&#0160;とその関連クラスに新しいプロパティとパラメータが追加されました。</p>
<ul>
<li>StructuralSectionUtils.GetStructuralElementDefinitionData()<br />構造要素の断面と位置を定義します。</li>
<li>StructuralSection.StructuralSectionGeneralShape()<br />構造断面ジオメトリに関する形状を特定するための列挙値を取得します。</li>
<li>StructuralSection.AnalysisParams <br />断面に関連する構造解析のための汎用的なパラメータへのアクセサーを取得します。</li>
<li>StructuralSection.GetBoundarySize() <br />断面のバウンダリのサイズを取得します。</li>
<li>StructuralSection.General*&#0160; <br />様々な構造断面のクラスが追加されました。</li>
</ul>
<p>&#0160;</p>
<p>次回は、MEP 設計分野の新機能についてご紹介いたします。</p>
<p>By Ryuji Ogasawara</p>

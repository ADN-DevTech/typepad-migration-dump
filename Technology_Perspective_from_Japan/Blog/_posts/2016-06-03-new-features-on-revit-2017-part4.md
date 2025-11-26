---
layout: "post"
title: "Revit 2017 の新機能 ～ その4"
date: "2016-06-03 02:12:43"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/06/new-features-on-revit-2017-part4.html "
typepad_basename: "new-features-on-revit-2017-part4"
typepad_status: "Publish"
---

<div>前回は、<a href="http://adndevblog.typepad.com/technology_perspective/2016/05/new-features-on-revit-2017-part3.html">Revit 2017 の構造エンジニアリング分野の新機能と更新内容、API の対応状況</a>ついて解説致しました。</div>
<div>&#0160;</div>
<div>梁、柱、ブレースなど複数要素の接合方法を指定できる「構造接合」や、詳細な鉄筋モデリングを実現する「鉄筋継手」、「曲げメッシュ筋のスケッチ」、コンクリート要素形状の傾斜面に沿って「段階的に形状変化する鉄筋セット」など、ぜひご活用ください。</div>
<div>&#0160;</div>
<div>今回は、MEP&#0160;エンジニアリング分野の新機能についてご紹介していきます。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>製造用パーツのモデリングを改善</strong></span></div>
<div>新しい「回転」ツールと「パーツを挿入」コマンドにより、製造用部品モデルの作成をより正確にすばやく行うことができるようになりました。</div>
<div>&#0160;</div>
<div style="padding-left: 30px;"><strong>回転ツール</strong></div>
<div style="padding-left: 30px;">改善されたキャンバス内回転ツールでは、軸を中心として任意の角度にパーツを簡単に回転させることができます。接続されている端点を中心として接続されているパーツを回転させる際に、詳細に制御できるため、生産性も向上します。</div>
<div style="padding-left: 30px;">&#0160;</div>
<div style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1f1d063970c-pi" style="display: inline;"><img alt="Rotation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1f1d063970c image-full img-responsive" src="/assets/image_494436.jpg" title="Rotation" /></a>&#0160;</div>
<div style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/u2oDmJecUoM?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;<br />
<div style="padding-left: 30px;"><strong>パーツを挿入</strong></div>
<div style="padding-left: 30px;">新しい[パーツを挿入]コマンドにより、バルブ、ダンパー、ティーなどのインライン パーツを、直線状の既存の経路内（ダクトや配管セグメント）に簡単に配置することができます。</div>
</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>MEP 設計モデルを製造用部品モデルに変換</strong></span></div>
<div>MEP 設計モデルを製造用部品モデルに変換することができるようになりました。</div>
<div>新しい[設計から製造]ツールを使用して、コーディネーション モデルと施工用のモデルをすばやく準備し、詳細な設計レベルのモデル要素を、施工レベルの詳細要素に変換することができます。</div>
<div>&#0160;</div>
<div>[設計から製造]ツールでは、選択した設計レベルのダクト要素とパイプ要素に対して、関連づけることができる部品種別の一覧が表示されます。変換された要素は、製造用部品種別で設定されている規則に従って生成されます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb090b6cc4970d-pi" style="display: inline;"><img alt="Conversion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb090b6cc4970d image-full img-responsive" src="/assets/image_243239.jpg" title="Conversion" /></a></div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/JzqJFJL_Zas?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>製作図の作成</strong></span></div>
<div>Revit の製造用パーツをドキュメント化する新しい機能により、モデル レイアウトのドキュメントを効率的に作成することができます。</div>
<div>&#0160;</div>
<div>製造用パーツに断熱材とライニングのサブカテゴリを個別に表示できるようになりました。</div>
<div>また製造用パーツで MEP の隠線を優先して表示することができます。</div>
<div>製造用データベースで部品種別に割り当てられている立上り立下り記号は、Revit の立上り立下り記号に自動的にマップされます。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/RrOgxU2cyjo?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>製造用ハンガー部品の改善</strong></span></div>
<div>支持材の詳細計画中に柔軟性の高い吊材作成機能を使用することで、モデルの施工性を確認することができます。</div>
<div>吊材同士を支持する多層構造のハンガー部品をモデリングできます。構造物の位置に合わせて受け材のロッド位置を調整したり、他の部品種別の使用を避けることができます。片持ち梁要素のサポートにより、吊材の受け材を拡張して追加の部品種別をサポートすることができます。&#0160;</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/M590mWaLM38?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>製造用部品レイアウトの改善</strong></span></div>
<div>Revit 製造用部品モデル内のギャップを埋める新しい[トリム/延長]、[クイック接続]、[ルートと塗り潰し]ツールが追加されました。</div>
<div>&#0160;</div>
<div>2 つの直線間のギャップ(ダクトの枝管を本管に接続する場合や配管をヘッダに接続する場合など)を埋めるには、[トリム/延長]を、継手と別の直線間において追加の継手が不要な場所にあるギャップを埋めるには[クイック接続]を使用することができます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb090b6f5b970d-pi" style="display: inline;"><img alt="Quick" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb090b6f5b970d img-responsive" src="/assets/image_871264.jpg" title="Quick" /></a></div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1f1d321970c-pi" style="display: inline;"><img alt="Quick2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1f1d321970c img-responsive" src="/assets/image_377446.jpg" title="Quick2" /></a></div>
<div>&#0160;</div>
<div>[ルートと塗り潰し]ツールは、製造用部品モデルのレイアウトを自動化し、より効率的かつ正確にモデリングできます。2 つの未接続のコネクタの間にパーツを追加すると、パーツを 1 つずつ配置するよりも高速かつ効率的にパーツを追加できます。部品種別に応じて 1 つ以上の経路/配置パターンがあります。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/qnu-4VkvC-U?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>
<div>次回は、Revit 2017 API について解説いたします。</div>
<div>&#0160;</div>
<div>By Ryuji Ogasawara</div>
</div>

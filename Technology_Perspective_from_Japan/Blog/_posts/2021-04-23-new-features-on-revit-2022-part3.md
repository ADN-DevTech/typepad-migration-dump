---
layout: "post"
title: "Revit 2022 の新機能 その3"
date: "2021-04-23 01:11:11"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part3.html "
typepad_basename: "new-features-on-revit-2022-part3"
typepad_status: "Publish"
---

<p>Revit 2022 の新機能と改良された機能をシリーズでご紹介しております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part1.html">Revit 2022 の新機能 その1</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part2.html">Revit 2022 の新機能 その2</a></li>
</ul>
<p>今回は、建築設計分野の新機能と機能向上の内容となります。</p>
<hr />
<p><strong>テーパ壁</strong></p>
<p>意匠壁または構造壁としてテーパ壁を作成できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4835200c-pi" style="display: inline;"><img alt="Revit2022-03-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4835200c image-full img-responsive" src="/assets/image_21058.jpg" title="Revit2022-03-04" /></a></p>
<p>テーパ壁は、垂直壁とほぼ同じ方法で、作成および修正します。</p>
<p>断面をテーパ付きに設定する前に、壁の構造の 1 つのレイヤを「可変」に設定する必要があります。<br />テーパ壁を有効するには、垂直壁と同様に配置して、断面のインスタンス パラメータで[テーパ付き]を選択します。<br />壁の面の角度は、壁タイプ プロパティで設定できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802536ab200d-pi" style="display: inline;"><img alt="Revit2022-03-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802536ab200d image-full img-responsive" src="/assets/image_20791.jpg" title="Revit2022-03-03" /></a></p>
<p>壁の面の角度が壁のタイプ プロパティで定義されている場合は、その角度が適用されます。</p>
<p>壁タイプ プロパティの角度は、個々の壁に対して壁インスタンス プロパティで上書きすることもできます。<br />[タイプ プロパティをオーバーライド]を選択し、[外角]と[内角]のパラメータを使用して、オーバーライドの角度を設定します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a00130200b-pi" style="display: inline;"><img alt="Revit2022-03-05" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a00130200b img-responsive" src="/assets/image_74171.jpg" title="Revit2022-03-05" /></a></p>
<hr />
<p><strong>非躯体壁レイヤを非表示</strong></p>
<p>平面図ビューで表示とグラフィックスの上書きを通じて、壁の非躯体レイヤを非表示にできるようなりました。</p>
<p>壁アセンブリの躯体境界間にある壁のレイヤは、表示されたままになります。<br />簡略の細部レベルに設定されたビューで、非躯体壁レイヤを非表示にすることはできません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a00299200b-pi" style="display: inline;"><img alt="Revit2022-03-08" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a00299200b img-responsive" src="/assets/image_418755.jpg" title="Revit2022-03-08" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a002a6200b-pi" style="display: inline;"><img alt="Revit2022-03-07 (2)" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a002a6200b img-responsive" src="/assets/image_547882.jpg" title="Revit2022-03-07 (2)" /></a></p>
<hr />
<p><strong>傾斜した壁のプロファイルを編集する</strong></p>
<p>傾斜した壁のプロファイルの編集を妨げる制限が取り除かれました。<br />垂直壁のプロファイルを編集するのと同じ方法で、傾斜した壁のプロファイルを編集できるようになりました。</p>
<hr />
<p><strong>スロープの指定点勾配</strong></p>
<p>指定点勾配の注釈をスロープ要素に直接配置できるようになりました。<br />スロープ要素に直接傾斜の注釈を作成できない制限が削除されました。指定点勾配を、要素の面またはエッジの特定の点を含む、スロープ要素に直接配置できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802536fc200d-pi" style="display: inline;"><img alt="Revit2022-03-09" class="asset  asset-image at-xid-6a0167607c2431970b0278802536fc200d img-responsive" src="/assets/image_372484.jpg" title="Revit2022-03-09" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/RuxWmigO1bg?list=PLY-ggSrSwbZoVjn_wbz8NtHOV6DiX3Xek" width="712"></iframe></p>
<hr />
<p><strong>カーテン ウォール マリオンにタグを付ける</strong></p>
<p>カーテン ウォール マリオンのカテゴリにタグを付けることができるようになりました。<br />マルチカテゴリ タグを使用してタグを付けるか、既存のタグを編集してカテゴリを[カーテン ウォール マリオン タグ]に変更することにより、カーテン ウォール マリオン専用のタグを作成します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4873200c-pi" style="display: inline;"><img alt="Revit2022-03-10" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4873200c img-responsive" src="/assets/image_172775.jpg" title="Revit2022-03-10" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/YiC8_T94794?list=PLY-ggSrSwbZoVjn_wbz8NtHOV6DiX3Xek" width="712"></iframe></p>
<hr />
<p><strong>動線設計ツールキット</strong></p>
<p>ルート解析には、複数のルートを解析する機能とルート解析タスクの実行に役立つ新しいファミリ コンテンツが追加されました。</p>
<p>動線設計ツールキットに含まれるツールはこれまでアドオンとしてリリースされていましたが、ルート解析ツールの一部としてインストールされるようになりました。動線設計ツールキットには次のツールが含まれています。</p>
<ul>
<li>複数経路: 最小離隔距離で移動経路の点間に複数のルートを同時に作成します。</li>
<li>一方向インジケータ: ファミリをモデルに配置して移動方向を示します。ファミリは移動経路の計算で考慮されます。</li>
<li>人物コンテンツ: モデルにファミリを配置して、スペースの占有を表し、移動経路の線をブロックします。</li>
<li>空間グリッド: 部屋ベースのグリッド オーバーレイを配置して、部屋の空間距離を視覚化します。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880253773200d-pi" style="display: inline;"><img alt="Revit2022-03-11" class="asset  asset-image at-xid-6a0167607c2431970b027880253773200d img-responsive" src="/assets/image_773836.jpg" title="Revit2022-03-11" /></a></p>
<hr />
<p><strong>FormIt との相互運用性の向上</strong></p>
<p>FormIt を概念モデルを開発するためのツールとして使用し、データを失うことなく Revit で設計を調整します。</p>
<p>FormIt の相互運用性が向上したことで、Revit と FormIt の両方でモデルを簡単に操作できるようになりました。<br />アプリケーション間で共有されるジオメトリが更新され、より統一された外観になりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/wreNVbRVhcA?list=PLY-ggSrSwbZoVjn_wbz8NtHOV6DiX3Xek" width="712"></iframe></p>
<hr />
<p><strong>RPC の機能強化</strong></p>
<p>よりフォトリアリスティックなコンテンツが提供されるようになりました。</p>
<ul>
<li>従来の RPC と比較して、改善された 3D リアリスティック ビューの視覚化</li>
<li>レンダリング ビュー以外のビューでの簡易表現</li>
<li>交通(車)の RPC の RPC パラメータに対する新しい色コントロール</li>
<li>家具カテゴリのレンダリングの外観プロパティをサポート</li>
<li>同じ RPC レンダリングの外観を共有するキャンバス内に、多数の RPC インスタンスがある場合の最適化されたパフォーマンス</li>
<li>RPC を必要としないシナリオで作業する場合、リアリスティック ビューの RPC オブジェクトをオフにすることによるパフォーマンスの向上</li>
<li>28 の新しい RPC (人物、輸送機関、家具など)<br />注: これらの新しい RPC 要素は、クラウド レンダリングではサポートされていません。</li>
</ul>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/63M7cH_5aHs?list=PLY-ggSrSwbZoVjn_wbz8NtHOV6DiX3Xek" width="712"></iframe></p>
<hr />
<p><strong>ジェネレーティブデザイン</strong></p>
<p>2022 年のリリースでは、過去数回の 2021 ジェネレーティブデザインの更新で改善されたすべての内容が Revit に統合されています。<br />詳細については、<a href="https://help.autodesk.com/view/RVT/2022/JPN/?guid=GUID-E7E38EEB-2BE7-4195-981E-8EA86B24D3D4">こちら</a>をご参照ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a001e6200b-pi" style="display: inline;"><img alt="Revit2022-03-12" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a001e6200b img-responsive" src="/assets/image_506560.jpg" title="Revit2022-03-12" /></a></p>
<p>\サンプルのスタディ タイプに、3 つの追加のスタディ タイプが利用可能になりました。</p>
<ul>
<li>オブジェクトをランダムに配置</li>
<li>オブジェクトをグリッド状に配置</li>
<li>オブジェクトをステップ グリッド状に配置。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdecd4938200c-pi" style="display: inline;"><img alt="Revit2022-03-13" class="asset  asset-image at-xid-6a0167607c2431970b026bdecd4938200c img-responsive" src="/assets/image_232000.jpg" title="Revit2022-03-13" /></a><br /><br />なお、Autodesk University 2020 にて Forge の Design Automation API for Revit を取り上げた下記のクラスでは、ジェネレーティブデザインのサンプル「オブジェクトのグリッド配置」のアイデアをベースに、遺伝的アルゴリズムのオープンソースライブラリを利用したサンプルもご紹介しております。</p>
<ul>
<li><a href="https://www.autodesk.com/autodesk-university/ja/class/Design-Automation-Revit-jichukarayingyonghe-2020">SD473594: Design Automation for Revit: 基礎から応用へ</a><br /><br /></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788025387d200d-pi" style="display: inline;"><img alt="Revit2022-03-14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788025387d200d image-full img-responsive" src="/assets/image_196536.jpg" title="Revit2022-03-14" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a00307200b-pi" style="display: inline;"><img alt="Revit2022-03-15" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a00307200b image-full img-responsive" src="/assets/image_412741.jpg" title="Revit2022-03-15" /></a></p>
<hr />
<p>次回は、構造設計分野の新機能と改良された機能についてご紹介致します。</p>
<p>By Ryuji Ogasawara</p>

---
layout: "post"
title: "Revit 2022 の新機能 その4"
date: "2021-04-30 01:12:07"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part4.html "
typepad_basename: "new-features-on-revit-2022-part4"
typepad_status: "Publish"
---

<p>Revit 2022 の新機能と改良された機能をシリーズでご紹介しております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part1.html">Revit 2022 の新機能 その1</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part2.html">Revit 2022 の新機能 その2</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/04/new-features-on-revit-2022-part3.html">Revit 2022 の新機能 その3</a></li>
</ul>
<p>今回は、構造設計分野の新機能と機能向上の内容となります。</p>
<hr />
<p>先日、<a href="https://gems.autodesk.com/c/express/88ff59bb-a5e3-43c0-b182-05d4ccaee6c8">Revit 2022 新機能ご紹介オンラインセミナー</a>が実施され、収録動画が公開されました。<br />弊社、日本人講師による解説を通じて新機能を網羅的に把握できるため、お時間のある方はこちらもぜひご視聴ください。</p>
<p class="asset-video"><iframe width="712" height="400" src="https://www.youtube.com/embed/Xzp6meppbvs?list=PLdMYeRRM4zCOsknv2ZQK5tKfXjIC6lfzQ" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen=""></iframe></p>
<hr />
<p><strong>鉄筋セットの鉄筋を個別に移動する</strong></p>
<p>選択した鉄筋セットや配筋システムで、個々の鉄筋をコントロールできるようになりました。<br />これにより、干渉を回避し、システムのロジックを維持できるようになります。</p>
<p>新しい [ 鉄筋を編集 ]コマンドを使用して、選択した鉄筋セット、パスまたは面に配筋システムを選択表示します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802647ea200d-pi" style="display: inline;"><img alt="Revit2022-04-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802647ea200d image-full img-responsive" src="/assets/image_956247.jpg" title="Revit2022-04-01" /></a></p>
<p>1 つまたは複数の個別の鉄筋を選択し、鉄筋を移動、削除、リセットします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802647f5200d-pi" style="display: inline;"><img alt="Revit2022-04-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802647f5200d image-full img-responsive" src="/assets/image_839827.jpg" title="Revit2022-04-02" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a11323200b-pi" style="display: inline;"><img alt="Revit2022-04-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a11323200b image-full img-responsive" src="/assets/image_482501.jpg" title="Revit2022-04-03" /></a></p>
<p>これにより、鉄筋セットやシステムのロジックを維持しながら、他の鉄筋、開口部、または他の要素との干渉を回避できます。<br />削除された鉄筋はどのビューにも表示されず、集計表にもカウントされません。</p>
<p>複数の鉄筋セット、パス配筋システム、または面配筋システムを同時に編集することができます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/EPjCpHb0aro?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>2 点による迅速かつ正確な鉄筋の配置</strong></p>
<p>2 点を指定して単一の鉄筋または鉄筋セットを配置し、鉄筋形状を展開する境界ボックスを定義できるようになりました。<br />単一セグメントの直線鉄筋の場合、寸法と方向を直接指定できます。</p>
<p>2 点で定義された境界ボックスに鉄筋形状を拡張することで、鉄筋が作成されます。</p>
<p>[構造]タブ [配筋]パネル [鉄筋]で、新しい[2 点で配置]コマンドを使用します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880264807200d-pi" style="display: inline;"><img alt="Revit2022-04-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880264807200d image-full img-responsive" src="/assets/image_342783.jpg" title="Revit2022-04-04" /></a><br />[修正 | 鉄筋を配置]コンテキスト タブ [配置方法]パネルで、 (2 点で配置)をクリックし、配置の方向を選択して、プロジェクト内の任意の参照に鉄筋を位置合わせします。</p>
<p><img class="image-full img-responsive" src="/assets/2021-04-27_17-32-02-1.gif" /></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/eq1Mpvw9gbw?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>実際の鉄筋径を使用して鉄筋をモデリングする</strong></p>
<p>実際の鉄筋の直径を使用して鉄筋をモデリングすることで、干渉を回避できるようになりました。これは、直径の大きい鉄筋が多数含まれるコンクリート要素で特に重要です。</p>
<p>[モデル鉄筋径]を[鉄筋径]より大きい値に編集して、鉄筋のサイズを大きく設定します。鉄筋を配置し、鉄筋の拘束を編集して、製造データを抽出します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802648b7200d-pi" style="display: inline;"><img alt="Revit2022-04-06" class="asset  asset-image at-xid-6a0167607c2431970b0278802648b7200d img-responsive" src="/assets/image_83452.jpg" title="Revit2022-04-06" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/vlCcya3woJ0?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>接合をプロファイル サイズに関連付ける</strong></p>
<p>鉄鋼プロファイル プロパティ、鉄鋼の等級、および部材端力に基づいて、鉄骨接合タイプを構造入力要素に関連付けるための規則を簡単に定義できるようになりました。</p>
<p>一般的な標準で定義された規則を組み込むことで、より優れた鉄骨接合ライブラリの作成が可能になります。また、Dynamo プレーヤと強化されたサンプル スクリプトを使用して配置を自動化できます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/zGpnmYGQ86w?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>カスタム曲げフリー フォーム鉄筋の形状コードを選択する</strong></p>
<p>鉄筋形状に自動的に一致させることができないフリー フォーム鉄筋に、異なる形状を割り当てられるようになりました。</p>
<p>これを実行するには、フリー フォーム鉄筋を作成し、鉄筋インスタンス プロパティで別の形状を割り当て​ます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/Q46TYHb2p_g?feature=oembed" width="712"></iframe></p>
<hr />
<p><strong>鉄筋を配置する際に設定を記憶する</strong></p>
<p>鉄筋を配置するために使用される設定は、Revit セッション全体で記憶されます。</p>
<p>次の設定は記憶され、鉄筋を配置するたびに値が保持されます。</p>
<ul>
<li>レイアウト</li>
<li>配置面</li>
<li>配置の向き</li>
<li>鉄筋形状</li>
<li>鉄筋タイプ</li>
</ul>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/uemGhq2NjQs?feature=oembed" width="712"></iframe></p>
<hr />
<p>次回は、MEP 設計分野の新機能と改良された機能についてご紹介致します。</p>
<p>By Ryuji Ogasawara</p>

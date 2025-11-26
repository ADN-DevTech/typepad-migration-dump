---
layout: "post"
title: "Revit 2021 の新機能 その1"
date: "2020-04-10 01:09:26"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-revit-2021-part1.html "
typepad_basename: "new-features-on-revit-2021-part1"
typepad_status: "Publish"
---

<p>今年も Revit の新バージョンとなる Revit 2021 がリリースされました。</p>
<p>今回から複数回にわたって、Revit 2021 の新機能と更新内容、API の対応状況をご紹介していきます。今回は、プラットフォームに共通の新機能と機能向上の内容となります。<br /><br />また、Revit 2020 更新プログラムを購入したユーザ向けのほとんどの新機能と機能拡張が、Revit 2021 でも利用できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51efc02200b-pi" style="display: inline;"><img alt="Revit-2021-hero-image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51efc02200b image-full img-responsive" src="/assets/image_750575.jpg" title="Revit-2021-hero-image" /></a></p>
<p><strong>プラットフォームの新機能及び機能向上</strong></p>
<p><strong>傾斜壁<br /></strong>これまで多数のご要望を頂いておりました「傾斜した壁」を作成できるようになりました。壁を作成または修正する際に、[断面]パラメータを使用して傾斜させることができます。次に、[垂直方向からの角度]パラメータを使用して、-90 ～ +90 度までの角度を指定します。0 度は垂直です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51ef4da200b-pi" style="display: inline;"><img alt="Revit2021_1" class="asset  asset-image at-xid-6a0167607c2431970b0240a51ef4da200b img-responsive" src="/assets/image_470247.jpg" title="Revit2021_1" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/dsCiFao2t4Q?feature=oembed" width="500"></iframe></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/FiZODowrVSM?feature=oembed" width="500"></iframe><br /><br />&#0160;API としては、新たに 3つのビルトインパラメータが追加されました。</p>
<ul>
<li>WALL_CROSS_SECTION
<ul>
<li>垂直壁か傾斜壁かを設定</li>
</ul>
</li>
<li>WALL_SINGLE_SLANT_ANGLE_FROM_VERTICAL
<ul>
<li>傾斜角</li>
</ul>
</li>
<li>INSERT_ORIENTATION
<ul>
<li>挿入する向きの操作 (壁に沿うのか垂直なのか)</li>
</ul>
</li>
</ul>
<p class="asset-video">また、API で壁要素を取り扱う際には、壁が常に垂直であると想定しないようにご注意ください。</p>
<p class="asset-video">&#0160;</p>
<p><strong>PDF とラスタ イメージのリンクとロード解除</strong><br />PDF またはイメージ ファイルを、表示の正確性、パフォーマンス、および機能を維持したまま、ローカルまたはクラウドの保存場所から 2D ビューにリンクできます。この機能により、BIM を作成するときに、BIM プロセス以外の他のユーザに共有されている 2D ドキュメントを、効率的に使用して参照することができます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b44edbf200c-pi" style="display: inline;"><img alt="Revit2021_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b44edbf200c image-full img-responsive" src="/assets/image_755286.jpg" title="Revit2021_2" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/C5z8nUo8uW8?feature=oembed" width="500"></iframe></p>
<p class="asset-video">API では、ImageType クラスのメソッド・プロパティが追加されております。</p>
<ul>
<li>ImageType.Resolution / WidthInPixels / HeightInPixels / Width / Height
<ul>
<li>イメージのサイズ、解像度のプロパティにアクセスします。</li>
</ul>
</li>
<li>ImageTypeOptions.SourceType
<ul>
<li>ImageType のソースをリンクかインポートに選択できます。</li>
</ul>
</li>
<li>ImageType.Source
<ul>
<li>どのようにイメージが作成されたか表します。</li>
</ul>
</li>
<li>ImageType.Status
<ul>
<li>ロードされているかアンロードされているか表します。</li>
</ul>
</li>
<li>ImageType.Unload()
<ul>
<li>リンクされたイメージをアンロードします</li>
</ul>
</li>
</ul>
<p>&#0160;</p>
<p><strong>集計表機能の向上</strong></p>
<p>大規模な集計表を操作する場合、主要な見出しとタイトル行をフリーズすることで、常に現在の集計表ビューに表示しておくことができます。これにより、見出しとタイトルは集計表を下にスクロールした場合でも表示されたままになります。見出しをロックするには、リボン上の[見出しを固定]ボタンをクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51ef7c0200b-pi" style="display: inline;"><img alt="Revit2021_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51ef7c0200b image-full img-responsive" src="/assets/image_585043.jpg" title="Revit2021_3" /></a></p>
<p>また集計表の行を見やすくするために、行を縞模様の色でストライプ表示することができるようになりました。 [集計表プロパティ]ダイアログの[外観]タブでこの機能を有効にすると、1 行おきにカスタム色を選択できます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51ef7cc200b-pi" style="display: inline;"><img alt="Revit2021_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51ef7cc200b image-full img-responsive" src="/assets/image_536184.jpg" title="Revit2021_4" /></a><br />それぞれの機能に対応する API も公開されました。</p>
<ul>
<li>ViewSchedule.HasStripedRows
<ul>
<li>集計表がストライプデザインかどうか判別</li>
</ul>
</li>
<li>ViewSchedule.UseStripedRowsOnSheets
<ul>
<li>シートでストライプデザインを使用するかコントロール</li>
</ul>
</li>
<li>ViewSchedule.Get/SetStripedRowsColor()
<ul>
<li>ストライプの色を設定</li>
</ul>
</li>
</ul>
<ul>
<li>ViewSchedule.IsHeaderFrozen
<ul>
<li>集計表の見出しを固定か非固定か設定</li>
</ul>
</li>
</ul>
<p><strong>橋梁</strong><br />Revit の組み込みカテゴリが拡張され、Revit での橋梁設計のサポートが強化されました。これは、Autodesk InfraWorks および Revit の土木構造物のワークフローをサポートします。鉄筋も橋梁要素をホストに設定することができるようになりました。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/gHVAGTQRSAE?feature=oembed" width="500"></iframe><br />新しいインフラ カテゴリ フィルタの分野が追加されました。<br />インフラ フィルタ内で、26 個のモデル カテゴリと 20 個の注釈カテゴリが使用できるようになりました。これらのカテゴリは、表示/グラフィックスの上書き、オブジェクト スタイル、集計表、タグをサポートします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b44ef08200c-pi" style="display: inline;"><img alt="Revit2021_5" class="asset  asset-image at-xid-6a0167607c2431970b025d9b44ef08200c img-responsive" src="/assets/image_330463.jpg" title="Revit2021_5" /></a></p>
<p>&#0160;</p>
<p><strong>ビュー フィルタ - ビューで有効化</strong><br />ビューに適用されるビュー フィルタを、[表示/グラフィックスの上書き]ダイアログ ボックスで簡単に有効または無効に切り替えることができるようになりました。ビュー フィルタの効果は、ビューに対してフィルタを追加および削除するのではなく、有効/無効のチェックボックスで切り替えます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b44ef59200c-pi" style="display: inline;"><img alt="Revit2021_6" class="asset  asset-image at-xid-6a0167607c2431970b025d9b44ef59200c img-responsive" src="/assets/image_716587.jpg" title="Revit2021_6" /></a></p>
<p>API では、下記のメソッドが追加されております。</p>
<ul>
<li>View.Get/SetIsFilterEnabled()
<ul>
<li>指定のビューでフィルタが有効化されているかどうか確認します。</li>
</ul>
</li>
<li>View.GetOrderedFilters()&#0160;
<ul>
<li>適用されているビューフィルタをその適用の順序で取得します。</li>
</ul>
</li>
</ul>
<p>&#0160;</p>
<p><strong>ファミリ内のボイドの[ジオメトリを切り取り]パラメータ</strong><br />ファミリで使用されるボイドに、[ジオメトリを切り取り]パラメータが追加されました。このパラメータで、ファミリ内の交差するジオメトリをボイドで切り取るかどうかをコントロールします。また、ボイドをファミリタイプのパラメータに関連づけることもできます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4fa3d92200d-pi" style="display: inline;"><img alt="Revit2021_7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4fa3d92200d img-responsive" src="/assets/image_342910.jpg" title="Revit2021_7" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/fzTllh2XCI8?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>タグ回転</strong><br />より多くのタグ カテゴリで、[コンポーネントに沿って回転]ファミリ パラメータを利用できるようになりました。特殊装置タグ、マスタグ、一般モデルタグ、家具タグなども対象です。タグの基準点は、ホストのコンポーネントに関連づけられ、よりフレキシブルになります。<br /><br />このパラメータにより、プロジェクトをドキュメント化する際の柔軟性が向上します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b44f1f1200c-pi" style="display: inline;"><img alt="Revit2021_9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b44f1f1200c img-responsive" src="/assets/image_47068.jpg" title="Revit2021_9" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4fa3f4a200d-pi" style="display: inline;"><img alt="Revit2021_8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4fa3f4a200d image-full img-responsive" src="/assets/image_760758.jpg" title="Revit2021_8" /></a><br /><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/SrpnwJoz83I?feature=oembed" width="500"></iframe></p>
<p>次回は、建築分野の新機能と更新内容についてご紹介致します。</p>
<p>By Ryuji Ogasawara</p>

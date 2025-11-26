---
layout: "post"
title: "Autodesk Revit 2023 の新機能 ～ その3"
date: "2022-04-22 03:44:43"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/04/new-features-on-revit-2023-part3.html "
typepad_basename: "new-features-on-revit-2023-part3"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/new-features-on-revit-2023-part2.html">前回の記事</a>に引き続き Autodesk Revit 2023の新機能として、今回は、建築設計に関する新機能と機能改善をご紹介致します。</p>
<hr />
<p><strong>FormIt/Revit ワークフローの改善</strong></p>
<p>FormIt は、初期段階の設計コンセプトのスケッチ、コラボレート、解析、修正を行うことができます。また、プロジェクトの初期段階から BIM ベースのコンセプト デザインを使用して効率的に作業できます。</p>
<ul>
<li><a href="https://windows.help.formit.autodesk.com/v/japanese/">FormIt for Windows</a></li>
</ul>
<p>Revit 2023 では、Revit と FormIt 間で相互に初期段階や詳細なモデリングの作業を行うことがこれまでになく簡単になりました。</p>
<p style="padding-left: 40px;"><strong>[CAD をリンク]の FormIt ファイル</strong><br />Revit の[CAD をリンク]機能で FormIt AXM ファイル形式を受け入れ、FormIt ファイルを共有できるようになりました。この機能により、FormIt と Revit で作業する設計者のコラボレーションが改善されます。[挿入]タブの[CAD をリンク]にアクセスします。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="/assets/image_223580.jpg" style="display: inline;"><img alt="Revit2023-03-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa6aa1d200c image-full img-responsive" src="/assets/image_223580.jpg" title="Revit2023-03-01" /></a></p>
<p style="padding-left: 40px;">&#0160;</p>
<p style="padding-left: 40px;"><strong>3D スケッチのワークフローでリンクする</strong><br />3D スケッチのワークフローで FormIt モデルを Revit にリンクして、共同作業を改善できるようになりました。旧バージョンではモデルがリンクされずにインポートされていましたが、このワークフローを使用することで改善されました。</p>
<p style="padding-left: 40px;">FormIt の[3D スケッチ]ボタンを使用して Revit でモデルを起動します。このボタンをクリックすると、モデルが FormIt でリンクとして開きます。FormIt で設計をスケッチした後、[Revit に送信]ボタンをクリックするか、Revit モデルを保存して、更新したモデルを FormIt に戻します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="/assets/image_719607.jpg" style="display: inline;"><img alt="Revit2023-03-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e151864a200b image-full img-responsive" src="/assets/image_719607.jpg" title="Revit2023-03-02" /></a></p>
<p style="padding-left: 40px;">&#0160;</p>
<p style="padding-left: 40px;"><strong>FormIt で編集</strong><br />[FormIt で編集]ボタンを使用すると、FormIt から Revit リンクをクリックして直接編集することができます。FormIt で Revit リンク(AXM ファイルから)を選択して、[修正]タブの[FormIt で編集]ボタンにアクセスします。[FormIt で編集]ボタンをクリックすると、FormIt Pro for Windows でモデルを開いて編集できるようになります。編集が完了したら、[Revit に送信]をクリックするか、FormIt でモデルを保存して、Revit でモデルを更新します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="/assets/image_937480.jpg" style="display: inline;"><img alt="Revit2023-03-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788078efae200d image-full img-responsive" src="/assets/image_937480.jpg" title="Revit2023-03-03" /></a></p>
<p style="padding-left: 40px;">注: FormIt ワークフローの 3D スケッチと編集の両方を利用するには、コンピュータに FormIt Windows Pro をインストールしておく必要があります。</p>
<p>&#0160;</p>
<p><strong>3D で計測する</strong><br />3D ビューで計測ツールを使用できるようになりました。選択した 2 つのポイント間、またはチェーンに沿って選択したポイント間を計測します。</p>
<p>最初に選択した点に対して垂直な平面で測定値を拘束するには、[Ctrl]キーを使用します。<br />キーボード ショートカットの SP(スナップ、垂線)オーバーライドを使用して、ターゲットのラインまたは平面に対する垂直距離を計測します。</p>
<p><a class="asset-img-link" href="/assets/image_111900.jpg" style="display: inline;"><img alt="Revit2023-03-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1518693200b image-full img-responsive" src="/assets/image_111900.jpg" title="Revit2023-03-04" /></a></p>
<p>&#0160;</p>
<p><strong>シート上でビューをスワップする</strong><br />シートに配置されたビューポートで、関連付けられたビューをモデル内の別のビューにスワップできます。</p>
<p>ビューと位置合わせのパラメータは、ビュー プロパティとリボン パネルの両方で使用できます。</p>
<p>[位置合わせ]パラメータには次の 2 つのオプションがあります。</p>
<ul>
<li><strong>[ビューポートの中心]</strong>: ビューポートの中心を使用してビューを入れ替え、位置を変更します。これが既定です。</li>
<li><strong>[ビューの原点]</strong>: 各ビューの原点を使用してビューを入れ替えます。これは、ビュー内のジオメトリをシート間で位置合わせする場合に、平面図ビューなどの特定のビュー タイプで特に役立ちます。</li>
</ul>
<p><a class="asset-img-link" href="/assets/image_50423.jpg" style="display: inline;"><img alt="Revit2023-03-05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa6aa75200c image-full img-responsive" src="/assets/image_50423.jpg" title="Revit2023-03-05" /></a></p>
<p><br /><strong>集計表をシートでフィルタする</strong><br />集計表ツールで、シートでフィルタする機能がサポートされるようになりました。<br />集計表には、集計表が配置されているシートのビューポートで表示される要素のみが表示されます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/U2dS31VsvQs?feature=oembed" width="712"></iframe></p>
<p><br /><strong>インタラクティブな日照シミュレーション</strong><br />ビュー内で時刻や人工照明のオプションを簡単にコントロールできるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa6aace200c-pi" style="display: inline;"><img alt="Revit2023-03-06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa6aace200c image-full img-responsive" src="/assets/image_876972.jpg" title="Revit2023-03-06" /></a></p>
<p>[日照シミュレーション]タブにアクセスするには</p>
<ol>
<li>プロジェクト ブラウザで、日照シミュレーションのアニメーションを作成したビューをダブルクリックします。</li>
<li>ビュー コントロール バーで、 (シャドウ オン)をクリックしてから、 (太陽のパス オン)をクリックし、コンテキスト メニューから[日照シミュレーション]を選択します。</li>
</ol>
<p class="asset-video"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/Pyk4RJq9joU?feature=oembed" width="712"></iframe></p>
<p>[日照シミュレーション]タブには、次の 4 つのパネルがあります。</p>
<ul>
<li><strong>プレビューと再生</strong>: アニメーションの最初から最後までの再生、前後への移動、ループでの再生を行います。</li>
<li><strong>表示</strong>: プレビュー中に速度、太陽光の強度、影の強度を変更します。</li>
<li><strong>シミュレーション タイプ</strong>: 日照シミュレーションのタイプを変更します。[日時指定イメージ]、[1 日]、[複数日]、[位置指定]から選択します。</li>
<li><strong>プリセットとデータ</strong>: コントロールは、プレビューするシミュレーションのタイプによって異なります。[日時指定イメージ]シミュレーション タイプの場合は、現在のプリセットを選択して、シミュレートする現在の日付と時刻を設定できます。[1 日]および[複数日]シミュレーション タイプの場合は、現在のプリセットを選択して、現在のフレーム日時を確認し、シミュレートする現在のフレームを設定できます。[位置指定]シミュレーション タイプの場合は、現在のプリセットを選択し、シミュレートする方位角と高度を設定できます。</li>
</ul>
<p>By Ryuji Ogasawara</p>

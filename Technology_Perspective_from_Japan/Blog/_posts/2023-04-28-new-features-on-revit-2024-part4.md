---
layout: "post"
title: "Revit 2024 新機能 ～ その４"
date: "2023-04-28 01:41:26"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/04/new-features-on-revit-2024-part4.html "
typepad_basename: "new-features-on-revit-2024-part4"
typepad_status: "Publish"
---

<p>今回は、Autodesk Revit 2024 の建築設計分野に関連する新機能をご紹介いたします。</p>
<p><strong>地形ソリッド</strong></p>
<p>地盤面要素をソリッド ジオメトリとして作成できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a333e8200c-pi" style="display: inline;"><img alt="Revit2024-04-01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a333e8200c image-full img-responsive" src="/assets/image_488698.jpg" title="Revit2024-04-01" /></a></p>
<p>地形ソリッド要素は、モデルの地盤面と外構の状態を表します。境界をスケッチし、境界内に標高ポイントを追加して、地形ソリッドを作成することができます。<br />また、CAD ファイルまたは CSV ファイルを読み込んで作成することもできます。</p>
<p>マテリアルのサーフェス パターンを割り当てたり、他のモデルジオメトリで切り取ることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b68535f073200d-pi" style="display: inline;"><img alt="Revit2024-04-02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b68535f073200d image-full img-responsive" src="/assets/image_814434.jpg" title="Revit2024-04-02" /></a></p>
<p>なお、Revit 2023.1 までは、地形は地盤面要素をサーフェスという形態で作成することができました。<br />既存のプロジェクトでのみサポートされている旧形式の要素の扱い方については、下記のページでご案内しております。<br />地盤面から地形ソリッドを生成することもできます。</p>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2024/JPN/?guid=GUID-8A8A947D-C2E6-4DFB-9B49-091574C8EDA3">地盤面</a></li>
</ul>
<hr />
<p><strong>テクスチャ表示スタイル</strong></p>
<p>新しい[テクスチャ]表示スタイルを使用して、照明効果なしでレンダリング マテリアルのテクスチャを適用できるようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b68535deba200d-pi" style="display: inline;"><img alt="Revit2024-04-03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b68535deba200d image-full img-responsive" src="/assets/image_610075.jpg" title="Revit2024-04-03" /></a></p>
<p>マテリアルのテクスチャを表示するが照明効果やレンダリングを使用しないプレゼンテーションやビューを作成する場合は、[テクスチャ]表示スタイルを使用します。</p>
<p>[テクスチャ]表示スタイルでは、モデル内の要素にマテリアルの外観アセットが適用されますが、照明効果は適用されません。テクスチャは、モデル内のすべてのサーフェスで均一な値で表示されます。</p>
<hr />
<p><strong>ジオメトリを切り取りの機能強化</strong></p>
<p>プロジェクト環境で切り取りできるように、追加のカテゴリが有効になりました。</p>
<p>次のカテゴリまたはサブカテゴリを使用して、プロジェクト環境で切り取りを行うことができるようになりました。</p>
<ul>
<li>地形ソリッド</li>
<li>天井</li>
<li>床</li>
<li>スラブ エッジ</li>
<li>屋根</li>
<li>鼻隠し</li>
<li>樋</li>
<li>軒裏</li>
<li>構造基礎</li>
<li>壁</li>
<li>壁の造作材</li>
<li>マスのボイド作成</li>
</ul>
<hr />
<p><strong>リボンから日照設定にアクセスする</strong></p>
<p>リボンから直接[日照設定]ダイアログにアクセスできるようになりました。</p>
<p>[日照設定]ダイアログにすばやくアクセスするには、[日照シミュレーション]タブ [プリセットとデータ]パネルの新しいボタンを使用します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b68535dede200d-pi" style="display: inline;"><img alt="Revit2024-04-04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b68535dede200d image-full img-responsive" src="/assets/image_486431.jpg" title="Revit2024-04-04" /></a></p>
<hr />
<p><strong>秒単位の間隔で日照シミュレーションを実行する</strong></p>
<p>日照シミュレーションを実行する場合に、秒単位で計測したより短い時間間隔を使用できるようになりました。</p>
<p>15、30、45、または 60 秒間隔を使用して、より高い精度で影を調査できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a333f8200c-pi" style="display: inline;"><img alt="Revit2024-04-05" class="asset  asset-image at-xid-6a0167607c2431970b02b751a333f8200c img-responsive" src="/assets/image_712528.jpg" title="Revit2024-04-05" /></a></p>
<hr />
<p><strong>Family テンプレートについて</strong></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/04/new-features-on-revit-2024-part1.html">Revit 2024 新機能 ～ その１</a>でご案内しておりました、日本語のファミリテンプレートが欠落している問題について、<strong><a href="https://forums.autodesk.com/t5/revit-navisworks-ri-ben-yu/bajon-meinorevitkontentsu-famiri-tenpureto-famiritenpureto-nodaunrodoni-guanshite/td-p/10239727">こちらの記事</a></strong>で解決方法が公開されましたので、下記に抜粋いたします。</p>
<p>当初の Revit 2024 でのリリースでは、Revit 2024 の日本語用コンテンツに一部抜け落ちがみられました。</p>
<p>この問題は、<strong>4月19日 Autodesk Account ページ上で公開された「Japanese Content v1 for Revit 2024」をインストール</strong>することで解消できます。</p>
<p>随時ダウンロードページのインストーラーも更新があるかと思いますが、一旦ファミリテンプレートに不足が見られる場合は、<a href="https://manage.autodesk.com/products/updates">アカウントページ</a>から、「Japanese Content v1 for Revit 2024」をインストールし正しいコンテンツが追加されたことをご確認ください。</p>
<hr />
<p>By Ryuji Ogasawara</p>

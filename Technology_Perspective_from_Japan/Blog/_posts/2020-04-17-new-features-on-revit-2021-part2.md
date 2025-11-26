---
layout: "post"
title: "Revit 2021 の新機能 その 2"
date: "2020-04-17 01:10:00"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-revit-2021-part2.html "
typepad_basename: "new-features-on-revit-2021-part2"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-revit-2021-part1.html">前回に</a>引き続き、今回は、Revit 2021 の建築分野の新機能と機能向上についてご紹介致します。</p>
<p><strong>リアリスティック ビューの強化</strong></p>
<p>建築設計分野での大きな課題の一つに、3D ビューでのビジュアライズ性能とインタラクション性能があります。<br />Revit 2021 では、リアリスティックビューの機能が強化され、デザインの視覚的な影響をダイレクトに理解・評価できるようになりました。<br />ステークホルダーや共同設計者に設計の意図をよりわかりやすく伝えることができるようになります。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Bbi_lU4nPlo?feature=oembed" width="500"></iframe></p>
<p>新しいリアリスティックビュー機能は、リアリスティックなマテリアルと照明で設計することができます。<br />マテリアルは、より最適な表現に向上し、自動露出機能は、建物の外側から内側に入るようなケースでも、複雑で曖昧な設定を取り除きました。<br />また、[グラフィックス表示オプション]でエッジが有効になっている場合のエッジの表示が改善されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b47229d200c-pi" style="display: inline;"><img alt="Revit2021_10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b47229d200c image-full img-responsive" src="/assets/image_176646.jpg" title="Revit2021_10" /></a></p>
<p>そして、ビューのナビゲーション操作はより速くスムースに改善し、描画速度が10倍（前バージョン比）になりました。<br />新しいリアリスティックモードのウォークスルーで、クライアントとリアルタイムにコミュニケーションができます。</p>
<p>下記の動画で前バージョンとの比較をご覧いただけます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/9ltyNFKzFI0?feature=oembed" width="500"></iframe></p>
<p>この機能が追加されたことで、インタラクティブ レイトレースモードは廃止となりました。<br />また Revit 2020 と比較して、Revit 2021 でのマテリアルと照明は、同じ表現になることを保証していません。</p>
<p>&#0160;</p>
<p><strong>ジェネレーティブ デザイン</strong></p>
<p>Generative Design in Revit で、デザインのゴール、拘束、および入力値に基づいて代替デザインを迅速に生成できるようになりました。<br />デザインの問題に対して、調査、最適化、および情報に基づく意思決定を行うことができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4fc69e1200d-pi" style="display: inline;"><img alt="Revit2021_12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4fc69e1200d image-full img-responsive" src="/assets/image_876412.jpg" title="Revit2021_12" /></a></p>
<p>下記の動画で、そのワークフローをご覧いただけます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/pD3jKh3UJvQ?feature=oembed" width="500"></iframe></p>
<p>まず、Generative Design in Revit を使用するには、[管理]タブ上で[スタディを作成]をクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a5212b00200b-pi" style="display: inline;"><img alt="Revit2021_13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a5212b00200b image-full img-responsive" src="/assets/image_649366.jpg" title="Revit2021_13" /></a></p>
<p>[スタディを作成]ダイアログ ボックスに、使用可能なスタディ タイプが一覧表示されます。Revit のインストール時に、スタディ タイプのサンプルが3つ追加されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a5212b08200b-pi" style="display: inline;"><img alt="Revit2021_14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a5212b08200b img-responsive" src="/assets/image_725899.jpg" title="Revit2021_14" /></a></p>
<ul>
<li>3 ボックスのマス
<ul>
<li>3 つの簡単なマスの高さと相対位置を変更して、マス モデルを生成します。</li>
</ul>
</li>
<li>ワークスペース レイアウト
<ul>
<li>デスクの列に合わせて、ドア、窓、柱を考慮しながら部屋のレイアウトを生成します。</li>
</ul>
</li>
<li>窓からのビューを最大にする
<ul>
<li>部屋に視点をいくつか生成し、それらの視点から見える外の景色の質を表すスコアを計算します。</li>
</ul>
</li>
</ul>
<p>※Dynamo を使用すると、作業で発生しているデザイン上の特定の課題に対処するための Dynamo グラフ(スクリプト)を作成できます。<br />新しいグラフを作成してテストした後、作成者は Dynamo から書き出して、Generative Design in Revit のスタディ タイプとして使用することができます。</p>
<p>次に、[スタディを作成]ダイアログ ボックスでスタディ タイプを選択すると、[スタディを定義]ダイアログが表示されます。<br />代替デザインを生成するための基礎となるロジック（方法）を選択します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b4722c0200c-pi" style="display: inline;"><img alt="Revit2021_15" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b4722c0200c img-responsive" src="/assets/image_714207.jpg" title="Revit2021_15" /></a>　 <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a5212b25200b-pi" style="display: inline;"><img alt="Revit2021_16" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a5212b25200b img-responsive" src="/assets/image_903704.jpg" title="Revit2021_16" /></a></p>
<ul>
<li>Optimize: 目標を達成するために反復処理する</li>
<li>Randomize: 範囲内でランダム値を使用する</li>
<li>Cross-product: 範囲内で値を均等に分布</li>
<li>Like this: 望ましい成果のバリエーションを検討する</li>
</ul>
<p>そして、ゴール、拘束、定数、変数などの設計基準を入力します。<br />[生成]をクリックして、スタディを開始します。[成果を検討]ダイアログ ボックスが開き、スタディの進行状況が表示されます。</p>
<p>スタディが完了すると、代替デザインや成果を検討して、ニーズに最も適したソリューションを見つけることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b4722d7200c-pi" style="display: inline;"><img alt="Revit2021_12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b4722d7200c image-full img-responsive" src="/assets/image_871407.jpg" title="Revit2021_12" /></a></p>
<p>モデルに統合する特定の成果が決定した場合は、その成果を選択して[Revit 要素を作成]をクリックします。<br />デザインがモデルに統合され、必要に応じて要素が追加および修正されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a5212b3a200b-pi" style="display: inline;"><img alt="Revit2021_17" class="asset  asset-image at-xid-6a0167607c2431970b0240a5212b3a200b img-responsive" src="/assets/image_457511.jpg" title="Revit2021_17" /></a></p>
<p>今回ご紹介した Generative Design in Revit を使用するには、Autodesk Architecture, Engineering &amp; Construction (AEC) Collection を購入し、アクティベーションが別途必要になりますので、ご注意ください。</p>
<p>追記：<br />AEC Collection のライセンスをお持ちでない場合でも、Dynamo for Revit を通じて、ジェネレーティブデザインの機能をお試し頂くことができます。<br />Dynamo for Revit の[ジェネレーティブ デザイン]タブのメニューから、「ジェネレーティブデザイン用に書き出し」でスタディタイプを作成し、「スタディを作成」「成果を検討」の機能をご利用いただけます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a522160c200b-pi" style="display: inline;"><img alt="Revit2021_18" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a522160c200b image-full img-responsive" src="/assets/image_8940.jpg" title="Revit2021_18" /></a></p>
<p>詳細につきましては、下記のページをご参照ください。</p>
<ul>
<li><a href="https://help.autodesk.com/view/RVT/2021/JPN/?guid=GUID-BA7949DA-C2F8-4B00-9EC0-16180E0A6663">Generative Design(Dynamo 作成者向け)</a></li>
</ul>
<p>新しいリアリスティックビューモードとジェネレーティブデザインをぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>

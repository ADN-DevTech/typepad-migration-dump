---
layout: "post"
title: "Revit 2020 の新機能 その1"
date: "2019-04-19 02:06:25"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-revit-2020-part1.html "
typepad_basename: "new-features-on-revit-2020-part1"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a454d3e5200c-pi" style="float: right;"><img alt="Revit-2020-badge-256px" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a454d3e5200c img-responsive" src="/assets/image_737977.jpg" style="margin: 0px 0px 5px 5px;" title="Revit-2020-badge-256px" /></a>今年も Revit の新バージョンとなる Revit 2020 がリリースされました。今回から複数回にわたって、Revit 2020 の新機能と更新内容をご紹介していきます。Revit 2019 更新プログラムでオートデスクのサブスクリプション メンバーが使用できるようになったほとんどの新機能と機能拡張が、Revit 2020 でも利用できるようになりました。</p>
<p><strong>Revit Ideas</strong></p>
<p>Revit Ideas フォーラムは、Revit のお客様からご要望を投稿頂けるフォーラムです。お客様からの推薦や投票によって支持を得られたご要望は、新機能・拡張機能として追加されております。</p>
<p><a class="asset-img-link" href="https://forums.autodesk.com/t5/revit-ideas/idb-p/302/tab/most-kudoed" style="display: inline;"><img alt="Revit Ideas" class="asset  asset-image at-xid-6a0167607c2431970b0224e03521eb200d img-responsive" src="/assets/image_444562.jpg" title="Revit Ideas" /></a></p>
<p>英語になってしまいますが、Ideas フォーラムへのご要望の投稿もぜひご検討ください。</p>
<p><a href="https://forums.autodesk.com/t5/revit-ideas/idb-p/302/tab/most-kudoed">https://forums.autodesk.com/t5/revit-ideas/idb-p/302/tab/most-kudoed</a></p>
<p>&#0160;</p>
<p><strong>Revit ロードマップ</strong></p>
<p>Revit 2019.2 のリリースに伴い、2019年1月23日に Revit ロードマップの最新版が公開されました。</p>
<p><a class="asset-img-link" href="http://blogs.autodesk.com/revit/category/roadmap/" style="display: inline;"><img alt="RoadMap" class="asset  asset-image at-xid-6a0167607c2431970b0223c84668ea200c img-responsive" src="/assets/image_377810.jpg" title="RoadMap" /></a></p>
<p>ロードマップで公開されているステートメントは、現在の Autodesk が把握している事項を反映しており、将来の利用可能性について約束や保証を意図するものではありません。そのため、予告なしに変更されることがあります。そのため、このステートメントに依存することがないようにお願い申し上げます。</p>
<p><a href="http://blogs.autodesk.com/revit/category/roadmap/">http://blogs.autodesk.com/revit/category/roadmap/</a></p>
<p>最新のロードマップでは、Revit 2019 に引き続き、Revit 2020 のキーコンセプトとして 3つのテーマが設けられております。</p>
<ul>
<li><strong>設計機能</strong><br />設計の意図を形作るための効率的なモデル作成<br />快適で直観的、コンテキストのわかりやすい経験を重視<br />高精度で詳細なモデル</li>
<li><strong>最適化機能</strong><br />解析、シミュレーション、デザインの最適化<br />タスクの自動化による生産性の向上<br />ソフトウェアが設計作業に集中できる環境を提供</li>
<li><strong>接続機能</strong><br />建設業界に特化したコラボレーションツールでプロジェクトのチームを繋げる<br />複数の専門分野のワークフローを可能にする<br />プロジェクトのライフサイクルにおける全てのフェーズに BIM 活用を拡げる</li>
</ul>
<p>今回は、Revit 2020 の<strong>「設計機能」</strong>をテーマとした新機能についてご紹介いたします。Revit 2020 の新機能には、設計の意図をより正確に表し、より詳細なドキュメントを作成する機能に焦点を当てています。</p>
<p><strong>楕円形の壁を作成する</strong></p>
<p>Revit の[楕円]または[部分楕円]描画ツールを使用して、高度なジオメトリの壁およびカーテン ウォールを作成できます。これらのツールは、意匠壁や構造壁を作成または修正しているときに、[描画]パネルで使用できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a2a4d7200b-pi" style="display: inline;"><img alt="Revit 2020 Part1-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a2a4d7200b image-full img-responsive" src="/assets/image_578235.jpg" title="Revit 2020 Part1-1" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/n7pW2myDmV8?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>分電盤のフィード スルー ラグ</strong></p>
<p>フィード スルー ラグを介して分電盤を接続できるようになりました。子パネル(すなわち給電先のパネル)が給電元のパネルから連続するように回路番号を定義できます。<br />この機能は、文書作成タスクを効率化し、解析をサポートするシステムを通じて、設計者がより優れたモデルを作成するのに役立ちます。</p>
<p>&#0160;</p>
<p><strong>幹線矢印をカスタマイズする</strong></p>
<p>印刷された電気図面での幹線矢印の読みやすさを改善するために、マルチ回路幹線などの幹線矢印のスタイル（矢印の角度やサイズなど）をカスタマイズできます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/ExtD07JYc_I?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p>下記の<strong>構造エンジニアリング分野の新機能と改善</strong>は、コンクリートの詳細化と、製造、および建設プロセスにて、より忠実度の高いモデリングツールをとして、設計と製造を結び付けるのに役立ちます。</p>
<p><strong>改善された鉄筋のコピーおよび移動ロジック</strong></p>
<p>形状駆動鉄筋のコピーおよび移動が予測しやすくなったため、鉄筋のモデリングがより正確になり、設計意図が尊重されるようになりました。これによって時間が節約され、一貫性のある製造データ生成を可能にします。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/T94b0AJJZLc?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>フリー フォーム鉄筋のための複数鉄筋注釈</strong></p>
<p>平面鉄筋と平行鉄筋でフリーフォーム鉄筋セットを寸法設定するには、複数鉄筋注釈を使用します。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/vhRIK1FOv8I?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>コンクリート面への複数の鉄筋注釈</strong></p>
<p>コンクリート ホスト面、または Revit のその他の寸法参照に対して、鉄筋セットの位置を寸法設定します。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/EFztt5mQTpo?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>モデルインプレイスの階段内の鉄筋</strong></p>
<p>モデルインプレイスの階段に鉄筋を配置し、コンクリート建築物の非常に詳細なモデルを提供できます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/5qFMa2KHfG0?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>標準鋼接合を伝播する</strong></p>
<p>現在のプロジェクトで標準鋼接合を伝播します。同一のコンテキストがあるモデル内のすべての場所で、選択した接合が適用されます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/NmEwg9f2ciU?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>標準鉄骨接合タイプのパラメータ</strong></p>
<p>Revit タイプの機能が標準鉄骨接合に導入され、このような種類の要素での作業を容易にします。<br />接続パラメータに基づいて、各接続ファミリのタイプをカスタマイズします。同じタイプのすべてのインスタンスを一箇所から更新します。同じプロジェクトまたは別のプロジェクトで、同じ接続構成を再利用します。接続グループのプロパティを一致させます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/1VjuRFnsfWo?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p><strong>集計表、タグ、およびフィルタの新しい鉄骨製造形状パラメータ</strong></p>
<p>鉄骨製造要素には、集計表、タグ、およびフィルタで利用可能な追加情報を表示する新しいパラメータがあります。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/XxjGD39bNwU?feature=oembed" width="500"></iframe></p>
<p>次回は、<strong>「最適化機能」</strong>をテーマとした新機能と更新内容についてご案内いたします。</p>
<p>By Ryuji Ogasawara</p>

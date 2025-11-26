---
layout: "post"
title: "Revit 2019 の新機能 その1"
date: "2018-04-27 01:29:10"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-revit-2019-part1.html "
typepad_basename: "new-features-on-revit-2019-part1"
typepad_status: "Publish"
---

<p>今年も Revit の新バージョンとなる Revit 2019 がリリースされました。</p>
<p>今回から複数回にわたって、Revit 2019 の新機能と更新内容、API の対応状況をご紹介していきます。<br />Revit 2018 更新プログラムでオートデスクのサブスクリプション メンバーが使用できるようになったほとんどの新機能と機能拡張が、Revit 2019 でも利用できるようになりました。</p>
<p>Part.1 では Revit 2019 の新機能の全体像を 3つのテーマに沿ってご紹介します。Part.2 では専門分野共通のコア機能、Part.3 は建築設計分野、Part.4 と Part.5 で構造設計分野とMEP 設計分野の機能をそれぞれ取り上げる予定です。</p>
<p><strong>Revit Ideas</strong></p>
<p>Revit Ideas フォーラムは、Revit のお客様からご要望を投稿頂けるフォーラムです。お客様からの推薦や投票によって支持を得られたご要望は、新機能・拡張機能として追加されております。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03521eb200d-pi" style="display: inline;"><img alt="Revit Ideas" class="asset  asset-image at-xid-6a0167607c2431970b0224e03521eb200d img-responsive" src="/assets/image_444562.jpg" title="Revit Ideas" /></a></p>
<p>英語になってしまいますが、Ideas フォーラムへのご要望の投稿もぜひご検討ください。</p>
<p><a href="https://forums.autodesk.com/t5/revit-ideas/idb-p/302/tab/most-kudoed">https://forums.autodesk.com/t5/revit-ideas/idb-p/302/tab/most-kudoed</a></p>
<p>&#0160;</p>
<p><strong>Revit ロードマップ</strong></p>
<p>Revit 2018.3 及び、2019 のリリースに伴い、2018年4月13日に Revit ロードマップの最新版が公開されました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84668ea200c-pi" style="display: inline;"><img alt="RoadMap" class="asset  asset-image at-xid-6a0167607c2431970b0223c84668ea200c img-responsive" src="/assets/image_377810.jpg" title="RoadMap" /></a></p>
<p>ロードマップで公開されているステートメントは、現在の Autodesk が把握している事項を反映しており、将来の利用可能性について約束や保証を意図するものではありません。そのため、予告なしに変更されることがあります。そのため、このステートメントに依存することがないようにお願い申し上げます。</p>
<p><a href="http://blogs.autodesk.com/revit/category/roadmap/">http://blogs.autodesk.com/revit/category/roadmap/</a></p>
<p>最新のロードマップでは、Revit 2019 のキーコンセプトとして 3つのテーマが公開されました。</p>
<ul>
<li><strong>設計</strong><br />設計の意図を形作るための効率的なモデル作成<br />快適で直観的、コンテキストのわかりやすい経験を重視<br />高精度で詳細なモデル</li>
<li><strong>最適化機能</strong><br />解析、シミュレーション、デザインの最適化<br />タスクの自動化による生産性の向上<br />ソフトウェアが設計作業に集中できる環境を提供</li>
<li><strong>接続機能</strong><br />建設業界に特化したコラボレーションツールでプロジェクトのチームを繋げる<br />複数の専門分野のワークフローを可能にする<br />プロジェクトのライフサイクルにおける全てのフェーズに BIM 活用を拡げる</li>
</ul>
<p>それでは、Revit 2019 のダイジェストをこれら 3つのテーマに沿ってご紹介いたします。この内容は、上記の Revit ロードマップの最新版を抜粋して翻訳しております。</p>
<p><strong>設計</strong></p>
<p>このテーマには、プロジェクトのデータを作成するための優れたツール、機能、およびエクスペリエンスが含まれています。設計は時間の大部分を費やします。そのため、私たちは、ユーザーが、必要なモデルを、必要な詳細レベルで、必要なドキュメントをより簡単に作成できる環境を提供することを目指しております。<br />これは容易なことではありませんが、私たちはこれが大きなインパクトを与えることができる領域であると認識しています。</p>
<p>私たちがデザインをより効率的にするための1つの方法は、設計者の意図をより理解することです。 Revit 2019 では、<strong><span style="color: #ff0000;">ビューフィルタの規則で OR 条件</span></strong> を使用すると、ビューを要件に合わせてより迅速にカスタマイズできるようになります。<br />また複雑な配管の設計をサポートするために、Revit 2019 は、<strong><span style="color: #ff0000;">冷温水配管系統で油圧分離を追加</span></strong>できるようになり、より正確なシステム計算を容易にします。</p>
<p><img alt="Revit-2910-or-filters-1024x535" class="asset  asset-image at-xid-6a0167607c2431970b0224df2e22bc200b img-responsive" src="/assets/image_133147.jpg" title="Revit-2910-or-filters-1024x535" /></p>
<p>私たちはまた、より楽しく、直感的でコンテキストに満ちた経験を築くために投資しました。その 1つは、Revit 2019 で、<strong><span style="color: #ff0000;">パースペクティブビュー上でトリミングをオフに設定できる機能の追加</span></strong>です。Revit 2017 で追加されたパースペクティブビューでのモデル編集機能と併せて、パースペクティブビューで様々な設計作業を行うことができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c8466946200c-pi" style="display: inline;"><img alt="Revit-2019-uncropped-3d" class="asset  asset-image at-xid-6a0167607c2431970b0223c8466946200c img-responsive" src="/assets/image_823334.jpg" title="Revit-2019-uncropped-3d" /></a></p>
<p>そしてもう1つの新しい追加機能は、<strong>3D ビューでレベルを表示・編集できる機能</strong>です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df2e22cb200b-pi" style="display: inline;"><img alt="Revit-2019-levels-in-3d" class="asset  asset-image at-xid-6a0167607c2431970b0224df2e22cb200b img-responsive" src="/assets/image_30509.jpg" title="Revit-2019-levels-in-3d" /></a></p>
<p>また、より高精度にディテールをモデリングすることがますます重要になっていることも理解しています。そのため、より詳細な精度で鉄骨構造及び接合をモデル化する機能を追加しました。また、Revit 2019 では、<span style="color: #ff0000;"><strong>「Structural Precast for Revit」</strong></span>で、複雑なプレキャストコンクリートオブジェクトの詳細設計をサポートする拡張機能にも取り組んでいます。<br /><br /></p>
<p><strong>最適化機能</strong></p>
<p>最適化のテーマは、作業の容易さと生産性を向上させると同時に、より優れた建物を提供するための作業にも役立ちます。</p>
<p>たとえば、ビューはどのモデルでも最も基本的なインタラクションの1つなので、Revit 2019 では、<strong><span style="color: #ff0000;">タブ ビュー/タイル ビュー</span></strong>と<strong><span style="color: #ff0000;">マルチモニタサポートの追加</span></strong>により、大幅な改善を図りました。</p>
<p><img alt="Revit-2019-multiple-monitors" class="asset  asset-image at-xid-6a0167607c2431970b0224df2e22c0200b img-responsive" src="/assets/image_566966.jpg" title="Revit-2019-multiple-monitors" /></p>
<p>次に自動化は、今後成長する領域だと考えております。Revit 2019 では、プレキャストコンクリートのワークフローで、設計を製造フェーズで利用できるように、ルールベースで自動化する機能を追加しました。ロードマップでは、鉄骨接合の自動設計により、構造のオートメーション機能を追加する予定です。また、<strong><span style="color: #ff0000;">Forge の Design Automation API が Revit に対応</span></strong>することで、クラウドネイティブなソリューション をRevit のデータと連携させることができます。</p>
<p>同様に、解析とシミュレーションは、より優れた効率的な建物を作るための重要な分野です。ロードマップでは、MEP システムのゾーニングや設備の選択、サイジングなどの分野で、さらに多くの自動化と最適化の機能の追加を計画しています。ロードマップへの新たな追加点として、避難シミュレーションのために経路と距離を計算して、設計プロセスの早期段階でより効率的な意思決定を行うことができる機能も検討しています。</p>
<p>&#0160;</p>
<p><strong>接続</strong><br />接続は、プロジェクトチームの連携と、より良いマルチプロダクトのワークフローを実現する上で重要です。私たちの目標は、コラボレーションして情報を交換してプロセスを改善することです。この分野では、オートデスクのツール間でデータを共有し、IFC や PDF などの業界標準をサポートし、プロジェクトチームがより良い共同作業を行うためのツールを提供することに重点を置いています。</p>
<p>一例として、今回のリリースでは、<strong><span style="color: #ff0000;">次世代の Collaboration for Revit（別名 BIM 360 Design）が導入</span></strong>されました。 Revit の <strong><span style="color: #ff0000;">クラウドワークシェアリングから BIM 360 Document Management と BIM 360 Design Collaboration に接続</span></strong>できるようにすることで、プロジェクトチームは、共通のデータプラットフォームを介してデータを交換し、社内外のステークホルダーとプロジェクトを共有するワークフローをサポートします。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e0352238200d-pi" style="display: inline;"><img alt="BIM-360-Design-image-1024x615" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0224e0352238200d image-full img-responsive" src="/assets/image_641917.jpg" title="BIM-360-Design-image-1024x615" /></a></p>
<p>また、Revit へのデータの取り込み方法の改善についても検討しています。Revit モデルに PDFファイルを簡単にリンクできるように、<strong><span style="color: #ff0000;">PDF アンダーレイ機能をロードマップに追加</span></strong>しました。最新のファイル形式をサポートするために SketchUp のインポートを改善することも検討しています。さらに、私たちは IFC の機能を強化することで、IFC4 認証を取得し、IFC が当社の製品の重要な部分であると考えています。</p>
<p>ワークフローでは、製造モデリングの改善と、製造用図面の自動化と CAM エクスポートの機能で、製造工程とダイレクトに接続できる方法に投資しています。これは、BIM をプロジェクトのライフサイクルのあらゆる側面に拡張するための重要なステップです。</p>
<p>接続に投資している分野の多くは、可能な限り多くの人々が BIM プロセスに参加し、ワークフローや経験を改善することを可能にすることです。私たちは、これらの分野を Revit だけでなくオートデスク全体の戦略の重要な部分として優先することにしました。</p>
<p>&#0160;</p>
<p>Revit ロードマップでは、Revit のキーコンセプト、開発チームが計画している新機能や改善案などを確認することができます。今回は、Revit 2019 の新機能のダイジェストを含めてご紹介いたしました。<br />次回は、専門分野共通のコア機能の新機能と更新内容、API の対応状況についてご案内いたします。</p>
<p>By Ryuji Ogasawara</p>

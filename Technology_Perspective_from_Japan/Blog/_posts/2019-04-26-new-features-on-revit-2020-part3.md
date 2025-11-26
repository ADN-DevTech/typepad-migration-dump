---
layout: "post"
title: "Revit 2020 の新機能 その3"
date: "2019-04-26 03:07:33"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-revit-2020-part3.html "
typepad_basename: "new-features-on-revit-2020-part3"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a455fa4c200c-pi" style="float: right;"><img alt="Revit-2020-badge-256px" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a455fa4c200c img-responsive" src="/assets/image_736745.jpg" style="margin: 0px 0px 5px 5px;" title="Revit-2020-badge-256px" /></a><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-revit-2020-part2.html">前回に</a>引き続き、Revit 2020 の新機能についてご紹介させて頂きます。</p>
<p>今回は、<strong>「接続機能」</strong>をテーマとした新機能と更新内容についてご案内いたします。</p>
<p><strong>「接続機能」</strong>は、プロジェクトチームの連携を強化し、マルチプロダクト間のワークフローをより良くするための重要なテーマです。<br />ユーザーが円滑にコラボレーションでき、他者と情報交換するプロセスを改善することが、一つのゴールとなっています。</p>
<p>つまり、この分野では、オートデスクのソフトウェアやサービス間でデータを共有したり、IFCやPDFなどの業界標準をサポートしたり、プロジェクトチームが互いに連携して作業できるようにするツールを提供します。</p>
<p>&#0160;</p>
<p><strong>PDF のアンダーレイ</strong></p>
<p>Revit Ideas で最も投票数の多かったリクエストが Revit 2020 でサポートされました。<br />1 ページまたは複数ページの PDF ファイルを Revit の 2D ビューに読み込んで、モデルの参照として使用できるようになりました。<br />これにより、2D データを BIM と接続して、建設のワークフローを改善します。</p>
<ul>
<li>ラスター イメージと同じ方法で、PDF ファイルを操作する</li>
<li>ベクトル データを含む PDF にスナップする</li>
<li>[イメージを管理]ダイアログに、イメージおよび PDF ファイルの相対パスおよび BIM 360 クラウドのパスが表示されます。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4577d85200c-pi" style="display: inline;"><img alt="Revit 2020 Part3-7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4577d85200c image-full img-responsive" src="/assets/image_265882.jpg" title="Revit 2020 Part3-7" /></a></p>
<p>例えば、下記のように地図データをプロジェクトに重ね合わせることも簡単です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4809879200d-pi" style="display: inline;"><img alt="Revit 2020 Part3-3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4809879200d image-full img-responsive" src="/assets/image_469997.jpg" title="Revit 2020 Part3-3" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a53740200b-pi" style="display: inline;"><img alt="Revit 2020 Part3-1" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a53740200b img-responsive" src="/assets/image_806738.jpg" title="Revit 2020 Part3-1" /></a></p>
<p>PDF のベクトルデータにスナップできるため、平面図のデータをベースにモデルに起こすこともできます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a5374b200b-pi" style="display: inline;"><img alt="Revit 2020 Part3-2" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a5374b200b img-responsive" src="/assets/image_133883.jpg" title="Revit 2020 Part3-2" /></a></p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/RqJ0hgJ9sIA?feature=oembed" width="500"></iframe></p>
<p class="asset-video">&#0160;</p>
<p class="asset-video"><strong>SketchUp モデルを読み込む</strong></p>
<p class="asset-video">最新バージョンの SketchUp モデルを Revit にロードできます。Revit 2020 では、Sketchup 2018 のインポートに対応しました。<br />SketchUp による早期の設計フェーズが完了したら、そのまま Revit にインポートして、ワークフローを繋げることができます。</p>
<p class="asset-video"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/4zkfYe2zYaE?feature=oembed" width="500"></iframe></p>
<p>&#0160;</p>
<p><strong>Revit Extension for Fabrication</strong></p>
<p>Revit から製造用ジョブに製造用部品レイアウトを書き出すには、Revit Extension for Fabrication を使用します。</p>
<p>拡張機能を使用すると、Autodesk Fabrication 製品(CADmep、ESTmep、CAMduct)で作成した製造用ジョブを読み込んで、Revit でモデリングを続行することができます。</p>
<p>Revit 2020 では、製造データの新しいエクスポートのオプションが追加され、Excel などのスプレッドシートのアプリケーションにインポート可能な CSV または TXT データファイルを生成できるようになりました。</p>
<p>これらの詳細データを、プレコンストラクションフェーズや、集計や調達などの製造のワークフローでご活用いただけます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a480988a200d-pi" style="display: inline;"><img alt="Revit 2020 Part3-4" class="asset  asset-image at-xid-6a0167607c2431970b0240a480988a200d img-responsive" src="/assets/image_120076.jpg" title="Revit 2020 Part3-4" /></a></p>
<p>&#0160;</p>
<p><strong>クラウドモデルアップグレード</strong></p>
<p>BIM 360 プロジェクトに保存されているクラウドモデルの Revit バージョンを、ボタンの 1 クリックだけでアップグレードできるようになりました。</p>
<p>これまでは、Revit を起動してプロジェクトを選択し、長時間を要して最新バージョンに移行する必要があった作業が、クラウド上でバックグラウンドで実行できるようになったことで、待機時間を飛躍的に節約できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4576981200c-pi" style="display: inline;"><img alt="Revit 2020 Part3-5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4576981200c image-full img-responsive" src="/assets/image_556148.jpg" title="Revit 2020 Part3-5" /></a></p>
<p>詳細は下記のページに記載されております。</p>
<ul>
<li><a href="http://help.autodesk.com/view/BIM360D/JPN/?guid=BIM360D_Document_Management_About_Document_Management_to_upgrade_revit_cloud_models_html">Revit のクラウド モデルをアップグレードするには</a></li>
</ul>
<p>実はこの機能は、バックエンドでは、Forge の <strong>Design Automation API for Revit</strong> が利用されています。</p>
<p>現在、BIM 360 のクラウドサービス（BIM 360 Design, BIM 360 Glue, BIM 360 Build など）は、既存の BIM 360 を一新する 次世代 BIM 360（別名、NextGen BIM 360、Next Generation BIM 360）として、Forge を基盤として作り替えられています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a45769b1200c-pi" style="display: inline;"><img alt="Revit 2020 Part3-6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a45769b1200c image-full img-responsive" src="/assets/image_938778.jpg" title="Revit 2020 Part3-6" /></a></p>
<p>もちろん、BIM 360 でも Revit が最も重要なデータとして位置付けられておりますので、BIM 360 上で Revit モデルを活用するための様々な機能が今後も追加されていくはずです。</p>
<p>Revit プロジェクトのバージョンアップグレードは、従来は、Revit アプリケーションを起動して、プロジェクトを開く段階で、そのプロジェクトが作成された Revit バージョンを識別し、起動中の Revit よりも低いバージョンであれば、アップグレードをするというフローが必要でした。</p>
<p>クラウドモデルアップグレードは、Revit のコアエンジンをクラウド上で起動し、クラウドストレージに保存されている Revit プロジェクトを開いて、クラウド上でバージョンアップグレードします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a48098ee200d-pi" style="float: right;"><img alt="DesignAutomationRevit" class="asset  asset-image at-xid-6a0167607c2431970b0240a48098ee200d img-responsive" src="/assets/image_791701.jpg" style="margin: 0px 0px 5px 5px;" title="DesignAutomationRevit" /></a><strong><span style="text-decoration: underline; color: #ff0000;">このように、Revit のコアエンジンをクラウド上で起動し、Revit アドインを実行する仕組みが、Forge の API としてデベロッパー向けにも公開されております。</span></strong></p>
<p>それが、<strong>Design Automation API for Revit</strong> です。</p>
<p>ただし、サードパーティーのデベロッパーは、BIM 360 の各サービスに直接、カスタム機能を追加することはできません。</p>
<p>Design Automation API for Revit の概要については、下記のブログ記事をご参照ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/design-automation-api-for-revit-public-beta.html">Design Automation API for Revit パブリックベータ版の公開</a><br /><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/understanding-steps-to-use-design-automation-api-for-revit.html"></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/understanding-steps-to-use-design-automation-api-for-revit.html">Design Automation API for Revit 開発手順の理解</a></li>
</ul>
<p>Design Automation API for Revit にご興味がある方は、<strong>5月28日（火）東京、5月31日（金） 大阪にてワークショップを開催</strong>いたしますので、ぜひご参加ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/forge-1-day-workshop-design-automation-api-for-revit-2019-5.html">Forge 1 Day Workshop - Design Automation API for Revit - 開催 - 2019年5月</a><br /><br /></li>
</ul>
<p>By Ryuji Ogasawara</p>

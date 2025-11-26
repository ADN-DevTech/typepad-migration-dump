---
layout: "post"
title: "Forge DevCon Las Vegas 2018 アップデート"
date: "2018-11-13 00:49:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/11/forge-devcon-las-vegas-2018-update.html "
typepad_basename: "forge-devcon-las-vegas-2018-update"
typepad_status: "Publish"
---

<p>米国太平洋標準時間の 11 月 12 日（日本標準時間 11 月 13 日）、Las Vegas で 2 日間の会期予定で開催される Forge DevCon 2018 の初日を迎え、キーボート及びブレイクアウト セッションが実施されました。あと 1 日会期を残していますが、今回の DevCon でキーとなった点についてご案内しておきたいと思います。なお、キーノートの事前登録数は 1,100 名で、同じく日本からの事前登録数は 40 名でした。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad379e5ed200c-pi" style="display: inline;"><img alt="Devcon_keynote1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad379e5ed200c image-full img-responsive" src="/assets/image_651449.jpg" title="Devcon_keynote1" /></a></p>
<p>キーノートでは、従来のデスクトップ製品間で起こりがちだったデザイン ファイルを使ったデータ交換について、問題の核心がデータがアプリケーションの機能ありきだった点が改めて指摘されました。具体的には、データ ファイル形式がアプリケーションの機能によってアプリケーション毎に定義されてしまい、それらの各種データ ファイル形式を使ったデータ交換機能も、各アプリケーションの実装に委ねられてしまっていた、という点です。これによって、あたかもアプリケーション間に壁が存在するような印象があったことも否めません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bfa85f200b-pi" style="display: inline;"><img alt="Devcon_keynote2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bfa85f200b image-full img-responsive" src="/assets/image_102829.jpg" title="Devcon_keynote2" /></a></p>
<p>これらに壁を取り除くソリューションとして説明されたキーワードが <strong>Data at the Center</strong>&#0160;です。Data at the Center は、従来のアプリケーション機能で成り立っていた「点から点」のデータ交換ではなく、すべてのデータをクラウド上に初めから保存し、必要なデータをやり取りする形を採用する方法です。</p>
<p>実は、このコンセプトは一昨年の Forge DevCon 2016 で初めて紹介されて、着々と開発を進めてきた<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/11/consider-about-forge-hfdm.html" rel="noopener noreferrer" target="_blank"> HFDM</a></strong>&#0160;のコンセプトに他なりません。ただし、純粋な HFDM データに加え、従来のデザイン ファイル データ形式も同等に扱えるように改良を加えた点が異なります。重要なのは、アプリケーションありきのデータ ファイル形式（あるいはデータ交換機能）ではなく、データありきのアプリケーション開発へ、オートデスクの開発方針が転換されたものと考られる点です。今日の設計/デザイン作業が、まだデスクトップ製品（アプリケーション）と、そのデータ作成で成立していることを考えれば、妥当な方針転換とも言えるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bfa89f200b-pi" style="display: inline;"><img alt="Devcon_keynote3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bfa89f200b image-full img-responsive" src="/assets/image_429505.jpg" title="Devcon_keynote3" /></a></p>
<p>今回の Data at the Center コンセプトの採用によって、3rd party の開発者の皆様への Forge API （Forge Data Platform）提供に遅れが出ることになってしまうかと思いますが、クラウドを真に利用し、かつ、実用性を兼ね備えた API が今後登場することが期待されます。別の言い方をすれば、Forge を使って HFDM を採用する Web アプリケーションだけでなく、従来のデスクトップ製品も、クラウドを使った&#0160;Data at the Center コンセプトを採用することが示唆されたとも考えられます。</p>
<p>話題は変わりますが、今回は Forge Village と題した展示エリアが Forge DevCon の開催フロアで用意され、Forge System Integrator 各社をはじめ、Forge ISV 等のパートナー企業による展示がおこなわれました。日本からは、<a href="https://www.apptec.co.jp/" rel="noopener noreferrer" target="_blank">応用技術株式会社</a>にブース展示をいただき、多くのお客様や日本でのパートナーをお探しの企業の方々にお越しいただきました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a016a2200d-pi" style="display: inline;"><img alt="Village" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a016a2200d image-full img-responsive" src="/assets/image_397523.jpg" title="Village" /></a></p>
<p>Autodesk University のオープン前で Forge DevCon 初日のみの展示エリアという位置づけですが、Forge を目指してご来場いただく方がほとんどなので、効果的な出会いの場になったのではないかと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a0164e200d-pi" style="display: inline;"><img alt="Village1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a0164e200d image-full img-responsive" src="/assets/image_536134.jpg" title="Village1" /></a></p>
<p><span aria-level="4" class=" _2iem" role="heading">また、Forge Village の AR|VR Village コーナーでは 、日本から参加された<a href="https://hololab.co.jp" rel="noopener" target="_blank">株式会社</a><a href="https://hololab.co.jp/" rel="noopener noreferrer" target="_blank">ホロラボ </a>の HoloLens アプリ「AR CAD Viewer」をデモ展示いただきました。</span></p>
<p><span aria-level="4" class=" _2iem" role="heading"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad379f1b6200c-pi" style="display: inline;"><img alt="Village2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad379f1b6200c image-full img-responsive" src="/assets/image_743315.jpg" title="Village2" /></a></span></p>
<p>By Toshiaki Isezaki</p>

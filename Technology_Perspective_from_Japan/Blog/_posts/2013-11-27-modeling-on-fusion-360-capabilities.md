---
layout: "post"
title: "Fusion 360 モデリングと機能"
date: "2013-11-27 02:03:00"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/11/modeling-on-fusion-360-capabilities.html "
typepad_basename: "modeling-on-fusion-360-capabilities"
typepad_status: "Publish"
---

<p>クラウドを利用した Autodesk Fusion 360 は、いままでになく容易に操作を習得が出来る、フリーモデリング機能を備えたメカニカル 3D CAD で、AutoCAD 2012 や 2013、その他製品に同梱されていた Inventor Fusion Technology Preview が持つユニークな機能を踏襲しているばかりか、新しい T-Spline ベースの編集も可能な画期的なクラウド サービスです。現段階では Trial 版を無償 90日間お試しいただくフェーズであるため、残念ながら日本語化はされていません。ただ、日本のユーザの方にもご利用いただけるのは言うまでもありません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0191cee7970c-pi" style="display: inline;"><img alt="Fusion-360-badge-1000px" class="asset  asset-image at-xid-6a0167607c2431970b019b0191cee7970c" src="/assets/image_250095.jpg" title="Fusion-360-badge-1000px" /></a></p>
<p>デスクトップ版の AutoCAD とクラウドを利用した AutoCAD 360 の違いでも明確なとおり、もちろん、Fusion 360 は デスクトップ版の Inventor を置き換えるものではありません。クラウドの利点を最大限発揮するコラボレーション機能を目的としたプロフェッショナルな設計者や、メカニカル デザインを手掛ける小規模なデザイン事務所、個人事業主を主要な想定ユーザとしています。 その意味では、オートデスクがコンシューマ向けに提供している 123D シリーズサービス・製品とは、一線を画しています。今回は、Fusion 360 の機能の概要を動画と使って簡単に紹介したいと思います。</p>
<p>Fusion 360 は、Autodesk 360 を利用するクラウド サービスで、編集データは Autodesk 360 に保存されているのが前提です。もちろん、SSL通信をはじめとした業界標準のセキュリティ機能を備えているほか、共有ユーザを指定して、コラボレーション機能の利用を明確に宣言しないかぎり、保存されたデータが他人から参照されたり、編集されることはありません。</p>
<p>Fusion 360 で 3D モデルを新規に作成し始めることもできますが、他社製品を含むデスクトップ CAD が作成したデータをインポートして、Fusion 360 で編集していくこともできます。My Home 画面の New Data 領域から Import タブを表示して、[Choose File] ボタンでクライアント コンピュータにあるデータ ファイルを指定し、[Import] ボタンを使ってファイルをインポート（クラウドにアップロード）することが出来ます。現在、インポート可能なファイル形式は次のとおりです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0191a87c970b-pi" style="display: inline;"><img alt="ImportableDataType" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0191a87c970b img-responsive" src="/assets/image_610239.jpg" title="ImportableDataType" /></a></p>
<p>3D モデルを新規に作成する場合には、My Home 画面の New Data 領域で Design タブを表示後、名前を入力して [Create] ボタンをクリックするだけです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01920474970d-pi" style="display: inline;"><img alt="NewModel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b01920474970d image-full img-responsive" src="/assets/image_563158.jpg" title="NewModel" /></a></p>
<p>既存データを指定した場合でも、新規にデータを作成する場合でも、前述の操作で AutoCAD 360 にデータがアップロードされます。編集を始めると、編集終了時に自動的にバージョン管理されたデータが保存されるようになります。</p>
<p>実際のモデリングで使用するユーザ インタフェースには、T-Spline ボディを作成、編集する [SCULPT] ワークスペース、ソリッドボディを作成、編集する [MODEL] ワークスペース、サーフェスを作成、編集する [PATCH] ワークスペースの 3 つのメニュー ワークスペースが用意されています。適宜、このワークスペースを切り替えながら操作をしていくことになります。</p>
<p>また、モデリング時には Body と Component という言葉を理解する必要があります。デスクトップ版 Inventor の用語に簡単に置き換えるなら、Body はパーツ、Component はオカレンスとして考えることが出来ます。それでは、上下カバーをヒンジで拘束したケースの作成過程を見てみてください。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/sf55wrbgVr8?feature=oembed" width="500"></iframe>&#0160;</p>
<p>この動画で明らかなのは、Fusion 360 でモデリングしていく上で、Inventor や他社製のメカニカル 3D CAD に見られるようなスケッチ平面、パーツ環境、アセンブリ環境といった編集モードを明確に切り替える必要がない、という点です。これゆえに、容易な習得が可能という訳です。最後の部分では、マテリアルを提供してモデルの質感を表現したり、干渉チェック機能を利用して、干渉箇所を新しいコンポーネントとして作成することもしていす。</p>
<p>さて、最近では、Inventor にもフリーフォーム デザインの機能が充実してきていますが、もともとは、前述の &#0160;Inventor Fusion Technology Preview で評価いただいたものです。Fusion 360 も、<a href="http://en.wikipedia.org/wiki/T-spline" target="_blank">T-Spline</a> という新しいスプラインベースのフリーフォーム モデリング機能があります。次の動画では、その部分を紹介しています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/QKyaOU7Ehv4?feature=oembed" width="500"></iframe>&#0160;</p>
<p>既存データをインポートして Fusion 360 で編集する場合には、Fusion 360 で正しくフィーチャを認識する必要があります。Fusion 360 には、Inventor Fusion Technology Preview が持っていたような「フィーチャ認識」機能が用意されています。次の動画は、AutoCAD DWG ファイルをインポートして作成したモデルに対して、フィーチャ認識（Find Feaure）させて編集をする過程を紹介しています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/FdYCfBqnITM?feature=oembed" width="500"></iframe>&#0160;</p>
<p>さて、モデリング中には、AutoCAD などのデスクトップ製品と同じように「表示スタイル」を変更して、陰線処理を含むワイヤーフレームやシェーディングなどの見やすい表示環境で選択することができます。また、RapitRT と呼ばれるリアルタイム レンダリング テクノロジも見逃せません。マウス操作で画面中央下の Visual Style から「Ray Traceing」を指定すれば、ビューを変更する度に、品質の応じた高品質な表現を得ることが出来ます。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/4pO1pT4jIfw?feature=oembed" width="500"></iframe>&#0160;</p>
<p>Ray Tracing 表現には、Interactive、Good、Best の 3 つの品質があります。得に Best を選択した場合には、マテリアルの質感はデスクトップ製品のレンダリング機能と同等の結果を得ることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b019ed93c970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VisualQuarity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b019ed93c970b image-full img-responsive" src="/assets/image_966383.jpg" title="VisualQuarity" /></a></p>
<p style="text-align: left;">残念ながら、Ray Tracing の結果を画像ファイルとしてダウンロードすることはできませんが、その必要がある場合には、ダウンロード可能なファイル形式で 3D モデルをダウンロードして、Autodesk 360 Rendering を利用することも出来るわけです（マテリアルの再適用が必要）。現在ダウンロード可能なファイル形式は、次のとおりです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01921f30970b-pi" style="display: inline;"><img alt="DownloadableFileType" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b01921f30970b img-responsive" src="/assets/image_591570.jpg" title="DownloadableFileType" /></a></p>
<p>クラウド ベースのメカニカル 3D CAD と聞いて、機能も能力も見劣りのするものとお考えだったのではないでしょうか?</p>
<p>ここで紹介したのは、Fusion 360 のほんの一部の機能です。次世代を担う新しい設計環境として、みなさんの業務を変えていく可能性を秘めています。トライアルは<a href="http://autodesk.com/tryfusion360" target="_self">http://autodesk.com/tryfusion360</a> にアクセスするだけで始められます。 クライアントのハードウェア アクセラレーションを利用するために小さなアプリケーションモジュールをダウンロードしてインストールする必要がありますが、アカウントは、無償の <a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=6625" target="_blank"><strong>Autodesk ID</strong></a> で利用できます。クラウド ベースのメカニカル 3D CAD、<strong>Autodesk Fusion 360</strong> をぜひお試しください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>

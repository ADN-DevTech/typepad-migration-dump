---
layout: "post"
title: "Autodesk ReCap/ReCap Pro"
date: "2014-02-26 01:53:06"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/02/autodesk-recap-and-recap-pro.html "
typepad_basename: "autodesk-recap-and-recap-pro"
typepad_status: "Publish"
---

<p>点群データを扱うツールとして、無償の Autodesk ReCap と、<a href="http://adndevblog.typepad.com/technology_perspective/2013/09/launching-rental-plan.html" target="_blank">レンタル ライセンス</a>&#0160;として有償提供される Autodesk ReCap Pro が存在します。無償版の Autodesk ReCap は、<a href="http://www.autodesk.com/products/recap/get-started" target="_blank">http://www.autodesk.com/products/recap/get-started</a> からダウンロードしてご利用いただくことが出来ます。また、AutoCAD 2014 などの 2014 製品にも同梱されています。この無償版 Autodesk ReCap は、リリース後にアップデートされています。アップデート作業は、ReCap を起動してからユーザ インタフェース上で実行することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d7eb5c3970d-pi" style="display: inline;"><img alt="ReCap_Update" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d7eb5c3970d img-responsive" src="/assets/image_779648.jpg" title="ReCap_Update" /></a></p>
<p>最終のアップデートは2013年の12月に行われていますが、最後のアップデートを適用すると、Autodesk ReCap Pro の機能を 30 日間無償で利用できるようになります。</p>
<p>今日は、デスクトップ製品としての Autodesk ReCap と他のオートデスク製品や Autodesk ReCap 360 クラウド サービスとの連携を含め、そのワークフローをご紹介しておきます。まず、無償版の Autodesk ReCap で何がかのなのかをご案内しておきます。</p>
<p>Autodesk ReCap は、3D レーザースキャナが計測したデータを点群データとして表示する機能や、計測時に内蔵する CCD カメラが取得した画像を合成して表示する Real View と呼ばれるパノラマ画像表示を提供します。更に、点群上の領域を指定して不要な点群を除去したり、精緻な距離計測や注釈といったマークアップを加えることができます。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/vOCovzS3v0I?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="text-align: left;">また、編集やマークアップを加えた ReCap プロジェクトを Autodesk 360 にパブリッシュすることで、他のメンバと共有してコレボレーションすることが出来ます。パブリッシュ可能な ReCap プロジェクトは、パノラマ計測が可能な 3D レーザースキャナでデータから生成されたサポート ファイルが存在する必要があります。具体的には、プロジェクトのサポート フォルダ内に、プロジェクトが参照する実際の点群データ（*.rcs）の他に、ストラクチャ データ（*.rcc）ファイルが必要です。Autodesk ReCap 上で点群データ（*.rcs）のみしか含まないプロジェクトを開いている場合には、パブリッシュ ボタンは表示されません。</p>
<p style="text-align: left;">&#0160; &#0160; &#0160; &#0160; &#0160;&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3b626970b-pi" style="display: inline;"><img alt="Publish_Button" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3b626970b img-responsive" src="/assets/image_383620.jpg" style="width: 400px;" title="Publish_Button" /></a><br />&#0160;<br /> &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3b605970b-pi" style="display: inline;"><img alt="No_Publish_Button" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3b605970b img-responsive" src="/assets/image_41377.jpg" title="No_Publish_Button" /></a>&#0160;</p>
<p style="text-align: left;">共有されたデータは、Real View の状態を&#0160;Autodesk 360 や Autodesk ReCap 360 上から Web ブラウザを使ってReal View を参照できるので、パブリッシュ前に加えられたマークアップを Autodesk ReCap がインストールされていない環境でも参照できます。ReCap 360 上でも距離計測と注釈をマークアップして保存できます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/no65skFxwK0?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>Autodesk ReCap プロジェクトやエクスポートした点群ファイルは、そのまま AutoCAD や Revit、InfraWorks といったデスクチップ製品にインポートして再利用できます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc699a3970b-pi" style="display: inline;"><img alt="ReCap_Workflow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc699a3970b image-full img-responsive" src="/assets/image_141400.jpg" title="ReCap_Workflow" /></a></p>
<p>ここまで紹介した内容は、無償版の Autodesk ReCap で利用できる機能です。Autodesk ReCap Pro へのアップグレードは、レンタル ライセンスとして <a href="http://store.autodesk.co.jp/store/adskjp/ja_JP/pd/ThemeID.29255400/productID.285068900" target="_blank">Autodesk ストア</a> からご購入いただけます。購入プロセスが完了すると、無償版 Autodesk ReCap のアクティベーションを経て、ウィンドウ タイトルに Pro という文字が表示されるようになり、ロックされて利用できなくなっていた ReCap Pro の機能が利用できるようになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d81a0ec970d-pi" style="display: inline;"><img alt="ReCap Pro" class="asset  asset-image at-xid-6a0167607c2431970b01a73d81a0ec970d img-responsive" src="/assets/image_350955.jpg" title="ReCap Pro" /></a></p>
<p>ReCap と ReCap Pro の違いは次のとおりです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc3b76c970b-pi" style="display: inline;"><img alt="ReCap_vs_ReCap_Pro" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc3b76c970b img-responsive" src="/assets/image_151358.jpg" title="ReCap_vs_ReCap_Pro" /></a></p>
<p>ReCap Pro 固有の機能は、異なるスキャンデータを1つの空間（プロジェクト）に登録と、計測コントロールを配置して精緻な計測結果の取得が可能になります。前者は、1つの座標系で複数のスキャンデータを統合できる点、後者は、RMS（Root Mean Square、二乗平均平方根）を用いた高い精度の値を得ることが出来るようになります。</p>
<p style="text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/D7otHvqepZM?feature=oembed" width="500"></iframe>&#0160;</p>
<p>最後になってしまいましたが、ご案内した機能が網羅的に紹介されている&#0160;Autodesk ReCap のイメージビデオがありますので、こちらをご覧ください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/EA7iwhwI_J8?feature=oembed" width="500"></iframe>&#0160;</p>
<p>利用用途はさまざまですが、現況として取り込んだ点群データの利用は、今後急速に拡大していくはずです。&#0160;</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>

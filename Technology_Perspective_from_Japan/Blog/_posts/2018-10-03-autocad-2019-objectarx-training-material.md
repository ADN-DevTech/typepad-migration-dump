---
layout: "post"
title: "AutoCAD 2019 ObjectARX トレーニング マテリアル"
date: "2018-10-03 00:15:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/10/autocad-2019-objectarx-training-material.html "
typepad_basename: "autocad-2019-objectarx-training-material"
typepad_status: "Publish"
---

<p>先日の AutoCAD 2019 用 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/09/autocad-2019-dotnet_api-training-materials.html" rel="noopener noreferrer" target="_blank">AutoCAD .NET API トレーニング マテリアル</a></strong>に続いて、ObjectARX のトレーニング マテリアルを公開します。</p>
<ul>
<li>本マテリアルは、<a href="http://www.autodesk.co.jp/developautocad" rel="noopener noreferrer" target="_blank">http://www.autodesk.co.jp/developautocad</a>&#0160;で公開している&#0160;<a href="http://images.autodesk.com/adsk/files/ObjectARXWizards-2018.zip">ObjectARX 2019 Wizard</a>&#0160;を利用しています。</li>
<li>&quot;C:\ObjectARX 2019” に&#0160;ObjectARX 2019 SDK が、&quot;C:\Program Files\Autodesk\AutoCAD 2019&quot; に AutoCAD 2019 がインストールされている環境でサンプル プロジェクトを作成、同梱していますので、これ以外のパスで&#0160;ObjectARX 2019 SDK と AutoCAD 2019 をインストールされている環境では、Visual Studio 2017 Update 2 以降のバージョンで正しくプロジェクトを開けなかったり、ビルドが出来なかったりする可能性があります。ご注意ください。</li>
</ul>
<hr />
<p><strong>AutoCAD 2019 ObjectARX トレーニング マテリアル</strong></p>
<p>AutoCAD 2019 ObjectARX トレーニング マテリアルは、次のリンクからダウンロードすることが出来ます。&#0160;</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b022ad395c404200d img-responsive"><a href="http://adndevblog.typepad.com/files/autocad-2019-objectarx-training.zip"><strong>AutoCAD 2019 ObjectARX トレーニング マテリアル をダウンロード</strong></a></span></p>
<p><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d287e87e970c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d287e888970c img-responsive"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3943385200d-pi"><img alt="Objectarx_training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3943385200d image-full img-responsive" src="/assets/image_417071.jpg" title="Objectarx_training" /></a><br /></span></span></strong></p>
<p>autocad-2019-objectarx-training.zip を展開すると、次の 3 つのファイルとフォルダが展開されます。</p>
<ul>
<li><strong>ObjectARX Guide.chm ファイル</strong><br />ObjectARX ガイドブック と ObjectARX トレーニング の 2つのパートに分割して記述された CHM 形式のトレーニング マテリアルです。ObjectARX トレーニング配下の基礎編 Lesson 1 ～ Lesson 8 と応用編 Lesson 9 ～ Lesson 14 に沿って ObjectARX ガイドブックの内容をトレースすることが出来ます。<br /><br /></li>
<li><strong>AutoCAD 2019 ObjectARX Training.pdf ファイル</strong><br />ObjectARX Guide.chm ファイルを補完する PowerPoint プレゼンテーションの PDF 版です。<br /><br /></li>
<li><strong>WizardProject フォルダ</strong><br />ObjectARX トレーニング のパートの回答と ObjectARX ガイドブックに記載された一部サンプル コードを内包する Visual Studio 2017 プロジェクトが含まれます。</li>
</ul>
<hr />
<p>なお、初めて AutoCAD API を使って開発をされる場合には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener noreferrer" target="_blank">移植性の問題</a>&#0160;</strong>から、<span style="background-color: #ffff00;">カスタム オブジェクトや不変リアクタなど、ObjectARX 固有実装が必要な場合を除き、AutoCAD .NET API の利用をお勧めしています。<span style="background-color: #ffffff;">例えば、このマテリアルの例では、カスタム オブジェクト MYDICTIONARY を格納する ディクショナリを不変リアクタとして利用して、関連付けたオブジェクト（下記の例では線分）の色を変更すると、カスタム エンティティ&#0160;ASDKMYCIRCLE の色も連動して変化する実装を扱っています。これらのオブジェクトの関連付け（<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener noreferrer" target="_blank">ソフトポインタ参照</a></strong>）は図面ファイルに保存され維持されることになります。</span></span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad36f93cc200c-pi" style="display: inline;"><img alt="Persistent-reactor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad36f93cc200c image-full img-responsive" src="/assets/image_90803.jpg" title="Persistent-reactor" /></a></p>
<p>このような特殊な実装が不要なら、AutoCAD .NET API の使用が最適です。パレット インタフェースやダイアログなどのユーザ インタフェース実装やメモリ管理、UNICODE 文字列の処理など、さまざまな場面で .NET Framework の利点を享受していただくことが出来るためです。</p>
<hr />
<p>トレーニング マテリアルのダウンロード後、ObjectARX Guide.chm ファイルを開いてもコンテンツが空白で何も表示されない場合には、エクスプローラ上でマウス右クリックで .chm ファイルのプロパティを表示させて、「許可する」または「ブロックの解除」にチェックを入れてみてください。ブロックされたコンテンツが表示されるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39610a4200d-pi" style="display: inline;"><img alt="Disable_block" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39610a4200d image-full img-responsive" src="/assets/image_779146.jpg" title="Disable_block" /></a></p>
<p>By Toshiaki Isezaki&#0160;</p>

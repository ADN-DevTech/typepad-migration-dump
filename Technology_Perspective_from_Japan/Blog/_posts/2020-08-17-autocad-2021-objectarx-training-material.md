---
layout: "post"
title: "AutoCAD 2021 ObjectARX トレーニング マテリアル"
date: "2020-08-17 21:29:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/08/autocad-2021-objectarx-training-material.html "
typepad_basename: "autocad-2021-objectarx-training-material"
typepad_status: "Publish"
---

<div class="entry-content">
<div class="entry-body">
<p>先日の AutoCAD 2021 用 <strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/autocad-2021-dotnet_api-training-materials.html" rel="noopener noreferrer" target="_blank">AutoCAD .NET API トレーニング マテリアル</a></strong>に続いて、ObjectARX のトレーニング マテリアルを公開します。</p>
<ul>
<li>本マテリアルは、<a href="http://www.autodesk.co.jp/developautocad" rel="noopener noreferrer" target="_blank">http://www.autodesk.co.jp/developautocad</a>&#0160;で公開している&#0160;<a href="https://github.com/ADN-DevTech/ObjectARX-Wizards/raw/ForAutoCAD2021/ObjectARXWizardsInstaller/ObjectARXWizard2021.zip">ObjectARX 2021 Wizard</a>&#0160;を利用しています。</li>
<li>&quot;C:\ObjectARX 2021” に ObjectARX 2021 SDK が、&quot;C:\Program Files\Autodesk\AutoCAD 2021&quot; に AutoCAD 2021 がインストールされている環境でサンプル プロジェクトを作成、同梱していますので、これ以外のパスで ObjectARX 2021 SDK と AutoCAD 2021 をインストールされている環境では、Visual Studio 2019 version 16 以降のバージョンで正しくプロジェクトを開けなかったり、ビルドが出来なかったりする可能性があります。ご注意ください。</li>
</ul>
<hr />
<p><strong>AutoCAD 2021 ObjectARX トレーニング マテリアル</strong></p>
<p>AutoCAD 2021 ObjectARX トレーニング マテリアルは、次のリンクからダウンロードすることが出来ます。&#0160;</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/AutoCAD%202021%20ObjectARX%20Training.zip"><span class="asset  asset-generic at-xid-6a0167607c2431970b026bde8bde55200c img-responsive"><strong>AutoCAD 2021 ObjectARX トレーニング マテリアル</strong> をダウンロード</span></a></p>
<p><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d287e87e970c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d287e888970c img-responsive"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95e408d200b-pi" style="display: inline;"><img alt="Materials" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e95e408d200b image-full img-responsive" src="/assets/image_681807.jpg" title="Materials" /></a><br /></span></span></strong></p>
<p>AutoCAD 2021 ObjectARX Training.zip を展開すると、AutoCAD 2021 ObjectARX Training フォルダ下に次の 3 つのファイルとフォルダが展開されます。</p>
<ul>
<li><strong>ObjectARX Guide.chm ファイル</strong><br />ObjectARX ガイドブック と ObjectARX トレーニング の 2つのパートに分割して記述された CHM 形式のトレーニング マテリアルです。ObjectARX トレーニング配下の基礎編 Lesson 1 ～ Lesson 8 と応用編 Lesson 9 ～ Lesson 14 に沿って ObjectARX ガイドブックの内容をトレースすることが出来ます。<br /><br /></li>
<li><strong>AutoCAD 2021 ObjectARX Training.pdf ファイル</strong><br />ObjectARX Guide.chm ファイルを補完する PowerPoint プレゼンテーションの PDF 版です。<br /><br /></li>
<li><strong>WizardProject フォルダ</strong><br />ObjectARX トレーニング のパートの回答と ObjectARX ガイドブックに記載された一部サンプル コードを内包する Visual Studio 2019 プロジェクトが含まれます。</li>
</ul>
<hr />
<p>なお、初めて AutoCAD API を使って開発をされる場合には、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener noreferrer" target="_blank">移植性の問題</a>&#0160;</strong>から、カスタム オブジェクトや不変リアクタなど、ObjectARX 固有実装が必要な場合を除き、AutoCAD .NET API の利用をお勧めしています。例えば、このマテリアルの例では、カスタム オブジェクト MYDICTIONARY を格納する ディクショナリを不変リアクタとして利用して、関連付けたオブジェクト（下記の例では線分）の色を変更すると、カスタム エンティティ&#0160;ASDKMYCIRCLE の色も連動して変化する実装を扱っています。これらのオブジェクトの関連付け（<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener noreferrer" target="_blank">ソフトポインタ参照</a></strong>）は図面ファイルに保存され維持されることになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be40a5e7d200d-pi" style="display: inline;"><img alt="Persistene-reactor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be40a5e7d200d image-full img-responsive" src="/assets/image_42221.jpg" title="Persistene-reactor" /></a></p>
<p>このような特殊な実装が不要なら、AutoCAD .NET API の使用が最適です。パレット インタフェースやダイアログなどのユーザ インタフェース実装やメモリ管理、UNICODE 文字列の処理など、さまざまな場面で .NET Framework の利点を享受していただくことが出来るためです。</p>
<hr />
<p>トレーニング マテリアルのダウンロード後、ObjectARX Guide.chm ファイルを開いてもコンテンツが空白で何も表示されない場合には、エクスプローラ上でマウス右クリックで .chm ファイルのプロパティを表示させて、「許可する」または「ブロックの解除」にチェックを入れてみてください。ブロックされたコンテンツが表示されるはずです。</p>
<p>By Toshiaki Isezaki&#0160;</p>
</div>
</div>

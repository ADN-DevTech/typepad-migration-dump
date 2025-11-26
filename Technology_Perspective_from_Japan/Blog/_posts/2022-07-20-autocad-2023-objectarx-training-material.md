---
layout: "post"
title: "AutoCAD 2023 ObjectARX トレーニング マテリアル"
date: "2022-07-20 00:29:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/07/autocad-2023-objectarx-training-material.html "
typepad_basename: "autocad-2023-objectarx-training-material"
typepad_status: "Publish"
---

<p>AutoCAD 2023 用のObjectARX のトレーニング マテリアルを公開します。</p>
<ul>
<li>本マテリアルは、<a href="https://www.autodesk.co.jp/developer-network/platform-technologies/autocad" rel="noopener" target="_blank">https://www.autodesk.co.jp/developer-network/platform-technologies/autocad</a>&#0160;で公開している&#0160;<a href="https://github.com/ADN-DevTech/ObjectARX-Wizards/raw/ForAutoCAD2023/ObjectARXWizardsInstaller/ObjectARXWizards-2023.zip">ObjectARX 2023 Wizard</a>&#0160;を利用しています。</li>
<li>&quot;C:\ObjectARX 2023” に ObjectARX 2023 SDK が、&quot;C:\Program Files\Autodesk\AutoCAD 2023&quot; に AutoCAD 2023 がインストールされている環境でサンプル プロジェクトを作成、同梱していますので、これ以外のパスで ObjectARX 2023 SDK と AutoCAD 2023 をインストールされている環境では、Visual Studio 2019 version 16.11.5 バージョンで正しく同プロジェクトを開けなかったり、ビルドが出来なかったりする可能性があります。ご注意ください。</li>
</ul>
<hr />
<p><strong>AutoCAD 2023 ObjectARX トレーニング マテリアル</strong></p>
<p>AutoCAD 2023 ObjectARX トレーニング マテリアルは、次のリンクからダウンロードすることが出来ます。&#0160;</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0282e15616ef200b img-responsive"><a href="https://adndevblog.typepad.com/files/autocad-2023-objectarx-training.zip"><strong>AutoCAD 2023 ObjectARX トレーニング マテリアル</strong>をダウンロード</a></span></p>
<p><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d287e87e970c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d287e888970c img-responsive"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fab4450200c-pi" style="display: inline;"><img alt="Oarx_training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fab4450200c image-full img-responsive" src="/assets/image_776099.jpg" title="Oarx_training" /></a><br /></span></span></strong></p>
<p>autocad-2023-objectarx-training.zip を展開すると、AutoCAD 2023 ObjectARX Training フォルダの下に 3 つのファイルとフォルダが展開されます。</p>
<ul>
<li><strong>ObjectARX Guide.chm ファイル</strong><br />ObjectARX ガイドブック と ObjectARX トレーニング の 2つのパートに分割して記述された CHM 形式のトレーニング マテリアルです。ObjectARX トレーニング配下の基礎編 Lesson 1 ～ Lesson 8 と応用編 Lesson 9 ～ Lesson 14 に沿って ObjectARX ガイドブックの内容をトレースすることが出来ます。<br /><br />
<ul>
<li>基礎編
<ul>
<li>Lesson 1 プロジェクトとコマンドの作成</li>
<li>Lesson 2 固定座標でモデル空間に円を追加</li>
<li>Lesson 3 ユーザ対話でモデル空間に円を追加</li>
<li>Lesson 4 新しい画層を作成して 選択した円の画層に設定</li>
<li>Lesson 5 イテレタを使った図面調査</li>
<li>Lesson 6 MFC ダイアログとドキュメント ロック</li>
<li>Lesson 7 データベース リアクタとドキュメント毎データ</li>
<li>Lesson 8 カスタム オブジェクト</li>
</ul>
</li>
<li>応用編
<ul>
<li>Lesson 9 外部図面アクセス</li>
<li>Lesson 10 外部図面への図形書き出し</li>
<li>Lesson 11 カスタム エンティティの作成</li>
<li>Lesson 12 カスタム ディクショナリの作成</li>
<li>Lesson 13 ディクショナリ イテレタ</li>
<li>Lesson 14 不変リアクタ<br /><br /></li>
</ul>
</li>
</ul>
</li>
<li><strong>AutoCAD 2023 ObjectARX Training.pdf ファイル</strong><br />ObjectARX Guide.chm ファイルを補完する PowerPoint プレゼンテーションの PDF 版です。<br /><br /></li>
<li><strong>WizardProject フォルダ</strong><br />ObjectARX トレーニング のパートの回答コードを内包する Visual Studio 2019 プロジェクトが含まれます。</li>
</ul>
<hr />
<p>なお、初めて AutoCAD API を使って開発をされる場合には、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener noreferrer" target="_blank">移植性の問題</a>&#0160;</strong>から、カスタム オブジェクトや不変リアクタなど、ObjectARX 固有実装が必要な場合を除き、AutoCAD .NET API の利用をお勧めしています。例えば、このマテリアルの例では、カスタム オブジェクト MYDICTIONARY を格納する ディクショナリを不変リアクタとして利用して、関連付けたオブジェクト（下記の例では上部の図形）の色を変更すると、カスタム エンティティ ASDKMYCIRCLE の色も連動して変化する実装を扱っています。これらのオブジェクトの関連付け（<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener noreferrer" target="_blank">ソフトポインタ参照</a></strong>）は図面ファイルに保存され維持されることになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807d9307200d-pi" style="display: inline;"><img alt="Persistene-reactor" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807d9307200d image-full img-responsive" src="/assets/image_691868.jpg" title="Persistene-reactor" /></a></p>
<p>このような特殊な実装が不要なら、AutoCAD .NET API の使用が最適です。パレット インタフェースやダイアログなどのユーザ インタフェース実装やメモリ管理、UNICODE 文字列の処理など、さまざまな場面で .NET Framework の利点を享受していただくことが出来るためです。</p>
<hr />
<p>トレーニング マテリアルのダウンロード後、ObjectARX Guide.chm ファイルを開いてもコンテンツが空白で何も表示されない場合には、エクスプローラ上でマウス右クリックで .chm ファイルのプロパティを表示させて、「許可する」にチェックを入れてみてください。ブロックされたコンテンツが表示されるはずです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fab4593200c-pi" style="display: inline;"><img alt="Accept_chm" class="asset  asset-image at-xid-6a0167607c2431970b02942fab4593200c img-responsive" src="/assets/image_221392.jpg" style="width: 350px;" title="Accept_chm" /></a></p>
<p>By Toshiaki Isezaki&#0160;</p>

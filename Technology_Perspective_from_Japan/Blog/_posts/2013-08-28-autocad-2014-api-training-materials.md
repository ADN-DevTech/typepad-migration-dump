---
layout: "post"
title: "AutoCAD 2014 API トレーニング マテリアル"
date: "2013-08-28 00:29:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/08/autocad-2014-api-training-materials.html "
typepad_basename: "autocad-2014-api-training-materials"
typepad_status: "Publish"
---

<p>AutoCAD 2014 用にチューニングした AutoCAD .NET API と ObjectARX のトレーニング マテリアルを公開しました。どちらも、<a href="http://www.autodesk.co.jp/developautocad" target="_blank">http://www.autodesk.co.jp/developautocad</a>&#0160;で公開している <a href="http://images.autodesk.com/adsk/files/AutoCAD_2014_dotnet_wizards.zip">AutoCAD 2014 DotNet Wizards</a>&#0160;と&#0160;<a href="http://images.autodesk.com/adsk/files/ObjectARXWizards-2014.zip">ObjectARX 2014 Wizard</a>&#0160;を利用したものになっていますので、これから習得を目指す方は、こちらの利用をお勧めします。</p>
<p>それぞれ、Autodesk Knowledge Network サイト(<a href="https://knowledge.autodesk.com/ja/" target="_blank">https://knowledge.autodesk.com/ja/</a>) の次のリンクから直接入手していただくことが出来ます。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0BC.html" target="_blank">AutoCAD 2014 </a></strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0BC.html" target="_blank"><strong>の</strong><strong> .NET API </strong></a><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0BC.html" target="_blank">を学習するためのリソースはありますか？</a></strong></p>
<p style="padding-left: 30px;"><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u08n.html" target="_blank"><strong>AutoCAD 2014 </strong><strong>の</strong><strong> ObjectARX </strong><strong>を学習するためのリソースはありますか？</strong></a><strong>&#0160;&#0160;</strong></p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901ecd29de970b-pi" style="display: inline;"><img alt="Images" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01901ecd29de970b image-full" src="/assets/image_317299.jpg" title="Images" /></a></p>
<p>既に、AutoCAD 2013 用に .NET API と ObjectARX のトレーニングマテリアルを公開済ですが、下記の部分を少し修正、追記をしています。</p>
<p><strong>AutoCAD 2014&#0160;</strong><strong>.NET API トレーニング マテリアルの変更点</strong></p>
<ul>
<li>コマンド実行コンテキストの項を追加しました。VBA を .NET API に移行してCOM を利用される方が増えていますが、VBA と異なり、.NET API ではマクロではなく、コマンドを定義します。この時、遭遇する問題がコマンド実行コンテキストです。具体的には、プログラムを使って図面を開く操作を実装した場合、アプリケーション実行コンテキストでコマンドを定義せずに処理をすすめてしまうと、さまざまな問題に遭遇することになります。ここでは、ドキュメント実行コンテキスト内からアプリケーション実行コンテキストを利用して図面を開くコードを紹介しています。<br /><br />なお、COM と .NET API を混在された実装でコマンド定義される場合は、.NET API 側に実装内容を統一されたうえで、コマンド実行コンテキストを明確意識して使用されることをお勧めします。<a href="http://adndevblog.typepad.com/technology_perspective/2013/05/command_and_system_variable.html" target="_blank">こちらの記事&#0160;</a>もあわせてご参照ください。</li>
</ul>
<p><strong><strong>AutoCAD 2014 ObjectARX</strong><strong>&#0160;トレーニング マテリアルの変更点</strong><br /></strong></p>
<ul>
<li>ObjectARX Wizard for AutoCAD 2013(ObjectARX 2013 Wizard) の日本語版 Visual Studio 2010 上での不備があったため、AutoCAD 2013 ObjectARX トレーニングマテリアル内では、ObjectARX Wizard for AutoCAD 2012(ObjectARX 2012 Wizard) を利用していました。この影響で、プロジェクト作成後には、AutoCAD 2013 に<strong>&#0160;</strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u02U.html" target="_blank"><strong>手動でプロジェクト設定を施す必要</strong>&#0160;</a>がありましたが、今回は、初めから AutoCAD 2014 用の&#0160;ObjectARX Wizard for AutoCAD 2014(ObjectARX 2014 Wizard) を利用しているので、プロジェクトの手動調整は必要ありません。</li>
</ul>
<ul>
<li>ObjectARX 2014 Wizard には、Visual Studio 内にツールバーとして表示される ObjectARX Addin が廃止されています。このため、主に新しいクラスを派生した際にメンバ関数のオーバーライド指示で使用していた<strong>&#0160;Autodesk Class Explorer</strong> を使った箇所を変更しています。具体的には、Autodesk Class Explorer 上の操作でおこなっていたメンバ関数の実装を、マテリアルからのコピー&amp;ペーストで補うように修正を加えてあります。リアクタの実装やカスタム オブジェクト定義が影響する部分です。</li>
</ul>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>

---
layout: "post"
title: "Visual Studio Code での Fusion 360 Python 開発"
date: "2020-05-11 00:02:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/05/python-development-for-fusion-360-using-vs-code.html "
typepad_basename: "python-development-for-fusion-360-using-vs-code"
typepad_status: "Publish"
---

<p>オートデスク製品の <strong><a href="https://azure.microsoft.com/ja-jp/products/visual-studio-code/" rel="noopener" target="_blank">Visual Studio Code</a></strong>、別名、VS Code の利用が広がっています。VS Code は、Microsoft 社が<a href="https://github.com/microsoft/vscode" rel="noopener" target="_blank"><strong>オープン ソース</strong></a>として<strong>無償</strong>で公開している統合開発環境です。</p>
<p>もっとも最近では、AutoCAD 2021 のリリースとともに、従来の VisualLISP エディタに替わって AutoLISP の開発環境としても<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html" rel="noopener" target="_blank">採用</a></strong>されています。このブログでは Fusion 360 API について、あまり触れていませんが、実は、Fusion 360 の Python 開発にも VS Code が<strong><a href="http://help.autodesk.com/view/fusion360/ENU/?guid=GUID-743C88FB-CA3F-44B0-B0B9-FCC378D0D782#Editing" rel="noopener" target="_blank">採用</a></strong>されています。</p>
<p>もともと、Fusion 360 API を用いたスクリプト、または、アドイン開発には、C++、Python、JavaScript が<a href="https://adndevblog.typepad.com/technology_perspective/2015/11/fusion-360-api-choose-development-language.html" rel="noopener" target="_blank">サポート</a>されていました。その後、Fusion 360 の外部プロセスで動作ししていた JavaScript のみ、<a href="https://adndevblog.typepad.com/technology_perspective/2018/03/about-javascript-support-on-fusion-360-api.html" rel="noopener" target="_blank">メインテナンスモードに移行</a>して、現在では C++ と Python がサポートされています。このうち、ビルドが不要で、Windows と Mac の両プラットフォームでそのまま利用出来る Python が人気を博しています。当初、Python 開発には、オープンソースの <strong><a href="https://www.spyder-ide.org/" rel="noopener" target="_blank">Spyder</a></strong> が採用されていましたが、VS Code がこれに替わった経緯となります。</p>
<p>VS Code で Fusion 360 用の Python プログラムをで編集、デバッグするのは至って簡単です。</p>
<ol>
<li>VS Code を<a href="https://code.visualstudio.com/download" rel="noopener" target="_blank">ダウンロード ページ</a>からダウンロード、インストールしてください。</li>
<li>次に、Fusion 360 を起動して、[デザイン] ワークスペースの [ツール] リボンタブから [アドイン] パネルの [スクリプトとアドイン] をクリックして、任意のサンプルスクリプトを選んで [編集] ボタンをクリックしてみてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec16cd2d200c-pi" style="display: inline;"><img alt="Scripit_and_addin" class="asset  asset-image at-xid-6a0167607c2431970b0263ec16cd2d200c img-responsive" src="/assets/image_641082.jpg" title="Scripit_and_addin" /></a></li>
<li>VS Code で Python の編集とデバッグを可能にする、<span style="text-decoration: underline;">最新バージョンの</span>&#0160; <strong><a href="https://marketplace.visualstudio.com/items?itemName=ms-python.python" rel="noopener" target="_blank">Microsoft&#0160;Python Extension</a></strong>（拡張機能）が自動的にインストールされます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec16cd97200c-pi" style="display: inline;"><img alt="Python_extension_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec16cd97200c image-full img-responsive" src="/assets/image_344569.jpg" title="Python_extension_install" /></a></li>
<li>VS Code 上に選択したサンプルスクリプトが表示され（読み込まれ）、VS Code 上で F5 キーを押下、あるいは、[実行] &gt; [デバッグの開始] メニューをクリックすると、スクリプトが実行されて、Fusion 360 に反映されるはずです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9451980200b-pi" style="display: inline;"><img alt="Debug" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9451980200b image-full img-responsive" src="/assets/image_98602.jpg" title="Debug" /></a></li>
</ol>
<p>もし、Fusion 360 の [スクリプトとアドイン] ダイアログで [編集] ボタンで VS Code が起動しない場合は、Fusion 360 を再インストールしてみてください。</p>
<p>Fusion 360 で作成したモデルや<strong><a href="https://forge-fusion360-meetup.herokuapp.com/" rel="noopener" target="_blank">アニメーション</a></strong>なども Forge Viewer で<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/09/fusion-360-animation-play-on-forge-viewer.html" rel="noopener" target="_blank">２次利用出来ます</a></strong>。VS Code で Forge 開発を効率化する点についても、後日、このブログでご案内する予定です。</p>
<p>By Toshiaki Isezaki</p>

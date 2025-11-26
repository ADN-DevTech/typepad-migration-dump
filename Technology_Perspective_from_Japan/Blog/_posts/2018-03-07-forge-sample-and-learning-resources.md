---
layout: "post"
title: "Forge サンプルと学習リソース"
date: "2018-03-07 00:14:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/forge-sample-and-learning-resources.html "
typepad_basename: "forge-sample-and-learning-resources"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e06731970c-pi" style="float: right;"><img alt="Forge Sample" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e06731970c img-responsive" src="/assets/image_908808.jpg" style="margin: 0px 0px 5px 5px;" title="Forge Sample" /></a>過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/various-forge-samples.html" rel="noopener noreferrer" target="_blank">多様な Forge サンプル</a></strong> でもご案内したとおり、Forge には多様なサンプルが用意されていて、同時に、開発時に参照いただけるソースコーも GitHub で公開されています。Forge に初めて触れる方には有用かと思いますが、先に触れたブログ記事の記載時期から時間も経過していて、記事内でリンクしている&#0160;<strong><a href="https://autodesk-forge.github.io/" rel="noopener noreferrer" target="_blank">https://autodesk-forge.github.io/</a></strong>&#0160;も<strong><a href="https://developer.autodesk.com/" rel="noopener noreferrer" target="_blank"> デベロッパ ポータル</a></strong>&#0160;から直接アクセス出来なくなっています。そこで、改めて、サンプルや SDK、Quickstarts の参照方法について触れておきたいと思います。</p>
<p>サンプルへのアクセス ポイントは、他の学習リソース同様、従来通り、<strong><a href="https://developer.autodesk.com/" rel="noopener noreferrer" target="_blank">デベロッパ ポータル</a></strong>&#0160;にあります。[Documentation] メニューから&#0160; [Examples] をクリックしてみてください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c955c526970b-pi" style="display: inline;"><img alt="Forge_samples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c955c526970b image-full img-responsive" src="/assets/image_990454.jpg" title="Forge_samples" /></a></p>
<p>Learn by Examples ページが開いて、多数のサンプル プログラム一覧がボックス状に表示されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09f90e6d970d-pi" style="display: inline;"><img alt="Examples_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09f90e6d970d image-full img-responsive" src="/assets/image_695020.jpg" title="Examples_page" /></a></p>
<p>多数のサンプルが表示されるので、ページ上部にあるコンボボックスを使って、参照したい開発言語や環境、サンプルで使われている Forge Platform API をフィルタ表示させることも出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09f90fa6970d-pi" style="display: inline;"><img alt="Filter_for_examples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09f90fa6970d image-full img-responsive" src="/assets/image_362869.jpg" title="Filter_for_examples" /></a></p>
<p>各ボックスをクリックすると、ソースコードを記載する GitHub リポジトリへジャンプするはずです。あとは、各リポジトリ ページのガイダンス（英語）に従って、ローカルに各サンプルの実行環境を作成してテストすることが可能です。</p>
<p>なお、この一覧の中には、純粋なサンプルではなく、Forge 開発に必要な RESTful 呼び出しなどをラップする、通称、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/forge-sdk.html" rel="noopener noreferrer" target="_blank">Forge SDK</a></strong> も記載されています。もちろん、記載されている SDK ボックスをクリックすれば、GitHub リポジトリへジャンプしてソースコードを参照したり、SDK の利用方法を把握したりすることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09f90e91970d-pi" style="display: inline;"><img alt="Forge_sdks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09f90e91970d image-full img-responsive" src="/assets/image_194081.jpg" title="Forge_sdks" /></a></p>
<p>紛らわしいのですが、<strong><a href="https://developer.autodesk.com/" rel="noopener noreferrer" target="_blank">デベロッパ ポータル</a></strong>&#0160;の [Documentation] メニューには、あたかも Forge SDK のみを表示する [SDK] リンクが用意されていますが、このリンクで表示されるのは、Forge SDK を使ってはじめの一歩を実践するためのサンプル、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/forge-quick-start.html" rel="noopener noreferrer" target="_blank">Quickstarts</a></strong>&#0160;です。実際に SDK が記載されているのは、前述の通り、[Examples] リンクから表示可能な Learn by Examples ページです。ご注意ください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09f90eab970d-pi" style="display: inline;"><img alt="Forge_sdk_link" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09f90eab970d image-full img-responsive" src="/assets/image_173713.jpg" title="Forge_sdk_link" /></a></p>
<p>Learn by Examples ページ、Quickstarts ページに記載されている内容 のいくつかは、このブログで日本語でご案内していますので、サンプル タイトルなどとともに Web 検索していただくといいかもしれません。Forge 1 Day Workshop でもご紹介している&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part1.html" rel="noopener noreferrer" target="_blank">新しい Forge Viewer チュートリアル改定版 ～ その1</a></strong>、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part2.html" rel="noopener noreferrer" target="_blank">新しい Forge Viewer チュートリアル改定版 ～ その2</a></strong>、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/05/forge-nodejs-quick-start-part1.html" rel="noopener noreferrer" target="_blank">Forge Node.js クイックスタート ～ その1</a></strong>、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/05/forge-nodejs-quick-start-part2.html" rel="noopener noreferrer" target="_blank">Forge Node.js クイックスタート ～ その2</a></strong> などがその例です。</p>
<p>&#0160;By Toshiaki Isezaki</p>

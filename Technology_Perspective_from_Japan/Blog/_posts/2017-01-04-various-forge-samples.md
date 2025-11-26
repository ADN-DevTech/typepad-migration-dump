---
layout: "post"
title: "多様な Forge サンプル"
date: "2017-01-04 01:18:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/01/various-forge-samples.html "
typepad_basename: "various-forge-samples"
typepad_status: "Publish"
---

<p>Forge には、既に数多くのサンプル コードが作成されていて、GitHub 上でソースコードが公開されています。従来、少々面倒だったのは、直接 GitHub にアクセスして各リポジトリ ページの内容を見てみないと、使用されている API や用途、実現している機能を把握する出来なかった点です。</p>
<p>今回、<strong><a href="https://developer.autodesk.com/" rel="noopener noreferrer" target="_blank">デベロッパ ポータル</a></strong>&#0160;からアクセス可能なサンプル一覧ページが用意されて、各種サンプルの概要を簡単に把握することが出来るようになりました。サンプル一覧ページには、<strong><a href="https://autodesk-forge.github.io/" rel="noopener noreferrer" target="_blank">https://autodesk-forge.github.io/</a></strong>&#0160;から直接アクセスすることも出来ます。ページ上部には、使用している Forge Platform API や使用言語でフィルタする機能も用意されているので、短い時間で求めているサンプルを見つけることが出来るはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0964983d970d-pi" style="display: inline;"><img alt="Samples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0964983d970d image-full img-responsive" src="/assets/image_649390.jpg" title="Samples" /></a></p>
<p>いくつかのサンプルには、Heroku などにホストされて、すぐに実行中のサンプルをテストすることも出来る Live Demo リンクが用意されています。ここでは、Live Demo リンクが用意されている代表的なサンプルをご紹介しておきたいと思います。それぞれ、Forge が持つ機能を把握する手助けとなりますので、状況にあわせた Forge のデモで利用することが出来るはずです。</p>
<hr />
<p><strong>forge-rcdb.nodejs</strong></p>
<p style="padding-left: 30px;">Responsive Connected Database と名付けられたサンプルで、Forge Viewer でストリーミング配信された形状データとメタデータ（属性、プロパティ）とは別に、それらにリンクされた外部データベースの情報を Forge Viewer 内のプロパティ ウィンドウに表示します。すぐに試すことが出来る Live Demo の URL は、<strong><a href="https://forge-rcdb.autodesk.io/" rel="nofollow">https://forge-rcdb.autodesk.io/</a></strong>&#0160;です。</p>
<p style="padding-left: 30px;">画面右手の Database 領域に表示されているのが、MongoDB を使った外部データベースの情報です。Material 列を選択すると、該当する 3D モデルが Forge Viewer 内にフォーカスされてきます。この時、Viewer 内でプロパティ ウィンドウを開いていると、もともと、デザイン データを持っているメタデータに加えて、MongoDB 上の関係データを表示します。この際に、Supplier や Price、Currency の値を変更すると、Viewer 上のプロパティ ウィンドウに表示中のデータもリアルタイムに更新されることが分かります。Forge Viewer が、他のデータソースと連携できる例と言えます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c3a6b1970b-pi" style="display: inline;"><img alt="Forge-rcdb.nodejs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c3a6b1970b image-full img-responsive" src="/assets/image_247207.jpg" title="Forge-rcdb.nodejs" /></a></p>
<hr />
<p><strong>model.derivative-nodejs-sample</strong></p>
<p style="padding-left: 30px;">3-legged 認証を使って A360 のユーザ ストレージ領域にアクセスして、アップロード済のデザイン ファイルを画面右手に設定した Forge Viewer 領域に表示するサンプルです。Live Demo の URL は、<strong><a href="https://derivatives.autodesk.io/" rel="noopener noreferrer" target="_blank">https://derivatives.autodesk.io/</a></strong> です。</p>
<p style="padding-left: 30px;">Viewer 表示用だけでなく、Model Derivative API が持つ、他のデザイン ファイル形式への変換とダウンロードもテストすることが出来ます。また、デザイン ファイルの各バージョンを表示させることが出来るだけでなく、2016 年 11 月に追加されたデザイン データの Version へのアタッチメント機能もテストすることが出来ます。Sign In するユーザ アカウントにもよりますが、A360 Team、Fusion Team、BIM 360 Team や BIM 360 Docs のストレージと比較することで、3rd Party アプリも同じデータにアクセス出来ることを紹介することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0966919b970d-pi" style="display: inline;"><img alt="Model.derivative-nodejs-sample" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0966919b970d image-full img-responsive" src="/assets/image_510411.jpg" title="Model.derivative-nodejs-sample" /></a></p>
<hr />
<p><strong>model.derivative-nodejs-google.drive.viewer</strong></p>
<p style="padding-left: 30px;">パブリック ストレージ サービスである <strong><a href="https://www.google.com/intl/ja_ALL/drive/" rel="noopener noreferrer" target="_blank">Google Drive</a></strong> 内のデザイン データにアクセスして、Forge Viewer に表示するサンプルです。Live Demo の URL は、<strong><a href="https://forgegoogledriveviewer.herokuapp.com/" rel="noopener noreferrer" target="_blank">https://forgegoogledriveviewer.herokuapp.com/</a></strong>&#0160;です。</p>
<p style="padding-left: 30px;">Google アカウントで Google Drive サインインすることで、Google Drive 側のフォルダ構造を表示します。この中からデザイン ファイルをクリックすると、Model Derivative API が表示用に Forge の Object Storage Service（OSS）にデザイン ファイルを転送して変換処理を開始し、画面右側に設定された Forge Viewer 領域にモデルを表示します。デザイン ファイルが、必ずしもオートデスクのクラウド サービスのストレージになくとも、<strong><a href="https://ja.wikipedia.org/wiki/OAuth" rel="noopener noreferrer" target="_blank">OAuth2</a></strong> を利用して透過的にアクセス出来る例とも言えます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d24d7760970c-pi" style="display: inline;"><img alt="Model.derivative-nodejs-google.drive.viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d24d7760970c image-full img-responsive" src="/assets/image_969394.jpg" title="Model.derivative-nodejs-google.drive.viewer" /></a></p>
<p style="padding-left: 30px;">類似したサンプルには、<strong><a href="https://www.box.com/ja-jp/home" rel="noopener noreferrer" target="_blank">BOX ストレージ サービス</a></strong>上のデザイン データにアクセスして Forge Viewer に表示する&#0160;<strong>model.derivative-nodejs-box.viewer</strong> サンプルや、同じく BOX ストレージと オートデスクのクラウドサービス側のストレージ間でデザイン ファイルにアクセスして相互に転送する&#0160;<strong>data.management-nodejs-integration.box</strong> サンプルも提供されています。</p>
<hr />
<p>Live Demo を利用すれば、GitHub リポジトリ上のプロジェクトをローカル コンピュータにクローンする必要がないので、手軽にテストやデモに利用することが出来ます。もちろん、Node.js などの開発環境を用意する必要もありません。ぜひ、お試しください。</p>
<p>By Toshiaki Isezaki</p>

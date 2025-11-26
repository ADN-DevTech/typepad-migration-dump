---
layout: "post"
title: "Data Visualization :リファレンス アプリのローカル実行"
date: "2021-04-28 00:04:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/data-viz-ref-app-local-run.html "
typepad_basename: "data-viz-ref-app-local-run"
typepad_status: "Publish"
---

<p>AU 2020 の <a href="https://www.autodesk.com/autodesk-university/ja/forge-content/au_class-urn:adsk.content:content:93829aaa-a58c-4c44-bcdb-f6335220a4e6" rel="noopener" target="_blank">Forge ロードマップ： Visual Insights-モデル内のデータの視覚化</a> や&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2021/02/forge-online-viewer-update.html" rel="noopener" target="_blank">Forge Online - Viewer アップデート</a> の「デジタルツインの取り組みと Data Visualization エクステンション」でご案内していた Forge Viewer でセンサー情報を可視化してデジタルツインを実現する <strong>Data Visualization エクステンション </strong>が Public Beta となりました。</p>
<p><a class="asset-img-link" href="https://hyperion.autodesk.io/" rel="noopener" target="_blank"><img alt="Hyperion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278801d9288200d image-full img-responsive" src="/assets/image_963271.jpg" title="Hyperion" /></a></p>
<p>本来、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/" rel="noopener" target="_blank">Forge Viewer エクステンション</a>としての導入予定だったのですが、ボリュームの大きさとデジタルツインの可視化ソリューションとしての確立を目的に、ドキュメントも Forge Viewer から独立した記載となっています。また、同時に、GitHub 上で Data Visualization エクステンションを使ったリファレンス アプリが公開が開始されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99b91c7200b-pi" style="display: inline;"><img alt="Data-viz-document" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99b91c7200b image-full img-responsive" src="/assets/image_489015.jpg" title="Data-viz-document" /></a></p>
<p>リファレンス アプリの詳細については<a href="https://forge.autodesk.com/en/docs/dataviz/v1/developers_guide/introduction/" rel="noopener" target="_blank"> Developer&#39;s Guide</a> に譲りますが、ドキュメントが Mac 環境をベースに「ターミナル」での操作を前提に記述されているので、補足として、Windows のローカル環境でリファレンス アプリを実行する手順をご紹介しておきたいと思います。</p>
<ul>
<li>ターミナルでは コマンド プロンプト（旧名 DOS コマンド）ではなく、Linux コマンドを使用します。例えば、現在のフォルダ（カレントディレクトリ）を変更する cd コマンドは共通ですが、パス指定時の区切りは \ （バックスラッシュ、日本では￥）ではなく、/ スラッシュとなります。また、フォルダ内一覧表示は dir コマンドではなく、ls コマンドを使用します。画面履歴の消去にはは cls コマンドではなく、clear コマンド を使用します。</li>
<li>プロンプト表示は「ユーザ名@マシン名 プラットフォーム名」となります。</li>
</ul>
<hr />
<p><strong>準備</strong></p>
<p style="padding-left: 40px;">Data Visualization リファレンス アプリの実行には、Version 12 以上の<strong> Node.js</strong> が必要です。また、ここでは、Mac のターミナルの代替として、Windows 上で<strong> Git Bash</strong> を使用出来ます。</p>
<p style="padding-left: 40px;">Git Bash は <strong>git for Windows</strong> に含まれていて、Forge Workshop などでも利用した実績があるため、<a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">Forge の開発環境 </a>のブログ記事で導入についての情報を記載しています。もし、インストールがない場合には、同記事の Node.js と git for Windows の項目を確認の上、インストールをしてみてください。</p>
<hr />
<p><strong>手順</strong></p>
<ol>
<li>Data Visualization リファレンス アプリのリポジトリをローカル環境にクローンします。Git Bush を起動して、cd コマンドでクローンしたいフォルダに移動したら、<strong>git clone https://github.com/Autodesk-Forge/forge-dataviz-iot-reference-app.git</strong> と入力してクローンを開始します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec8df92200c-pi" style="display: inline;"><img alt="Git_clone" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec8df92200c image-full img-responsive" src="/assets/image_316478.jpg" title="Git_clone" /></a></li>
<li>forge-dataviz-iot-refer ence-app フォルダが作成され、リポジトリの内容がクローンされています。<strong>cd forge-dataviz-iot-reference-app</strong>&#0160; と入力して、現在のフォルダを forge-dataviz-iot-reference-app フォルダに移動します。</li>
<li><strong>npm install</strong> と入力して、リファレンス アプリが利用する Node.js パッケージ（ミドルウェア）をインストールします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdec8e0e8200c-pi" style="display: inline;"><img alt="Npm_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdec8e0e8200c image-full img-responsive" src="/assets/image_241380.jpg" title="Npm_install" /></a></li>
<li><strong>npm run dev</strong> と入力して、Node.js を待機状態に起動します。</li>
<li>Web ブラウザを起動して <strong>localhost:9000</strong> と入力すると、リファレンス アプリが表示されるはずです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99b96e1200b-pi" style="display: inline;"><img alt="Rlocalhost_9000" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99b96e1200b image-full img-responsive" src="/assets/image_624557.jpg" title="Rlocalhost_9000" /></a></li>
</ol>
<hr />
<p>ここまでの手順はコマンド プロンプトでも動作させることが出来ます。ただ、後続する&#0160;<a class="menu-leaf" href="https://forge.autodesk.com/en/docs/dataviz/v1/developers_guide/quickstart/replace_model/" id="ad70c654-21e4-033a-2e30-3f2e6f0d9be6" rel="noopener" target="_blank">Replacing the Default Model</a> では、一部、コマンド プロンプトで動作しない箇所があります。ご注意ください。</p>
<p><span style="background-color: #ffff00;">Data Visualization エクステンションは 2021 年 5 月 3 日に正式にリリースされました。</span></p>
<p>By Toshiaki Isezaki</p>

---
layout: "post"
title: "Design Automation API for Inventor - Visual Studio template のご紹介"
date: "2020-08-24 00:01:00"
author: "Takehiro Kato"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/08/design-automation-api-for-inventor-visual-studio-template.html "
typepad_basename: "design-automation-api-for-inventor-visual-studio-template"
typepad_status: "Publish"
---

<p>今回の記事では、Design Automation API for Inventorで使用するInventor Plug-inを開発する際に便利なVisual Studio template をご紹介いたします。</p>
<p>このテンプレートを使用することにより、Inventor Plug-inの作成に必須の処理を含んだスケルトンコードを生成することができます。</p>
<p>また、このVisual Studioテンプレートを使用することのもう一つの利点は、ローカルでのデバッグ作業を行うヘルパープロジェクトを生成してくれることにあります。</p>
<p>ご存知のように、Design Automation for Inventorを利用するにあたっては、自動化する処理を記述したInventor Plug-inを作成し、バイナリファイルとPackageContents.xmlをZip圧縮をして、AppBundleとしてアップロードをする必要があります。一方で、作成したPlug-inの動作を確認するために、クラウド上のInventorコアエンジンを用いて毎回AppBundleにアップロードするというのは少々手間がかかりすぎます。</p>
<p>このため、Design Automation for Inventorで動作を確認する前に、このVisual Studioテンプレートから生成されるヘルパープロジェクトを用いて、ローカルコンピュータで事前の動作確認をすることをお勧めいたします。</p>
<p>&#0160;</p>
<ul>
<li><span style="font-size: 12pt;">インストール方法</span></li>
</ul>
<p>インストールは、Visual Studio 2017のエディタ内から行います。[ツール]-[機能拡張と更新プログラム]を選択します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95d6f34200b-pi" style="display: inline;"><img alt="1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e95d6f34200b image-full img-responsive" src="/assets/image_210486.jpg" title="1" /></a></p>
<p>&#0160;</p>
<p>表示されたダイアログの左のリストから、オンラインを選択し、右側のパネルの検索文字列に”Forge”を入力して検索をすると、Design Automation for Invenorがリストに表示されるので、ダウンロードボタンを押してダウンロードをします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be40987b1200d-pi" style="display: inline;"><img alt="2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be40987b1200d image-full img-responsive" src="/assets/image_603743.jpg" title="2" /></a></p>
<p>ダウンロード完了後、Visual Studioを終了すると以下のようなメッセージが表示されるので、「Modify」をクリックするとインストールが開始されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde8ac122200c-pi" style="display: inline;"><img alt="3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde8ac122200c image-full img-responsive" src="/assets/image_299974.jpg" title="3" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<ul>
<li><span style="font-size: 12pt;">新規プロジェクトの作成とデバッグ実行</span></li>
</ul>
<p>インストールをしたテンプレートを使用してプロジェクトを作成するには、Visual Studioの新規プロジェクト作成時にVisual C#配下の「Design Automation for Inventor」を選択して新規にプロジェクトを作成します。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde8ac134200c-pi" style="display: inline;"><img alt="4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde8ac134200c image-full img-responsive" src="/assets/image_838579.jpg" title="4" /></a></p>
<p>&#0160;</p>
<p>Plug-in用のプロジェクトを含むソリューションが作成されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95d6fa1200b-pi" style="display: inline;"><img alt="5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e95d6fa1200b image-full img-responsive" src="/assets/image_636352.jpg" title="5" /></a></p>
<p>&#0160;</p>
<p>ソリューションをビルドすると、ソリューション作成フォルダ\Outputフォルダ配下に、Plug-inのバイナリファイルを含むZipファイルが作成されます。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4098811200d-pi" style="display: inline;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4098811200d-pi" style="display: inline;"><img alt="6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4098811200d image-full img-responsive" src="/assets/image_551352.jpg" title="6" /></a></p>
<p>&#0160;</p>
<p>また、DebugPluginLocallyプロジェクトをスタートアッププロジェクトに設定し、デバッグを開始するとInventorが起動してPlug-inの処理（RunWithArgumentsメソッド）が実行されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95d6fec200b-pi" style="display: inline;"><img alt="7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e95d6fec200b image-full img-responsive" src="/assets/image_654722.jpg" title="7" /></a></p>
<p>&#0160;</p>
<p>なお、Plug-inに引き渡す引数を追加・変更する場合は、DebugPluginLocallyプロジェクトのProgram.csファイルの以下の部分を編集します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde8ac1d5200c-pi" style="display: inline;"><img alt="9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde8ac1d5200c image-full img-responsive" src="/assets/image_637136.jpg" title="9" /></a></p>
<p>&#0160;</p>
<p>また、Interactionプロジェクトを使用することで、作成したPlug-inをAppBundleとしてForge環境へアップロードすることができます。</p>
<p>Interactionプロジェクト配下のappsettings.jsonを開き、対象となるForge ApplicationのClientIDとCleintSecretに変更し保存をします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95d702f200b-pi" style="display: inline;"><img alt="8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e95d702f200b image-full img-responsive" src="/assets/image_1559.jpg" title="8" /></a></p>
<p>&#0160;</p>
<p>Interactionプロジェクトをスタートアッププロジェクトに設定し、デバッグ実行をするとコンソールが起動し、以下のようなメニューが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde8ac299200c-pi" style="display: inline;"><img alt="10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde8ac299200c image-full img-responsive" src="/assets/image_115747.jpg" title="10" /></a></p>
<p>&#0160;</p>
<p>AppBundleの作成する場合は、0(ゼロ）を入力することで、ビルドしたAppBundleがアップロードされます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde8ac2c0200c-pi" style="display: inline;"><img alt="11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde8ac2c0200c image-full img-responsive" src="/assets/image_906192.jpg" title="11" /></a></p>
<p>&#0160;</p>
<p>なお、現在のところDesign Automation API for Inventor用のVisual Studio templateは、Visual Studio 2017環境にインストールが可能です。</p>
<p>2020/9/7追記</p>
<p><span style="text-decoration: line-through;">Visual Studio 2019環境ではインストール時にエラーとなりますのでご留意ください。</span></p>
<p>Visual Studio 2019でもインストールできることを確認しました。Visual Studio 2019でインストール時にエラーが発生する場合は、Visual Studioの最新のUpdateを適用後、PCを再起動してから再実行してみてください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
<p>&#0160;</p>

---
layout: "post"
title: "Autodesk Exchange アプリケーションストア　インストーラの仕様変更について"
date: "2014-02-19 23:27:52"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/02/autodesk-exchange-digital-signature.html "
typepad_basename: "autodesk-exchange-digital-signature"
typepad_status: "Publish"
---

<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;">Autodesk Developer Networkの工藤　暁です。今回は<a href="http://apps.exchange.autodesk.com/ja" target="_blank">Autodesk Exchange アプリケーション ストア</a>に公開されますインストーラの仕様変更についてご案内させて頂きます。</span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;">これまで弊社にて作成させて頂きましたインストーラにはデジタル署名が登録されておりましたが、これより新たに公開されるインストーラには弊社にてデジタル署名が登録される事は御座いません。もしデジタル署名が必要なお客様には、御不便をお掛け致しますがデジタル署名が無登録のインストーラをお送りさせて頂きますので、皆様にデジタル署名を御登録頂き弊社に送り返して頂く事となります。</span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc175fd970b-pi" style="display: inline;"><img alt="Capture0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc175fd970b image-full img-responsive" src="/assets/image_568162.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture0" /></a></span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"></span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"> </span></h2>
<p align="left" class="MsoNormal" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; text-align: left;"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"><span lang="JA" style="color: black;">さて実際にデジタル署名が必要なお客様にはどの様な環境が必要になるでしょうか。ここでは</span><span style="color: black;">Windows<span lang="JA">環境例を一つ御紹介させて頂きます。今回は、下記のツールを２つと環境設定ファイル一つの動作を確認致しました。</span></span></span></p>
<ul>
<li>
<h2><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"><span style="color: black;"><span style="font-style: normal; font-variant: normal; font-weight: normal; line-height: normal; font-size-adjust: none; font-stretch: normal; -moz-font-feature-settings: normal; -moz-font-language-override: normal;">&#0160;</span></span><span style="color: black;"><a href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa387764(v=vs.85).aspx">SignTool</a></span></span></h2>
</li>
<li>
<h2><span style="font-size: 13pt; font-family: arial,helvetica,sans-serif; color: black;"><a href="http://www.openssl.org/related/binaries.html">OpenSSL</a></span></h2>
</li>
<li>
<h2><span style="font-size: 13pt; font-family: arial,helvetica,sans-serif; color: black;"><a href="http://stuff.mit.edu/afs/athena/contrib/crypto/openssl.cnf">openssl.cnf</a></span></h2>
</li>
</ul>
<h2 align="left"><span style="font-size: 13pt; font-family: arial,helvetica,sans-serif; color: black;"> SignTool<span lang="JA">については</span>Windows SDK<span lang="JA">に付随するものなので、</span>Visual Studio<span lang="JA">等をインストールされている方は既にお持ちかもしれません。インストールが終了しコマンドパスが通っている事を御確認頂きましたら、下記イメージの様に</span>openssl.exe<span lang="JA">を実行してみて下さい。プライベートキー等のファイルが作成されます。（各コマンドのパラメータ等は貴社の御仕様に併せ御変更下さい、詳しくは上記ＵＲＬよりマニュアルを御参照下さい）</span></span></h2>
<h2 align="left"><span style="font-size: 13pt; font-family: arial,helvetica,sans-serif; color: black;"><span lang="JA"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d7c7d84970d-pi" style="display: inline;"><img alt="Capture1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d7c7d84970d image-full img-responsive" src="/assets/image_942275.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture1" /></a><br /></span></span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;">さらに、SignTool.exeにて使用する.pfxファイルを作成します。此処で指定するパスワードはSignTool.exeにても使用しますので御注意下さい。</span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511713e07970c-pi" style="display: inline;"><img alt="Capture2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511713e07970c image-full img-responsive" src="/assets/image_781889.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture2" /></a></span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;">最後にSignTool.exeにてインストーラのデジタル署名の有無を確認した後登録、確認を行っております。</span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d7c7db5970d-pi" style="display: inline;"><img alt="Capture3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d7c7db5970d image-full img-responsive" src="/assets/image_334055.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture3" /></a></span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;">ファイルプロパティにてもデジタル署名の有無の確認が可能です。</span></h2>
<h2 align="left"><span style="font-family: arial,helvetica,sans-serif; font-size: 13pt;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d7c7dc8970d-pi" style="display: inline;"><img alt="Capture4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d7c7dc8970d img-responsive" src="/assets/image_61098.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Capture4" /></a>駆け足ではありますが、環境設定にお役立て頂ければ幸いです。</span></h2>

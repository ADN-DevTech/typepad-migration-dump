---
layout: "post"
title: "古い開発環境の維持について"
date: "2017-03-06 00:15:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/03/maintaining-old-development-environment.html "
typepad_basename: "maintaining-old-development-environment"
typepad_status: "Publish"
---

<p>ご存じのとおり、デスクトップ製品や開発ツールの動作環境は製品よってさまざまです。AutoCAD や Revit、Inventor のオートデスクのデスクトップ製品と関連する環境は、次のリンクでご確認いただけます。</p>
<ul>
<li><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/JPN/System-requirements-for-AutoCAD.html" rel="noopener noreferrer" target="_blank">AutoCAD のサポート動作環境</a></li>
<li><a href="https://knowledge.autodesk.com/ja/support/revit-products/troubleshooting/caas/sfdcarticles/sfdcarticles/JPN/System-requirements-for-Autodesk-Revit-products.html" rel="noopener noreferrer" target="_blank">Revit のサポート動作環境</a></li>
<li><a href="https://knowledge.autodesk.com/ja/support/inventor-products/troubleshooting/caas/sfdcarticles/sfdcarticles/JPN/System-requirements-for-Autodesk-Inventor-Products.html" rel="noopener noreferrer" target="_blank">Inventor のサポート動作環境</a></li>
<li><a href="https://www.microsoft.com/ja-jp/dev/support/tools.aspx">Visual Studio のサポート動作環境</a></li>
<li><a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=12257036" rel="noopener noreferrer" target="_blank">ObjectARX のサポート開発環境</a></li>
</ul>
<p>いずれも、コンピュータに物理的にインストールされた Windows 上にインストールして利用することを前提にしています。ただ、使用するコンピュータの更新などで、要求される環境の構築や維持が難しい場合もあります。</p>
<p>例えば、古い Windows が<a href="https://www.microsoft.com/ja-jp/atlife/article/windows10-portal/eos.aspx" rel="noopener noreferrer" target="_blank">サポート切れ</a>で新規に入手できなくなってしまい、オートデスク製品がサポートする環境を維持できない場面です。また、製品と開発ツールの組み合わせが、さらに環境構築を難しくする場合もあります。AutoCAD 2015 で ObjectARX を使ったのアドイン開発する場合には、Windows 7/8/8.1 にインストールされた Visual Studio 2012 Update 4 が必要になりますが、AutoCAD 2015 のリリース当時、まだ、Windows 10 が発売されていなかったため、今日現在最新の Windows 10 や Visual Studio 2015 で開発することは出来ません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09801518970d-pi" style="display: inline;"><img alt="Acad2015_dev_env" class="asset  asset-image at-xid-6a0167607c2431970b01bb09801518970d img-responsive" src="/assets/image_612886.jpg" title="Acad2015_dev_env" /></a></p>
<p>Windows 10 をサポートする &#0160;AutoCAD 2017 のサポート開発も、今後の Windows や Visual Studio のリリースによっては、環境を維持できなくなることも予想出来ます。すでに、Visual Studio 2015 用に Update 3 がリリースされていますが、AutoCAD 2017 がサポートする環境は、Update を適用しない Visual Studio 2015 となってしまっています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09801594970d-pi" style="display: inline;"><img alt="Acad2017_dev_env" class="asset  asset-image at-xid-6a0167607c2431970b01bb09801594970d img-responsive" src="/assets/image_354861.jpg" title="Acad2017_dev_env" /></a></p>
<p>このような状況でも、仮想環境ではなく、オートデスクがサポートする環境を、コンピュータに直接インストールされた Windows に構築してお使いいただくのが望ましいのは変わりありません。しかしながら、純粋な開発作業としては、仮想環境での環境構築をまったく否定するものではありません。</p>
<p>ご存知のように、ローカル環境（コンピュータに物理的にインストールされた Windows）をホスト OS として、仮想マシンにゲスト OS を構築する仮想化ツールがいくつか存在します。Windows とは別に購入する必要がありますが、代表的なものでは&#0160;<a href="http://store.vmware.com/DRHM/store?Action=DisplayProductDetailsPage&amp;SiteID=vmwjapan&amp;Locale=ja_JP&amp;ThemeID=37113000&amp;productID=323700100" rel="noopener noreferrer" target="_blank">VMWare Workstation</a> を挙げることが出来ます。また、<a href="http://www.parallels.com/jp/products/desktop/" rel="noopener noreferrer" target="_blank">Parallels Desktop for Mac</a>&#0160;という仮想ツールを利用することで、Mac 上の仮想マシンで Windows を稼動させることも出来てしまいます。下記は、Windows 7 Enterprise（ホスト OS）上の VMWare Workstation で作成した仮想マシンで動作する Windows 7 Professional（ゲスト OS） の例です。ゲスト OS 上には AutoCAD 2014 がインストールされていることがわかります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8dd2c2e970b-pi" style="display: inline;"><img alt="Vmware" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8dd2c2e970b image-full img-responsive" src="/assets/image_566505.jpg" title="Vmware" /></a></p>
<p>また、Windows 10 Pro 以上のエディションに搭載される <a href="https://docs.microsoft.com/ja-jp/virtualization/hyper-v-on-windows/about/" rel="noopener noreferrer" target="_blank">Hyper-V</a> なら、仮想化ツールを別購入する必要はありません。&#0160;下記は、Windows 10 Pro（ホスト OS）上の Hyper-V で作成した仮想マシンで動作する Windows 7 Professional（ゲスト OS）の例です。こちらの場合、ゲスト OS 上には AutoCAD 2015 がインストールされていることがわかります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980573a970d-pi" style="display: inline;"><img alt="Hyper-v4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980573a970d image-full img-responsive" src="/assets/image_887691.jpg" title="Hyper-v4" /></a>&#0160;</p>
<p>ハードウェア アクセラレーションが無効になってしまうなどの制約があるため、オートデスク製品自体を使用する目的で仮想環境の利用をお勧めすることは出来ませんが、参考までにご紹介させていただきました。やむをえない場合には、検討する価値はあるかと思います。</p>
<p>By Toshiaki Isezaki</p>

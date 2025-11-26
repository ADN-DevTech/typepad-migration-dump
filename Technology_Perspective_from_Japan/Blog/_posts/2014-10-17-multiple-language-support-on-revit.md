---
layout: "post"
title: "Revit の多言語対応"
date: "2014-10-17 00:11:25"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/multiple-language-support-on-revit.html "
typepad_basename: "multiple-language-support-on-revit"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/07/use-of-autocad-language-pack.html" target="_blank">AutoCAD と同様に</a>、Revit にも Language Pack（言語パック）の利用が可能です。 日本語版の Revit をお持ちなら、次の紹介する URL から&#0160;Language Pack（言語パック） をダウンロードして、Revit がインストールされているコンピュータに適用することで、日本語版以外の言語に Revit のユーザ インタフェースを変えて利用することが出来ます。&#0160;</p>
<p style="padding-left: 30px;"><strong>Revit 2013:</strong><br /><a href="http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-2013-language-packs.html?v=2013" target="_blank">http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-2013-language-packs.html?v=2013</a></p>
<p style="padding-left: 30px;"><strong>Revit 2014:</strong><br /><a href="http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-2014-language-packs.html" target="_blank">http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-2014-language-packs.html</a></p>
<p style="padding-left: 30px;"><strong>Revit 2015:</strong><br /><a href="http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-products-2015-language-packs.html" target="_blank">http://knowledge.autodesk.com/support/revit-products/downloads/caas/downloads/content/autodesk-revit-products-2015-language-packs.html</a></p>
<p>利用したい言語の言語パックをダウンロードすると、 自己解凍形式のインストーラを得ることが出来ます。このファイルを実行すると、インストーラが自動的に解凍されてインストールを開始することが出来ます。下記の画面は、簡体中国語の言語パック インストーラを起動した状態のものです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07dab18970c-pi" style="display: inline;"><img alt="Chinese_lp_installer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d07dab18970c image-full img-responsive" src="/assets/image_214922.jpg" title="Chinese_lp_installer" /></a></p>
<p style="text-align: left;">インストール時には、言語パックをインストールするフォルダを指定することになりますが、通常、日本語版の Revit がインストールされたフォルダを指定するのが一般的です。このフォルダは、既定では C:\Program Files\Autodesk\ になります。</p>
<p style="text-align: left;">Revit の言語パックの導入方法は、バージョンによって差異がありますので、詳細は Revit 側のオンライン ヘルプを参照されることをお勧めします。</p>
<p style="text-align: left;">インストールが完了すると、デスクトップにインストールした言語版の Revit を起動するためのショートカットが作成されます。また、Windows 7 では、[スタート] メニュー内の Autodesk フォルダにも、このショートカット表示されるようになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f3bfd2970b-pi" style="display: inline;"><img alt="Shortcuts" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f3bfd2970b img-responsive" src="/assets/image_254190.jpg" title="Shortcuts" /></a></p>
<p style="text-align: left;">もちろん、個別にダブルクリックして起動することで、異なる言語の Revit を同時に利用することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07dbb30970c-pi" style="display: inline;"><img alt="Ui_langueages" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d07dbb30970c image-full img-responsive" src="/assets/image_521950.jpg" title="Ui_langueages" /></a></p>
<p style="text-align: left;">各国語版のプロジェクト テンプレートによって、ファミリの名所などが異なるので、Revit API を用いて複数の言語に対応するアドイン アプリケーションを開発する際には便利です。</p>
<p style="text-align: left;">不要になった言語パックは、個別にコントロール パネルからアンインストールすることも可能です。ただし、最初にインストールされていた Revit 本体は、インストール時の言語と一体化しているので、Revit 本体をアンインストールする必要があります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0798e110970d-pi" style="display: inline;"><img alt="Uninstall" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0798e110970d image-full img-responsive" src="/assets/image_425668.jpg" title="Uninstall" /></a></p>
<p style="text-align: left;">By Toshiaki Isezaki</p>
<p style="text-align: left;">&#0160;</p>

---
layout: "post"
title: "Design Automation API for Inventor - Inventor Plugin、AppBundleの作成"
date: "2020-05-13 19:13:01"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-inventor-addin.html "
typepad_basename: "design-automation-api-for-inventor-inventor-addin"
typepad_status: "Draft"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e948300b200b-pi" style="display: inline;"><img alt="Autodesk-forge-logo-pms-color-black-stacked" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e948300b200b image-full img-responsive" src="/assets/image_657512.jpg" title="Autodesk-forge-logo-pms-color-black-stacked" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-postman-sample-tutorial.html" rel="noopener" target="_blank">前回までの記事</a>で、PostmanとサンプルのInventorのPluginを利用したDesign Automation API for Inventorのチュートリアルの動作を確認してきました。</p>
<p>チュートリアルでDesign Automationの動作を確認して、次に皆様が思われることは以下のような内容ではないでしょうか？</p>
<p>&#0160;</p>
<p>・現在ご利用中のInventorのAddinをDesign Automationで利用して作業を自動化したい</p>
<p>・Design Automationで作業を自動化するためにInventorのPluginを新規開発したい</p>
<p>&#0160;</p>
<p>まず初めに説明しますと、InventorのAddinはそのままの形では、Design Automation API for Inventorで利用することは出来ません。</p>
<p>なぜなら、InventorのAddinはInventorのGUIから起動、操作することが前提となっている作りとなっているためとなります。自動で処理を行うDesign Automationで利用するためにはGUIに依存した処理を修正する必要があるためです。</p>
<p>&#0160;</p>
<p>そこで、今回の記事ではサンプルのInventor Addinを用いて、Design Automation API for Inventorで利用する際に必要となる修正方法と、Design Automationで利用のAppBundleの作成方法について、以下の手順で解説したいと思います。</p>
<p>&#0160;</p>
<p>１．サンプルAddinのダウンロード</p>
<p>２．アドインを修正してプラグイン化</p>
<p>３．プラグインのローカルでの動作確認</p>
<p>４．AppBundleの作成</p>
<p>&#0160;</p>
<p>なお、InventorのAddinは、.NET Core をベースとするため、開発環境としてVisual Studio を利用します。</p>
<p>開発環境の構築がお済み出ない場合は、InventorのSDK等を参照して、InventorのAddinの開発環境の構築をお願いいたします。</p>
<p>&#0160;</p>
<p>それでは、解説を始めたいと思います。</p>
<p>&#0160;</p>
<p>１．サンプルAddinのダウンロードと動作の確認</p>
<p style="padding-left: 40px;">１-１．以下のリンクからサンプルのAddinのVisual Studio .net(C#)プロジェクトをダウンロード</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0263e949dce6200b img-responsive"><a href="https://adndevblog.typepad.com/files/change-param-addin-master.zip">Change-param-addin-masterをダウンロード</a></span></p>
<p style="padding-left: 40px;">１-２．Zipファイルを解凍し、Administrator権限で起動したVisual Studio 2017でソリューションファイルを開きます。</p>
<p style="padding-left: 40px;">１-３．ソリューションをビルドし、作成されたアドインをInventorに読み込ませます。InventorへのAddinのロード方法については、<a href="https://adndevblog.typepad.com/technology_perspective/2020/01/about-automatic-inventor-add-in-load.html">こちら</a>のブログ記事をご参照ください。</p>
<p style="padding-left: 40px;">１-４．</p>
<p style="padding-left: 40px;">&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>２．アドインを修正してプラグイン化</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>３．プラグインのローカルでの動作確認</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>４．AppBundleの作成</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>本記事での解説は以上となります。</p>
<p>&#0160;</p>
<p>いかがでしたでしょうか、是非ご自身でInventor のPluginを作成してDesign Automationで利用を試してみてください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>

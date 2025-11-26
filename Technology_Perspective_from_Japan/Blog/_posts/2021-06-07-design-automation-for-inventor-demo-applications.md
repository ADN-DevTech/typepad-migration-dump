---
layout: "post"
title: "Design Automation for Inventorを用いたデモアプリケーションのご紹介"
date: "2021-06-07 01:11:48"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/06/design-automation-for-inventor-demo-applications.html "
typepad_basename: "design-automation-for-inventor-demo-applications"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802eb3ef200d-pi"><img alt="図2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802eb3ef200d img-responsive" src="/assets/image_270876.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="図2" /></a></p>
<p>&#0160;</p>
<p>2019年10月28日にForge Design Automation for Inventorがリリースされてから約1年半となります。</p>
<p>既に実業務でご活用をいただいているお客様も多くいらっしゃいますが、いざ利用を検討しようとすると以下のようなことでお悩みの方がいらっしゃるのではないでしょうか。</p>
<p>&#0160;</p>
<ul>
<li>Design Automationを活用してみたいと考えてはいるけれども、どう活用したらいいのかが分からない</li>
<li>Viewerが便利そうなのは分かるが、Model Derivativeなど、各種APIを組み合わせる必要があり、どのようなアプリケーションが作れるのかのイメージがわかない</li>
<li>手元にあるパート・アセンブリファイルで、手軽にForgeのAPIを試してみたい</li>
</ul>
<p>&#0160;</p>
<p>これまで、このブログを通してのForge OnlineやAutodesk University等でご案内をしてきておりますが、Forge自身はAutoCAD、Inventor、Revit等のデスクトップ製品とは異なり、開発者に向けて、クラウド上で実行されるAPIを提供するサービスであるため、一般のお客様には敷居が高いと感じられているかもしれません。</p>
<p>&#0160;</p>
<p>そこで今回の記事では、手軽にDesign Automaton for Inventorを実行することのできるデモアプリケーションをご紹介したいと思います。</p>
<p>&#0160;</p>
<p>デモアプリケーションなので、実業務で利用するためには、ご自身でアプリケーションを構築する必要がありますが、実際に動くデモアプリケーションを使ってみることで、どういった使い方ができるのかや、どう活用していけるのかのイメージが湧きやすいのではないかと思います。</p>
<p>&#0160;</p>
<p>それでは、紹介していきたいと思います。</p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">Forge Configurator Inventor</span></strong></span></p>
<p><a href="http://inventor-config-demo.autodesk.io/">http://inventor-config-demo.autodesk.io/</a></p>
<p>&#0160;</p>
<p>一つ目のデモアプリケーションは、Design Automationを用いたコンフィグレータアプリとなります。</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>URLにアクセスすると、WheelとWrenchの2つのプロジェクトが表示されます。</p>
<p>&#0160;<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded6bf5b200c-pi" style="display: inline;"><img alt="1" class="asset  asset-image at-xid-6a0167607c2431970b026bded6bf5b200c img-responsive" src="/assets/image_486383.jpg" title="1" /></a></p>
<p>&#0160;</p>
<p>画面上部で、Modelタブを選択するとアップロードしたプロジェクト内のパートまたはアセンブリファイルをForge Viewerで参照することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802eb266200d-pi" style="display: inline;"><img alt="3" class="asset  asset-image at-xid-6a0167607c2431970b0278802eb266200d img-responsive" src="/assets/image_953794.jpg" title="3" /></a></p>
<p>&#0160;</p>
<p>画面左側には対象のモデルでの変更可能なパラメータが表示されます。</p>
<p>もし、対象のファイル内にiLogicのフォームが定義されている場合、その内容が左側のパネルに反映されます。</p>
<p>&#0160;</p>
<p>パラメータを変更して、Updateボタンを押すとDesing Automationの処理が実行され、変更したパラメータが反映されたViewが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802eb2a2200d-pi" style="display: inline;"><img alt="4" class="asset  asset-image at-xid-6a0167607c2431970b0278802eb2a2200d img-responsive" src="/assets/image_777548.jpg" title="4" /></a></p>
<p>&#0160;</p>
<p>画面上部で、BOMタブを選択するとモデルのBOM情報を参照することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded6c015200c-pi" style="display: inline;"><img alt="5" class="asset  asset-image at-xid-6a0167607c2431970b026bded6c015200c img-responsive" src="/assets/image_63912.jpg" title="5" /></a></p>
<p>&#0160;</p>
<p>また、画面上部でDrawingタブを選択するとプロジェクト内に含まれる図面ファイルを表示することが出来ます。</p>
<p>図面ファイルの表示時に、Modelsタブで行った変更を反映するためにDesing Automationが実行されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802eb2dd200d-pi" style="display: inline;"><img alt="6" class="asset  asset-image at-xid-6a0167607c2431970b0278802eb2dd200d img-responsive" src="/assets/image_884502.jpg" title="6" /></a></p>
<p>また、左下のExport PDFをクリックすると図面がPDF化されダウンロードすることが出来ます。</p>
<p>&#0160;</p>
<p>画面上部で、Downloadsタブを選択すると、パート、アセンブリファイル、図面ファイル、BOMおよびモデルをRevit Family ファイルに出力した結果をダウンロードすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded6c053200c-pi" style="display: inline;"><img alt="7" class="asset  asset-image at-xid-6a0167607c2431970b026bded6c053200c img-responsive" src="/assets/image_77547.jpg" title="7" /></a></p>
<p>&#0160;</p>
<p>このデモアプリケーションでは、初期表示されているプロジェクトを参照するだけではなく、自身でお持ちのパート、アセンブリファイルをアップロードし、Viewerで表示・パラメータを変更してDesign Automationの実行などを行うことが出来ます。</p>
<p>&#0160;</p>
<p>右上のAutodeskアイコンをクリックして、Autodesk IDでログインをすることで、自身のファイルをアップロードすることも可能となります。</p>
<p>アップロードが可能なファイルはInventorのiptまたは、Zipでパッケージ化されたアセンブリファイルです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e10711d4200b-pi" style="display: inline;"><img alt="2" class="asset  asset-image at-xid-6a0167607c2431970b0282e10711d4200b img-responsive" src="/assets/image_546895.jpg" title="2" /></a></p>
<p>&#0160;</p>
<p>このデモアプリは、GitHubでソースコードが公開されており、自身の環境に構築することも可能です。</p>
<p><a href="https://github.com/Autodesk-Forge/forge-configurator-inventor">https://github.com/Autodesk-Forge/forge-configurator-inventor</a></p>
<p>&#0160;</p>
<p><span style="font-size: 15pt;"><strong><span style="text-decoration: underline;">Inventor iLogic</span></strong></span></p>
<p><a href="https://forge-ilogic.herokuapp.com/">https://forge-ilogic.herokuapp.com/</a></p>
<p>&#0160;</p>
<p>このデモアプリは、InventorのパートファイルをアップロードするとDesing Automationを実行し、パートファイル内にパートファイル中のiLogicルールとパラメータを取得した後に、Viewerに表示をします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1071399200b-pi" style="display: inline;"><img alt="図1" class="asset  asset-image at-xid-6a0167607c2431970b0282e1071399200b img-responsive" src="/assets/image_572190.jpg" title="図1" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802eb410200d-pi" style="display: inline;"><img alt="図4" class="asset  asset-image at-xid-6a0167607c2431970b0278802eb410200d img-responsive" src="/assets/image_163959.jpg" title="図4" /></a></p>
<p>Design Automationで取得したパラメータや、iLogicは右上のiLogicボタンを押すことで参照することが出来、また画面上から、iLogicを選択してDesing Automationで選択したiLogicを実行することもできます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e10713ae200b-pi" style="display: inline;"><img alt="図5" class="asset  asset-image at-xid-6a0167607c2431970b0282e10713ae200b img-responsive" src="/assets/image_702221.jpg" title="図5" /></a></p>
<p>iLogicを実行すると、実行結果を反映したパートファイルを元に、Viewerの表示が更新されます。</p>
<p>&#0160;</p>
<p>今お使いのiLogicがあれば、Desing Automationで実行させるための修正は必要ですが、比較的簡単にDesing AutomationでiLogicを実行することができますので、既存のiLogicをベースにして、Design Automationを使って自動化するタスクを検討をしてみるのも良いかと思います。</p>
<p>&#0160;</p>
<p>なお、iLogicをDesing Automationで実行するために必要な修正内容については、以下のForge Onlineで解説をしておりますので、是非ご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-onlinedesign-automationinventor-ilogic.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_619370.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Forge Online：Design Automation：Inventor iLogicの活用</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Forge Online、今回はDesign Automation：Inventor iLogicの活用についてご案内いたします。 動画で利用しているプレゼンテーション資料（PDF）は次のリンクからダウンロードいただけます。スライド下部にある URL で、スライドで説明した内容のブログ記事、また、参考 Web ページを参照することが出来ます。またスライドの最後にも、参考ブログ記事へのリンクをまとめています。 Forge Online：Design Automation：Inventor iLogicの活用をダウンロード Design Automation：Inventor iLogicの活用 By Takehiro Kato</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>いかがでしたでしょうか。</p>
<p>ここでご紹介したデモアプリケーションが、皆様のForge Design Automation for Inventorを活用した、デジタルトランスフォーメンション、業務効率化の検討のお役に立てば幸いです。</p>
<p>&#0160;</p>
<p>by Takehiro Kato</p>

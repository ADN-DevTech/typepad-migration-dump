---
layout: "post"
title: "Design Automation API for Revit - CountDelete サンプル"
date: "2019-05-24 01:08:40"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/05/design-automation-api-for-revit-count-delete-elements-sample.html "
typepad_basename: "design-automation-api-for-revit-count-delete-elements-sample"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4600bd3200c-pi" style="float: right;"></a>Design Automation API for Revit について、今年の1月28日にパブリックベータ版が公開されてから、下記のブログ記事で、その概要から始まり、 Postman Sample と .NET Core Sample の動作確認の方法、さらには Model Derivative API による SVF 変換と Viewer 表示の機能を追加する方法をご紹介してきました。</p>
<p>下記は、関連するブログ記事のリファレンスとなります。<br />初めから学習したいという方は、これらの記事をご参照ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/design-automation-api-for-revit-public-beta.html">Design Automation API for Revit パブリックベータ版の公開</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/understanding-steps-to-use-design-automation-api-for-revit.html">Design Automation API for Revit 開発手順の理解</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/setup-design-automation-api-for-revit-postman-sample.html">Design Automation API for Revit - Postman Sample のセットアップ</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-1.html">Design Automation API for Revit - Postman Sample で動作確認 1</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-2.html">Design Automation API for Revit - Postman Sample で動作確認 2</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/setup-design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample.html">Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample のセットアップ</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part1.html">Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample で動作確認</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part2.html">Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample の処理結果を Viewer で表示</a></li>
</ul>
<p>そして、これらの記事で紹介している内容を、1 Day Workshop という形で学習いただける機会を設けております。<br />次回は、5月28日（火）東京、5月31日（金） 大阪にて開催いたしますので、ぜひご参加ください。<br /><strong><span style="text-decoration: underline;"><span style="color: #ff4040; text-decoration: underline;">両日とも5月27日（月）に、申込受付を締め切らせていただきます。</span></span></strong></p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/forge-1-day-workshop-design-automation-api-for-revit-2019-5.html">Forge 1 Day Workshop - Design Automation API for Revit - 開催 - 2019年5月</a></li>
</ul>
<p>また、Design Automation API V3 のドキュメントページでは、追加の学習リソースとして、<strong>CountDelete サンプル</strong>が公開されております。<br />今回は、このサンプルについてご紹介いたします。</p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/more-learning-resources/">More Learning Resources</a><br /><a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/more-learning-resources/">https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/more-learning-resources/</a></li>
</ul>
<p><strong>CountDelete サンプル</strong>は、２つの Revit アドインを 1つの Forge アプリケーションで、それぞれ用途に応じて切り替えて実行できるサンプルです。</p>
<p>1つは、<strong>Revit の要素をカテゴリ別に集計して、その結果をテキストファイルで出力するアドイン</strong>と、もう一つは、<strong>指定のカテゴリのファミリインスタンスを削除して Revit プロジェクトを出力するアドイン</strong>です。</p>
<p>このサンプルは、Learn Autodesk Forge チュートリアルの .NET Core サンプルと同じ仕組みで実装されておりますので、このサンプルを起動する手順は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part1.html">こちらのブログ記事</a></strong>をご参照ください。</p>
<p>CountDelete サンプルを起動すると、次のような画面が表示されます。</p>
<p>右上の「Configure」ボタンをクリックすると、AppBundle と Activity を登録するためのダイアログが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4895740200d-pi" style="display: inline;"><img alt="CountDelete1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4895740200d image-full img-responsive" src="/assets/image_428087.jpg" title="CountDelete1" /></a></p>
<p>下記のダイアログで、「CountIt」と指定し、起動するエンジンを「Autodesk.Revit+2019」に設定して、「Create/Update」ボタンをクリックしてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4adddb3200b-pi" style="display: inline;"><img alt="CountDelete3" class="asset  asset-image at-xid-6a0167607c2431970b0240a4adddb3200b img-responsive" src="/assets/image_363488.jpg" title="CountDelete3" /></a></p>
<p>AppBundle と Activity の登録が成功すると、元の画面の右サイドにあるログ出力コンポーネント上に、下記のようにそれぞれの ID が出力されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4600a6b200c-pi" style="display: inline;"><img alt="CountDelete4" class="asset  asset-image at-xid-6a0167607c2431970b0240a4600a6b200c img-responsive" src="/assets/image_428670.jpg" title="CountDelete4" /></a></p>
<p>「DeleteElements」の AppBundle も同じ操作で登録します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a489575e200d-pi" style="display: inline;"><img alt="CountDelete5" class="asset  asset-image at-xid-6a0167607c2431970b0240a489575e200d img-responsive" src="/assets/image_787636.jpg" title="CountDelete5" /></a></p>
<p>次に、このサンプルで使用する Revit プロジェクトをアップロードするための Bucket を作成します。</p>
<p>左サイドのコンポーネントは、Authentication API の 2-legged 認証 で Forge アプリに割り当てられている専用のストレージ領域のファイルをツリー構造で表示するオブジェクトブラウザです。Bucket が存在しない場合は、右上の「New Bucket」ボタンで新規に Bucket を作成します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4adddd5200b-pi" style="display: inline;"><img alt="CountDelete7" class="asset  asset-image at-xid-6a0167607c2431970b0240a4adddd5200b img-responsive" src="/assets/image_999201.jpg" title="CountDelete7" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4895793200d-pi" style="display: inline;"><img alt="CountDelete8" class="asset  asset-image at-xid-6a0167607c2431970b0240a4895793200d img-responsive" src="/assets/image_2946.jpg" title="CountDelete8" /></a></p>
<p>Bucket が作成できたら、その Bucket を右クリックして、コンテキストメニューから「Upload File」を選択して、Revit プロジェクトをアップロードします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a48957a4200d-pi" style="display: inline;"><img alt="CountDelete9" class="asset  asset-image at-xid-6a0167607c2431970b0240a48957a4200d img-responsive" src="/assets/image_485538.jpg" title="CountDelete9" /></a></p>
<p>アップロードが完了すると、Bucket 配下に Revit プロジェクトが表示されます。また、Revit プロジェクトを選択すると、画面中央に「Start Translation」ボタンが表示されます。ボタンをクリックすると、SVF 変換が始まり、変換が終了したら自動的に Forge Viewer が起動します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4adde29200b-pi" style="display: inline;"><img alt="CountDelete10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4adde29200b image-full img-responsive" src="/assets/image_338155.jpg" title="CountDelete10" /></a></p>
<p>Existing activities から「CountItActivity+dev」を選択し、「Start workitem」ボタンをクリックすると、Design Automation API の Workitem が実行されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4895810200d-pi" style="display: inline;"><img alt="CountDelete11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4895810200d image-full img-responsive" src="/assets/image_471829.jpg" title="CountDelete11" /></a></p>
<p>Workitem の処理が完了すると、右サイドのログ出力コンポーネントには、処理のログと、出力されたテキストファイルのダウンロードリンクが表示されます。また、左サイドのオブジェクトブラウザでは、そのテキストファイルが Bucket 上に保存されていることがわかります。</p>
<p>そして、それぞれのカテゴリ毎に集計した結果の数量が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4adde57200b-pi" style="display: inline;"><img alt="CountDelete12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4adde57200b image-full img-responsive" src="/assets/image_738073.jpg" title="CountDelete12" /></a></p>
<p>次は、削除する要素のカテゴリ「Windows」を選択し、Existing activities から「DeleteElementsActivity+dev」を選択し、「Start workitem」ボタンをクリックし、Design Automation API の Workitem を実行します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4600b32200c-pi" style="display: inline;"><img alt="CountDelete13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4600b32200c image-full img-responsive" src="/assets/image_955919.jpg" title="CountDelete13" /></a></p>
<p>Workitem の処理が完了したら、左サイドのオブジェクトブラウザで、新規に作成された Revit プロジェクトを選択し、「Start Translation」ボタンをクリックして SVF 変換を行います。変換が完了すると Forge Viewer が起動し、プロジェクト内に配置されている窓ファミリのインスタンスが削除されていることを確認できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4895852200d-pi" style="display: inline;"><img alt="CountDelete14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4895852200d image-full img-responsive" src="/assets/image_611968.jpg" title="CountDelete14" /></a></p>
<p>このように、Design Automation API を利用して、複数の Revit アドインを切り替えて実行できる環境を作成したり、あるいは、特定の業務課題を解決するためのワークフローをカバーするように、複数の Revit アドインを順番に実行させる仕組みを作成することで、Forge の活用の場はさらに拡がると考えられます。</p>
<p>ご興味ある方は、ぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>

---
layout: "post"
title: "Design Automation API for Inventor - Inventor iLogic利用時の注意点"
date: "2020-12-07 00:01:00"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/design-automation-api-for-inventor-note-writing-an-ilogic-for-da.html "
typepad_basename: "design-automation-api-for-inventor-note-writing-an-ilogic-for-da"
typepad_status: "Publish"
---

<p>前回の記事では、Desing Automation for InventorでiLogicを実行する方法について解説をしてきましたが、今回の記事では、Design Automation for InventorでiLogicを利用する際の注意点について解説をしたいと思ます。</p>
<p>&#0160;</p>
<p>もし、前回の記事をお読みでない方は、先に以下のリンクからご参照いただくことをお勧めいたします。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/design-automation-api-for-inventor-various-way-to-run-ilogic.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_474486.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Design Automation API for Inventor - iLogicを利用したDesign Automationの実行方法</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">InventorのiLogicをDesing Automationで利用する方法についての解説をしてきました。Pluginの開発には、.Net を用いて開発を行いますので...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>さて、Design Automation for InventorでのiLogicの実行ですが、実はデスクトップ環境で利用していたiLogicを、Desing Automation for Inventorで利用するためには、いくつか修正を行わなければならない場合がありますので、注意が必要です。</p>
<p>&#0160;</p>
<p>といっても、それほど多くの注意点はなく、今回ご紹介する、以下の3つの点にご留意いただければ多くの場合は問題なく動作させることができるかと思います。</p>
<ol>
<li>&#0160;ThisApplication.ActiveDocumentではなく、ThisDoc.Documentを使用する</li>
<li>UI表示をする処理を取り除く</li>
<li>LoggerではなくTraceを利用する</li>
</ol>
<p>&#0160;</p>
<p>それではそれぞれについて解説をしていきます。</p>
<h2 style="position: relative; color: white; background: #81d0cb; line-height: 1.4; padding: 0.5em 0.5em 0.5em 0.5em;">Inventor iLogic利用時の3つの注意点</h2>
<h4>1．ThisApplication.ActiveDocumentではなく、ThisDoc.Documentを使用する</h4>
<p>デスクトップ版のInventorアプリケーションでは、ユーザーが任意にドキュメントを開いてアクティブ化できるため、あるコマンドを実行するときに、現在アクティブなドキュメントを知る必要がある場合があります。この目的で使用するのが、ApplicationクラスのActiveDocumentプロパティです。</p>
<p>一方で、Design Automationを実行するクラウド環境では、Inventor Serverを使用して、アプリケーション（つまり、AppBundle内のコード）がすべてを完全に制御することができます。このため、どのドキュメントがアクティブであるかを追跡することにはあまり意味がありません。アプリケーションは、開いたドキュメントと操作する必要のあるドキュメントを認識している必要があり、このため、InventorServerには、ActiveDocumentプロパティがありません。</p>
<p>&#0160;</p>
<p>ルールの目的によって異なりますが、ほとんどの場合はiLogicルールのThisApplication.ActiveDocumentをThisDoc.Document（ルールが実行されているドキュメント）に置き換えるだけで済みます。</p>
<p>ただし、場合によっては、ThisApplication.ActiveDocumentが、ルールが実行されているパーツを含む最上位アセンブリへのショートカットである可能性があります。</p>
<p>このような場合は、以下の関数のような処理が必要となります。</p>
<pre><code>
Function GetTopAssembly(doc As Document) As Document
  If doc.ReferencingDocuments.Count = 0 Then
    Set GetTopAssembly = doc
  Else
    Set GetTopAssembly = GetTopAssembly(doc.ReferencingDocuments(1))
  End If
End Function
</code></pre>
<p>&#0160;</p>
<p>上記はVBAですので、iLogicの場合には、「Set」キーワードを削除する必要があります。</p>
<p>また、Design AutomationではThisApplication自体もサポートされていません。その代わりに、デスクトップでもサポートされているThisServerを使用できますが、ActiveDocumentプロパティのように、Inventor Serverではサポートされていない関数がある点に注意が必要です。</p>
<h4>2．UI表示をする処理を取り除く</h4>
<p>前回のブログ記事や、過去のDesing Automationに関する記事でも言及がありますが、Desing Automationの処理はクラウドサーバ上で実行されるため、MessageBox.Show(), MsgBox(), iLogicForm.Show(), InputBox()といったユーザインタラクションが必要なGUI表示処理を記述することは出来ません。</p>
<p>GUI処理の除去を支援し、場所を見つけるのに役立つ「iLogicRuleExporter」と呼ばれる非常に便利なツールがあります。 これにより、Inventorドキュメントとその参照ドキュメントからすべてのルールをiLogicVbファイルに抽出できます。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://github.com/ADN-DevTech/iLogicRuleExporter" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_145672.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">ADN-DevTech/iLogicRuleExporter</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Utility to export all Rules from the Active assembly to *.iLogicVb files - ADN-DevTech/iLogicRuleExporter.When you run it, it will find an active Inventor process, get the active document, and export all rules in it and in all referenced documents. The rules are exported as individual .iLogicVb files, in the same folder as the documents themselves. Generally it&#39;s a good idea to search for .iLogicVb files in the folders beforehand. But it&#39;s unlikely there will be name clashes because the files are generated with names in this format:</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>これにより、ソースコードで、特定のパラメータが変更された場所、特定のInventorAPIが使用された場所などの特定の処理を検索するのがはるかに簡単になります。</p>
<p>UIコンポーネントが使用されている場所を見つけて、それらを除去するのにも役立にちます。</p>
<p>&#0160;</p>
<h4>3．Loggerではなく、Traceを使用する</h4>
<p>サーバー上で処理を実行する場合、コード内で何が起こっているかをログに記録することがさらに重要になります。</p>
<p>iLogicではInventorユーザーインターフェイスの[iLogicログ]タブにメッセージを記録できる、Loggerと呼ばれるオブジェクトがありますが、現時点では、これらのログはWorkItemのレポートに表示されません。</p>
<p>一方で、トレースログ（Trace.WriteLine（）など）は、WorkItemのレポートに出力されるため、Design Automationでは、Loggerの代わりに、トレースログを使用するようにしてください。</p>
<p>&#0160;</p>
<h2 style="position: relative; color: white; background: #81d0cb; line-height: 1.4; padding: 0.5em 0.5em 0.5em 0.5em;">まとめ</h2>
<p>今回の記事では、iLogicをDesingAutomationで使用する場合の注意点を解説いたしました。</p>
<p>&#0160;</p>
<p>既存のiLogicをDesing Automationで活用する、新規にiLogicを作成してDesingAutomationで活用する際のご参考になれば幸いです。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>

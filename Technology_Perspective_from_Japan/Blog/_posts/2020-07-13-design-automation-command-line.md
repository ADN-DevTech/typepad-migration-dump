---
layout: "post"
title: "Design Automation API のコマンドライン指定の変更について"
date: "2020-07-13 01:09:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/07/design-automation-command-line.html "
typepad_basename: "design-automation-command-line"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9482f86200b-pi" style="display: inline;"><img alt="Quotes_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9482f86200b image-full img-responsive" src="/assets/image_868458.jpg" title="Quotes_0" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19cd55200c-pi" style="display: inline;"><br /></a></p>
<p>Design Automation API でコアエンジンの動作を定義する Activity では、コア エンジンをどう起動するか、起動後にどのドキュメントを開くか、どのスクリプトを実行するかなどを、コマンドライン（&quot;commandLine&quot; 属性）を使用して指定することになります。ご参考まで、API ドキュメントの <strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/field-guide/#activity" rel="noopener" target="_blank">このセクション</a></strong>で、Activity について詳しく説明しています。</p>
<p>次の例は、一般的な &quot;commandLine&quot; 属性値です。コアエンジンにパスを渡していることに注意してください。次に、起動時にロードするドキュメント（ファイル）、AppBundle、実行するスクリプト文字列を渡しています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="code-overflow-x hljs ruby" id="snippet-0"><span class="hljs-variable">$(</span>engine.path)\<span class="hljs-constant">InventorCoreConsole</span>.exe /i <span class="hljs-variable">$(</span>args[<span class="hljs-constant">InventorDoc</span>].path) /al <span class="hljs-variable">$(</span>appbundles[<span class="hljs-constant">SomeApp</span>].path) /s <span class="hljs-variable">$(</span>settings[script].path)  </code></pre>
<p>このように、Design Automation API を利用する Forge アプリの実際の処理は、コアエンジンにドキュメント - 3D モデル、または 2D 図面などのデザイン ファイルへのパス（args[].path）と、App Bundle へのパス（appbundles[].path）の値を提供するところから始まることになります。</p>
<p>パスの値には、スペースやその他の文字が含まれる可能性があり、潜在的に、期待しない文字列がコマンドラインが渡される可能性が懸念されています。 そこで、そのようなシナリオを回避するために、コマンドラインに渡す文字列には、ダブルクォーテーション（””）を使って値を渡すことが推奨されています。</p>
<p>今まで、コマンドラインへのパスの指定にダブルクォーテーション（””）は必須ではありませんでしたが、<span style="background-color: #ffff00;"><strong>10月1日から</strong>、ドキュメントのパス指定（args[].path）と、 App Bundle のパス指定（appbundles[].path）には、ダブルクォーテーション（””）で囲んで値を渡すことが必須となります。</span></p>
<p><span style="background-color: #ffff00;"><span style="background-color: #ffffff;">先の例に適用すると、次のようになります</span></span><span style="background-color: #ffffff;">。</span></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="code-overflow-x hljs bash" id="snippet-1">$(engine.path)\InventorCoreConsole.exe /i <span class="hljs-string">&quot;<span class="hljs-variable">$(args[InventorDoc].path)</span>&quot;</span> /al <span class="hljs-string">&quot;<span class="hljs-variable">$(appbundles[SomeApp].path)</span>&quot;</span> /s $(settings[script].path) </code></pre>
<p>Learn Forge チュートリアル（英語）の <strong><a href="https://learnforge.autodesk.io/#/designautomation/activity/" rel="noopener" target="_blank">Activity セクション</a></strong> も参考にしてみてください。</p>
<p>By Toshiaki Isezaki</p>

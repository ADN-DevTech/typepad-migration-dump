---
layout: "post"
title: "Model Derivative API での Revit 2014 以前のファイル変換エラー"
date: "2018-06-25 00:01:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/06/translation-error-with-prior-revit-2014-files.html "
typepad_basename: "translation-error-with-prior-revit-2014-files"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2018/04/model-derivative-updates-revit-support.html" rel="noopener noreferrer" target="_blank"> </a><strong><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39bb6d8200b-pi" style="float: right;"><img alt="Icon - model derivative api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39bb6d8200b img-responsive" src="/assets/image_171733.jpg" style="margin: 0px 0px 5px 5px;" title="Icon - model derivative api" /></a>Model Derivative API と Revit ファイルについて重要なお知らせ</strong> でご案内したとおり、順次（6月末迄には）、Revit 2014 以前に .rvt ファイル変換がサポートされないようなります。Model Deribative API で当該ファイルを変換しようとすると、次のような JSON レスポンス（マニフェスト）で、その旨が通知されるはずです。</p>
<pre>                         :<br />   &quot;name&quot;: &quot;LittleHouse_2014.rvt&quot;,<br />   &quot;hasThumbnail&quot;: &quot;false&quot;,<br />   &quot;status&quot;: &quot;failed&quot;,<br />   &quot;progress&quot;: &quot;complete&quot;,<br />   &quot;messages&quot;: [<br />     {<br /><strong>       &quot;type&quot;: &quot;error&quot;,</strong><br /><strong>       &quot;code&quot;: &quot;Revit-UnsupportedFileType&quot;,</strong><br /><strong>       &quot;message&quot;: &quot;&lt;message&gt;The file is not a Revit file or is not a supported version.&lt;/message&gt;&quot;</strong><br />     },<br />     {<br />       &quot;type&quot;: &quot;error&quot;,<br />       &quot;message&quot;: &quot;Possibly recoverable warning exit code from extractor: -536870935&quot;,<br />       &quot;code&quot;: &quot;TranslationWorker-RecoverableInternalFailure&quot;<br />     }<br />   ],<br />   &quot;outputType&quot;: &quot;svf“<br />                         :</pre>
<p>無償のオンラインビューアである&#0160;<a href="https://viewer.autodesk.com/" rel="noopener noreferrer" target="_blank"><strong>Autodesk Viewer（https://viewer.autodesk.com/）</strong></a>を使った新規アップロード後の変換処理にも、順次、この変更が適用されています。日本語ページでも、この点が Supported Format 注記に既に記載されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c84e2f7a200c-pi" style="display: inline;"><img alt="Supported_formats" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0223c84e2f7a200c image-full img-responsive" src="/assets/image_694012.jpg" title="Supported_formats" /></a></p>
<p>By Toshiaki Isezaki</p>

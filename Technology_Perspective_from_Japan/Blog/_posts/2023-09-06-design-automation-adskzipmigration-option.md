---
layout: "post"
title: "Design Automation API：adskZipMigration オプション"
date: "2023-09-06 00:01:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/09/design-automation-adskzipmigration-option.html "
typepad_basename: "design-automation-adskzipmigration-option"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25e5f47200d-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25e5f47200d image-full img-responsive" src="/assets/image_781564.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></p>
<p>Design Automation API に新しく adskZipMigration オプションを導入しています。ここでは、なぜ adskZipMigration を導入したのか、そして、導入によって解決される問題についてご紹介しておきたいと思います。</p>
<p><strong>背景と問題</strong></p>
<p>AutoCAD コアエンジンと Revit コアエンジンは、指定するパラメータの条件によっては Activity で指定した zip パラメータの <a  _mstmutation="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/" title="localName"><code data-stringify-type="code">localName</code>&#0160;</a> を無視することがあります（すべてのケースではありません）。この影響で、<code data-stringify-type="code">localName</code> 内で指定されたサブフォルダではなく、ジョブのルートフォルダや作業フォルダ（期待した場所ではない）に ZIP ファイルが解凍される場合があります。</p>
<p>簡単な例を見てみましょう。</p>
<p>素材ファイルを指定する入力パラメータを考えると、Activity の zip パラメータに &#39;true&#39; に設定され、pathInZip パラメータが null でない場合が当てはまります。この場合、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/#body-structure" rel="noopener" target="_blank">ドキュメント</a> 上の説明では、WorkItem の URL を通して渡された zip ファイルは、Design Automation の実行環境である仮想マシンの&#0160;&#0160;&#39;<strong><span style="color: #0000ff;">inputs</span></strong>&#39; フォルダに解凍されるはずです。</p>
<table border="1">
<thead>
<tr>
<th style="width: 14.85px;">
<p><span style="font-size: 10pt; font-family: helvetica;">#</span></p>
</th>
<th style="width: 70.87px;">
<p><span style="font-size: 10pt; font-family: helvetica;">Activity</span></p>
</th>
<th style="width: 108.85px;">
<p><span style="font-size: 10pt; font-family: helvetica;">Workitem</span></p>
</th>
<th style="width: 104.33px;">
<p><span style="font-size: 10pt; font-family: helvetica;">Arg direction</span></p>
</th>
<th style="width: 452.7px;">
<p><span style="font-size: 10pt; font-family: helvetica;">Comments</span></p>
</th>
</tr>
</thead>
<tbody>
<tr>
<td style="width: 14.85px; text-align: center;"><span style="font-size: 10pt; font-family: helvetica;">1</span></td>
<td style="width: 70.87px; text-align: center;"><span style="font-size: 10pt; font-family: helvetica;">zip==true</span></td>
<td style="width: 108.85px; text-align: center;"><span style="font-size: 10pt; font-family: helvetica;">pathInZip!=null</span></td>
<td style="width: 104.33px; text-align: center;"><span style="font-size: 10pt; font-family: helvetica;">input</span></td>
<td style="width: 452.7px;"><span style="font-size: 10pt; font-family: helvetica;">Zip is uncompressed to the folder specified in&#0160;<cite>localname</cite>. Any&#0160;<cite>path</cite>&#0160;reference to this argument will expand to full path of&#0160;<cite>pathInZip</cite>.（入力指定した Zip は localname で指定されたフォルダに展開される。この引数へのパス参照は pathInZip のフルパスに展開される。）</span></td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<pre><code class="language-json hljs ">{
	&quot;<span class="hljs-attribute">activityId</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;aps.DrawingGeneration+prod&quot;</span></span>,
	&quot;<span class="hljs-attribute">arguments</span>&quot;: <span class="hljs-value">{
		&quot;<span class="hljs-attribute">InputFile</span>&quot;: {
			<strong>&quot;<span class="hljs-attribute">zip</span>&quot;: <span class="hljs-literal">true</span></strong>,
			<strong>&quot;<span class="hljs-attribute">localName</span>&quot;: <span class="hljs-string">&quot;<span style="color: #0000ff;">inputs</span>&quot;</span></strong>,</span> // サブフォルダ input 内に ZIP ファイルが展開されることを期待<span class="hljs-value">
			&quot;<span class="hljs-attribute">pathInZip</span>&quot;: <span class="hljs-string">&quot;Template.dwg&quot;</span>,
			&quot;<span class="hljs-attribute">url</span>&quot;: <span class="hljs-string">&quot;url to download zip file&quot;</span>
		},
		&quot;<span class="hljs-attribute">Result</span>&quot;: {
			&quot;<span class="hljs-attribute">url</span>&quot;: <span class="hljs-string">&quot;url to upload results&quot;</span>,
			&quot;<span class="hljs-attribute">localName</span>&quot;: <span class="hljs-string">&quot;outputs&quot;</span>,
			&quot;<span class="hljs-attribute">verb</span>&quot;: <span class="hljs-string">&quot;put&quot;</span>,
			&quot;<span class="hljs-attribute">headers</span>&quot;: {
				&quot;<span class="hljs-attribute">x-ms-blob-type</span>&quot;: <span class="hljs-string">&quot;BlockBlob&quot;</span>,
				&quot;<span class="hljs-attribute">Content-type</span>&quot;: <span class="hljs-string">&quot;application/octet-stream&quot;</span>
			}
		},
		&quot;<span class="hljs-attribute">onComplete</span>&quot;: {
			&quot;<span class="hljs-attribute">url</span>&quot;: <span class="hljs-string">&quot;abc.url&quot;</span>,
			&quot;<span class="hljs-attribute">verb</span>&quot;: <span class="hljs-string">&quot;post&quot;</span>
		}
	}
</span>}</code></pre>
<p>ところが、上記期待とは異なり（Design Automation API 側の問題で）、現在、いずれの ZIP ファイルも作業フォルダに解凍されてしまっています。</p>
<p>期待した展開先：（正しい動作）</p>
<pre class="c-mrkdwn__pre" data-stringify-type="pre" style="padding-left: 40px;">T:\Aces\Jobs\8213a96d225b4ba6848797a5d212f868\<span style="color: #0000ff;"><strong>inputs</strong></span>\Template.dwg</pre>
<p>実際の展開先：（誤った動作）</p>
<pre class="c-mrkdwn__pre" data-stringify-type="pre" style="padding-left: 40px;">T:\Aces\Jobs\8213a96d225b4ba6848797a5d212f868\Template.dwg</pre>
<p>このため、この動作を修正してしまうと、一部の開発者がアプリ側で（誤った動作に依存する）実装済の入力ファイル取得処理が失敗してしまうことになります。</p>
<p><strong>新たなソリューションは？</strong></p>
<p>そこで、（本来の動作に沿った実装と誤った動作に対応した実装）両者の動作が尊重されるように、いくつかの変更を加えています。</p>
<ol data-border="0" data-indent="0" data-stringify-type="ordered-list">
<li data-stringify-border="0" data-stringify-indent="0">zip ファイルは古い（正しくない）場所と新しい（正しい）場所の両方に解凍されますが、「2 次解凍」（新しい場所への解凍）によるエラーはすべて無視します。</li>
<li data-stringify-border="0" data-stringify-indent="0">「2 次解凍」（アプリ実装を変更する必要があることを示す）をおこなう必要がある場合には、移行に関する警告をレポートログに記録します。&#0160;
<ul>
<li data-stringify-border="0" data-stringify-indent="0">ログ例：:<br />&#0160;
<pre><code class="language-bash hljs ">Warning: Zip Migration: the input of <span class="hljs-string">&#39;inputs&#39;</span> is unzipped to the correct location of <span class="hljs-string">&#39;T:\Aces\Jobs\d278348cb72b4a40b8d6ac6ff3720066\vararg_inputs&#39;</span>.]
[Runner] [INFORMATION] [<span class="hljs-number">0</span>] [Runner] [EC2AMAZ-<span class="hljs-number">88</span>Q8Q37] [WiID=d278348cb72b4a40b8d6ac6ff3720066. Warning: Zip Migration: the input of <span class="hljs-string">&#39;inputs&#39;</span> is unzipped to the correct location of <span class="hljs-string">&#39;T:\Aces\Jobs\d278348cb72b4a40b8d6ac6ff3720066\inputs&#39;</span>.]</code></pre>
</li>
</ul>
</li>
<li>また、正しい場所への解凍によるエラーを無視せざるを得ない場合は、レポートログに実装変更の警告を記録します。</li>
<li>WorkItem に新しい引数<strong> <code>adskZipMigration</code></strong>&#0160;を用意して、正しい動作に沿った実装を持つアプリ/プラグイン/スクリプトをテスト出来るようにしています。（zip ファイルは localName で指定されたサブフォルダにのみ解凍されます。この場合、レポートログへの記録はありません。）</li>
</ol>
<p>WorkItem リクエストボディ ペイロードの例：</p>
<pre><code class="language-json hljs ">{
   &quot;<span class="hljs-attribute">activityId</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;myNickName.mytest+Latest&quot;</span></span>,
   &quot;<span class="hljs-attribute">arguments</span>&quot;: 
   <span class="hljs-value">{
      &quot;<span class="hljs-attribute">input</span>&quot;: {xxxxxxxx},
      &quot;<span class="hljs-attribute">result</span>&quot;: {yyyyyyyy},
      <strong>&quot;<span class="hljs-attribute">adskZipMigration</span>&quot; : <span class="hljs-literal">true</span></strong>
   }
</span>}</code></pre>
<p><strong>ご対応のお願い</strong></p>
<p>上記ケースにおいて、当面の間（いまのところ 2023年12月までを予定）、WorkItem 実行時に ZIP ファイルを両方の場所に解凍し、必要に応じて実装の移行を促す警告を発します。前述のとおり、移行が必要な場合には、レポートログで内容を把握出来るようにしています。古い動作を完全に停止する前に、（該当する実装の移行がうまく進んで、）少なくとも 2 週間は移行警告を検出しなくなることを目標にしています。</p>
<p>ご不明な点がございましたら、<a href="https://aps.autodesk.com/get-help" rel="noopener" target="_blank">Get Help</a> までお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/design-automation-introducing-new-adskzipmigration-argument" rel="noopener" target="_blank">Design Automation: Introducing the New `adskZipMigration` Argument | Autodesk Platform Services</a>&#0160;から転写・翻訳、補足したものです。</p>
<p>By Toshiaki Isezaki</p>

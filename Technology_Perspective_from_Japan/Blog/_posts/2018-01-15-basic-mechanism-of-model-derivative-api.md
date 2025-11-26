---
layout: "post"
title: "Model Derivative API の基本的な仕組み"
date: "2018-01-15 00:53:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/01/basic-mechanism-of-model-derivative-api.html "
typepad_basename: "basic-mechanism-of-model-derivative-api"
typepad_status: "Publish"
---

<p>現在の <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/10/solutions-forge-deliver.html" rel="noopener noreferrer" target="_blank">Forge が提供するソリューション</a></strong> で重要な役割を担っているのが、さまざまなデザイン ファイルの変換をおこなう Model Derivative API です。Viewer にストリーミング配信・表示する用途の変換だけでなく、一部、別のデザイン ファイル形式への変換処理も可能になっています。今回は、Model Derivative API の仕組みと変換後の要素について、簡単にまとめておきたいと思います。</p>
<p>Model Derivative APIは、ソースファイルをさまざまな形式の出力ファイル（派生データ）に変換します。 これらのファイルに関する情報（出力ファイルのURNや変換されたジョブの状態など）は、派生データに関する参照がマニフェストに格納されていて、それら参照から各種派生データやメタデータをダウンロードして取得すると、モデル内の個々のオブジェクト、その幾何学的表現、および、関連プロパティを識別することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09e603f0970d-pi" style="display: inline;"><img alt="FieldGuide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09e603f0970d image-full img-responsive" src="/assets/image_445971.jpg" title="FieldGuide" /></a></p>
<div class="line-block">
<div class="line">次の表は、Model Derivative API を説明する上で重要な用語の定義です。</div>
<div class="line">&#0160;</div>
</div>
<table border="1" cellspacing="0" class="docutils" style="width: 100%;"><colgroup><col width="11%" /><col width="89%" /></colgroup>
<thead valign="bottom">
<tr class="row-odd">
<th class="head" style="text-align: center;">用語</th>
<th class="head" style="width: 794.743px; text-align: center;">定義</th>
</tr>
</thead>
<tbody valign="top">
<tr class="row-even">
<th style="width: 20%; text-align: left; vertical-align: middle;">
<p>ソース ファイル</p>
<p>(source file)</p>
</th>
<td style="width: 794.743px; text-align: left; vertical-align: middle;">
<div class="first last line-block">
<div class="line">他のファイル形式に変換するために使用されたソース ファイルを指します。ソース ファイルは主にデザイン ファイルであることが一般的です。また、「シードファイル」と呼ばれることもあります。サポートされているファイル形式については、&#0160;Model Derivative API が提供する RESTful API、<a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/formats-GET">GET formats</a>&#0160;endpoint を参照してください。</div>
</div>
</td>
</tr>
<tr class="row-odd">
<th style="width: 15%; text-align: left; vertical-align: middle;">
<p>派生データ</p>
<p>(derivatives)</p>
</th>
<td style="width: 794.743px; text-align: left; vertical-align: middle;">ソース ファイルから変換された出力ファイル・データを指します。</td>
</tr>
<tr class="row-even">
<th style="width: 15%; text-align: left; vertical-align: middle;">
<p>マニフェスト</p>
<p>(manifest)</p>
</th>
<td style="width: 794.743px; text-align: left; vertical-align: middle;">派生データのタイプと派生ファイルの URN など、変換されたジョブのステータスと派生データに関する情報の両方を保持する JSON コンテナです。Model Derivative API で変換処理をした際に生成されます。</td>
</tr>
<tr class="row-odd">
<th style="width: 15%; text-align: left; vertical-align: middle;">
<p>メタデータ</p>
<p>(metadata)</p>
</th>
<td style="width: 794.743px; text-align: left; vertical-align: middle;">ソース ファイルから識別可能な要素とプロパティを抽出するために使用されます。</td>
</tr>
</tbody>
</table>
<div class="section" id="translation">
<h2>変換 - Translation</h2>
<p>開発者は、ソース ファイルをさまざまなタイプの出力ファイルに同時に変換できます。 各ソース ファイルの派生データへの参照は変換時に生成される 1 つのマニフェストに格納されているため、簡単に見つけることができます。 また、モデルの一部をジオメトリ ファイルに変換することもできます。</p>
<p>次のワークフローは、ソース ファイルを目的の出力タイプに変換する方法と、その派生データをダウンロードする方法です。</p>
<ol class="arabic">
<li>
<p class="first"><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/formats-GET">GET formats</a>&#0160;endpoint を呼び出して、Forgeでサポートされている各ソース ファイル形式毎に変換可能なの最新リストを取得します。サポートされる変換に関する詳細は、<a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/overview/supported-translations">Supported Translation Formats table</a>&#0160;を参照してください。</p>
<p>注意：Forge で変換可能なリストには、適宜、新しいファイル形式が追加されています。</p>
</li>
<li>
<p class="first"><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST">POST job</a>&#0160;endpoint を呼び出して、ソースファイルを他の異なるファイル形式に変換（出力）します。</p>
</li>
<li>
<p class="first"><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-GET">GET :urn/manifest</a>&#0160;endpoint を呼び出して、変換処理が終了してダウンロードや参照が可能か否かをチェックすます。詳細は、下記の “<strong>変換ジョブ ステータス</strong>” セクションを参照してください。</p>
</li>
<li>
<p class="first">変換処理が完了した場合には、<a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-GET">GET :urn/manifest</a>&#0160;endpoint を呼び出して派生データの URN を取得します。</p>
</li>
<li>
<p class="first"><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeurn-GET">GET :urn/manifest/:derivativeurn</a>&#0160;endpoint で各種派生データをダウンロードする URN を取得します。</p>
</li>
</ol>
<div class="section" id="translation-job-status">
<h3>変換ジョブ ステータス</h3>
<p><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST">POST job</a>&#0160;endpoint は非同期であり、プログラムの実行を停止するのではなく、バックグラウンドで実行されるジョブを開始します。<a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST">POST job</a>&#0160;endpoint がジョブが正しく起動して&#0160; <code class="docutils literal"><span class="pre">success</span></code>&#0160;レスポンスを返しても、&#0160;<a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-GET">GET :urn/manifest</a>&#0160;endpoint を呼び出して非同期ジョブが完了しているかどうかをチェックする必要があります。</p>
</div>
</div>
<div class="section" id="data-extraction">
<h2>データ抽出 - Data Extraction</h2>
<p>Model Derivative API は、ソース ファイルからメタデータを抽出するのに使用できます。メタデータは、階層ツリー内のオブジェクトと、そのプロパティとジオメトリを識別するために使用できます。 ほとんどの CAD アプリケーション（例：Fusion や Inventor）は、単一のオブジェクト&#0160; ツリーや設計モデルのプロパティ セットを持ちますが、いくつかのアプリケーション（例：Revit）は、ユーザが複数のオブジェクト ツリーやプロパティ セットを有するモデルを設計することを可能にするため、モデルのオブジェクト ツリーとプロパティ セットを「モデルビュー」と呼び、 複数のモデルビュー（例：HVAC、建築、パースビュー）を有するモデルは、各モデルビュー毎に異なるオブジェクトツリーとプロパティのセットを持っています。</p>
<img alt="../../../_images/model_2.png" src="/assets/model_2.png" />&#0160;
<p>注意：このデータを抽出するには、デザイン ファイルを&#0160; <a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST">POST job</a>&#0160;endpoint を使用してSVFファイルに変換する必要があります。</p>
<ul class="simple">
<li><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-GET">GET :urn/metadata</a>&#0160;endpoint は、モデルのメタデータに関する情報を提供します。</li>
<li><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-GET">GET :urn/metadata/:guid</a>&#0160;endpoint は、選択したメタデータのオブジェクトツリーを返します。</li>
<li><a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET">GET :urn/metadata/:guid/properties</a>&#0160;endpoint は、特定のメタデータ ビュー内のオブジェクトとそのプロパティのフラット・リストを返します。</li>
</ul>
<p>データ抽出についての詳細は、<a class="reference external" href="https://developer.autodesk.com/en/docs/model-derivative/v2/tutorials">Step-by-Step Tutorials </a>を参照してください。</p>
</div>
<p>By Toshiaki Isezaki&#0160;</p>
<p>&#0160;</p>

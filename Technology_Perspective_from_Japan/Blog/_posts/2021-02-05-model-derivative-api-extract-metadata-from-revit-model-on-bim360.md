---
layout: "post"
title: "Model Derivative API - BIM 360 上の Revit モデルからメタデータを一括取得する方法とそのデータ構造"
date: "2021-02-05 00:41:32"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/02/model-derivative-api-extract-metadata-from-revit-model-on-bim360.html "
typepad_basename: "model-derivative-api-extract-metadata-from-revit-model-on-bim360"
typepad_status: "Publish"
---

<p>Autodesk University 2020 にて配信致しました<strong> <a href="https://adndevblog.typepad.com/technology_perspective/2020/11/au-2020-japanese-lightnung-talk.html">Forge 日本語ライトニングトーク</a></strong>「BIM 360 プロジェクトとアカウントのディープファイル横断検索」の中で、Revit モデルのメタデータを一括取得する方法について解説致しましたが、その際、プロパティの値の形式についてご質問がございました。</p>
<p>そこで今回は、その回答も含めて、<strong><a href="https://forge.autodesk.com/blog/accessing-design-metadata-without-viewer">元のブログ記事</a></strong>をベースに、<strong>BIM 360 に保存されている Revit モデルからメタデータを一括取得する方法とそのデータ構造について</strong>解説致します。</p>
<p>Model Derivative API を通じてメタデータを取得する方法は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html">こちらの記事</a></strong>で既にご紹介しております通り、Forge Viewer を使用することなく、Revit モデルからメタデータ、つまり要素のプロパティの情報にアクセスすることができます。</p>
<p>ただし用途によっては、プロパティを随時検索するのではなく、予め一括でメタデータを取得したいという場合がございます。</p>
<p>これを実現する方法としては、<strong>3つのオプション</strong>があります。</p>
<h3><strong>オプション1 : HTTP エンドポイント</strong></h3>
<p>まず1つ目は、SVF ファイルからプロパティの情報を JSON 文字列として一括取得する方法です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebb1cf2200c-pi" style="display: inline;"><img alt="Metadata_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebb1cf2200c image-full img-responsive" src="/assets/image_370916.jpg" title="Metadata_02" /></a></p>
<p><br /><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-metadata-guid-properties-GET/"><strong>このエンドポイント</strong></a>に対して、HTTP リクエストを送る際、クエリパラメータに [objectid] を指定しなければ、そのモデルに保持されている全ての要素のプロパティを一括して JSON データとして取得することができます。</p>
<p>ただし、JSON データが 20 MB 以上になる場合は、クエリパラメータに [forceget=true] を指定する必要があります。<br />正常にダウンロードが完了すると、次のような形式でプロパティのデータを取得することができます。<br /><strong><span style="color: #ff4040;">寸法の値には、数値の文字列に加えて、単位の形式を示す文字列が含まれています。</span></strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788012f89e200d-pi" style="display: inline;"><img alt="Metadata_01" class="asset  asset-image at-xid-6a0167607c2431970b02788012f89e200d img-responsive" src="/assets/image_916590.jpg" title="Metadata_01" /></a></p>
<p>なお、BIM 360 上の Revit モデルを URN に指定する際は、Data Management API を通じて、BIM 360 上の Revit モデルの Version Id を取得しますが、これを Base64 (URL Safe) エンコードに変換する際には、注意が必要です。</p>
<p>例えば、次のような Version Id が取得できた場合、この文字列をそのまま Base64 (URL Safe) エンコードすると、「?」が「/」に置き換わりますが、この値でエンドポイントにリクエストしてもエラーになります。</p>
<p style="padding-left: 40px;"><strong>Version Id</strong> : urn:adsk.wipprod:fs.file:vf.GT7D9cshRGS7GsP1YBQNVQ?version=2</p>
<p style="padding-left: 40px;"><strong>Base64 エンコード（誤）</strong>: dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkdUN0Q5Y3NoUkdTN0dzUDFZQlFOVlE/dmVyc2lvbj0y</p>
<p><span style="color: #ff4040;"><strong>正しい URN は、「?」で Split して、それぞれを「 _ 」で連結した結果の文字列になります。</strong></span></p>
<p style="padding-left: 40px;"><strong>Base64 エンコード（正）</strong>: dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkdUN0Q5Y3NoUkdTN0dzUDFZQlFOVlE_dmVyc2lvbj0y<br /><br /></p>
<h3><strong>オプション2 : SQLite データベース</strong></h3>
<p>2つ目の方法は、SQLite データベースのファイルを取得する方法です。</p>
<p>Model Derivative サービスは、すべてのプロパティの情報を SQLite ファイルに保存しております。<br />Revit モデルのマニフェストを取得し、その結果の JSON データから mime タイプが application/autodesk-db に、role が Autodesk.CloudPlatform.PropertyDatabase に設定さ れている model.sdb ファイルがこれに該当します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788012f908200d-pi" style="display: inline;"><img alt="Metadata_05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788012f908200d image-full img-responsive" src="/assets/image_894618.jpg" title="Metadata_05" /></a></p>
<p>例えば、オプション 1 の Revit モデルのマニフェストを取得すると、該当の URN は次のようになります。</p>
<pre><code>&quot;urn:adsk.viewing:fs.file:dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkdUN0Q5Y3NoUkdTN0dzUDFZQlFOVlE_dmVyc2lvbj0y/output/Resource/model.sdb&quot;</code></pre>
<p>この URN 文字列を下記のように URL エンコードして、<strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeurn-GET/">リクエストするエンドポイント</a></strong>の[derivativeurn]に指定します。</p>
<pre><code>urn%3Aadsk.viewing%3Afs.file%3AdXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkdUN0Q5Y3NoUkdTN0dzUDFZQlFOVlE_dmVyc2lvbj0y%2Foutput%2FResource%2Fmodel.sdb</code></pre>
<p>model.sdb ファイルは、SQLite のデータベースブラウザなどで開いて確認することができます。<br /><a href="https://sqlitebrowser.org/"><strong>DB Browser for SQLite</strong> </a>で開くと、4つのテーブルから構成されるデータベースになっていることがわかります。<br />テーブルのスキーマは次の通りです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebb1d4d200c-pi" style="display: inline;"><img alt="Metadata_03" class="asset  asset-image at-xid-6a0167607c2431970b026bdebb1d4d200c img-responsive" src="/assets/image_233980.jpg" title="Metadata_03" /></a></p>
<p>このツールでは、開いているデータベースに対して SQL 文で検索することができます。<br />例えば、ある特定の要素のプロパティの情報を取得すると、次のような結果が得られます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebb1d5d200c-pi" style="display: inline;"><img alt="Metadata_04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebb1d5d200c image-full img-responsive" src="/assets/image_976540.jpg" title="Metadata_04" /></a></p>
<p><span style="color: #ff4040;"><strong>このように、寸法のプロパティの値には、数値のみが保持されており、dataType というカラムで、単位タイプを確認できるようになっていることがわかります。</strong></span></p>
<p>なお、dbId と externalId の違いについては、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/externalid-and-dbid.html">こちらのブログ記事</a></strong>をご参照ください。<br />Revit モデルでは、externalId は、Revit の要素に割り振られている UniqueId に対応しております。<br /><br /></p>
<h3><strong>オプション3 : Forge Viewer に最適化されたデータベース</strong></h3>
<p>3つ目の方法は、SQLite のデータベースファイルを Forge Viewer に最適化された形式で取得する方法です。</p>
<p>model.sdb ファイルは、ローカル環境で利用する際には便利ですが、Web アプリケーション上で利用する場合は、より容易にアクセスできる形式が必要です。</p>
<p>Forge Viewer では、モデルのメタデータは、パフォーマンスを維持するために、SQLite データベースのテーブル名にそれぞれ対応するよう分割された複数のファイルを使用しています。<br />それぞれのファイルは、<strong>JSON ファイルを圧縮した *.json.gz 形式</strong>となり、個別にダウンロードすることができます。</p>
<ul>
<li><strong>objects_ids.json.gz</strong>
<ul>
<li>object ID (Forge Viewer で使用される &quot;dbId&quot;)</li>
<li>external ID (デザインファイルの形式によって異なる要素ID、Revit の場合は “GUID”)</li>
</ul>
</li>
<li><strong>objects_attrs.json.gz</strong>
<ul>
<li>プロパティ名、カテゴリ、タイプ、単位など</li>
</ul>
</li>
<li><strong>objects_vals.json.gz</strong>
<ul>
<li>プロパティの値</li>
</ul>
</li>
</ul>
<p>例えば、下記のように output/Resource/ファイル名 を指定してダウンロードすることができます。<br />ファイルの使用方法は、<strong><a href="https://github.com/petrbroz/forge-convert-utils/blob/develop/src/common/propdb-reader.ts">こちらの GitHub サンプル</a></strong>をご参照ください。</p>
<pre><code>derivatives/urn:adsk.viewing:fs.file:dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkdUN0Q5Y3NoUkdTN0dzUDFZQlFOVlE_dmVyc2lvbj0y/output/Resource/objects_vals.json.gz</code></pre>
<p>オプション 2 とオプション 3 に共通する EAV(エンティティ・アトリビュート・バリュー)の構造図は次のようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788012fa20200d-pi" style="display: inline;"><img alt="Metadata_07" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788012fa20200d image-full img-responsive" src="/assets/image_736082.jpg" title="Metadata_07" /></a></p>
<p>Revit モデルのメタデータを様々なアプリケーションでご利用いただければ幸いです。</p>
<p>By Ryuji Ogasawara</p>

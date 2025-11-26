---
layout: "post"
title: "Model Derivative API：ZIP ファイルのアップロードと変換"
date: "2020-12-09 00:51:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/zip-file-upload-and-translation.html "
typepad_basename: "zip-file-upload-and-translation"
typepad_status: "Publish"
---

<p>以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/10/forge-viewer-2d-sheets-layouts.html" rel="noopener" target="_blank">Forge Viewer：2D シート/レイアウト</a></strong> のブログ記事でも触れたことがありますが、外部参照を含む AutoCAD の DWG ファイルや Revit リンクを含む Revit プロジェクト（RVT）ファイル、パーツやサブアセンブリを内包する Inventor などの製造系 CAD ファイルなど、複数ファイルで構成されているデザイン ファイルを Model Derivative API で変換して Forge Viewer で表示することが出来ます。</p>
<p>この時、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" target="_blank"><strong>POST job</strong></a> endpoint 呼び出しで必要なリクエスト ボディには、親ファイルの指定と&#0160; ZIP 圧縮されていることを示すパラメータを明示的に記述する必要があります。次の例は、複数の外部参照図面を参照する 8th floor.dwg を親図面が ZIP 圧縮したファイルを変換するための リクエスト ボディ（JSON）です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">&#0160;       {<br />            input: {<br />                urn: encodedURN,<br /><strong>                rootFilename: &quot;8th floor.dwg&quot;,</strong><br /><strong>                compressedUrn: true</strong><br />            },<br />            output: {<br />                formats: [<br />                    {<br />                        type: &quot;svf&quot;,<br />                        views: [&quot;2d&quot;, &quot;3d&quot;]<br />                    }<br />                ]<br />            }<br />        }</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e97ba54d200b-pi" style="display: inline;"><img alt="Ref_structure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e97ba54d200b image-full img-responsive" src="/assets/image_103375.jpg" title="Ref_structure" /></a></p>
<p>なお、ZIP ファイルを手動操作で 作成する場合には、ファイル名エンコードに UTF-8 を使用するようにしてください。日本語版 Windows で Windows エクスプローラーの ZIP 圧縮機能を使用すると、Shift-JIS コードでファイル名がエンコードされてしまい、（デザイン ファイル名に日本語が使われていると）ファイル名が文字化けした状態で解凍されてしまい、ファイル名の解決が出来ずに変換処理が失敗してしまいます。手動操作で ZIP 圧縮する際には、エンコード指定が可能な <a href="https://sevenzip.osdn.jp/" rel="noopener" target="_blank">7-Zip</a> などのツールの使用をお勧めします。</p>
<p>ZIP ファイルの利用は、複数で構成されたデザイン ファイルだけが対象ではありません。ファイルサイズが大きくなりがちな テキスト形式の DXF ファイルや IFC ファイルなど、単一ファイルのアップロードと変換でも使用することが出来ます。テキスト形式では、データサイズが大きければ大きいほど、改行コードなどが増加して全体のファイルサイズが大きくなってしまう傾向があるため、ZIP 圧縮は Data Management API によるアップロード時間の短縮にも一役買うことが出来ます。</p>
<p>例えば、Revit のサンプル プロジェクトして提供している Technical_school-current_m.rvt（15.5 MB）を Revit 2021 で IFC ファイルに書き出すと、出力された Technical_school-current_m.ifc&#0160;は 44.1MB になってしまいますが、ZIP 圧縮すると Technical_school-current_m.zip は 8.68 MB までファイル サイズが減少します。</p>
<p>単一ファイルを ZIP ファイルで変換する際にも、親ファイルの指定と&#0160; ZIP 圧縮されていることを示すパラメータをリクエスト ボディで記述する必要があります。ご注意ください。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">&#0160;       {<br />            input: {<br />                urn: encodedURN,<br /><strong>                rootFilename: &quot;Technical_school-current_m.ifc&quot;,</strong><br /><strong>                compressedUrn: true</strong><br />            },<br />            output: {<br />                formats: [<br />                    {<br />                        type: &quot;svf&quot;,<br />                        views: [&quot;2d&quot;, &quot;3d&quot;]<br />                    }<br />                ]<br />            }<br />        }</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e97ba825200b-pi" style="display: inline;"><img alt="Technical_school-current_m.zip" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e97ba825200b image-full img-responsive" src="/assets/image_167250.jpg" title="Technical_school-current_m.zip" /></a></p>
<p>ZIP ファイルから変換された SVF ファイルの Forge Viewer 表示には、特に固有の指定は必要ありません。アップロードした ZIP ファイルの URN を指定するのみです。</p>
<p>By Toshiaki Isezaki</p>

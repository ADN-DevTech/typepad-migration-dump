---
layout: "post"
title: "Model Derivative API：RVT >> DWG 変換"
date: "2022-11-28 00:01:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/model-derivative-apirvt-dwg-translation.html "
typepad_basename: "model-derivative-apirvt-dwg-translation"
typepad_status: "Publish"
---

<p>デザイン ファイルを Autodesk Platform Services（旧 Forge）の Viewer にする際、Model Derivative API の <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/" rel="noopener" target="_blank">POST job</a> エンドポイントを使って、シード ファイルとも呼ばれるでデザイン ファイルを SVF または SVF2 に変換します。シード ファイルの種類にもよりますが、Model Derivative API&#0160; には SVF/SVF2 以外のファイルにも変換可能なファイル形式が存在します。</p>
<p><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/" rel="noopener" target="_blank">POST job</a> エンドポイントで変換可能なファイル形式は、シード ファイルの種類毎に <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/supported-translations/" rel="noopener" target="_blank">Supported Translations </a>で確認することが出来ます。この中には、Revit プロジェクト ファイ（RVT ファイル）ルの DWG への変換も含まれます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c90c937200d-pi" style="display: inline;"><img alt="Md" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c90c937200d image-full img-responsive" src="/assets/image_118419.jpg" title="Md" /></a></p>
<hr />
<p><strong>RVT &gt;&gt; DWG 変換で設定と準備</strong></p>
<p>POST job エンドポイントで RVT-DWG 変換する際には、RVT ファイルに埋め込まれた DWG 書き出し設定が利用されます。この設定は「<strong>どのシートを変換対象とするか</strong>」と「<strong>どのように変換するか</strong>」の 2 つに分けて考えることが出来ます。このた、変換前には Revit 側で設定を適切に調整して 、RVT ファイルに保存しておく必要があります。</p>
<p><strong>どのシートを変換対象とするか：</strong></p>
<p style="padding-left: 40px;">この内容は、<a href="https://help.autodesk.com/view/RVT/2023/JPN/?guid=GUID-09FBF9E2-6ECF-447D-8FA8-12AB16495BC3" rel="noopener" target="_blank">パブリッシュ設定</a> ダイアログと同様の内容で、パブリッシュ セットを作成して、セット毎に対象となるシートを指定します。「セットを選択」で変換で使用するパブリッシュ セットにチェックします。<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/"></a></p>
<ul>
<li><strong>「平面系」 パブリッシュ セットで「シート:0901 - 配置図」を指定した例</strong></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a71c91200b-pi" style="display: inline;"><img alt="Publish_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a71c91200b img-responsive" src="/assets/image_505660.jpg" title="Publish_settings" /></a></p>
<p><strong>どのように変換するか：</strong></p>
<p style="padding-left: 40px;"><a href="https://help.autodesk.com/view/RVT/2023/JPN/?guid=GUID-A170C41E-7C8A-4D7D-A552-FBFB4BD604A0" rel="noopener" target="_blank">「DWG/DXF 書き出しの一般オプション</a>」中には、変換後のる DWG の構造を指定する「シートとリンクのビューを外部参照として書き出し」オプションと、DWG ファイル形式を指定する「ファイル形式に書き出し」オプションがあります。</p>
<p style="padding-left: 40px;">&#0160;[DWG 書き出し設定] ダイアログの [...] ボタンから [DWG/DXF 書き出し設定を修正] ダイアログを呼び出し、新しい書き出し設定を作成します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c9107fa200d-pi" style="display: inline;"><img alt="Dwg_export_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c9107fa200d image-full img-responsive" src="/assets/image_668279.jpg" title="Dwg_export_settings" /></a></p>
<p style="padding-left: 40px;">① の[新しい書き出し設定] ボタンをクリックして任意の設定名を入力したら（ここでは「設定 1」）、② でアクティブな書き出し設定になっていることを確認、③ の［一般］タブで ④ の「シートとリンクのビューを外部参照として書き出し」オプションと「ファイル形式に書き出し」オプションを指定、[OK] ボタンをクリックします。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a758f8200b-pi" style="display: inline;"><img alt="Link_options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a758f8200b image-full img-responsive" src="/assets/image_830877.jpg" title="Link_options" /></a></p>
<p style="padding-left: 40px;">[DWG 書き出し設定] ダイアログに戻ったら、まず、① でアクティブな設定が適切な書き出し設定になっていることを確認して（ここでは「設定 1」）、② の [セットを保存して閉じる] ボタンで作成した設定情報を RVT ファイルに埋め込みます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c910b36200d-pi" style="display: inline;"><img alt="Save_dwg_export_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c910b36200d image-full img-responsive" src="/assets/image_768716.jpg" title="Save_dwg_export_settings" /></a></p>
<p>この状態の RVT ファイルを保存して OSS Bucket にアップロード、Model Derivative API の <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/" rel="noopener" target="_blank">POST job</a> エンドポイントで DWG に変換することになります。</p>
<p>POST job エンドポイント実行時には、advanced オプションを使って RVT ファイルに埋め込まれた書き出し設定名を指定します。（<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/">POST Start Translation Job</a>&#0160;の「Attributes that Apply to DWG Outputs」項参照）<strong><br /></strong></p>
<ul>
<li><strong>「設定 1」書き出し設定を指定した JSON ペイロード例</strong><br />
<pre>{
    &quot;input&quot;: {
        &quot;urn&quot;: &quot;<strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVTのURN&gt;&gt;</span></span></strong>&quot;
    },
    &quot;output&quot;: {
        &quot;formats&quot;: [
            {
                &quot;type&quot;: &quot;dwg&quot;,
                &quot;views&quot;: [
                    &quot;2d&quot;
                ],
                &quot;advanced&quot;: {
                    &quot;exportSettingName&quot;: &quot;<strong>設定 1</strong>&quot;
                }
            }
        ]
    }
}</pre>
</li>
<li>RVT ファイルに指定した書き出し設定名が見つからないと、RVT ファイルでアクティブになっている書き出し設定が使用されます。アクティブな書き出し設定とは、[DWG 書き出し設定] ダイアログの「書き出し設定を選択」に表示されている書き出し設定を指します。</li>
</ul>
<hr />
<p><strong>「シートとリンクのビューを外部参照として書き出し」オプション有効時の変換動作（既定の変換動作）：</strong></p>
<p>「シートとリンクのビューを外部参照として書き出し」オプションにチェックを入れた書き出し設定の場合、Model Derivative API は、Revit シート上のビュー毎に DWG を作成し、AutoCAD の外部参照として親図面から参照するよう変換します。変換が完了すると、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/manifest/urn-manifest-GET/" rel="noopener" target="_blank">GET {urn}/manifest</a> エンドポイントから次のようなマニフェストを取得出来るはずです。</p>
<pre> {
    &quot;urn&quot;: &quot;<strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVTのURN&gt;&gt;</span></span></strong>&quot;,
    &quot;derivatives&quot;: [
        {
            &quot;children&quot;: [
                {
                    &quot;urn&quot;: &quot;<strong><span style="background-color: #0000ff; color: #ffffff;">&lt;&lt;変換後の子DWGのURN&gt;&gt;（...0901 - 配置図-平面図 - 配置図.dwg）</span></strong>&quot;,
                    &quot;role&quot;: &quot;dwg&quot;,
                    &quot;mime&quot;: &quot;application/vnd.autodesk.autocad.dwg&quot;,
                    &quot;guid&quot;: &quot;97d0b9b3-6b92-7d90-94ed-07c570454a93&quot;,
                    &quot;type&quot;: &quot;resource&quot;,
                    &quot;status&quot;: &quot;success&quot;
                },
                {
                    &quot;urn&quot;: &quot;<strong><span style="background-color: #0000ff; color: #ffffff;">&lt;&lt;変換後の親DWGのURN&gt;&gt;（...</span></strong><span style="background-color: #0000ff; color: #ffffff;"><strong>0901 - 配置図.dwg）</strong></span>&quot;,
                    &quot;role&quot;: &quot;dwg&quot;,
                    &quot;mime&quot;: &quot;application/vnd.autodesk.autocad.dwg&quot;,
                    &quot;guid&quot;: &quot;312d29b3-61b9-673d-995b-0c4ccc8e44ca&quot;,
                    &quot;type&quot;: &quot;resource&quot;,
                    &quot;status&quot;: &quot;success&quot;
                }
            ],
            &quot;progress&quot;: &quot;complete&quot;,
            &quot;outputType&quot;: &quot;dwg&quot;,
            &quot;status&quot;: &quot;success&quot;
        }
    ],
    &quot;hasThumbnail&quot;: &quot;true&quot;,
    &quot;messages&quot;: [
        {
            &quot;type&quot;: &quot;warning&quot;,
            &quot;code&quot;: &quot;Revit-MissingLink&quot;,
            &quot;message&quot;: [
                &quot;＜message＞Missing link files: ＜ul＞{0}＜/ul＞＜/message＞&quot;,
                &quot;サンプル構造.rvt&quot;
            ]
        }
    ],
    &quot;progress&quot;: &quot;complete&quot;,
    &quot;type&quot;: &quot;manifest&quot;,
    &quot;region&quot;: &quot;US&quot;,
    &quot;version&quot;: &quot;1.0&quot;,
    &quot;status&quot;: &quot;success&quot;
}</pre>
<p>ここでは、「平面系」セットで指定されている <strong>0901 - 配置図.dwg</strong> と、0901 - 配置図.dwg 内に配置されてる「配置図-平面図」ビューが <strong>0901 - 配置図-平面図 - 配置図.dwg</strong> として作成されて、0901 - 配置図.dwg が 0901 - 配置図-平面図 - 配置図.dwg を外部参照するかたちで変換が実施されます。</p>
<ul style="list-style-type: circle;">
<li>マニフェストには、変換された DWG がリストされるので、記載されるパス（Model Derivative API の Step by Step Tutorials にある <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/tutorials/translate-to-obj/" rel="noopener" target="_blank">Translate a Source File</a>&#0160;の <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/tutorials/translate-to-obj/task4-download-obj-file/" rel="noopener" target="_blank">Task 4 - Download OBJ File</a> では &lt;URL_SAFE_URN_OF_SOURCE_FILE&gt; と &lt;URN_OF_OBJ_FILE&gt; と表記）を使ってダウンロードをすることになります。&lt;URL_SAFE_URN_OF_SOURCE_FILE&gt; は <strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVT のURN&gt;&gt;</span></span></strong> 、&lt;URN_OF_OBJ_FILE&gt; は URL エンコードされた変換済の URN です。</li>
<li>変換後の DWG の URL は <a href="https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0" rel="noopener" target="_blank">URL エンコード</a>していない状態で、urn:adsk.viewing:fs.file:<strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVT のURN&gt;&gt;</span></span></strong>/output/Resource/シート/0901 - 配置図 42370965/dwg/0901 - 配置図.dwg ですが、実際には urn%3Aadsk.viewing%3Afs.file%3A<strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVT のURN&gt;&gt;</span></span></strong>%2Foutput%2FResource%2F%E3%82%B7%E3%83%BC%E3%83%88%2F0901%20-%20%E9%85%8D%E7%BD%AE%E5%9B%B3%2042370965%2Fdwg%2F0901%20-%20%E9%85%8D%E7%BD%AE%E5%9B%B3.dwg のようになります。URN の Bucket Key にあたる部分が <strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVTのURN&gt;&gt;</span></span></strong> の値になっていることに注意してください。</li>
</ul>
<hr />
<p><strong>「シートとリンクのビューを外部参照として書き出し」オプション無効時の変換動作：</strong></p>
<p>「シートとリンクのビューを外部参照として書き出し」オプションにチェックを外した書き出し設定の場合、Model Derivative API は、Revit シート上のビューの内容をすべて DWG にバインドします。ビュー毎に外部参照 DWG を作成しなくなるので、、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/manifest/urn-manifest-GET/" rel="noopener" target="_blank">GET {urn}/manifest</a> エンドポイントから得られるマニフェストはシンプルなものになります。先の例と同じパブリッシュ セットを変換した場合、次のようなマニフェストになるはずです。</p>
<pre>{
    &quot;urn&quot;: &quot;<strong><span style="background-color: #0000ff; color: #ffffff;"><span style="background-color: #ff0000;">&lt;&lt;RVTのURN&gt;&gt;</span></span></strong>&quot;: [
        {
            &quot;children&quot;: [
                {
                    &quot;urn&quot;: &quot;<strong><span style="background-color: #0000ff; color: #ffffff;">&lt;&lt;変換後のDWGのURN&gt;&gt;（...</span></strong><span style="background-color: #0000ff; color: #ffffff;"><strong>0901 - 配置図.dwg）</strong></span>&quot;,
                    &quot;role&quot;: &quot;dwg&quot;,
                    &quot;mime&quot;: &quot;application/vnd.autodesk.autocad.dwg&quot;,
                    &quot;guid&quot;: &quot;312d29b3-61b9-673d-995b-0c4ccc8e44ca&quot;,
                    &quot;type&quot;: &quot;resource&quot;,
                    &quot;status&quot;: &quot;success&quot;
                }
            ],
            &quot;progress&quot;: &quot;complete&quot;,
            &quot;outputType&quot;: &quot;dwg&quot;,
            &quot;status&quot;: &quot;success&quot;
        }
    ],
    &quot;hasThumbnail&quot;: &quot;true&quot;,
    &quot;messages&quot;: [
        {
            &quot;type&quot;: &quot;warning&quot;,
            &quot;code&quot;: &quot;Revit-MissingLink&quot;,
                &quot;＜message＞Missing link files: ＜ul＞{0}＜/ul＞＜/message＞&quot;,
                &quot;サンプル構造.rvt&quot;
            ]
        }
    ],
    &quot;progress&quot;: &quot;complete&quot;,
    &quot;type&quot;: &quot;manifest&quot;,
    &quot;region&quot;: &quot;US&quot;,
    &quot;version&quot;: &quot;1.0&quot;,
    &quot;status&quot;: &quot;success&quot;
}</pre>
<hr />
<p><strong>ダウンロード</strong></p>
<p>変換された DWG ファイルのダウンロード方法は、Model Derivative API の Step by Step Tutorials にある <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/tutorials/translate-to-obj/" rel="noopener" target="_blank">Translate a Source File</a>&#0160;の <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/tutorials/translate-to-obj/task4-download-obj-file/" rel="noopener" target="_blank">Task 4 - Download OBJ File</a> に準拠します。この箇所の POSTMAN コレクションの説明は、<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fgithub.com%2FAutodesk-Forge%2Fforge-tutorial-postman%2Fblob%2Fmaster%2FModelDerivative_01%2Finstructions%2Ftask-4.md&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C7963654908cf4f04fbb508dacde6de60%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638048688774549933%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=vy8dBd6aOHpVoyZuBQDqdH2bgWx0zmmTG5AV%2FkHEcV4%3D&amp;reserved=0" rel="noopener" target="_blank">forge-tutorial-postman/task-4.md at master · Autodesk-Forge/forge-tutorial-postman · GitHub</a>&#0160;から参照することが出来ます。</p>
<p>変換された DWG の実際のダウンロードは少し煩雑です。ダウンロードには、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeUrn-signedcookies-GET/" rel="noopener" target="_blank">GET {urn}/manifest/{derivativeurn}/signedcookies</a> エンドポイントと使って、署名付き URL と同 URL 実行に必要な署名付きクッキーを取得する必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a72261200b-pi" style="display: inline;"><img alt="GET urn-manifest-derivativeurn-signedcookies" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a72261200b image-full img-responsive" src="/assets/image_284843.jpg" title="GET urn-manifest-derivativeurn-signedcookies" /></a></p>
<p>署名付き URL は <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeUrn-signedcookies-GET/" rel="noopener" target="_blank">GET {urn}/manifest/{derivativeurn}/signedcookies</a> エンドポイントのレスポンス Body に、署名付きクッキーはレスポンス Header に返されます。</p>
<p>署名付き URL の行使時には、Header に <strong>Cookie</strong> のキー名を作成し、値には <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeUrn-signedcookies-GET/" rel="noopener" target="_blank">GET {urn}/manifest/{derivativeurn}/signedcookies</a> エンドポイントのレスポンス Header に返された 3 つの Set-Cookie の値を順位にセミコロン <strong>;</strong> で区切ったものを指定します。（CloudFront-Policy= ... HTTPOnly<strong>;</strong>CloudFront-Key-Pair-Id=... HTTPOnly<strong>;</strong>CloudFront-Signature=... HTTPOnly）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a72311200b-pi" style="display: inline;"><img alt="Download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a72311200b image-full img-responsive" src="/assets/image_761677.jpg" title="Download" /></a></p>
<p>「シートとリンクのビューを外部参照として書き出し」オプションが有効な変換の場合、ビュー毎に作成されたすべての DWG をダウンロードしたら、同じパス上に配置して親図面となる DWG を AutoCAD で開くと、その内容を確認することが出来ます。（ここまでの例では親図面は <strong>0901 - 配置図.dwg</strong>）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c910ca9200d-pi" style="display: inline;"><img alt="Translate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c910ca9200d image-full img-responsive" src="/assets/image_436089.jpg" title="Translate" /></a></p>
<hr />
<p><strong>制限</strong></p>
<ul>
<li>Model Derivative API の RVT-DWG 変換では、リンク情報に制限があります。もし、2D シート内のラスター画像がアタッチされていると、既定では、ビューと同じく外部参照 DWG を作成して親図面から参照するように変換します。具体的には、アタッチされたラスター画像毎に DWG が作成されて、親図面は作成された DWG を参照することになります。<br /><br />変換対象に RVT ファイルが「シートとリンクのビューを外部参照として書き出し」オプションが無効な RVT ファイルの場合は、ラスター画像毎の外部参照 DWG は作成されません。<br /><br />ただし、<span style="text-decoration: underline;">いずれの場合も</span>、マニフェストには実際のラスター画像ファイルがリストされないため、画像ファイルをダウンロード出来ません。このため、AutoCAD でダウンロードした親図面を開くと外部参照エラーになってしまいます。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c90d3b7200d-pi" style="display: inline;"><img alt="Missing_xref" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c90d3b7200d image-full img-responsive" src="/assets/image_317984.jpg" title="Missing_xref" /></a></li>
</ul>
<ul>
<li>AutoCADでは図面に貼り付けたラスターイメージを DWG 内に埋め込むバインドの機能がありません。<br />参考：<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fknowledge.autodesk.com%2Fja%2Fsupport%2Fautocad%2Flearn-explore%2Fcaas%2Fsfdcarticles%2Fsfdcarticles%2FkA93g0000000Rp0.html&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C7963654908cf4f04fbb508dacde6de60%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638048688774549933%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=1VbMXDqMQn5xfVgg8O16kuNehV92luF9wLnaav2i1zQ%3D&amp;reserved=0">AutoCADでは外部参照でアタッチしたイメージデータをバインド出来ないのか | AutoCAD | Autodesk Knowledge Network</a></li>
<li>表題欄などに埋め込んだロゴなどのラスター画像には「シートとリンクのビューを外部参照として書き出し」オプション内容にかかわらず外部参照 DWG は作成されませんが、マニフェストには同ラスター画像はリストされません。</li>
<li>Model Derivative API の RVT-DWG 変換は、2D シートのみサポートされます。3D は変換出来ません。</li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>

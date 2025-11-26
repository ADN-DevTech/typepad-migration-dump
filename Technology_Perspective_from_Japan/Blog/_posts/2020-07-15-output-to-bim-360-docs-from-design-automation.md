---
layout: "post"
title: "Design Automation から BIM 360 Docs への出力"
date: "2020-07-15 00:18:54"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/07/output-to-bim-360-docs-from-design-automation.html "
typepad_basename: "output-to-bim-360-docs-from-design-automation"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9566d67200b-pi" style="display: inline;"></a>D<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e89df6200d-pi" style="float: right;"><img alt="Image2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e89df6200d img-responsive" src="/assets/image_330156.jpg" style="margin: 0px 0px 5px 5px;" title="Image2" /></a>esign Automation API を使って生成した成果ファイルを、BIM 360 Docs など、オートデスク SaaS が使っているストレージに保存（Design Automation から見てアップロード）する際には、少し注意が必要です。</p>
<p>オートデスク SaaS ストレージへファイルをアップロードする方法は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/file-upload-to-autodesk-saas-storage.html" rel="noopener" target="_blank">オートデスク SaaS ストレージへのファイル アップロード</a></strong> のブログ記事でもご案内しているとおりです。</p>
<p>BIM 360 Docs ストレージへのアクセスには、BIM 360 Docs ユーザの認可を求める 3-legged OAuth を使った Access Token と、スーパーユーザとしてプロジェクト間の横断的なアクセスを可能とする 2-legged OAuth を使った Access Token を、用途や目的に合わせて使い分けることが出来ます。ここでは、アクセスにユーザ認可を得るのではなく。プロジェクトを横断的にアクセスする Forge アプリが Design Automation API を使って成果ファイルをアップロードするシナリオを考えていきます。</p>
<p>Design Automation API でアップロードする際、Activity を次のリクエスト ボディ（一部抜粋）のように登録したと仮定します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    “parameters”: {
        &quot;DWGInput&quot;: {
            &quot;zip&quot;: false,
            &quot;ondemand&quot;: false,
            &quot;verb&quot;: &quot;get&quot;,
            &quot;description&quot;: &quot;Source drawing&quot;,
            &quot;required&quot;: true
        },
        &quot;PDFOutput&quot;: {
            &quot;zip&quot;: false,
            &quot;ondemand&quot;: false,
            &quot;verb&quot;: &quot;put&quot;,
            &quot;description&quot;: &quot;Output PDF drawing&quot;,
            &quot;required&quot;: true,
            &quot;localName&quot;: &quot;result.pdf&quot;
        },
<span style="color: #0000ff;"><strong>        &quot;PDFOutputToBIM360&quot;: {
            &quot;zip&quot;: false,
            &quot;ondemand&quot;: false,
            &quot;verb&quot;: &quot;put&quot;,
            &quot;description&quot;: &quot;Upload PDF drawing to BIM 360 Docs&quot;,
            &quot;required&quot;: false,
            &quot;localName&quot;: &quot;result.pdf&quot;
        }</strong></span></code></pre>
<p>この Activity を使った WorkItem を次のように指定したとします。この際、<span style="background-color: #ffff00; color: #ff0000;"><strong>name</strong></span> の変数で保持するのは、Avtivity で指定した localName である result.pdf ではなく、result.pdf を格納するために WorkItem タスクの実行直前に <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-storage-POST/" rel="noopener" target="_blank">POST <strong>projects/:</strong><strong>project_id</strong><strong>/storage</strong></a> endpoint で登録した Storage 上の 出力ファイル名になります（同 endpoint のレスポンスボディが返す Storage ID から抽出した Object Name です）。例）48f8dcde-ebb2-4944-b191-34b66990aa93.pdf</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"> {
    &quot;activityId&quot;: DA4A_FQ_ID,
    &quot;arguments&quot;: {
                                       :
        &quot;DWGInput&quot;: {
                                       :
        },    	
        “PDFOutput&quot;: {
                                       :
        }
<span style="color: #0000ff;"><strong>        &quot;PDFOutputToBIM360&quot;: {
            &quot;url&quot;: &quot;https://developer.api.autodesk.com/oss/v2/buckets/wip.dm.prod/objects/&quot; + </strong></span><span style="color: #ff0000; background-color: #ffff00;"><strong>name</strong></span><span style="color: #0000ff;"><strong>,
            &quot;headers&quot;: {
                &quot;Authorization&quot;: &quot;Bearer xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx&quot;,
                &quot;Content-Type&quot;: &quot;application/octet-stream&quot;
            },
            &quot;verb&quot;: &#39;put&#39;
        },

</strong></span></code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>これで、WorkItem の実行時に BIM 360 Docs 上にファイルがアップロード（保存）されるようになります。ただし、まだ足りない部分があります。BIM 360 Docs をはじめとしたオートデスクの SaaS ストレージでは、アップロードされたファイルはバージョン管理されることになります。つまり、WorkItem によってアップロードされたファイルは、バージョン付けをしないと BIM 360 Docs のユーザインタフェースには表示されません。</p>
<p>BIM 360 Docs に Design Automation API の成果物であるファイルを表示するには、WorkItem 終了時に通知される onComplete コールバック URL を使用して、このバージョン付けの処理を実装します。onComplete コールバックで通知を得るためには、WorkItem の登録時に次ように指定します。<span style="color: #0000ff; background-color: #ffff00;">黄色</span>で反転した URL については後述します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"> {
    &quot;activityId&quot;: DA4A_FQ_ID,
    &quot;arguments&quot;: {
                                       :
        &quot;DWGInput&quot;: {
                                       :
        },    	
        “PDFOutput&quot;: {
                                       :
        }
<span style="color: #111111;">        &quot;PDFOutputToBIM360&quot;: {
            &quot;url&quot;: &quot;https://developer.api.autodesk.com/oss/v2/buckets/wip.dm.prod/objects/&quot; + name</span><span style="color: #0000ff;"><span style="color: #111111;">,
            &quot;headers&quot;: {
                &quot;Authorization&quot;: &quot;Bearer xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx&quot;,
                &quot;Content-Type&quot;: &quot;application/octet-stream&quot;
            },
            &quot;verb&quot;: &#39;put&#39;
        },
</span><strong>        &quot;onComplete&quot;: {
            &quot;verb&quot;: &quot;post&quot;,
            &quot;url&quot;: &quot;<span style="background-color: #ffff00;">http://4db4af1c.ngrok.io/</span>onComplete&quot;
        }

</strong></span></code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>もちろん、同じ Web サーバー実装で onComplete コールバック URL の<a href="https://expressjs.com/ja/guide/routing.html" rel="noopener" target="_blank">ルーティング</a>実装が必要です。次のコードは、Node.js を使って <a href="https://expressjs.com/ja/" rel="noopener" target="_blank">express</a> パッケージ（ミドルウェア）の router インスタンスをしたコールバック URL の実装例です。&#0160;<a class="reference external" href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-items-POST" rel="noopener" target="_blank">POST projects/:project_id/items</a> endpoint を使って、アップロードした Item（ファイル）に最初のバージョン（Version 1）を登録しています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">// onComplete callback
router.post(&quot;/onComplete&quot;, function (req, res) {

    console.log(&quot;**** onComplete callback was invoked !!&quot;);

    oAuth2TwoLegged.authenticate().then(function (credentials) {

        var payload =
        {
            &quot;jsonapi&quot;: {
                &quot;version&quot;: &quot;1.0&quot;
            },
            &quot;data&quot;: {
                &quot;type&quot;: &quot;items&quot;,
                &quot;attributes&quot;: {
                    &quot;displayName&quot;: RESULT_PDF,
                    &quot;extension&quot;: {
                        &quot;type&quot;: &quot;items:autodesk.bim360:File&quot;,
                        &quot;version&quot;: &quot;1.0&quot;
                    }
                },
                &quot;relationships&quot;: {
                    &quot;tip&quot;: {
                        &quot;data&quot;: {
                            &quot;type&quot;: &quot;versions&quot;,
                            &quot;id&quot;: &quot;1&quot;
                        }
                    },
                    &quot;parent&quot;: {
                        &quot;data&quot;: {
                            &quot;type&quot;: &quot;folders&quot;,
                            &quot;id&quot;: FOLDER_ID
                        }
                    }
                }
            },
            &quot;included&quot;: [
                {
                    &quot;type&quot;: &quot;versions&quot;,
                    &quot;id&quot;: &quot;1&quot;,
                    &quot;attributes&quot;: {
                        &quot;name&quot;: RESULT_PDF,
                        &quot;extension&quot;: {
                            &quot;type&quot;: &quot;versions:autodesk.bim360:File&quot;,
                            &quot;version&quot;: &quot;1.0&quot;
                        }
                    },
                    &quot;relationships&quot;: {
                        &quot;storage&quot;: {
                            &quot;data&quot;: {
                                &quot;type&quot;: &quot;objects&quot;,
                                &quot;id&quot;: STORAGE_ID
                            }
                        }
                    }
                }
            ]
        };

        uri = &quot;https://developer.api.autodesk.com/data/v1/projects/&quot; + PROJECT_ID + &quot;/items&quot;;
        request.post({
            url: uri,
            headers: {
                &#39;content-type&#39;: &#39;application/vnd.api+json&#39;,
                &#39;authorization&#39;: &#39;Bearer &#39; + credentials.access_token
            },
            body: JSON.stringify(payload)
        }, function (error, versionres, body) {
            var data = JSON.stringify(versionres);
            if (JSON.parse(data).statusCode === 201) {
                console.log(&quot; &quot; + STORAGE_ID + &quot;&#39;s version 1 was created&quot;);
            } else {
                console.log(&quot;Error : &quot; + JSON.stringify(JSON.parse(body).errors));
            }
        });

    }, defaultHandleError);

});

</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>ここまでの実装で、Design Automation の WorkItem から BIM 360 Docs に成果物となるファイルをアップロードして、バージョン付けすることが出来ます。</p>
<p>ただ、上記の例では、PDF ファイルをアップロードしているので、実際には、PDF をパブリッシュする作業（シートの抽出）が必要です。残念ながら、この作業は、対応する endpoint がないので手動操作が必要となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e764ed200d-pi" style="float: right;"><img alt="Ngrok" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e764ed200d img-responsive" src="/assets/image_977181.jpg" style="margin: 0px 0px 5px 5px;" title="Ngrok" /></a>さて、最後に WorkItem のリクエストボディの JSON で触れた、<span style="color: #0000ff; background-color: #ffff00;">黄色</span>で反転した onComplete コールバック URL についてご案内しておきます。通常、onComplete コールバック URL には、Forge アプリのデプロイ後の URL を明記することになりますが、ここではデプロイ前のローカル PC 内で通知を得ることを想定して、ローカルに通知を得るために疑似的に URL を生成する <strong>ngrok</strong>&#0160;ユーティリティを使って、<code class="language-javascript code-overflow-x hljs " id="snippet-4"><span style="color: #0000ff;"><strong><span style="background-color: #ffff00;">http://4db4af1c.ngrok.io/</span></strong></span></code>名の URL を設定、WorkItem に指定しています。</p>
<p>ngrok については、同じくコールバック URL を使用する WebHooks API を題材としてブログ記事 <strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/12/local-development-environment-for-forge-webhooks-api.html" rel="noopener" target="_blank">Forge Webhooks API ローカル開発環境について</a>&#0160;</strong>でもご案内していますので、入手方法や使用方法については、そちらの記事をご確認ください。</p>
<p><strong>ngrok</strong> ユーティリティが生成する URL は、コマンドプロンプト上で ngrok.exe を実行している場合のみ有効です。また、ngrok は毎回異なる URL を生成するので、以前 ngrok が生成して利用した URL は、次のセッションでは利用できません。ご注意ください。</p>
<p>By Toshiaki Isezaki</p>

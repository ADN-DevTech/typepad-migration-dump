---
layout: "post"
title: "BIM 360 Docs へのアップロード ユーザの指定"
date: "2020-08-19 01:00:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/08/specify-user-who-uploads-to-bim-360-docs.html "
typepad_basename: "specify-user-who-uploads-to-bim-360-docs"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/bim-360-docs-and-forge-oauth.html" rel="noopener" target="_blank"><strong>BIM 360 Docs と Forge OAuth</strong></a> のブログ記事でもご案内していますとおり、BIM 360 Docs ストレージへのアクセスには、BIM 360 Docs ユーザの認可を求める 3-legged OAuth を使った Access Token と、スーパーユーザとしてプロジェクト間の横断的なアクセスを可能とする 2-legged OAuth を使った Access Token を、用途や目的に合わせて使い分けることが出来ます。</p>
<p>ただし、意図的に 2-legged OAuth を使った Access Token を使用しなければならないケースもあります。例えば、BIM 360 のすべてのプロジェクトを取得する <a class="reference external" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/projects-GET" rel="noopener" target="_blank">GET projects</a> endpoint を呼び出す場合、両 Token を使ったレスポンスは次のような結果となります。</p>
<p style="padding-left: 40px;"><strong>&lt;2-legged Token&gt;</strong><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e92e21200d-pi" style="display: inline;"><img alt="Get_projects_2_legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e92e21200d image-full img-responsive" src="/assets/image_346206.jpg" title="Get_projects_2_legged" /></a><br /><strong>&lt;3-legged Token&gt;</strong><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e956fc32200b-pi" style="display: inline;"><img alt="Get_projects_3_legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e956fc32200b image-full img-responsive" src="/assets/image_229091.jpg" title="Get_projects_3_legged" /></a></p>
<p>同様に、BIM 360 アカウントのすべてのユーザを取得する <a class="reference external" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/users-GET" rel="noopener" target="_blank">GET users</a> endpoint の場合、両 Token を使ったレスポンスは次のようになるはずです。</p>
<p style="padding-left: 40px;"><strong>&lt;2-legged Token&gt;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e956fc6b200b-pi" style="display: inline;"><img alt="Get_users_2_legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e956fc6b200b image-full img-responsive" src="/assets/image_143030.jpg" title="Get_users_2_legged" /></a><br /></strong><strong>&lt;3-legged Token&gt;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec289889200c-pi" style="display: inline;"><img alt="Get_users_3_legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec289889200c image-full img-responsive" src="/assets/image_383026.jpg" title="Get_users_3_legged" /></a><br /></strong></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/file-upload-to-autodesk-saas-storage.html" rel="noopener" target="_blank">プロジェクトに Item（ファイル）をアップロードして Version 登録（バージョン付け）する Forge アプリ</a> を考える場合、3-legged OAuth を使って BIM 360 Docs ユーザの認可を経て取得した Access Token を使うと、アップロードしたユーザとして、認可を与えたユーザ名がログに表示されることになります。</p>
<p>この時、2-legged Token&#0160; を使用すると、アップロードユーザが表示されない、言わば、匿名ユーザとしてファイル アップロードした状態になってしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e9343b200d-pi" style="display: inline;"><img alt="Uploader1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e9343b200d image-full img-responsive" src="/assets/image_548944.jpg" title="Uploader1" /></a></p>
<p>2-legged Token でアップロード ユーザを明示的にログしたい場合には、Stotage 作成、ファイルアップロード、バージョン付けをおこなう endpoint 呼び出し時に、’<strong>x-user-id</strong>’ をリクエスト ヘッダーに追加して、前述の <a class="reference external" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/users-GET" rel="noopener" target="_blank">GET users</a> endpoint で取得した &#39;<strong>uid</strong>&#39; の値を指定してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95701aa200b-pi" style="display: inline;"><img alt="Uid" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e95701aa200b image-full img-responsive" src="/assets/image_933083.jpg" title="Uid" /></a></p>
<p>次のコードは、 アップロードした Item に、<a class="reference external" href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-items-POST" rel="noopener" target="_blank">POST projects/:project_id/items</a> endpoint を使って最初のバージョンを登録する際の例です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">                                            ：
        uri = &quot;https://developer.api.autodesk.com/data/v1/projects/&quot; + PROJECT_ID + &quot;/items&quot;;
        request.post({
            url: uri,
            headers: {
                &#39;content-type&#39;: &#39;application/vnd.api+json&#39;,
                <strong>&#39;x-user-id&#39;: &#39;200708030638689&#39;,</strong> // uid
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
<p>この方法で、2-legged Token を使用した場合でも、アップロードしたユーザをログすることが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e93463200d-pi" style="display: inline;"><img alt="Uploader2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e93463200d image-full img-responsive" src="/assets/image_385081.jpg" title="Uploader2" /></a></p>
<p>By Toshiaki Isezaki</p>

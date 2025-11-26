---
layout: "post"
title: "Windows での cURL 使用について"
date: "2024-09-25 00:02:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/09/use-of-curl-on-windows.html "
typepad_basename: "use-of-curl-on-windows"
typepad_status: "Publish"
---

<p>APS のエンドポイントを説明する <a href="https://aps.autodesk.com/developer/documentation" rel="noopener" target="_blank">ドキュメント</a>では、リクエスト ヘッダーやリクエスト ボディを同時に確認出来る <a href="https://ja.wikipedia.org/wiki/CURL" rel="noopener" target="_blank">cURL</a> を使った例を多数記載しています。Windows 10 以降、cURL が<a href="https://curl.se/windows/microsoft.html" rel="noopener" target="_blank">標準で搭載</a>されて、すぐにコマンド プロンプト（cmd.exe）で使える環境が整っていることもあり、cURL でエンドポイントをテストしようとする方が増えているようです。</p>
<p>ただ、実際にコマンド プロンプトでエンドポイントを呼び出そうとすると、実行時に構文エラーになってしまい、うまくテスト出来ない状態になってしまうかもしれません。よくあるのは、リクエストで指定する JSON ボディで、キー名と値に使う <span style="font-family: monospace, arial, helvetica, sans-serif;"><strong>”</strong></span>（ダブル クォーテーション）が正しく認識されない、というものです。</p>
<p>ダブル クォーテーションを正しく認識させるためのは、<span style="font-family: monospace, arial, helvetica, sans-serif;"><strong>&quot;</strong></span> の前に <span style="font-family: tahoma, arial, helvetica, sans-serif;"><strong>\</strong><span style="font-family: &#39;monospace&#39;;">（使用</span></span>フォントにより円記号 ¥ で<span style="font-family: &#39;monospace&#39;;">表示</span><span style="font-family: &#39;monospace&#39;;">）</span> のエスケープ文字を配置する必要があります。改行を省いた状態で Model Derivative API の <a href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/" rel="noopener" target="_blank">POST job</a> エンドポイントを呼び出す場合、次のような記述が必要になります。</p>
<pre>curl --request POST --url https://developer.api.autodesk.com/modelderivative/v2/designdata/job --header &quot;Content-Type: application/json&quot; --header &quot;Authorization: Bearer <em>&lt;&lt; ACCESS TOKEN &gt;&gt;</em>&quot; --data &quot;{ \&quot;input\&quot;: { \&quot;urn\&quot;: \&quot;<em>&lt;&lt; URN &gt;&gt;</em>\&quot; }, \&quot;output\&quot;: { \&quot;formats\&quot;: [ { \&quot;type\&quot;: \&quot;svf2\&quot;, \&quot;views\&quot;: [ \&quot;2d\&quot;, \&quot;3d\&quot; ] } ] } }&quot;
</pre>
<p>原因はわかったものの、これではリクエストの JSON ボディの値を変更したり、オプションを追加したりするのが厄介です。</p>
<p>このような場合、JSON ボディを UTF-8 形式の .json ファイルとして保存、cURL 構文で同ファイルを参照する方法をとることで、煩雑さを低減することが可能です。先の例で使用した POST job エンドポイントの場合、本来の記述のまま、リクエスト ボディを保存することが出来ます。（ここでは <em>body.json</em>）</p>
<pre>{
    &quot;input&quot;: {
        &quot;urn&quot;: &quot;<em>&lt;&lt; URN &gt;&gt;</em>&quot;
    },
    &quot;output&quot;: {
        &quot;formats&quot;: [
            {
                &quot;type&quot;: &quot;svf2&quot;,
                &quot;views&quot;: [
                    &quot;2d&quot;,
                    &quot;3d&quot;
                ]
            }
        ]
    }
}
</pre>
<p>あとは cURL 構文で、<em>body.json</em> ファイルを参照するようパス指定するだけです。次の例では、cd コマンドを使って現在のフォルダ（カレント ディレクトリ）を <em>body.json</em> ファイルのあるフォルダに変更して実行することを想定しています。</p>
<pre>curl --request POST --url https://developer.api.autodesk.com/modelderivative/v2/designdata/job --header &quot;Content-Type: application/json&quot; --header &quot;Authorization: Bearer <em>&lt;&lt; ACCESS TOKEN &gt;&gt;</em>&quot; --data &quot;@body.json&quot;
</pre>
<p>こうすると、リクエスト ボディの変更も少し楽になるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d0fee0200d-pi" style="display: inline;"><img alt="Curl_on_cmd" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d0fee0200d image-full img-responsive" src="/assets/image_102295.jpg" title="Curl_on_cmd" /></a></p>
<p>エンドポイントのテスト・評価には、もちろん、ユーザー インターフェースを持つ <a href="https://www.postman.com/" rel="noopener" target="_blank">Postman</a> や <a href="https://insomnia.rest/" rel="noopener" target="_blank">Insomnia</a> のようなツールが使い易いのは言うまでもありません。</p>
<p>By Toshiaki Isezaki</p>

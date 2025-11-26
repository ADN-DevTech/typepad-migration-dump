---
layout: "post"
title: "Design Automation API：adskDebug オプション"
date: "2023-03-13 00:24:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-apiadskdebug-option.html "
typepad_basename: "design-automation-apiadskdebug-option"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751753c11200b-pi" style="display: inline;"><img alt="AdskDebug_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751753c11200b image-full img-responsive" src="/assets/image_247714.jpg" title="AdskDebug_option" /></a></p>
<p>Design Automation API を利用したタスクの実行単位を WorkItem と呼んでいます。WorkItem の実行時には、タスクを処理するための仮想環境がクラウドに動的に作成されて、WorkIem の実行終了とともに破棄されます。</p>
<p>なんらかの要因で WorkItem の実行が失敗してしまった場合、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> エンドポイントで得られる JSON レスポンスや、同レスポンスにあるレポートログ（report.txt）の URL を取得、ダウンロードして原因を特定していくのが一般的です。</p>
<p>ただ、レポートログの参照のみでは、仮想環境上で使用、あるいは、生成/編集されたファイルを物理的に入手して調べることが出来ません。WorkItem が使用したファイルを入手出来れば、内容を解析する手助けにもなります。</p>
<p>このような場面では、昨年追加された <strong>adskDebug オプション</strong>を使用することが出来ます。</p>
<p>WorkItem 呼び出し時に <a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/" rel="noopener" target="_blank">POST workitems</a> エンドポイントのリクエストボディ（ JSON ペイロード）に次のように指定すると、WorkIem 実行時に使用した作業フォルダ（<em>T:\Aces\Jobs\3dd248b6f8d14946b670a30362e2ddbd</em> フォルダのような）の内容を ZIP 圧縮して、ダウンロードするための URL がレポートログに追記されるようになります。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; // Create WorkItem</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; var payload =</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160;&quot;activityId&quot;: &quot;MyDAapp.TestHarness+dev&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160;&quot;arguments&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Input&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/&quot; + SOURCE_NAME,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;headers&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;},</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;url&quot;: &quot;data:application/json,&quot; + paramsJSON</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Output&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/&quot; + RESULT_NAME,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;headers&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;},</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;verb&quot;: &#39;put&#39;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="background-color: #ffff00; font-size: 10pt;"><strong>&#0160; &#0160; &#0160; &#0160; &quot;adskDebug&quot;: {</strong></span></div>
<div><span style="background-color: #ffff00; font-size: 10pt;"><strong>&#0160; &#0160; &#0160; &#0160; &#0160;&quot;uploadJobFolder&quot;: true</strong></span></div>
<div><span style="background-color: #ffff00; font-size: 10pt;"><strong>&#0160; &#0160; &#0160; &#0160; },</strong></span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;onComplete&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;verb&quot;: &quot;post&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160;&quot;url&quot;: &quot;http://dummy/api/oncomplete&quot;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160;}</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; };</span></div>
</blockquote>
</div>
<p><a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> エンドポイントでレスポンスを得ると、 &quot;debugInfoUrl&quot; から ZIP ファイルのダウンロード URL を得ることが出来ます。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt;">...</span></div>
<div><span style="font-size: 10pt;">&#0160; {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &quot;status&quot;: &quot;failedInstructions&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &quot;reportUrl&quot;: &quot;https://dasprod-store.s3.amazonaws.com/workItem/&lt;&lt;長いので省略&gt;&gt;&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; <span style="background-color: #ffff00;">&quot;debugInfoUrl&quot;: &quot;https://dasprod-store.s3.amazonaws.com/workItem/&lt;&lt;長いので省略&gt;&gt;&quot;</span>,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &quot;stats&quot;: {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;timeQueued&quot;: &quot;2023-03-07T01:16:31.7905734Z&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;timeDownloadStarted&quot;: &quot;2023-03-07T01:16:31.9004055Z&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;timeInstructionsStarted&quot;: &quot;2023-03-07T01:16:32.7135692Z&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;timeInstructionsEnded&quot;: &quot;2023-03-07T01:16:37.7145756Z&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;timeUploadEnded&quot;: &quot;2023-03-07T01:16:38.2854694Z&quot;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;bytesDownloaded&quot;: 2560980,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &quot;bytesUploaded&quot;: 427790</span></div>
<div><span style="font-size: 10pt;">&#0160; },</span></div>
<div><span style="font-size: 10pt;">&#0160; &quot;id&quot;: &quot;3dd248b6f8d14946bd70a30362e2ddbd&quot;</span></div>
<div><span style="font-size: 10pt;">}</span></div>
</blockquote>
</div>
<p>ダウンロードした ZIP ファイルには、既に破棄されている仮想環境の作業フォルダのファイルとローミング フォルダが含まれます。Design Automation API for Revit の場合には、<a href="https://help.autodesk.com/view/RVT/2023/JPN/?guid=GUID-477C6854-2724-4B5D-8B95-9657B636C48D" rel="noopener" target="_blank">ジャーナル </a>を見つけて調査に利用することが可能です。</p>
<ul style="list-style-type: circle;">
<li><a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> エンドポイントが返すレポートログのダウンロード URL（&quot;reportUrl&quot; 値）の有効期間は 1 時間です。</li>
<li>adskDebug オプションで用意される ZIP ファイルのダウンロード URL は、エンジン毎にシステム内部で設定されるMaxAllowedPendingTime 値（非公開）＋3600 秒です。</li>
<li>adskDebug オプションの利用はデバッグ用途に限定して利用してください。デプロイした本番環境での利用は無意味です。</li>
</ul>
<p>By Toshiaki Isezaki</p>

---
layout: "post"
title: "Design Automation：adskMask オプション"
date: "2024-10-09 00:02:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/09/desigm-automation-adskmask-option.html "
typepad_basename: "desigm-automation-adskmask-option"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d5a0aa200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d5a0aa200b image-full img-responsive" src="/assets/image_558016.jpg" title="Aps" /></a></p>
<p>Design Automation API には、<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-apiadskdebug-option.html" rel="noopener" target="_blank">adskDebug オプション</a> の他にもデバッグ時の作業を意識した WorkItem 作成時のオプションがあります。</p>
<p>WorkItem 呼び出し時に&#0160;<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/" rel="noopener" target="_blank">POST workitems</a> エンドポイントのリクエストボディ（JSON ペイロード）に次のように指定すると、レポート ログ内に記載される Signed URL（署名付き URL） をマスクするようになります。</p>
<pre>{
    &quot;activityId&quot;: DA_FQ_ID,
    &quot;arguments&quot;: {
        &quot;DWGInput&quot;: {
            &quot;url&quot;: signedURLforInput,
            &quot;headers&quot;: {
                &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token
            },
            &quot;verb&quot;: &quot;get&quot;
        },
        &quot;Params&quot;: {
            &quot;url&quot;: &quot;data:application/json,&quot; + paramsJSON
        },
        &quot;FontMap&quot;: {
            &quot;url&quot;: signedURLforInput2,
            &quot;headers&quot;: {
                &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token
            },
            &quot;verb&quot;: &quot;get&quot;
        },
        &quot;PDFOutput&quot;: {
            &quot;url&quot;: signedURLforOutput,
            &quot;headers&quot;: {
                &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token
            },
            &quot;verb&quot;: &#39;put&#39;
        },
        &quot;adskDebug&quot;: {
            &quot;uploadJobFolder&quot;: true
        },
<span style="color: #0000ff;"><strong>        &quot;adskMask&quot;: true,</strong></span>
        &quot;onComplete&quot;: {
            &quot;verb&quot;: &quot;post&quot;,
            &quot;url&quot;: &quot;https://myapp.com/api/oncomplete&quot;
        }
    }
};
</pre>
<p>このようなペイロードを持つ WorkItem を実行すると、次のようなレポートログが生成されるようになります。</p>
<pre>...
[10/05/2024 06:39:29] Starting work item 0fe44d455d8746ac85abeed514b9521a
[10/05/2024 06:39:29] Start download phase.
[10/05/2024 06:39:29] Start downloading input: verb - &#39;Get&#39;, url - &#39;<strong>https://cdn.us.oss.api.autodesk.com/</strong><span style="background-color: #ffff00;"><strong>Masked:C0umf7GG5di2OuyT/L1zlXpgk20=</strong></span>&#39;
[10/05/2024 06:39:29] Embedded resource [{&quot;color&quot;:&quot;1&quot;,&quot;quantity&quot;:&quot;1&quot;,&quot;leaf&quot;:&quot;true&quot;,&quot;width&quot;:&quot;1639.25&quot;,&quot;height&quot;:&quot;755.2&quot;,&quot;viewport:{\&quot;name\&quot;:\&quot;\&quot;,\&quot;eye\&quot;:[287.0944770146143,-648.0526370471215,0],\&quot;target\&quot;:[0,0,0],\&quot;up\&quot;:[0,0,1],\&quot;worldUpVector\&quot;:[0,0,1],\&quot;pivotPoint\&quot;:[0,0,0],\&quot;distanceToOrbit\&quot;:708.7986026481874,\&quot;aspectRatio\&quot;:2.1706215459108913,\&quot;projection\&quot;:\&quot;perspective\&quot;,\&quot;isOrthographic\&quot;:false,\&quot;fieldOfView\&quot;:39.59775192067671}&quot;:&quot;&quot;}] is saved as a file in &#39;Unicode&#39; at: &#39;C:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\params.json&#39;.
[10/05/2024 06:39:29] Start downloading input: verb - &#39;Get&#39;, url - &#39;<strong>https://cdn.us.oss.api.autodesk.com/<span style="background-color: #ffff00;">Masked:y4DETvQs40/PsG8P8M9RnLN27ik=</span></strong>&#39;
[10/05/2024 06:39:29] End downloading file. Source=https://cdn.us.oss.api.autodesk.com/Masked:1CHAxLw4wvst4z9tTrJPbFr+65E=:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\template.dwg,BytesDownloaded=2560555,Duration=352ms
[10/05/2024 06:39:30] End downloading file. Source=https://cdn.us.oss.api.autodesk.com/Masked:9Wsya8LrKMHLKE1hfBcM6QfYVbY=:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\dwg.fmp,BytesDownloaded=27,Duration=920ms
[10/05/2024 06:39:30] End download phase successfully.
[10/05/2024 06:39:30] Start preparing script and command line parameters.
[10/05/2024 06:39:30] Command line: [ /i &quot;C:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\template.dwg&quot; /dwgfontmap /al &quot;C:\DARoot\Applications\1442b4fbcf90fd28c5bfd2e255cfa6a4.AjFukUWeRk05eA9XpH8Nnh62BzPD60mg.TableFanConfigurator[1].package&quot; /s C:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\setting_script.scr /fonts &quot;C:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\fonts_3380.txt&quot;]
[10/05/2024 06:39:30] End preparing script and command line parameters.
[10/05/2024 06:39:30] Start script phase.
...
...
...
[10/05/2024 06:39:37] End script phase.
[10/05/2024 06:39:37] Start upload phase.
[10/05/2024 06:39:37] Uploading &#39;C:\DARoot\Jobs\0fe44d455d8746ac85abeed514b9521a\quotation.pdf&#39;: verb - &#39;Put&#39;, url - &#39;<strong>https://cdn.us.oss.api.autodesk.com/<span style="background-color: #ffff00;">Masked:U18NF6Hds/WrhlRw7OLh5RNCvys=</span></strong>&#39;
[10/05/2024 06:39:38] End upload phase successfully.
[10/05/2024 06:39:38] Job finished with result Succeeded
[10/05/2024 06:39:38] Job Status:
{
  &quot;status&quot;: &quot;success&quot;,
  &quot;reportUrl&quot;: &quot;<strong>https://dasprod-store.s3.amazonaws.com/<span style="background-color: #ffff00;">Masked:DG6Ohh9ip90FoU0G/0Mcape0/XI=</span></strong>&quot;,
  &quot;debugInfoUrl&quot;: &quot;<strong>https://dasprod-store.s3.amazonaws.com/<span style="background-color: #ffff00;">Masked:BIYKOEYVWm3LqZ4fqrtAufLxJBE=</span></strong>&quot;,
  &quot;activityId&quot;: &quot;AjFukUWeRk05eA9XpH8Nnh62BzPD60mg.TableFanConfigurator+dev&quot;,
  &quot;stats&quot;: {
    &quot;timeQueued&quot;: &quot;2024-10-05T06:39:28.9311799Z&quot;,
    &quot;timeDownloadStarted&quot;: &quot;2024-10-05T06:39:29.0093762Z&quot;,
    &quot;timeInstructionsStarted&quot;: &quot;2024-10-05T06:39:30.207032Z&quot;,
    &quot;timeInstructionsEnded&quot;: &quot;2024-10-05T06:39:37.4665154Z&quot;,
    &quot;timeUploadEnded&quot;: &quot;2024-10-05T06:39:38.2780165Z&quot;,
    &quot;bytesDownloaded&quot;: 2560984,
    &quot;bytesUploaded&quot;: 410678
  },
  &quot;id&quot;: &quot;0fe44d455d8746ac85abeed514b9521a&quot;
}</pre>
<p>言うまでもなく、adskMask オプションを指定しない場合や、&quot;adskMask&quot;: false と指定した場合には、上記に太字で記した URL は、実際に使用できる完全な URL で表示されてしまいます。アプリ開発後の評価期間中にセキュアなデバッグ作業が必要とされるような場合には、このオプションの利用をご検討ください。</p>
<ul>
<li>adskMask オプションを指定すると、<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-apiadskdebug-option.html" rel="noopener" target="_blank">adskDebug オプション</a> で生成された debugInfoUrl の URL もマスクされてしまうのでご注意ください。</li>
<li>adskMask オプションによる URL のマスクは、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/design-automation-api-multipart-support-s3-upload.html" rel="noopener" target="_blank">Design Automation API：Direct-to-S3 アプローチを簡素化する新機能</a> の方法でご案内した入出力パラメータの URL（&quot;urn:adsk.objects:os.object:&lt;BucketKey&gt;/&lt;ObjectKey&gt;&quot; 様の形式）には適用されません。</li>
</ul>
<p>By Toshiaki Isezaki</p>

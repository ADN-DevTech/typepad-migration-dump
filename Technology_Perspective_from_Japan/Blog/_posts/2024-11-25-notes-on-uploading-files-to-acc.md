---
layout: "post"
title: "Data Management API：ACC へのファイル アップロードの注意点"
date: "2024-11-25 00:06:15"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/11/notes-on-uploading-files-to-acc.html "
typepad_basename: "notes-on-uploading-files-to-acc"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da7bbd200b-pi" style="display: inline;"><img alt="Upload_to_acc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860da7bbd200b image-full img-responsive" src="/assets/image_552957.jpg" title="Upload_to_acc" /></a></p>
<p>Data Management API を使って Fusion Team（含む 旧 BIM 360 Team・A360 Personal）や Autodesk Construction Cloud（Autodesk Docs、旧 BIM 360 Docs）などのオートデスクのストレージ サービスにファイルをアップロードを把握する際、<a href="https://aps.autodesk.com/en/docs/data/v2/developers_guide/overview/" rel="noopener" target="_blank">公式ドキュメント</a>の How-to Guide に記載されている <a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">Upload a File</a>&#0160;を参考にされると思います。</p>
<p>同ドキュメントに記載されている手順は、Fusion Team と Autodesk Construction Cloud で共通しています。ただし、<a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-7-create-the-first-version-of-the-uploaded-file" rel="noopener" target="_blank">Step 7: Create the first version of the uploaded file</a>、<a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-8-update-the-version-of-a-file" rel="noopener" target="_blank">Step 8: Update the version of a file</a> で紹介されている cURL 使用のリクエスト ボディの JSON ペイロード内容には注意が必要です。</p>
<p><strong>Step 7 の cURL 呼び出し例の場合：</strong></p>
<div>
<div>
<pre>curl -X POST -H &quot;Authorization: Bearer nFRJxzCD8OOUr7hzBwbr06D76zAT&quot; -H &quot;Content-Type: application/vnd.api+json&quot; -H &quot;Accept: application/vnd.api+json&quot; -d &#39;{
    &quot;jsonapi&quot;: { &quot;version&quot;: &quot;1.0&quot; },
    &quot;data&quot;: {
      &quot;type&quot;: &quot;items&quot;,
      &quot;attributes&quot;: {
        &quot;displayName&quot;: &quot;myfile.jpg&quot;,
        &quot;extension&quot;: {
          &quot;type&quot;: &quot;items:<span style="color: #111111; background-color: #ffff00;"><strong>autodesk.core:File</strong></span>&quot;,
          &quot;version&quot;: &quot;1.0&quot;
        }
      },
      &quot;relationships&quot;: {
        &quot;tip&quot;: {
          &quot;data&quot;: {
            &quot;type&quot;: &quot;versions&quot;, &quot;id&quot;: &quot;1&quot;
          }
        },
        &quot;parent&quot;: {
          &quot;data&quot;: {
            &quot;type&quot;: &quot;folders&quot;,
            &quot;id&quot;: &quot;urn:adsk.wipprod:fs.folder:co.mgS-lb-BThaTdHnhiN_mbA&quot;
          }
        }
      }
    },
    &quot;included&quot;: [
      {
        &quot;type&quot;: &quot;versions&quot;,
        &quot;id&quot;: &quot;1&quot;,
        &quot;attributes&quot;: {
          &quot;name&quot;: &quot;myfile.jpg&quot;,
          &quot;extension&quot;: {
            &quot;type&quot;: &quot;versions:<span style="color: #111111; background-color: #ffff00;">autodesk.core:File</span>&quot;,
            &quot;version&quot;: &quot;1.0&quot;
          }
        },
        &quot;relationships&quot;: {
          &quot;storage&quot;: {
            &quot;data&quot;: {
              &quot;type&quot;: &quot;objects&quot;,
              &quot;id&quot;: &quot;urn:adsk.objects:os.object:wip.dm.prod/2a6d61f2-49df-4d7b-9aed-439586d61df7.jpg&quot;
            }
          }
        }
      }
    ]
  }&#39; &quot;https://developer.api.autodesk.com/data/v1/projects/<span style="color: #0000ff;"><strong>a.</strong></span>cGVyc29uYWw6d2lwMWZxYWUyOWNlZGY4I0QyMDE2MDQxODM5NDM2NzM/items&quot;

</pre>
<p>この Step で説明されているファイルのアップロード先は、Fusion Team（含む 旧 BIM 360 Team・A360 Personal）になっています。これは、扱っている Hub ID が <span style="color: #0000ff;"><strong>a.</strong></span> で始まっている点で判別することが出来ます。ここでは、黄色で反転で示した Items と Versions スコープの type 値が、<strong>autodesk.core:File</strong> になってる点にも着目ください。</p>
<p>Autodesk Construction Cloud（Autodesk Docs、旧 BIM 360 Docs）の Hub ID は、<strong>b.</strong> で始まります。同様に、許容される Items と Versions スコープの type 値は、<strong>autodesk.bim360:File</strong> になります。</p>
</div>
</div>
<p>このため、 <a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">Upload a File</a> 記載の JSON ペイロード内容の Hub ID、Folder ID、Object ID 等のみを変更して、そのまま Autodesk Construction Cloud にアップロードしたファイルにバージョンを与えようとすると、400 BAD_INPUT エラーでバージョン登録に失敗してしまいます。</p>
<p>Autodesk Construction Cloud へのバージョン登録には、<strong>autodesk.bim360:File</strong> を指定するようにしてください。</p>
<p><strong>最初のバージョン登録（Step 7）時のリクエスト ボディの JSON ペイロード例：</strong></p>
<pre>{
    &quot;jsonapi&quot;: {
      &quot;version&quot;: &quot;1.0&quot;
    },
    &quot;data&quot;: {
      &quot;type&quot;: &quot;items&quot;,
      &quot;attributes&quot;: {
        &quot;displayName&quot;: &quot;result.pdf&quot;,
        &quot;extension&quot;: {
          &quot;type&quot;: &quot;items:<span style="background-color: #ffff00;"><strong>autodesk.bim360:File</strong></span>&quot;,
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
            &quot;id&quot;: &quot;urn:adsk.wipprod:fs.folder:co.FtKfsRS2Re6g0keqduq8tw&quot;
          }
        }
      }
    },
    &quot;included&quot;: [
      {
        &quot;type&quot;: &quot;versions&quot;,
        &quot;id&quot;: &quot;1&quot;,
        &quot;attributes&quot;: {
          &quot;name&quot;: &quot;result.pdf&quot;,
          &quot;extension&quot;: {
            &quot;type&quot;: &quot;versions:<span style="background-color: #ffff00;"><strong>autodesk.bim360:File</strong></span>&quot;,
            &quot;version&quot;: &quot;1.0&quot;
          }
        },
        &quot;relationships&quot;: {
          &quot;storage&quot;: {
            &quot;data&quot;: {
              &quot;type&quot;: &quot;objects&quot;,
              &quot;id&quot;: &quot;urn:adsk.objects:os.object:wip.dm.prod/c6b4e59d-8315-451f-be83-afa751e8525b.pdf&quot;
            }
          }
        }
      }
    ]
  }</pre>
<p>By Toshiaki Isezaki</p>

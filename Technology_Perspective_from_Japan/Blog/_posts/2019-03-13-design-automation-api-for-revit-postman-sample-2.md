---
layout: "post"
title: "Design Automation API for Revit - Postman Sample で動作確認 2"
date: "2019-03-13 01:20:07"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-2.html "
typepad_basename: "design-automation-api-for-revit-postman-sample-2"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-1.html">前回のブログ記事</a>に引き続き、Postman で SketchIt サンプルの動作を確認する手順をご紹介します。<br />既に AppBundle と Activity の登録が完了している状態ですので、今回は、実際のジョブとなる WorkItem を作成して、処理結果を取得するところまでを解説いたします。</p>
<p><strong><br />9. WorkItem の作成</strong></p>
<p>Postman コレクションの [SketchItApi]-&gt;[WorkItem]フォルダ配下にある[Send Workitem]リクエストを選択します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/da/us-east/v3/workitems</strong>、メソッドは <strong>POST</strong> となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46d16be200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46d16be200d image-full img-responsive" src="/assets/image_74629.jpg" title="DesignAutomationRevitPostman12" /></a></p>
<p>Body タブでは、JSON 形式で、WorkItem のパラメータを送信します。</p>
<p style="padding-left: 40px;">&quot;activityId&quot;: Activity ID の指定<br />&quot;arguments&quot;: Activity で定義した入出力のパラメータに対応する実際の値</p>
<p>&quot;sketchItInput&quot;プロパティの&quot;url&quot;には、JSON 形式の座標データを、&quot;result&quot;プロパティの&quot;url&quot;には、<strong>処理結果を出力するクラウドストレージの URL</strong> を設定します。<br /><br />ここでは、環境変数&quot;sketchItResultUrl&quot;を参照するように設定されていますが、これまでのステップでは、この URL はまだ取得しておりません。</p>
<p><span style="text-decoration: underline;"><strong>そのため、WorkItem 作成のリクエストは、この段階では完了することはできません。</strong></span></p>
<p>実は、WorkItem で設定するファイル（Revit プロジェクトやテキストファイル）のクラウドストレージの URLは、事前に取得しなければなりません。<br />そして、この URL のことを、<strong>署名付き URL （Signed URL）</strong>といいます。</p>
<p>Forge の Data Management API では、この署名付き URL（Signed URL）を取得する方法がサポートされています。</p>
<p>以降、Forge の Data Management API を利用して、署名付き URL（Signed URL）を取得する手順を説明いたします。<br /><br /></p>
<p><strong>10. Authentiocan API によるアクセス トークンの取得</strong></p>
<p>前回、Design Automation API を利用するために、2-legged 認証で、scope をコードの生成と実行の権限に相当する、<strong>&quot;code:all&quot;</strong> に指定してアクセス トークンを取得しました。</p>
<p>Data Management API を利用するためには、<strong>前回とは異なるリソースへのアクセス権限が必要</strong>になります。</p>
<p style="padding-left: 40px;"><strong>&quot;bucket:read bucket:update bucket:create bucket:delete data:write data:create data:read&quot;</strong></p>
<p>Postman コレクションの[DataAPI]-&gt;[Authentication]フォルダ配下にある [New token]リクエストを選択します。<br />すると、[New token]リクエストの画面がメイン画面に表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a443f6bc200c-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a443f6bc200c image-full img-responsive" src="/assets/image_481112.jpg" title="DesignAutomationRevitPostman13" /></a></p>
<p>Body タブをアクティブにすると、scope パラメータに上記のスコープの値が設定されていることが確認できます。</p>
<p>Send ボタンをクリックすると、リクエストが送信され、レスポンスには、アクセス トークンとその有効期限が返却されます。<br />取得されたアクセス トークンは、環境変数に新たに dataApiToken パラメータとして追加されます。<br /><br />以降の Data Management API のリクエストには、この環境変数のパラメータを通じて、アクセス トークンが設定されます。<br /><br /></p>
<p><strong>11. Bucket の作成</strong></p>
<p>Postman サンプルでは、処理結果の出力先に、OSS（ Object Storage Service） の Bucket を利用します。<br />Bucket に関する解説は、下記のブログ記事をご参照ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2017/04/cloud-storage-forge-uses.html">Forge が使用するクラウド ストレージ</a><br /><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html"></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html">Bucket に関してのサマリー</a><br /><br /></li>
</ul>
<p>Postman コレクションの[DataAPI]-&gt;[Bucket]フォルダ配下にある [Create Bucket]リクエストを選択します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/oss/v2/buckets</strong>、メソッドは <strong>POST</strong> となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46d1705200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman14" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46d1705200d image-full img-responsive" src="/assets/image_790302.jpg" title="DesignAutomationRevitPostman14" /></a></p>
<p>Body タブをアクティブにすると、bucketKey パラメータに環境変数&quot;bucketKey&quot;が設定されることがわかります。<br />policyKey パラメータには、24時間有効な &quot;transient&quot;が設定されています。</p>
<p>※環境変数&quot;bucketKey&quot;は、アクセストークン取得時のスクリプト処理（[Tests]タブに記載されているスクリプト）で、自動的に環境変数&quot;dasNickName&quot;を小文字変換した文字列が設定されます。別の名前で Bucket を作成したい場合は、この値を変更してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46d1738200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman15" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46d1738200d image-full img-responsive" src="/assets/image_683017.jpg" title="DesignAutomationRevitPostman15" /></a></p>
<p>リクエストを送信すると、レスポンスには、200 OK と Bucket の情報が返却されます。</p>
<p><br /><strong>12. 処理結果を出力する Revit プロジェクトファイルの保存場所を確保する</strong></p>
<p>Bucket が作成できたら、次はファイルを保存するための場所を確保する必要があります。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/oss/v2/buckets/:bucketKey/objects/:objectName</strong>、メソッドは <strong>PUT</strong> となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491b33a200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman17" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491b33a200b image-full img-responsive" src="/assets/image_985837.jpg" title="DesignAutomationRevitPostman17" /></a></p>
<p>このエンドポイントは、<a href="https://forge.autodesk.com/en/docs/data/v2/tutorials/app-managed-bucket/">ファイルのデータを送信することで、直接ファイルをアップロードすることもできます</a>が、この段階では、Revit ファイルはまだ作成されていないため、データは送信しません。<br /><br />そのため、ここでは SketchIt.rvt という<strong>ファイルの保存場所を確保</strong>します。</p>
<p>レスポンスには、200 OK と確保したファイルの保存場所の情報が返却されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46d1791200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman18" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46d1791200d image-full img-responsive" src="/assets/image_74983.jpg" title="DesignAutomationRevitPostman18" /></a><br /><br /></p>
<p><strong>13. 書き込みアクセス権のある署名付き URL （Signed URL）の取得</strong></p>
<p>確保したファイルの保存場所から、<strong>書き込みアクセス権のある署名付きURL</strong>を取得します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/oss/v2/buckets/:bucketKey/objects/:objectKey/signed</strong> で、メソッドは <strong>POST</strong> です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491b3b9200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman19" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491b3b9200b image-full img-responsive" src="/assets/image_203158.jpg" title="DesignAutomationRevitPostman19" /></a></p>
<p>このエンドポイントは、<strong>指定された有効期限内にオブジェクトをダウンロード・アップロードするために使用できる署名付きURLを作成</strong>します。<br />署名付きURLが指すオブジェクトが削除されるか、または署名付きURLが期限切れになる前に期限切れになると、署名付きURLは無効になります。</p>
<p>URLが最初に１度だけ使用された後に期限切れになるようにURLを設定したり、URLへの読み取りまたは書き込みアクセス権を設定することもできます。</p>
<p>アクセス権は、URL クエリパラメータとして、以下 のいずれかを指定して設定できます。</p>
<ul>
<li>access=read</li>
<li>access=write</li>
<li>access=readwrite</li>
</ul>
<p>したがって、今回は、確保したファイルの保存場所に Revit プロジェクトファイルを保存するため、<strong>access=write</strong> を設定します。</p>
<p>レスポンスには、200 OK と併せて、<strong>署名付き URL （Signed URL）</strong>が返却されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491b3de200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman20" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491b3de200b image-full img-responsive" src="/assets/image_888804.jpg" title="DesignAutomationRevitPostman20" /></a></p>
<p>※AWS S3 でも類似の事前署名付き URL （Pre-signed URL）がサポートされております。詳細は<a href="https://docs.aws.amazon.com/ja_jp/AmazonS3/latest/dev/PresignedUrlUploadObject.html">こちらの記事（署名付き URL を使用したオブジェクトのアップロード）</a>をご参照ください。</p>
<p><strong><br />14. WorkItem の作成</strong></p>
<p>ステップ 10 ～ 13 で、処理結果を出力するための署名付き URL （Signed URL）を取得することができました。<br />これでようやく、ステップ 9 WorkItem の作成に戻って、リクエストを送信することができます。</p>
<p>既に環境変数&quot;sketchItResultUrl&quot; には、事前署名付き URL （Pre-signed URL）が設定されています。</p>
<p>もう一度、Postman コレクションの [SketchItApi]-&gt;[WorkItem]フォルダ配下にある[Send Workitem]リクエストを選択し、Send ボタンをクリックして、リクエストを実行します。</p>
<p>レスポンスには、200 OK と合わせて、<strong>WorkItem の処理の進捗状況を表す status と、WorkItem の ID</strong> が返却されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491b411200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman21" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491b411200b image-full img-responsive" src="/assets/image_467481.jpg" title="DesignAutomationRevitPostman21" /></a></p>
<p><br /><strong>15. WorkItem 進捗状況の確認</strong></p>
<p>WorkItem ID を通じて、<strong>処理の進捗状況とログ</strong>を確認することができます。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/da/us-east/v3/workitems/:id</strong> で、メソッドは <strong>GET</strong> です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46d1852200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman22" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46d1852200d image-full img-responsive" src="/assets/image_290394.jpg" title="DesignAutomationRevitPostman22" /></a></p>
<p>status の値は、下記のいずれかが返されます。<br />処理に失敗した場合は failedInstructions、成功した場合は success となります。</p>
<ul>
<li>pendinginprogress</li>
<li>cancelled</li>
<li>failedDownload</li>
<li>failedInstructions</li>
<li>failedUpload</li>
<li>failedLimitProcessingTime</li>
<li>failedLimitDataSize</li>
<li>success</li>
</ul>
<p>reportUrl の値は、ログファイルの URL です。ブラウザからアクセスして、report.txt というファイルをダウンロードすることができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491b476200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman23" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491b476200b image-full img-responsive" src="/assets/image_855660.jpg" title="DesignAutomationRevitPostman23" /></a></p>
<p><br /><strong>16. 処理結果のダウンロード</strong></p>
<p>最後に Data Management API を利用して処理結果をダウンロードします。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/oss/v2/buckets/:bucketKey/objects/:objectName</strong>、メソッドは <strong>GET</strong> です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491b485200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman24" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491b485200b image-full img-responsive" src="/assets/image_427387.jpg" title="DesignAutomationRevitPostman24" /></a></p>
<p>リクエストを送信後、レスポンスの右上にある Download ボタンをクリックすると、SketchIt.rvt を取得できます。<br />Revit 2018 で開いて確認してみましょう。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a443f851200c-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman26" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a443f851200c image-full img-responsive" src="/assets/image_122200.jpg" title="DesignAutomationRevitPostman26" /></a></p>
<p>Postman サンプルでの動作確認は終わりです。<br />ご興味のある方は、他の2つのサンプルもお試しください。</p>
<p>次回は、Learn Autodesk Forge チュートリアルのサンプルを動作確認するためのセットアップ手順をご紹介します。</p>
<p>By Ryuji Ogasawara</p>

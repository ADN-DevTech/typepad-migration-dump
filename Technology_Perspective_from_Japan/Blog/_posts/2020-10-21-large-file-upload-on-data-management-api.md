---
layout: "post"
title: "Data Management API：大きなサイズのファイル アップロードについて"
date: "2020-10-21 00:04:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/10/large-file-upload-on-data-management-api.html "
typepad_basename: "large-file-upload-on-data-management-api"
typepad_status: "Publish"
---

<p>Data Management API の <strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT object</a></strong> endpoint を使ってファイルをアップロードする場合、ファイル サイズが大きいと、アップロードに失敗するケースがあります。もちろん、アップロード時にネットワーク負荷の状態が影響することがりますが、<strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT object</a></strong> endpoint は 100 MB 以下のファイルサイズを前提にしている点にも留意が必要です。同 endpoint のリファレンス ドキュメントにも、その点が明記されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e96ce3d4200b-pi" style="display: inline;"><img alt="Put_object" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e96ce3d4200b image-full img-responsive" src="/assets/image_93072.jpg" title="Put_object" /></a></p>
<p>100 MB 以上のサイズを持つファイルのアップロードには、<strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT object</a></strong> endpoint の代わりに <strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-resumable-PUT/" rel="noopener" target="_blank">PUT object/resumable</a></strong> endpoint の使用をお勧めします。逆に、100MB に満たない場合には、従来通り、<strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT object</a></strong> endpoint をお使いください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4191519200d-pi" style="display: inline;"><img alt="Put_object_resumable" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4191519200d image-full img-responsive" src="/assets/image_299211.jpg" title="Put_object_resumable" /></a></p>
<p>アップロードしたファイルが正しくアップロードされているかは、両 endpoint レスポンスに含まる <strong><a href="https://ja.wikipedia.org/wiki/SHA-1" rel="noopener" target="_blank">SHA-1</a></strong> パラメータの値を使ってチェックすることが出来ます。</p>
<p>SHA-1 <strong><a href="https://ja.wikipedia.org/wiki/%E3%83%81%E3%82%A7%E3%83%83%E3%82%AF%E3%82%B5%E3%83%A0" rel="noopener" target="_blank">チェックサム</a></strong>は、アップロードとダウンロードの整合性をチェックするために広く使用されています。 すべての主要言語で利用でき、メッセージ（この場合はファイル）を表す 16 進値を生成します。 アプリはこの値をローカルで（アップロード前に）認識しているため、サーバー上の値が同じであるかどうかを確認し、アップロード中にデータが破損していないことを確認できます。 ダウンロードに有効な場合も同じです。</p>
<p>SHA-1 は Forge OSS へのアップロードで常に利用可能でしたが、今までは、アップロード後にアプリが、レスポンスから SHA-1 値を確認する必要がありました。</p>
<p>現在では、上記 2 つの endpoint が、オプションの <strong>x-ads-content-sha1</strong> リクエストヘッダーで Forge サーバーが<a href="https://support.microsoft.com/en-us/help/889768/how-to-compute-the-md5-or-sha-1-cryptographic-hash-values-for-a-file" rel="noopener" target="_blank">ローカル</a>との一致を確認することが出来るようになっています。もし、SHA-1 値が一致しない場合はアップロードが失敗したと見なしてステータスコード 400 を返します。これにより、破損したデータのアップロードを抑止することが出来るようになっています。レスポンスには、<strong>x-ads-content-sha1</strong> ヘッダーを介してサーバーが計算した SHA-1 値も含まれます。これは、呼び出し元（アプリ）で確認する際に使用することが出来ます。</p>
<p>このリクエスト ヘッダーはオプションであるため、既存のアプリには影響しません。 また、レスポンス ヘッダーは常に存在しますが、同様にアプリには影響しません。</p>
<p>By Toshiaki Isezaki</p>

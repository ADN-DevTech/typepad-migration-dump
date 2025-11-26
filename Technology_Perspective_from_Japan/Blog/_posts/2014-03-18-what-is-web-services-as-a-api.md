---
layout: "post"
title: "クラウドの仕組みと API としての Web サービス"
date: "2014-03-18 15:13:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/03/what-is-web-services-as-a-api.html "
typepad_basename: "what-is-web-services-as-a-api"
typepad_status: "Publish"
---

<p>このブログでは、オートデスクのデスクトップ製品の他に、多くのクラウド サービスをご紹介してきました。それらクラウド サービスの他にも、現在では、Autodesk 360 の配下には、非常に沢山のクラウドサービスが用意されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511739900970c-pi" style="display: inline;"><img alt="Autodesk_360_cloud_services" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511739900970c image-full img-responsive" src="/assets/image_618924.jpg" title="Autodesk_360_cloud_services" /></a></p>
<p>個々のサービスは <a href="http://ja.wikipedia.org/wiki/SAAS" target="_blank"><strong>SaaS</strong></a>（Software As A Service）と呼ばれたり、単に <strong>Web サービス</strong> と呼ばれることがあります。従来のデスクトップ製品が、「アプリケーション」や「ソフトウェア」と呼ばれるのとは少し区別されていることがわかります。今回は、複数のサービスを持つ&#0160;Autodesk 360 が、具体的にどのように構築されているのか、その概要を最初に説明したいと思います。その後で、これから登場するであろう API について、簡単に提供する仕組みを簡単にご紹介したいと思います。</p>
<p><strong>クラウド インフラで利用されている仮想化</strong></p>
<p>デスクトップ製品を運用する際には、自前でコンピュータを購入して、そのコンピュータに製品をインストールすることになります。Web サービスは、デスクトップ製品のようにプログラムで作成されている点は同じですが、使用するコンピュータにインストールするのではなく、<a href="http://ja.wikipedia.org/wiki/%E3%83%9B%E3%82%B9%E3%83%86%E3%82%A3%E3%83%B3%E3%82%B0%E3%82%B5%E3%83%BC%E3%83%90" target="_blank">サーバー コンピュータ上でホスティング</a>して、クライアントから要求に応じてサーバー上でプログラムを実行させるようになります。</p>
<p>Autodesk 360 のように膨大な数のユーザ（クライアント）からのリクエストを処理していくためには、膨大な数のサーバー コンピュータが必要になります。それらを自前で購入してセットアップやメインテナンスしていくのは、とても大変でコストがかかるは自明です。そこで、オートデスクは、独自にサーバーコンピュータを多数用意して Web サービス運用をしているのはなく、<strong><a href="http://aws.amazon.com/jp/" target="_blank">Amazon Web Services</a></strong>、通称、<strong>AWS</strong> をプラットフォームにすることを選択しました。AWS は、<strong><a href="http://ja.wikipedia.org/wiki/Platform_as_a_Service" target="_blank">PaaS</a></strong>（Platform As A Service）とも言われていて、Autodesk 360 のようなクラウド サービスのインフラを提供しています。</p>
<p>PaaS である AWS は、オートデスクのようなソフトウェア ベンダーに代わって、ハードウェアを含むプラットフォームを提供しています。その中にはストレージ機能のみを提供する <a href="http://aws.amazon.com/jp/s3/" target="_blank"><strong>Amazon S3 サービス</strong></a>や、演算処理機能を提供する <a href="http://aws.amazon.com/jp/ec2/" target="_blank"><strong>Amazon EC2 サービス</strong></a>などのサービスが存在します。オートデスクは、S3 を使って Autodesk 360 のストレージ サービスを、EC2 で <a href="http://adndevblog.typepad.com/technology_perspective/2013/02/%E3%82%AA%E3%83%BC%E3%83%88%E3%83%87%E3%82%B9%E3%82%AF%E3%81%AE%E3%82%AF%E3%83%A9%E3%82%A6%E3%83%89%E3%82%B5%E3%83%BC%E3%83%93%E3%82%B9-autodesk-360-%E3%81%9D%E3%81%AE3.html" target="_blank"><strong>Autodesk 360 Rendering</strong></a> や <a href="http://adndevblog.typepad.com/technology_perspective/2013/10/recap-photo-cloud-service.html" target="_blank"><strong>Autodesk ReCap 360</strong></a> などの演算処理サービスを提供しています。もっとも、オートデスクは認証システムも含めて、「Autodesk 360」として作り込んだ形で提供しているので、利用するユーザは、決して AWS の存在を意識することはありません。</p>
<p>さて、AWS クラウド プラットフォームでは、1 台づつのコンピュータに通常の OS をインストールして、ユーザに割り当てているのではありません。当然、そのような行為はコストの増加に繋がってしますからです。実際には、物理的なサーバーコンピュータ上に<a href="http://ja.wikipedia.org/wiki/%E3%83%8F%E3%82%A4%E3%83%91%E3%83%BC%E3%83%90%E3%82%A4%E3%82%B6%E3%83%BC" target="_blank"><strong>ハイパーバイザー</strong></a>を使って、複数の OS を仮想化しています。EC2 では、Windows Server や Linux といった代表的なサーバーOSを、オートデスクのようソフトウェア ベンダーの要望に合わせて仮想化して提供しています。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcc9ff78970b-pi" style="display: inline;"><img alt="Cloud_Platform" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcc9ff78970b image-full img-responsive" src="/assets/image_147938.jpg" title="Cloud_Platform" /></a></p>
<p>この仕組みは、以前、ご紹介した「<a href="http://adndevblog.typepad.com/technology_perspective/2013/12/license-type-for-vitualization.html" target="_blank"><strong>仮想化環境に対応したライセンスについて</strong></a>」 でもご紹介しています。つまり、シンクライアントやプライベート クラウドといった環境で、デスクトップ製品を配信する仕組みを、もっと大規模にしたものと捉えることも出来ると思います。</p>
<p>クラウド上で既存のデスクトップ製品を運用することも可能ですが、オートデスクはそういった手法は選択せずに、専用の Web サービスを提供すること選択しています。なぜなら、クラウドを使う利点が次のように明確であるためです。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51179e3ff970c-pi" style="display: inline;"><img alt="Cloud_Benefit" class="asset  asset-image at-xid-6a0167607c2431970b01a51179e3ff970c img-responsive" src="/assets/image_372763.jpg" title="Cloud_Benefit" /></a>&#0160;</p>
<p>従来のデスクトップ製品では実現できない、あるいは、デスクトップ製品を使うことでが足かせになってしまうことも同時に抑止する必要があるのです。もちろん、デスクトップ製品は存在し続けることも認識していて、開発の継続も表明しています。</p>
<p>Autodesk 360 も登場から 3 年を経て、各種 Web サービスの種類と機能が充実してきています。そこで、Autodesk 360 をプラットフォームとして開発プラットフォームとして提供することが視野に入ってきているのです。当然、API を利用してカスタマイズすることが想定されることになりますが、デスクトップ製品の API とは少し異なる形態で提供されるはずです。</p>
<p><strong>API としての Web サービス</strong></p>
<p>それが API としての&#0160;<a href="http://ja.wikipedia.org/wiki/Web%E3%82%B5%E3%83%BC%E3%83%93%E3%82%B9" target="_blank"><strong>Web サービス</strong></a>です。少し紛らわしいのですが、Autodesk 360 のように、クラウドを使ったソフトウェア機能（アプリケーション サービス）も Web サービスと呼びますが、API として Web サービスと呼ぶこともあります。Web サービスは、クラウドに「ホスティング」してクラウド上で実行する処理を実装します（クライアント側で動作させる仕組みを持つものもあります）。</p>
<p>次に、API としての Web サービスの簡単な例を考えてみましょう。この例では、様々な種類のクライアントから製品を示す番号を送って、対応する製品の価格を取得する機能を仮定します。この Web サービスの根幹は、「クライアントから要求された番号を価格に変換してクライアントに返す」、といった簡単な処理です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51179e66d970c-pi" style="display: inline;"><img alt="Webサービス例" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a51179e66d970c image-full img-responsive" src="/assets/image_64723.jpg" title="Webサービス例" /></a>&#0160;</p>
<p>クラウドを利用する利点に沿うなら、おそらく次の動画のようなイメージになるはずです。画面が小さいので、可能でれば最大化してご覧ください。ここでは、Web ブラウザでも、AutoCAD 内のアドオン アプリケーションでも、モバイル デバイスでも、この Web サービスにアクセスしていることが分かります。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/HjshNK1oi94?feature=oembed" width="459"></iframe>&#0160;</p>
<p>様々なタイプのクライアントから Web サービスにアクセスするには、クライアント側のタイプやプラットフォーム、開発環境に関係なくアクセス出来る API が用意されている必要があります。個別に API や ソフトウェア ライブラリ、SDK などを用意するのは大変です。</p>
<p>「クライアントから要求された番号を価格に変換してクライアントに返す」 Web サービスでは、もちろん、クライアントのタイプを考慮しないようになっています。クライアント側で、どのようなユーザ インタフェースを実装するかは、Web サービスを利用する開発者の好みで自由に変化できるよう、逆にクライアント タイプに特化した機能を Web サービス API で提供しないようにしています。また、インターネットが普及した現在、クライアントから要求を送信したり、クラウドからクライアントに結果を戻すのは、標準化されたテクノロジを補うことが出来ます。</p>
<p>デスクトップ製品の API は、クライアントのタイプや製品自体の機能に依存するのが一般的なので、Windows や Mac の違いがあると移植が大変困難です。日本では発売されていませんが、例えば、AutoCAD for Mac のアドオン アプリケーションは、Windows 版の AutoCAD では動作しません。その逆も然りです。</p>
<p><strong>マッシュアップ</strong></p>
<p>API としての Web サービスが、とてもシンプルなものであることが何となくご理解いただけたかと思います。簡単すぎて、何が出来るのか心配になるくらいです。そこで、<a href="http://ja.wikipedia.org/wiki/%E3%83%9E%E3%83%83%E3%82%B7%E3%83%A5%E3%82%A2%E3%83%83%E3%83%97_(Web%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0)" target="_blank"><strong>マッシュアップ</strong></a> という言葉をご紹介しておきましょう。詳細は、先に埋め込んだハイパーリンク先にあるウィキペディアの説明に譲りますが、全く異なるアプリケーション サービスがシンプルな Web サービス（API）を組み合わせることで、全く別のアプリケーション サービスを構築することが出来るのです。もちろん、Web サービスによって得られた結果を、クライアント タイプによって、どう表現するかは開発者の手腕にかかっています。</p>
<p>以前に紹介した&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2013/12/autodesk-technologies-on-grabcad.html" target="_blank">GrabCAD 内のオートデスク サービスの利用</a>&#0160;や、<a href="http://adndevblog.typepad.com/technology_perspective/2014/01/fusion-360-cloud-service-update.html" target="_blank">Fusion 360 内で利用されているビューイング機能やレンダリング機能</a>&#0160;は、マッシュアップの一例です。</p>
<p>By Toshiaki Isezaki&#0160;</p>
<p>&#0160;</p>

---
layout: "post"
title: "Inventor 2022でのAPI Update  その2"
date: "2021-05-24 01:05:53"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/inventor-2022-api-update-part2.html "
typepad_basename: "inventor-2022-api-update-part2"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802344df200d-pi" style="display: inline;"><img alt="Autodesk-inventor-badge-1024" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802344df200d image-full img-responsive" src="/assets/image_852887.jpg" title="Autodesk-inventor-badge-1024" /></a></p>
<p>今回は、Inventor 2022のモデル状態について、Inventor以外の関連製品・サービスでのAPIの対応状況について、ご案内をしたいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">Apprentice Server</span></span></strong></p>
<p>Inventorの関連製品として、InventorアプリケーションAPIのサブセットを提供するInventor Apprentice Serverがあります。</p>
<p>Inventor Apprentice Serverは、無償のInventor Viewの一部としてインストールされInventorファイルへのアクセス（主に読み取り、一部書き込みが可能）ができるアプリケーションを開発することが出来ます。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/support/inventor/troubleshooting/caas/downloads/content/inventor-view-2022.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_513464.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor View 2022 | Inventor 2022</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Autodesk Inventor View 2022 スタンドアロン<br /><br />高品質な製品を提供するための継続的な取り組みとして、オートデスクは Autodesk Inventor View 2022 のスタンドアロン バージョンをリリースしました....</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>Inventor Apprentice Serverで、モデル状態を持つファイルへアクセスした場合の挙動について、確認をしてみたいと思います。</p>
<p>今回の検証でも、前回の記事で用いたiptファイル（<span class="asset  asset-generic at-xid-6a0167607c2431970b026bded231ab200c img-responsive"><a href="https://adndevblog.typepad.com/files/part1.ipt">Part1をダウンロード</a></span>）を利用して、iPropertyの「部品番号」の値を取得して動作の確認をします。</p>
<p>このiptファイルは、3つのモデル状態（マスター、モデル状態1、モデル状態2）を持ち、それぞれのモデル状態でiPropertyの「部品番号」と「ストック番号」に異なる値が設定されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded22c72200c-pi" style="display: inline;"><img alt="1" class="asset  asset-image at-xid-6a0167607c2431970b026bded22c72200c img-responsive" src="/assets/image_897890.jpg" title="1" /></a></p>
<p>&#0160;</p>
<p>利用するソースコードは、以下のような内容となります。</p>
<pre><code>
        Try
            Dim invApprentice As InventorApprentice.ApprenticeServerComponent = New InventorApprentice.ApprenticeServerComponent()
            Dim oPartDoc As InventorApprentice.ApprenticeServerDocument
            oPartDoc = invApprentice.Open(&quot;Part1.ipt&quot;)
            &#39; Get the design tracking property set. 
            Dim invDesignInfo As InventorApprentice.PropertySet = oPartDoc.PropertySets.Item(&quot;Design Tracking Properties&quot;)

            &#39; Get the part number property. 
            Dim invPartNumberProperty As InventorApprentice.Property = invDesignInfo.Item(&quot;Part Number&quot;)
            MessageBox.Show(&quot;Part Number: &quot; &amp; invPartNumberProperty.Value)
<br />        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
</code></pre>
<p>&#0160;</p>
<p>iptファイルを開き、”マスター” モデル状態をアクティブなモデル状態にしてiptファイルを保存して実行をすると以下のメッセージボックスが表示されます。 <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e103d941200b-pi" style="display: inline;"><img alt="6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e103d941200b img-responsive" src="/assets/image_378122.jpg" title="6" /></a></p>
<p>次に、”モデル状態1” モデル状態をアクティブなモデル状態にしてiptファイルを保存して実行をすると以下のメッセージボックスが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802b7681200d-pi" style="display: inline;"><img alt="7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802b7681200d img-responsive" src="/assets/image_559138.jpg" title="7" /></a></p>
<p>&#0160;</p>
<p>このように、Apprentice Serverで既存のAPIを使用して、モデル状態に依存する内容にアクセスした場合、最後に対象のファイルが保存された際にアクティブなモデル状態の内容にアクセスします。</p>
<p>&#0160;</p>
<p>それでは、Apprentice Serverで任意のモデル状態の内容にアクセスすることや、現在のモデル状態を変更することは出来るのでしょうか？</p>
<p>&#0160;</p>
<p>残念ながら、Apprentice Serverでは任意のモデル状態の内容にアクセスすることや、現在のモデル状態を変更する等のAPIは用意されておりません。これは、Apprentice ServerがInventor ファイルへの読み取りを行うAPIを提供するという製品であるという位置づけから、モデル状態に関するAPIをスコープ外としているためです。</p>
<p>&#0160;</p>
<p>将来的には変更がされる可能性はありますが、現時点ではApprentice Serverからモデル状態にアクセスは出来ない事にご留意ください。</p>
<p>&#0160;</p>
<p>それでは次に、ForgeのModel Derivative APIについてみていきたいと思います。</p>
<p>&#0160;</p>
<p><strong><span style="text-decoration: underline;"><span style="font-size: 15pt;">Forge Model Derivative API</span></span></strong></p>
<p>Forge のModel Derivative APIを用いて、モデル状態を持つInventorファイルから、SVF等のViewerファイルを出力した場合どのようになるでしょうか？</p>
<p>&#0160;</p>
<p>結論から申し上げますと、Model Derivative APIでは、Apprentice Serverと同様に最後に対象のファイルが保存された際にアクティブなモデル状態が扱われ、出力されるSVFファイルにも、その状態が反映されます。</p>
<p>&#0160;</p>
<p>このため、全モデル状態を持つSVFファイルを出力したいといった場合には、Design Automationを用いて、モデル状態を切り替えながら、各モデル状態に対応したSVFファイルを出力するといったカスタマイズが必要となります。</p>
<p>&#0160;</p>
<p>このDesign Automationを用いたカスタマイズについては、別途詳細を解説したいと思います。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>

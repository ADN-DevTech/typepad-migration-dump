---
layout: "post"
title: "Design Automation API for Inventor - iLogicを利用したDesign Automationの実行方法"
date: "2020-11-29 19:01:00"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/11/design-automation-api-for-inventor-various-way-to-run-ilogic.html "
typepad_basename: "design-automation-api-for-inventor-various-way-to-run-ilogic"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e97b15b1200b-pi" style="display: inline;"><img alt="無題" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e97b15b1200b image-full img-responsive" src="/assets/image_560498.jpg" title="無題" /></a></p>
<p>これまでのDesign Automation API for Inventorの解説記事では、Inventor Pluginを用いてDesing Automation for Inventorを実行する方法について解説をしてきました。</p>
<p>Pluginの開発には、.Net を用いて開発を行いますので、.Net Frameworkが提供する多くの機能が利用できるため、柔軟でパワフルかつメンテナンス性に優れたプログラムを開発しやすいといった利点がある一方で、Visual Studioを用いてのプログラム開発に、敷居が高さを感じていらっしゃる方もいらっしゃるのではないかと思います。</p>
<p>&#0160;</p>
<p>一方で、InventorにはiLogicというドキュメント内・外にルールを記述する仕組みがあります。iLogicは、Inventorに組み込みのエディタで、Visual Basic &#0160;(VB.NET)を用いて小さなプログラムを開発することができ、開発がしやすく簡単に利用することができます。このため、すでにご活用されているiLogic、または新規にiLogicを開発してDesing Automation for Inventorで利用したいと考えている方もいらっしゃるのではないかと思います。</p>
<p>&#0160;</p>
<p>そこで、今回の記事では、InventorのiLogicを利用してDesing Automationを実行する方法について解説をしたいと思います。</p>
<h2 style="position: relative; color: white; background: #81d0cb; line-height: 1.4; padding: 0.5em 0.5em 0.5em 0.5em;">Design Automation についてのおさらい</h2>
<p>InventorのiLogicをDesing Automationで実行する方法の解説の前に、Design Automationについて簡単におさらいをしたいと思います。</p>
<p>&#0160;</p>
<p>Design Automationを実行するにあたって、以下の3つのDesign Automationのコンポーネントについて理解をする必要があります。</p>
<ol>
<li>AppBundle</li>
<li>Activity</li>
<li>WorkItem</li>
</ol>
<p>&#0160;</p>
<p>AppBundleとは、Desing Automationで実行させたい「処理」が記述されたプラグインを、Desing Automationの実行系が利用できるようにパッケージ化したものとなります。</p>
<p>Activityとは、Desing Automationで実行するタスクの入出力および、実行するAppBundleを定義したものとなります。</p>
<p>WorkItemは、Activityに具体的な入出力値を指定して、実行するものとなります。</p>
<p>&#0160;</p>
<p>この3つは、よくプログラムにおける関数の宣言、定義、呼び出しに例えて説明がされますが、ActivityがC/C++言語でのヘッダファイルでの関数の宣言に、AppBundleが関数の定義に、そしてWorkItemが、関数の呼び出しに相当いたします。</p>
<p>Design Automation APIには、AppBundle、Activity、WorkItemそれぞれに作成、問い合わせなどのAPI（エンドポイント）が存在します。</p>
<p>&#0160;</p>
<p>また、Design Automationはクラウド上のサーバで実行されることから、以下のような点を理解する必要があります。</p>
<ol>
<li>処理の対象となるファイル（Inventorのパートファイルやアセンブリ等）はクラウドストレージ上に配置する必要がある</li>
<li>処理はDesign Automation実行毎に、クラウドサーバ上の一時領域で実行され、処理結果はクラウドストレージを介して取得する必要がある</li>
<li>処理はサーバ上で実行されるため、GUIでのインタラクションを伴う処理を実行することは出来ない</li>
</ol>
<p>&#0160;</p>
<p>Design Automationの仕組みについては、以下のForge Online動画記事 「Design Automation API の理解」でも解説をしておりますのでご参照ください</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-basics.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 150px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_473702.jpg" style="width: 100%; height: auto; max-height: 150px; min-width: 0; border: 0 none; margin: 0;" width="150" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Forge Online - Design Automation：タスクの自動化</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Forge Online、今回から数回にわたってタスクの自動化で利用される Design Automation API についてご案内していきたいと思います。まずは、登場の背景や歴史を含め、何が出来るのかその概要と、少し踏み込んでその仕組みについてご紹介します。 </span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、Design Automation Inventorについては、Forge Online動画記事にて詳細を解説しておりますので、是非ご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-inventor.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 150px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_58864.jpg" style="width: 100%; height: auto; max-height: 150px; min-width: 0; border: 0 none; margin: 0;" width="150" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Forge Online - Design Automation：Inventor タスクの自動化</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Forge Online、今回は、Inventor タスクの自動化と題して、Design Automation API for Inventor（DA4I）についてご案内したいと思います。 動画で利用しているプレゼンテーション資料（PDF）は次のリンクからダウンロードいただけます。</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<h2 style="position: relative; color: white; background: #81d0cb; line-height: 1.4; padding: 0.5em 0.5em 0.5em 0.5em;">Design AutomationでInventor iLogicの実行</h2>
<p>Design Automationについての理解をおさらいしましたので、実際にDesign AutomationでiLogicを使う方法について解説をしたいと思います。</p>
<p>&#0160;</p>
<p>iLogicを実行する方法には、大きく分けて以下の2つの方法があります。</p>
<ol>
<li>設計データ内(.prtや.iam等)のiLogicを実行する方法</li>
<li>外部ファイルのiLogicを指定して実行する方法</li>
</ol>
<p>&#0160;</p>
<p>また、2の外部ファイルのiLogicを指定して実行する場合、外部ファイルのiLogicをどこに置くかおよび、iLogicファイルを指定する方法で、以下の3つのパターンがあります。</p>
<p style="padding-left: 40px;">2-1．外部ファイルのiLogicをクラウドストレージに格納し、WorkItemのParameterで指定する方法</p>
<p style="padding-left: 40px;">2-2．外部ファイルのiLogicをクラウドストレージに格納し、ActivityのSettingで指定する方法</p>
<p style="padding-left: 40px;">2-3．外部ファイルのiLogicをAppBundleに含める方法</p>
<p>&#0160;</p>
<p>それぞれの場合に共通することは、ActivityでDesing Automation for Inventorの実行エンジンである、InventorCoreConsole.exeの実行コマンドラインに、/s オプションで、実行するiLogicのスクリプトを指定する点にあります。</p>
<p>InventorCoreConsole.exeのコマンドラインについては、以下のコマンドラインリファレンスをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/cmdLine/cmdLine-inventor/" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 150px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_20695.jpg" style="width: 100%; height: auto; max-height: 150px; min-width: 0; border: 0 none; margin: 0;" width="150" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor Command Line Reference</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">The commandLine parameter of an Activity lets you specify the executable (InventorCoreConsole.Exe - the Inventor Core Console) that must be run and the command line arguments that must be passed when a WorkItem executes the Activity. In addition to the supported standard parameters you can specify custom arguments, which can be accessed from within custom plugins.</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>それでは、具体的にそれぞれの場合についてどのように指定するかを見ていきましょう。</p>
<p>&#0160;</p>
<h4>1．設計データ内(.prtや.iam等)のiLogicを実行する方法</h4>
<p>Activity のcommandLineの/s オプションを指定して実行するiLogicスクリプトを指定します。iLogicスクリプトは、settings配下に定義しており、実行するInventor iLogicのルール名を、iLogicVb.RunRule(\&quot;MyRule\&quot;)の形で記述し、から参照します。以下の例では、MyRuleという名前のiLogicが実行されることになります。</p>
<p>実行するiLogicルール名は、”（ダブルクオーテーション）で囲む必要があり、エスケープ文字列\を追加して、\&quot;MyRule\&quot;としています。</p>
<p>&#0160;</p>
<ul style="list-style-type: square;">
<li>Activity</li>
</ul>
<pre><code>{
&#0160;&#0160;&#0160;&#0160;&quot;id&quot;: &quot;{ActivityId}&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;commandLine&quot;: [
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;$(engine.path)\\InventorCoreConsole.exe /i \&quot;$(args[InventorDoc].path)\&quot; /s $(settings[script].path)&quot;
&#0160;&#0160;&#0160;&#0160;],
&#0160;&#0160;&#0160;&#0160;&quot;parameters&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;get&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;post&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;engine&quot;: &quot;Autodesk.Inventor+2021&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;settings&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;script&quot;:{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;value&quot;:&quot;iLogicVb.RunRule(\&quot;MyRule\&quot;)&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;description&quot;: &quot;Execute iLogic rule within a part file.&quot;
}
</code></pre>
<div>
<p>&#0160;</p>
<p>この場合のWorkItemの指定は特別なことはなく、入力となるInventorファイルと、Desing Automation実行結果のInventorファイルを格納するOSSのURLを指定するだけとなります。</p>
<ul style="list-style-type: square;">
<li>WorkItem</li>
</ul>
</div>
<pre><code>{
 &#0160;&#0160;&#0160;&quot;activityId&quot;: &quot;{specify activity id + alias}&quot;,
 &#0160;&#0160;&#0160;&quot;arguments&quot;: {
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input file url}&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify output file url}&quot;,
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;put&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;}
}</code></pre>
<p>&#0160;</p>
<p>また、上述の例では、ActivityにiLogicルール名のMyRuleを固定で指定しましたが、WorkItem毎に実行するiLogicルール名を変更することも可能で、この場合は以下のようになります。</p>
<ul style="list-style-type: square;">
<li>Activity</li>
</ul>
<pre><code>{
    &quot;id&quot;: &quot;{ActivityId}&quot;,
    &quot;commandLine&quot;: [
        &quot;$(engine.path)\\InventorCoreConsole.exe /i \&quot;$(args[InventorDoc].path)\&quot; /s  $(args[iLogicName].path)&quot;
    ],
    &quot;parameters&quot;: {
      &quot;InventorDoc&quot;: {
        &quot;verb&quot;: &quot;get&quot;,
        &quot;localName&quot;: &quot;Input.ipt&quot;
      },
       &quot;iLogicName&quot;: {
       	&quot;verb&quot;: &quot;read&quot;
      },
      &quot;OutputIpt&quot;: {
        &quot;localName&quot;: &quot;Input.ipt&quot;,
        &quot;verb&quot;: &quot;post&quot;
      }
    },
    &quot;engine&quot;: &quot;Autodesk.Inventor+2021&quot;,
    &quot;settings&quot;: {},
    &quot;description&quot;: &quot;Execute iLogic rule within a part file.&quot;
}
</code></pre>
<ul style="list-style-type: square;">
<li>WorkItem</li>
</ul>
<pre><code>
{
 &#0160;&#0160;&#0160;&quot;activityId&quot;: &quot;{specify activity id + alias}&quot;,
 &#0160;&#0160;&#0160;&quot;arguments&quot;: {
 &#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {<br /> &#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input file url}&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},
	&quot;iLogicName&quot;: {
	&#0160;&#0160;&#0160;&#0160;&quot;value&quot;: &quot;iLogicVb.RunRule(\&quot;MyRule\&quot;)&quot;
	},
	&quot;OutputIpt&quot;: {
	 &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify output file url}&quot;,
	 &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;put&quot;
	}
&#0160;&#0160;&#0160;&#0160;}
}
</code></pre>
<p>&#0160;</p>
<p>ここまででの指定を見て、お気づきの方がいらっしゃるかもしれませんがMyRuleを実行している、<span class="hljs-selector-tag">iLogicVb</span><span class="hljs-selector-class">.RunRule()というのは、i引数で指定された名前をもつiLogicルールを実行するLogicの組み込み関数です。</span></p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/ja/support/inventor/learn-explore/caas/CloudHelp/cloudhelp/2019/JPN/Inventor-iLogic/files/GUID-07DA57D0-B34C-49D8-8B44-5C554A07C359-htm.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 59px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_651625.jpg" style="width: 50%; height: 50%; min-width: 0; border: 0 none; margin: 0;" width="59" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">[その他を実行]関数のリファレンス(iLogic) | Inventor 2019 | Autodesk Knowledge Network</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">iLogic には、他の関数を実行する関数が何種類かあります。 通常は、ルール内のパラメータを変更してルールをトリガします。この関数は...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><span class="hljs-selector-class">つまりWorkItem、Activityで<span class="hljs-selector-tag">iLogicVb</span>.RunRule()を記載していた箇所に ( \n は改行</span><span class="hljs-selector-class">): &quot;Trace.TraceInformation(\&quot;Start of MyScript\&quot;)\nTrace.TraceInformation(\&quot;Setting height to 2 cm\&quot;)\nParameter(\&quot;height\&quot;) = \&quot;2 cm\&quot;\nTrace.TraceInformation(\&quot;Updating document\&quot;)\nInventorVb.DocumentUpdate()\nTrace.TraceInformation(\&quot;Saving changes\&quot;)\nThisDoc.Save()\nTrace.TraceInformation(\&quot;End of MyScript\&quot;)&quot;といった形で、直接iLogicの処理を記述して実行させることも可能です。&#0160;</span></p>
<p>ただし、この方法は実行するiLogicの見通しが非常に悪いためあまりお勧めできる方法ではありません。</p>
<p>&#0160;</p>
<h4>2-1．外部ファイルのiLogicをクラウドストレージに格納し、WorkItemのParameterで指定する方法</h4>
<p>それでは、外部ファイルのiLogicを指定する方法について解説をしていきます。</p>
<p>基本的には、設計データ内のiLogicを実行する方法で説明をした方法とほぼ同じで、/s オプションに外部ファイルのURLを指定する形となります。</p>
<p>入力ファイル中のiLogicルールを実行する場合と異なり、iLogicが記載されたファイルもクラウドストレージにアップロードしてURLを指定する必要がある点にご注意ください。</p>
<p>&#0160;</p>
<ul style="list-style-type: square;">
<li>Activity</li>
</ul>
<pre><code>{
    &quot;id&quot;: &quot;{ActivityId}&quot;,
    &quot;commandLine&quot;: [
        &quot;$(engine.path)\\InventorCoreConsole.exe /i \&quot;$(args[InventorDoc].path)\&quot; /s  $(args[iLogicVb].path)&quot;
    ],
    &quot;parameters&quot;: {
      &quot;InventorDoc&quot;: {
        &quot;verb&quot;: &quot;get&quot;,
        &quot;localName&quot;: &quot;Input.ipt&quot;
      },
       &quot;iLogicVb&quot;: {
       	&quot;verb&quot;: &quot;get&quot;<br />        &quot;localName&quot;: &quot;input.iLogicVb&quot;
      },
      &quot;OutputIpt&quot;: {
        &quot;localName&quot;: &quot;Input.ipt&quot;,
        &quot;verb&quot;: &quot;post&quot;
      }
    },
    &quot;engine&quot;: &quot;Autodesk.Inventor+2021&quot;,
    &quot;settings&quot;: {},
    &quot;description&quot;: &quot;Execute specified iLogic  file.&quot;
}
</code></pre>
<ul style="list-style-type: square;">
<li>WorkItem</li>
</ul>
<pre><code>
{
 &#0160;&#0160;&#0160;&quot;activityId&quot;: &quot;{specify activity id + alias}&quot;,
 &#0160;&#0160;&#0160;&quot;arguments&quot;: {
 &#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {<br /> &#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input file url}&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},
	&quot;iLogicVb&quot;: {
	&#0160;&#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input iLogic file url}&quot;
	},
	&quot;OutputIpt&quot;: {
	 &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify output file url}&quot;,
	 &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;put&quot;
	}
&#0160;&#0160;&#0160;&#0160;}
}
</code></pre>
<p>&#0160;</p>
<h4>2-2．外部ファイルのiLogicをクラウドストレージに格納し、ActivityのSettingで指定する方法</h4>
<p>2-1では、外部ファイルをWorkItemで指定しましたが、WorkItem実行毎に同じiLogicを実行したい場合など毎回WorkItemでiLogicファイルのURLを指定するのは面倒です。</p>
<p>このような場合には、ActivityにiLogicファイルを指定することも可能です。</p>
<p>この場合の指定は以下のようになります。parameterではなくsettingsセクションでiLogicファイルを指定していることにご注意ください。</p>
<ul style="list-style-type: square;">
<li>Activity</li>
</ul>
<pre><code>{
&#0160;&#0160;&#0160;&#0160;&quot;id&quot;: &quot;{ActivityId}&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;commandLine&quot;: [
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;$(engine.path)\\InventorCoreConsole.exe /i \&quot;$(args[InventorDoc].path)\&quot; /s $(settings[iLogicVb].path)&quot;
&#0160;&#0160;&#0160;&#0160;],
&#0160;&#0160;&#0160;&#0160;&quot;parameters&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;get&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;post&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;engine&quot;: &quot;Autodesk.Inventor+2021&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;settings&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;iLogicVb&quot;:{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;:&quot;{specify input iLogic file url}&quot;,<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;get&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;description&quot;: &quot;Execute specified iLogic file&quot;
}</code></pre>
<div>
<p>&#0160;</p>
<p>この場合WorkItemではiLogicファイルのURLを指定は不要となります。</p>
<ul style="list-style-type: square;">
<li>WorkItem</li>
</ul>
</div>
<pre><code>{
 &#0160;&#0160;&#0160;&quot;activityId&quot;: &quot;{specify activity id + alias}&quot;,
 &#0160;&#0160;&#0160;&quot;arguments&quot;: {
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input file url}&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify output file url}&quot;,
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;put&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;}
}</code></pre>
<h4>&#0160;</h4>
<h4>&#0160;</h4>
<h4>2-3．外部ファイルのiLogicをAppBundleに含める方法</h4>
<p>AppBundleにアップロードするzipファイル内にiLogicファイルを含めて、iLogicを実行する方法です。</p>
<ul style="list-style-type: square;">
<li>AppBundle</li>
</ul>
<p>以下のスクリーンショット例のようにAppBundle内にiLogicVbファイルを含めて、AppBundleを作成します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e97b0c24200b-pi" style="display: inline;"><img alt="Tempsnip" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e97b0c24200b image-full img-responsive" src="/assets/image_928495.jpg" title="Tempsnip" /></a></p>
<p>&#0160;</p>
<ul style="list-style-type: square;">
<li>Activity</li>
</ul>
<p>ActivityでAppbundle内のiLogicVbファイルを/sオプションに指定します。</p>
<p>この時、 AppBundleを&#0160;/al オプションで指定してしまった場合、 /s オプションで指定したiLogicコードは無視され読み込まれないことに注意をしてください。</p>
<pre><code>{
&#0160;&#0160;&#0160;&#0160;&quot;id&quot;: &quot;{ActivityId}&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;commandLine&quot;: [
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;$(engine.path)\\InventorCoreConsole.exe /i \&quot;$(args[InventorDoc].path)\&quot; /s \&quot;$(appbundles[{AppBundle Name}].path)\\iLogicPlugin.bundle\\Contents\\input.iLogicVb\&quot;&quot;
&#0160;&#0160;&#0160;&#0160;],
&#0160;&#0160;&#0160;&#0160;&quot;parameters&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;get&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;post&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;engine&quot;: &quot;Autodesk.Inventor+2021&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;settings&quot;: {}
&#0160;&#0160;&#0160;&#0160;},<br /> &#0160;&#0160;&#0160;&quot;appbundles&quot;: [{AppBundleId + AppBundleAlias}],
&#0160;&#0160;&#0160;&#0160;&quot;description&quot;: &quot;Execute iLogic file inside AppBundle&quot;
}</code></pre>
<div>
<div>
<p>&#0160;</p>
<p>WorkItemからは、入力のiptファイルおよび出力先のURLを指定するだけとなります。</p>
<ul style="list-style-type: square;">
<li>WorkItem</li>
</ul>
</div>
<pre><code>{
 &#0160;&#0160;&#0160;&quot;activityId&quot;: &quot;{specify activity id + alias}&quot;,
 &#0160;&#0160;&#0160;&quot;arguments&quot;: {
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input file url}&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify output file url}&quot;,
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;put&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;}
}</code></pre>
</div>
<p>&#0160;</p>
<h2 style="position: relative; color: white; background: #81d0cb; line-height: 1.4; padding: 0.5em 0.5em 0.5em 0.5em;">Design AutomationでInventor iLogicにパラメータ指定して実行する方法</h2>
<p>さて、ここまでDesing AutomationでiLogicを実行する方法について説明をしてきました。</p>
<p>これまでの説明では、iLogicはすべて、固定のロジックを実行する形となっていましたが、実際には、DesingAutomation実行時にiLogicに値を指定したい場合があるかと思います。ここでは、iLogicに値を渡す方法とiLogicからどのように値を取得するのかを解説をしたいと思います。</p>
<p>&#0160;</p>
<p>実は、iLogicに値を渡す方法はとても簡単でActivity でInventorCoreConsole.exeのコマンドラインに、既定のオプション以外の任意の名前を指定して値を渡すだけとなります。</p>
<p>&#0160;</p>
<p>以下はheightというオプションで値を渡すサンプルとなります。</p>
<p>&#0160;</p>
<ul style="list-style-type: square;">
<li>Activity</li>
</ul>
<pre><code>{
&#0160;&#0160;&#0160;&#0160;&quot;id&quot;: &quot;{ActivityId}&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;commandLine&quot;: [
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;$(engine.path)\\InventorCoreConsole.exe /i \&quot;$(args[InventorDoc].path)\&quot; /s $(settings[iLogicVb].path) /height $args[height].value&quot;
&#0160;&#0160;&#0160;&#0160;],
&#0160;&#0160;&#0160;&#0160;&quot;parameters&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;get&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&quot;height&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;read&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;: &quot;Input.ipt&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;post&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;engine&quot;: &quot;Autodesk.Inventor+2021&quot;,
&#0160;&#0160;&#0160;&#0160;&quot;settings&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;iLogicVb&quot;:{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;:&quot;{specify input iLogic file url}&quot;,<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;get&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;description&quot;: &quot;Execute specified iLogic file&quot;
}</code></pre>
<div>
<p>&#0160;</p>
<p>次に、WorkItemでは作成したパラメータのheightの値（以下の例では10.5）を指定します。</p>
<ul style="list-style-type: square;">
<li>WorkItem</li>
</ul>
</div>
<pre><code>{
 &#0160;&#0160;&#0160;&quot;activityId&quot;: &quot;{specify activity id + alias}&quot;,
 &#0160;&#0160;&#0160;&quot;arguments&quot;: {
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;InventorDoc&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify input file url}&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},<br />&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;height{<br />&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;value&quot;: &quot;10.5&quot;<br />&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;OutputIpt&quot;: {
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;url&quot;: &quot;{specify output file url}&quot;,
&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;&quot;verb&quot;: &quot;put&quot;
&#0160;&#0160;&#0160;  &#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;}
}</code></pre>
<p>&#0160;</p>
<p>このWorkItemを実行するとiLgoic実行時に10.5という値が渡されますが、iLogic側からアクセスするには、iLogicのRuleArguments()関数を使用します。</p>
<p>例えば、次のようなコードでheightパラメータにアクセスをして、指定された値を元にパートのパラメータを変更し、ドキュメントを保存することができます。</p>
<p>&#0160;</p>
<ul>
<li>iLogic</li>
</ul>
<pre><code>Trace.TraceInformation(&quot;Start of MyScript&quot;) 
Trace.TraceInformation(&quot;Setting height to 2 in&quot;)

Parameter(&quot;height&quot;) = RuleArguments(&quot;height&quot;) 

Trace.TraceInformation(&quot;Updating document&quot;) 
InventorVb.DocumentUpdate() 

Trace.TraceInformation(&quot;Saving changes&quot;) 
ThisDoc.Save() 

Trace.TraceInformation(&quot;End of MyScript&quot;)
</code></pre>
<p>&#0160;</p>
<p>RuleArguments関数については、以下に詳細の解説がありますのでご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/ja/support/inventor/learn-explore/caas/CloudHelp/cloudhelp/2019/JPN/Inventor-iLogic/files/GUID-32B66838-22E4-4A0A-B5BB-862350C76B36-htm.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 59px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_651625.jpg" style="width: 50%; height: 50%; min-width: 0; border: 0 none; margin: 0;" width="59" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">iLogic の高度な技法のリファレンス | Inventor 2019 | Autodesk Knowledge Network</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">iLogic ルールは、Inventor パラメータの代入ステートメント、定義済み iLogic 関数、および単純な VB.NET コードのみで記述できます。 ただし、これらの技法のみに限定されているわけではありません....</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<h2 style="position: relative; color: white; background: #81d0cb; line-height: 1.4; padding: 0.5em 0.5em 0.5em 0.5em;">まとめ</h2>
<p>今回の記事では、InventorのiLogicを利用したDesing Automation for Inventorについて、Design Automationの基本的なおさらい、Design AutomationでiLogicを実行する方法、iLogicにパラメータ値を指定して実行する方法について解説をしました。</p>
<p>&#0160;</p>
<p>非常に簡単に、Design AutomationからiLogicを利用できることをご理解いただけたのではないかと思います。</p>
<p>皆様も是非、iLogicを用いたDesing Automation for Inventorのご活用を検討いただければと存じます。</p>
<p>&#0160;</p>
<p>次回の記事では、Desing Automation for InventorでiLogicを使用する場合の注意点について解説をしたいと思います。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
<pre><code></code></pre>

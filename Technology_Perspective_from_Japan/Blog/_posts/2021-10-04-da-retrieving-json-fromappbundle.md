---
layout: "post"
title: "Design Automation API：WorkItem からの JSON 反映"
date: "2021-10-04 00:02:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/10/da-retrieving-json-fromappbundle.html "
typepad_basename: "da-retrieving-json-fromappbundle"
typepad_status: "Publish"
---

<p>Design Automation API を利用するアプリでは、多くの場合、Web ブラウザをフロントエンド インタフェースに使用して、エンドユーザが Design Automation API の AppBundle（アドイン）が処理すべきパラメータを指定します。</p>
<p>パラメータは、 JSON データとて WorkItem の実行領域にファイル（.json）に保存されるので、アドインが JSON ファイルを読み取り、成果ファイルの生成や編集などに反映することが出来ます。</p>
<p>Web ブラウザからパラメータを JSON 渡しする際の Activity や WorkItem の定義は <a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-autocad.html" rel="noopener" target="_blank"><strong>Forge Online - Design Automation：AutoCAD タスクの自動化</strong></a> の記事（<strong>AppBundle での Design Automation</strong>）で、アドインが JSON ファイルを読み取る具体的な方法は、<a href="https://adndevblog.typepad.com/technology_perspective/2021/07/autocad-addin-conversion-to-design-automation-api.html" rel="noopener" target="_blank"><strong>AutoCAD アドインの Design Automation API 化</strong> </a>の記事で、それぞれご紹介しています。</p>
<p>さて、場合によっては、逆に WorkItem（アドイン）が処理した JSON データの内容を、クライアントの Web ブラウザに渡したい場合があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef5b5e4200c-pi" style="display: inline;"><img alt="Da_json" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef5b5e4200c image-full img-responsive" src="/assets/image_152127.jpg" title="Da_json" /></a></p>
<p>例えば、過去にご紹介した次のような処理を挙げることが出来ます。</p>
<p>クライアント コンピュータからアップロードした図面ファイル内をパース、不要になったブロック定義を削除しつつ、削除したブロック定義に含まれていた図形数をグラフ化してレポートするような処理です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded203fd200c-pi"><img alt="Block_cleaner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bded203fd200c image-full img-responsive" src="/assets/image_518007.jpg" title="Block_cleaner" /></a></p>
<p>この処理では、グラフ表示にオープン ソースの <a href="https://www.chartjs.org/" rel="noopener" target="_blank"><strong>Chart.js</strong>（https://www.chartjs.org/）</a>を使用しています。Chart.js では、グラフ化する情報を JSON で定義することになります。</p>
<p>例えば、次の円グラフは、後述の JavaScript コードで描画することが可能です。（<span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#39;graph&#39; は、HTML に定義したグラフ領域を示す &lt;canvas&gt; タグに付けた ID です。）</span></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1260913200b-pi" style="display: inline;"><img alt="Default_chart" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1260913200b image-full img-responsive" src="/assets/image_383651.jpg" title="Default_chart" /></a></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_chart_json&#0160;=&#0160;{
&#0160;&#0160;&#0160;&#0160;type:&#0160;$(&#39;#charttype&#39;).text()/*&#39;pie&#39;*/,
&#0160;&#0160;&#0160;&#0160;data:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;labels:&#0160;[&#39;Block&#0160;A&#39;,&#0160;&#39;Block&#0160;B&#39;,&#0160;&#39;Block&#0160;C&#39;,&#0160;&#39;Block&#0160;D&#39;,&#0160;&#39;Block&#0160;E&#39;,&#0160;&#39;Block&#0160;F&#39;],
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;datasets:&#0160;[{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;label:&#0160;&#39;#&#0160;of&#0160;inner&#0160;element&#39;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;data:&#0160;[40,&#0160;30,&#0160;20,&#0160;10,&#0160;5,&#0160;3],
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}]
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;options:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;plugins:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;colorschemes:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;scheme:&#0160;&#39;brewer.Greys7&#39;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;legend:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;position:&#0160;&#39;right&#39;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;}
};
var&#0160;canvas&#0160;=&#0160;document.getElementById(&#39;graph&#39;);
_chart&#0160;=&#0160;new&#0160;Chart(canvas,&#0160;_chart_json);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>つまり、Design Automation API の AppBundle、ここでは C# を使った .NET API アドインは、図面をパースして、パージしたブロック定義内の要素数をカウント、動的に JSON データを生成する処理の実装が必要になります。</p>
<p>次の C# コードは、アドインが上記処理をする箇所の抜粋です。JSON 生成には、Newtonsoft 社がオープンソースとして公開している <strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/Newtonsoft%20Json.NET" rel="noopener" target="_blank">Json.NET</a>&#0160;</strong>パッケージ（ライブラリ）を使用しています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">  &#0160;Database db = Application.DocumentManager.MdiActiveDocument.Database;
&#0160;&#0160;&#0160;Log(&quot;\nGot database ...&quot;);

&#0160;&#0160;&#0160;InputParams inputParams = JsonConvert.DeserializeObject(File.ReadAllText(&quot;.\\params.json&quot;));
&#0160;&#0160;&#0160;bool boolPurge = inputParams.purge;
&#0160;&#0160;&#0160;bool boolPreview = inputParams.preview;
&#0160;&#0160;&#0160;Log(&quot;\nAddin retrieves Purge:{0}, Preview:{1}&quot;, boolPurge, boolPreview);

&#0160;&#0160;&#0160;try
&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;using (Transaction tr = db.TransactionManager.StartTransaction())
&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;ObjectIdCollection objIds = new ObjectIdCollection();
&#0160;&#0160;&#0160;&#0160;&#0160;BlockTable tbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead, false) as BlockTable;
&#0160;&#0160;&#0160;&#0160;&#0160;IEnumerator enu1 = tbl.GetEnumerator();
&#0160;&#0160;&#0160;&#0160;&#0160;while (enu1.MoveNext())
&#0160;&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;objIds.Add((ObjectId)enu1.Current);
&#0160;&#0160;&#0160;&#0160;&#0160;}

&#0160;&#0160;&#0160;&#0160;&#0160;db.Purge(objIds);
&#0160;&#0160;&#0160;&#0160;&#0160;Log(&quot;\nBlockTableRecords are listed by Purge ...&quot;);

<span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160;&#0160;&#0160;List strLabels = new List();
&#0160;&#0160;&#0160;&#0160;&#0160;List lValues = new List();
&#0160;&#0160;&#0160;&#0160;&#0160;List datasets = new List();</strong>
<strong>
&#0160;&#0160;&#0160;&#0160;&#0160;ObjectId objId;
&#0160;&#0160;&#0160;&#0160;&#0160;BlockTableRecord rec;
&#0160;&#0160;&#0160;&#0160;&#0160;long length;
&#0160;&#0160;&#0160;&#0160;&#0160;IEnumerator enu2 = objIds.GetEnumerator();
&#0160;&#0160;&#0160;&#0160;&#0160;while (enu2.MoveNext())
&#0160;&#0160;&#0160;&#0160;&#0160;{
</strong><span style="color: #111111;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;objId = (ObjectId)enu2.Current;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;rec = tr.GetObject(objId, OpenMode.ForWrite, false) as BlockTableRecord;</span><strong>
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;System.Collections.Generic.IEnumerable idCollection = rec.Cast();
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;length = idCollection.Count();
</strong><span style="color: #111111;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;if (boolPurge)
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;rec.Erase();
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Log(&quot;\n {0} BlockTableRecord\t - contains {1} entities -\t was purged\n&quot;, rec.Name, length);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;else
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Log(&quot;\n {0} BlockTableRecord\t - contains {1} entities -\t can be purged\n&quot;, rec.Name, length);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}</span><strong>
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;strLabels.Add(rec.Name);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;lValues.Add(length);
&#0160;&#0160;&#0160;&#0160;&#0160;}
</strong><span style="color: #111111;">&#0160;&#0160;&#0160;&#0160;&#0160;tr.Commit();
</span><strong>
&#0160;&#0160;&#0160;&#0160;&#0160;datasets.Add(new datasets());
&#0160;&#0160;&#0160;&#0160;&#0160;datasets[0].label = &quot;# of inner element&quot;;
&#0160;&#0160;&#0160;&#0160;&#0160;datasets[0].data = lValues.ToArray();

&#0160;&#0160;&#0160;&#0160;&#0160;data data = new data();
&#0160;&#0160;&#0160;&#0160;&#0160;data.labels = strLabels.ToArray();
&#0160;&#0160;&#0160;&#0160;&#0160;data.datasets = datasets.ToArray();

&#0160;&#0160;&#0160;&#0160;&#0160;OutputParams outputParams = new OutputParams();
&#0160;&#0160;&#0160;&#0160;&#0160;outputParams.type = &quot;pie&quot;;
&#0160;&#0160;&#0160;&#0160;&#0160;outputParams.data = data;
&#0160;&#0160;&#0160;&#0160;&#0160;outputParams.options = new JRaw(&quot;{ \&quot;plugins\&quot;: { \&quot;colorschemes\&quot;: { \&quot;scheme\&quot;: \&quot;brewer.Paired12\&quot; } }, \&quot;legend\&quot;: { \&quot;position\&quot;: \&quot;right\&quot; } }&quot;);
&#0160;&#0160;&#0160;&#0160;&#0160;string output = JsonConvert.SerializeObject(outputParams);
&#0160;&#0160;&#0160;&#0160;&#0160;using (var file = new StreamWriter(@&quot;.\\chart.json&quot;, false, System.Text.Encoding.UTF8))
&#0160;&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;file.Write(output);
&#0160;&#0160;&#0160;&#0160;&#0160;}
</strong></span>&#0160;&#0160;&#0160;&#0160;&#0160;if (boolPurge)
&#0160;&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;string strName = &quot;.\\&quot; + Application.GetSystemVariable(&quot;DWGNAME&quot;) as string;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Application.DocumentManager.MdiActiveDocument.Editor.Command(&quot;QSAVE&quot;);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Log(&quot;\n{0} was saved ...&quot;, strName);
&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;catch (Autodesk.AutoCAD.Runtime.Exception ex)
&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;Log(ex.Message);
&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;finally
&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;}
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>ログ出力やヘルパークラスの定義は次の通りです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"> &#0160;private static void Log(string format, params object[] args) {
&#0160; &#0160;Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(format, args);&#0160;
&#0160; }

&#0160;&#0160;public class InputParams
&#0160;&#0160;{
&#0160;&#0160;&#0160;public bool purge { get; set; }
&#0160;&#0160;&#0160;public bool preview { get; set; }
&#0160;&#0160;}

&#0160;&#0160;public class OutputParams
&#0160;&#0160;{
&#0160;&#0160;&#0160;public string type { get; set; }
&#0160;&#0160;&#0160;public data data { get; set; }
&#0160;&#0160;&#0160;public JRaw options { get; set; }
&#0160;&#0160;}

&#0160;&#0160;public class data
&#0160;&#0160;{
&#0160;&#0160;&#0160;public string[] labels { get; set; }
&#0160;&#0160;&#0160;public datasets[] datasets { get; set; }
&#0160;&#0160;}

&#0160;&#0160;public class datasets
&#0160;&#0160;{
&#0160;&#0160;&#0160;public string label { get; set; }
&#0160;&#0160;&#0160;public long[] data { get; set; }
&#0160;&#0160;}
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>このアドイン処理によって、WorkItem 実行時の作業領域に、グラフ化する情報が chart.json ファイルとして保存されることが分かります。</p>
<p>もちろん、chart.json ファイルを正しくクライアントに渡せるよう、Design Automation API 側の Activity で chart.json 用の動作を定義、WorkItem で処理するように指定することも必須です。</p>
<p>次の JavaScript コード抜粋は、Activity 定義時に <a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/activities-POST/" rel="noopener" target="_blank"><strong>POST activities</strong></a> endpoint へ渡すの JSON Body 記述です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"> &#0160;//&#0160;Create&#0160;Activity
&#0160;&#0160;var&#0160;payload&#0160;=
&#0160;&#0160;{
&#0160;&#0160;&#0160;&quot;id&quot;:&#0160;DA4A_UQ_ID,
&#0160;&#0160;&#0160;&quot;commandLine&quot;:&#0160;[&#39;$(engine.path)\\accoreconsole.exe&#0160;/i&#0160;&quot;$(args[DWGInput].path)&quot;&#0160;/al&#0160;&quot;$(appbundles[PurgeBlock].path)&quot;&#0160;/s&#0160;$(settings[script].path)&#39;],
&#0160;&#0160;&#0160;&quot;parameters&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&quot;DWGInput&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;zip&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;ondemand&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&quot;get&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;description&quot;:&#0160;&quot;Source&#0160;drawing&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;required&quot;:&#0160;true
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;Params&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;zip&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;ondemand&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&quot;get&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;description&quot;:&#0160;&quot;Input&#0160;parameters&#0160;to&#0160;specify&#0160;behavior&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;required&quot;:&#0160;true,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;:&#0160;&quot;params.json&quot;
&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&quot;DWGOutput&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;zip&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;ondemand&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&quot;put&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;description&quot;:&#0160;&quot;Output&#0160;DWG&#0160;drawing&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;required&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;:&#0160;&quot;purged.dwg&quot;
&#0160;&#0160;&#0160;&#0160;},
<span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160;&#0160;&quot;ChartOutput&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;zip&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;ondemand&quot;:&#0160;false,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&quot;put&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;description&quot;:&#0160;&quot;Output&#0160;Chart&#0160;JSON&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;required&quot;:&#0160;true,
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localName&quot;:&#0160;&quot;chart.json&quot;
&#0160;&#0160;&#0160;&#0160;}</strong></span>
&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&quot;settings&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&quot;script&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&quot;value&quot;:&#0160;&quot;PurgeBlock\n&quot;
&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&quot;engine&quot;: &quot;Autodesk.AutoCAD+24_1&quot;,
&#0160;&#0160;&#0160;&quot;appbundles&quot;:&#0160;[DA4A_FQ_ID],
&#0160;&#0160;&#0160;&quot;description&quot;:&#0160;&quot;Purge&#0160;Block&quot;
&#0160;&#0160;};</code></pre>
<p>同様に <a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/" rel="noopener" target="_blank"><strong>POST workitems</strong></a> endpoint に渡す WorkItem の JSON Body は次のようになります。変数 <span style="color: #0000ff;"><strong>CHART_JSON</strong></span> には、予め、WorkItem 実行時に生成しておいた chart.json 入出力用の Signed URL が格納されている点にご注意ください。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"> &#0160;&#0160;&#0160;&#0160;&#0160;//&#0160;Create&#0160;WorkItem
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var&#0160;payload&#0160;=
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;activityId&quot;:&#0160;DA4A_FQ_ID,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;arguments&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;DWGInput&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;:&#0160;signedURLforInput,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;headers&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Authorization&quot;:&#0160;&quot;Bearer&#0160;&quot;&#0160;+&#0160;credentials.access_token,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Content-type&quot;:&#0160;&quot;application/octet-stream&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&quot;get&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Params&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;:&#0160;&quot;data:application/json,&quot;&#0160;+&#0160;paramsJSON
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;DWGOutput&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;:&#0160;signedURLforOutput,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;headers&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Authorization&quot;:&#0160;&quot;Bearer&#0160;&quot;&#0160;+&#0160;credentials.access_token,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Content-Type&quot;:&#0160;&quot;application/octet-stream&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&#39;put&#39;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;localname&quot;:&#0160;SOURCE_DWG
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
<span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;ChartOutput&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;:&#0160;CHART_JSON,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;headers&quot;:&#0160;{
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Authorization&quot;:&#0160;&quot;Bearer&#0160;&quot;&#0160;+&#0160;credentials.access_token,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Content-Type&quot;:&#0160;&quot;application/json&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;:&#0160;&#39;put&#39;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;},</strong></span>
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;onComplete&quot;: {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;verb&quot;: &quot;post&quot;,
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;url&quot;: &quot;-<em>deployed root URL-</em>/api/oncomplete&quot;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;}
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;};</code></pre>
<p>今回の例では、確認の目的でクライアント側に Forge Viewer を配置しています。Model Derivative API で変換した SVF/SVF2 を Viewer 上に表示するには、viewables:read の <a href="https://adndevblog.typepad.com/technology_perspective/2019/06/scopes-on-oauth.html" rel="noopener" target="_blank"><strong>Scope</strong></a>（スコープ）を持つ Access Token（アクセス トークン）を利用するのが一般的です。一方、Design Automation API の各種処理（endpoint 呼び出し）には、code:all の Scope を持つ Access Tokenが必要です。</p>
<p>クライアント側に code:all の Scope を持つ Access Token が渡ってしまうのは、セキュリティ上、好ましい状態ではありませんので、Forge を利用する Web サーバー（Forge アプリ）で独自にルーティングした endpoint を用意して、 code:all の Scope を持つ Access Token をクライアントから隠蔽している点にご注意ください。Forge Viewer を持つ Forge アプリでは、通常、このような実装がおこなわれています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdef5ff00200c-pi" style="display: inline;"><img alt="Da_app_implementation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdef5ff00200c image-full img-responsive" src="/assets/image_727531.jpg" title="Da_app_implementation" /></a></p>
<p>この例では、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank"><strong>GET workitems/:id</strong></a> endpoint を使ったポーリング処理で WorkItem の完了を検出し次第、クライアントから Web サーバー上にルーティングした endpoint を呼び出し、前述の Signed URL からグラフ用に生成した JSON データを得るようになっています。</p>
<p>Node.js で実装した Chart.js 用の JSON 取得用 endpoint 実装は次のとおりです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">// Get Chart.json on bucket
router.get(&quot;/get-chart&quot;, function (req, res) {
&#0160;&#0160;&#0160;&#0160;https.get(<span style="color: #0000ff;"><strong>CHART_JSON</strong></span>, function (chartres) {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;var body = &#39;&#39;;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;chartres.setEncoding(&#39;utf8&#39;);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;chartres.on(&#39;data&#39;, function (chunk) {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;body += chunk;
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;console.log(&quot; Chart JSON = &quot; + body);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;res.end(body);
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;});
&#0160;&#0160;&#0160;&#0160;}).on(&#39;error&#39;, function (e) {
&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;console.log(e.message);
&#0160;&#0160;&#0160;&#0160;});
});</code></pre>
<p>クライアント（Web ブラウザ）からの上記 endpoint 呼び出し（AJAX）とグラフ更新は、次の JavaScript コードが担います。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    // Get Chart JSON
    uri = &#39;/api/get-chart&#39;;
    $.ajax({
     url: uri,
     type: &#39;GET&#39;,
     contentType: &#39;application/json&#39;
    }).done(function (res) {

<span style="color: #0000ff;"><strong>     _chart.data = JSON.parse(_chart_json).data;
     _chart.options = JSON.parse(_chart_json).options;
     _chart.update();</strong></span>

     if (JSON.stringify(JSON.parse(JSON.stringify(JSON.parse(res).data)).labels) === &#39;[]&#39;) {
      console.log(&quot;!!! No blocks to be purgeable&quot;);
      alert(&quot;No blocks to be purgeable&quot;);
     }
    }).fail(function (jqXHR, textStatus, errorThrown) {
     console.log(&#39;Failed to get Chart JSON: &#39;, jqXHR, textStatus, errorThrown);
    });
</code></pre>
<p>※ 本記事は 2022年8月 に「Design Automation API：AppBundle からの JSON 取得」から「Design Automation API：WorkItem からの JSON 反映」に改題しました。</p>
<p>By Toshiaki Isezaki</p>

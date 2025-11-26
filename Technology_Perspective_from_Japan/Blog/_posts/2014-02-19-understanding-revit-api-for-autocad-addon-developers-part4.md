---
layout: "post"
title: "AutoCAD アドオン開発者のための Revit API 入門～データ"
date: "2014-02-19 00:50:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/02/understanding-revit-api-for-autocad-addon-developers-part4.html "
typepad_basename: "understanding-revit-api-for-autocad-addon-developers-part4"
typepad_status: "Publish"
---

<p>今回は、アドインの場合でもマクロの場合でも、Revit API で処理を実装する上で重要な データについて、AutoCAD との違いをご案内していきましょう。まずは、Revit で扱うことになるファイルから整理していきます。</p>
<p><strong>Revit のデータファイル</strong></p>
<p>AutoCAD の場合、図面ファイルは .dwg の拡張子を持つファイルを採用していますが、その用途によって拡張子を変更しています。例えば、図面テンプレートには .dwt の拡張子を、標準仕様図面には .dws の拡張子をそれぞれ適用されているはずです。Revit で扱うファイルにも複数の拡張子があって、その目的によって拡張子が異なっています。</p>
<p style="padding-left: 30px;"><strong>プロジェクト ファイル（.rvt ファイル）</strong></p>
<p style="padding-left: 30px;">実運用で利用するファイルです。このファイルに BIM も基づいた3D モデルを作成したり、シートと呼ばれる 2D 図面を作成していきます。ちょうど、AutoCAD のモデル空間にある 3D モデルと、レイアウト（ペーパー空間）の 2D 図面といった関係に似ています。AutoCAD と異なるのは、シートにおける表現でも、平面図、立面図、断面図、集計表 など、Revit によって情報やタイプが厳密に管理されている点です。</p>
<p style="padding-left: 30px;">また、プロジェクト ファイルには、BIM に沿ったファミリがプロジェクト内の 3D モデルとして配置されている、いないにかかわらず、最低限のファミリ定義情報が多数含まれています。AutoCAD で言えば、ユーザ定義のブロック定義と考えることが出来ますが、ほぼすべての建設・設備・構造の表現にファミリを利用する Revit では、この影響で、ファイル サイズの大きさが相対的に大きくなる傾向にあります。画面上に一切モデルが作図されていない新規プロジェクトでも、一定以上のファイルサイズなのは、このためです。</p>
<p style="padding-left: 30px;">次の画像は、建設テンプレートを基に新規に作成したプロジェクト ファイル上で、[プロジェクト ブラウザ] を使って見た定義済のファミリ情報です。ドアと壁を展開表示していますが、定義済のタイプが複数存在することがわかります。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fb827aff970b-pi" style="display: inline;"><img alt="Project_browser" class="asset  asset-image at-xid-6a0167607c2431970b01a3fb827aff970b" src="/assets/image_407483.jpg" title="Project_browser" /></a></p>
<p style="padding-left: 30px;">プロジェクトに定義されていないファミリが、後述するファミリ ファイル（.rfa）ファイルをロードして、プロジェクト ファイルに取り込むことも出来ます。</p>
<p style="padding-left: 30px;"><strong>プロジェクト テンプレート ファイル（.rte ファイル）</strong></p>
<p style="padding-left: 30px;">プロジェクトを新規に作成する際に利用するテンプレート ファイルで、Revit のインストール時に日本語環境用のテンプレートが多数インストールされます。Buliding Design Suite に含まれる OneBox Revit では、建設テンプレート、建築テンプレート、構造テンプレート、機械テンプレートがインストールされているはずです。プロジェクト テンプレートには、一般的なファミリ定義を含む各種既定情報が含まれているので、適切なテンプレートを参照してプロジェクト ファイルを作成することで、すぐに BIM モデルを作成していくことが可能です。</p>
<p style="padding-left: 30px;"><strong>ファミリ ファイル（.rfa ファイル）</strong></p>
<p style="padding-left: 30px;">ファミリの定義ファイルで、ファミリ ドキュメント ファイルとも呼ばれることがあります。Revit 上でファミリ ファイルを&#0160;開くと、ファミリ エディタを利用してファミリ定義を作成していくことが出来ます。具体的には、ファミリ形状の定義とパラメータの適用、複数のタイプの追加、といった操作が可能です。ファミリ エディタは、AutoCAD で言うブロック エディタのような感じです。</p>
<p style="padding-left: 30px;">ファミリ ファイルには、3D 表示時のジオメトリ表現や、2D 詳細図時のジオメトリ表現など、多彩な表現を異なるタイプとともに定義していくことが出来ます。また、どの部分にパラメータを与えて、パラメトリックな変更に対応させるかを、拘束情報とともに付加していきます。</p>
<p style="padding-left: 30px;">Revit API の視点では、ファミリ エディタでの操作をカバーできる唯一の環境です。異なる言い方をするなら、Revit API でファミリ定義情報を直接編集できるのは、ファミリ ファイル（コンポーネント ファミリ）を Revit で開いているときに限られます。</p>
<p style="padding-left: 30px;">ファミリ ファイルには、作成しようとするファミリによって、含まれるカテゴリやタイプ情報が異なります。つまり、ファミリによって、API からアクセスできるカテゴリ、タイプ、パラメータが異なります。この点は、プロジェクトファイルにファミリ インスタンスを作成した場合にも影響を持ち続けることになります。</p>
<p style="padding-left: 30px;"><strong>ファミリ ドキュメント テンプレート ファイル（.rft ファイル）</strong></p>
<p style="padding-left: 30px;">ファミリ ファイルを作成しようとする際に参照するテンプレート ファイルです。Revit には、あらかじめ複数のファミリ テンプレートファイルが用意されていますが、用意されていないタイプのファミリを新規に作成する場合には、「一般モデル(メートル単位).rft」を参照して作成してくことも出来ます。</p>
<p style="padding-left: 30px;">ファミリ ファイルと同様に、ファミリによって、含まれるカテゴリやタイプ情報が異なります。&#0160;</p>
<p><strong>ファミリ</strong></p>
<p>Revit API を分かり難くしている要因とも言えますが、ファミリを理解すれば、API を使ったカスタマイズを容易に把握することが出来ます。ここで、ファミリ関連用語として、Revit API 上で出てくる複数の用語を説明しておきます。これらの違いを理解するだけでも、最初の印象がだいぶ変わると思います。</p>
<p>ファミリという表現を Revit API 上で扱う場合、通常、次の 3 タイプのいずれかを表します。</p>
<p style="padding-left: 30px;"><strong>ファミリ</strong></p>
<p style="padding-left: 30px;">ファミリ定義情報です。プロジェクト ファイルのファミリ定義情報の場合、その情報にアクセスは出来ますが、定義情報そのものを編集することが出来ません。ファミリには、最低 1 つ、通常複数の<strong>ファミリ シンボル</strong>が含まれます。</p>
<p style="padding-left: 30px;"><strong>ファミリ シンボル</strong>&#0160;</p>
<p style="padding-left: 30px;">ファミリ内に定義された特定の <strong>ファミリ タイプ&#0160;</strong>を示します。通常、ファミリには複数のタイプが用意されますが、そのうちの 1 タイプのみを表現する FamilySymbol クラスで定義されています。ファミリ シンボルという表現は、Revit では API でのみ利用するので、Revit のユーザ インタフェースにはない表現です。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51032f070970c-pi" style="display: inline;"><img alt="Family_symbol" class="asset  asset-image at-xid-6a0167607c2431970b01a51032f070970c" src="/assets/image_90634.jpg" title="Family_symbol" /></a>&#0160;</p>
<p style="padding-left: 30px;"><strong>ファミリ インスタンス</strong></p>
<p style="padding-left: 30px;">最低限のファミリ定義情報は、プロジェクト ファイルにあらかじめ登録されていますが、それらを使ってプロジェクト ファイル内の作図領域に配置されたファミリ シンボルと考えることが出来ます。</p>
<p style="padding-left: 30px;">AutoCAD で説明するなら、図面ファイル内のブロック定義を参照するブロック参照の位置づけです。配置されたファミリ インスタンスは、同じタイプであっても、個別の要素として識別されます。FamilyInstance クラスで定義されています。</p>
<p>これら 3 つの表現とは別に、ファミリには 3 つの種類が存在します。ファミリの種類によっては、API アクセスに制限が出る状況が存在します。</p>
<p style="padding-left: 30px;"><strong>コンポーネント ファミリ（標準ファミリ）</strong></p>
<p style="padding-left: 30px;">ファミリ ファイル（.rfa）に定義することが出来るファミリです。窓やドアといった建具や、家具、スポット ライト、ダクトなど、国やメーカーによって様々なファミリ ファイルを事前に作成して、プロジェクトにロードして利用することが出来ます。&#0160;</p>
<p style="padding-left: 30px;">Revit API でファミリの編集をおこなえるのは、コンポーネント ファミリを Revit で開いてファミリ エディタが起動している間のみです。</p>
<p style="padding-left: 30px;">プロジェクト ファイルにロードされたコンポーネント ファミリに対しては、API アクセスはできますが、新しいタイプ（ファミリ シンボル）を作成するなどの編集操作は出来ません。</p>
<p style="padding-left: 30px;"><strong>システム ファミリ</strong></p>
<p style="padding-left: 30px;">Revit 組み込み（ファミリ テンプレート組み込み）のファミリで、 独立したファミリ ファイル（.rfa）として保存したり、編集したりすることは出来ません。壁や屋根など、他のファミリ インスタンス化したコンポーネント ファミリをホストできるファミリがシステム ファミリである傾向があります。</p>
<p style="padding-left: 30px;">そんな場合でも、Revit API でシステム ファミリを直接編集することが出来ません。</p>
<p style="padding-left: 30px;"><strong>インプレイス ファミリ</strong></p>
<p style="padding-left: 30px;">Revit 上に開いて編集中の&#0160;プロジェクト ファイル内で、[インプレイスを作成] コマンドを使って作成したコンポーネント ファミリと考えることが出来ます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b04101112970d-pi" style="display: inline;"><img alt="Inplace_family" class="asset  asset-image at-xid-6a0167607c2431970b019b04101112970d" src="/assets/image_806194.jpg" title="Inplace_family" /></a></p>
<p style="padding-left: 30px;">ファミリ ファイルをして保存できないので、Revit API では編集することは出来ません。&#0160;</p>
<p><strong>カテゴリ</strong></p>
<p>カテゴリも Revit で扱うデータには重要です。プロジェクト ファイル内には、ファミリやファミリ インスタンスの他に、非ファミリ オブジェクトと呼ばれるビューやマテリアルなどが混在していますが、いずれの要素も、その種類を示すカテゴリ情報を持っています。例えば、「450 x 450 mm」角柱ファミリも「600 x 600 mm」角柱ファミリも、柱カテゴリに属します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fb8e85f5970b-pi" style="display: inline;"><img alt="Categories" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fb8e85f5970b image-full img-responsive" src="/assets/image_195936.jpg" title="Categories" /></a></p>
<p>ほんの少ししか紹介していませんが、このように、Revit はドキュメントの種類とファミリの種類、また、Revit 上でのドキュメントの編集状態によって、Revit API でアクセスできる範囲が変わってきます。Revit 製品をよく理解しない状態で Revit API を習得しようとすると、かなり習得するのが困難に感じてしまう点を否めません。API に取り組む前には、出来るだけ Revit 製品自体の習得に努めていただくことを強くお勧めします。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>

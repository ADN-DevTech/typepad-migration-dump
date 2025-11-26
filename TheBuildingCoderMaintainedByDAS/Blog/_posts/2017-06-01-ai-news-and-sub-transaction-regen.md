---
layout: "post"
title: "AI News and Sub-Transaction Regen"
date: "2017-06-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AI"
  - "Deep Learning"
  - "News"
  - "Regen"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/06/ai-news-and-sub-transaction-regen.html "
typepad_basename: "ai-news-and-sub-transaction-regen"
typepad_status: "Publish"
---

<p>Things continue moving fast in AI, and
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33">need to regenerate</a> in
the Revit API remains unchanged:</p>

<ul>
<li><a href="#2">AI News</a></li>
<li><a href="#3">Subtransaction Requires Regeneration</a></li>
</ul>

<h4><a name="2"></a>AI News</h4>

<p>I <a href="http://thebuildingcoder.typepad.com/blog/2016/01/bim-programming-madrid-and-spanish-connectivity.html#7">reported</a> on
the surprising success of <a href="https://en.wikipedia.org/wiki/AlphaGo">AlphaGo</a> 18
months ago, when it unexpectedly defeated legendary player Lee Se-dol 4-1, cf.
the <a href="https://www.theverge.com/google-deepmind">DeepMind Go challenge overview</a>.</p>

<p>That revolutionised the expectation and perception of AI of scientists and the industry alike.</p>

<p>Now another important step was taken, completely settling the matter:
<a href="https://www.theverge.com/2017/5/27/15704088/alphago-ke-jie-game-3-result-retires-future">AlphaGo retires from competitive Go after defeating world number one Ke Jie 3-0</a>.</p>

<p>In this game, Ke Jie made use of some unconventional new moves that AlphaGo invented and first demonstrated in its previous public games... nota bene, it invented new moves after 3000 years of human exploration... one question this raises: 'Invent'? Or discover?</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2884627970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2884627970c img-responsive" style="width: 300px; " alt="Go game" title="Go game" src="/assets/image_f16199.jpg" /></a><br /></p>

<p></center></p>

<p>The DeepMind research team behind AlphaGo has conclusively proved its point and is moving on to new and greener pastures.</p>

<p>Another recent impressive example of what AI enables is provided by
the <a href="https://siliconangle.com/blog/2017/05/28/startup-uses-ai-create-gui-source-code-simple-screenshots">startup that uses AI to create programs from simple screenshots</a>,
cf. the corresponding <a href="https://arxiv.org/pdf/1705.07962.pdf">research paper <em>pix2code: Generating Code from a Graphical User Interface Screenshot</em></a>.</p>

<p>Exciting times indeed.</p>

<p>Back to the Revit API:</p>

<h4><a name="3"></a>Sub-Transaction Requires Regeneration</h4>

<p>Alexander Ignatovich, <a href="https://github.com/CADBIMDeveloper">@CADBIMDeveloper</a>,
aka Александр Игнатович, shares another important example that fits in perfectly with our continuing series demonstrating
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.33">need to regenerate</a>.</p>

<p>In his own words:</p>

<blockquote>
  <p>It's time to share some piece of knowledge about sub-transactions in your brilliant blog ;-)</p>
  
  <p>I've spent some time, so it should be documented somewhere.</p>
  
  <p>As most Revit programmers know, the document automatically regenerates on transaction commit.</p>
  
  <p>My problem was, that sub-transactions do not cause this, as I previously assumed.</p>
  
  <p>I wanted to retrieve family symbol geometry. I activated the family symbol earlier in a sub-transaction, but I didn't call the <code>document.Regenerate</code> method, so the <code>familySymbol.get_Geometry</code> method returned null. I was confused, because in the debugger I saw that my family symbol is active. I also looked at the activated family symbol geometry, and it was not null.</p>
  
  <p>After I added document regeneration, my code execution path looks like this:</p>
</blockquote>

<pre class="code">
  start main transaction
  {
    ...

    start sub transaction
    {
      ...

      if (!familySymbol.IsActive)
        familySymbol.Activate()

      ...

      subtransaction.Commit()
    }

    document.Regenerate()

    ...

    geometry = familySymbol.get_Geometry(options)

    ...
  }
</pre>

<blockquote>
  <p>I previously considered sub-transactions as mini transactions with practically the same behaviour, except they are nested to the transaction and make real changes to model only when outer transaction is committed.</p>
  
  <p>Now I understand that nothing is really committed automatically to the Revit database before completion and commitment of the outer-most transaction.</p>
</blockquote>

<p>Many thanks to Alexander for explicitly pointing this out!</p>

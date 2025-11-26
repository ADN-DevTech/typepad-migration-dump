---
layout: "post"
title: "Importing Python scripts from a different path than predefined ones"
date: "2013-02-22 02:33:48"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "FBX"
  - "Maya"
  - "MotionBuilder"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2013/02/importing-python-scripts-from-a-different-path-than-predefined-ones.html "
typepad_basename: "importing-python-scripts-from-a-different-path-than-predefined-ones"
typepad_status: "Publish"
---

<p>This post applies to Maya, but MotionBuilder as well (or any Python based application).</p>
<p>The other day I wanted to source a Python script into Maya and he took me few minutes to realize there was no easy/automatic way to do this in Maya other than loading the script into the Maya Script editor and run it unlike with MEL and the source command where you can give the full path name of the MEL script.</p>
<p>Fine, but I wanted to do it from an application. Why that? I usually reply to that question why not ;)<br />No seriously, imagine you got Maya already running, you download a script, you got the choice to copy it to one of the Maya user&#39; profile &#39;scripts&#39; folders, and then using MEL, or C++ source the Python script using import. But what if you put the script somewhere else?</p>
<p>I first thought, well that is easy - all Python needs is to have the PYTHONPATH setup properly, and MEL gives us an easy way to change it. Something along this lines:</p>
<pre class="brush: cpp; toolbar: false;">{
	string $py =`getenv PYTHONPATH` ;
	string $me =&quot;/MyPath/scripts&quot; ;
	string $test =`match $me $py` ;
	if ( $test == &quot;&quot; ) {
		if ( `about -nt` ) {
			$py +=&quot;;&quot; + $me ;
		} else {
			$py +=&quot;:&quot; + $me ;
		}
		putenv PYTHONPATH $py ;
	}
}
</pre>
<p>The code is between {} to avoid global namespace clashing, but the getenv/putenv approach does not work, when you do a &#39;import myscript&#39; you&#39;ll get an error like this:</p>
<p>&#0160; &#0160; # Error: line 1: ImportError: file &lt;maya console&gt; line 1: No module named myscript #&#0160;</p>
<p>But if you read the Python documentation, that is supposed to work :(</p>
<p>The trick is that, maybe MEL updates the PYTHONPATH, but the Python engine did not saw the change. If you now run this code:</p>
<pre class="brush: python; toolbar: false;">import sys
for item in sys.path:
    print item
</pre>
<p>You won&#39;t see your new path, and this is why it does not work. So instead of doing the pure MEL code, you need to do it from Python, but then this is &#39;<a href="http://en.wikipedia.org/wiki/Chicken_or_the_egg" target="_self">The chicken or the egg causality dilemma</a>&#39; since to do it from Python we need to load our python script :(</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d41365782970c-pi" style="display: inline;"><img alt="Chicken_egg_dilema" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d41365782970c" src="/assets/image_9aa37e.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Chicken_egg_dilema" /></a></p>
<p>Again MEL comes to the rescue, and you can run this code instead:</p>
<pre class="brush: cpp; toolbar: false;">{
	string $me =&quot;/MyPath/scripts&quot; ;
	python (&quot;import sys&quot;) ;
	string $py[] =python (&quot;sys.path&quot;) ;
	if ( stringArrayCount ($me, $py) == 0 )
		python (&quot;sys.path.insert(0, &#39;&quot; + $me + &quot;&#39;)&quot;) ;
}
</pre>
<p>And next time you do &#39;import myscript&#39;, that will work just fine.</p>
<p>From that we could easily create a command to temporary add a path, source the python script into the Python engine as Python will never load again the script once it is loaded, even if you &#39;import&#39; it again. In Maya you need to use &#39;reload&#39; or &#39;force&#39; in Python.</p>
<p><strong>For the anecdote</strong>, we now have a scientific proof from genetic studies, that the egg was first %\</p>

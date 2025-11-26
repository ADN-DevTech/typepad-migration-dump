---
layout: "post"
title: "Got it right in MEL"
date: "2013-09-19 00:40:25"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Maya"
  - "MEL"
original_url: "https://around-the-corner.typepad.com/adn/2013/09/got-it-right-in-mel.html "
typepad_basename: "got-it-right-in-mel"
typepad_status: "Publish"
---

<p>evalDeferred() never worked with unquoted commands. Recently, someone reported that Maya 2014 works differently than previous releases when using evalDeferred(). the issue came-up with the following code:</p>
<pre class="brush: cpp; toolbar: false;">evalDeferred(print("test"))</pre>
<p>When MEL sees a function call being passed as a parameter to another function, it calls that function first and then passes the result down to the calling function. That is a general rule, and applies everywhere in MEL like in other languages. So MEL is not different here.</p>
<p> In the example of evalDeferred(), MEL called 'print("test")' and got back an empty result, which it then passed on to evalDeferred(). evalDeferred() scheduled that empty result for execution on the next idle event at which time the command would silently fail since an empty result is not a valid command string.
For example, if you do the following in Maya 2013:</p>
<pre class="brush: cpp; toolbar: false;">print("before\n");
evalDeferred(print("during\n"));
print("after\n");
</pre>
<p>you get the following result:</p>
<pre class="brush: cpp; toolbar: false;">before
during
after
</pre>
<p>Note that 'during' is printed before 'after', whereas if it had been properly deferred it would have been printed last. If instead you quote the parameter to evalDeferred:</p>
<pre class="brush: cpp; toolbar: false;">print("before\n");
evalDeferred("print(\"during\\n\")");
print("after\n");
</pre>
<p>you get the correct result:</p>
<pre class="brush: cpp; toolbar: false;">before
after
during
</pre>
<p>
The difference in Maya 2014 is that MEL correctly recognizes that the result returned by the 'print' command is not a string and flags the error right away, rather than later on when the idle queue runs.
Where does this leave you?
If the expression being passed to evalDeferred() is a valid string expression (e.g. a call to a function which returns a string) then it will still work as before.
If the expresion being passed to evalDeferred() is NOT a valid string expression and NEEDED to be deferred for proper operation, then your script was not working properly before but now you'll get an error alerting you to that.
The only situation in which this will break a working script is if the expression being passed to evalDeferred() is a valid string expression which didn't really need to be deferred. But in that case, no need of evalDeferred(), the issue remaining would be that the code is not executed at the same time anymore.</p>
<p>&nbsp;</p>

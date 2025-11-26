---
layout: "post"
title: "Select All Elements in the Viewer with View and Data API with JavaScript"
date: "2015-12-06 18:40:53"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/12/select-all-elements-in-the-viewer-with-view-and-data-api-with-javascript.html "
typepad_basename: "select-all-elements-in-the-viewer-with-view-and-data-api-with-javascript"
typepad_status: "Publish"
---

<p style="text-align: left;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/shiya-luo.html">Shiya Luo</a></p>
<p>Selecting objects in the viewer requires you to pass in the dbId's of the elements you'd like to select. The select(dbid) function in the Viewer3D class lets you pass in either an integer which is the dbId of the element to select, or an array containing the dbId's of all the elements to select.</p>
<p>Which means to select all the elements, you just need to pass in ALL the dbId's of your model.</p>
<p>The elements are stored as an object tree in the viewer. The getObjectTree(successCallback, errorCallback) function in the Viewer3D class lets you manipulate the model in a tree structure. To get the tree, simply:</p>
<pre>var tree;
//viewer is your viewer object
viewer.getObjectTree(function (objTree) {
	tree = objTree;
});</pre>
<p>This saves the object tree as a variable.</p>
<p>Once you got the tree, you want to start with the root:</p>
<pre>var root = tree.root;</pre>
<p>With the root, you can now do anything with the tree. Each node in the tree contains all the info you can use to trace back to the original model. There are things like</p>
<pre>node.children</pre>
<p>which gives you all the children of the node, and</p>
<pre>node.parent</pre>
<p>which gives you all the parents of the node.</p>
<p>The most important one in this case is the</p>
<pre>node.dbId</pre>
<p>Which gives you the dbId of the element(node).</p>
<p>Now, to select all the elements in the viewer, just write a function that iterate through the tree and push all the dbId's of the nodes into an array. You will pass in this array in the select(dbId) function to select all the elements.</p>
<pre>function getAlldbIds (root) {
	var alldbId = [];
	if (!root) {
		return alldbId;
	}
	var queue = []; 
	queue.push(root); //push the root into queue
	while (queue.length &gt; 0) {
		var node = queue.shift(); // the current node
		alldbId.push(node.dbId);
		if (node.children) {
			// put all the children in the queue too
			for (var i = 0; i &lt; node.children.length; i++) {
				queue.push(node.children[i]);
			}
		}
	};
	return alldbId;
};
</pre>
<p>This is a very simple&nbsp;breadth-first search algorithm.</p>
<p>To select all the functions, simply pass in all the dbIds:</p>
<pre>var allDbIds = getAlldbIds(root);
viewer.select(allDbIds);</pre>
<p>All the elements are selected in the viewer.</p>
<p>&nbsp; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f62e42970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f62e42970b img-responsive" style="margin: 0px 5px 5px 0px;" title="Original" src="/assets/image_3372e8.jpg" alt="Original" /></a></p>

<p>---------------------------</p>
<p>Version 2.5 update:</p>
<p>After version 2.5, the code to get instance tree has changed slightly, see the post: http://adndevblog.typepad.com/cloud_and_mobile/2016/03/breaking-change-in-accessing-model-instance-tree-with-view-data-api.html</p>
<p>Now, to get the instance tree:</p>
<pre>var instanceTree = viewer.model.getData().instanceTree;
var rootId = this.rootId = instanceTree.getRootId();</pre>
<p>With the root id, to get all the ids, use the following code snippet:</p>
<pre>function getAlldbIds (rootId) {
	var alldbId = [];
	if (!rootId) {
		return alldbId;
	}
	var queue = [];
	queue.push(rootId);
	while (queue.length > 0) {
		var node = queue.shift();
		alldbId.push(node);
		instanceTree.enumNodeChildren(node, function(childrenIds) {
			queue.push(childrenIds);
		});
	}
	return alldbId;
}</pre>

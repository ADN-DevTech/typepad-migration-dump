---
layout: "post"
title: "Maya’s Deformer Architecture"
date: "2012-09-25 23:20:00"
author: "Cyrille Fauvel"
categories:
  - "Cyrille Fauvel"
  - "Geometry"
  - "Maya"
  - "Modeling"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/maya-deformer-architecture.html "
typepad_basename: "maya-deformer-architecture"
typepad_status: "Publish"
---

<p>This post is the <strong>second</strong> of a series of post which will talk about public information and source code available on Maya algorithms. Not sure if you noticed it, but the first one was on the <a href="http://around-the-corner.typepad.com/adn/2012/08/maya-fluid-shader.html" target="_self">Maya Fluid Shader</a>. </p>
<p>Recently we had a request for more information about MFnSkinCLuster class as the documentation does not provide all the information about how the whole thing is working and the Maya devkit samples only access an already existing skinned mesh vs create.</p>
<p>The algorithm itself is widely
available on the web and is described in <a href="http://pubs.cs.uct.ac.za/archive/00000403/01/skinning_afrigraph.pdf" target="_self">this review paper</a>&#0160;(the one called “Skeletal Subspace Deformation”). The paper also gives a lot of references to other places where it is covered.</p>
<p>In Maya internals,&#0160;Deformer nodes are designed to contain algorithms that move points. Examples of deformers are lattices, blendShapes, skinning, and clusters. User-defined deformers can also be written the API using the MPxDeformerNode class.</p>
<p><strong>Deformers in the DG</strong><strong>&#0160;</strong></p>
<p>Deformers are placed in the construction history of the shape being deformed. A typical dependency graph set-up including deformers is shown below:</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017744de3ff6970d-pi" style="display: inline;"><img alt="Deformer_dg" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017744de3ff6970d image-full" src="/assets/image_edef8c.jpg" title="Deformer_dg" /></a><br /><em>Hypergraph
view of deformers in the history of a shape</em></p>
<p>At the far right, pSphereShape is the deformed shape node. At the far left, pSphereShapeOrig is the so-called intermediate object. The intermediate object is never displayed (unless you disable its intermediateObject attribute), but is at the head of every chain of deformers. It serves as a storage place for the original undeformed positions of the shape.</p>
<p>In a typical deformer chain, shape data flows out of the intermediate object and then flows through each of the deformers. In this example, the deformers that are traversed are highlighted in yellow: tweak1, cluster1 and ffd1. In this example, only a single shape flows through ffd1. However, most deformers can operate on any number of shapes since the input and output geometry attributes of the deformer base class are multis.
The two exceptions to this rule are skinClusters and the wrap node. Because of
implementation details, the skinCluster and the wrap node can operate on only a
single geometry. Additional geometries connected to the skinCluster and the wrap
will be ignored.</p>
The dotted lines in the hypergraph view above indicate that some auxiliary nodes are not displayed. In this case, the auxiliary nodes are groupParts nodes which are used to store component lists for the deformer sets.
<p><strong>Deformers and Sets</strong><strong>&#0160;</strong></p>
<p>Each deformer is associated with a single set. It will deform only the points that are included in its set. If you add points to a deformers using the sets command, the deformer will automatically be wired up to the construction history of the shape. Similarly, if you remove points from a set, the deformer will stop acting upon those points. The set used by the deformer is a vertex-restricted set: it can contain only vertices. Given a deformer, the easiest way to find its set is using the MFnGeometryFilter::deformerSet() method. The returned set can then be used to create an MFnSet which can be used to access membership information. For example:</p>
<pre class="brush: cpp; toolbar: false;">MFnGeometryFilter fnDeformer(defObject);
MObject setObject =fnDeformer.deformerSet();
MFnSet fnSet(setObject);
MSelectionList members;
fnSet.getMembers(members,false);
MItSelectionList itr(members);
for ( ; !itr.isDone(); itr.next() ) {
	MDagPath path;
	MObject components;
	itr.getDagPath(path, components);
	...
}
</pre>
<p>The findDeformersCmd.cpp example plug-in contains the entire source of the above code fragment.</p>
<p><strong>The GroupParts node and GroupId node</strong></p>
<p>Ideally, as a Maya user or API
developer, there is no reason to understand the groupParts node or the other
connections that Maya uses to represent sets. However, people frequently ask
about them, so here is some background information.</p>
<p>Most sets in Maya are composed of only a single node: the objectSet node. However, component sets in Maya are different.
Component sets are stored within the shape data rather than within the objectSet node. The set is connected to a groupId node per shape, and a groupParts node per &quot;construction-historied&quot; shape. If you look at the history of a shape in the hypergraph, by default Maya will hide the groupParts and the groupId because they clutter the view and are not of interest. If you turn on display of &quot;auxiliary nodes&quot;, the groupParts and groupId will be drawn.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c3200cb7e970b-pi" style="display: inline;"><img alt="Deformer_dg_group" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c3200cb7e970b image-full" src="/assets/image_798fc6.jpg" title="Deformer_dg_group" /></a><br /><em>Hypergraph
view showing the groupParts, groupId and objectSet node for an ffd deformer.
Note that the tweak node was deleted from this shape&#39;s history in order to make
the figure less cluttered.</em></p>
<p>The groupId node is simply a means
of storing an id that is unique within the scene for fast look-up of the
membership of the set within the shape&#39;s list of sets.</p>
<p>The groupParts node stores component
sets for shapes with construction history. This is because as the shape data
flows through the history, it may change its numbering. Component lists are
stored using the vertex index and some construction history nodes modify the
vertex numbering - for example, &quot;polySplit&quot;,
&quot;deleteComponent&quot;, etc. Because the groupParts is in the construction
history, it remains immune to the vertex numbering changes. It receives the
shape data before the vertices are renumbered. This architecture has proved
useful for certain poly modeling operations. However, it can also causes
problems. If you modify the history ahead of a groupParts, your set may no
longer going to contain reasonable indices. Or if you modify the topology after
a groupParts, it can then be very difficult to edit weights or other details
earlier in the dependency graph that rely on a different indexing scheme. </p>
<p>For example, let&#39;s say you create a
10x10 polyPlane with history. You select the upper vertices in the plane, and
then create a cluster. The cluster will act on the selected vertices, which are
contained in its set. Now you select the history node for the plane and modify
the plane&#39;s number of divisions from 10x10 to 9x10. The membership of the
cluster will now be quite different than that which you originally specified.
If you spent a lot of time weighting vertices, this can be pretty frustrating.
There are workarounds such as exporting the weight map before changing
topology, and then importing the weight map.</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3c2f1340970c-pi" style="display: inline;"><img alt="Cluster_10x10" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3c2f1340970c image-full" src="/assets/image_cef8b0.jpg" title="Cluster_10x10" /></a><br /><em>Membership
of the cluster created on a 10x10 plane (left) and after history is modified to
9x10 (right)</em></p>
<p><strong>Deformers and Ordering</strong><strong>&#0160;</strong></p>
<p>The order that you add deformers to
an object makes a big difference in how they work in the scene since each
deformer takes the output of the previous deformer as its input. Here&#39;s an
example:</p>
We took a model of a giant and then added a sine
wave nonlinear deformer, and then added a cluster deformer. We then selected
the cluster and translated the cluster up in Y. Since the cluster is after the
sine wave, the giant in its sine wave position gets translated upwards by the
cluster node. Next, we reordered the cluster so that it is ahead of the sine
wave node in the dependency graph. Now, when the cluster is translated upwards,
it moves the top half of the giant out of the region of influence of the sine
wave deformer and as we move the cluster up and down we control which regions
of the man are deformed by the sine wave.
<p style="text-align: center;"><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017c3200d0e8970b-pi" style="display: inline;"><img alt="Deformer_ordering" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017c3200d0e8970b image-full" src="/assets/image_a4a5dc.jpg" title="Deformer_ordering" /></a><br /><em>Demonstration
of ordering of deformers:&#0160;initial
state (left), sine followed by translated cluster (middle), translated cluster
followed by sine (right)</em></p>
<p><strong>Tweak Nodes</strong><strong>&#0160;</strong></p>
<p>A tweak is the name given to
translations of vertices on shapes in Maya. Shapes without construction history
are able to add tweak deltas directly into their vertex position data. However,
shapes with construction history need to store the tweaks as deltas that get
added to the shape data coming in through the history attribute.</p>
When a deformer is added to a shape in Maya, we
do lot of work to prevent tweaks from existing on the shape. Why? Consider what
would happen otherwise:
<p style="text-align: center;"><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017744de5489970d-pi" style="display: inline;"><img alt="Tweak_node" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017744de5489970d image-full" src="/assets/image_b2a9f4.jpg" title="Tweak_node" /></a><em><span style="text-align: center;">Salty
the seal&#39;s initial state (left), Salty&#39;s nose was tweaked to make it grow - ok
(middle),&#0160;</span><span style="text-align: center;">Salty rotated his neck. We wanted the nose to
come along but it didn&#39;t - not ok (right)</span></em></p>
<p>In the above picture (left), the
user has skinned the seal shape to a skeleton so that rotating the various
joints creates a smooth skinning deformation. In a later plot twist, Salty
tells a lie, and is beset by a strange Pinocchio spell. The user needed to make
Salty&#39;s nose grow, and accomplished this by moving a few vertices over in X. In
this example, there is no tweak node, so the vertex deltas are stored on the
shape node. It all looks ok until we rotate the neck. Now since the tweaks get
added after the skinning deformation, the deltas move the nose vertices over in
X even though Salty&#39;s nose is no longer aligned with the X-axis.&#0160; Salty&#39;s
nose has turned into a strange flat protuberance instead of its original,
Pinocchio-esque coniness. In order for Salty to rotate his head successfully,
we need the skinning algorithm to act on Salty&#39;s tweaked nose, instead of
having the tweaks act on Salty&#39;s skinned nose.</p>
To address this issue, a tweak deformer is
created automatically and inserted at the front of the deformer chain. If the
shape being deformed had existing tweaks or animated CVs, the tweaks &amp;
animation are moved onto the tweak node. Otherwise, the tweak node is left
empty until the user grabs some cvs and moves them (or keys them). New tweaks
are also added to the tweak node (unless the user manually deletes it, in which
case we revert to the old behavior).
<p><strong>Deformers and Topology</strong><strong>&#0160;</strong></p>
<p>The word topology has several
meanings, but in this discussion, we are referring to the underlying structure
of the shape. For example, a mesh&#39;s topology defines how its vertices are
connected into edges, and edges connected into faces in order to build the
total mesh. The topology of a NURBS surface is its number of divisions in u,v.
In Maya&#39;s construction history, changing the topology of a shape frequently
reorders its vertices. For example, if you select some vertices in a polygon
with history and delete them, the polygon will reorder all subsequent vertices
in order to maintain a tight packing. </p>
Several Maya deformers store weights per-vertex
including:
<ul>
<li>clusters</li>
<li>rigid skinning (jointClusters)</li>
<li>smooth skinning (skinClusters)</li>
<li>plug-in deformers (MPxDeformerNode)</li>
</ul>
<p>Deformers store their weights
sparsely using the index of the weighted vertex. This can cause some anomolies
as discussed previously with component sets. Changing the vertex ordering prior
to the deformer will cause the weights to become correlated with new vertices.
Changing the vertex ordering after the deformer is ok in one sense, since the
construction history preserves the original topology into the deformer and only
modifies it later. However, it makes it difficult to figure out the indices for
the weights outside of compute since on the final shape the indices may have an
entirely different index than they have inside the deformer.</p>
<p><strong>Skinning</strong></p>
<p>Skinning is the name given in Maya to the
process of deforming a shape according to the transformations of joints or
other influence objects. There are two types of skinning in Maya: smooth and
rigid. Smooth skinning produces a skin that in general is smooth around joints
as soon as it is bound, though users will typically modify the weights to
achieve more customized affects. Rigid skinning produces a skin that initially
behaves as if it is parented to the joint that it is attached to. Then either
flexors can be added to produce smoothing around joints, or weights on the
rigid jointCluster node can be modified.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017744de6afc970d-pi" style="display: inline;"><img alt="Skinning" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017744de6afc970d image-full" src="/assets/image_1a130a.jpg" title="Skinning" /></a></p>
<p>The table above details some of the differences between smooth and rigid skinning. The most significant architectural detail is the fact that rigid skinning produces a single jointCluster per joint. That jointCluster is then shared by all skins that are rigidly bound to that joint. On the other hand, smooth skinning produces one skinCluster per skin, and all of the influences that affect that skin are connected to the skinCluster.
Smooth skinning in general is the easier technique for users, and its algorithm is quicker to compute (this of course is dependent on the number of influences that are given weights).</p>
<p><strong>Smooth Skinning</strong><strong>&#0160;</strong></p>
<p><em>Node configuration</em></p>
<p>In smooth skinning, there is one
skinCluster per skin. So unlike many deformers, you cannot modify the set
membership of a skinCluster to add additional skins. Each skin must have its
own skinCluster. You can modify the set membership of a skinCluster by removing
vertices -- for example if you wanted to skin only half of an object.</p>
<p>Given a skinCluster, there can be
any number of influence objects. The worldMatrix output of the influence
objects will be connected into the skinCluster&#39;s matrix attribute. The
attribute logicalIndex of this connection is used by the skinCluster to
correlate influences with their weights. The api method
MfnSkinCluster::indexForInfluenceObject lets you query the skinCluster to find
out the index of the associated influence object.</p>
<p>The weights for the skinCluster are
stored in the multi-multi attribute called weightList[].weights[]. The first
index of the multi is the index of the vertex. The second index is the index of
the influence object. The weights are in general stored sparsely, so that if
the user has only weighted a given vertex by 2 of the 50 influence objects,
only 2 elements will exist in the weights array for that vertex. The exception
is that if data exists in the datablock and is later set to 0 by some
operation. The zero-valued data will remain in the datablock until the file is
saved, at which time Maya&#39;s file save code will notice that the value 0.0 is
default data that does not need to be saved. The next time the file is read in,
only the non-zero valued elements will exist in the skinCluster.</p>
<p>The transformation of the joints at
the time of the bind in called the bindPose in Maya. The skinning algorithm
needs this information to do the deformation. The bindPose in Maya is stored in
a node called the dagPose node. It is stored in more detail than is needed by
the skinning algorithm because the extra information is&#0160; needed to help
Maya to restore the bindPose for the user.&#0160; </p>
<p>The dagPose node stores the world
matrix for all of the influences at the time of the bind. It also stores the
local matrix as a full transformation matrix (MTransformationMatrix), and
stores parenting information. Only the worldMatrix is used by the skinning
algorithm, and is contained in the skinCluster node in the bindPreMatrix
attribute, which is a multi-attribute that correlates its multi-index to that
of the related influence object. </p>
Note that for joints only, the bindMatrix is
also stored in the joint in the &quot;bindPose&quot; attribute. The joint has
some other bind-related attributes, but they are obsolete in Maya4.0 and remain
only for backwards compatibility when reading Maya1.0 files.
<p><em>Algorithm</em></p>
<p>Each point P is assigned a set of
weights {w1,w2,...,wn}, one weight for each of the &#39;n&#39; influence objects
affecting the skin with the default weight value being 0.&#0160; Typically, the
following holds:</p>
<p style="padding-left: 30px;">w1 + w2 + ... + wn = 1.0</p>
<p>Let Ti be the current world
transformation matrix of influence `i` and Bi be the world transformation
matrix of the same influence at the time of the bind.</p>
<p>If W(p) is the world position of P
at the time of the bind, then we compute Li(p) to be the position of point P in
the local coordinate system of influence i:</p>
<p style="padding-left: 30px;">Li(p)&#0160; =&#0160; Bi-1 * W(p)</p>
<p>We then compute what the world
position of the point would be if it was &quot;parented&quot; under transform
&#39;i&#39;:</p>
<p style="padding-left: 30px;">Ni(p)&#0160; = Ti * Li(p)</p>
<p>We multiply this world position by
the corresponding weight for influence i and get:</p>
<p style="padding-left: 30px;">Mi(p)&#0160; = wi * Ni(p)</p>
<p>We now add up all the Mi(p) to get
the deformed position of the point in world space:</p>
<p style="padding-left: 30px;">NewWorldPos(p)&#0160; = M1(p) + M2(p)
+ ... + Mn(p)</p>
Notice that all the rotations happen at the
joint/transform pivots and we do a linear interpolation using the weight to
compute the final point positions.

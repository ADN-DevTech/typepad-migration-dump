---
layout: "post"
title: "Item Object: Project Property"
date: "2015-09-07 00:53:12"
author: "Michal Liu"
categories:
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2015/09/item-object-project-property.html "
typepad_basename: "item-object-project-property"
typepad_status: "Publish"
---

<p>In PLM Scripting, the built-in object under item matching the <strong>Project Management </strong>tab is <strong><em>Project</em> </strong>object. An <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of <strong><em>Project</em></strong> objects will be returned by calling <em>item.project.subTasks. </em>They give you all the projects of the item. The item here must be an instance of <a href="http://justonesandzeros.typepad.com/blog/2015/05/item-in-script.html" target="_blank">Item</a>.</p>
<p>Basically a project task can be divided into three classes: text task, linked task and milestone task. In scripting, all of them are represented using <strong><em>Project</em></strong> object, but there is some difference when we deal with each of them. In this post, let’s talk about what the common ground they have and what the difference between them.</p>
<hr />
<p><strong>Text project task</strong></p>
<p>A text project task is a task not linked to any item. Its title is given by user.</p>
<blockquote>
<p><em>var textTask = item.project.subTasks[0];</em></p>
<p><em>// r/w title <br />textTask.title = ‘new title’;</em></p>
<p><em>// r/w start date of project <br />textTask.start_date = new Date();</em></p>
<p><em>// r/w end date of project <br />textTask.end_date = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);</em></p>
<p><em>// r/w progress, must be in [0, 100] <br />textTask.progress = 50;</em></p>
<p><em>// read duration <br />var duration = textTask.duration;</em></p>
<p><em>// read owner, the parent item of the project <br />var owner = textTask.owner;</em></p>
</blockquote>
<hr />
<p><strong>Linked project task</strong></p>
<p>A linked project task is a task linking to an item. It also has the same properties as a text project task, however the <em>title</em> property will be the linked item’s descriptor instead of the text defined by the user. The linked item can be gotten via <em>linkedTask.item. </em></p>
<blockquote>
<p><em>var linkedTask = item.project.subTasks[1];</em></p>
<p><em>// read linked item. An Item object is returned <br />var linkedItem = linkedTask.item;</em></p>
</blockquote>
<hr />
<p><strong>Milestone project task (Read-only task)</strong></p>
<p>If a linked project contains milestones, then we call it a milestone project task, or a read-only project task because all the properties of it are read-only. That’s the only difference between linked task and milestone task.</p>
<hr />
<p><strong>Create project task</strong></p>
<p>The general form to create a task is:</p>
<blockquote>
<p><em>new Task(Title/LinkedItem[, StartDate[, EndDate[,Progress]]]);</em></p>
</blockquote>
<p>The square brackets parts are optional. The default value for both start and end dates is today, and the default progress is 0.</p>
<p>Create a text task:</p>
<blockquote>
<p><em>// create a text task <br />var newTextTask = new Task(‘Text Task’, new Date(), new Date(new Date().getTime() + 24 * 60 * 60 * 1000), 0); <br />// add task into array <br />item.project.subTasks.push(newTextTask);</em></p>
</blockquote>
<p>Create a linked/read-only task:</p>
<blockquote>
<p><em>// fetch item to be linked <br />var itemToBeLinked = loadItem(1234); <br />// create linked task, leave the rest parameters default <br />var newLinkedTask = new Task(itemToBeLinked); <br />// add new task into array <br />item.project.subTasks.push(newLinkedTask);</em></p>
</blockquote>
<p>&#0160;</p>
<p>--Michal</p>

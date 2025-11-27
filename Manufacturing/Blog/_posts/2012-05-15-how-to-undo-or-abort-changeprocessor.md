---
layout: "post"
title: "How to Undo or Abort ChangeProcessor"
date: "2012-05-15 22:01:41"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/how-to-undo-or-abort-changeprocessor.html "
typepad_basename: "how-to-undo-or-abort-changeprocessor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>If you want to abort ChangeProcessor, you can use the TransactionManager.UndoTransaction to undo a ChangeProcessor that committed. You can check the ChangeProcessor.Transact to know if it is transacting one, and compare the ChangeDefinition.CommandName with Transaction.DisplayName in TransactionManager.CommittedTransactions to make sure you undo the right transaction.</p>  <p>And if you are in the current ChangeProcessor_OnExecute procedure, you can also set the Succeeded = False to abort the current ChangeProcessor.</p>

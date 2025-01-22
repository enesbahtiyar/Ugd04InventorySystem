using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished;

    private string questId;

    public void InitailizeQuestStep(string questId)
    {
        this.questId = questId;
    }


    protected void FinishQuestStep()
    {
        if(!isFinished)
        {
            isFinished = true;

            EventHandler.CallOnAdvanceQuest(questId);

            Destroy(this.gameObject);
        }
    }
}

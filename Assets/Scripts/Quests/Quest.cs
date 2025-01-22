using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public SO_QuestInfo info;

    public QuestState state;

    private int currentQuestStepIndex;

    public Quest(SO_QuestInfo info)
    {
        this.info = info;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }


    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep queststep = Object.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            queststep.InitailizeQuestStep(info.id);

        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.Log("YOOOK");
        }

        return questStepPrefab;
    }
}

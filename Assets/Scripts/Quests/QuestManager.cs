using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
        Quest quest = GetQuestById("CollectItems");
        Debug.Log(quest.info.displayName);
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            EventHandler.CallOnQuestStateQuestChanged(quest);
        }
    }

    private void OnEnable()
    {
        EventHandler.OnStartQuest += StartQuest;
        EventHandler.OnAdvanceQuest += AdvanceQuest;
        EventHandler.OnFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        EventHandler.OnStartQuest -= StartQuest;
        EventHandler.OnAdvanceQuest -= AdvanceQuest;
        EventHandler.OnFinishQuest -= FinishQuest;
    }
    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        quest.MoveToNextStep();

        if(quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);

        ClaimRewards(quest);

        ChangeQuestState(id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {

        InventoryManager.Instance.AddItem(InventoryLocation.player, quest.info.itemCode);
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        EventHandler.CallOnQuestStateQuestChanged(quest);
    }

    private void Update()
    {
        foreach(Quest quest in questMap.Values)
        {
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private bool CheckRequirementMet(Quest quest)
    {

        bool meetRequirements = true;
        foreach(SO_QuestInfo prequsiteQuest in quest.info.questPrequisites)
        {
            if(GetQuestById(prequsiteQuest.id).state != QuestState.FINISHED)
            {
                meetRequirements = false;
            }
        }

        return meetRequirements;
    }
    private Dictionary<string, Quest> CreateQuestMap()
    {
        SO_QuestInfo[] allQuests = Resources.LoadAll<SO_QuestInfo>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(SO_QuestInfo quest in allQuests)
        {
            if(idToQuestMap.ContainsKey(quest.id))
            {
                Debug.LogWarning("Dublicate quest found");
            }
            else
            {
                idToQuestMap.Add(quest.id, new Quest(quest));
            }
        }

        return idToQuestMap;
    }


    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];

        if(quest == null)
        {
            Debug.Log("Quest not found for this id: " + id);
        }

        return quest;
    }
}

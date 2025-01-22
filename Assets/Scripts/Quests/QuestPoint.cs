using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private SO_QuestInfo so_QuestInfo;

    private bool playerIsNear;

    private string id;

    private QuestState currentQuestState;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool FinishPoint = true;

    private void Awake()
    {
        id = so_QuestInfo.id;
    }

    private void OnEnable()
    {
        EventHandler.OnQuestStateChanged += QuestStateChange;
    }

    private void OnDisable()
    {
        EventHandler.OnQuestStateChanged -= QuestStateChange;
    }

    private void Update()
    {
        if(playerIsNear && Input.GetKeyDown(KeyCode.Q) && startPoint && currentQuestState == QuestState.CAN_START)
        {
            EventHandler.CallOnStartQuest(id);
        }


        if (playerIsNear && Input.GetKeyDown(KeyCode.Q) && FinishPoint && currentQuestState == QuestState.CAN_FINISH)
        {
            EventHandler.CallOnFinishQuest(id);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id == id)
        {
            currentQuestState = quest.state;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}

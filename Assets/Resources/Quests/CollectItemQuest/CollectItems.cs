using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : QuestStep
{
    private int itemsCollected = 0;

    private int itemsToCollect = 3;


    private void OnEnable()
    {
        EventHandler.ItemAdded += ItemCollected;
    }

    private void OnDisable()
    {
        EventHandler.ItemAdded -= ItemCollected;
    }

    private void ItemCollected(int itemCode)
    {
        if(itemsCollected < itemsToCollect)
        {
            itemsCollected++;
        }


        if (itemsCollected >= itemsToCollect)
        {
            FinishQuestStep();
        }
    }
}

using HeneGames.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/QuestScriptableObject")]
public class QuestScriptableObject : ScriptableObject
{
    public int questID;
    public string questGiver;
    public string questName;
    public string questDescription;
    public int RewardMoney;
    public List<ScriptableObject> RewardItems;
    public bool isTransport;

    public QuestType questType;
    public enum QuestType
    {
        Collect,
        Talk,
        Discover
    }

    [Header("Collect")]
    public ScriptableObject item;
    public int collectAmount;

    public bool isComplete;
    public List<NPC_Sentence> pickUpSentences = new List<NPC_Sentence>();
    public List<NPC_Sentence> notCompleteSentences = new List<NPC_Sentence>();
    public List<NPC_Sentence> completeSentences = new List<NPC_Sentence>();

    public void StartQuest()
    {
        if (Quests.ActiveQuests.Contains(this) || Quests.CompletedQuests.Contains(this)) { return; }
        Quests.ActiveQuests.Add(this);
    }

    public void TryCompleteQuest()
    {
        if (!Quests.ActiveQuests.Contains(this)) { return; }
        if (ReturnCompleteStatus())
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        Quests.ActiveQuests.Remove(this);
        Quests.CompletedQuests.Add(this);
    }

    public bool ReturnCompleteStatus()
    {
        if (questType == QuestType.Collect)
        {
            if (!PlayerInventory.ItemsInInventory.ContainsKey(item as IPickupable)) { return false; }

            if (PlayerInventory.ItemsInInventory[item as IPickupable] >= collectAmount)
            {
                isComplete = true;
            }
            return isComplete;
        }
        return isComplete;
    }

    public List<NPC_Sentence> GetNPCSentences()
    {
        if (Quests.ActiveQuests.Contains(this))
        {
            return notCompleteSentences;
        }
        else if (Quests.CompletedQuests.Contains(this))
        {
            return completeSentences;
        }
        else
        {
            return pickUpSentences;
        }
    }
}

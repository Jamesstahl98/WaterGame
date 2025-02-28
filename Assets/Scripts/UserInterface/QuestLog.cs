using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject questObjectPrefab;
    [SerializeField] private RectTransform itemsParent;

    public void Awake()
    {
        gameObject.SetActive(false);
    }
    public void RefreshQuestLog()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in Quests.ActiveQuests)
        {
            GameObject questObject = Instantiate(questObjectPrefab);
            questObject.transform.SetParent(itemsParent);
            questObject.transform.Find("QuestGiver").GetComponent<TextMeshProUGUI>().text = quest.questGiver;
            questObject.transform.Find("QuestDescription").GetComponent<TextMeshProUGUI>().text = quest.questDescription;
            questObject.transform.Find("Image").GetComponent<Image>().sprite = (quest.item as IPickupable).GetSprite();
        }
    }
}

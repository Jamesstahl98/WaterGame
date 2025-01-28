using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeneGames.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private List<QuestScriptableObject> quests;
        [SerializeField] private InventoryController inventoryController;

        private QuestScriptableObject quest;
        private int currentSentence;
        private float coolDownTimer;
        private bool dialogueIsOn;
        private DialogueTrigger dialogueTrigger;

        public enum TriggerState
        {
            Collision,
            Input
        }

        [Header("References")]
        [SerializeField] private AudioSource audioSource;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;

        [Header("Dialogue")]
        [SerializeField] private TriggerState triggerState;
        [SerializeField] public List<NPC_Sentence> Sentences = new List<NPC_Sentence>();

        private void Awake()
        {
            foreach (var quest in quests)
            {
                if (!Quests.CompletedQuests.Contains(quest))
                {
                    this.quest = quest;
                    break;
                }
            }
        }

        private void Update()
        {
            //Timer
            if(coolDownTimer > 0f)
            {
                coolDownTimer -= Time.deltaTime;
            }

            //Start dialogue by input
            if (Input.GetKeyDown(DialogueUI.instance.actionInput) && dialogueTrigger != null && !dialogueIsOn)
            {
                Sentences = quest.GetNPCSentences();
                if(Sentences == quest.completeSentences)
                {
                    Sentences[Sentences.Count - 1].sentenceEvent.AddListener(CompleteQuest);
                }
                //Trigger event inside DialogueTrigger component
                if (dialogueTrigger != null)
                {
                    dialogueTrigger.startDialogueEvent.Invoke();
                }

                startDialogueEvent.Invoke();

                //If component found start dialogue
                DialogueUI.instance.StartDialogue(this);

                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                dialogueIsOn = true;
            }
        }

        //Start dialogue by trigger
        private void OnTriggerEnter(Collider other)
        {
            if (triggerState == TriggerState.Collision && !dialogueIsOn)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Trigger event inside DialogueTrigger component and store refenrece
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    //If component found start dialogue
                    DialogueUI.instance.StartDialogue(this);

                    dialogueIsOn = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (triggerState == TriggerState.Collision && !dialogueIsOn)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Trigger event inside DialogueTrigger component and store refenrece
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    //If component found start dialogue
                    DialogueUI.instance.StartDialogue(this);

                    dialogueIsOn = true;
                }
            }
        }

        //Start dialogue by pressing DialogueUI action input
        private void OnTriggerStay(Collider other)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Show interaction UI
                    DialogueUI.instance.ShowInteractionUI(true);

                    //Store refenrece
                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Show interaction UI
                    DialogueUI.instance.ShowInteractionUI(true);

                    //Store refenrece
                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Try to find the "DialogueTrigger" component from the exiting collider
            if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                //Stop dialogue
                StopDialogue();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //Try to find the "DialogueTrigger" component from the exiting collider
            if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                //Stop dialogue
                StopDialogue();
            }
        }

        public void StartDialogue()
        {
            //Start event
            if(dialogueTrigger != null)
            {
                dialogueTrigger.startDialogueEvent.Invoke();
            }

            //Reset sentence index
            currentSentence = 0;

            //Show first sentence in dialogue UI
            ShowCurrentSentence();

            //Play dialogue sound
            PlaySound(Sentences[currentSentence].sentenceSound);

            //Cooldown timer
            coolDownTimer = Sentences[currentSentence].skipDelayTime;
        }

        public void NextSentence(out bool lastSentence)
        {
            //The next sentence cannot be changed immediately after starting
            if (coolDownTimer > 0f)
            {
                lastSentence = false;
                return;
            }

            //Add one to sentence index
            currentSentence++;

            //Next sentence event
            if (dialogueTrigger != null)
            {
                dialogueTrigger.nextSentenceDialogueEvent.Invoke();
            }

            nextSentenceDialogueEvent.Invoke();

            //If last sentence stop dialogue and return
            if (currentSentence > Sentences.Count - 1)
            {
                StopDialogue();

                lastSentence = true;

                endDialogueEvent.Invoke();

                return;
            }

            //If not last sentence continue...
            lastSentence = false;

            //Play dialogue sound
            PlaySound(Sentences[currentSentence].sentenceSound);

            //Show next sentence in dialogue UI
            ShowCurrentSentence();

            //Cooldown timer
            coolDownTimer = Sentences[currentSentence].skipDelayTime;
        }

        public void StopDialogue()
        {
            //Stop dialogue event
            if (dialogueTrigger != null)
            {
                dialogueTrigger.endDialogueEvent.Invoke();
            }

            //Hide dialogue UI
            DialogueUI.instance.ClearText();

            //Stop audiosource so that the speaker's voice does not play in the background
            if(audioSource != null)
            {
                audioSource.Stop();
            }

            //Remove trigger refence
            dialogueIsOn = false;
            dialogueTrigger = null;
        }

        private void PlaySound(AudioClip _audioClip)
        {
            //Play the sound only if it exists
            if (_audioClip == null || audioSource == null)
                return;

            //Stop the audioSource so that the new sentence does not overlap with the old one
            audioSource.Stop();

            //Play sentence sound
            audioSource.PlayOneShot(_audioClip);
        }

        private void ShowCurrentSentence()
        {
            if (Sentences[currentSentence].dialogueCharacter != null)
            {
                //Show sentence on the screen
                DialogueUI.instance.ShowSentence(Sentences[currentSentence].dialogueCharacter, Sentences[currentSentence]);

                //Invoke sentence event
                Sentences[currentSentence].sentenceEvent.Invoke();
            }
            else
            {
                DialogueCharacter _dialogueCharacter = new DialogueCharacter();
                _dialogueCharacter.characterName = "";
                _dialogueCharacter.characterPhoto = null;

                DialogueUI.instance.ShowSentence(_dialogueCharacter, Sentences[currentSentence]);

                //Invoke sentence event
                Sentences[currentSentence].sentenceEvent.Invoke();
            }
        }

        public int CurrentSentenceLength()
        {
            if(Sentences.Count <= 0)
                return 0;

            return Sentences[currentSentence].sentence.Length;
        }

        public void CompleteQuest()
        {
            foreach (IPickupable item in quest.RewardItems)
            {
                inventoryController.AddItemToInventory(item);
            }
            for(int i = 0; i < quest.collectAmount; i++)
            {
                inventoryController.RemoveItem(quest.item as IPickupable, quest.collectAmount);
                inventoryController.CheckForEmptyItemSlots();
            }
            inventoryController.UpdateMoney(quest.RewardMoney);
            if (quests.IndexOf(quest) + 1 < quests.Count)
            {
                Debug.Log("Follow up quest found");
                quest = quests[quests.IndexOf(quest) + 1];
            }
            else
            {
                Debug.Log($"{quests.IndexOf(quest)} quests.Count: {quests.Count}");
                Debug.Log("Follow up quest not found");
                quest = null;
            }
        }
    }

    [System.Serializable]
    public class NPC_Sentence
    {
        [Header("------------------------------------------------------------")]

        public DialogueCharacter dialogueCharacter;

        public Sprite targetSprite;

        [TextArea(3, 10)]
        public string sentence;

        public float skipDelayTime = 0.5f;

        public AudioClip sentenceSound;

        public UnityEvent sentenceEvent;
    }
}
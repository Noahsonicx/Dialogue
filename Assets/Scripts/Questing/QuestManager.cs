using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance = null;

        public List<Quest> quests = new List<Quest>();
        
        private List<Quest> activeQuests = new List<Quest>();
        private Dictionary<string, Quest> questDatabase = new Dictionary<string, Quest>();

        public List<Quest> GetActiveQuests() => activeQuests;

        public void UpdateQuest(string _id)
        {
            // this is the same as checking if the key exists. If it does returning it
            // TryGetValue returns a boolean if it succesfully get the item
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.InProgress)
                {
                    // Check if the quest is ready to complete. if it is update the stage
                    // otherwise retain the stage
                    quest.stage =
                        quest.CheckQuestCompletion() ?
                        QuestStage.RequirementsMet :
                        quest.stage;

                    /* Other way of doing quest.stage = quest.CheckQuestCompletion() ? 
                     * if(quest.CheckQuestCompletion())
                    {
                        quest.stage = QuestStage.RequirementsMet;
                    }
                    */
                }
            }
        }

        // Take in the player
        public void CompleteQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                quest.stage = QuestStage.Complete;
                activeQuests.Remove(quest);

                // Find all related quest that are going to be unlocked
                foreach (string questId in quest.unlockedQuests)
                {
                    
                    if(questDatabase.TryGetValue(questId, out Quest unlocked))
                    {
                        // Update their stages
                        unlocked.stage = QuestStage.Unlocked;
                    }
                }
                     

                // Give the player the reward
                // quest.reward <== this helps somehow
            }
        }

        public void AcceptQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.Unlocked)
                {
                    // The quest can be updated to in-progress
                    quest.stage = QuestStage.InProgress;
                    activeQuests.Add(quest);
                }
            }
        }
        private void Awake()
        {
            // If the instance isn't set
            if (instance == null)
            {
                instance = null;
            }
            // If the instance is already set and it isn't this gameObject.
            // Destroy this gameObject
            else if (instance != this)
            {
                Destroy(gameObject);

            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Find all the quests in the game
            quests.Clear();
            quests.AddRange(FindObjectsOfType<Quest>());
            // foreach function is specific to list types that just functions
            // like a foreach loop with lamdes.... cause lamdes are cool bro.
            quests.ForEach(quest =>
           {
               // If this quest doesn't exist already store it in the database
               if (!questDatabase.ContainsKey(quest.title))
                   questDatabase.Add(quest.title, quest);
               else
                   Debug.LogError("THAT QUEST ALREADY EXISTS YOU DINGUS");
           });
        }
    }
}
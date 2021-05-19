using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    // THIS IS AN EXAMPLE
    public class KillQuest : Quest
    {
        public int requiredKills;
        public int killCount;
        // Some enemy type

        public override bool CheckQuestCompletion()
        {
            // add to the kill count

            return killCount == requiredKills;
        }
    }
}

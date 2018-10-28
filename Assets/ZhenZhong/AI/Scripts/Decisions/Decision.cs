using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AI.StateController controller);
    }
}

using System.Collections;
using System.Collections.Generic;

using Rewired;

namespace Primitives
{
    namespace Core
    {
        using UnityEngine;

        public class InputHandler : MonoBehaviour
        {
            public int playerId = 0;
            private Player player;

            public List<ITriggerable> triggers;
            public List<ITriggerable1D> triggers1D;
            public List<ITriggerable2D> triggers2D;

            public void Awake()
            {
                triggers = new List<ITriggerable>();
                Debug.Log(triggers.Count);
                player = ReInput.players.GetPlayer(playerId);
            }

            private void Update()
            {
                foreach (ITriggerable trigger in triggers)
                    if (GetButton(trigger.TriggerName))
                        trigger.OnTrigger();

                foreach (ITriggerable1D trigger in triggers1D)
                    trigger.OnTrigger(GetAxis(trigger.TriggerName));

                foreach (ITriggerable2D trigger in triggers2D)
                    trigger.OnTrigger(GetAxisPair(trigger.TriggerName));
            }

            public bool GetButtonDown(string id) { return player != null && player.GetButtonDown(id); }
            public bool GetButtonUp(string id) { return player != null && player.GetButtonUp(id); }
            public bool GetButton(string id) { return player != null && player.GetButton(id); }
            public bool GetAnyButtonDown() { return player != null && player.GetAnyButtonDown(); }
            public bool GetAnyButton() { return player != null && player.GetAnyButton(); }
            public float GetAxis(string id) { return player == null ? 0 : player.GetAxisRaw(id); }
            public float GetAxisDown(string id)
            {
                if (player == null || player.GetNegativeButtonDown(id) == false && player.GetButtonDown(id) == false)
                    return 0;
                return player.GetAxisRaw(id);
            }

            public virtual Vector2 GetAxisPairDown(string axisPairName, bool restrictToXAxis = false, bool restrictToYAxis = false)
            {
                Vector2 input = GetAxisPairDown(axisPairName);

                if (restrictToXAxis && restrictToYAxis)
                    input = input.LargestAxis();
                else if (restrictToXAxis)
                    input.y = 0;
                else if (restrictToYAxis)
                    input.x = 0;

                return input;
            }

            public Vector2 GetAxisPair(string axisPairName, bool restrictToXAxis = false, bool restrictToYAxis = false)
            {
                Vector2 input = GetAxisPair(axisPairName);

                if (restrictToXAxis && restrictToYAxis)
                    input = input.LargestAxis();
                else if (restrictToXAxis)
                    input.y = 0;
                else if (restrictToYAxis)
                    input.x = 0;

                return input;
            }

            public float GetAxisPairRotation(string axisPairName)
            {
                Vector2 input = GetAxisPair(axisPairName);
                return Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            }
        }
    }
}

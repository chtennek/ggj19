using System.Collections;
using System.Collections.Generic;

using Rewired;

using Primitives.Core;

namespace Primitives
{
    namespace Input
    {
        using UnityEngine;

        public class InputHandler : MonoBehaviour
        {
            public int playerId = 0;
            private Player player;

            public List<InputBehaviour> actions;

            public void Awake()
            {
                player = ReInput.players.GetPlayer(playerId);
            }

            private void Update()
            {
                foreach (InputBehaviour action in actions) {
                    if (GetButton(action.actionName))
                        action.OnTrigger();

                    action.OnInput(GetAxisPair(action.actionName));
                }
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
            public Vector2 GetAxis2D(string id) {
                return player == null ? Vector2.zero : player.GetAxis2D(id + "Horizontal", id + "Vertical");
            }

            public Vector2 GetAxisPair(string axisPairName, bool restrictToXAxis = false, bool restrictToYAxis = false)
            {
                Vector2 input = GetAxis2D(axisPairName);

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
                Vector2 input = GetAxis2D(axisPairName);
                return Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            }
        }
    }
}

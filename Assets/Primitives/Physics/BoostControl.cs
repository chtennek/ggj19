using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Primitives.Core;

namespace Primitives
{
    namespace Physics
    {
        public class BoostControl : MonoBehaviour, ITriggerable
        {
            public PhysicsHandler physics;
            public string triggerName = "Boost";
            public string TriggerName { get { return triggerName; } }

            [Header("Parameters")]
            public bool overrideVelocity = true; // Set velocity along direction instead of adding, usually you want this
            public float magnitude = 5f;
            public Vector3 direction = Vector3.up; // [TODO] auto-normalize this


            protected void Reset()
            {
                physics = GetComponent<PhysicsHandler>();
            }

            public void OnTrigger()
            {
                Boost(magnitude);
            }

            public void Boost(float magnitude)
            {
                if (overrideVelocity == true)
                    magnitude -= Vector3.Dot(physics.Velocity, direction.normalized); // Adjust magnitude to account for current velocity

                physics.AddForce(magnitude * direction.normalized, ForceMode2D.Impulse);
            }
        }
    }
}

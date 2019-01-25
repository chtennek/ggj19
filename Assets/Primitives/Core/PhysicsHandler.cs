using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primitives
{
    namespace Core
    {
        public class PhysicsHandler : MonoBehaviour
        {
            public Rigidbody rb;
            public Rigidbody2D rb2D;
            public Collider coll;
            public Collider2D coll2D;

            public Vector3 Velocity
            {
                get
                {
                    if (rb != null) return rb.velocity;
                    else if (rb2D != null) return rb2D.velocity;
                    return Vector3.zero;
                }
                set
                {
                    if (rb != null) rb.velocity = value;
                    else if (rb2D != null) rb2D.velocity = value;
                }
            }

            public float Speed
            {
                get { return Velocity.magnitude; }
                set { Velocity = value * Velocity.normalized; }
            }

            protected void Reset()
            {
                rb = GetComponent<Rigidbody>();
                rb2D = GetComponent<Rigidbody2D>();
                coll = GetComponent<Collider>();
                coll2D = GetComponent<Collider2D>();
            }

            public void AddForce(Vector3 force, ForceMode2D mode = ForceMode2D.Force)
            {
                if (rb != null && rb.isKinematic == false)
                {
                    switch (mode)
                    {
                        case ForceMode2D.Force:
                            rb.AddForce(force, ForceMode.Force);
                            break;
                        case ForceMode2D.Impulse:
                            rb.AddForce(force, ForceMode.Impulse);
                            break;
                    }
                }

                if (rb2D != null && rb2D.isKinematic == false)
                {
                    rb2D.AddForce(force, mode);
                }
            }
        }
    }
}

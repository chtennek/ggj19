using UnityEngine;

namespace Primitives {
    namespace Core {
        public interface ITriggerable2D
        {
            string TriggerName { get; }
            void OnTrigger(Vector2 v);
        }
    }
}

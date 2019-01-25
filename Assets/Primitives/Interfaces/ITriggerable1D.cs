namespace Primitives {
    namespace Core {
        public interface ITriggerable1D
        {
            string TriggerName { get; }
            void OnTrigger(float x);
        }
    }
}

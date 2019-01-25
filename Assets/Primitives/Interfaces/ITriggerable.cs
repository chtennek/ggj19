namespace Primitives {
    namespace Core {
        public interface ITriggerable
        {
            string TriggerName { get; }
            void OnTrigger();
        }
    }
}

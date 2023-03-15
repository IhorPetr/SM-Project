namespace CQRS.Core.Events
{
    public abstract class BaseEvent
    {
        protected BaseEvent(string Type) 
        { 
            this.Type = Type;
        }

        public int Version { get; set; }
        public string Type { get; set; }
    }
}

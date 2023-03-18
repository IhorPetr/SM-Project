using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Infrastructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<PostAggregate>
    {
        private readonly IEventStore _eventStore;
        private readonly IEventProducer _eventProducer;
        private const string kafkaTopicVariableName = "KAFKA_TOPIC";

        public EventSourcingHandler(IEventStore eventStore, IEventProducer eventProducer)
        {
            _eventStore = eventStore;
            _eventProducer = eventProducer;
        }

        public async Task<PostAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new PostAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);
            if(events == null || !events.Any())
            {
                return aggregate;
            }

            aggregate.ReplyEvents(events);
            aggregate.Version = events.Select(x => x.Version).Max();
            return aggregate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventAsync(aggregate.Id, aggregate.GetUncommitedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommited();
        }

        public async Task RepublishEventAsync()
        {
            var aggregateIds = await _eventStore.GetAggregateIdsAsync();
            if(aggregateIds == null || !aggregateIds.Any())
            {
                return;
            }
            
            foreach(var aggregateId in aggregateIds)
            {
                var aggregate = await GetByIdAsync(aggregateId);
                if(aggregate == null)
                {
                    continue;
                }

                var events = await _eventStore.GetEventsAsync(aggregateId);

                foreach(var @event in events)
                {
                    string topic = Environment.GetEnvironmentVariable(kafkaTopicVariableName) ?? throw new KeyNotFoundException($"Can not found variable: {kafkaTopicVariableName}");
                    await _eventProducer.ProducerAsync(topic, @event);
                }
            }
        }
    }
}

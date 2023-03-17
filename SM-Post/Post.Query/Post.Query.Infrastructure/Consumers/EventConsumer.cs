﻿using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using Post.Common.Events.Comment;
using Post.Common.Events.Message;
using Post.Common.Events.Post;
using Post.Query.Infrastructure.Converters;
using Post.Query.Infrastructure.Handlers;
using System.Text.Json;

namespace Post.Query.Infrastructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IEventHandler _eventHandler;

        public EventConsumer(IOptions<ConsumerConfig> consumerConfig, IEventHandler eventHandler)
        {
            _consumerConfig = consumerConfig.Value;
            _eventHandler = eventHandler;
        }

        public async void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumeResult = consumer.Consume();
                if (consumeResult?.Message == null)
                {
                    continue;
                }

                var options = new JsonSerializerOptions
                {
                    Converters = { new EventJsonConverter() }
                };

                var @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, options);
                switch (@event)
                {
                    case PostCreatedEvent postCreate:
                        await _eventHandler.On(postCreate);
                        break;
                    case MessageUpdatedEvent messageUpdate:
                        await _eventHandler.On(messageUpdate);
                        break;
                    case PostLikedEvent messageLike:
                        await _eventHandler.On(messageLike);
                        break;
                    case CommentAddedEvent commentAdd:
                        await _eventHandler.On(commentAdd);
                        break;
                    case CommentUpdatedEvent commentUpdate:
                        await _eventHandler.On(commentUpdate);
                        break;
                    case CommentRemovedEvent commentRemove:
                        await _eventHandler.On(commentRemove);
                        break;
                    case PostRemovedEvent postRemove:
                        await _eventHandler.On(postRemove);
                        break;
                    default:
                        throw new NotImplementedException($"Subscription for {@event.GetType().Name} event is not set");
                }

                consumer.Commit(consumeResult);
            }
        }
    }
}

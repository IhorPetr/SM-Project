using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure
{
    public static class ServicesExtensions
    {
        private const string kafkaTopicVariableName = "KAFKA_TOPIC";

        public static void InitDatabase(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            context.Database.EnsureCreated();
        }

        public static void InitKafkaTopic(this IHost app)
        {
            string topic = Environment.GetEnvironmentVariable(kafkaTopicVariableName) ?? throw new KeyNotFoundException($"Can not found variable: {kafkaTopicVariableName}");
            using var scope = app.Services.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IOptions<ConsumerConfig>>();
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = config.Value.BootstrapServers }).Build();
            adminClient.CreateTopicsAsync(new TopicSpecification[] {
                    new TopicSpecification { Name = topic, ReplicationFactor = 1, NumPartitions = 1 } }).ConfigureAwait(false);
        }
    }
}

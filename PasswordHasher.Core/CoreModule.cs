using Autofac;
using PasswordHasher.Core.Hashing;
using PasswordHasher.Core.Jobs;
using PasswordHasher.Core.Repositories;
using PasswordHasher.Core.Stats;

namespace PasswordHasher.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Hasher>().As<IHasher>();
            builder.RegisterType<JobRepository>().As<IJobRepository>();
            builder.RegisterType<StatsProvider>().As<IStatsProvider>();

            RegisterJobUtilities(builder);
        }

        private void RegisterJobUtilities(ContainerBuilder builder)
        {
            builder.RegisterType<JobBag>().As<IJobBag>().SingleInstance();
            builder.RegisterType<JobEngine>().As<IJobEngine>();
            builder.RegisterType<HashJobProcessor>();
            builder.RegisterType<TimedHashJobProcessorDecorator>();
            builder.Register(context =>
                context.Resolve<TimedHashJobProcessorDecorator>(TypedParameter.From<IHashJobProcessor>(
                context.Resolve<HashJobProcessor>())
            )).As<IHashJobProcessor>();
        }
    }
}
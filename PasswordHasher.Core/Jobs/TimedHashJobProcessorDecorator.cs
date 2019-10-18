using System.Diagnostics;
using PasswordHasher.Core.Entities;

namespace PasswordHasher.Core.Jobs
{
    public class TimedHashJobProcessorDecorator : IHashJobProcessor
    {
        private IHashJobProcessor _decorated;

        public TimedHashJobProcessorDecorator(IHashJobProcessor hashJobProcessor)
        {
            _decorated = hashJobProcessor;
        }

        public Job Process(Job job, string input)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = _decorated.Process(job, input);
            stopwatch.Stop();
            result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            return result;
        }
    }
}
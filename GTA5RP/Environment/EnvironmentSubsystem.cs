using System;
using GTA;
using GTA5RP.Core.Contracts;
using GTA5RP.Environment;

namespace GTA5RP.Environment
{
    public class EnvironmentSubsystem : ISubsystem
    {
        public NeedsSystem Needs { get; private set; }
        public WantedSystem Wanted { get; private set; }
        public SpawnService Spawner { get; private set; }

        public string Name => "Environment";
        public int Version => 1;

        public void Init(IServiceProvider services)
        {
            Needs = new NeedsSystem();
            Wanted = new WantedSystem();
            Spawner = new SpawnService();
        }

        public void Tick()
        {
            Needs.Tick();
            Wanted.Tick();
        }

        public void Dispose() { }
    }
}

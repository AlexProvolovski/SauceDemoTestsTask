using NUnit.Framework;
using log4net.Config;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(3)]

[assembly: XmlConfigurator(ConfigFile = "Properties\\log4net.config", Watch = true)]
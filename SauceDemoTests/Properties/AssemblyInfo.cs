﻿using NUnit.Framework;

[assembly: LevelOfParallelism(3)]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
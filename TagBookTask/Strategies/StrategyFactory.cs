using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TagBookTask.Models;

namespace TagBookTask.Strategies
{
    public static class StrategyFactory
    {
        private static readonly Dictionary<PreferentialType, IStrategy> Dictionary = new Dictionary<PreferentialType, IStrategy>();

        static StrategyFactory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(o => o.GetInterface("IStrategy") != null && !o.IsAbstract).ToArray();

            foreach (PreferentialType item in Enum.GetValues(typeof(PreferentialType)))
            {
                var type = types.First(o => o.Name.Contains(string.Format("{0}{1}", item, "Strategy")));
                Dictionary[item] = (IStrategy)Activator.CreateInstance(type, true);
            }
        }

        public static IStrategy GetStrategy(PreferentialType preferentialType)
        {
            return Dictionary[preferentialType];
        }
    }
}

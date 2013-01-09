using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoGuards.Engine.Emitters
{
    public class EmitterResolver
    {
        private static Lazy<Dictionary<string, AutoGuardEmitter>> _emitters = new Lazy<Dictionary<string, AutoGuardEmitter>>(() =>
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.BaseType == typeof (AutoGuardEmitter))
                .Select(t => (AutoGuardEmitter) Activator.CreateInstance(t))
                .ToDictionary(k => k.EmitsFor.Name, v => v);
        });

        public AutoGuardEmitter Resolve(Type autoGuardType)
        {
            if (autoGuardType == null)
            {
                throw new ArgumentNullException("autoGuardType");
            }

            return Resolve(autoGuardType.Name);
        }

        public AutoGuardEmitter Resolve(string autoGuardTypeName)
        {
            if (string.IsNullOrWhiteSpace("autoGuardTypeName"))
            {
                throw new ArgumentException("autoGuardTypeName");
            }

            AutoGuardEmitter emitter = null;

            if (!_emitters.Value.ContainsKey(autoGuardTypeName))
            {
                throw new InvalidOperationException(string.Format("AutoGuard '{0}' does not have a corresponding emitter", autoGuardTypeName));
            }

            return _emitters.Value[autoGuardTypeName];
        }
    }
}

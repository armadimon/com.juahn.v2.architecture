using System;
using System.Collections.Generic;
using System.Linq;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 타입을 키로 인스턴스를 보관하는 경량 IOC 컨테이너.
    /// 이 패키지에 자체 포함되어 다른 패키지에 의존하지 않는다.
    /// </summary>
    public class IOCContainer
    {
        private readonly Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else
            {
                mInstances.Add(key, instance);
            }
        }

        public T Get<T>() where T : class
        {
            var key = typeof(T);

            if (mInstances.TryGetValue(key, out var retInstance))
            {
                return retInstance as T;
            }

            return null;
        }

        public IEnumerable<T> GetInstancesByType<T>()
        {
            var type = typeof(T);
            return mInstances.Values.Where(instance => type.IsInstanceOfType(instance)).Cast<T>();
        }

        public void Clear() => mInstances.Clear();
    }
}

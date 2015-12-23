using System;
using System.Linq;
using System.Collections.Generic;

namespace Utils
{
    public abstract class BareActor<T> : IActor<T> where T : struct, IConvertible
    {
        private Dictionary<T, List<Action<Object>>> callbacks = new Dictionary<T, List<Action<Object>>>();

        public BareActor()
        {
#if UNITY_EDITOR
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
#endif

            foreach (T action in Enum.GetValues(typeof(T)))
            {
                callbacks.Add(action, new List<Action<Object>>());
            }
        }

        public RegisterResult<T> register(T action, Action<Object> func)
        {
            callbacks[action].Add(func);
            return new RegisterResult<T>(this, action, func);
        }

        public IKeyGetter unregister<A>(A action, Action<Object> func)
        {
            T realAction = (T)Convert.ChangeType(action, typeof(T));
            callbacks[realAction].Remove(func);

            return new RegisterResult<T>(this, realAction, func);
        }

        protected void fire(T action)
        {
            foreach (Action<Object> func in callbacks[action].ToList())
            {
                func.Invoke(this);
            }
        }

        protected void fire(T action, Object obj)
        {
            foreach (Action<Object> func in callbacks[action].ToList())
            {
                func.Invoke(obj);
            }
        }
    }
}
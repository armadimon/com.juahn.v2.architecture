using System.Collections.Generic;

namespace Juahn.V2.Architecture
{
    /// <summary>
    /// 여러 UnRegister 핸들을 모아 일괄 해제할 수 있는 대상.
    /// </summary>
    public interface IUnRegisterList
    {
        List<IUnRegister> UnregisterList { get; }
    }

    public static class UnRegisterListExtension
    {
        public static void AddToUnregisterList(this IUnRegister self, IUnRegisterList unRegisterList) =>
            unRegisterList.UnregisterList.Add(self);

        public static void UnRegisterAll(this IUnRegisterList self)
        {
            foreach (var unRegister in self.UnregisterList)
            {
                unRegister.UnRegister();
            }

            self.UnregisterList.Clear();
        }
    }
}

/*
 * Author:durow
 * interface of ServiceContainer
 * Date:2017.02.12
 */

using System;

namespace Ayx.AvalonSword
{
    public interface IServiceContainer
    {
        void Add<TInterface, TService>();
        void AddSingleton<TInterface, TService>(TService service);
        TInterface GetService<TInterface>();
        object GetService(Type type);
    }
}

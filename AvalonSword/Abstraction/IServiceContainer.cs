/*
 * Author:durow
 * interface of ServiceContainer
 * Date:2017.02.12
 */

using System;

namespace Ayx.AvalonSword.Abstraction
{
    public interface IServiceContainer
    {
        void Add<TInterface, TService>();
        void AddSingleton<TInterface, TService>();
        void AddSingleton<TInterface>(TInterface item);
        TInterface GetService<TInterface>();
        object GetService(Type type);
    }
}

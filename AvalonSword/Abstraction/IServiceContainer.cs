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
        void Add<TInterface, TService>() where TService : TInterface;
        void AddSingleton<TInterface, TService>() where TService : TInterface;
        void AddSingleton<TInterface>(TInterface item);
        TInterface GetService<TInterface>();
        object GetService(Type type);
        bool Contains<T>();
    }
}

using System.Collections.Generic;
using fennecs;

namespace jam;

public static class FennecsNotStupidExtensions
{
    public static void Add<T>(IEnumerable<Entity> self, T component)
    {
        foreach (var entity in self) entity.Add(component);
    }

    public static void Add<T>(IEnumerable<Entity> self, T component, Entity target)
    {
        foreach (var entity in self) entity.Add(component, target);
    }

    public static void Remove<T>(IEnumerable<Entity> self) 
    {
        foreach (var entity in self) entity.Remove<T>();
    }

    public static void Remove<T>(IEnumerable<Entity> self, Entity target) 
    {
        foreach (var entity in self) entity.Remove<T>(target);
    }

    public static void Link<L>(IEnumerable<Entity> self, L link) where L : class
    {
        foreach (var entity in self) entity.Add(fennecs.Link.With(link));
    }
    public static void Unlink<L>(IEnumerable<Entity> self, L link) where L : class
    {
        foreach (var entity in self) entity.Remove(fennecs.Link.With(link));
    }
}
using System.Collections.Immutable;

namespace EqualitySpike;

public class ComparerDiff<TExisting, TIncoming, TKey>
    where TExisting : class, TKey
    where TIncoming : class, TKey
    where TKey : notnull
{
    public ImmutableSortedSet<TExisting> ExistingOnly { get; }
    public ImmutableSortedSet<TIncoming> IncomingOnly { get; }
    public ImmutableSortedSet<TExisting> ExistingIntersection { get; }
    public ImmutableSortedSet<TIncoming> IncomingIntersection { get; }

    public static ComparerDiff<TExisting, TIncoming, TKey> Create(
        IList<TExisting> existing,
        IList<TIncoming> incoming,
        IComparer<TKey> comparer)
    {
        var existingSet = new SortedSet<TExisting>(comparer);
        var incomingSet = new SortedSet<TIncoming>(comparer);

        var existingOnly = existingSet.ExceptBy(incoming.AsEnumerable<TKey>(), _ => _)
            .ToImmutableSortedSet<TExisting>(comparer);
        var incomingOnly = incomingSet.ExceptBy(existing.AsEnumerable<TKey>(), _ => _)
            .ToImmutableSortedSet<TIncoming>(comparer);
        var existingIntersection = existingSet.IntersectBy(incoming.AsEnumerable<TKey>(), _ => _)
            .ToImmutableSortedSet<TExisting>(comparer);
        var incomingIntersection = incomingSet.IntersectBy(existing.AsEnumerable<TKey>(), _ => _)
            .ToImmutableSortedSet<TIncoming>(comparer);

        return new ComparerDiff<TExisting, TIncoming, TKey>(
            existingOnly,
            incomingOnly,
            existingIntersection,
            incomingIntersection);
    }

    private ComparerDiff(
        ImmutableSortedSet<TExisting> existingOnly,
        ImmutableSortedSet<TIncoming> incomingOnly,
        ImmutableSortedSet<TExisting> existingIntersection,
        ImmutableSortedSet<TIncoming> incomingIntersection)
    {
        ExistingOnly = existingOnly;
        IncomingOnly = incomingOnly;
        ExistingIntersection = existingIntersection;
        IncomingIntersection = incomingIntersection;
    }
}

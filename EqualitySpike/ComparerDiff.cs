using System.Collections.Immutable;

namespace EqualitySpike
{
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

            var existingOnly = existingSet.ExceptBy(incoming.AsEnumerable<TKey>(), _ => _);
            var incomingOnly = incomingSet.ExceptBy(existing.AsEnumerable<TKey>(), _ => _);
            var existingIntersection = existingSet.IntersectBy(incoming.AsEnumerable<TKey>(), _ => _);
            var incomingIntersection = incomingSet.IntersectBy(existing.AsEnumerable<TKey>(), _ => _);

            return new ComparerDiff<TExisting, TIncoming, TKey>(
                existingOnly.ToImmutableSortedSet<TExisting>(comparer),
                incomingOnly.ToImmutableSortedSet<TIncoming>(comparer),
                existingIntersection.ToImmutableSortedSet<TExisting>(comparer),
                incomingIntersection.ToImmutableSortedSet<TIncoming>(comparer));
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
}

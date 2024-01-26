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
            var existingPerKey = existing.ToImmutableSortedDictionary(_ => _, _ => _, comparer);
            var incomingPerKey = incoming.ToImmutableSortedDictionary(_ => _, _ => _, comparer);

            var allKeys = existing.AsEnumerable<TKey>().Concat(incoming);
            var sortedKeys = new SortedSet<TKey>(allKeys, comparer);

            var diff = sortedKeys.Aggregate(
                new
                {
                    ExistingOnly = new SortedSet<TExisting>(comparer),
                    IncomingOnly = new SortedSet<TIncoming>(comparer),
                    ExistingIntersection = new SortedSet<TExisting>(comparer),
                    IncomingIntersection = new SortedSet<TIncoming>(comparer)
                },
                (acc, key) =>
                {
                    var inExisting = existingPerKey.ContainsKey(key);
                    var inIncoming = incomingPerKey.ContainsKey(key);

                    if (inExisting && !inIncoming)
                    {
                        acc.ExistingOnly.Add(existingPerKey[key]);
                    }
                    else if (inIncoming && !inExisting)
                    {
                        acc.IncomingOnly.Add(incomingPerKey[key]);
                    }
                    else
                    {
                        acc.ExistingIntersection.Add(existingPerKey[key]);
                        acc.IncomingIntersection.Add(incomingPerKey[key]);
                    }
                    return acc;
                });

            return new ComparerDiff<TExisting, TIncoming, TKey>(
                diff.ExistingOnly.ToImmutableSortedSet<TExisting>(comparer),
                diff.IncomingOnly.ToImmutableSortedSet<TIncoming>(comparer),
                diff.ExistingIntersection.ToImmutableSortedSet<TExisting>(comparer),
                diff.IncomingIntersection.ToImmutableSortedSet<TIncoming>(comparer));
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

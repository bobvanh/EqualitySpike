namespace EqualitySpike
{
    public class Diff<TLeft, TRight, TEquality>
        where TLeft : TEquality
        where TRight : TEquality
    {
        private Diff(ISet<TLeft> leftOnly, ISet<TRight> rightOnly, ISet<TEquality> intersection)
        {
            LeftOnly = leftOnly;
            RightOnly = rightOnly;
            Intersection = intersection;
        }

        public ISet<TLeft> LeftOnly { get; }
        public ISet<TRight> RightOnly { get; }
        public ISet<TEquality> Intersection { get; }

        public static Diff<TLeft, TRight, TEquality> Create(
            IEnumerable<TLeft> lefts, 
            IEnumerable<TRight> rights, 
            IEqualityComparer<TEquality> comparer)
        {
            // dit is een leuke oefening.. niet meer aan toe gekomen
            //
            throw new NotImplementedException();
        }
    }
}

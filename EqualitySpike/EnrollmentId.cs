namespace EqualitySpike;

public interface IEnrollmentId
{
    string Code { get; }
    int Occasion { get; }
}

public static class EnrollmentId
{
    public static Func<IEnrollmentId?, IEnrollmentId?, bool> AreEqual 
        => (a, b) => a?.Code == b?.Code && a?.Occasion == b?.Occasion;

    public static int GetHashCode(IEnrollmentId id) => $"{id.Code}{id.Occasion}".GetHashCode();

    public static IEqualityComparer<IEnrollmentId> EqualityComparer 
        => EqualityComparer<IEnrollmentId>.Create(AreEqual, GetHashCode);

    public static int CompareById(IEnrollmentId a, IEnrollmentId b)
    {
        int codeOutcome = a.Code.CompareTo(b.Code);
        int result = codeOutcome != 0 ? codeOutcome : a.Occasion.CompareTo(b.Occasion);
        return result;
    }

    public static IComparer<IEnrollmentId> Comparer => Comparer<IEnrollmentId>.Create(CompareById);

    public static Func<IEnrollmentId, bool> BuildEquals(IEnrollmentId b)
        => a => AreEqual(a, b);
}

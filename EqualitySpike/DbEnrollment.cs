namespace EqualitySpike;

public class DbEnrollment : IEnrollmentId
{
    public string Code { get; set; } = null!;
    public int Occasion { get; set; }
    public string HabbieBabbie { get; set; } = null!;
    public DateTime EnterDate { get; set; }

    public override string ToString() => $"DbItem:{Code}-{Occasion}";
}

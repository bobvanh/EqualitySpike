namespace EqualitySpike;

public class MessageEnrollment : IEnrollmentId
{
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = null!;
    public int Occasion { get; set; }

    public static MessageEnrollment Hak_1 = new MessageEnrollment
    {
        Code = "HAK",
        Occasion = 1
    };

    public static List<MessageEnrollment> Items = new List<MessageEnrollment>
    {
        Hak_1,
        new MessageEnrollment { Code = "HAK", Occasion = 2 },
        new MessageEnrollment { Code = "HAK", Occasion = 3 }
    };

    public override string ToString() => $"Message:{Code}-{Occasion}";
}

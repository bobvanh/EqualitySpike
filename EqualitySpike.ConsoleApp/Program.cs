using EqualitySpike;

var messageEnrollments = new List<MessageEnrollment>
    {
        MessageEnrollment.Hak_1,
        new MessageEnrollment { Code = "HAK", Occasion = 2 },
        new MessageEnrollment { Code = "HAK", Occasion = 3 }
    };

var dbEnrollments = new List<DbEnrollment>
    {
        new DbEnrollment { Code = "HAK", Occasion = 1  },
        new DbEnrollment { Code = "HAK", Occasion = 2 },
        new DbEnrollment { Code = "TAK", Occasion = 1 }
    };

// Als je 2 verschillende classes hebt die dezelfde entiteit vertegenwoordigen
// dan kan je de identificerende properties abstraheren in een interface
//
// Zo kan je bijvoorbeeld set based vergelijkingen doen door een IComparer te
// schrijven voor de interface:
//
ISet<IEnrollmentId> dbItemsSet = new SortedSet<IEnrollmentId>(dbEnrollments, EnrollmentId.Comparer);
ISet<IEnrollmentId> messageItemsSet = new SortedSet<IEnrollmentId>(messageEnrollments, EnrollmentId.Comparer);
bool listIdsAreEqual = dbItemsSet.SetEquals(messageItemsSet);
Console.WriteLine($"List are equal id wise : {listIdsAreEqual}");
//
// Of met een IEqualityComparer
//
dbItemsSet = new HashSet<IEnrollmentId>(dbItemsSet, EnrollmentId.EqualityComparer);
messageItemsSet = new HashSet<IEnrollmentId>(messageEnrollments, EnrollmentId.EqualityComparer);
listIdsAreEqual = dbItemsSet.SetEquals(messageItemsSet);
Console.WriteLine($"List are equal id wise : {listIdsAreEqual}");

// Je kunt ook set based operaties gebruiken met behulp van de EqualityComparer
//
var notInDb = dbEnrollments
    .ExceptBy(MessageEnrollment.Items.AsEnumerable<IEnrollmentId>(), _ => _, EnrollmentId.EqualityComparer);

Console.WriteLine($"Not in db items: {string.Join(",", notInDb)}");

// Met een truukje kan je ook je predicate in een IQueryable op een eenduidige 
// manier hergebruiken. Dit is vooral handig als je op meerdere properties moet
// vergelijken
//
var queryableDbItems = dbEnrollments.AsQueryable();
var hak_1 = queryableDbItems.FirstOrDefault(EnrollmentId.BuildEquals(MessageEnrollment.Hak_1));

Console.WriteLine($"Works in IQueryable: {hak_1 != null}");

Console.ReadKey();


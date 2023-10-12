using System.Linq;

namespace Nachhilfen;

public class Service {
    public int Level { get; set; }
    public string Subject { get; set; }

    public override string ToString() {
        return $"{Subject} in {Level}. Klassen";
    }

    public static bool TryParse(string line, out Service? service, out string studentName) {
        service = Parse(line, out studentName);
        return service != null;
    }


    private static Service? Parse(string line, out string studentName) {
        studentName = "";
        var split = line.Split(';');
        var splitList = split.ToList();
        if (split.Length != 3 || splitList.Any(string.IsNullOrEmpty)) {
            return null;
        }

        var levelResult = int.TryParse(split[2], out var level);
        if (!levelResult) {
            return null;
        }

        var subject = split[1];
        studentName = split[0];
        return new Service {
            Level = level,
            Subject = subject
        };
    }

    public string Serialize(string studentName) {
        return $"{studentName};{Subject};{Level}";
    }
}
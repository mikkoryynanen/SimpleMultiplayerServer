using System.Security.AccessControl;

namespace Shared;

public class Packets
{
    [Serializable]
    public struct TestPackage
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
using System.Collections.Generic;

namespace Symbols.Api
{
    public static class AppConfiguration
    {
        public static string JwtKey = "5ba3668536c949ec9bd589d805568833d66739e3b810ece2cdeeac30d7ea4084";
        public static List<User> Users { get; set; } = new List<User>();
        public static List<CommunicationPair> CommunicationPairs { get; set; } = new List<CommunicationPair>();
        public static List<Symbol> Symbols { get; set; } = new List<Symbol>();

        public class User
        {
            public string Username { get; set; }
            public string Name { get; set; }
        }

        public class CommunicationPair
        {
            public string UsernameOne { get; set; }
            public string UsernameTwo { get; set; }
        }

        public class Symbol
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string Tooltip { get; set; }
            public string Icon { get; set; }
        }
    }
}

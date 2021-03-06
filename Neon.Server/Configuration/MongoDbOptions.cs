namespace Neon.Server.Configuration {
    public class MongoDbOptions {
        public static string IDENTIFIER = "MongoDb";

        public string Hostname { get; set; }
        public ushort Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}
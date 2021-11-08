namespace telegram_bot
{
    public class Update
    {
        public int update_id { get; set; }
        public message message { get; set; }
    }
    public class message
    {
        public int message_id { get; set; }
        public message_from from { get; set; }
        public message_chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
    }
    public class message_from
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }
    }
    public class message_chat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }
}

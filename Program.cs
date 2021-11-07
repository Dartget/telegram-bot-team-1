using Microsoft.Extensions.Configuration;
using System;

namespace ReadingAppSettingsJson
{
    public class result
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public bool can_join_groups { get; set; }
        public bool can_read_all_group_messages { get; set; }
        public bool supports_inline_queries { get; set; }
    }

    public class BotConfiguration
    {
        public string BotToken { get; set; }
        public string HostAddress { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) //way to json file
                .AddJsonFile("appsettings.json").Build(); //open json file
         
            var sectionmid = config.GetSection(nameof(result)); //variable that searches for attributes named nameof(result)
            var sectiondown = sectionmid.GetSection(nameof(BotConfiguration));// same, but in result
           
            var resultconfig = sectionmid.Get<result>();//returns the attribute value
            var BotConfigurationconfig = sectiondown.Get<BotConfiguration>();//same


            Console.WriteLine(config.GetSection("ok").Value);
            Console.WriteLine(resultconfig.id);       
            Console.WriteLine(BotConfigurationconfig.BotToken);
            Console.WriteLine(BotConfigurationconfig.HostAddress);




        }
    }
}

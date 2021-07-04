using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SchoolRings.Data
{
    public class ScheduleManager
    {
        public async Task<Schedule> GetSchedule(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject<Schedule>(await reader.ReadToEndAsync());
                }
            }
        }

        public void SaveShedule(Schedule schedule, string fileName)
        {
            string json = JsonConvert.SerializeObject(schedule);
            File.WriteAllText(fileName, json);
        }
    }

}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Record.API.Models
{
    public class Preamble
    {
        public Preamble() { }

        public Preamble(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public async static Task<List<Preamble>> GetAllPreables()
        {
            List<Preamble> preambles = JsonConvert.DeserializeObject<List<Preamble>>(await File.ReadAllTextAsync(Environment.CurrentDirectory + "\\PreambleText.json"));
            return preambles;
        }

        public async static Task<Preamble> GetPreamble(string id)
        {
            var preamble = new Preamble();
            var preambles = await GetAllPreables();
            foreach (var item in preambles)
            {
                if (item.Id == id)
                {
                    return new Preamble(item.Id, item.Text);
                }
            }
            return new Preamble();
        }

        public static string GeneratePreamble(string absenceId)
        {
            string absencePreambleId = AbsenceType.GetAbsenceType(absenceId).Result.PreambleId;
            string preambleText = GetPreamble(absencePreambleId).Result.Text;

            return preambleText;
        }
    }


}

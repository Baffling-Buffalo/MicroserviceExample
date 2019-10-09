using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Record.API.Models
{
    public class Rationale
    {
        public Rationale() { }

        public Rationale(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public async static Task<List<Rationale>> GetAllRationales()
        {
            List<Rationale> rationales = JsonConvert.DeserializeObject<List<Rationale>>(await File.ReadAllTextAsync(Environment.CurrentDirectory + "\\RationaleText.json"));
            return rationales;
        }

        public async static Task<Rationale> GetRationale(string id)
        {
            var rationale = new Rationale();
            var rationales = await GetAllRationales();
            foreach (var item in rationales)
            {
                if (item.Id == id)
                {
                    return new Rationale(item.Id, item.Text);
                }
            }
            return new Rationale();
        }

        public static string GenerateRationaleText(string absenceId)
        {
            string absenceRationaleId = AbsenceType.GetAbsenceType(absenceId).Result.RationaleId;
            string rationaleText = GetRationale(absenceRationaleId).Result.Text;

            return rationaleText;
        }
    }


}

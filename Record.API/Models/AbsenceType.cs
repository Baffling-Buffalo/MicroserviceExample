using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Record.API.Models
{
    public class AbsenceType
    {
        public AbsenceType() { }

        public AbsenceType(string id, string superiorId, string description, string descriptionForForm, string preambleId, string mainTextId, string rationaleId)
        {
            Id = id;
            SuperiorId = superiorId;
            Description = description;
            DescriptionForForm = descriptionForForm;
            PreambleId = preambleId;
            MainTextId = mainTextId;
            RationaleId = rationaleId;
        }

        public string Id { get; set; }
        public string SuperiorId { get; set; }
        public string Description { get; set; }
        public string DescriptionForForm { get; set; }
        public string PreambleId { get; set; }
        public string MainTextId { get; set; }
        public string RationaleId { get; set; }

        public async static Task<List<AbsenceType>> GetAllAbsenceTypes()
        {
            List<AbsenceType> types = JsonConvert.DeserializeObject<List<AbsenceType>>(await File.ReadAllTextAsync(Environment.CurrentDirectory + "\\AbsenceTypes.json"));

            return types;
        }

        public async static Task<AbsenceType> GetAbsenceType(string id)
        {
            var types = await GetAllAbsenceTypes();
            foreach (var item in types)
            {
                if (item.Id == id)
                {
                    return new AbsenceType(item.Id, item.SuperiorId, item.Description, item.DescriptionForForm, item.PreambleId, item.MainTextId, item.RationaleId);
                }
            }
            return new AbsenceType();
        }
    }
}

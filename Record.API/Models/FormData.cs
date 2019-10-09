using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Record.API.Models
{
    public class FormData
    {
        [JsonProperty(PropertyName = "directorateName")]
        public string DirectorateName { get; set; }
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "employeeEmail")]
        public string EmployeeEmail { get; set; }
        [JsonProperty(PropertyName = "managerEmail")]
        public string ManagerEmail { get; set; }
        [JsonProperty(PropertyName = "signatoryPosition")]
        public string SignatoryPosition { get; set; }
        [JsonProperty(PropertyName = "signatoryFullName")]
        public string SignatoryFullName { get; set; }
        [JsonProperty(PropertyName = "preambleText")]
        public string PreambleText { get; set; }
        [JsonProperty(PropertyName = "mainText")]
        public string MainText { get; set; }
        [JsonProperty(PropertyName = "rationaleText")]
        public string RationaleText { get; set; }
        [JsonProperty(PropertyName = "deliver")]
        public string Deliver { get; set; }
    }
}

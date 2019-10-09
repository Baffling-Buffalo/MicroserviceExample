using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record.API.Models
{
    public class MainText
    {
        public MainText() { }

        public MainText(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public async static Task<List<MainText>> GetAll()
        {
            List<MainText> mainTexts = JsonConvert.DeserializeObject<List<MainText>>(await File.ReadAllTextAsync(Environment.CurrentDirectory + "\\MainText.json", Encoding.UTF8));
            return mainTexts;
        }

        public async static Task<MainText> GetMainText(string id)
        {
            var mainText = new MainText();
            var mainTexts = await GetAll();
            foreach (var item in mainTexts)
            {
                if (item.Id == id)
                {
                    return new MainText(item.Id, item.Text);
                }
            }
            return new MainText();
        }

        public static string GenerateMainText(JsonSet json)
        {
            var ZAPOSLEN = "";
            var IMENOVAN = "";
            var RAD = "";
            var RAD_OD_DO = "počev od " + json.ODSUSTVO_OD + " godine do " + json.ODSUSTVO_DO + " godine";
            var PO_OPIS = AbsenceType.GetAbsenceType(json.ID_ODSUSTVA).Result.DescriptionForForm;

            //set variables by 'POL'
            switch (json.POL)
            {
                case "Z": ZAPOSLEN = "zaposlena"; IMENOVAN = "Imenovana"; break;
                case "M": ZAPOSLEN = "zaposleni"; IMENOVAN = "Imenovani"; break;
            }

            //set variables by 'BR_DANA'
            switch (Convert.ToInt32(json.BR_DANA))
            {
                case 1: RAD = "radnog"; RAD_OD_DO = "i to " + json.ODSUSTVO_OD + " godine"; break;
                case int n when n > 1 && n < 5: RAD = "radna";  break;
                case int n when n >= 5: RAD = "radnih"; break;
            }

            string mainTextId = AbsenceType.GetAbsenceType(json.ID_ODSUSTVA).Result.MainTextId;

            StringBuilder mainText = new StringBuilder();
            mainText.Append(GetMainText(mainTextId).Result.Text);

            mainText.Replace("<IME_I_PREZIME>", json.IME_I_PREZIME);
            mainText.Replace("<RADBR>", json.RADBR);
            mainText.Replace("<ZAPOSLEN>", ZAPOSLEN);
            mainText.Replace("<NAZIV_OJ>", json.NAZIV_OJ);
            mainText.Replace("<NAZIV_RM>", json.NAZIV_RM);
            mainText.Replace("<BR_DANA>", json.BR_DANA);
            mainText.Replace("<RAD>", RAD);
            mainText.Replace("<RAD_OD_DO>", RAD_OD_DO);
            mainText.Replace("<PO_OPIS>", PO_OPIS);
            mainText.Replace("<IMENOVAN>", IMENOVAN);
            mainText.Replace("<DATUM_JAVLJANJA_NA_POSAO>", json.DATUM_JAVLJANJA_NA_POSAO);

            return mainText.ToString();
        }
    }
}

using Input_Form.Models;
using Microsoft.Extensions.Primitives;

namespace Input_Form
{
    public class FormulaConverter
    {
        public static string[] Tokens = {
            "+", "-", "*", "/", "Pow", "Sqrt", "Cbrt", "(", ")", ",",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public static string ConvertFormula(string formula)
        {
            string trimmedFormula = formula;
            foreach (var token in Tokens)
            {
                trimmedFormula = trimmedFormula.Replace(token, "");
            }

            string bufferFormula = formula;
            foreach (char c in trimmedFormula)
            {
                string character = c.ToString();
                string replacement = $"($('#{c}').val())";
                bufferFormula = bufferFormula.Replace(character, replacement);
            }

            bufferFormula = bufferFormula.Replace("Pow", "Math.pow");
            bufferFormula = bufferFormula.Replace("Sqrt", "Math.sqrt");
            bufferFormula = bufferFormula.Replace("Cbrt", "Math.cbrt");
            bufferFormula = "return " + bufferFormula;

            return bufferFormula;
        }
        public static Form ConvertAllFormulas(Form form)
        {
            Form bufferForm = form;
            string discriminantFormula = form.Discriminant.Formula;
            string firstResultFormula = form.FirstResult.Formula;
            string secondResultFormula = form.SecondResult.Formula;

            string[] thesaurus =
            {
                form.IndicatorA.Title.Replace("Значение ", ""),
                form.IndicatorB.Title.Replace("Значение ", ""),
                form.IndicatorC.Title.Replace("Значение ", ""),
                "D"
            };

            Dictionary<string, string> mathOperators = new Dictionary<string, string>
            {
                {"Pow", "Math.pow" },
                {"Sqrt", "Math.sqrt" },
                {"Cbrt", "Math.cbrt" }
            };

            foreach (string value in thesaurus)
            {
                string replacement = $"$('#{value}').val()";

                discriminantFormula = discriminantFormula.Replace(value, replacement);
                firstResultFormula = firstResultFormula.Replace(value, replacement);
                secondResultFormula = secondResultFormula.Replace(value, replacement);
            }

            foreach (var pair in mathOperators)
            {
                discriminantFormula = discriminantFormula.Replace(pair.Key, pair.Value);
                firstResultFormula = firstResultFormula.Replace(pair.Key, pair.Value);
                secondResultFormula = secondResultFormula.Replace(pair.Key, pair.Value);
            }

            bufferForm.Discriminant.Value = "return " + discriminantFormula;
            bufferForm.FirstResult.Value = "return " + firstResultFormula;
            bufferForm.SecondResult.Value = "return " + secondResultFormula;

            return bufferForm;
        }
    }
}

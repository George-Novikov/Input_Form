using Input_Form.Models;
using Microsoft.Extensions.Primitives;

namespace Input_Form
{
    public class FormulaConverter
    {
        private static string[] Tokens = {
            "+", "-", "*", "/", "Pow", "Sqrt", "Cbrt", "(", ")", ",",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        private static Dictionary<string, string> mathOperators = new Dictionary<string, string>
            {
                {"Pow", "Math.pow" },
                {"Sqrt", "Math.sqrt" },
                {"Cbrt", "Math.cbrt" }
            };
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

            foreach (var pair in mathOperators)
            {
                bufferFormula = bufferFormula.Replace(pair.Key, pair.Value);
            }

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
                form.IndicatorA.Title,
                form.IndicatorB.Title,
                form.IndicatorC.Title,
                form.Discriminant.Title,
                form.FirstResult.Title,
                form.SecondResult.Title
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

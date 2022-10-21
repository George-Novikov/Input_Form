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
    }
}

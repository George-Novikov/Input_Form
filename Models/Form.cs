using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Input_Form.Models
{
    public class Form
    {
        public int FormId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string FormName { get; set; } = "";
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Indicator IndicatorA { get; set; }
        public double ValueA { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Indicator IndicatorB { get; set; }
        public double ValueB { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Indicator IndicatorC { get; set; }
        public double ValueC { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Indicator Discriminant { get; set; }
        public double DiscriminantValue { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Indicator FirstResult { get; set; }
        public double FirstResultValue { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Indicator SecondResult { get; set; }
        public double SecondResultValue { get; set; }

        public void SetFormCreationDateTime()
        {
            CreationDateTime = DateTime.Now;
            FormName = "Form_" + CreationDateTime.ToString();
        }
        public void InitializeDefaultValues()
        {
            IndicatorA = new ManualInput { Title = "Значение A", Formula = "-" };
            IndicatorB = new ManualInput { Title = "Значение B", Formula = "-" };
            IndicatorC = new ManualInput { Title = "Значение C", Formula = "-" };
        }
        public void InitializeDefaultFormulas()
        {
            Discriminant = new Formula { Title = "Дискриминант", Formula = "Pow(B,2)-4*A*C" };
            FirstResult = new Formula { Title = "Первый результат", Formula = "(-B+Sqrt(D))/2*A" };
            SecondResult = new Formula { Title = "Второй результат", Formula = "(-B-Sqrt(D))/2*A" };
        }
        public bool CalculateValues()
        {
            Discriminant.Value = (Math.Pow(IndicatorB.Value, 2)) - (4 * IndicatorA.Value * IndicatorC.Value);

            if (Discriminant.Value >= 0)
            {
                if (IndicatorA.Value != 0)
                {
                    FirstResult.Value = (-IndicatorB.Value + Math.Sqrt(Discriminant.Value)) / (2 * IndicatorA.Value);
                    SecondResult.Value = (-IndicatorB.Value - Math.Sqrt(Discriminant.Value)) / (2 * IndicatorA.Value);
                    SetValues();
                    return true;
                }
                else
                {
                    FirstResult.Value = 0;
                    SecondResult.Value = 0;
                    SetValues();
                    return true;
                }
            }
            else
            {
                FirstResult.Value = 0;
                SecondResult.Value = 0;
                SetValues();
                return false;
            }
        }
        public void SetValues()
        {
            ValueA = IndicatorA.Value;
            ValueB = IndicatorB.Value;
            ValueC = IndicatorC.Value;
            DiscriminantValue = Discriminant.Value;
            FirstResultValue = FirstResult.Value;
            SecondResultValue = SecondResult.Value;

            IndicatorA.Description = IndicatorA.Type + "_Result_" + ValueA.ToString();
            IndicatorB.Description = IndicatorB.Type + "_Result_" + ValueB.ToString();
            IndicatorC.Description = IndicatorC.Type + "_Result_" + ValueC.ToString();
            Discriminant.Description = Discriminant.Type + "_Result_" + DiscriminantValue.ToString();
            FirstResult.Description = FirstResult.Type + "_Result_" + FirstResultValue.ToString();
            SecondResult.Description = SecondResult.Type + "_Result_" + SecondResultValue.ToString();
        }
    }
}

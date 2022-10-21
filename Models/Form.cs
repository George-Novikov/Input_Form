﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Input_Form.Models
{
    public class Form
    {
        public int FormId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string FormName { get; set; } = "";
        public Indicator IndicatorA { get; set; }
        public string ValueA { get; set; } = "0";
        public Indicator IndicatorB { get; set; }
        public string ValueB { get; set; } = "0";
        public Indicator IndicatorC { get; set; }
        public string ValueC { get; set; } = "0";
        public Indicator Discriminant { get; set; }
        public string DiscriminantValue { get; set; } = "0";
        public Indicator FirstResult { get; set; }
        public string FirstResultValue { get; set; } = "0";
        public Indicator SecondResult { get; set; }
        public string SecondResultValue { get; set; } = "0";

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

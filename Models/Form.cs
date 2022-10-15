using System.ComponentModel.DataAnnotations.Schema;

namespace Input_Form.Models
{
    public class Form
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Title { get; set; } = "";
        public Indicator? ValueA { get; set; }
        public Indicator? ValueB { get; set; }
        public Indicator? ValueC { get; set; }
        public Indicator? Discriminant { get; set; }
        public Indicator? FirstResult { get; set; }
        public Indicator? SecondResult { get; set; }
        public Form()
        {
            CreationDateTime = DateTime.Now;
        }
        public void InitializeDefaultValues()
        {
            ValueA = new ManualInput { Title = "Значение A" };
            ValueB = new ManualInput { Title = "Значение B" };
            ValueC = new ManualInput { Title = "Значение C" };
        }
        public void InitializeDefaultFormulas()
        {
            Discriminant = new Formula
            {
                Title = "Дискриминант",
                Value = Math.Pow(ValueB.Value, 2) - 4 * ValueA.Value * ValueC.Value
            };
            FirstResult = new Formula
            {
                Title = "Первый результат",
                Value = (-ValueB.Value + Math.Sqrt(Discriminant.Value)) / 2 * ValueA.Value
            };
            SecondResult = new Formula
            {
                Title = "Второй результат",
                Value = (-ValueB.Value - Math.Sqrt(Discriminant.Value)) / 2 * ValueA.Value
            };
        }
    }
}

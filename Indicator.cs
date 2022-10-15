using Input_Form.Models;

namespace Input_Form
{
    public class Indicator
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = "";
        public double Value { get; set; } = 0;
        public string Type { get; set; }
        public Indicator()
        {
            Type = this.GetType().Name.ToString();
        }
        public static Form InitializeForm()
        {
            Form form = new Form();

            ManualInput a = new ManualInput { Id = 1, Title = "Значение A" };
            ManualInput b = new ManualInput { Id = 2, Title = "Значение B" };
            ManualInput c = new ManualInput { Id = 3, Title = "Значение C" };

            Formula discriminant = new Formula
            {
                Id = 4,
                Title = "Дискриминант",
                Value = Math.Pow(b.Value, 2) - 4 * a.Value * c.Value
            };
            Formula firstResult = new Formula
            {
                Id = 5,
                Title = "Первый результат",
                Value = (-b.Value + Math.Sqrt(discriminant.Value)) / 2 * a.Value
            };
            Formula secondResult = new Formula
            {
                Id = 6,
                Title = "Второй результат",
                Value = (-b.Value - Math.Sqrt(discriminant.Value)) / 2 * a.Value
            };

            form.ValueA = a;
            form.ValueB = b;
            form.ValueC = c;
            form.Discriminant = discriminant;
            form.FirstResult = firstResult;
            form.SecondResult = secondResult;

            return form;
        }
    }
    public class Formula : Indicator { }
    public class ManualInput : Indicator { }
}

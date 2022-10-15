using Input_Form.Models;

namespace Input_Form
{
    public class Indicator
    {
        public int IndicatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = "";
        public double Value { get; set; } = 0;
        public string Formula { get; set; } = "";
        public string Type { get; set; }
        public Indicator()
        {
            Type = this.GetType().Name.ToString();
        }
        public static List<Indicator> InitializeDb()
        {
            List<Indicator> indicators = new List<Indicator>();

            return indicators;
        }
    }
    public class Formula : Indicator { }
    public class ManualInput : Indicator { }
}

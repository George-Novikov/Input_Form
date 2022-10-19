using Input_Form.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Input_Form
{
    public class Indicator
    {
        public int IndicatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = "";
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public double Value { get; set; } = 0;
        public string Formula { get; set; } = "";
        public string Type { get; set; }
        public Indicator()
        {
            Type = this.GetType().Name.ToString();
        }
    }
    public class Formula : Indicator { }
    public class ManualInput : Indicator { }
}

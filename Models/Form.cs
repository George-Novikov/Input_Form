namespace Input_Form.Models
{
    public class Form
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Title { get; set; }
        public Indicator ValueA { get; set; }
        public Indicator ValueB { get; set; }
        public Indicator ValueC { get; set; }
        public Indicator Discriminant { get; set; }
        public Indicator FirstResult { get; set; }
        public Indicator SecondResult { get; set; }
        public Form()
        {
            CreationDateTime = DateTime.Now;
        }
    }
}

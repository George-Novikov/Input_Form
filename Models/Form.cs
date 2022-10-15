using System.ComponentModel.DataAnnotations.Schema;

namespace Input_Form.Models
{
    public class Form
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Title { get; set; } = "";
        public Indicator ValueA { get; set; }
        public Indicator ValueB { get; set; }
        public Indicator ValueC { get; set; }
        public Indicator Discriminant { get; set; }
        public Indicator FirstResult { get; set; }
        public Indicator SecondResult { get; set; }
        public DateTime SetFormCreationDateTime()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime;
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
                Value = Math.Pow(ValueB.Value, 2) - 4 * ValueA.Value * ValueC.Value,
                Formula = "Pow(B,2)-4*A*C"
            };
            FirstResult = new Formula
            {
                Title = "Первый результат",
                Value = (-ValueB.Value + Math.Sqrt(Discriminant.Value)) / 2 * ValueA.Value,
                Formula = "(-B+Sqrt(D))/2*A"
            };
            SecondResult = new Formula
            {
                Title = "Второй результат",
                Value = (-ValueB.Value - Math.Sqrt(Discriminant.Value)) / 2 * ValueA.Value,
                Formula = "(-B-Sqrt(D))/2*A"
            };
        }

        public static Form LoadForm()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var form = db.Forms.OrderByDescending(f => f.Id).FirstOrDefault();
                Form loadedForm;

                if (form != null)
                {
                    loadedForm = form;
                }
                else
                {
                    loadedForm = new Form();
                    loadedForm.SetFormCreationDateTime();
                    loadedForm.InitializeDefaultValues();
                    loadedForm.InitializeDefaultFormulas();
                    using (ApplicationContext nestedDb = new ApplicationContext())
                    {
                        nestedDb.Indicators.AddRange(loadedForm.ValueA, loadedForm.ValueB, loadedForm.ValueC);
                        nestedDb.Forms.Add(loadedForm);
                        nestedDb.SaveChanges();
                    }
                }
                return loadedForm;
            }
        }
    }
}

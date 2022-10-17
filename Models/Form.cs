using System.ComponentModel.DataAnnotations.Schema;

namespace Input_Form.Models
{
    public class Form
    {
        public int FormId { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string FormName { get; set; } = "";
        public Indicator IndicatorA { get; set; }
        public double ValueA { get; set; }
        public Indicator IndicatorB { get; set; }
        public double ValueB { get; set; }
        public Indicator IndicatorC { get; set; }
        public double ValueC { get; set; }
        public Indicator Discriminant { get; set; }
        public double DiscriminantValue { get; set; }
        public Indicator FirstResult { get; set; }
        public double FirstResultValue { get; set; }
        public Indicator SecondResult { get; set; }
        public double SecondResultValue { get; set; }

        public void SetFormCreationDateTime()
        {
            CreationDateTime = DateTime.Now;
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
            Discriminant.Value = Math.Pow(IndicatorB.Value, 2) - 4 * IndicatorA.Value * IndicatorC.Value;

            if (Discriminant.Value >= 0)
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

            FormName = "Form_" + CreationDateTime.ToShortDateString();
        }

        public static Form LoadForm()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var form = db.Forms.OrderByDescending(f => f.FormId).FirstOrDefault();
                db.Entry(form).Reference(f => f.IndicatorA).Load();
                db.Entry(form).Reference(f => f.IndicatorB).Load();
                db.Entry(form).Reference(f => f.IndicatorC).Load();
                db.Entry(form).Reference(f => f.Discriminant).Load();
                db.Entry(form).Reference(f => f.FirstResult).Load();
                db.Entry(form).Reference(f => f.SecondResult).Load();

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
                        nestedDb.Indicators.AddRange(loadedForm.IndicatorA, loadedForm.IndicatorB, loadedForm.IndicatorC);
                        nestedDb.Forms.Add(loadedForm);
                        nestedDb.SaveChanges();
                    }
                }
                return loadedForm;
            }
        }
    }
}

using Input_Form.Models;

namespace Input_Form
{
    public class FormCreator
    {
        public static Form LoadForm()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var form = db.Forms.OrderByDescending(f => f.FormId).FirstOrDefault();

                Form loadedForm;

                if (form != null)
                {
                    db.Entry(form).Reference(f => f.IndicatorA).Load();
                    db.Entry(form).Reference(f => f.IndicatorB).Load();
                    db.Entry(form).Reference(f => f.IndicatorC).Load();
                    db.Entry(form).Reference(f => f.Discriminant).Load();
                    db.Entry(form).Reference(f => f.FirstResult).Load();
                    db.Entry(form).Reference(f => f.SecondResult).Load();
                    loadedForm = form;
                }
                else
                {
                    loadedForm = new Form();
                    loadedForm.SetFormCreationDateTime();
                    loadedForm.InitializeDefaultValues();
                    loadedForm.InitializeDefaultFormulas();
                    loadedForm.SetValues();
                    using (ApplicationContext nestedDb = new ApplicationContext())
                    {
                        nestedDb.Forms.Add(loadedForm);
                        nestedDb.SaveChanges();
                    }
                }
                return loadedForm;
            }
        }
        
        public static void SaveForm(Form form)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Forms.Add(form);
                db.SaveChanges();
            }
        }
    }
}

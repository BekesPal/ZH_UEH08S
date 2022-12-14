using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZH_UEH08S
{
    public partial class UserControl2 : UserControl
    {
        Models.TkContext context = new Models.TkContext();
        public UserControl2()
        {
            InitializeComponent();
        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if(form2.ShowDialog() == DialogResult.OK)
            {
                Models.Student student = new Models.Student();
                student.Name = form2.textBoxNev.Text;
                student.Neptun = form2.textBoxNeptun.Text;
                student.StudentId = context.Students.OrderByDescending(s => s.StudentId).First().StudentId + 1;

                context.Students.Add(student);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            studentBindingSource.DataSource = context.Students.ToList();
            }
        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            studentBindingSource.DataSource = context.Students.ToList();
        }
    }
}

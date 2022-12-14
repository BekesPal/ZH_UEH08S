using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZH_UEH08S
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(this.ValidateChildren() == true) this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBoxNev_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxNev.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxNev, "Nem lehet üres");
            }
        }

        private void textBoxNev_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxNev,string.Empty);
        }

        private void textBoxNeptun_Validating(object sender, CancelEventArgs e)
        {
            Regex regex = new Regex("^[A-Z0-9a-z]{6}$");
            if(!regex.IsMatch(textBoxNeptun.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBoxNeptun, "Helyes formátumban adja meg");
            }
        }

        private void textBoxNeptun_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxNeptun,string.Empty);
        }
    }
}

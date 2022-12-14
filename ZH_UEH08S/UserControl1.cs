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
    public partial class UserControl1 : UserControl
    {
        Models.TkContext context = new Models.TkContext();
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            FilterStudents();
            FilterTextbooks();
        }

        private void txtStudent_TextChanged(object sender, EventArgs e)
        {
            FilterStudents();
        }

        private void FilterStudents()
        {
            var students = from x in context.Students
                           where x.Name.Contains(txtStudent.Text)
                           select x;
            listStudent.DataSource = students.ToList();
            listStudent.DisplayMember = "Name";
        }

        private void txtTextBook_TextChanged(object sender, EventArgs e)
        {
            FilterTextbooks();
        }

        private void FilterTextbooks()
        {
            var textbooks = from x in context.Textbooks
                            where x.Title.Contains(txtTextBook.Text)
                            select x;
            listTextBook.DataSource = textbooks.ToList();
            listTextBook.DisplayMember = "Title";
        }

        private void listStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListOrders();

        }

        private void ListOrders()
        {
            var selectedStudent = (Models.Student)listStudent.SelectedValue;
            var orders = from x in context.Orders
                         where x.StudentFk == selectedStudent.StudentId
                         select new OrderDetail
                         {
                             OrderSk = x.OrderSk,
                             Title = x.TextbookFkNavigation.Title,
                             Price = x.TextbookFkNavigation.Price
                         };
            orderDetailBindingSource.DataSource = orders.ToList();
            var total = (from x in orders
                         select x.Price).Sum();
            txtRendelesOsszeg.Text = total.ToString() + " Ft";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedStudent = (Models.Student)listStudent.SelectedValue;
            var selectedTextBook = (Models.Textbook)listTextBook.SelectedValue;
            Models.Order newOrder = new Models.Order()
            {
                StudentFk = selectedStudent.StudentId,
                TextbookFk = selectedTextBook.TextbookId
            };
            context.Orders.Add(newOrder);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            ListOrders();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var selectedOrderSk = ((OrderDetail)orderDetailBindingSource.Current).OrderSk;
            var orderToBeDeleted = (from x in context.Orders
                                    where x.OrderSk == selectedOrderSk
                                    select x).FirstOrDefault();
            if(MessageBox.Show("Biztosan ki akarja törölni","Törlés megerősítése",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                context.Orders.Remove(orderToBeDeleted);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                ListOrders();
            }
            
        }
    }
}

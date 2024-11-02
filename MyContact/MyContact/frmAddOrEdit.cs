using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContact
{
    public partial class frmAddOrEdit : Form
    {
        ContactsEntities db = new ContactsEntities();
        public int contactID =0;
        public frmAddOrEdit()
        {
            InitializeComponent();
        }

        private void frmAddOrEdit_Load(object sender, EventArgs e)
        {
            if(contactID == 0)
            {
                this.Text = "افزودن شخص مدنظر";
            }
            else
            {
                this.Text = "ویرایش شخص مورد نظر";
                MyContact contact = db.MyContact.Find(contactID);
              
                txtName.Text = contact.Name;    
                txtEmail.Text = contact.Email;
                txtMobile.Text = contact.Mobile;
                txtAddress.Text = contact.Address;
                txtFamily.Text = contact.Family;
                txtAge.Text = contact.Age.ToString();
                btnSubmit.Text = "ویرایش";
            }
        }

        bool ValidateInputs()
        {


            if (txtName.Text == "")
            {

                MessageBox.Show("لطفا نام را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("لطفا ایمیل را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFamily.Text == "")
            {
                MessageBox.Show("لطفا نام خانوادگی را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtAge.Value == 0)
            {
                MessageBox.Show("لطفا سن را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobile.Text == "")
            {
                MessageBox.Show("لطفا موبایل را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(ValidateInputs())
            {
                if (contactID ==0)
                {
                    MyContact myContact = new MyContact()
                    {
                        Name=txtName.Text,
                        Family=txtFamily.Text,
                        Address=txtAddress.Text,
                        Email=txtEmail.Text,
                        Age=(int)txtAge.Value,
                        Mobile=txtMobile.Text,
                    };
                    db.MyContact.Add(myContact);
                }
                else
                {
                    var contact = db.MyContact.Find(contactID);
                    contact.Name = txtName.Text;
                    contact.Family = txtFamily.Text;
                    contact.Address = txtAddress.Text;
                    contact.Email = txtEmail.Text;
                    contact.Age = (int)txtAge.Value; 
                    contact.Mobile=txtMobile.Text;
                }
                db.SaveChanges();
                MessageBox.Show("عملیات با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }

        }

    }
}

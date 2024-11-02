﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BindGrid()
        {

            using (ContactsEntities db = new ContactsEntities())
            {
                dgContacts.AutoGenerateColumns = false;
                dgContacts.Columns[0].Visible = false;
                dgContacts.DataSource = db.MyContact.ToString();
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAddOrEdit frm = new frmAddOrEdit();
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                string name = dgContacts.CurrentRow.Cells[1].Value.ToString();
                string family = dgContacts.CurrentRow.Cells[2].Value.ToString();
                string fullName = name + "  " + family;

                if (MessageBox.Show($"آیا از حذف {fullName} مطمئن هستید ؟", "توجه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int contactID = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());

                   using(ContactsEntities db = new ContactsEntities())
                    {
                        MyContact myContact = db.MyContact.Single(x => x.ContactID == contactID);
                        db.MyContact.Remove(myContact);
                        db.SaveChanges();
                    }

                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفا کاربری را وارد کنید!");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using(ContactsEntities db = new ContactsEntities())
            {
                dgContacts.DataSource = db.MyContact
                   .Where(x => x.Name.Contains(txtSearch.Text) || x.Family.Contains(txtSearch.Text)).ToList();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContacts.CurrentRow != null)
            {
                int contactID = int.Parse(dgContacts.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEdit frm = new frmAddOrEdit();
                frm.contactID = contactID;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }          
        }
        private void dgContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

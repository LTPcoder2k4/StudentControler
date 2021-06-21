using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using System.IO;

namespace Student_Controler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Declare();
            LoadHistory();
            buttonAdjust.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void LoadHistory()
        {
            try
            {
                using (StreamReader sr = new StreamReader("db.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] temp = line.Split('|');
                        Student student = new Student(); 
                        student.SetData(temp[0], temp[1], temp[2], temp[3], temp[4], temp[5]);
                        listView.Items.Add(student.GetItem());

                    }
                }
            }
            catch (Exception)
            {
                using (StreamWriter sw = new StreamWriter("db.txt"))
                {
                }
            }
        }

        private void Declare()
        {
            listView.Columns.Add("Name", 160);
            listView.Columns.Add("Class", 80);
            listView.Columns.Add("Code", 80);
            listView.Columns.Add("Birth", 100);
            listView.Columns.Add("Address", 185);
            listView.Columns.Add("Gender", 80);
            listView.View = View.Details;
            listView.FullRowSelect = true;
        }

        private void Renew()
        {
            inputAddress.Texts = "";
            inputClass.Texts = "";
            inputCode.Texts = "";
            inputName.Texts = "";
            checkFemale.Checked = false;
            checkMale.Checked = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.SetData(inputName.Texts, inputClass.Texts, inputCode.Texts, inputBirth.Value.ToString("dd-MM-yyyy"), inputAddress.Texts, (checkMale.Checked) ? "Male" : "Female");
            if (student.IsLegal() && (checkMale.Checked || checkFemale.Checked))
            {
                listView.Items.Add(student.GetItem());
                Renew();
            }
            else
            {
                MessageBox.Show("Please fill in the form completely!", "Error");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            listView.Items.Remove(listView.SelectedItems[0]);
            buttonAdjust.Enabled = false;
            buttonDelete.Enabled = false;
            Renew();
        }

        private void listView_Click(object sender, EventArgs e)
        {
            buttonAdjust.Enabled = true;
            buttonDelete.Enabled = true;
            ListViewItem item = listView.SelectedItems[0];
            inputName.Texts = item.SubItems[0].Text;
            inputClass.Texts = item.SubItems[1].Text;
            inputCode.Texts = item.SubItems[2].Text;
            inputBirth.Value = new DateTime(int.Parse(item.SubItems[3].Text[6].ToString() + item.SubItems[3].Text[7].ToString() + item.SubItems[3].Text[8].ToString() + item.SubItems[3].Text[9].ToString()), int.Parse(item.SubItems[3].Text[3].ToString() + item.SubItems[3].Text[4].ToString()), int.Parse(item.SubItems[3].Text[0].ToString() + item.SubItems[3].Text[1].ToString()));
            inputAddress.Texts = item.SubItems[4].Text;
            checkFemale.Checked = (item.SubItems[5].Text == "Female") ? true : false;
            checkMale.Checked = (item.SubItems[5].Text == "Male") ? true : false;
        }

        private void buttonAdjust_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView.SelectedItems[0];
            item.SubItems[0].Text = inputName.Texts;
            item.SubItems[1].Text = inputClass.Texts;
            item.SubItems[2].Text = inputCode.Texts;
            item.SubItems[3].Text = inputBirth.Value.ToString("dd-MM-yyyy");
            item.SubItems[4].Text = inputAddress.Texts;
            item.SubItems[5].Text = (checkMale.Checked) ? "Male" : "Female";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("db.txt"))
            {
                foreach (ListViewItem item in listView.Items)
                {
                    foreach (ListViewItem.ListViewSubItem obj in item.SubItems)
                    { 
                        sw.Write(obj.Text.ToString() + "|");
                    }
                    sw.WriteLine();
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Controler
{
    class Student
    {
        private string Name, Class, Code, Birth, Address, Gender;
        public void SetData(string name, string clas, string code, string birth, string address, string gender)
        {
            Name = name;
            Class = clas;
            Code = code;
            Birth = birth;
            Address = address;
            Gender = gender;
        }

        public bool IsLegal()
        {
            return !(Name.Length == 0 || Class.Length == 0 || Code.Length == 0 || Birth.Length == 0 || Address.Length == 0);
        }

        public ListViewItem GetItem()
        {
            ListViewItem item = new ListViewItem(Name);
            item.SubItems.Add(Class);
            item.SubItems.Add(Code);
            item.SubItems.Add(Birth);
            item.SubItems.Add(Address);
            item.SubItems.Add(Gender);
            return item;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T223817
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                bindingSource1.Add(new DataItem() { Count = i });
            }
        }

        private void repositoryItemCustomEdit1_InvalidCharacter(object sender, InvalidCharacterEventArgs e)
        {
            // if you need to show a custom tooltip, do this in this event and set the e.Handled property to True to prevent the default tooltip
        }
    }

    public class DataItem
    {
        public int Count { get; set; }
        public DataItem()
        {
        }
    }
}

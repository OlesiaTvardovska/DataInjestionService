using NewsApp.Desktop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace NewsApp.Desktop
{
    public partial class Form1 : Form
    {
        private const int n = 20;
        private List<News> newsList;
        public Form1()
        {
            newsList = null;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            
            newsList = (await NewsService.GetUnmarkedNews()).news_list.Take(n).ToList();
            foreach (var item in newsList){
                listBox1.Items.Add(item.Title);
            };

            //for (int i = 0; i < n; i++)
            //{
            //    string istr = i.ToString();

            //    Button newButton = new Button();
            //    newButton.Text = istr;
            //    newButton.Name = "btn" + istr;
            //    newButton.Visible = true;
            //    newButton.BackColor = Color.AliceBlue;
            //    newButton.ForeColor = Color.Black;

            //    listBox1.Controls.Add(newButton);
            //    newButton.Dock = DockStyle.Top;
            //    newButton.BringToFront();
            //}
            Debug.WriteLine(newsList);
        }
    }
}

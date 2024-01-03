using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace copymaster
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.listBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
           // this.listBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox1_DragEnter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                listBox1.Items.Add(s[i]);
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            //MessageBox.Show("ENTER");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count==0)
            {
                MessageBox.Show("drag file to list box");                return;
            }
            string path_des = textBox1.Text;
            if (path_des.Substring(path_des.Length - 1) != @"\") path_des += @"\";
            if (!Directory.Exists(path_des))
            {
                MessageBox.Show("Path not found !");                return;
            }


           

            string[] str_list = new string[listBox1.Items.Count];
            for (int i = 0; i < listBox1.Items.Count; i++) 
                str_list[i]=listBox1.Items[i].ToString();

            string path_common = GetLongestCommonPrefix(str_list);

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string full_path = Path.GetDirectoryName(listBox1.Items[i].ToString());
                string fullpath_file = listBox1.Items[i].ToString();
                string file_name = Path.GetFileName(fullpath_file);
                //start create direcory
                fn_create_path(full_path, path_common, path_des);
                //start copy files
                string mid_path = "";
                if (full_path != path_common)
                    mid_path = full_path.Substring(path_common.Length + 1) + @"\";
                string new_path = path_des + mid_path  + file_name;
                File.Copy(fullpath_file, new_path ,true);

            }
            MessageBox.Show("Successfull");
            System.Diagnostics.Process.Start(path_des);
            
            


        }
        
        public static string GetLongestCommonPrefix(string[] s)
        {
            int k = s[0].Length;
            for (int i = 1; i < s.Length; i++)
            {
                k = Math.Min(k, s[i].Length);
                for (int j = 0; j < k; j++)
                    if (s[i][j] != s[0][j])
                    {
                        k = j;
                        break;
                    }
            }
            int int_last_backslash = s[0].Substring(0, k).LastIndexOf(@"\");
            return s[0].Substring(0, k).Substring(0,int_last_backslash);
        }
        

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            

        }
        private void fn_create_path(string str_fullpath,string str_commonpath,string str_des)
        {
            if (str_fullpath.Length==str_commonpath.Length)
            {
                return;
            }
            string x = str_fullpath.Substring(str_commonpath.Length+1);
            //MessageBox.Show(x);

            string[] words = x.Split('\\');
            string tmp = "";
            for (int i = 0; i < words.Length; i++)
            {
                tmp+=words[i]+@"\";
                Directory.CreateDirectory(str_des+tmp);
                //MessageBox.Show(str_des+tmp);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                System.Diagnostics.Process.Start(textBox1.Text);
            }
            else
            {
                MessageBox.Show("not found");
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show("vahid.zahani@gmail.com");
        }

      
    }
}

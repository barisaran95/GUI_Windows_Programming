using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string file_path = "";
        bool saved = true;
        //int current_line = 1;
        int command_current_line=0;
        string selected_string = "";
        int selected_index = 0;
        List<string> lines = new List<string>();
        int current_line = 0;
        int current_first = 0;
        int suffix_lines=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            
        }

        //EXIT CLICK
        private void menuItem10_Click(object sender, EventArgs e)
        {
            if (!saved) {
                var confirmResult = MessageBox.Show("Do you want to save changes?", "Text Editor",MessageBoxButtons.YesNoCancel);
                if (confirmResult == DialogResult.Yes)
                {
                    menuItem7_Click(sender, e);
                    Application.Exit();
                }
                else if (confirmResult == DialogResult.No)
                {
                    Application.Exit();
                }
                else
                {
                    return;
                }
            }
            else {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //SAVE CLICK
        private void menuItem7_Click(object sender, EventArgs e)
        {
            if (file_path == "")
            {
                menuItem8_Click(sender, e);
            }
            else
            {
                System.IO.StreamWriter tmp = new System.IO.StreamWriter(file_path);
                tmp.Write(richTextBox1.Text);
                tmp.Close();
            }
        }

        //SAVE AS CLICK
        private void menuItem8_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.Title = "SAVE AS";
            if (svf.ShowDialog() == DialogResult.OK)
            {
                file_path = svf.FileName;
                System.IO.StreamWriter tmp = new System.IO.StreamWriter(file_path);
                tmp.Write(richTextBox1.Text);
                tmp.Close();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            saved = false;
            textBox3.Text = "Total Lines: " + richTextBox1.Lines.Count();
            textBox4.Text = "Column: " + (richTextBox1.SelectionStart- richTextBox1.GetFirstCharIndexOfCurrentLine()+1);
            suffix_lines = richTextBox1.Lines.Count();
            List<string> tmp = new List<string>();
            for (int i=0;i<suffix_lines;i++)
            {
                tmp.Add("===");
            }
            textBox6.Lines = tmp.ToArray();
        }

        //OPEN CLICK
        private void menuItem9_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                var confirmResult = MessageBox.Show("Do you want to save changes?", "Text Editor", MessageBoxButtons.YesNoCancel);
                if (confirmResult == DialogResult.Yes)
                {
                    menuItem7_Click(sender, e);
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                }
                else if (confirmResult == DialogResult.No)
                {
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                }
                else
                {
                    return;
                }
            }
            richTextBox1.Clear();
            richTextBox2.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "OPEN";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                file_path = ofd.FileName;
                System.IO.StreamReader tmp = new System.IO.StreamReader(ofd.FileName);
                richTextBox1.Text = tmp.ReadToEnd();
                int i = richTextBox1.SelectionStart;
                System.IO.FileInfo fi = new System.IO.FileInfo(ofd.FileName);
                long size = fi.Length;
                tmp.Close();
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            saved = false;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
        
        }

        //COMMANDLINE KEYDOWN
        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBox4.Text = "Column: " + (richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1);
            if (richTextBox2.Text.Length>0 && richTextBox2.Text[0] == '\n')
            {
                richTextBox2.Text = "";
                
            }          
            if (e.KeyCode == Keys.Enter)
            {
                string[] words = richTextBox2.Text.Split(' ');

                if (richTextBox2.Text.Length == 0)
                {
                    return;
                }
                if(selected_string != "")
                {
                    richTextBox1.SelectAll();
                    richTextBox1.SelectionColor = System.Drawing.Color.Black;
                    richTextBox1.SelectionBackColor = System.Drawing.Color.White;
                }
                if (words[0] == "f" || words[0] == "find")
                {
                    string find_text= words[1];
                    if (richTextBox1.Text.Contains(find_text))
                    {  
                        selected_string = find_text;
                        int index = 0;
                        while (index < richTextBox1.Text.Length)
                        {
                            int startIndex = richTextBox1.Text.IndexOf(find_text,index);
                            if (startIndex == -1)
                            {
                                index = richTextBox1.Text.Length;
                            }
                            else
                            {
                                richTextBox1.Select(startIndex, find_text.Length);
                                richTextBox1.SelectionColor = System.Drawing.Color.White;
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Blue;
                                index = startIndex + find_text.Length;
                                richTextBox1.SelectionStart = current_first;
                            }
                        }
                    }
                    else
                    {
                        textBox2.Text = "Couldn't find the string";
                    }
                }
                else if(words[0] == "r" || words[0]=="replace")
                {
                    string find_text = words[1];
                    string replaced_with = "";
                    for (int i = 2; i < words.Length; i++)
                    {
                        replaced_with += words[i];
                        if (i != words.Length - 1)
                        {
                            replaced_with += " ";
                        }
                    }
                    if (richTextBox1.Text.Contains(find_text))
                    {                       
                        int index = 0;
                        while (index < richTextBox1.Text.Length)
                        {
                            int startIndex = richTextBox1.Text.IndexOf(find_text, index);
                            if (startIndex == -1)
                            {
                                index = richTextBox1.Text.Length;
                            }
                            else
                            {
                                richTextBox1.Select(startIndex, find_text.Length);
                                richTextBox1.SelectedText=replaced_with;
                                richTextBox1.Select(startIndex, replaced_with.Length);
                                richTextBox1.SelectionColor = System.Drawing.Color.Black;
                                richTextBox1.SelectionBackColor = System.Drawing.Color.Yellow;
                                index = startIndex + replaced_with.Length;
                                richTextBox1.SelectionStart = current_first;
                            }
                        }
                    }
                    else
                    {
                        textBox2.Text = "Couldn't find the string";
                    }
                }
                else if(words[0] == "#")
                {
                    try
                    {
                        int go_to_line = Convert.ToInt32(words[1]);
                        current_first = richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1);
                        richTextBox1.SelectionStart = current_first;
                        richTextBox1.Focus();
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }

                }
                else if (words[0] == "up")
                {
                    try
                    {
                        int go_to_line = current_line-Convert.ToInt32(words[1]);
                        current_first = richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1);
                        richTextBox1.SelectionStart = current_first;
                        richTextBox1.Focus();
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                else if (words[0] == "down")
                {
                    try
                    {
                        int go_to_line = current_line + Convert.ToInt32(words[1]);
                        current_first = richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1);
                        richTextBox1.SelectionStart = current_first;
                        richTextBox1.Focus();
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                else if (words[0] == "left")
                {
                    try
                    {
                        int go_to_column = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1 - Convert.ToInt32(words[1]);
                        richTextBox1.SelectionStart = go_to_column;
                        richTextBox1.Focus();
                        textBox4.Text = "Column: " + go_to_column;
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                else if (words[0] == "right")
                {
                    try
                    {
                        int go_to_column = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1 + Convert.ToInt32(words[1]);
                        richTextBox1.SelectionStart = go_to_column;
                        richTextBox1.Focus();
                        textBox4.Text = "Column: " + go_to_column;
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                else if (words[0] == "save")
                {
                    menuItem7_Click(sender, e);
                }
                else if (words[0] == "save-as")
                {
                    menuItem8_Click(sender, e);
                }
                else if (words[0] == "open")
                {
                    menuItem9_Click(sender, e);
                }
                else if (words[0] == "setcl")
                {
                    try
                    {
                        int go_to_line = Convert.ToInt32(words[1]);
                        current_first = richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1);
                        richTextBox1.SelectionStart = current_first;
                        richTextBox1.Focus();
                        richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1), richTextBox1.GetFirstCharIndexFromLine(go_to_line)-1- richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1));
                        richTextBox1.SelectionColor = System.Drawing.Color.Red;
                        selected_string = "panpa";
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                else if (words[0] == "forward")
                {
                    try
                    {
                        int go_to_line = current_line + richTextBox1.Height / richTextBox1.Font.Height;
                        current_first = richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1);
                        richTextBox1.SelectionStart = current_first;
                        richTextBox1.ScrollToCaret();                      
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                else if (words[0] == "backward")
                {
                    try
                    {
                        int go_to_line = current_line - richTextBox1.Height / richTextBox1.Font.Height;
                        current_first = richTextBox1.GetFirstCharIndexFromLine(go_to_line - 1);
                        richTextBox1.SelectionStart = current_first;
                        richTextBox1.ScrollToCaret();                        
                    }
                    catch
                    {
                        textBox2.Text = "INVALID";
                    }
                }
                richTextBox2.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        //NEW CLICK
        private void menuItem11_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                var confirmResult = MessageBox.Show("Do you want to save changes?", "Text Editor", MessageBoxButtons.YesNoCancel);
                if (confirmResult == DialogResult.Yes)
                {
                    menuItem7_Click(sender, e);
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                }
                else if (confirmResult == DialogResult.No)
                {
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                }
                else
                {
                    return;
                }
            }
            else
            {
                richTextBox1.Clear();
                richTextBox2.Clear();
            }
        }

        //LINE TEXTBOX
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        //COLUMN TEXTBOX
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //NEW BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            menuItem11_Click(sender, e);
        }

        //OPEN BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            menuItem9_Click(sender, e);
        }

        //SAVE BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            menuItem7_Click(sender, e);
        }

        //SAVE AS BUTTON
        private void button4_Click(object sender, EventArgs e)
        {
            menuItem8_Click(sender, e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {     
            textBox4.Text = "Column: " + (richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1);
            current_line = richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine()) + 1;
            current_first = richTextBox1.GetFirstCharIndexOfCurrentLine();
            textBox5.Text = "Line: " + (richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine())+1);
  
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("===COMMANDS===:\n"+
                            "   save\n" +
                            "       saves the document\n"+
                            "       example usage\n"+
                            "           save\n"+
                            "   save-as\n" +
                            "       saves the document with a new name\n" +
                            "       example usage\n" +
                            "           save-as\n" +
                            "   open\n" +
                            "       opens a document\n" +
                            "       example usage\n" +
                            "           open\n" +
                            "   f/find:\n" +
                            "       highlights all of the text that matches with the argument\n" +
                            "       example usage:\n" +
                            "           f memati\n" +
                            "           find memati\n" +
                            "   r/replace:\n" +
                            "       replaces all of the first argument with the second argument\n" +
                            "       example usage:\n" +
                            "           r memati erol\n" +
                            "           replace memati erol\n" +
                            "   #:\n" +
                            "       updates current line as the argument\n" +
                            "       example usage:\n" +
                            "           # 8\n" +
                            "   up\n" +
                            "       decreases current line\n" +
                            "       example usage\n" +
                            "           up 4\n" +
                            "   down\n" +
                            "       increases current line\n" +
                            "       example usage\n" +
                            "           down 4\n" +
                            "   left\n" +
                            "       decreases the current column\n" +
                            "       example usage\n" +
                            "           left 4\n" +
                            "   right\n" +
                            "       increases the current column\n" +
                            "       example usage\n" +
                            "           right 4\n" +
                            "   setcl:\n" +
                            "       updates current line as the argument and changes its font color\n" +
                            "       example usage:\n" +
                            "           setcl 6\n" +
                            "   forward\n" +
                            "       Scrolls one “screenfull” toward the end of the file\n" +
                            "       example usage\n" +
                            "           forward\n" +
                            "   backward\n" +
                            "       Scrolls one “screenfull” toward the top of the file\n" +
                            "       example usage\n" +
                            "           backward\n" +
                            "===SUFFIX===//YOU NEED TO DELETE THE === FIRST:"+
                            "   i\n" +
                            "       Insert lines\n" +
                            "       example usage\n" +
                            "           i 5\n" +
                            "   a\n" +
                            "       Insert after\n" +
                            "       example usage\n" +
                            "           a\n" +
                            "   b\n" +
                            "       Insert before\n" +
                            "       example usage\n" +
                            "           b\n" +
                            "   c\n" +
                            "       Copy next # lines\n" +
                            "       example usage\n" +
                            "           c 5\n" +
                            "   m\n" +
                            "       Move # lines starting at this line\n" +
                            "       example usage\n" +
                            "           m 4\n" +
                            "   \"\n" +
                            "       Duplicate current line\n" +
                            "       example usage\n" +
                            "           \" 5\n" 
                            );
        }
        
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < suffix_lines; i++)
                {

                    if (textBox6.Lines[i] != "===")
                    {
                        string[] words = textBox6.Lines[i].Split(' ');
                        if (words[0] == "i")
                        {
                            List<string> tmp = richTextBox1.Lines.ToList<string>();
                            for (int j = 0; j < Convert.ToInt32(words[1]); j++)
                            {
                                tmp.Insert(textBox6.GetLineFromCharIndex(textBox6.GetFirstCharIndexOfCurrentLine()) + 1, "");
                            }
                            richTextBox1.Lines = tmp.ToArray();
                        }
                        else if (words[0] == "a")
                        {
                            List<string> tmp = richTextBox1.Lines.ToList<string>();
                            tmp.Insert(textBox6.GetLineFromCharIndex(textBox6.GetFirstCharIndexOfCurrentLine()) + 1, "");
                            richTextBox1.Lines = tmp.ToArray();
                        }
                        else if (words[0] == "b")
                        {
                            List<string> tmp = richTextBox1.Lines.ToList<string>();
                            tmp.Insert(textBox6.GetLineFromCharIndex(textBox6.GetFirstCharIndexOfCurrentLine()), "");
                            richTextBox1.Lines = tmp.ToArray();
                        }
                        else if (words[0] == "c")
                        {
                            string str = "";
                            for(int j = 0; j < Convert.ToInt32(words[1]); j++)
                            {
                                str += richTextBox1.Lines[i+j] + "\r\n";
                            }
                            Clipboard.SetText(str);
                        }
                        else if (words[0] == "\"")
                        {
                            List<string> tmp = richTextBox1.Lines.ToList<string>();
                            tmp.Insert(textBox6.GetLineFromCharIndex(textBox6.GetFirstCharIndexOfCurrentLine()) + 1, tmp[textBox6.GetLineFromCharIndex(textBox6.GetFirstCharIndexOfCurrentLine())]);
                            richTextBox1.Lines = tmp.ToArray();
                        }
                        else if (words[0] == "m")
                        {
                            List<string> tmp = richTextBox1.Lines.ToList<string>();
                            for (int j = 0; j < Convert.ToInt32(words[1]); j++)
                            {
                                tmp.Insert(textBox6.GetLineFromCharIndex(textBox6.GetFirstCharIndexOfCurrentLine()) + 1, "");
                            }
                            richTextBox1.Lines = tmp.ToArray();
                        }
                    }
                }
            }
        }
    }
}

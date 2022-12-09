using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace banbatch
{
    public partial class Form1 : Form
    {
        public Scenario scenario;
        public bool assign_setuped = false;
        public Label target; 

        public Form1()
        {
            InitializeComponent();
            scenario = new Scenario(Controls);
            target = null;
        }

        public Control GetComponent(string target)
        {
            foreach (Control component in this.Controls)
                if (component.Name == target)
                    return component;

            return null;
        }

        public bool ComponentValueInputed()
        {
            if (numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && numericUpDown4.Value != 0)
            {
                if (numericUpDown3.Enabled)
                {
                    if (numericUpDown3.Value != 0) return true;
                    return false;
                }
                return true;
            }
            return false;
        }

        private void LabelClick(object sender, EventArgs e)
        {
            if (target == null)
            {
                target = (Label)sender;
                target.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                Label swapTarget = (Label)sender;
                string number = swapTarget.Text;
                swapTarget.Text = target.Text;
                target.Text = number;
                target.BorderStyle = BorderStyle.FixedSingle;
                target = null;
            }
        }

        private void Control(object sender, EventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.CheckBox")
            {
                numericUpDown3.Enabled = !numericUpDown3.Enabled;
                button1.Enabled = ComponentValueInputed();
            }
            else
            {
                if (ComponentValueInputed()) button1.Enabled = true;
                else if (((NumericUpDown)sender).Value == 0)
                {
                    button1.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!assign_setuped)
            {
                Controls.Remove(label1);
                int numbers = 0;
                int assign_up_index = (numericUpDown3.Enabled) ? 1 : -1;
                decimal assign = numericUpDown3.Value;

                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    for (int j = 0; j < numericUpDown2.Value; j++)
                    {
                        if (numbers >= numericUpDown4.Value) break;
                        numbers++;
                        Label new_c = new Label();
                        new_c.Text = numbers.ToString();
                        new_c.AutoSize = false;
                        new_c.Size = new Size(100, 40);
                        new_c.TextAlign = ContentAlignment.MiddleCenter;
                        new_c.BorderStyle = BorderStyle.FixedSingle;
                        new_c.Click += this.LabelClick;

                        scenario.SetPosition(new_c, i, j, (i == assign_up_index) ? true : false);
                        scenario.AddComponent(new_c, i, j);
                    }
                    if (i == assign_up_index)
                    {
                        assign--;
                        if (assign <= 0 && numericUpDown3.Enabled)
                        {
                            assign = numericUpDown3.Value; 
                            assign_up_index += 2;
                        }
                        else assign_up_index++;
                    }
                }
                assign_setuped = true; 
            }
            SetArrangement();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetArrangement()
        {
            int[] arr = new int[(int)numericUpDown4.Value];
            int tortals = 0;
            Random random = new Random();
            for (int i = 0; i < numericUpDown4.Value; i++) arr[i] = (i + 1);
            arr = arr.OrderBy(x => random.Next()).ToArray();

            for (int section = 0; section < numericUpDown1.Value; section++)
            {
                for (int assign = 0; assign < numericUpDown2.Value; assign++)
                {
                    if (tortals >= numericUpDown4.Value) break; 
                    scenario.SetName(section, assign, arr[tortals].ToString());
                    tortals++;
                }
            }
        }
    }
}

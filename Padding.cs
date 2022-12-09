using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace banbatch
{
    public class Scenario
    {
        public Label[][] banbatchs = new Label[10][];
        public int Padding = 60;
        public int Interval = 120;
        public Control.ControlCollection collection;

        public Scenario(Control.ControlCollection c)
        {
            for (int i = 0; i < 10; i++)
            {
                if (banbatchs[i] == null) banbatchs[i] = new Label[10];
            }
            collection = c;
        }

        public bool SetPosition(Control control, int i, int j, bool assign)
        {
            if (i <= 0)
            {
                control.Location = new Point((i + 1) * Padding, (j + 1) * Padding);
            }
            else
            {
                Label component = banbatchs[i - 1][j];
                control.Location = new Point(
                    component.Location.X + ((assign) ? component.Size.Width - 1 : Interval), component.Location.Y);
            }
            return true; 
        }
        
        public void AddComponent(Label component, int i, int j)
        {
            banbatchs[i][j] = component;
            collection.Add(component);
        }

        public void SetName(int i, int j, string name)
        {
            if (banbatchs[i][j] != null) banbatchs[i][j].Text = name;
        }

    }
}

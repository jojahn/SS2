using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ss2_hacking
{
    public partial class ss2form : Form
    {
        private EventBus eventBus = EventBus.getEventBus();

        public ss2form()
        {
            InitializeComponent();
        }

        private void form_load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(0,0));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button0_1_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(0, 1));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button0_2_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(0, 2));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button1_0_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(1, 0));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button1_1_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(1, 1));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button1_2_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(1, 2));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button2_0_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(2, 0));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button2_1_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(2, 1));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }

        private void button2_2_Click(object sender, EventArgs e)
        {
            eventBus.publish(new NodeClickEvent(2, 2));
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Cyan;
        }
        
    }
}

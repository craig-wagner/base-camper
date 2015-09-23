using KmlUtilities;
using System;
using System.Windows.Forms;

namespace BaseCamper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            var parser = new KmlParser();
            var newKmlFile = parser.FixBaseCampKml(txtKmlFile.Text);
            MessageBox.Show("New KML file created. File name is " + newKmlFile + ".", "New File Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtKmlFile.Text = openFileDialog.FileName;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HurDatForms
{
    public partial class Form1 : Form
    {
        List<Hur> hurDat;

        public Form1()
        {
            InitializeComponent();

            gridHur.ReadOnly = true;
            gridHurDetail.ReadOnly = true;

            List<string> states = new List<string>();
            states.Add(string.Empty);
            states.AddRange(new StateLookup().GetStates());
            stateDropdown.DataSource = states;
        }

        //Update hurricane detail if selected hurricane changes
        private void gridHur_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Hur hur = (Hur) gridHur.Rows[e.RowIndex].DataBoundItem;
            gridHurDetail.DataSource = hur.Details;
        }

        //Filter list of models using LINQ
        private void filter_Click(object sender, EventArgs e)
        {
            if (hurDat == null)
            {
                MessageBox.Show("Load data first.");
                return;
            }

            List<Hur> filteredHur = hurDat;
            string state = (string)stateDropdown.SelectedValue;

            //If both state and recordId are set, then filter any hurricane detail on state AND recordId
            if (!string.IsNullOrEmpty(state) && recordText.Text.Length > 0)
            { 
                filteredHur = filteredHur.Where(h => h.Details.Any(d => d.State == state && d.RecordId == recordText.Text)).ToList();
                foreach (Hur hur in filteredHur)
                {
                    hur.Details = hur.Details.Where(d => d.State == state && d.RecordId == recordText.Text).ToList();
                }
            }
            else if (recordText.Text.Length > 0)
            { 
                filteredHur = filteredHur.Where(h => h.Details.Any(d => d.RecordId == recordText.Text)).ToList();
                foreach (Hur hur in filteredHur)
                {
                    hur.Details = hur.Details.Where(d => d.RecordId == recordText.Text).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(state))
            { 
                filteredHur = filteredHur.Where(h => h.Details.Any(d => d.State == state)).ToList();
                foreach (Hur hur in filteredHur)
                {
                    hur.Details = hur.Details.Where(d => d.State == state).ToList();
                }
            }

            int minYear;
            if (minYearText.Text.Length > 0)
            {
                if (!int.TryParse(minYearText.Text, out minYear))
                    MessageBox.Show("Invalid Min Year.");
                else
                {
                    filteredHur = filteredHur.Where(h => h.Year >= minYear).ToList();
                }
            }

            this.numText.Text = filteredHur.Count.ToString();
            gridHur.DataSource = filteredHur;
        }

        //Parse data and load into list of models
        private void loadData_Click(object sender, EventArgs e)
        {
            HurDatParser p = new HurDatParser();
            hurDat = p.ParseHurDat();

            if (hurDat == null)
            { 
                MessageBox.Show("Error while parsing. Check console for more info.");
                return;
            }

            gridHur.DataSource = hurDat;
            this.numText.Text = hurDat.Count.ToString();
        }

        //Print out the hurricane name, date and max wind as per requirements
        private void report_Click(object sender, EventArgs e)
        {
            List<Hur> filteredHur = (List<Hur>) gridHur.DataSource;

            if (filteredHur == null)
            {
                MessageBox.Show("Load data first.");
                return;
            }

            StringBuilder csv = new StringBuilder();
            foreach (Hur h in filteredHur)
            {
                foreach (HurDetail d in h.Details)
                {
                    DateTime date = new DateTime(d.Year, d.Month, d.Day);
                    string strDate = date.ToString("MM/dd/yyyy");
                    string line = string.Format("{0}, {1}, {2}", h.Name, strDate, d.MaxWind);
                    csv.AppendLine(line);
                }
            }

            string fileName = "hurdatareport" + DateTime.Now.ToString("MMddyy_HHmmss") + ".csv";
            File.WriteAllText(fileName, csv.ToString());
            Process.Start(fileName);
        }
    }
}

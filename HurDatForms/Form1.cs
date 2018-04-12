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
        private List<Hur> hurList;
        private string hurDatFile = @"Files/hurdat2-1851-2016-041117.txt";

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
            try
            { 
                Hur hur = (Hur) gridHur.Rows[e.RowIndex].DataBoundItem;
                gridHurDetail.DataSource = hur.Details;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Filter list of models using LINQ
        private void filter_Click(object sender, EventArgs e)
        {
            try
            {
                if (hurList == null)
                {
                    MessageBox.Show("Load data first.");
                    return;
                }

                if (recordText.Text.Length > 1)
                { 
                    MessageBox.Show("Record Identifier must be 1 character.");
                    return;
                }

                List<Hur> filteredHur = hurList;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Parse data and load into list of models
        private void loadData_Click(object sender, EventArgs e)
        {
            try
            { 
                HurDatParser p = new HurDatParser(hurDatFile);
                string errors;
                List<Hur> results = p.ParseHurDat(out errors);

                if (!string.IsNullOrEmpty(errors))
                { 
                    MessageBox.Show(errors);
                    return;
                }

                if (results == null)
                { 
                    MessageBox.Show("ParseHurDat returned null");
                    return;
                }

                hurList = results;
                gridHur.DataSource = hurList;
                this.numText.Text = hurList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Print out the hurricane name, date and max wind as per requirements
        private void report_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

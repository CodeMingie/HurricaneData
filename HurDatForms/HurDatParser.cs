using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurDatForms
{
    public class HurDatParser
    {
        string hurdatFile;

        public HurDatParser(string file)
        {
            hurdatFile = file;
        }

        //Validate data file, outputing any errors to console. Return true if no error.
        private bool ValidateHurDat(out string errors)
        {
            StringBuilder errorBuilder = new StringBuilder();
            
            if (!File.Exists(hurdatFile))
                errorBuilder.AppendLine("File does not exist.");

            string line;
            int lineNum = 0;
            
            System.IO.StreamReader file = new System.IO.StreamReader(hurdatFile);
            int expectedEntries = -1;
            int currentEntries = 0;

            while ((line = file.ReadLine()) != null)
            {
                lineNum++;
                string[] blocks = line.Split(',');

                //Header line
                if (blocks.Length == 4)
                {
                    if (blocks[0].Length != 8)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Not 8 characters");

                    string basin = blocks[0].Substring(0, 2);
                    string strCycloneNum = blocks[0].Substring(2, 2);
                    string strYear = blocks[0].Substring(4, 4);

                    int cycloneNum;
                    int year;

                    if (!int.TryParse(strCycloneNum, out cycloneNum))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse cyclone number");

                    if (!int.TryParse(strYear, out year))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse year");

                    string hurName = blocks[1];

                    string strTrackEntries = blocks[2];
                    int trackEntries;

                    if (!int.TryParse(strTrackEntries, out trackEntries))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse track entries");

                    if (expectedEntries == -1)
                    {
                        expectedEntries = trackEntries;
                        continue;
                    }

                    if (expectedEntries != currentEntries)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Different from expected Entries");

                    expectedEntries = trackEntries;
                    currentEntries = 0;
                }
                //Data line
                else if (blocks.Length == 21)
                {
                    currentEntries++;
                    if (blocks[0].Length != 8)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Not 8 characters");

                    string strYear = blocks[0].Substring(0, 4);
                    string strMonth = blocks[0].Substring(4, 2);
                    string strDay = blocks[0].Substring(6, 2);

                    int year;
                    int month;
                    int day;

                    if (!int.TryParse(strYear, out year))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse year");

                    if (!int.TryParse(strMonth, out month))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse month");

                    if (!int.TryParse(strDay, out day))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse day");

                    if (blocks[1].Trim().Length != 4)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Data line time not 4 characters");

                    string strHour = blocks[0].Substring(0, 2);
                    string strMinutes = blocks[0].Substring(2, 2);

                    int hour;
                    int minutes;

                    if (!int.TryParse(strHour, out hour))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse hour");

                    if (!int.TryParse(strMinutes, out minutes))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse minute");

                    string recordId = blocks[2].Trim();

                    if (recordId.Length > 1)
                        errorBuilder.AppendLine(lineNum.ToString() + ", RecordId more than length 1");

                    string status = blocks[3].Trim();

                    if (status.Length > 2)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Status more than length 2");


                    if (blocks[4].Trim().Length < 4)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Invalid latitude length");

                    string strLat = blocks[4].Substring(0, blocks[4].Length - 1).Trim();
                    char latHemi = blocks[4][blocks[4].Length - 1];

                    if (latHemi != 'S' && latHemi != 'N')
                        errorBuilder.AppendLine(lineNum.ToString() + ", Invalid latitude hemisphere");

                    decimal lat;

                    if (!decimal.TryParse(strLat, out lat))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse latitude");

                    if (lat > 90)
                        errorBuilder.Append(lineNum.ToString() + ", Latitude exceeds 90.");

                    if (blocks[5].Trim().Length < 4)
                        errorBuilder.AppendLine(lineNum.ToString() + ", Invalid longitude length");

                    string strLng = blocks[5].Substring(0, blocks[5].Length - 1).Trim();
                    char lngHemi = blocks[5][blocks[5].Length - 1];

                    if (lngHemi != 'W' && lngHemi != 'E')
                        errorBuilder.AppendLine(lineNum.ToString() + ", Invalid longitude hemisphere");

                    decimal lng;

                    if (!decimal.TryParse(strLng, out lng))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse longitude");

                    //if (lng > 180)
                        //errorBuilder.AppendLine(lineNum.ToString() + ", Longitude exceeds 180");

                    string strMaxWind = blocks[6].Trim();
                    int maxWind;
                    if (!int.TryParse(strMaxWind, out maxWind))
                        errorBuilder.AppendLine(lineNum.ToString() + ", Cannot parse max wind");
                }
                else
                {
                    errorBuilder.AppendLine(lineNum.ToString() + ", Invalid number of columns");
                }
            }

            if (expectedEntries != currentEntries)
                errorBuilder.AppendLine(lineNum.ToString() + ", Different from expected Entries");

            file.Close();

            //if (errorBuilder.Length > 0)
            //    Console.Out.WriteLine(errorBuilder.ToString());
            errors = errorBuilder.ToString();

            return errorBuilder.Length == 0;
        }

        public List<Hur> ParseHurDat(out string errors)
        {
            if (!ValidateHurDat(out errors))
                return null;

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(hurdatFile);
            List<Hur> hurList = new List<Hur>();
            int hurId = 0;
            Hur currentHur = new Hur();
            StateLookup sl = new StateLookup();

            while ((line = file.ReadLine()) != null)
            {
                string[] blocks = line.Split(',');
                
                //Header line
                if (blocks.Length == 4)
                {
                    string basin = blocks[0].Substring(0, 2);
                    string strCycloneNum = blocks[0].Substring(2, 2);
                    string strYear = blocks[0].Substring(4, 4);

                    int cycloneNum;
                    int year;

                    int.TryParse(strCycloneNum, out cycloneNum);
                    int.TryParse(strYear, out year);
                    string hurName = blocks[1].Trim();
                    string strTrackEntries = blocks[2];
                    int trackEntries;
                    int.TryParse(strTrackEntries, out trackEntries);

                    hurId++;

                    Hur hur = new Hur();
                    hur.Id = hurId;
                    hur.Basin = basin;
                    hur.CycloneNum = cycloneNum;
                    hur.Year = year;
                    hur.Name = hurName;
                    hur.NumEntries = trackEntries;
                    hur.Details = new List<HurDetail>();
                    hurList.Add(hur);
                    currentHur = hur;
                }
                //Data line
                else if (blocks.Length == 21)
                {
                    string strYear = blocks[0].Substring(0, 4);
                    string strMonth = blocks[0].Substring(4, 2);
                    string strDay = blocks[0].Substring(6, 2);

                    int year;
                    int month;
                    int day;

                    int.TryParse(strYear, out year);
                    int.TryParse(strMonth, out month);
                    int.TryParse(strDay, out day);

                    string strHour = blocks[0].Substring(0, 2);
                    string strMinutes = blocks[0].Substring(2, 2);

                    int hour;
                    int minutes;

                    int.TryParse(strHour, out hour);
                    int.TryParse(strMinutes, out minutes);

                    string recordId = blocks[2].Trim();
                    string status = blocks[3].Trim();

                    string strLat = blocks[4].Substring(0, blocks[4].Length - 1).Trim();
                    char latHemi = blocks[4][blocks[4].Length - 1];

                    decimal lat;
                    decimal.TryParse(strLat, out lat);

                    if (latHemi == 'S')
                        lat = lat * -1;

                    string strLng = blocks[5].Substring(0, blocks[5].Length - 1).Trim();
                    char lngHemi = blocks[5][blocks[5].Length - 1];

                    decimal lng;
                    decimal.TryParse(strLng, out lng);

                    if (lngHemi == 'W')
                        lng = lng * -1;

                    string strMaxWind = blocks[6].Trim();
                    int maxWind;
                    int.TryParse(strMaxWind, out maxWind);

                    HurDetail detail = new HurDetail();
                    detail.HurId = currentHur.Id;
                    detail.Year = year;
                    detail.Month = month;
                    detail.Day = day;
                    detail.Hour = hour;
                    detail.Minute = minutes;
                    detail.RecordId = recordId;
                    detail.Status = status;
                    detail.Latitude = lat;
                    detail.Longtitude = lng;
                    detail.MaxWind = maxWind;
                    detail.State = sl.GetStateFromCor(lng, lat);

                    currentHur.Details.Add(detail);
                }
            }

            file.Close();

            return hurList;
        }
    }
}

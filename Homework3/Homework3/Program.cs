/*
    Waveguide C# Course
    Homework 3
    Steve Ruff
    2/12/2016
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Xml;

namespace Homework3
{
    class Program
    {
        string inputDir = @"C:\Users\sruff\Source\Repos\sruff-homework\Homework3\Homework3\data\";
        string outputDir = @"C:\Users\sruff\Source\Repos\sruff-homework\Homework3\Homework3\data\output\";

        struct bankData
        {
            public string bankName;
            public string acctHolder;
            public string acctNum;
            public string fedRefNum;
            public bool deposit;
            public decimal amount;
            public DateTime transDate;
        };

        static DataTable bankDataTable;

        static void Main(string[] args)
        {
            Program P = new Program();
            P.Initialize();
            P.ReadFiles();
            P.CreateExcelFile();
            // P.MoveFiles();
        }

        void Initialize()
        {
            bankDataTable = new DataTable();
            bankDataTable.Columns.Add("bankName", typeof(string));
            bankDataTable.Columns.Add("acctHolder", typeof(string));
            bankDataTable.Columns.Add("acctNum", typeof(string));
            bankDataTable.Columns.Add("fedRefNum", typeof(string));
            bankDataTable.Columns.Add("amount", typeof(decimal));
            bankDataTable.Columns.Add("transdate", typeof(DateTime));
        }

        void ReadFiles()
        {
            if (Directory.Exists(inputDir))
            {
                // For each of the files in the input directory, pass the file to the correct method depending upon the first two characters of its filename
                foreach (var v in Directory.GetFiles(inputDir))
                {
                    if (Path.GetFileName(v).Substring(0,2).Equals("Wa")) {
                        processWachoviaFile(v);
                    }
                    else if (Path.GetFileName(v).Substring(0,2).Equals("Su"))
                    {
                        processSuntrustFile(v);
                    }
                }
            }
        }

        void CreateExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Workbooks.Add();
            excel.Visible = true;
            excel.DisplayAlerts = false;

            excel.Range["A1"].Value = bankDataTable.Columns[0].ColumnName;
            excel.Range["B1"].Value = bankDataTable.Columns[1].ColumnName;
            excel.Range["C1"].Value = bankDataTable.Columns[2].ColumnName;
            excel.Range["D1"].Value = bankDataTable.Columns[3].ColumnName;
            excel.Range["E1"].Value = bankDataTable.Columns[4].ColumnName;
            excel.Range["F1"].Value = bankDataTable.Columns[5].ColumnName;
            // set date and time format for column F
            excel.Range["F1", "F99"].NumberFormat = "M/D/YYYY H:MM AM/PM";
            // set width for column F
            excel.Range["F1"].EntireColumn.ColumnWidth = 17;

            // shamelessly stolen from Terri
            int j = 2;
            foreach (DataRow row in bankDataTable.Rows)
            {
                int i = 65;
                foreach( var item in row.ItemArray)
                {
                    char c1 = (char)i++;
                    string cell = c1 + j.ToString();
                    excel.Range[cell].Value = item.ToString();
                }
                j++;
            }
        }

        void MoveFiles()
        {
            try
            {
                // if output directory doesn't exist, create it
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
                // move each of the processed input files to the output directory
                foreach (var v in Directory.GetFiles(inputDir))
                {
                    File.Move(v, outputDir + Path.GetFileName(v));
                }
            }
            catch (Exception ex1)
            {
                Console.WriteLine("Error moving files: " + ex1.Message);
            }
        }

        // also stolen from Terri
        void processWachoviaFile(string fileName)
        {
            using (StreamReader strm_reader = new StreamReader(fileName))
            {
                string line;
                while ((line = strm_reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    bankData bd = new bankData();
                    bd.bankName = values[0].Trim();
                    bd.acctHolder = values[1].Trim();
                    bd.acctNum = values[2].Trim();
                    bd.fedRefNum = values[3].Trim();
                    string tempValue4 = values[4].Trim();
                    if (tempValue4.Equals("+"))  // be carefule with comparison a string != a character.  Use double quotes
                    {
                        bd.deposit = true;
                        bd.amount = Convert.ToDecimal(values[6]);
                    }
                    else
                    {
                        bd.deposit = false;
                        bd.amount = (-1.0m) * Convert.ToDecimal(values[6]);
                    }
                    try
                    {
                        bd.transDate = System.DateTime.Parse(values[7]);
                    }
                    catch (Exception ex)
                    {
                        bd.transDate = DateTime.Today;
                    }

                    // add data to table
                    bankDataTable.Rows.Add(bd.bankName, bd.acctHolder, bd.acctNum, bd.fedRefNum, bd.amount, bd.transDate);
                }
            }
        }

        // Terri, again.
        void processSuntrustFile(string fileName)
        {
            bankData bd = new bankData();

            try
            {
                using (XmlReader myReader = XmlReader.Create(fileName))
                {
                    while (myReader.Read())
                    {
                        if (myReader.IsStartElement())  // start tag
                        {
                            switch (myReader.Name)
                            {
                                case "Data":  // don't really need to detect this - nothing to do
                                    break;
                                case "transaction":  // start of a dataset
                                    bd = new bankData();  // create new instance
                                    bd.bankName = "Suntrust";
                                    break;
                                case "AcctHolder":
                                    if (!myReader.IsEmptyElement)
                                    {
                                        if (myReader.Read()) bd.acctHolder = myReader.Value;
                                    }
                                    break;
                                case "AcctNum":
                                    if (!myReader.IsEmptyElement)
                                    {
                                        if (myReader.Read()) bd.acctNum = myReader.Value;
                                    }
                                    break;
                                case "FedRefNum":
                                    if (!myReader.IsEmptyElement)
                                    {
                                        if (myReader.Read()) bd.fedRefNum = myReader.Value;
                                    }
                                    break;
                                case "DorW":
                                    if (!myReader.IsEmptyElement)
                                    {
                                        if (myReader.Read())
                                        {
                                            string temp1 = myReader.Value;
                                            bd.deposit = (temp1.Equals("D")) ? true : false;  //"?" operator - inline If statement
                                        }
                                    }
                                    break;
                                case "Amount":
                                    if (!myReader.IsEmptyElement)
                                    {
                                        try
                                        {
                                            if (myReader.Read()) bd.amount = Convert.ToDecimal(myReader.Value);
                                        }
                                        catch { bd.amount = 0.0m; }
                                    }
                                    break;
                                case "DateTime":
                                    if (!myReader.IsEmptyElement)
                                    {
                                        try
                                        {
                                            if (myReader.Read()) bd.transDate = System.DateTime.Parse(myReader.Value);
                                        }
                                        catch { bd.transDate = System.DateTime.Today; }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (myReader.NodeType == XmlNodeType.EndElement)
                        {
                            switch (myReader.Name)
                            {
                                case "transaction":
                                    // add data to table
                                    if (!bd.deposit)
                                    {
                                        bd.amount = (-1.0m) * bd.amount;
                                    }
                                    bankDataTable.Rows.Add(bd.bankName, bd.acctHolder, bd.acctNum, bd.fedRefNum, bd.amount, bd.transDate);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with Sntrust files: " + ex.ToString());
            }
        }
    }
}

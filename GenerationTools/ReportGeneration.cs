using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Nikitin.GenerationTools.Models;
using Nikitin.Tools;
using Nikitin.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Nikitin.GenerationTools
{
    internal class ReportGeneration
    {
        public static async System.Threading.Tasks.Task DoADriverReportAsync(Driver Model)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                await System.Threading.Tasks.Task.Run(() => GenerateDriverReport(Model, sv.FileName));
                CustomMessageBox.Show("Генерация отчёта о Водителе успешно завершена!");
            }

        }

        private static void GenerateDriverReport(Driver Model, string NewPath)
        {
            try
            {
                byte[] FileBytes = Properties.Resources.DriverReport;
                string TempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(TempFilePath, FileBytes);

                Word.Application App = new Word.Application();
                App.Visible = false;
                Word.Document document = App.Documents.Open(TempFilePath);

                ChangeWordsDriver(Model, document);
                GenerateTableDriverReport(Model, document);

                document.SaveAs2(FileName: NewPath);
                document.Close();
                App.Quit();
            }
            catch (System.Exception ex)
            {
                CustomMessageBox.Show("Ошибка генерации отчёта");
            }
        }

        private static void GenerateTableDriverReport(Driver Model, Document document)
        {
            Table table = document.Tables[1];
            int CountOfOrders = Model.AllRequestCount;
            for (int i = 1; i <= CountOfOrders; i++)
                table.Rows.Add();
            int index = 0;
            foreach (Row row in table.Rows)
            {
                if (row.Index == 1)
                    continue;
                foreach (Cell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 1)
                    {
                        string address = DbUtils.GetAddress(Convert.ToDouble(Model.requests[index].FirstPointLng), Convert.ToDouble(Model.requests[index].FirstPointLat)).Result;
                        cell.Range.Text = address[0..address.IndexOf(',')];
                    }
                    else if (cell.ColumnIndex == 2)
                        cell.Range.Text = $"{Model.requests[index].RequestDate:dd.MM.yyyy}";
                    else if (cell.ColumnIndex == 3)
                        cell.Range.Text = Model.requests[index].IdPurityStatusNavigation.PurityStatusName;
                    else if (cell.ColumnIndex == 4)
                        cell.Range.Text = $"{Model.requests[index].IdCarNavigation.IdCarModelNavigation.IdBrandNavigation.BrandName} {Model.requests[index].IdCarNavigation.IdCarModelNavigation.ModelName}";
                    else if (cell.ColumnIndex == 5)
                        cell.Range.Text = $"{Model.requests[index].IdCarNavigation.StateNumber}";
                }
                index++;
            }
        }

        private static void ChangeWordsDriver(Driver Model, Document document)
        {
            ToolsForGeneration.ReplaceWordWithUnderline("{DateOfDrawingUp}", $"{DateTime.Now:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWord("{CurrentDate}", $"{DateTime.Now:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWordWithBold("{Driver}", Model.DriverFIO, document);
            ToolsForGeneration.ReplaceWord("{RequestCount}", Model.AllRequestCount.ToString(), document);
            ToolsForGeneration.ReplaceWord("{BlackIceCount}", $"{Model.BlackIceCount}", document);
            ToolsForGeneration.ReplaceWord("{StartDate}", $"{Model.StartDateTime:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWord("{EndDate}", $"{Model.EndDateTime:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWordWithBold("{ShowFallCount}", $"{Model.SnowFallCount}", document);
        }

        public static async System.Threading.Tasks.Task DoARequestReportAsync(RequestsModel Model)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Microsoft Word Document (*.docx)|*.docx";
            if (sv.ShowDialog() == true)
            {
                await System.Threading.Tasks.Task.Run(() => GenerateRequestReport(Model, sv.FileName));
                CustomMessageBox.Show("Генерация отчёта о заказах успешно завершена!");
            }

        }

        private static void GenerateRequestReport(RequestsModel Model, string NewPath)
        {
            try
            {
                byte[] FileBytes = Properties.Resources.RequestsReport;
                string TempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(TempFilePath, FileBytes);

                Word.Application App = new Word.Application();
                App.Visible = false;
                Word.Document document = App.Documents.Open(TempFilePath);

                ChangeWordsRequest(Model, document);
                GenerateTableRequestReport(Model, document);

                document.SaveAs2(FileName: NewPath);
                document.Close();
                App.Quit();
            }
            catch (System.Exception ex)
            {
                CustomMessageBox.Show("Ошибка генерации отчёта");
            }
        }

        private static void GenerateTableRequestReport(RequestsModel Model, Document document)
        {
            Table table = document.Tables[1];
            int CountOfOrders = Model.AllRequestCount;
            for (int i = 1; i <= CountOfOrders; i++)
                table.Rows.Add();
            int index = 0;
            foreach (Row row in table.Rows)
            {
                if (row.Index == 1)
                    continue;
                foreach (Cell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 1)
                    {
                        string address = DbUtils.GetAddress(Convert.ToDouble(Model.requests[index].FirstPointLng), Convert.ToDouble(Model.requests[index].FirstPointLat)).Result;
                        cell.Range.Text = address[0..address.IndexOf(',')];
                    }
                    else if (cell.ColumnIndex == 2)
                        cell.Range.Text = $"{Model.requests[index].RequestDate:dd.MM.yyyy}";
                    else if (cell.ColumnIndex == 3)
                        cell.Range.Text = Model.requests[index].IdPurityStatusNavigation.PurityStatusName;
                    else if (cell.ColumnIndex == 4)
                        cell.Range.Text = $"{Model.requests[index].IdCarNavigation.IdCarModelNavigation.IdBrandNavigation.BrandName} " +
                                          $"{Model.requests[index].IdCarNavigation.IdCarModelNavigation.ModelName}";
                    else if (cell.ColumnIndex == 5)
                        cell.Range.Text = $"{Model.requests[index].IdCarNavigation.StateNumber}";
                    else if (cell.ColumnIndex == 6)
                        cell.Range.Text = $"{Model.requests[index].IdCarNavigation.IdDriverNavigation.Surname} " +
                                          $"{Model.requests[index].IdCarNavigation.IdDriverNavigation.Name} " +
                                          $"{Model.requests[index].IdCarNavigation.IdDriverNavigation.Patronymic}";
                }
                index++;
            }
        }

        private static void ChangeWordsRequest(RequestsModel Model, Document document)
        {
            ToolsForGeneration.ReplaceWordWithUnderline("{DateOfDrawingUp}", $"{DateTime.Now:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWord("{CurrentDate}", $"{DateTime.Now:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWord("{RequestCount}", Model.AllRequestCount.ToString(), document);
            ToolsForGeneration.ReplaceWord("{StartDate}", $"{Model.StartDateTime:dd.MM.yyyy}", document);
            ToolsForGeneration.ReplaceWord("{EndDate}", $"{Model.EndDateTime:dd.MM.yyyy}", document);
        }
    }
}

using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Models;
using ChemicalLaboratory.Models.Equipment;
using ChemicalLaboratory.Models.Experiment;
using ChemicalLaboratory.Models.Reagent;
using ChemicalLaboratory.Models.ViewModels;
using ChemicalLaboratory.Pages.Home;
using EFCore.Services;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.Xml.Linq;

namespace ChemicalLaboratory.Pages.Add
{

    public class UpdateExperimentModel : PageModel
    {
        private readonly IExperimentService _experimentService;
        public UpdateExperimentModel(IExperimentService experimentService) { _experimentService = experimentService; }

        [BindProperty(SupportsGet = true)]
        public int ExperimentID { get; set; }

        [BindProperty(SupportsGet = true)]
        public ExperimentViewModel Experiment { get; set; } = null!;
        [BindProperty(SupportsGet = true)]
        public List<EquipmentDataModel>? Equipment { get; set; } 
        [BindProperty(SupportsGet = true)]
        public List<ReagentExperiment>? Reagent { get; set; } 
        public List<SelectListItem>? EquipmentList { get; set; }
        [BindProperty(SupportsGet = true)]
        public int EquipmentId { get; set; }
        public List<SelectListItem>? ReagentList { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ReagentId { get; set; }
        [BindProperty]
        public int reportId { get; set; } = 0;

        public async Task<IActionResult> OnGet(int id)
        {
            ExperimentID = JsonRequest.Instance(id).id;

            if (Request.Query.TryGetValue("DeleteEquipment", out var deleteEquipmentValue))
            {
                if (int.TryParse(deleteEquipmentValue, out var equipmentId))
                {
                    await _experimentService.DeleteEquipmentAsync(equipmentId);
                    //SQLCommand.DeleteRecord("DELETE FROM ExperimentEquipment WHERE idExpEq = @Id", equipmentId);
                }
            }

            if (Request.Query.TryGetValue("DeleteReagent", out var deleteReagentValue))
            {
                if (int.TryParse(deleteReagentValue, out var reagentId))
                {
                    await _experimentService.DeleteReagentAsync(reagentId);
                    //SQLCommand.DeleteRecord("DELETE FROM ReagentExperiment WHERE idReagExpetiment = @Id", reagentId);
                }
            }

            LoadData(ExperimentID);
            EquipmentList = SQLCommand.Get2FieldOption("select idEquipment, Name from EquipmentSchema.Equipment");
            ReagentList = SQLCommand.Get2FieldOption("select idReagent, Name from ReagentSchema.Reagent");

            if (Request.Query.TryGetValue("action", out var action))
            {
                switch (action)
                {
                    case "equipment":
                        {
                            Equipment.Add(new EquipmentDataModel());
                            break;
                        }

                    case "reagent":
                        {
                            Reagent.Add(new ReagentExperiment());
                            break;
                        }

                    case "save":
                        {
                            break;
                        }
                }
            }

            return Page();
        }


        public void LoadData(int Id)
        {
            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                connection.Open();

                string select = @"select * from ExperimentSchema.Experiment
										where idExperiment = @id";

                SqlCommand command = new SqlCommand(select, connection);
                command.Parameters.AddWithValue("@id", Id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // в теории if не нужен т.к. при авторизации пользователь был обнаружен
                    if (reader.Read())
                    {
                        // Эксперимент
                        Experiment.idExperiment = reader["idExperiment"] != DBNull.Value ? Convert.ToInt32(reader["idExperiment"]) : 0;
                        Experiment.Name = reader["Name"] != DBNull.Value ? Convert.ToString(reader["Name"]) : "";
                        Experiment.Description = reader["Description"] != DBNull.Value ? Convert.ToString(reader["Description"]) : "";
                        Experiment.StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : DateTime.MinValue;
                        Experiment.EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : DateTime.MinValue;
                        Experiment.Result = reader["Result"] != DBNull.Value ? Convert.ToString(reader["Result"]) : "";
                        Experiment.Status = reader["status"] != DBNull.Value ? Convert.ToString(reader["status"]) : "";
                    }
                }


                string selectExperimentReagent = "select \r\nrsr.idReagent, re.UseCount, re.mass as UseMass, rsr.Dansity, rsr.ChemicalFormula, rsr.Name as ReagentName, re.idReagExpetiment\r\nfrom ExperimentSchema.Experiment exse \r\nleft join ReagentExperiment re on re.idExperiment = exse.idExperiment\r\nleft join ReagentSchema.Reagent rsr on re.idReagent = rsr.idReagent\r\nwhere exse.idExperiment = @idExperiment";
                command.Parameters.AddWithValue("@idExperiment", Id);

                command.CommandText = selectExperimentReagent;

                using (SqlDataReader experimentReader = command.ExecuteReader())
                {
                    // Обработка второго результата (например, список экспериментов пользователя)
                    while (experimentReader.Read())
                    {
                        var reagent = new ReagentExperiment
                        {
                            UseCount = experimentReader["UseCount"] != DBNull.Value ? Convert.ToInt32(experimentReader["UseCount"]) : 0,
                            Mass = experimentReader["UseMass"] != DBNull.Value ? Convert.ToDouble(experimentReader["UseMass"]) : 0,
                            idReagentExperiment = experimentReader["idReagExpetiment"] != DBNull.Value ? Convert.ToInt32(experimentReader["idReagExpetiment"]) : 0,

                            idReagentDataModel = new ReagentManufacturer
                            {
                                //PurityDegree = experimentReader["PurityDegree"] != DBNull.Value ? Convert.ToDecimal(experimentReader["PurityDegree"]) : 0,

                                Reagent = new ReagentDataModel
                                {
                                    idReagent = experimentReader["idReagent"] != DBNull.Value ? Convert.ToInt32(experimentReader["idReagent"]) : 0,
                                    Name = experimentReader["ReagentName"] != DBNull.Value ? Convert.ToString(experimentReader["ReagentName"]) : "",
                                    ChemicalFormula = experimentReader["ChemicalFormula"] != DBNull.Value ? Convert.ToString(experimentReader["ChemicalFormula"]) : "",
                                    Dansity = experimentReader["Dansity"] != DBNull.Value ? Convert.ToDecimal(experimentReader["Dansity"]) : 0,
                                }
                            }
                        };

                        // Добавить в список экспериментов, если нужно
                        Reagent.Add(reagent);
                    }
                }

                string selectExperimentEquipment = "select \r\n\tee.idExpEq, ese.Name, ese.Model, ese.Description, ese.kind, ese.Status\r\nfrom ExperimentSchema.Experiment exse \r\nleft join ExperimentEquipment ee on ee.idExperiment = exse.idExperiment\r\nleft join EquipmentSchema.Equipment ese on ese.idEquipment = ee.idEquipment\r\nwhere exse.idExperiment = @idEquipmentExperiment";
                command.Parameters.AddWithValue("@idEquipmentExperiment", Id);

                command.CommandText = selectExperimentEquipment;

                using (SqlDataReader experimentReader = command.ExecuteReader())
                {
                    // Обработка второго результата (например, список экспериментов пользователя)
                    while (experimentReader.Read())
                    {
                        var reagent = new EquipmentDataModel
                        {
                            idEquipment = experimentReader["idExpEq"] != DBNull.Value ? Convert.ToInt32(experimentReader["idExpEq"]) : 0,
                            Name = experimentReader["Name"] != DBNull.Value ? Convert.ToString(experimentReader["Name"]) : "",
                            Model = experimentReader["Model"] != DBNull.Value ? Convert.ToString(experimentReader["Model"]) : "",
                            Description = experimentReader["Description"] != DBNull.Value ? Convert.ToString(experimentReader["Description"]) : "",
                            Kind = experimentReader["kind"] != DBNull.Value ? Convert.ToString(experimentReader["kind"]) : "",
                            Status = experimentReader["Status"] != DBNull.Value ? Convert.ToString(experimentReader["Status"]) : "",
                        };

                        // Добавить в список экспериментов, если нужно
                        Equipment.Add(reagent);
                    }
                }
            }
        }
        public async Task<IActionResult> OnPost([FromBody] UpdateReagent updatedReagent)
        {
            await OnGet(0);
            ExperimentID = JsonRequest.Instance(0).id;

            if (await SQLCommand.UpdateReagentExperimentRecord(updatedReagent, Reagent))
            {
                TempData["EmailNotification"] = "📧 Уведомление отправлено на почту.";
                await SQLCommand.ReagentResidueCheck();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostSaveDate([FromBody] Experiment updateExperiment)
        {
            updateExperiment.Id = JsonRequest.Instance(0).id;
            /*await */
            SQLCommand.UpdateExperiment(updateExperiment);

            // return JsonResult(new { message = "Данные успешно сохранены!" });
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostNewEquip(int EquipmentId)
        {

            const string query = "insert into ExperimentEquipment (idExperiment,idEquipment)\r\nvalues(@idExperiment, @idEquipment)";

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idExperiment", JsonRequest.Instance(0).id);
                    command.Parameters.AddWithValue("@idEquipment", EquipmentId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch //(Exception ex)
                    {
                    }
                }
            }

            await OnGet(0);
            return Page();
        }

        public async Task<IActionResult> OnPostNewReagent(int ReagentId)
        {

            const string query = "insert into ReagentExperiment(idExperiment,idReagent)\r\nvalues(@idExperiment, @idReagent)";

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idExperiment", JsonRequest.Instance(0).id);
                    command.Parameters.AddWithValue("@idReagent", ReagentId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch //(Exception ex)
                    {
                    }
                }
            }

            await OnGet(0);
            return Page();
        }

        //static IActionResult ExportToExcel()
        //{

        //}

        public async Task<IActionResult> OnPostExport()
        {
            await OnGet(0); // Подготовка данных

            switch (reportId)
            {
                case 0:
                    return GenerateExcelReport();
                case 1:
                    return GeneratePdfReport();
                case 2:
                    return GenerateXmlReport();
                case 3:
                    return GenerateJsonReport();
                default:
                    return BadRequest("Неверный reportId");
            }
        }

        private IActionResult GenerateExcelReport()
        {
            using (var memoryStream = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Отчет");
                    int Row = 1;

                    // Добавляем данные в Excel
                    worksheet.Cells[Row, 1].Value = "Отчет"; Row++;
                    worksheet.Cells[Row, 1].Value = "Дата:";
                    worksheet.Cells[Row, 2].Value = DateTime.Now.ToString("dd.MM.yyyy"); Row += 2;

                    worksheet.Cells[Row, 1].Value = "Эксперимент"; Row++;
                    worksheet.Cells[Row, 1].Value = "Название";
                    worksheet.Cells[Row, 2].Value = Experiment.Name; Row++;

                    worksheet.Cells[Row, 1].Value = "Описание";
                    worksheet.Cells[Row, 2].Value = Experiment.Description; Row++;

                    worksheet.Cells[Row, 1].Value = "Начало";
                    worksheet.Cells[Row, 2].Value = Experiment.StartDate; Row++;

                    worksheet.Cells[Row, 1].Value = "Конец";
                    worksheet.Cells[Row, 2].Value = Experiment.EndDate; Row++;

                    worksheet.Cells[Row, 1].Value = "Результат";
                    worksheet.Cells[Row, 2].Value = Experiment.Result; Row++;

                    worksheet.Cells[Row, 1].Value = "Статус";
                    worksheet.Cells[Row, 2].Value = Experiment.Status; Row += 2;

                    worksheet.Cells[4, 6].Value = "Использованное оборудование:"; Row++;

                    worksheet.Cells[5, 6].Value = "Название";
                    worksheet.Cells[5, 7].Value = "Модель";
                    worksheet.Cells[5, 8].Value = "Описание";
                    worksheet.Cells[5, 9].Value = "Вид";
                    worksheet.Cells[5, 10].Value = "Статус"; Row++;

                    int i = 6;
                    foreach (var item in Equipment)
                    {
                        worksheet.Cells[i, 6].Value = item.Name;
                        worksheet.Cells[i, 7].Value = item.Model;
                        worksheet.Cells[i, 8].Value = item.Description;
                        worksheet.Cells[i, 9].Value = item.Kind;
                        worksheet.Cells[i, 10].Value = item.Status;
                        i++;
                    }

                    Row++;
                    worksheet.Cells[4, 13].Value = "Используемые реагенты:"; Row++;

                    worksheet.Cells[5, 13].Value = "Название";
                    worksheet.Cells[5, 14].Value = "Химическая формула";
                    worksheet.Cells[5, 15].Value = "Плотность";
                    worksheet.Cells[5, 16].Value = "Использованная масса";
                    worksheet.Cells[5, 17].Value = "Количество реагента"; Row++;

                    i = 6;
                    foreach (var item in Reagent)
                    {
                        worksheet.Cells[i, 13].Value = item.idReagentDataModel.Reagent.Name;
                        worksheet.Cells[i, 14].Value = item.idReagentDataModel.Reagent.ChemicalFormula;
                        worksheet.Cells[i, 15].Value = item.idReagentDataModel.Reagent.Dansity;
                        worksheet.Cells[i, 16].Value = item.UseCount;
                        worksheet.Cells[i, 17].Value = item.Mass;
                        i++;
                    }

                    package.Save();
                }

                // Возвращаем файл
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
            }
        }

        private IActionResult GeneratePdfReport()
        {
            using (var memoryStream = new MemoryStream())
            {
                // Используем библиотеку iText7 для создания PDF
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);

                // Создаем документ
                var document = new Document(pdf);

                // Создаем шрифт с поддержкой Unicode (кириллица)
                var fontPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "fonts", "FreeSans.ttf");
                PdfFont font = PdfFontFactory.CreateFont(fontPath);

                // Добавляем данные в PDF
                document.Add(new Paragraph("Отчет").SetFont(font));
                document.Add(new Paragraph($"Дата: {DateTime.Now:dd.MM.yyyy}").SetFont(font));

                document.Add(new Paragraph("Эксперимент").SetFont(font));
                document.Add(new Paragraph($"Название: {Experiment.Name}").SetFont(font));
                document.Add(new Paragraph($"Описание: {Experiment.Description}").SetFont(font));
                document.Add(new Paragraph($"Начало: {Experiment.StartDate}").SetFont(font));
                document.Add(new Paragraph($"Конец: {Experiment.EndDate}").SetFont(font));
                document.Add(new Paragraph($"Результат: {Experiment.Result}").SetFont(font));
                document.Add(new Paragraph($"Статус: {Experiment.Status}").SetFont(font));

                document.Add(new Paragraph("Использованное оборудование:").SetFont(font));
                var table = new Table(5);
                table.SetFont(font); // Устанавливаем шрифт для таблицы

                table.AddHeaderCell("Название");
                table.AddHeaderCell("Модель");
                table.AddHeaderCell("Описание");
                table.AddHeaderCell("Вид");
                table.AddHeaderCell("Статус");

                foreach (var item in Equipment)
                {
                    table.AddCell(item.Name);
                    table.AddCell(item.Model);
                    table.AddCell(item.Description);
                    table.AddCell(item.Kind);
                    table.AddCell(item.Status);
                }

                document.Add(table);

                document.Add(new Paragraph("Используемые реагенты:").SetFont(font));
                var reagentTable = new Table(5);
                reagentTable.SetFont(font); // Устанавливаем шрифт для таблицы

                reagentTable.AddHeaderCell("Название");
                reagentTable.AddHeaderCell("Химическая формула");
                reagentTable.AddHeaderCell("Плотность");
                reagentTable.AddHeaderCell("Использованная масса");
                reagentTable.AddHeaderCell("Количество реагента");

                foreach (var item in Reagent)
                {
                    if (item.idReagentDataModel.Reagent.Name == "") continue;
                    reagentTable.AddCell(item.idReagentDataModel.Reagent.Name);
                    reagentTable.AddCell(item.idReagentDataModel.Reagent.ChemicalFormula);
                    reagentTable.AddCell(item.idReagentDataModel.Reagent.Dansity.ToString());
                    reagentTable.AddCell(item.Mass.ToString());
                    reagentTable.AddCell(item.UseCount.ToString());
                }

                document.Add(reagentTable);

                document.Close();

                // Возвращаем файл
                return File(memoryStream.ToArray(), "application/pdf", "Report.pdf");
            }
        }

        private IActionResult GenerateXmlReport()
        {
            var xml = new XElement("Report",
                new XElement("Date", DateTime.Now.ToString("dd.MM.yyyy")),
                new XElement("Experiment",
                    new XElement("Name", Experiment.Name),
                    new XElement("Description", Experiment.Description),
                    new XElement("StartDate", Experiment.StartDate),
                    new XElement("EndDate", Experiment.EndDate),
                    new XElement("Result", Experiment.Result),
                    new XElement("Status", Experiment.Status)
                ),
                new XElement("Equipment",
                    from item in Equipment
                    select new XElement("Item",
                        new XElement("Name", item.Name),
                        new XElement("Model", item.Model),
                        new XElement("Description", item.Description),
                        new XElement("Kind", item.Kind),
                        new XElement("Status", item.Status)
                    )
                ),
                new XElement("Reagents",
                    from item in Reagent
                    select new XElement("Item",
                        new XElement("Name", item.idReagentDataModel.Reagent.Name),
                        new XElement("ChemicalFormula", item.idReagentDataModel.Reagent.ChemicalFormula),
                        new XElement("Dansity", item.idReagentDataModel.Reagent.Dansity),
                        new XElement("Mass", item.Mass),
                        new XElement("UseCount", item.UseCount)
                    )
                )
            );

            var xmlString = xml.ToString();
            var bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);

            return File(bytes, "application/xml", "Report.xml");
        }

        private IActionResult GenerateJsonReport()
        {
            var report = new
            {
                Date = DateTime.Now.ToString("dd.MM.yyyy"),
                Experiment = new
                {
                    Name = Experiment.Name,
                    Description = Experiment.Description,
                    StartDate = Experiment.StartDate,
                    EndDate = Experiment.EndDate,
                    Result = Experiment.Result,
                    Status = Experiment.Status
                },
                Equipment = Equipment.Select(e => new
                {
                    Name = e.Name,
                    Model = e.Model,
                    Description = e.Description,
                    Kind = e.Kind,
                    Status = e.Status
                }),
                Reagents = Reagent.Select(r => new
                {
                    Name = r.idReagentDataModel.Reagent.Name,
                    ChemicalFormula = r.idReagentDataModel.Reagent.ChemicalFormula,
                    Dansity = r.idReagentDataModel.Reagent.Dansity,
                    Mass = r.Mass,
                    UseCount = r.UseCount
                })
            };

            var json = System.Text.Json.JsonSerializer.Serialize(report, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);

            return File(bytes, "application/json", "Report.json");
        }
    }

    public class Experiment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Result { get; set; }
        public string? Status { get; set; }
    }
    public class UpdateReagent
    {
        public int idReagentExperiment { get; set; }
        public double? UseMass { get; set; }
        public int? UseCount { get; set; }
    }
}

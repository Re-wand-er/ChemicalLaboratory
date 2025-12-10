using ChemicalLaboratory.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ChemicalLaboratory.Pages.Add
{
    public class NewUserModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<ReagentForDB> NewReagents { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnPostInsert()
        {
            if (!ModelState.IsValid)
            {
                // Собираем сообщения об ошибках для конкретных полей
                var errorMessages = new List<string>();

                foreach (var state in ModelState)
                {
                    var fieldName = state.Key; // Имя поля
                    var errors = state.Value.Errors; // Список ошибок для этого поля

                    foreach (var error in errors)
                    {
                        // Добавляем ошибку в список (можно форматировать по желанию)
                        errorMessages.Add($"Поле '{fieldName}': {error.ErrorMessage}");
                    }
                }

                // Формируем сообщение об ошибке
                ErrorMessage = "Обнаружены ошибки:\n" + string.Join("\n", errorMessages);
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                // Формируем общий запрос для всех записей
                string insertQuery = @"
                    INSERT INTO ReagentSchemaView
                    (ReagentName, Dansity, ChemicalFormula, mass,
                     idManufacturer, DateOfManufacture, series, idPurity, PurityDegree,
                     SupplierName, email, PhoneNumber)
                    VALUES ";

                // Собираем строки значений
                var valueSets = new List<string>();
                var parameters = new List<SqlParameter>();
                for (int i = 0; i < NewReagents.Count; i++)
                {
                    var reagent = NewReagents[i];
                    valueSets.Add($"(@Name{i}, @Dansity{i}, @ChemicalFormula{i}, @Mass{i}, " +
                                  $"@idManufacture{i}, @DateOfManufacture{i}, @Series{i}, @PurityClassification{i}, " +
                                  $"@PurityDegree{i}, @SupplierName{i}, @Email{i}, @PhoneNumber{i})");

                    // Добавляем параметры
                    parameters.Add(new SqlParameter($"@Name{i}", reagent.Name));
                    parameters.Add(new SqlParameter($"@Dansity{i}", reagent.Dansity));
                    parameters.Add(new SqlParameter($"@ChemicalFormula{i}", reagent.ChemicalFormula));
                    parameters.Add(new SqlParameter($"@Mass{i}", reagent.Mass));
                    parameters.Add(new SqlParameter($"@idManufacture{i}", reagent.idManufacture));
                    parameters.Add(new SqlParameter($"@DateOfManufacture{i}", reagent.DateOfManufacture));
                    parameters.Add(new SqlParameter($"@Series{i}", reagent.series));
                    parameters.Add(new SqlParameter($"@PurityClassification{i}", reagent.PurityClassification));
                    parameters.Add(new SqlParameter($"@PurityDegree{i}", reagent.PurityDegree));
                    parameters.Add(new SqlParameter($"@SupplierName{i}", reagent.SupplierName));
                    parameters.Add(new SqlParameter($"@Email{i}", reagent.SupplierMail));
                    parameters.Add(new SqlParameter($"@PhoneNumber{i}", reagent.SupplierPhone));
                }

                // Объединяем запрос
                insertQuery += string.Join(", ", valueSets) + ";";

                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Добавляем все параметры в команду
                        command.Parameters.AddRange(parameters.ToArray());

                        // Выполняем запрос
                        int rowsAffected = command.ExecuteNonQuery();
                        // Можно проверить rowsAffected, чтобы убедиться, что все строки добавлены
                    }
                }
                catch (SqlException ex)
                {
                    // Логгирование или обработка исключений
                    //Console.WriteLine(ex.Message);
                    ErrorMessage = "Ошибка: " + ex.Message;
                }
            }

            return RedirectToPage("/Home/Reagent");
        }

        public IActionResult OnPostAddNewItem()
        {
            if (NewReagents == null)
            {
                NewReagents = new List<ReagentForDB>();
            }

            NewReagents.Add(new ReagentForDB());

            return Page();
        }

        public void OnGet()
        {
            if (NewReagents == null || NewReagents.Count == 0)
            {
                NewReagents = new List<ReagentForDB>();
                //NewReagents.Add(new ReagentForDB( ));
            }
        }
    }

    public class ReagentForDB
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Dansity { get; set; }
        public string ChemicalFormula { get; set; } = string.Empty;
        public decimal? Mass { get; set; }
        public DateTime? DateOfManufacture { get; set; }
        public string series { get; set; } = string.Empty;
        public string PurityDegree { get; set; } = string.Empty;
        public int PurityClassification { get; set; } = 0;
        public int idManufacture { get; set; } = 0;

        public string SupplierName { get; set; } = string.Empty;
        public string SupplierMail { get; set; } = string.Empty;
        public string SupplierPhone { get; set; } = string.Empty;
    }
}
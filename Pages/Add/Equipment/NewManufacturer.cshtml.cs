using ChemicalLaboratory.Domain;
using ChemicalLaboratory.Models.Equipment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ChemicalLaboratory.Pages.Add.Equipment
{
    public class NewManufacturerModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public List<EManufacturerDataModel> NewManufacturer { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
        public void OnGet()
        {
        }

        public IActionResult OnPostInsert()
        {
            if (!ModelState.IsValid)
            {
                // Собираем сообщения об ошибках
                var errorMessages = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .SelectMany(ms => ms.Value.Errors.Select(e => $"Поле '{ms.Key}': {e.ErrorMessage}"))
                    .ToList();

                ErrorMessage = "Обнаружены ошибки:\n" + string.Join("\n", errorMessages);
                return Page();
            }

            var valueSets = new List<string>();

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction(); // Начало транзакции

                try
                {
                    // Запрос для вставки записей в таблицу Manufacturer
                    string insertManufacturerQuery = @"
                        INSERT INTO EquipmentSchema.Manufacturer
                        (email, PhoneNumber, address, City, Country)
                        VALUES ";

                    for (int i = 0; i < NewManufacturer.Count; i++)
                    {
                        var reagent = NewManufacturer[i];

                        using (SqlCommand command = new SqlCommand(insertManufacturerQuery, connection, transaction))
                        {
                            valueSets.Add($"(@Email{i}, @PhoneNumber{i}, @Address{i}, @City{i}, @Country{i})");
                            // Добавляем параметры
                            command.Parameters.AddWithValue("@Email", reagent.email ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@PhoneNumber", reagent.PhoneNumber ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Address", reagent.Address ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@City", reagent.City ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Country", reagent.Country ?? (object)DBNull.Value);

                            // Выполняем запрос и получаем вставленный id

                        }
                    }
                    transaction.Commit(); // Подтверждаем транзакцию

                    insertManufacturerQuery += string.Join(", ", valueSets) + ";";
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Откатываем транзакцию в случае ошибки
                    ErrorMessage = $"Ошибка SQL: {ex.Message}";
                    return Page();
                }
            }

            return RedirectToPage("/Home/Equipment");
        }


    }
}

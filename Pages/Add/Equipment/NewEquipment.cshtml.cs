using ChemicalLaboratory.Pages.Home;
using ChemicalLaboratory.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChemicalLaboratory.Pages.Add.Equipment
{
    public class NewEquipmentModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<Home.Equipment> NewEquipment { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> ManufactureName { get; set; } //= new();

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            //Нужно получать два значения из базы: ключ производства и почту 
            ManufactureName = SQLCommand.Get2FieldOption("select idManufacturer, email from EquipmentSchema.Manufacturer");

            return Page();
        }

        public IActionResult OnPostInsert()
        {
            if (!ModelState.IsValid)
            {
                // Собираем сообщения об ошибках
                var errorMessages = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .SelectMany(ms => ms.Value.Errors.Select(e => $"Поле: {e.ErrorMessage}"))
                    .ToList();

                ErrorMessage = "Обнаружены ошибки:\n" + string.Join("\n", errorMessages);
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    List<int> insertedIds = new List<int>();

                    // Сначала добавляем записи в Equipment
                    foreach (var equipment in NewEquipment)
                    {
                        if (equipment.Id == 0)
                        {
                            string insertEquipmentQuery = @"
                        INSERT INTO EquipmentSchema.Equipment (Name, Model, Description, kind, Status)
                        VALUES (@Name, @Model, @Description, @Kind, @Status);
                        SELECT SCOPE_IDENTITY();";

                            using (SqlCommand command = new SqlCommand(insertEquipmentQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Name", equipment.Name);
                                command.Parameters.AddWithValue("@Model", equipment.Model);
                                command.Parameters.AddWithValue("@Description", equipment.Description ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Kind", equipment.Kind);
                                command.Parameters.AddWithValue("@Status", equipment.Status);

                                object result = command.ExecuteScalar();
                                if (result != null)
                                {
                                    int newId = Convert.ToInt32(result);
                                    insertedIds.Add(newId);
                                }
                            }
                        }
                    }

                    // Затем добавляем записи в Equipment и связываем с EquipmentManufacturer
                    foreach (var equipment in NewEquipment.Where(e => e.Id != 0))
                    {
                        // Добавляем запись в Equipment
                        string insertEquipmentQuery = @"
                    INSERT INTO EquipmentSchema.Equipment (Name, Model, Description, kind, Status)
                    VALUES (@Name, @Model, @Description, @Kind, @Status);
                    SELECT SCOPE_IDENTITY();";

                        int equipmentId;
                        using (SqlCommand command = new SqlCommand(insertEquipmentQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Name", equipment.Name);
                            command.Parameters.AddWithValue("@Model", equipment.Model);
                            command.Parameters.AddWithValue("@Description", equipment.Description);
                            command.Parameters.AddWithValue("@Kind", equipment.Kind);
                            command.Parameters.AddWithValue("@Status", equipment.Status);

                            object result = command.ExecuteScalar();
                            equipmentId = Convert.ToInt32(result);
                        }

                        // Добавляем запись в EquipmentManufacturer
                        string insertManufacturerQuery = @"
                    INSERT INTO EquipmentSchema.EquipmentManufacturer (idEquipment, idManufacturer, PurchaseDate, GuaranteeDate)
                    VALUES (@EquipmentId, @ManufacturerId, @PurchaseDate, @GuaranteeDate);";

                        using (SqlCommand command = new SqlCommand(insertManufacturerQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@EquipmentId", equipmentId);
                            command.Parameters.AddWithValue("@ManufacturerId", equipment.Id); // Здесь Id - это ManufacturerId
                            command.Parameters.AddWithValue("@PurchaseDate", equipment.PurchaseDate);
                            command.Parameters.AddWithValue("@GuaranteeDate", equipment.GuaranteeDate ?? (object)DBNull.Value);

                            command.ExecuteNonQuery();
                        }
                    }

                    // Подтверждаем транзакцию
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Откатываем транзакцию в случае ошибки
                    transaction.Rollback();
                    ErrorMessage = "Ошибка при сохранении данных: " + ex.Message;
                    return Page();
                }
            }

            return RedirectToPage("/Home/Equipment");
        }
    }
}

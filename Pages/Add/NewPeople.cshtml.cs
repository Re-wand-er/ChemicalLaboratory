using ChemicalLaboratory.Pages.Home;
using EFCore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using ChemicalLaboratory.Domain;

namespace ChemicalLaboratory.Pages.Add
{
    public class NewPeopleModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<PeopleDTO> NewPeople { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
        public void OnGet()
        {
        }

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
                    insert into PeopleSchema.People 
                    (FirstName, MiddleName, LastName, Sex, email, JobPosition, idWorkSchedule, SystemRole, Login, PasswordHash)
                    values ";

                // Собираем строки значений
                var valueSets = new List<string>();
                var parameters = new List<SqlParameter>();

                for (int i = 0; i < NewPeople.Count; i++)
                {
                    var reagent = NewPeople[i];
                    valueSets.Add($"(@FirstName{i}, @MiddleName{i}, @LastName{i}, @Sex{i}, " +
                                  $"@email{i}, @JobPosition{i}, @idWorkSchedule{i}, @SystemRole{i}, " +
                                  $"@Login{i}, @PasswordHash{i})");

                    // Добавляем параметры
                    parameters.Add(new SqlParameter($"@FirstName{i}",      reagent.FirstName));
                    parameters.Add(new SqlParameter($"@MiddleName{i}",     reagent.MiddleName));
                    parameters.Add(new SqlParameter($"@LastName{i}",       reagent.LastName));
                    parameters.Add(new SqlParameter($"@Sex{i}",            reagent.Sex));
                    parameters.Add(new SqlParameter($"@email{i}",          reagent.Email));
                    parameters.Add(new SqlParameter($"@JobPosition{i}",    reagent.JobPosition));
                    parameters.Add(new SqlParameter($"@idWorkSchedule{i}", reagent.WorkShift));
                    parameters.Add(new SqlParameter($"@SystemRole{i}",     reagent.SystemRole));
                    parameters.Add(new SqlParameter($"@Login{i}",          reagent.Login));
                    parameters.Add(new SqlParameter($"@PasswordHash{i}",   reagent.PasswordHash));
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

            return RedirectToPage("/Home/People");
        }
    }
}

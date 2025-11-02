using ChemicalLaboratory.Models;
using ChemicalLaboratory.Models.Equipment;
using ChemicalLaboratory.Models.Experiment;
using ChemicalLaboratory.Models.People;
using ChemicalLaboratory.Models.Reagent;
using ChemicalLaboratory.Pages.Add;
using ChemicalLaboratory.Pages.Home;
using EFCore.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using static ChemicalLaboratory.Domain.MailSender;

namespace ChemicalLaboratory.Domain
{
    public class SQLCommand
    {

        public static string ip = "WIN-OD2JJLP2EBU\\SQLSERVER2,1434"; // IP-адрес сервера
        public static string ip_2 = "WIN-OD2JJLP2EBU\\SERVER2,1435"; // IP-адрес сервера
        public static string Old_ConnectionString = "Server=" + ip_2 + ";Database=ChemicalLaboratory;TrustServerCertificate=True;User Id=test;Password=#root42;"; //User Id=Server #root42_

        public static string ip_3 = "localhost,1433";
        public static string connectionString = "Server=" + ip_3 + ";Database=ChemicalLaboratory;TrustServerCertificate=True;User Id=sa;Password=#Root42@oAsDf4;"; //User Id=Server #root42_

        // Универсальный метод для выполнения SQL-запроса и получения списка объектов
        private static List<T> ExecuteQuery<T>(string query, Func<SqlDataReader, T> mapFunction)
        {
            var resultList = new List<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = mapFunction(reader);
                                resultList.Add(item);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            return resultList;
        }

        // Метод для получения списка людей
        public static List<PeopleDataModel> GetPeople()
        {
            string query = "select * from PeopleSchema.People psp\r\njoin PeopleSchema.WorkSchedule psws on psp.idWorkSchedule = psws.idWorkSchedule";
            return ExecuteQuery(query, reader => new PeopleDataModel
            {
                IdPeople = reader.GetInt32(0),
                FirstName = reader.GetString(3),
                MiddleName = reader.GetString(4),
                LastName = reader.GetString(5),
                Email = reader.GetString(6),
                Sex = reader.GetString(7),
                SystemRole = reader.GetString(8),
                JobPosition = reader.GetString(9),
                Login = reader.GetString(10),
                Password = ColumnExists(reader, "PasswordHash") ? Convert.ToString(reader["PasswordHash"]) : null,

                IdWorkShedule = new WorkScheduleDataModel
                {
                    idWorkSchedule = reader.GetInt32(12),
                    WorkShift = reader.GetString(13),
                    StartTime = reader.GetTimeSpan(14),
                    EndTime = reader.GetTimeSpan(15),
                }
            });
        }

        public static List<ReagentManufacturer> GetDataFromReagentSchema(string query)
        {
            //string query = "SELECT * FROM ReagentSchema.Reagent";
            return ExecuteQuery(query, reader => new ReagentManufacturer
            {
                // Reagent
                Reagent = new ReagentDataModel
                {
                    idReagent = ColumnExists(reader, "idReagent") ? Convert.ToInt32(reader["idReagent"]) : 0,
                    Dansity = ColumnExists(reader, "Dansity") ? Convert.ToDecimal(reader["Dansity"]) : 0,
                    Name = ColumnExists(reader, "ReagentName") ? Convert.ToString(reader["ReagentName"]) : "",
                    ChemicalFormula = ColumnExists(reader, "ChemicalFormula") ? Convert.ToString(reader["ChemicalFormula"]) : "",
                    Mass = ColumnExists(reader, "mass") ? Convert.ToDecimal(reader["mass"]) : 0,
                },

                // Reagent & Manufacture
                idReagManuf = ColumnExists(reader, "IdReagManuf") ? Convert.ToInt32(reader["IdReagManuf"]) : 0,
                DateOfManufacture = ColumnExists(reader, "DateOfManufacture") ? Convert.ToDateTime(reader["DateOfManufacture"]) : DateTime.MinValue,
                series = ColumnExists(reader, "series") ? Convert.ToString(reader["series"]) : "",
                PurityClassification = ColumnExists(reader, "Classification") ? Convert.ToString(reader["Classification"]) : "",
                PurityDegree = ColumnExists(reader, "PurityDegree") ? Convert.ToDecimal(reader["PurityDegree"]) : 0,

                Manufacturer = new RManufacturerDataModel
                {
                    idRManufacturerDataModel = ColumnExists(reader, "IdManufacturer") ? Convert.ToInt32(reader["IdManufacturer"]) : 0,
                    Name = ColumnExists(reader, "ManufactureName") ? Convert.ToString(reader["ManufactureName"]) : "",
                    Email = ColumnExists(reader, "ManufactureEmail") ? Convert.ToString(reader["ManufactureEmail"]) : "",
                    Address = ColumnExists(reader, "address") ? Convert.ToString(reader["address"]) : "",
                    City = ColumnExists(reader, "City") ? Convert.ToString(reader["City"]) : "",
                    Country = ColumnExists(reader, "country") ? Convert.ToString(reader["country"]) : "",
                },

                Supplier = new SupplierDataModel
                {
                    idSupplier = ColumnExists(reader, "idSupplier") ? Convert.ToInt32(reader["idSupplier"]) : 0,
                    Name = ColumnExists(reader, "SupplierName") ? Convert.ToString(reader["SupplierName"]) : "",
                    email = ColumnExists(reader, "SupplierEmail") ? Convert.ToString(reader["SupplierEmail"]) : "",
                    PhoneNumber = ColumnExists(reader, "PhoneNumber") ? Convert.ToString(reader["PhoneNumber"]) : "",
                }
            });
        }

        public static List<EquipmentManufacturer> GetDataFromEquipmentSchema(string query)
        {
            //string query = "SELECT * FROM ReagentSchema.Reagent";
            return ExecuteQuery(query, reader => new EquipmentManufacturer
            {

                idEquipmentManufacturer = ColumnExists(reader, "idEquipmentManufacturer") ? Convert.ToInt32(reader["idEquipmentManufacturer"]) : 0,
                PurchaseDate = ColumnExists(reader, "PurchaseDate") && reader["PurchaseDate"] != DBNull.Value
                                                                    ? DateOnly.FromDateTime(Convert.ToDateTime(reader["PurchaseDate"]))
                                                                    : default,

                GuaranteeDate = ColumnExists(reader, "GuaranteeDate") && reader["GuaranteeDate"] != DBNull.Value
                                                                    ? DateOnly.FromDateTime(Convert.ToDateTime(reader["GuaranteeDate"]))
                                                                    : default,

                Equipment = new EquipmentDataModel
                {
                    idEquipment = ColumnExists(reader, "idEquipment") ? Convert.ToInt32(reader["idEquipment"]) : 0,
                    Name = ColumnExists(reader, "Name") ? Convert.ToString(reader["Name"]) : "",
                    Description = ColumnExists(reader, "Description") ? Convert.ToString(reader["Description"]) : "",
                    Kind = ColumnExists(reader, "kind") ? Convert.ToString(reader["kind"]) : "",
                    Model = ColumnExists(reader, "Model") ? Convert.ToString(reader["Model"]) : "",
                    Status = ColumnExists(reader, "Status") ? Convert.ToString(reader["Status"]) : "",
                },

                Manufacturer = new EManufacturerDataModel
                {
                    idManufacturer = ColumnExists(reader, "idManufacturer") ? Convert.ToInt32(reader["idManufacturer"]) : 0,
                    Address = ColumnExists(reader, "address") ? Convert.ToString(reader["address"]) : "",
                    City = ColumnExists(reader, "City") ? Convert.ToString(reader["City"]) : "",
                    Country = ColumnExists(reader, "Country") ? Convert.ToString(reader["Country"]) : "",
                    email = ColumnExists(reader, "email") ? Convert.ToString(reader["email"]) : "",
                    PhoneNumber = ColumnExists(reader, "PhoneNumber") ? Convert.ToString(reader["PhoneNumber"]) : "",
                }
            });
        }

        public static List<EquipmentManufacturer> GetDataFromExperimentSchema(string query)
        {
            //string query = "SELECT * FROM ReagentSchema.Reagent";
            return ExecuteQuery(query, reader => new EquipmentManufacturer
            {

                idEquipmentManufacturer = ColumnExists(reader, "idEquipmentManufacturer") ? Convert.ToInt32(reader["idEquipmentManufacturer"]) : 0,
                PurchaseDate = ColumnExists(reader, "PurchaseDate") && reader["PurchaseDate"] != DBNull.Value
                                                                    ? DateOnly.FromDateTime(Convert.ToDateTime(reader["PurchaseDate"]))
                                                                    : default,

                GuaranteeDate = ColumnExists(reader, "GuaranteeDate") && reader["GuaranteeDate"] != DBNull.Value
                                                                    ? DateOnly.FromDateTime(Convert.ToDateTime(reader["GuaranteeDate"]))
                                                                    : default,

                Equipment = new EquipmentDataModel
                {
                    idEquipment = ColumnExists(reader, "idEquipment") ? Convert.ToInt32(reader["idEquipment"]) : 0,
                    Name = ColumnExists(reader, "Name") ? Convert.ToString(reader["Name"]) : "",
                    Description = ColumnExists(reader, "Description") ? Convert.ToString(reader["Description"]) : "",
                    Kind = ColumnExists(reader, "kind") ? Convert.ToString(reader["kind"]) : "",
                    Model = ColumnExists(reader, "Model") ? Convert.ToString(reader["Model"]) : "",
                    Status = ColumnExists(reader, "Status") ? Convert.ToString(reader["Status"]) : "",
                },

                Manufacturer = new EManufacturerDataModel
                {
                    idManufacturer = ColumnExists(reader, "idManufacturer") ? Convert.ToInt32(reader["idManufacturer"]) : 0,
                    Address = ColumnExists(reader, "address") ? Convert.ToString(reader["address"]) : "",
                    City = ColumnExists(reader, "City") ? Convert.ToString(reader["City"]) : "",
                    Country = ColumnExists(reader, "Country") ? Convert.ToString(reader["Country"]) : "",
                    email = ColumnExists(reader, "email") ? Convert.ToString(reader["email"]) : "",
                    PhoneNumber = ColumnExists(reader, "PhoneNumber") ? Convert.ToString(reader["PhoneNumber"]) : "",
                }
            });
        }

        public static List<ExperimentDataModel> GetExperiments() // переделается
        {
            string query = "select * from ExperimentSchema.Experiment";
            return ExecuteQuery(query, reader => new ExperimentDataModel
            {
                idExperiment = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                StartDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                EndDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                Result = reader.IsDBNull(5) ? null : reader.GetString(5),
                Status = reader.GetString(6)
            });
        }

        // По сути можно просто иметь метод Get2FieldOption и передовать туда sql запрос

        public static List<SelectListItem> Get2FieldOption(string query)
        {
            return ExecuteQuery(query, reader => new SelectListItem
            {
                Value = reader.GetInt32(0).ToString(),
                Text = reader.GetString(1),
            });
        }

        public static void DeleteRecord(string query, int id)
        {
            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        connection.OpenAsync();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch //(Exception ex)
                    {
                        // Обработка ошибок (можно добавить логирование)
                    }
                }
            }
        }

        public async static /*void*/ Task UpdateReagentRecord(ReagentDataModel reagentDataModel)
        {
            const string query = "UPDATE ReagentSchema.Reagent\r\n" +
                                    "SET \r\n" +
                                    "Dansity = @Dansity,\r\n" +
                                    "Name = @Name,\r\n" +
                                    "ChemicalFormula = @ChemicalFormula,\r\n" +
                                    "mass = @Mass\r\n" +
                                    "WHERE idReagent = @idReagent"; // Убедитесь, что имя таблицы и столбца соответствуют вашей схеме

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Dansity", reagentDataModel.Dansity);
                    command.Parameters.AddWithValue("@Name", reagentDataModel.Name);
                    command.Parameters.AddWithValue("@ChemicalFormula", reagentDataModel.ChemicalFormula);
                    command.Parameters.AddWithValue("@Mass", reagentDataModel.Mass);
                    command.Parameters.AddWithValue("@idReagent", reagentDataModel.idReagent);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    catch //(Exception ex)
                    {
                        // Обработка ошибок (можно добавить логирование)
                    }
                }
            }
        }

        public async static /*void*/ Task UpdatePeopleRecord(PeopleDTO reagentDataModel)
        {
            const string query = "UPDATE PeopleSchema.People\n" +
                "SET\r\n    FirstName = @FirstName,\r\n    MiddleName = @MiddleName,\r\n\tLastName = @LastName," +
                " Sex = @Sex,\r\n    email = @Email,\r\n    JobPosition = @JobPosition,\r\n\tidWorkSchedule = @idWorkSchedule," +
                "SystemRole = @SystemRole,\r\n\tLogin = @Login\n" +
                "WHERE\r\n    IdPeople = @IdPeople;\r\n"; // ,\r\n\tPasswordHash = @PasswordHash

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", reagentDataModel.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", reagentDataModel.MiddleName);
                    command.Parameters.AddWithValue("@LastName", reagentDataModel.LastName);
                    command.Parameters.AddWithValue("@Sex", reagentDataModel.Sex);
                    command.Parameters.AddWithValue("@Email", reagentDataModel.Email);
                    command.Parameters.AddWithValue("@JobPosition", reagentDataModel.JobPosition);
                    command.Parameters.AddWithValue("@idWorkSchedule", reagentDataModel.WorkSchedule);
                    command.Parameters.AddWithValue("@SystemRole", reagentDataModel.SystemRole);
                    command.Parameters.AddWithValue("@Login", reagentDataModel.Login);
                    //command.Parameters.AddWithValue("@PasswordHash",   reagentDataModel.PasswordHash);
                    command.Parameters.AddWithValue("@IdPeople", reagentDataModel.IdPeople);

                    try
                    {
                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    catch //(Exception ex)
                    {
                        // Обработка ошибок (можно добавить логирование)
                    }
                }
            }
        }

        // mode тип update: 1 - только для оборудования, 2 - только для производителя, 3 и для того и для того
        public async static Task UpdateEquipmentRecord(Equipment reagentDataModel, int mode)
        {

            const string queryEquipment = "UPDATE EquipmentSchema.Equipment\r\nSET     \r\nName = @Name,  \r\nDescription = @Description,  \r\nModel = @Model,  \r\nkind = @kind,     \r\nStatus = @Status\r\nWHERE idEquipment = @idEquipment; "; // Убедитесь, что имя таблицы и столбца соответствуют вашей схеме
            const string queryManufacture = "UPDATE EquipmentSchema.Manufacturer\r\nSET     \r\nemail = @email,    \r\nPhoneNumber = @PhoneNumber,  \r\naddress = @address,     \r\nCity = @City,\r\nCountry = @Country\r\nWHERE idManufacturer = @idManufacturer;";
            const string queryEquipmentManufacture = "UPDATE ese\r\nSET     \r\nName = @Name,   \r\nModel = @Model, \r\nDescription = @Description,\r\nkind = @kind,     \r\nStatus = @Status\r\nfrom EquipmentSchema.Equipment ese\r\njoin EquipmentSchema.EquipmentManufacturer esem on ese.idEquipment = esem.idEquipment\r\nwhere idEquipmentManufacturer = @idEquipmentManufacturer;\r\n\r\nUPDATE esm\r\nSET     \r\nemail = @email,    \r\nPhoneNumber = @PhoneNumber,  \r\naddress = @address,     \r\nCity = @City,\r\nCountry = @Country\r\nfrom EquipmentSchema.Manufacturer esm \r\njoin EquipmentSchema.EquipmentManufacturer esem on esm.idManufacturer = esem.idManufacturer\r\nwhere idEquipmentManufacturer = @idEquipmentManufacturer;\r\n\r\nUpdate esem\r\nset \r\n\tPurchaseDate = @PurchaseDate,\r\n\tGuaranteeDate = @GuaranteeDate\r\nfrom EquipmentSchema.EquipmentManufacturer esem\r\nwhere idEquipmentManufacturer = @idEquipmentManufacturer;";

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {

                switch (mode)
                {
                    case 1:
                        {
                            using (SqlCommand command = new SqlCommand(queryEquipment, connection))
                            {
                                command.Parameters.AddWithValue("@Name", reagentDataModel.Name);
                                command.Parameters.AddWithValue("@Model", reagentDataModel.Model);
                                command.Parameters.AddWithValue("@Description", reagentDataModel.Description);
                                command.Parameters.AddWithValue("@kind", reagentDataModel.Kind);
                                command.Parameters.AddWithValue("@Status", reagentDataModel.Status);
                                command.Parameters.AddWithValue("@idEquipment", reagentDataModel.Id);

                                try
                                {
                                    await connection.OpenAsync();
                                    int rowsAffected = await command.ExecuteNonQueryAsync();
                                }
                                catch //(Exception ex)
                                {
                                    // Обработка ошибок (можно добавить логирование)
                                }
                            }

                            break;
                        }

                    case 2:
                        {
                            using (SqlCommand command = new SqlCommand(queryManufacture, connection))
                            {
                                command.Parameters.AddWithValue("@email", reagentDataModel.Email);
                                command.Parameters.AddWithValue("@PhoneNumber", reagentDataModel.PhoneNumber);
                                command.Parameters.AddWithValue("@address", reagentDataModel.Address);
                                command.Parameters.AddWithValue("@City", reagentDataModel.City);
                                command.Parameters.AddWithValue("@Country", reagentDataModel.Country);
                                command.Parameters.AddWithValue("@idManufacturer", reagentDataModel.Id);

                                try
                                {
                                    await connection.OpenAsync();
                                    int rowsAffected = await command.ExecuteNonQueryAsync();
                                }
                                catch //(Exception ex)
                                {
                                    // Обработка ошибок (можно добавить логирование)
                                }
                            }

                            break;
                        }

                    case 3:
                        {

                            var purchaseDate = DateOnly.Parse(reagentDataModel.PurchaseDate);
                            var guaranteeDate = DateOnly.Parse(reagentDataModel.GuaranteeDate);

                            using (SqlCommand command = new SqlCommand(queryEquipmentManufacture, connection))
                            {
                                command.Parameters.AddWithValue("@Name", reagentDataModel.Name);
                                command.Parameters.AddWithValue("@Model", reagentDataModel.Model);
                                command.Parameters.AddWithValue("@Description", reagentDataModel.Description);
                                command.Parameters.AddWithValue("@kind", reagentDataModel.Kind);
                                command.Parameters.AddWithValue("@Status", reagentDataModel.Status);
                                command.Parameters.AddWithValue("@email", reagentDataModel.Email);
                                command.Parameters.AddWithValue("@PhoneNumber", reagentDataModel.PhoneNumber);
                                command.Parameters.AddWithValue("@address", reagentDataModel.Address);
                                command.Parameters.AddWithValue("@City", reagentDataModel.City);
                                command.Parameters.AddWithValue("@Country", reagentDataModel.Country);
                                command.Parameters.AddWithValue("@PurchaseDate", reagentDataModel.PurchaseDate);
                                command.Parameters.AddWithValue("@GuaranteeDate", reagentDataModel.GuaranteeDate);
                                command.Parameters.AddWithValue("@idEquipmentManufacturer", reagentDataModel.Id);

                                try
                                {
                                    await connection.OpenAsync();
                                    int rowsAffected = await command.ExecuteNonQueryAsync();
                                }
                                catch //(Exception ex)
                                {
                                    // Обработка ошибок (можно добавить логирование)
                                }
                            }
                            break;
                        }
                }
            }
        }
        private static bool ColumnExists(SqlDataReader reader, string columnName)
        {
            try
            {
                // Получаем индекс столбца по имени
                int columnIndex = reader.GetOrdinal(columnName);
                return columnIndex >= 0; // Если индекс >= 0, столбец существует
            }
            catch (IndexOutOfRangeException)
            {
                // Если столбец не найден, метод вернет false
                return false;
            }
        }

        //public async static Task UpdateReagentExperimentRecord(UpdateReagent reagentDataModel)
        //{
        //    double mass = 0;

        //    //const string query = "select mass from ReagentExperiment\r\nwhere idReagExpetiment = @idReagExpetiment";
        //    using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
        //    {
        //        const string query2 = "UPDATE ReagentExperiment\r\nSET\r\nmass = @mass,\r\nUseCount = @UseCount\r\nWHERE\r\nidReagExpetiment = @idReagExpetiment;"; // ,\r\n\tPasswordHash = @PasswordHash


        //        using (SqlCommand command = new SqlCommand(query2, connection))
        //        {
        //            command.Parameters.AddWithValue("@mass", reagentDataModel.UseMass);
        //            command.Parameters.AddWithValue("@UseCount", reagentDataModel.UseCount);
        //            command.Parameters.AddWithValue("@idReagExpetiment", reagentDataModel.idReagentExperiment);

        //            try
        //            {
        //                await connection.OpenAsync();
        //                int rowsAffected = await command.ExecuteNonQueryAsync();
        //            }
        //            catch //(Exception ex)
        //            {
        //                // Обработка ошибок (можно добавить логирование)
        //            }
        //        }

        //    }
        //}
        public async static Task<bool> UpdateReagentExperimentRecord(UpdateReagent reagentDataModel, List<ReagentExperiment> Reagent)
        {
            bool result = false;

            decimal mass = 0;
            var id = Reagent.FirstOrDefault(r => r.idReagentExperiment == reagentDataModel.idReagentExperiment);
            var massDiff = reagentDataModel.UseMass - id?.Mass;

            var idReagent = id?.idReagentDataModel.Reagent.idReagent;

            const string selectQuery = "select mass from ReagentSchema.Reagent\r\nwhere idReagent = @IdReagent and mass > @UseMass";

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                await connection.OpenAsync();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    // SELECT
                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection, transaction))
                    {
                        selectCommand.Parameters.AddWithValue("@IdReagent", idReagent);
                        selectCommand.Parameters.AddWithValue("@UseMass", massDiff);

                        using (SqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                mass = reader.GetDecimal(0); // предполагаем, что mass всегда есть
                            }
                            else
                            {
                                throw new Exception("Недостаточно массы реагента");
                            }
                        }
                    }

                    // UPDATE
                    using (SqlCommand updateCommand = new SqlCommand())
                    {
                        updateCommand.Connection = connection;
                        updateCommand.Transaction = transaction;
                        updateCommand.CommandText = @"
                                                        UPDATE ReagentSchema.Reagent
                                                        SET mass = @NewReagentMass
                                                        WHERE idReagent = @idReagent;

                                                        UPDATE ReagentExperiment
                                                        SET mass = @mass, UseCount = @UseCount
                                                        WHERE idReagExpetiment = @idReagExpetiment;
                                                    ";

                        updateCommand.Parameters.AddWithValue("@NewReagentMass", (double)mass - massDiff);
                        updateCommand.Parameters.AddWithValue("@idReagent", idReagent);
                        updateCommand.Parameters.AddWithValue("@mass", reagentDataModel.UseMass);
                        updateCommand.Parameters.AddWithValue("@UseCount", reagentDataModel.UseCount);
                        updateCommand.Parameters.AddWithValue("@idReagExpetiment", reagentDataModel.idReagentExperiment);

                        await updateCommand.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // логирование ошибки желательно
                    throw;
                }
            }

            return result;
        }
        public async static Task ReagentResidueCheck()
        {
            var lowStockReagents = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "select Name, ChemicalFormula, mass from ReagentSchema.Reagent  WHERE mass < 150";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string? name = ColumnExists(reader, "Name") ? Convert.ToString(reader["Name"]) : "";
                        string? ChemicalFormula = ColumnExists(reader, "ChemicalFormula") ? Convert.ToString(reader["ChemicalFormula"]) : "";
                        decimal mass = ColumnExists(reader, "mass") ? Convert.ToDecimal(reader["mass"]) : 0;
                        lowStockReagents.Add($"{name} {ChemicalFormula} ({mass} гр.)\n");
                    }
                }
            }

            if (lowStockReagents.Count > 0)
            {
                string subject = "⚠️ Низкий уровень реагентов";
                string body = "Следующие реагенты требуют пополнения:\n\n" + string.Join("\n", lowStockReagents);

                await SendMailToEmail(UserProfile.Instance(0).User.Email, subject, body);
            }
        }
        public static void UpdatePeoplePassword(string Password, string Login, string Email)
        {
            const string query = "update PeopleSchema.People\r\nset PasswordHash = HASHBYTES('SHA2_256', @PasswordHash)\r\nwhere Login = @Login and email = @Email"; // ,\r\n\tPasswordHash = @PasswordHash

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PasswordHash", Password);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.Parameters.AddWithValue("@Email", Email);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch //(Exception ex)
                    {
                        // Обработка ошибок (можно добавить логирование)
                    }
                }
            }
        }

        public /*async*/ static void/*Task*/ UpdateExperiment(Experiment experiment)
        {
            const string query = "update ExperimentSchema.Experiment " +
                "set Name = @Name, Description = @Description, StartDate = @StartDate, EndDate = @EndDate, Result = @Result, status = @status " +
                "where idExperiment = @idExperiment";


            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", experiment.Name);
                    command.Parameters.AddWithValue("@Description", experiment.Description);
                    command.Parameters.AddWithValue("@StartDate", experiment.StartDate);
                    command.Parameters.AddWithValue("@EndDate", experiment.EndDate);
                    command.Parameters.AddWithValue("@Result", experiment.Result);
                    command.Parameters.AddWithValue("@status", experiment.Status);
                    command.Parameters.AddWithValue("@idExperiment", experiment.Id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch //(Exception ex)
                    {
                        // Обработка ошибок (можно добавить логирование)
                    }
                }
            }
        }
    }
}

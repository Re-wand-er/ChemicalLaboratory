using ChemicalLaboratory.Domain;
using Microsoft.Data.SqlClient;
using ChemicalLaboratory.Models.Experiment;
using ChemicalLaboratory.Models.Equipment;
using ChemicalLaboratory.Models.Reagent;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace ChemicalLaboratory.Models.People
{
    public class UserProfile
    {
        public PeopleDataModel User { get; set; }
        public ExperimentDataModel Experiment { get; set; }
        public List<EquipmentDataModel> Equipment { get; set; }
        public List<ReagentExperiment> Reagent { get; set; }
        //public ReagentExperiment reagentExperiment { get; set; }

        //?
        //public ExperimentEquipment experimentEquipment { get; set; }

        public static UserProfile? _instance;
        public UserProfile? GetInstance() 
        {
            return _instance;
        }

        private static readonly object _lock = new object();

        public static UserProfile Instance(int idPeople)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserProfile(idPeople);
                    }
                }
            }
            return _instance;
        }

        private UserProfile(int idPeople)
        {
            User        = new PeopleDataModel();
            Experiment  = new ExperimentDataModel();
            Equipment   = new List<EquipmentDataModel>();
            Reagent     = new List<ReagentExperiment>();
            LoadUserData(idPeople);
        }

        private void LoadUserData(int idPeople)
        {
            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                connection.Open();

                string select = @"select * from PeopleSchema.People psp 
                                    join PeopleSchema.WorkSchedule psw on psp.idWorkSchedule = psw.idWorkSchedule 
                                    left join ExperimentSchema.Experiment ese on ese.idExperiment = psp.idExperiment 
                                    where idPeople = @id";

                SqlCommand command = new SqlCommand(select, connection);
                command.Parameters.AddWithValue("@id", idPeople);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // в теории if не нужен т.к. при авторизации пользователь был обнаружен
                    if (reader.Read())
                    {   // Проверки на повторяемость

                        // Работники 
                        //reader["IdManufacturer"] != DBNull.Value ? Convert.ToInt32(reader["IdManufacturer"]) : 0,
                        User.IdPeople       = reader["idPeople"]     != DBNull.Value ? Convert.ToInt32 (reader["idPeople"])     : 0;
                        User.FirstName      = reader["FirstName"]    != DBNull.Value ? Convert.ToString(reader["FirstName"])    : "";
                        User.MiddleName     = reader["MiddleName"]   != DBNull.Value ? Convert.ToString(reader["MiddleName"])   : "";
                        User.LastName       = reader["LastName"]     != DBNull.Value ? Convert.ToString(reader["LastName"])     : "";
                        User.email          = reader["email"]        != DBNull.Value ? Convert.ToString(reader["email"])        : "";
                        User.Sex            = reader["Sex"]          != DBNull.Value ? Convert.ToString(reader["Sex"])          : "";
                        User.SystemRole     = reader["SystemRole"]   != DBNull.Value ? Convert.ToString(reader["SystemRole"])   : "";
                        User.JobPosition    = reader["JobPosition"]  != DBNull.Value ? Convert.ToString(reader["JobPosition"])  : "";
                        User.Login          = reader["Login"]        != DBNull.Value ? Convert.ToString(reader["Login"])        : "";
                        User.PasswordHash   = reader["PasswordHash"] != DBNull.Value ? Convert.ToString(reader["PasswordHash"]) : "";
                        // Рабочая смена
                        User.IdWorkShedule.idWorkSchedule = reader["idWorkSchedule"] != DBNull.Value ? Convert.ToInt32 (reader["idWorkSchedule"]) : 0;
                        User.IdWorkShedule.WorkShift      = reader["WorkShift"]      != DBNull.Value ? Convert.ToString(reader["WorkShift"])      : "";
                        User.IdWorkShedule.StartTime      = reader["StartTime"] != DBNull.Value ? reader.IsDBNull(reader.GetOrdinal("StartTime")) 
                                                            ? TimeSpan.MinValue : reader.GetTimeSpan(reader.GetOrdinal("StartTime")) : TimeSpan.MinValue;

                        User.IdWorkShedule.EndTime = reader["EndTime"] != DBNull.Value
                            ? reader.IsDBNull(reader.GetOrdinal("EndTime"))
                                ? TimeSpan.MinValue // Если NULL, то назначаем минимальное значение TimeSpan
                                : reader.GetTimeSpan(reader.GetOrdinal("EndTime")) // Иначе берем значение из БД
                            : TimeSpan.MinValue; // Если значение вообще NULL, назначаем минимальное значение TimeSpan

                        // Эксперимент
                        Experiment.idExperiment = reader["idExperiment"] != DBNull.Value ? Convert.ToInt32   (reader["idExperiment"]) : 0;
                        Experiment.Name         = reader["Name"]         != DBNull.Value ? Convert.ToString  (reader["Name"])         : "";
                        Experiment.Description  = reader["Description"]  != DBNull.Value ? Convert.ToString  (reader["Description"])  : "";
                        Experiment.StartDate    = reader["StartDate"]    != DBNull.Value ? Convert.ToDateTime(reader["StartDate"])    : DateTime.MinValue;
                        Experiment.EndDate      = reader["EndDate"]      != DBNull.Value ? Convert.ToDateTime(reader["EndDate"])      : DateTime.MinValue;
                        Experiment.Result       = reader["Result"]       != DBNull.Value ? Convert.ToString  (reader["Result"])       : "";
                        Experiment.Status       = reader["status"]       != DBNull.Value ? Convert.ToString  (reader["status"])       : "";
                    }
                }


                string selectExperimentReagent = "select \r\nrsr.idReagent, re.UseCount, re.mass as UseMass, rsr.Dansity, rsr.ChemicalFormula, rsr.Name as ReagentName\r\nfrom ExperimentSchema.Experiment exse \r\nleft join ReagentExperiment re on re.idExperiment = exse.idExperiment\r\nleft join ReagentSchema.Reagent rsr on re.idReagent = rsr.idReagent\r\nwhere exse.idExperiment = @idExperiment";
                command.Parameters.AddWithValue("@idExperiment", Experiment.idExperiment);

                command.CommandText = selectExperimentReagent;

                using (SqlDataReader experimentReader = command.ExecuteReader())
                {
                    // Обработка второго результата (например, список экспериментов пользователя)
                    while (experimentReader.Read())
                    {
                        var reagent = new ReagentExperiment
                        {
                            UseCount = experimentReader["UseCount"] != DBNull.Value ? Convert.ToInt32(experimentReader["UseCount"])  : 0,
                            Mass     = experimentReader["UseMass"]  != DBNull.Value ? Convert.ToDouble(experimentReader["UseMass"]) : 0,

                            idReagentDataModel = new ReagentManufacturer
                            {
                                //PurityDegree = experimentReader["PurityDegree"] != DBNull.Value ? Convert.ToDecimal(experimentReader["PurityDegree"]) : 0,

                                Reagent = new ReagentDataModel
                                {
                                    idReagent       = experimentReader["idReagent"]       != DBNull.Value ? Convert.ToInt32  (experimentReader["idReagent"])       : 0,
                                    Name            = experimentReader["ReagentName"]     != DBNull.Value ? Convert.ToString (experimentReader["ReagentName"])     : "",
                                    ChemicalFormula = experimentReader["ChemicalFormula"] != DBNull.Value ? Convert.ToString (experimentReader["ChemicalFormula"]) : "",
                                    Dansity         = experimentReader["Dansity"]         != DBNull.Value ? Convert.ToDecimal(experimentReader["Dansity"])         : 0,
                                }
                            }
                        };

                        // Добавить в список экспериментов, если нужно
                        Reagent.Add(reagent);
                    }
                }

                string selectExperimentEquipment = "select \r\n\tese.idEquipment, ese.Name, ese.Model, ese.Description, ese.kind, ese.Status\r\nfrom ExperimentSchema.Experiment exse \r\nleft join ExperimentEquipment ee on ee.idExperiment = exse.idExperiment\r\nleft join EquipmentSchema.Equipment ese on ese.idEquipment = ee.idEquipment\r\nwhere exse.idExperiment = @idEquipmentExperiment";
                command.Parameters.AddWithValue("@idEquipmentExperiment", Experiment.idExperiment);

                command.CommandText = selectExperimentEquipment;

                using (SqlDataReader experimentReader = command.ExecuteReader())
                {
                    // Обработка второго результата (например, список экспериментов пользователя)
                    while (experimentReader.Read())
                    {
                        var reagent = new EquipmentDataModel
                        {
                            idEquipment = experimentReader["idEquipment"] != DBNull.Value ? Convert.ToInt32 (experimentReader["idEquipment"]) : 0,
                            Name        = experimentReader["Name"]        != DBNull.Value ? Convert.ToString(experimentReader["Name"])        : "",
                            Model       = experimentReader["Model"]       != DBNull.Value ? Convert.ToString(experimentReader["Model"])       : "",
                            Description = experimentReader["Description"] != DBNull.Value ? Convert.ToString(experimentReader["Description"]) : "",
                            Kind        = experimentReader["kind"]        != DBNull.Value ? Convert.ToString(experimentReader["kind"])        : "",
                            Status      = experimentReader["Status"]      != DBNull.Value ? Convert.ToString(experimentReader["Status"])      : "",
                        };

                        // Добавить в список экспериментов, если нужно
                        Equipment.Add(reagent);
                    }
                }
            }
        }

        public void Dispose()
        {
            _instance = null;
        }
    }
}


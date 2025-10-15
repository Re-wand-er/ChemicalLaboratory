using ChemicalLaboratory.Models.People;
using Microsoft.Data.SqlClient;

namespace ChemicalLaboratory.Domain.UserServices
{
    public interface IUserService
    {
        PeopleDataModel? ValidateUser(string username, string password);
    }

    public class UserService : IUserService
    {
        /*private readonly YourDbContext _context;

        public UserService(YourDbContext context)
        {
            _context = context;
        }*/

        public PeopleDataModel? ValidateUser(string Login, string Password)
        {
            PeopleDataModel user = new PeopleDataModel();

            if (Login == "" || Password == "")
                return null;

            using (SqlConnection connection = new SqlConnection(SQLCommand.connectionString))
            {
                connection.Open();

                var select = "select * from Authorising(@Login, @Password)";
                SqlCommand command = new SqlCommand(select, connection);
                command.Parameters.AddWithValue("@Login", Login); // ToString может быть излишним
                command.Parameters.AddWithValue("@Password", Password/*.GetHashCode()*/.ToString());

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Кроме логина да пароля вроде ничего и не надо
                        user.IdPeople       = reader.GetInt32(0);
                        user.idExperiment   = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        user.idWorkShedule  = reader.GetInt32(2);
                        user.FirstName      = reader.GetString(3);
                        user.MiddleName     = reader.GetString(4);
                        user.LastName       = reader.GetString(5);
                        user.Email          = reader.GetString(6);
                        user.Sex            = reader.GetString(7);
                        user.SystemRole     = reader.GetString(8);
                        user.JobPosition    = reader.GetString(9);
                        user.Login          = reader.GetString(10);
                        user.Password   = reader.GetString(11);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return user;
        }

    }
}

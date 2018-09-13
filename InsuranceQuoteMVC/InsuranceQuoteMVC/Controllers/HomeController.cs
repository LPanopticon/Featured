using InsuranceQuoteMVC.Models;
using InsuranceQuoteMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceQuoteMVC.Controllers
{
    public class HomeController : Controller
    {
       private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Autoquote;Integrated Security=True;
                                                  Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                                  ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //nothing crazy here, brings up the user input index page
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear,
            string carMake, string carModel, int speedingTickets, string dUI, string fullCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress)
                || string.IsNullOrEmpty(Convert.ToString(dateOfBirth)) || string.IsNullOrEmpty(Convert.ToString(carYear))
                || string.IsNullOrEmpty(carMake) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(Convert.ToString(speedingTickets))
                || string.IsNullOrEmpty(dUI) || string.IsNullOrEmpty(fullCoverage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {


                string queryString = @"INSERT INTO QuoteInfo (firstName, LastName, EmailAddress, DateOfBirth, CarYear, CarMake,CarModel, SpeedingTickets, DUI, FullCoverage, QuoteTotal)
                                      VALUES (@FirstName, @LastName, @EmailAddress, @DateOfBirth, @CarYear, @Carmake, @CarModel, @SpeedingTickets, @DUI, @FullCoverage, @QuoteTotal)";
                //quote calculations begin here.
                int userAge = Convert.ToInt32(DateTime.Now.Year - dateOfBirth.Year);
                decimal AgeVal;
                decimal YearVal;
                decimal MakeVal;
                decimal DUIVal;
                decimal QuoteTotal;
                
                if (userAge > 18 && userAge < 25 || (userAge > 100))
                {
                     AgeVal = 25;
                }
                else if (userAge < 18)
                {
                     AgeVal = 100;
                }
                else { AgeVal = 0; }

                if (carYear < 2000 || carYear > 2015)
                {
                    YearVal = 25;
                }
                else { YearVal = 0; }
                if (carMake == "Porsche" && carModel == "911 Carrera")
                {
                    MakeVal = 50;
                }
                else if (carMake == "Porsche")
                {
                    MakeVal = 25;
                }
                else { MakeVal = 0; }
                decimal SpeedVal = speedingTickets * 10;

                if (dUI == "Yes")
                {
                     DUIVal = (50 + AgeVal + YearVal + MakeVal + SpeedVal) * 1.25m;
                }
                else { DUIVal = 50 + AgeVal + YearVal + MakeVal + SpeedVal; }
                
                if (fullCoverage == "Yes")
                {
                    QuoteTotal = DUIVal * 1.5m;
                }
                else
                {
                    QuoteTotal = DUIVal;
                }               
                //Here the quotees end here, no need for QuoteTotal before now.
                    
                using (SqlConnection connection = new SqlConnection(connectionString))// If this works then we know I got things right.
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                    command.Parameters.Add("@DateOfBirth", SqlDbType.Date);
                    command.Parameters.Add("@CarYear", SqlDbType.Int);
                    command.Parameters.Add("@CarMake", SqlDbType.VarChar);
                    command.Parameters.Add("@CarModel", SqlDbType.VarChar);
                    command.Parameters.Add("@SpeedingTickets", SqlDbType.Int);
                    command.Parameters.Add("@DUI", SqlDbType.VarChar);
                    command.Parameters.Add("@FullCoverage", SqlDbType.VarChar);
                    command.Parameters.Add("@QuoteTotal", SqlDbType.Int);

                    command.Parameters["@FirstName"].Value = firstName;
                    command.Parameters["@LastName"].Value = lastName;
                    command.Parameters["@EmailAddress"].Value = emailAddress;
                    command.Parameters["@DateOfBirth"].Value = dateOfBirth;
                    command.Parameters["@CarYear"].Value = carYear;
                    command.Parameters["@CarMake"].Value = carMake;
                    command.Parameters["@CarModel"].Value = carModel;
                    command.Parameters["@SpeedingTickets"].Value = speedingTickets;
                    command.Parameters["@DUI"].Value = dUI;
                    command.Parameters["@FullCoverage"].Value = fullCoverage;
                    command.Parameters["@QuoteTotal"].Value = QuoteTotal;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                    return View("Success");
            }

        }
                
       public ActionResult Admin()// only need Id in the Admin View, so it is only here and passed into ResultVM
        {
            string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, DateOfBirth, CarYear, CarMake, CarModel,
                                SpeedingTickets, DUI, FullCoverage, QuoteTotal FROM QuoteInfo";
            List<QuoteRequest> requests = new List<QuoteRequest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var request = new QuoteRequest();
                    request.Id = Convert.ToInt32(reader["Id"]);
                    request.FirstName = reader["FirstName"].ToString();
                    request.LastName = Convert.ToString(reader["LastName"]);
                    request.EmailAddress = reader["EmailAddress"].ToString();
                    request.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    request.CarYear = Convert.ToInt32(reader["CarYear"]);
                    request.CarMake = reader["CarMake"].ToString();
                    request.CarModel = reader["CarModel"].ToString();
                    request.SpeedingTickets = Convert.ToInt32(reader["SpeedingTickets"]);
                    request.DUI = reader["DUI"].ToString();
                    request.FullCoverage = reader["FullCoverage"].ToString();
                    request.QuoteTotal = Convert.ToInt32(reader["QuoteTotal"]);
                    requests.Add(request);


                }

            }

            var resultVMs = new List<ResultVM>();
            foreach (var request in requests)
            {
                var resultVM = new ResultVM();
                resultVM.Id = request.Id;
                resultVM.FirstName = request.FirstName;
                resultVM.LastName = request.LastName;
                resultVM.EmailAddress = request.EmailAddress;
                resultVM.QuoteTotal = request.QuoteTotal;
                resultVMs.Add(resultVM);



            }
                return View(resultVMs);
        }
    }
}
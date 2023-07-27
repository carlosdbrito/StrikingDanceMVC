using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StrikingDanceMVC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace StrikingDanceMVC.Controllers
{
    public class CustomerController : Controller
    {
                
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        
        //string connectionString = "Data Source=localhost, 1433;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Itacolomi1%;";

        // GET: CustomerController
        [HttpGet]
        public ActionResult Index()
        {
            
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("DataBase");

            using (SqlConnection con = new SqlConnection(connectionString))
            { 
                con.Open();
                //SqlCommand cmd = con.CreateCommand();
                string query = "SELECT Id, Nome, Email, CONVERT(VARCHAR(10), Nascimento, 103) AS Nascimento, DataInclusao FROM StrinkingDance..Customer with (NOLOCK)";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dt);
            }

            return View(dt);
        }

            

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View( new CustomerModel());
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerModel customerModel)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return View();
                //return View("Index", ModelState);
            }

            string connectionString = _configuration.GetConnectionString("DataBase");
            using (SqlConnection con = new SqlConnection(connectionString)) 
            { 
                con.Open();                
                string query = "INSERT INTO StrinkingDance..Customer (Nome, Email, Nascimento) VALUES (@Nome, @Email, @Nascimento)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Nome", customerModel.Nome);
                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                cmd.Parameters.AddWithValue("@Nascimento", customerModel.Nascimento);                
                cmd.ExecuteNonQuery();
            }

                return RedirectToAction(nameof(Index));
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return View();
                //return View("Index", ModelState);
            }

            CustomerModel customerModel = new CustomerModel();
            DataTable dt = new DataTable();
            string connectionString = _configuration.GetConnectionString("DataBase");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, Nome, Email, Nascimento FROM StrinkingDance..Customer with (NOLOCK) WHERE Id = @Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query,con);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);
                adapter.Fill(dt);

                if(dt.Rows.Count == 1)
                {
                    customerModel.Id = Convert.ToInt32(dt.Rows[0][0]);
                    customerModel.Nome = dt.Rows[0][1].ToString();
                    customerModel.Email = dt.Rows[0][2].ToString();
                    customerModel.Nascimento = Convert.ToDateTime(dt.Rows[0][3]);
                    return View(customerModel);
                }
                else return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel customerModel)
        {
            string connectionString = _configuration.GetConnectionString("DataBase");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE StrinkingDance..Customer SET Nome = @Nome, Email = @Email, Nascimento = @Nascimento, DataInclusao = GetDate() WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", customerModel.Id);
                cmd.Parameters.AddWithValue("@Nome", customerModel.Nome);
                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                cmd.Parameters.AddWithValue("@Nascimento", customerModel.Nascimento);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            CustomerModel customerModel = new CustomerModel();
            DataTable dt = new DataTable();

            string connectionString = _configuration.GetConnectionString("DataBase");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id, Nome, Email, Nascimento FROM StrinkingDance..Customer with (NOLOCK) WHERE Id = @Id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);
                adapter.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    customerModel.Id = Convert.ToInt32(dt.Rows[0][0]);
                    customerModel.Nome = dt.Rows[0][1].ToString();
                    customerModel.Email = dt.Rows[0][2].ToString();
                    customerModel.Nascimento = Convert.ToDateTime(dt.Rows[0][3]);
                    return View(customerModel);
                }
                else return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerModel customerModel)
        {
            string connectionString = _configuration.GetConnectionString("DataBase");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM StrinkingDance..Customer WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", customerModel.Id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

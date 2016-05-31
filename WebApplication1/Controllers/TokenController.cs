using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TokenController : ApiController
    {
        [HttpGet]
        public IHttpActionResult removeAll(string useremail)
        {
            using (SqlConnection connection =
            new SqlConnection(HomeController.connectString))
            {
                // Create the Command and Parameter objects.
                string queryString = "DELETE FROM dbo.mTable WHERE useremail=@useremail";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@useremail", useremail);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult removeWithId(string id)
        {
            using (SqlConnection connection =
            new SqlConnection(HomeController.connectString))
            {
                // Create the Command and Parameter objects.
                string queryString = "DELETE FROM dbo.mTable WHERE id=@id";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult addtoken(WebApplication1.Models.AccountInfo.SubAccount account)
        {
            if (addTokenInternal(account))
                return Ok();
            return NotFound();
        }

        public static bool addTokenInternal(WebApplication1.Models.AccountInfo.SubAccount account)
        {
            using (SqlConnection connection =
            new SqlConnection(HomeController.connectString))
            {
                // Create the Command and Parameter objects.
                string queryString = "insert into mTable values (@useremail,@provider,@token,@userid, NEWID())";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@useremail", account.useremail);
                command.Parameters.AddWithValue("@token", account.token);
                command.Parameters.AddWithValue("@userid", account.userid);
                command.Parameters.AddWithValue("@provider", account.provider);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }

        [HttpGet]
        public IHttpActionResult getAll(string useremail)
        {
            return Json(getAllInternal(useremail));
        }

        public static AccountInfo getAllInternal(string useremail) {
            var ret = new AccountInfo();
            ret.useremail = useremail;
            ret.accountlist = new List<AccountInfo.SubAccount>();
            using (SqlConnection connection =
            new SqlConnection(HomeController.connectString))
            {
                // Create the Command and Parameter objects.
                string queryString = "select * FROM dbo.mTable WHERE useremail=@useremail";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@useremail", useremail);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var tp = new WebApplication1.Models.AccountInfo.SubAccount();
                    tp.provider = reader.GetString(1);
                    tp.token = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                    {
                        tp.userid = reader.GetString(3);
                    }

                    tp.id = reader.GetString(4);
                    ret.accountlist.Add(tp);
                }
                reader.Close();
            }

            return ret;
        }
    }
}

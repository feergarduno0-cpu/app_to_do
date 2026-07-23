using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using app_to_do.Models;

namespace app_to_do.Controllers
{
    public class UsuarioController : Controller
    {
        // Cadena de conexión estándar para XAMPP
        private string connectionString = "Server=localhost;Database=tbl_usuarios;Uid=root;Pwd=;";

        // 1. CONSULTAR / LISTAR (Read)
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, user, pwd FROM tbl_usuarios";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new UsuarioModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                User = reader["user"].ToString(),
                                Pwd = reader["pwd"].ToString()
                            });
                        }
                    }
                }
            }
            return View(usuarios);
        }

        // 2. ALTA - Vista
        public IActionResult Create() => View();

        // 2. ALTA - Guardar en BD MySQL
        [HttpPost]
        public IActionResult Create(string user, string pwd)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tbl_usuarios (user, pwd) VALUES (@user, @pwd)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        // 3. CAMBIO - Vista
        public IActionResult Edit(int id)
        {
            UsuarioModel? usuario = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, user, pwd FROM tbl_usuarios WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new UsuarioModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                User = reader["user"].ToString(),
                                Pwd = reader["pwd"].ToString()
                            };
                        }
                    }
                }
            }
            return View(usuario);
        }

        // 3. CAMBIO - Guardar cambios en BD MySQL
        [HttpPost]
        public IActionResult Edit(int id, string user, string pwd)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tbl_usuarios SET user = @user, pwd = @pwd WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        // 4. BAJA - Eliminar de BD MySQL
        public IActionResult Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM tbl_usuarios WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}